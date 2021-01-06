$(document).ready(function () {
    document.title = 'FORM AGENT FEE SETUP';
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

        win = $("#WinAgentFeeSetup").kendoWindow({
            height: 700,
            title: "Agent Fee Setup",
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

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: null,
        });
        $("#DateAmortize").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

    }

    var GlobValidator = $("#WinAgentFeeSetup").kendoValidator().data("kendoValidator");

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

            $("#AgentFeeSetupPK").val(dataItemX.AgentFeeSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#AgentPK").val(dataItemX.AgentPK);
            $("#Date").data("kendoDatePicker").value(dataItemX.Date);
            $("#DateAmortize").data("kendoDatePicker").value(dataItemX.DateAmortize);
            $("#RangeFrom").val(dataItemX.RangeFrom);
            $("#RangeTo").val(dataItemX.RangeTo);
            $("#MIFeeAmount").val(dataItemX.MIFeeAmount);
            $("#MIFeePercent").val(dataItemX.MIFeePercent);
            $("#FeeType").val(dataItemX.FeeType);
            $("#FeeTypeDesc").val(dataItemX.FeeTypeDesc);
            $("#TypeTrx").val(dataItemX.TypeTrx);
            $("#TypeTrxDesc").val(dataItemX.TypeTrxDesc);
            $("#FundPK").val(dataItemX.FundPK);
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

        $("#RangeFrom").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setRangeFrom()
        });
        function setRangeFrom() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.RangeFrom;
            }
        }

        $("#RangeTo").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setRangeTo()
        });
        function setRangeTo() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.RangeTo;
            }
        }

        $("#MIFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setMIFeeAmount()
        });
        function setMIFeeAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MIFeeAmount;
            }
        }

        $("#MIFeePercent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setMIFeePercent()
        });
        function setMIFeePercent() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MIFeePercent;
            }
        }

        //combo box AgentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeAgentPK,
                    value: setCmbAgentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeAgentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbAgentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentPK == 0) {
                    return "";
                } else {
                    return dataItemX.AgentPK;
                }
            }
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

        //TypeTrx
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AgentFeeTrxType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TypeTrx").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTypeTrx,
                    value: setCmbTypeTrx()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeTypeTrx() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbTypeTrx() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TypeTrx == 0) {
                    return "";
                } else {
                    return dataItemX.TypeTrx;
                }
            }
        }


        //FeeType
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AgentFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FeeType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFeeType,
                    value: setCmbFeeType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



        function onChangeFeeType() {
            clearDataAgentFee();
            clearDataAgentFeeSetup();
            RequiredAttributes(this.value());
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 1) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
            }
            else if (this.value() == 2) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
            }
            else if (this.value() == 3) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
            }
            else if (this.value() == 4) {
                $("#lblDateAmortize").hide();
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
                $("#lblMIPercent").hide();
            }
            else if (this.value() == 5) {
                $("#lblDateAmortize").hide();
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
                $("#lblMIPercent").hide();


            }

            else if (this.value() == 6) {
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
                $("#lblMIPercent").hide();
            }
        }
        
        function setCmbFeeType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FeeType == 0) {
                    return "";
                } else {
                    return dataItemX.FeeType;
                }
            }
        }

     
        ////CommissionType
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboCommissionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/FundFeeType",
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#CommissionType").kendoComboBox({
        //            dataValueField: "Code",
        //            dataTextField: "DescOne",
        //            filter: "contains",
        //            suggest: true,
        //            dataSource: data,
        //            change: onChangeCommissionType,
        //            value: setCmbCommissionType()
        //        });
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


        //function onChangeCommissionType() {

        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}
        //function setCmbCommissionType() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.CommissionType == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.CommissionType;
        //        }
        //    }
        //}

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
        $("#AgentFeeSetupPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#AgentPK").val("");
        $("#AgentID").val("");
        $("#Date").val("");
        $("#DateAmortize").val("");
        $("#RangeFrom").val("");
        $("#RangeTo").val("");
        $("#MIFeeAmount").val("");
        $("#MIFeePercent").val("");
        $("#FeeType").val("");
        $("#FeeTypeDesc").val("");
        $("#TypeTrx").val("");
        $("#TypeTrxDesc").val("");
        $("#FundPK").val("");
        $("#FundID").val("");
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
                             AgentFeeSetupPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             AgentPK: { type: "number" },
                             AgentID: { type: "string" },
                             AgentName: { type: "string" },
                             Date: { type: "date" },
                             DateAmortize: { type: "date" },
                             RangeFrom: { type: "number" },
                             RangeTo: { type: "number" },
                             MIFeeAmount: { type: "number" },
                             MIFeePercent: { type: "number" },
                             FeeType: { type: "number" },
                             FeeTypeDesc: { type: "string" },
                             TypeTrx: { type: "number" },
                             TypeTrxDesc: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
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
            var gridApproved = $("#gridAgentFeeSetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridAgentFeeSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridAgentFeeSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var AgentFeeSetupApprovedURL = window.location.origin + "/Radsoft/AgentFeeSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(AgentFeeSetupApprovedURL);

        $("#gridAgentFeeSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Agent Fee Setup"
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
                { field: "AgentFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund", width: 200 },
                { field: "AgentID", title: "AgentID", width: 200 },
                { field: "AgentName", title: "AgentName", width: 200 },
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "RangeFrom", title: "Range From", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "RangeTo", title: "RangeTo", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } }, {
                    field: "MIFeePercent", title: "MI Fee Percent", width: 120,
                    template: "#: MIFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "DateAmortize", title: "Date Amortize", width: 110, template: "#= (DateAmortize == '1/1/1900 12:00:00 AM') ? ' ' : kendo.toString(kendo.parseDate(DateAmortize), 'dd/MMM/yyyy')#" },
                { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                { field: "TypeTrxDesc", title: "Type Trx", width: 200 },
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
        $("#TabAgentFeeSetup").kendoTabStrip({
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
                        var AgentFeeSetupPendingURL = window.location.origin + "/Radsoft/AgentFeeSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(AgentFeeSetupPendingURL);
                        $("#gridAgentFeeSetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Agent Fee Setup"
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
                                { field: "AgentFeeSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "AgentID", title: "AgentID", width: 200 },
                                { field: "AgentName", title: "AgentName", width: 200 },
                                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "RangeFrom", title: "Range From", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "RangeTo", title: "RangeTo", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } }, {
                                    field: "MIFeePercent", title: "MI Fee Percent", width: 120,
                                    template: "#: MIFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "DateAmortize", title: "Date Amortize", width: 110, template: "#= (DateAmortize == '1/1/1900 12:00:00 AM') ? ' ' : kendo.toString(kendo.parseDate(DateAmortize), 'dd/MMM/yyyy')#" },
                                { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                                { field: "TypeTrxDesc", title: "Type Trx", width: 200 },
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

                        var AgentFeeSetupHistoryURL = window.location.origin + "/Radsoft/AgentFeeSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(AgentFeeSetupHistoryURL);

                        $("#gridAgentFeeSetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Agent Fee Setup"
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
                                { field: "AgentFeeSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund", width: 200 },
                                { field: "AgentID", title: "AgentID", width: 200 },
                                { field: "AgentName", title: "AgentName", width: 200 },
                                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                                { field: "RangeFrom", title: "Range From", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "RangeTo", title: "RangeTo", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } }, {
                                    field: "MIFeePercent", title: "MI Fee Percent", width: 120,
                                    template: "#: MIFeePercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "DateAmortize", title: "Date Amortize", width: 110, template: "#= (DateAmortize == '1/1/1900 12:00:00 AM') ? ' ' : kendo.toString(kendo.parseDate(DateAmortize), 'dd/MMM/yyyy')#" },
                                { field: "FeeTypeDesc", title: "Fee Type", width: 200 },
                                { field: "TypeTrxDesc", title: "Type Trx", width: 200 },
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
        var grid = $("#gridAgentFeeSetupHistory").data("kendoGrid");
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

                    var Validate = {
                        FundPK: $('#FundPK').val(),
                        AgentPK: $('#AgentPK').val(),
                        Date: $('#Date').val(),
                        RangeFrom: $('#RangeFrom').val(),
                        RangeTo: $('#RangeTo').val(),
                        EntryUsersID: sessionStorage.getItem("user"),
                        FeeType: $('#FeeType').val()
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/AgentFeeSetup/AddValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(Validate),
                        success: function (data) {
                            if (data == "FALSE") {

                                var AgentFeeSetup = {
                                    FundPK: $('#FundPK').val(),
                                    AgentPK: $('#AgentPK').val(),
                                    Date: $('#Date').val(),
                                    DateAmortize: $('#DateAmortize').val(),
                                    RangeFrom: $('#RangeFrom').val(),
                                    RangeTo: $('#RangeTo').val(),
                                    MIFeeAmount: $('#MIFeeAmount').val(),
                                    MIFeePercent: $('#MIFeePercent').val(),
                                    FeeType: $('#FeeType').val(),
                                    TypeTrx: $('#TypeTrx').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AgentFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AgentFeeSetup_I",
                                    type: 'POST',
                                    data: JSON.stringify(AgentFeeSetup),
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
                                alertify.alert(data);
                                refresh();
                            }
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "AgentFeeSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var AgentFeeSetup = {
                                    AgentFeeSetupPK: $('#AgentFeeSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundPK: $('#FundPK').val(),
                                    AgentPK: $('#AgentPK').val(),
                                    Date: $('#Date').val(),
                                    DateAmortize: $('#DateAmortize').val(),
                                    RangeFrom: $('#RangeFrom').val(),
                                    RangeTo: $('#RangeTo').val(),
                                    MIFeeAmount: $('#MIFeeAmount').val(),
                                    MIFeePercent: $('#MIFeePercent').val(),
                                    FeeType: $('#FeeType').val(),
                                    TypeTrx: $('#TypeTrx').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AgentFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AgentFeeSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(AgentFeeSetup),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "AgentFeeSetup",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "AgentFeeSetup" + "/" + $("#AgentFeeSetupPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "AgentFeeSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AgentFeeSetup = {
                                AgentFeeSetupPK: $('#AgentFeeSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AgentFeeSetup_A",
                                type: 'POST',
                                data: JSON.stringify(AgentFeeSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "AgentFeeSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AgentFeeSetup = {
                                AgentFeeSetupPK: $('#AgentFeeSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AgentFeeSetup_V",
                                type: 'POST',
                                data: JSON.stringify(AgentFeeSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentFeeSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "AgentFeeSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AgentFeeSetup = {
                                AgentFeeSetupPK: $('#AgentFeeSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AgentFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AgentFeeSetup_R",
                                type: 'POST',
                                data: JSON.stringify(AgentFeeSetup),
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
    
    function clearDataAgentFee() {
        $("#MIFeeAmount").data("kendoNumericTextBox").value("");
        $("#RangeFrom").data("kendoNumericTextBox").value("");
        $("#RangeTo").data("kendoNumericTextBox").value("");
        $("#MIFeePercent").data("kendoNumericTextBox").value("");
        $("#DateAmortize").val("");
        $("#RangeTo").attr('readonly', false);

    }

    function clearDataAgentFeeSetup() {
        $("#lblRangeFrom").show();
        $("#lblRangeTo").show();
        $("#lblDateAmortize").show();
        $("#lblMIAmount").show();
        $("#lblFeeType").show();
        $("#lblDate").show();
        $("#lblMIPercent").show();

    }

    function RequiredAttributes(_type) {
        if (_type == 1 || _type == 2) {
            $("#DateAmortize").attr("required", false);
            $("#MIFeeAmount").attr("required", false);
        }
        else if (_type == 3 || _type == 4) {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#MIFeePercent").attr("required", false);
            $("#DateAmortize").attr("required", false);
        }
        else if (_type == 5) {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#MIFeeAmount").attr("required", false);
        }
    }
});