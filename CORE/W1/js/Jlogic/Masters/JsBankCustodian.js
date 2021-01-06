$(document).ready(function () {
    document.title = 'FORM BANK CUSTODIAN';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListCity;
    var htmlCity;
    var htmlCityDesc;
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
    }

    
      

    function initWindow() {

        win = $("#WinBankCustodian").kendoWindow({
            height: 1000,
            title: "Bank Custodian Detail",
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

        WinListCity = $("#WinListCity").kendoWindow({
            height: "520px",
            title: "City List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCityClose
        }).data("kendoWindow");

    }



    var GlobValidator = $("#WinBankCustodian").kendoValidator().data("kendoValidator");

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

            $("#BankCustodianPK").val(dataItemX.BankCustodianPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#Address").val(dataItemX.Address);
            $("#City").val(dataItemX.City);
            $("#CityDesc").val(dataItemX.CityDesc);
            $("#Branch").val(dataItemX.Branch);
            $("#ContactPerson").val(dataItemX.ContactPerson);
            $("#Email1").val(dataItemX.Email1);
            $("#Email2").val(dataItemX.Email2);
            $("#Email3").val(dataItemX.Email3);
            $("#Attn1").val(dataItemX.Attn1);
            $("#Attn2").val(dataItemX.Attn2);
            $("#Attn3").val(dataItemX.Attn3);
            $("#ClearingCode").val(dataItemX.ClearingCode);
            $("#RTGSCode").val(dataItemX.RTGSCode);
            $("#BICode").val(dataItemX.BICode);
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

        $("#Phone1").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setPhone1()
        });
        function setPhone1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Phone1;
            }
        }
        $("#Phone2").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setPhone2()
        });
        function setPhone2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Phone2;
            }
        }
        $("#Phone3").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setPhone3()
        });
        function setPhone3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Phone3;
            }
        }

        $("#Fax1").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setFax1()
        });
        function setFax1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Fax1;
            }
        }
        $("#Fax2").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setFax2()
        });
        function setFax2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Fax2;
            }
        }
        $("#Fax3").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setFax3()
        });
        function setFax3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Fax3;
            }
        }

        $("#FeeLLG").kendoNumericTextBox({
            format: "n4",
            value: setFeeLLG()

        });
        function setFeeLLG() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FeeLLG;
            }
        }

        $("#FeeRTGS").kendoNumericTextBox({
            format: "n4",
            value: setFeeRTGS()

        });
        function setFeeRTGS() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FeeRTGS;
            }
        }

        $("#MinforRTGS").kendoNumericTextBox({
            format: "n4",
            value: setMinforRTGS()

        });
        function setMinforRTGS() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MinforRTGS;
            }
        }

        //Rounding Mode
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/RoundingMode",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#JournalRoundingMode").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeRoundingMode,
                    dataSource: data,
                    value: setCmbJournalRoundingMode()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeRoundingMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbJournalRoundingMode() {
            if (e == null) {
                return 3;
            } else {
                if (dataItemX.JournalRoundingMode == 0) {
                    return "";
                } else {
                    return dataItemX.JournalRoundingMode;
                }
            }
        }


        //Decimal Places
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DecimalPlaces",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#JournalDecimalPlaces").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    change: onChangeDecimalPlaces,
                    dataSource: data,
                    value: setCmbJournalDecimalPlaces()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeDecimalPlaces() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbJournalDecimalPlaces() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.JournalDecimalPlaces == 0) {
                    return 0;
                } else {
                    return dataItemX.JournalDecimalPlaces;
                }
            }
        }

        //Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/BankCustodianType",
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


        //Fund Account PK
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundAccountPK").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    change: onChangeFundAccountPK,
                    dataSource: data,
                    value: setDFundAccountPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundAccountPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setDFundAccountPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundAccountPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundAccountPK;
                }
            }
        }

        $("#BitRDN").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitRDN,
            value: setCmbBitRDN()
        });
        function OnChangeBitRDN() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitRDN() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitRDN;
            }
        }

        $("#BitSyariah").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitSyariah,
            value: setCmbBitSyariah()
        });
        function OnChangeBitSyariah() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbBitSyariah() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitSyariah;
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
        $("#BankCustodianPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Type").val("");
        $("#OfficeName").val("");
        $("#Address").val("");
        $("#Branch").val("");
        $("#ContactPerson").val("");
        $("#Fax1").val("");
        $("#Fax2").val("");
        $("#Fax3").val("");
        $("#Email1").val("");
        $("#Email2").val("");
        $("#Email3").val("");
        $("#Attn1").val("");
        $("#Attn2").val("");
        $("#Attn3").val("");
        $("#BICode").val("");
        $("#FundAccountPK").val("");
        $("#ClearingCode").val("");
        $("#RTGSCode").val("");
        $("#BitRDN").val("");
        $("#BitSyariah").val("");
        $("#City").val("");
        $("#CityDesc").val("");
        $("#Phone1").val("");
        $("#Phone2").val("");
        $("#Phone3").val("");
        $("#FeeLLG").val("");
        $("#FeeRTGS").val("");
        $("#MinforRTGS").val("");
        $("#JournalRoundingMode").val("");
        $("#JournalDecimalPlaces").val("");
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
                             BankCustodianPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Type: { type: "string" },
                             TypeDesc: { type: "string" },
                             Address: { type: "string" },
                             Branch: { type: "string" },
                             ContactPerson: { type: "String" },
                             Fax1: { type: "string" },
                             Fax2: { type: "string" },
                             Fax3: { type: "string" },
                             Email1: { type: "string" },
                             Email2: { type: "string" },
                             Email3: { type: "string" },
                             Attn1: { type: "string" },
                             Attn2: { type: "string" },
                             Attn3: { type: "string" },
                             BICode: { type: "string" },
                             FundAccountPK: { type: "number" },
                             FundAccountID: { type: "string" },
                             FundAccountName: { type: "string" },
                             ClearingCode: { type: "string" },
                             RTGSCode: { type: "string" },
                             BitRDN: { type: "boolean" },
                             BitSyariah: { type: "boolean" },
                             City: { type: "number" },
                             CityDesc: { type: "string" },
                             Phone1: { type: "string" },
                             Phone2: { type: "string" },
                             Phone3: { type: "string" },
                             FeeLLG: { type: "number" },
                             FeeRTGS: { type: "number" },
                             MinforRTGS: { type: "number" },
                             JournalRoundingMode: { type: "number" },
                             JournalRoundingModeDesc: { type: "string" },
                             JournalDecimalPlaces: { type: "number" },
                             JournalDecimalPlacesDesc: { type: "string" },
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
            var gridApproved = $("#gridBankCustodianApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridBankCustodianPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridBankCustodianHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var BankCustodianApprovedURL = window.location.origin + "/Radsoft/BankCustodian/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(BankCustodianApprovedURL);

        $("#gridBankCustodianApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form BankCustodian"
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
                { field: "BankCustodianPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 400 },
                { field: "TypeDesc", title: "Type", width: 200 },
                { field: "Address", title: "Address", width: 700 },
                { field: "CityDesc", title: "City", width: 200 },
                { field: "Branch", title: "Branch", width: 200 },
                { field: "ContactPerson", title: "Contact Person", width: 200 },
                { field: "Fax1", title: "Fax 1", width: 200 },
                { field: "Fax2", title: "Fax 2", width: 200 },
                { field: "Fax3", title: "Fax 3", width: 200 },
                { field: "Email1", title: "Email 1", width: 200 },
                { field: "Email2", title: "Email 2", width: 200 },
                { field: "Email3", title: "Email 3", width: 200 },
                { field: "Attn1", title: "Attn 1", width: 200 },
                { field: "Attn2", title: "Attn 2", width: 200 },
                { field: "Attn3", title: "Attn 3", width: 200 },
                { field: "Phone1", title: "Phone 1", width: 200 },
                { field: "Phone2", title: "Phone 2", width: 200 },
                { field: "Phone3", title: "Phone 3", width: 200 },
                { field: "BICode", title: "BI Code", width: 200 },
                { field: "FundAccountID", title: "Fund Account", width: 300 },
                { field: "ClearingCode", title: "Clearing Code", width: 200 },
                { field: "RTGSCode", title: "RTGS Code", width: 200 },
                { field: "BitRDN", title: "RDN", width: 200, template: "#= BitRDN ? 'Yes' : 'No' #" },
                { field: "BitSyariah", title: "Syariah", width: 200, template: "#= BitSyariah ? 'Yes' : 'No' #" },
                { field: "FeeLLG", title: "Fee LLG", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "FeeRTGS", title: "Fee RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MinforRTGS", title: "Min for RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "JournalRoundingModeDesc", title: "Journal Rounding Mode", width: 250 },
                { field: "JournalDecimalPlacesDesc", title: "Journal Decimal Places", width: 250 },
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
        $("#TabBankCustodian").kendoTabStrip({
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
                        var BankCustodianPendingURL = window.location.origin + "/Radsoft/BankCustodian/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(BankCustodianPendingURL);
                        $("#gridBankCustodianPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form BankCustodian"
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
                                { field: "BankCustodianPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "TypeDesc", title: "Type", width: 200 },
                                { field: "Address", title: "Address", width: 700 },
                                { field: "CityDesc", title: "City", width: 200 },
                                { field: "Branch", title: "Branch", width: 200 },
                                { field: "ContactPerson", title: "Contact Person", width: 200 },
                                { field: "Fax1", title: "Fax 1", width: 200 },
                                { field: "Fax2", title: "Fax 2", width: 200 },
                                { field: "Fax3", title: "Fax 3", width: 200 },
                                { field: "Email1", title: "Email 1", width: 200 },
                                { field: "Email2", title: "Email 2", width: 200 },
                                { field: "Email3", title: "Email 3", width: 200 },
                                { field: "Attn1", title: "Attn 1", width: 200 },
                                { field: "Attn2", title: "Attn 2", width: 200 },
                                { field: "Attn3", title: "Attn 3", width: 200 },
                                { field: "Phone1", title: "Phone 1", width: 200 },
                                { field: "Phone2", title: "Phone 2", width: 200 },
                                { field: "Phone3", title: "Phone 3", width: 200 },
                                { field: "BICode", title: "BI Code", width: 200 },
                                { field: "FundAccountID", title: "Fund Account", width: 300 },
                                { field: "ClearingCode", title: "Clearing Code", width: 200 },
                                { field: "RTGSCode", title: "RTGS Code", width: 200 },
                                { field: "BitRDN", title: "RDN", width: 200, template: "#= BitRDN ? 'Yes' : 'No' #" },
                                { field: "BitSyariah", title: "Syariah", width: 200, template: "#= BitSyariah ? 'Yes' : 'No' #" },
                                { field: "FeeLLG", title: "Fee LLG", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "FeeRTGS", title: "Fee RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MinforRTGS", title: "Min for RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "JournalRoundingModeDesc", title: "Journal Rounding Mode", width: 250 },
                                { field: "JournalDecimalPlacesDesc", title: "Journal Decimal Places", width: 250 },
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

                        var BankCustodianHistoryURL = window.location.origin + "/Radsoft/BankCustodian/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(BankCustodianHistoryURL);

                        $("#gridBankCustodianHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form BankCustodian"
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
                                { field: "BankCustodianPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 400 },
                                { field: "TypeDesc", title: "Type", width: 200 },
                                { field: "Address", title: "Address", width: 700 },
                                { field: "CityDesc", title: "City", width: 200 },
                                { field: "Branch", title: "Branch", width: 200 },
                                { field: "ContactPerson", title: "Contact Person", width: 200 },
                                { field: "Fax1", title: "Fax 1", width: 200 },
                                { field: "Fax2", title: "Fax 2", width: 200 },
                                { field: "Fax3", title: "Fax 3", width: 200 },
                                { field: "Email1", title: "Email 1", width: 200 },
                                { field: "Email2", title: "Email 2", width: 200 },
                                { field: "Email3", title: "Email 3", width: 200 },
                                { field: "Attn1", title: "Attn 1", width: 200 },
                                { field: "Attn2", title: "Attn 2", width: 200 },
                                { field: "Attn3", title: "Attn 3", width: 200 },
                                { field: "Phone1", title: "Phone 1", width: 200 },
                                { field: "Phone2", title: "Phone 2", width: 200 },
                                { field: "Phone3", title: "Phone 3", width: 200 },
                                { field: "BICode", title: "BI Code", width: 200 },
                                { field: "FundAccountID", title: "Fund Account", width: 300 },
                                { field: "ClearingCode", title: "Clearing Code", width: 200 },
                                { field: "RTGSCode", title: "RTGS Code", width: 200 },
                                { field: "BitRDN", title: "RDN", width: 200, template: "#= BitRDN ? 'Yes' : 'No' #" },
                                { field: "BitSyariah", title: "Syariah", width: 200, template: "#= BitSyariah ? 'Yes' : 'No' #" },
                                { field: "FeeLLG", title: "Fee LLG", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "FeeRTGS", title: "Fee RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MinforRTGS", title: "Min for RTGS", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "JournalRoundingModeDesc", title: "Journal Rounding Mode", width: 250 },
                                { field: "JournalDecimalPlacesDesc", title: "Journal Decimal Places", width: 250 },
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
        var grid = $("#gridBankCustodianHistory").data("kendoGrid");
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

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "BankCustodian",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var BankCustodian = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Address: $('#Address').val(),
                                    Branch: $('#Branch').val(),
                                    Type: $('#Type').val(),
                                    ContactPerson: $('#ContactPerson').val(),
                                    Fax1: $('#Fax1').val(),
                                    Fax2: $('#Fax2').val(),
                                    Fax3: $('#Fax3').val(),
                                    Email1: $('#Email1').val(),
                                    Email2: $('#Email2').val(),
                                    Email3: $('#Email3').val(),
                                    Attn1: $('#Attn1').val(),
                                    Attn2: $('#Attn2').val(),
                                    Attn3: $('#Attn3').val(),
                                    BICode: $('#BICode').val(),
                                    FundAccountPK: $('#FundAccountPK').val(),
                                    ClearingCode: $('#ClearingCode').val(),
                                    RTGSCode: $('#RTGSCode').val(),
                                    BitRDN: $('#BitRDN').val(),
                                    BitSyariah: $('#BitSyariah').val(),
                                    City: $('#City').val(),
                                    Phone1: $('#Phone1').val(),
                                    Phone2: $('#Phone2').val(),
                                    Phone3: $('#Phone3').val(),
                                    FeeLLG: $('#FeeLLG').val(),
                                    FeeRTGS: $('#FeeRTGS').val(),
                                    MinforRTGS: $('#MinforRTGS').val(),
                                    JournalRoundingMode: $('#JournalRoundingMode').val(),
                                    JournalDecimalPlaces: $('#JournalDecimalPlaces').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankCustodian/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankCustodian_I",
                                    type: 'POST',
                                    data: JSON.stringify(BankCustodian),
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

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankCustodianPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankCustodian",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var BankCustodian = {
                                    BankCustodianPK: $('#BankCustodianPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Address: $('#Address').val(),
                                    Branch: $('#Branch').val(),
                                    Type: $('#Type').val(),
                                    ContactPerson: $('#ContactPerson').val(),
                                    Fax1: $('#Fax1').val(),
                                    Fax2: $('#Fax2').val(),
                                    Fax3: $('#Fax3').val(),
                                    Email1: $('#Email1').val(),
                                    Email2: $('#Email2').val(),
                                    Email3: $('#Email3').val(),
                                    Attn1: $('#Attn1').val(),
                                    Attn2: $('#Attn2').val(),
                                    Attn3: $('#Attn3').val(),
                                    BICode: $('#BICode').val(),
                                    FundAccountPK: $('#FundAccountPK').val(),
                                    ClearingCode: $('#ClearingCode').val(),
                                    RTGSCode: $('#RTGSCode').val(),
                                    BitRDN: $('#BitRDN').val(),
                                    BitSyariah: $('#BitSyariah').val(),
                                    City: $('#City').val(),
                                    Phone1: $('#Phone1').val(),
                                    Phone2: $('#Phone2').val(),
                                    Phone3: $('#Phone3').val(),
                                    FeeLLG: $('#FeeLLG').val(),
                                    FeeRTGS: $('#FeeRTGS').val(),
                                    MinforRTGS: $('#MinforRTGS').val(),
                                    JournalRoundingMode: $('#JournalRoundingMode').val(),
                                    JournalDecimalPlaces: $('#JournalDecimalPlaces').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankCustodian/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankCustodian_U",
                                    type: 'POST',
                                    data: JSON.stringify(BankCustodian),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankCustodianPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankCustodian",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "BankCustodian" + "/" + $("#BankCustodianPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankCustodianPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankCustodian",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BankCustodian = {
                                BankCustodianPK: $('#BankCustodianPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BankCustodian/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankCustodian_A",
                                type: 'POST',
                                data: JSON.stringify(BankCustodian),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankCustodianPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankCustodian",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BankCustodian = {
                                BankCustodianPK: $('#BankCustodianPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BankCustodian/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankCustodian_V",
                                type: 'POST',
                                data: JSON.stringify(BankCustodian),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankCustodianPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankCustodian",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BankCustodian = {
                                BankCustodianPK: $('#BankCustodianPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BankCustodian/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankCustodian_R",
                                type: 'POST',
                                data: JSON.stringify(BankCustodian),
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


    function getDataSourceListCity() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CityRHB",
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

    $("#btnListCity").click(function () {
        WinListCity.center();
        WinListCity.open();
        initListCity();
        htmlCity = "#City";
        htmlCityDesc = "#CityDesc";
    });


    function initListCity() {
        var dsListCity = getDataSourceListCity();
        $("#gridListCity").kendoGrid({
            dataSource: dsListCity,
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
               { command: { text: "Select", click: ListCitySelect }, title: " ", width: 60 },
               { field: "DescOne", title: "City", width: 200 }
            ]
        });
    }

    function onWinListCityClose() {
        $("#gridListCity").empty();
    }

    function ListCitySelect(e) {
        var grid = $("#gridListCity").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCityDesc).val(dataItemX.DescOne);
        $(htmlCity).val(dataItemX.Code);
        WinListCity.close();

    }



});
