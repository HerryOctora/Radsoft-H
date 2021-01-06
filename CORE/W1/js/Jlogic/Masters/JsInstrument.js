$(document).ready(function () {
    document.title = 'FORM INSTRUMENT';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListIssuer;
    var htmlIssuerPK;
    var htmlIssuerID;
    var sector;
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

        $("#BtnImportInstrument").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnExportInstrument").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
    }

    

    function initWindow() {
        $("#IssueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

        });

        $("#MaturityDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

        });

        $("#FirstCouponDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

        });




        win = $("#WinInstrument").kendoWindow({
            height: 1050,
            title: "Instrument Detail",
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

        WinListIssuer = $("#WinListIssuer").kendoWindow({
            height: "520px",
            title: "Issuer List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListIssuerClose
        }).data("kendoWindow");

    }

    $("#BtnImportInstrument").click(function () {
        document.getElementById("FileImportInstrument").click();
    });

    $("#FileImportInstrument").change(function () {
        $.blockUI({});
        
        var data = new FormData();
        var files = $("#FileImportInstrument").get(0).files;
        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        if (files.length > 0) {
            data.append("Instrument", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Instrument_Import",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportInstrument").val("");
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportInstrument").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $.unblockUI();
            $("#FileImportInstrument").val("");
        }
    });

    $("#BtnExportInstrument").click(function () {

        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
                var _permission = "Instrument_O";
                var InstrumentExport = {
                    ReportName: $('#Name').val(),
                    ValueDateFrom: $('#DateFrom').val(),
                    ValueDateTo: $('#DateTo').val(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Instrument/InstrumentExport/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(InstrumentExport),
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

    var GlobValidator = $("#WinInstrument").kendoValidator().data("kendoValidator");

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

            HideLabel();
            RequiredAttributes();
        } else {
            RequiredAttributes();
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

            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#Category").val(dataItemX.Category);
            $("#InstrumentTypeID").val(dataItemX.InstrumentTypeID);
            $("#ReksadanaTypeID").val(dataItemX.ReksadanaTypeID);
            //$("#DepositoTypeID").val(dataItemX.DepositoTypeID);
            $("#ISIN").val(dataItemX.ISIN);
            //$("#SAPCust").val(dataItemX.SAPCust);
            $("#BankID").val(dataItemX.BankID + " - " + dataItemX.BankName);
            $("#IssuerID").val(dataItemX.IssuerID + " - " + dataItemX.IssuerName);
            $("#SectorID").val(dataItemX.SectorID + " - " + dataItemX.SectorName);
            $("#HoldingID").val(dataItemX.HoldingID + " - " + dataItemX.HoldingName);
            $("#MarketID").val(dataItemX.MarketID);
            $("#IssueDate").data("kendoDatePicker").value(dataItemX.IssueDate);
            $("#MaturityDate").data("kendoDatePicker").value(dataItemX.MaturityDate);
            $("#FirstCouponDate").data("kendoDatePicker").value(dataItemX.FirstCouponDate);
            $("#LotInShare").val(100);
            $("#CurrencyID").val(dataItemX.CurrencyID);
            $("#BloombergCode").val(dataItemX.BloombergCode);
            $("#CounterpartID").val(dataItemX.CounterpartID + " - " + dataItemX.CounterpartID);
            $("#BankAccountNo").val(dataItemX.BankAccountNo);

            $("#InstrumentCompanyTypeID").val(dataItemX.InstrumentCompanyTypeID);
            $("#AnotherRating").val(dataItemX.AnotherRating);
            $("#BloombergSecID").val(dataItemX.BloombergSecID);
            $("#ShortName").val(dataItemX.ShortName);

            $("#BloombergISIN").val(dataItemX.BloombergISIN);

            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'MM/dd/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'MM/dd/yyyy HH:mm:ss'));

        }
        //LblReksadanaType

        function RequiredAttributes() {

            if (_GlobClientCode == "11") {

                $("#SAPCustID").attr("required", true);
                $("#LblSAP").show();

            }
            else {
                $("#SAPCustID").attr("required", false);
                $("#LblSAP").hide();
            }
        }

        $("#Affiliated").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeAffiliated,
            value: setCmbAffiliated()
        });
        function OnChangeAffiliated() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbAffiliated() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.Affiliated;
            }
        }

        $("#BitIsShortSell").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitIsShortSell,
            value: setCmbBitIsShortSell()
        });
        function OnChangeBitIsShortSell() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitIsShortSell() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitIsShortSell;
            }
        }

        $("#BitIsMargin").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitIsMargin,
            value: setCmbBitIsMargin()
        });
        function OnChangeBitIsMargin() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitIsMargin() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitIsMargin;
            }
        }

        $("#BitIsScriptless").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitIsScriptless,
            value: setCmbBitIsScriptless()
        });
        function OnChangeBitIsScriptless() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitIsScriptless() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitIsScriptless;
            }
        }

        $("#BitIsSuspend").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitIsSuspend,
            value: setCmbBitIsSuspend()
        });
        function OnChangeBitIsSuspend() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitIsSuspend() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitIsSuspend;
            }
        }

        $("#BitIsForeign").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitIsForeign,
            value: setCmbBitIsForeign()
        });
        function OnChangeBitIsForeign() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitIsForeign() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitIsForeign;
            }
        }




        //InstrumentTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/InstrumentType/GetInstrumentTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentTypePK").kendoComboBox({
                    dataValueField: "InstrumentTypePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInstrumentTypePK,
                    value: setCmbInstrumentTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInstrumentTypePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if (this.value() == 6) {
                $("#LblCounterpart").show();
                $("#LblBankAccountNo").show();
            } else {
                $("#LblCounterpart").hide();
                $("#LblBankAccountNo").hide();
            }
            GlobInstrumentType = this.value();
            //BOND
            ShowHideLabelByInstrumentType(GlobInstrumentType);

        }
        function setCmbInstrumentTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentTypePK;
                }
            }
        }
        if (e != null) {
            ShowHideLabelByInstrumentType(dataItemX.InstrumentTypePK);
        }


        //ReksadanaTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/ReksadanaType/GetReksadanaTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ReksadanaTypePK").kendoComboBox({
                    dataValueField: "ReksadanaTypePK",
                    dataTextField: "Description",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeReksadanaTypePK,
                    value: setCmbReksadanaTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeReksadanaTypePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbReksadanaTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ReksadanaTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.ReksadanaTypePK;
                }
            }
        }

        //DepositoTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/DepositoType/GetDepositoTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepositoTypePK").kendoComboBox({
                    dataValueField: "DepositoTypePK",
                    dataTextField: "DepType",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDepositoTypePK,
                    value: setCmbDepositoTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeDepositoTypePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDepositoTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DepositoTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.DepositoTypePK;
                }
            }
        }

        //BankPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Bank/GetBankCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankPK").kendoComboBox({
                    dataValueField: "BankPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeBankPK,
                    value: setCmbBankPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeBankPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBankPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankPK;
                }
            }
        }



        //SectorPK
        $.ajax({
            url: window.location.origin + "/Radsoft/SubSector/GetSubSectorCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SectorPK").kendoComboBox({
                    dataValueField: "SubSectorPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeSectorPK,
                    value: setCmbSectorPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeSectorPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSectorPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SectorPK == 0) {
                    return "";
                } else {
                    return dataItemX.SectorPK;
                }
            }
        }


        //MarketPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Market/GetMarketCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MarketPK").kendoComboBox({
                    dataValueField: "MarketPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    index: 0,
                    change: OnChangeMarketPK,
                    value: setCmbMarketPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeMarketPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbMarketPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MarketPK == 0) {
                    return "";
                } else {
                    return dataItemX.MarketPK;
                }
            }
        }


        //InstrumentCompanyTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/InstrumentCompanyType/GetInstrumentCompanyTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentCompanyTypePK").kendoComboBox({
                    dataValueField: "InstrumentCompanyTypePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInstrumentCompanyTypePK,
                    value: setCmbInstrumentCompanyTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInstrumentCompanyTypePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInstrumentCompanyTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentCompanyTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentCompanyTypePK;
                }
            }
        }




        //CurrencyPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCurrencyPK,
                    value: setCmbCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.CurrencyPK;
                }
            }
        }



        //Interest Payment Type

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestPaymentType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestPaymentType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInterestPaymentType,
                    value: setCmbInterestPaymentType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInterestPaymentType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInterestPaymentType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestPaymentType == 0) {
                    return "";
                } else {
                    return dataItemX.InterestPaymentType;
                }
            }
        }

        //Interest Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInterestType,
                    value: setCmbInterestType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInterestType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInterestType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestType == 0) {
                    return "";
                } else {
                    return dataItemX.InterestType;
                }
            }
        }

        //Bond Rating
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BondRating",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BondRating").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeBondRating,
                    value: setCmbBondRating()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeBondRating() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBondRating() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BondRating == 0) {
                    return "";
                } else {
                    return dataItemX.BondRating;
                }
            }
        }

        //CounterpartPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CounterpartPK").kendoComboBox({
                    dataValueField: "CounterpartPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCounterpartPK,
                    value: setCmbCounterpartPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeCounterpartPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbCounterpartPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CounterpartPK == 0) {
                    return "";
                } else {
                    return dataItemX.CounterpartPK;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestDaysType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InterestDaysType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInterestDaysType,
                    value: setCmbInterestDaysType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInterestDaysType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInterestDaysType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestDaysType == 0) {
                    return "";
                } else {
                    return dataItemX.InterestDaysType;
                }
            }
        }

        // Numeric
        $("#InterestPercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setInterestPercent()

        });
        function setInterestPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.InterestPercent;
            }
        }

        $("#RegulatorHaircut").kendoNumericTextBox({
            format: "n0",
            value: setRegulatorHaircut()

        });
        function setRegulatorHaircut() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.RegulatorHaircut;
            }
        }

        $("#Liquidity").kendoNumericTextBox({
            format: "n0",
            value: setLiquidity()

        });
        function setLiquidity() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Liquidity;
            }
        }

        $("#NAWCHaircut").kendoNumericTextBox({
            format: "n0",
            value: setNAWCHaircut()

        });
        function setNAWCHaircut() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.NAWCHaircut;
            }
        }

        $("#CompanyHaircut").kendoNumericTextBox({
            format: "n0",
            value: setCompanyHaircut()

        });
        function setCompanyHaircut() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CompanyHaircut;
            }
        }

        $("#TaxExpensePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setTaxExpensePercent()

        });
        function setTaxExpensePercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TaxExpensePercent;
            }
        }

        $("#Category").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Deposito On Call", value: "Deposito On Call" },
                { text: "Deposit Normal", value: "Deposit Normal" }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeCategory,
            value: setCmbCategory()
        });
        function OnChangeCategory() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbCategory() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Category;
            }
        }


        //SAPCustID
        $.ajax({
            url: window.location.origin + "/Radsoft/SAPMaster/GetDataCustomerCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#SAPCustID").kendoComboBox({
                    dataValueField: "ID",
                    dataTextField: "Name",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeSAPCustID,
                    value: setCmbSAPCustID()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeSAPCustID() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbSAPCustID() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SAPCustID == 0) {
                    return "";
                } else {
                    return dataItemX.SAPCustID;
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
        $("#InstrumentPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Category").val("");
        $("#Affiliated").prop('checked', false);
        $("#InstrumentTypePK").val("");
        $("#InstrumentTypeID").val("");
        $("#ReksadanaTypePK").val("");
        $("#ReksadanaTypeID").val("");
        $("#DepositoTypePK").val("");
        $("#DepositoTypeID").val("");
        $("#ISIN").val("");
        $("#BankPK").val("");
        $("#BankID").val("");
        $("#IssuerPK").val("");
        $("#IssuerID").val("");
        $("#SectorPK").val("");
        $("#SectorID").val("");
        $("#HoldingPK").val("");
        $("#HoldingID").val("");
        $("#MarketPK").val("");
        $("#MarketID").val("");
        $("#IssueDate").val("");
        $("#MaturityDate").val("");
        $("#InterestPercent").val("");
        $("#InterestPaymentType").val("");
        $("#InterestType").val("");
        $("#LotInShare").val("");
        $("#BitIsSuspend").prop('checked', false);
        $("#CurrencyPK").val("");
        $("#CurrencyID").val("");
        $("#RegulatorHaircut").val("");
        $("#Liquidity").val("");
        $("#NAWCHaircut").val("");
        $("#CompanyHaircut").val("");
        $("#BondRating").val("");
        $("#BitIsShortSell").prop('checked', false);
        $("#BitIsMargin").prop('checked', false);
        $("#BitIsScriptless").prop('checked', false);
        $("#TaxExpensePercent").val("");
        $("#InterestDaysType").val("");
        $("#BloombergCode").val("");
        $("#FirstCouponDate").val("");
        $("#BitIsForeign").val("");
        $("#CounterpartPK").val("");
        $("#CounterpartID").val("");
        $("#BankAccountNo").val("");
        $("#SAPCustID").val("");

        $("#InstrumentCompanyTypePK").val("");
        $("#InstrumentCompanyTypeID").val("");
        $("#AnotherRating").val("");
        $("#BloombergSecID").val("");
        $("#ShortName").val("");

        $("#BloombergISIN").val("");

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
                            InstrumentPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ID: { type: "string" },
                            Name: { type: "string" },
                            Category: { type: "string" },
                            Affiliated: { type: "boolean" },
                            InstrumentTypePK: { type: "number" },
                            InstrumentTypeID: { type: "string" },
                            ReksadanaTypePK: { type: "number" },
                            ReksadanaTypeID: { type: "string" },
                            DepositoTypePK: { type: "number" },
                            DepositoTypeID: { type: "string" },
                            ISIN: { type: "string" },
                            SAPCustID: { type: "string" },
                            BankPK: { type: "number" },
                            BankID: { type: "string" },
                            BankName: { type: "string" },
                            IssuerPK: { type: "number" },
                            IssuerID: { type: "string" },
                            IssuerName: { type: "string" },
                            SectorPK: { type: "number" },
                            SectorID: { type: "string" },
                            SectorName: { type: "string" },
                            HoldingPK: { type: "number" },
                            HoldingID: { type: "string" },
                            HoldingName: { type: "string" },
                            MarketPK: { type: "number" },
                            MarketID: { type: "string" },
                            IssueDate: { type: "date" },
                            MaturityDate: { type: "date" },
                            InterestPercent: { type: "number" },
                            InterestPaymentType: { type: "number" },
                            InterestPaymentTypeDesc: { type: "string" },
                            InterestType: { type: "number" },
                            InterestTypeDesc: { type: "string" },
                            LotInShare: { type: "number" },
                            BitIsSuspend: { type: "boolean" },
                            CurrencyPK: { type: "number" },
                            CurrencyID: { type: "string" },
                            RegulatorHaircut: { type: "number" },
                            Liquidity: { type: "number" },
                            NAWCHaircut: { type: "number" },
                            CompanyHaircut: { type: "number" },
                            BondRating: { type: "string" },
                            BondRatingDesc: { type: "string" },
                            BitIsShortSell: { type: "boolean" },
                            BitIsMargin: { type: "boolean" },
                            BitIsScriptless: { type: "boolean" },
                            TaxExpensePercent: { type: "number" },
                            InterestDaysType: { type: "number" },
                            InterestDaysTypeDesc: { type: "string" },
                            BloombergCode: { type: "string" },
                            BitIsForeign: { type: "boolean" },
                            CounterpartPK: { type: "number" },
                            CounterpartID: { type: "string" },
                            CounterpartName: { type: "string" },
                            BankAccountNo: { type: "string" },
                            FirstCouponDate: { type: "date" },

                            InstrumentCompanyTypePK: { type: "number" },
                            InstrumentCompanyTypeID: { type: "string" },
                            InstrumentCompanyTypeName: { type: "string" },

                            AnotherRating: { type: "string" },
                            BloombergSecID: { type: "string" },
                            ShortName: { type: "string" },

                            BloombergISIN: { type: "string" },

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

                        }
                    }
                }
            });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridInstrumentApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridInstrumentPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridInstrumentHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var InstrumentApprovedURL = window.location.origin + "/Radsoft/Instrument/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(InstrumentApprovedURL);

        $("#gridInstrumentApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Instrument"
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
                { field: "InstrumentPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 400 },
                { field: "Category", title: "Category", width: 200, hidden: true },
                { field: "Affiliated", title: "Affiliated", width: 200, template: "#= Affiliated ? 'Yes' : 'No' #" },
                { field: "InstrumentTypePK", title: "InstrumentTypePK", width: 200, hidden: true },
                { field: "InstrumentTypeID", title: "Instrument Type", width: 200 },
                { field: "ReksadanaTypePK", title: "ReksadanaTypePK", width: 200, hidden: true },
                { field: "ReksadanaTypeID", title: "Reksadana Type", width: 200 },
                { field: "DepositoTypePK", title: "DepositoTypePK", width: 200, hidden: true },
                { field: "DepositoTypeID", title: "Deposito Type", width: 200 },
                { field: "ISIN", title: "ISIN", width: 200 },
                { field: "SAPCustID", title: "SAPCust", width: 200 },
                { field: "BankPK", title: "BankPK", width: 200, hidden: true },
                { field: "BankID", title: "BankID", width: 200 },
                { field: "BankName", title: "Bank Name", width: 300 },
                { field: "IssuerPK", title: "IssuerPK", width: 200, hidden: true },
                { field: "IssuerID", title: "Issuer ID", width: 200 },
                { field: "IssuerName", title: "Issuer Name", width: 300 },
                { field: "SectorPK", title: "SectorPK", width: 200, hidden: true },
                { field: "SectorID", title: "Sector ID", width: 200 },
                { field: "SectorName", title: "Sector Name", width: 200 },
                { field: "HoldingPK", title: "HoldingPK", width: 200, hidden: true },
                { field: "HoldingID", title: "Holding ID", width: 200 },
                { field: "HoldingName", title: "Holding Name", width: 200 },
                { field: "MarketPK", title: "MarketPK", width: 200, hidden: true },
                { field: "MarketID", title: "Market ID", width: 200 },
                { field: "IssueDate", title: "Issue Date", width: 110, template: "#= (IssueDate == null) ? ' ' : kendo.toString(kendo.parseDate(IssueDate), 'dd/MMM/yyyy')#" },
                { field: "MaturityDate", title: "Maturity Date", width: 110, template: "#= (MaturityDate == null) ? ' ' : kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                { field: "FirstCouponDate", title: "First Coupon Date", width: 110, template: "#= (FirstCouponDate == null) ? ' ' : kendo.toString(kendo.parseDate(FirstCouponDate), 'dd/MMM/yyyy')#" },
                { field: "InterestPercent", title: "Interest Percent", width: 200, template: "#: InterestPercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                { field: "InterestPaymentType", title: "InterestPaymentType", width: 200, hidden: true },
                { field: "InterestPaymentTypeDesc", title: "Interest Payment Type", width: 200 },
                { field: "InterestType", title: "InterestType", width: 200, hidden: true },
                { field: "InterestTypeDesc", title: "Interest Type", width: 200 },
                { field: "LotInShare", title: "LotInShare", width: 200, hidden: true },
                { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                { field: "CurrencyPK", title: "CurrencyPK", width: 200, hidden: true },
                { field: "CurrencyID", title: "Currency ID", width: 200 },
                { field: "RegulatorHaircut", title: "Regulator Haircut", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Liquidity", title: "Liquidity", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "NAWCHaircut", title: "NAWC Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CompanyHaircut", title: "Company Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "BondRating", title: "BondRating", width: 200, hidden: true },
                { field: "BondRatingDesc", title: "Bond Rating", width: 200 },
                { field: "BitIsShortSell", title: "Short Sell", width: 200, template: "#= BitIsShortSell ? 'Yes' : 'No' #" },
                { field: "BitIsMargin", title: "Margin", width: 200, template: "#= BitIsMargin ? 'Yes' : 'No' #" },
                { field: "BitIsScriptless", title: "Scriptless", width: 200, template: "#= BitIsScriptless ? 'Yes' : 'No' #" },
                { field: "TaxExpensePercent", title: "Tax Expense (%)", width: 200, template: "#: TaxExpensePercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                { field: "InterestDaysType", title: "InterestDaysType", width: 200, hidden: true },
                { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 200 },
                { field: "BloombergCode", title: "Bloomberg Code", width: 200 },
                { field: "BloombergISIN", title: "Bloomberg ISNI", width: 200 },
                { field: "BitIsForeign", title: "Foreign", width: 200, template: "#= BitIsForeign ? 'Yes' : 'No' #" },
                { field: "CounterpartPK", title: "CounterpartPK", width: 200, hidden: true },
                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                { field: "BankAccountNo", title: "Bank Account No", width: 200 },

                { field: "InstrumentCompanyTypePK", title: "InstrumentCompanyTypePK", width: 200, hidden: true },
                { field: "InstrumentCompanyTypeID", title: "InstrumentCompanyTypeID", width: 200 },

                { field: "AnotherRating", title: "Another Rating", width: 200 },
                { field: "BloombergSecID", title: "Bloomberg Security ID", width: 200 },
                { field: "ShortName", title: "Short Name", width: 200 },

                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabInstrument").kendoTabStrip({
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
                        var InstrumentPendingURL = window.location.origin + "/Radsoft/Instrument/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(InstrumentPendingURL);
                        $("#gridInstrumentPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Instrument"
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
                                { field: "InstrumentPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "Category", title: "Category", width: 200, hidden: true },
                                { field: "Affiliated", title: "Affiliated", width: 200, template: "#= Affiliated ? 'Yes' : 'No' #" },
                                { field: "InstrumentTypePK", title: "InstrumentTypePK", width: 200, hidden: true },
                                { field: "InstrumentTypeID", title: "Instrument Type", width: 200 },
                                { field: "ReksadanaTypePK", title: "ReksadanaTypePK", width: 200, hidden: true },
                                { field: "ReksadanaTypeID", title: "Reksadana Type", width: 200 },
                                { field: "DepositoTypePK", title: "DepositoTypePK", width: 200, hidden: true },
                                { field: "DepositoTypeID", title: "Deposito Type", width: 200 },
                                { field: "ISIN", title: "ISIN", width: 200 },
                                { field: "SAPCustID", title: "SAPCust", width: 200 },
                                { field: "BankPK", title: "BankPK", width: 200, hidden: true },
                                { field: "BankID", title: "BankID", width: 200 },
                                { field: "BankName", title: "Bank Name", width: 300 },
                                { field: "IssuerPK", title: "IssuerPK", width: 200, hidden: true },
                                { field: "IssuerID", title: "Issuer ID", width: 200 },
                                { field: "IssuerName", title: "Issuer Name", width: 300 },
                                { field: "SectorPK", title: "SectorPK", width: 200, hidden: true },
                                { field: "SectorID", title: "Sector ID", width: 200 },
                                { field: "SectorName", title: "Sector Name", width: 200 },
                                { field: "HoldingPK", title: "HoldingPK", width: 200, hidden: true },
                                { field: "HoldingID", title: "Holding ID", width: 200 },
                                { field: "HoldingName", title: "Holding Name", width: 200 },
                                { field: "MarketPK", title: "MarketPK", width: 200, hidden: true },
                                { field: "MarketID", title: "Market ID", width: 200 },
                                { field: "IssueDate", title: "Issue Date", width: 110, template: "#= (IssueDate == null) ? ' ' : kendo.toString(kendo.parseDate(IssueDate), 'dd/MMM/yyyy')#" },
                                { field: "MaturityDate", title: "Maturity Date", width: 110, template: "#= (MaturityDate == null) ? ' ' : kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                                { field: "InterestPercent", title: "Interest Percent", width: 200, template: "#: InterestPercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "FirstCouponDate", title: "First Coupon Date", width: 110, template: "#= (FirstCouponDate == null) ? ' ' : kendo.toString(kendo.parseDate(FirstCouponDate), 'dd/MMM/yyyy')#" },
                                { field: "InterestPaymentType", title: "InterestPaymentType", width: 200, hidden: true },
                                { field: "InterestPaymentTypeDesc", title: "Interest Payment Type", width: 200 },
                                { field: "InterestType", title: "InterestType", width: 200, hidden: true },
                                { field: "InterestTypeDesc", title: "Interest Type", width: 200 },
                                { field: "LotInShare", title: "LotInShare", width: 200, hidden: true },
                                { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                                { field: "CurrencyPK", title: "CurrencyPK", width: 200, hidden: true },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "RegulatorHaircut", title: "Regulator Haircut", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "Liquidity", title: "Liquidity", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "NAWCHaircut", title: "NAWC Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "CompanyHaircut", title: "Company Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "BondRating", title: "BondRating", width: 200, hidden: true },
                                { field: "BondRatingDesc", title: "Bond Rating", width: 200 },
                                { field: "BitIsShortSell", title: "Short Sell", width: 200, template: "#= BitIsShortSell ? 'Yes' : 'No' #" },
                                { field: "BitIsMargin", title: "Margin", width: 200, template: "#= BitIsMargin ? 'Yes' : 'No' #" },
                                { field: "BitIsScriptless", title: "Scriptless", width: 200, template: "#= BitIsScriptless ? 'Yes' : 'No' #" },
                                { field: "TaxExpensePercent", title: "Tax Expense (%)", width: 200, template: "#: TaxExpensePercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "InterestDaysType", title: "InterestDaysType", width: 200, hidden: true },
                                { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 200 },
                                { field: "BloombergCode", title: "Bloomberg Code", width: 200 },
                                { field: "BloombergISIN", title: "Bloomberg ISNI", width: 200 },
                                { field: "BitIsForeign", title: "Foreign", width: 200, template: "#= BitIsForeign ? 'Yes' : 'No' #" },
                                { field: "CounterpartPK", title: "CounterpartPK", width: 200, hidden: true },
                                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                                { field: "BankAccountNo", title: "Bank Account No", width: 200 },

                                { field: "InstrumentCompanyTypePK", title: "InstrumentCompanyTypePK", width: 200, hidden: true },
                                { field: "InstrumentCompanyTypeID", title: "InstrumentCompanyTypeID", width: 200 },

                                { field: "AnotherRating", title: "Another Rating", width: 200 },
                                { field: "BloombergSecID", title: "Bloomberg Security ID", width: 200 },
                                { field: "ShortName", title: "Short Name", width: 200 },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var InstrumentHistoryURL = window.location.origin + "/Radsoft/Instrument/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(InstrumentHistoryURL);

                        $("#gridInstrumentHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Instrument"
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
                                { field: "InstrumentPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "Category", title: "Category", width: 200, hidden: true },
                                { field: "Affiliated", title: "Affiliated", width: 200, template: "#= Affiliated ? 'Yes' : 'No' #" },
                                { field: "InstrumentTypePK", title: "InstrumentTypePK", width: 200, hidden: true },
                                { field: "InstrumentTypeID", title: "Instrument Type", width: 200 },
                                { field: "ReksadanaTypePK", title: "ReksadanaTypePK", width: 200, hidden: true },
                                { field: "ReksadanaTypeID", title: "Reksadana Type", width: 200 },
                                { field: "DepositoTypePK", title: "DepositoTypePK", width: 200, hidden: true },
                                { field: "DepositoTypeID", title: "Deposito Type", width: 200 },
                                { field: "ISIN", title: "ISIN", width: 200 },
                                { field: "SAPCustID", title: "SAPCust", width: 200 },
                                { field: "BankPK", title: "BankPK", width: 200, hidden: true },
                                { field: "BankID", title: "BankID", width: 200 },
                                { field: "BankName", title: "Bank Name", width: 300 },
                                { field: "IssuerPK", title: "IssuerPK", width: 200, hidden: true },
                                { field: "IssuerID", title: "Issuer ID", width: 200 },
                                { field: "IssuerName", title: "Issuer Name", width: 300 },
                                { field: "SectorPK", title: "SectorPK", width: 200, hidden: true },
                                { field: "SectorID", title: "Sector ID", width: 200 },
                                { field: "SectorName", title: "Sector Name", width: 200 },
                                { field: "HoldingPK", title: "HoldingPK", width: 200, hidden: true },
                                { field: "HoldingID", title: "Holding ID", width: 200 },
                                { field: "HoldingName", title: "Holding Name", width: 200 },
                                { field: "MarketPK", title: "MarketPK", width: 200, hidden: true },
                                { field: "MarketID", title: "Market ID", width: 200 },
                                { field: "IssueDate", title: "Issue Date", width: 110, template: "#= (IssueDate == null) ? ' ' : kendo.toString(kendo.parseDate(IssueDate), 'dd/MMM/yyyy')#" },
                                { field: "MaturityDate", title: "Maturity Date", width: 110, template: "#= (MaturityDate == null) ? ' ' : kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                                { field: "InterestPercent", title: "Interest Percent", width: 200, template: "#: InterestPercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "FirstCouponDate", title: "First Coupon Date", width: 110, template: "#= (FirstCouponDate == null) ? ' ' : kendo.toString(kendo.parseDate(FirstCouponDate), 'dd/MMM/yyyy')#" },
                                { field: "InterestPaymentType", title: "InterestPaymentType", width: 200, hidden: true },
                                { field: "InterestPaymentTypeDesc", title: "Interest Payment Type", width: 200 },
                                { field: "InterestType", title: "InterestType", width: 200, hidden: true },
                                { field: "InterestTypeDesc", title: "Interest Type", width: 200 },
                                { field: "LotInShare", title: "LotInShare", width: 200, hidden: true },
                                { field: "BitIsSuspend", title: "Suspend", width: 200, template: "#= BitIsSuspend ? 'Yes' : 'No' #" },
                                { field: "CurrencyPK", title: "CurrencyPK", width: 200, hidden: true },
                                { field: "CurrencyID", title: "Currency ID", width: 200 },
                                { field: "RegulatorHaircut", title: "Regulator Haircut", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "Liquidity", title: "Liquidity", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "NAWCHaircut", title: "NAWC Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "CompanyHaircut", title: "Company Haircut", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "BondRating", title: "BondRating", width: 200, hidden: true },
                                { field: "BondRatingDesc", title: "Bond Rating", width: 200 },
                                { field: "BitIsShortSell", title: "Short Sell", width: 200, template: "#= BitIsShortSell ? 'Yes' : 'No' #" },
                                { field: "BitIsMargin", title: "Margin", width: 200, template: "#= BitIsMargin ? 'Yes' : 'No' #" },
                                { field: "BitIsScriptless", title: "Scriptless", width: 200, template: "#= BitIsScriptless ? 'Yes' : 'No' #" },
                                { field: "TaxExpensePercent", title: "Tax Expense (%)", width: 200, template: "#: TaxExpensePercent  # %", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "InterestDaysType", title: "InterestDaysType", width: 200, hidden: true },
                                { field: "InterestDaysTypeDesc", title: "Interest Days Type", width: 200 },
                                { field: "BloombergCode", title: "Bloomberg Code", width: 200 },
                                { field: "BloombergISIN", title: "Bloomberg ISNI", width: 200 },
                                { field: "BitIsForeign", title: "Foreign", width: 200, template: "#= BitIsForeign ? 'Yes' : 'No' #" },
                                { field: "CounterpartPK", title: "CounterpartPK", width: 200, hidden: true },
                                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                                { field: "BankAccountNo", title: "Bank Account No", width: 200 },

                                { field: "InstrumentCompanyTypePK", title: "InstrumentCompanyTypePK", width: 200, hidden: true },
                                { field: "InstrumentCompanyTypeID", title: "InstrumentCompanyTypeID", width: 200 },

                                { field: "AnotherRating", title: "Another Rating", width: 200 },
                                { field: "BloombergSecID", title: "Bloomberg Security ID", width: 200 },
                                { field: "ShortName", title: "Short Name", width: 200 },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },


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
        var grid = $("#gridInstrumentHistory").data("kendoGrid");
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
            //alert($('#SectorPK').val());
            if ($('#InstrumentTypePK').val() == 2 || $('#InstrumentTypePK').val() == 3) {
                ChooseSector($('#SectorPK').val());
            }
            alertify.confirm("Are you sure want to Add data?", function (e) {

                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Instrument",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var Instrument = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Category: $('#Category').val(),
                                    Affiliated: $('#Affiliated').val(),
                                    InstrumentTypePK: $('#InstrumentTypePK').val(),
                                    ReksadanaTypePK: $('#ReksadanaTypePK').val(),
                                    DepositoTypePK: $('#DepositoTypePK').val(),
                                    ISIN: $('#ISIN').val(),
                                    BankPK: $('#BankPK').val(),
                                    IssuerPK: $('#IssuerPK').val(),
                                    SectorPK: $('#SectorPK').val(),
                                    HoldingPK: $('#HoldingPK').val(),
                                    MarketPK: $('#MarketPK').val(),
                                    IssueDate: $('#IssueDate').val(),
                                    MaturityDate: $('#MaturityDate').val(),
                                    InterestPercent: $('#InterestPercent').val(),
                                    InterestPaymentType: $('#InterestPaymentType').val(),
                                    InterestType: $('#InterestType').val(),
                                    LotInShare: $('#LotInShare').val(),
                                    BitIsSuspend: $('#BitIsSuspend').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    RegulatorHaircut: $('#RegulatorHaircut').val(),
                                    Liquidity: $('#Liquidity').val(),
                                    NAWCHaircut: $('#NAWCHaircut').val(),
                                    CompanyHaircut: $('#CompanyHaircut').val(),
                                    BondRating: $('#BondRating').val(),
                                    BitIsShortSell: $('#BitIsShortSell').val(),
                                    BitIsMargin: $('#BitIsMargin').val(),
                                    BitIsScriptless: $('#BitIsScriptless').val(),
                                    TaxExpensePercent: $('#TaxExpensePercent').val(),
                                    InterestDaysType: $('#InterestDaysType').val(),
                                    BloombergCode: $('#BloombergCode').val(),
                                    BitIsForeign: $('#BitIsForeign').val(),
                                    FirstCouponDate: $('#FirstCouponDate').val(),
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    SAPCustID: $('#SAPCustID').val(),

                                    InstrumentCompanyTypePK: $('#InstrumentCompanyTypePK').val(),

                                    AnotherRating: $('#AnotherRating').val(),
                                    BloombergSecID: $('#BloombergSecID').val(),
                                    ShortName: $('#ShortName').val(),

                                    BloombergISIN: $('#BloombergISIN').val(),

                                    EntryUsersID: sessionStorage.getItem("user"),


                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Instrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Instrument_I",
                                    type: 'POST',
                                    data: JSON.stringify(Instrument),
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
                                alertify.alert("Data ID Same Not Allow!");
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


    //function ChooseSector(_sectorpk)
    //{
    //    if ($('#InstrumentTypePK').val() == 2 || $('#InstrumentTypePK').val() == 3)
    //    {
    //        if ($('#SectorPK').val() == 0 || $('#SectorPK').val() == null || $('#SectorPK').val() == "") {
    //            alertify.alert("Please Choose Sector First!");
    //            $("#SectorPK").attr("required", true);
    //            //return 0;

    //        } else if ($('#FirstCouponDate').val() == 0 || $('#FirstCouponDate').val() == null || $('#FirstCouponDate').val() == "") {
    //            alertify.alert("Please Insert First Coupon Date First!");
    //            $("#FirstCouponDate").attr("required", true);
    //            //return 0;
    //        }
    //    }
        
    //    else
    //    {
            
    //        return 1;
    //    }

            

    //    }

    function ChooseSector(_sectorpk) {
        if ($('#InstrumentTypePK').val() == 3 || $('#InstrumentTypePK').val() == 9 || $('#InstrumentTypePK').val() == 12 || $('#InstrumentTypePK').val() == 14) {
            if ($('#SectorPK').val() == 0 || $('#SectorPK').val() == null || $('#SectorPK').val() == "") {
                alertify.alert("Please Choose Sector First!");
                $("#SectorPK").attr("required", true);
            }
        }

        if ($('#InstrumentTypePK').val() == 2 || $('#InstrumentTypePK').val() == 3 || $('#InstrumentTypePK').val() == 9 || $('#InstrumentTypePK').val() == 12 || $('#InstrumentTypePK').val() == 13 || $('#InstrumentTypePK').val() == 14 || $('#InstrumentTypePK').val() == 15) {
            if ($('#FirstCouponDate').val() == 0 || $('#FirstCouponDate').val() == null || $('#FirstCouponDate').val() == "") {
                alertify.alert("Please Insert First Coupon Date First!");
                $("#FirstCouponDate").attr("required", true);

            }
        }
    }

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            //alert($('#SectorPK').val());
            if ($('#InstrumentTypePK').val() == 2 || $('#InstrumentTypePK').val() == 3) {
                ChooseSector($('#SectorPK').val());
            }
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Instrument",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {

                                var Instrument = {
                                    InstrumentPK: $('#InstrumentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Category: $('#Category').val(),
                                    Affiliated: $('#Affiliated').val(),
                                    InstrumentTypePK: $('#InstrumentTypePK').val(),
                                    ReksadanaTypePK: $('#ReksadanaTypePK').val(),
                                    DepositoTypePK: $('#DepositoTypePK').val(),
                                    ISIN: $('#ISIN').val(),
                                    BankPK: $('#BankPK').val(),
                                    IssuerPK: $('#IssuerPK').val(),
                                    SectorPK: $('#SectorPK').val(),
                                    HoldingPK: $('#HoldingPK').val(),
                                    MarketPK: $('#MarketPK').val(),
                                    IssueDate: $('#IssueDate').val(),
                                    MaturityDate: $('#MaturityDate').val(),
                                    InterestPercent: $('#InterestPercent').val(),
                                    InterestPaymentType: $('#InterestPaymentType').val(),
                                    InterestType: $('#InterestType').val(),
                                    LotInShare: $('#LotInShare').val(),
                                    BitIsSuspend: $('#BitIsSuspend').val(),
                                    CurrencyPK: $('#CurrencyPK').val(),
                                    RegulatorHaircut: $('#RegulatorHaircut').val(),
                                    Liquidity: $('#Liquidity').val(),
                                    NAWCHaircut: $('#NAWCHaircut').val(),
                                    CompanyHaircut: $('#CompanyHaircut').val(),
                                    BondRating: $('#BondRating').val(),
                                    BitIsShortSell: $('#BitIsShortSell').val(),
                                    BitIsMargin: $('#BitIsMargin').val(),
                                    BitIsScriptless: $('#BitIsScriptless').val(),
                                    TaxExpensePercent: $('#TaxExpensePercent').val(),
                                    InterestDaysType: $('#InterestDaysType').val(),
                                    BloombergCode: $('#BloombergCode').val(),
                                    BitIsForeign: $('#BitIsForeign').val(),
                                    CounterpartPK: $('#CounterpartPK').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    FirstCouponDate: $('#FirstCouponDate').val(),
                                    SAPCustID: $('#SAPCustID').val(),
                                    InstrumentCompanyTypePK: $('#InstrumentCompanyTypePK').val(),
                                    AnotherRating: $('#AnotherRating').val(),
                                    BloombergSecID: $('#BloombergSecID').val(),
                                    ShortName: $('#ShortName').val(),
                                    BloombergISIN: $('#BloombergISIN').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Instrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Instrument_U",
                                    type: 'POST',
                                    data: JSON.stringify(Instrument),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Instrument",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                                    {
                                        read:
                                            {
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Instrument" + "/" + $("#InstrumentPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Instrument",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var Instrument = {
                                InstrumentPK: $('#InstrumentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Instrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Instrument_A",
                                type: 'POST',
                                data: JSON.stringify(Instrument),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Instrument",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var Instrument = {
                                InstrumentPK: $('#InstrumentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Instrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Instrument_V",
                                type: 'POST',
                                data: JSON.stringify(Instrument),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Instrument",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss')) {
                            var Instrument = {
                                InstrumentPK: $('#InstrumentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Instrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Instrument_R",
                                type: 'POST',
                                data: JSON.stringify(Instrument),
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


    function getDataSourceListIssuer() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: window.location.origin + "/Radsoft/Issuer/GetIssuerCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                             IssuerPK: { type: "Number" },
                             ID: { type: "string" },
                         }
                     }
                 }
             });
    }

    $("#btnListIssuer").click(function () {
        WinListIssuer.center();
        WinListIssuer.open();
        initListIssuer();
        htmlIssuerPK = "#IssuerPK";
        htmlIssuerID = "#IssuerID";
        htmlIssuerName = "#IssuerName";
    });

    function initListIssuer() {
        var dsListIssuer = getDataSourceListIssuer();
        $("#gridListIssuer").kendoGrid({
            dataSource: dsListIssuer,
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
               { command: { text: "Select", click: ListIssuerSelect }, title: " ", width: 60 },
               { field: "IssuerPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "ID", width: 200 },
               //{ field: "Name", title: "Name", width: 300 }
            ]
        });
    }

    function onWinListIssuerClose() {
        $("#gridListIssuer").empty();
    }

    function ListIssuerSelect(e) {
        var grid = $("#gridListIssuer").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlIssuerName).val(dataItemX.Name);
        $(htmlIssuerID).val(dataItemX.ID);
        $(htmlIssuerPK).val(dataItemX.IssuerPK);
        onChangeIssuerPK();
        WinListIssuer.close();

    }

    function onChangeIssuerPK() {
        $.ajax({
            url: window.location.origin + "/Radsoft/Issuer/GetHoldingByIssuerPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#IssuerPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#HoldingPK").val(data.HoldingPK);
                $("#HoldingID").val(data.HoldingID);
                $("#HoldingName").val(data.HoldingName);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }

    function onChangeIssuerName() {
        $("#IssuerPK").data("kendoComboBox").select($("#IssuerName").data("kendoComboBox").select());
        onChangeIssuerPK();
    }

    function ShowHideLabelByInstrumentType(_type) {
        ClearRequiredAttribute();
        HideLabel();
        //BOND
        $("#LotInShare").val(1);
        if (_type == 3 || _type == 9 || _type == 12 || _type == 14) {
            $("#LblIssuer").show();
            $("#LblIssueDate").show();
            $("#LblInterestDaysType").show();
            $("#LblInterestType").show();
            $("#LblInterestPaymentType").show();
            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblBondRating").show();
            $("#LblTaxExpensePercent").show();
            $("#LblSector").show();
            $("#LblFirstCouponDate").show();
            $("#LblIssuerDate").show();

            $("#BondRating").attr("required", true);
            $("#InterestDaysType").attr("required", true);
            $("#InterestType").attr("required", true);
            $("#InterestPaymentType").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#MaturityDate").attr("required", true);
            $("#TaxExpensePercent").attr("required", true);
            $("#CurrencyPK").attr("required", true);
            $("#FirstCouponDate").attr("required", true);
            $("#SectorPK").attr("required", true);
            $("#InstrumentCompanyTypePK").attr("");

        }

        else if (_type == 2) {
            $("#LblIssuer").show();
            $("#LblIssueDate").show();
            $("#LblInterestDaysType").show();
            $("#LblInterestType").show();
            $("#LblInterestPaymentType").show();
            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblTaxExpensePercent").show();
            $("#LblFirstCouponDate").show();
            $("#LblIssuerDate").show();





            $("#InterestDaysType").attr("required", true);
            $("#InterestType").attr("required", true);
            $("#InterestPaymentType").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#MaturityDate").attr("required", true);
            $("#TaxExpensePercent").attr("required", true);
            $("#CurrencyPK").attr("required", true);
            $("#FirstCouponDate").attr("required", true);
            $("#InstrumentCompanyTypePK").attr("");


            if (_GlobClientCode == '28') {
                $("#LblSector").hide();
                $("#SectorPK").attr("required", false);
            } else {
                $("#LblSector").show();
                $("#SectorPK").attr("required", true);
            }

        }
        else if (_type == 13 || _type == 15) {
            $("#LblIssuer").show();
            $("#LblIssueDate").show();
            $("#LblInterestDaysType").show();
            $("#LblInterestType").show();
            $("#LblInterestPaymentType").show();
            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblTaxExpensePercent").show();
            $("#LblFirstCouponDate").show();
            $("#LblIssuerDate").show();

            $("#InterestDaysType").attr("required", true);
            $("#InterestType").attr("required", true);
            $("#InterestPaymentType").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#MaturityDate").attr("required", true);
            $("#TaxExpensePercent").attr("required", true);
            $("#CurrencyPK").attr("required", true);
            $("#FirstCouponDate").attr("required", true);
            $("#InstrumentCompanyTypePK").attr("");

        }

        else if (_type == 5 || _type == 10) {
            $("#LblDepositoType").show();
            $("#LblBank").show();
            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblCategory").show();
            $("#LblTaxExpensePercent").show();
            $("#LblFirstCouponDate").hide();
            $("#LblIssuerDate").hide();
            $("#LblIssuer").show();

            $("#DepositoTypePK").attr("required", true);
            $("#BankPK").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#MaturityDate").attr("required", true);
            $("#Category").attr("required", true);
            $("#TaxExpensePercent").attr("required", true);
            $("#CurrencyPK").attr("required", true);
            $("#FirstCouponDate").attr("required", false);
            $("#SectorPK").attr("required", false);
            $("#InstrumentCompanyTypePK").attr("");

        }

        else if (_type == 1 || _type == 4 || _type == 16) {
            $("#LblIssuer").show();
            $("#LblSector").show();
            $("#LblHolding").show();
            $("#LblLotInShare").show();
            $("#LblIssuerDate").show();

            $("#IssuerPK").attr("required", false);
            $("#CurrencyPK").attr("required", true);
            $("#FirstCouponDate").attr("required", false);
            $("#SectorPK").attr("required", false);
            $("#LotInShare").val(100);
            $("#InstrumentCompanyTypePK").attr("");
        }
        else if (_type == 7 || _type == 8 || _type == 11) {
            $("#LblReksadanaType").hide();
            $("#LblRegulatorHaircut").show();
            $("#LblLiquidity").show();
            $("#LblNAWCHaircut").show();
            $("#LblIssuerDate").show();
            $("#LblSector").show();

            if (_GlobClientCode == '11') {
                $("#LblCounterpart").show();
                $("#LblBankAccountNo").show();
            } else {
                $("#LblCounterpart").hide();
                $("#LblBankAccountNo").hide();
            }

            $("#ReksadanaTypePK").attr("required", false);
            $("#CurrencyPK").attr("required", true);
            $("#FirstCouponDate").attr("required", false);
            $("#SectorPK").attr("required", true);
            $("#CounterpartPK").attr("required", false);
            $("#BankAccountNo").attr("required", false);
            $("#InstrumentCompanyTypePK").attr("");
        }
        else {
            $("#LblReksadanaType").show();
            $("#LblRegulatorHaircut").show();
            $("#LblLiquidity").show();
            $("#LblNAWCHaircut").show();
            $("#LblIssuerDate").show();

            if (_GlobClientCode == '11') {
                $("#LblCounterpart").show();
                $("#LblBankAccountNo").show();
            } else {
                $("#LblCounterpart").hide();
                $("#LblBankAccountNo").hide();
            }

            $("#ReksadanaTypePK").attr("required", true);
            $("#CurrencyPK").attr("required", true);
            $("#FirstCouponDate").attr("required", false);
            $("#SectorPK").attr("required", false);
            $("#CounterpartPK").attr("required", false);
            $("#BankAccountNo").attr("required", false);
            $("#InstrumentCompanyTypePK").attr("");
        }

    }

    function HideLabel() {
        $("#LblDepositoType").hide();
        $("#LblIssuerDate").hide();
        $("#LblIssuer").hide();
        $("#LblReksadanaType").hide();
        $("#LblBank").hide();
        $("#LblInterestDaysType").hide();
        $("#LblSector").hide();
        $("#LblRegulatorHaircut").hide();
        $("#LblInterestType").hide();
        $("#LblHolding").hide();
        $("#LblLiquidity").hide();
        $("#LblInterestPaymentType").hide();
        $("#LblLotInShare").hide();
        $("#LblNAWCHaircut").hide();
        $("#LblInterestPercent").hide();
        $("#LblCompanyHaircut").hide();
        $("#LblMaturityDate").hide();
        $("#LblCategory").hide();
        $("#LblBondRating").hide();
        $("#LblTaxExpensePercent").hide();
        $("#LblCounterpart").hide();
        $("#LblBankAccountNo").hide();
        $("#LblFirstCouponDate").hide();

        if (_GlobClientCode == '11') {
            $("#LblCounterpart").show();
            $("#LblBankAccountNo").show();
        } else {
            $("#LblCounterpart").hide();
            $("#LblBankAccountNo").hide();
        }
        if (_GlobClientCode == '22') {
            $("#LblInstrumentCompanyType").show();
        } else {
            $("#LblInstrumentCompanyType").hide();
        }
    }

    function ClearAttribute() {
        $("#DepositoTypePK").val("");
        $("#IssuerDate").val("");
        $("#IssuerPK").val("");
        $("#ReksadanaTypePK").val("");
        $("#BankPK").val("");
        $("#InterestDaysType").val("");
        $("#SectorPK").val("");
        $("#RegulatorHaircut").val("");
        $("#InterestType").val("");
        $("#HoldingPK").val("");
        $("#Liquidity").val("");
        $("#InterestPaymentType").val("");
        $("#LotInShare").val("");
        $("#NAWCHaircut").val("");
        $("#InterestPercent").val("");
        $("#CompanyHaircut").val("");
        $("#MaturityDate").val("");
        $("#Category").val("");
        $("#BondRating").val("");
        $("#TaxExpensePercent").val("");
        $("#CounterpartPK").val("");
        $("#BankAccountNo").val("");
        $("#InstrumentCompanyTypePK").val("");
    }

    function ClearRequiredAttribute() {
        $("#BondRating").removeAttr("required");
        $("#InterestDaysType").removeAttr("required");
        $("#InterestType").removeAttr("required");
        $("#InterestPaymentType").removeAttr("required");
        $("#InterestPercent").removeAttr("required");
        $("#MaturityDate").removeAttr("required");
        $("#CurrencyPK").removeAttr("required");
        $("#DepositoTypePK").removeAttr("required");
        $("#BankPK").removeAttr("required");
        $("#IssuerPK").removeAttr("required");
        $("#ReksadanaTypePK").removeAttr("required");
        $("#Category").removeAttr("required");
        $("#TaxExpensePercent").removeAttr("required");
        $("#CounterpartPK").removeAttr("required");
        $("#BankAccountNo").removeAttr("required");
        $("#InstrumentCompanyTypePK").removeAttr("");
    }
});
