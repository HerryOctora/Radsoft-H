$(document).ready(function () {
    document.title = 'FORM BLACKLISTNAME';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListNationality;
    var htmlNationality;
    var _statusOFACSDN, _statusOFACAdd, _statusOFACAlt;
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
        $("#BtnImport").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportBlacklistName").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportBlacklistNameDTTOT").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportBlacklistNamePPATK").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportBlacklistNameKPK").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnDownloadTemplateBlacklistName").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnDownloadTemplate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCheckAllClient").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });

        $("#BtnImportOFAC").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportOFACSDN").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportOFACAddress").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnImportOFACAlt").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnProcessOFAC").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnImportDowJones").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    if (_GlobClientCode == "10" || _GlobClientCode == "20") {
        $("#BtnCheckAllClient").show();
    }
    else {
        $("#BtnCheckAllClient").hide();
    }


    $("#BtnCheckAllClient").click(function () {


        alertify.confirm("Are you sure want to Insert BlackList Name ?", function (e) {
            if (e) {
                $.blockUI();


                $.ajax({
                    url: window.location.origin + "/Radsoft/BlackListName/InsertBlackListName/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }

                });
            }
        });

    });

    if (_GlobClientCode == '10') {
        $("#BtnImport").hide();
        $("#BtnImportBlacklistName").show();
    }
    else {

        $("#BtnImport").show();
        $("#BtnImportBlacklistName").hide();
    }

    if (_GlobClientCode == '20') {
        $("#NameAlias").attr("required", true);
    }
    else {
        $("#NameAlias").attr("required", false);

    }


    function initWindow() {

        //$("#TanggalLahir").kendoDatePicker({
        //    format: "dd/MMM/yyyy",
        //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        //    value: null
        //});

        //WinListNationality = $("#WinListNationality").kendoWindow({
        //    height: "520px",
        //    title: "Nationality List",
        //    visible: false,
        //    width: "570px",
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //    close: onWinListNationalityClose
        //}).data("kendoWindow");

        win = $("#WinBlackListName").kendoWindow({
            height: 800,
            title: "Black List Name Detail",
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

        winImport = $("#WinImport").kendoWindow({
            height: 300,
            title: "Black List Name Detail",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        winImportOFAC = $("#WinImportOFAC").kendoWindow({
            height: 400,
            title: "Import OFAC Detail",
            visible: false,
            width: 600,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpOFACClose
        }).data("kendoWindow");


        winDownload = $("#WinDownload").kendoWindow({
            height: 300,
            title: "Black List Name Detail",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");
    }

    var GlobValidator = $("#WinBlackListName").kendoValidator().data("kendoValidator");

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

            $("#BlackListNamePK").val(dataItemX.BlackListNamePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#NoDoc").val(dataItemX.NoDoc);
            $("#Name").val(dataItemX.Name);
            $("#NoIDQ").val(dataItemX.NoIDQ);
            $("#NameAlias").val(dataItemX.NameAlias);
            $("#NoID").val(dataItemX.NoID);
            $("#TempatLahir").val(dataItemX.TempatLahir);
            $("#TanggalLahir").val(dataItemX.TanggalLahir);
            $("#Kewarganegaraan").val(dataItemX.Kewarganegaraan);
            $("#Alamat").val(dataItemX.Alamat);
            $("#Description").val(dataItemX.Description);
            $("#Pekerjaan").val(dataItemX.Pekerjaan);
            $("#NomorPasport").val(dataItemX.NomorPasport);
            $("#JenisKelamin").val(dataItemX.JenisKelamin);
            $("#Agama").val(dataItemX.Agama);
            $("#NoKTP").val(dataItemX.NoKTP);
            $("#NoNPWP").val(dataItemX.NoNPWP);
            $("#Tanggal").val(dataItemX.Tanggal); 
            $("#SumberData").val(dataItemX.SumberData);
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

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BlackListNameType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeType,
                    dataSource: data,
                    value: setCmbType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Type == 0) {
                    return "";
                } else {
                    return dataItemX.Type;
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
        $("#BlackListNamePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#NoDoc").val("");
        $("#Type").val("");
        $("#TypeDesc").val("");
        $("#Name").val("");
        $("#NoIDQ").val("")
        $("#NameAlias").val("");
        $("#NoID").val("")
        $("#TempatLahir").val("")
        $("#TanggalLahir").val("");
        $("#Kewarganegaraan").val("");
        $("#Alamat").val("");
        $("#Description").val("");
        $("#Pekerjaan").val("");
        $("#NomorPasport").val("");
        $("#JenisKelamin").val("");
        $("#Agama").val("");
        $("#NoKTP").val("");
        $("#NoNPWP").val("");
        $("#Tanggal").val("");
        $("#SumberData").val("");
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
                             BlackListNamePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             NoDoc: { type: "string" },
                             Type: { type: "number" },
                             TypeDesc: { type: "string" },
                             Name: { type: "string" },
                             NoIDQ: { type: "string" },
                             NameAlias: { type: "string" },
                             NoID: { type: "string" },
                             TempatLahir: { type: "string" },
                             TanggalLahir: { type: "string" },
                             Kewarganegaraan: { type: "string" },
                             Alamat: { type: "string" },
                             Description: { type: "string" },
                             Pekerjaan: { type: "string" },
                             NomorPasport: { type: "string" },
                             JenisKelamin: { type: "string" },
                             Agama: { type: "string" },
                             NoKTP: { type: "string" },
                             NoNPWP: { type: "string" },
                             Tanggal: { type: "string" },
                             SumberData: { type: "string" },
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
            var gridApproved = $("#gridBlackListNameApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridBlackListNamePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridBlackListNameHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var BlackListNameApprovedURL = window.location.origin + "/Radsoft/BlackListName/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(BlackListNameApprovedURL);

        $("#gridBlackListNameApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Black List Name"
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
                { field: "BlackListNamePK", title: "SysNo.", width: 100 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "NoDoc", title: "NoDoc", width: 400 },
                { field: "Type", title: "Type", hidden: true, width: 200 },
                { field: "TypeDesc", title: "Type", width: 200 },
                { field: "Name", title: "Name", width: 400 },
                { field: "NoIDQ", title: "NoIDQ", width: 400 },
                { field: "NameAlias", title: "Name Alias", width: 400 },
                { field: "NoID", title: "NoID", width: 400 },
                { field: "TempatLahir", title: "TempatLahir", width: 400 },
                { field: "TanggalLahir", title: "Tanggal Lahir", width: 200 },
                { field: "Kewarganegaraan", title: "Kewarganegaraan", width: 200 },
                { field: "Alamat", title: "Alamat", width: 200 },
                { field: "Description", title: "Description", width: 400 },
                { field: "Pekerjaan", title: "Pekerjaan", width: 200 }, 
                { field: "NomorPasport", title: "Nomor Pasport", width: 150 },
                { field: "JenisKelamin", title: "Jenis Kelamin", width: 200 },
                { field: "Agama", title: "Agama", width: 200 },
                { field: "NoKTP", title: "No KTP", width: 200 },
                { field: "NoNPWP", title: "No NPWP", width: 200 },
                { field: "Tanggal", title: "Tanggal", width: 200 },
                { field: "SumberData", title: "Sumber Data", width: 200 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabBlackListName").kendoTabStrip({
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
                        var BlackListNamePendingURL = window.location.origin + "/Radsoft/BlackListName/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(BlackListNamePendingURL);
                        $("#gridBlackListNamePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Black List Name"
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
                                { field: "BlackListNamePK", title: "SysNo.", width: 100 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "NoDoc", title: "NoDoc", width: 400 },
                                { field: "Type", title: "Type", hidden: true, width: 200 },
                                { field: "TypeDesc", title: "Type", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "NoIDQ", title: "NoIDQ", width: 400 },
                                { field: "NameAlias", title: "Name Alias", width: 400 },
                                { field: "NoID", title: "NoID", width: 400 },
                                { field: "TempatLahir", title: "TempatLahir", width: 400 },
                                { field: "TanggalLahir", title: "Tanggal Lahir", width: 200 },
                                { field: "Kewarganegaraan", title: "Kewarganegaraan", width: 200 },
                                { field: "Alamat", title: "Alamat", width: 200 },
                                { field: "Description", title: "Description", width: 400 },
                                { field: "Pekerjaan", title: "Pekerjaan", width: 200 },
                                { field: "NomorPasport", title: "Nomor Pasport", width: 150 },
                                { field: "JenisKelamin", title: "Jenis Kelamin", width: 200 },
                                { field: "Agama", title: "Agama", width: 200 },
                                { field: "NoKTP", title: "No KTP", width: 200 },
                                { field: "NoNPWP", title: "No NPWP", width: 200 },
                                { field: "Tanggal", title: "Tanggal", width: 200 },
                                { field: "SumberData", title: "Sumber Data", width: 200 },
                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var BlackListNameHistoryURL = window.location.origin + "/Radsoft/BlackListName/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(BlackListNameHistoryURL);

                        $("#gridBlackListNameHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form BlackListName"
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
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "BlackListNamePK", title: "SysNo.", width: 100 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "NoDoc", title: "NoDoc", width: 400 },
                                { field: "Type", title: "Type", hidden: true, width: 200 },
                                { field: "TypeDesc", title: "Type", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "NoIDQ", title: "NoIDQ", width: 400 },
                                { field: "NameAlias", title: "Name Alias", width: 400 },
                                { field: "NoID", title: "NoID", width: 400 },
                                { field: "TempatLahir", title: "TempatLahir", width: 400 },
                                { field: "TanggalLahir", title: "Tanggal Lahir", width: 200 },
                                { field: "Kewarganegaraan", title: "Kewarganegaraan", width: 200 },
                                { field: "Alamat", title: "Alamat", width: 200 },
                                { field: "Description", title: "Description", width: 400 },
                                { field: "Pekerjaan", title: "Pekerjaan", width: 200 },
                                { field: "NomorPasport", title: "Nomor Pasport", width: 150 },
                                { field: "JenisKelamin", title: "Jenis Kelamin", width: 200 },
                                { field: "Agama", title: "Agama", width: 200 },
                                { field: "NoKTP", title: "No KTP", width: 200 },
                                { field: "NoNPWP", title: "No NPWP", width: 200 },
                                { field: "Tanggal", title: "Tanggal", width: 200 },
                                { field: "SumberData", title: "Sumber Data", width: 200 },
                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
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
        var grid = $("#gridBlackListNameHistory").data("kendoGrid");
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
        $("#ID").attr('readonly', false);
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    var BlackListName = {
                        NoDoc: $('#NoDoc').val(),
                        Type: $('#Type').val(),
                        Name: $('#Name').val(),
                        NoIDQ: $('#NoIDQ').val(),
                        NameAlias: $('#NameAlias').val(),
                        NoID: $('#NoID').val(),
                        TempatLahir: $('#TempatLahir').val(),
                        TanggalLahir: $('#TanggalLahir').val(),
                        Kewarganegaraan: $('#Kewarganegaraan').val(),
                        Alamat: $('#Alamat').val(),
                        Description: $('#Description').val(),
                        Pekerjaan: $('#Pekerjaan').val(),
                        NomorPasport: $('#NomorPasport').val(),
                        JenisKelamin: $('#JenisKelamin').val(),
                        Agama: $('#Agama').val(),
                        NoKTP: $('#NoKTP').val(),
                        NoNPWP: $('#NoNPWP').val(),
                        Tanggal: $('#Tanggal').val(),
                        SumberData: $('#SumberData').val(),

                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/BlackListName/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_I",
                        type: 'POST',
                        data: JSON.stringify(BlackListName),
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
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BlackListNamePK").val() + "/" + $("#HistoryPK").val() + "/" + "BlackListName",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var BlackListName = {
                                    BlackListNamePK: $('#BlackListNamePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    NoDoc: $('#NoDoc').val(),
                                    Type: $('#Type').val(),
                                    Name: $('#Name').val(),
                                    NoIDQ: $('#NoIDQ').val(),
                                    NameAlias: $('#NameAlias').val(),
                                    NoID: $('#NoID').val(),
                                    TempatLahir: $('#TempatLahir').val(),
                                    TanggalLahir: $('#TanggalLahir').val(),
                                    Kewarganegaraan: $('#Kewarganegaraan').val(),
                                    Alamat: $('#Alamat').val(),
                                    Description: $('#Description').val(),
                                    Pekerjaan: $('#Pekerjaan').val(),
                                    NomorPasport: $('#NomorPasport').val(),
                                    JenisKelamin: $('#JenisKelamin').val(),
                                    Agama: $('#Agama').val(),
                                    NoKTP: $('#NoKTP').val(),
                                    NoNPWP: $('#NoNPWP').val(),
                                    Tanggal: $('#Tanggal').val(),
                                    SumberData: $('#SumberData').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BlackListName/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_U",
                                    type: 'POST',
                                    data: JSON.stringify(BlackListName),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BlackListNamePK").val() + "/" + $("#HistoryPK").val() + "/" + "BlackListName",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "BlackListName" + "/" + $("#BlackListNamePK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BlackListNamePK").val() + "/" + $("#HistoryPK").val() + "/" + "BlackListName",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BlackListName = {
                                BlackListNamePK: $('#BlackListNamePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BlackListName/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_A",
                                type: 'POST',
                                data: JSON.stringify(BlackListName),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BlackListNamePK").val() + "/" + $("#HistoryPK").val() + "/" + "BlackListName",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BlackListName = {
                                BlackListNamePK: $('#BlackListNamePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BlackListName/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_V",
                                type: 'POST',
                                data: JSON.stringify(BlackListName),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BlackListNamePK").val() + "/" + $("#HistoryPK").val() + "/" + "BlackListName",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BlackListName = {
                                BlackListNamePK: $('#BlackListNamePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BlackListName/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_R",
                                type: 'POST',
                                data: JSON.stringify(BlackListName),
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

    function getDataSourceListNationality() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDINationality",
                                     dataType: "json"
                                 }
                         },
                 batch: true,
                 cache: false,
                 error: function (e) {
                     alert(e.errorThrown + " - " + e.xhr.responseText);
                     this.cancelChanges();
                 },
                 pageSize: 25,
                 schema: {
                     model: {
                         fields: {
                             Code: { type: "string" },
                             DescOne: { type: "string" },

                         }
                     }
                 }
             });
    }

    $("#btnListNationality").click(function () {
        WinListNationality.center();
        WinListNationality.open();
        initListNationality();
        htmlNationality = "#Nationality";
        htmlNationalityDesc = "#NationalityDesc";

    });
    $("#btnClearListNationality").click(function () {
        $("#Nationality").val("");
        $("#NationalityDesc").val("");
    });

    function initListNationality() {
        var dsListNationality = getDataSourceListNationality();
        $("#gridListNationality").kendoGrid({
            dataSource: dsListNationality,
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
            columns: [
               { command: { text: "Select", click: ListNationalitySelect }, title: " ", width: 60 },
               //{ field: "Code", title: "No", width: 100 },
               { field: "DescOne", title: "Nationality", width: 200 }
            ]
        });
    }

    function onWinListNationalityClose() {
        $("#gridListNationality").empty();
    }

    function ListNationalitySelect(e) {
        var grid = $("#gridListNationality").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlNationalityDesc).val(dataItemX.DescOne);
        $(htmlNationality).val(dataItemX.Code);
        WinListNationality.close();

    }

    $("#BtnImport").click(function () {
        document.getElementById("FileImportBlackListName").click();
    });

    $("#FileImportBlackListName").change(function () {
        $.blockUI({});
        
        var data = new FormData();
        var files = $("#FileImportBlackListName").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("BlackListName", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportBlackListName").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBlackListName").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportBlackListName").val("");
        }
    });

    $("#BtnImportBlacklistName").click(function () {
        winImport.center();
        winImport.open();
    });



    $("#BtnImportBlacklistNameDTTOT").click(function () {
        if ($('#DataSource').val() == "" || $('#DataSource').val() == null)
        {
            alertify.alert("Data Source Empty, Please Check")
        }
        else
        {
            document.getElementById("FileImportBlackListNameDTTOT").click();
        }
        
    });

    $("#FileImportBlackListNameDTTOT").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportBlackListNameDTTOT").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("BlackListNameDTTOT", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_Import" + "/" + $('#DataSource').val(),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportBlackListNameDTTOT").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBlackListNameDTTOT").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportBlackListNameDTTOT").val("");
        }
    });


    $("#BtnImportBlacklistNamePPATK").click(function () {
        if ($('#DataSource').val() == "" || $('#DataSource').val() == null)
        {
            alertify.alert("Data Source Empty, Please Check")
        }
        else
        {
            document.getElementById("FileImportBlackListNamePPATK").click();
        }
    });

    $("#FileImportBlackListNamePPATK").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportBlackListNamePPATK").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("BlackListNamePPATK", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_Import" + "/" + $('#DataSource').val(),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportBlackListNamePPATK").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBlackListNamePPATK").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportBlackListNamePPATK").val("");
        }
    });


    $("#BtnImportBlacklistNameKPK").click(function () {
        if ($('#DataSource').val() == "" || $('#DataSource').val() == null) {
            alertify.alert("Data Source Empty, Please Check")
        }
        else
        {
            document.getElementById("FileImportBlackListNameKPK").click();
        }
        
    });

    $("#FileImportBlackListNameKPK").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportBlackListNameKPK").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("BlackListNameKPK", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_Import" + "/" + $('#DataSource').val(),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportBlackListNameKPK").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBlackListNameKPK").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportBlackListNameKPK").val("");
        }
    });


    $("#BtnDownloadTemplateBlacklistName").click(function () {
        $("#Version").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            enable:false,
            dataSource: [
                { text: "DTTOT v1", value: 1 },
                { text: "DTTOT v2", value: 2 },
                { text: "PPATK", value: 3 },
                { text: "KPK", value: 4 },

            ],
            filter: "contains",
            suggest: true
        });


        winDownload.center();
        winDownload.open();
    });

    $("#BtnDownloadTemplate").click(function (e) {
        var _url;

        Download("E://Radsoft//16 Agustus//CORE//W1//Upload//2130316799DBJK1FXD1420180507.txt")
        //FetchURL($('#Version').val());
        
        //return false;  // called to ensure there is no postback
        
        //e.preventDefault();  //stop the browser from following
        //window.location.href = "E://Radsoft//15%20Agustus//CORE//W1//Upload//2130316799DBJK1FXD1420180507.txt";
    });


    function Download(_url) {
        document.getElementById('my_iframe').src = _url;
    };

    function FetchURL(_version){
        var _url;
        if (_version == 1) {
            _url = "..\..\..\Template\DTTOT_v1.docx";
        }
        else if (_version == 2) {
            _url = 'E:\\Radsoft\\15 Agustus\\CORE\\W1\\Template\\DTTOT_v2';
        }
        else if (_version == 3) {
            _url = 'E:\\Radsoft\\15 Agustus\\CORE\\W1\\Template\\PPATK';
        }
        else if (_version == 4) {
            _url = 'E:\\Radsoft\\15 Agustus\\CORE\\W1\\Template\\KPK';
        }

        downloadURL(_url);
    }

    function downloadURL(_url) {
       
        e.preventDefault();
        window.location.href = _url;
    };

    function onPopUpOFACClose() {

        $("#StatusOFACSDN").text("");
        $("#StatusOFACAddress").text("");
        $("#StatusOFACAlt").text("");

    }

    $("#BtnImportOFAC").click(function () {
        _statusOFACSDN = 0;
        _statusOFACAdd = 0;
        _statusOFACAlt = 0;

        winImportOFAC.center();
        winImportOFAC.open();
    });

    $("#BtnImportOFACSDN").click(function () {
        document.getElementById("FileImportOFACSDN").click();
    });

    $("#FileImportOFACSDN").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportOFACSDN").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("OFACSDN", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportOFACSDN").val("");
                    $("#StatusOFACSDN").text("READY");
                    _statusOFACSDN = 1;
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportOFACSDN").val("");
                    _statusOFACSDN = 0;
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportOFACSDN").val("");
            _statusOFACSDN = 0;
        }
    });

    $("#BtnImportOFACAddress").click(function () {
        document.getElementById("FileImportOFACAddress").click();
    });

    $("#FileImportOFACAddress").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportOFACAddress").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("OFACAdd", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportOFACAddress").val("");
                    $("#StatusOFACAddress").text("READY");
                    _statusOFACAdd = 1;
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportOFACAddress").val("");
                    _statusOFACAdd = 0;
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportOFACAddress").val("");
            _statusOFACAdd = 0;
        }
    });

    $("#BtnImportOFACAlt").click(function () {
        document.getElementById("FileImportOFACAlt").click();
    });

    $("#FileImportOFACAlt").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportOFACAlt").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("OFACAlt", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BlackListName_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportOFACAlt").val("");
                    $("#StatusOFACAlt").text("READY");
                    _statusOFACAlt = 1;
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportOFACAlt").val("");
                    _statusOFACAlt = 0;
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportOFACAlt").val("");
            _statusOFACAlt = 0;
        }
    });

    $("#BtnProcessOFAC").click(function () {

        if (_statusOFACSDN == 0 || _statusOFACAdd == 0 || _statusOFACAlt == 0) {
            alertify.alert("Please Upload the file needed to be process!");
        }
        else {
            alertify.confirm("Are you sure want to process OFAC data?", function (e) {
                if (e) {
                    $.blockUI({});

                    $.ajax({
                        url: window.location.origin + "/Radsoft/BlackListName/InsertFromOFAC/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            winImportOFAC.close();
                            alertify.alert(data);
                            refresh();
                            $.unblockUI();

                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
            });
        }


    });


    $("#BtnImportDowJones").click(function () {
        document.getElementById("FileImportDowJones").click();
    });

    $("#FileImportDowJones").change(function () {
        $.blockUI({});

        var data = new FormData();
        var files = $("#FileImportDowJones").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }
        if (files.length > 0) {
            data.append("DowJones", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DowJones_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportDowJones").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportDowJones").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportDowJones").val("");
        }
    });



});
