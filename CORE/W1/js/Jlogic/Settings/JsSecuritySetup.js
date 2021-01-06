$(document).ready(function () {
    document.title = 'FORM SECURITY SETUP';
    
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
    }

    function initWindow() {
        win = $("#WinSecuritySetup").kendoWindow({
            height: 550,
            title: "Security Setup Detail",
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

    var GlobValidator = $("#WinSecuritySetup").kendoValidator().data("kendoValidator");

    function validateData() {
        if (GlobValidator.validate()) {
            return 1;
        } else {
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
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnReject").show();
                $("#BtnApproved").show();
                $("#BtnVoid").hide();
                $("#BtnOldData").show();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnReject").hide();
                $("#BtnApproved").hide();
                $("#BtnVoid").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnReject").hide();
                $("#BtnApproved").hide();
                $("#BtnVoid").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnReject").hide();
                $("#BtnApproved").hide();
                $("#BtnVoid").hide();
                $("#BtnOldData").hide();
            }

            $("#SecuritySetupPK").val(dataItemX.SecuritySetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#DefaultPassword").val(dataItemX.DefaultPassword);
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

        //combo box PasswordCharacterType
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/PasswordType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PasswordCharacterType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangePasswordCharacterType,
                    value: setCmbPasswordCharacterType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangePasswordCharacterType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPasswordCharacterType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PasswordCharacterType == 0) {
                    return "";
                } else {
                    return dataItemX.PasswordCharacterType;
                }
            }
        }

        $("#BitChangePasswordAtReset").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitChangePasswordAtReset,
            value: setCmbBitChangePasswordAtReset()
        });
        function OnChangeBitChangePasswordAtReset() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitChangePasswordAtReset() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitChangePasswordAtReset;
            }
        }

        $("#BitReusedPassword").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitReusedPassword,
            value: setCmbBitReusedPassword()
        });
        function OnChangeBitReusedPassword() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitReusedPassword() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.BitReusedPassword;
            }
        }

        $("#MaxLoginRetry").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setMaxLoginRetry()

        });
        function setMaxLoginRetry() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MaxLoginRetry;
            }
        }

        $("#MinimumPasswordLength").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setMinimumPasswordLength()

        });
        function setMinimumPasswordLength() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MinimumPasswordLength;
            }
        }

        $("#PasswordExpireDays").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setPasswordExpireDays()

        });
        function setPasswordExpireDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PasswordExpireDays;
            }
        }

        $("#UsersExpireDays").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setUsersExpireDays()

        });
        function setUsersExpireDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.UsersExpireDays;
            }
        }

        $("#HoursChangePassword").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setHoursChangePassword()

        });
        function setHoursChangePassword() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.HoursChangePassword;
            }
        }

        $("#PasswordExpireLevel").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setPasswordExpireLevel()

        });
        function setPasswordExpireLevel() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PasswordExpireLevel;
            }
        }

        $("#IdleTimeMinutes").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setIdleTimeMinutes()

        });
        function setIdleTimeMinutes() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.IdleTimeMinutes;
            }
        }

        $("#ExpireSessionTime").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setExpireSessionTime()

        });
        function setExpireSessionTime() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.ExpireSessionTime;
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
        $("#SecuritySetupPK").val("");
        $("#HistoryPK").val("");
        $("#Status").val("");
        $("#Notes").val("");
        $("#MaxLoginRetry").val("");
        $("#PasswordCharacterType").val("");
        $("#MinimumPasswordLength").val("");
        $("#BitChangePasswordAtReset").val("");
        $("#PasswordExpireDays").val("");
        $("#UsersExpireDays").val("");
        $("#BitReusedPassword").val("");
        $("#HoursChangePassword").val("");
        $("#PasswordExpireLevel").val("");
        $("#DefaultPassword").val("");
        $("#IdleTimeMinutes").val(""); 
        $("#ExpireSessionTime").val("");
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
                             SecuritySetupPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             MaxLoginRetry: { type: "number" },
                             PasswordCharacterType: { type: "number" },
                             PasswordCharacterTypeDesc: { type: "string" },
                             MinimumPasswordLength: { type: "number" },
                             BitChangePasswordAtReset: { type: "boolean" },
                             PasswordExpireDays: { type: "number" },
                             UsersExpireDays: { type: "number" },
                             BitReusedPassword: { type: "boolean" },
                             HoursChangePassword: { type: "number" },
                             PasswordExpireLevel: { type: "number" },
                             DefaultPassword: { type: "string" },
                             IdleTimeMinutes: { type: "number" },
                             ExpireSessionTime: { type: "number" },
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
            var gridApproved = $("#gridSecuritySetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridSecuritySetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridSecuritySetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var SecuritySetupApprovedURL = window.location.origin + "/Radsoft/SecuritySetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(SecuritySetupApprovedURL);

        $("#gridSecuritySetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Security Setup"
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
                { field: "SecuritySetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "MaxLoginRetry", title: "Max Login Retry", width: 200, attributes: { style: "text-align:right;" } },
                { field: "PasswordCharacterTypeDesc", title: "Password Character Type", width: 250 },
                { field: "MinimumPasswordLength", title: "Minimum Password Length", width: 250, attributes: { style: "text-align:right;" } },
                { field: "BitChangePasswordAtReset", title: "Change Password At Reset", width: 250, template: "#= BitChangePasswordAtReset ? 'Yes' : 'No' #" },
                { field: "PasswordExpireDays", title: "Password Expire Days", width: 250, attributes: { style: "text-align:right;" } },
                { field: "UsersExpireDays", title: "Users Expire Days", width: 200, attributes: { style: "text-align:right;" } },
                { field: "BitReusedPassword", title: "Reused Password", width: 200, template: "#= BitReusedPassword ? 'Yes' : 'No' #" },
                { field: "HoursChangePassword", title: "Hours Change Password", width: 250, attributes: { style: "text-align:right;" } },
                { field: "PasswordExpireLevel", title: "Password Expire Level", width: 250, attributes: { style: "text-align:right;" } },
                { field: "DefaultPassword", title: "Default Password", width: 200 },
                { field: "IdleTimeMinutes", title: "Idle Time Minutes", width: 200, attributes: { style: "text-align:right;" } },
                { field: "ExpireSessionTime", title: "Expire Session Time", width: 200, attributes: { style: "text-align:right;" } },
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
        $("#TabSecuritySetup").kendoTabStrip({
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
                        var SecuritySetupPendingURL = window.location.origin + "/Radsoft/SecuritySetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(SecuritySetupPendingURL);
                        $("#gridSecuritySetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Security Setup"
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
                                { field: "SecuritySetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "MaxLoginRetry", title: "Max Login Retry", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "PasswordCharacterTypeDesc", title: "Password Character Type", width: 250 },
                                { field: "MinimumPasswordLength", title: "Minimum Password Length", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "BitChangePasswordAtReset", title: "Change Password At Reset", width: 250, template: "#= BitChangePasswordAtReset ? 'Yes' : 'No' #" },
                                { field: "PasswordExpireDays", title: "Password Expire Days", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "UsersExpireDays", title: "Users Expire Days", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "BitReusedPassword", title: "Reused Password", width: 200, template: "#= BitReusedPassword ? 'Yes' : 'No' #" },
                                { field: "HoursChangePassword", title: "Hours Change Password", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "PasswordExpireLevel", title: "Password Expire Level", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "DefaultPassword", title: "Default Password", width: 200 },
                                { field: "IdleTimeMinutes", title: "Idle Time Minutes", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "ExpireSessionTime", title: "Expire Session Time", width: 200, attributes: { style: "text-align:right;" } },
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

                        var SecuritySetupHistoryURL = window.location.origin + "/Radsoft/SecuritySetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(SecuritySetupHistoryURL);

                        $("#gridSecuritySetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Security Setup"
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
                                { field: "SecuritySetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "MaxLoginRetry", title: "Max Login Retry", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "PasswordCharacterTypeDesc", title: "Password Character Type", width: 250 },
                                { field: "MinimumPasswordLength", title: "Minimum Password Length", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "BitChangePasswordAtReset", title: "Change Password At Reset", width: 250, template: "#= BitChangePasswordAtReset ? 'Yes' : 'No' #" },
                                { field: "PasswordExpireDays", title: "Password Expire Days", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "UsersExpireDays", title: "Users Expire Days", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "BitReusedPassword", title: "Reused Password", width: 200, template: "#= BitReusedPassword ? 'Yes' : 'No' #" },
                                { field: "HoursChangePassword", title: "Hours Change Password", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "PasswordExpireLevel", title: "Password Expire Level", width: 250, attributes: { style: "text-align:right;" } },
                                { field: "DefaultPassword", title: "Default Password", width: 200 },
                                { field: "IdleTimeMinutes", title: "Idle Time Minutes", width: 200, attributes: { style: "text-align:right;" } },
                                { field: "ExpireSessionTime", title: "Expire Session Time", width: 200, attributes: { style: "text-align:right;" } },
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
        var grid = $("#gridSecuritySetupHistory").data("kendoGrid");
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
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasApproved/" + sessionStorage.getItem("user") + "/" + "SecuritySetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                var SecuritySetup = {
                                    MaxLoginRetry: $('#MaxLoginRetry').val(),
                                    PasswordCharacterType: $('#PasswordCharacterType').val(),
                                    MinimumPasswordLength: $('#MinimumPasswordLength').val(),
                                    BitChangePasswordAtReset: $('#BitChangePasswordAtReset').val(),
                                    PasswordExpireDays: $('#PasswordExpireDays').val(),
                                    UsersExpireDays: $('#UsersExpireDays').val(),
                                    BitReusedPassword: $('#BitReusedPassword').val(),
                                    HoursChangePassword: $('#HoursChangePassword').val(),
                                    PasswordExpireLevel: $('#PasswordExpireLevel').val(),
                                    DefaultPassword: $('#DefaultPassword').val(),
                                    IdleTimeMinutes: $('#IdleTimeMinutes').val(),
                                    ExpireSessionTime: $('#ExpireSessionTime').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/SecuritySetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SecuritySetup_I",
                                    type: 'POST',
                                    data: JSON.stringify(SecuritySetup),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        win.close();
                                        refresh();
                                    },
                                });
                            } else {
                                alertify.alert("Data has exist, no more addition ");
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SecuritySetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SecuritySetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var SecuritySetup = {
                                    SecuritySetupPK: $('#SecuritySetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    MaxLoginRetry: $('#MaxLoginRetry').val(),
                                    PasswordCharacterType: $('#PasswordCharacterType').val(),
                                    MinimumPasswordLength: $('#MinimumPasswordLength').val(),
                                    BitChangePasswordAtReset: $('#BitChangePasswordAtReset').val(),
                                    PasswordExpireDays: $('#PasswordExpireDays').val(),
                                    UsersExpireDays: $('#UsersExpireDays').val(),
                                    BitReusedPassword: $('#BitReusedPassword').val(),
                                    HoursChangePassword: $('#HoursChangePassword').val(),
                                    PasswordExpireLevel: $('#PasswordExpireLevel').val(),
                                    DefaultPassword: $('#DefaultPassword').val(),
                                    IdleTimeMinutes: $('#IdleTimeMinutes').val(),
                                    ExpireSessionTime: $('#ExpireSessionTime').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/SecuritySetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SecuritySetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(SecuritySetup),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SecuritySetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SecuritySetup",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "SecuritySetup" + "/" + $("#SecuritySetupPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SecuritySetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SecuritySetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasApproved/" + sessionStorage.getItem("user") + "/" + "SecuritySetup",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == true) {
                                        var SecuritySetup = {
                                            SecuritySetupPK: $('#SecuritySetupPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/SecuritySetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SecuritySetup_A",
                                            type: 'POST',
                                            data: JSON.stringify(SecuritySetup),
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
                                        alertify.alert("Data has exist, no more addition!");
                                        win.close();
                                        refresh();
                                    }
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SecuritySetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SecuritySetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var SecuritySetup = {
                                SecuritySetupPK: $('#SecuritySetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/SecuritySetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SecuritySetup_V",
                                type: 'POST',
                                data: JSON.stringify(SecuritySetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#SecuritySetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "SecuritySetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var SecuritySetup = {
                                SecuritySetupPK: $('#SecuritySetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/SecuritySetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SecuritySetup_R",
                                type: 'POST',
                                data: JSON.stringify(SecuritySetup),
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
