$(document).ready(function () {
    document.title = 'FORM Payable Card';
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

        win = $("#WinPayableCard").kendoWindow({
            height: 450,
            title: "Payable Card Detail",
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



    var GlobValidator = $("#WinPayableCard").kendoValidator().data("kendoValidator");

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

            $("#PayableCardPK").val(dataItemX.PayableCardPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ConsigneePK").val(dataItemX.ConsigneePK);
            $("#PoNo").val(dataItemX.PoNo);
            $("#Description").val(dataItemX.Description);
            $("#TotalAmount").val(dataItemX.TotalAmount);
            $("#TermDesc1").val(dataItemX.TermDesc1);
            $("#AmountTerm1").val(dataItemX.AmountTerm1);
            $("#TermDesc2").val(dataItemX.TermDesc2);
            $("#AmountTerm2").val(dataItemX.AmountTerm2);
            $("#TermDesc3").val(dataItemX.TermDesc3);
            $("#AmountTerm3").val(dataItemX.AmountTerm3);
            $("#TermDesc4").val(dataItemX.TermDesc4);
            $("#AmountTerm4").val(dataItemX.AmountTerm4);
            $("#TermDesc5").val(dataItemX.TermDesc5);
            $("#AmountTerm5").val(dataItemX.AmountTerm5);
            $("#TotalPaid").val(dataItemX.TotalPaid);
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
        //combo box ConsigneePK
        $.ajax({
            url: window.location.origin + "/Radsoft/Consignee/GetConsigneeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ConsigneePK").kendoComboBox({
                    dataValueField: "ConsigneePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeConsigneePK,
                    value: setCmbConsigneePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeConsigneePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbConsigneePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ConsigneePK == 0) {
                    return "";
                } else {
                    return dataItemX.ConsigneePK;
                }
            }
        }

        $("#TotalAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTotalAmount(),
            change: OnChangeTotalAmount

        });
        function setTotalAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.TotalAmount;
            }
        }

        function OnChangeTotalAmount() {
            $("#TotalPaid").data("kendoNumericTextBox").value($("#TotalAmount").data("kendoNumericTextBox").value() +
            $("#AmountTerm1").data("kendoNumericTextBox").value() + $("#AmountTerm2").data("kendoNumericTextBox").value() +
            $("#AmountTerm3").data("kendoNumericTextBox").value() + $("#AmountTerm4").data("kendoNumericTextBox").value() +
            $("#AmountTerm5").data("kendoNumericTextBox").value()
                );
        }

        $("#AmountTerm1").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setAmountTerm1(),
            change: OnChangeAmountTerm1
        });
        function setAmountTerm1() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AmountTerm1;
            }
        }

        function OnChangeAmountTerm1() {
            $("#TotalPaid").data("kendoNumericTextBox").value($("#TotalAmount").data("kendoNumericTextBox").value() +
            $("#AmountTerm1").data("kendoNumericTextBox").value() + $("#AmountTerm2").data("kendoNumericTextBox").value() +
            $("#AmountTerm3").data("kendoNumericTextBox").value() + $("#AmountTerm4").data("kendoNumericTextBox").value() +
            $("#AmountTerm5").data("kendoNumericTextBox").value()
                );
        }

        $("#AmountTerm2").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setAmountTerm2(),
            change: OnChangeAmountTerm2
        });
        function setAmountTerm2() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AmountTerm2;
            }
        }


        function OnChangeAmountTerm2() {
            $("#TotalPaid").data("kendoNumericTextBox").value($("#TotalAmount").data("kendoNumericTextBox").value() +
            $("#AmountTerm1").data("kendoNumericTextBox").value() + $("#AmountTerm2").data("kendoNumericTextBox").value() +
            $("#AmountTerm3").data("kendoNumericTextBox").value() + $("#AmountTerm4").data("kendoNumericTextBox").value() +
            $("#AmountTerm5").data("kendoNumericTextBox").value()
                );
        }

        $("#AmountTerm3").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setAmountTerm3(),
            change: OnChangeAmountTerm3
        });
        function setAmountTerm3() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AmountTerm3;
            }
        }

        function OnChangeAmountTerm3() {
            $("#TotalPaid").data("kendoNumericTextBox").value($("#TotalAmount").data("kendoNumericTextBox").value() +
            $("#AmountTerm1").data("kendoNumericTextBox").value() + $("#AmountTerm2").data("kendoNumericTextBox").value() +
            $("#AmountTerm3").data("kendoNumericTextBox").value() + $("#AmountTerm4").data("kendoNumericTextBox").value() +
            $("#AmountTerm5").data("kendoNumericTextBox").value()
                );
        }

        $("#AmountTerm4").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setAmountTerm4(),
            change: OnChangeAmountTerm4
        });
        function setAmountTerm4() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AmountTerm4;
            }
        }

        function OnChangeAmountTerm4() {
            $("#TotalPaid").data("kendoNumericTextBox").value($("#TotalAmount").data("kendoNumericTextBox").value() +
            $("#AmountTerm1").data("kendoNumericTextBox").value() + $("#AmountTerm2").data("kendoNumericTextBox").value() +
            $("#AmountTerm3").data("kendoNumericTextBox").value() + $("#AmountTerm4").data("kendoNumericTextBox").value() +
            $("#AmountTerm5").data("kendoNumericTextBox").value()
                );
        }

        $("#AmountTerm5").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setAmountTerm5(),
            change: OnChangeAmountTerm5
        });
        function setAmountTerm5() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.AmountTerm5;
            }
        }


        function OnChangeAmountTerm5() {
            $("#TotalPaid").data("kendoNumericTextBox").value($("#TotalAmount").data("kendoNumericTextBox").value() +
            $("#AmountTerm1").data("kendoNumericTextBox").value() + $("#AmountTerm2").data("kendoNumericTextBox").value() +
            $("#AmountTerm3").data("kendoNumericTextBox").value() + $("#AmountTerm4").data("kendoNumericTextBox").value() +
            $("#AmountTerm5").data("kendoNumericTextBox").value()
                );
        }
        $("#TotalPaid").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setTotalPaid()
        });
        function setTotalPaid() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.TotalPaid;
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
    function clearDataWhenAdd() {
        $("#gridReferenceDetail").empty();
        $("#Notes").val("");
        $("#TotalAmount").data("kendoNumericTextBox").value("");
        $("#AmountTerm1").data("kendoNumericTextBox").value("");
        $("#AmountTerm2").data("kendoNumericTextBox").value("");
        $("#AmountTerm3").data("kendoNumericTextBox").value("");
        $("#AmountTerm4").data("kendoNumericTextBox").value("");
        $("#AmountTerm5").data("kendoNumericTextBox").value("");
        $("#TotalPaid").data("kendoNumericTextBox").value("");
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
    function clearData() {
        $("#PayableCardPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ConsigneePK").val("");
        $("#PoNo").val("");
        $("#Description").val("");
        $("#TotalAmount").data("kendoNumericTextBox").value("");
        $("#TermDesc1").val("");
        $("#AmountTerm1").data("kendoNumericTextBox").value("");
        $("#TermDesc2").val("");
        $("#AmountTerm2").val("");
        $("#TermDesc3").val("");
        $("#AmountTerm3").val("");
        $("#TermDesc4").val("");
        $("#AmountTerm4").val("");
        $("#TermDesc5").val("");
        $("#AmountTerm5").val("");
        $("#TotalPaid").data("kendoNumericTextBox").value("");
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
                             PayableCardPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ConsigneePK: { type: "number" },
                             ConsigneeID: { type: "string" },
                             PoNo: { type: "string" },
                             Description: { type: "string" },
                             TotalAmount: { type: "number" },
                             TermDesc1: { type: "string" },
                             AmountTerm1: { type: "number" },
                             TermDesc2: { type: "string" },
                             AmountTerm2: { type: "number" },
                             TermDesc3: { type: "string" },
                             AmountTerm3: { type: "number" },
                             TermDesc4: { type: "string" },
                             AmountTerm4: { type: "number" },
                             TermDesc5: { type: "string" },
                             AmountTerm5: { type: "number" },
                             TotalPaid: { type: "number" },
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
            var gridApproved = $("#gridPayableCardApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridPayableCardPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridPayableCardHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var PayableCardApprovedURL = window.location.origin + "/Radsoft/PayableCard/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(PayableCardApprovedURL);

        $("#gridPayableCardApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Payable Card"
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
                { field: "PayableCardPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "ConsigneeID", title: "Consignee", width: 200 },
                { field: "PoNo", title: "PoNo", width: 200 },
                { field: "Description", title: "Description", width: 400 },
                { field: "TotalAmount", title: "TotalAmount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TermDesc1", title: "TermDesc1", width: 200 },
                { field: "AmountTerm1", title: "AmountTerm1", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TermDesc2", title: "TermDesc2", width: 200 },
                { field: "AmountTerm2", title: "AmountTerm2", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TermDesc3", title: "TermDesc3", width: 200 },
                { field: "AmountTerm3", title: "AmountTerm3", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TermDesc4", title: "TermDesc4", width: 200 },
                { field: "AmountTerm4", title: "AmountTerm4", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TermDesc5", title: "TermDesc5", width: 200 },
                { field: "AmountTerm5", title: "AmountTerm5", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "TotalPaid", title: "TotalPaid", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
        $("#TabPayableCard").kendoTabStrip({
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
                        var PayableCardPendingURL = window.location.origin + "/Radsoft/PayableCard/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(PayableCardPendingURL);
                        $("#gridPayableCardPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Payable Card"
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
                                { field: "PayableCardPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ConsigneeID", title: "Consignee", width: 200 },
                                { field: "PoNo", title: "PoNo", width: 200 },
                                { field: "Description", title: "Description", width: 400 },
                                { field: "TotalAmount", title: "TotalAmount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc1", title: "TermDesc1", width: 200 },
                                { field: "AmountTerm1", title: "AmountTerm1", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc2", title: "TermDesc2", width: 200 },
                                { field: "AmountTerm2", title: "AmountTerm2", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc3", title: "TermDesc3", width: 200 },
                                { field: "AmountTerm3", title: "AmountTerm3", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc4", title: "TermDesc4", width: 200 },
                                { field: "AmountTerm4", title: "AmountTerm4", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc5", title: "TermDesc5", width: 200 },
                                { field: "AmountTerm5", title: "AmountTerm5", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TotalPaid", title: "TotalPaid", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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

                        var PayableCardHistoryURL = window.location.origin + "/Radsoft/PayableCard/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(PayableCardHistoryURL);

                        $("#gridPayableCardHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Payable Card"
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
                                { field: "PayableCardPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "ConsigneeID", title: "Consignee", width: 200 },
                                { field: "PoNo", title: "PoNo", width: 200 },
                                { field: "Description", title: "Description", width: 400 },
                                { field: "TotalAmount", title: "TotalAmount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc1", title: "TermDesc1", width: 200 },
                                { field: "AmountTerm1", title: "AmountTerm1", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc2", title: "TermDesc2", width: 200 },
                                { field: "AmountTerm2", title: "AmountTerm2", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc3", title: "TermDesc3", width: 200 },
                                { field: "AmountTerm3", title: "AmountTerm3", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc4", title: "TermDesc4", width: 200 },
                                { field: "AmountTerm4", title: "AmountTerm4", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TermDesc5", title: "TermDesc5", width: 200 },
                                { field: "AmountTerm5", title: "AmountTerm5", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                { field: "TotalPaid", title: "TotalPaid", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
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
        var grid = $("#gridPayableCardHistory").data("kendoGrid");
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
    function recalAmount() {
        var _TotalPaid = $("#TotalAmount").data("kendoNumericTextBox").value() * ($("#AmountTerm1").data("kendoNumericTextBox").value());
        $("#TotalPaid").data("kendoNumericTextBox").value(_TotalPaid);

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
                    var PayableCard = {
                        ConsigneePK: $('#ConsigneePK').val(),
                        PoNo: $('#PoNo').val(),
                        Description: $('#Description').val(),
                        TotalAmount: $('#TotalAmount').val(),
                        TermDesc1: $('#TermDesc1').val(),
                        AmountTerm1: $('#AmountTerm1').val(),
                        TermDesc2: $('#TermDesc2').val(),
                        AmountTerm2: $('#AmountTerm2').val(),
                        TermDesc3: $('#TermDesc3').val(),
                        AmountTerm3: $('#AmountTerm3').val(),
                        TermDesc4: $('#TermDesc4').val(),
                        AmountTerm4: $('#AmountTerm4').val(),
                        TermDesc5: $('#TermDesc5').val(),
                        AmountTerm5: $('#AmountTerm5').val(),
                        TotalPaid: $('#TotalPaid').val(),
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/PayableCard/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PayableCard_I",
                        type: 'POST',
                        data: JSON.stringify(PayableCard),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#PayableCardPK").val() + "/" + $("#HistoryPK").val() + "/" + "PayableCard",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var PayableCard = {
                                    PayableCardPK: $('#PayableCardPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ConsigneePK: $('#ConsigneePK').val(),
                                    PoNo: $('#PoNo').val(),
                                    Description: $('#Description').val(),
                                    TotalAmount: $('#TotalAmount').val(),
                                    TermDesc1: $('#TermDesc1').val(),
                                    AmountTerm1: $('#AmountTerm1').val(),
                                    TermDesc2: $('#TermDesc2').val(),
                                    AmountTerm2: $('#AmountTerm2').val(),
                                    TermDesc3: $('#TermDesc3').val(),
                                    AmountTerm3: $('#AmountTerm3').val(),
                                    TermDesc4: $('#TermDesc4').val(),
                                    AmountTerm4: $('#AmountTerm4').val(),
                                    TermDesc5: $('#TermDesc5').val(),
                                    AmountTerm5: $('#AmountTerm5').val(),
                                    TotalPaid: $('#TotalPaid').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/PayableCard/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PayableCard_U",
                                    type: 'POST',
                                    data: JSON.stringify(PayableCard),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#PayableCardPK").val() + "/" + $("#HistoryPK").val() + "/" + "PayableCard",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "PayableCard" + "/" + $("#PayableCardPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#PayableCardPK").val() + "/" + $("#HistoryPK").val() + "/" + "PayableCard",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var PayableCard = {
                                PayableCardPK: $('#PayableCardPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/PayableCard/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PayableCard_A",
                                type: 'POST',
                                data: JSON.stringify(PayableCard),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#PayableCardPK").val() + "/" + $("#HistoryPK").val() + "/" + "PayableCard",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var PayableCard = {
                                PayableCardPK: $('#PayableCardPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/PayableCard/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PayableCard_V",
                                type: 'POST',
                                data: JSON.stringify(PayableCard),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#PayableCardPK").val() + "/" + $("#HistoryPK").val() + "/" + "PayableCard",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var PayableCard = {
                                PayableCardPK: $('#PayableCardPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/PayableCard/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "PayableCard_R",
                                type: 'POST',
                                data: JSON.stringify(PayableCard),
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



    function TotalPaid()
    {
        var TotalPaid = {
            TotalAmount: $('#TotalAmount').val(),
            AmountTerm1: $('#AmountTerm1').val(),
            AmountTerm2: $('#AmountTerm2').val(),
            AmountTerm3: $('#AmountTerm3').val(),
            AmountTerm4: $('#AmountTerm4').val(),
            AmountTerm5: $('#AmountTerm5').val()
        };
        $.ajax({
                url: window.location.origin + "/Radsoft/PayableCard/TotalPaid/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (GetCustomerHistory) {
                    $('#TotalPaid').val(GetCustomerHistory.TotalPaid)
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        
    }
});
