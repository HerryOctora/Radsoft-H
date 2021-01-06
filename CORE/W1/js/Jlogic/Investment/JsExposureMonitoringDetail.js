$(document).ready(function () {
    document.title = 'FORM Exposure Monitoring Detail';
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

    if (_GlobClientCode == "20") {
        $("#LblDate").hide();
        $("#LblInputDate").hide();
        $("#LblFormatDate").hide();
    }
    else {
        $("#LblDate").show();
        $("#LblInputDate").show();
        $("#LblFormatDate").show();
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
    }

    function initWindow() {


        $("#FilterStatusExposure").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Done", value: 1 },
                { text: "On Progress", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeFilterStatusExposure,
            index: 1
        });
        function OnChangeFilterStatusExposure() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            refresh();
        }




    

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDate,
            value: new Date(),
        });

        function OnChangeDate() {
            var _Date = Date.parse($("#Date").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_Date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#Date").data("kendoDatePicker").value(new Date());
                return;
            }
            refresh();
        }


        $("#TanggalPelanggaran").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeTanggalPelanggaran,
        });

        function OnChangeTanggalPelanggaran() {
            var _date = Date.parse($("#TanggalPelanggaran").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            //else {
            //    $.ajax({
            //        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            //        type: 'GET',
            //        contentType: "application/json;charset=utf-8",
            //        success: function (data) {
            //            if (data == true) {

            //                alertify.alert("Date is Holiday, Please Insert Date Correctly!")
            //            }
            //        },
            //        error: function (data) {
            //            alertify.alert(data.responseText);
            //        }
            //    });
            //}

        }

        $("#TanggalSurat").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeTanggalSurat,
        });

        function OnChangeTanggalSurat() {
            var _date = Date.parse($("#TanggalSurat").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            //else {
            //    $.ajax({
            //        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            //        type: 'GET',
            //        contentType: "application/json;charset=utf-8",
            //        success: function (data) {
            //            if (data == true) {

            //                alertify.alert("Date is Holiday, Please Insert Date Correctly!")
            //            }
            //        },
            //        error: function (data) {
            //            alertify.alert(data.responseText);
            //        }
            //    });
            //}

        }

        $("#TanggalTerimaSurat").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeTanggalTerimaSurat,
        });

        function OnChangeTanggalTerimaSurat() {
            var _date = Date.parse($("#TanggalTerimaSurat").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            //else {
            //    $.ajax({
            //        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            //        type: 'GET',
            //        contentType: "application/json;charset=utf-8",
            //        success: function (data) {
            //            if (data == true) {

            //                alertify.alert("Date is Holiday, Please Insert Date Correctly!")
            //            }
            //        },
            //        error: function (data) {
            //            alertify.alert(data.responseText);
            //        }
            //    });
            //}

        }

        $("#ExDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeExDate,
        });

        function OnChangeExDate() {
            var _date = Date.parse($("#ExDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            //else {
            //    $.ajax({
            //        url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            //        type: 'GET',
            //        contentType: "application/json;charset=utf-8",
            //        success: function (data) {
            //            if (data == true) {

            //                alertify.alert("Date is Holiday, Please Insert Date Correctly!")
            //            }
            //        },
            //        error: function (data) {
            //            alertify.alert(data.responseText);
            //        }
            //    });
            //}

        }


        win = $("#WinExposureMonitoringDetail").kendoWindow({
            height: 830,
            title: "Exposure Monitoring Detail",
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

    var GlobValidator = $("#WinExposureMonitoringDetail").kendoValidator().data("kendoValidator");

    function validateData() {


        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
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
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
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

            $("#ExposureMonitoringDetailPK").val(dataItemX.ExposureMonitoringDetailPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#BankCustodian").val(dataItemX.BankCustodian);
            $("#KebijakanInvestasi").val(dataItemX.KebijakanInvestasi);
            $("#Exposure").val(dataItemX.Exposure);
            $("#TanggalPelanggaran").data("kendoDatePicker").value(kendo.parseDate(dataItemX.TanggalPelanggaran), 'dd/MMM/yyyy');
            $("#Batasan").val(dataItemX.Batasan);
            $("#NoSurat").val(dataItemX.NoSurat);
            $("#TanggalSurat").data("kendoDatePicker").value(kendo.parseDate(dataItemX.TanggalSurat), 'dd/MMM/yyyy');
            $("#TanggalTerimaSurat").data("kendoDatePicker").value(kendo.parseDate(dataItemX.TanggalTerimaSurat), 'dd/MMM/yyyy');
            $("#ExDate").data("kendoDatePicker").value(kendo.parseDate(dataItemX.ExDate), 'dd/MMM/yyyy');
            $("#Remarks").val(dataItemX.Remarks);
           

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

        $("#FundPK").attr('readonly', true);
        $("#BankCustodian").attr('readonly', true);
        $("#KebijakanInvestasi").attr('readonly', true);
        $("#Exposure").attr('readonly', true);
        $("#TanggalPelanggaran").attr('readonly', true);
        $("#Batasan").attr('readonly', true);
        $("#ExDate").attr('readonly', true);
        $("#StatusExposure").attr('readonly', true);


        //ComboBoxFundPK
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

        $("#Remedy").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "10 Days", value: 1 },
                { text: "20 Days", value: 2 },
                { text: "40 Days", value: 3 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeRemedy,
            value: setCmbRemedy()
        });
        function OnChangeRemedy() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                var _days = 0;
                if ($("#Remedy").val() == 1)
                    _days = 10;
                else if ($("#Remedy").val() == 2)
                    _days = 20;
                else if ($("#Remedy").val() == 3)
                    _days = 40;

                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#TanggalSurat").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _days,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#ExDate").data("kendoDatePicker").value(kendo.parseDate(data), 'dd/MMM/yyyy');

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        }
       
        function setCmbRemedy() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Remedy;
            }
        }


        $("#StatusExposure").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Done", value: 1 },
                { text: "On Progress", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatusExposure,
            value: setCmbStatusExposure()
        });
        function OnChangeStatusExposure() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbStatusExposure() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.StatusExposure;
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
        $("#ExposureMonitoringDetailPK").val("");
        $("#HistoryPK").val("");
        $("#FundPK").val("");
        $("#BankCustodian").val("");
        $("#KebijakanInvestasi").val("");
        $("#Exposure").val("");
        $("#TanggalPelanggaran").data("kendoDatePicker").value(null);
        $("#Batasan").val("");
        $("#NoSurat").val("");
        $("#TanggalSurat").data("kendoDatePicker").value(null);
        $("#TanggalTerimaSurat").data("kendoDatePicker").value(null);
        $("#Remedy").val("");
        $("#ExDate").data("kendoDatePicker").value(null);
        $("#StatusExposure").val("");
        $("#Remarks").val("");

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
                             ExposureMonitoringDetailPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Date: {type:"date"},
                             FundPK: { type: "number" },
                             FundName: { type: "string" },
                             BankCustodian: { type: "string" },
                             KebijakanInvestasi: { type: "string" },
                             Exposure: { type: "string" },
                             TanggalPelanggaran: { type: "date" },
                             Batasan: { type: "string" },
                             NoSurat: { type: "string" },
                             TanggalSurat: { type: "date" },
                             TanggalTerimaSurat: { type: "date" },
                             Remedy: { type: "number" },
                             ExDate: { type: "date" },
                             StatusExposure: { type: "number" },
                             Remarks: { type: "string" },

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

        if ($("#FilterStatusExposure").val() == "") {
            _statusExposure = 1;
        }
        else {
            _statusExposure = $("#FilterStatusExposure").val();
        }


        if (tabindex == undefined || tabindex == 0) {
            var newDS = getDataSource(window.location.origin + "/Radsoft/ExposureMonitoringDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _statusExposure);
            $("#gridExposureMonitoringDetailApproved").data("kendoGrid").setDataSource(newDS);
            //initGrid();
        }
        if (tabindex == 1) {
            var newDS = getDataSource(window.location.origin + "/Radsoft/ExposureMonitoringDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _statusExposure);
            $("#gridExposureMonitoringDetailPending").data("kendoGrid").setDataSource(newDS);
            //initGrid();
        }
        if (tabindex == 2) {
            var newDS = getDataSource(window.location.origin + "/Radsoft/ExposureMonitoringDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _statusExposure);
            $("#gridExposureMonitoringDetailHistory").data("kendoGrid").setDataSource(newDS);
            //initGrid();
        }
    }

    function initGrid() {
        if ($("#Date").val() == null || $("#Date").val() == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        if ($("#FilterStatusExposure").val() == "") {
            _statusExposure = 1;
        }
        else {
            _statusExposure = $("#FilterStatusExposure").val();
        }


        var ExposureMonitoringDetailApprovedURL = window.location.origin + "/Radsoft/ExposureMonitoringDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _statusExposure,
            dataSourceApproved = getDataSource(ExposureMonitoringDetailApprovedURL);
        if (_GlobClientCode == "20") {
            $("#gridExposureMonitoringDetailApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Exposure Monitoring Detail"
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
                    { field: "ExposureMonitoringDetailPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundName", title: "Fund", width: 200 },
                    { field: "BankCustodian", title: "Bank Custodian", width: 200 },
                    { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200 },
                    { field: "Exposure", title: "Exposure", width: 200 },
                    { field: "TanggalPelanggaran", title: "Tanggal Pelanggaran", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "Batasan", title: "Batasan", width: 200 },
                    { field: "NoSurat", title: "No. Surat", width: 200 },
                    { field: "TanggalSurat", title: "Tanggal Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "TanggalTerimaSurat", title: "Tanggal Terima Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "RemedyDesc", title: "Remedy", width: 200 },
                    { field: "ExDate", title: "ExDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "StatusExposure", title: "StatusExposure", width: 200 },
                    { field: "Remarks", title: "Remarks", width: 200 },

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
        else {
            $("#gridExposureMonitoringDetailApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Exposure Monitoring Detail"
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
                    { field: "ExposureMonitoringDetailPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "FundName", title: "Fund", width: 200 },
                    { field: "BankCustodian", title: "Bank Custodian", width: 200 },
                    { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200 },
                    { field: "Exposure", title: "Exposure", width: 200 },
                    { field: "TanggalPelanggaran", title: "Tanggal Pelanggaran", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "Batasan", title: "Batasan", width: 200 },
                    { field: "NoSurat", title: "No. Surat", width: 200 },
                    { field: "TanggalSurat", title: "Tanggal Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "TanggalTerimaSurat", title: "Tanggal Terima Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "Remedy", title: "Remedy", width: 200 },
                    { field: "ExDate", title: "ExDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                    { field: "StatusExposure", title: "StatusExposure", width: 200 },
                    { field: "Remarks", title: "Remarks", width: 200 },

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


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabExposureMonitoringDetail").kendoTabStrip({
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

                        var ExposureMonitoringDetailPendingURL = window.location.origin + "/Radsoft/ExposureMonitoringDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _statusExposure,
                            dataSourcePending = getDataSource(ExposureMonitoringDetailPendingURL);
                        if (_GlobClientCode == "20") {
                            $("#gridExposureMonitoringDetailPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Exposure Monitoring Detail"
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
                                    { field: "ExposureMonitoringDetailPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundName", title: "Fund", width: 200 },
                                    { field: "BankCustodian", title: "Bank Custodian", width: 200 },
                                    { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200 },
                                    { field: "Exposure", title: "Exposure", width: 200 },
                                    { field: "TanggalPelanggaran", title: "Tanggal Pelanggaran", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "Batasan", title: "Batasan", width: 200 },
                                    { field: "NoSurat", title: "No. Surat", width: 200 },
                                    { field: "TanggalSurat", title: "Tanggal Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "TanggalTerimaSurat", title: "Tanggal Terima Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "RemedyDesc", title: "Remedy", width: 200 },
                                    { field: "ExDate", title: "ExDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "StatusExposure", title: "StatusExposure", width: 200 },
                                    { field: "Remarks", title: "Remarks", width: 200 },

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
                        else {
                            $("#gridExposureMonitoringDetailPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Exposure Monitoring Detail"
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
                                    { field: "ExposureMonitoringDetailPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundName", title: "Fund", width: 200 },
                                    { field: "BankCustodian", title: "Bank Custodian", width: 200 },
                                    { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200 },
                                    { field: "Exposure", title: "Exposure", width: 200 },
                                    { field: "TanggalPelanggaran", title: "Tanggal Pelanggaran", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "Batasan", title: "Batasan", width: 200 },
                                    { field: "NoSurat", title: "No. Surat", width: 200 },
                                    { field: "TanggalSurat", title: "Tanggal Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "TanggalTerimaSurat", title: "Tanggal Terima Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "Remedy", title: "Remedy", width: 200 },
                                    { field: "ExDate", title: "ExDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "StatusExposure", title: "StatusExposure", width: 200 },
                                    { field: "Remarks", title: "Remarks", width: 200 },

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

                    }
                    if (tabindex == 2) {

                        var ExposureMonitoringDetailHistoryURL = window.location.origin + "/Radsoft/ExposureMonitoringDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _statusExposure,
                            dataSourceHistory = getDataSource(ExposureMonitoringDetailHistoryURL);
                        if (_GlobClientCode == "20") {
                            $("#gridExposureMonitoringDetailHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Exposure Monitoring Detail"
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
                                    { field: "ExposureMonitoringDetailPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundName", title: "Fund", width: 200 },
                                    { field: "BankCustodian", title: "Bank Custodian", width: 200 },
                                    { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200 },
                                    { field: "Exposure", title: "Exposure", width: 200 },
                                    { field: "TanggalPelanggaran", title: "Tanggal Pelanggaran", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "Batasan", title: "Batasan", width: 200 },
                                    { field: "NoSurat", title: "No. Surat", width: 200 },
                                    { field: "TanggalSurat", title: "Tanggal Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "TanggalTerimaSurat", title: "Tanggal Terima Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "RemedyDesc", title: "Remedy", width: 200 },
                                    { field: "ExDate", title: "ExDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "StatusExposure", title: "StatusExposure", width: 200 },
                                    { field: "Remarks", title: "Remarks", width: 200 },

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
                        else {
                            $("#gridExposureMonitoringDetailHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Exposure Monitoring Detail"
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
                                    { field: "ExposureMonitoringDetailPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "FundName", title: "Fund", width: 200 },
                                    { field: "BankCustodian", title: "Bank Custodian", width: 200 },
                                    { field: "KebijakanInvestasi", title: "Kebijakan Investasi", width: 200 },
                                    { field: "Exposure", title: "Exposure", width: 200 },
                                    { field: "TanggalPelanggaran", title: "Tanggal Pelanggaran", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "Batasan", title: "Batasan", width: 200 },
                                    { field: "NoSurat", title: "No. Surat", width: 200 },
                                    { field: "TanggalSurat", title: "Tanggal Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "TanggalTerimaSurat", title: "Tanggal Terima Surat", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "Remedy", title: "Remedy", width: 200 },
                                    { field: "ExDate", title: "ExDate", format: "{0:dd/MMM/yyyy}", width: 200 },
                                    { field: "StatusExposure", title: "StatusExposure", width: 200 },
                                    { field: "Remarks", title: "Remarks", width: 200 },

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

                    }
                } else {
                    refresh();
                }
            }
        });
    }

    function gridHistoryDataBound() {
        var grid = $("#gridExposureMonitoringDetailHistory").data("kendoGrid");
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
                    var ExposureMonitoringDetail = {
                        FundPK: $('#FundPK').val(),
                        BankCustodian: $('#BankCustodian').val(),
                        KebijakanInvestasi: $('#KebijakanInvestasi').val(),
                        Exposure: $('#Exposure').val(),
                        TanggalPelanggaran: $('#TanggalPelanggaran').val(),
                        Batasan: $('#Batasan').val(),
                        NoSurat: $('#NoSurat').val(),
                        TanggalSurat: $('#TanggalSurat').val(),
                        TanggalTerimaSurat: $('#TanggalTerimaSurat').val(),
                        Remedy: $('#Remedy').val(),
                        ExDate: $('#ExDate').val(),
                        StatusExposure: $('#StatusExposure').val(),
                        Remarks: $('#Remarks').val(),


                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/ExposureMonitoringDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ExposureMonitoringDetail_I",
                        type: 'POST',
                        data: JSON.stringify(ExposureMonitoringDetail),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExposureMonitoringDetailPK").val() + "/" + $("#HistoryPK").val() + "/" + "ExposureMonitoringDetail",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var ExposureMonitoringDetail = {
                                    ExposureMonitoringDetailPK: $('#ExposureMonitoringDetailPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundPK: $('#FundPK').val(),
                                    BankCustodian: $('#BankCustodian').val(),
                                    KebijakanInvestasi: $('#KebijakanInvestasi').val(),
                                    Exposure: $('#Exposure').val(),
                                    TanggalPelanggaran: $('#TanggalPelanggaran').val(),
                                    Batasan: $('#Batasan').val(),
                                    NoSurat: $('#NoSurat').val(),
                                    TanggalSurat: $('#TanggalSurat').val(),
                                    TanggalTerimaSurat: $('#TanggalTerimaSurat').val(),
                                    Remedy: $('#Remedy').val(),
                                    ExDate: $('#ExDate').val(),
                                    StatusExposure: $('#StatusExposure').val(),
                                    Remarks: $('#Remarks').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/ExposureMonitoringDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ExposureMonitoringDetail_U",
                                    type: 'POST',
                                    data: JSON.stringify(ExposureMonitoringDetail),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExposureMonitoringDetailPK").val() + "/" + $("#HistoryPK").val() + "/" + "ExposureMonitoringDetail",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "ExposureMonitoringDetail" + "/" + $("#ExposureMonitoringDetailPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExposureMonitoringDetailPK").val() + "/" + $("#HistoryPK").val() + "/" + "ExposureMonitoringDetail",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ExposureMonitoringDetail = {
                                ExposureMonitoringDetailPK: $('#ExposureMonitoringDetailPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ExposureMonitoringDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ExposureMonitoringDetail_A",
                                type: 'POST',
                                data: JSON.stringify(ExposureMonitoringDetail),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExposureMonitoringDetailPK").val() + "/" + $("#HistoryPK").val() + "/" + "ExposureMonitoringDetail",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ExposureMonitoringDetail = {
                                ExposureMonitoringDetailPK: $('#ExposureMonitoringDetailPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ExposureMonitoringDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ExposureMonitoringDetail_V",
                                type: 'POST',
                                data: JSON.stringify(ExposureMonitoringDetail),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ExposureMonitoringDetailPK").val() + "/" + $("#HistoryPK").val() + "/" + "ExposureMonitoringDetail",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ExposureMonitoringDetail = {
                                ExposureMonitoringDetailPK: $('#ExposureMonitoringDetailPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ExposureMonitoringDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ExposureMonitoringDetail_R",
                                type: 'POST',
                                data: JSON.stringify(ExposureMonitoringDetail),
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
});