$(document).ready(function () {
    document.title = 'FORM REGULER INSTRUCTION';
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

        $("#BtnInsertReguler").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnOkRegulerInstruction").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnCancelRegulerInstruction").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }

    
    

    function initWindow() {

    $("#StartingDate").kendoDatePicker({
            format: "dd/MMM/yyyy",           
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
            
    });

    $("#ExpirationDate").kendoDatePicker({
            format: "dd/MMM/yyyy",       
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
       
    });

        win = $("#WinRegulerInstruction").kendoWindow({
            height: 950,
            title: "RegulerInstruction Detail",
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


    WinListFundClient = $("#WinListFundClient").kendoWindow({
        height: 450,
        title: "List FundClient ",
        visible: false,
        width: 750,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 100 })
        },
        close: onWinListFundClientClose
    }).data("kendoWindow");

    function onWinListFundClientClose() {
        $("#gridListFundClient").empty();
    }

    WinListRegulerInstruction = $("#WinListRegulerInstruction").kendoWindow({
        height: 200,
        title: "List Reguler Instruction ",
        visible: false,
        width: 400,
        modal: true,
        open: function (e) {
            this.wrapper.css({ top: 100 })
        },
        close: onWinListRegulerInstructionClose
    }).data("kendoWindow");



    var GlobValidator = $("#WinRegulerInstruction").kendoValidator().data("kendoValidator");

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
        $("#btnListFundClientPK").kendoButton({
            icon: "ungroup"
        }).data("kendoButton")

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

            $("#RegulerInstructionPK").val(dataItemX.RegulerInstructionPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#FundClientPK").val(dataItemX.FundClientPK);
            $("#FundClientID").val(dataItemX.FundClientID + " - " + dataItemX.FundClientName);
            $("#FundPK").val(dataItemX.FundPK);
            $("#FundID").val(dataItemX.FundID);
            //$("#TrxType").val(dataItemX.TypeRegulerInstruction);
            $("#BankRecipientPK").val(dataItemX.BankRecipientDesc + " - " + dataItemX.BankRecipientAccountNo);
            $("#StartingDate ").data("kendoDatePicker").value(dataItemX.StartingDate);
            $("#ExpirationDate ").data("kendoDatePicker").value(dataItemX.ExpirationDate);
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

        $("#TrxType").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Subscription", value: 1 },
                { text: "Redemption", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeTrxType,
            value: setCmbTrxType()
        });
        console.log($("#TrxType").index());
        function OnChangeTrxType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


            if ($("#TrxType").val() == 1)
                $("#LblBankRecipientPK").hide();
            else
                $("#LblBankRecipientPK").show();


        }
        function setCmbTrxType() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TrxType;
            }


            if ($("#TrxType").val() == 1)
                $("#LblBankRecipientPK").hide();
            else
                $("#LblBankRecipientPK").show();

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
            $("#FundCashRefPK").data("kendoComboBox").value("");
            if (e != null) {
                if (this.value() == dataItemX.FundPK) {
                    return;
                }
            }
            else {
                getCashRefPKByFundPK($("#FundPK").val());
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


        //Combo Box Cash Ref 
        if ($("#FundPK").val() == "" || $("#FundPK").val() == 0) {
            var _fundPK = 0
        }
        else {
            var _fundPK = $("#FundPK").val()
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/SUB",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundCashRefPK").kendoComboBox({
                    dataValueField: "FundCashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeCashRefPK,
                    value: setCmbCashRefPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeCashRefPK() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            getCashRefPKByFundPK($("#FundPK").data("kendoComboBox").value());

        }
        function setCmbCashRefPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundCashRefPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundCashRefPK;
                }
            }
        }
        function getCashRefPKByFundPK(_fundPK) {
            $.ajax({
                url: window.location.origin + "/Radsoft/FundCashRef/GetFundCashRefComboByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/SUB",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#FundCashRefPK").kendoComboBox({
                        dataValueField: "FundCashRefPK",
                        dataTextField: "ID",
                        dataSource: data,
                        filter: "contains",
                        suggest: true,
                        index: 0,
                        change: onChangeCashRefPK

                    });
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
            function onChangeCashRefPK() {

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }


            }

        }


        //Combo Box Bank Recipient
        if ($("#FundClientPK").val() == "" || $("#FundClientPK").val() == 0) {
            var _fundClientPK = 0
        }
        else {
            var _fundClientPK = dataItemX.FundClientPK;
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetBankRecipientComboByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankRecipientPK").kendoComboBox({
                    dataValueField: "BankRecipientPK",
                    dataTextField: "AccountNo",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankRecipient,
                    value: setCmbBankRecipient()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankRecipient() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }
        function setCmbBankRecipient() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankRecipientPK == 0) {
                    return "";
                } else {
                    return dataItemX.BankRecipientPK;
                }
            }
        }

        $("#AutoDebitDate").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setAutoDebitDate()
        });
        function setAutoDebitDate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AutoDebitDate;
            }
        }

        $("#GrossAmount").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            change: onChangeGrossAmount,
            value: setGrossAmount()
        });
        function setGrossAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.GrossAmount;
            }
        }


        $("#FeePercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            change: onChangeFeePercent,
            value: setFeePercent(),


        });
        function setFeePercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FeePercent == 0) {
                    return 0;
                } else {
                    return dataItemX.FeePercent;
                }
            }
        }


        $("#FeeAmount").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            change: onChangeFeeAmount,
            value: setFeeAmount()
        });
        function setFeeAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.FeeAmount;
            }
        }

        $("#NetAmount").kendoNumericTextBox({
            format: "n0",
            decimals: 0,
            value: setNetAmount()
        });
        function setNetAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.NetAmount;
            }
        }
        $("#NetAmount").data("kendoNumericTextBox").enable(false);



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
        $("#RegulerInstructionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundClientPK").val("");
        $("#FundClientID").val("");
        $("#FundClientName").val("");
        $("#FundPK").val("");
        $("#FundID").val("");
        $("#FundCashRefPK").val("");
        $("#FundCashRefID").val("");
        $("#TrxType").val("");
        $("#AutoDebitDate").val("");
        $("#StartingDate").data("kendoDatePicker").value(null);
        $("#ExpirationDate").data("kendoDatePicker").value(null);
        $("#GrossAmount").val("");
        $("#FeePercent").val("");
        $("#FeeAmount").val("");
        $("#NetAmount").val("");
        $("#BankRecipientPK").val("");
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
                             RegulerInstructionPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             FundClientPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundCashRefPK: { type: "number" },
                             FundCashRefID: { type: "string" },
                             TrxType: { type: "number" },
                             TrxTypeDesc: { type: "string" },
                             AutoDebitDate: { type: "number" },
                             StartingDate: { type: "date" },
                             ExpirationDate: { type: "date" },
                             GrossAmount: { type: "number" },
                             FeePercent: { type: "number" },
                             FeeAmount: { type: "number" },
                             NetAmount: { type: "number" },
                             BankRecipientPK: { type: "number" },
                             BankRecipientDesc: { type: "string" },
                             BankRecipientAccountNo: { type: "string" },
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
            var gridApproved = $("#gridRegulerInstructionApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridRegulerInstructionPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridRegulerInstructionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var RegulerInstructionApprovedURL = window.location.origin + "/Radsoft/RegulerInstruction/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(RegulerInstructionApprovedURL);

        $("#gridRegulerInstructionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Reguler Instruction"
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
                { field: "RegulerInstructionPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundClientPK", title: "FundClientPK", hidden: true, width: 200 },
                { field: "FundClientID", title: "Fund Client ID", width: 300 },
                { field: "FundClientName", title: "Fund Client Name", width: 300 },
                { field: "TrxType", title: "TrxType", hidden: true, width: 300 },
                { field: "TrxTypeDesc", title: "Type", width: 300 },
                { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                { field: "FundID", title: "Fund ID", width: 150 },
                { field: "AutoDebitDate", title: "Auto Debit Date", width: 300 },              
                { field: "StartingDate", title: "Starting Date", width: 150, template: "#= kendo.toString(kendo.parseDate(StartingDate), 'dd/MMM/yyyy')#" },
                { field: "ExpirationDate", title: "ExpirationDate", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpirationDate), 'dd/MMM/yyyy')#" },
                { field: "GrossAmount", title: "GrossAmount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                {
                    field: "FeePercent", title: "Fee Percent", width: 200,
                    template: "#: FeePercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FeeAmount", title: "Fee Amount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "NetAmount", title: "NetAmount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } }, 
                { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                { field: "FundCashRefPK", title: "FundCashRefPK", hidden: true, width: 200 },
                { field: "FundCashRefID", title: "Fund Cash Ref", width: 200 },
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
        $("#TabRegulerInstruction").kendoTabStrip({
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
                        var RegulerInstructionPendingURL = window.location.origin + "/Radsoft/RegulerInstruction/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(RegulerInstructionPendingURL);
                        $("#gridRegulerInstructionPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Reguler Instruction"
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
                                { field: "RegulerInstructionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundClientPK", title: "FundClientPK", hidden: true, width: 200 },
                                { field: "FundClientID", title: "Fund Client ID", width: 300 },
                                { field: "FundClientName", title: "Fund Client Name", width: 300 },
                                { field: "TrxType", title: "TrxType", hidden: true, width: 300 },
                                { field: "TrxTypeDesc", title: "Type", width: 300 },
                                { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "AutoDebitDate", title: "Auto Debit Date", width: 300 },
                                { field: "StartingDate", title: "Starting Date", width: 150, template: "#= kendo.toString(kendo.parseDate(StartingDate), 'dd/MMM/yyyy')#" },
                                { field: "ExpirationDate", title: "ExpirationDate", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpirationDate), 'dd/MMM/yyyy')#" },
                                { field: "GrossAmount", title: "GrossAmount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                {
                                   field: "FeePercent", title: "Fee Percent", width: 200,
                                   template: "#: FeePercent  # %", attributes: { style: "text-align:right;" }
                                },
                                { field: "FeeAmount", title: "Fee Amount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                { field: "NetAmount", title: "NetAmount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                                { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                                { field: "FundCashRefPK", title: "FundCashRefPK", hidden: true, width: 200 },
                                { field: "FundCashRefID", title: "Fund Cash Ref", width: 200 },
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

                        var RegulerInstructionHistoryURL = window.location.origin + "/Radsoft/RegulerInstruction/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(RegulerInstructionHistoryURL);

                        $("#gridRegulerInstructionHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Reguler Instruction"
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
                                { field: "RegulerInstructionPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundClientPK", title: "FundClientPK", hidden: true, width: 200 },
                                { field: "FundClientID", title: "Fund Client ID", width: 300 },
                                { field: "TrxType", title: "TrxType", hidden: true, width: 300 },
                                { field: "TrxTypeDesc", title: "Type", width: 300 },
                                { field: "FundClientName", title: "Fund Client Name", width: 300 },
                                { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                                { field: "FundID", title: "Fund ID", width: 150 },
                                { field: "AutoDebitDate", title: "Auto Debit Date", width: 300 },
                                { field: "StartingDate", title: "Starting Date", width: 150, template: "#= kendo.toString(kendo.parseDate(StartingDate), 'dd/MMM/yyyy')#" },
                                { field: "ExpirationDate", title: "ExpirationDate", width: 150, template: "#= kendo.toString(kendo.parseDate(ExpirationDate), 'dd/MMM/yyyy')#" },
                                { field: "GrossAmount", title: "GrossAmount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                {
                                    field: "FeePercent", title: "Fee Percent", width: 200,
                                    template: "#: FeePercent  # %", attributes: { style: "text-align:right;" }
                                },
                                { field: "FeeAmount", title: "Fee Amount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                { field: "NetAmount", title: "NetAmount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                { field: "BankRecipientPK", title: "Bank Recipient", hidden: true, width: 200 },
                                { field: "BankRecipientDesc", title: "Bank Recipient", width: 200 },
                                { field: "FundCashRefPK", title: "FundCashRefPK", hidden: true, width: 200 },
                                { field: "FundCashRefID", title: "Fund Cash Ref", width: 200 },
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
        var grid = $("#gridRegulerInstructionHistory").data("kendoGrid");
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
                    var RegulerInstruction = {
                        FundClientPK: $('#FundClientPK').val(),
                        FundPK: $('#FundPK').val(),
                        FundCashRefPK: $('#FundCashRefPK').val(),
                        TrxType: $('#TrxType').val(),
                        AutoDebitDate: $('#AutoDebitDate').val(),
                        StartingDate: $('#StartingDate').val(),
                        ExpirationDate: $('#ExpirationDate').val(),
                        GrossAmount: $('#GrossAmount').val(),
                        FeePercent: $('#FeePercent').val(),
                        FeeAmount: $('#FeeAmount').val(),
                        NetAmount: $('#NetAmount').val(),
                        BankRecipientPK: $('#BankRecipientPK').val(),
                        EntryUsersID: sessionStorage.getItem("user")

                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/RegulerInstruction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RegulerInstruction_I",
                        type: 'POST',
                        data: JSON.stringify(RegulerInstruction),
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
            
            alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RegulerInstructionPK").val() + "/" + $("#HistoryPK").val() + "/" + "RegulerInstruction",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var RegulerInstruction = {
                                    RegulerInstructionPK: $('#RegulerInstructionPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundClientPK: $('#FundClientPK').val(),
                                    FundPK: $('#FundPK').val(),
                                    FundCashRefPK: $('#FundCashRefPK').val(),
                                    TrxType: $('#TrxType').val(),
                                    AutoDebitDate: $('#AutoDebitDate').val(),
                                    StartingDate: $('#StartingDate').val(),
                                    ExpirationDate: $('#ExpirationDate').val(),
                                    GrossAmount: $('#GrossAmount').val(),
                                    FeePercent: $('#FeePercent').val(),
                                    FeeAmount: $('#FeeAmount').val(),
                                    NetAmount: $('#NetAmount').val(),
                                    BankRecipientPK: $('#BankRecipientPK').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/RegulerInstruction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RegulerInstruction_U",
                                    type: 'POST',
                                    data: JSON.stringify(RegulerInstruction),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RegulerInstructionPK").val() + "/" + $("#HistoryPK").val() + "/" + "RegulerInstruction",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "RegulerInstruction" + "/" + $("#RegulerInstructionPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RegulerInstructionPK").val() + "/" + $("#HistoryPK").val() + "/" + "RegulerInstruction",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                            var RegulerInstruction = {
                                RegulerInstructionPK: $('#RegulerInstructionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/RegulerInstruction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RegulerInstruction_A",
                                type: 'POST',
                                data: JSON.stringify(RegulerInstruction),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RegulerInstructionPK").val() + "/" + $("#HistoryPK").val() + "/" + "RegulerInstruction",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var RegulerInstruction = {
                                RegulerInstructionPK: $('#RegulerInstructionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/RegulerInstruction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RegulerInstruction_V",
                                type: 'POST',
                                data: JSON.stringify(RegulerInstruction),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#RegulerInstructionPK").val() + "/" + $("#HistoryPK").val() + "/" + "RegulerInstruction",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var RegulerInstruction = {
                                RegulerInstructionPK: $('#RegulerInstructionPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/RegulerInstruction/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "RegulerInstruction_R",
                                type: 'POST',
                                data: JSON.stringify(RegulerInstruction),
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


    function getDataSourceListFundClient() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/FundClient/GetFundClientCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                                 FundClientPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" }
                             }
                         }
                     }
                 });



    }

    $("#btnListFundClientPK").click(function () {
        initListFundClientPK();

        WinListFundClient.center();
        WinListFundClient.open();
        htmlFundClientPK = "#FundClientPK";
        htmlFundClientID = "#FundClientID";



    });

    function initListFundClientPK() {
        var dsListFundClient = getDataSourceListFundClient();
        $("#gridListFundClient").kendoGrid({
            dataSource: dsListFundClient,
            height: 400,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            pageable: true,
            columnMenu: false,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListFundClientSelect }, title: " ", width: 85 },
               { field: "FundClientPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "Client ID", width: 300 }

            ]
        });
    }

    function ListFundClientSelect(e) {
        var grid = $("#gridListFundClient").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $("#FundClientName").val(dataItemX.Name);
        $("#FundClientID").val(dataItemX.ID);
        $(htmlFundClientPK).val(dataItemX.FundClientPK);
        getBankRecipientComboByFundClientPK(dataItemX.FundClientPK);
        WinListFundClient.close();


    }



    $("#BtnInsertReguler").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/RegulerInstruction/GetAutoDebitDateFromRegulerInstruction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamAutoDebitDate").kendoComboBox({
                    dataValueField: "AutoDebitDate",
                    dataTextField: "AutoDebitDate",
                    dataSource: data,
                    change: OnChangeParamAutoDebitDate,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });
            },
            error: function (data) {
                alertify.alert("Please Fill Data Report Correctly");
            }
        });


        function OnChangeParamAutoDebitDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        WinListRegulerInstruction.center();
        WinListRegulerInstruction.open();
    });


 
    $("#BtnOkRegulerInstruction").click(function () {
        

        alertify.confirm("Are you sure want to Insert data Reguler Instruction ?", function (e) {
            if (e) {
                var RegulerInstruction = {
                    AutoDebitDate: $("#ParamAutoDebitDate").data("kendoComboBox").value(),
                    EntryUsersID: sessionStorage.getItem("user")
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/RegulerInstruction/GenerateDataRegulerInstructionToClientSubscription/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(RegulerInstruction),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        WinListRegulerInstruction.close();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });

    $("#BtnCancelRegulerInstruction").click(function () {
        
        alertify.confirm("Are you sure want to cancel Insert data Reguler Instruction ?", function (e) {
            if (e) {
                WinListRegulerInstruction.close();
                alertify.alert("Cancel Insert");
            }
        });
    });


    function onWinListRegulerInstructionClose() {
        $("#ParamAutoDebitDate").val("")

    }

    function onChangeGrossAmount() {
        
        $("#NetAmount").data("kendoNumericTextBox").value(($("#GrossAmount").data("kendoNumericTextBox").value()) / (1 + ($("#FeePercent").data("kendoNumericTextBox").value() / 100)));
        Recalculate();
    }
    function onChangeFeePercent() {
        $("#NetAmount").data("kendoNumericTextBox").value(($("#GrossAmount").data("kendoNumericTextBox").value()) / (1 + ($("#FeePercent").data("kendoNumericTextBox").value()/100)));
        Recalculate();
    }
    function onChangeFeeAmount() {
        $("#NetAmount").data("kendoNumericTextBox").value(($("#GrossAmount").data("kendoNumericTextBox").value()) - $("#FeeAmount").data("kendoNumericTextBox").value());
        $("#FeePercent").data("kendoNumericTextBox").value(($("#FeeAmount").data("kendoNumericTextBox").value()) / $("#NetAmount").data("kendoNumericTextBox").value() * 100);
    }

    function Recalculate() {
        $("#FeeAmount").data("kendoNumericTextBox").value(($("#GrossAmount").data("kendoNumericTextBox").value()) - $("#NetAmount").data("kendoNumericTextBox").value())
    }


    function getBankRecipientComboByFundClientPK(_fundClientPK) {
        $.ajax({
            url: window.location.origin + "/Radsoft/FundClient/GetBankRecipientComboByFundClientPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundClientPK,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#BankRecipientPK").kendoComboBox({
                    dataValueField: "BankRecipientPK",
                    dataTextField: "AccountNo",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeBankRecipient,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function onChangeBankRecipient() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

    }
});
