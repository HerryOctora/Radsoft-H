$(document).ready(function () {
    document.title = 'FORM Company Position';
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
        $("#BtnCompanyPositionRestructure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnCompanyPositionScheme").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });

        $("#BtnAddCompanyPositionScheme").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

    }

    

    function initWindow() {

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
     

        win = $("#WinCompanyPosition").kendoWindow({
            height: 750,
            title: "CompanyPosition Detail",
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

        WinCompanyPositionScheme = $("#WinCompanyPositionScheme").kendoWindow({
            height: 1000,
            title: "* Company Position Scheme",
            visible: false,
            width: 1100,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinCompanyPositionSchemeClose
        }).data("kendoWindow");

        WinAddCompanyPositionScheme = $("#WinAddCompanyPositionScheme").kendoWindow({
            height: 400,
            title: "* ADD SCHEME",
            visible: false,
            width: 600,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 20 })
            },
            close: onPopUpAddCompanyPositionSchemeClose
        }).data("kendoWindow");
   

    }

    var GlobValidator = $("#WinCompanyPosition").kendoValidator().data("kendoValidator");



    function validateData() {
        

        if (GlobValidator.validate()) {
            //alert("Validation sucess");
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

            $("#CompanyPositionPK").val(dataItemX.CompanyPositionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#Depth").val(dataItemX.Depth);
            $("#Levels").val(dataItemX.Levels);
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


        $("#Groups").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeGroups,
            value: setCmbGroups()
        });
        function OnChangeGroups() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbGroups() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.Groups;
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/CompanyPosition/GetCompanyPositionComboGroupOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParentPK").kendoComboBox({
                    dataValueField: "CompanyPositionPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    value: setCmbParentPK(),
                    change: OnChangeParentPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeParentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbParentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ParentPK == 0) {
                    return "";
                } else {
                    return dataItemX.ParentPK;
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
        $("#CompanyPositionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Groups").val("");
        $("#Levels").val("");
        $("#ParentPK").val("");
        $("#ParentID").val("");
        $("#Depth").val("");
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
                             CompanyPositionPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Groups: { type: "boolean" },
                             Levels: { type: "number" },
                             ParentPK: { type: "number" },
                             ParentID: { type: "string" },
                             ParentName: { type: "string" },
                             Depth: { type: "number" },
                             ParentPK1: { type: "number" },
                             ParentPK2: { type: "number" },
                             ParentPK3: { type: "number" },
                             ParentPK4: { type: "number" },
                             ParentPK5: { type: "number" },
                             ParentPK6: { type: "number" },
                             ParentPK7: { type: "number" },
                             ParentPK8: { type: "number" },
                             ParentPK9: { type: "number" },
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
            var gridApproved = $("#gridCompanyPositionApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridCompanyPositionPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridCompanyPositionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var CompanyPositionApprovedURL = window.location.origin + "/Radsoft/CompanyPosition/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(CompanyPositionApprovedURL);

        $("#gridCompanyPositionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form CompanyPosition"
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
                { field: "CompanyPositionPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "ParentID", title: "Parent ID", width: 200 },
                { field: "ParentName", title: "Parent Name", width: 300 },
                { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
        $("#TabCompanyPosition").kendoTabStrip({
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
                        var CompanyPositionPendingURL = window.location.origin + "/Radsoft/CompanyPosition/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(CompanyPositionPendingURL);
                        $("#gridCompanyPositionPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CompanyPosition"
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
                                { field: "CompanyPositionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                                { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "ParentID", title: "Parent ID", width: 200 },
                                { field: "ParentName", title: "Parent Name", width: 300 },
                                { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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

                        var CompanyPositionHistoryURL = window.location.origin + "/Radsoft/CompanyPosition/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(CompanyPositionHistoryURL);

                        $("#gridCompanyPositionHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form CompanyPosition"
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
                                { field: "CompanyPositionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                                { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "ParentID", title: "Parent ID", width: 200 },
                                { field: "ParentName", title: "Parent Name", width: 300 },
                                { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
        var grid = $("#gridCompanyPositionHistory").data("kendoGrid");
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
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "CompanyPosition",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var CompanyPosition = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Groups: $("#Groups").val(),
                                    Levels: $('#Levels').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    Depth: $('#Depth').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CompanyPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyPosition_I",
                                    type: 'POST',
                                    data: JSON.stringify(CompanyPosition),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CompanyPosition",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var CompanyPosition = {
                                    CompanyPositionPK: $('#CompanyPositionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Groups: $("#Groups").val(),
                                    Levels: $('#Levels').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    Depth: $('#Depth').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CompanyPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyPosition_U",
                                    type: 'POST',
                                    data: JSON.stringify(CompanyPosition),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CompanyPosition",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CompanyPosition" + "/" + $("#CompanyPositionPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CompanyPosition",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            if ($("#ID").val() == null || $("#ID").val() == "") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CompanyPosition/CompanyPosition_GenerateNewCompanyPositionID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyPosition_GenerateNewCompanyPositionID",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if ($("#ID").val() == null || $("#ID").val() == "") {
                                            $("#ID").val(data);
                                            alertify.alert("Your New Client ID is " + data);
                                        }
                                        var CompanyPosition = {
                                            CompanyPositionPK: $('#CompanyPositionPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/CompanyPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyPosition_A",
                                            type: 'POST',
                                            data: JSON.stringify(CompanyPosition),
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
                            else {
                                var CompanyPosition = {
                                    CompanyPositionPK: $('#CompanyPositionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CompanyPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyPosition_A",
                                    type: 'POST',
                                    data: JSON.stringify(CompanyPosition),
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
                        else {
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CompanyPosition",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CompanyPosition = {
                                CompanyPositionPK: $('#CompanyPositionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CompanyPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyPosition_V",
                                type: 'POST',
                                data: JSON.stringify(CompanyPosition),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPositionPK").val() + "/" + $("#HistoryPK").val() + "/" + "CompanyPosition",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CompanyPosition = {
                                CompanyPositionPK: $('#CompanyPositionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CompanyPosition/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CompanyPosition_R",
                                type: 'POST',
                                data: JSON.stringify(CompanyPosition),
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

    $("#BtnCompanyPositionRestructure").click(function () {
        
        alertify.confirm("Are you sure want to Restructure CompanyPosition?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/CompanyPosition/CompanyPositionUpdateParentAndDepth/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
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

    // Company Position Scheme


    $("#BtnCompanyPositionScheme").click(function () {
        showCompanyPositionScheme();
    });

    function showCompanyPositionScheme(e) {

        //CategoryScheme
        $.ajax({
            url: window.location.origin + "/Radsoft/CategoryScheme/GetCategorySchemeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterCategoryScheme").kendoComboBox({
                    dataValueField: "CategorySchemePK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFilterCategoryScheme,
              
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeFilterCategoryScheme() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            InitGridDetailCompanyPositionScheme();
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/CompanyPosition/GetCompanyPositionComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterCompanyPosition").kendoComboBox({
                    dataValueField: "CompanyPositionPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFilterCompanyPosition,
                
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeFilterCompanyPosition() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            InitGridDetailCompanyPositionScheme();


        }
        InitGridDetailCompanyPositionScheme();


        WinCompanyPositionScheme.center();
        WinCompanyPositionScheme.open();

    }






  

    function getDataSourceDetailCompanyPositionScheme(_url) {
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
                 schema: {
                     model: {
                         fields: {
                             CategorySchemeID: { type: "string", editable: false },
                             CompanyPositionID: { type: "string", editable: false },
                             FeePercent: { type: "number", editable: true },

                         }
                     }
                 },

             });
    }



    function showDetailsCompanyPositionScheme(e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        grid = $("#gridInformationCompanyPositionScheme").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        InitGridDetailCompanyPositionScheme(dataItemX.CategorySchemePK, dataItemX.CompanyPositionPK);

    }

    function InitGridDetailCompanyPositionScheme() {
        $("#gridDetailCompanyPositionScheme").empty();


        if ($("#FilterCategoryScheme").val() == 0 || $("#FilterCategoryScheme").val() == "" || $("#FilterCategoryScheme").val() == "ALL") {
            _FilterCategoryScheme = "0";
        }
        else {
            _FilterCategoryScheme = $("#FilterCategoryScheme").val();
        }

        if ($("#FilterCompanyPosition").val() == 0 || $("#FilterCompanyPosition").val() == "") {
            _FilterCompanyPosition = "0";
        }
        else {
            _FilterCompanyPosition = $("#FilterCompanyPosition").val();
        }


        var Detail = window.location.origin + "/Radsoft/CompanyPosition/GetDataDetailCompanyPositionScheme/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _FilterCategoryScheme + "/" + _FilterCompanyPosition,
            dataSourceDetailCompanyPositionScheme = getDataSourceDetailCompanyPositionScheme(Detail);


        gridDetailCompanyPositionScheme = $("#gridDetailCompanyPositionScheme").kendoGrid({
            dataSource: dataSourceDetailCompanyPositionScheme,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "500px",
            editable: "incell",
            excel: {
                fileName: "Detail_CompanyPositionScheme.xlsx"
            },
            columns: [
                { command: { text: "Update", click: _update }, title: " ", width: 80 },
                {
                    field: "CategorySchemeID", title: "Category Scheme", headerAttributes: {
                        style: "text-align: center"
                    }, width: 200
                },
                {
                    field: "CompanyPositionID", title: "Company Position", headerAttributes: {
                        style: "text-align: center"
                    }, width: 200
                },
                {
                    field: "FeePercent", title: "Fee %", format: "{0:n4}", attributes: { style: "text-align:right;" },
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 100
                },
                

            ]
        }).data("kendoGrid");
    }



    function _update(e) {

        if (e) {
            var grid;
            grid = $("#gridDetailCompanyPositionScheme").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var CompanyPositionScheme = {
                CategorySchemePK: dataItemX.CategorySchemePK,
                CompanyPositionPK: dataItemX.CompanyPositionPK,
                FeePercent: dataItemX.FeePercent,
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/CompanyPosition/ValidateMaxPercentCompanyPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(CompanyPositionScheme),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Update Data ?", function (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CompanyPosition/UpdateCompanyPositionScheme/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(CompanyPositionScheme),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Update Success");
                                    InitGridDetailCompanyPositionScheme(dataItemX.CategorySchemePK, dataItemX.CompanyPositionPK);

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        });

                    } else {
                        alertify.alert("Total Fee Percent > 100% !");
                        $.unblockUI();
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });

        }

    }


    function onWinCompanyPositionSchemeClose() {
        clearDataCompanyPositionScheme();
    }

    function clearDataCompanyPositionScheme() {
        $("#gridDetailCompanyPositionScheme").empty();
        $("#FilterCategoryScheme").val("");
        $("#FilterCompanyPosition").val("");
    }



    $("#BtnAddCompanyPositionScheme").click(function () {

        //CategoryScheme
        $.ajax({
            url: window.location.origin + "/Radsoft/CategoryScheme/GetCategorySchemeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamCategoryScheme").kendoComboBox({
                    dataValueField: "CategorySchemePK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamCategoryScheme,

                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeParamCategoryScheme() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }


        $.ajax({
            url: window.location.origin + "/Radsoft/CompanyPosition/GetCompanyPositionComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamCompanyPosition").kendoComboBox({
                    dataValueField: "CompanyPositionPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamCompanyPosition,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeParamCompanyPosition() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }



        }

        $("#ParamFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
        });


        WinAddCompanyPositionScheme.center();
        WinAddCompanyPositionScheme.open();

    });

    $("#BtnOkAddCompanyPositionScheme").click(function () {


        alertify.confirm("Are you sure want to Add this data ?", function (e) {
            if (e) {
                var CompanyPositionScheme = {
                    CategorySchemePK: $('#ParamCategoryScheme').val(),
                    CompanyPositionPK: $('#ParamCompanyPosition').val(),
                    FeePercent: $('#ParamFeePercent').val(),
                    EntryUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/CompanyPosition/ValidateCheckHasAddCompanyPositionScheme/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#ParamCategoryScheme').val() + "/" + $('#ParamCompanyPosition').val(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {

                            $.ajax({
                                url: window.location.origin + "/Radsoft/CompanyPosition/ValidateMaxPercentCompanyPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(CompanyPositionScheme),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/CompanyPosition/AddCompanyPositionScheme/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                            type: 'POST',
                                            data: JSON.stringify(CompanyPositionScheme),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert("Add Scheme Success");
                                                InitGridDetailCompanyPositionScheme($('#ParamCategoryScheme').val(), $('#ParamCompanyPosition').val());

                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    } else {
                                        alertify.alert("Total Fee Percent > 100% !");
                                        $.unblockUI();
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                    $.unblockUI();
                                }
                            });

                        } else {
                            alertify.alert("Has Already Data Scheme !");
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });
            }
        });

    });

    $("#BtnCancelAddCompanyPositionScheme").click(function () {

        alertify.confirm("Are you sure want to Close ?", function (e) {
            if (e) {
                WinAddCompanyPositionScheme.close();
                alertify.alert("Close CompanyPositionScheme");
            }
        });
    });

    function onPopUpAddCompanyPositionSchemeClose() {
        $("#ParamCategoryScheme").val("");
        $("#ParamCompanyPosition").val("");
        $("#ParamFeePercent").val("");
      

    }
});
