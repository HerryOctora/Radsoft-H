$(document).ready(function () {
    document.title = 'FORM CUSTOMER DASHBOARD';
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
        $("#BtnCustomerDashboardRpt").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
    }

    function resetNotification() {
        $("#toggleCSS").attr("href", "../../styles/alertify.default.css");
        alertify.set({
            labels: {
                ok: "OK",
                cancel: "Cancel"
            },
            delay: 4000,
            buttonReverse: false,
            buttonFocus: "ok"
        });
    }

    function initWindow() {
        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeDate,
            value: new Date(),
        });
        
        function OnChangeDate() {
            var _Date = Date.parse($("#Date").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_Date) {
        
                alertify.success("Wrong Format Date DD/MMM/YYYY");
                $("#Date").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }


    }

    var GlobValidator = $("#WinCustomerDashboard").kendoValidator().data("kendoValidator");

    function validateData() {
        

        if (GlobValidator.validate()) {
            //alert("Validation sucess");
            return 1;
        }
        else {
            alertify.error("Validation not Pass");
            return 0;
        }
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
                            
                            SID: { type: "string" },
                            IFUACode: { type: "string" },
                            FundClientID: { type: "string" },
                            InternalCategory: { type: "string" },
                            FundClientName: { type: "string" },
                            AgentName: { type: "string" },
                            BankName: { type: "string" },
                            BankAccNo: { type: "string" },
                            BankAccName: { type: "string" },
                            FundID: { type: "string" },
                            Amount: { type: "number" },
                            Unit: { type: "number" },
                            LastNav: { type: "number" },
                            LastNavDate: { type: "date" },
                            PlaceOfBirth: { type: "string" },
                            DateOfBirth: { type: "date" },
                            IDType: { type: "string" },
                            IDNo: { type: "string" },
                            MotherName: { type: "string" },
                            HighRisk: { type: "string" },
                            RegDate: { type: "date" },
                            OnlineFLag: { type: "string" },
                            CantSub: { type: "boolean" },
                            CantRed: { type: "boolean" },
                            LastKYCUpdate: { type: "date" },
                            Phone: { type: "string" },
                            Email: { type: "string" },
                            Type: { type: "string" },

                         }
                     }
                 }
             });
    }

    function refresh() {
        if (tabindex == undefined || tabindex == 0) {
            initGrid();
        }
    }

    function initGrid() {
        
        $("#gridCustomerDashboardApproved").empty();
        if ($("#Date").val() == null || $("#Date").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var CustomerDashboardApprovedURL = window.location.origin + "/Radsoft/CustomerDashboard/GetDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(CustomerDashboardApprovedURL);

        }

        var grid = $("#gridCustomerDashboardApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Customer Dashboard"
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

                { field: "SID", title: "SID", width: 200 },
                { field: "IFUACode", title: "IFUA Code", width: 200 },
                { field: "FundClientID", title: "Client ID", width: 200 },
                { field: "InternalCategory", title: "Internal Category", width: 200 },
                { field: "FundClientName", title: "Client Name", width: 200 },
                { field: "AgentName", title: "Agent Name", width: 200 },
                { field: "BankName", title: "Bank Name", width: 200 },
                { field: "BankAccNo", title: "Bank Acc No", width: 200 },
                { field: "BankAccName", title: "Bank Acc Name", width: 200 },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "Unit", title: "Unit", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "LastNav", title: "Last Nav", width: 200, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                { field: "LastNavDate", title: "Last Nav Date", width: 200, template: "#= kendo.toString(kendo.parseDate(LastNavDate), 'dd/MMM/yyyy')#" },
                { field: "PlaceOfBirth", title: "Place Of Birth", width: 200 },
                { field: "DateOfBirth", title: "Date Of Birth", width: 200, template: "#= kendo.toString(kendo.parseDate(DateOfBirth), 'dd/MMM/yyyy')#" },
                { field: "IDType", title: "ID Type", width: 200 },
                { field: "IDNo", title: "ID No", width: 200 },
                { field: "MotherName", title: "Mother Name", width: 200 },
                { field: "HighRisk", title: "KYC Risk Profile", width: 200 },
                { field: "KYCRiskAppetite", title: "KYC Risk Appetite", width: 200 },
                { field: "RegDate", title: "Reg Date", width: 200, template: "#= kendo.toString(kendo.parseDate(RegDate), 'dd/MMM/yyyy')#" },
                { field: "OnlineFLag", title: "Online ID", width: 200 },
                { field: "CantSub", title: "Cant Sub", width: 200, template: "#= CantSub ? 'Yes' : 'No' #" },
                { field: "CantRed", title: "Cant Red", width: 200, template: "#= CantRed ? 'Yes' : 'No' #" },
                { field: "LastKYCUpdate", title: "Last KYC Update", width: 200, template: "#= kendo.toString(kendo.parseDate(LastKYCUpdate), 'dd/MMM/yyyy')#" },
                { field: "Phone", title: "Phone", width: 200 },
                { field: "Email", title: "Email", width: 200 },
                { field: "Type", title: "Type", width: 200 },
                
            ]
        }).data("kendoGrid");
        
    }


    $("#BtnRefresh").click(function () {
        refresh();
    });


    $("#BtnCustomerDashboardRpt").click(function () {
        
        alertify.confirm("Are you sure want to Download data ?", function (e) {
            if (e) {
                $.blockUI({});
               
                var Report = {
                    Date: $('#Date').val(),
                    
                };

                _url = window.location.origin + "/Radsoft/Reports/GenerateCustomerDashboardRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id");
               
                $.ajax({
                    url: _url,
                    type: 'POST',
                    data: JSON.stringify(Report),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.unblockUI();
                        //window.location = data
                        var newwindow = window.open(data, '_blank');
                    },
                    error: function (data) {
                        $.unblockUI();
                        alertify.alert(data.responseText);
                    }
                });
            }
        });

    });


});

