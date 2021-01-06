$(document).ready(function () {

    var win;
    var GlobValidator = $("#WinManageInvestment").kendoValidator().data("kendoValidator");
    var checkedIds = {};
    var checkedName = {};
    var button = $("#ShowGrid").data("kendoButton");
    var tabindex;

    var gridHeight = screen.height - 300;


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


    initWindow();



    //function LoadData() {
    //    //DataSource definition
    //    dataSourcess = new kendo.data.DataSource({
    //        transport: {
    //            read: {
    //                url: window.location.origin + "/Radsoft/FundClient/GetFundClientComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                dataType: "json"

    //            }

    //        },
    //        batch: true,
    //        //cache: false,
    //        error: function (e) {
    //            alert(e.errorThrown + " - " + e.xhr.responseText);
    //            this.cancelChanges();
    //        },
    //        pageSize: 10,
    //        schema: {
    //            model: {
    //                fields: {
    //                    FundClientPK: { type: "number" },
    //                    ID: { type: "string" },
    //                    Name: { type: "string" },
    //                    IFUA: { type: "string" },
    //                    SID: { type: "string" },

    //                }
    //            }
    //        }
    //    });



    //    //Grid definition
    //    var gridFundClient = $("#gridFundClient").kendoGrid({
    //        dataSource: dataSourcess,
    //        pageable: true,
    //        height: 430,
    //        width: 350,
    //        //define dataBound event handler
    //        dataBound: onDataBound,
    //        sortable: true,
    //        filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
    //        columns: [
    //        //define template column with checkbox and attach click event handler
    //        { template: "<input type='checkbox' class='checkbox' />" }, {
    //            field: "ID",
    //            title: "Client ID",
    //            width: "120px"
    //        }, {
    //            field: "Name",
    //            title: "Client Name",
    //            width: "300px"
    //        }, {
    //            field: "IFUA",
    //            title: "IFUA Code",
    //            width: "185px"
    //        }, {
    //            field: "SID",
    //            title: "SID",
    //            width: "150px"
    //        }
    //        ],
    //        editable: "inline"
    //    }).data("kendoGrid");

    //    //bind click event to the checkbox
    //    gridFundClient.table.on("click", ".checkbox", selectRow);


    //    //on click of the checkbox:
    //    function selectRow() {
    //        var checked = this.checked,
    //        rowA = $(this).closest("tr"),
    //        gridFundClient = $("#gridFundClient").data("kendoGrid"),
    //        dataItemZ = gridFundClient.dataItem(rowA);

    //        checkedIds[dataItemZ.FundClientPK] = checked;
    //        checkedName[dataItemZ.Name] = checked;
    //        if (checked) {
    //            //-select the row
    //            rowA.addClass("k-state-selected");



    //        } else {
    //            //-remove selection
    //            rowA.removeClass("k-state-selected");
    //        }
    //    }

    //    //on dataBound event restore previous selected rows:
    //    function onDataBound(e) {
    //        var view = this.dataSource.view();
    //        for (var i = 0; i < view.length; i++) {
    //            if (checkedIds[view[i].FundClientPK]) {
    //                this.tbody.find("tr[data-uid='" + view[i].uid + "']")
    //                .addClass("k-state-selected")
    //                .find(".checkbox")
    //                .attr("checked", "checked");
    //            }
    //            if (checkedName[view[i].Name]) {
    //                this.tbody.find("tr[data-uid='" + view[i].uid + "']")
    //                .addClass("k-state-selected")
    //                .find(".checkbox")
    //                .attr("checked", "checked");
    //            }
    //        }
    //    }
    //}

    function HideParameter() {
        $("#LblDateFrom").hide();
    }
    function initWindow() {


        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#ShowGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#CloseGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnGetSysNo").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnUpdateSettledDate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });


        $("#ValueDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateFrom
        });

        $("#ValueDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
                $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
            }
        }


        $("#ParamSettledDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            change: onChangeParamSettledDate,
        });

        function onChangeParamSettledDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        win = $("#WinManageInvestment").kendoWindow({
            height: 550,
            title: "Manage Investment",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");


        //WinFundClient = $("#WinFundClient").kendoWindow({
        //    height: 500,
        //    title: "Fund Client List Detail",
        //    visible: false,
        //    width: 850,
        //    modal: true,
        //    open: function (e) {
        //        this.wrapper.css({ top: 80 })
        //    },
        //}).data("kendoWindow");


        WinUpdateSettledDate = $("#WinUpdateSettledDate").kendoWindow({
            height: 150,
            title: "* Update Settled Date",
            visible: false,
            width: 350,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");





        //$("#BitAllClient").change(function () {
        //    if (this.checked == true) {
        //        // disable button
        //        $("#ShowGrid").data("kendoButton").enable(false);
        //    }
        //    else {

        //        // enable button
        //        $("#ShowGrid").data("kendoButton").enable(true);
        //    }
        //});

        //$("#ShowGrid").click(function () {
        //    WinFundClient.center();
        //    WinFundClient.open();
        //    LoadData();
        //});



        ////Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundFrom").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,

                });


                $("#FundFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

        ////FundClient
        //$.ajax({
        //    url: window.location.origin + "/Radsoft/FundClient/GetFundClientDetailComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'GET',
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {
        //        $("#FundClientFrom").kendoMultiSelect({
        //            dataValueField: "FundClientPK",
        //            dataTextField: "ID",
        //            filter: "contains",
        //            dataSource: data,

        //        });


        //        $("#FundClientFrom").data("kendoMultiSelect").value("0");
        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});

        //function OnChangeFundClientFrom() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }


        //}


        $("#Type").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "OMS Invesment", value: 1 },
                { text: "Dealing Instruction", value: 2 },
                { text: "Settlement Instruction", value: 3 },
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeType,
            index: 0,
        });

        function OnChangeType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
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
            index: 0,
        });

        function OnChangeStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        ////SysNo
        //var ManageInvestment = {

        //    DateFrom: $('#ValueDateFrom').val(),
        //    DateTo: $('#ValueDateTo').val(),
        //    FundFrom: 0,
        //    FundClientFrom: 0,
        //    FundText: "",
        //    FundClientText: "",
        //    Type: $('#Type').val(),
        //    Status: $('#Status').val()
        //};


        //$.ajax({
        //    url: window.location.origin + "/Radsoft/ManageInvestment/GetSysNo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
        //    type: 'POST',
        //    data: JSON.stringify(ManageInvestment),
        //    contentType: "application/json;charset=utf-8",
        //    success: function (data) {

        //        $("#SysNo").kendoMultiSelect({
        //            dataValueField: "SysNo",
        //            dataTextField: "SysNo",
        //            filter: "contains",
        //            dataSource: data,

        //        });
        //        //$("#SysNo").data("kendoMultiSelect").value("0");

        //    },
        //    error: function (data) {
        //        alertify.alert(data.responseText);
        //    }
        //});


        win.center();
        win.open();
    }





    $("#BtnGetSysNo").click(function (e) {


            $('#lblUpdateSettledDate').show();

            var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
            var stringFundFrom = '';
            for (var i in ArrayFundFrom) {
                stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

            }
            stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

   

            var FundText = $("#FundFrom").data("kendoMultiSelect").dataItems();
            var stringFundText = '';
            for (var i in FundText) {
                stringFundText = stringFundText + FundText[i].ID + ',';

            }


         

            var ManageInvestment = {

                DateFrom: $('#ValueDateFrom').val(),
                DateTo: $('#ValueDateTo').val(),
                FundFrom: stringFundFrom,
                FundText: stringFundText,
                Type: $('#Type').val(),
            };
            $.ajax({
                url: window.location.origin + "/Radsoft/ManageInvestment/GetSysNo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(ManageInvestment),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    IniGridApplyParameter();
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });



    });



    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close");
            }
        });
    });


    //$("#CloseGrid").click(function () {

    //    WinFundClient.close();
    //});

    function ClearAttribute() {
        $("#LblValueDate").hide();
        $("#LblValueDateFrom").hide();
        $("#LblDateTo").hide();
    }




    function IniGridApplyParameter() {
        $("#gridApplyParameter").empty();

        var ApprovedURL = window.location.origin + "/Radsoft/ManageInvestment/GetDataApplyParameterInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
             dataSourceApplyParameter = getDataApplyParameter(ApprovedURL);



        var grid = $("#gridApplyParameter").kendoGrid({
            dataSource: dataSourceApplyParameter,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Manage Investment"
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
            columns: [

                {
                   field: "Selected",
                   width: 50,
                   template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                   headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                   filterable: true,
                   sortable: false,
                   columnMenu: false
                },
                { field: "PK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "SettledDate", title: "Settled Date", width: 150, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'dd/MMM/yyyy')#" },
                { field: "Type", hidden: true, title: "Type", width: 120 },
                { field: "TypeDesc", title: "Type", width: 120 },
                { field: "FundID", title: "Fund ID", width: 115 },
                { field: "TrxTypeID", title: "Trx Type", width: 115 },
                { field: "InstrumentTypeID", title: "Ins. Type", width: 115 },
                { field: "InstrumentID", title: "Instrument ID", width: 115 },
                { field: "DoneVolume", hidden: true, title: "Done Volume", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "DonePrice", hidden: true, title: "Done Price", width: 100, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "TotalAmount", hidden: true, title: "Total Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },

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
    }

    function selectDataApproved(e) {


        var grid = $("#gridApplyParameter").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        var _PK = dataItemX.PK;
        var _type = dataItemX.Type;
        var _checked = this.checked;
        SelectDeselectData(_checked, _PK, _type);

    }


    function SelectDeselectData(_a, _b, _c) {
        $.ajax({
            url: window.location.origin + "/Radsoft/ManageInvestment/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + _c,
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
            url: window.location.origin + "/Radsoft/ManageInvestment/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a,
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


    function getDataApplyParameter(_url) {
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
                 pageSize: 500,
                 schema: {
                     model: {

                         fields: {
                             PK: { type: "number" },
                             Date: { type: "date" },
                             Selected: { type: "boolean" },
                             Type: { type: "number" },
                             TypeDesc: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             InvestmentPK: { type: "number" },
                             DealingPK: { type: "number" },
                             SettlementPK: { type: "number" },
                             DoneVolume: { type: "number" },
                             DonePrice: { type: "number" },
                             TotalAmount: { type: "number" },

                         }
                     }
                 },

             });
    }






    $("#BtnUpdateSettledDate").click(function () {
        showUpdateSettledDate();
    });

    function showUpdateSettledDate(e) {

        WinUpdateSettledDate.center();
        WinUpdateSettledDate.open();

    }

    $("#BtnOkUpdateSettledDate").click(function () {


        alertify.confirm("Are you sure want to Update Settle Date ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/ManageInvestment/UpdateSettledDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamSettledDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Update Settled Date Success !");
                        IniGridApplyParameter();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });

    });

    $("#BtnCancelUpdateSettledDate").click(function () {

        alertify.confirm("Are you sure want to cancel Update?", function (e) {
            if (e) {
                WinUpdateSettledDate.close();
                alertify.alert("Cancel Update");
            }
        });
    });


    $("#BtnRefresh").click(function () {
        IniGridApplyParameter();
    });


});