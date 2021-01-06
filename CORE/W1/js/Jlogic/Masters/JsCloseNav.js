$(document).ready(function () {
    document.title = 'FORM CLOSE NAV';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinGetNav;
    //fund grid
    var checkedIds = {};
    var checkedApproved = {};
    var checkedPending = {};
        
    //end Fund grid
    //1
    initButton();
    //2
    initWindow();
    if (_GlobClientCode == "18") {
        $("#lblApprove1BySelected").show();
        $("#lblApprove2BySelected").show();
    }
    else {
        $("#lblApprove1BySelected").hide();
        $("#lblApprove2BySelected").hide();
    }
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
        $("#BtnGetNav").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnImportCloseNav").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnImportNavReconcile").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        }); 

        $("#BtnImportFxd").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnGetNavWindow").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnImportCloseNavSInvest").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        //--------------------------------------------//
        $("#BtnApprove1BySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });
        $("#BtnApprove2BySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

    }


    function initWindow() {
        $("#ParamDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
        });
        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom,
            value: new Date(),
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateTo,
            value: new Date(),
        });
        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDate,
        });

        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {
                
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            refresh();
        }
        function OnChangeDateTo() {
            var _DateTo = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateTo) {
                
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }
        function OnChangeDate() {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
            else {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            
                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }

        }

        win = $("#WinCloseNav").kendoWindow({
            height: 550,
            title: "Close Nav Detail",
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



        WinNavReconcile = $("#WinNavReconcile").kendoWindow({
            height: 750,
            title: "* Nav Reconcile",
            visible: false,
            width: 1100,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinNavReconcileClose
        }).data("kendoWindow");

        WinGetNav = $("#WinGetNav").kendoWindow({
            height: 500,
            title: "Get NAV",
            visible: false,
            width: 700,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinGetNavClose
        }).data("kendoWindow");



        WinImportFxd = $("#WinImportFxd").kendoWindow({
            height: 250,
            title: "Import Fxd",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            //close: onWinGetNavClose
        }).data("kendoWindow");
    }




    var GlobValidator = $("#WinCloseNav").kendoValidator().data("kendoValidator");
    var GlobValidatorGetNav = $("#WinGetNav").kendoValidator().data("kendoValidator");

    function validateData() {

        if (Number.isInteger(parseInt($("#FundPK").val())) == false) {
            alertify.alert("Please Choose Fund!");
            return 0;
        }

        if ($("#Date").val() != "") {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date dd/MMM/yyyy");
                return 0;
            }
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
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridCloseNavApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridCloseNavPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridCloseNavHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
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

            $("#CloseNavPK").val(dataItemX.CloseNavPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(kendo.parseDate(dataItemX.Date), 'dd/MMM/yyyy');
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
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFundPK,
                    value: setCmbFundPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeFundPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbFundPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }

        $("#Nav").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setNav(),
        });

        function setNav() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Nav == 0) {
                    return "";
                } else {
                    return dataItemX.Nav;
                }
            }
        }

        $("#OutstandingUnit").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setOutstandingUnit(),
        });

        function setOutstandingUnit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OutstandingUnit == 0) {
                    return "";
                } else {
                    return dataItemX.OutstandingUnit;
                }
            }
        }

        $("#OutstandingUnit").data("kendoNumericTextBox").enable(false);

        $("#AUM").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setAUM(),
        });

        function setAUM() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AUM == 0) {
                    return "";
                } else {
                    return dataItemX.AUM;
                }
            }
        }

        $("#Approved1").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: false,
            //change: OnChangApprove1,
            value: setCmbApproved1()
        });
        //function OnChangApprove1() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbApproved1() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.Approved1;
            }
        }

        $("#Approved2").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: false,
            //change: OnChangApprove2,
            value: setCmbApproved2()
        });
        //function OnChangApprove2() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        function setCmbApproved2() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.Approved2;
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
        $("#CloseNavPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").data("kendoDatePicker").value(null);
        $("#FundPK").val("");
        $("#Nav").val("");
        $("#OutstandingUnit").val("");
        $("#AUM").val("");
        $("#Approved1").val("");
        $("#Approved2").val("");
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
                 error: function (e) {
                     alert(e.errorThrown + " - " + e.xhr.responseText);
                     this.cancelChanges();
                 },
                 pageSize: 20,
                 schema: {
                     model: {
                         fields: {
                             CloseNAVPK: { type: "number" },
                             Nav: { type: "number" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridCloseNavPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridCloseNavHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {

        $("#gridCloseNavApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CloseNavApprovedURL = window.location.origin + "/Radsoft/CloseNav/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(CloseNavApprovedURL);

        }
        var grid = $("#gridCloseNavApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            groupable: {
                messages: {
                    empty: "Form Close Nav"
                }
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            scrollable: {
                virtual: true
            },
            pageable: {
                input: true,
                numeric: false
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            dataBound: onDataBoundApproved,
            toolbar: ["excel"],
            editable: "inline",
            columns: [
                {
                    headerTemplate: "<input type='checkbox' id='chbB' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbB'></label>",
                    template: "<input type='checkbox'   class='checkboxApproved' />", width: 45
                },
                { command: { text: "Show", click: showDetails }, title: " ", width: 80 },

                { field: "CloseNavPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "Nav", title: "Nav", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                { field: "AUM", title: "AUM", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "OutstandingUnit", title: "Outstanding Unit from S-Invest", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                //{ field: "Approved1", title: "Approved 1", width: 200, template: "#= Approved1 ? 'Yes' : 'No' #" },
                // { field: "Approved2", title: "Approved 2", width: 200, template: "#= Approved2 ? 'Yes' : 'No' #" },
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

        grid.table.on("click", ".checkboxApproved", selectRowApproved);
        var oldPageSizeApproved = 0;


        $('#chbB').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxApproved').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            grid.dataSource.pageSize(oldPageSizeApproved);

        });


        //$("#showSelectionApproved").bind("click", function () {
        //    var checked = [];
        //    for (var i in checkedApproved) {
        //        if (checkedApproved[i]) {
        //            checked.push(i);
        //        }
        //    }
        //    console.log(checked + ' ' + checked.length);
        //});

        function selectRowApproved() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridCloseNavApproved").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedApproved[dataItemZ.CloseNavPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundApproved(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedApproved[view[i].CloseNavPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxApproved")
                        .attr("checked", "checked");
                }
            }
        }


        //$("#SelectedAllApproved").change(function () {

        //    var _checked = this.checked;
        //    var _msg;
        //    if (_checked) {
        //        _msg = "Check All";
        //    } else {
        //        _msg = "UnCheck All"
        //    }

        //    alertify.alert(_msg);
        //    SelectDeselectAllData(_checked, "Approved");

        //});


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabCloseNav").kendoTabStrip({
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
        $("#BtnApprove1BySelected").hide();
        $("#BtnApprove2BySelected").hide();
    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnVoidBySelected").show();
        if (_GlobClientCode == "18")
        {
            $("#BtnApprove1BySelected").show();
            $("#BtnApprove2BySelected").show();
        }
        else
        {
            $("#BtnApprove1BySelected").hide();
            $("#BtnApprove2BySelected").hide();
        }
       
    }

    //function SelectDeselectData(_a, _b) {
    //    $.ajax({
    //        url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CloseNav/" + _a + "/" + _b,
    //        type: 'GET',
    //        contentType: "application/json;charset=utf-8",
    //        success: function (data) {
    //            var _msg;
    //            if (_a) {
    //                _msg = "Select Data ";
    //            } else {
    //                _msg = "DeSelect Data ";
    //            }
    //            alertify.set('notifier','position', 'top-center'); alertify.success(_msg + _b);
    //        },
    //        error: function (data) {
    //            alertify.alert(data.responseText);
    //        }
    //    });
    //}
    //function SelectDeselectAllData(_a, _b) {
    //    $.ajax({
    //        url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/CloseNav/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
    //        type: 'GET',
    //        contentType: "application/json;charset=utf-8",
    //        success: function (data) {
    //            $(".cSelectedDetail" + _b).prop('checked', _a);
    //        },
    //        error: function (data) {
    //            alertify.alert(data.responseText);
    //        }
    //    });
    //}

    function RecalGridPending() {
        $("#gridCloseNavPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CloseNavPendingURL = window.location.origin + "/Radsoft/CloseNav/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(CloseNavPendingURL);

        }

        if (_GlobClientCode == '24') {

            var grid = $("#gridCloseNavPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Close Nav"
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
                dataBound: onDataBoundPending,
                toolbar: ["excel"],
                columns: [
                    {
                        headerTemplate: "<input type='checkbox' id='chbPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "CloseNavPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 200 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "Nav", title: "Nav", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "AUM", title: "AUM", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OutstandingUnit", title: "Outstanding Unit from S-Invest", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    //{ field: "Approved1", title: "Approved 1", width: 200, template: "#= Approved1 ? 'Yes' : 'No' #" },
                    //{ field: "Approved2", title: "Approved 2", width: 200, template: "#= Approved2 ? 'Yes' : 'No' #" },
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
        }
        else {

            var grid = $("#gridCloseNavPending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Close Nav"
                    }
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                pageable: {
                    input: true,
                    numeric: false
                },
                reorderable: true,
                dataBound: onDataBoundPending,
                sortable: true,
                resizable: true,
                toolbar: ["excel"],
                columns: [
                    {
                        headerTemplate: "<input type='checkbox' id='chbPending' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbPending'></label>",
                        template: "<input type='checkbox'   class='checkboxPending' />", width: 45
                    },
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "CloseNavPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 200 },
                    { field: "Nav", title: "Nav", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "AUM", title: "AUM", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OutstandingUnit", title: "Outstanding Unit from S-Invest", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    //{ field: "Approved1", title: "Approved 1", width: 200, template: "#= Approved1 ? 'Yes' : 'No' #" },
                    //{ field: "Approved2", title: "Approved 2", width: 200, template: "#= Approved2 ? 'Yes' : 'No' #" },
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
        }

        grid.table.on("click", ".checkboxPending", selectRowPending);
        var oldPageSizeApproved = 0;


        $('#chbPending').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSizeApproved = grid.dataSource.pageSize();
            grid.dataSource.pageSize(grid.dataSource.data().length);

            $('.checkboxPending').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            grid.dataSource.pageSize(oldPageSizeApproved);

        });

        function selectRowPending() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                grid = $("#gridCloseNavPending").data("kendoGrid"),
                dataItemZ = grid.dataItem(rowA);

            checkedPending[dataItemZ.CloseNavPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBoundPending(e) {

            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedPending[view[i].CloseNavPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxPending")
                        .attr("checked", "checked");
                }
            }
        }

        //$("#SelectedAllPending").change(function () {

        //    var _checked = this.checked;
        //    var _msg;
        //    if (_checked) {
        //        _msg = "Check All";
        //    } else {
        //        _msg = "UnCheck All"
        //    }
        //    alertify.alert(_msg);
        //    SelectDeselectAllData(_checked, "Pending");

        //});

        //grid.table.on("click", ".cSelectedDetailPending", selectDataPending);

        //function selectDataPending(e) {


        //    var grid = $("#gridCloseNavPending").data("kendoGrid");
        //    var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        //    var _CloseNavPK = dataItemX.CloseNavPK;
        //    var _checked = this.checked;
        //    SelectDeselectData(_checked, _CloseNavPK);

        //}

        ResetButtonBySelectedData();
        $("#BtnVoidBySelected").hide();

    }

    function RecalGridHistory() {

        $("#gridCloseNavHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CloseNavHistoryURL = window.location.origin + "/Radsoft/CloseNav/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceHistory = getDataSource(CloseNavHistoryURL);

        }

        if (_GlobClientCode == '24') {

            $("#gridCloseNavHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Close Nav"
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
                    { field: "CloseNavPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 200 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "Nav", title: "Nav", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "AUM", title: "AUM", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OutstandingUnit", title: "Outstanding Unit from S-Invest", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    //{ field: "Approved1", title: "Approved 1", width: 200, template: "#= Approved1 ? 'Yes' : 'No' #" },
                    // { field: "Approved2", title: "Approved 2", width: 200, template: "#= Approved2 ? 'Yes' : 'No' #" },
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

            $("#gridCloseNavHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Close Nav"
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
                    { field: "CloseNavPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 200 },
                    { field: "Nav", title: "Nav", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    { field: "AUM", title: "AUM", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "OutstandingUnit", title: "Outstanding Unit from S-Invest", width: 250, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                    //{ field: "Approved1", title: "Approved 1", width: 200, template: "#= Approved1 ? 'Yes' : 'No' #" },
                    // { field: "Approved2", title: "Approved 2", width: 200, template: "#= Approved2 ? 'Yes' : 'No' #" },
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


        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnApprove1BySelected").hide();
        $("#BtnApprove2BySelected").hide();
    }


    function gridHistoryDataBound() {
        var grid = $("#gridCloseNavHistory").data("kendoGrid");
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
                    var CloseNav = {
                        Date: $('#Date').val(),
                        FundPK: $('#FundPK').val(),
                        Nav: $('#Nav').val(),
                        AUM: $('#AUM').val(),
                        Approved1: $('#Approved1').val(),
                        Approved2: $('#Approved2').val(),
                        EntryUsersID: sessionStorage.getItem("user"),
                        FundFrom: $('#FundPK').val(),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/CloseNav/ValidateGetCloseNav/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(CloseNav),
                        success: function (data) {
                            $.blockUI();
                            if (data == false) {
                                var CloseNav = {
                                    Date: $('#Date').val(),
                                    FundPK: $('#FundPK').val(),
                                    Nav: $('#Nav').val(),
                                    AUM: $('#AUM').val(),
                                    Approved1: $('#Approved1').val(),
                                    Approved2: $('#Approved2').val(),
                                    EntryUsersID: sessionStorage.getItem("user"),

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CloseNav/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CloseNav_I",
                                    type: 'POST',
                                    data: JSON.stringify(CloseNav),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        win.close();
                                        refresh();
                                        $.unblockUI();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("Already get data For this Day, Void / Reject First!");
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
        }
    });


    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CloseNavPK").val() + "/" + $("#HistoryPK").val() + "/" + "CloseNav",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                
                                            var CloseNav = {
                                                CloseNavPK: $('#CloseNavPK').val(),
                                                HistoryPK: $('#HistoryPK').val(),
                                                Date: $('#Date').val(),
                                                FundPK: $('#FundPK').val(),
                                                Nav: $('#Nav').val(),
                                                AUM: $('#AUM').val(),
                                                Approved1: $('#Approved1').val(),
                                                Approved2: $('#Approved2').val(),
                                                Notes: str,
                                                EntryUsersID: sessionStorage.getItem("user")
                                            };
                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/CloseNav/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CloseNav_U",
                                                type: 'POST',
                                                data: JSON.stringify(CloseNav),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CloseNavPK").val() + "/" + $("#HistoryPK").val() + "/" + "CloseNav",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "CloseNav" + "/" + $("#CloseNavPK").val(),
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

    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CloseNavPK").val() + "/" + $("#HistoryPK").val() + "/" + "CloseNav",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CloseNav = {
                                CloseNavPK: $('#CloseNavPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CloseNav_V",
                                type: 'POST',
                                data: JSON.stringify(CloseNav),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#CloseNavPK").val() + "/" + $("#HistoryPK").val() + "/" + "CloseNav",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var CloseNav = {
                                CloseNavPK: $('#CloseNavPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CloseNav_R",
                                type: 'POST',
                                data: JSON.stringify(CloseNav),
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


    $("#BtnApproved").click(function () {

        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ClosePricePK").val() + "/" + $("#HistoryPK").val() + "/" + "ClosePrice",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                        
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/CloseNav/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'MM/dd/yyyy HH:mm:ss'))
                                            {
                                                var ClosePrice = {
                                                    ClosePricePK: $('#ClosePricePK').val(),
                                                    HistoryPK: $('#HistoryPK').val(),
                                                    ApprovedUsersID: sessionStorage.getItem("user")
                                                };
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/ClosePrice/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ClosePrice_A",
                                                    type: 'POST',
                                                    data: JSON.stringify(ClosePrice),
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
                                else {
                                    alertify.alert("Data Has Been Add");
                                }
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



    $("#BtnApproveBySelected").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCloseNAVSelected = '';

        for (var i in ArrayFundFrom) {
            stringCloseNAVSelected = stringCloseNAVSelected + ArrayFundFrom[i] + ',';

        }
        stringCloseNAVSelected = stringCloseNAVSelected.substring(0, stringCloseNAVSelected.length - 1)



        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                $.ajax({

                    url: window.location.origin + "/Radsoft/CloseNav/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "false") {

                            var CloseNav = {
                                CloseNAVSelected: stringCloseNAVSelected,
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'POST',
                                data: JSON.stringify(CloseNav),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();

                                    if (_GlobClientCode == "29") {
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/HostToHostTeravin/NAVUpdate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                            type: 'GET',
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                console.log(data);
                                            },
                                            error: function (data) {
                                                alertify.alert(data.responseText);
                                            }
                                        });
                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }
                        else {
                            alertify.alert("Data Has Been Add");
                        }
                    }
                });
            }
        });

    });

    $("#BtnApprove1BySelected").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCloseNAVSelected = '';

        for (var i in ArrayFundFrom) {
            stringCloseNAVSelected = stringCloseNAVSelected + ArrayFundFrom[i] + ',';

        }
        stringCloseNAVSelected = stringCloseNAVSelected.substring(0, stringCloseNAVSelected.length - 1)

        alertify.confirm("Are you sure want to Approve 1 by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/CloseNav/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "false") {

                            var CloseNav = {
                                CloseNAVSelected: stringCloseNAVSelected,
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/Approve1BySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CloseNav_Approve1BySelected",
                                type: 'POST',
                                data: JSON.stringify(CloseNav),
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
                        else {
                            alertify.alert("Data Has Been Add");
                        }
                    }
                });
            }
        });
    });

    $("#BtnApprove2BySelected").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCloseNAVSelected = '';

        for (var i in ArrayFundFrom) {
            stringCloseNAVSelected = stringCloseNAVSelected + ArrayFundFrom[i] + ',';

        }
        stringCloseNAVSelected = stringCloseNAVSelected.substring(0, stringCloseNAVSelected.length - 1)

        alertify.confirm("Are you sure want to Approve 2 by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/CloseNav/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "false") {

                            var CloseNav = {
                                CloseNAVSelected: stringCloseNAVSelected,
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/Approve2BySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CloseNav_Approve2BySelected",
                                type: 'POST',
                                data: JSON.stringify(CloseNav),
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
                        else {
                            alertify.alert("Data Has Been Add");
                        }
                    }
                });
            }
        });
    });

    $("#BtnRejectBySelected").click(function (e) {

        var All = 0;
        All = [];
        for (var i in checkedPending) {
            if (checkedPending[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCloseNAVSelected = '';

        for (var i in ArrayFundFrom) {
            stringCloseNAVSelected = stringCloseNAVSelected + ArrayFundFrom[i] + ',';

        }
        stringCloseNAVSelected = stringCloseNAVSelected.substring(0, stringCloseNAVSelected.length - 1)

        console.log("Reject - " + stringCloseNAVSelected);
        
        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {

                var CloseNav = {
                    CloseNAVSelected: stringCloseNAVSelected,
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/CloseNav/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(CloseNav),
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
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringCloseNAVSelected = '';
        
        for (var i in ArrayFundFrom) {
            stringCloseNAVSelected = stringCloseNAVSelected + ArrayFundFrom[i] + ',';

        }
        stringCloseNAVSelected = stringCloseNAVSelected.substring(0, stringCloseNAVSelected.length - 1)

        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                var CloseNav = {
                    CloseNavSelected: stringCloseNAVSelected
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/CloseNav/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(CloseNav),
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

    $("#BtnImportNavReconcile").click(function () {
        document.getElementById("FileImportNavReconcile").click();
    });

    $("#FileImportNavReconcile").change(function () {
        

        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportNavReconcile").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }


        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("NavReconcile", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "NavReconcile_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportNavReconcile").val("");
                    GenerateGrid();
                    WinNavReconcile.center();
                    WinNavReconcile.open();

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportNavReconcile").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportNavReconcile").val("");
        }


    });


    $("#BtnImportFxd").click(function () {

        //Type//
        $("#TypeFile").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "CSV", value: "csv" },
                { text: "TXT", value: "txt" },

            ],
            filter: "contains",
            change: onChangeFormatImport,
            suggest: true
        });

        function onChangeFormatImport() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        WinImportFxd.center();
        WinImportFxd.open();
    });

    $("#BtnImportFxd11").click(function () {
        document.getElementById("FileImportFxd").click();
    });

    $("#FileImportFxd").change(function () {
        var filenamess = "";
        getFileType(document.getElementById('FileImportFxd').value);
        filenamess = filenames;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }


        if (filenamess == $("#TypeFile").val()) {
            $.blockUI({});
            var data = new FormData();
            var files = $("#FileImportFxd").get(0).files;
            var _d = files[0].name.substring(17, 25);
            if (files.length > 0) {
                data.append("FxdImport", files[0]);
                $.ajax({
                    url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Fxd_Import/01-01-1900/0",
                    type: 'POST',
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        $.unblockUI();
                        $("#FileImportFxd").val("");
                        alertify.alert(data);


                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                        $("#FileImportFxd").val("");
                    }
                });
            } else {
                alertify.alert("Please Choose Correct File");
                $("#FileImportNavReconcile").val("");
            }


        } else {

            $.unblockUI();
            alertify.alert("INI SALAH, PLEASE CEK DLU FORMATNYA!!!");
        }

    });

    function getFileType(filename) {
        parts = filename.split('.');
        filenames = parts[1];
        return filenames;
    }

    function onWinNavReconcileClose() {
        GlobValidator.hideMessages();
        clearData();
        refresh();
    }

    function getDataSourceReconcile(_url) {
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
                 pageSize: 1000,
                 schema: {
                     model: {
                         fields: {
                             NavReconcileTempPK: { type: "number" },
                             Selected: { type: "boolean" },
                             FundName: { type: "string" },
                             NavSystem: { type: "number" },
                             NavRecon: { type: "number" },
                         }
                     }
                 }
             });
    }

    function refreshGrid() {
        var newDS = getDataSource(window.location.origin + "/Radsoft/CloseNav/GetDataNavReconcile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"));
        $("#gridData").data("kendoGrid").setDataSource(newDS);

    }

    var gridData;
    function GenerateGrid() {
        $("#gridData").empty();
        var URL = window.location.origin + "/Radsoft/CloseNav/GetDataGenerateByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
          dataSourceApproved = getDataSourceReconcile(URL);
        gridData = $("#gridData").kendoGrid({
            dataSource: dataSourceApproved,
            height: 800,
            scrollable: {
                virtual: true
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
            toolbar: kendo.template($("#gridData").html()),
            columns: [
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailReconcile' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllReconcile' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "NavReconcilePK", title: "SysNo.", width: 95 },
                { field: "ValueDate", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MMM/yyyy')#" },
                { field: "FundName", title: "FundName", width: 200 },
                { field: "NavSystem", title: "Nav System", format: "{0:n4}", width: 200 },
                { field: "NavRecon", title: "Nav Recon", format: "{0:n4}", width: 200 },
            ]
        }).data("kendoGrid");
        $("#SelectedAllReconcile").change(function () {
            
            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }

            alertify.alert(_msg);
            SelectDeselectAllDataReconcile(_checked, "Approved");

        });

        gridData.table.on("click", ".cSelectedDetailReconcile", selectDataReconcile);

        function selectDataReconcile(e) {
            

            var grid = $("#gridData").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _navReconcilePK = dataItemX.NavReconcilePK;
            var _checked = this.checked;
            SelectDeselectDataReconcile(_checked, _navReconcilePK);

        }

    }

    function SelectDeselectDataReconcile(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/NavReconcile/" + _a + "/" + _b,
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
    function SelectDeselectAllDataReconcile(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/NavReconcile/" + _a,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("gridData").prop('checked', _a);
                refreshGrid();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

    $("#BtnInsertNav").click(function (e) {
        if (e.handled !== true) // This will prevent event triggering more then once
        {
            
            alertify.confirm("Are you sure want to Insert Nav?", function () {
                $.ajax({
                    url: window.location.origin + "/Radsoft/CloseNav/ValidateGenerateEndDayTrails/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/CloseNav/ValidateNavReconcile/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data == false) {
                                        var UpdateNav = {
                                            EntryUsersID: sessionStorage.getItem("user"),
                                        };

                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/CloseNav/InsertNavReconcileBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                            type: 'POST',
                                            data: JSON.stringify(UpdateNav),
                                            contentType: "application/json;charset=utf-8",
                                            success: function (data) {
                                                alertify.alert("Insert Nav Success");
                                                WinNavReconcile.close();
                                            },
                                        });
                                    } else {
                                        alertify.alert("Already get data For this Day, Void / Reject First!");

                                    }
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Please Check End Day Trails this Day First!");
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });
                   
            });
            e.handled = true;
        }
    });

    $("#BtnGetNavWindow").click(function () {
        showGetNav();
    });

    function showGetNav(e) {
        //fund grid disable dibawah ini
        ////Fund
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //            $("#FundFrom").kendoMultiSelect({
        //                dataValueField: "FundPK",
        //                dataTextField: "ID",
        //                filter: "contains",
        //                dataSource: data,
        //            });
        //            $("#FundFrom").data("kendoMultiSelect").value("0");


        //    },

        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        LoadData();
        //end fund grid

        //WinParamGenerate.center();
        //WinParamGenerate.open();

        WinGetNav.center();
        WinGetNav.open();

    }

    

    function onWinGetNavClose() {

        for (var i in checkedIds) {
            checkedIds[i] = null
        }
    }

    $("#BtnGetNav").click(function () {
        //fund grid
        var All = 0;
        All = [];
        for (var i in checkedIds) {
            if (checkedIds[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringFundFrom = '';
        // end fund grid

        //clearDataMulti();
        //var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        //var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)


        if (_GlobClientCode == '02' || _GlobClientCode == '03' || _GlobClientCode == '21' || _GlobClientCode == '25') {
            alertify.confirm("Are you sure want to Get Nav data ?", function (e) {
                if (e) {
                    var CloseNav = {
                        FundFrom: stringFundFrom,
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateEndDayTrailsFundPortfolioParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(CloseNav),
                        success: function (data) {
                            if (data.Result == 1) {
                                alertify.alert(data.Notes);
                                $.unblockUI();
                            }
                            else {
                                var CloseNav = {
                                    FundFrom: stringFundFrom,
                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckSubsRedempParamFund/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") ,
                                    type: 'POST',
                                    contentType: "application/json;charset=utf-8",
                                    data: JSON.stringify(CloseNav),
                                    success: function (data) {
                                        if (data.Result == 1) {
                                            alertify.alert(data.Notes);
                                            $.unblockUI();
                                        }
                                        else {
                                            var CloseNav = {
                                                FundFrom: stringFundFrom,
                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/CloseNav/ValidateGetCloseNav/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'POST',
                                                contentType: "application/json;charset=utf-8",
                                                data: JSON.stringify(CloseNav),
                                                success: function (data) {
                                                    $.blockUI();
                                                    if (data == false) {
                                                        var CloseNav = {
                                                            FundFrom: stringFundFrom,
                                                        };

                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/CloseNav/GetCloseNavByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                            type: 'POST',
                                                            data: JSON.stringify(CloseNav),
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                alertify.alert(data);
                                                                refresh();
                                                                $.unblockUI();
                                                            },
                                                            error: function (data) {
                                                                alertify.alert(data.responseText);
                                                                $.unblockUI();
                                                            }
                                                        });
                                                    } else {
                                                        alertify.alert("Already get data For this Day, Void / Reject First!");
                                                        $.unblockUI();
                                                    }
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });

                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });

                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
            });
        }
        else {
            alertify.confirm("Are you sure want to Get Nav data ?", function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/EndDayTrailsFundPortfolio/ValidateGenerateEndDayTrailsFundPortfolio/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {
                                var CloseNav = {
                                    FundFrom: stringFundFrom,
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/EndDayTrails/ValidateGenerateCheckSubsRedemp/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'POST',
                                    contentType: "application/json;charset=utf-8",
                                    data: JSON.stringify(CloseNav),
                                    success: function (data) {
                                        if (data == false) {
                                            var CloseNav = {
                                                FundFrom: stringFundFrom,
                                            };

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/CloseNav/ValidateGetCloseNav/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'POST',
                                                contentType: "application/json;charset=utf-8",
                                                data: JSON.stringify(CloseNav),
                                                success: function (data) {
                                                    $.blockUI();
                                                    if (data == false) {
                                                        var CloseNav = {
                                                            FundFrom: stringFundFrom,
                                                        };

                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/CloseNav/GetCloseNavByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                            type: 'POST',
                                                            data: JSON.stringify(CloseNav),
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                alertify.alert(data);
                                                                refresh();
                                                                $.unblockUI();
                                                            },
                                                            error: function (data) {
                                                                alertify.alert(data.responseText);
                                                                $.unblockUI();
                                                            }
                                                        });
                                                    } else {
                                                        alertify.alert("Already get data For this Day, Void / Reject First!");
                                                        $.unblockUI();
                                                    }
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });
                                        } else {
                                            alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
                                            $.unblockUI();
                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });
                            } else {
                                alertify.alert("Please Check End Day Trails Fund Portfolio First!");
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
        }


    });

    $("#BtnCancelGetNav").click(function () {

        alertify.confirm("Are you sure want to cancel Get Nav?", function (e) {
            if (e) {
                WinGetNav.close();
                alertify.alert("Cancel Get Nav");
            }
        });
    });

    function clearDataMulti() {
        $("#FundFrom").val("");

    }

    $("#BtnImportCloseNavSInvest").click(function () {
        document.getElementById("FileImportCloseNavSInvest").click();
    });

    $("#FileImportCloseNavSInvest").change(function () {
        //getFileType(document.getElementById('FileImportCloseNavSInvest').value);
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportCloseNavSInvest").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        if (fileSize > _GlobMaxFileSizeInMB) {
            $.unblockUI();
            alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
            return;
        }

        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("CloseNavSInvest", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "CloseNavSInvest_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportCloseNavSInvest").val("");
                    alertify.alert(data)
                    refresh();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportCloseNavSInvest").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportCloseNavSInvest").val("");
        }

    });


    //fund grid //2
    function LoadData() {

        var _date = kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");

        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/Fund/GetFundComboByMaturityDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    dataType: "json"

                }

            },
            batch: true,
            error: function (e) {
                alert(e.errorThrown + " - " + e.xhr.responseText);
                this.cancelChanges();
            },
            pageSize: 10,
            schema: {
                model: {
                    fields: {
                        FundPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },

                    }
                }
            }
        });


        var gridFundClient = $("#gridFund").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 350,
            dataBound: onDataBound,
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
                {
                    headerTemplate: "<input type='checkbox' id='chbA' class='k-checkbox header-checkbox'><label class='k-checkbox-label' for='chbA'></label>",
                    template: "<input type='checkbox'   class='checkboxFundList' />"
                }

                , {
                    field: "ID",
                    title: "Fund ID",
                    width: "300px"
                }, {
                    field: "Name",
                    title: "Fund Name",
                    width: "300px"
                }
            ],
            editable: "inline"
        }).data("kendoGrid");

        gridFundClient.table.on("click", ".checkboxFundList", selectRow);

        var oldPageSize = 0;

        $('#chbA').change(function (ev) {

            var checked = ev.target.checked;

            oldPageSize = gridFundClient.dataSource.pageSize();
            gridFundClient.dataSource.pageSize(gridFundClient.dataSource.data().length);

            $('.checkboxFundList').each(function (idx, item) {
                if (checked) {
                    if (!($(item).closest('tr').is('.k-state-selected'))) {
                        $(item).click();
                    }
                } else {
                    if ($(item).closest('tr').is('.k-state-selected')) {
                        $(item).click();
                    }
                }
            });

            gridFundClient.dataSource.pageSize(oldPageSize);

        });


        function selectRow() {
            var checked = this.checked,
                rowA = $(this).closest("tr"),
                gridFundClient = $("#gridFund").data("kendoGrid"),
                dataItemZ = gridFundClient.dataItem(rowA);

            checkedIds[dataItemZ.FundPK] = checked;
            if (checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }

        function onDataBound(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].FundPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                        .addClass("k-state-selected")
                        .find(".checkboxFundList")
                        .attr("checked", "checked");
                }
            }
        }
    }



    // end fund grid

    //$("#showSelection").bind("click", function () {
    //    var checked = [];
    //    for (var i in checkedIds) {
    //        if (checkedIds[i]) {
    //            checked.push(i);
    //        }
    //    }

    //    console.log(checked + ' ' + checked.length);
    //});
});
