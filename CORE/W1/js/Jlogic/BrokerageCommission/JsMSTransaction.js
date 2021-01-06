$(document).ready(function () {
    document.title = 'FORM BC MASTER TRANSACTION';
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
        $("#BtnImportMSTransaction").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
        $("#BtnImportTransaction").kendoButton({
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
        

        win = $("#WinMSTransaction").kendoWindow({
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

        WinImportMSTransaction = $("#WinImportMSTransaction").kendoWindow({
            height: "520px",
            title: "Instrument List",
            visible: false,
            width: "570px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            //close: onWinImportMSTransactionClose
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

        WinImportTransaction = $("#WinImportTransaction").kendoWindow({
            height: 250,
            title: "Import Transaction",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            //close: onWinGetNavClose
        }).data("kendoWindow");



    }



    var GlobValidator = $("#WinMSTransaction").kendoValidator().data("kendoValidator");


    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#MSTransactionPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Date").data("kendoDatePicker").value(null);
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#MSTransactionValue").val("");
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
            var gridPending = $("#gridMSTransactionPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridMSTransactionHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {

        $("#gridMSTransactionApproved").empty();
        var MSTransactionApprovedURL = window.location.origin + "/Radsoft/BrokerageCommission/GetDataTransaction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            dataSourceApproved = getDataSource(MSTransactionApprovedURL);

        var grid = $("#gridMSTransactionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form BC MASTER TRANSACTION"
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
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'MM/dd/yyyy')#" },
                { field: "Sales", title: "Sales", width: 200 },
                { field: "No_Cust", title: "No_Cust", width: 140 },
                { field: "Name", title: "Name", width: 250 },
                { field: "RecBy", title: "RecBy", width: 100 },
                { field: "Buying", title: "Buying", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "Selling", title: "Selling", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "Netting", title: "Netting", width: 200, format: "{0:n6}", attributes: { style: "text-align:right;" } },
                { field: "Total_Trans", title: "Total_Trans", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Comm", title: "Comm", width: 200, attributes: { style: "text-align:right;" } },
                { field: "Vat", title: "Vat", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Levy", title: "Levy", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Other", title: "Other", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Pph", title: "Pph", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Rebate", title: "Rebate", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Net_Reb", title: "Net_Reb", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Expense", title: "Expense", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                
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


            var grid = $("#gridMSTransactionApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _MSTransactionPK = dataItemX.MSTransactionPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _MSTransactionPK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabMSTransaction").kendoTabStrip({
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

    function gridHistoryDataBound() {
        var grid = $("#gridMSTransactionHistory").data("kendoGrid");
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

    $("#BtnImportMSTransaction").click(function () {
        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
        });


        WinImportTransaction.center();
        WinImportTransaction.open();
    });

    $("#BtnImportTransaction").click(function () {
        document.getElementById("FileImportMSTransaction").click();
    });

    $("#FileImportMSTransaction").change(function () {
        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportMSTransaction").get(0).files;
        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("BC_MSTransaction", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/BrokerageCommission/UploadDataFourParams/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "MSTransaction_Import/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    //if (data == "false") {
                    $("#FileImportMSTransaction").val("");
                    $.unblockUI();
                    alertify.alert(data)
                    //}
                    //else {
                    //    $.unblockUI();
                    //    $("#FileImportMSTransaction").val("");
                    //    alertify.alert(data)
                    //}
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportMSTransaction").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportMSTransaction").val("");
        }
    });

});
