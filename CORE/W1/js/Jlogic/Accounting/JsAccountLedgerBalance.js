$(document).ready(function () {
    var win;
    var winTB;


    initWindow();
    initGrid();

    function initWindow() {
        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeValueDate
        });

        function OnChangeValueDate() {
            refresh();
        }

        $("#ParamValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamValueDateFrom
        });
        $("#ParamValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamValueDateTo
        });

        function OnChangeParamValueDateFrom() {

            var currentDate = Date.parse($("#ParamValueDateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#ParamValueDateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#ParamValueDateFrom").data("kendoDatePicker").value() != null) {
                $("#ParamValueDateTo").data("kendoDatePicker").value($("#ParamValueDateFrom").data("kendoDatePicker").value());
            }
            refreshActivity();
        }
        function OnChangeParamValueDateTo() {

            var currentDate = Date.parse($("#ParamValueDateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#ParamValueDateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refreshActivity();
        }

        $("#Status").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "POSTED ONLY", value: 1 },
               { text: "REVISED ONLY", value: 2 },
               { text: "APPROVED ONLY", value: 3 },
               { text: "PENDING ONLY", value: 4 },
               { text: "HISTORY ONLY", value: 5 },
               { text: "POSTED & APPROVED", value: 6 },
               { text: "POSTED & APPROVED & PENDING", value: 7 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatus,
            index: 6
        });

        function OnChangeStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else
            {
                refresh();
            }

        }

      


        winTB = $("#WinTB").kendoWindow({
            height: 2000,
            title: "TRIAL BALANCE",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");


        winAccountActivity = $("#winAccountActivity").kendoWindow({
            height: 1300,
            title: "* Account Activity",
            visible: false,
            width: 1000,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

    }

    

    function refresh()
    {
        var newDS = getDataTrialBalance();
        $("#gridTB").data("kendoGrid").setDataSource(newDS);
    }

    function refreshActivity() {
        var _prevDate = $("#ParamValueDateFrom").data("kendoDatePicker").value();
        _getStartBalance(_prevDate, $("#ParamAccountID").val(), $("#ParamAccountName").val());
        var newDS = getDataAccountActivity(kendo.toString(_prevDate, "MM-dd-yy"), kendo.toString($("#ParamValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"), $("#ParamAccountID").val(), $("#ParamAccountName").val(), $("#ParamStatus").data("kendoComboBox").value());
        $("#gridAccountActivity").data("kendoGrid").setDataSource(newDS);

    }


    function gridTBOndataBound(e) {
        var grid = $("#gridTB").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.BitIsGroups == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("TBReconcileHeader");
            } else if (row.BitIsChange == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("TBReconcileHeader");
            }
        });
    }
    
    function initGrid()
    {
        var dsTrialBalance = getDataTrialBalance();
        $("#gridTB").empty();
        $("#gridTB").kendoGrid({
            dataSource: dsTrialBalance,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            editable: "incell",
            dataBound: gridTBOndataBound,
            groupable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            toolbar: ["excel"],
            columns: [
               { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
               { field: "BitIsGroups", title: "IsGroup", hidden: true, width: 50 },
               { field: "BitIsChange", title: "IsChange", hidden: true, width: 50 },
               { field: "Header", title: "Header", width: 150 },
               { field: "ID", title: "ID", hidden: true, width: 100 },
               { field: "Name", title: "Name", width: 150 },
               { field: "ParentName", title: "Parent Name",hidden:true, width: 200 },
               {
                   field: "PreviousBaseBalance", title: "Prev Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:right;" }
               },
               {
                   field: "Movement", title: "Movement", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:right;" }
               },
               {
                   field: "CurrentBaseBalance", title: "End Balance", width: 150, format: "{0:n2}", footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , groupFooterTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n2')#</div>"
                        , headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:right;" }
               },

            ]
        });
        $("#lblDate").text(kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "dd-MMM-yy"));
        winTB.center();
        winTB.open();
    }


    function getDataTrialBalance() {
        return new kendo.data.DataSource(
                 {
                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/AccountLedgerBalance/GetTrialBalance/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#Status").data("kendoComboBox").value(),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     batch: true,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 10000,
                     schema: {
                         model: {
                             fields: {
                                 Header: { type: "string", editable: false },
                                 ID: { type: "string", editable: false },
                                 Name: { type: "string", editable: false },
                                 ParentName: { type: "string", editable: false },
                                 PreviousBaseBalance: { editable: false },
                                 Movement: { editable: false },
                                 CurrentBaseBalance: { editable: false },
                                 BitIsChange: { type: "boolean" },
                     
                             }
                         }
                     },

                     aggregate: [{ field: "PreviousBaseBalance", aggregate: "sum" },
                             { field: "Movement", aggregate: "sum" },
                             { field: "CurrentBaseBalance", aggregate: "sum" },

                     ]
                 });
    }

   

    function showDetails(e) {
        $("#ParamStartBalance").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
        });

        $("#ParamStatus").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
               { text: "POSTED ONLY", value: 1 },
               { text: "REVISED ONLY", value: 2 },
               { text: "APPROVED ONLY", value: 3 },
               { text: "PENDING ONLY", value: 4 },
               { text: "HISTORY ONLY", value: 5 },
               { text: "POSTED & APPROVED", value: 6 },
               { text: "POSTED & APPROVED & PENDING", value: 7 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeStatus,
            index: 6
        });

        function OnChangeStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else {
                refreshActivity();
            }

        }

        var date = $("#ValueDate").data("kendoDatePicker").value(), y = date.getFullYear(), m = date.getMonth();
        var _prevDate = new Date(y, m, 0);

        var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
        var dsAccountActivity = getDataAccountActivity(kendo.toString(_prevDate, "dd-MMM-yy"), kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "dd-MMM-yy"), dataItemX.ID, dataItemX.Name, $("#ParamStatus").data("kendoComboBox").value());
        $("#gridAccountActivity").empty();
        $("#gridAccountActivity").kendoGrid({
            dataSource: dsAccountActivity,
            height: "90%",
            scrollable: {
                virtual: true
            },
            resizable: true,
            dataBound: gridTBOndataBound,
            columns: [
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "ID", title: "ID", hidden: true, width: 100 },
                { field: "Name", title: "Name", hidden: true, width: 150 },
                { field: "Description", title: "Description", width: 350 },
                {
                    field: "Debit", title: "Debit", width: 150, format: "{0:n2}"
                        , headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:right;" }
                },
                {
                    field: "Credit", title: "Credit", width: 150, format: "{0:n2}"
                        , headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:right;" }
                },
                {
                    field: "Balance", title: "Balance", width: 150, format: "{0:n2}"
                        , headerAttributes: {
                            style: "text-align: center"
                        }, attributes: { style: "text-align:right;" }
                },

            ]
        });

        _getStartBalance(_prevDate, dataItemX.ID);
       
        


        winAccountActivity.center();
        winAccountActivity.open();
    }

    function getDataAccountActivity(_valueDateFrom, _valueDateTo, _ID, _Name, _status) {
        
        $("#ParamAccountID").val(_ID);
        $("#ParamAccountName").val(_Name);
        $("#ParamValueDateFrom").data("kendoDatePicker").value(new Date(_valueDateFrom));
        $("#ParamValueDateTo").data("kendoDatePicker").value(new Date(_valueDateTo));
        return new kendo.data.DataSource(
                 {
                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/AccountLedgerBalance/GetDataAccountActivity/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _valueDateFrom + "/" + _valueDateTo + "/" + _ID + "/" + _status,
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     batch: true,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 10000,
                     schema: {
                         model: {
                             fields: {
                                 ID: { type: "string", editable: false },
                                 Name: { type: "string", editable: false },
                                 Description: { type: "string", editable: false },
                                 Debit: { editable: false },
                                 Credit: { editable: false },
                                 Balance: { editable: false },

                             }
                         }
                     },

                 });
    }

    function _getStartBalance(_prevDate, _id) {
        $("#ParamStartBalance").data("kendoNumericTextBox").value("0");
        $.ajax({
            url: window.location.origin + "/Radsoft/Journal/GetStartBalanceByAccountPKByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString(_prevDate, "dd-MMM-yy") + "/" + _id,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamStartBalance").data("kendoNumericTextBox").value(data);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }

});