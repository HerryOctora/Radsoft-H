$(document).ready(function () {
    document.title = 'FORM COUPON SCHEDULER';
    
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
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }

    function initWindow() {
        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: new Date()
        });
        $("#CouponFromDate").kendoDatePicker({
            format: "MM/dd/yyyy",
        });
        $("#CouponToDate").kendoDatePicker({
            format: "MM/dd/yyyy",
        });

        win = $("#WinCouponScheduler").kendoWindow({
            height: 500,
            title: "Coupon Scheduler Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentComboByInstrumentTypePK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") +"/2",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FilterInstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFilterInstrumentPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeFilterInstrumentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            
            refresh();
        }
    }

    function showDetails(e) {
        if (e == null) {
            return;
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));            
            $("#CouponFromDate").data("kendoDatePicker").value(dataItemX.CouponFromDate);
            $("#CouponToDate").data("kendoDatePicker").value(dataItemX.CouponToDate);
            $("#Description").val(dataItemX.Description);
            //$("#UsersID").val(dataItemX.UsersID);
            //$("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInstrumentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#InstrumentPK").kendoComboBox({
                    dataValueField: "InstrumentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeInstrumentPK,
                    value: setCmbInstrumentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeInstrumentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbInstrumentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentPK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentPK;
                }
            }
        }

        $("#CouponRate").kendoNumericTextBox({
            format: "###.#### \\%",
            decimals: 4,
            value: setCouponRate()

        });
        function setCouponRate() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CouponRate;
            }
        }

        $("#CouponDays").kendoNumericTextBox({
            format: "n0",
            value: setCouponDays()

        });
        function setCouponDays() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.CouponDays;
            }
        }

        win.center();
        win.open();
    }

    function onPopUpClose() {
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#CouponFromDate").data("kendoDatePicker").value(null);
        $("#CouponToDate").data("kendoDatePicker").value(null);
        $("#Description").val("");
    }

    function showButton() {
        $("#BtnCancel").show();
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
                             AutoNo: { type: "number" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             CouponFromDate: { type: "date" },
                             CouponToDate: { type: "date" },
                             CouponRate: { type: "number" },
                             CouponDays: { type: "number" },
                             Description: { type: "string" },
                             UsersID: { type: "string" },
                             LastUpdate: { type: "date" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        //if (tabindex == undefined || tabindex == 0) {
        
        //}

        var valueDate = kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy");
        var instrumentPK = $("#FilterInstrumentPK").val();
        if (valueDate == undefined || valueDate == "" || valueDate == null) {
            valueDate = kendo.toString(new Date(), "MM-dd-yy");
        }
        if (instrumentPK == undefined || instrumentPK == "" || instrumentPK == null) {
            instrumentPK = 0;
        }

        // Check Existing First CouponDate
        $.ajax({
            url: window.location.origin + "/Radsoft/CouponScheduler/CheckExistingFirstCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + instrumentPK + "/",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == true) {
                    var newDS = getDataSource(window.location.origin + "/Radsoft/CouponScheduler/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + instrumentPK + "/");
                    //var newDS = getDataSource(window.location.origin + "/Radsoft/CouponScheduler/GetDataByMaturityDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + instrumentPK + "/" + valueDate);

                    $("#gridCouponScheduler").data("kendoGrid").setDataSource(newDS);
                } else {
                    alertify.alert("The first coupon date has not been set, please input first!");
                    return;
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
                return;
            }
        });
    }

    function initGrid() {
        var valueDate = kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy");
        var instrumentPK = $("#FilterInstrumentPK").val();
        if (valueDate == undefined || valueDate == "" || valueDate == null) {
            valueDate = kendo.toString(new Date(), "MM-dd-yy");
        }
        if (instrumentPK == undefined || instrumentPK == "" || instrumentPK == null) {
            instrumentPK = 0;
        }

        var CouponSchedulerApprovedURL = window.location.origin + "/Radsoft/CouponScheduler/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + instrumentPK + "/",
          dataSourceApproved = getDataSource(CouponSchedulerApprovedURL);

        //var CouponSchedulerApprovedURL = window.location.origin + "/Radsoft/CouponScheduler/GetDataByMaturityDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + instrumentPK + "/" + valueDate,
        //  dataSourceApproved = getDataSource(CouponSchedulerApprovedURL);

        $("#gridCouponScheduler").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Coupon Scheduler"
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
                { field: "InstrumentPK", title: "InstrumentNo.", filterable: false, hidden: true, width: 100 },
                { field: "InstrumentID", title: "Instrument", width: 300 },
                { field: "CouponFromDate", title: "Date From", width: 150, template: "#= kendo.toString(kendo.parseDate(CouponFromDate), 'MM/dd/yyyy') #" },
                { field: "CouponToDate", title: "Date To", width: 150, template: "#= kendo.toString(kendo.parseDate(CouponToDate), 'MM/dd/yyyy') #" },
                { field: "CouponRate", title: "Rate (%)", width: 150, format: "{0:n4}", template: "#= CouponRate # %", attributes: { style: "text-align:right;" } },
                { field: "CouponDays", title: "Days", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Description", title: "Description", width: 500 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabCouponScheduler").kendoTabStrip({
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
                } else {
                    refresh();
                }
            }
        });
    }

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {        
        alertify.confirm("Are you sure want to close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

});
