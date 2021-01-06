﻿$(document).ready(function () {
    document.title = 'FORM COMPANY';
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

        win = $("#WinCompany").kendoWindow({
            height: 600,
            title: "Company Detail",
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



    var GlobValidator = $("#WinCompany").kendoValidator().data("kendoValidator");

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

            $("#CompanyPK").val(dataItemX.CompanyPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#Address").val(dataItemX.Address);
            $("#NPWP").val(dataItemX.NPWP);
            $("#DirectorOne").val(dataItemX.DirectorOne);
            $("#DirectorTwo").val(dataItemX.DirectorTwo);
            $("#DirectorThree").val(dataItemX.DirectorThree);
            $("#MKBDCode").val(dataItemX.MKBDCode);
            $("#NoIDPJK").val(dataItemX.NoIDPJK);
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

        $("#PPEMinimalMKBD").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setPPEMinimalMKBD()
        });
        function setPPEMinimalMKBD() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.PPEMinimalMKBD;
            }
        }

        $("#MIMinimalMKBD").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setMIMinimalMKBD()
        });
        function setMIMinimalMKBD() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MIMinimalMKBD;
            }
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
        $("#Fax").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setFax()
        });
        function setFax() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Fax;
            }
        }

        //Default Currency
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DefaultCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDefaultCurrencyPK,
                    value: setCmbDefaultCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeDefaultCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDefaultCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DefaultCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.DefaultCurrencyPK;
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
        $("#CompanyPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Address").val("");
        $("#NPWP").val("");
        $("#Phone").val("");
        $("#Fax").val("");
        $("#DirectorOne").val("");
        $("#DirectorTwo").val("");
        $("#DirectorThree").val("");
        $("#PPEMinimalMKBD").val("");
        $("#MIMinimalMKBD").val("");
        $("#DefaultCurrencyPK").val("");
        $("#MKBDCode").val("");
        $("#NoIDPJK").val("");
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
                             CompanyPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ID: { type: "string" },
                             Name: { type: "string" },
                             Address: { type: "string" },
                             NPWP: { type: "string" },
                             Phone: { type: "string" },
                             Fax: { type: "string" },
                             DirectorOne: { type: "string" },
                             DirectorTwo: { type: "string" },
                             DirectorThree: { type: "string" },
                             PPEMinimalMKBD: { type: "number" },
                             MIMinimalMKBD: { type: "number" },
                             DefaultCurrencyPK: { type: "number" },
                             DefaultCurrencyID: { type: "string" },
                             MKBDCode: { type: "string" },
                             NoIDPJK: { type: "string" },
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
            var gridApproved = $("#gridCompanyApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridCompanyPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridCompanyHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var CompanyApprovedURL = window.location.origin + "/Radsoft/Company/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(CompanyApprovedURL);

        $("#gridCompanyApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Company"
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
                { field: "CompanyPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "Address", title: "Address", width: 700 },
                { field: "NPWP", title: "NPWP", width: 200 },
                { field: "Phone", title: "Phone", width: 200 },
                { field: "Fax", title: "Fax", width: 200 },
                { field: "DirectorOne", title: "Director One", width: 200 },
                { field: "DirectorTwo", title: "Director Two", width: 200 },
                { field: "DirectorThree", title: "Director Three", width: 200 },
                { field: "PPEMinimalMKBD", title: "PPE Minimal MKBD", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MIMinimalMKBD", title: "MI Minimal MKBD", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "DefaultCurrencyID", title: "Default Currency", width: 200 },
                { field: "MKBDCode", title: "MKBD Code", width: 200 },
                { field: "NoIDPJK", title: "NoIDPJK", width: 200 },
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
        $("#TabCompany").kendoTabStrip({
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
                        var CompanyPendingURL = window.location.origin + "/Radsoft/Company/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(CompanyPendingURL);
                        $("#gridCompanyPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Company"
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
                                { field: "CompanyPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Address", title: "Address", width: 700 },
                                { field: "NPWP", title: "NPWP", width: 200 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "Fax", title: "Fax", width: 200 },
                                { field: "DirectorOne", title: "Director One", width: 200 },
                                { field: "DirectorTwo", title: "Director Two", width: 200 },
                                { field: "DirectorThree", title: "Director Three", width: 200 },
                                { field: "PPEMinimalMKBD", title: "PPE Minimal MKBD", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MIMinimalMKBD", title: "MI Minimal MKBD", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "DefaultCurrencyID", title: "Default Currency", width: 200 },
                                { field: "MKBDCode", title: "MKBD Code", width: 200 },
                                { field: "NoIDPJK", title: "NoIDPJK", width: 200 },
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

                        var CompanyHistoryURL = window.location.origin + "/Radsoft/Company/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(CompanyHistoryURL);

                        $("#gridCompanyHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Company"
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
                                { field: "CompanyPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ID", title: "ID", width: 200 },
                                { field: "Name", title: "Name", width: 300 },
                                { field: "Address", title: "Address", width: 700 },
                                { field: "NPWP", title: "NPWP", width: 200 },
                                { field: "Phone", title: "Phone", width: 200 },
                                { field: "Fax", title: "Fax", width: 200 },
                                { field: "DirectorOne", title: "Director One", width: 200 },
                                { field: "DirectorTwo", title: "Director Two", width: 200 },
                                { field: "DirectorThree", title: "Director Three", width: 200 },
                                { field: "PPEMinimalMKBD", title: "PPE Minimal MKBD", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MIMinimalMKBD", title: "MI Minimal MKBD", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "DefaultCurrencyID", title: "Default Currency", width: 200 },
                                { field: "MKBDCode", title: "MKBD Code", width: 200 },
                                { field: "NoIDPJK", title: "NoIDPJK", width: 200 },
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
        var grid = $("#gridCompanyHistory").data("kendoGrid");
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
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Company",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasApproved/" + sessionStorage.getItem("user") + "/" + "Company",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == true) {

                                            var Company = {
                                                ID: $('#ID').val(),
                                                Name: $('#Name').val(),
                                                Address: $('#Address').val(),
                                                NPWP: $('#NPWP').val(),
                                                Phone: $('#Phone').val(),
                                                Fax: $('#Fax').val(),
                                                DirectorOne: $('#DirectorOne').val(),
                                                DirectorTwo: $('#DirectorTwo').val(),
                                                DirectorThree: $('#DirectorThree').val(),
                                                PPEMinimalMKBD: $('#PPEMinimalMKBD').val(),
                                                MIMinimalMKBD: $('#MIMinimalMKBD').val(),
                                                DefaultCurrencyPK: $('#DefaultCurrencyPK').val(),
                                                MKBDCode: $('#MKBDCode').val(),
                                                NoIDPJK: $('#NoIDPJK').val(),
                                                EntryUsersID: sessionStorage.getItem("user")

                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/Company/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Company_I",
                                                type: 'POST',
                                                data: JSON.stringify(Company),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPK").val() + "/" + $("#HistoryPK").val() + "/" + "Company",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var Company = {
                                    CompanyPK: $('#CompanyPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Address: $('#Address').val(),
                                    NPWP: $('#NPWP').val(),
                                    Phone: $('#Phone').val(),
                                    Fax: $('#Fax').val(),
                                    DirectorOne: $('#DirectorOne').val(),
                                    DirectorTwo: $('#DirectorTwo').val(),
                                    DirectorThree: $('#DirectorThree').val(),
                                    PPEMinimalMKBD: $('#PPEMinimalMKBD').val(),
                                    MIMinimalMKBD: $('#MIMinimalMKBD').val(),
                                    DefaultCurrencyPK: $('#DefaultCurrencyPK').val(),
                                    MKBDCode: $('#MKBDCode').val(),
                                    NoIDPJK: $('#NoIDPJK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Company/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Company_U",
                                    type: 'POST',
                                    data: JSON.stringify(Company),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPK").val() + "/" + $("#HistoryPK").val() + "/" + "Company",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Company" + "/" + $("#CompanyPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPK").val() + "/" + $("#HistoryPK").val() + "/" + "Company",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasApproved/" + sessionStorage.getItem("user") + "/" + "Company",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == true) {
                                        var Company = {
                                            CompanyPK: $('#CompanyPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Company/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Company_A",
                                            type: 'POST',
                                            data: JSON.stringify(Company),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPK").val() + "/" + $("#HistoryPK").val() + "/" + "Company",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Company = {
                                CompanyPK: $('#CompanyPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Company/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Company_V",
                                type: 'POST',
                                data: JSON.stringify(Company),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CompanyPK").val() + "/" + $("#HistoryPK").val() + "/" + "Company",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Company = {
                                CompanyPK: $('#CompanyPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Company/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Company_R",
                                type: 'POST',
                                data: JSON.stringify(Company),
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
