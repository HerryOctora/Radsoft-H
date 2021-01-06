$(document).ready(function () {
    document.title = 'FORM CLOSE PRICE';
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
        $("#BtnImportMSSales").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

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
        $("#Date").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDate,
        });

        function OnChangeDate() {
            var _date = Date.parse($("#Date").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date MM/DD/YYYY");
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

        win = $("#WinMSSales").kendoWindow({
            height: 650,
            title: "Close Price Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        WinImportMSSales = $("#WinImportMSSales").kendoWindow({
            height: "520px",
            title: "Instrument List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            //close: onWinImportMSSalesClose
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



    var GlobValidator = $("#WinMSSales").kendoValidator().data("kendoValidator");

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
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridMSSalesApproved").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
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

            $("#MSSalesPK").val(dataItemX.MSSalesPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
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

        $("#MSSalesValue").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setMSSalesValue()
        });
        function setMSSalesValue() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MSSalesValue;
            }
        }

        $("#LowPriceValue").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setLowPriceValue()
        });
        function setLowPriceValue() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.LowPriceValue;
            }
        }

        $("#HighPriceValue").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setHighPriceValue()
        });
        function setHighPriceValue() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.HighPriceValue;
            }
        }
        $("#LiquidityPercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setLiquidityPercent()

        });
        function setLiquidityPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.LiquidityPercent;
            }
        }
        $("#HaircutPercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setHaircutPercent()
        });
        function setHaircutPercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.HaircutPercent;
            }
        }

        $("#CloseNAV").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setCloseNAV()
        });
        function setCloseNAV() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CloseNAV;
            }
        }

        $("#TotalNAVReksadana").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setTotalNAVReksadana()
        });
        function setTotalNAVReksadana() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TotalNAVReksadana;
            }
        }

        $("#NAWCHaircut").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNAWCHaircut()
        });
        function setNAWCHaircut() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.NAWCHaircut;
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
        $("#MSSalesPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").data("kendoDatePicker").value(null);
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#MSSalesValue").val("");
        $("#LowPriceValue").val("");
        $("#HighPriceValue").val("");
        $("#LiquidityPercent").val("");
        $("#HaircutPercent").val("");
        $("#CloseNAV").val("");
        $("#TotalNAVReksadana").val("");
        $("#NAWCHaircut").val("");
        $("#BondRating").val("");
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
                 pageSize: 1000,
                 schema: {
                     model: {
                         fields: {
                             MSSalesPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Date: { type: "date" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             MSSalesValue: { type: "number" },
                             LowPriceValue: { type: "number" },
                             HighPriceValue: { type: "number" },
                             LiquidityPercent: { type: "number" },
                             HaircutPercent: { type: "number" },
                             CloseNAV: { type: "number" },
                             TotalNAVReksadana: { type: "number" },
                             NAWCHaircut: { type: "number" },
                             BondRating: { type: "string" },
                             BondRatingDesc: { type: "string" },
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
            initGrid();
        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridMSSalesPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridMSSalesHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {

        $("#gridMSSalesApproved").empty();
        var MSSalesApprovedURL = window.location.origin + "/Radsoft/BrokerageCommission/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(MSSalesApprovedURL);

        var grid = $("#gridMSSalesApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Close Price"
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
                { field: "Code", title: "Code", width: 95 },
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 200 },
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


            var grid = $("#gridMSSalesApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _MSSalesPK = dataItemX.MSSalesPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _MSSalesPK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabMSSales").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },

            activate: function (e) {

                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);
                    refresh();
                }
            }
        });
        
    }

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnImportMSSales").click(function () {
        document.getElementById("FileImportMSSales").click();
    });

    $("#FileImportMSSales").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportMSSales").get(0).files;
        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("BC_MSSales", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/BrokerageCommission/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "MSSales_Import/01-01-1900/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    //if (data == "false") {
                        $("#FileImportMSSales").val("");
                        $.unblockUI();
                        alertify.alert(data)
                    //}
                    //else {
                    //    $.unblockUI();
                    //    $("#FileImportMSSales").val("");
                    //    alertify.alert(data)
                    //}
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportMSSales").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportMSSales").val("");
        }
    });

});
