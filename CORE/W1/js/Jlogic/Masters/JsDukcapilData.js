$(document).ready(function () {
    document.title = 'FORM DukcapilData';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListCountry;
    var htmlCountry;
    var htmlCountryDesc;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    function initButton() {
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
    }


    function initWindow() {

        $("#TGL_LHR").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });
        $("#FCTGL_LHR").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null
        });

        win = $("#WinDukcapilData").kendoWindow({
            height: 600,
            title: "DUKCAPIL DATA Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

    }
    var GlobValidator = $("#WinDukcapilData").kendoValidator().data("kendoValidator");

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
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
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

            $("#DukcapilDataPK").val(dataItemX.DukcapilDataPK);
            $("#SinvestID").val(dataItemX.SinvestID);
            $("#NIK").val(dataItemX.NIK);
            $("#NAMA_LGKP").val(dataItemX.NAMA_LGKP);
            $("#AGAMA").val(dataItemX.AGAMA);
            $("#KAB_NAME").val(dataItemX.KAB_NAME);
            $("#NO_RW").val(dataItemX.NO_RW);
            $("#KEC_NAME").val(dataItemX.KEC_NAME);
            $("#JENIS_PKRJN").val(dataItemX.JENIS_PKRJN);
            $("#NO_RT").val(dataItemX.NO_RT);
            $("#NO_KEL").val(dataItemX.NO_KEL);
            $("#ALAMAT").val(dataItemX.ALAMAT);
            $("#NO_KEC").val(dataItemX.NO_KEC);
            $("#TMPT_LHR").val(dataItemX.TMPT_LHR);
            $("#STATUS_KAWIN").val(dataItemX.STATUS_KAWIN);
            $("#NO_PROP").val(dataItemX.NO_PROP);
            $("#PROP_NAME").val(dataItemX.PROP_NAME);
            $("#NO_KAB").val(dataItemX.NO_KAB);
            $("#KEL_NAME").val(dataItemX.KEL_NAME);
            $("#JENIS_KLMIN").val(dataItemX.JENIS_KLMIN);
            $("#TGL_LHR").val(kendo.toString(kendo.parseDate(dataItemX.TGL_LHR), 'dd/MMM/yyyy'));
            $("#FCNIK").val(dataItemX.FCNIK);
            $("#FCNAMA_LGKP").val(dataItemX.FCNAMA_LGKP);
            $("#FCAGAMA").val(dataItemX.FCAGAMA);
            $("#FCJENIS_PKRJN").val(dataItemX.FCJENIS_PKRJN);
            $("#FCALAMAT").val(dataItemX.FCALAMAT);
            $("#FCTMPT_LHR").val(dataItemX.FCTMPT_LHR);
            $("#FCSTATUS_KAWIN").val(dataItemX.FCSTATUS_KAWIN);
            $("#FCJENIS_KLMIN").val(dataItemX.FCJENIS_KLMIN);
            $("#FCTGL_LHR").val(kendo.toString(kendo.parseDate(dataItemX.FCTGL_LHR), 'dd/MMM/yyyy'));
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
        $("#DukcapilDataPK").val("");
        $("#Notes").val("");
        $("#NIK").val("");
        $("#NAMA_LGKP").val("");
        $("#AGAMA").val("");
        $("#KAB_NAME").val("");
        $("#NO_RW").val("");
        $("#KEC_NAME").val("");
        $("#JENIS_PKRJN").val("");
        $("#NO_RT").val("");
        $("#NO_KEL").val("");
        $("#ALAMAT").val("");
        $("#NO_KEC").val("");
        $("#TMPT_LHR").val("");
        $("#STATUS_KAWIN").val("");
        $("#NO_PROP").val("");
        $("#PROP_NAME").val("");
        $("#NO_KAB").val("");
        $("#KEL_NAME").val("");
        $("#JENIS_KLMIN").val("");
        $("#TGL_LHR").val("");
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
                             DukcapilDataPK: { type: "number" },
                             NIK: { type: "string" },
                             NAMA_LGKP: { type: "string" },
                             AGAMA: { type: "string" },
                             KAB_NAME: { type: "string" },
                             NO_RW: { type: "string" },
                             KEC_NAME: { type: "string" },
                             JENIS_PKRJN: { type: "string" },
                             NO_RT: { type: "string" },
                             NO_KEL: { type: "number" },
                             ALAMAT: { type: "string" },
                             NO_KEC: { type: "string" },
                             TMPT_LHR: { type: "string" },
                             STATUS_KAWIN: { type: "string" },
                             NO_PROP: { type: "string" },
                             PROP_NAME: { type: "string" },
                             NO_KAB: { type: "string" },
                             KEL_NAME: { type: "string" },
                             JENIS_KLMIN: { type: "string" },
                             TGL_LHR: { type: "string" },
                             FCNIK: { type: "string" },
                             FCNAMA_LGKP: { type: "string" },
                             FCAGAMA: { type: "string" },
                             FCJENIS_PKRJN: { type: "string" },
                             FCALAMAT: { type: "string" },
                             FCTMPT_LHR: { type: "string" },
                             FCSTATUS_KAWIN: { type: "string" },
                             FCJENIS_KLMIN: { type: "string" },
                             FCTGL_LHR: { type: "string" },
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridDukcapilDataApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
    }

    function initGrid() {
        var DukcapilDataApprovedURL = window.location.origin + "/Radsoft/DukcapilData/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(DukcapilDataApprovedURL);

        $("#gridDukcapilDataApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form DukcapilData"
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
                { field: "DukcapilDataPK", title: "SysNo.", width: 95 },
                { field: "NIK", title: "NIK", width: 150 },
                { field: "NAMA_LGKP", title: "Nama Lengkap", width: 250 },
                { field: "TMPT_LHR", title: "Tempat Lahir", width: 150, },
                { field: "TGL_LHR", title: "Tgl Lahir", width: 150, template: "#= kendo.toString(kendo.parseDate(TGL_LHR), 'dd/MMM/yyyy')#" },
                { field: "AGAMA", title: "Agama", width: 150 },
                { field: "JENIS_KLMIN", title: "Jenis Kelamin", width: 150 },
                { field: "JENIS_PKRJN", title: "Pekerjaan", width: 150 },
                { field: "STATUS_KAWIN", title: "Status Kawin", width: 150 },
                { field: "ALAMAT", title: "Alamat", width: 400 },
                { field: "PROP_NAME", title: "Provinsi", width: 200 },
                { field: "NO_PROP", title: "No. Provinsi", width: 120 },
                { field: "KAB_NAME", title: "Kabupaten", width: 200 },
                { field: "NO_KAB", title: "No.Kabupaten", width: 140 },
                { field: "KEC_NAME", title: "Kecamatann", width: 200 },
                { field: "NO_KEC", title: "No. Kecamaten", width: 120 },
                { field: "KEL_NAME", title: "Kelurahan", width: 200 },
                { field: "NO_KEL", title: "Kelurahan", width: 120 },
                { field: "NO_RW", title: "RW", width: 120 },
                { field: "NO_RT", title: "RT", width: 120 },
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabDukcapilData").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {
                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);
                } else {
                    refresh();
                }
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

});
