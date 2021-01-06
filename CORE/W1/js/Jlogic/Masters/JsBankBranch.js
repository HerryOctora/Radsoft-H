$(document).ready(function () {
    document.title = 'FORM BANK BRANCH';
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

        win = $("#WinBankBranch").kendoWindow({
            height: 800,
            title: "Bank Branch Detail",
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

    var GlobValidator = $("#WinBankBranch").kendoValidator().data("kendoValidator");

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

            $("#BankBranchPK").val(dataItemX.BankBranchPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Address").val(dataItemX.Address);
            $("#City").val(dataItemX.City);
            $("#CityDesc").val(dataItemX.CityDesc);
            $("#ContactPerson").val(dataItemX.ContactPerson);
            $("#Email1").val(dataItemX.Email1);
            $("#Email2").val(dataItemX.Email2);
            $("#Email3").val(dataItemX.Email3);
            $("#Attn1").val(dataItemX.Attn1);
            $("#Attn2").val(dataItemX.Attn2);
            $("#Attn3").val(dataItemX.Attn3);
            $("#BankAccountName").val(dataItemX.BankAccountName);
            $("#BankAccountNo ").val(dataItemX.BankAccountNo);
            $("#PTPCode").val(dataItemX.PTPCode);
            $("#BitIsEnabled").prop('checked', dataItemX.BitIsEnabled);
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
            //mask: "(999) 000-00000",
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
            //mask: "(999) 000-00000",
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
            //mask: "(999) 000-00000",
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
            //mask: "(999) 000-00000",
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
            //mask: "(999) 000-00000",
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
            //mask: "(999) 000-00000",
            value: setFax3()
        });
        function setFax3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Fax3;
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
            hideLabel(this.value())
        }
        function setCmbType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Type == 0) {
                    return "";
                }
                else {
                    hideLabel(dataItemX.Type)
                    return dataItemX.Type;
                }
            }

            
        }
        function hideLabel(_type) {
            if (_type == "2") {
                $("#lblBankNo").show();
                $("#lblBankName").show();
                $("#lblPTPCode").hide();
                $("#BankAccountName").attr("required", false);
                $("#BankAccountNo").attr("required", false);
                $("#PTPCode").attr("required", false);
                //$("#lblBankNo").hide();
                //$("#lblBankName").hide();
                //$("#lblPTPCode").hide();
                //$("#BankAccountName").attr("required", false);
                //$("#BankAccountNo").attr("required", false);
                //$("#PTPCode").attr("required", false);
            }
            else {
                $("#lblBankNo").show();
                $("#lblBankName").show();
                $("#lblPTPCode").show();
                $("#BankAccountName").attr("required", true);
                $("#BankAccountNo").attr("required", true);
                $("#PTPCode").attr("required", true);
            }
        }


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
                    change: onChangeBankPK,
                    dataSource: data,
                    value: setCmbBankPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeBankPK() {

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

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboBankBranch/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/InterestDaysType",
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
        $("#BankBranchPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#BankPK").val("");
        $("#Type").val("");
        $("#Address").val("");
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
        $("#City").val("");
        $("#CityDesc").val("");
        $("#Phone1").val("");
        $("#Phone2").val("");
        $("#Phone3").val("");
        $("#PTPCode").val("");
        $("#BankAccountName").val("");
        $("#BankAccountNo").val("");
        $("#BitIsEnabled").prop('checked', false);
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
                             BankBranchPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Type: { type: "string" },
                             TypeDesc: { type: "string" },
                             Address: { type: "string" },
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
                             City: { type: "number" },
                             CityDesc: { type: "string" },
                             Phone1: { type: "string" },
                             Phone2: { type: "string" },
                             Phone3: { type: "string" },
                             PTPCode: { type: "string" },
                             JournalRoundingMode: { type: "number" },
                             JournalRoundingModeDesc: { type: "string" },
                             JournalDecimalPlaces: { type: "number" },
                             JournalDecimalPlacesDesc: { type: "string" },
                             BankAccountName: { type: "string" },
                             BankAccountNo: { type: "string" },
                             BitIsEnabled: { type: "boolean" },
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
            var gridApproved = $("#gridBankBranchApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridBankBranchPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridBankBranchHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var BankBranchApprovedURL = window.location.origin + "/Radsoft/BankBranch/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(BankBranchApprovedURL);
        if (_GlobClientCode == "05") {
            $("#gridBankBranchApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form BankBranch"
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
                    { field: "BankBranchPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "BankCode", title: "Bank Code", width: 150 },
                    { field: "BankName", title: "Bank Name", width: 250 },
                    { field: "PTPCode", title: "Branch Code", width: 150 },
                    { field: "ID", title: "Branch Name", width: 250 },
                    { field: "BankAccountName", title: "Bank Account Name", width: 200 },
                    { field: "BankAccountNo", title: "Bank Account No", width: 180 },
                    { field: "ContactPerson", title: "Contact Person", width: 200 },
                    { field: "Phone", title: "Phone", width: 200 },
                    { field: "Fax1", title: "Fax", width: 200 },
                    { field: "Email1", title: "Email", width: 250 },
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
            $("#gridBankBranchApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form BankBranch"
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
                    { field: "BankBranchPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ID", title: "ID", width: 200 },
                    { field: "TypeDesc", title: "Type", width: 200 },
                    { field: "Address", title: "Address", width: 700 },
                    { field: "CityDesc", title: "City", width: 200 },
                    { field: "ContactPerson", title: "Contact Person", width: 200 },
                    { field: "BankAccountName", title: "Bank Account Name", width: 200 },
                    { field: "BankAccountNo", title: "Bank Account No", width: 200 },
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
                    { field: "PTPCode", title: "PTP Code", width: 200 },
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
        $("#TabBankBranch").kendoTabStrip({
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
                        var BankBranchPendingURL = window.location.origin + "/Radsoft/BankBranch/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(BankBranchPendingURL);
                        if (_GlobClientCode == "05") {
                            $("#gridBankBranchPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form BankBranch"
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
                                    { field: "BankBranchPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "BankCode", title: "Bank Code", width: 150 },
                                    { field: "BankName", title: "Bank Name", width: 250 },
                                    { field: "PTPCode", title: "Branch Code", width: 150 },
                                    { field: "ID", title: "Branch Name", width: 250 },
                                    { field: "BankAccountName", title: "Bank Account Name", width: 200 },
                                    { field: "BankAccountNo", title: "Bank Account No", width: 180 },
                                    { field: "ContactPerson", title: "Contact Person", width: 200 },
                                    { field: "Phone", title: "Phone", width: 200 },
                                    { field: "Fax1", title: "Fax", width: 200 },
                                    { field: "Email1", title: "Email", width: 250 },
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
                            $("#gridBankBranchPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form BankBranch"
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
                                    { field: "BankBranchPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "ID", title: "ID", width: 200 },
                                    { field: "TypeDesc", title: "Type", width: 200 },
                                    { field: "Address", title: "Address", width: 700 },
                                    { field: "CityDesc", title: "City", width: 200 },
                                    { field: "ContactPerson", title: "Contact Person", width: 200 },
                                    { field: "ContactPerson", title: "Contact Person", width: 200 },
                                    { field: "BankAccountName", title: "Bank Account Name", width: 200 },
                                    { field: "BankAccountNo", title: "Bank Account No", width: 200 },
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
                                    { field: "PTPCode", title: "PTP Code", width: 200 },
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

                        var BankBranchHistoryURL = window.location.origin + "/Radsoft/BankBranch/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(BankBranchHistoryURL);

                        $("#gridBankBranchHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form BankBranch"
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
                                { field: "BankBranchPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "TypeDesc", title: "Type", width: 200 },
                                { field: "Address", title: "Address", width: 700 },
                                { field: "CityDesc", title: "City", width: 200 },
                                { field: "ContactPerson", title: "Contact Person", width: 200 },
                                { field: "ContactPerson", title: "Contact Person", width: 200 },
                                { field: "BankAccountName", title: "Bank Account Name", width: 200 },
                                { field: "BankAccountNo", title: "Bank Account No", width: 200 },
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
                                { field: "PTPCode", title: "PTP Code", width: 200 },
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
        var grid = $("#gridBankBranchHistory").data("kendoGrid");
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
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "BankBranch",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var BankBranch = {
                                    ID: $('#ID').val(),
                                    Address: $('#Address').val(),
                                    BankPK: $('#BankPK').val(),
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
                                    City: $('#City').val(),
                                    Phone1: $('#Phone1').val(),
                                    Phone2: $('#Phone2').val(),
                                    Phone3: $('#Phone3').val(),
                                    PTPCode: $('#PTPCode').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    BankAccountName: $('#BankAccountName').val(),
                                    InterestDaysType: $("#InterestDaysType").val(),
                                    BitIsEnabled: $('#BitIsEnabled').is(":checked"),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankBranch/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankBranch_I",
                                    type: 'POST',
                                    data: JSON.stringify(BankBranch),
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

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankBranchPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankBranch",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var BankBranch = {
                                    BankBranchPK: $('#BankBranchPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Address: $('#Address').val(),
                                    BankPK: $('#BankPK').val(),
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
                                    City: $('#City').val(),
                                    Phone1: $('#Phone1').val(),
                                    Phone2: $('#Phone2').val(),
                                    Phone3: $('#Phone3').val(),
                                    PTPCode: $('#PTPCode').val(),
                                    BankAccountNo: $('#BankAccountNo').val(),
                                    BankAccountName: $('#BankAccountName').val(),
                                    InterestDaysType: $("#InterestDaysType").val(),
                                    BitIsEnabled: $('#BitIsEnabled').is(":checked"),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/BankBranch/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankBranch_U",
                                    type: 'POST',
                                    data: JSON.stringify(BankBranch),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankBranchPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankBranch",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "BankBranch" + "/" + $("#BankBranchPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankBranchPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankBranch",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BankBranch = {
                                BankBranchPK: $('#BankBranchPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BankBranch/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankBranch_A",
                                type: 'POST',
                                data: JSON.stringify(BankBranch),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankBranchPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankBranch",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BankBranch = {
                                BankBranchPK: $('#BankBranchPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BankBranch/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankBranch_V",
                                type: 'POST',
                                data: JSON.stringify(BankBranch),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#BankBranchPK").val() + "/" + $("#HistoryPK").val() + "/" + "BankBranch",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var BankBranch = {
                                BankBranchPK: $('#BankBranchPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/BankBranch/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BankBranch_R",
                                type: 'POST',
                                data: JSON.stringify(BankBranch),
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
