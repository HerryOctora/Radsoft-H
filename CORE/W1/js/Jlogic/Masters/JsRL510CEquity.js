$(document).ready(function () {
    document.title = 'FORM RL510CEquity';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinListFundClient;
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
        win = $("#WinRL510CEquity").kendoWindow({
            height: 600,
            title: "RL510CEquity Detail",
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

    var GlobValidator = $("#WinRL510CEquity").kendoValidator().data("kendoValidator");

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
            grid = $("#gridRL510CEquityApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            dirty = null;
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
        }


        $("#").val(dataItemX.MKBDTrailsDesc);


        $.ajax({
            url: window.location.origin + "/Radsoft/MKBDTrails/GetMKBDTrailsCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MKBDTrailsPK").kendoComboBox({
                    dataValueField: "MKBDTrailsPK",
                    dataTextField: "LogMessages",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeMKBDTrailsPK,
                    value: setCmbMKBDTrailsPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeMKBDTrailsPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbMKBDTrailsPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MKBDTrailsPK == 0) {
                    return "";
                } else {
                    return dataItemX.MKBDTrailsPK;
                }
            }
        }

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


        //combo box InstrumentTypePK
        $.ajax({
            url: window.location.origin + "/Radsoft/InstrumentType/GetInstrumentTypeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentTypePK").kendoComboBox({
                    dataValueField: "InstrumentTypePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInstrumentTypePK,
                    value: setCmbInstrumentTypePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeInstrumentTypePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbInstrumentTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentTypePK;
                }
            }
        }


        //combo box HoldingPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Holding/GetHoldingCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#HoldingPK").kendoComboBox({
                    dataValueField: "HoldingPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeHoldingPK,
                    value: setCmbHoldingPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeHoldingPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbHoldingPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.HoldingPK == 0) {
                    return "";
                } else {
                    return dataItemX.HoldingPK;
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


        $("#Volume").kendoNumericTextBox({
            format: "n4",
            value: setVolume(),
            //change: OnChangeAmount
        });
        function setVolume() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Volume == 0) {
                    return "";
                } else {
                    return dataItemX.Volume;
                }
            }
        }

        $("#Price").kendoNumericTextBox({
            format: "n4",
            value: setPrice(),
            //change: OnChangeAmount
        });
        function setPrice() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Price == 0) {
                    return "";
                } else {
                    return dataItemX.Price;
                }
            }
        }

        $("#ClosePrice").kendoNumericTextBox({
            format: "n4",
            value: setClosePrice(),
            //change: OnChangeAmount
        });
        function setClosePrice() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ClosePrice == 0) {
                    return "";
                } else {
                    return dataItemX.ClosePrice;
                }
            }
        }

        $("#MarketValue").kendoNumericTextBox({
            format: "n4",
            value: setMarketValue(),
            //change: OnChangeAmount
        });
        function setMarketValue() {
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

        $("#TotalEquity").kendoNumericTextBox({
            format: "n4",
            value: setTotalEquity(),
            //change: OnChangeAmount
        });
        function setTotalEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TotalEquity == 0) {
                    return "";
                } else {
                    return dataItemX.TotalEquity;
                }
            }
        }

        $("#ConcentrationRisk").kendoNumericTextBox({
            format: "n4",
            value: setConcentrationRisk(),
            //change: OnChangeAmount
        });
        function setConcentrationRisk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ConcentrationRisk == 0) {
                    return "";
                } else {
                    return dataItemX.ConcentrationRisk;
                }
            }
        }

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
        $("#Date").val("");
        $("#FundPK").val("");
        $("#ManagementFeeAmount").val("");
        $("#CustodiFeeAmount").val("");
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
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundName: { type: "string" },
                             ManagementFeeAmount: { type: "number" },
                             CustodiFeeAmount: { type: "number" },
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

        $("#gridRL510CEquityApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var RL510CEquityApprovedURL = window.location.origin + "/Radsoft/RL510CEquity/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(RL510CEquityApprovedURL);

        }

        var grid = $("#gridRL510CEquityApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form RL510CEquity"
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
                //{ field: "RL510CEquityPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "MKBDTrailsPK", hidden: true, title: "MKBDTrailsPK", width: 200 },
                { field: "MKBDTrailsDesc", title: "MKBDTrails", width: 200 },
                { field: "InstrumentPK", hidden: true, title: "InstrumentPK", width: 200 },
                { field: "InstrumentDesc", title: "Instrument", width: 200 },
                { field: "InstrumentTypePK", hidden: true, title: "InstrumentTypePK", width: 200 },
                { field: "InstrumentTypeDesc", title: "InstrumentType", width: 200 },
                { field: "HoldingPK", hidden: true, title: "HoldingPK", width: 200 },
                { field: "HoldingDesc", title: "Holding", width: 200 },
                { field: "MKBD01", title: "MKBD01", width: 200 },
                { field: "MKBD02", title: "MKBD02", width: 200 },
                { field: "MKBD03", title: "MKBD03", width: 200 },
                { field: "MKBD09", title: "MKBD09", width: 200 },
                { field: "Volume", title: "Volume", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Price", title: "Price", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "ClosePrice", title: "ClosePrice", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MarketValue", title: "Market Value", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "HaircutPercent", title: "Haircut Percent", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "HaircutAmount", title: "Haircut Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "AfterHaircutAmount", title: "After Haircut Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "TotalEquity", title: "Total Equity", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "ConcentrationRisk", title: "Concentration Risk", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RankingLiabilities", title: "Ranking Liabilities", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
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

    $("#BtnNew").click(function () {
        $("#ID").attr('readonly', false);
        showDetails(null);
    });

});

