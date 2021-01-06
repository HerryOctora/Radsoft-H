$(document).ready(function () {
    document.title = 'FORM AGENT FUND POSITION';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var GlobValidatorFee = $("#winAgentFeeSummary").kendoValidator().data("kendoValidator");
    var GlobValidator = $("#winAgentPosition").kendoValidator().data("kendoValidator");

    var checkedIds = {};
    var checkedName = {};
    var button = $("#ShowGrid").data("kendoButton");
    var buttonFee = $("#ShowGridFee").data("kendoButton");

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

     
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
     
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnGenerate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

        $("#BtnGenerateAgentFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });

    }

    function initWindow() {
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
                
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
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
                
                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }

        $("#ParamDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDateFrom
        });

        $("#ParamDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        function OnChangeParamDateFrom() {
            if ($("#ParamDateFrom").data("kendoDatePicker").value() != null) {
                $("#ParamDateTo").data("kendoDatePicker").value($("#ParamDateFrom").data("kendoDatePicker").value());
            }
        }



        $("#ParamFeeDateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamFeeDateFrom
        });

        $("#ParamFeeDateTo").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        function OnChangeParamFeeDateFrom() {
            if ($("#ParamFeeDateFrom").data("kendoDatePicker").value() != null) {
                $("#ParamFeeDateTo").data("kendoDatePicker").value($("#ParamFeeDateFrom").data("kendoDatePicker").value());
            }
        }


        winAgentPosition = $("#winAgentPosition").kendoWindow({
            height: 400,
            title: "Generate Agent Position",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onwinAgentPositionClose
        }).data("kendoWindow");

        winAgentFeeSummary = $("#winAgentFeeSummary").kendoWindow({
            height: 400,
            title: "Generate Agent Fee Summary",
            visible: false,
            width: 500,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onwinAgentFeeSummaryClose
        }).data("kendoWindow");


        WinAgent = $("#WinAgent").kendoWindow({
            height: 500,
            title: "Agent List Detail",
            visible: false,
            width: 850,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");
      
        WinAgentFee = $("#WinAgentFee").kendoWindow({
            height: 500,
            title: "Agent List Detail",
            visible: false,
            width: 850,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
        }).data("kendoWindow");

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
                             AgentFundPositionPK: { type: "number" },
                             Date: { type: "date" },
                             AgentPK: { type: "number" },
                             AgentID: { type: "string" },
                             AgentName: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             UnitAmount: { type: "number" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
    }

    function initGrid() {
        
        $("#gridAgentFundPositionApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var AgentFundPositionApprovedURL = window.location.origin + "/Radsoft/AgentFundPosition/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(AgentFundPositionApprovedURL);

        }

        var grid = $("#gridAgentFundPositionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Client Position"
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
                //{ field: "AgentFundPositionPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 200, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                       //{ field: "AgentPK", title: "Agent PK", width: 500 },
                       //{ field: "FundPK", title: "Fund PK", width: 500 },
                { field: "AgentName", title: "Agent Name", width: 500 },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "UnitAmount", title: "Unit", width: 300, format: "{0:n4}", attributes: { style: "text-align:right;" } },
            ]
        }).data("kendoGrid");
      

      

    }

    $("#BtnRefresh").click(function () {
        refresh();
    });

   


    $("#BtnGenerate").click(function (e) {


        $("#BtnGenerateAgentPosition").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#ShowGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#CloseGrid").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });

     

   



        $("#BitAllAgent").change(function () {
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
            WinAgent.center();
            WinAgent.open();
            LoadData();
        });
        $("#CloseGrid").click(function () {

            WinAgent.close();
        });

        function validateData(All) {

            if ($('#BitAllAgent').is(":checked") == false && All == 0) {
                return 0;
            }
            else {
                return 1;
            }
        }

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
                    //change: OnChangeFundFrom,

                    //suggest: true,
                    //index: 0
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
            //$("#FundTo").data("kendoComboBox").value($("#FundFrom").data("kendoComboBox").value());
        }

        //Agent
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentFrom").kendoMultiSelect({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,

                });


                $("#AgentFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundClientFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }

        winAgentPosition.center();
        winAgentPosition.open();
    });



    function LoadData() {
        //DataSource definition
        dataSourcess = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/Agent/GetAgentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                        AgentPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },

                    }
                }
            }
        });



        //Grid definition
        var gridAgent = $("#gridAgent").kendoGrid({
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
            { template: "<input type='checkbox' class='checkbox' />", width: "80px" }, {
                field: "ID",
                title: "Agent ID",
                width: "200px"
            }, {
                field: "Name",
                title: "Agent Name"
            }, 
            ],
            editable: "inline"
        }).data("kendoGrid");

        //bind click event to the checkbox
        gridAgent.table.on("click", ".checkbox", selectRow);


        //on click of the checkbox:
        function selectRow() {
            var checked = this.checked,
            rowA = $(this).closest("tr"),
            gridAgent = $("#gridAgent").data("kendoGrid"),
            dataItemZ = gridAgent.dataItem(rowA);

            checkedIds[dataItemZ.AgentPK] = checked;
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
                if (checkedIds[view[i].AgentPK]) {
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



    $("#BtnGenerateAgentPosition").click(function (e) {
        var All = 0;

        if ($('#BitAllAgent').is(":checked") == true) {
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
        var val = validateData(All);
        if (val == 1) {
            var ArrayFundFrom = $("#FundFrom").data("kendoMultiSelect").value();
            var stringFundFrom = '';
            for (var i in ArrayFundFrom) {
                stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

            }
            stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

            var ArrayAgentFrom = All;
            var stringAgentFrom = '';
            for (var i in ArrayAgentFrom) {
                stringAgentFrom = stringAgentFrom + ArrayAgentFrom[i] + ',';

            }
            stringAgentFrom = stringAgentFrom.substring(0, stringAgentFrom.length - 1)

            var FundText = $("#FundFrom").data("kendoMultiSelect").dataItems();
            var stringFundText = '';
            for (var i in FundText) {
                stringFundText = stringFundText + FundText[i].ID + ',';

            }


            var ArrayAgentText = AllName;
            var stringAgentText = '';
            for (var i in ArrayAgentText) {
                stringAgentText = stringAgentText + ArrayAgentText[i] + ',';

            }


            //if (validateData() == 1) {
            alertify.confirm("Are you sure want to Generate data ?", function (e) {
                if (e) {
                    $.blockUI({});

                    var GenerateAgentPosition = {

                        ValueDateFrom: $('#ParamDateFrom').val(),
                        ValueDateTo: $('#ParamDateTo').val(),
                        FundFrom: stringFundFrom,
                        AgentFrom: stringAgentFrom,
                        FundText: stringFundText,
                        AgentText: stringAgentText,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/AgentFundPosition/GenerateAgentPosition/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateAgentPosition),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            alertify.alert(data);
                            refresh();
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
        else {
            alertify.alert("Please Choose Client!")
        }

    });

    function onwinAgentPositionClose() {
        $('#FundFrom').data('kendoMultiSelect').destroy();
        $('#FundFrom').unwrap('.k-multiselect').show().empty();
        $(".k-multiselect-wrap").remove();
        $("#AgentFrom").val("")
        $("#ParamDateFrom").val("")
        $("#ParamDateTo").val("")
    }


    // AGENT FEE


    $("#BtnGenerateAgentFee").click(function (e) {


        $("#BtnGenerateAgentFeeSummary").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#ShowGridFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnDownload.png"
        });
        $("#CloseGridFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });





        $("#BitAllAgentFee").change(function () {
            if (this.checked == true) {
                // disable button
                $("#ShowGridFee").data("kendoButton").enable(false);
            }
            else {

                // enable button
                $("#ShowGridFee").data("kendoButton").enable(true);
            }
        });

        $("#ShowGridFee").click(function () {
            WinAgent.center();
            WinAgent.open();
            LoadData();
        });
        $("#CloseGrid").click(function () {

            WinAgentFee.close();
        });

        function validateData(All) {

            if ($('#BitAllAgentFee').is(":checked") == false && All == 0) {
                return 0;
            }
            else {
                return 1;
            }
        }

        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFeeFundFrom").kendoMultiSelect({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,

                });


                $("#ParamFeeFundFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



        //Agent
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AgentFeeFrom").kendoMultiSelect({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    dataSource: data,
                    //change: OnChangeFundClientFrom,

                    //suggest: true,
                    //index: 0
                });


                $("#AgentFeeFrom").data("kendoMultiSelect").value("0");
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });



        winAgentFeeSummary.center();
        winAgentFeeSummary.open();
    });



    function LoadDataFee() {
        //DataSource definition
        dataSourceFee = new kendo.data.DataSource({
            transport: {
                read: {
                    url: window.location.origin + "/Radsoft/Agent/GetAgentComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
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
                        AgentPK: { type: "number" },
                        ID: { type: "string" },
                        Name: { type: "string" },

                    }
                }
            }
        });



        //Grid definition
        var gridAgentFee = $("#gridAgentFee").kendoGrid({
            dataSource: dataSourceFee,
            pageable: true,
            height: 430,
            width: 350,
            //define dataBound event handler
            dataBound: onDataBoundFee,
            sortable: true,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            columns: [
            //define template column with checkbox and attach click event handler
            { template: "<input type='checkbox' class='checkbox' />", width: "80px" }, {
                field: "ID",
                title: "Agent ID",
                width: "200px"
            }, {
                field: "Name",
                title: "Agent Name"
            },
            ],
            editable: "inline"
        }).data("kendoGrid");

        //bind click event to the checkbox
        gridAgent.table.on("click", ".checkbox", selectRowFee);


        //on click of the checkbox:
        function selectRowFee() {
            var checked = this.checked,
            rowA = $(this).closest("tr"),
            gridAgent = $("#gridAgentFee").data("kendoGrid"),
            dataItemZ = gridAgent.dataItem(rowA);

            checkedIds[dataItemZ.AgentPK] = checked;
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
        function onDataBoundFee(e) {
            var view = this.dataSource.view();
            for (var i = 0; i < view.length; i++) {
                if (checkedIds[view[i].AgentPK]) {
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



    $("#BtnGenerateAgentFeeSummary").click(function (e) {
        var All = 0;

        if ($('#BitAllAgentFee').is(":checked") == true) {
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
        var val = validateData(All);
        if (val == 1) {
            var ArrayFundFrom = $("#ParamFeeFundFrom").data("kendoMultiSelect").value();
            var stringFundFrom = '';
            for (var i in ArrayFundFrom) {
                stringFundFrom = stringFundFrom + ArrayFundFrom[i] + ',';

            }
            stringFundFrom = stringFundFrom.substring(0, stringFundFrom.length - 1)

            var ArrayAgentFrom = All;
            var stringAgentFrom = '';
            for (var i in ArrayAgentFrom) {
                stringAgentFrom = stringAgentFrom + ArrayAgentFrom[i] + ',';

            }
            stringAgentFrom = stringAgentFrom.substring(0, stringAgentFrom.length - 1)

            var FundText = $("#ParamFeeFundFrom").data("kendoMultiSelect").dataItems();
            var stringFundText = '';
            for (var i in FundText) {
                stringFundText = stringFundText + FundText[i].ID + ',';

            }


            var ArrayAgentText = AllName;
            var stringAgentText = '';
            for (var i in ArrayAgentText) {
                stringAgentText = stringAgentText + ArrayAgentText[i] + ',';

            }


            //if (validateData() == 1) {
            alertify.confirm("Are you sure want to Generate data ?", function (e) {
                if (e) {
                    $.blockUI({});

                    var GenerateAgentFeeSummary = {

                        ValueDateFrom: $('#ParamFeeDateFrom').val(),
                        ValueDateTo: $('#ParamFeeDateTo').val(),
                        FundFrom: stringFundFrom,
                        AgentFrom: stringAgentFrom,
                        FundText: stringFundText,
                        AgentText: stringAgentText,
                        EntryUsersID: sessionStorage.getItem("user")
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/AgentFundPosition/GenerateAgentFeeSummary/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        data: JSON.stringify(GenerateAgentFeeSummary),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            alertify.alert(data);
                            refresh();
                        },
                        error: function (data) {
                            $.unblockUI();
                            alertify.alert(data.responseText);
                        }
                    });
                }
            });
        }
        else {
            alertify.alert("Please Choose Client!")
        }

    });

    function onwinAgentFeeSummaryClose() {
        $('#ParamFeeFundFrom').data('kendoMultiSelect').destroy();
        $('#ParamFeeFundFrom').unwrap('.k-multiselect').show().empty();
        $(".k-multiselect-wrap").remove();
        $("#AgentFeeFrom").val("")
        $("#ParamFeeDateFrom").val("")
        $("#ParamFeeDateTo").val("")
    }



});
