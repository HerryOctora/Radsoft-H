$(document).ready(function () {
    document.title = 'FORM Risk Profile Monitoring';
    //Global Variabel
    var win;
    var tabindex;
    var _d = new Date();
    var gridHeight = screen.height - 300;


    $("#ValueDate").kendoDatePicker({
        format: "dd/MMM/yyyy",
        parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        value: new Date(),
        change: OnChangeValueDate
    });
    
    function OnChangeValueDate() {
        
        var currentDate = Date.parse($("#ValueDate").data("kendoDatePicker").value());
        //Check if Date parse is successful
        if (!currentDate) {
            alertify.alert("Wrong Format Date DD/MM/YYYY");
            $("#ValueDate").data("kendoDatePicker").value(new Date());
            return;
        }
        
        if ($("#ValueDate").data("kendoDatePicker").value() != null) {

        }
        refresh();
    }

        
    //1
    initButton();
    //2
    initGrid();

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnAutoSuspend").click(function () {
        AutoSuspend();
    });
    $("#BtnOpenNewTab").kendoButton({
        imageUrl: "../../Images/Icon/IcBtnAdd.png"
    });
    $("#BtnOpenNewTab").kendoButton({
        imageUrl: "../../Images/Icon/IcBtnAdd.png"
    });
    $("#BtnOpenNewTab").kendoButton({
        imageUrl: "../../Images/Icon/IcBtnAdd.png"
    });

    function initButton() {
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnAutoSuspend").kendoButton({
            imageUrl: "../../Images/Icon/refresh2.png"
        });
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
        var newDS = getDataSource(window.location.origin + "/Radsoft/RiskProfileMonitoring/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"));
        $("#gridRiskProfileMonitoring").data("kendoGrid").setDataSource(newDS);
    }


    function initGrid() {
        var RiskProfileMonitoringURL = window.location.origin + "/Radsoft/RiskProfileMonitoring/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
        dataSource = getDataSource(RiskProfileMonitoringURL);

        $("#gridRiskProfileMonitoring").kendoGrid({
            dataSource: dataSource,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Risk Profile Monitoring"
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
               //{ command: { text: "Suspend", click: showSuspend }, title: " ", width: 100 },
               { field: "FundClientID", title: "FundClientID", width: 200 },
               { field: "FundClientName", title: "FundClientName", width: 200 },
               { field: "SID", title: "SID", width: 200 },
               { field: "IFUA", title: "IFUA", width: 200 },
               { field: "FundRiskProfile", title: "Fund Risk Profile", width: 200 },
               { field: "InvestorRiskProfile", title: "Investor Risk Profile", width: 200 },
               { field: "FundID", title: "FundID", width: 200 },
               { field: "UnitPosition", title: "UnitPosition", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "CashPosition", title: "CashPosition", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
            ]
        });
    }

    function onDataBound() {
        var grid = $("#gridRiskProfileMonitoring").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.BitIsSuspend == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("rowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("rowPending");
            }
        });
    }

});
