$(document).ready(function () {
    var win;
    var GlobValidator = $("#WinManageURTransaction").kendoValidator().data("kendoValidator");
    var checkedIds = {};
    var checkedName = {};
    var button = $("#ShowGrid").data("kendoButton");

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

    function LoadData() {
        //DataSource definition
        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/FundClient/GetFundClientComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    dataType: "json"

                }

            },
            batch: true,
            //cache: false,
            error: function (e) {
                alert(e.errorThrown + " - " + e.xhr.responseText);
                this.cancelChanges();
            },
            pageSize: 10,
            schema: {
                model: {
                    fields: {
                        FundClientPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },
                        IFUA: { type: "string" },
                        SID: { type: "string" },

                    }
                }
            }
        });



        //Grid definition
        var gridFundClient = $("#gridFundClient").kendoGrid({
            dataSource: dataSourcess,
            pageable: true,
            height: 430,
            width: 350,
            //define dataBound event handler
            dataBound: onDataBound,
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
            //define template column with checkbox and attach click event handler
            { template: "<input type='checkbox' class='checkbox' />" }, {
                field: "ID",
                title: "Client ID",
                width: "120px"
            }, {
                field: "Name",
                title: "Client Name",
                width: "300px"
            }, {
                field: "IFUA",
                title: "IFUA Code",
                width: "185px"
            }, {
                field: "SID",
                title: "SID",
                width: "150px"
            }
            ],
            editable: "inline"
        }).data("kendoGrid");

        //bind click event to the checkbox
        gridFundClient.table.on("click", ".checkbox", selectRow);


        //on click of the checkbox:
        function selectRow() {
            var checked = this.checked,
            rowA = $(this).closest("tr"),
            gridFundClient = $("#gridFundClient").data("kendoGrid"),
            dataItemZ = gridFundClient.dataItem(rowA);

            checkedIds[dataItemZ.FundClientPK] = checked;
            checkedName[dataItemZ.Name] = checked;
            if (checked) {
                //-select the row
                rowA.addClass("k-state-selected");



            } else {
                //-remove selection
                rowA.removeClass("k-state-selected");
            }
        }

        //on dataBound event restore previous selected rows:
        function onDataBound(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].FundClientPK]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                    .addClass("k-state-selected")
                    .find(".checkbox")
                    .attr("checked", "checked");
                }
                if (checkedName[view[i].Name]) {
                    this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                    .addClass("k-state-selected")
                    .find(".checkbox")
                    .attr("checked", "checked");
                }
            }
        }
    }

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

        $("#BtnUpdateNAVDate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnPostingBySelectedZMANAGE_UR").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
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
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeValueDateTo
        });

        function OnChangeValueDateFrom() {
            if ($("#ValueDateFrom").data("kendoDatePicker").value() != null) {
                $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
            }
            RecalParameter();
            InitGridApplyParameter();
        }

        function OnChangeValueDateTo() {

            RecalParameter();
            InitGridApplyParameter();
        }


        $("#ParamNavDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            change: onChangeParamNavDate,
        });

        function onChangeParamNavDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        win = $("#WinManageURTransaction").kendoWindow({
            height: 550,
            title: "Manage UR Transaction",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            }
        }).data("kendoWindow");


        WinFundClient = $("#WinFundClient").kendoWindow({
            height: 500,
            title: "Fund Client List Detail",
            visible: false,
            width: 850,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");


        WinUpdateNavDate = $("#WinUpdateNavDate").kendoWindow({
            height: 150,
            title: "* Update Nav Date",
            visible: false,
            width: 350,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

      



        $("#BitAllClient").change(function () {
            if (this.checked == true) {
                // disable button
                $("#ShowGrid").data("kendoButton").enable(false);
            }
            else {

                // enable button
                $("#ShowGrid").data("kendoButton").enable(true);
            }
        });

        $("#ShowGrid").click(function () {
            WinFundClient.center();
            WinFundClient.open();
            LoadData();
        });
        //Fund
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
                    change: OnChangeFundFrom,
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

            RecalParameter();
            InitGridApplyParameter();

        }

     


        $("#Type").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Subscription", value: 1 },
                { text: "Redemption", value: 2 },
                { text: "Switching", value: 3 },
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
            RecalParameter();
            InitGridApplyParameter();
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
            index: 2,
        });

        function OnChangeStatus() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            RecalParameter();
            InitGridApplyParameter();
        }


        //Trx Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SubscriptionType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    index: 0,
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeTrxType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');

            }
            RecalParameter();
            InitGridApplyParameter();
        }



        $("#BitAllClient").prop('checked', true);

        var ManageURTransaction = {

            DateFrom: $('#ValueDateFrom').val(),
            DateTo: $('#ValueDateTo').val(),
            FundFrom: 0,
            FundClientFrom: 0,
            Type: 1,
            Status: 3,
            TrxType: 1,
        };


        $.ajax({
            url: window.location.origin + "/Radsoft/ManageURTransaction/InitDataApplyParameter/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(ManageURTransaction),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                InitGridApplyParameter();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
       


        win.center();
        win.open();
    }

    $('#BitAllClient').change(function () {
        if (this.checked) {
            RecalParameter();
            InitGridApplyParameter();
        }
    });

    function RecalParameter() {
        var All = 0;

        if ($('#BitAllClient').is(":checked") == true) {
            All = 0;
            AllName = 0;
        }
        else {
            All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }

            AllName = [];
            for (var i in checkedName) {
                if (checkedName[i]) {
                    AllName.push(i);
                }
            }


        }

        var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
        var stringFundFrom = '';
        for (var i in ArrayFundFrom) {
            stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

        }
        stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

        var ArrayFundClientFrom = All;
        var stringFundClientFrom = '';
        for (var i in ArrayFundClientFrom) {
            stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

        }
        stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

        var FundText = $("#FundFrom").data("kendoMultiSelect").dataItems();
        var stringFundText = '';
        for (var i in FundText) {
            stringFundText = stringFundText + FundText[i].ID + ',';

        }


        var ArrayFundClientText = AllName;
        var stringFundClientText = '';
        for (var i in ArrayFundClientText) {
            stringFundClientText = stringFundClientText + ArrayFundClientText[i] + ',';

        }


        var ManageURTransaction = {

            DateFrom: $('#ValueDateFrom').val(),
            DateTo: $('#ValueDateTo').val(),
            FundFrom: stringFundFrom,
            FundClientFrom: stringFundClientFrom,
            Type: $('#Type').val(),
            Status: $('#Status').val(),
            TrxType: $('#TrxType').val(),
        };


        $.ajax({
            url: window.location.origin + "/Radsoft/ManageURTransaction/InitDataApplyParameter/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(ManageURTransaction),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                InitGridApplyParameter();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }
 

    $("#BtnCancel").click(function () {

        alertify.confirm("Are you sure want to cancel and close Reporting?", function (e) {
            if (e) {
                win.close();
                alertify.success("Close Report");
            }
        });
    });

    $("#CloseGrid").click(function () {
        RecalParameter();
        InitGridApplyParameter();


        WinFundClient.close();
    });

    function ClearAttribute() {
        $("#LblValueDate").hide();
        $("#LblValueDateFrom").hide();
        $("#LblDateTo").hide();
    }

    function validatedata(All) {
        
            if ($('#BitAllClient').is(":checked") == false && All == 0) {
                return 0;
            }
            else {
                return 1;
            }
    }
   
    function InitGridApplyParameter() {
        $("#gridApplyParameter").empty();
        var All = 0;

        if ($('#BitAllClient').is(":checked") == true) {
            All = 0;
            AllName = 0;
        }
        else {
            All = [];
            for (var i in checkedIds) {
                if (checkedIds[i]) {
                    All.push(i);
                }
            }

            AllName = [];
            for (var i in checkedName) {
                if (checkedName[i]) {
                    AllName.push(i);
                }
            }


        }
        var val = validatedata(All);
        if (val == 1) {

            $('#lblUpdateNAVDate').show();
            $('#lblPostingBySelectedZMANAGE_UR').show();

            var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
            var stringFundFrom = '';
            for (var i in ArrayFundFrom) {
                stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

            }
            stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

            var ArrayFundClientFrom = All;
            var stringFundClientFrom = '';
            for (var i in ArrayFundClientFrom) {
                stringFundClientFrom = stringFundClientFrom + ArrayFundClientFrom[i] + ',';

            }
            stringFundClientFrom = stringFundClientFrom.substring(0, stringFundClientFrom.length - 1)

            var FundText = $("#FundFrom").data("kendoMultiSelect").dataItems();
            var stringFundText = '';
            for (var i in FundText) {
                stringFundText = stringFundText + FundText[i].ID + ',';

            }


            var ArrayFundClientText = AllName;
            var stringFundClientText = '';
            for (var i in ArrayFundClientText) {
                stringFundClientText = stringFundClientText + ArrayFundClientText[i] + ',';

            }

            var ManageURTransaction = {

                DateFrom: $('#ValueDateFrom').val(),
                DateTo: $('#ValueDateTo').val(),
                FundFrom: stringFundFrom,
                FundClientFrom: stringFundClientFrom,
                FundText: stringFundText,
                FundClientText: stringFundClientText,
                Type: $('#Type').val(),
                Status: $('#Status').val(),
                TrxType: $('#TrxType').val(),
            };


            var ApprovedURL = window.location.origin + "/Radsoft/ManageURTransaction/GetDataApplyParameter/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                 dataSourceApplyParameter = getDataApplyParameter(ApprovedURL);


            var grid = $("#gridApplyParameter").kendoGrid({
                dataSource: dataSourceApplyParameter,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Manage UR Transaction"
                    }
                },
                filterable: false,
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
                    { field: "SysNo", title: "SysNo.", width: 95 },
                    { field: "Date", title: "Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                    { field: "Type", hidden: true, title: "Type", width: 120 },
                    { field: "TypeDesc", title: "Type", width: 120 },
                    { field: "FundClientName", title: "Fund Client", width: 150 },
                    { field: "FundID", title: "Fund ID", width: 115 },
                    { field: "CashAmount", title: "Cash Amount", width: 165, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "UnitAmount", title: "Unit Amount", width: 165, format: "{0:n8}", attributes: { style: "text-align:right;" } },

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


        else {
            alertify.alert("Please Choose Client!")
        }
    }

    function selectDataApproved(e) {


        var grid = $("#gridApplyParameter").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        var _PK = dataItemX.SysNo;
        var _type = dataItemX.Type;
        var _checked = this.checked;
        SelectDeselectData(_checked, _PK, _type);

    }

    function SelectDeselectData(_a, _b, _c) {
        $.ajax({
            url: window.location.origin + "/Radsoft/ManageURTransaction/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _a + "/" + _b + "/" + _c,
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
            url: window.location.origin + "/Radsoft/ManageURTransaction/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" +  _a,
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
                             SysNo: { type: "number" },
                             Date: { type: "date" },
                             Selected: { type: "boolean" },
                             Type: { type: "number" },
                             TypeDesc: { type: "string" },
                             FundClientPK: { type: "number" },
                             FundClientID: { type: "string" },
                             FundClientName: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             CashAmount: { type: "number" },
                             UnitAmount: { type: "number" },

                         }
                     }
                 },

             });
    }


    $("#BtnUpdateNAVDate").click(function () {
        showUpdateNAVDate();
    });

    function showUpdateNAVDate(e) {

        WinUpdateNavDate.center();
        WinUpdateNavDate.open();

    }

    $("#BtnOkUpdateNavDate").click(function () {


        alertify.confirm("Are you sure want to Update Nav Date ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/ManageURTransaction/UpdateNavDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamNavDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert("Update Nav Date Success !");
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        });

    });

    $("#BtnCancelUpdateNavDate").click(function () {

        alertify.confirm("Are you sure want to cancel Update?", function (e) {
            if (e) {
                WinUpdateNavDate.close();
                alertify.alert("Cancel Update");
            }
        });
    });

    $("#BtnPostingBySelectedZMANAGE_UR").click(function (e) {
        //1.Subs, 2.Red, 3.Swi

        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                if ($("#Type").val() == 1)
                {
                        //$.blockUI();
                        $.ajax({
                            url: window.location.origin + "/Radsoft/ManageURTransaction/ValidateClientSubscriptionBySelectedDataByManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/ManageURTransaction/ValidatePostingClientSubscriptionBySelectedByManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == 1) {
                                                alertify.alert("Posting Cancel, Must be Generate End Day Trails Yesterday First / Client Subscription Already Posting");
                                                $.unblockUI();

                                            }
                                            else if (data == 2) {
                                                alertify.alert("Posting Cancel, Date is Holiday !");
                                                $.unblockUI();

                                            } else {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/ClientSubscription/PostingBySelectedDataManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data);
                                                        //refresh();
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                            $.unblockUI();
                                        }
                                    });
                                } else {
                                    alertify.alert("NAV Must Ready First, Please Click Get NAV");
                                    $.unblockUI();
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                                $.unblockUI();
                            }
                        });

              

                }

                else if ($("#Type").val() == 2) {
                        $.blockUI();
                        $.ajax({
                            url: window.location.origin + "/Radsoft/ManageURTransaction/ValidateClientRedemptionBySelectedDataByManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/ManageURTransaction/ValidatePostingClientRedemptionBySelectedByManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == 1) {
                                                alertify.alert("Posting Cancel, Must be Generate End Day Trails Yesterday First / Client Redemption Already Posting");
                                                $.unblockUI();
                                            }
                                            else if (data == 2) {
                                                alertify.alert("Posting Cancel, Date is Holiday !");
                                                $.unblockUI();

                                            } else {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/ClientRedemption/PostingBySelectedDataManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data);
                                                        //refresh();
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                    }
                                                });
                                            }
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                            $.unblockUI();
                                        }
                                    });

                                } else {
                                    alertify.alert("NAV Must Ready First, Please Click Get NAV");
                                    $.unblockUI();
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });

              

                }
                else {
                     $.blockUI();
                        $.ajax({
                            url: window.location.origin + "/Radsoft/ManageURTransaction/ValidateClientSwitchingBySelectedDataByManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data == false) {
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/ManageURTransaction/ValidatePostingClientSwitchingBySelectedByManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                        type: 'GET',
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            if (data == 1) {
                                                alertify.alert("Posting Cancel, Must be Generate End Day Trails Yesterday First / Client Switching Already Posting");
                                                $.unblockUI();
                                            }
                                            else if (data == 2) {
                                                alertify.alert("Posting Cancel, Date is Holiday !");
                                                $.unblockUI();

                                            } else {
                                                $.ajax({
                                                    url: window.location.origin + "/Radsoft/ClientSwitching/PostingBySelectedDataManageUR/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#ValueDateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                    type: 'GET',
                                                    contentType: "application/json;charset=utf-8",
                                                    success: function (data) {
                                                        $.unblockUI();
                                                        alertify.alert(data);
                                                        //refresh();
                                                    },
                                                    error: function (data) {
                                                        alertify.alert(data.responseText);
                                                        $.unblockUI();
                                                    }
                                                });
                                            }
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                            $.unblockUI();
                                        }
                                    });

                                } else {
                                    alertify.alert("NAV or Total Cash Amount Not Ready, Please Click Get NAV");
                                    $.unblockUI();
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                                $.unblockUI();
                            }
                        });

                    }

            }
        });
    });

});