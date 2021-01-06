$(document).ready(function () {
    document.title = 'FORM DAILY DATA FOR COMMISSION RPT NEW LOG'; 
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinGetNav;
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


    }

    function initWindow() {
        //$("#DateFrom").kendoDatePicker({
        //    format: "dd/MMM/yyyy",
        //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        //    change: OnChangeDateFrom,
        //    value: new Date(),
        //});
        //$("#DateTo").kendoDatePicker({
        //    format: "dd/MMM/yyyy",
        //    parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        //    change: OnChangeDateTo,
        //    value: new Date(),
        //});


        //function OnChangeDateFrom() {
        //    var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
        //    //Check if Date parse is successful
        //    if (!_DateFrom) {

        //        alertify.alert("Wrong Format Date DD/MMM/YYYY");
        //        $("#DateFrom").data("kendoDatePicker").value(new Date());
        //        return;
        //    }
        //    $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
        //    refresh();
        //}
        //function OnChangeDateTo() {
        //    var _DateTo = Date.parse($("#DateTo").data("kendoDatePicker").value());
        //    //Check if Date parse is successful
        //    if (!_DateTo) {

        //        alertify.alert("Wrong Format Date DD/MMM/YYYY");
        //        $("#DateTo").data("kendoDatePicker").value(new Date());
        //        return;
        //    }

        //    refresh();
        //}



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
                             DailyDataForCommissionRptNewLogPK: { type: "number" },
                             DateFrom: { type: "date" },
                             DateTo: { type: "date" },
                             UsersID: { type: "string" },
                             GenerateTime: { type: "date" },
                             Fund: { type: "string" },
                             Client: { type: "string" },
                             LastUpdate: { type: "date" }
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

        $("#gridDailyDataForCommissionRptNewLogApproved").empty();
     

            var DailyDataForCommissionRptNewLogURL = window.location.origin + "/Radsoft/DailyDataForCommissionRptNewLog/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 ,
            dataSourceApproved = getDataSource(DailyDataForCommissionRptNewLogURL);



        var grid = $("#gridDailyDataForCommissionRptNewLogApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Daily Data For Commission Rpt New Log"
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
                { field: "DailyDataForCommissionRptNewLogPK", title: "SysNo.", width: 95 },
                { field: "DateFrom", title: "Date From", width: 150, template: "#= kendo.toString(kendo.parseDate(DateFrom), 'dd/MMM/yyyy')#" },
                { field: "DateTo", title: "Date To", width: 150, template: "#= kendo.toString(kendo.parseDate(DateTo), 'dd/MMM/yyyy')#" },
                { field: "UsersID", title: "Users ID", width: 150 },
                { field: "GenerateTime", title: "Generate Time", width: 200, format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "Fund", title: "Fund", width: 500 },
                { field: "Client", title: "Client", width: 500 },
                { field: "LastUpdate", title: "Last Update", width: 200, format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        }).data("kendoGrid");
        



    }

    $("#BtnRefresh").click(function () {
        refresh();
    });




});