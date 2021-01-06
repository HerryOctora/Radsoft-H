$(document).ready(function () {
    document.title = 'FORM USERS';
    
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

        $("#BtnAccessTrail").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnManage.png"
        });

        $("#BtnResetUser").kendoButton({

        });

        $("#BtnEnableUser").kendoButton({

        });

        $("#BtnDisableUser").kendoButton({

        });

        $("#BtnCancelUserManage").kendoButton({

        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }

    function initWindow() {
        $("#ExpireUsersDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: null
        });
        $("#ExpirePasswordDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: null
        });
        $("#ExpireSessionTime").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: null
        });

        win = $("#WinUsers").kendoWindow({
            height: 720,
            title: "Users Detail",
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

        WinUsersAcessTrail = $("#WinUsersAcessTrail").kendoWindow({
            height: 450,
            title: "* Setup and Access Trail",
            visible: false,
            width: 750,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");
    }

    var GlobValidator = $("#WinUsers").kendoValidator().data("kendoValidator");

    function validateData() {
        
        if ($("#ExpireUsersDate").val() != "" || $("#ExpirePasswordDate").val() != "") {
            var _expireUsersDate = Date.parse($("#ExpireUsersDate").data("kendoDatePicker").value());
            var _expirePasswordDate = Date.parse($("#ExpirePasswordDate").data("kendoDatePicker").value());
          
            //Check if Date parse is successful
            if (!_expireUsersDate || !_expirePasswordDate) {
                
                alertify.alert("Wrong Format Date");
                return 0;
            }
        }

        if (GlobValidator.validate()) {

            if ($("#ID").val().length > 40) {
                alertify.alert("Validation not Pass, char more than 40 for ID");
                return 0;
            }

            if ($("#Name").val().length > 125) {
                alertify.alert("Validation not Pass, char more than 125 for Name");
                return 0;
            }

            if ($("#Password").val().length > 500) {
                alertify.alert("Validation not Pass, char more than 500 for Password");
                return 0;
            }

            if ($("#Notes").val().length > 1000) {
                alertify.alert("Validation not Pass, char more than 1000 for Notes");
                return 0;
            }

                        
            if ($("#JobTitle").val().length > 50) {
                alertify.alert("Validation not Pass, char more than 50 for JobTitle");
                return 0;
            }

            if ($("#Email").val().length > 50) {
                alertify.alert("Validation not Pass, char more than 50 for Email");
                return 0;
            }

            if ($("#PrevPassword1").val().length > 500) {
                alertify.alert("Validation not Pass, char more than 500 for PrevPassword1");
                return 0;
            }

            if ($("#PrevPassword2").val().length > 500) {
                alertify.alert("Validation not Pass, char more than 500 for PrevPassword2");
                return 0;
            }

            if ($("#PrevPassword3").val().length > 500) {
                alertify.alert("Validation not Pass, char more than 500 for PrevPassword3");
                return 0;
            }
            if ($("#PrevPassword4").val().length > 500) {
                alertify.alert("Validation not Pass, char more than 500 for PrevPassword5");
                return 0;
            }

            if ($("#PrevPassword5").val().length > 500) {
                alertify.alert("Validation not Pass, char more than 500 for PrevPassword5");
                return 0;
            }

            return 1;
        }

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

            $("#UsersPK").val(dataItemX.UsersPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#Email").val(dataItemX.Email);
            $("#UserClientMode").val(dataItemX.UserClientMode);
            $("#Password").val(dataItemX.Password);
            $("#JobTitle").val(dataItemX.JobTitle);
            $("#OfficePK").val(dataItemX.OfficePK);
            $("#OfficeID").val(dataItemX.OfficeID);
            $("#ExpireUsersDate").data("kendoDatePicker").value(dataItemX.ExpireUsersDate);
            $("#ExpirePasswordDate").data("kendoDatePicker").value(dataItemX.ExpirePasswordDate);
            $("#ExpireSessionTime").val(kendo.toString(kendo.parseDate(dataItemX.ExpireSessionTime), 'dd/MMM/yyyy'));
            $("#LoginRetry").val(dataItemX.LoginRetry);
            $("#PrevPassword1").val(dataItemX.PrevPassword1);
            $("#PrevPassword2").val(dataItemX.PrevPassword2);
            $("#PrevPassword3").val(dataItemX.PrevPassword3);
            $("#PrevPassword4").val(dataItemX.PrevPassword4);
            $("#PrevPassword5").val(dataItemX.PrevPassword5);
            $("#PrevPassword6").val(dataItemX.PrevPassword6);
            $("#PrevPassword7").val(dataItemX.PrevPassword7);
            $("#PrevPassword8").val(dataItemX.PrevPassword8);
            $("#PrevPassword9").val(dataItemX.PrevPassword9);
            $("#PrevPassword10").val(dataItemX.PrevPassword10);
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

        $("#UserClientMode").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "OPS", value: 1 },
                { text: "AGENT", value: 2 }
            ],
            filter: "contains",
            suggest: "",
            change: OnChangeUserClientMode,
            value: setCmbUserClientMode(),
            index: 1
        });
        function OnChangeUserClientMode() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbUserClientMode() {
            if (e == null) {
                return '';
            } else {
                return dataItemX.UserClientMode;
            }
        }


        //function OnChangeUserClientMode() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //    else {
        //        refresh();
        //    }

        //}
        //function setCmbUserClientMode() {
        //    if (e == null) {
        //        return '';
        //    } else {
        //        return dataItemX.UserClientMode;
        //    }
        //}

        $("#BitPasswordReset").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangePasswordReset,
            value: setCmbBitPasswordReset()
        });
        function OnChangePasswordReset() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitPasswordReset() {
            if (e == null) {
                return true;
            } else {
                return dataItemX.BitPasswordReset;
            }
        }

        $("#BitEnabled").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeBitEnabled,
            value: setCmbBitBitEnabled()
        });
        function OnChangeBitEnabled() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbBitBitEnabled() {
            if (e == null) {
                return true;
            } else {
                return dataItemX.BitEnabled;
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Office/GetOfficeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#OfficePK").kendoComboBox({
                    dataValueField: "OfficePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    value: setCmbOfficePK(),
                    change: OnChangeOfficePK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeOfficePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbOfficePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OfficePK == 0) {
                    return "";
                } else {
                    return dataItemX.OfficePK;
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
        $("#UsersPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Email").val("");
        $("#Password").val("");
        $("#JobTitle").val("");
        $("#OfficePK").val("");
        $("#OfficeID").val("");
        $("#BitEnabled").val("");
        $("#ExpireUsersDate").data("kendoDatePicker").value(null);
        $("#ExpirePasswordDate").data("kendoDatePicker").value(null);
        $("#ExpireSessionTime").data("kendoDatePicker").value(null);
        $("#LoginRetry").val("");
        $("#UserClientMode").val("");
        $("#PrevPassword1").val("");
        $("#PrevPassword2").val("");
        $("#PrevPassword3").val("");
        $("#PrevPassword4").val("");
        $("#PrevPassword5").val("");
        $("#PrevPassword6").val("");
        $("#PrevPassword7").val("");
        $("#PrevPassword8").val("");
        $("#PrevPassword9").val("");
        $("#PrevPassword10").val("");
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
                             UsersPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Email: { type: "string" },
                             Password: { type: "string" },
                             JobTitle: { type: "string" },
                             OfficePK: { type: "number" },
                             OfficeID: { type: "string" },
                             OfficeName: { type: "string" },
                             ExpireUsersDate: { type: "date" },
                             ExpirePasswordDate: { type: "date" },
                             ExpireSession: { type: "date" },
                             LoginRetry: { type: "number" },
                             UserClientMode: { type: "number" },
                             BitPasswordReset: { type: "boolean" },
                             BitEnabled: { type: "boolean" },
                             PrevPassword1: { type: "string" },
                             PrevPassword2: { type: "string" },
                             PrevPassword3: { type: "string" },
                             PrevPassword4: { type: "string" },
                             PrevPassword5: { type: "string" },
                             PrevPassword6: { type: "string" },
                             PrevPassword7: { type: "string" },
                             PrevPassword8: { type: "string" },
                             PrevPassword9: { type: "string" },
                             PrevPassword10: { type: "string" },
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
            var gridApproved = $("#gridUsersApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridUsersPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridUsersHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var userApprovedURL = window.location.origin + "/Radsoft/users/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(userApprovedURL);

        $("#gridUsersApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Users"
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
                { field: "UsersPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "Email", title: "Email", width: 200 },
                { field: "JobTitle", title: "JobTitle", width: 200 },
                { field: "OfficePK", title: "OfficePK", hidden: true, width: 120 },
                { field: "OfficeID", title: "Office", width: 200 },
                { field: "OfficeName", title: "Name", width: 300 },
                { field: "BitEnabled", title: "BitEnabled", width: 150, template: "#= BitEnabled ? 'Yes' : 'No' #" },
                { field: "UserClientModeDesc", title: "UserClientMode", width: 150},
                { field: "LoginRetry", title: "LoginRetry", width: 150 },
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
        $("#TabUsers").kendoTabStrip({
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
                        var userPendingURL = window.location.origin + "/Radsoft/users/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(userPendingURL);
                        $("#gridUsersPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Users"
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
                                { field: "UsersPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Email", title: "Email", width: 200 },
                                { field: "JobTitle", title: "JobTitle", width: 200 },
                                { field: "OfficePK", title: "OfficePK", hidden: true, width: 120 },
                                { field: "OfficeID", title: "Office", width: 200 },
                                { field: "OfficeName", title: "Name", width: 300 },
                                { field: "BitEnabled", title: "BitEnabled", width: 150, template: "#= BitEnabled ? 'Yes' : 'No' #" },
                                { field: "UserClientModeDesc", title: "UserClientMode", width: 150},
                                { field: "LoginRetry", title: "LoginRetry", width: 150 },
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

                        var userHistoryURL = window.location.origin + "/Radsoft/users/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(userHistoryURL);

                        $("#gridUsersHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Users"
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
                                { field: "UsersPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Email", title: "Email", width: 200 },
                                { field: "JobTitle", title: "JobTitle", width: 200 },
                                { field: "OfficePK", title: "OfficePK", hidden: true, width: 120 },
                                { field: "OfficeID", title: "Office", width: 200 },
                                { field: "OfficeName", title: "Name", width: 300 },
                                { field: "BitEnabled", title: "BitEnabled", width: 150, template: "#= BitEnabled ? 'Yes' : 'No' #" },
                                { field: "UserClientModeDesc", title: "UserClientMode", width: 150},
                                { field: "LoginRetry", title: "LoginRetry", width: 150 },
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
        var grid = $("#gridUsersHistory").data("kendoGrid");
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
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Users",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Users/CheckExistingEmail/" + sessionStorage.getItem("user") + "/" + $("#Email").val() + "/0",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == true) {


                                            var users = {
                                                ID: $('#ID').val(),
                                                Name: $('#Name').val(),
                                                Email: $("#Email").val(),
                                                Password: $("#Password").val(),
                                                JobTitle: $("#JobTitle").val(),
                                                OfficePK: $("#OfficePK").val(),
                                                UserClientMode: $("#UserClientMode").val(),
                                                ExpireUsersDate: $("#ExpireUsersDate").val(),
                                                ExpirePasswordDate: $("#ExpirePasswordDate").val(),
                                                LoginRetry: $("#LoginRetry").val(),
                                                BitPasswordReset: $("#BitPasswordReset").val(),
                                                BitEnabled: $("#BitEnabled").val(),
                                                EntryUsersID: sessionStorage.getItem("user")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/users/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Users_I",
                                                type: 'POST',
                                                data: JSON.stringify(users),
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
                                            alertify.alert("Email Same Not Allow!");
                                            win.close();
                                            refresh();
                                        }
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UsersPK").val() + "/" + $("#HistoryPK").val() + "/" + "Users",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Users/CheckExistingEmail/" + sessionStorage.getItem("user") + "/" + $("#Email").val() + "/" + $("#UsersPK").val(),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == true) {

                                            var users = {
                                                UsersPK: $('#UsersPK').val(),
                                                HistoryPK: $('#HistoryPK').val(),
                                                ID: $('#ID').val(),
                                                Name: $('#Name').val(),
                                                Email: $("#Email").val(),
                                                Password: $("#Password").val(),
                                                Notes: str,
                                                JobTitle: $("#JobTitle").val(),
                                                OfficePK: $("#OfficePK").val(),
                                                UserClientMode: $("#UserClientMode").val(),
                                                ExpireUsersDate: $("#ExpireUsersDate").val(),
                                                ExpirePasswordDate: $("#ExpirePasswordDate").val(),
                                                LoginRetry: $("#LoginRetry").val(),
                                                BitPasswordReset: $("#BitPasswordReset").val(),
                                                BitEnabled: $("#BitEnabled").val(),
                                                EntryUsersID: sessionStorage.getItem("user")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/users/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Users_U",
                                                type: 'POST',
                                                data: JSON.stringify(users),
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
                                            alertify.alert("Add User Failed, Email Already Exist!!!");
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
        }
    });

    $("#BtnOldData").click(function () {        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UsersPK").val() + "/" + $("#HistoryPK").val() + "/" + "Users",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Users" + "/" + $("#UsersPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UsersPK").val() + "/" + $("#HistoryPK").val() + "/" + "Users",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var users = {
                                UsersPK: $('#UsersPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/users/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Users_A",
                                type: 'POST',
                                data: JSON.stringify(users),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UsersPK").val() + "/" + $("#HistoryPK").val() + "/" + "Users",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var users = {
                                UsersPK: $('#UsersPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/users/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Users_V",
                                type: 'POST',
                                data: JSON.stringify(users),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#UsersPK").val() + "/" + $("#HistoryPK").val() + "/" + "Users",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var users = {
                                UsersPK: $('#UsersPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/users/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Users_R",
                                type: 'POST',
                                data: JSON.stringify(users),
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

    $("#BtnAccessTrail").click(function () {
        $.ajax({
            url: window.location.origin + "/Radsoft/Users/GetUsersCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#UsersID").kendoComboBox({
                    dataValueField: "UsersPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeUsersPK,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        WinUsersAcessTrail.center();
        WinUsersAcessTrail.open();
    });

    function OnChangeUsersPK() {
        if ($("#UsersID").data("kendoComboBox").text() != "" || $("#UsersID").data("kendoComboBox").text() != null) {
            $.ajax({
                url: window.location.origin + "/Radsoft/UsersAccessTrail/GetUserAccessTrailByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#UsersID").data("kendoComboBox").text(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#LoginSuccessTimeLast").val(data.LoginSuccessTimeLast);
                    $("#LoginFailTimeLast").val(data.LoginFailTimeLast);
                    $("#LogoutTimeLast").val(data.LogoutTimeLast);
                    $("#ChangePasswordTimeLast").val(data.ChangePasswordTimeLast);
                    $("#LoginSuccessFreq").val(data.LoginSuccessFreq);
                    $("#LoginFailFreq").val(data.LoginFailFreq);
                    $("#LogoutFreq").val(data.LogoutFreq);
                    $("#ChangePasswordFreq").val(data.ChangePasswordFreq);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        } else {
            $("#LoginSuccessTimeLast").val("");
            $("#LoginFailTimeLast").val("");
            $("#LogoutTimeLast").val("");
            $("#ChangePasswordTimeLast").val("");
            $("#LoginSuccessFreq").val("");
            $("#LoginFailFreq").val("");
            $("#LogoutFreq").val("");
            $("#ChangePasswordFreq").val("");
        }
    }

    $("#BtnResetUser").click(function () {        
        if ($('#UsersID').val() == "" || $('#UsersID').val() == null) {
            alertify.alert("Please Choose User ID");
            return;
        }
        alertify.confirm("Are you sure want to Reset User?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Users/Users_Reset/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Users_Reset" + "/" + $("#UsersID").data("kendoComboBox").text(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnEnableUser").click(function () {        
        if ($('#UsersID').val() == "" || $('#UsersID').val() == null) {
            alertify.alert("Please Choose User ID");
            return;
        }
        alertify.confirm("Are you sure want to Enable User?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Users/Users_Enable/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Users_Enable" + "/" + $("#UsersID").data("kendoComboBox").text(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnDisableUser").click(function () {        
        if ($('#UsersID').val() == "" || $('#UsersID').val() == null) {
            alertify.alert("Please Choose User ID");
            return;
        }
        alertify.confirm("Are you sure want to Disable User?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Users/Users_Disable/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Users_Disable" + "/" + $("#UsersID").data("kendoComboBox").text(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnCancelUserManage").click(function () {        
        alertify.confirm("Are you sure want to Close?", function (e) {
            if (e) {
                WinUsersAcessTrail.close();
                alertify.alert("Canceled Users Manage");
            }
        });
    });

});
