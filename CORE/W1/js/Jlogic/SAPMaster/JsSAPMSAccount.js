$(document).ready(function () {
    document.title = 'FORM SAP Master Account';
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
        $("#BtnRefresh").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnGetData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnCheckData").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnUpdate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });
    }

    function initWindow() {

        win = $("#WinSAPMSAccount").kendoWindow({
            height: 275,
            title: "SAP Master Account Detail",
            visible: false,
            width: 900,
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

       
    }


    $("#BtnRefresh").click(function () {
        refresh();
    });

    var GlobValidator = $("#WinSAPMSAccount").kendoValidator().data("kendoValidator");

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
        var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));

        $("#SAPMSAccountPK").val(dataItemX.SAPMSAccountPK);
        $("#HistoryPK").val(dataItemX.HistoryPK);
        $("#Notes").val(dataItemX.Notes);
        $("#ID").val(dataItemX.ID);
        $("#Name").val(dataItemX.Name);
        $("#EntryUsersID").val(dataItemX.EntryUsersID);
        $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
        $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

        $("#AccountType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "GL", value: 1 },
                { text: "AR", value: 2 },
            ],
            filter: "contains",
            change: OnChangeAccountType,
            value: setAccountType(),
            suggest: true
        });

        function OnChangeAccountType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setAccountType() {
            if (dataItemX.AccountType == null || dataItemX.AccountType == 0) {
                return "";
            } else {
                return dataItemX.AccountType;
            }
        }

        win.center();
        win.open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        refresh();
    }

    function clearData() {
        $("#SAPMSAccountPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
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
                             ID: { type: "string" },
                             Name: { type: "string" },
                             AccountType: { type: "number" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        var gridApproved = $("#gridSAPMSAccountApproved").data("kendoGrid");
        gridApproved.dataSource.read();
    }

    function initGrid() {
        var SAPMSAccountApprovedURL = window.location.origin + "/Radsoft/SAPMaster/GetDataAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
          dataSourceApproved = getDataSource(SAPMSAccountApprovedURL);

        $("#gridSAPMSAccountApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form SAPMSAccount"
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
                { field: "ID", title: "ID", width: 200 },
                { field: "Name", title: "Name", width: 300 },
                { field: "AccountType", title: "Account Type",hidden : true, width: 300 },
                { field: "AccountTypeID", title: "Account Type", width: 300 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabSAPMSAccount").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            }
        });
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

    $("#BtnGetData").click(function () {
        
        alertify.confirm("Are you sure want to Get Data Account ?", function (e) {
            if (e) {
               
                            $.ajax({
                                url: window.location.origin + "/Radsoft/SAPMaster/GetDataAccountFromSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/GetMasterData_SAP",
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
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

    $("#BtnCheckData").click(function () {

        alertify.confirm("Are you sure want to Check Data Account ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/SAPMaster/CheckDataAccountFromSAP/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == "")
                        {
                            alertify.alert("All Data Is Exist");
                            refresh();
                            $.unblockUI();
                        }
                        else
                        {

                            alertify.alert(data);
                            refresh();
                            $.unblockUI();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });
            }
        });
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                        var Bank = {
                            Name: $('#Name').val(),
                            ID: $('#ID').val(),
                            AccountType: $('#AccountType').val(),
                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/SAPMaster/ActionAccount/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "SAPMSAccount_U",
                            type: 'POST',
                            data: JSON.stringify(Bank),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.success(data);
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
    
});
