$(document).ready(function () {
    document.title = 'FORM EMPLOYEE';
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

    if (_GlobClientCode == "03") {
        $("#LblNamaLisence").show();
        $("#LblExpiredDateLisence").show();
        $("#LblStatusLisence").show();
        $("#LblBitReportToOJK").show();
        $("#BtnEmployeeReport").show();

    }
    else {
        $("#LblNamaLisence").hide();
        $("#LblExpiredDateLisence").hide();
        $("#LblStatusLisence").hide();
        $("#LblBitReportToOJK").hide();
        $("#BtnEmployeeReport").hide();
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
        $("#BtnGenerateEmployeeText").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnEmployeeReport").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
    }


    function initWindow() {
        $("#SKDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date()
        });

        $("#SKExpiredDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date()
        });

        $("#TanggalLahir").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#DateOfWork").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date()
        });

        $("#ExpiredDateLisence").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        win = $("#WinEmployee").kendoWindow({
            height: 550,
            title: "Employee Detail",
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
      
    var GlobValidator = $("#WinEmployee").kendoValidator().data("kendoValidator");

    function validateData() {
        
        
        //if ($("#SKDate").val() != "" || $("#DateOfWork").val() != "") {
        //    var _sKDate = Date.parse($("#SKDate").data("kendoDatePicker").value());
        //    var _dateOfWork = Date.parse($("#DateOfWork").data("kendoDatePicker").value());
        //    //Check if Date parse is successful
        //    if (!_sKDate || !_dateOfWork) {
        //        
        //        alertify.alert("Wrong Format Date DD/MM/YYYY");
        //        return 0;
        //    }
        //}
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

            $("#EmployeePK").val(dataItemX.EmployeePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Name").val(dataItemX.Name);
            $("#SKNumber").val(dataItemX.SKNumber);
            if (dataItemX.SKDate == 'Mon Jan 01 1900 00:00:00 GMT+0700 (SE Asia Standard Time)') {
                $("#SKDate").data("kendoDatePicker").value("");
            } else {
                $("#SKDate").data("kendoDatePicker").value(new Date(dataItemX.SKDate));
            }
            $("#TanggalLahir").data("kendoDatePicker").value(dataItemX.TanggalLahir);
            $("#NIK").val(dataItemX.NIK);
            $("#NPWP").val(dataItemX.NPWP);
            $("#AlamatSesuaiKTP").val(dataItemX.AlamatSesuaiKTP);
            $("#AlamatDomisili").val(dataItemX.AlamatDomisili);
            $("#NoBPJSKetenagakerjaan").val(dataItemX.NoBPJSKetenagakerjaan);
            $("#NoBPJSKesehatan").val(dataItemX.NoBPJSKesehatan);
            $("#NoRekTabungan").val(dataItemX.NoRekTabungan);
            $("#BranchCode").val(dataItemX.BranchCode);
            //$("#SKDate").data("kendoDatePicker").value(dataItemX.SKDate);
            $("#DateOfWork").data("kendoDatePicker").value(dataItemX.DateOfWork);
            $("#SKExpiredDate").data("kendoDatePicker").value(new Date(dataItemX.SKExpiredDate));
            $("#SubRedempCode").val(dataItemX.SubRedempCode);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

            $("#NamaLisence").val(dataItemX.NamaLisence);
            $("#ExpiredDateLisence").val(kendo.toString(kendo.parseDate(dataItemX.ExpiredDateLisence), 'dd/MMM/yyyy'));
            $("#BitReportToOJK").prop('checked', dataItemX.BitReportToOJK);
        }

        //combo box SKFormat

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FormatSK",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SKFormat").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeSKFormat,
                    value: setCmbSKFormat()
                });
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
        function OnChangeSKFormat() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSKFormat() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SKFormat == 0) {
                    return "";
                } else {
                    return dataItemX.SKFormat;
                }
            }
        }
        //combo box Jabatan
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Position",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Position").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangePosition,
                    value: setCmbPosition()
                });
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
        function OnChangePosition() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPosition() {
            if (e == null) {
                return "";
            } else {

                return dataItemX.Position;

            }
        }


        ////combo box Branch Code//
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BranchCode",
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#BranchCode").kendoComboBox({
        //            dataValueField: "Code",
        //            dataTextField: "DescOne",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            change: OnChangeBranchCodeType,
        //            value: setCmbBranchCodeType()
        //        });
        //    },
        //    error: function (data) {
        //        alert(data.responseText);
        //    }
        //});
        //function OnChangeBranchCodeType() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}
        //function setCmbBranchCodeType() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        return dataItemX.BranchCode;
        //    }
        //}

        //combo box Chief Code//
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CEOSigning",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ChiefCode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeChiefCodeType,
                    value: setCmbChiefCodeType()
                });
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
        function OnChangeChiefCodeType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbChiefCodeType() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.ChiefCode;

            }
        }

        $("#JenisKelamin").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Laki - Laki", value: 1 },
                { text: "Perempuan", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeJenisKelamin,
            value: setCmbJenisKelamin()
        });
        function OnChangeJenisKelamin() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbJenisKelamin() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.JenisKelamin;
            }
        }

        $("#StatusPerkawinan").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Belum Kawin", value: 1 },
                { text: "Kawin", value: 2 },
                { text: "Lainnya", value: 3 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatusPerkawinan,
            value: setCmbStatusPerkawinan()
        });
        function OnChangeStatusPerkawinan() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbStatusPerkawinan() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.StatusPerkawinan;
            }
        }

        $("#JumlahAnak").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setJumlahAnak()
        });
        function setJumlahAnak() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.JumlahAnak;
            }
        }

        $("#StatusLisence").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Expired", value: 0 },
                { text: "Active", value: 1 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatusLisence,
            value: setCmbStatusLisence()
        });
        function OnChangeStatusLisence() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbStatusLisence() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.StatusLisence;
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
        $("#EmployeePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Name").val("");
        $("#SKNumber").val("");
        $("#SKDate").val("");
        $("#SKExpiredDate").val("");
        $("#DateOfWork").val("");
        $("#AlamatDomisili").val("");
        $("#SKFormat").val("");
        $("#Position").val("");
        $("#BranchCode").val("");
        $("#ChiefCode").val("");
        $("#SubRedempCode").val("");
        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#JenisKelamin").val("");
        $("#TanggalLahir").val("");
        $("#NIK").val("");
        $("#NPWP").val("");
        $("#AlamatSesuaiKTP").val("");
        $("#NoBPJSKetenagakerjaan").val("");
        $("#NoBPJSKesehatan").val("");
        $("#NoRekTabungan").val("");
        $("#StatusPerkawinan").val("");
        $("#JumlahAnak").data("kendoNumericTextBox").value(0);
        $("#SKDate").data("kendoDatePicker").value(new Date());
        $("#SKExpiredDate").data("kendoDatePicker").value(new Date());
        $("#DateOfWork").data("kendoDatePicker").value(new Date());

        $("#NamaLisence").val("");
        $("#ExpiredDateLisence").val("");
        $("#StatusLisence").val("");
        $("#BitReportToOJK").prop('checked', false);
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
                            EmployeePK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            Name: { type: "string" },
                            SKNumber: { type: "string" },
                            SKDate: { type: "date" },
                            SKFormat: { type: "number" },
                            SKFormatDesc: { type: "string" },
                            Position: { type: "number" },
                            PositionDesc: { type: "string" },
                            DateOfWork: { type: "date" },
                            BranchCode: { type: "string" },
                            //BranchCodeDesc: { type: "string" },
                            ChiefCode: { type: "number" },
                            ChiefCodeDesc: { type: "string" },
                            SubRedempCode: { type: "string" },
                            EntryUsersID: { type: "string" },
                            EntryTime: { type: "date" },
                            UpdateUsersID: { type: "string" },
                            UpdateTime: { type: "date" },
                            ApprovedUsersID: { type: "string" },
                            ApprovedTime: { type: "date" },
                            VoidUsersID: { type: "string" },
                            VoidTime: { type: "date" },
                            LastUpdate: { type: "date" },
                            Timestamp: { type: "string" },
                            JenisKelamin: { type: "number" },
                            JenisKelaminDesc: { type: "string" },
                            TanggalLahir: { type: "date" },
                            NIK: { type: "string" },
                            NPWP: { type: "string" },
                            AlamatSesuaiKTP: { type: "string" },
                            AlamatDomisili: { type: "string" },
                            NoBPJSKetenagakerjaan: { type: "string" },
                            NoBPJSKesehatan: { type: "string" },
                            NoRekTabungan: { type: "string" },
                            StatusPerkawinan: { type: "number" },
                            StatusPerkawinanDesc: { type: "string" },
                            JumlahAnak: { type: "number" },
                            NamaLisence: { type: "string" },
                            ExpiredDateLisence: { type: "string" },
                            StatusLisence: { type: "number" },
                            StatusLisenceDesc: { type: "string" },
                            BitReportToOJK: { type: "boolean" },
                        }
                    }
                }
            });
    }


    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridEmployeeApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridEmployeePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridEmployeeHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var EmployeeApprovedURL = window.location.origin + "/Radsoft/Employee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(EmployeeApprovedURL);

        $("#gridEmployeeApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Employee"
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
                { field: "EmployeePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Name", title: "Name", width: 300 },
                { field: "SKNumber", title: "SK Number", width: 200 },
                { field: "SKDate", title: "SK Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SKDate), 'MM/dd/yyyy')#" },
                { field: "SKExpiredDate", title: "SK Expired Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SKExpiredDate), 'MM/dd/yyyy')#" },
                { field: "SKFormatDesc", title: "SK Format", width: 200 },
                { field: "PositionDesc", title: "Position", width: 400 },
                { field: "DateOfWork", title: "Date Of Work", width: 200, template: "#= kendo.toString(kendo.parseDate(DateOfWork), 'MM/dd/yyyy')#" },
                { field: "BranchCode", title: "Branch Code", width: 200 },
                //{ field: "BranchCodeDesc", title: "Branch Code", width: 200 },
                { field: "ChiefCodeDesc", title: "Chief Code", width: 170 },
                { field: "JenisKelaminDesc", title: "Jenis Kelamin", width: 200 },
                { field: "TanggalLahir", title: "Tanggal Lahir", width: 200 },
                { field: "NIK", title: "NIK", width: 200 },
                { field: "NPWP", title: "NPWP", width: 200 },
                { field: "AlamatSesuaiKTP", title: "Alamat Sesuai KTP", width: 200 },
                { field: "AlamatDomisili", title: "Alamat Domisili", width: 200 },
                { field: "NoBPJSKetenagakerjaan", title: "No BPJS Ketenagakerjaan", width: 200 },
                { field: "NoBPJSKesehatan", title: "No BPJS Kesehatan", width: 200 },
                { field: "NoRekTabungan", title: "No Rekening Tabungan", width: 200 },
                { field: "StatusPerkawinanDesc", title: "Status Perkawinan", width: 200 },
                { field: "JumlahAnak", title: "Jumlah Anak", width: 200 },

                { field: "NamaLisence", title: "Nama Lisence", width: 200 },
                { field: "ExpiredDateLisence", title: "Expired Date Lisence", width: 200, },
                { field: "StatusLisenceDesc", title: "Status Lisence", width: 200 },
                { field: "BitReportToOJK", title: "Bit Report To OJK", width: 200, template: "#= BitReportToOJK ? 'Yes' : 'No' #" },

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
        $("#TabEmployee").kendoTabStrip({
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
                        var EmployeePendingURL = window.location.origin + "/Radsoft/Employee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(EmployeePendingURL);
                        $("#gridEmployeePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Employee"
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
                                { field: "EmployeePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "SKNumber", title: "SK Number", width: 200 },
                                { field: "SKDate", title: "SK Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SKDate), 'MM/dd/yyyy')#" },
                                { field: "SKExpiredDate", title: "SK Expired Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SKExpiredDate), 'MM/dd/yyyy')#" },
                                { field: "SKFormatDesc", title: "SK Format", width: 200 },
                                { field: "PositionDesc", title: "Position", width: 400 },
                                { field: "DateOfWork", title: "Date Of Work", width: 200, template: "#= kendo.toString(kendo.parseDate(DateOfWork), 'MM/dd/yyyy')#" },
                                { field: "BranchCode", title: "Branch Code", width: 200 },
                                //{ field: "BranchCodeDesc", title: "Branch Code", width: 200 },
                                { field: "ChiefCodeDesc", title: "Chief Code", width: 170 },
                                { field: "SubRedempCode", title: "Sub Redemp Code", width: 200 },
                                { field: "JenisKelaminDesc", title: "Jenis Kelamin", width: 200 },
                                { field: "TanggalLahir", title: "Tanggal Lahir", width: 200 },
                                { field: "NIK", title: "NIK", width: 200 },
                                { field: "NPWP", title: "NPWP", width: 200 },
                                { field: "AlamatSesuaiKTP", title: "Alamat Sesuai KTP", width: 200 },
                                { field: "AlamatDomisili", title: "Alamat Domisili", width: 200 },
                                { field: "NoBPJSKetenagakerjaan", title: "No BPJS Ketenagakerjaan", width: 200 },
                                { field: "NoBPJSKesehatan", title: "No BPJS Kesehatan", width: 200 },
                                { field: "NoRekTabungan", title: "No Rekening Tabungan", width: 200 },
                                { field: "StatusPerkawinanDesc", title: "Status Perkawinan", width: 200 },
                                { field: "JumlahAnak", title: "Jumlah Anak", width: 200 },

                                { field: "NamaLisence", title: "Nama Lisence", width: 200 },
                                { field: "ExpiredDateLisence", title: "Expired Date Lisence", width: 200 },
                                { field: "StatusLisenceDesc", title: "Status Lisence", width: 200 },
                                { field: "BitReportToOJK", title: "Bit Report To OJK", width: 200, template: "#= BitReportToOJK ? 'Yes' : 'No' #" },


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

                        var EmployeeHistoryURL = window.location.origin + "/Radsoft/Employee/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(EmployeeHistoryURL);

                        $("#gridEmployeeHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Employee"
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
                                { field: "EmployeePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "SKNumber", title: "SK Number", width: 200 },
                                { field: "SKDate", title: "SK Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SKDate), 'MM/dd/yyyy')#" },
                                { field: "SKExpiredDate", title: "SK Expired Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SKExpiredDate), 'MM/dd/yyyy')#" },
                                { field: "SKFormatDesc", title: "SK Format", width: 200 },
                                { field: "PositionDesc", title: "Position", width: 400 },
                                { field: "DateOfWork", title: "Date Of Work", width: 200, template: "#= kendo.toString(kendo.parseDate(DateOfWork), 'MM/dd/yyyy')#" },
                                { field: "BranchCode", title: "Branch Code", width: 200 },
                                //{ field: "BranchCodeDesc", title: "Branch Code", width: 200 },
                                { field: "ChiefCodeDesc", title: "Chief Code", width: 170 },
                                { field: "SubRedempCode", title: "Sub Redemp Code", width: 200 },
                                { field: "JenisKelaminDesc", title: "Jenis Kelamin", width: 200 },
                                { field: "TanggalLahir", title: "Tanggal Lahir", width: 200 },
                                { field: "NIK", title: "NIK", width: 200 },
                                { field: "NPWP", title: "NPWP", width: 200 },
                                { field: "AlamatSesuaiKTP", title: "Alamat Sesuai KTP", width: 200 },
                                { field: "AlamatDomisili", title: "Alamat Domisili", width: 200 },
                                { field: "NoBPJSKetenagakerjaan", title: "No BPJS Ketenagakerjaan", width: 200 },
                                { field: "NoBPJSKesehatan", title: "No BPJS Kesehatan", width: 200 },
                                { field: "NoRekTabungan", title: "No Rekening Tabungan", width: 200 },
                                { field: "StatusPerkawinanDesc", title: "Status Perkawinan", width: 200 },
                                { field: "JumlahAnak", title: "Jumlah Anak", width: 200 },

                                { field: "NamaLisence", title: "Nama Lisence", width: 200 },
                                { field: "ExpiredDateLisence", title: "Expired Date Lisence", width: 200 },
                                { field: "StatusLisenceDesc", title: "Status Lisence", width: 200 },
                                { field: "BitReportToOJK", title: "Bit Report To OJK", width: 200, template: "#= BitReportToOJK ? 'Yes' : 'No' #" },


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
        var grid = $("#gridEmployeeHistory").data("kendoGrid");
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

                    var Employee = {
                        Name: $('#Name').val(),
                        SKNumber: $('#SKNumber').val(),
                        SKDate: $('#SKDate').val(),
                        SKExpiredDate: $('#SKExpiredDate').val(),
                        SKFormat: $('#SKFormat').val(),
                        Position: $('#Position').val(),
                        DateOfWork: $('#DateOfWork').val(),
                        BranchCode: $('#BranchCode').val(),
                        ChiefCode: $('#ChiefCode').val(),
                        JenisKelamin: $('#JenisKelamin').val(),
                        TanggalLahir: $('#TanggalLahir').val(),
                        NIK: $('#NIK').val(),
                        NPWP: $('#NPWP').val(),
                        AlamatSesuaiKTP: $('#AlamatSesuaiKTP').val(),
                        AlamatDomisili: $('#AlamatDomisili').val(),
                        NoBPJSKetenagakerjaan: $('#NoBPJSKetenagakerjaan').val(),
                        NoBPJSKesehatan: $('#NoBPJSKesehatan').val(),
                        NoRekTabungan: $('#NoRekTabungan').val(),
                        StatusPerkawinan: $('#StatusPerkawinan').val(),
                        JumlahAnak: $('#JumlahAnak').val(),
                        NamaLisence: $('#NamaLisence').val(),
                        ExpiredDateLisence: $('#ExpiredDateLisence').val(),
                        StatusLisence: $('#StatusLisence').val(),
                        BitReportToOJK: $('#BitReportToOJK').is(":checked"),
                        SubRedempCode: $('#SubRedempCode').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Employee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Employee_I",
                        type: 'POST',
                        data: JSON.stringify(Employee),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EmployeePK").val() + "/" + $("#HistoryPK").val() + "/" + "Employee",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var Employee = {
                                    EmployeePK: $('#EmployeePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Name: $('#Name').val(),
                                    SKNumber: $('#SKNumber').val(),
                                    SKDate: $('#SKDate').val(),
                                    SKExpiredDate: $('#SKExpiredDate').val(),
                                    SKFormat: $('#SKFormat').val(),
                                    Position: $('#Position').val(),
                                    DateOfWork: $('#DateOfWork').val(),
                                    BranchCode: $('#BranchCode').val(),
                                    ChiefCode: $('#ChiefCode').val(),
                                    SubRedempCode: $('#SubRedempCode').val(),
                                    JenisKelamin: $('#JenisKelamin').val(),
                                    TanggalLahir: $('#TanggalLahir').val(),
                                    NIK: $('#NIK').val(),
                                    NPWP: $('#NPWP').val(),
                                    AlamatSesuaiKTP: $('#AlamatSesuaiKTP').val(),
                                    AlamatDomisili: $('#AlamatDomisili').val(),
                                    NoBPJSKetenagakerjaan: $('#NoBPJSKetenagakerjaan').val(),
                                    NoBPJSKesehatan: $('#NoBPJSKesehatan').val(),
                                    NoRekTabungan: $('#NoRekTabungan').val(),
                                    StatusPerkawinan: $('#StatusPerkawinan').val(),
                                    JumlahAnak: $('#JumlahAnak').val(),
                                    NamaLisence: $('#NamaLisence').val(),
                                    ExpiredDateLisence: $('#ExpiredDateLisence').val(),
                                    StatusLisence: $('#StatusLisence').val(),
                                    BitReportToOJK: $('#BitReportToOJK').is(":checked"),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Employee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Employee_U",
                                    type: 'POST',
                                    data: JSON.stringify(Employee),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EmployeePK").val() + "/" + $("#HistoryPK").val() + "/" + "Employee",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Employee" + "/" + $("#EmployeePK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EmployeePK").val() + "/" + $("#HistoryPK").val() + "/" + "Employee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Employee = {
                                EmployeePK: $('#EmployeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Employee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Employee_A",
                                type: 'POST',
                                data: JSON.stringify(Employee),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EmployeePK").val() + "/" + $("#HistoryPK").val() + "/" + "Employee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Employee = {
                                EmployeePK: $('#EmployeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Employee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Employee_V",
                                type: 'POST',
                                data: JSON.stringify(Employee),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#EmployeePK").val() + "/" + $("#HistoryPK").val() + "/" + "Employee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Employee = {
                                EmployeePK: $('#EmployeePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Employee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Employee_R",
                                type: 'POST',
                                data: JSON.stringify(Employee),
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


    $("#BtnGenerateEmployeeText").click(function () {

        alertify.confirm("Are you sure want to Generate?", function (e) {
            if (e) {

                var Employee = {
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/Employee/GenerateEmployeeText/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(Employee),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        if (data != null) {
                            $.unblockUI();
                            $("#downloadFileRadsoft").attr("href", data);
                            $("#downloadFileRadsoft").attr("download", "Employee.txt");
                            document.getElementById("downloadFileRadsoft").click();
                        }
                        else {
                            alertify.alert("Create Report Failed Or No Data");
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });

    });

    $("#BtnEmployeeReport").click(function () {

        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                //var _permission = "EmployeeReport_O";
                var EmployeeReport = {
                    //ReportName: $('#Name').val(),
                    //ValueDateFrom: $('#DateFrom').val(),
                    //ValueDateTo: $('#DateTo').val(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Employee/GenerateEmployeeReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(EmployeeReport),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        var newwindow = window.open(data, '_blank'); // ini untuk tarik report PDF tambah newtab

                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });



});
