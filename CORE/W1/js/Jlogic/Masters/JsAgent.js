$(document).ready(function () {
    document.title = 'FORM AGENT';
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


    if (_GlobClientCode == "32") {
        $("#LblWAPERDNo").show();
        $("#LblWAPERDExpiredDate").show();
        $("#LblAAJINo").show();
        $("#LblAAJIExpiredDate").show();
    }
    else {
        $("#LblWAPERDNo").hide();
        $("#LblWAPERDExpiredDate").hide();
        $("#LblAAJINo").hide();
        $("#LblAAJIExpiredDate").hide();
    }

    function initButton() {
        if (_GlobClientCode == "08") {
            $("#BtnAgentTreeSetup").show();
        }
        else {
            $("#BtnAgentTreeSetup").hide();
        }

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
        $("#BtnAgentRestructure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnAddAgentFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
        $("#BtnSaveAgentFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApprove.png"
        });
        $("#BtnCancelAgentFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnRejectAgentFee").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnReject.png"
        });
        $("#BtnAgentTreeSetup").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });

        $("#BtnShowAgentTreeSetup").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });

        $("#BtnGenerateAgentTreeSetup").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAccountRestructure.png"
        });



    }



    function initWindow() {

        $("#Date").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });
        $("#DateAmortize").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
        });

        $("#ParamDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeParamDate
        });
        function OnChangeParamDate() {
            var _FilterDate = Date.parse($("#ParamDate").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_FilterDate) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
                return;
            }

            $("#gridDetailAgentTreeSetup").empty();
        }


        win = $("#WinAgent").kendoWindow({
            height: 750,
            title: "Agent Detail",
            visible: false,
            width: 1200,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onPopUpClose
        }).data("kendoWindow");


        WinAddAgentFee = $("#WinAddAgentFee").kendoWindow({
            height: 750,
            title: "Add Agent Fee",
            visible: false,
            width: 1300,
            modal: false,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinAddAgentFeeClose
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


        WinAgentTreeSetup = $("#WinAgentTreeSetup").kendoWindow({
            height: 1000,
            title: "* Agent Tree Setup",
            visible: false,
            width: 1100,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinAgentTreeSetupClose
        }).data("kendoWindow");


        $("#WAPERDExpiredDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeWAPERDExpiredDate,
        });

        function OnChangeWAPERDExpiredDate() {
            var _date = Date.parse($("#WAPERDExpiredDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
        }

        $("#AAJIExpiredDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeAAJIExpiredDate,
        });

        function OnChangeAAJIExpiredDate() {
            var _date = Date.parse($("#AAJIExpiredDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {

                alertify.alert("Wrong Format Date DD/MMM/YYYY");
            }
        }

    }

    var GlobValidator = $("#WinAgent").kendoValidator().data("kendoValidator");
    var GlobValidatorAgentFee = $("#WinAddAgentFee").kendoValidator().data("kendoValidator");


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

    function validateDataAgentFee(_bit) {
        if (_bit == true) {
            return 1;
        }
        else {
            if (GlobValidatorAgentFee.validate()) {
                //alert("Validation sucess");
                return 1;
            }
            else {
                alertify.alert("Validation not Pass");
                return 0;
            }
        }


    }


    function showDetails(e) {
        if (e == null) {
            HideBtnAdd(0);
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            HideBitAgent();
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            HideBtnAdd(dataItemX.Status);
            HideBitAgent();
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
                $("#ID").attr('readonly', true);
            }
            if (dataItemX.Status == 3) {

                $("#StatusHeader").val("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 4) {
                $("#StatusHeader").val("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }

            $("#AgentPK").val(dataItemX.AgentPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ID").val(dataItemX.ID);
            $("#Name").val(dataItemX.Name);
            $("#NoRek").val(dataItemX.NoRek);
            $("#Email").val(dataItemX.Email);
            $("#Address").val(dataItemX.Address);
            $("#TaxID").val(dataItemX.TaxID);
            $("#BankInformation").val(dataItemX.BankInformation);
            $("#BeneficiaryName").val(dataItemX.BeneficiaryName);
            $("#Description").val(dataItemX.Description);
            $("#JoinDate").val(dataItemX.JoinDate);
            $("#Depth").val(dataItemX.Depth);
            $("#Levels").val(dataItemX.Levels);
            $("#BitisAgentBank").prop('checked', dataItemX.BitisAgentBank);
            $("#BitIsAgentCSR").prop('checked', dataItemX.BitIsAgentCSR);
            $("#NPWPNo").val(dataItemX.NPWPNo);
            $("#BitPPH23").prop('checked', dataItemX.BitPPH23);
            $("#BitPPH21").prop('checked', dataItemX.BitPPH21);
            $("#BitPPN").prop('checked', dataItemX.BitPPN);

            $("#WAPERDNo").val(dataItemX.WAPERDNo);
            $("#WAPERDExpiredDate").data("kendoDatePicker").value(kendo.parseDate(dataItemX.WAPERDExpiredDate), 'dd/MMM/yyyy');
            $("#AAJINo").val(dataItemX.AAJINo);
            $("#AAJIExpiredDate").data("kendoDatePicker").value(kendo.parseDate(dataItemX.AAJIExpiredDate), 'dd/MMM/yyyy');

            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));
        }

        function HideBitAgent() {
            if (_GlobClientCode == "03") {
                $("#LblbitIsAgentBank").show();
                $("#LblBitIsAgentCSR").show();
                $("#LblBitPPH23").show();
                $("#LblBitPPH21").show();
            }
            else {
                $("#LblbitIsAgentBank").hide();
                $("#LblBitIsAgentCSR").hide();
                $("#LblBitPPH23").show();
                $("#LblBitPPH21").show();
            }

            if (_GlobClientCode == "08") {
                $("#LblCompanyPositionSchemePK").show();
            }
        }

        $("#BitPPH23").change(function () {
            if (this.checked == true) {
                // disable button
                $("#LblPPH23Percent").show();
                $("#BitPPH21").prop('checked', false);
            }
            else {

                // enable button
                $("#LblPPH23Percent").hide();
            }
        });
        $("#BitPPH21").change(function () {
            if (this.checked == true) {
                // disable button
                $("#BitPPH23").prop('checked', false);
                $("#LblPPH23Percent").hide();
            }
            else {

                // enable button
            }
        });



        //Type
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/SDIClientType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Type").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeClientType,
                    value: setCmbClientType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeClientType() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbClientType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Type == 0) {
                    return "";
                } else {
                    return dataItemX.Type;
                }
            }
        }

        $("#AgentFee").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setAgentFee()
        });
        function setAgentFee() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.AgentFee;
            }
        }
        $("#PPH23Percent").kendoNumericTextBox({
            format: "##.#### \\%",
            decimals: 4,
            value: setPPH23Percent()
        });
        function setPPH23Percent() {
            if (e == null) {
                return 2;
            } else {
                return dataItemX.PPH23Percent;
            }
        }

        $("#MFeeMethod").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Mark to Market", value: 1 },
                { text: "NAV 1000", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeMFeeMethod,
            value: setCmbMFeeMethod()
        });
        function OnChangeMFeeMethod() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbMFeeMethod() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MFeeMethod;
            }
        }

        $("#SharingFeeCalculation").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Management Fee Calculation", value: 1 },
                { text: "AUM Calculation", value: 2 }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeSharingFeeCalculation,
            value: setCmbSharingFeeCalculation()
        });
        function OnChangeSharingFeeCalculation() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbSharingFeeCalculation() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.SharingFeeCalculation;
            }
        }

        $("#Phone").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setPhone()
        });
        function setPhone() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Phone;
            }
        }
        $("#Fax").kendoMaskedTextBox({
            mask: "(999) 000-00000",
            value: setFax()
        });
        function setFax() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.Fax;
            }
        }

        $("#Groups").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangeGroups,
            value: setCmbGroups()
        });
        function OnChangeGroups() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbGroups() {
            if (e == null) {
                return false;
            } else {
                return dataItemX.Groups;
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboGroupOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    value: setCmbParentPK(),
                    change: OnChangeParentPK
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeParentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbParentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ParentPK == 0) {
                    return "";
                } else {
                    return dataItemX.ParentPK;
                }
            }
        }


        $("#RangeFrom").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setRangeFrom()
        });
        function setRangeFrom() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.RangeFrom;
            }
        }

        $("#RangeTo").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setRangeTo()
        });
        function setRangeTo() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.RangeTo;
            }
        }
        $("#JoinDate").kendoDatePicker({
            format: "dd/MMM/yyyy",
            value: new Date(),
            parseFormats: ["dd-MM-yyyy", "dd-MM-yy", "dd/MM/yy", "dd/MM/yyyy", "ddMMyyyy", "ddMMyy"],
            change: OnChangeJoinDate
        });

        function OnChangeJoinDate() {
            if ($("#JoinDate").data("kendoDatePicker").value() != null) {
                if (_GlobClientCode != "01") {
                    $("#ValueDateTo").data("kendoDatePicker").value($("#ValueDateFrom").data("kendoDatePicker").value());
                }


            }

        }

        $("#MIFeeAmount").kendoNumericTextBox({
            format: "n2",
            decimals: 2,
            value: setMIFeeAmount()
        });
        function setMIFeeAmount() {
            if (e == null) {
                return "";
            } else {
                return dataItemX.MIFeeAmount;
            }
        }

        if (_GlobClientCode == "24") {
            $("#MIFeePercent").kendoNumericTextBox({
                format: "##.############### \\%",
                decimals: 15,
                value: setMIFeePercent()
            });
            function setMIFeePercent() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.MIFeePercent;
                }
            }
        }

        else {
            $("#MIFeePercent").kendoNumericTextBox({
                format: "##.#### \\%",
                decimals: 4,
                value: setMIFeePercent()
            });
            function setMIFeePercent() {
                if (e == null) {
                    return "";
                } else {
                    return dataItemX.MIFeePercent;
                }
            }
        }


        //combo box AgentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DAgentPK").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeAgentPK,
                    value: setCmbAgentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeAgentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbAgentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentPK == 0) {
                    return "";
                } else {
                    return dataItemX.AgentPK;
                }
            }
        }

        //combo box FundPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DFundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
                    value: setCmbFundPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeFundPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
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

        //TypeTrx
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AgentFeeTrxType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TypeTrx").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTypeTrx,
                    value: setCmbTypeTrx()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeTypeTrx() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbTypeTrx() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TypeTrx == 0) {
                    return "";
                } else {
                    return dataItemX.TypeTrx;
                }
            }
        }


        //FeeType
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AgentFeeType",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FeeType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeFeeType,
                    value: setCmbFeeType()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeFeeType() {
            clearDataAgentFee();
            clearDataAgentFeeSetup();
            RequiredAttributes(this.value());
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            if (this.value() == 1) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
            }
            else if (this.value() == 2) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
            }
            else if (this.value() == 3) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
            }
            else if (this.value() == 4) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
            }
            else if (this.value() == 5) {
                $("#lblDateAmortize").hide();
                $("#lblMIAmount").hide();
            }

            else if (this.value() == 6) {
                $("#lblRangeFrom").hide();
                $("#lblRangeTo").hide();
                $("#lblMIPercent").hide();
            }
        }


        function setCmbFeeType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FeeType == 0) {
                    return "";
                } else {
                    return dataItemX.FeeType;
                }
            }
        }


        //combo box DepartmentPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Department/GetDepartmentCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DepartmentPK").kendoComboBox({
                    dataValueField: "DepartmentPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDepartmentPK,
                    value: setCmbDepartmentPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeDepartmentPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbDepartmentPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DepartmentPK == 0) {
                    return "";
                } else {
                    return dataItemX.DepartmentPK;
                }
            }
        }


        //combo box Company Position Scheme
        $.ajax({
            url: window.location.origin + "/Radsoft/CompanyPosition/GetCompanyPositionSchemeCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CompanyPositionSchemePK").kendoComboBox({
                    dataValueField: "CompanyPositionSchemePK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeCompanyPositionSchemePK,
                    value: setCmbCompanyPositionSchemePK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeCompanyPositionSchemePK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbCompanyPositionSchemePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CompanyPositionSchemePK == 0) {
                    return "";
                } else {
                    return dataItemX.CompanyPositionSchemePK;
                }
            }
        }

        win.center();
        win.open();
    }




    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#AgentPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#ID").val("");
        $("#Name").val("");
        $("#Type").val("");
        $("#AgentFee").val("");
        $("#NoRek").val("");
        $("#Phone").val("");
        $("#Fax").val("");
        $("#Email").val("");
        $("#Address").val("");
        $("#TaxID").val("");
        $("#BankInformation").val("");
        $("#BeneficiaryName").val("");
        $("#Description").val("");
        $("#JoinDate").val("");
        $("#Groups").val("");
        $("#Levels").val("");
        $("#ParentPK").val("");
        $("#ParentID").val("");
        $("#Depth").val("");
        $("#DepartmentPK").val("");
        $("#CompanyPositionSchemePK").val("");
        $("#BitisAgentBank").prop('checked', false);
        $("#BitIsAgentCSR").prop('checked', false);
        $("#NPWPNo").val("");
        $("#BitPPH23").prop('checked', false);
        $("#BitPPH21").prop('checked', false);
        $("#MFeeMethod").val("");
        $("#SharingFeeCalculation").val("");

        $("#WAPERDNo").val("");
        $("#WAPERDExpiredDate").val("");
        $("#AAJINo").val("");
        $("#AAJIExpiredDate").val("");

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
                schema: {
                    model: {
                        fields: {
                            AgentPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            ID: { type: "string" },
                            Name: { type: "string" },
                            Type: { type: "number" },
                            TypeDesc: { type: "string" },
                            AgentFee: { type: "number" },
                            NoRek: { type: "string" },
                            Phone: { type: "string" },
                            Fax: { type: "string" },
                            Email: { type: "string" },
                            Address: { type: "string" },
                            TaxID: { type: "string" },
                            BankInformation: { type: "string" },
                            BeneficiaryName: { type: "string" },
                            Description: { type: "string" },
                            JoinDate: { type: "date" },
                            Groups: { type: "boolean" },
                            Levels: { type: "number" },
                            ParentPK: { type: "number" },
                            ParentID: { type: "string" },
                            ParentName: { type: "string" },
                            Depth: { type: "number" },
                            ParentPK1: { type: "number" },
                            ParentPK2: { type: "number" },
                            ParentPK3: { type: "number" },
                            ParentPK4: { type: "number" },
                            ParentPK5: { type: "number" },
                            ParentPK6: { type: "number" },
                            ParentPK7: { type: "number" },
                            ParentPK8: { type: "number" },
                            ParentPK9: { type: "number" },
                            DepartmentPK: { type: "number" },
                            DepartmentID: { type: "string" },
                            CompanyPositionSchemePK: { type: "number" },
                            CompanyPositionSchemeID: { type: "string" },
                            BitisAgentBank: { type: "boolean" },
                            BitIsAgentCSR: { type: "boolean" },
                            NPWPNo: { type: "string" },
                            BitPPH23: { type: "boolean" },
                            BitPPH21: { type: "boolean" },
                            MFeeMethod: { type: "number" },
                            MFeeMethodDesc: { type: "string" },
                            SharingFeeCalculation: { type: "number" },
                            SharingFeeCalculationDesc: { type: "string" },

                            WAPERDNo: { type: "string" },
                            WAPERDExpiredDate: { type: "date" },
                            AAJINo: { type: "string" },
                            AAJIExpiredDate: { type: "date" },

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
        if (tabindex == undefined || tabindex == 0) {
            var gridApproved = $("#gridAgentApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridAgentPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridAgentHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var AgentApprovedURL = window.location.origin + "/Radsoft/Agent/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(AgentApprovedURL);
        if (_GlobClientCode == "08") {
            $("#gridAgentApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Agent"
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
                    { field: "AgentPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ID", title: "ID", width: 200 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "TypeDesc", title: "Type", width: 200 },
                    { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                    { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "ParentID", title: "Parent ID", width: 200 },
                    { field: "ParentName", title: "Parent Name", width: 300 },
                    { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    {
                        field: "AgentFee", title: "Agent Fee %", width: 200, format: "{0:n4}",
                        template: "#: AgentFee  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    { field: "DepartmentID", title: "Department ID", width: 200 },
                    { field: "NoRek", title: "No Rek", width: 200 },
                    { field: "Phone", title: "Phone", width: 200 },
                    { field: "Fax", title: "Fax", width: 200 },
                    { field: "Email", title: "Email", width: 200 },
                    { field: "Address", title: "Address", width: 1000 },
                    { field: "TaxID", title: "Tax ID", width: 200 },
                    { field: "BankInformation", title: "Bank Information", width: 200 },
                    { field: "BeneficiaryName", title: "Beneficiary Name", width: 200 },
                    { field: "Description", title: "Description", width: 300 },
                    { field: "JoinDate", title: "JoinDate", format: "{0:dd/MMM/yyyy}", width: 180 },
                    { field: "RangeFrom", title: "Range From", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "RangeTo", title: "Range To", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, hidden: true, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    {
                        field: "MIFeePercent", title: "MI Fee Percent", width: 120, hidden: true, format: "{0:n4}",
                        template: "#: MIFeePercent  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    { field: "CompanyPositionSchemeID", title: "CompanyPositionScheme ID", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }
        else {
            $("#gridAgentApproved").kendoGrid({
                dataSource: dataSourceApproved,
                height: gridHeight,
                scrollable: {
                    virtual: true
                },
                groupable: {
                    messages: {
                        empty: "Form Agent"
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
                    { field: "AgentPK", title: "SysNo.", width: 95 },
                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                    { field: "ID", title: "ID", width: 200 },
                    { field: "Name", title: "Name", width: 300 },
                    { field: "TypeDesc", title: "Type", width: 200 },
                    { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                    { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "ParentID", title: "Parent ID", width: 200 },
                    { field: "ParentName", title: "Parent Name", width: 300 },
                    { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    {
                        field: "AgentFee", title: "Agent Fee %", width: 200, format: "{0:n4}",
                        template: "#: AgentFee  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    { field: "DepartmentID", title: "Department ID", width: 200 },
                    { field: "NoRek", title: "No Rek", width: 200 },
                    { field: "Phone", title: "Phone", width: 200 },
                    { field: "Fax", title: "Fax", width: 200 },
                    { field: "Email", title: "Email", width: 200 },
                    { field: "Address", title: "Address", width: 1000 },
                    { field: "TaxID", title: "Tax ID", width: 200 },
                    { field: "BankInformation", title: "Bank Information", width: 200 },
                    { field: "BeneficiaryName", title: "Beneficiary Name", width: 200 },
                    { field: "Description", title: "Description", width: 300 },
                    { field: "JoinDate", title: "JoinDate", format: "{0:dd/MMM/yyyy}", width: 180 },
                    { field: "RangeFrom", title: "Range From", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "RangeTo", title: "Range To", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                    { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, hidden: true, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                    {
                        field: "MIFeePercent", title: "MI Fee Percent", width: 120, hidden: true, format: "{0:n4}",
                        template: "#: MIFeePercent  # %",
                        attributes: { style: "text-align:right;" }
                    },
                    { field: "CompanyPositionSchemeID", hidden: true, title: "CompanyPositionScheme ID", width: 200 },
                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                ]
            });
        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabAgent").kendoTabStrip({
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
                        var AgentPendingURL = window.location.origin + "/Radsoft/Agent/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(AgentPendingURL);
                        if (_GlobClientCode == "08") {
                            $("#gridAgentPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Agent"
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
                                    { field: "AgentPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "ID", title: "ID", width: 200 },
                                    { field: "Name", title: "Name", width: 300 },
                                    { field: "TypeDesc", title: "Type", width: 200 },
                                    { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                                    { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "ParentID", title: "Parent ID", width: 200 },
                                    { field: "ParentName", title: "Parent Name", width: 300 },
                                    { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "AgentFee", title: "Agent Fee %", width: 200, format: "{0:n4}",
                                        template: "#: AgentFee  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "NoRek", title: "No Rek", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "Phone", title: "Phone", width: 200 },
                                    { field: "Fax", title: "Fax", width: 200 },
                                    { field: "Email", title: "Email", width: 200 },
                                    { field: "Address", title: "Address", width: 1000 },
                                    { field: "TaxID", title: "Tax ID", width: 200 },
                                    { field: "BankInformation", title: "Bank Information", width: 200 },
                                    { field: "BeneficiaryName", title: "Beneficiary Name", width: 200 },
                                    { field: "Description", title: "Description", width: 300 },
                                    { field: "JoinDate", title: "JoinDate", format: "{0:dd/MMM/yyyy}", width: 180 },
                                    { field: "RangeFrom", title: "Range From", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "RangeTo", title: "Range To", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, hidden: true, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "MIFeePercent", title: "MI Fee Percent", width: 120, hidden: true, format: "{0:n4}",
                                        template: "#: MIFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "CompanyPositionSchemeID", title: "CompanyPositionScheme ID", width: 200 },
                                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                                ]
                            });
                        }
                        else {
                            $("#gridAgentPending").kendoGrid({
                                dataSource: dataSourcePending,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Agent"
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
                                    { field: "AgentPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "ID", title: "ID", width: 200 },
                                    { field: "Name", title: "Name", width: 300 },
                                    { field: "TypeDesc", title: "Type", width: 200 },
                                    { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                                    { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "ParentID", title: "Parent ID", width: 200 },
                                    { field: "ParentName", title: "Parent Name", width: 300 },
                                    { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "AgentFee", title: "Agent Fee %", width: 200, format: "{0:n4}",
                                        template: "#: AgentFee  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "NoRek", title: "No Rek", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "Phone", title: "Phone", width: 200 },
                                    { field: "Fax", title: "Fax", width: 200 },
                                    { field: "Email", title: "Email", width: 200 },
                                    { field: "Address", title: "Address", width: 1000 },
                                    { field: "TaxID", title: "Tax ID", width: 200 },
                                    { field: "BankInformation", title: "Bank Information", width: 200 },
                                    { field: "BeneficiaryName", title: "Beneficiary Name", width: 200 },
                                    { field: "Description", title: "Description", width: 300 },
                                    { field: "JoinDate", title: "JoinDate", width: 180 },
                                    { field: "RangeFrom", title: "Range From", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "RangeTo", title: "Range To", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, hidden: true, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "MIFeePercent", title: "MI Fee Percent", width: 120, hidden: true, format: "{0:n4}",
                                        template: "#: MIFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "CompanyPositionSchemeID", hidden: true, title: "CompanyPositionScheme ID", width: 200 },
                                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }
                                ]
                            });
                        }
                    }
                    if (tabindex == 2) {

                        var AgentHistoryURL = window.location.origin + "/Radsoft/Agent/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(AgentHistoryURL);
                        if (_GlobClientCode == "08") {
                            $("#gridAgentHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Agent"
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
                                    { field: "AgentPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "ID", title: "ID", width: 200 },
                                    { field: "Name", title: "Name", width: 300 },
                                    { field: "TypeDesc", title: "Type", width: 200 },
                                    { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                                    { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "ParentID", title: "Parent ID", width: 200 },
                                    { field: "ParentName", title: "Parent Name", width: 300 },
                                    { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "AgentFee", title: "Agent Fee %", width: 200, format: "{0:n4}",
                                        template: "#: AgentFee  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "NoRek", title: "No Rek", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "Phone", title: "Phone", width: 200 },
                                    { field: "Fax", title: "Fax", width: 200 },
                                    { field: "Email", title: "Email", width: 200 },
                                    { field: "Address", title: "Address", width: 1000 },
                                    { field: "TaxID", title: "Tax ID", width: 200 },
                                    { field: "BankInformation", title: "Bank Information", width: 200 },
                                    { field: "BeneficiaryName", title: "Beneficiary Name", width: 200 },
                                    { field: "Description", title: "Description", width: 300 },
                                    { field: "JoinDate", title: "JoinDate", width: 180 },
                                    { field: "RangeFrom", title: "Range From", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "RangeTo", title: "Range To", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, hidden: true, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "MIFeePercent", title: "MI Fee Percent", width: 120, hidden: true, format: "{0:n4}",
                                        template: "#: MIFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "CompanyPositionSchemeID", title: "CompanyPositionScheme ID", width: 200 },
                                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

                                ]
                            });
                        }
                        else {
                            $("#gridAgentHistory").kendoGrid({
                                dataSource: dataSourceHistory,
                                height: gridHeight,
                                scrollable: {
                                    virtual: true
                                },
                                groupable: {
                                    messages: {
                                        empty: "Form Agent"
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
                                    { field: "AgentPK", title: "SysNo.", width: 95 },
                                    { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                    { field: "StatusDesc", title: "Status", width: 200 },
                                    { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                    { field: "ID", title: "ID", width: 200 },
                                    { field: "Name", title: "Name", width: 300 },
                                    { field: "TypeDesc", title: "Type", width: 200 },
                                    { field: "Groups", title: "Groups", width: 150, template: "#= Groups ? 'Yes' : 'No' #" },
                                    { field: "Levels", title: "Levels", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "ParentID", title: "Parent ID", width: 200 },
                                    { field: "ParentName", title: "Parent Name", width: 300 },
                                    { field: "Depth", title: "Depth", width: 150, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "AgentFee", title: "Agent Fee %", width: 200, format: "{0:n4}",
                                        template: "#: AgentFee  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "NoRek", title: "No Rek", width: 200 },
                                    { field: "DepartmentID", title: "Department ID", width: 200 },
                                    { field: "Phone", title: "Phone", width: 200 },
                                    { field: "Fax", title: "Fax", width: 200 },
                                    { field: "Email", title: "Email", width: 200 },
                                    { field: "Address", title: "Address", width: 1000 },
                                    { field: "TaxID", title: "Tax ID", width: 200 },
                                    { field: "BankInformation", title: "Bank Information", width: 200 },
                                    { field: "BeneficiaryName", title: "Beneficiary Name", width: 200 },
                                    { field: "Description", title: "Description", width: 300 },
                                    { field: "JoinDate", title: "JoinDate", width: 180 },
                                    { field: "RangeFrom", title: "Range From", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "RangeTo", title: "Range To", width: 120, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                                    { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, hidden: true, format: "{0:n2}", attributes: { style: "text-align:right;" } },
                                    {
                                        field: "MIFeePercent", title: "MI Fee Percent", width: 120, hidden: true, format: "{0:n4}",
                                        template: "#: MIFeePercent  # %",
                                        attributes: { style: "text-align:right;" }
                                    },
                                    { field: "CompanyPositionSchemeID", hidden: true, title: "CompanyPositionScheme ID", width: 200 },
                                    { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                    { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                    { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                    { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                    { field: "VoidUsersID", title: "VoidID", width: 200 },
                                    { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 }

                                ]
                            });
                        }
                    }
                } else {
                    refresh();
                }
            }
        });
    }

    function gridHistoryDataBound() {
        var grid = $("#gridAgentHistory").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.StatusDesc == "APPROVED") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowApprove");
            } else if (row.StatusDesc == "VOID") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else if (row.StatusDesc == "WAITING") {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowWaiting");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
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

    $("#BtnNew").click(function () {
        $("#ID").attr('readonly', false);
        showDetails(null);
    });


    function HideBtnAdd(_status) {
        if (_status == 1) {
            $("#BtnAddAgentFee").show();
        }
        else if (_status == 2) {
            $("#BtnAddAgentFee").show();
        }
        else if (_status == 3) {
            $("#BtnAddAgentFee").hide();
        }
        else if (_status == 0) {
            $("#BtnAddAgentFee").hide();
        }
    }


    $("#BtnAddAgentFee").click(function () {
        clearDataAgentFee();
        GridAgentFee();
        if ($("#AgentPK").val() == 0 || $("#AgentPK").val() == null) {
            alertify.alert("There's no Agent");
        } else {
            showAddAgentFee();
        }
    });

    function showAddAgentFee(e) {
        var dataItemX;
        if (e == null) {
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid = $("#gridAddAgentFee").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

        }

        WinAddAgentFee.center();
        WinAddAgentFee.open();

    }

    function onWinAddAgentFeeClose() {
        GlobValidatorAgentFee.hideMessages();
        clearDataAgentFee();
        refresh();
    }

    function clearDataAgentFee() {
        $("#MIFeeAmount").data("kendoNumericTextBox").value("");
        $("#RangeFrom").data("kendoNumericTextBox").value("");
        $("#RangeTo").data("kendoNumericTextBox").value("");
        $("#MIFeePercent").data("kendoNumericTextBox").value("");
        $("#DateAmortize").val("");
        $("#RangeTo").attr('readonly', false);

    }

    function clearDataAgentFeeSetup() {
        $("#lblRangeFrom").show();
        $("#lblRangeTo").show();
        $("#lblDateAmortize").show();
        $("#lblMIAmount").show();
        $("#lblFeeType").show();
        $("#lblDate").show();
        $("#lblMIPercent").show();

    }

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckExistingID/" + sessionStorage.getItem("user") + "/" + $("#ID").val() + "/" + "Agent",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {

                                var Agent = {
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Type: $('#Type').val(),
                                    AgentFee: $('#AgentFee').val(),
                                    NoRek: $('#NoRek').val(),
                                    Phone: $('#Phone').val(),
                                    Fax: $('#Fax').val(),
                                    Email: $('#Email').val(),
                                    Address: $('#Address').val(),
                                    TaxID: $('#TaxID').val(),
                                    BankInformation: $('#BankInformation').val(),
                                    BeneficiaryName: $('#BeneficiaryName').val(),
                                    Description: $('#Description').val(),
                                    JoinDate: $('#JoinDate').val(),
                                    Groups: $("#Groups").val(),
                                    Levels: $('#Levels').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    Depth: $('#Depth').val(),
                                    DepartmentPK: $('#DepartmentPK').val(),
                                    BitisAgentBank: $('#BitisAgentBank').is(":checked"),
                                    BitIsAgentCSR: $('#BitIsAgentCSR').is(":checked"),
                                    CompanyPositionSchemePK: $('#CompanyPositionSchemePK').val(),
                                    NPWPNo: $('#NPWPNo').val(),
                                    BitPPH23: $('#BitPPH23').is(":checked"),
                                    BitPPH21: $('#BitPPH21').is(":checked"),
                                    MFeeMethod: $('#MFeeMethod').val(),
                                    SharingFeeCalculation: $('#SharingFeeCalculation').val(),
                                    WAPERDNo: $('#WAPERDNo').val(),
                                    WAPERDExpiredDate: $('#WAPERDExpiredDate').val(),
                                    AAJINo: $('#AAJINo').val(),
                                    AAJIExpiredDate: $('#AAJIExpiredDate').val(),
                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Agent/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Agent_I",
                                    type: 'POST',
                                    data: JSON.stringify(Agent),
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
                                alertify.alert("Data ID Same Not Allow!");
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
    });


    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Agent",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var Agent = {
                                    AgentPK: $('#AgentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ID: $('#ID').val(),
                                    Name: $('#Name').val(),
                                    Type: $('#Type').val(),
                                    AgentFee: $('#AgentFee').val(),
                                    NoRek: $('#NoRek').val(),
                                    Phone: $('#Phone').val(),
                                    Fax: $('#Fax').val(),
                                    Email: $('#Email').val(),
                                    Address: $('#Address').val(),
                                    TaxID: $('#TaxID').val(),
                                    BankInformation: $('#BankInformation').val(),
                                    BeneficiaryName: $('#BeneficiaryName').val(),
                                    Description: $('#Description').val(),
                                    JoinDate: $('#JoinDate').val(),
                                    Groups: $("#Groups").val(),
                                    Levels: $('#Levels').val(),
                                    ParentPK: $('#ParentPK').val(),
                                    Depth: $('#Depth').val(),
                                    DepartmentPK: $('#DepartmentPK').val(),
                                    BitisAgentBank: $('#BitisAgentBank').is(":checked"),
                                    BitIsAgentCSR: $('#BitIsAgentCSR').is(":checked"),
                                    CompanyPositionSchemePK: $('#CompanyPositionSchemePK').val(),
                                    NPWPNo: $('#NPWPNo').val(),
                                    BitPPH23: $('#BitPPH23').is(":checked"),
                                    BitPPH21: $('#BitPPH21').is(":checked"),
                                    MFeeMethod: $('#MFeeMethod').val(),
                                    SharingFeeCalculation: $('#SharingFeeCalculation').val(),
                                    WAPERDNo: $('#WAPERDNo').val(),
                                    WAPERDExpiredDate: $('#WAPERDExpiredDate').val(),
                                    AAJINo: $('#AAJINo').val(),
                                    AAJIExpiredDate: $('#AAJIExpiredDate').val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Agent/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Agent_U",
                                    type: 'POST',
                                    data: JSON.stringify(Agent),
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
    });

    $("#BtnOldData").click(function () {

        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Agent",
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
                                    url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Agent" + "/" + $("#AgentPK").val(),
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

        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Agent",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            if ($("#ID").val() == null || $("#ID").val() == "") {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Agent/Agent_GenerateNewAgentID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Agent_GenerateNewAgentID",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        if ($("#ID").val() == null || $("#ID").val() == "") {
                                            $("#ID").val(data);
                                            alertify.alert("Your New Client ID is " + data);
                                        }
                                        var Agent = {
                                            AgentPK: $('#AgentPK').val(),
                                            HistoryPK: $('#HistoryPK').val(),
                                            ApprovedUsersID: sessionStorage.getItem("user")
                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Agent/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Agent_A",
                                            type: 'POST',
                                            data: JSON.stringify(Agent),
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
                            else {
                                var Agent = {
                                    AgentPK: $('#AgentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    ApprovedUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Agent/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Agent_A",
                                    type: 'POST',
                                    data: JSON.stringify(Agent),
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
                        }
                        else {
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

        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Agent",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Agent = {
                                AgentPK: $('#AgentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Agent/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Agent_V",
                                type: 'POST',
                                data: JSON.stringify(Agent),
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

        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AgentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Agent",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var Agent = {
                                AgentPK: $('#AgentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Agent/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "Agent_R",
                                type: 'POST',
                                data: JSON.stringify(Agent),
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

    $("#BtnAgentRestructure").click(function () {

        alertify.confirm("Are you sure want to Restructure Agent?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Agent/AgentUpdateParentAndDepth/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
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



    //$("#BtnSaveAgentFee").click(function () {
    //    var val = validateDataAgentFee($('#BitMaxRangeTo').is(":checked"));
    //    RequiredAttributes($('#FeeType').val());
    //    var DateAmortize;
    //    var MiFeeAmount;
    //    var RangeFrom;
    //    var RangeTo;
    //    var MiFeePercent;

    //    if (val == 1) {
    //        alertify.confirm("Are you sure want to Add data?", function (e) {
    //            if (e) {
    //                if ($('#FeeType').val() == 5) {
    //                    $.ajax({
    //                        url: window.location.origin + "/Radsoft/Agent/CheckDataFlat/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#FundPK').val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FeeType').val() + "/" + $('#AgentPK').val(),
    //                        type: 'GET',
    //                        contentType: "application/json;charset=utf-8",
    //                        success: function (data) {
    //                            if (data == false) {

    //                                //$.ajax({
    //                                //    url: window.location.origin + "/Radsoft/Agent/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AgentPK').val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FeeType').val(),
    //                                //    type: 'GET',
    //                                //    contentType: "application/json;charset=utf-8",
    //                                //    success: function (data) {
    //                                //        if (data == false) {
    //                                            if ($('#DateAmortize').val() == 0 || $('#DateAmortize').val() == null || $('#DateAmortize').val() == "") {
    //                                                DateAmortize = ""
    //                                            }
    //                                            else {
    //                                                DateAmortize = $('#DateAmortize').val()
    //                                            }

    //                                            if ($('#MiFeeAmount').val() == 0 || $('#MiFeeAmount').val() == null || $('#MiFeeAmount').val() == "") {
    //                                                MiFeeAmount = 0
    //                                            }
    //                                            else {
    //                                                MiFeeAmount = $('#MiFeeAmount').val()
    //                                            }

    //                                            if ($('#RangeFrom').val() == 0 || $('#RangeFrom').val() == null || $('#RangeFrom').val() == "") {
    //                                                RangeFrom = 0
    //                                            }
    //                                            else {
    //                                                RangeFrom = $('#RangeFrom').val()
    //                                            }

    //                                            if ($('#RangeTo').val() == 0 || $('#RangeTo').val() == null || $('#RangeTo').val() == "") {
    //                                                RangeTo = 0
    //                                            }
    //                                            else {
    //                                                RangeTo = $('#RangeTo').val()
    //                                            }

    //                                            if ($('#MiFeePercent').val() == 0 || $('#MiFeePercent').val() == null || $('#MiFeePercent').val() == "") {
    //                                                MiFeePercent = 0
    //                                            }
    //                                            else {
    //                                                MiFeePercent = $('#MiFeePercent').val()
    //                                            }
    //                                            if ($('#BitMaxRangeTo').is(":checked") == true) {
    //                                                RangeTo = '9999'
    //                                            }
    //                                            else {
    //                                                RangeTo = $('#RangeTo').val()
    //                                            }
    //                                            var Agent = {
    //                                                AgentPK: $('#AgentPK').val(),
    //                                                TypeTrx: $('#TypeTrx').val(),
    //                                                FeeType: $('#FeeType').val(),
    //                                                Date: $('#Date').val(),
    //                                                DateAmortize: DateAmortize,
    //                                                MiFeeAmount: MiFeeAmount,
    //                                                FundPK: $('#FundPK').val(),
    //                                                RangeFrom: RangeFrom,
    //                                                RangeTo: RangeTo,
    //                                                MiFeePercent: MiFeePercent,
    //                                                EntryUsersID: sessionStorage.getItem("user")

    //                                            };
    //                                            $.ajax({
    //                                                url: window.location.origin + "/Radsoft/Agent/AgentFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                                type: 'POST',
    //                                                data: JSON.stringify(Agent),
    //                                                contentType: "application/json;charset=utf-8",
    //                                                success: function (data) {
    //                                                    alertify.alert(data);
    //                                                    WinAddAgentFee.close();
    //                                                    refresh();
    //                                                },
    //                                                error: function (data) {
    //                                                    alertify.alert(data.responseText);
    //                                                }
    //                                            });
    //                                //        }
    //                                //        else {
    //                                //            alertify.alert("Data Has Been Add, Check Your Date");
    //                                //            //WinAddAgentFee.close();
    //                                //            refresh();
    //                                //        }
    //                                //    }
    //                                //});

    //                            }
    //                            else {
    //                                alertify.alert("Cannot Add Flat in this Day");
    //                                //WinAddAgentFee.close();
    //                                refresh();
    //                            }
    //                        }
    //                    });
    //                }
    //                else {
    //                    //$.ajax({
    //                    //    url: window.location.origin + "/Radsoft/Agent/CheckHasAdd/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AgentPK').val() + "/" + kendo.toString($("#Date").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FeeType').val(),
    //                    //    type: 'GET',
    //                    //    contentType: "application/json;charset=utf-8",
    //                    //    success: function (data) {
    //                    //        if (data == false) {
    //                                if ($('#DateAmortize').val() == 0 || $('#DateAmortize').val() == null || $('#DateAmortize').val() == "") {
    //                                    DateAmortize = ""
    //                                }
    //                                else {
    //                                    DateAmortize = $('#DateAmortize').val()
    //                                }

    //                                if ($('#MiFeeAmount').val() == 0 || $('#MiFeeAmount').val() == null || $('#MiFeeAmount').val() == "") {
    //                                    MiFeeAmount = 0
    //                                }
    //                                else {
    //                                    MiFeeAmount = $('#MiFeeAmount').val()
    //                                }

    //                                if ($('#RangeFrom').val() == 0 || $('#RangeFrom').val() == null || $('#RangeFrom').val() == "") {
    //                                    RangeFrom = 0
    //                                }
    //                                else {
    //                                    RangeFrom = $('#RangeFrom').val()
    //                                }

    //                                if ($('#RangeTo').val() == 0 || $('#RangeTo').val() == null || $('#RangeTo').val() == "") {
    //                                    RangeTo = 0
    //                                }
    //                                else {
    //                                    RangeTo = $('#RangeTo').val()
    //                                }

    //                                if ($('#MiFeePercent').val() == 0 || $('#MiFeePercent').val() == null || $('#MiFeePercent').val() == "") {
    //                                    MiFeePercent = 0
    //                                }
    //                                else {
    //                                    MiFeePercent = $('#MiFeePercent').val()
    //                                }
    //                                if ($('#BitMaxRangeTo').is(":checked") == true) {
    //                                    RangeTo = '9999'
    //                                }
    //                                else {
    //                                    RangeTo = $('#RangeTo').val()
    //                                }
    //                                var Agent = {
    //                                    AgentPK: $('#AgentPK').val(),
    //                                    TypeTrx: $('#TypeTrx').val(),
    //                                    FeeType: $('#FeeType').val(),
    //                                    Date: $('#Date').val(),
    //                                    DateAmortize: DateAmortize,
    //                                    MiFeeAmount: MiFeeAmount,
    //                                    FundPK: $('#FundPK').val(),
    //                                    RangeFrom: RangeFrom,
    //                                    RangeTo: RangeTo,
    //                                    MiFeePercent: MiFeePercent,
    //                                    EntryUsersID: sessionStorage.getItem("user")

    //                                };
    //                                $.ajax({
    //                                    url: window.location.origin + "/Radsoft/Agent/AgentFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
    //                                    type: 'POST',
    //                                    data: JSON.stringify(Agent),
    //                                    contentType: "application/json;charset=utf-8",
    //                                    success: function (data) {
    //                                        alertify.alert(data);
    //                                        WinAddAgentFee.close();
    //                                        refresh();
    //                                    },
    //                                    error: function (data) {
    //                                        alertify.alert(data.responseText);
    //                                    }
    //                                });
    //                    //        }
    //                    //        else {
    //                    //            alertify.alert("Data Has Been Add, Check Your Date");
    //                    //            //WinAddAgentFee.close();
    //                    //            refresh();
    //                    //        }
    //                    //    }
    //                    //});
    //                }


    //            }
    //        });
    //    }

    //});


    $("#BtnSaveAgentFee").click(function () {
        //var val = validateData();
        var val = 1;
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {

                    var Validate = {
                        FundPK: $('#DFundPK').val(),
                        AgentPK: $('#DAgentPK').val(),
                        Date: $('#Date').val(),
                        RangeFrom: $('#RangeFrom').val(),
                        RangeTo: $('#RangeTo').val(),
                        EntryUsersID: sessionStorage.getItem("user"),
                        FeeType: $('#FeeType').val()
                    };

                    $.ajax({
                        url: window.location.origin + "/Radsoft/AgentFeeSetup/AddValidate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(Validate),
                        success: function (data) {
                            if (data == "FALSE") {

                                var AgentFeeSetup = {
                                    FundPK: $('#DFundPK').val(),
                                    AgentPK: $('#DAgentPK').val(),
                                    Date: $('#Date').val(),
                                    DateAmortize: $('#DateAmortize').val(),
                                    RangeFrom: $('#RangeFrom').val(),
                                    RangeTo: $('#RangeTo').val(),
                                    MIFeeAmount: $('#MIFeeAmount').val(),
                                    MIFeePercent: $('#MIFeePercent').val(),
                                    FeeType: $('#FeeType').val(),
                                    TypeTrx: $('#TypeTrx').val(),

                                    JoinDate: $('#JoinDate').val(),
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AgentFeeSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AgentFeeSetup_I",
                                    type: 'POST',
                                    data: JSON.stringify(AgentFeeSetup),
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        alertify.alert(data);
                                        win.close();
                                        refreshAgentFeeSetup();
                                    },
                                    error: function (data) {
                                        alertify.alert(data.responseText);
                                    }
                                });

                            }
                            else {
                                alertify.alert(data);
                                refresh();
                            }
                        }
                    });

                }
            });
        }
    });

    $("#BtnCancelAgentFee").click(function () {

        alertify.confirm("Are you sure want to close Add Agent Fee ?",
            function (e) {
                if (e) {
                    WinAddAgentFee.close();
                    alertify.alert("Close Add Agent Fee");
                }
            });
    });

    function getDataSourceAgentFee(_url) {
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
                            AgentPK: { type: "number" },
                            Agent: { type: "string" },
                            TypeTrx: { type: "number" },
                            TypeTrxDesc: { type: "string" },
                            FeeType: { type: "number" },
                            FeeTypeDesc: { type: "string" },
                            Date: { type: "date" },
                            DateAmortize: { type: "date" },
                            MIFeeAmount: { type: "number" },
                            FundPK: { type: "number" },
                            FundName: { type: "string" },
                            RangeFrom: { type: "number" },
                            RangeTo: { type: "number" },
                            MIFeePercent: { type: "number" },
                        }
                    }
                }
            });
    }

    function GridAgentFee() {
        $("#gridAddAgentFee").empty();
        var AgentFeeURL = window.location.origin + "/Radsoft/Agent/GetDataAgentFee/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $('#AgentPK').val(),
            dataSourceApproved = getDataSourceAgentFee(AgentFeeURL);

        var gridDetail = $("#gridAddAgentFee").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Agent Fee"
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
            columns: [
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetail' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAll' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "AgentFeeSetupPK", title: "SysNo.", width: 95 },
                { field: "Date", title: "Date", width: 110, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                { field: "FundPK", title: "FundPK", hidden: true, width: 80 },
                { field: "FundName", title: "Fund", width: 250 },
                { field: "TypeTrxDesc", title: "Type Trx", width: 130 },
                { field: "FeeTypeDesc", title: "Fee Type", width: 130 },
                { field: "RangeFrom", title: "Range From", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "RangeTo", title: "Range To", width: 120, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "MIFeeAmount", title: "MI Fee Amount", width: 120, format: "{0:n2}", attributes: { style: "text-align:right;" } }, {
                    field: "MIFeePercent", title: "MI Fee Percent", width: 120, format: "{0:n4}",
                    template: "#: MIFeePercent  # %",
                    attributes: { style: "text-align:right;" }
                },
                { field: "DateAmortize", title: "Date Amortize", width: 110, template: "#= (DateAmortize == null) ? ' ' : kendo.toString(kendo.parseDate(DateAmortize), 'dd/MMM/yyyy')#" },
            ]
        }).data("kendoGrid");
        $("#SelectedAll").change(function () {

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

        gridDetail.table.on("click", ".cSelectedDetail", selectDataPending);

        function selectDataPending(e) {


            var grid = $("#gridAddAgentFee").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _AgentFeeSetupPK = dataItemX.AgentFeeSetupPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _AgentFeeSetupPK);

        }


        function SelectDeselectData(_a, _b) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Host/SelectDeselectData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AgentFeeSetup/" + _a + "/" + _b,
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
                url: window.location.origin + "/Radsoft/Host/SelectDeselectAllData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AgentFeeSetup/" + _a,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $(".cSelectedDetail").prop('checked', _a);
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });
        }

    }



    $("#BtnRejectAgentFee").click(function () {

        alertify.confirm("Are you sure want to Reject This Selected Data ?", function (e) {
            if (e) {

                $.ajax({
                    url: window.location.origin + "/Radsoft/Agent/RejectedDataAgentFeeSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#AgentPK").val(),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        WinAddAgentFee.close();
                        refresh();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });
            }
        });


    });

    function RequiredAttributes(_type) {
        if (_type == 1 || _type == 2) {
            $("#DateAmortize").attr("required", false);
            $("#MIFeeAmount").attr("required", false);
        }
        else if (_type == 3 || _type == 4) {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#MIFeePercent").attr("required", false);
            $("#BitMaxRangeTo").attr("required", false);

        }
        else if (_type == 5) {
            $("#RangeFrom").attr("required", false);
            $("#RangeTo").attr("required", false);
            $("#DateAmortize").attr("required", false);
            $("#MIFeeAmount").attr("required", false);
            $("#BitMaxRangeTo").attr("required", false);
        }
    }

    function refreshAgentFeeSetup() {
        var gridDetail = $("#gridAddAgentFee").data("kendoGrid");
        gridDetail.dataSource.read();
    }



    $("#BtnAgentTreeSetup").click(function () {
        showAgentTreeSetup();
    });

    function showAgentTreeSetup(e) {
        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundComboRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFund").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamFund,
                    index: 0
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeParamFund() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            InitGridInformationAgentTreeSetup();
            $("#gridDetailAgentTreeSetup").empty();
        }


        $.ajax({
            url: window.location.origin + "/Radsoft/Agent/GetAgentComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamAgent").kendoComboBox({
                    dataValueField: "AgentPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: onChangeParamAgent,
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });


        function onChangeParamAgent() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            InitGridInformationAgentTreeSetup();
            $("#gridDetailAgentTreeSetup").empty();

        }



        WinAgentTreeSetup.center();
        WinAgentTreeSetup.open();

    }


    function onWinAgentTreeSetupClose() {
        $('#ParamFund').val("");
        $("#ParamAgent").val("");
        $("#ParamDate").data("kendoDatePicker").value(null);
    }


    //$("#BtnShowAgentTreeSetup").click(function () {
    //    InitGridInformationAgentTreeSetup();
    //    InitGridDetailAgentTreeSetup();
    //});

    function getDataSourceInformationAgentTreeSetup(_url) {
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
                            Date: { type: "date" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },

                        }
                    }
                },

            });
    }


    function getDataSourceDetailAgentTreeSetup(_url) {
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
                            FundID: { type: "string", editable: false },
                            ParentID: { type: "string", editable: false },
                            ParentName: { type: "string", editable: false },
                            Levels: { type: "number", editable: false },
                            FeePercent: { type: "number", editable: true },
                            ChildPK: { type: "number", editable: false },
                            ParentPK: { type: "number", editable: false },

                        }
                    }
                },

            });
    }

    function InitGridInformationAgentTreeSetup() {
        $("#gridInformationAgentTreeSetup").empty();
        if ($("#ParamFund").val() == "" || $("#ParamFund").val() == "" || $("#ParamFund").val() == "ALL") {
            _paramFund = "0";
        }
        else {
            _paramFund = $("#ParamFund").val();
        }

        if ($("#ParamAgent").val() == "" || $("#ParamAgent").val() == "") {
            _paramAgent = "0";
        }
        else {
            _paramAgent = $("#ParamAgent").val();
        }



        var Info = window.location.origin + "/Radsoft/Agent/GetDataInformationAgentTreeSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _paramFund + "/" + _paramAgent,
            dataSourceInformationAgentTreeSetup = getDataSourceInformationAgentTreeSetup(Info);


        gridInformationAgentTreeSetup = $("#gridInformationAgentTreeSetup").kendoGrid({
            dataSource: dataSourceInformationAgentTreeSetup,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "300px",
            excel: {
                fileName: "Information_AgentTreeSetup.xlsx"
            },
            columns: [
                { command: { text: "Details", click: showDetailsAgentTreeSetup }, title: " ", width: 80 },
                { field: "Date", title: "Date", width: 100, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                {
                    field: "FundPK", title: "Fund PK", headerAttributes: {
                        style: "text-align: center"
                    }, width: 400, hidden: true,
                },
                {
                    field: "FundID", title: "Fund ID", headerAttributes: {
                        style: "text-align: center"
                    }, width: 400
                },



            ]
        }).data("kendoGrid");
    }


    function showDetailsAgentTreeSetup(e) {
        if (e.handled == true) // This will prevent event triggering more then once
        {
            return;
        }
        e.handled = true;
        grid = $("#gridInformationAgentTreeSetup").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        InitGridDetailAgentTreeSetup(kendo.toString(kendo.parseDate(dataItemX.Date), 'MM-dd-yy'), dataItemX.FundPK);

    }

    function InitGridDetailAgentTreeSetup(_date, _fundPK) {
        $("#gridDetailAgentTreeSetup").empty();
        //if (_fundPK == "" || _fundPK == null || _fundPK == "ALL") {
        //    _paramFund = "0";
        //}
        //else {
        //    _paramFund = _fundPK;
        //}

        _paramAgent = $("#ParamAgent").val();

        var Detail = window.location.origin + "/Radsoft/Agent/GetDataDetailAgentTreeSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _date + "/" + _fundPK + "/" + _paramAgent,
            dataSourceDetailAgentTreeSetup = getDataSourceDetailAgentTreeSetup(Detail);


        gridDetailAgentTreeSetup = $("#gridDetailAgentTreeSetup").kendoGrid({
            dataSource: dataSourceDetailAgentTreeSetup,
            reorderable: true,
            sortable: true,
            resizable: true,
            scrollable: {
                virtual: true
            },
            pageable: false,
            filterable: { extra: true, operators: { string: { contains: "Contain", eq: "Is equal to", neq: "Is not equal to" } } },
            height: "500px",
            editable: "incell",
            excel: {
                fileName: "Detail_AgentTreeSetup.xlsx"
            },
            columns: [
                { command: { text: "Update", click: _update }, title: " ", width: 80 },
                { field: "Date", title: "Date", hidden: true, width: 100, template: "#= kendo.toString(kendo.parseDate(Date), 'dd/MMM/yyyy')#" },
                {
                    field: "FundID", title: "Fund ID", headerAttributes: {
                        style: "text-align: center"
                    }, width: 100
                },
                {
                    field: "ParentID", title: "Parent ID", headerAttributes: {
                        style: "text-align: center"
                    }, width: 150
                },

                {
                    field: "ParentName", title: "Parent Name", headerAttributes: {
                        style: "text-align: center"
                    }, width: 200
                },
                {
                    field: "Levels", title: "Levels", format: "{0:n0}", attributes: { style: "text-align:right;" }, headerAttributes: {
                        style: "text-align: center"
                    }, width: 100,

                },
                {
                    field: "FeePercent", title: "Fee %", format: "{0:n4}", attributes: { style: "text-align:right;" },
                    headerAttributes: {
                        style: "text-align: center"
                    }, width: 100
                },
                {
                    field: "ChildPK", title: "ChildPK", headerAttributes: {
                        style: "text-align: center"
                    }, width: 100, hidden: true,
                },
                {
                    field: "ParentPK", title: "ParentPK", headerAttributes: {
                        style: "text-align: center"
                    }, width: 100, hidden: true,
                },
                {
                    field: "FundPK", title: "FundPK", headerAttributes: {
                        style: "text-align: center"
                    }, width: 100, hidden: true,
                },

            ]
        }).data("kendoGrid");
    }

    $("#BtnGenerateAgentTreeSetup").click(function () {
        var _date = kendo.toString($("#ParamDate").data("kendoDatePicker").value(), "MM-dd-yyyy");
        if (_date == null || _date == "") {
            alertify.alert("Please Fill Date");
            return;
        }
        if ($("#ParamFund").val() == "") {
            _paramFund = "0";
        }
        else {
            _paramFund = $("#ParamFund").val();
        }
        if ($("#ParamAgent").val() == "") {
            alertify.alert("Please Choose Agent First !")
            return;
        }

        alertify.confirm("Are you sure want to Generate data for " + _date, function (e) {
            if (e) {
                $.blockUI({});
                var AgentTreeSetup = {
                    ParamDate: _date,
                    ParamFund: _paramFund,
                    ParamAgent: $("#ParamAgent").val(),
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Agent/GenerateAgentTreeSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(AgentTreeSetup),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        alertify.alert(data);
                        $.unblockUI();
                        InitGridInformationAgentTreeSetup();
                        $("#gridDetailAgentTreeSetup").empty();
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                        $.unblockUI();
                    }
                });
            }
        });

    });


    function _update(e) {

        if (e) {
            var grid;
            grid = $("#gridDetailAgentTreeSetup").data("kendoGrid");
            dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));

            var AgentTreeSetup = {
                Date: dataItemX.Date,
                FundPK: dataItemX.FundPK,
                ChildPK: dataItemX.ChildPK,
                ParentPK: dataItemX.ParentPK,
                Levels: dataItemX.Levels,
                FeePercent: dataItemX.FeePercent,
            };

            $.ajax({
                url: window.location.origin + "/Radsoft/Agent/ValidateUpdateAgentTreeSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                type: 'POST',
                data: JSON.stringify(AgentTreeSetup),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == false) {
                        alertify.confirm("Are you sure want to Update Data ?", function (e) {
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Agent/UpdateAgentTreeSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                type: 'POST',
                                data: JSON.stringify(AgentTreeSetup),
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    alertify.alert("Update Success");
                                    InitGridDetailAgentTreeSetup(kendo.toString(kendo.parseDate(dataItemX.Date), 'MM-dd-yy'), dataItemX.FundPK);

                                },
                                error: function (data) {
                                    alertify.alert(data.responseText);
                                }
                            });
                        });

                    } else {
                        alertify.alert("Total Fee Percent > 100% !");
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


    function onWinAgentTreeSetupClose() {
        clearDataAgentTreeSetup();
    }

    function clearDataAgentTreeSetup() {
        $("#gridInformationAgentTreeSetup").empty();
        $("#gridDetailAgentTreeSetup").empty();
        $("#ParamAgent").val("");
    }
});