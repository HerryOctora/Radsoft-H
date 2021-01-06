$(document).ready(function () {
    document.title = 'FORM Interface Journal SAP';
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

    ResetSelectedSAP();


    function initButton() {
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnGetData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnPostingAmortizeBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        
        $("#BtnReportLPTI").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });
        $("#BtnOkReportLPTI").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelReportLPTI").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnAmortize").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnCheckMonthly").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnPreview").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnSendToSAP").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
    }

    function initWindow() {

        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom,
            value: new Date(),
        });

        $("#PostingDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            //change: OnChangeDateFrom,
            value: new Date(),
        });

        $("#PostingDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            //change: OnChangeDateFrom,
            value: new Date(),
        });

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterFundID").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeFilterFundID,
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }


        });
        function onChangeFilterFundID() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else
            {
                refresh();
            }
     
        }


        win = $("#WinInterfaceJournalSAP").kendoWindow({
            height: 225,
            title: "SAP Master Account Detail",
            visible: false,
            width: 700,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 100 })
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

        WinDetailData = $("#WinDetailData").kendoWindow({
            height: 500,
            title: "Detail Data",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 55 })
            },
            close: onPopUpDetailDataClose
        }).data("kendoWindow");

        WinReportLPTI = $("#WinReportLPTI").kendoWindow({
            height: 150,
            title: "* Report LPTI",
            visible: false,
            width: 300,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

        
        WinAmortize = $("#WinAmortize").kendoWindow({
            height: 600,
            title: "* Amortize / Interest / Reval",
            visible: false,
            width: 1300,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinAmortizeClose
        }).data("kendoWindow");

        WinPostingAmortize = $("#WinPostingAmortize").kendoWindow({
            height: 300,
            title: "* Posting ",
            visible: false,
            width: 500,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            //close: onWinAmortizeClose
        }).data("kendoWindow");



        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                //$("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }
            refresh();
        }

    }


    $("#BtnRefresh").click(function () {
        refresh();
    });

    var GlobValidator = $("#WinInterfaceJournalSAP").kendoValidator().data("kendoValidator");

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
        //taro grid disini
        var grid;
        if ($("#WinAmortize").is(':visible') == false) {
            grid = $("#gridInterfaceJournalSAPApproved").data("kendoGrid");        
        }
        else
        {
            grid = $("#gridAmortize").data("kendoGrid");
        }


        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        initGridDetailData(dataItemX.FundJournalPK,dataItemX.FundJournalType, dataItemX.FundPK, dataItemX.Description)
        WinDetailData.center();
        WinDetailData.open();
    }

    function onPopUpClose() {
        refreshDetailData();
    }



    function onPopUpDetailDataClose() {
        //refresh();
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
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             FundJournalPK: { type: "number" },
                             InvestmentPK: { type: "number" },
                             Date: { type: "date" },
                             FundJournalDate: { type: "date" },
                             BuySell: { type: "string" },
                             FundJournalType: { type: "string" },
                             Description: { type: "string" },
                             JournalReference: { type: "string" },
                             DocFrom: { type: "string" },
                         }
                     }
                 }
             });
    }

    //function refresh() {
    //    initGrid();
    //}

    function initGrid() {
        $("#gridInterfaceJournalSAPApproved").empty();
        if ($("#FilterFundID").val() == "" || $("#FilterFundID").val() == "ALL") {
            _fundID = "0";
        }
        else {
            _fundID = $("#FilterFundID").val();
        }
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var InterfaceJournalSAPApprovedURL = window.location.origin + "/Radsoft/SAPMaster/GetDataInterfaceJournalSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _fundID,
          dataSourceApproved = getDataSource(InterfaceJournalSAPApprovedURL);
        }

        

        var grid = $("#gridInterfaceJournalSAPApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Interface Journal SAP"
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
            dataBound: gridApprovedInterfaceOnDataBound,
            toolbar: ["excel"],
            columns: [
                { command: { text: "Show Versi SAP", click: showDetails }, title: "Versi SAP", width: 100 },
                {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
                },
                { field: "FundPK", hidden : true ,title: "FundPK", width: 150 },
                { field: "FundID", title: "Fund ID", width: 150 },
                { field: "FundJournalPK",hidden : true, title: "Journal No.", width: 95 },
                { field: "InvestmentPK", title: "Trx No.", width: 95 },
                { field: "Date", title: "Trx Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundJournalDate", title: "Journal Date", width: 150, template: "#= kendo.toString(kendo.parseDate(FundJournalDate), 'dd/MMM/yyyy')#" },
                { field: "BuySell", title: "Type", width: 150 },
                { field: "FundJournalType", hidden: true, title: "FundJournalType", width: 150 },
                { field: "Description", title: "Description", width: 200 },
                { field: "JournalReference", title: "Journal Reference", width: 120 },
                { field: "DocFrom", title: "Doc From", width: 100 },
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


            var grid = $("#gridInterfaceJournalSAPApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _investmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _investmentPK);

        }
        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/SAPMaster/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var _msg;
                    if (_a) {
                        _msg = "Select Data ";
                    } else {
                        _msg = "DeSelect Data ";
                    }
                    alertify.set('notifier', 'position', 'top-center'); alertify.success(_msg + _b);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

        function SelectDeselectAllData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/SAPMaster/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $(".cSelectedDetail" + _b).prop('checked', _a);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }      

    }

    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };


    function refreshDetailData() {
        var gridDetailData = $("#gridDetailInterfaceJournalSAP").data("kendoGrid");
        gridDetailData.dataSource.read();
    }


    function getDataSourceDetail(_url) {
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
                 pageSize: 50,
                 aggregate: [
                     { field: "BaseDebit", aggregate: "sum" },
                     { field: "BaseCredit", aggregate: "sum" }],

                 schema: {
                     model: {
                         fields: {
                             FundJournalPK: { type: "number" },
                             AutoNo: { type: "number" },
                             AccountID: { type: "string" },
                             AccountName: { type: "string" },
                             DetailDescription: { type: "string" },
                             BaseDebit: { type: "number" },
                             BaseCredit: { type: "number" },

                         }
                     }
                 }
             });
    }

    function initGridDetailData(_fundjournalPK, _fundjournalType, _fundPK, _description) {

        if (_fundjournalType == "TRANSACTION" || _fundjournalType == "REC COUPON" || _fundjournalType == "REC COUPON DEPOSITO" || _fundjournalType == "ADJUSTMENT" || _fundjournalType == "REC COUPON EBA")
        {
            _description = "0";
        }


        $("#gridDetailInterfaceJournalSAP").empty();
        var InterfaceJournalSAPApprovedURL = window.location.origin + "/Radsoft/SAPMaster/GetDataDetailInterfaceJournalSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundjournalPK + "/" + _fundjournalType + "/" + _fundPK + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _description,
          dataSourceApproved = getDataSourceDetail(InterfaceJournalSAPApprovedURL);

        $("#gridDetailInterfaceJournalSAP").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form InterfaceJournalSAP"
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
                { field: "FundJournalPK", title: "FundJournalPK",hidden: true, width: 45 },
                { field: "AutoNo", title: "AutoNo", width: 100 },
                { field: "AccountID", title: "AccountID", width: 150 },
                { field: "AccountName", title: "AccountName", width: 200 },
                { field: "DetailDescription", title: "Description", width: 300 },
                { field: "BaseDebit", title: "Base Debit", width: 150, format: "{0:n2}", footerTemplate: "<div id='sumBaseDebit' style='text-align: right'>#= kendo.toString(sum, 'n2') #</div>", attributes: { style: "text-align:right;" } },
                { field: "BaseCredit", title: "Base Credit", width: 150, format: "{0:n2}", footerTemplate: "<div id='sumBaseCredit' style='text-align: right'>#= kendo.toString(sum, 'n2') #</div>", attributes: { style: "text-align:right;" } },
            ]
        });

    }


    $("#BtnPostingBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                $.blockUI();
                $.ajax({
                    url: window.location.origin + "/Radsoft/SAPMaster/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy")+ "/TRANSACTION",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
      

                        alertify.alert(data);
                        ResetSelectedSAP();
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });

            }
        });
    });


    $("#BtnCheckMonthly").click(function (e) {
        showWinCheckMonthly();

        
    });

    function showWinCheckMonthly() {
   

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PostingParamFundID").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamFundID,
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }


        });
        function onChangeParamFundID() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                refreshAmortize();
            }
        }

        $("#PostingParamJournalType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "ALL", value: '1' },
               { text: "ADJUSTMENT", value: '2' },
               { text: "AMORTIZE", value: '3' },
               { text: "DIVIDEND", value: '4' },
               { text: "INTEREST BOND", value: '5' },
               { text: "INTEREST DEPOSIT", value: '6' },
               { text: "INTEREST EBA", value: '12' },
               { text: "INTEREST FUND", value: '7' },
               { text: "MATURE BANK", value: '8' },
               { text: "PORTFOLIO REVALUATION", value: '9' },
               { text: "REC COUPON", value: '10' },
               { text: "REC COUPON DEPOSITO", value: '11' },
               { text: "REC COUPON EBA", value: '13' },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeParamJournalType,
            index: 0
        });
        function OnChangeParamJournalType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }



        WinPostingAmortize.center();
        WinPostingAmortize.open();
    }


    $("#BtnPreview").click(function (e) {
        if (e) {
            $.blockUI();
            var Preview = {
                DateFrom: $('#PostingDateFrom').val(),
                DateTo: $('#PostingDateTo').val(),
                FundJournalType: $("#PostingParamJournalType").data("kendoComboBox").text(),
                FundID: $('#PostingParamFundID').val(),
            };
            $.ajax({
                url: window.location.origin + "/Radsoft/SAPMaster/PreviewInterfaceJournalInterestSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(Preview),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var newwindow = window.open(data, '_blank');
                    $.unblockUI();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });

        }


    });

    $("#BtnSendToSAP").click(function (e) {
        alertify.confirm("Are you sure want to Send SAP?", function (e) {
            if (e) {
                $.blockUI();
                $.ajax({
                    url: window.location.origin + "/Radsoft/SAPMaster/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#ParamJournalType").data("kendoComboBox").text(),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();


                        alertify.alert(data);
                        ResetSelectedSAP();
                        refreshAmortize();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });

            }
        });



    });




    $("#BtnReportLPTI").click(function () {
        showReportLPTI();
    });

    function showReportLPTI(e) {

        $("#DownloadMode").kendoComboBox({
            dataValueField: "text",
            dataTextField: "text",
            dataSource: [
               { text: "Excel" },
               { text: "PDF" },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeDownloadMode,
            index: 0
        });
        function OnChangeDownloadMode() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        WinReportLPTI.center();
        WinReportLPTI.open();

    }

    $("#BtnOkReportLPTI").click(function () {
        alertify.confirm("Are you sure want to Download Report LPTI data ?", function (e) {
            if (e) {
                $.blockUI({});
                var ReportLPTI = {
                    ParamListDate: $('#DateFrom').val(),
                    DownloadMode: $('#DownloadMode').val(),
                };

                $.ajax({
                    url: window.location.origin + "/Radsoft/SAPMaster/ReportLPTIBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(ReportLPTI),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        var newwindow = window.open(data, '_blank');
                        $.unblockUI();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });

            }
        });

    });

    $("#BtnCancelReportLPTI").click(function () {

        alertify.confirm("Are you sure want to cancel Download?", function (e) {
            if (e) {
                WinReportLPTI.close();
                alertify.alert("Cancel Download");
            }
        });
    });





    $("#BtnAmortize").click(function () {
        showWinAmortize();
    });

    function showWinAmortize(e) {

        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFundID").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamFundID,
                    index: 0,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }


        });
        function onChangeParamFundID() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else
            {
                refreshAmortize();
            }
        }

        $("#ParamJournalType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "ALL", value: '1' },
               { text: "ADJUSTMENT", value: '2' },
               { text: "AMORTIZE", value: '3' },
               { text: "DIVIDEND", value: '4' },
               { text: "INTEREST BOND", value: '5' },
               { text: "INTEREST DEPOSIT", value: '6' },
               { text: "INTEREST EBA", value: '12' },
               { text: "INTEREST FUND", value: '7' },
               { text: "MATURE BANK", value: '8' },
               { text: "PORTFOLIO REVALUATION", value: '9' },
               { text: "REC COUPON", value: '10' },
               { text: "REC COUPON DEPOSITO", value: '11' },
               { text: "REC COUPON EBA", value: '13' },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeParamJournalType,
            index: 0
        });
        function OnChangeParamJournalType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else
            {
                refreshAmortize();
            }
           
        }
        ShowAmortize();

        WinAmortize.center();
        WinAmortize.open();

    }


    function ShowAmortize() {
        $("#gridAmortize").empty();
        var dsListAmortize = InitDataSourceAmortize();
        var gridAmortize = $("#gridAmortize").kendoGrid({
            dataSource: dsListAmortize,
            height: 800,
            scrollable: {
                virtual: true
            },
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columnMenu: false,
            reorderable: true,
            sortable: true,
            dataBound: gridApprovedAmortizeOnDataBound,
            resizable: true,
            columns: [
                { command: { text: "Show Versi SAP", click: showDetails }, title: "Versi SAP", width: 100 },
                {
                    field: "Selected",
                    width: 30,
                    template: "<input class='cSelectedDetailAmortize' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedDetailAmortize' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "FundPK", hidden : true ,title: "FundPK", width: 150 },
               { field: "FundID", title: "Fund ID", width: 150 },
               { field: "FundJournalPK",hidden : true, title: "Journal No.", width: 100 },
               { field: "Date", title: "Value Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
               { field: "FundJournalType", title: "Journal Type", width: 120 },
               { field: "Description", title: "Description", width: 150 },
               { field: "DOCSAP", title: "DOC SAP", width: 120 },
                 
            ]
        }).data("kendoGrid");

        $("#SelectedDetailAmortize").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }


            SelectDeselectAllDataAmortize(_checked);

        });

        gridAmortize.table.on("click", ".cSelectedDetailAmortize", selectDataAmortize);

        function selectDataAmortize(e) {


            var gridAmortize = $("#gridAmortize").data("kendoGrid");
            var dataItemX = gridAmortize.dataItem($(e.currentTarget).closest("tr"));
            var _journalPK = dataItemX.FundJournalPK;
            var _fundJournalType = dataItemX.FundJournalType;
            var _description = dataItemX.Description;
            var _checked = this.checked;
            SelectDeselectDataAmortize(_checked, _journalPK, _fundJournalType, _description);

        }



    }

    function SelectDeselectDataAmortize(_a, _b, _c, _d) {
        if (_c == "REC COUPON" || _c == "ADJUSTMENT" || _c == "REC COUPON DEPOSITO" || _c == "REC COUPON EBA") {
            _d = "0";
        }
            $.ajax({
                url: window.location.origin + "/Radsoft/SAPMaster/SelectDeselectDataAmortize/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + _c + "/" + _d + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var _msg;
                    if (_a) {
                        _msg = "Select Data ";
                    } else {
                        _msg = "DeSelect Data ";
                    }
                    alertify.set('notifier', 'position', 'top-center'); alertify.success(_msg + _b);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }
        function SelectDeselectAllDataAmortize(_a) {
            $.ajax({
                url: window.location.origin + "/Radsoft/SAPMaster/SelectDeselectAllDataByDateAmortize/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#ParamJournalType").data("kendoComboBox").text(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $(".cSelectedDetailPTP").prop('checked', _a);
                    refreshAmortize();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

        function refreshAmortize() {
            if ($("#ParamJournalType").data("kendoComboBox").text() == "" || $("#ParamJournalType").data("kendoComboBox").text() == "ALL") {
                _journalType = "0";
            }
            else {
                _journalType = $("#ParamJournalType").data("kendoComboBox").text();
            }
            if ($("#ParamFundID").val() == "" || $("#ParamFundID").val() == "ALL") {
                _fundID = "0";
            }
            else {
                _fundID = $("#ParamFundID").val();
            }
            var newDSAmortize = getDataSourceAmortize(window.location.origin + "/Radsoft/SAPMaster/GetDataAmortizeInterfaceSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _journalType + "/" + _fundID);
            $("#gridAmortize").data("kendoGrid").setDataSource(newDSAmortize);


        }


        // Untuk List Amortize

        function InitDataSourceAmortize() {

            return new kendo.data.DataSource(

                      {

                          transport:
                                  {

                                      read:
                                          {

                                              url: window.location.origin + "/Radsoft/SAPMaster/InitAmortizeInterfaceSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                              dataType: "json"
                                          }
                                  },
                          batch: true,
                          cache: false,
                          error: function (e) {
                              alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                              this.cancelChanges();
                          },
                          pageSize: 25,
                          schema: {
                              model: {
                                  fields: {
                                      Selected: { type: "boolean" },
                                      FundID: { type: "string" },
                                      FundJournalPK: { type: "number" },
                                      ValueDate: { type: "string" },
                                      JournalType: { type: "string" },
                                      Description: { type: "string" },
                                      DOCSAP: { type: "string" }

                                  }
                              }
                          }
                      });
        }


        function getDataSourceAmortize() {

            return new kendo.data.DataSource(

                      {

                          transport:
                                  {

                                      read:
                                          {

                                              url: window.location.origin + "/Radsoft/SAPMaster/GetDataAmortizeInterfaceSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _journalType + "/" + _fundID,
                                              dataType: "json"
                                          }
                                  },
                          batch: true,
                          cache: false,
                          error: function (e) {
                              alertify.alert(e.errorThrown + " - " + e.xhr.responseText);
                              this.cancelChanges();
                          },
                          pageSize: 25,
                          schema: {
                              model: {
                                  fields: {
                                      Selected: { type: "boolean" },
                                      FundID: { type: "string" },
                                      FundJournalPK: { type: "number" },
                                      ValueDate: { type: "string" },
                                      JournalType: { type: "string" },
                                      Description: { type: "string" },
                                      DOCSAP: { type: "string" }

                                  }
                              }
                          }
                      });
        }



    function onWinAmortizeClose() {
        $("#ParamJournalType").val(1);
    }



    function ResetSelectedSAP() {
        $.ajax({
            url: window.location.origin + "/Radsoft/SAPMaster/ResetSelectedSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

    }


    function gridApprovedInterfaceOnDataBound() {
        var grid = $("#gridInterfaceJournalSAPApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.DocFrom == "") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            }  else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }

    function gridApprovedAmortizeOnDataBound() {
        var grid = $("#gridAmortize").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.DOCSAP == "") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
    }

});
