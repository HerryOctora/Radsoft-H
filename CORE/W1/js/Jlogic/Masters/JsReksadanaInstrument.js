$(document).ready(function () {
    document.title = 'FORM ReksadanaInstrument';
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

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#ExpiredDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        win = $("#WinReksadanaInstrument").kendoWindow({
            height: 550,
            title: "Fund Window Redemption Detail",
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



    var GlobValidator = $("#WinReksadanaInstrument").kendoValidator().data("kendoValidator");

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

            $("#ReksadanaInstrumentPK").val(dataItemX.ReksadanaInstrumentPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#ExpiredDate").data("kendoDatePicker").value(dataItemX.ExpiredDate);
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

        $("#InterestPercent").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setInterestPercent(),
            //change: OnChangeAmount
        });
        function setInterestPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestPercent == 0) {
                    return "";
                } else {
                    return dataItemX.InterestPercent;
                }
            }
        }

        $("#AvgPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setAvgPrice()
        });
        function setAvgPrice() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AvgPrice;
            }
        }

        $("#BuyVolume").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setBuyVolume()
        });
        function setBuyVolume() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.BuyVolume;
            }
        }

        $("#SellVolume1").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellVolume1()
        });
        function setSellVolume1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellVolume1;
            }
        }

        $("#SellVolume2").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellVolume2()
        });
        function setSellVolume2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellVolume2;
            }
        }

        $("#SellVolume3").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellVolume3()
        });
        function setSellVolume3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellVolume3;
            }
        }

        $("#SellVolume4").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellVolume4()
        });
        function setSellVolume4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellVolume4;
            }
        }

        $("#SellVolume5").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellVolume5()
        });
        function setSellVolume5() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellVolume5;
            }
        }

        $("#SellVolume6").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellVolume6()
        });
        function setSellVolume6() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellVolume6;
            }
        }

        $("#SellPrice1").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellPrice1()
        });
        function setSellPrice1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellPrice1;
            }
        }

        $("#SellPrice2").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellPrice2()
        });
        function setSellPrice2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellPrice2;
            }
        }

        $("#SellPrice3").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellPrice3()
        });
        function setSellPrice3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellPrice3;
            }
        }

        $("#SellPrice4").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellPrice4()
        });
        function setSellPrice4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellPrice4;
            }
        }

        $("#SellPrice5").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellPrice5()
        });
        function setSellPrice5() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellPrice5;
            }
        }

        $("#SellPrice6").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setSellPrice6()
        });
        function setSellPrice6() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SellPrice6;
            }
        }

        $("#EndVolume").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setEndVolume()
        });
        function setEndVolume() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.EndVolume;
            }
        }

        $("#Totaldays").kendoNumericTextBox({
            format: "0",
            value: setTotaldays(),
            //change: OnChangeAmount
        });
        function setTotaldays() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Totaldays == 0) {
                    return "";
                } else {
                    return dataItemX.Totaldays;
                }
            }
        }


        // ReksadanaPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentReksadanaCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ReksadanaPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeReksadanaPK,
                    value: setCmbReksadanaPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeReksadanaPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

        function setCmbReksadanaPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ReksadanaPK == 0) {
                    return "";
                } else {
                    return dataItemX.ReksadanaPK;
                }
            }
        }


        // InstrumentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentBondCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeInstrumentPK,
                    value: setCmbInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeInstrumentPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

        function setCmbInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentPK;
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
        $("#ReksadanaInstrumentPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").val("");
        $("#ReksadanaPK").val("");
        $("#InstrumentPK").val("");
        $("#ExpiredDate").val("");
        $("#InstrumentPK").val("");

        $("#InterestPercent").val("");
        $("#AvgPrice").val("");
        $("#BuyVolume").val("");
        $("#SellVolume1").val("");
        $("#SellVolume2").val("");
        $("#SellVolume3").val("");
        $("#SellVolume4").val("");
        $("#SellVolume5").val("");
        $("#SellVolume6").val("");
        $("#SellPrice1").val("");
        $("#SellPrice2").val("");
        $("#SellPrice3").val("");
        $("#SellPrice4").val("");
        $("#SellPrice5").val("");
        $("#SellPrice6").val("");
        $("#EndVolume").val("");
        $("#Totaldays").val("");

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
                             ReksadanaInstrumentPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Date: { type: "date" },
                             ReksadanaPK: { type: "string" },
                             ReksadanaID: { type: "string" },
                             InstrumentPK: { type: "string" },
                             InstrumentID: { type: "string" },
                             ExpiredDate: { type: "date" },

                             InterestPercent: { type: "number" },
                             AvgPrice: { type: "number" },
                             BuyVolume: { type: "number" },
                             SellVolume1: { type: "number" },
                             SellVolume2: { type: "number" },
                             SellVolume3: { type: "number" },
                             SellVolume4: { type: "number" },
                             SellVolume5: { type: "number" },
                             SellVolume6: { type: "number" },
                             SellPrice1: { type: "number" },
                             SellPrice2: { type: "number" },
                             SellPrice3: { type: "number" },
                             SellPrice4: { type: "number" },
                             SellPrice5: { type: "number" },
                             SellPrice6: { type: "number" },
                             EndVolume: { type: "number" },
                             Totaldays: { type: "number" },

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
            var gridApproved = $("#gridReksadanaInstrumentApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridReksadanaInstrumentPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridReksadanaInstrumentHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var ReksadanaInstrumentApprovedURL = window.location.origin + "/Radsoft/ReksadanaInstrument/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(ReksadanaInstrumentApprovedURL);

        $("#gridReksadanaInstrumentApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form ReksadanaInstrument"
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
                { field: "ReksadanaInstrumentPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "ReksadanaID", title: "Reksadana", width: 200 },
                { field: "InstrumentID", title: "Instrument", width: 200 },
                { field: "ExpiredDate", title: "Expired Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy')#" },

                { field: "InterestPercent", title: "Interest Percent", width: 200, attributes: { style: "text-align:right;" }, template: "#: InterestPercent  # %", },
                 { field: "AvgPrice", title: "Avg Price", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "BuyVolume", title: "Buy Volume", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellVolume1", title: "Sell Volume1", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellVolume2", title: "Sell Volume2", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellVolume3", title: "Sell Volume3", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellVolume4", title: "Sell Volume4", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellVolume5", title: "Sell Volume5", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellVolume6", title: "Sell Volume6", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellPrice1", title: "Sell Price1", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellPrice2", title: "Sell Price2", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellPrice3", title: "Sell Price3", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellPrice4", title: "Sell Price4", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellPrice5", title: "Sell Price5", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "SellPrice6", title: "Sell Price6", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "EndVolume", title: "End Volume", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                 { field: "Totaldays", title: "Total days", width: 200, attributes: { style: "text-align:right;" } },

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
        $("#TabReksadanaInstrument").kendoTabStrip({
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
                        var ReksadanaInstrumentPendingURL = window.location.origin + "/Radsoft/ReksadanaInstrument/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(ReksadanaInstrumentPendingURL);
                        $("#gridReksadanaInstrumentPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form ReksadanaInstrument"
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
                                { field: "ReksadanaInstrumentPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "ReksadanaID", title: "Reksadana", width: 200 },
                                { field: "InstrumentID", title: "Instrument", width: 200 },
                                { field: "ExpiredDate", title: "Expired Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy')#" },

                              { field: "InterestPercent", title: "Interest Percent", width: 200, attributes: { style: "text-align:right;" }, template: "#: InterestPercent  # %", },
                                 { field: "AvgPrice", title: "Avg Price", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "BuyVolume", title: "Buy Volume", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume1", title: "Sell Volume1", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume2", title: "Sell Volume2", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume3", title: "Sell Volume3", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume4", title: "Sell Volume4", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume5", title: "Sell Volume5", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume6", title: "Sell Volume6", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice1", title: "Sell Price1", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice2", title: "Sell Price2", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice3", title: "Sell Price3", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice4", title: "Sell Price4", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice5", title: "Sell Price5", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice6", title: "Sell Price6", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "EndVolume", title: "End Volume", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "Totaldays", title: "Total days", width: 200, attributes: { style: "text-align:right;" } },

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

                        var ReksadanaInstrumentHistoryURL = window.location.origin + "/Radsoft/ReksadanaInstrument/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(ReksadanaInstrumentHistoryURL);

                        $("#gridReksadanaInstrumentHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form ReksadanaInstrument"
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
                                { field: "ReksadanaInstrumentPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "ReksadanaID", title: "Reksadana", width: 200 },
                                { field: "InstrumentID", title: "Instrument", width: 200 },
                                { field: "ExpiredDate", title: "Expired Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ExpiredDate), 'dd/MMM/yyyy')#" },

                               { field: "InterestPercent", title: "Interest Percent", width: 200, attributes: { style: "text-align:right;" }, template: "#: InterestPercent  # %", },
                                 { field: "AvgPrice", title: "Avg Price", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "BuyVolume", title: "Buy Volume", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume1", title: "Sell Volume1", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume2", title: "Sell Volume2", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume3", title: "Sell Volume3", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume4", title: "Sell Volume4", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume5", title: "Sell Volume5", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellVolume6", title: "Sell Volume6", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice1", title: "Sell Price1", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice2", title: "Sell Price2", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice3", title: "Sell Price3", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice4", title: "Sell Price4", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice5", title: "Sell Price5", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "SellPrice6", title: "Sell Price6", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "EndVolume", title: "End Volume", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                                 { field: "Totaldays", title: "Total days", width: 200, attributes: { style: "text-align:right;" } },

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
        var grid = $("#gridReksadanaInstrumentHistory").data("kendoGrid");
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

                        var ReksadanaInstrument = {
                            Date: $('#Date').val(),
                            ReksadanaPK: $('#ReksadanaPK').val(),
                            InstrumentPK: $('#InstrumentPK').val(),
                            ExpiredDate: $('#ExpiredDate').val(),

                            InterestPercent: $('#InterestPercent').val(),
                            AvgPrice: $('#AvgPrice').val(),
                            BuyVolume: $('#BuyVolume').val(),
                            SellVolume1: $('#SellVolume1').val(),
                            SellVolume2: $('#SellVolume2').val(),
                            SellVolume3: $('#SellVolume3').val(),
                            SellVolume4: $('#SellVolume4').val(),
                            SellVolume5: $('#SellVolume5').val(),
                            SellVolume6: $('#SellVolume6').val(),
                            SellPrice1: $('#SellPrice1').val(),
                            SellPrice2: $('#SellPrice2').val(),
                            SellPrice3: $('#SellPrice3').val(),
                            SellPrice4: $('#SellPrice4').val(),
                            SellPrice5: $('#SellPrice5').val(),
                            SellPrice6: $('#SellPrice6').val(),
                            EndVolume: $('#EndVolume').val(),
                            Totaldays: $('#Totaldays').val(),

                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/ReksadanaInstrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ReksadanaInstrument_I",
                            type: 'POST',
                            data: JSON.stringify(ReksadanaInstrument),
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
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
                alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ReksadanaInstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "ReksadanaInstrument",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                    var ReksadanaInstrument = {
                                        ReksadanaInstrumentPK: $('#ReksadanaInstrumentPK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        Date: $('#Date').val(),
                                        ReksadanaPK: $('#ReksadanaPK').val(),
                                        InstrumentPK: $('#InstrumentPK').val(),
                                        ExpiredDate: $('#ExpiredDate').val(),

                                        InterestPercent: $('#InterestPercent').val(),
                                        AvgPrice: $('#AvgPrice').val(),
                                        BuyVolume: $('#BuyVolume').val(),
                                        SellVolume1: $('#SellVolume1').val(),
                                        SellVolume2: $('#SellVolume2').val(),
                                        SellVolume3: $('#SellVolume3').val(),
                                        SellVolume4: $('#SellVolume4').val(),
                                        SellVolume5: $('#SellVolume5').val(),
                                        SellVolume6: $('#SellVolume6').val(),
                                        SellPrice1: $('#SellPrice1').val(),
                                        SellPrice2: $('#SellPrice2').val(),
                                        SellPrice3: $('#SellPrice3').val(),
                                        SellPrice4: $('#SellPrice4').val(),
                                        SellPrice5: $('#SellPrice5').val(),
                                        SellPrice6: $('#SellPrice6').val(),
                                        EndVolume: $('#EndVolume').val(),
                                        Totaldays: $('#Totaldays').val(),

                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/ReksadanaInstrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ReksadanaInstrument_U",
                                        type: 'POST',
                                        data: JSON.stringify(ReksadanaInstrument),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ReksadanaInstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "ReksadanaInstrument",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "ReksadanaInstrument" + "/" + $("#ReksadanaInstrumentPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ReksadanaInstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "ReksadanaInstrument",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ReksadanaInstrument = {
                                ReksadanaInstrumentPK: $('#ReksadanaInstrumentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ReksadanaInstrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ReksadanaInstrument_A",
                                type: 'POST',
                                data: JSON.stringify(ReksadanaInstrument),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ReksadanaInstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "ReksadanaInstrument",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ReksadanaInstrument = {
                                ReksadanaInstrumentPK: $('#ReksadanaInstrumentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ReksadanaInstrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ReksadanaInstrument_V",
                                type: 'POST',
                                data: JSON.stringify(ReksadanaInstrument),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#ReksadanaInstrumentPK").val() + "/" + $("#HistoryPK").val() + "/" + "ReksadanaInstrument",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var ReksadanaInstrument = {
                                ReksadanaInstrumentPK: $('#ReksadanaInstrumentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/ReksadanaInstrument/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "ReksadanaInstrument_R",
                                type: 'POST',
                                data: JSON.stringify(ReksadanaInstrument),
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
