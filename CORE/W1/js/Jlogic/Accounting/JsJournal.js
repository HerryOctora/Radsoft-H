$(document).ready(function () {
    document.title = 'FORM JOURNAL';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 330;
    var winJournalDetail;
    var upOradd;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobStatus;

    var _defaultPeriodPK;
    var GlobDecimalPlaces;


    if (_GlobClientCode == "03") {
        $("#LblEmployee").show();
        $("#LblOffice").hide();
        $("#LblKindOfCost").show();
        $("#LblConsignee").hide();
        $("#LblAgent").show();
        $("#LblDirect").hide();
        $("#LblClient").show();
    }
    else {
        $("#LblEmployee").hide();
        $("#LblOffice").show();
        $("#LblKindOfCost").hide();
        $("#LblConsignee").show();
        $("#LblAgent").hide();
        $("#LblDirect").show();
        $("#LblClient").hide();
    }



    $.ajax({
        url: window.location.origin + "/Radsoft/Period/GetPeriodPkByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fy,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            _defaultPeriodPK = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });


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

        $("#BtnPosting").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnReversed").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRevise.png"
        });

        $("#BtnVoucher").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
        });
        $("#BtnReverseBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReverseAll.png"
        });
        $("#BtnRefreshDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnAddDetail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSave").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnClose").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnImportJournal").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnRetrieveMFee").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnRetrieveClosePriceReksadana").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnRetrieveDataMKBD").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnExportJournal").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        

    }
    $("#BtnImportJournal").click(function () {
        document.getElementById("FileImportJournal").click();
    });

    $("#FileImportJournal").change(function () {
        $.blockUI({});
        
        var data = new FormData();
        var files = $("#FileImportJournal").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }


        if (files.length > 0) {
            data.append("Journal", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    alertify.alert(data);
                    $.unblockUI();
                    $("#FileImportJournal").val("");
                    refresh();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportJournal").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportJournal").val("");
        }
    });

    function resetNotification() {
        $("#toggleCSS").attr("href", "../../styles/alertify.default.css");
        alertify.set({
            labels: {
                ok: "OK",
                cancel: "Cancel"
            },
            delay: 4000,
            buttonReverse: false,
            buttonFocus: "ok"
        });
    }
    function initWindow() {

        //GetJournalDecimalPlaces
        $.ajax({
            url: window.location.origin + "/Radsoft/AccountingSetup/GetJournalDecimalPlaces/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                GlobDecimalPlaces = data;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDate
        });

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateTo
        });


        $("#ParamDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateFrom
        });
        $("#ParamDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateTo
        });

        function OnChangeParamDateFrom() {

            var _date = Date.parse($("#ParamDateFrom").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            $("#ParamDateTo").data("kendoDatePicker").value($("#ParamDateFrom").data("kendoDatePicker").value());

         
        }
        function OnChangeParamDateTo() {
            var _date = Date.parse($("#ParamDateTo").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
      
        }

        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
                $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            }
        }
        function OnChangeDateFrom() {
        
                var _date = Date.parse($("#DateFrom").data("kendoDatePicker").value());
                if (!_date) {
                    
                    alertify.alert("Wrong Format Date DD/MM/YYYY");
                    return;
                }
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
         
            refresh();
        }
        function OnChangeDateTo() {
            var _date = Date.parse($("#DateTo").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            refresh();
        }

        $("#ParamDateFromA").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateFromA
        });
        $("#ParamDateToA").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateToA
        });

        function OnChangeParamDateFromA() {

            var _date = Date.parse($("#ParamDateFromA").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            $("#ParamDateToA").data("kendoDatePicker").value($("#ParamDateFromA").data("kendoDatePicker").value());


        }
        function OnChangeParamDateToA() {
            var _date = Date.parse($("#ParamDateToA").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }

        }

        $("#ParamDateFromMKBD").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateFromMKBD
        });
        $("#ParamDateToMKBD").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateToMKBD
        });

        function OnChangeParamDateFromMKBD() {

            var _date = Date.parse($("#ParamDateFromMKBD").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }
            $("#ParamDateToMKBD").data("kendoDatePicker").value($("#ParamDateFromMKBD").data("kendoDatePicker").value());


        }
        function OnChangeParamDateToMKBD() {
            var _date = Date.parse($("#ParamDateToMKBD").data("kendoDatePicker").value());
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return;
            }

        }

        // disini
        winJournalDetail = $("#WinJournalDetail").kendoWindow({
            height: 450,
            title: "* JOURNAL DETAIL",
            visible: false,
            width: 1280,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinJournalDetailClose
        }).data("kendoWindow");

        win = $("#WinJournal").kendoWindow({
            height: 1500,
            title: "* JOURNAL HEADER",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        winOldData = $("#WinOldData").kendoWindow({
            height: 500,
            title: "Data Comparison",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            }
        }).data("kendoWindow");


        WinListReference = $("#WinListReference").kendoWindow({
            height: 500,
            title: "List Reference ",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },
            close: onWinListReferenceClose
        }).data("kendoWindow");

        WinRetrieveMFee = $("#WinRetrieveMFee").kendoWindow({
            height: 150,
            title: "* Retrieve Manamegemnt Fee",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        WinRetrieveClosePriceReksadana = $("#WinRetrieveClosePriceReksadana").kendoWindow({
            height: 150,
            title: "* Retrieve ClosePrice Reksadana",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");


        WinRetrieveDataMKBD = $("#WinRetrieveDataMKBD").kendoWindow({
            height: 150,
            title: "* Retrieve Data MKBD",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

    }
    var GlobValidatorJournal = $("#WinJournal").kendoValidator().data("kendoValidator");
    function validateDataJournal() {
        
        if (GlobValidatorJournal.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }
    var GlobValidatorJournalDetail = $("#WinJournalDetail").kendoValidator().data("kendoValidator");
    function validateDataJournalDetail() {
        
        if (GlobValidatorJournalDetail.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function validateDepartment() {
        if (_GlobClientCode == "12")
        {
            return 1;
        }
        else
        {
            if (GlobValidatorJournalDetail.validate()) {
                //alert("Validation sucess");
                return 1;
            }
            else {
                if ($('#DAccountPK').val() == 2) {
                    $("#DDepartmentPK").attr("required", false);
                }
                else {
                    $("#DDepartmentPK").attr("required", true);
                }
            }
        }

    }
    function showDetails(e) {
        var dataItemX;
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#StatusHeader").text("NEW");
            $("#TrxInformation").hide();
            $("#BtnPosting").hide();
            $("#BtnReversed").hide();
            $("#ValueDate").data("kendoDatePicker").value(_d);
            GlobStatus = 0;
        } else {
            if (e.handled == true) {
                return;
            }
            e.handled = true;

            if (tabindex == 0 || tabindex == undefined) {
                var grid = $("#gridJournalApproved").data("kendoGrid");
                GlobStatus = 2;
            }
            if (tabindex == 1) {
                var grid = $("#gridJournalPending").data("kendoGrid");
                GlobStatus = 1;
            }
            if (tabindex == 2) {
                var grid = $("#gridJournalHistory").data("kendoGrid");
                GlobStatus = 3;
            }
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnReversed").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Reversed == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnVoid").hide();
                $("#BtnReversed").show();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Reversed == 1) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnReversed").hide();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Reversed == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnPosting").show();
                $("#BtnReversed").hide();
                $("#TrxInformation").show();
                $("#BtnUpdate").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnReversed").hide();
                $("#BtnOldData").hide();

            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnReversed").hide();
                $("#BtnOldData").hide();
            }
            dirty = null;

            $("#JournalPK").val(dataItemX.JournalPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Status").val(dataItemX.Status);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(new Date(dataItemX.ValueDate));
            $("#TrxNo").val(dataItemX.TrxNo);
            $("#TrxName").val(dataItemX.TrxName);
            $("#Reference").val(dataItemX.Reference);
            $("#Type").val(dataItemX.Type);
            $("#Description").val(dataItemX.Description);
            if (dataItemX.Posted == true) {
                $("#Posted").prop('checked', true);
            }

            if (dataItemX.Posted == false && dataItemX.Reversed == false) {
                $("#State").removeClass("Posted").removeClass("Reversed").addClass("ReadyForPosting");
                $("#State").text("READY FOR POSTING");

            }
            if (dataItemX.Posted == true) {
                $("#State").removeClass("ReadyForPosting").removeClass("Reversed").addClass("Posted");
                $("#State").text("POSTED");
            }
            if (dataItemX.Reversed == true) {
                $("#State").removeClass("Posted").removeClass("ReadyForPosting").addClass("Reversed");
                $("#State").text("REVERSED");
            }
            $("#PostedBy").text(dataItemX.PostedBy);
            $("#ReversedBy").text(dataItemX.ReversedBy);
            $("#ReverseNo").val(dataItemX.ReverseNo);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            if (dataItemX.ReversedTime == null) {
                $("#ReversedTime").text("");
            } else {
                $("#ReversedTime").text(kendo.toString(kendo.parseDate(dataItemX.ReversedTime), 'MM/dd/yyyy HH:mm:ss'));
            }
            if (dataItemX.PostedTime == null) {
                $("#PostedTime").text("");
            } else {
                $("#PostedTime").text(kendo.toString(kendo.parseDate(dataItemX.PostedTime), 'MM/dd/yyyy HH:mm:ss'));
            }

            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
            $("#gridJournalDetail").empty();

            initGridJournalDetail();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    dataSource: data,
                    enabled: false,
                    change: OnChangePeriodPK,
                    value: setCmbPeriodPK()
                });


            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangePeriodPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbPeriodPK() {
            if (e == null) {
                return _defaultPeriodPK;
            } else {
                return dataItemX.PeriodPK;
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/JournalType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: OnChangeType,
                    value: setCmbType(),
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeType() {

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
            clearData()
            showButton();
            $("#gridJournalDetail").empty();
            refresh();
    }
    function clearData() {
        GlobValidatorJournal.hideMessages();
        $("#JournalPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#TrxNo").val("");
        $("#TrxName").val("");
        $("#Reference").val("");
        $("#Type").val("");
        $("#Description").val("");
        $("#Reversed").prop('checked', false);
        $("#ReverseNo").val("");
        $("#ReverseBy").val("");
        $("#ReversedTime").val("");
        $("#Posted").prop('checked', false);
        $("#PostedBy").val("");
        $("#PostedTime").val("");
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
                 pageSize: 10,
                 schema: {
                     model: {
                         fields: {
                             JournalPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "String" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "date" },
                             TrxNo: { type: "string" },
                             TrxName: { type: "string" },
                             Reference: { type: "string" },
                             Type: { type: "string" },
                             TypeDesc: { type: "string" },
                             Description: { type: "string" },
                             Posted: { type: "boolean" },
                             PostedBy: { type: "string" },
                             PostedTime: { type: "date" },
                             Reversed: { type: "boolean" },
                             ReverseNo: { type: "number" },
                             ReversedBy: { type: "string" },
                             ReversedTime: { type: "date" },
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
        if (tabindex == 0 || tabindex == undefined) {
            initGrid()
        }
        if (tabindex == 1) {
            RecalGridPending();
        }
        if (tabindex == 2) {
            RecalGridHistory();
        }

    }
  
    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };
    function gridApprovedOnDataBound() {
        var grid = $("#gridJournalApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPending");
            } else if (row.Reversed == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }
    function gridHistoryDataBound() {
        var grid = $("#gridJournalHistory").data("kendoGrid");
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
    function initGrid() {
        
        $("#gridJournalApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var JournalApprovedURL = window.location.origin + "/Radsoft/Journal/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(JournalApprovedURL);
        }

        //$("#gridJournalDetail").empty();
        var grid = $("#gridJournalApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Journal"
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
            dataBound: gridApprovedOnDataBound,
            toolbar: ["excel"],
            detailInit: journalDetailGrid,
            columns: [
               { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
               //{ command: { text: "Posting", click: showPosting }, title: " ", width: 90 },
               //{ command: { text: "Reverse", click: showReverse }, title: " ", width: 90 },
               {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
               },
               { field: "JournalPK", title: "SysNo.", width: 95 },
               { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
               { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
               { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
               { field: "Reversed", title: "Reversed", width: 150, template: "#= Reversed ? 'Yes' : 'No' #" },
               { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
               { field: "RefNo", title: "RefNo", width: 200 },
               { field: "Reference", title: "Reference", width: 200 },
               { field: "Description", title: "Description", width: 350 },
               { field: "TrxNo", title: "Trx No", width: 200, attributes: { style: "text-align:right;" } },
               { field: "TrxName", title: "Trx Name", width: 200 },
               { field: "Type", title: "Type", hidden: true, width: 120 },
               { field: "TypeDesc", title: "Type", width: 200 },
               { field: "PeriodID", title: "Period ID", width: 200 },
               { field: "ReverseNo", title: "Reverse No", width: 200, attributes: { style: "text-align:right;" } },
               { field: "PostedBy ", title: "Posted By", width: 200 },
               { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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

        grid.table.on("click", ".cSelectedDetailApproved", selectDataApproved);

        function selectDataApproved(e) {
            

            var grid = $("#gridJournalApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _journalPK = dataItemX.JournalPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _journalPK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabJournal").kendoTabStrip({
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
                        RecalGridPending();
                    }
                    if (tabindex == 2) {
                        RecalGridHistory();
                    }
                } else {
                    refresh();
                }
            }
        });
        ResetButtonBySelectedData();
        $("#BtnRejectBySelected").hide();
        $("#BtnApproveBySelected").hide();
    }

    function ResetButtonBySelectedData()
    {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnVoidBySelected").show();
        $("#BtnReverseBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Journal/" + _a + "/" + _b,
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
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Journal/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $(".cSelectedDetail" + _b).prop('checked', _a);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }
    function RecalGridPending() {
        $("#gridJournalPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var JournalPendingURL = window.location.origin + "/Radsoft/Journal/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(JournalPendingURL);
        }
        var grid = $("#gridJournalPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Journal"
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
            detailInit: journalDetailGrid,
            columns: [
              { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
              {
                  field: "Selected",
                  width: 50,
                  template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                  headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                  filterable: true,
                  sortable: false,
                  columnMenu: false
              },
              { field: "JournalPK", title: "SysNo.", width: 95 },
              { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
              { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
              { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
              { field: "Reversed", title: "Reversed", width: 150, template: "#= Reversed ? 'Yes' : 'No' #" },
              { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              { field: "RefNo", title: "RefNo", width: 200 },
              { field: "Reference", title: "Reference", width: 200 },
              { field: "Description", title: "Description", width: 350 },
              { field: "TrxNo", title: "Trx No", width: 200, attributes: { style: "text-align:right;" } },
              { field: "TrxName", title: "Trx Name", width: 200 },
              { field: "Type", title: "Type", hidden: true, width: 120 },
              { field: "TypeDesc", title: "Type", width: 200 },
              { field: "PeriodID", title: "Period ID", width: 200 },
              { field: "ReverseNo", title: "Reverse No", width: 200, attributes: { style: "text-align:right;" } },
              { field: "PostedBy ", title: "Posted By", width: 200 },
              { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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
        }).data("kendoGrid");
        $("#SelectedAllPending").change(function () {
            
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }
            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Pending");

        });

        grid.table.on("click", ".cSelectedDetailPending", selectDataPending);

        function selectDataPending(e) {
            

            var grid = $("#gridJournalPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _journalPK = dataItemX.JournalPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _journalPK);

        }

        ResetButtonBySelectedData();
        $("#BtnPostingBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnReverseBySelected").hide();
    }
    function RecalGridHistory() {
        $("#gridJournalHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {
            var JournalHistoryURL = window.location.origin + "/Radsoft/Journal/GetDataFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(JournalHistoryURL);
        }
        $("#gridJournalHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Journal"
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
            dataBound: gridHistoryDataBound,
            resizable: true,
            toolbar: ["excel"],
            detailInit: journalDetailGrid,
            columns: [
              { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
              { field: "JournalPK", title: "SysNo.", width: 95 },
              { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
              { field: "Status", title: "Status", filterable: false, hidden: true, width: 120 },
              { field: "StatusDesc", title: "StatusDesc", width: 200 },
              { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
              { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
              { field: "Reversed", title: "Reversed", width: 150, template: "#= Reversed ? 'Yes' : 'No' #" },
              { field: "ValueDate", title: "Value Date", width: 180, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
              { field: "RefNo", title: "RefNo", width: 200 },
              { field: "Reference", title: "Reference", width: 200 },
              { field: "Description", title: "Description", width: 350 },
              { field: "TrxNo", title: "Trx No", width: 200, attributes: { style: "text-align:right;" } },
              { field: "TrxName", title: "Trx Name", width: 200 },
              { field: "Type", title: "Type", hidden: true, width: 120 },
              { field: "TypeDesc", title: "Type", width: 200 },
              { field: "PeriodID", title: "Period ID", width: 200 },
              { field: "ReverseNo", title: "Reverse No", width: 200, attributes: { style: "text-align:right;" } },
              { field: "PostedBy ", title: "Posted By", width: 200 },
              { field: "PostedTime", title: "P. Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
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

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnPostingBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnReverseBySelected").hide();
    }
    // bagian JournalDetail
    function refreshJournalDetailGrid() {
        var gridJournalDetail = $("#gridJournalDetail").data("kendoGrid");
        gridJournalDetail.dataSource.read();
    }
    function getDataSourceJournalDetail(_url) {
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
                 pageSize: 500,
                 aggregate: [{ field: "Amount", aggregate: "sum" },
                     { field: "Debit", aggregate: "sum" },
                     { field: "Credit", aggregate: "sum" },
                     { field: "BaseDebit", aggregate: "sum" },
                     { field: "BaseCredit", aggregate: "sum" }],

                 schema: {
                     model: {
                         fields: {
                             JournalPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             AutoNo: { type: "number" },
                             AccountPK: { type: "number" },
                             AccountID: { type: "string" },
                             AccountName: { type: "string" },
                             CurrencyPK: { type: "number" },
                             CurrencyID: { type: "string" },
                             OfficePK: { type: "number" },
                             OfficeID: { type: "string" },
                             OfficeName: { type: "string" },
                             DepartmentPK: { type: "number" },
                             DepartmentID: { type: "string" },
                             DepartmentName: { type: "string" },
                             AgentPK: { type: "number" },
                             AgentID: { type: "string" },
                             AgentName: { type: "string" },
                             CounterpartPK: { type: "number" },
                             CounterpartID: { type: "string" },
                             CounterpartName: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             ConsigneePK: { type: "number" },
                             ConsigneeID: { type: "string" },
                             ConsigneeName: { type: "string" },
                             DetailDescription: { type: "string" },
                             DocRef: { type: "string" },
                             DebitCredit: { type: "string" },
                             Amount: { type: "number" },
                             Debit: { type: "number" },
                             Credit: { type: "number" },
                             CurrencyRate: { type: "number" },
                             BaseDebit: { type: "number" },
                             BaseCredit: { type: "number" },
                             LastUsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }
    function showJournalDetail(e) {
        var dataItemX;
        if (e == null) {
            upOradd = 0;
            $("#DAutoNo").val("");
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;

            upOradd = 1;
            var grid = $("#gridJournalDetail").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            $("#DAutoNo").val(dataItemX.AutoNo);
            $("#DDetailDescription").val(dataItemX.DetailDescription);
            $("#DDocRef").val(dataItemX.DocRef);
            $("#DLastUsersID").val(dataItemX.LastUsersID);
            $("#DDebitCredit").val(dataItemX.DebitCredit);
            $("#DAmount").val(dataItemX.Amount);
            $("#DDebit").val(dataItemX.Debit);
            $("#DCredit").val(dataItemX.Credit);
            $("#DCurrencyRate").val(dataItemX.CurrencyRate);
            $("#DBaseDebit").val(dataItemX.BaseDebit);
            $("#DBaseCredit").val(dataItemX.BaseCredit);
            $("#DLastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));
        }

        //Combo box AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnlyExcludeCashRef/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DAccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeDAccountPk,
                    value: setDAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function setDAccountPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AccountPK;
            }
        }

        function onChangeDAccountPk() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
                return;
            }


            $.ajax({
                url: window.location.origin + "/Radsoft/Account/Account_LookupByAccountPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#DAccountPK").data("kendoComboBox").value(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#DCurrencyPK").data("kendoComboBox").value(data.CurrencyPK);
                    var LastRate = {
                        Date: $('#ValueDate').val(),
                        CurrencyPK: data.CurrencyPK,
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CurrencyRate/GetLastCurrencyRate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(LastRate),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#DCurrencyRate").data("kendoNumericTextBox").value(data);
                            recalAmount($("#DAmount").data("kendoNumericTextBox").value());
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

        //Combo box CurrencyPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDCurrencyPK,
                    enabled: false,
                    value: setDCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CurrencyPK;
            }
        }

        //Combo box OfficePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Office/GetOfficeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DOfficePK").kendoComboBox({
                    dataValueField: "OfficePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeDOfficePK,
                    dataSource: data,
                    value: setDOfficePK()
                });
                $("#DOfficePK").data("kendoComboBox").value(1);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDOfficePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDOfficePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OfficePK == 0) {
                    return "";
                }
                else {
                    return dataItemX.OfficePK;
                }

            }
        }

        //Combo box DepartmentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentComboShowFinanceOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DDepartmentPK").kendoComboBox({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeDDepartmentPK,
                    dataSource: data,
                    value: setDDepartmentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDDepartmentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDDepartmentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DepartmentPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.DepartmentPK;
                }
            }
        }

        //Combo box AgentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DAgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: OnChangeDAgentPK,
                    dataSource: data,
                    value: setDAgentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDAgentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDAgentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.AgentPK;
                }
            }
        }

        //Combo box CounterpartPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DCounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    filter: "contains",
                    change: OnChangeDCounterpartPK,
                    suggest: true,
                    dataSource: data,
                    value: setDCounterpartPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDCounterpartPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDCounterpartPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CounterpartPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.CounterpartPK;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DInstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    change: OnChangeDInstrumentPK,
                    suggest: true,
                    dataSource: data,
                    value: setDInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDInstrumentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                }
                else {
                    return dataItemX.InstrumentPK;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Consignee/GetConsigneeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DConsigneePK").kendoComboBox({
                    dataValueField: "ConsigneePK",
                    dataTextField: "ID",
                    filter: "contains",
                    change: OnChangeDConsigneePK,
                    suggest: true,
                    dataSource: data,
                    value: setCmbDConsigneePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDConsigneePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDConsigneePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ConsigneePK == 0) {
                    return "";
                } else {
                    if (dataItemX.ConsigneePK == 0) {
                        return "";
                    }
                    else {
                        return dataItemX.ConsigneePK;
                    }
                }
            }
        }


        $("#DDebitCredit").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "DEBIT", value: "D" },
                { text: "CREDIT", value: "C" },
            ],
            filter: "contains",
            suggest: true,
            change: onChangeDDebitCredit,
        });

            $("#DAmount").kendoNumericTextBox({
                format: "n" + GlobDecimalPlaces,
                decimals: GlobDecimalPlaces,
                change: onChangeDAmount,
            });
        


            $("#DDebit").kendoNumericTextBox({
                format: "n" + GlobDecimalPlaces,
                decimals: GlobDecimalPlaces,
            });
        

        $("#DDebit").data("kendoNumericTextBox").enable(false);

            $("#DCredit").kendoNumericTextBox({
                format: "n" + GlobDecimalPlaces,
                decimals: GlobDecimalPlaces,
            });
        

        $("#DCredit").data("kendoNumericTextBox").enable(false);


            $("#DCurrencyRate").kendoNumericTextBox({
                format: "n" + GlobDecimalPlaces,
                decimals: GlobDecimalPlaces,
            });
        

        $("#DCurrencyRate").data("kendoNumericTextBox").enable(false);


            $("#DBaseDebit").kendoNumericTextBox({
                format: "n" + GlobDecimalPlaces,
                decimals: GlobDecimalPlaces,
            });
        


        $("#DBaseDebit").data("kendoNumericTextBox").enable(false);

            $("#DBaseCredit").kendoNumericTextBox({
                format: "n" + GlobDecimalPlaces,
                decimals: GlobDecimalPlaces,
            });
        


        $("#DBaseCredit").data("kendoNumericTextBox").enable(false);

        winJournalDetail.center();
        winJournalDetail.open();

    }
    function DeletejournalDetail(e) {
        if (e.handled == true) {
            return;
        }
        e.handled = true;
        
    
            var dataItemX;
            var grid = $("#gridJournalDetail").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        
            $.ajax({
                url: window.location.origin + "/Radsoft/Journal/CheckJournalStatus/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#JournalPK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to DELETE detail ?", function (e) {
                            if (e) {

                                var JournalDetail = {
                                    JournalPK: dataItemX.JournalPK,
                                    LastUsersID: sessionStorage.getItem("user"),
                                    AutoNo: dataItemX.AutoNo
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/JournalDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "JournalDetail_D",
                                    type: 'POST',
                                    data: JSON.stringify(JournalDetail),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        refreshJournalDetailGrid();
                                        alertify.alert(data);
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            }
                        });
                    }
                    else
                    {
                        alertify.alert("Journal Has Posted");
                    }
                }
            });
    }
    function initGridJournalDetail() {
        var JournalDetailURL = window.location.origin + "/Radsoft/JournalDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + $("#JournalPK").val(),
          dataSourceJournalDetail = getDataSourceJournalDetail(JournalDetailURL);

        $("#gridJournalDetail").kendoGrid({
            dataSource: dataSourceJournalDetail,
            height: 600,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Journal Detail"
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
               {
                   command: { text: "show", click: showJournalDetail }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   command: { text: "Delete", click: DeletejournalDetail }, title: " ", width: 80, locked: true, lockable: false
               },
               {
                   field: "AutoNo", title: "No.", filterable: false, width: 70, locked: true, lockable: false
               },
                     { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               {
                   field: "AccountID", title: "Account ID", width: 250, locked: true, lockable: false
               },
               {
                   field: "AccountName", title: "Account Name", width: 250, locked: true, lockable: false
               },
               { field: "DebitCredit", title: "D/C", width: 70 },
               { field: "Amount", title: "Amount", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n" + GlobDecimalPlaces + "')#</div>", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "Debit", title: "Debit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n" + GlobDecimalPlaces + "')#</div>", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "Credit", title: "Credit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n" + GlobDecimalPlaces + "')#</div>", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "DetailDescription", title: "Detail Description", width: 300 },
               { field: "DocRef", title: "Doc Ref", width: 300 },
               { field: "JournalPK", title: "Journal No.", hidden: true, filterable: false, width: 120 },
               { field: "AccountPK", title: "AccountPK", hidden: true, width: 100 },
               { field: "CurrencyID", title: "Currency", width: 120 },
               { field: "CurrencyRate", title: "Rate", width: 170, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "BaseDebit", title: "BaseDebit", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", footerTemplate: "<div id='sumBaseDebit' style='text-align: right'>#= kendo.toString(sum, 'n" + GlobDecimalPlaces + "') #</div>", attributes: { style: "text-align:right;" } },
               { field: "BaseCredit", title: "BaseCredit", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", footerTemplate: "<div id='sumBaseCredit' style='text-align: right'>#= kendo.toString(sum, 'n" + GlobDecimalPlaces + "') #</div>", attributes: { style: "text-align:right;" } },
               { field: "OfficeID", title: "Office ID", width: 200 },
               { field: "OfficeName", title: "Name", width: 200 },
               { field: "DepartmentID", title: "Department ID", width: 200 },
               { field: "DepartmentName", title: "Name", width: 200 },
               { field: "ConsigneeID", title: "ConsigneeID ID", width: 200 },
               { field: "ConsigneeName", title: "Name", width: 200 },
               { field: "AgentID", title: "Agent ID", width: 200 },
               { field: "AgentName", title: "Name", width: 200 },
               { field: "InstrumentID", title: "Instrument ID", width: 200 },
               { field: "InstrumentName", title: "Name", width: 200 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });

    }
    function journalDetailGrid(e) {
        var JournalDetailURL = window.location.origin + "/Radsoft/JournalDetail/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + e.data.JournalPK,
         dataSourceJournalDetail = getDataSourceJournalDetail(JournalDetailURL);

        $("<div/>").appendTo(e.detailCell).kendoGrid({
            dataSource: dataSourceJournalDetail,
            height: 250,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Journal Detail"
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
               {
                   field: "AutoNo", title: "No.", filterable: false, width: 70, locked: true, lockable: false
               },
                     { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
               {
                   field: "AccountID", title: "Account ID", width: 250, locked: true, lockable: false
               },
               {
                   field: "AccountName", title: "Account Name", width: 250, locked: true, lockable: false
               },
               { field: "DebitCredit", title: "D/C", width: 70 },
               { field: "Amount", title: "Amount", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n" + GlobDecimalPlaces + "')#</div>", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "Debit", title: "Debit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n" + GlobDecimalPlaces + "')#</div>", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "Credit", title: "Credit", width: 200, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n" + GlobDecimalPlaces + "')#</div>", format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "DetailDescription", title: "Detail Description", width: 300 },
               { field: "DocRef", title: "Doc Ref", width: 300 },
               { field: "JournalPK", title: "Journal No.", hidden: true, filterable: false, width: 120 },
               { field: "AccountPK", title: "AccountPK", hidden: true, width: 100 },
               { field: "CurrencyID", title: "Currency", width: 120 },
               { field: "CurrencyRate", title: "Rate", width: 170, format: "{0:n" + GlobDecimalPlaces + "}", attributes: { style: "text-align:right;" } },
               { field: "BaseDebit", title: "BaseDebit", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", footerTemplate: "<div id='sumBaseDebit' style='text-align: right'>#= kendo.toString(sum, 'n" + GlobDecimalPlaces + "') #</div>", attributes: { style: "text-align:right;" } },
               { field: "BaseCredit", title: "BaseCredit", width: 200, format: "{0:n" + GlobDecimalPlaces + "}", footerTemplate: "<div id='sumBaseCredit' style='text-align: right'>#= kendo.toString(sum, 'n" + GlobDecimalPlaces + "') #</div>", attributes: { style: "text-align:right;" } },
               { field: "OfficeID", title: "Office ID", width: 200 },
               { field: "OfficeName", title: "Name", width: 200 },
               { field: "DepartmentID", title: "Department ID", width: 200 },
               { field: "DepartmentName", title: "Name", width: 200 },
               { field: "ConsigneeID", title: "ConsigneeID ID", width: 200 },
               { field: "ConsigneeName", title: "Name", width: 200 },
               { field: "AgentID", title: "Agent ID", width: 200 },
               { field: "AgentName", title: "Name", width: 200 },
               { field: "InstrumentID", title: "Instrument ID", width: 200 },
               { field: "InstrumentName", title: "Name", width: 200 },
               { field: "LastUsersID", title: "LastUsersID", width: 180 },
               { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 200 }
            ]
        });
    }
    function onChangeDAmount() {
        recalAmount(this.value());
    }
    function onChangeDDebitCredit() {
        if (this.value() && this.selectedIndex == -1) {
            var dt = this.dataSource._data[0];
            this.text('');
        }
        recalAmount($("#DAmount").data("kendoNumericTextBox").value());
    }
    function recalAmount(_amount) {
        if ($("#DDebitCredit").data("kendoComboBox").value() == 'D') {
            $("#DDebit").data("kendoNumericTextBox").value(_amount);
            $("#DCredit").data("kendoNumericTextBox").value(0);
            $("#DBaseDebit").data("kendoNumericTextBox").value(_amount * $("#DCurrencyRate").data("kendoNumericTextBox").value());
            $("#DBaseCredit").data("kendoNumericTextBox").value(0);
        } else if ($("#DDebitCredit").data("kendoComboBox").value() == 'C') {
            $("#DDebit").data("kendoNumericTextBox").value(0);
            $("#DCredit").data("kendoNumericTextBox").value(_amount);
            $("#DBaseDebit").data("kendoNumericTextBox").value(0);
            $("#DBaseCredit").data("kendoNumericTextBox").value(_amount * $("#DCurrencyRate").data("kendoNumericTextBox").value());
        } else if (_amount > 0) {
            alertify.alert("Please Choose Debit or Credit");
        }
    }
    function onWinJournalDetailClose() {
        GlobValidatorJournalDetail.hideMessages();
        $("#DAccountPK").val("");
        $("#DCurrencyPK").val("");
        $("#DOfficePK").val("");
        $("#DDepartmentPK").val("");
        $("#DAgentPK").val("");
        $("#DCounterpartPK").val("");
        $("#DInstrumentPK").val("");
        $("#DDetailDescription").val("");
        $("#DDocRef").val("");
        $("#DDebitCredit").val("");
        $("#DAmount").val("");
        $("#DDebit").val("");
        $("#DCredit").val("");
        $("#DCurrencyRate").val("");
        $("#DBaseDebit").val("");
        $("#DBaseCredit").val("");
        $("#DLastUsersID").val("");
        $("#DLastUpdate").val("");
    }
    function onWinJournalPostingClose() {

        $("#Posting").val("");
        $("#PostPeriodPK").val("");
        $("#PostDateFrom").val("");
        $("#PostDateTo").val("");
        $("#PostJournalFrom").val("");
        $("#PostJournalTo").val("");
        $("#LblPostDateFrom").hide();
        $("#CmbPostDateFrom").hide();
        $("#LblPostDateTo").hide();
        $("#LblPostJournalFrom").show();
        $("#LblPostJournalTo").show();
        refresh();
    }

    $("#BtnRefreshDetail").click(function () {
        refreshJournalDetailGrid();
    });
    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#JournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "Journal",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Journal" + "/" + $("#JournalPK").val(),
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
    $("#BtnRefresh").click(function () {
        refresh();

    });
    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });
    $("#BtnClose").click(function () {
        
        alertify.confirm("Are you sure want to Close and Clear Detail?", function (e) {
            if (e) {
                winJournalDetail.close();
                alertify.alert("Close Detail");
            }
        });
    });
    $("#BtnNew").click(function () {
        showDetails(null);
    });

    $("#BtnAddDetail").click(function () {
        
        if ($("#JournalPK").val() == 0 || $("#JournalPK").val() == null) {
            alertify.alert("There's no Journal Header");
        } else if (GlobStatus == 3) { //SEKURITAS : else if (GlobStatus == 2 || GlobStatus == 3) {
            alertify.alert("Journal Already History");
        } else {
            showJournalDetail();
        }
    });
    $("#BtnSave").click(function () {
        var validate = validateDepartment();
        var val = validateDataJournalDetail();
        if (val == 1) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Journal/CheckJournalStatus/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#JournalPK').val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        if ($("#DAutoNo").val() == "") {
                            alertify.confirm("Are you sure want to Add JOURNAL DETAIL ?", function (e) {
                                if (e) {

                                    var JournalDetail = {
                                        JournalPK: $('#JournalPK').val(),
                                        AutoNo: $('#DAutoNo').val(),
                                        Status: 2,
                                        AccountPK: $('#DAccountPK').val(),
                                        CurrencyPK: $('#DCurrencyPK').val(),
                                        OfficePK: $('#DOfficePK').val(),
                                        DepartmentPK: $('#DDepartmentPK').val(),
                                        AgentPK: $('#DAgentPK').val(),
                                        CounterpartPK: $('#DCounterpartPK').val(),
                                        InstrumentPK: $('#DInstrumentPK').val(),
                                        ConsigneePK: $('#DConsigneePK').val(),
                                        DetailDescription: $('#DDetailDescription').val(),
                                        DocRef: $('#DDocRef').val(),
                                        DebitCredit: $('#DDebitCredit').val(),
                                        Amount: $('#DAmount').val(),
                                        Debit: $('#DDebit').val(),
                                        Credit: $('#DCredit').val(),
                                        CurrencyRate: $('#DCurrencyRate').val(),
                                        BaseDebit: $('#DBaseDebit').val(),
                                        BaseCredit: $('#DBaseCredit').val(),
                                        LastUsersID: sessionStorage.getItem("user")

                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/JournalDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "JournalDetail_I",
                                        type: 'POST',
                                        data: JSON.stringify(JournalDetail),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $("#gridJournalDetail").empty();
                                            initGridJournalDetail();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                            });
                        } else {
                            alertify.confirm("Are you sure want to UPDATE JOURNAL DETAIL ?", function (e) {
                                if (e) {

                                    var JournalDetail = {
                                        JournalPK: $('#JournalPK').val(),
                                        AutoNo: $('#DAutoNo').val(),
                                        Status: 2,
                                        AccountPK: $('#DAccountPK').val(),
                                        CurrencyPK: $('#DCurrencyPK').val(),
                                        OfficePK: $('#DOfficePK').val(),
                                        DepartmentPK: $('#DDepartmentPK').val(),
                                        AgentPK: $('#DAgentPK').val(),
                                        CounterpartPK: $('#DCounterpartPK').val(),
                                        InstrumentPK: $('#DInstrumentPK').val(),
                                        ConsigneePK: $('#DConsigneePK').val(),
                                        DetailDescription: $('#DDetailDescription').val(),
                                        DocRef: $('#DDocRef').val(),
                                        DebitCredit: $('#DDebitCredit').val(),
                                        Amount: $('#DAmount').val(),
                                        Debit: $('#DDebit').val(),
                                        Credit: $('#DCredit').val(),
                                        CurrencyRate: $('#DCurrencyRate').val(),
                                        BaseDebit: $('#DBaseDebit').val(),
                                        BaseCredit: $('#DBaseCredit').val(),
                                        LastUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/JournalDetail/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "JournalDetail_U",
                                        type: 'POST',
                                        data: JSON.stringify(JournalDetail),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            $("#gridJournalDetail").empty();
                                            initGridJournalDetail();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });
                                }
                            });
                        }
                    }
                    else {
                        alertify.alert("Journal Has Posted");
                    }
                }
            });
            
        }
    });
    $("#BtnResetFilter").click(function () {
        $("#DateFrom").data("kendoDatePicker").value(null);
        $("#DateTo").data("kendoDatePicker").value(null);
        refresh();
    });
    $("#BtnAdd").click(function () {
        if ($("#JournalPK").val() > 0) {
            alertify.alert("JOURNAL HEADER ALREADY EXIST, Cancel and click add new to add more Header");
            return
        }
        var val = validateDataJournal();
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add JOURNAL HEADER ?", function (e) {
                if (e) {
                    if ($('#Reference').val() == null || $('#Reference').val() == "") {
                        setTimeout(function () {
                            alertify.confirm("Are you sure want to Generate New Reference ?", function (e) {
                                if (e) {
                                    var _urlRef = "";
                                    if (_GlobClientCode == "04") {
                                        _urlRef = "/Radsoft/CustomClient04/JournalReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#Type").data("kendoComboBox").text() + "/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                    } else if (_GlobClientCode == "05") {
                                        _urlRef = "/Radsoft/CustomClient05/JournalReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "GJ" + "/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                    } else if (_GlobClientCode == "21") {
                                        _urlRef = "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/GJ/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                    }
                                    else {
                                        _urlRef = "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/ADJ/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference";
                                    }
                                    $.ajax({
                                        url: window.location.origin + _urlRef,
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            $("#Reference").val(data);
                                            //alertify.alert("Your new reference is " + data);
                                            var Journal = {
                                                ValueDate: $('#ValueDate').val(),
                                                PeriodPK: $("#PeriodPK").val(),
                                                Reference: data,
                                                Type: $('#Type').val(),
                                                Description: $('#Description').val(),
                                                EntryUsersID: sessionStorage.getItem("user")

                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_I",
                                                type: 'POST',
                                                data: JSON.stringify(Journal),
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    alertify.alert(data.Message);
                                                    $("#JournalPK").val(data.JournalPK);
                                                    $("#HistoryPK").val(data.HistoryPK);
                                                    refresh();
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
                            })
                        }, 1000);
                    }
                    else {
                        var Journal = {
                            ValueDate: $('#ValueDate').val(),
                            PeriodPK: $("#PeriodPK").val(),
                            Reference: $("#Reference").val(),
                            Type: $('#Type').val(),
                            Description: $('#Description').val(),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_I",
                            type: 'POST',
                            data: JSON.stringify(Journal),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data.Message);
                                $("#JournalPK").val(data.JournalPK);
                                $("#HistoryPK").val(data.HistoryPK);
                                refresh();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                }
            });
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateDataJournal();
        if (val == 1) {
            if ($("#sumBaseDebit").text() != $("#sumBaseCredit").text()) {
                alertify.alert("Journal Not Balance");
                return;
            }
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#JournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "Journal",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var Journal = {
                                    JournalPK: $('#JournalPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ValueDate: $('#ValueDate').val(),
                                    PeriodPK: $("#PeriodPK").val(),
                                    Reference: $('#Reference').val(),
                                    TrxNo: $('#TrxNo').val(),
                                    TrxName: $('#TrxName').val(),
                                    Type: $('#Type').val(),
                                    Description: $('#Description').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_U",
                                    type: 'POST',
                                    data: JSON.stringify(Journal),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
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
    $("#BtnApproved").click(function () {
        
        if ($("#sumBaseDebit").text() != $("#sumBaseCredit").text()) {
            alertify.alert("Journal Not Balance");
            return;
        }
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#JournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "Journal",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Journal = {
                                JournalPK: $('#JournalPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_A",
                                type: 'POST',
                                data: JSON.stringify(Journal),
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
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVERSED") {
                alertify.alert("Journal Already Posted / Reversed, Void Cancel");
            } else {
                if (e) {
                    var Journal = {
                        JournalPK: $('#JournalPK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        VoidUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_V",
                        type: 'POST',
                        data: JSON.stringify(Journal),
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
            }
        });
    });
    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                var Journal = {
                    JournalPK: $('#JournalPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_R",
                    type: 'POST',
                    data: JSON.stringify(Journal),
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

    $("#BtnDownloadMaster").click(function () {
        
        alertify.confirm("Are you sure want to Download data Master ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Reports/MasterDataReport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.location = data
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnPosting").click(function () {
        
        if ($("#sumBaseDebit").text() != $("#sumBaseCredit").text()) {
            alertify.alert("Journal Not Balance");
            return;
        }
        alertify.confirm("Are you sure want to Posting JOURNAL?", function (e) {
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVERSED") {
                alertify.alert("Journal Already Posted / Reversed, Posting Cancel");
            } else {
                if (e) {
                    if (dirty == true) {
                        alertify.alert("Change must be Update First, Posting Cancel");
                        return;
                    }

                    if ($("#Posted").is(":checked")) {
                        alert("Journal already Posted");
                        return;
                    }

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#JournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "Journal",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var Journal = {
                                    JournalPK: $('#JournalPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    PostedBy: sessionStorage.getItem("user"),
                                    PeriodPK: $('#PeriodPK').val()
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_Posting",
                                    type: 'POST',
                                    data: JSON.stringify(Journal),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#State").removeClass("ReadyForPosting").removeClass("Reserved").addClass("Posted");
                                        $("#State").text("POSTED");
                                        $("#PostedBy").text(sessionStorage.getItem("user"));
                                        $("#Posted").prop('checked', true);
                                        $("#Reserved").prop('checked', false);
                                        alertify.alert(data);
                                        win.close();
                                        refresh()
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
            }
        });
    });
    function showPosting(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to Posting ?", function (a) {
                if (a) {
                    var grid = $("#gridJournalApproved").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
                    var pos = _dataItemX.Posted;

                    if (pos == true) {
                        alertify.alert("Data Already Posted");
                    }
                    else {

                        var _journalPK = _dataItemX.JournalPK;
                        var _periodPK = _dataItemX.PeriodPK;
                        var _historyPK = _dataItemX.HistoryPK;
                        var Posting = {
                            JournalPK: _journalPK,
                            PeriodPK: _periodPK,
                            HistoryPK: _historyPK,
                            PostedBy: sessionStorage.getItem("user")
                        };



                        $.ajax({
                            url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_Posting",
                            type: 'POST',
                            data: JSON.stringify(Posting),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
                                refresh();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });

                    }



                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }
    $("#BtnReversed").click(function () {
        
        alertify.confirm("Are you sure want to Reversed JOURNAL?", function (e) {

            if ($("#Type").val() >= 3) {
                alertify.alert("Transaction Or Cashier Can't Be Reverse From Here.");
                return;
            }
            if ($("#State").text() == "REVERSED") {

            } else {
                if (e) {
                    if (dirty == true) {
                        alertify.alert("Change must be Update First, Reversed Cancel");
                        return;
                    }

                    if ($("#Reversed").is(":checked")) {
                        alertify.alert("Journal already Reversed");
                        return;
                    }
                    if (!$("#Posted").is(":checked")) {
                        alertify.alert("Journal Still Pending");
                        return;
                    }

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#JournalPK").val() + "/" + $("#HistoryPK").val() + "/" + "Journal",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var Journal = {
                                    JournalPK: $('#JournalPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ReversedBy: sessionStorage.getItem("user"),
                                    PeriodPK: $('#PeriodPK').val()
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_Reversed",
                                    type: 'POST',
                                    data: JSON.stringify(Journal),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#State").removeClass("ReadyForPosting").removeClass("Posted").addClass("Reserved");
                                        $("#State").text("REVERSED");
                                        $("#ReversedBy").text(sessionStorage.getItem("user"));
                                        $("#Reversed").prop('checked', true);
                                        $("#Posted").prop('checked', false);
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
            }
        });
    });
    function showReverse(e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to Reversed ?", function (a) {
                if (a) {
                    var grid = $("#gridJournalApproved").data("kendoGrid");
                    var _dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

                    if (_dataItemX.Type >= 3) {
                        alertify.alert("Transaction Or Cashier Can't Be Reverse From Here.");
                        return;
                    }

                    if (_dataItemX.Reversed == true) {
                        alertify.alert("Data Already Reversed");
                    }
                    else if (_dataItemX.Posted == false) {
                        alertify.alert("Data Not Posting Yet");
                    }
                    else {
                        var _journalPK = _dataItemX.JournalPK;
                        var _periodPK = _dataItemX.PeriodPK;
                        var _historyPK = _dataItemX.HistoryPK;
                        var Reversed = {
                            JournalPK: _journalPK,
                            PeriodPK: _periodPK,
                            HistoryPK: _historyPK,
                            ReversedBy: sessionStorage.getItem("user")

                        };

                        $.ajax({
                            url: window.location.origin + "/Radsoft/Journal/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Journal_Reversed",
                            type: 'POST',
                            data: JSON.stringify(Reversed),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
                                refresh();
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });

                    }
                } else {
                    alertify.alert("You've clicked Cancel");
                }
            });
            e.handled = true;
        }
    }

    function validateDataPosting() {
        if ($("#PostDateFrom").data("kendoDatePicker").value() > $("#PostDateTo").data("kendoDatePicker").value()) {
            alertify.alert("Date From >  Date To");
            return 0;
        }
        else if ($("#PostJournalFrom").data("kendoComboBox").value() > $("#PostJournalTo").data("kendoComboBox").value()) {
            alertify.alert("Journal From >  Journal To");
            return 0;
        }

        else {
            return 1;
        }
    }
    $("#BtnVoucher").click(function () {
        
        alertify.confirm("Are you sure want to Download Journal Voucher ?", function (e) {
            if (e) {
                var Journal = {
                    JournalPK: $('#JournalPK').val()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/JournalVoucher/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(Journal),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.open(data);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/ValidateCheckSumJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Journal/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert(data);
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnRejectBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnVoidBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });
    $("#BtnPostingBySelected").click(function () {
        
        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                if (_GlobClientCode == "12") {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Journal/ValidateCheckStatusPosting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == "") {
                                PostingBySelected();
                            } else {
                                alertify.alert(data);
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
                else {
                    PostingBySelected();
                }
            }
        });
    });

    function PostingBySelected() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Journal/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                alertify.alert(data);
                refresh();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
            
    }

    $("#BtnReverseBySelected").click(function () {
        
        alertify.confirm("Are you sure want to Reverse by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/ReverseBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


    $("#BtnRetrieveMFee").click(function () {
        showRetrieveMFee();
    });

    function showRetrieveMFee(e) {

        WinRetrieveMFee.center();
        WinRetrieveMFee.open();

    }

    $("#BtnOkRetrieveMFee").click(function () {
        

        alertify.confirm("Are you sure want to Retrieve Management Fee ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/ValidateCheckRetrieveMKBDResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ManagementFee",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Journal/ValidateCheckJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/ManagementFee",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == "") {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/FundDailyFee/ValidateCheckNotExistsFundDailyFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == "") {

                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/Journal/RetrieveManagementFeeByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {

                                                            alertify.alert(data)
                                                            WinRetrieveMFee.close();
                                                            refresh();
                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                        }
                                                    });
                                                } else {
                                                    alertify.alert(data);
                                                }
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    } else {
                                        alertify.alert(data);
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert(data);
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnCancelRetrieveMFee").click(function () {
        
        alertify.confirm("Are you sure want to cancel Retrieve Management Fee?", function (e) {
            if (e) {
                WinRetrieveMFee.close();
                alertify.alert("Cancel Retrieve Management Fee");
            }
        });
    });

    function getDataSourceListReference() {

        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + "/Radsoft/Journal/ReferenceSelectFromJournal/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#PeriodPK").data("kendoComboBox").value(),
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                          this.cancelChanges();
                      },
                      pageSize: 25,
                      schema: {
                          model: {
                              fields: {
                                  Reference: { type: "string" }

                              }
                          }
                      }
                  });
    }

    function initListReference() {
        var dsListReference = getDataSourceListReference();
        $("#gridListReference").kendoGrid({
            dataSource: dsListReference,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: true,
            pageable: true,
            columnMenu: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListReferenceSelect }, title: " ", width: 100 },
               { field: "Reference", title: "Reference", width: 100 }
            ]
        });
    }

    function onWinListReferenceClose() {
        $("#gridListReference").empty();
    }

    function ListReferenceSelect(e) {
        var grid = $("#gridListReference").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlReference).val(dataItemX.Reference);
        //$("#Reference").val(dataItemX.Reference);
        WinListReference.close();


    }

    $("#btnListReference").click(function () {
        WinListReference.center();
        WinListReference.open();
        initListReference();
        htmlReference = "#Reference";
    });

    $("#BtnRetrieveClosePriceReksadana").click(function () {
        showRetrieveClosePriceReksadana();
    });

    function showRetrieveClosePriceReksadana(e) {

        WinRetrieveClosePriceReksadana.center();
        WinRetrieveClosePriceReksadana.open();

    }

    $("#BtnOkRetrieveClosePriceReksadana").click(function () {

        
        alertify.confirm("Are you sure want to Retrieve ClosePrice Reksadana ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/ValidateCheckRetrieveMKBDResult/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFromA").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateToA").data("kendoDatePicker").value(), "MM-dd-yy") + "/ClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "") {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/ValidateNotCheckExistsCloseNAVForAccounting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFromA").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateToA").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == "") {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/ClosePrice/ValidateCheckExistsClosePriceReksadanaForAccounting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFromA").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateToA").data("kendoDatePicker").value(), "MM-dd-yy"),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                if (data == "") {
                                                    $.ajax({
                                                        url: window.location.origin + "/Radsoft/Journal/RetrieveClosePriceReksadanaByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFromA").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateToA").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                        type: 'GET',
                                                        contentType: "application/json;charset=utf-8",
                                                        success: function (data) {

                                                            alertify.alert(data)
                                                            WinRetrieveClosePriceReksadana.close();

                                                        },
                                                        error: function (data) {
                                                            alertify.alert(data.responseText);
                                                        }
                                                    });
                                                } else {
                                                    alertify.alert(data);
                                                }
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    } else {
                                        alertify.alert(data);
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert(data);
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnCancelRetrieveClosePriceReksadana").click(function () {
        
        alertify.confirm("Are you sure want to cancel Retrieve Close Price Reksadana?", function (e) {
            if (e) {
                WinRetrieveClosePriceReksadana.close();
                alertify.alert("Cancel Retrieve Close Price Reksadana");
            }
        });
    });

    $("#BtnRetrieveDataMKBD").click(function () {
        showRetrieveDataMKBD();
    });

    function showRetrieveDataMKBD(e) {

        WinRetrieveDataMKBD.center();
        WinRetrieveDataMKBD.open();

    }

    $("#BtnOkRetrieveDataMKBD").click(function () {


        alertify.confirm("Are you sure want to Retrieve Data MKBD ?", function (e) {
            if (e) {


                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/RetrieveDataMKBDByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamDateFromMKBD").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ParamDateToMKBD").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        alertify.alert(data)
                        WinRetrieveDataMKBD.close();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });
    });

    $("#BtnCancelRetrieveDataMKBD").click(function () {

        alertify.confirm("Are you sure want to cancel Retrieve Data MKBD ?", function (e) {
            if (e) {
                WinRetrieveClosePriceReksadana.close();
                alertify.alert("Cancel Retrieve Data MKBD");
            }
        });
    });


    $("#BtnExportJournal").click(function () {
        
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var _permission = "Journal_O";
                var JournalExport = {
                    ReportName: $('#Name').val(),
                    ValueDateFrom: $('#DateFrom').val(),
                    ValueDateTo: $('#DateTo').val(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Journal/JournalExport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(JournalExport),
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