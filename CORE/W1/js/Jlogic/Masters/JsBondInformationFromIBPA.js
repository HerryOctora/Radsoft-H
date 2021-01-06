$(document).ready(function () {
    document.title = 'FORM Bond Information From IBPA';
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

        $("#BtnImportBondInformationFromIBPA").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });

        $("#BtnUpdateToInstrument").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnUpdate.png"
        });

    }

    function initWindow() {
        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDateFrom,
            value: new Date(),
        });


        //sblmnya dsni



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
                             BondInformationFromIBPAPK: { type: "number" },
                             Date: { type: "date" },
                             Series: { type: "string" },
                             ISINCode: { type: "string" },
                             BondName: { type: "string" },
                             Rating: { type: "string" },
                             CouponRate: { type: "number" },
                             MaturityDate: { type: "date" },
                             TTM: { type: "number" },
                             TodayYield: { type: "number" },
                             TodayLowPrice: { type: "number" },
                             TodayFairPrice: { type: "number" },
                             TodayHighPrice: { type: "number" },
                             Change: { type: "number" },
                             YesterdayYield: { type: "number" },
                             YesterdayPrice: { type: "number" },
                             LastWeekYield: { type: "number" },
                             LastWeekPrice: { type: "number" },

                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        initGrid();
    }

    function OnChangeDateFrom() {
        var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
        //Check if Date parse is successful
        if (!_DateFrom) {

            alertify.alert("Wrong Format Date DD/MMM/YYYY");
            $("#DateFrom").data("kendoDatePicker").value(new Date());
            return;
        }
        refresh();
    }

    function initGrid() {

        $("#gridBondInformationFromIBPAApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var BondInformationFromIBPAApprovedURL = window.location.origin + "/Radsoft/BondInformationFromIBPA/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(BondInformationFromIBPAApprovedURL);

        }

        var grid = $("#gridBondInformationFromIBPAApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Bond Information From IBPA"
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
                //{ field: "BondInformationFromIBPAPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 150, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "Series", title: "Series", width: 150 },
                { field: "ISINCode", title: "ISINCode", width: 150 },
                { field: "BondName", title: "BondName", width: 500 },
                { field: "Rating", title: "Rating", width: 150 },
                { field: "CouponRate", title: "CouponRate", width: 150 },
                { field: "MaturityDate", title: "MaturityDate", width: 150, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'dd/MMM/yyyy')#" },
                { field: "TTM", title: "TTM", width: 150 },
                { field: "TodayYield", title: "TodayYield", width: 150 },
                { field: "TodayLowPrice", title: "TodayLowPrice", width: 150 },
                { field: "TodayFairPrice", title: "TodayFairPrice", width: 150 },
                { field: "TodayHighPrice", title: "TodayHighPrice", width: 150 },
                { field: "Change", title: "Change", width: 150 },
                { field: "YesterdayYield", title: "YesterdayYield", width: 150 },
                { field: "YesterdayPrice", title: "YesterdayPrice", width: 150 },
                { field: "LastWeekYield", title: "LastWeekYield", width: 150 },
                { field: "LastWeekPrice", title: "LastWeekPrice", width: 150 },
            ]
        }).data("kendoGrid");

    }

    $("#BtnRefresh").click(function () {
        refresh();
    });



    $("#BtnImportBondInformationFromIBPA").click(function () {
        document.getElementById("FileImportBondInformationFromIBPA").click();
    });

    $("#FileImportBondInformationFromIBPA").change(function () {


        $.blockUI({});
        var data = new FormData();
        var files = $("#FileImportBondInformationFromIBPA").get(0).files;

        var fileSize = this.files[0].size / 1024 / 1024;

        //if (fileSize > _GlobMaxFileSizeInMB) {
        //    $.unblockUI();
        //    alertify.alert("Max Upload Size is: " + _GlobMaxFileSizeInMB + "MB")
        //    return;
        //}


        var _d = files[0].name.substring(17, 25);
        if (files.length > 0) {
            data.append("BondInformationFromIBPA", files[0]);
            $.ajax({
                url: window.location.origin + "/Radsoft/Upload/UploadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "BondInformationFromIBPA_Import/0",
                type: 'POST',
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.unblockUI();
                    $("#FileImportBondInformationFromIBPA").val("");
                    initGrid();
                    alertify.alert("Import Done");

                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                    $("#FileImportBondInformationFromIBPA").val("");
                }
            });
        } else {
            alertify.alert("Please Choose Correct File");
            $("#FileImportBondInformationFromIBPA").val("");
        }


    });

    $("#BtnUpdateToInstrument").click(function () {
        var _date = kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }

        alertify.confirm("Are you sure want Update To Instrument for " + _date, function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/BondInformationFromIBPA/UpdateToInstrument/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        win.close();
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
});