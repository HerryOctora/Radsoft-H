$(document).ready(function () {
    document.title = 'FORM RL510CDeposito';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
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

        $("#BtnAdd").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnOldData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnOldData.png"
        });

        $("#BtnVoid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoid.png"
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
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            //value: null
        });
        $("#Date").data("kendoDatePicker").enable(false);
        $("#DueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            //value: null

        });
        $("#DueDate").data("kendoDatePicker").enable(false);
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


        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {

                alertify.success("Wrong Format Date DD/MMM/YYYY");
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

                alertify.success("Wrong Format Date DD/MMM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }
        win = $("#WinRL510CDeposito").kendoWindow({
            height: 600,
            title: "RL510CDeposito Detail",
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

    var GlobValidator = $("#WinRL510CDeposito").kendoValidator().data("kendoValidator");

    function validateData() {


        if (GlobValidator.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.error("Validation not Pass");
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
            grid = $("#gridRL510CDepositoApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            dirty = null;
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#DueDate").data("kendoDatePicker").value(dataItemX.DueDate);
            $("#IsLiquidated").prop('checked', dataItemX.IsLiquidated);
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




        //$.ajax({
        //    url: window.location.origin + "/Radsoft/MKBDTrails/GetMKBDTrailsCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#MKBDTrailsPK").kendoComboBox({
        //            dataValueField: "MKBDTrailsPK",
        //            dataTextField: "LogMessages",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            change: OnChangeMKBDTrailsPK,
        //            value: setCmbMKBDTrailsPK()
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //function OnChangeMKBDTrailsPK() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}

        //function setCmbMKBDTrailsPK() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.MKBDTrailsPK == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.MKBDTrailsID;
        //        }
        //    }
        //}

        //combo box InstrumentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    enabled: false,
                    dataSource: data,
                    change: OnChangeInstrumentPK,
                    value: setCmbInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeInstrumentPK() {
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


        //combo box DepositoType
        $.ajax({
            url: window.location.origin + "/Radsoft/DepositoType/GetDepositoTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepositoTypePK").kendoComboBox({
                    dataValueField: "DepositoTypePK",
                    dataTextField: "DepType",
                    filter: "contains",
                    suggest: true,
                    enabled: false,
                    dataSource: data,
                    change: OnChangeDepositoTypePK,
                    value: setCmbDepositoTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDepositoTypePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbDepositoTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DepositoTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.DepositoTypePK;
                }
            }
        }

        $("#MKBD01").kendoNumericTextBox({
            format: "n",
            value: setMKBD01(),
            //change: OnChangeAmount
        });
        function setMKBD01() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MKBD01 == 0) {
                    return "";
                } else {
                    return dataItemX.MKBD01;
                }
            }
        }
        $("#MKBD01").data("kendoNumericTextBox").enable(false);

        $("#MKBD02").kendoNumericTextBox({
            format: "n",
            value: setMKBD02(),
            //change: OnChangeAmount
        });
        function setMKBD02() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MKBD02 == 0) {
                    return "";
                } else {
                    return dataItemX.MKBD02;
                }
            }
        }
        $("#MKBD02").data("kendoNumericTextBox").enable(false);

        $("#MKBD03").kendoNumericTextBox({
            format: "n",
            value: setMKBD03(),
            //change: OnChangeAmount
        });
        function setMKBD03() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MKBD03 == 0) {
                    return "";
                } else {
                    return dataItemX.MKBD03;
                }
            }
        }
        $("#MKBD03").data("kendoNumericTextBox").enable(false);

        $("#MKBD09").kendoNumericTextBox({
            format: "n",
            value: setMKBD09(),
            //change: OnChangeAmount
        });
        function setMKBD09() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MKBD09 == 0) {
                    return "";
                } else {
                    return dataItemX.MKBD09;
                }
            }
        }
        $("#MKBD09").data("kendoNumericTextBox").enable(false);


        $("#MarketValue").kendoNumericTextBox({
            format: "n4",
            value: seMarketValue(),
            //change: OnChangeAmount
        });
        function seMarketValue() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MarketValue == 0) {
                    return "";
                } else {
                    return dataItemX.MarketValue;
                }
            }
        }
        $("#MarketValue").data("kendoNumericTextBox").enable(false);

        $("#IsUnderDuedateAmount").kendoNumericTextBox({
            format: "n4",
            value: setIsUnderDuedateAmount(),
            //change: OnChangeAmount
        });
        function setIsUnderDuedateAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IsUnderDuedateAmount == 0) {
                    return "";
                } else {
                    return dataItemX.IsUnderDuedateAmount;
                }
            }
        }
        $("#IsUnderDuedateAmount").data("kendoNumericTextBox").enable(false);

        $("#NotUnderDuedateAmount").kendoNumericTextBox({
            format: "n4",
            value: setNotUnderDuedateAmount(),
            //change: OnChangeAmount
        });
        function setNotUnderDuedateAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.NotUnderDuedateAmount == 0) {
                    return "";
                } else {
                    return dataItemX.NotUnderDuedateAmount;
                }
            }
        }
        $("#NotUnderDuedateAmount").data("kendoNumericTextBox").enable(false);

        $("#QuaranteedAmount").kendoNumericTextBox({
            format: "n4",
            value: setQuaranteedAmount(),
            //change: OnChangeAmount
        });
        function setQuaranteedAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.QuaranteedAmount == 0) {
                    return "";
                } else {
                    return dataItemX.QuaranteedAmount;
                }
            }
        }
        $("#QuaranteedAmount").data("kendoNumericTextBox").enable(false);

        $("#NotLiquidatedAmount").kendoNumericTextBox({
            format: "n4",
            value: setNotLiquidatedAmount(),
            //change: OnChangeAmount
        });
        function setNotLiquidatedAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.NotLiquidatedAmount == 0) {
                    return "";
                } else {
                    return dataItemX.NotLiquidatedAmount;
                }
            }
        }
        $("#NotLiquidatedAmount").data("kendoNumericTextBox").enable(false);

        $("#HaircutAmount").kendoNumericTextBox({
            format: "n4",
            value: setHaircutAmount(),
            //change: OnChangeAmount
        });
        function setHaircutAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.HaircutAmount == 0) {
                    return "";
                } else {
                    return dataItemX.HaircutAmount;
                }
            }
        }
        $("#HaircutAmount").data("kendoNumericTextBox").enable(false);

        $("#HaircutPercent").kendoNumericTextBox({
            format: "n4",
            value: setHaircutPercent(),
            //change: OnChangeAmount
        });
        function setHaircutPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.HaircutPercent == 0) {
                    return "";
                } else {
                    return dataItemX.HaircutPercent;
                }
            }
        }
        $("#HaircutPercent").data("kendoNumericTextBox").enable(false);

        $("#AfterHaircutAmount").kendoNumericTextBox({
            format: "n4",
            value: setAfterHaircutAmount(),
            //change: OnChangeAmount
        });
        function setAfterHaircutAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AfterHaircutAmount == 0) {
                    return "";
                } else {
                    return dataItemX.AfterHaircutAmount;
                }
            }
        }
        $("#AfterHaircutAmount").data("kendoNumericTextBox").enable(false);

        $("#LiquidatedAmount").kendoNumericTextBox({
            format: "n4",
            value: setLiquidatedAmount(),
            //change: OnChangeAmount
        });
        function setLiquidatedAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.LiquidatedAmount == 0) {
                    return "";
                } else {
                    return dataItemX.LiquidatedAmount;
                }
            }
        }
        $("#LiquidatedAmount").data("kendoNumericTextBox").enable(false);

        $("#RankingLiabilities").kendoNumericTextBox({
            format: "n4",
            value: setRankingLiabilities(),
            //change: OnChangeAmount
        });
        function setRankingLiabilities() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RankingLiabilities == 0) {
                    return "";
                } else {
                    return dataItemX.RankingLiabilities;
                }
            }
        }
        $("#RankingLiabilities").data("kendoNumericTextBox").enable(false);


        win.center();
        win.open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        showButton();
        refresh();
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
                             Date: { type: "date" },
                             DueDate: { type: "date" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" },
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            initGrid();
        }
    }

    function initGrid() {

        $("#gridRL510CDepositoApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var RL510CDepositoApprovedURL = window.location.origin + "/Radsoft/RL510CDeposito/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(RL510CDepositoApprovedURL);

        }

        var grid = $("#gridRL510CDepositoApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form RL510CDeposito"
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
                //{ field: "RL510CDepositoPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "DueDate", title: "DueDate", width: 150, template: "#= kendo.toString(kendo.parseDate(DueDate), 'dd/MMM/yyyy')#" },
                { field: "MKBDTrailsPK", hidden: true, title: "MKBDTrailsPK", width: 200 },
                { field: "MKBDTrailsID", hidden: true, title: "MKBD Trails", width: 200 },
                { field: "InstrumentPK", hidden: true, title: "InstrumentPK", width: 200 },
                { field: "InstrumentID", title: "Instrument", width: 150 },
                { field: "DepositoTypePK", hidden: true, title: "DepositoTypePK", width: 200 },
                { field: "DepositoTypeID", title: "Deposito Type", width: 150 },
                { field: "IsLiquidated", title: "Is Liquidated", width: 200, template: "#= IsLiquidated ? 'Yes' : 'No' #" },
                { field: "MKBD01", hidden: true, title: "MKBD01", width: 150 },
                { field: "MKBD02", hidden: true, title: "MKBD02", width: 150 },
                { field: "MKBD03", hidden: true, title: "MKBD03", width: 150 },
                { field: "MKBD09", hidden: true, title: "MKBD09", width: 150 },
                { field: "IsUnderDuedateAmount", title: "Is Under Duedate Amount", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "NotUnderDuedateAmount", title: "Not Under Duedate Amount", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "QuaranteedAmount", title: "Quaranteed Amount", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "NotLiquidatedAmount", title: "Not Liquidated Amount", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "LiquidatedAmount", title: "Liquidated Amount", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MarketValue", title: "Market Value", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "HaircutPercent", title: "Haircut Percent", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "HaircutAmount", title: "Haircut Amount", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AfterHaircutAmount", title: "After Haircut Amount", width: 150, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RankingLiabilities", title: "Ranking Liabilities", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");

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

    //$("#BtnNew").click(function () {
    //    $("#ID").attr('readonly', false);
    //    showDetails(null);
    //});

});

