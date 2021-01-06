$(document).ready(function () {
    document.title = 'DASHBOARD';

    initGridDashOne();
    initGridTwo();

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
                         
                         }
                     }
                 }
             });
    }

    function initGridDashOne() {
        var URL = window.location.origin + "/Radsoft/Dashboard/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(URL);

        $("#gridDashOne").kendoGrid({
            dataSource: dataSourceApproved,
            height: 800,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Drag here to group data"
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
                //{ command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "TableName", title: "Table Name", width: 200 },
                { field: "NoSystem", title: "System Number", width: 80 },
                { field: "Description", title: "Description", width: 300 },
                { field: "Reason", title: "Reason", width: 700 }
            ]
        });
    }

    function initGridTwo()
    {
        var URL = window.location.origin + "/Radsoft/Dashboard/GetData_Dashboard_TotalPendingTransaction/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
         dataSourceApproved = getDataSource(URL);

        $("#gridDashTwo").kendoGrid({
            dataSource: dataSourceApproved,
            height: 400,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Drag here to group data"
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
                //{ command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                { field: "TableName", title: "Table Name", width: 200 },
                { field: "TotalPending", title: "Total Pending Trx", width: 100 }
            ]
        });
    }
});