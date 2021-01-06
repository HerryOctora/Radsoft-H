$(document).ready(function () {
    document.title = 'FORM DISTRIBUTED INCOME';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    var gridHeight = screen.height - 300;
    var WinListFundClient;
    var WinListPreview;
    var htmlFundClientPK;
    var htmlFundClientID;
    var htmlFundClientName;
    var dirty;
    var upOradd;
    var GlobPolicy;
    var checkedApproved = {};

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

        $("#BtnPosting").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnRevise").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRevise.png"
        });


        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });

        $("#BtnRecalculate").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
        $("#BtnSInvestDistributedIncomeRptBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnPTPByDistributedIncomeRptBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnPostingBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPostingAll.png"
        });
        $("#BtnReviseBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });
        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnPreview").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRefresh.png"
        });
        $("#BtnSInvestDistributedIncomeForSA").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });

        $("#BtnGetNavBySelected").kendoButton({ //Aprroved
            imageUrl: "../../Images/Icon/IcBtnVoucher.png"
        });
    }

    

    function initWindow() {

        $("#ExDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            
            change: OnChangeExDate
        });

        function OnChangeExDate()
        {
            _getNav($("#FundPK").val());
        }



        $("#PaymentDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]

        });



        $("#LastUpdate").kendoDatePicker({
            format: "dd/MMM/yyyy HH:mm:ss",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"]
        });

        $("#ValueDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],

            change: OnChangeValueDate
        });
        function OnChangeValueDate() {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }
            if ($("#ValueDate").data("kendoDatePicker").value() != null) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/CheckHoliday/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            
                            alertify.alert("Date is Holiday, Please Insert Date Correctly!")
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

                //Cek WorkingDays ExDate
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 1,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#ExDate").data("kendoDatePicker").value(new Date(data));

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

                //Cek WorkingDays PaymentDate
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 2,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#PaymentDate").data("kendoDatePicker").value(new Date(data));
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

                _getNav($("#FundPK").val());
         
            }
        }


        $("#DateFrom").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateFrom
        });
        $("#DateTo").kendoDatePicker({
            format: "dd/MMM/yyyy ",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            value: new Date(),
            change: OnChangeDateTo
        });


        function OnChangeDateFrom() {
            
            var currentDate = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateFrom").data("kendoDatePicker").value(new Date());
                return;
            }


            if ($("#DateFrom").data("kendoDatePicker").value() != null) {
                $("#DateTo").data("kendoDatePicker").value($("#DateFrom").data("kendoDatePicker").value());
            }
            refresh();
        }
        function OnChangeDateTo() {
            
            var currentDate = Date.parse($("#DateTo").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!currentDate) {
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }


        win = $("#WinDistributedIncome").kendoWindow({
            height: 1250,
            title: " Distributed Income Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
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
            }
        }).data("kendoWindow");


        WinListPreview = $("#WinListPreview").kendoWindow({
            height: 1000,
            title: "Preview List",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListPreviewClose
        }).data("kendoWindow");

    }


    var GlobValidator = $("#WinDistributedIncome").kendoValidator().data("kendoValidator");

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

    function showDetails(e) {


        var dataItemX;
        if (e == null) {

            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#StatusHeader").text("NEW");
            $("#BtnRevise").hide();
            $("#BtnPosting").hide();
            $("#BtnRecalculate").show();
            $("#TrxInformation").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            $("#BtnPreview").hide();
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridDistributedIncomeApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridDistributedIncomePending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridDistributedIncomeHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            if (dataItemX.Status == 1) {
                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").show();
                $("#BtnRevise").hide();
                $("#BtnOldData").show();
                $("#BtnApproved").show();
                $("#BtnPreview").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").hide();
                $("#BtnRevise").show();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
                $("#BtnPreview").show();
            }

            if (dataItemX.Status == 2 && dataItemX.Revised == 1) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnRecalculate").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();
                $("#BtnPreview").hide();
            }

            if (dataItemX.Status == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnPosting").show();
                $("#BtnRecalculate").show();
                $("#BtnRevise").hide();
                $("#BtnVoid").hide();
                $("#TrxInformation").show();
                $("#BtnOldData").hide();
                $("#BtnPreview").show();

            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#TrxInformation").show();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").hide();
                $("#BtnPreview").hide();

            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnRecalculate").hide();
                $("#BtnOldData").hide();
                $("#BtnPreview").hide();
            }

            dirty = null;

            $("#DistributedIncomePK").val(dataItemX.DistributedIncomePK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#ExDate").data("kendoDatePicker").value(dataItemX.ExDate);
            $("#PaymentDate").data("kendoDatePicker").value(dataItemX.PaymentDate);
            if (dataItemX.Posted == true) {
                $("#Posted").prop('checked', true);
            }

            if (dataItemX.Posted == false && dataItemX.Revised == false) {
                $("#State").removeClass("Posted").removeClass("Revised").addClass("ReadyForPosting");
                $("#State").text("READY FOR POSTING");
                $("#Posted").prop('checked', false);
                $("#Revised").prop('checked', false);
            }

            if (dataItemX.Posted == true) {
                $("#State").removeClass("ReadyForPosting").removeClass("Revised").addClass("Posted");
                $("#State").text("POSTED");
            }

            if (dataItemX.Revised == true) {
                $("#State").removeClass("Posted").removeClass("ReadyForPosting").addClass("Revised");
                $("#State").text("REVISED");
            }

            if (dataItemX.Revised == true) {
                $("#Revised").prop('checked', true);
            }
            $("#PostedBy").text(dataItemX.PostedBy);
            $("#RevisedBy").text(dataItemX.RevisedBy);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            if (dataItemX.RevisedTime == null) {
                $("#RevisedTime").text("");
            } else {
                $("#RevisedTime").text(kendo.toString(kendo.parseDate(dataItemX.RevisedTime), 'MM/dd/yyyy HH:mm:ss'));
            }
            if (dataItemX.PostedTime == null) {
                $("#RevisedTime").text("");
            } else {
                $("#PostedTime").text(kendo.toString(kendo.parseDate(dataItemX.PostedTime), 'MM/dd/yyyy HH:mm:ss'));
            }

            if (dataItemX.Policy == 2 || dataItemX.Policy == 3) {
                $("#trNav").show();
            }
            else {
                $("#trNav").hide();
            }

            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").data("kendoDatePicker").value(dataItemX.LastUpdate);

        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DistributedIncomePolicy",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Policy").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangePolicy,
                    value: setCmbPolicy()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangePolicy() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            else
            {
                GlobPolicy = this.value();
                ShowHideLabel(GlobPolicy, $("#DistributedType").val());
            }
            if ($("#Policy").val() == 2 || $("#Policy").val() == 3) {
                $("#trNav").show();
            }
            else {
                $("#trNav").hide();
            }

           

            $("#FundPK").data("kendoComboBox").value("");
            $("#UnitPosition").data("kendoNumericTextBox").value(0);

        }
        function setCmbPolicy() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Policy == 0) {
                    return "";
                } else {
                    return dataItemX.Policy;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DistributedIncomeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DistributedIncomeType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeDistributedIncomeType,
                    value: setCmbDistributedIncomeType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeDistributedIncomeType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

        }
        function setCmbDistributedIncomeType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DistributedIncomeType == 0) {
                    return "";
                } else {
                    return dataItemX.DistributedIncomeType;
                }
            }
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFundPK,
                    value: setCmbFundPK()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            _getNav($("#FundPK").val());

            $.ajax({
                url: window.location.origin + "/Radsoft/Fund/Fund_GetUnitPositionForDistributedIncome/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#FundPK").val() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {              
                    $("#UnitPosition").data("kendoNumericTextBox").value(data);     
                },
                error: function (data) {
                    $("#UnitPosition").data("kendoNumericTextBox").value(0);
                    alertify.alert("No Data Unit Position");
                }
            });
            $("#DistributedIncomePerUnit").data("kendoNumericTextBox").value(0);




        }

        function setCmbFundPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FundPK == 0) {
                    return "";
                } else {
                    return dataItemX.FundPK;
                }
            }
        }



        $("#Nav").kendoNumericTextBox({
            format: "n4",
            decimals: 4,
            value: setNav(),
        });
        $("#Nav").data("kendoNumericTextBox").enable(false);

        function setNav() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.Nav == 0) {
                    return "";
                } else {
                    return dataItemX.Nav;
                }
            }
        }




        $("#CashAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            change: onchangeCashAmount,
            value: setCashAmount(),
        });

        function onchangeCashAmount() {
            $("#DistributedIncomePerUnit").data("kendoNumericTextBox").value(0);
        }

        function setCashAmount() {
            if (e == null) {
                return 0;
            } else {
                if (dataItemX.CashAmount == 0) {
                    return "";
                } else {
                    return dataItemX.CashAmount;
                }
            }
        }

        $("#DistributedIncomePerUnit").kendoNumericTextBox({
            format: "n12",
            decimals: 12,
            change: onChangeDistributedIncomePerUnit,
            value: setDistributedIncomePerUnit()
        });
        $("#DistributedIncomePerUnit").data("kendoNumericTextBox").enable(false);
        function setDistributedIncomePerUnit() {
            if (e == null) {
                return 0;
            } else {

                return dataItemX.DistributedIncomePerUnit;

            }
        }
        function onChangeDistributedIncomePerUnit() {
            $("#CashAmount").data("kendoNumericTextBox").value(0);
        }


        $("#VariableAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setVariableAmount()
        });

        function setVariableAmount() {
            if (e == null) {
                return 0;
            } else {

                return dataItemX.VariableAmount;

            }
        }



        $("#UnitPosition").kendoNumericTextBox({
            format: "n8",
            decimals: 8,
            value: setUnitPosition(),
        });
        $("#UnitPosition").data("kendoNumericTextBox").enable(false);
        function setUnitPosition() {
            if (e == null) {
                return 0;
            } else {
                if ($("#FundPK").val() == "")
                {
                    _fundPK = dataItemX.FundPK;
                }
                else
                {
                    _fundPK = $("#FundPK").val();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Fund/Fund_GetUnitPositionForDistributedIncome/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#UnitPosition").data("kendoNumericTextBox").value(data);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        }

        $("#DistributedType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            dataSource: [
                { text: "By Total Cash", value: 1 },
                { text: "By Per Unit Per Cash", value: 2 },

            ],
            filter: "contains",
            change: OnChangeDistributedType,
            value: setCmbDistributedType(),
            suggest: true
        });

        function setCmbDistributedType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DistributedType == 0) {
                    return "";
                } else {
                    return dataItemX.DistributedType;
                }
            }
        }



        function OnChangeDistributedType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if($("#DistributedType").val() == 1)
            {
                $("#CashAmount").data("kendoNumericTextBox").value("");
                $("#DistributedIncomePerUnit").data("kendoNumericTextBox").value("");
                $("#CashAmount").data("kendoNumericTextBox").enable(true);
                $("#DistributedIncomePerUnit").data("kendoNumericTextBox").enable(false);
            }
            else
            {
                $("#CashAmount").data("kendoNumericTextBox").value("");
                $("#DistributedIncomePerUnit").data("kendoNumericTextBox").value("");
                $("#DistributedIncomePerUnit").data("kendoNumericTextBox").enable(true);
                $("#CashAmount").data("kendoNumericTextBox").enable(false);
            }

        }

        if (e != null) {
            ShowHideLabel(dataItemX.Policy, dataItemX.DistributedType);
        }


        win.center();
        win.open();
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
    }

    function clearData() {

        $("#DistributedIncomePK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Policy").val("");
        $("#DistributedIncomeType").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#ExDate").data("kendoDatePicker").value(null);
        $("#PaymentDate").data("kendoDatePicker").value(null);
        $("#FundPK").val("");
        $("#FundID").val("");
        $("#Nav").val("");
        $("#CashAmount").val("");
        $("#UnitPosition").val("");
        $("#DistributedIncomePerUnit").val(""); 
        $("#DistributedType").val("");
        $("#VariableAmount").val("");
        $("#Posted").val("");
        $("#Revised").val("");
        $("#PostedBy").val("");
        $("#PostedTime").val("");
        $("#RevisedBy").val("");
        $("#RevisedTime").val("");
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

    function showButton() {
        $("#BtnUpdate").show();
        $("#BtnAdd").show();
        $("#BtnVoid").show();
        $("#BtnReject").show();
        $("#BtnApproved").show();
        $("#BtnUnApproved").show();
        $("#BtnOldData").show();


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
                 aggregate: [{ field: "CashAmount", aggregate: "sum" }],
                 schema: {
                     model: {
                         fields: {
                             DistributedIncomePK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             ValueDate: { type: "date" },
                             ExDate: { type: "date" },
                             PaymentDate: { type: "date" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             Policy: { type: "number" },
                             PolicyDesc: { type: "string" },
                             DistributedIncomeType: { type: "number" },
                             DistributedIncomeTypeDesc: { type: "string" },
                             Nav: { type: "number" },
                             CashAmount: { type: "number" },
                             DistributedIncomePerUnit: { type: "number" },
                             DistributedType: { type: "number" },
                             VariableAmount: { type: "number" },
                             Posted: { type: "boolean" },
                             PostedBy: { type: "string" },
                             PostedTime: { type: "date" },
                             Revised: { type: "boolean" },
                             RevisedBy: { type: "string" },
                             RevisedTime: { type: "date" },
                             EntryUsersID: { type: "string" },
                             EntryTime: { type: "date" },
                             UpdateUsersID: { type: "string" },
                             UpdateTime: { type: "date" },
                             ApprovedUsersID: { type: "string" },
                             ApprovedTime: { type: "date" },
                             VoidUsersID: { type: "string" },
                             VoidTime: { type: "date" },
                             LastUpdate: { type: "date" },
                             Timestamp: { type: "string" }
                         }
                     }
                 }
             });
    }

    function refresh() {
        // initGrid()
        if (tabindex == 0 || tabindex == undefined) {
            initGrid()


        }
        if (tabindex == 1) {
            RecalGridPending();
            var gridPending = $("#gridDistributedIncomePending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            var gridHistory = $("#gridDistributedIncomeHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function gridApprovedOnDataBound() {
        var grid = $("#gridDistributedIncomeApproved").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.Posted == false) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
            } else if (row.Revised == true && row.Posted == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowRevised");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("approvedRowPosted");
            }
        });
    }

    function gridPendingOnDataBound() {
        var grid = $("#gridDistributedIncomePending").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            $('tr[data-uid="' + row.uid + '"] ').addClass("RowPending");
        });
    }

    function initGrid() {

        $("#gridDistributedIncomeApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var DistributedIncomeApprovedURL = window.location.origin + "/Radsoft/DistributedIncome/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceApproved = getDataSource(DistributedIncomeApprovedURL);

        }

        if (_GlobClientCode == '24') {
            var grid = $("#gridDistributedIncomeApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form DistributedIncome"
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
                dataBound: gridApprovedOnDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        field: "Selected",
                        width: 50,
                        template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                        headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                        filterable: true,
                        sortable: false,
                        columnMenu: false
                    },
                    { field: "DistributedIncomePK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ValueDate", title: "Cum Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "ExDate", title: "Ex Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 150 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "Policy", title: "Policy", hidden: true, width: 150 },
                    { field: "PolicyDesc", title: "Policy", width: 200 },
                    { field: "DistributedIncomeType", title: "DistributedIncomeType", hidden: true, width: 150 },
                    { field: "DistributedIncomeTypeDesc", title: "Distributed Income Type", width: 200 },
                    { field: "DistributedType", title: "Distributed Type", hidden: true, width: 150 },
                    { field: "DistributedTypeDesc", title: "Distributed Type", width: 200 },
                    { field: "Nav", title: "Nav", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DistributedIncomePerUnit", title: "DI / Unit", width: 250, format: "{0:n12}", attributes: { style: "text-align:right;" } },
                    { field: "VariableAmount", title: "If > Then Cash", width: 250, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var grid = $("#gridDistributedIncomeApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form DistributedIncome"
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
                dataBound: gridApprovedOnDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        field: "Selected",
                        width: 50,
                        template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                        headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                        filterable: true,
                        sortable: false,
                        columnMenu: false
                    },
                    { field: "DistributedIncomePK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ValueDate", title: "Cum Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "ExDate", title: "Ex Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 150 },
                    { field: "Policy", title: "Policy", hidden: true, width: 150 },
                    { field: "PolicyDesc", title: "Policy", width: 200 },
                    { field: "DistributedIncomeType", title: "DistributedIncomeType", hidden: true, width: 150 },
                    { field: "DistributedIncomeTypeDesc", title: "Distributed Income Type", width: 200 },
                    { field: "DistributedType", title: "Distributed Type", hidden: true, width: 150 },
                    { field: "DistributedTypeDesc", title: "Distributed Type", width: 200 },
                    { field: "Nav", title: "Nav", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DistributedIncomePerUnit", title: "DI / Unit", width: 250, format: "{0:n12}", attributes: { style: "text-align:right;" } },
                    { field: "VariableAmount", title: "If > Then Cash", width: 250, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

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

        function selectDataApproved(e) {



            var rowA = $(e.currentTarget).closest("tr");
            var grid = $("#gridDistributedIncomeApproved").data("kendoGrid");
            var dataItemX = grid.dataItem(rowA);
            var _DistributedIncomePK = dataItemX.DistributedIncomePK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _DistributedIncomePK);

            checkedApproved[_DistributedIncomePK] = _checked;
            if (_checked) {
                rowA.addClass("k-state-selected");
            } else {
                rowA.removeClass("k-state-selected");
            }
        }


        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabDistributedIncome").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },

            activate: function (e) {

                tabindex = $(e.item).index();
                if ($.inArray(tabindex, loadedTabs) == -1) {
                    loadedTabs.push(tabindex);
                    if (tabindex == 1) {
                        RecalGridPending();
                    }
                    if (tabindex == 2) {
                        RecalGridHistory();
                    }
                } else {
                    refresh();
                }
            }
        });
        ResetButtonBySelectedData();
        $("#BtnVoidBySelected").show();
        $("#BtnPostingBySelected").show();
        $("#BtnReviseBySelected").show();
        $("#BtnSInvestDistributedIncomeRptBySelected").show();
        if (_GlobClientCode == '03') {
            $("#BtnPTPByDistributedIncomeRptBySelected").show();
        }
        $("#BtnSInvestDistributedIncomeForSA").show();
        $("#BtnGetNavBySelected").show();


    }


    function SelectDeselectData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DistributedIncome/" + _a + "/" + _b,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var _msg;
                if (_a) {
                    _msg = "Select Data ";
                } else {
                    _msg = "DeSelect Data ";
                }
                alertify.set('notifier','position', 'top-center'); alertify.success(_msg + _b);
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }
    function SelectDeselectAllData(_a, _b) {
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/DistributedIncome/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
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

    function RecalGridPending() {
        $("#gridDistributedIncomePending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var DistributedIncomePendingURL = window.location.origin + "/Radsoft/DistributedIncome/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourcePending = getDataSource(DistributedIncomePendingURL);

        }

        if (_GlobClientCode == '24') {
            var grid = $("#gridDistributedIncomePending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form DistributedIncome"
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
                dataBound: gridPendingOnDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        field: "Selected",
                        width: 50,
                        template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                        headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                        filterable: true,
                        sortable: false,
                        columnMenu: false
                    },
                    { field: "DistributedIncomePK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ValueDate", title: "Cum Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "ExDate", title: "Ex Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 150 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "Policy", title: "Policy", hidden: true, width: 150 },
                    { field: "PolicyDesc", title: "Policy", width: 200 },
                    { field: "DistributedIncomeType", title: "DistributedIncomeType", hidden: true, width: 150 },
                    { field: "DistributedIncomeTypeDesc", title: "Distributed Income Type", width: 200 },
                    { field: "DistributedType", title: "Distributed Type", hidden: true, width: 150 },
                    { field: "DistributedTypeDesc", title: "Distributed Type", width: 200 },
                    { field: "Nav", title: "Nav", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DistributedIncomePerUnit", title: "DI / Unit", width: 250, format: "{0:n12}", attributes: { style: "text-align:right;" } },
                    { field: "VariableAmount", title: "If > Then Cash", width: 250, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }
        else {
            var grid = $("#gridDistributedIncomePending").kendoGrid({
                dataSource: dataSourcePending,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form DistributedIncome"
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
                dataBound: gridPendingOnDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    //{ command: { text: "Batch", click: showBatch }, title: " ", width: 80 },
                    {
                        field: "Selected",
                        width: 50,
                        template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                        headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                        filterable: true,
                        sortable: false,
                        columnMenu: false
                    },
                    { field: "DistributedIncomePK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ValueDate", title: "Cum Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "ExDate", title: "Ex Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 150 },
                    { field: "Policy", title: "Policy", hidden: true, width: 150 },
                    { field: "PolicyDesc", title: "Policy", width: 200 },
                    { field: "DistributedIncomeType", title: "DistributedIncomeType", hidden: true, width: 150 },
                    { field: "DistributedIncomeTypeDesc", title: "Distributed Income Type", width: 200 },
                    { field: "DistributedType", title: "Distributed Type", hidden: true, width: 150 },
                    { field: "DistributedTypeDesc", title: "Distributed Type", width: 200 },
                    { field: "Nav", title: "Nav", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DistributedIncomePerUnit", title: "DI / Unit", width: 250, format: "{0:n12}", attributes: { style: "text-align:right;" } },
                    { field: "VariableAmount", title: "If > Then Cash", width: 250, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "Last Update", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            }).data("kendoGrid");
        }

        $("#SelectedAllPending").change(function () {

            var _checked = this.checked;
            var _msg;
            if (_checked) {
                _msg = "Check All";
            } else {
                _msg = "UnCheck All"
            }
            alertify.alert(_msg);
            SelectDeselectAllData(_checked, "Pending");

        });

        grid.table.on("click", ".cSelectedDetailPending", selectDataPending);

        function selectDataPending(e) {


            var grid = $("#gridDistributedIncomePending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _DistributedIncomePK = dataItemX.DistributedIncomePK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _DistributedIncomePK);

        }

        ResetButtonBySelectedData();

        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnSInvestDistributedIncomeRptBySelected").show();
        if (_GlobClientCode == '03') {
            $("#BtnPTPByDistributedIncomeRptBySelected").show();
        }
        $("#BtnSInvestDistributedIncomeForSA").show();

    }

    function RecalGridHistory() {

        $("#gridDistributedIncomeHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else {

            var DistributedIncomeHistoryURL = window.location.origin + "/Radsoft/DistributedIncome/GetDataByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                dataSourceHistory = getDataSource(DistributedIncomeHistoryURL);

        }

        if (_GlobClientCode == '24') {
            $("#gridDistributedIncomeHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form DistributedIncome"
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
                dataBound: gridHistoryDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "DistributedIncomePK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ValueDate", title: "Cum Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "ExDate", title: "Ex Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 150 },
                    { field: "FundName", title: "Fund Name", width: 200 },
                    { field: "Policy", title: "Policy", hidden: true, width: 150 },
                    { field: "PolicyDesc", title: "Policy", width: 200 },
                    { field: "DistributedIncomeType", title: "DistributedIncomeType", hidden: true, width: 150 },
                    { field: "DistributedIncomeTypeDesc", title: "Distributed Income Type", width: 200 },
                    { field: "DistributedType", title: "Distributed Type", hidden: true, width: 150 },
                    { field: "DistributedTypeDesc", title: "Distributed Type", width: 200 },
                    { field: "Nav", title: "Nav", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DistributedIncomePerUnit", title: "DI / Unit", width: 250, format: "{0:n12}", attributes: { style: "text-align:right;" } },
                    { field: "VariableAmount", title: "If > Then Cash", width: 250, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }
        else {
            $("#gridDistributedIncomeHistory").kendoGrid({
                dataSource: dataSourceHistory,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form DistributedIncome"
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
                dataBound: gridHistoryDataBound,
                toolbar: ["excel"],
                columns: [
                    { command: { text: "Show", click: showDetails }, title: " ", width: 80 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "DistributedIncomePK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ValueDate", title: "Cum Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'dd/MM/yyyy')#" },
                    { field: "ExDate", title: "Ex Date", hidden: true, width: 150, template: "#= kendo.toString(kendo.parseDate(ExDate), 'dd/MM/yyyy')#" },
                    { field: "PaymentDate", title: "Payment Date", width: 150, template: "#= kendo.toString(kendo.parseDate(PaymentDate), 'dd/MM/yyyy')#" },
                    { field: "FundID", title: "Fund ID", width: 150 },
                    { field: "Policy", title: "Policy", hidden: true, width: 150 },
                    { field: "PolicyDesc", title: "Policy", width: 200 },
                    { field: "DistributedIncomeType", title: "DistributedIncomeType", hidden: true, width: 150 },
                    { field: "DistributedIncomeTypeDesc", title: "Distributed Income Type", width: 200 },
                    { field: "DistributedType", title: "Distributed Type", hidden: true, width: 150 },
                    { field: "DistributedTypeDesc", title: "Distributed Type", width: 200 },
                    { field: "Nav", title: "Nav", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                    { field: "CashAmount", title: "Cash Amount", width: 250, footerTemplate: "<div style='text-align: right'>#=kendo.toString(sum,'n0')#</div>", format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "DistributedIncomePerUnit", title: "DI / Unit", width: 250, format: "{0:n12}", attributes: { style: "text-align:right;" } },
                    { field: "VariableAmount", title: "If > Then Cash", width: 250, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    { field: "FundPK", title: "Fund", hidden: true, width: 200 },
                    { field: "Posted", title: "Posted", width: 150, template: "#= Posted ? 'Yes' : 'No' #" },
                    { field: "Revised", title: "Revised", width: 150, template: "#= Revised ? 'Yes' : 'No' #" },
                    { field: "PostedBy", title: "Posted By", width: 200 },
                    { field: "PostedTime", title: "Posted Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "RevisedBy", title: "Revised By", width: 200 },
                    { field: "RevisedTime", title: "Revised Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "Update ID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "Approved ID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "Void ID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }


        ResetButtonBySelectedData();
    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnVoidBySelected").hide();
        $("#BtnPostingBySelected").hide();
        $("#BtnReviseBySelected").hide();
        $("#BtnSInvestDistributedIncomeRptBySelected").hide();
        $("#BtnPTPByDistributedIncomeRptBySelected").hide();
        $("#BtnSInvestDistributedIncomeForSA").hide();
        $("#BtnGetNavBySelected").hide();
    }

    function gridHistoryDataBound() {
        var grid = $("#gridDistributedIncomeHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            }
            else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
    }

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
            if (e) {
                win.close();
                alertify.alert("Close Detail");
            }
        });
    });

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnNew").click(function () {
        showDetails(null);
    });

    //add,update,old data,approve,reject 
    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {
            if ($('#DistributedType').val() == 1 && $('#DistributedIncomePerUnit').val() == 0) {
                alertify.alert("Please Recalculate First!");
                return;
            }
            else if ($('#DistributedType').val() == 2 && $('#CashAmount').val() == 0) {
                alertify.alert("Please Recalculate First!");
                return;
            }
            else {

                alertify.confirm("Are you sure want to Add data?", function (e) {
                    if (e) {
                        var DistributedIncome = {
                            DistributedIncomePK: $('#DistributedIncomePK').val(),
                            HistoryPK: $('#HistoryPK').val(),
                            ValueDate: $('#ValueDate').val(),
                            ExDate: $('#ExDate').val(),
                            PaymentDate: $('#PaymentDate').val(),
                            FundPK: $('#FundPK').val(),
                            Policy: $('#Policy').val(),
                            DistributedIncomeType: $('#DistributedIncomeType').val(),
                            Nav: $('#Nav').val(),
                            CashAmount: $('#CashAmount').val(),
                            DistributedIncomePerUnit: $('#DistributedIncomePerUnit').val(),
                            DistributedType: $('#DistributedType').val(),
                            VariableAmount: $('#VariableAmount').val(),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/DistributedIncome/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DistributedIncome_I",
                            type: 'POST',
                            data: JSON.stringify(DistributedIncome),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                alertify.alert(data);
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

        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            if ($('#DistributedType').val() == 1 && $('#DistributedIncomePerUnit').val() == 0) {
                alertify.alert("Please Recalculate First!");
                return;
            }
            else if ($('#DistributedType').val() == 2 && $('#CashAmount').val() == 0) {
                alertify.alert("Please Recalculate First!");
                return;
            }
            else {

                alertify.prompt("Are you sure want to Update , please give notes:", "", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DistributedIncomePK").val() + "/" + $("#HistoryPK").val() + "/" + "DistributedIncome",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                    var DistributedIncome = {
                                        DistributedIncomePK: $('#DistributedIncomePK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        ValueDate: $('#ValueDate').val(),
                                        ExDate: $('#ExDate').val(),
                                        PaymentDate: $('#PaymentDate').val(),
                                        FundPK: $('#FundPK').val(),
                                        Policy: $('#Policy').val(),
                                        DistributedIncomeType: $('#DistributedIncomeType').val(),
                                        Nav: $('#Nav').val(),
                                        CashAmount: $('#CashAmount').val(),
                                        DistributedIncomePerUnit: $('#DistributedIncomePerUnit').val(),
                                        DistributedType: $('#DistributedType').val(),
                                        VariableAmount: $('#VariableAmount').val(),
                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/DistributedIncome/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DistributedIncome_U",
                                        type: 'POST',
                                        data: JSON.stringify(DistributedIncome),
                                        contentType: "application/json;charset=utf-8",
                                        success: function (data) {
                                            alertify.alert(data);
                                            win.close();
                                            refresh();
                                        },
                                        error: function (data) {
                                            alertify.alert(data.responseText);
                                        }
                                    });

                                } else {
                                    alertify.alert("Data has been Changed by other user, Please check it first!");
                                    win.close();
                                    refresh();
                                }
                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    }
                });
            }

        }

    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DistributedIncomePK").val() + "/" + $("#HistoryPK").val() + "/" + "DistributedIncome",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                    $("#gridOldData").empty();

                    $("#gridOldData").kendoGrid({
                        dataSource: {
                            transport:
                                    {
                                        read:
                                            {
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "DistributedIncome" + "/" + $("#DistributedIncomePK").val(),
                                                dataType: "json"
                                            }
                                    },
                            batch: true,
                            cache: false,
                            error: function (e) {
                                alert(e.errorThrown + " - " + e.xhr.responseText);
                                this.cancelChanges();
                            },
                            pageSize: 10,
                            schema: {
                                model: {
                                    fields: {
                                        FieldName: { type: "string" },
                                        OldValue: { type: "string" },
                                        NewValue: { type: "string" },
                                        Similarity: { type: "number" },
                                    }
                                }
                            }
                        },
                        filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                        columnMenu: false,
                        pageable: {
                            input: true,
                            numeric: false
                        },
                        height: 470,
                        reorderable: true,
                        sortable: true,
                        resizable: true,
                        dataBound: gridOldDataDataBound,
                        columns: [
                            { field: "FieldName", title: "FieldName", width: 300 },
                            { field: "OldValue", title: "OldValue", width: 300 },
                            { field: "NewValue", title: "NewValue", width: 300 },
                            { field: "Similarity", title: "Similarity", width: 120 }
                        ]
                    });
                    winOldData.center();
                    winOldData.open();
                } else {
                    alertify.alert("Data has been Changed by other user, Please check it first!");
                    win.close();
                    refresh();
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    function gridOldDataDataBound(e) {
        var grid = e.sender;
        if (grid.dataSource.total() == 0) {
            var colCount = grid.columns.length;
            $(e.sender.wrapper)
                .find('tbody')
                .append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data">no data</td></tr>');
        }
    };

    $("#BtnApproved").click(function () {
        

        alertify.confirm("Are you sure want to Approved data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DistributedIncomePK").val() + "/" + $("#HistoryPK").val() + "/" + "DistributedIncome",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var DistributedIncome = {
                                DistributedIncomePK: $('#DistributedIncomePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DistributedIncome/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DistributedIncome_A",
                                type: 'POST',
                                data: JSON.stringify(DistributedIncome),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh();

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    

    $("#BtnVoid").click(function () {
        
        alertify.confirm("Are you sure want to Void data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DistributedIncomePK").val() + "/" + $("#HistoryPK").val() + "/" + "DistributedIncome",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var DistributedIncome = {
                                DistributedIncomePK: $('#DistributedIncomePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DistributedIncome/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DistributedIncome_V",
                                type: 'POST',
                                data: JSON.stringify(DistributedIncome),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh();

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#DistributedIncomePK").val() + "/" + $("#HistoryPK").val() + "/" + "DistributedIncome",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var DistributedIncome = {
                                DistributedIncomePK: $('#DistributedIncomePK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DistributedIncome/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DistributedIncome_R",
                                type: 'POST',
                                data: JSON.stringify(DistributedIncome),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    win.close();
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        } else {
                            alertify.alert("Data has been Changed by other user, Please check it first!");
                            win.close();
                            refresh();
                        }
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


    $("#BtnApproveBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/DistributedIncome/ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnRejectBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Reject by Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/DistributedIncome/RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnVoidBySelected").click(function (e) {
        var All = 0;
        All = [];
        for (var i in checkedApproved) {
            if (checkedApproved[i]) {
                All.push(i);
            }
        }

        var ArrayFundFrom = All;
        var stringDistributedIncomeSelected = '';

        for (var i in ArrayFundFrom) {
            stringDistributedIncomeSelected = stringDistributedIncomeSelected + ArrayFundFrom[i] + ',';

        }
        stringDistributedIncomeSelected = stringDistributedIncomeSelected.substring(0, stringDistributedIncomeSelected.length - 1)

        if (stringDistributedIncomeSelected == "") {
            alertify.alert("There's No Selected Data")
            return;
        }
        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                console.log(stringDistributedIncomeSelected);
                var DistributedIncome = {
                    UnitRegistrySelected: stringDistributedIncomeSelected,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/DistributedIncome/ValidateVoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'POST',
                    data: JSON.stringify(DistributedIncome),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data == false) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/DistributedIncome/VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                type: 'GET',
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert(data);
                                    refresh();
                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        }

                        else {
                            alertify.alert("Distributed Income Already Posted");
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

    function formatNumber(x) {
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts.join(".");
    }

    $("input[type='text']").change(function () {
        dirty = true;
    });

    $("input[type='number']").change(function () {
        dirty = true;
    });

  


    $("#BtnRecalculate").click(function (e) {
        if ($("#ExDate").val() == null || $("#ExDate").val() == "") {
            alertify.alert("Please Fill Ex Date First");
            return;
        }
        else if ($("#FundPK").val() == null || $("#FundPK").val() == "") {
            alertify.alert("Please Fill Fund First");
            return;
        }
        //if ($("#Policy").val() == 1) {
        //    _distributedIncomePerUnit = $("#CashAmount").val() / $("#UnitPosition").val();
        //    $("#DistributedIncomePerUnit").data("kendoNumericTextBox").value(_distributedIncomePerUnit);
        //}
        //else
        //{
        //    _distributedIncomePerUnit = $("#CashAmount").val() / ($("#UnitPosition").val() * $("#Nav").val());
        //    $("#DistributedIncomePerUnit").data("kendoNumericTextBox").value(_distributedIncomePerUnit);
        //}
        if ($("#DistributedType").val() == 1) {
            _distributedIncomePerUnit = $("#CashAmount").val() / $("#UnitPosition").val();
            $("#DistributedIncomePerUnit").data("kendoNumericTextBox").value(_distributedIncomePerUnit);
        }
        else {
            _distributedIncomePerUnit = $("#DistributedIncomePerUnit").val() * $("#UnitPosition").val();
            $("#CashAmount").data("kendoNumericTextBox").value(_distributedIncomePerUnit);
        }

        refresh();
        //alertify.alert("Recalculate Done");

    });


    function _getNav(_fundPK) {
        if ($("#FundPK").val() == 0 || $("#FundPK").val() == null || $("#ExDate").val() == null) {
            $("#Nav").data("kendoNumericTextBox").value(0);
        }
        else {
            $.ajax({
                url: window.location.origin + "/Radsoft/CloseNav/GetCloseNavByDateAndFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fundPK + "/" + kendo.toString($("#ExDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/1",
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#Nav").data("kendoNumericTextBox").value(data);
                },
                error: function (data) {
                    $("#Nav").data("kendoNumericTextBox").value(0);
                    alertify.alert("No Data Nav");
                }
            });

        }


    }


    $("#BtnSInvestDistributedIncomeRptBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Download S-Invest Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/DistributedIncome/SInvestDistributedIncomeRptBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#downloadRadsoftFile").attr("href", data);
                        $("#downloadRadsoftFile").attr("download", "RadsoftSInvestFile_DistributedIncome.txt");
                        document.getElementById("downloadRadsoftFile").click();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


    $("#BtnPostingBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Posting by Selected Data ?", function (e) {
            if (e) {
                if (kendo.toString($("#DateFrom").data("kendoDatePicker").value(), 'MM-dd-yy') == kendo.toString($("#DateTo").data("kendoDatePicker").value(), 'MM-dd-yy')) {
                    $.blockUI();
                    $.ajax({
                        url: window.location.origin + "/Radsoft/DistributedIncome/ValidatePostingNAVBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == false) {

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DistributedIncome/ValidatePostingBySelected/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if (data == false) {

                                            $.ajax({
                                                url: window.location.origin + "/Radsoft/DistributedIncome/ValidateGenerateCheckSubsRedemp/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                type: 'GET',
                                                contentType: "application/json;charset=utf-8",
                                                success: function (data) {
                                                    if (data == false) {

                                                        $.ajax({
                                                            url: window.location.origin + "/Radsoft/DistributedIncome/PostingBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                                                            type: 'GET',
                                                            contentType: "application/json;charset=utf-8",
                                                            success: function (data) {
                                                                $.unblockUI();
                                                                alertify.alert(data);
                                                                refresh();
                                                            },
                                                            error: function (data) {
                                                                alertify.alert(data.responseText);
                                                            }
                                                        });
                                                    } else {
                                                        alertify.alert("Please Posting Subscription / Redemption Yesterday First!");
                                                        $.unblockUI();
                                                    }
                                                },
                                                error: function (data) {
                                                    alertify.alert(data.responseText);
                                                    $.unblockUI();
                                                }
                                            });

                                        } else {
                                            alertify.alert("Posting Cancel, Must be Generate End Day Trails Yesterday First / Distributed Income Already Posting");
                                            $.unblockUI();
                                        }
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                        $.unblockUI();
                                    }
                                });

                            } else {
                                alertify.alert("NAV Must Ready First !");
                                $.unblockUI();
                            }
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                            $.unblockUI();
                        }
                    });
                }
                else {
                    alertify.alert("Please Posting Distributed Income by Day");
                }

            }
        });
    });

    $("#BtnReviseBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Revise by Selected Data ?", function (e) {
            if (e) {
                if ($("#DateFrom").data("kendoDatePicker").value() == $("#DateTo").data("kendoDatePicker").value()) {
                    $.blockUI();

                    $.ajax({
                        url: window.location.origin + "/Radsoft/DistributedIncome/ReviseBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $.unblockUI();
                            alertify.alert(data);
                            refresh();
                        },
                        error: function (data) {
                            alertify.alert(data.responseText);
                        }
                    });

                }
                else {
                    alertify.alert("Please Revise Distributed Income by Day");
                }

            }
        });
    });

    $("#BtnPreview").click(function () {
        if ($("#StatusHeader").val() == "PENDING")
        {
            alertify.alert("Please Approve Distributed Income First !");
        }
        else

        {
            initListPreview();

            WinListPreview.center();
            WinListPreview.open();
        }



    });


    function initListPreview() {
        var dsListPreviewDistributedIncome = InitDataPreviewDistributedIncome();
        if ($("#Policy").data("kendoComboBox").value() == 1) {
            var gridPreviewDistributedIncome = $("#gridPreviewDistributedIncome").kendoGrid({
                dataSource: dsListPreviewDistributedIncome,
                height: 500,
                scrollable: {
                    virtual: true
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                reorderable: true,
                sortable: true,
                resizable: true,
                columns: [
                   { field: "FundClientName", title: "Client Name", width: 150 },
                   { field: "IFUA", title: "IFUA", width: 150 },
                   { field: "SID", title: "SID", width: 150 },
                   { field: "BegUnit", title: "Beginning Unit", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                   { field: "DistributeCash", title: "Distributed Cash", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 120 },
                ]
            }).data("kendoGrid");
        }
        else if ($("#Policy").data("kendoComboBox").value() == 2) {
            var gridPreviewDistributedIncome = $("#gridPreviewDistributedIncome").kendoGrid({
                dataSource: dsListPreviewDistributedIncome,
                height: 500,
                scrollable: {
                    virtual: true
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                reorderable: true,
                sortable: true,
                resizable: true,
                columns: [
                   { field: "FundClientName", title: "Client Name", width: 150 },
                   { field: "IFUA", title: "IFUA", width: 150 },
                   { field: "SID", title: "SID", width: 150 },
                   { field: "BegUnit", title: "Beginning Unit", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                   { field: "DistributeUnit", title: "Distributed Unit", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                ]
            }).data("kendoGrid");
        }
        else {
            var gridPreviewDistributedIncome = $("#gridPreviewDistributedIncome").kendoGrid({
                dataSource: dsListPreviewDistributedIncome,
                height: 500,
                scrollable: {
                    virtual: true
                },
                filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
                columnMenu: false,
                reorderable: true,
                sortable: true,
                resizable: true,
                columns: [
                   { field: "FundClientName", title: "Client Name", width: 150 },
                   { field: "IFUA", title: "IFUA", width: 150 },
                   { field: "SID", title: "SID", width: 150 },
                   { field: "BegUnit", title: "Beginning Unit", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                   { field: "DistributeCash", title: "Distributed Cash", format: "{0:n2}", attributes: { style: "text-align:right;" }, width: 120 },
                   { field: "DistributeUnit", title: "Distributed Unit", format: "{0:n4}", attributes: { style: "text-align:right;" }, width: 120 },
                ]
            }).data("kendoGrid");
        }

    }

    function onWinListPreviewClose() {
        $("#gridPreviewDistributedIncome").empty();
    }




    function InitDataPreviewDistributedIncome() {

        return new kendo.data.DataSource(

                  {

                      transport:
                              {

                                  read:
                                      {

                                          url: window.location.origin + "/Radsoft/DistributedIncome/InitDataPreviewDistributedIncomeByFundPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ExDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FundPK").val() + "/" + $("#Policy").val(),
                                          dataType: "json"
                                      }
                              },
                      batch: true,
                      cache: false,
                      error: function (e) {
                          alertify.alert("There's No Data!");
                          this.cancelChanges();
                      },
                      pageSize: 10000,
                      schema: {
                          model: {
                              fields: {

                                    FundClientName: { type: "string" },
                                    IFUA: { type: "string" },
                                    SID: { type: "string" },
                                    BegUnit: { type: "number" },
                                    DistributeCash: { type: "number" },
                                    DistributeUnit: { type: "number" },

                              }
                          }
                      }
                  });
    }


    $("#BtnPosting").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;

        alertify.confirm("Are you sure want to Posting Distributed Income?", function (e) {
            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alertify.alert("Distributed Income Already Posted / Revised, Posting Cancel");
            } else {
                if (e) {

                    $.blockUI();

                    $.ajax({
                        url: window.location.origin + "/Radsoft/DistributedIncome/ValidatePosting/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ExDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#FundPK").val(),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                var DistributedIncome = {
                                    DistributedIncomePK: $('#DistributedIncomePK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    PostedBy: sessionStorage.getItem("user"),
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/DistributedIncome/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DistributedIncome_Posting",
                                    type: 'POST',
                                    data: JSON.stringify(DistributedIncome),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#State").removeClass("ReadyForPosting").removeClass("UnPosted").addClass("Posted");
                                        $("#State").text("POSTED");
                                        $("#PostedBy").text(sessionStorage.getItem("user"));
                                        $("#Posted").prop('checked', true);
                                        $("#UnPosted").prop('checked', false);
                                        $.unblockUI();
                                        alertify.alert(data);
                                        win.close();
                                        refresh();
                                    },
                                    error: function (data) {
                                        $.unblockUI();
                                        alert(data.responseText);
                                    }
                                });
                            } else {
                                alertify.alert("There's no data Fund Client Position in this day!");
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



    $("#BtnRevise").click(function (e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;

        alertify.confirm("Are you sure want to Revise Distributed Income?", function (e) {
            if ($("#State").text() == "Revised") {
                alert("Client Subscription Already Posted / Revised, Revised Cancel");
            } else {
                if (e) {

                    $.blockUI();


                    var DistributedIncome = {
                        DistributedIncomePK: $('#DistributedIncomePK').val(),
                        HistoryPK: $('#HistoryPK').val(),
                        RevisedBy: sessionStorage.getItem("user"),
                    };
                    $.ajax({
                        url: window.location.origin + "/Radsoft/DistributedIncome/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "DistributedIncome_Revise",
                        type: 'POST',
                        data: JSON.stringify(DistributedIncome),
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            $("#State").removeClass("ReadyForPosting").removeClass("Posted").addClass("Reserved");
                            $("#State").text("Revised");
                            $("#RevisedBy").text(sessionStorage.getItem("user"));
                            $("#Revised").prop('checked', true);
                            $("#Posted").prop('checked', false);
                            $.unblockUI();
                            alertify.alert(data);
                            win.close();
                            refresh();
                        },
                        error: function (data) {
                            $.unblockUI();
                            alert(data.responseText);
                        }
                    });

                }
            }
        });

    });

    function ShowHideLabel(_policy, _type) {

        if (_type == 1) {
            $("#CashAmount").data("kendoNumericTextBox").enable(true);
            $("#DistributedIncomePerUnit").data("kendoNumericTextBox").enable(false);
        }
        else {
            $("#DistributedIncomePerUnit").data("kendoNumericTextBox").enable(true);
            $("#CashAmount").data("kendoNumericTextBox").enable(false);
        }

        if (_policy == 3) {
            $("#LblVariableAmount").show();
        }
        else {
            $("#LblVariableAmount").hide();
        }

    }


    $("#BtnSInvestDistributedIncomeForSA").click(function (e) {

        alertify.confirm("Are you sure want to Download S-Invest Data For SA ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/DistributedIncome/SInvestDistributedIncomeForSA/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#downloadRadsoftFile").attr("href", data);
                        $("#downloadRadsoftFile").attr("download", "RadsoftSInvestFile_DistributedIncomeForSA.txt");
                        document.getElementById("downloadRadsoftFile").click();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


    $("#BtnGetNavBySelected").click(function (e) {
      

        alertify.confirm("Are you sure want to Get Nav by Selected Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/DistributedIncome/GetNavBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/UnitRegistry_GetNavBySelected",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        refresh();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });

    $("#BtnPTPByDistributedIncomeRptBySelected").click(function (e) {

        alertify.confirm("Are you sure want to Download S-Invest Data ?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/DistributedIncome/PTPByDistributedIncomeRptBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#downloadRadsoftFile").attr("href", data);
                        $("#downloadRadsoftFile").attr("download", "RadsoftSInvestFile_DistributedIncome.txt");
                        document.getElementById("downloadRadsoftFile").click();

                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });
    });


});
