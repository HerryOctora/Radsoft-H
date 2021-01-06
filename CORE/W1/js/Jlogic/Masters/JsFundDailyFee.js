$(document).ready(function () {
    document.title = 'FORM FUND DAILY FEE';
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
        $("#BtnAccountRestructure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
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
        win = $("#WinFundDailyFee").kendoWindow({
            height: 600,
            title: "funddailyfee Detail",
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

    var GlobValidator = $("#WinFundDailyFee").kendoValidator().data("kendoValidator");

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
            grid = $("#gridFundDailyFeeApproved").data("kendoGrid");    
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            dirty = null;
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
        }

        //combo box FundPK
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
                    change: OnChangeFundPK,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundPK() {
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

        $("#ManagementFeeAmount").kendoNumericTextBox({
            format: "n4",
            value: setManagementFeeAmount(),
            //change: OnChangeAmount
        });
        function setManagementFeeAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ManagementFeeAmount == 0) {
                    return "";
                } else {
                    return dataItemX.ManagementFeeAmount;
                }
            }
        }

        $("#CustodiFeeAmount").kendoNumericTextBox({
            format: "n4",
            value: setCustodiFeeAmount(),
            //change: OnChangeAmount
        });
        function setCustodiFeeAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CustodiFeeAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CustodiFeeAmount;
                }
            }
        }

        $("#SubscriptionFeeAmount").kendoNumericTextBox({
            format: "n4",
            value: setSubscriptionFeeAmount(),
            //change: OnChangeAmount
        });
        function setSubscriptionFeeAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SubscriptionFeeAmount == 0) {
                    return "";
                } else {
                    return dataItemX.SubscriptionFeeAmount;
                }
            }
        }

        $("#RedemptionFeeAmount").kendoNumericTextBox({
            format: "n4",
            value: setRedemptionFeeAmount(),
            //change: OnChangeAmount
        });
        function setRedemptionFeeAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RedemptionFeeAmount == 0) {
                    return "";
                } else {
                    return dataItemX.RedemptionFeeAmount;
                }
            }
        }

        $("#SwitchingFeeAmount").kendoNumericTextBox({
            format: "n4",
            value: setSwitchingFeeAmount(),
            //change: OnChangeAmount
        });
        function setSwitchingFeeAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SwitchingFeeAmount == 0) {
                    return "";
                } else {
                    return dataItemX.SwitchingFeeAmount;
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
        $("#SubscriptionFeeAmount").val("");
        $("#RedemptionFeeAmount").val("");
        $("#SwitchingFeeAmount").val("");
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
                             SubscriptionFeeAmount: { type: "number" },
                             RedemptionFeeAmount: { type: "number" },
                             SwitchingFeeAmount: { type: "number" },
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
        
        $("#gridFundDailyFeeApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var FundDailyFeeApprovedURL = window.location.origin + "/Radsoft/FundDailyFee/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(FundDailyFeeApprovedURL);

        }

        var grid = $("#gridFundDailyFeeApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Daily Fee"
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
                //{ field: "FundDailyFeePK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundPK", hidden: true, title: "FundPK", width: 200 },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "FundName", title: "Fund Name", width: 200 },
                { field: "ManagementFeeAmount", title: "Management Fee Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "CustodiFeeAmount", title: "Custodi Fee Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                 { field: "SubscriptionFeeAmount", title: "Subscription Fee Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                  { field: "RedemptionFeeAmount", title: "Redemption Fee Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                   { field: "SwitchingFeeAmount", title: "Switching Fee Amount", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
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

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
           
                    
                var funddailyfee = {
                    Date: $('#Date').val(),
                    FundPK: $('#FundPK').val(),
                    ManagementFeeAmount: $('#ManagementFeeAmount').val(),
                    CustodiFeeAmount: $('#CustodiFeeAmount').val(),
                    SubscriptionFeeAmount: $('#SubscriptionFeeAmount').val(),
                    RedemptionFeeAmount: $('#RedemptionFeeAmount').val(),
                    SwitchingFeeAmount: $('#SwitchingFeeAmount').val(),

                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundDailyFee/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundDailyFee_U",
                    type: 'POST',
                    data: JSON.stringify(funddailyfee),
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
                       
            });
        }
    });
});

