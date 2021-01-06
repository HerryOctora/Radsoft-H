$(document).ready(function () {
    document.title = 'FORM OFFICE';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListCity;
    var htmlCityPK;
    var htmlCityDesc;
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
        win = $("#WinOffice").kendoWindow({
            height: 700,
            title: "Office Detail",
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

        WinListCountry = $("#WinListCountry").kendoWindow({
            height: "520px",
            title: "Country List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCountryClose
        }).data("kendoWindow");
    }



    var GlobValidator = $("#WinOffice").kendoValidator().data("kendoValidator");

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

            $("#OfficePK").val(dataItemX.OfficePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#Address").val(dataItemX.Address);
            $("#CityPK").val(dataItemX.CityPK);
            $("#CityDesc").val(dataItemX.CityDesc);
            $("#Country").val(dataItemX.Country);
            $("#CountryDesc").val(dataItemX.CountryDesc);
            $("#ZipCode").val(dataItemX.ZipCode);
            $("#FaxServerName").val(dataItemX.FaxServerName);
            $("#Email").val(dataItemX.Email);
            $("#Manager").val(dataItemX.Manager);
            $("#PaymentInstruction").val(dataItemX.PaymentInstruction);
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

        $("#Phone").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setPhone()
        });
        function setPhone() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Phone;
            }
        }
        $("#FaxNo").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setFaxNo()
        });
        function setFaxNo() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FaxNo;
            }
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

        //CashRef Payment
        $.ajax({
            url: window.location.origin + "/Radsoft/CashRef/GetCashRefCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashRefPaymentPK").kendoComboBox({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeCashRefPaymentPK,
                    filter: "contains",
                    suggest: true,
                    value: setCmbCashRefPaymentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function setCmbCashRefPaymentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashRefPaymentPK == 0) {
                    return "";
                } else {
                    return dataItemX.CashRefPaymentPK;
                }
            }
        }

        function OnChangeCashRefPaymentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //Parent
        $.ajax({
            url: window.location.origin + "/Radsoft/Office/GetOfficeComboGroupOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParentPK").kendoComboBox({
                    dataValueField: "OfficePK",
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
        $("#OfficePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Address").val("");
        $("#CityPK").val("");
        $("#CityDesc").val("");
        $("#Country").val("");
        $("#CountryDesc").val("");
        $("#ZipCode").val("");
        $("#Phone").val("");
        $("#FaxNo").val("");
        $("#FaxServerName").val("");
        $("#Email").val("");
        $("#Manager").val("");
        $("#CashRefPaymentPK").val("");
        $("#CashRefPaymentID").val("");
        $("#PaymentInstruction").val("");
        $("#Groups").val("");
        $("#ParentPK").val("");
        $("#ParentID").val("");
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
                             OfficePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Address: { type: "string" },
                             CityPK: { type: "number" },
                             CityDesc: { type: "string" },
                             Country: { type: "string" },
                             CountryDesc: { type: "string" },
                             ZipCode: { type: "string" },
                             Phone: { type: "string" },
                             FaxNo: { type: "string" },
                             FaxServerName: { type: "string" },
                             Email: { type: "string" },
                             Manager: { type: "string" },
                             CashRefPaymentPK: { type: "number" },
                             CashRefPaymentID: { type: "string" },
                             PaymentInstruction: { type: "string" },
                             Groups: { type: "boolean" },
                             ParentPK: { type: "number" },
                             ParentID: { type: "string" },
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
            var gridApproved = $("#gridOfficeApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridOfficePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridOfficeHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var OfficeApprovedURL = window.location.origin + "/Radsoft/Office/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(OfficeApprovedURL);

        $("#gridOfficeApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Office"
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
                { field: "OfficePK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "Address", title: "Address", width: 700 },
                { field: "CityDesc", title: "City", width: 250 },
                { field: "CountryDesc", title: "Country", width: 250 },
                { field: "ZipCode", title: "Zip Code", width: 200 },
                { field: "Phone", title: "Phone", width: 200 },
                { field: "FaxNo", title: "Fax No", width: 200 },
                { field: "FaxServerName", title: "Fax Server Name", width: 200 },
                { field: "Email", title: "Email", width: 200 },
                { field: "Manager", title: "Manager", width: 200 },
                { field: "CashRefPaymentID", title: "Cash Ref Payment", width: 200 },
                { field: "PaymentInstruction", title: "Payment Instruction", width: 700 },
                { field: "Groups", title: "Groups", width: 200, template: "#= Groups ? 'Yes' : 'No' #" },
                { field: "ParentID", title: "ParentID", width: 200 },
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
        $("#TabOffice").kendoTabStrip({
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
                        var OfficePendingURL = window.location.origin + "/Radsoft/Office/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(OfficePendingURL);
                        $("#gridOfficePending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Office"
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
                                { field: "OfficePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Address", title: "Address", width: 700 },
                                { field: "CityDesc", title: "City", width: 250 },
                                { field: "CountryDesc", title: "Country", width: 250 },
                                { field: "ZipCode", title: "Zip Code", width: 200 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "FaxNo", title: "Fax No", width: 200 },
                                { field: "FaxServerName", title: "Fax Server Name", width: 200 },
                                { field: "Email", title: "Email", width: 200 },
                                { field: "Manager", title: "Manager", width: 200 },
                                { field: "CashRefPaymentID", title: "Cash Ref Payment", width: 200 },
                                { field: "PaymentInstruction", title: "Payment Instruction", width: 700 },
                                { field: "Groups", title: "Groups", width: 200, template: "#= Groups ? 'Yes' : 'No' #" },
                                { field: "ParentID", title: "ParentID", width: 200 },
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

                        var OfficeHistoryURL = window.location.origin + "/Radsoft/Office/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(OfficeHistoryURL);

                        $("#gridOfficeHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Office"
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
                                { field: "OfficePK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Address", title: "Address", width: 700 },
                                { field: "CityDesc", title: "City", width: 250 },
                                { field: "CountryDesc", title: "Country", width: 250 },
                                { field: "ZipCode", title: "Zip Code", width: 200 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "FaxNo", title: "Fax No", width: 200 },
                                { field: "FaxServerName", title: "Fax Server Name", width: 200 },
                                { field: "Email", title: "Email", width: 200 },
                                { field: "Manager", title: "Manager", width: 200 },
                                { field: "CashRefPaymentID", title: "Cash Ref Payment", width: 200 },
                                { field: "PaymentInstruction", title: "Payment Instruction", width: 700 },
                                { field: "Groups", title: "Groups", width: 200, template: "#= Groups ? 'Yes' : 'No' #" },
                                { field: "ParentID", title: "ParentID", width: 200 },
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
        var grid = $("#gridOfficeHistory").data("kendoGrid");
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
                alertify.success("Close Detail");
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
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Office",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var Office = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Address: $('#Address').val(),
                                    CityPK: $('#CityPK').val(),
                                    Country: $('#Country').val(),
                                    ZipCode: $('#ZipCode').val(),
                                    Phone: $('#Phone').val(),
                                    FaxNo: $('#FaxNo').val(),
                                    FaxServerName: $('#FaxServerName').val(),
                                    Email: $('#Email').val(),
                                    Manager: $('#Manager').val(),
                                    CashRefPaymentPK: $('#CashRefPaymentPK').val(),
                                    PaymentInstruction: $('#PaymentInstruction').val(),
                                    Groups: $('#Groups').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Office/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Office_I",
                                    type: 'POST',
                                    data: JSON.stringify(Office),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.success(data);
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#OfficePK").val() + "/" + $("#HistoryPK").val() + "/" + "Office",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var Office = {
                                    OfficePK: $('#OfficePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Address: $('#Address').val(),
                                    CityPK: $('#CityPK').val(),
                                    Country: $('#Country').val(),
                                    ZipCode: $('#ZipCode').val(),
                                    Phone: $('#Phone').val(),
                                    FaxNo: $('#FaxNo').val(),
                                    FaxServerName: $('#FaxServerName').val(),
                                    Email: $('#Email').val(),
                                    Manager: $('#Manager').val(),
                                    CashRefPaymentPK: $('#CashRefPaymentPK').val(),
                                    PaymentInstruction: $('#PaymentInstruction').val(),
                                    Groups: $('#Groups').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Office/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Office_U",
                                    type: 'POST',
                                    data: JSON.stringify(Office),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.notify(data);
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#OfficePK").val() + "/" + $("#HistoryPK").val() + "/" + "Office",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Office" + "/" + $("#OfficePK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#OfficePK").val() + "/" + $("#HistoryPK").val() + "/" + "Office",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                            var Office = {
                                OfficePK: $('#OfficePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Office/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Office_A",
                                type: 'POST',
                                data: JSON.stringify(Office),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#OfficePK").val() + "/" + $("#HistoryPK").val() + "/" + "Office",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Office = {
                                OfficePK: $('#OfficePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Office/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Office_V",
                                type: 'POST',
                                data: JSON.stringify(Office),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#OfficePK").val() + "/" + $("#HistoryPK").val() + "/" + "Office",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Office = {
                                OfficePK: $('#OfficePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Office/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Office_R",
                                type: 'POST',
                                data: JSON.stringify(Office),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.success(data);
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
        htmlCityPK = "#CityPK";
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
               //{ field: "Code", title: "No", width: 100 },
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
        $(htmlCityPK).val(dataItemX.Code);
        WinListCity.close();

    }


    function getDataSourceListCountry() {
        return new kendo.data.DataSource(
             {
                 transport:
                         {
                             read:
                                 {
                                     url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDICountry",
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

    $("#btnListCountry").click(function () {
        WinListCountry.center();
        WinListCountry.open();
        initListCountry();
        htmlCountry = "#Country";
        htmlCountryDesc = "#CountryDesc";

    });


    function initListCountry() {
        var dsListCountry = getDataSourceListCountry();
        $("#gridListCountry").kendoGrid({
            dataSource: dsListCountry,
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
               { command: { text: "Select", click: ListCountrySelect }, title: " ", width: 60 },
               //{ field: "Code", title: "No", width: 100 },
               { field: "DescOne", title: "Country", width: 200 }
            ]
        });
    }

    function onWinListCountryClose() {
        $("#gridListCountry").empty();
    }

    function ListCountrySelect(e) {
        var grid = $("#gridListCountry").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCountryDesc).val(dataItemX.DescOne);
        $(htmlCountry).val(dataItemX.Code);
        WinListCountry.close();

    }
});
