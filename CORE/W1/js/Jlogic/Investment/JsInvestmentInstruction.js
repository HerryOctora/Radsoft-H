$(document).ready(function () {
    document.title = 'FORM INVESTMENT INSTRUCTION';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex;
    var gridHeight = screen.height - 300;
    var WinInvestmentListing;
    var WinListInstrument;
    var htmlInstrumentPK;
    var htmlInstrumentID;
    var htmlInstrumentName;
    var WinListCounterpart;
    var htmlCounterpartPK;
    var htmlCounterpartID;
    var htmlCounterpartName;
    var dirty;
    var upOradd;
    var _d = new Date();
    var _fy = _d.getFullYear();
    var GlobInstrumentType;
    var GlobTrxType;
    var GlobStatusDealing;
    var _defaultPeriodPK;

    $.ajax({
        url: window.location.origin + "/Radsoft/Period/GetPeriodPkByID/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + _fy,
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            _defaultPeriodPK = data;
        },
        error: function (data) {
            alertify.alert(data.responseText);
        }
    });

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
        $("#BtnListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });
        $("#BtnCancelListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnListFundExposurePreTradeListing").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnPosting.png"
        });

        $("#BtnOkListingFundExposure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnSave.png"
        });

        $("#BtnCancelListingFundExposure").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnClose.png"
        });
        $("#BtnApproveBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnApproveAll.png"
        });

        $("#BtnRejectBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnRejectAll.png"
        });
        $("#BtnVoidBySelected").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnVoidAll.png"
        });
        $("#BtnOpenNewTab").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnAdd.png"
        });
    }


    function initWindow() {
        $("#ParamListFundExposureDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        $("#ParamListDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: new Date(),
            change: onChangeParamListDate,
        });

        function onChangeParamListDate() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#ParamReferenceFrom").data("kendoComboBox").text("");
            $("#ParamReferenceTo").data("kendoComboBox").text("");
            GetReferenceFromInvestment();
            //GetMessageFromInvestmentNotes();
        }

        $("#FilterInstrumentType").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            dataSource: [
                    { text: "EQUITY", value: "1" },
                    { text: "BOND", value: "2" },
                    { text: "DEPOSITO", value: "3" },
            ],
            change: onChangeFilterInstrumentType,

        });

        function onChangeFilterInstrumentType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            refresh();
        }

        $("#LastCouponDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeLastCouponDate,
        });

        $("#NextCouponDate").kendoDatePicker({
            format: "MM/dd/yyyy",

        });
        $("#NextCouponDate").data("kendoDatePicker").enable(false);
        $("#MaturityDate").kendoDatePicker({
            format: "MM/dd/yyyy",
        });

        $("#MaturityDate").data("kendoDatePicker").enable(false);

        $("#SettledDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeSettledDate
        });

        $("#AcqDate").kendoDatePicker({
            format: "MM/dd/yyyy",
        });
        $("#AcqDate").data("kendoDatePicker").enable(false);

        function OnChangeSettledDate() {

            if (GlobInstrumentType == 2) {
                if ($("#LastCouponDate").data("kendoDatePicker").value() != null
                          && $("#LastCouponDate").data("kendoDatePicker").value() != ''
                           && $("#SettledDate").data("kendoDatePicker").value() != ''
                           && $("#SettledDate").data("kendoDatePicker").value() != null
                          && $('#InstrumentPK').val() != ''
                          && $('#InstrumentPK').val() != null
                          && $("#OrderPrice").data("kendoNumericTextBox").value() != ''
                          && $("#OrderPrice").data("kendoNumericTextBox").value() != null
                          && $("#Volume").data("kendoNumericTextBox").value() != ''
                          && $("#Volume").data("kendoNumericTextBox").value() != null
                          ) {
                    GetBondInterest();
                }
            }
            else if (GlobInstrumentType == 3) {
                GetTenorForTimeDeposit();
               
            }  
      
        }

        function OnChangeLastCouponDate() {
            if (GlobInstrumentType == 2) {
                if ($("#LastCouponDate").data("kendoDatePicker").value() != null
                          && $("#LastCouponDate").data("kendoDatePicker").value() != ''
                           && $("#SettledDate").data("kendoDatePicker").value() != ''
                           && $("#SettledDate").data("kendoDatePicker").value() != null
                          && $('#InstrumentPK').val() != ''
                          && $('#InstrumentPK').val() != null
                          && $("#OrderPrice").data("kendoNumericTextBox").value() != ''
                          && $("#OrderPrice").data("kendoNumericTextBox").value() != null
                          && $("#Volume").data("kendoNumericTextBox").value() != ''
                          && $("#Volume").data("kendoNumericTextBox").value() != null
                          ) {
                    GetBondInterest();
          
                }
                GetNextCouponDate();
            }
        }

        $("#InstructionDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            value: new Date(),
        });

        $("#DateFrom").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateFrom,
            value: new Date()
        });
        $("#DateTo").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeDateTo,
            value: new Date()
        });
        $("#ValueDate").kendoDatePicker({
            format: "MM/dd/yyyy",
            change: OnChangeValueDate,
        });

        function OnChangeDateFrom() {
            var _DateFrom = Date.parse($("#DateFrom").data("kendoDatePicker").value());
            //Check if Date parse is successful
            if (!_DateFrom) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
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
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                $("#DateTo").data("kendoDatePicker").value(new Date());
                return;
            }

            refresh();
        }
        function OnChangeValueDate() {
            $("#PeriodPK").data("kendoComboBox").text($("#ValueDate").data("kendoDatePicker").value().getFullYear().toString());
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
            }
            else {
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

                //Cek WorkingDays
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetNextWorkingDay/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + 3,
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#SettledDate").data("kendoDatePicker").value(new Date(data));
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }

        }

        win = $("#WinInvestmentInstruction").kendoWindow({
            height: 1100,
            title: "Investment Instruction Detail",
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
            },

        }).data("kendoWindow");

        WinListInstrument = $("#WinListInstrument").kendoWindow({
            height: 500,
            title: "Instrument List",
            visible: false,
            width: 900,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListInstrumentClose
        }).data("kendoWindow");

        WinListFundExposurePreTrade = $("#WinListFundExposurePreTrade").kendoWindow({
            height: 600,
            title: "Fund Exposure Pre Trade List",
            visible: false,
            width: 1300,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListFundExposurePreTradeClose
        }).data("kendoWindow");

        WinListFundExposurePreTradeListing = $("#WinListFundExposurePreTradeListing").kendoWindow({
            height: 250,
            title: "Fund Exposure Pre Trade",
            visible: false,
            width: 600,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListFundExposurePreTradeListingClose
        }).data("kendoWindow");

        WinListCounterpart = $("#WinListCounterpart").kendoWindow({
            height: 500,
            title: "Counterpart List",
            visible: false,
            width: 900,
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            close: onWinListCounterpartClose
        }).data("kendoWindow");

        WinInvestmentListing = $("#WinInvestmentListing").kendoWindow({
            height: 400,
            title: "* Listing Investment",
            visible: false,
            width: 900,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
            close: onWinInvestmentListingClose
        }).data("kendoWindow");
    }

    var GlobValidator = $("#WinInvestmentInstruction").kendoValidator().data("kendoValidator");

    function validateData() {
        
        if ($("#ValueDate").val() != "") {
            var _date = Date.parse($("#ValueDate").data("kendoDatePicker").value());

            //Check if Date parse is successful
            if (!_date) {
                
                alertify.alert("Wrong Format Date DD/MM/YYYY");
                return 0;
            }
        }
        if ($("#Amount").data("kendoNumericTextBox").value() == 0) {
            alertify.alert("Amount must be filled");
            return 0;
        }
        if (GlobValidator.validate()) {
            return 1;
        }
       
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }

    function showDetails(e) {
        ClearRequiredAttribute();
        if (e == null) {
          
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
            GlobInstrumentType = 0;
            GlobTrxType = 0;
            $("#InstructionDate").data("kendoDatePicker").value(_d);
        } else {
            if (e.handled == true) // This will prevent event triggering more then once
            {
                return;
            }
            e.handled = true;
            var grid;
            if (tabindex == 0 || tabindex == undefined) {
                grid = $("#gridInvestmentInstructionApproved").data("kendoGrid");
            }
            if (tabindex == 1) {
                grid = $("#gridInvestmentInstructionPending").data("kendoGrid");
            }
            if (tabindex == 2) {
                grid = $("#gridInvestmentInstructionHistory").data("kendoGrid");
            }
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.StatusInvestment == 1) {
                $("#StatusHeader").text("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUnApproved").hide();
                $("#TrxInformation").hide();
                $("#BtnPosting").hide();
                $("#BtnRevise").hide();
                $("#BtnOldData").show();
            }

            if (dataItemX.StatusInvestment == 2 && dataItemX.Posted == 1 && dataItemX.Revised == 0) {
                $("#StatusHeader").text("POSTED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnUnApproved").hide();
                $("#BtnVoid").hide();
                $("#BtnOldData").hide();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
                $("#TrxInformation").hide();

            }

            if (dataItemX.StatusInvestment == 2 && dataItemX.Revised == 1) {
                $("#StatusHeader").text("REVISED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").show();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
                $("#TrxInformation").hide();
                $("#BtnOldData").hide();

            }

            if (dataItemX.StatusInvestment == 2 && dataItemX.Posted == 0 && dataItemX.Revised == 0) {
                $("#StatusHeader").text("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
                $("#BtnUnApproved").hide();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
                $("#TrxInformation").hide();
                $("#BtnVoid").show();
                $("#BtnOldData").hide();
            }
            if (dataItemX.StatusInvestment == 3) {
                $("#StatusHeader").text("VOID");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnUnApproved").hide();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
                $("#TrxInformation").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.StatusInvestment == 4) {
                $("#StatusHeader").text("WAITING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnUnApproved").hide();
                $("#BtnRevise").hide();
                $("#BtnPosting").hide();
                $("#TrxInformation").hide();
                $("#BtnOldData").hide();
            }

            $("#InvestmentPK").val(dataItemX.InvestmentPK);
            $("#DealingPK").val(dataItemX.DealingPK);
            $("#StatusInvestment").val(dataItemX.StatusInvestment);
            $("#StatusDealing").val(dataItemX.StatusDealing);
            GlobStatusDealing = dataItemX.StatusDealing;
            GlobInstrumentType = dataItemX.InstrumentTypePK;
            GlobTrxType = dataItemX.TrxType;
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#ValueDate").data("kendoDatePicker").value(dataItemX.ValueDate);
            $("#InstructionDate").data("kendoDatePicker").value(dataItemX.InstructionDate);
            $("#Reference").val(dataItemX.Reference);
            $("#CounterpartPK").val(dataItemX.CounterpartPK);
            $("#CounterpartID").val(dataItemX.CounterpartID + " - " + dataItemX.CounterpartName);
            $("#InstrumentPK").val(dataItemX.InstrumentPK);
            $("#InstrumentID").val(dataItemX.InstrumentID + " - " + dataItemX.InstrumentName);
            $("#RangePrice").val(dataItemX.RangePrice);
            $("#SettledDate").data("kendoDatePicker").value(dataItemX.SettledDate);
            $("#LastCouponDate").data("kendoDatePicker").value(dataItemX.LastCouponDate);
            $("#NextCouponDate").data("kendoDatePicker").value(dataItemX.NextCouponDate);
            $("#MaturityDate").data("kendoDatePicker").value(dataItemX.MaturityDate);
            $("#AcqDate").data("kendoDatePicker").value(dataItemX.AcqDate);
            $("#InvestmentNotes").val(dataItemX.InvestmentNotes);     
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

        if (GlobInstrumentType == 3 && GlobTrxType == 3) {
            $("#MaturityDate").data("kendoDatePicker").enable(true);

        }
        else if (GlobInstrumentType == 3 && GlobTrxType == 2) {
            $("#MaturityDate").data("kendoDatePicker").enable(true);

        }
        else {
            $("#MaturityDate").data("kendoDatePicker").enable(false);

        }

        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/TransactionType/" + GlobInstrumentType,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#TrxType").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: onChangeTrxType,
                    value: setCmbTrxType()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function onChangeTrxType() {
            GlobTrxType = this.value();

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            if (GlobTrxType == 1) {
                $("#LblAcqPrice").hide();
                $("#LblAcqDate").hide();
                $("#InterestPercent").data("kendoNumericTextBox").enable(false);
                if (GlobInstrumentType == 3)
                {
                    $("#MaturityDate").data("kendoDatePicker").enable(true);
                }
                else
                {
                    $("#MaturityDate").data("kendoDatePicker").enable(false);
                }
             
            }
            else if (GlobTrxType == 2) {
                if (GlobInstrumentType == 3) {
                    $("#LblAcqPrice").hide();
                    $("#LblAcqDate").show();
                }
                else if (GlobInstrumentType == 1) {
                    $("#LblAcqPrice").show();
                    $("#LblAcqDate").hide();
                }
                else {
                    $("#LblAcqPrice").show();
                    $("#LblAcqDate").show();
                }

            }
            else if (GlobTrxType == 3) {
                $("#InterestPercent").data("kendoNumericTextBox").enable(true);
                $("#MaturityDate").data("kendoDatePicker").enable(true);
                if (GlobInstrumentType == 3) {
                    $("#LblAcqPrice").hide();
                    $("#LblAcqDate").show();
                }
                else {
                    $("#LblAcqPrice").show();
                    $("#LblAcqDate").show();
                }
            }
            ClearDataAttributes();
         
            
        }

        function setCmbTrxType() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TrxType == 0) {
                    return "";
                } else {
                    return dataItemX.TrxType;
                }
            }
        }

        $("#Volume").kendoNumericTextBox({
            format: "n0",
            change: OnChangeVolume,
            value: setVolume(),

        });

        function setVolume() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Volume == 0) {
                    return "";
                } else {
                    return dataItemX.Volume;
                }
            }
        }

     
        function OnChangeVolume() {
            $("#Lot").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() / 100);
            InvestmentRecalculate();

        }


        $("#Lot").kendoNumericTextBox({
            format: "n0",
            decimals:0,
            change: OnChangeLot,
            value: setLot(),

        });

        function setLot() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Lot == 0) {
                    return "";
                } else {
                    return dataItemX.Lot;
                }
            }
        }

        function OnChangeLot() {
            $("#Volume").data("kendoNumericTextBox").value($("#Lot").data("kendoNumericTextBox").value() * 100);
            InvestmentRecalculate();
        }

        $("#InterestPercent").kendoNumericTextBox({
            format: "##.###### \\%",
            decimals: 6,
            value: setInterestPercent(),


        });
        function setInterestPercent() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestPercent == 0) {
                    return "";
                } else {
                    return dataItemX.InterestPercent;
                }
            }
        }
        if (GlobInstrumentType == 3 && GlobTrxType == 3)
        {
            $("#InterestPercent").data("kendoNumericTextBox").enable(true);
        }
        else
        {
            $("#InterestPercent").data("kendoNumericTextBox").enable(false);
        }
    

    

        $("#AccruedInterest").kendoNumericTextBox({
            format: "n0",
            value: setAccruedInterest(),

        });

        function setAccruedInterest() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccruedInterest == 0) {
                    return "";
                } else {
                    return dataItemX.AccruedInterest;
                }
            }
        }
        $("#AccruedInterest").data("kendoNumericTextBox").enable(false);

        $("#Tenor").kendoNumericTextBox({
            format: "n0",
            value: setTenor(),

        });
        function setTenor() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Tenor == 0) {
                    return "";
                } else {
                    return dataItemX.Tenor;
                }
            }
        }

       

        $("#OrderPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setOrderPrice(),
            change: OnChangeOrderPrice
        });
        function setOrderPrice() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OrderPrice == 0) {
                    return "";
                } else {
                    return dataItemX.OrderPrice;
                }
            }
        }


        function OnChangeOrderPrice() {
            InvestmentRecalculate();
        }

        $("#AcqPrice").kendoNumericTextBox({
            format: "n6",
            decimals: 6,
            value: setAcqPrice()
        });
        function setAcqPrice() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AcqPrice == 0) {
                    return "";
                } else {
                    return dataItemX.AcqPrice;
                }
            }
        }

        //$("#AcqPrice").data("kendoNumericTextBox").enable(false);

        $("#Amount").kendoNumericTextBox({
            format: "n0",
            value: setAmount(),
            //change: OnChangeAmount

        });
        function setAmount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Amount == 0) {
                    return "";
                } else {
                    return dataItemX.Amount;
                }
            }
        }
        $("#Amount").data("kendoNumericTextBox").enable(false);

        //function OnChangeAmount() {
        //    if (GlobInstrumentType == 3) {
        //        $("#Volume").data("kendoNumericTextBox").value($("#Amount").data("kendoNumericTextBox").value());
        //        $("#Lot").data("kendoNumericTextBox").value(0);
        //        $("#OrderPrice").data("kendoNumericTextBox").value(1);
        //    } else {
        //        $("#Volume").data("kendoNumericTextBox").value(0);
        //        $("#Lot").data("kendoNumericTextBox").value(0);
        //        $("#OrderPrice").data("kendoNumericTextBox").value(0);
        //    }
        //}



        //combo box PeriodPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Period/GetPeriodCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#PeriodPK").kendoComboBox({
                    dataValueField: "PeriodPK",
                    dataTextField: "ID",
                    enable: false,
                    dataSource: data,
                    change: OnChangePeriodPK,
                    value: setCmbPeriodPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangePeriodPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbPeriodPK() {
            if (e == null) {
                return _defaultPeriodPK;
            } else {
                if (dataItemX.PeriodPK == 0) {
                    return "";
                } else {
                    return dataItemX.PeriodPK;
                }
            }
        }


        //Instrument Type

        $("#InstrumentTypePK").kendoComboBox({
            dataValueField: "value",
            dataTextField: "text",
            filter: "contains",
            suggest: true,
            dataSource: [
                    { text: "EQUITY", value: "1" },
                    { text: "BOND", value: "2" },
                    { text: "DEPOSITO", value: "3" },
            ],
            change: onChangeInstrumentType,
            value: setCmbInstrumentTypePK()
        });
        function setCmbInstrumentTypePK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InstrumentTypePK == 0) {
                    return "";
                } else {
                    return dataItemX.InstrumentTypePK;
                }
            }
        }
        function onChangeInstrumentType() {

            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


            GlobInstrumentType = this.value();
            //BOND
            ShowHideLabelByInstrumentType(GlobInstrumentType);


            $.ajax({
                url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueComboTransactionType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TransactionType" + "/" + "/" + GlobInstrumentType,
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $("#TrxType").kendoComboBox({
                        dataValueField: "Code",
                        dataTextField: "DescOne",
                        filter: "contains",
                        suggest: true,
                        dataSource: data,
                        change: onChangeTrxType,
                        value: setCmbTrxType()
                    });

                },
                error: function (data) {
                    alertify.alert("Please Choose Instrument Type First");

                }
            });

            function onChangeTrxType() {
                GlobTrxType = this.value();

                if (this.value() && this.selectedIndex == -1) {
                    var dt = this.dataSource._data[0];
                    this.text('');
                }
                if (GlobTrxType == 1) {
                    $("#LblAcqPrice").hide();
                    $("#LblAcqDate").hide();
                    $("#InterestPercent").data("kendoNumericTextBox").enable(false);
                    if (GlobInstrumentType == 3) {
                        $("#MaturityDate").data("kendoDatePicker").enable(true);
                    }
                    else {
                        $("#MaturityDate").data("kendoDatePicker").enable(false);
                    }

                }
                else if (GlobTrxType == 2) {
                    $("#InterestPercent").data("kendoNumericTextBox").enable(false);
                    $("#MaturityDate").data("kendoDatePicker").enable(false);
                    if (GlobInstrumentType == 3) {
                        $("#LblAcqPrice").hide();
                        $("#LblAcqDate").show();
                    }
                    else if (GlobInstrumentType == 1) {
                        $("#LblAcqPrice").show();
                        $("#LblAcqDate").hide();
                    }
                    else {
                        $("#LblAcqPrice").show();
                        $("#LblAcqDate").show();
                    }

                }
                else if (GlobTrxType == 3) {
                    $("#InterestPercent").data("kendoNumericTextBox").enable(true);
                    $("#MaturityDate").data("kendoDatePicker").enable(true);
                    if (GlobInstrumentType == 3) {
                        $("#LblAcqPrice").hide();
                        $("#LblAcqDate").show();
                    }
                    else {
                        $("#LblAcqPrice").show();
                        $("#LblAcqDate").show();
                    }
                }

                ClearDataAttributes();
            }


            function setCmbTrxType() {
                if (e == null) {
                    return "";
                } else {
                    if (dataItemX.TrxType == 0) {
                        return "";
                    } else {
                        return dataItemX.TrxType;
                    }
                }
            }

            $("#InstrumentPK").val("");
            $("#InstrumentID").val("");
            $("#InstrumentName").val("");
            $("#AcqPrice").data("kendoNumericTextBox").value(0);
            if (GlobInstrumentType == 3)
            {
                $("#OrderPrice").data("kendoNumericTextBox").value(1);
            }
            
            if (GlobInstrumentType == 3) {
                $("#OrderPrice").data("kendoNumericTextBox").value(1);
                $("#OrderPrice").data("kendoNumericTextBox").enable(false);
            } else {
                $("#OrderPrice").data("kendoNumericTextBox").value(0);
                $("#OrderPrice").data("kendoNumericTextBox").enable(true);
            }
            
            $("#Volume").data("kendoNumericTextBox").value(0);
            $("#Amount").data("kendoNumericTextBox").value(0);
            $("#Lot").data("kendoNumericTextBox").value(0);
            $("#InterestPercent").data("kendoNumericTextBox").value(0);
            $("#MaturityDate").data("kendoDatePicker").value(null);
            $("#LastCouponDate").data("kendoDatePicker").value(null);
            $("#NextCouponDate").data("kendoDatePicker").value(null);
            $("#AcqDate").data("kendoDatePicker").value(null);
            //if (GlobInstrumentType != 1) {
            //    $("#SettledDate").data("kendoDatePicker").value(null);
            //}
            $("#Tenor").data("kendoNumericTextBox").value(0);
            $("#AccruedInterest").data("kendoNumericTextBox").value(0);
        }

        if (e != null) {
            ShowHideLabelByInstrumentType(dataItemX.InstrumentTypePK, dataItemX.TrxType);
        }

        //Combo Box Fund 
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#FundPK").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
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



        win.center();
        win.open();
    }

    function ClearRequiredAttribute() {
        $("#OrderPrice").removeAttr("required");
        $("#AcqPrice").removeAttr("required");
        $("#Lot").removeAttr("required");
        $("#LotInShare").removeAttr("required");
        $("#SettledDate").removeAttr("required");
        $("#MaturityDate").removeAttr("required");
        $("#LastCouponDate").removeAttr("required");
        $("#NextCouponDate").removeAttr("required");
        $("#InterestPercent").removeAttr("required");
        $("#AccruedInterest").removeAttr("required");
        $("#Tenor").removeAttr("required");
        $("#InterestPercent").removeAttr("required");
        $("#AccruedInterest").removeAttr("required");
        $("#Volume").removeAttr("required");
       
    }

    function ShowHideLabelByInstrumentType(_type, _trxType) {
        ClearRequiredAttribute();
        GlobValidator.hideMessages();
        //BOND
        if (_type == 2) {

                $("#LblLot").hide();
                $("#LblLotInShare").hide();
                $("#LblVolume").hide();
                $("#LblNominal").show();
                $("#LblInterestPercent").show();
                $("#LblLastCouponDate").show();
                $("#LblNextCouponDate").show();
                //$("#LblSettlementDate").hide();
                $("#LblPrice").show();
                $("#LblRangePrice").show();
                $("#LblAccruedInterest").show();
                $("#LblMaturityDate").show();
                $("#LblTenor").show();
                if (_trxType == 1) {
                    $("#LblAcqPrice").hide();
                    $("#LblAcqDate").hide();
                }
                else
                {
                    $("#LblAcqDate").show();
                    $("#LblAcqPrice").show();
                }
                //$("#OrderPrice").attr("required", true);
                //$("#AcqPrice").attr("required", true);
                $("#Tenor").attr("required", true);
                $("#AccruedInterest").attr("required", true);
                //$("#Volume").attr("required", true);
                $("#InterestPercent").attr("required", true);
                $("#MaturityDate").attr("required", true);
                $("#NextCouponDate").attr("required", true);
                $("#LastCouponDate").attr("required", true);
            }

           


        
            //EQUITY
            else if (_type == 1) {

                $("#LblLot").show();
                $("#LblLotInShare").show();
                $("#LblVolume").show();
                $("#LblNominal").hide();
                $("#LblInterestPercent").hide();
                $("#LblMaturityDate").hide();
                //$("#LblSettlementDate").hide();
                $("#LblPrice").show();
                $("#LblRangePrice").show();
                $("#LblLastCouponDate").hide();
                $("#LblNextCouponDate").hide();
                $("#LblAccruedInterest").hide();
                $("#LblTenor").hide();
                // GANTI BAGIAN SINI KLO ADA PERUBAHAN LOT IN SHARE
                $("#LotInShare").val(100);
                if (_trxType == 1) {
                    $("#LblAcqPrice").hide();
                    $("#LblAcqDate").hide();
                }
                else {
                    $("#LblAcqDate").show();
                    $("#LblAcqPrice").show();
                }

            }

       
        
            //$("#AcqPrice").attr("required", true);
            //$("#OrderPrice").attr("required", true);
            //$("#Volume").attr("required", true);
            //$("#Lot").attr("required", true);
            //$("#LotInShare").attr("required", true);
            //$("#SettledDate").attr("required", true);
        
            //DEPOSITO
            else if (_type == 3) {
            $("#LblLot").hide();
            $("#LblLotInShare").hide();
            $("#LblVolume").hide();
            $("#LblNominal").show();
            //$("#LblSettlementDate").hide();
            $("#LblLastCouponDate").hide();
            $("#LblNextCouponDate").hide();
            $("#LblInterestPercent").show();
            $("#LblMaturityDate").show();
            $("#LblAccruedInterest").hide();
            //$("#LblAccruedInterest").hide();
            $("#LblPrice").show();
            $("#LblRangePrice").hide();
            $("#LblTenor").show();
            //$("#LblAcqDate").hide();
            //$("#Volume").attr("required", true);
            //$("#OrderPrice").attr("required", true);
            $("#MaturityDate").attr("required", true);
            $("#InterestPercent").attr("required", true);
            $("#OrderPrice").data("kendoNumericTextBox").value(1);

            if (_trxType == 1) {
                $("#LblAcqPrice").hide();
                $("#LblAcqDate").hide();
            }
            else {
                $("#LblAcqDate").show();
                $("#LblAcqPrice").show();
            }
        }


    }

    function ClearDataAttributes() {
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#OrderPrice").data("kendoNumericTextBox").value(0);
        $("#AcqPrice").data("kendoNumericTextBox").value(0);
        $("#Volume").data("kendoNumericTextBox").value(0);
        $("#Lot").data("kendoNumericTextBox").value(0);
        $("#Amount").data("kendoNumericTextBox").value(0);
        $("#InterestPercent").data("kendoNumericTextBox").value(0);
        $("#Tenor").data("kendoNumericTextBox").value(0);
        $("#AccruedInterest").data("kendoNumericTextBox").value(0);
        $("#AcqDate").data("kendoDatePicker").value(null);
        //$("#SettledDate").data("kendoDatePicker").value(null);
        $("#MaturityDate").data("kendoDatePicker").value(null);
        $("#LastCouponDate").data("kendoDatePicker").value(null);
        $("#NextCouponDate").data("kendoDatePicker").value(null);
    }

    function ClearDataBond() {

        $("#Lot").val("");
        $("#LotInShare").val("");
        $("#AcqPrice").val("");
        $("#AcqDate").data("kendoDatePicker").value("");

    }

    function ClearDataEquity() {
        $("#AccruedInterest").val("");
        $("#InterestPercent").val("");
        $("#MaturityDate").data("kendoDatePicker").value("");
        $("#AcqPrice").val("");
        $("#AcqDate").data("kendoDatePicker").value("");
    }

    function ClearDataMoney() {
        $("#Lot").val("");
        $("#LotInShare").val("");
        //$("#SettledDate").data("kendoDatePicker").value($("#ValueDate").data("kendoDatePicker").value());
        $("#AcqPrice").val("");
        //$("#AcqDate").data("kendoDatePicker").value("");
    }

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#InvestmentPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#PeriodPK").val("");
        $("#PeriodID").val("");
        $("#ValueDate").data("kendoDatePicker").value(null);
        $("#InstructionDate").data("kendoDatePicker").value(null);
        $("#Reference").val("");
        $("#InstrumentTypePK").val("");
        $("#TrxType").val("");
        $("#TrxTypeID").val("");
        $("#CounterpartPK").val("");
        $("#CounterpartID").val("");
        $("#CounterpartName").val("");
        $("#InstrumentPK").val("");
        $("#InstrumentID").val("");
        $("#InstrumentName").val("");
        $("#FundPK").val("");
        $("#FundID").val("");
        $("#OrderPrice").val("");
        $("#AcqPrice").val("");
        $("#RangePrice").val("");
        $("#Lot").val("");
        $("#LotInShare").val("");
        $("#Volume").val("");
        $("#Amount").val("");
        $("#InterestPercent").val("");
        $("#AccruedInterest").val("");
        $("#SettledDate").data("kendoDatePicker").value(null);
        $("#LastCouponDate").data("kendoDatePicker").value(null);
        $("#NextCouponDate").data("kendoDatePicker").value(null);
        $("#Tenor").val("0");
        $("#MaturityDate").data("kendoDatePicker").value(null);
        $("#AcqDate").data("kendoDatePicker").value(null);
        $("#InvestmentNotes").val("");
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
                 pageSize: 500,
                 schema: {
                     model: {
                         fields: {
                             InvestmentPK: { type: "number" },
                             DealingPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Selected: { type: "boolean" },
                             StatusInvestment: { type: "number" },
                             StatusDealing: { type: "number" },
                             StatusSettlement: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             PeriodPK: { type: "number" },
                             PeriodID: { type: "string" },
                             ValueDate: { type: "date" },
                             InstructionDate: { type: "date" },
                             Reference: { type: "string" },
                             InstrumentTypePK: { type: "number" },
                             InstrumentTypeID: { type: "string" },
                             TrxType: { type: "number" },
                             TrxTypeID: { type: "string" },
                             CounterpartPK: { type: "number" },
                             CounterpartID: { type: "string" },
                             CounterpartName: { type: "string" },
                             InstrumentPK: { type: "number" },
                             InstrumentID: { type: "string" },
                             InstrumentName: { type: "string" },
                             FundPK: { type: "number" },
                             FundID: { type: "string" },
                             OrderPrice: { type: "number" },
                             RangePrice: { type: "string" },
                             AcqPrice: { type: "number" },
                             Lot: { type: "number" },
                             LotInShare: { type: "number" },
                             Volume: { type: "number" },
                             Amount: { type: "number" },
                             SettledDate: { type: "date" },
                             InterestPercent: { type: "number" },
                             AccruedInterest: { type: "number" },
                             LastCouponDate: { type: "date" },
                             NextCouponDate: { type: "date" },
                             Tenor: { type: "number" },
                             MaturityDate: { type: "date" },
                             AcqDate: { type: "date" },
                             InvestmentNotes: { type: "string" },
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
                 }, group:
                     [
                     {
                         field: "FundID", aggregates: [
                         { field: "Lot", aggregate: "sum" }
                         ]
                     },
                     {
                         field: "TrxTypeID", aggregates: [
                         { field: "Lot", aggregate: "sum" }
                         ]
                     }

                     ],
             });
    }

    function refresh() {
        if (tabindex == 0 || tabindex == undefined) {
            initGrid();
        }
        if (tabindex == 1) {
            RecalGridPending();
            //var gridPending = $("#gridInvestmentInstructionPending").data("kendoGrid");
            //gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            RecalGridHistory();
            //var gridHistory = $("#gridInvestmentInstructionHistory").data("kendoGrid");
            //gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        $("#gridInvestmentInstructionApproved").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else if ($("#FilterInstrumentType").val() == null || $("#FilterInstrumentType").val() == "" && $("#Date").val() != "") {

            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceApproved = getDataSource(InvestmentInstructionApprovedURL);

        }
        else {
            var InvestmentInstructionApprovedURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FilterInstrumentType').val(),
            dataSourceApproved = getDataSource(InvestmentInstructionApprovedURL);

        }

        var grid = $("#gridInvestmentInstructionApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Investment Instruction"
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
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailApproved' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllApproved' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "InvestmentPK", title: "SysNo.", width: 95 },
                { field: "StatusInvestment", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 200 },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "InstrumentID", title: "Instrument ID", width: 150 },
                { field: "TrxTypeID", title: "Trx Type", width: 150 },
                { field: "OrderPrice", title: "Price", width: 180, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Lot", title: "Lot", width: 150, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "ValueDate", title: "Value Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", width: 150 },
                { field: "Reference", title: "Reference", width: 200 },
                { field: "InstrumentTypeID", title: "Instrument Type", width: 200 },
                { field: "TrxTypeID", title: "Trx Type", width: 200 },
                { field: "InstrumentID", title: "Instrument ID", width: 200 },
                { field: "InstrumentName", title: "Instrument Name", width: 300 },
                { field: "OrderPrice", title: "Order Price", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", width: 200 },
                { field: "Lot", title: "Lot", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                { field: "SettledDate", title: "Settled Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'MM/dd/yyyy')#" },
                { field: "LastCouponDate", title: "Last Coupon Date", width: 200, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", width: 200,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", width: 200 },
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
        }).data("kendoGrid");
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
            

            var grid = $("#gridInvestmentInstructionApproved").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _InvestmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _InvestmentPK);

        }

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabInvestmentInstruction").kendoTabStrip({
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
        $("#BtnRejectBySelected").hide();
        $("#BtnApproveBySelected").hide();
    }

    function ResetButtonBySelectedData() {
        $("#BtnApproveBySelected").show();
        $("#BtnRejectBySelected").show();
        $("#BtnVoidBySelected").show();
    }

    function SelectDeselectData(_a, _b) {
        if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
            var _type = "None";
        }
        else {
            var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectDataInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Investment/" + _a + "/" + _b + "/Investment" + "/" + _type,
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
        if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
            var _type = "None";
        }
        else {
            var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/SelectDeselectAllDataByDateInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Investment/" + _a + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type,
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
        $("#gridInvestmentInstructionPending").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else if ($("#FilterInstrumentType").val() == null || $("#FilterInstrumentType").val() == "" && $("#Date").val() != "") {

            var InvestmentInstructionPendingURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourcePending = getDataSource(InvestmentInstructionPendingURL);

        }
        else {
            var InvestmentInstructionPendingURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FilterInstrumentType').val(),
            dataSourcePending = getDataSource(InvestmentInstructionPendingURL);

        }
        var grid = $("#gridInvestmentInstructionPending").kendoGrid({
            dataSource: dataSourcePending,
            height: gridHeight,
            scrollable: {
                virtual: true
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
                {
                    field: "Selected",
                    width: 50,
                    template: "<input class='cSelectedDetailPending' type='checkbox'   #= Selected ? 'checked=checked' : '' # />",
                    headerTemplate: "<input id='SelectedAllPending' type='checkbox'  />",
                    filterable: true,
                    sortable: false,
                    columnMenu: false
                },
                { field: "InvestmentPK", title: "SysNo.", width: 95 },
                { field: "StatusInvestment", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 200 },
                { field: "FundID", title: "Fund ID", width: 200 },
                { field: "InstrumentID", title: "Instrument ID", width: 150 },
                { field: "TrxTypeID", title: "Trx Type", width: 150 },
                { field: "OrderPrice", title: "Price", width: 180, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Lot", title: "Lot", width: 150, hidden: true, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "ValueDate", title: "Value Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", width: 150 },
                { field: "Reference", title: "Reference", width: 200 },
                { field: "InstrumentTypeID", title: "Instrument Type", width: 200 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true, width: 200 },
                { field: "InstrumentID", title: "Instrument ID", width: 200 },
                { field: "InstrumentName", title: "Instrument Name", width: 300 },
                { field: "OrderPrice", title: "Order Price", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", width: 200 },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                { field: "SettledDate", title: "Settled Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'MM/dd/yyyy')#" },
                { field: "LastCouponDate", title: "Last Coupon Date", width: 200, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", width: 200,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 200 },
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
        }).data("kendoGrid");
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
            

            var grid = $("#gridInvestmentInstructionPending").data("kendoGrid");
            var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
            var _InvestmentPK = dataItemX.InvestmentPK;
            var _checked = this.checked;
            SelectDeselectData(_checked, _InvestmentPK);

        }

        ResetButtonBySelectedData();
        $("#BtnVoidBySelected").hide();

    }
    function RecalGridHistory() {

        $("#gridInvestmentInstructionHistory").empty();
        if ($("#DateFrom").val() == null || $("#DateFrom").val() == "" || $("#DateTo").val() == null || $("#DateTo").val() == "") {
            alertify.alert("Please Fill Date");
        }
        else if ($("#FilterInstrumentType").val() == null || $("#FilterInstrumentType").val() == "" && $("#Date").val() != "") {

            var InvestmentInstructionHistoryURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromTo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy"),
            dataSourceHistory = getDataSource(InvestmentInstructionHistoryURL);

        }
        else {
            var InvestmentInstructionHistoryURL = window.location.origin + "/Radsoft/Investment/GetDataInvestmentByDateFromToAndInstrumentType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9 + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $('#FilterInstrumentType').val(),
            dataSourceHistory = getDataSource(InvestmentInstructionHistoryURL);

        }
        $("#gridInvestmentInstructionHistory").kendoGrid({
            dataSource: dataSourceHistory,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Investment Instruction"
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
                { field: "InvestmentPK", title: "SysNo.", width: 95 },
                { field: "StatusInvestment", title: "Status", hidden: true, filterable: false, width: 100 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 100 },
                { field: "PeriodID", title: "Period ID", hidden: true, width: 200 },
                { field: "ValueDate", title: "Value Date", width: 200, template: "#= kendo.toString(kendo.parseDate(ValueDate), 'MM/dd/yyyy')#" },
                { field: "RefNo", title: "Ref No", width: 150 },
                { field: "Reference", title: "Reference", width: 200 },
                { field: "InstrumentTypeID", title: "Instrument Type", width: 200 },
                { field: "TrxTypeID", title: "Trx Type", hidden: true , width: 200 },
                { field: "InstrumentID", title: "Instrument ID", width: 200 },
                { field: "InstrumentName", title: "Instrument Name", width: 300 },
                { field: "OrderPrice", title: "Order Price", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "RangePrice", title: "Range Price", width: 200 },
                { field: "Lot", title: "Lot", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Volume", title: "Volume", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "Amount", title: "Amount", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                { field: "CounterpartID", title: "Counterpart ID", width: 200 },
                { field: "CounterpartName", title: "Counterpart Name", width: 300 },
                { field: "SettledDate", title: "Settled Date", width: 200, template: "#= kendo.toString(kendo.parseDate(SettledDate), 'MM/dd/yyyy')#" },
                { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'MM/dd/yyyy')#" },
                { field: "LastCouponDate", title: "Last Coupon Date", width: 200, template: "#= kendo.toString(kendo.parseDate(LastCouponDate), 'MM/dd/yyyy')#" },
                { field: "AcqPrice", title: "Acq Price", width: 200, format: "{0:n4}", attributes: { style: "text-align:right;" } },
                { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                {
                    field: "InterestPercent", title: "Interest Percent", width: 200,
                    template: "#: InterestPercent  # %", attributes: { style: "text-align:right;" }
                },
                { field: "FundID", title: "Fund ID", hidden: true, width: 200 },
                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
            ]
        });

        $("#BtnApproveBySelected").hide();
        $("#BtnRejectBySelected").hide();
        $("#BtnVoidBySelected").hide();
    }




    function gridHistoryDataBound() {
        var grid = $("#gridInvestmentInstructionHistory").data("kendoGrid");
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
        showDetails(null);
    });

    $("#BtnAdd").click(function () {
        var val = validateData();

        if (GlobInstrumentType == 2) {
            ClearDataBond();
        }
        else if (GlobInstrumentType == 1) {
            ClearDataEquity();
        }
        else if (GlobInstrumentType == 3) {
            ClearDataMoney();

        }
        if (val == 1) {
            
            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    if ($('#Reference').val() == null || $('#Reference').val() == "") {
                        
                        alertify.confirm("Are you sure want to Generate New Reference ?", function (e) {
                            if (e) {
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/CashierReference/CashierReference_GenerateNewReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/INV/" + $("#PeriodPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + "CashierReference_GenerateNewReference",
                                    type: 'GET',
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        $("#Reference").val(data);
                                        alertify.alert("Your new reference is " + data);
                                        var InvestmentInstruction = {
                                            PeriodPK: $('#PeriodPK').val(),
                                            ValueDate: $('#ValueDate').val(),
                                            Reference: $('#Reference').val(),
                                            InstructionDate: $('#InstructionDate').val(),
                                            InstrumentPK: $('#InstrumentPK').val(),
                                            InstrumentTypePK: GlobInstrumentType,
                                            OrderPrice: $('#OrderPrice').val(),
                                            RangePrice: $('#RangePrice').val(),
                                            AcqPrice: $('#AcqPrice').val(),
                                            Lot: $('#Lot').val(),
                                            LotInShare: $('#LotInShare').val(),
                                            Volume: $('#Volume').val(),
                                            Amount: $('#Amount').val(),
                                            TrxType: $('#TrxType').val(),
                                            TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                            CounterpartPK: $('#CounterpartPK').val(),
                                            SettledDate: $('#SettledDate').val(),
                                            LastCouponDate: $('#LastCouponDate').val(),
                                            NextCouponDate: $('#NextCouponDate').val(),
                                            MaturityDate: $('#MaturityDate').val(),
                                            AcqDate: $('#AcqDate').val(),
                                            InterestPercent: $('#InterestPercent').val(),
                                            AccruedInterest: $('#AccruedInterest').val(),
                                            FundPK: $('#FundPK').val(),
                                            Tenor: $('#Tenor').val(),
                                            InvestmentNotes: $('#InvestmentNotes').val(),
                                            EntryUsersID: sessionStorage.getItem("user")

                                        };
                                        $.ajax({
                                            url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_I",
                                            type: 'POST',
                                            data: JSON.stringify(InvestmentInstruction),
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
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if ($("#Amount").data("kendoNumericTextBox").value() == 0) {
            alertify.alert("Amount must be filled");
            return;
        }
        if (GlobInstrumentType == 2) {
            ClearDataBond();
        }
        else if (GlobInstrumentType == 1) {
            ClearDataEquity();
        }
        else if (GlobInstrumentType == 3) {
            ClearDataMoney();
        }
        //disini
        if (GlobStatusDealing == 0 || GlobStatusDealing == 1) {
            if (val == 1) {
                
                alertify.prompt("Are you sure want to Update, please give notes:","", function (e, str) {
                    if (e) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Investment",
                            type: 'GET',
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                    var InvestmentInstruction = {
                                        InvestmentPK: $('#InvestmentPK').val(),
                                        DealingPK: $('#DealingPK').val(),
                                        HistoryPK: $('#HistoryPK').val(),
                                        StatusInvestment: $('#StatusInvestment').val(),
                                        PeriodPK: $('#PeriodPK').val(),
                                        ValueDate: $('#ValueDate').val(),
                                        Reference: $('#Reference').val(),
                                        InstructionDate: $('#InstructionDate').val(),
                                        InstrumentPK: $('#InstrumentPK').val(),
                                        InstrumentTypePK: GlobInstrumentType,
                                        OrderPrice: $('#OrderPrice').val(),
                                        RangePrice: $('#RangePrice').val(),
                                        AcqPrice: $('#AcqPrice').val(),
                                        Lot: $('#Lot').val(),
                                        LotInShare: $('#LotInShare').val(),
                                        Volume: $('#Volume').val(),
                                        Amount: $('#Amount').val(),
                                        TrxType: $('#TrxType').val(),
                                        TrxTypeID: $("#TrxType").data("kendoComboBox").text(),
                                        CounterpartPK: $('#CounterpartPK').val(),
                                        SettledDate: $('#SettledDate').val(),
                                        LastCouponDate: $('#LastCouponDate').val(),
                                        NextCouponDate: $('#NextCouponDate').val(),
                                        MaturityDate: $('#MaturityDate').val(),
                                        AcqDate: $('#AcqDate').val(),
                                        InterestPercent: $('#InterestPercent').val(),
                                        AccruedInterest: $('#AccruedInterest').val(),
                                        FundPK: $('#FundPK').val(),
                                        Tenor: $('#Tenor').val(),
                                        InvestmentNotes: $('#InvestmentNotes').val(),
                                        Notes: str,
                                        EntryUsersID: sessionStorage.getItem("user")
                                    };
                                    $.ajax({
                                        url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_U",
                                        type: 'POST',
                                        data: JSON.stringify(InvestmentInstruction),
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
        else if (GlobStatusDealing == 3) {
            alertify.alert("Data has been Reject by Dealer, Please Contact Dealer!");
        }
        else {
            alertify.alert("Data has been Approved by Dealer, Please Contact Dealer!");

        }
    });

    $("#BtnOldData").click(function () {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Investment",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "Investment" + "/" + $("#InvestmentPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Investment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var InvestmentInstruction = {
                                InvestmentPK: $('#InvestmentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                InstrumentPK: $('#InstrumentPK').val(),
                                FundPK: $('#FundPK').val(),
                                InterestPercent: $('#InterestPercent').val(),
                                InstrumentTypePK: $("#InstrumentTypePK").val(),
                                TrxType: $("#TrxType").val(),
                                OrderPrice: $("#OrderPrice").val(),
                                Lot: $("#Lot").val(),
                                Volume: $("#Volume").val(),
                                Amount: $("#Amount").val(),
                                AccruedInterest: $("#AccruedInterest").val(),
                                Notes: $("#Notes").val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };

                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_A",
                                type: 'POST',
                                data: JSON.stringify(InvestmentInstruction),
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
        
        if (GlobStatusDealing == 0 || GlobStatusDealing == 1) {
            alertify.confirm("Are you sure want to Void data?", function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Investment",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var InvestmentInstruction = {
                                    InvestmentPK: $('#InvestmentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    VoidUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_V",
                                    type: 'POST',
                                    data: JSON.stringify(InvestmentInstruction),
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
        else if (GlobStatusDealing == 3) {
            alertify.alert("Data has been Reject by Dealer, Please Contact Dealer!");
        }
        else {
            alertify.alert("Data has been Approved by Dealer, Please Contact Dealer!");

        }
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Investment",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var InvestmentInstruction = {
                                InvestmentPK: $('#InvestmentPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_R",
                                type: 'POST',
                                data: JSON.stringify(InvestmentInstruction),
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

    $("#BtnUnApproved").click(function () {
        
        alertify.confirm("Are you sure want to UnApproved data?", function (e) {

            if ($("#State").text() == "POSTED" || $("#State").text() == "REVISED") {
                alert(" Trx Portfolio  Already Posted / Revised, UnApprove Cancel");
            } else {
                if (e) {

                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#InvestmentPK").val() + "/" + $("#HistoryPK").val() + "/" + "Investment",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var InvestmentInstruction = {
                                    InvestmentPK: $('#InvestmentPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/Investment/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "InvestmentInstruction_UnApproved",
                                    type: 'POST',
                                    data: JSON.stringify(InvestmentInstruction),
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
            }
        });
    });

    $("#btnListInstrumentPK").click(function () {
        initListInstrumentPK();

        WinListInstrument.center();
        WinListInstrument.open();
        htmlInstrumentPK = "#InstrumentPK";
        htmlInstrumentID = "#InstrumentID";


    });
    function getDataSourceListInstrument(_url) {



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
                         alertify.alert("Please Fill Value Date and Fund First!");
                         WinListInstrument.close();
                     },
                     pageSize: 100,
                     schema: {
                         model: {
                             fields: {
                                 InstrumentPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" },
                                 InterestPercent: { type: "number" },
                                 MaturityDate: { type: "date" },
                                 AcqDate: { type: "date" },
                                 AvgPrice: { type: "number" },
                                 Balance: { type: "number" },
                             }
                         }
                     }
                 });



    }
    function initListInstrumentPK() {
        GlobInstrumentType = $("#InstrumentTypePK").data("kendoComboBox").value();
        GlobTrxType = $("#TrxType").data("kendoComboBox").value();
        if (GlobInstrumentType == "" || GlobInstrumentType ==null)
        {
            GlobInstrumentType = 0
        }
        if (GlobTrxType == "" || GlobTrxType == null) {
            GlobTrxType = 0
        }
        var _url = window.location.origin + "/Radsoft/Instrument/GetInstrumentByInstrumentTypePKandTrxType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + GlobInstrumentType + "/" + GlobTrxType + "/" + $("#FundPK").data("kendoComboBox").value() + "/" + kendo.toString($("#ValueDate").data("kendoDatePicker").value(), "MM-dd-yy");
            var dsListInstrument = getDataSourceListInstrument(_url);
                  
            if (GlobTrxType == 1 ||  GlobTrxType == 0 || GlobTrxType == 4) {
                $("#gridListInstrument").kendoGrid({
                    dataSource: dsListInstrument,
                    height: "90%",
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    resizable: true,
                    groupable: true,
                    filterable: {
                        extra: false,
                        operators: {
                            string: {
                                contains: "Contain",
                                eq: "Is equal to",
                                neq: "Is not equal to"
                            }
                        }
                    },
                    pageable: true,
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    columns: [
                       { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
                       { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                       { field: "ID", title: "ID", width: 300 },
                       { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                       { field: "InterestPercent", title: "Interest Percent", width: 200 }
                    ]
                });

              
            }
            else {
                $("#gridListInstrument").kendoGrid({
                    dataSource: dsListInstrument,
                    height: "90%",
                    scrollable: {
                        virtual: true
                    },
                    reorderable: true,
                    sortable: true,
                    resizable: true,
                    groupable: true,
                    filterable: {
                        extra: false,
                        operators: {
                            string: {
                                contains: "Contain",
                                eq: "Is equal to",
                                neq: "Is not equal to"
                            }
                        }
                    },
                    pageable: true,
                    pageable: {
                        input: true,
                        numeric: false
                    },
                    columns: [
                       { command: { text: "Select", click: ListInstrumentSelect }, title: " ", width: 100 },
                       { field: "InstrumentPK", title: "", hidden: true, width: 100 },
                       { field: "ID", title: "ID", width: 300 },
                       { field: "Balance", title: "Balance", width: 200, format: "{0:n0}", attributes: { style: "text-align:right;" } },
                       { field: "AvgPrice", title: "Avg Price", width: 200, format: "{0:n8}", attributes: { style: "text-align:right;" } },
                       { field: "AcqDate", title: "Acq Date", width: 200, template: "#= kendo.toString(kendo.parseDate(AcqDate), 'MM/dd/yyyy')#" },
                       { field: "MaturityDate", title: "Maturity Date", width: 200, template: "#= kendo.toString(kendo.parseDate(MaturityDate), 'MM/dd/yyyy')#" },
                       { field: "InterestPercent", title: "Interest Percent", width: 200 }
                    ]
                });


            }

    }
    function onWinListInstrumentClose() {
        $("#gridListInstrument").empty();
    }
    function ListInstrumentSelect(e) {
        var grid = $("#gridListInstrument").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlInstrumentName).val(dataItemX.Name);
        $(htmlInstrumentID).val(dataItemX.ID);
        $(htmlInstrumentPK).val(dataItemX.InstrumentPK);

        var InvestmentInstruction = {
            InstrumentPK: $('#InstrumentPK').val(),
            TrxType: $("#TrxType").data("kendoComboBox").value(),
            FundPK: $("#FundPK").data("kendoComboBox").value(),
            ValueDate: $('#ValueDate').val(),
            InvestmentPK: $('#InvestmentPK').val()
        };

        $.ajax({
            url: window.location.origin + "/Radsoft/Instrument/GetInformationByInstrumentPKByTrxType/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'POST',
            data: JSON.stringify(InvestmentInstruction),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MaturityDate").data("kendoDatePicker").value(data.MaturityDate);
                $("#AcqDate").data("kendoDatePicker").value(data.AcqDate);
                $("#InterestPercent").data("kendoNumericTextBox").value(data.InterestPercent);
                $("#OrderPrice").data("kendoNumericTextBox").value(data.AvgPrice);
                $("#AcqPrice").data("kendoNumericTextBox").value(data.AvgPrice);
                $("#Volume").data("kendoNumericTextBox").value(data.Balance);
                if (GlobInstrumentType == 1) {
                    $("#Lot").data("kendoNumericTextBox").value(data.Balance / 100);
                    $("#Amount").data("kendoNumericTextBox").value(data.Balance * data.AvgPrice);
                }
                else if (GlobInstrumentType == 2) {
                    $("#Amount").data("kendoNumericTextBox").value(data.Balance * (data.AvgPrice/100));
                }
                else
                {
                    $("#OrderPrice").data("kendoNumericTextBox").value(1);
                    $("#Amount").data("kendoNumericTextBox").value(data.Balance);
                }
                WinListInstrument.close();
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

       

        //if (GlobInstrumentType == 2 || GlobInstrumentType == 3) {
        //    $.ajax({
        //        url: window.location.origin + "/Radsoft/Instrument/GetInstrumentInformationByInstrumentPK/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + dataItemX.InstrumentPK,
        //        type: 'GET',
        //        contentType: "application/json;charset=utf-8",
        //        success: function (data) {
        //            $("#MaturityDate").data("kendoDatePicker").value(data.MaturityDate);
        //            $("#InterestPercent").data("kendoNumericTextBox").value(data.InterestPercent);
        //            WinListInstrument.close();
        //        },
        //        error: function (data) {
        //            alertify.alert(data.responseText);
        //        }
        //    });


        //} else {
        //    WinListInstrument.close();
        //}
        //if (GlobInstrumentType != 1){
        //    $("#SettledDate").data("kendoDatePicker").value(null);
        //}
        $("#Tenor").data("kendoNumericTextBox").value(0);


    }

    $("#btnListCounterpartPK").click(function () {
        initListCounterpartPK();
        WinListCounterpart.center();
        WinListCounterpart.open();
        htmlCounterpartPK = "#CounterpartPK";
        htmlCounterpartID = "#CounterpartID";
    });
    function getDataSourceListCounterpart() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/Counterpart/GetCounterpartCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 25,
                     schema: {
                         model: {
                             fields: {
                                 CounterpartPK: { type: "Number" },
                                 ID: { type: "string" },
                                 Name: { type: "string" }
                             }
                         }
                     }
                 });



    }
    function initListCounterpartPK() {
        var dsListCounterpart = getDataSourceListCounterpart();
        $("#gridListCounterpart").kendoGrid({
            dataSource: dsListCounterpart,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
               { command: { text: "Select", click: ListCounterpartSelect }, title: " ", width: 100 },
               { field: "CounterpartPK", title: "", hidden: true, width: 100 },
               { field: "ID", title: "ID", width: 400 },
               //{ field: "Name", title: "Name", width: 100 }
            ]
        });
    }
    function onWinListCounterpartClose() {
        $("#gridListCounterpart").empty();
    }
    function ListCounterpartSelect(e) {
        var grid = $("#gridListCounterpart").data("kendoGrid");
        var dataItemX = grid.dataItem($(e.currentTarget).closest("tr"));
        dirty = true;
        $(htmlCounterpartName).val(dataItemX.Name);
        $(htmlCounterpartID).val(dataItemX.ID);
        $(htmlCounterpartPK).val(dataItemX.CounterpartPK);

        WinListCounterpart.close();


    }

    function InvestmentRecalculate() {

        if (GlobInstrumentType == 2) {
            price = $("#OrderPrice").data("kendoNumericTextBox").value() / 100
            $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * price);
            if ($("#SettledDate").data("kendoDatePicker").value() != null
                  && $("#SettledDate").data("kendoDatePicker").value() != ''
                  && $("#LastCouponDate").data("kendoDatePicker").value() != ''
                  && $("#LastCouponDate").data("kendoDatePicker").value() != null
                  && $('#InstrumentPK').val() != ''
                  && $('#InstrumentPK').val() != null
                  && $("#OrderPrice").data("kendoNumericTextBox").value() != ''
                  && $("#OrderPrice").data("kendoNumericTextBox").value() != null
                  && $("#Volume").data("kendoNumericTextBox").value() != ''
                  && $("#Volume").data("kendoNumericTextBox").value() != null
                  ) {
                GetBondInterest();
            }
        } else {
            $("#Amount").data("kendoNumericTextBox").value($("#Volume").data("kendoNumericTextBox").value() * $("#OrderPrice").data("kendoNumericTextBox").value());
        }

       
    }

    function GetBondInterest()
    {
        if ($("#SettledDate").data("kendoDatePicker").value() > $("#MaturityDate").data("kendoDatePicker").value()) {
            alertify.alert("Settled Date must be < =  than Maturity Date");
            return;
        }
        else if ($("#SettledDate").data("kendoDatePicker").value() < $("#ValueDate").data("kendoDatePicker").value()) {
            alertify.alert("Settled Date must be > =  than Value Date");
            return;
        }
        var InvestmentInstruction = {
            InstrumentPK: $('#InstrumentPK').val(),
            SettledDate: $('#SettledDate').val(),
            Volume: $("#Volume").data("kendoNumericTextBox").value(),
            OrderPrice: $("#OrderPrice").data("kendoNumericTextBox").value(),
            LastCouponDate: $('#LastCouponDate').val()
        };
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/Investment_GetBondInterest/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") ,
            type: 'POST',
            data: JSON.stringify(InvestmentInstruction),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AccruedInterest").data("kendoNumericTextBox").value(data.InterestAmount);
                $("#Tenor").data("kendoNumericTextBox").value(data.Tenor);
                InvestmentInstruction = null;
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    }


    $("#BtnListing").click(function () {
        showInvestmentListing();
    });


    // Untuk Form Listing

    function showInvestmentListing(e) {
        $("#ParamListDate").data("kendoDatePicker").value(new Date);



        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFundIDFrom").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeParamFundIDFrom,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

                //$("#ParamFundIDTo").kendoComboBox({
                //    dataValueField: "FundPK",
                //    dataTextField: "ID",
                //    dataSource: data,
                //    change: OnChangeParamFundIDTo,
                //    filter: "contains",
                //    suggest: true,
                //    index: data.length - 1
                //});
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeParamFundIDFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }

            $("#ParamReferenceFrom").data("kendoComboBox").text("");
            $("#ParamReferenceTo").data("kendoComboBox").text("");
            GetReferenceFromInvestment();
            //GetMessageFromInvestmentNotes();

        }


        //function OnChangeParamFundIDTo() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }
        //}
        if ($("#ParamFundIDFrom").val() == "" || $("#ParamFundIDFrom").val() == 0) {
            var _paramFundIDFrom = "0";
        }
        else {
            var _paramFundIDFrom = $("#ParamFundIDFrom").val()
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/GetReferenceFromInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamListDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _paramFundIDFrom,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamReferenceFrom").kendoComboBox({
                    dataValueField: "RefNo",
                    dataTextField: "Reference",
                    dataSource: data,
                    change: OnChangeReferenceFrom,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

                $("#ParamReferenceTo").kendoComboBox({
                    dataValueField: "RefNo",
                    dataTextField: "Reference",
                    dataSource: data,
                    change: OnChangeReferenceTo,
                    filter: "contains",
                    suggest: true,
                    index: data.length - 1
                });
            },
            error: function (data) {
                alertify.alert("Please Fill Data Report Correctly");
            }
        });


        function OnChangeReferenceFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#ParamReferenceTo").data("kendoComboBox").value($("#ParamReferenceFrom").data("kendoComboBox").value());
            GetMessageFromInvestmentNotesFromReference();
        }

        function OnChangeReferenceTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //$("#ParamInstType").kendoComboBox({
        //    dataValueField: "text",
        //    dataTextField: "text",
        //    dataSource: [
        //       { text: "BOND" },
        //       { text: "EQUITY" },
        //       { text: "DEPOSITO" },

        //    ],
        //    filter: "contains",
        //    suggest: true,
        //    change: OnChangeParamInsType
        //});

        //function OnChangeParamInsType() {
        //    if (this.value() && this.selectedIndex == -1) {
        //        var dt = this.dataSource._data[0];
        //        this.text('');
        //    }



        //}

        //PageBreak
        $("#PageBreak").kendoComboBox({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Yes", value: true },
                { text: "No", value: false }
            ],
            filter: "contains",
            suggest: true,
            change: OnChangePageBreak,
            value: setCmbPageBreak()
        });
        function OnChangePageBreak() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        function setCmbPageBreak() {
            if (e == null) {
                return true;
            } else {
                return dataItemX.PageBreak;
            }
        }

      


        WinInvestmentListing.center();
        WinInvestmentListing.open();

    }

    $("#BtnOkListing").click(function () {
        

        alertify.confirm("Are you sure want to Download Listing data ?", function (e) {
            if (e) {
                var InvestmentListing = {
                    ParamListDate: $('#ParamListDate').val(),
                    ParamFundIDFrom: $("#ParamFundIDFrom").data("kendoComboBox").text(),
                    ParamReferenceFrom: $("#ParamReferenceFrom").data("kendoComboBox").value(),
                    ParamReferenceTo: $("#ParamReferenceTo").data("kendoComboBox").value(),
                    ParamReferenceText: $("#ParamReferenceFrom").data("kendoComboBox").text(),
                    //ParamFundIDTo: $("#ParamFundIDTo").data("kendoComboBox").text(),
                    //ParamInstType: $('#ParamInstType').val(),
                    Message: $('#Message').val(),
                    PageBreak: $("#PageBreak").data("kendoComboBox").value()
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/InvestmentNotes/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Investment",
                    type: 'POST',
                    data: JSON.stringify(InvestmentListing),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $.ajax({
                            url: window.location.origin + "/Radsoft/Investment/InvestmentListingRpt/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(InvestmentListing),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                var newwindow = window.open(data, '_blank');

                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                            }
                        });
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }

                });
            }
        });

    });

    $("#BtnCancelListing").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinInvestmentListing.close();
                alertify.alert("Cancel Listing");
            }
        });
    });


    function onWinInvestmentListingClose() {
        //$("#ParamInstType").val(""),
        $("#ParamListDate").data("kendoDatePicker").value(null),
        $("#ParamFundIDFrom").val(""),
        $("#ParamFundIDTo").val(""),
        $("#ParamReferenceFrom").val(""),
        $("#ParamReferenceTo").val(""),
        $("#Message").val(""),
        $("#PageBreak").val("")
        $("#LblParamListDate").hide();
    }

    function GetNextCouponDate() {
        if ($("#LastCouponDate").data("kendoDatePicker").value() != null) {
            $.ajax({
                url: window.location.origin + "/Radsoft/Instrument/GetNextCouponDate/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#LastCouponDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#InstrumentPK").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                        $("#NextCouponDate").data("kendoDatePicker").value(new Date(data));
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                }
            });

        }
    }

    function GetTenorForTimeDeposit() {
        //if ($("#SettledDate").data("kendoDatePicker").value() > $("#MaturityDate").data("kendoDatePicker").value()) {
        //    alertify.alert("Settled Date must be < =  than Maturity Date");
        //    return;
        //}
        if ($("#SettledDate").data("kendoDatePicker").value() < $("#AcqDate").data("kendoDatePicker").value()) {
            alertify.alert("Settled Date must be > =  than Acq Date");
            return;
        }
        else {
            if ($("#SettledDate").data("kendoDatePicker").value() != null) {
                var Investment = {
                    MaturityDate: $('#MaturityDate').val(),
                    AcqDate: $('#AcqDate').val(),
                    SettledDate: $('#SettledDate').val(),
                    InstrumentPK: $('#InstrumentPK').val(),
                    TrxType: GlobTrxType,
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/Instrument/GetTenorForTimeDeposit/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                    type: 'POST',
                    data: JSON.stringify(Investment),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        $("#Tenor").data("kendoNumericTextBox").value(data);
                    },
                    error: function (data) {
                        alertify.alert(data.responseText);
                    }
                });

            }
        }

    }

    function GetReferenceFromInvestment() {
        if ($("#ParamFundIDFrom").val() == "" || $("#ParamFundIDFrom").val() == 0) {
            var _paramFundIDFrom = "0";
        }
        else {
            var _paramFundIDFrom = $("#ParamFundIDFrom").val()
        }
        $.ajax({
            url: window.location.origin + "/Radsoft/Investment/GetReferenceFromInvestment/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamListDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _paramFundIDFrom,
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamReferenceFrom").kendoComboBox({
                    dataValueField: "RefNo",
                    dataTextField: "Reference",
                    dataSource: data,
                    change: OnChangeReferenceFrom,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

                $("#ParamReferenceTo").kendoComboBox({
                    dataValueField: "RefNo",
                    dataTextField: "Reference",
                    dataSource: data,
                    change: OnChangeReferenceTo,
                    filter: "contains",
                    suggest: true,
                    index: data.length - 1
                });
            },
            error: function (data) {
                alertify.alert("Please Fill Data Report Correctly");
            }
        });


        function OnChangeReferenceFrom() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
            $("#ParamReferenceTo").data("kendoComboBox").value($("#ParamReferenceFrom").data("kendoComboBox").value());
        }

        function OnChangeReferenceTo() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
    }


    $("#BtnListFundExposurePreTradeListing").click(function () {
        showListFundExposurePreTradeListing();
    });


    // Untuk Form Listing Exposure Pre Trade

    function showListFundExposurePreTradeListing(e) {
        $("#ParamListFundExposureDate").data("kendoDatePicker").value(new Date);

        //Fund
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFundIDFromFundExposure").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeParamFundIDFromFundExposure,
                    filter: "contains",
                    suggest: true,
                    index: 0
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeParamFundIDFromFundExposure() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }


        }


 

        WinListFundExposurePreTradeListing.center();
        WinListFundExposurePreTradeListing.open();

    }

   

    $("#BtnCancelListingFundExposure").click(function () {
        
        alertify.confirm("Are you sure want to cancel listing?", function (e) {
            if (e) {
                WinListFundExposurePreTradeListing.close();
                alertify.alert("Cancel Listing");
            }
        });
    });


    function onWinListFundExposurePreTradeListingClose() {
        $("#ParamListFundExposureDate").data("kendoDatePicker").value(null),
        $("#ParamFundIDFromFundExposure").val("")
    }



    //disini



    $("#BtnOkListingFundExposure").click(function () {
        initListFundExposurePreTrade();
        WinListFundExposurePreTrade.center();
        WinListFundExposurePreTrade.open();

    });
    function getDataSourceListFundExposurePreTrade() {



        return new kendo.data.DataSource(

                 {


                     transport:
                             {

                                 read:
                                     {

                                         url: window.location.origin + "/Radsoft/FundExposure/GetFundExposurePreTrade/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamListFundExposureDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#ParamFundIDFromFundExposure").data("kendoComboBox").value(),
                                         dataType: "json"
                                     }
                             },
                     batch: true,
                     cache: false,
                     error: function (e) {
                         alert(e.errorThrown + " - " + e.xhr.responseText);
                         this.cancelChanges();
                     },
                     pageSize: 25,
                     schema: {
                         model: {
                             fields: {
                                 ExposureType: { type: "string" },
                                 ExposureID: { type: "string" },
                                 MarketValue: { type: "number" },
                                 ExposurePercent: { type: "number" },
                                 AlertMinExposure: { type: "boolean" },
                                 AlertMaxExposure: { type: "boolean" },
                                 WarningMinExposure: { type: "boolean" },
                                 WarningMaxExposure: { type: "boolean" }

                             }
                         }
                     }
                 });



    }
    function initListFundExposurePreTrade() {
        var dsListFundExposurePreTrade = getDataSourceListFundExposurePreTrade();
        $("#gridListFundExposurePreTrade").kendoGrid({
            dataSource: dsListFundExposurePreTrade,
            height: "90%",
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contain",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            pageable: {
                input: true,
                numeric: false
            },
            dataBound: gridFundExposurePreTradeDataBound,
            toolbar: ["excel"],
            columns: [
               { field: "ExposureType", title: "Exposure Type", width: 250 },
               { field: "ExposureID", title: "Exposure ID", width: 250 },
               { field: "MarketValue", title: "Market Value", width: 250, format: "{0:n0}", attributes: { style: "text-align:right;" } },
               { field: "ExposurePercent", title: "Exposure Percent", width: 250, format: "{0:n4}", attributes: { style: "text-align:right;" } },
               { field: "AlertMinExposure", title: "Alert Min Exposure", width: 250, template: "#= AlertMinExposure ? 'Yes' : 'No' #" },
               { field: "AlertMaxExposure", title: "Alert Max Exposure", width: 250, template: "#= AlertMaxExposure ? 'Yes' : 'No' #" },
               { field: "WarningMinExposure", title: "Warning Min Exposure", width: 250, template: "#= WarningMinExposure ? 'Yes' : 'No' #" },
               { field: "WarningMaxExposure", title: "Warning Max Exposure", width: 250, template: "#= WarningMaxExposure ? 'Yes' : 'No' #" },
            ]
        });
    }
    function onWinListFundExposurePreTradeClose() {
        $("#gridListFundExposurePreTrade").empty();
    }

    function gridFundExposurePreTradeDataBound() {
        var grid = $("#gridListFundExposurePreTrade").data("kendoGrid");
        var data = grid.dataSource.data();
        $.each(data, function (i, row) {
            if (row.AlertMinExposure == true || row.AlertMaxExposure == true || row.WarningMinExposure == true || row.WarningMaxExposure == true) {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowVoid");
            } else {
                $('tr[data-uid="' + row.uid + '"] ').addClass("historyRowPending");
            }
        });
    }
   

    $("#BtnApproveBySelected").click(function (e) {
        
        alertify.confirm("Are you sure want to Approve by Selected Data ?", function (e) {
            if (e) {
                if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
                    var _type = "None";
                }
                else {
                    var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Investment_ApproveBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Investment_ApproveBySelected",
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
                if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
                    var _type = "None";
                }
                else {
                    var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Investment_RejectBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Investment_RejectBySelected",
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
        
        alertify.confirm("Are you sure want to Void by Selected Data ?", function (e) {
            if (e) {
                if ($("#FilterInstrumentType").data("kendoComboBox").text() == "") {
                    var _type = "None";
                }
                else {
                    var _type = $("#FilterInstrumentType").data("kendoComboBox").text();
                }
                $.ajax({
                    url: window.location.origin + "/Radsoft/Investment/Investment_VoidBySelectedData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#DateFrom").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + kendo.toString($("#DateTo").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + _type + "/Investment_VoidBySelected",
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

    //function GetMessageFromInvestmentNotes() {

    //    if ($("#ParamListDate").data("kendoDatePicker").value() != null) {
    //        $.ajax({
    //            url: window.location.origin + "/Radsoft/Investment/GetMessageFromInvestmentNotes/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + kendo.toString($("#ParamListDate").data("kendoDatePicker").value(), "MM-dd-yy") + "/" + $("#ParamFundIDFrom").data("kendoComboBox").value() + "/Investment",
    //            type: 'GET',
    //            contentType: "application/json;charset=utf-8",
    //            success: function (data) {
    //                $("#Message").val(data);
    //            },
    //            error: function (data) {
    //                alertify.alert(data.responseText);
    //            }
    //        });

    //    }
    //}

    //function GetMessageFromInvestmentNotesFromReference() {
    //    $("#Message").val("");
    //    if ($("#ParamListDate").data("kendoDatePicker").value() != null) {

    //        var InvestmentNotes = {
    //            ParamReferenceFrom: $("#ParamReferenceFrom").data("kendoComboBox").text(),
    //        };

    //        $.ajax({
    //            url: window.location.origin + "/Radsoft/Investment/GetMessageFromInvestmentNotesFromReference/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/Investment",
    //            type: 'POST',
    //            data: JSON.stringify(InvestmentNotes),
    //            contentType: "application/json;charset=utf-8",
    //            success: function (data) {
    //                $("#Message").val(data);
    //            },
    //            error: function (data) {
    //                alertify.alert(data.responseText);
    //            }
    //        });

    //    }
    //}

});
