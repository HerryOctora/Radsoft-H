$(document).ready(function () {
    document.title = 'FORM ACCOUNTING SETUP';
    //Global Variabel
    var win;
    var tabindex;
    var winOldData;
    //1
    initButton();
    //2
    initWindow();
    //3
    initGrid();

    if (_GlobClientCode == "21") {
        $("#LblSwitchingFundAcc").show();

    }
    else {
        $("#LblSwitchingFundAcc").hide();

    }

    function initButton() {
        $("#BtnCancel").kendoButton({
            imageUrl: "../../Images/Icon/IcBtnCancel.png"
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
            imageUrl: "../../Images/Icon/IcBtnApproved.png"
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

        $("#BtnDownload").kendoButton({
            imageUrl: "../../Images/Icon/download1.png"
        });



    }
    
      
    function initWindow() {
        win = $("#WinAccountingSetup").kendoWindow({
            height: "1800px",
            title: "Accounting Setup Detail",
            visible: false,
            width: "1000px",
            modal: true,
            //activate: function () {
            //    $("#ID").focus();
            //},
            open: function (e) {
                this.wrapper.css({ top: 0 })
            },
            close: onPopUpClose
        }).data("kendoWindow");

        //window Form for Old Data
        winOldData = $("#WinOldData").kendoWindow({
            height: "600px",
            title: "Old Data",
            visible: false,
            width: "500px",
            modal: true,
            open: function (e) {
                this.wrapper.css({ top: 150 })
            },

            close: CloseOldData
        }).data("kendoWindow");
    }
    function CloseOldData() {
        $("#OldData").text("");
    }
    var GlobValidator = $("#WinAccountingSetup").kendoValidator().data("kendoValidator");
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

        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").text("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {
                $("#StatusHeader").text("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").text("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
            }
            if (dataItemX.Status == 3) {
                $("#StatusHeader").text("HISTORY");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").hide();
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnOldData").hide();
            }

            $("#AccountingSetupPK").val(dataItemX.AccountingSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#Income").val(dataItemX.Income);
            $("#Expense").val(dataItemX.Expense);
            $("#PPHFinal").val(dataItemX.PPHFinal);
            $("#InvInBond").val(dataItemX.InvInBond);
            $("#InvInBondGovIDR").val(dataItemX.InvInBondGovIDR);
            $("#InvInBondGovUSD").val(dataItemX.InvInBondGovUSD);
            $("#InvInBondCorpIDR").val(dataItemX.InvInBondCorpIDR);
            $("#InvInBondCorpUSD").val(dataItemX.InvInBondCorpUSD);
            $("#InvInEquity").val(dataItemX.InvInEquity);
            $("#InvInTD").val(dataItemX.InvInTD);
            $("#InvInTDUSD").val(dataItemX.InvInTDUSD);
            $("#InvInReksadana").val(dataItemX.InvInReksadana);
            $("#APPurchaseBond").val(dataItemX.APPurchaseBond);
            $("#APPurchaseEquity").val(dataItemX.APPurchaseEquity);
            $("#APPurchaseTD").val(dataItemX.APPurchaseTD);
            $("#ARSellBond").val(dataItemX.ARSellBond);
            $("#ARSellEquity").val(dataItemX.ARSellEquity);
            $("#ARSellTD").val(dataItemX.ARSellTD);
            $("#InterestPurchaseBond").val(dataItemX.InterestPurchaseBond);
            $("#InterestReceivableBond").val(dataItemX.InterestReceivableBond);
            $("#InterestReceivableBondGovIDR").val(dataItemX.InterestReceivableBondGovIDR);
            $("#InterestReceivableBondCorpIDR").val(dataItemX.InterestReceivableBondCorpIDR);
            $("#InterestReceivableBondGovUSD").val(dataItemX.InterestReceivableBondGovUSD);
            $("#InterestReceivableBondCorpUSD").val(dataItemX.InterestReceivableBondCorpUSD);
            $("#InterestReceivableTD").val(dataItemX.InterestReceivableTD);
            $("#CashBond").val(dataItemX.CashBond);
            $("#CashEquity").val(dataItemX.CashEquity);
            $("#CashTD").val(dataItemX.CashTD);
            $("#ForeignExchangeRevalAccount").val(dataItemX.ForeignExchangeRevalAccount);
            $("#UnrealisedEquity").val(dataItemX.UnrealisedEquity);
            $("#UnrealisedBond").val(dataItemX.UnrealisedBond);
            $("#UnrealisedReksadana").val(dataItemX.UnrealisedReksadana);
            $("#RealisedEquity").val(dataItemX.RealisedEquity);
            $("#RealisedBondGovIDR").val(dataItemX.RealisedBondGovIDR);
            $("#RealisedBondGovUSD").val(dataItemX.RealisedBondGovUSD);
            $("#RealisedBondCorpIDR").val(dataItemX.RealisedBondCorpIDR);
            $("#RealisedBondCorpUSD").val(dataItemX.RealisedBondCorpUSD);
            $("#RealisedBond").val(dataItemX.RealisedBond);
            $("#RealisedReksadana").val(dataItemX.RealisedReksadana);
            $("#RealisedAset").val(dataItemX.RealisedAset);
            $("#CadanganEquity").val(dataItemX.CadanganEquity);
            $("#CadanganBond").val(dataItemX.CadanganBond);
            $("#CadanganReksadana").val(dataItemX.CadanganReksadana);
            $("#BrokerageFee").val(dataItemX.BrokerageFee);
            $("#JSXLevyFee").val(dataItemX.JSXLevyFee);
            $("#KPEIFee").val(dataItemX.KPEIFee);
            $("#VATFee").val(dataItemX.VATFee);
            $("#SalesTax").val(dataItemX.SalesTax);
            $("#IncomeTaxArt23").val(dataItemX.IncomeTaxArt23);
            $("#EquitySellMethod").val(dataItemX.EquitySellMethod);
            $("#AveragePriority").val(dataItemX.AveragePriority);
            $("#FixedAsetBuyPPN").val(dataItemX.FixedAsetBuyPPN);
            $("#FixedAsetSellPPN").val(dataItemX.FixedAsetSellPPN);
            $("#DefaultCurrencyPK").val(dataItemX.DefaultCurrencyPK);
            $("#DecimalPlaces").val(dataItemX.DecimalPlaces);
            $("#TotalEquity").val(dataItemX.TotalEquity);

            $("#ARManagementFee").val(dataItemX.ARManagementFee);
            $("#ManagementFeeExpense").val(dataItemX.ManagementFeeExpense);
            $("#MKBD06Bank").val(dataItemX.MKBD06Bank);
            $("#MKBD06PettyCash").val(dataItemX.MKBD06PettyCash);
            $("#WHTFee").val(dataItemX.WHTFee);

            $("#ARSubscriptionFee").val(dataItemX.ARSubscriptionFee);
            $("#SubscriptionFeeIncome").val(dataItemX.SubscriptionFeeIncome);
            $("#ARRedemptionFee").val(dataItemX.ARRedemptionFee);
            $("#RedemptionFeeIncome").val(dataItemX.RedemptionFeeIncome);
            $("#ARSwitchingFee").val(dataItemX.ARSwitchingFee);
            $("#SwitchingFeeIncome").val(dataItemX.SwitchingFeeIncome);
            $("#SwitchingFundAcc").val(dataItemX.SwitchingFundAcc);

            $("#AgentCommissionExpense").val(dataItemX.AgentCommissionExpense);
            $("#AgentCommissionPayable").val(dataItemX.AgentCommissionPayable);
            $("#WHTPayablePPH21").val(dataItemX.WHTPayablePPH21);
            $("#WHTPayablePPH23").val(dataItemX.WHTPayablePPH23);
            $("#VatIn").val(dataItemX.VatIn);
            $("#VatOut").val(dataItemX.VatOut);
            $("#AgentCommissionCash").val(dataItemX.AgentCommissionCash);

            $("#AgentCSRExpense").val(dataItemX.AgentCSRExpense);
            $("#AgentCSRPayable").val(dataItemX.AgentCSRPayable);

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


        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboGroupsOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#MKBD06Bank").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbMKBD06Bank()
                });
                $("#MKBD06PettyCash").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbMKBD06PettyCash()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        //1.combo box AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/Account/GetAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#Income").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbIncome()
                });

                $("#Expense").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbExpense()
                });

                $("#PPHFinal").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbPPHFinal()
                });

                $("#InvInBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInBond()
                });

                $("#InvInBondGovIDR").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInBondGovIDR()
                });

                $("#InvInBondGovUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInBondGovUSD()
                });

                $("#InvInBondCorpIDR").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInBondCorpIDR()
                });

                $("#InvInBondCorpUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInBondCorpUSD()
                });

                $("#InvInEquity").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInEquity()
                });

                $("#InvInTD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInTD()
                });

                $("#InvInTDUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInTDUSD()
                });

                $("#InvInReksadana").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInvInReksadana()
                });

                $("#APPurchaseBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAPPurchaseBond()
                });

                $("#APPurchaseEquity").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAPPurchaseEquity()
                });

                $("#APPurchaseTD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAPPurchaseTD()
                });

                $("#APPurchaseReksadana").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAPPurchaseReksadana()
                });

                $("#ARSellBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARSellBond()
                });

                $("#ARSellEquity").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARSellEquity()
                });

                $("#ARSellTD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARSellTD()
                });

                $("#ARSellReksadana").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARSellReksadana()
                });

                $("#InterestPurchaseBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInterestPurchaseBond()
                });

                $("#InterestReceivableBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInterestReceivableBond()
                });

                $("#InterestReceivableBondGovIDR").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInterestReceivableBondGovIDR()
                });
                $("#InterestReceivableBondCorpIDR").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInterestReceivableBondCorpIDR()
                });
                $("#InterestReceivableBondGovUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInterestReceivableBondGovUSD()
                });
                $("#InterestReceivableBondCorpUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInterestReceivableBondCorpUSD()
                });


                $("#InterestReceivableTD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbInterestReceivableTD()
                });
                $("#ForeignExchangeRevalAccount").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbForeignExchangeRevalAccount()
                });
                $("#UnrealisedEquity").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbUnrealisedEquity()
                });
                $("#UnrealisedBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbUnrealisedBond()
                });
                $("#UnrealisedBondUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbUnrealisedBondUSD()
                });
                $("#UnrealisedReksadana").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbUnrealisedReksadana()
                });
                $("#RealisedEquity").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedEquity()
                });

                $("#RealisedBondGovIDR").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedBondGovIDR()
                });

                $("#RealisedBondGovUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedBondGovUSD()
                });

                $("#RealisedBondCorpIDR").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedBondCorpIDR()
                });

                $("#RealisedBondCorpUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedBondCorpUSD()
                });
                //
                $("#RealisedBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedBond()
                });
                $("#RealisedReksadana").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedReksadana()
                });
                $("#RealisedAset").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRealisedAset()
                });
                $("#CadanganEquity").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbCadanganEquity()
                });
                $("#CadanganBond").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbCadanganBond()
                });
                $("#CadanganBondUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbCadanganBondUSD()
                });
                $("#CadanganReksadana").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbCadanganReksadana()
                });
                $("#BrokerageFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbBrokerageFee()
                });
                $("#JSXLevyFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbJSXLevyFee()
                });
                $("#KPEIFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbKPEIFee()
                });
                $("#VATFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbVATFee()
                });
                $("#SalesTax").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbSalesTax()
                });
                $("#IncomeTaxArt23").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbIncomeTaxArt23()
                });

                $("#FixedAsetBuyPPN").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbFixedAsetBuyPPN()
                });

                $("#FixedAsetSellPPN").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbFixedAsetSellPPN()
                });

                //--

                $("#ARManagementFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARManagementFee()
                });
                $("#ManagementFeeExpense").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbManagementFeeExpense()
                });

                $("#TaxARManagementFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbTaxARManagementFee()
                });
                $("#TaxManagementFeeExpense").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbTaxManagementFeeExpense()
                });

                $("#WHTFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbWHTFee()
                });

                $("#TotalEquity").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbTotalEquity()
                });

                $("#UnrealizedAccountSGD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbUnrealizedAccountSGD()
                });

                $("#UnrealizedAccountUSD").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbUnrealizedAccountUSD()
                });

                $("#ARSubscriptionFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARSubscriptionFee()
                });
                $("#SubscriptionFeeIncome").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbSubscriptionFeeIncome()
                });
                $("#ARRedemptionFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARRedemptionFee()
                });
                $("#RedemptionFeeIncome").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbRedemptionFeeIncome()
                });
                $("#ARSwitchingFee").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbARSwitchingFee()
                });
                $("#SwitchingFeeIncome").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbSwitchingFeeIncome()
                });

                $("#SwitchingFundAcc").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbSwitchingFundAcc()
                });

                $("#AgentCommissionExpense").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAgentCommissionExpense()
                });

                $("#AgentCommissionPayable").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAgentCommissionPayable()
                });
                $("#WHTPayablePPH21").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbWHTPayablePPH21()
                });
                $("#WHTPayablePPH23").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbWHTPayablePPH23()
                });
                $("#VatIn").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbVatIn()
                });
                $("#VatOut").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbVatOut()
                });
                $("#AgentCommissionCash").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAgentCommissionCash()
                });

                $("#AgentCSRExpense").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAgentCSRExpense()
                });

                $("#AgentCSRPayable").kendoComboBox({
                    dataValueField: "AccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeAccountPK,
                    value: setCmbAgentCSRPayable()
                });
            },


            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }

        //
        function setCmbIncome() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Income == 0) {
                    return "";
                } else {
                    return dataItemX.Income;
                }
            }
        }
        function setCmbExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Expense == 0) {
                    return "";
                } else {
                    return dataItemX.Expense;
                }
            }
        }
        function setCmbPPHFinal() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PPHFinal == 0) {
                    return "";
                } else {
                    return dataItemX.PPHFinal;
                }
            }
        }
        //

        function setCmbInvInBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInBond == 0) {
                    return "";
                } else {
                    return dataItemX.InvInBond;
                }
            }
        }

        //
        function setCmbInvInBondGovIDR() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInBondGovIDR == 0) {
                    return "";
                } else {
                    return dataItemX.InvInBondGovIDR;
                }
            }
        }
        function setCmbInvInBondGovUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInBondGovUSD == 0) {
                    return "";
                } else {
                    return dataItemX.InvInBondGovUSD;
                }
            }
        }
        function setCmbInvInBondCorpIDR() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInBondCorpIDR == 0) {
                    return "";
                } else {
                    return dataItemX.InvInBondCorpIDR;
                }
            }
        }
        function setCmbInvInBondCorpUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInBondCorpUSD == 0) {
                    return "";
                } else {
                    return dataItemX.InvInBondCorpUSD;
                }
            }
        }
        //

        function setCmbInvInEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInEquity == 0) {
                    return "";
                } else {
                    return dataItemX.InvInEquity;
                }
            }
        }

        function setCmbInvInTD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInTD == 0) {
                    return "";
                } else {
                    return dataItemX.InvInTD;
                }
            }
        }

        function setCmbInvInTDUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInTDUSD == 0) {
                    return "";
                } else {
                    return dataItemX.InvInTDUSD;
                }
            }
        }

        //
        function setCmbInvInReksadana() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvInReksadana == 0) {
                    return "";
                } else {
                    return dataItemX.InvInReksadana;
                }
            }
        }
        //

        function setCmbAPPurchaseBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.APPurchaseBond == 0) {
                    return "";
                } else {
                    return dataItemX.APPurchaseBond;
                }
            }
        }

        function setCmbAPPurchaseEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.APPurchaseEquity == 0) {
                    return "";
                } else {
                    return dataItemX.APPurchaseEquity;
                }
            }
        }

        function setCmbAPPurchaseTD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.APPurchaseTD == 0) {
                    return "";
                } else {
                    return dataItemX.APPurchaseTD;
                }
            }
        }

        function setCmbAPPurchaseReksadana() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.APPurchaseReksadana == 0) {
                    return "";
                } else {
                    return dataItemX.APPurchaseReksadana;
                }
            }
        }

        function setCmbARSellBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARSellBond == 0) {
                    return "";
                } else {
                    return dataItemX.ARSellBond;
                }
            }
        }

        function setCmbARSellEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARSellEquity == 0) {
                    return "";
                } else {
                    return dataItemX.ARSellEquity;
                }
            }
        }

        function setCmbARSellTD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARSellTD == 0) {
                    return "";
                } else {
                    return dataItemX.ARSellTD;
                }
            }
        }

        function setCmbARSellReksadana() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARSellReksadana == 0) {
                    return "";
                } else {
                    return dataItemX.ARSellReksadana;
                }
            }
        }

        function setCmbInterestPurchaseBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestPurchaseBond == 0) {
                    return "";
                } else {
                    return dataItemX.InterestPurchaseBond;
                }
            }
        }

        function setCmbInterestReceivableBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableBond == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableBond;
                }
            }
        }
        //
        function setCmbInterestReceivableBondGovIDR() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableBondGovIDR == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableBondGovIDR;
                }
            }
        }
        function setCmbInterestReceivableBondCorpIDR() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableBondCorpIDR == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableBondCorpIDR;
                }
            }
        }
        function setCmbInterestReceivableBondGovUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableBondGovUSD == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableBondGovUSD;
                }
            }
        }
        function setCmbInterestReceivableBondCorpUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableBondCorpUSD == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableBondCorpUSD;
                }
            }
        }
        //

        function setCmbInterestReceivableTD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableTD == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableTD;
                }
            }
        }

        function setCmbForeignExchangeRevalAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ForeignExchangeRevalAccount == 0) {
                    return "";
                } else {
                    return dataItemX.ForeignExchangeRevalAccount;
                }
            }
        }

        function setCmbUnrealisedEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedEquity == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedEquity;
                }
            }
        }
        function setCmbUnrealisedBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedBond == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedBond;
                }
            }
        }
        function setCmbUnrealisedBondUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedBondUSD == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedBondUSD;
                }
            }
        }
        function setCmbUnrealisedReksadana() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedReksadana == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedReksadana;
                }
            }
        }
        function setCmbRealisedEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedEquity == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedEquity;
                }
            }
        }

        //
        function setCmbRealisedBondGovIDR() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedBondGovIDR == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedBondGovIDR;
                }
            }
        }
        function setCmbRealisedBondGovUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedBondGovUSD == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedBondGovUSD;
                }
            }
        }
        function setCmbRealisedBondCorpIDR() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedBondCorpIDR == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedBondCorpIDR;
                }
            }
        }
        function setCmbRealisedBondCorpUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedBondCorpUSD == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedBondCorpUSD;
                }
            }
        }
        //

        function setCmbRealisedBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedBond == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedBond;
                }
            }
        }
        function setCmbRealisedReksadana() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedReksadana == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedReksadana;
                }
            }
        }
        function setCmbRealisedAset() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedAset == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedAset;
                }
            }
        }
        function setCmbCadanganEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CadanganEquity == 0) {
                    return "";
                } else {
                    return dataItemX.CadanganEquity;
                }
            }
        }

        function setCmbCadanganBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CadanganBond == 0) {
                    return "";
                } else {
                    return dataItemX.CadanganBond;
                }
            }
        }
        function setCmbCadanganBondUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CadanganBondUSD == 0) {
                    return "";
                } else {
                    return dataItemX.CadanganBondUSD;
                }
            }
        }
        function setCmbCadanganReksadana() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CadanganReksadana == 0) {
                    return "";
                } else {
                    return dataItemX.CadanganReksadana;
                }
            }
        }
        function setCmbBrokerageFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BrokerageFee == 0) {
                    return "";
                } else {
                    return dataItemX.BrokerageFee;
                }
            }
        }
        function setCmbJSXLevyFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.JSXLevyFee == 0) {
                    return "";
                } else {
                    return dataItemX.JSXLevyFee;
                }
            }
        }

        function setCmbKPEIFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.KPEIFee == 0) {
                    return "";
                } else {
                    return dataItemX.KPEIFee;
                }
            }
        }
        function setCmbVATFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.VATFee == 0) {
                    return "";
                } else {
                    return dataItemX.VATFee;
                }
            }
        }
        function setCmbSalesTax() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SalesTax == 0) {
                    return "";
                } else {
                    return dataItemX.SalesTax;
                }
            }
        }
        function setCmbIncomeTaxArt23() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeTaxArt23 == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeTaxArt23;
                }
            }
        }

        function setCmbFixedAsetBuyPPN() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FixedAsetBuyPPN == 0) {
                    return "";
                } else {
                    return dataItemX.FixedAsetBuyPPN;
                }
            }
        }

        function setCmbFixedAsetSellPPN() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.FixedAsetSellPPN == 0) {
                    return "";
                } else {
                    return dataItemX.FixedAsetSellPPN;
                }
            }
        }

        //Additional

        function setCmbARManagementFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARManagementFee == 0) {
                    return "";
                } else {
                    return dataItemX.ARManagementFee;
                }
            }
        }

        function setCmbManagementFeeExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ManagementFeeExpense == 0) {
                    return "";
                } else {
                    return dataItemX.ManagementFeeExpense;
                }
            }
        }

        function setCmbTaxARManagementFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxARManagementFee == 0) {
                    return "";
                } else {
                    return dataItemX.TaxARManagementFee;
                }
            }
        }

        function setCmbTaxManagementFeeExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxManagementFeeExpense == 0) {
                    return "";
                } else {
                    return dataItemX.TaxManagementFeeExpense;
                }
            }
        }

        function setCmbMKBD06Bank() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MKBD06Bank == 0) {
                    return "";
                } else {
                    return dataItemX.MKBD06Bank;
                }
            }
        }

        function setCmbMKBD06PettyCash() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MKBD06PettyCash == 0) {
                    return "";
                } else {
                    return dataItemX.MKBD06PettyCash;
                }
            }
        }

        function setCmbWHTFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.WHTFee == 0) {
                    return "";
                } else {
                    return dataItemX.WHTFee;
                }
            }
        }

        function setCmbTotalEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TotalEquity == 0) {
                    return "";
                } else {
                    return dataItemX.TotalEquity;
                }
            }
        }

        function setCmbUnrealizedAccountSGD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealizedAccountSGD == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealizedAccountSGD;
                }
            }
        }

        function setCmbUnrealizedAccountUSD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealizedAccountUSD == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealizedAccountUSD;
                }
            }
        }

        function setCmbARSubscriptionFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARSubscriptionFee == 0) {
                    return "";
                } else {
                    return dataItemX.ARSubscriptionFee;
                }
            }
        }
        function setCmbSubscriptionFeeIncome() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SubscriptionFeeIncome == 0) {
                    return "";
                } else {
                    return dataItemX.SubscriptionFeeIncome;
                }
            }
        }
        function setCmbARRedemptionFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARRedemptionFee == 0) {
                    return "";
                } else {
                    return dataItemX.ARRedemptionFee;
                }
            }
        }
        function setCmbRedemptionFeeIncome() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RedemptionFeeIncome == 0) {
                    return "";
                } else {
                    return dataItemX.RedemptionFeeIncome;
                }
            }
        }
        function setCmbARSwitchingFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARSwitchingFee == 0) {
                    return "";
                } else {
                    return dataItemX.ARSwitchingFee;
                }
            }
        }
        function setCmbSwitchingFeeIncome() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SwitchingFeeIncome == 0) {
                    return "";
                } else {
                    return dataItemX.SwitchingFeeIncome;
                }
            }
        }
        function setCmbSwitchingFundAcc() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SwitchingFundAcc == 0) {
                    return "";
                } else {
                    return dataItemX.SwitchingFundAcc;
                }
            }
        }

        function setCmbAgentCommissionExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentCommissionExpense == 0) {
                    return "";
                } else {
                    return dataItemX.AgentCommissionExpense;
                }
            }
        }
        function setCmbAgentCommissionPayable() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentCommissionPayable == 0) {
                    return "";
                } else {
                    return dataItemX.AgentCommissionPayable;
                }
            }
        }
        function setCmbWHTPayablePPH21() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.WHTPayablePPH21 == 0) {
                    return "";
                } else {
                    return dataItemX.WHTPayablePPH21;
                }
            }
        }
        function setCmbWHTPayablePPH23() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.WHTPayablePPH23 == 0) {
                    return "";
                } else {
                    return dataItemX.WHTPayablePPH23;
                }
            }
        }
        function setCmbVatIn() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.VatIn == 0) {
                    return "";
                } else {
                    return dataItemX.VatIn;
                }
            }
        }
        function setCmbVatOut() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.VatOut == 0) {
                    return "";
                } else {
                    return dataItemX.VatOut;
                }
            }
        }
        function setCmbAgentCommissionCash() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentCommissionCash == 0) {
                    return "";
                } else {
                    return dataItemX.AgentCommissionCash;
                }
            }
        }

        function setCmbAgentCSRExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentCSRExpense == 0) {
                    return "";
                } else {
                    return dataItemX.AgentCSRExpense;
                }
            }
        }

        function setCmbAgentCSRPayable() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AgentCSRPayable == 0) {
                    return "";
                } else {
                    return dataItemX.AgentCSRPayable;
                }
            }
        }

        $.ajax({
            url: window.location.origin + "/Radsoft/CashRef/GetCashRefCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#CashBond").kendoComboBox({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeCashRefPK,
                    filter: "contains",
                    suggest: true,
                    value: setCmbCashBond()
                });

                $("#CashEquity").kendoComboBox({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeCashRefPK,
                    filter: "contains",
                    suggest: true,
                    value: setCmbCashEquity()
                });

                $("#CashTD").kendoComboBox({
                    dataValueField: "CashRefPK",
                    dataTextField: "ID",
                    dataSource: data,
                    change: OnChangeCashRefPK,
                    filter: "contains",
                    suggest: true,
                    value: setCmbCashTD()
                });

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });

        function OnChangeCashRefPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbCashBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashBond == 0) {
                    return "";
                } else {
                    return dataItemX.CashBond;
                }
            }
        }

        function setCmbCashEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashEquity == 0) {
                    return "";
                } else {
                    return dataItemX.CashEquity;
                }
            }
        }

        function setCmbCashTD() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashTD == 0) {
                    return "";
                } else {
                    return dataItemX.CashTD;
                }
            }
        }

        //Equity Sell Method
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/EquitySellMethod",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#EquitySellMethod").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: OnChangeEquitySellMethod,
                    value: setCmbEquitySellMethod()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeEquitySellMethod() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbEquitySellMethod() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.EquitySellMethod == 0) {
                    return "";
                } else {
                    return dataItemX.EquitySellMethod;
                }
            }
        }

        //Average Priority
        $.ajax({
            url: window.location.origin + "/Radsoft/MasterValue/GetMasterValueCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AveragePriority",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#AveragePriority").kendoComboBox({
                    dataValueField: "Code",
                    dataTextField: "DescOne",
                    dataSource: data,
                    change: OnChangeAveragePriority,
                    value: setCmbAveragePriority()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeAveragePriority() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAveragePriority() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AveragePriority == 0) {
                    return "";
                } else {
                    return dataItemX.AveragePriority;
                }
            }
        }

        //Default Currency
        $.ajax({
            url: window.location.origin + "/Radsoft/Currency/GetCurrencyCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#DefaultCurrencyPK").kendoComboBox({
                    dataValueField: "CurrencyPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeDefaultCurrencyPK,
                    value: setCmbDefaultCurrencyPK()
                });
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
        function OnChangeDefaultCurrencyPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbDefaultCurrencyPK() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DefaultCurrencyPK == 0) {
                    return "";
                } else {
                    return dataItemX.DefaultCurrencyPK;
                }
            }
        }

        $("#DecimalPlaces").kendoNumericTextBox({
            format: "n0",
            value: setDecimalPlaces()

        });
        function setDecimalPlaces() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.DecimalPlaces;
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
        $("#AccountingSetupPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");

        $("#Income").val("");
        $("#Expense").val("");
        $("#PPHFinal").val("");

        $("#InvInBond").val("");

        $("#InvInBondGovIDR").val("");
        $("#InvInBondGovUSD").val("");
        $("#InvInBondCorpIDR").val("");
        $("#InvInBondCorpUSD").val("");

        $("#InvInEquity").val("");
        $("#InvInTD").val("");

        $("#InvInReksadana").val("");

        $("#APPurchaseBond").val("");
        $("#APPurchaseEquity").val("");
        $("#APPurchaseTD").val("");
        $("#APPurchaseReksadana").val("");
        $("#ARSellBond").val("");
        $("#ARSellEquity").val("");
        $("#ARSellTD").val("");
        $("#ARSellReksadana").val("");
        $("#InterestPurchaseBond").val("");
        $("#InterestReceivableBond").val("");

        $("#InterestReceivableBondGovIDR").val("");
        $("#InterestReceivableBondCorpIDR").val("");
        $("#InterestReceivableBondGovUSD").val("");
        $("#InterestReceivableBondCorpUSD").val("");

        $("#InterestReceivableTD").val("");
        $("#CashBond").val("");
        $("#CashEquity").val("");
        $("#CashTD").val("");
        $("#ForeignExchangeRevalAccount").val("");
        $("#UnrealisedEquity").val("");
        $("#UnrealisedBond").val("");
        $("#UnrealisedBondUSD").val("");
        $("#UnrealisedReksadana").val("");
        $("#RealisedEquity").val("");

        $("#RealisedBondGovIDR").val("");
        $("#RealisedBondGovUSD").val("");
        $("#RealisedBondCorpIDR").val("");
        $("#RealisedBondCorpUSD").val("");

        $("#RealisedBond").val("");
        $("#RealisedReksadana").val("");
        $("#RealisedAset").val("");
        $("#CadanganEquity").val("");
        $("#CadanganBond").val("");
        $("#CadanganBondUSD").val("");
        $("#CadanganReksadana").val("");
        $("#BrokerageFee").val("");
        $("#JSXLevyFee").val("");
        $("#KPEIFee").val("");
        $("#VATFee").val("");
        $("#SalesTax").val("");
        $("#IncomeTaxArt23").val("");
        $("#EquitySellMethod").val("");
        $("#AveragePriority").val("");
        $("#FixedAsetBuyPPN").val("");
        $("#FixedAsetSellPPN").val("");
        $("#DefaultCurrencyPK").val("");
        $("#DecimalPlaces").val("");
        $("#TotalEquity").val("");

        $("#ARManagementFee").val("");
        $("#ManagementFeeExpense").val("");
        $("#TaxARManagementFee").val("");
        $("#TaxManagementFeeExpense").val("");
        $("#MKBD06Bank").val("");
        $("#MKBD06PettyCash").val("");
        $("#WHTFee").val("");

        $("#UnrealizedAccountSGD").val("");
        $("#UnrealizedAccountUSD").val("");

        $("#ARSubscriptionFee").val("");
        $("#SubscriptionFeeIncome").val("");
        $("#ARRedemptionFee").val("");
        $("#RedemptionFeeIncome").val("");
        $("#ARSwitchingFee").val("");
        $("#SwitchingFeeIncome").val("");
        $("#SwitchingFundAcc").val("");

        $("#AgentCommissionExpense").val("");
        $("#AgentCommissionPayable").val("");
        $("#WHTPayablePPH21").val("");
        $("#WHTPayablePPH23").val("");
        $("#VatIn").val("");
        $("#VatOut").val("");
        $("#AgentCommissionCash").val("");

        $("#AgentCSRExpense").val("");
        $("#AgentCSRPayable").val("");

        $("#EntryUsersID").val("");
        $("#UpdateUsersID").val("");
        $("#ApprovedUsersID").val("");
        $("#VoidUsersID").val("");
        $("#EntryTime").val("");
        $("#UpdateTime").val("");
        $("#ApprovedTime").val("");
        $("#VoidTime").val("");
        $("#LastUpdate").val("");
        $("#LastUpdateTime").val("");

    }

    // yang bawah belum

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
                pageSize: 10,
                schema: {
                    model: {
                        fields: {
                            AccountingSetupPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            Notes: { type: "String" },

                            Income: { type: "number" },
                            IncomeID: { type: "string" },
                            IncomeDesc: { type: "string" },
                            Expense: { type: "number" },
                            ExpenseID: { type: "string" },
                            ExpenseDesc: { type: "string" },
                            PPHFinal: { type: "number" },
                            PPHFinalID: { type: "string" },
                            PPHFinalDesc: { type: "string" },

                            InvInBond: { type: "number" },
                            InvInBondID: { type: "string" },
                            InvInBondDesc: { type: "string" },

                            InvInBondGovIDR: { type: "number" },
                            InvInBondGovIDRID: { type: "string" },
                            InvInBondGovIDRDesc: { type: "string" },
                            InvInBondGovUSD: { type: "number" },
                            InvInBondGovUSDID: { type: "string" },
                            InvInBondGovUSDDesc: { type: "string" },
                            InvInBondCorpIDR: { type: "number" },
                            InvInBondCorpIDRID: { type: "string" },
                            InvInBondCorpIDRDesc: { type: "string" },
                            InvInBondCorpUSD: { type: "number" },
                            InvInBondCorpUSDID: { type: "string" },
                            InvInBondCorpUSDDesc: { type: "string" },

                            InvInEquity: { type: "number" },
                            InvInEquityID: { type: "string" },
                            InvInEquityDesc: { type: "string" },
                            InvInTD: { type: "number" },
                            InvInTUSD: { type: "string" },
                            InvInTDID: { type: "string" },
                            InvInTDDesc: { type: "string" },

                            InvInReksadana: { type: "number" },
                            InvInReksadanaID: { type: "string" },
                            InvInReksadanaDesc: { type: "string" },

                            APPurchaseBond: { type: "number" },
                            APPurchaseBondID: { type: "string" },
                            APPurchaseBondDesc: { type: "string" },
                            APPurchaseEquity: { type: "number" },
                            APPurchaseEquityID: { type: "string" },
                            APPurchaseEquityDesc: { type: "string" },
                            APPurchaseTD: { type: "number" },
                            APPurchaseTDID: { type: "string" },
                            APPurchaseTDDesc: { type: "string" },
                            APPurchaseReksadana: { type: "number" },
                            APPurchaseReksadanaID: { type: "string" },
                            APPurchaseReksadanaDesc: { type: "string" },
                            ARSellBond: { type: "number" },
                            ARSellBondID: { type: "string" },
                            ARSellBondDesc: { type: "string" },
                            ARSellEquity: { type: "number" },
                            ARSellEquityID: { type: "string" },
                            ARSellEquityDesc: { type: "string" },
                            ARSellTD: { type: "number" },
                            ARSellTDID: { type: "string" },
                            ARSellTDDesc: { type: "string" },
                            ARSellReksadana: { type: "number" },
                            ARSellReksadanaID: { type: "string" },
                            ARSellReksadanaDesc: { type: "string" },
                            InterestPurchaseBond: { type: "number" },
                            InterestPurchaseBondID: { type: "string" },
                            InterestPurchaseBondDesc: { type: "string" },
                            InterestReceivableBond: { type: "number" },
                            InterestReceivableBondID: { type: "string" },
                            InterestReceivableBondDesc: { type: "string" },

                            InterestReceivableBondGovIDR: { type: "number" },
                            InterestReceivableBondGovIDRID: { type: "string" },
                            InterestReceivableBondGovIDRDesc: { type: "string" },
                            InterestReceivableBondCorpIDR: { type: "number" },
                            InterestReceivableBondCorpIDRID: { type: "string" },
                            InterestReceivableBondCorpIDRDesc: { type: "string" },
                            InterestReceivableBondGovUSD: { type: "number" },
                            InterestReceivableBondGovUSDID: { type: "string" },
                            InterestReceivableBondGovUSDDesc: { type: "string" },
                            InterestReceivableBondCorpUSD: { type: "number" },
                            InterestReceivableBondCorpUSDID: { type: "string" },
                            InterestReceivableBondCorpUSDDesc: { type: "string" },

                            InterestReceivableTD: { type: "number" },
                            InterestReceivableTDID: { type: "string" },
                            InterestReceivableTDDesc: { type: "string" },
                            CashBond: { type: "number" },
                            CashBondID: { type: "string" },
                            CashBondDesc: { type: "string" },
                            CashEquity: { type: "number" },
                            CashEquityID: { type: "string" },
                            CashEquityDesc: { type: "string" },
                            CashTD: { type: "number" },
                            CashTDID: { type: "string" },
                            CashTDDesc: { type: "string" },
                            ForeignExchangeRevalAccount: { type: "number" },
                            ForeignExchangeRevalAccountID: { type: "string" },
                            ForeignExchangeRevalAccountDesc: { type: "string" },
                            UnrealisedEquity: { type: "number" },
                            UnrealisedEquityID: { type: "string" },
                            UnrealisedEquityDesc: { type: "string" },
                            UnrealisedBond: { type: "number" },
                            UnrealisedBondID: { type: "string" },
                            UnrealisedBondDesc: { type: "string" },
                            UnrealisedBondUSD: { type: "number" },
                            UnrealisedBondIDUSD: { type: "string" },
                            UnrealisedBondDescUSD: { type: "string" },
                            UnrealisedReksadana: { type: "number" },
                            UnrealisedReksadanaID: { type: "string" },
                            UnrealisedReksadanaDesc: { type: "string" },
                            RealisedEquity: { type: "number" },
                            RealisedEquityID: { type: "string" },
                            RealisedEquityDesc: { type: "string" },

                            RealisedBondGovIDR: { type: "number" },
                            RealisedBondGovIDRID: { type: "string" },
                            RealisedBondGovIDRDesc: { type: "string" },
                            RealisedBondGovUSD: { type: "number" },
                            RealisedBondGovUSDID: { type: "string" },
                            RealisedBondGovUSDDesc: { type: "string" },
                            RealisedBondCorpIDR: { type: "number" },
                            RealisedBondCorpIDRID: { type: "string" },
                            RealisedBondCorpIDRDesc: { type: "string" },
                            RealisedBondCorpUSD: { type: "number" },
                            RealisedBondCorpUSDID: { type: "string" },
                            RealisedBondCorpUSDDesc: { type: "string" },

                            RealisedBond: { type: "number" },
                            RealisedBondID: { type: "string" },
                            RealisedBondDesc: { type: "string" },
                            RealisedReksadana: { type: "number" },
                            RealisedReksadanaID: { type: "string" },
                            RealisedReksadanaDesc: { type: "string" },
                            RealisedAset: { type: "number" },
                            RealisedAsetID: { type: "string" },
                            RealisedAsetDesc: { type: "string" },
                            CadanganEquity: { type: "number" },
                            CadanganEquityID: { type: "string" },
                            CadanganEquityDesc: { type: "string" },
                            CadanganBond: { type: "number" },
                            CadanganBondID: { type: "string" },
                            CadanganBondDesc: { type: "string" },
                            CadanganBondUSD: { type: "number" },
                            CadanganBondIDUSD: { type: "string" },
                            CadanganBondDescUSD: { type: "string" },
                            CadanganReksadana: { type: "number" },
                            CadanganReksadanaID: { type: "string" },
                            CadanganReksadanaDesc: { type: "string" },
                            BrokerageFee: { type: "number" },
                            BrokerageFeeID: { type: "string" },
                            BrokerageFeeDesc: { type: "string" },
                            JSXLevyFee: { type: "number" },
                            JSXLevyFeeID: { type: "string" },
                            JSXLevyFeeDesc: { type: "string" },
                            KPEIFee: { type: "number" },
                            KPEIFeeID: { type: "string" },
                            KPEIFeeDesc: { type: "string" },
                            VATFee: { type: "number" },
                            VATFeeID: { type: "string" },
                            VATFeeDesc: { type: "string" },
                            SalesTax: { type: "number" },
                            SalesTaxID: { type: "string" },
                            SalesTaxDesc: { type: "string" },
                            IncomeTaxArt23: { type: "number" },
                            IncomeTaxArt23ID: { type: "string" },
                            IncomeTaxArt23Desc: { type: "string" },
                            EquitySellMethod: { type: "number" },
                            EquitySellMethodDesc: { type: "string" },
                            AveragePriority: { type: "number" },
                            AveragePriorityDesc: { type: "string" },
                            FixedAsetBuyPPN: { type: "number" },
                            FixedAsetBuyPPNID: { type: "string" },
                            FixedAsetBuyPPNDesc: { type: "string" },
                            FixedAsetSellPPN: { type: "number" },
                            FixedAsetSellPPNID: { type: "string" },
                            FixedAsetSellPPNDesc: { type: "string" },

                            ARManagementFee: { type: "number" },
                            ARManagementFeeID: { type: "string" },
                            ARManagementFeeDesc: { type: "string" },
                            ManagementFeeExpense: { type: "number" },
                            ManagementFeeExpenseID: { type: "string" },
                            ManagementFeeExpenseDesc: { type: "string" },
                            TaxARManagementFee: { type: "number" },
                            TaxARManagementFeeID: { type: "string" },
                            TaxARManagementFeeDesc: { type: "string" },
                            TaxManagementFeeExpense: { type: "number" },
                            TaxManagementFeeExpenseID: { type: "string" },
                            TaxManagementFeeExpenseDesc: { type: "string" },
                            MKBD06Bank: { type: "number" },
                            MKBD06BankID: { type: "string" },
                            MKBD06BankDesc: { type: "string" },
                            MKBD06PettyCash: { type: "number" },
                            MKBD06PettyCashID: { type: "string" },
                            MKBD06PettyCashDesc: { type: "string" },
                            WHTFee: { type: "number" },
                            WHTFeeID: { type: "string" },
                            WHTFeeDesc: { type: "string" },
                            TotalEquity: { type: "number" },
                            TotalEquityID: { type: "string" },
                            TotalEquityDesc: { type: "string" },

                            UnrealizedAccountSGD: { type: "number" },
                            UnrealizedAccountSGDID: { type: "string" },
                            UnrealizedAccountSGDDesc: { type: "string" },

                            UnrealizedAccountUSD: { type: "number" },
                            UnrealizedAccountUSDID: { type: "string" },
                            UnrealizedAccountUSDDesc: { type: "string" },

                            ARSubscriptionFee: { type: "number" },
                            ARSubscriptionFeeID: { type: "string" },
                            ARSubscriptionFeeDesc: { type: "string" },

                            SubscriptionFeeIncome: { type: "number" },
                            SubscriptionFeeIncomeID: { type: "string" },
                            SubscriptionFeeIncomeDesc: { type: "string" },

                            ARRedemptionFee: { type: "number" },
                            ARRedemptionFeeID: { type: "string" },
                            ARRedemptionFeeDesc: { type: "string" },

                            RedemptionFeeIncome: { type: "number" },
                            RedemptionFeeIncomeID: { type: "string" },
                            RedemptionFeeIncomeDesc: { type: "string" },

                            ARSwitchingFee: { type: "number" },
                            ARSwitchingFeeID: { type: "string" },
                            ARSwitchingFeeDesc: { type: "string" },

                            SwitchingFeeIncome: { type: "number" },
                            SwitchingFeeIncomeID: { type: "string" },
                            SwitchingFeeIncomeDesc: { type: "string" },

                            SwitchingFundAcc: { type: "number" },
                            SwitchingFundAccID: { type: "string" },
                            SwitchingFundAccDesc: { type: "string" },


                            AgentCommissionExpense: { type: "number" },
                            AgentCommissionExpenseID: { type: "string" },
                            AgentCommissionExpenseDesc: { type: "string" },

                            AgentCommissionPayable: { type: "number" },
                            AgentCommissionPayableID: { type: "string" },
                            AgentCommissionPayableIDDesc: { type: "string" },

                            WHTPayablePPH21: { type: "number" },
                            WHTPayablePPH21ID: { type: "string" },
                            WHTPayablePPH21Desc: { type: "string" },

                            WHTPayablePPH23: { type: "number" },
                            WHTPayablePPH23ID: { type: "string" },
                            WHTPayablePPH23Desc: { type: "string" },

                            VatIn: { type: "number" },
                            VatInID: { type: "string" },
                            VatInDesc: { type: "string" },

                            VatOut: { type: "number" },
                            VatOutID: { type: "string" },
                            VatOutDesc: { type: "string" },

                            AgentCommissionCash: { type: "number" },
                            AgentCommissionCashID: { type: "string" },
                            AgentCommissionCashDesc: { type: "string" },

                            AgentCSRExpense: { type: "number" },
                            AgentCSRExpenseID: { type: "string" },
                            AgentCSRExpenseDesc: { type: "string" },

                            AgentCSRPayable: { type: "number" },
                            AgentCSRPayableID: { type: "string" },
                            AgentCSRPayableDesc: { type: "string" },


                            DefaultCurrencyPK: { type: "number" },
                            DefaultCurrencyID: { type: "string" },
                            DecimalPlaces: { type: "number" },
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
        if (tabindex == undefined) {
            tabindex = 0;
        }
        if (tabindex == 0) {
            var gridApproved = $("#gridAccountingSetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridAccountingSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridAccountingSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }
    
    function initGrid() {
        var AccountingSetupApprovedURL = window.location.origin + "/Radsoft/AccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(AccountingSetupApprovedURL);

        $("#gridAccountingSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: 470,
            scrollable: {
                virtual: true
            },
            reorderable: true,
            sortable: true,
            resizable: true,
            groupable: true,
            filterable: true,
            pageable: true,
            columnMenu: true,
            pageable: {
                input: true,
                numeric: false
            },

            columns: [
                { command: { text: "Details", click: showDetails }, title: " ", width: 85 },
                { field: "AccountingSetupPK", title: "SysNo.", filterable: false, width: 85 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },

                { field: "Income", title: "Income", width: 150, hidden: true },
                { field: "IncomeID", title: "Income (ID)", width: 200, hidden: true },
                { field: "IncomeDesc", title: "Income (Name)", width: 250, hidden: true },
                { field: "Expense", title: "Expense", width: 150, hidden: true },
                { field: "ExpenseID", title: "Expense (ID)", width: 200, hidden: true },
                { field: "ExpenseDesc", title: "Expense (Name)", width: 250, hidden: true },
                { field: "PPHFinal", title: "PPHFinal", width: 150, hidden: true },
                { field: "PPHFinalID", title: "PPHFinal (ID)", width: 200 },
                { field: "PPHFinalDesc", title: "PPHFinal (Name)", width: 250 },

                { field: "InvInBond", title: "InvInBond", width: 150, hidden: true },
                { field: "InvInBondID", title: "Investment In Bond (ID)", width: 200 },
                { field: "InvInBondDesc", title: "Investment In Bond (Name)", width: 250 },

                { field: "InvInBondGovIDR", title: "InvInBondGovIDR", width: 150, hidden: true },
                { field: "InvInBondGovIDRID", title: "Investment In Bond Goverment IDR (ID)", width: 200, hidden: true },
                { field: "InvInBondGovIDRDesc", title: "Investment In Bond Goverment IDR (Name)", width: 250, hidden: true },
                { field: "InvInBondGovUSD", title: "InvInBondGovUSD", width: 150, hidden: true },
                { field: "InvInBondGovUSDID", title: "Investment In Bond Goverment USD (ID)", width: 200, hidden: true },
                { field: "InvInBondGovUSDDesc", title: "Investment In Bond Goverment USD (Name)", width: 250, hidden: true },
                { field: "InvInBondCorpIDR", title: "InvInBondCorpIDR", width: 150, hidden: true },
                { field: "InvInBondCorpIDRID", title: "Investment In Bond Corporate IDR (ID)", width: 200, hidden: true },
                { field: "InvInBondCorpIDRDesc", title: "Investment In Bond Corporate IDR (Name)", width: 250, hidden: true },
                { field: "InvInBondCorpUSD", title: "InvInBondCorpUSD", width: 150, hidden: true },
                { field: "InvInBondCorpUSDID", title: "Investment In Bond Corporate USD (ID)", width: 200, hidden: true },
                { field: "InvInBondCorpUSDDesc", title: "Investment In Bond Corporate USD (Name)", width: 250, hidden: true },

                { field: "InvInEquity", title: "InvInEquity", width: 150, hidden: true },
                { field: "InvInEquityID", title: "Investment In Equity (ID)", width: 200 },
                { field: "InvInEquityDesc", title: "Investment In Equity (Name)", width: 250 },
                { field: "InvInTD", title: "InvInTD", width: 150, hidden: true },
                { field: "InvInTDID", title: "Investment In TD (ID)", width: 200 },
                { field: "InvInTDDesc", title: "Investment In TD (Name)", width: 250 },
                //TAMBAHAN
                { field: "InvInTDUSD", title: "Investment In TD USD", width: 200 },
                { field: "InvInTDUSDID", title: "Investment In TD USD (ID)", width: 200 },
                { field: "InvInTDUSDDesc", title: "Investment In TD (Name)", width: 200 },

                { field: "InvInReksadana", title: "InvInReksadana", width: 150, hidden: true },
                { field: "InvInReksadanaID", title: "Investment In Reksadana (ID)", width: 200 },
                { field: "InvInReksadanaDesc", title: "Investment In Reksadana (Name)", width: 250 },

                { field: "APPurchaseBond", title: "APPurchaseBond", width: 150, hidden: true },
                { field: "APPurchaseBondID", title: "AP Purchase Bond (ID)", width: 200 },
                { field: "APPurchaseBondDesc", title: "AP Purchase Bond (Name)", width: 250 },
                { field: "APPurchaseEquity", title: "APPurchaseEquity", width: 150, hidden: true },
                { field: "APPurchaseEquityID", title: "AP Purchase Equity (ID)", width: 200 },
                { field: "APPurchaseEquityDesc", title: "AP Purchase Equity (Name)", width: 250 },
                { field: "APPurchaseTD", title: "APPurchaseTD", width: 150, hidden: true },
                { field: "APPurchaseTDID", title: "AP Purchase TD (ID)", width: 200, hidden: true },
                { field: "APPurchaseTDDesc", title: "AP Purchase TD (Name)", width: 250, hidden: true },
                { field: "APPurchaseReksadana", title: "AP Purchase Reksadana", width: 150, hidden: true },
                { field: "APPurchaseReksadanaID", title: "AP Purchase Reksadana (ID)", width: 200, hidden: true },
                { field: "APPurchaseReksadanaDesc", title: "AP Purchase Reksadana (Name)", width: 250, hidden: true },
                { field: "ARSellBond", title: "ARSellBond", width: 150, hidden: true },
                { field: "ARSellBondID", title: "AR Sell Bond (ID)", width: 200 },
                { field: "ARSellBondDesc", title: "AR Sell Bond (Name)", width: 250 },
                { field: "ARSellEquity", title: "ARSellEquity", width: 150, hidden: true },
                { field: "ARSellEquityID", title: "AR Sell Equity (ID)", width: 200 },
                { field: "ARSellEquityDesc", title: "AR Sell Equity (Name)", width: 250 },
                { field: "ARSellTD", title: "ARSellTD", width: 150, hidden: true },
                { field: "ARSellTDID", title: "AR Sell TD (ID)", width: 200, hidden: true },
                { field: "ARSellTDDesc", title: "AR Sell TD (Name)", width: 250, hidden: true },
                { field: "ARSellReksadana", title: "AR Sell Reksadana", width: 150, hidden: true },
                { field: "ARSellReksadanaID", title: "AR Sell Reksadana (ID)", width: 200, hidden: true },
                { field: "ARSellReksadanaDesc", title: "AR Sell Reksadana (Name)", width: 250, hidden: true },
                { field: "InterestPurchaseBond", title: "InterestPurchaseBond", width: 150, hidden: true },
                { field: "InterestPurchaseBondID", title: "Interest Purchase Bond (ID)", width: 200 },
                { field: "InterestPurchaseBondDesc", title: "Interest Purchase Bond (Name)", width: 250 },
                { field: "InterestReceivableBond", title: "InterestReceivableBond", width: 150, hidden: true },
                { field: "InterestReceivableBondID", title: "Interest Receivable Bond (ID)", width: 200 },
                { field: "InterestReceivableBondDesc", title: "Interest Receivable Bond (Name)", width: 250 },

                { field: "InterestReceivableBondGovIDR", title: "InterestReceivableBondGovIDR", width: 150, hidden: true },
                { field: "InterestReceivableBondGovIDRID", title: "Interest Receivable Bond Goverment IDR (ID)", width: 200, hidden: true },
                { field: "InterestReceivableBondGovIDRDesc", title: "Interest Receivable Bond Goverment IDR (Name)", width: 250, hidden: true },
                { field: "InterestReceivableBondCorpIDR", title: "InterestReceivableBondCorpIDR", width: 150, hidden: true },
                { field: "InterestReceivableBondCorpIDRID", title: "Interest Receivable Bond Corporate IDR (ID)", width: 200, hidden: true },
                { field: "InterestReceivableBondCorpIDRDesc", title: "Interest Receivable Bond Corporate IDR (Name)", width: 250, hidden: true },
                { field: "InterestReceivableBondGovUSD", title: "InterestReceivableBondGovUSD", width: 150, hidden: true },
                { field: "InterestReceivableBondGovUSDID", title: "Interest Receivable Bond Goverment USD (ID)", width: 200, hidden: true },
                { field: "InterestReceivableBondGovUSDDesc", title: "Interest Receivable Bond Goverment USD (Name)", width: 250, hidden: true },
                { field: "InterestReceivableBondCorpUSD", title: "InterestReceivableBondCorpUSD", width: 150, hidden: true },
                { field: "InterestReceivableBondCorpUSDID", title: "Interest Receivable Bond Corporate USD (ID)", width: 200, hidden: true },
                { field: "InterestReceivableBondCorpUSDDesc", title: "Interest Receivable Bond Corporate USD (Name)", width: 250, hidden: true },

                { field: "InterestReceivableTD", title: "InterestReceivableTD", width: 150, hidden: true },
                { field: "InterestReceivableTDID", title: "Interest Receivable TD (ID)", width: 200, hidden: true },
                { field: "InterestReceivableTDDesc", title: "Interest Receivable TD (Name)", width: 250, hidden: true },
                { field: "CashBond", title: "Bank for Bond", width: 150, hidden: true },
                { field: "CashBondID", title: "Bank for Bond (ID)", width: 200, hidden: true },
                { field: "CashBondDesc", title: "Bank for Bond (Name)", width: 250, hidden: true },
                { field: "CashEquity", title: "Bank for Equity", width: 150, hidden: true },
                { field: "CashEquityID", title: "Bank for Equity (ID)", width: 200, hidden: true },
                { field: "CashEquityDesc", title: "Bank for Equity (Name)", width: 250, hidden: true },
                { field: "CashTD", title: "Bank for Time Deposit", width: 150, hidden: true },
                { field: "CashTDID", title: "Bank for Time Deposit (ID)", width: 200, hidden: true },
                { field: "CashTDDesc", title: "Bank for Time Deposit (Name)", width: 250, hidden: true },
                { field: "ForeignExchangeRevalAccount", title: "ForeignExchangeRevalAccount", width: 150, hidden: true },
                { field: "ForeignExchangeRevalAccountID", title: "Foreign Exchange Reval Account(ID)", width: 300, hidden: true },
                { field: "ForeignExchangeRevalAccountDesc", title: "Foreign Exchange Reval Account (Name)", width: 300, hidden: true },
                { field: "UnrealisedEquity", title: "UnrealisedEquity", width: 150, hidden: true },
                { field: "UnrealisedEquityID", title: "Unrealised Gain/Loss from sale Equity (ID)", width: 300 },
                { field: "UnrealisedEquityDesc", title: "Unrealised Gain/Loss from sale Equity (Name)", width: 300 },
                { field: "UnrealisedBond", title: "UnrealisedBond", width: 150, hidden: true },
                { field: "UnrealisedBondID", title: "Unrealised Gain/Loss from sale Bond (ID)", width: 300 },
                { field: "UnrealisedBondDesc", title: "Unrealised Gain/Loss from sale Bond (Name)", width: 300 },
                { field: "UnrealisedBondIDUSD", title: "Unrealised Gain/Loss from sale Bond (ID) USD", width: 300 },
                { field: "UnrealisedBondDescUSD", title: "Unrealised Gain/Loss from sale Bond (Name) USD", width: 300 },
                { field: "UnrealisedReksadana", title: "UnrealisedReksadana", width: 150, hidden: true },
                { field: "UnrealisedReksadanaID", title: "Unrealised Gain/Loss from sale Reksadana (ID)", width: 300 },
                { field: "UnrealisedReksadanaDesc", title: "Unrealised Gain/Loss from sale Reksadana (Name)", width: 350 },
                { field: "RealisedEquity", title: "RealisedEquity", width: 150, hidden: true },
                { field: "RealisedEquityID", title: "Realised Gain/Loss from sale Equity (ID)", width: 300 },
                { field: "RealisedEquityDesc", title: "Realised Gain/Loss from sale Equity (Name)", width: 300 },

                { field: "RealisedBondGovIDR", title: "RealisedBondGovIDR", width: 150, hidden: true },
                { field: "RealisedBondGovIDRID", title: "Realised Bond Goverment IDR (ID)", width: 200, hidden: true },
                { field: "RealisedBondGovIDRDesc", title: "Realised Bond Goverment IDR (Name)", width: 250, hidden: true },
                { field: "RealisedBondGovUSD", title: "RealisedBondGovUSD", width: 150, hidden: true },
                { field: "RealisedBondGovUSDID", title: "Realised Bond Goverment USD (ID)", width: 200, hidden: true },
                { field: "RealisedBondGovUSDDesc", title: "Realised Bond Goverment USD (Name)", width: 250, hidden: true },
                { field: "RealisedBondCorpIDR", title: "RealisedBondCorpIDR", width: 150, hidden: true },
                { field: "RealisedBondCorpIDRID", title: "Realised Bond Corporate IDR (ID)", width: 200, hidden: true },
                { field: "RealisedBondCorpIDRDesc", title: "Realised Bond Corporate IDR (Name)", width: 250, hidden: true },
                { field: "RealisedBondCorpUSD", title: "RealisedBondCorpUSD", width: 150, hidden: true },
                { field: "RealisedBondCorpUSDID", title: "Realised Bond Corporate USD (ID)", width: 200, hidden: true },
                { field: "RealisedBondCorpUSDDesc", title: "Realised Bond Corporate USD (Name)", width: 250, hidden: true },

                { field: "RealisedBond", title: "RealisedBond", width: 150, hidden: true },
                { field: "RealisedBondID", title: "Realised Gain/Loss from sale Bond (ID)", width: 300 },
                { field: "RealisedBondDesc", title: "Realised Gain/Loss from sale Bond (Name)", width: 300 },
                { field: "RealisedReksadana", title: "RealisedReksadana", width: 150, hidden: true },
                { field: "RealisedReksadanaID", title: "Realised Gain/Loss from sale Reksadana (ID)", width: 300 },
                { field: "RealisedReksadanaDesc", title: "Realised Gain/Loss from sale Reksadana (Name)", width: 350 },
                { field: "RealisedAset", title: "RealisedAset", width: 150, hidden: true },
                { field: "RealisedAsetID", title: "Realised Gain/Loss from sale Aset (ID)", width: 300, hidden: true },
                { field: "RealisedAsetDesc", title: "Realised Gain/Loss from sale Aset (Name)", width: 300, hidden: true },
                { field: "CadanganEquity", title: "CadanganEquity", width: 150, hidden: true },
                { field: "CadanganEquityID", title: "Cadangan Kenaikan (Penurunan) Equity (ID)", width: 300 },
                { field: "CadanganEquityDesc", title: "Cadangan Kenaikan (Penurunan) Equity (Name)", width: 350 },
                { field: "CadanganBond", title: "CadanganBond", width: 150, hidden: true },
                { field: "CadanganBondID", title: "Cadangan Kenaikan (Penurunan) Bond (ID)", width: 300 },
                { field: "CadanganBondDesc", title: "Cadangan Kenaikan (Penurunan) Bond (Name)", width: 300 },
                { field: "CadanganBondIDUSD", title: "Cadangan Kenaikan (Penurunan) Bond (ID) USD", width: 300 },
                { field: "CadanganBondDescUSD", title: "Cadangan Kenaikan (Penurunan) Bond (Name) USD", width: 300 },
                { field: "CadanganReksadana", title: "CadanganReksadana", width: 150, hidden: true },
                { field: "CadanganReksadanaID", title: "Cadangan Kenaikan (Penurunan) Reksadana (ID)", width: 350 },
                { field: "CadanganReksadanaDesc", title: "Cadangan Kenaikan (Penurunan) Reksadana (Name)", width: 350 },
                { field: "BrokerageFee", title: "BrokerageFee", width: 150, hidden: true },
                { field: "BrokerageFeeID", title: "Brokerage Fee (ID)", width: 200 },
                { field: "BrokerageFeeDesc", title: "Brokerage Fee (Name)", width: 250 },
                { field: "JSXLevyFee", title: "JSXLevyFee", width: 150, hidden: true },
                { field: "JSXLevyFeeID", title: "JSX Levy Fee (ID)", width: 200 },
                { field: "JSXLevyFeeDesc", title: "JSX Levy Fee (Name)", width: 250 },
                { field: "KPEIFee", title: "KPEIFee", width: 150, hidden: true },
                { field: "KPEIFeeID", title: "KPE IFee (ID)", width: 200, hidden: true },
                { field: "KPEIFeeDesc", title: "KPEI Fee (Name)", width: 250, hidden: true },
                { field: "VATFee", title: "VATFee", width: 150, hidden: true },
                { field: "VATFeeID", title: "VAT IFee (ID)", width: 200, hidden: true },
                { field: "VATFeeDesc", title: "VAT Fee (Name)", width: 250, hidden: true },
                { field: "SalesTax", title: "SalesTax", width: 150, hidden: true },
                { field: "SalesTaxID", title: "Sales Tax (ID)", width: 200, hidden: true },
                { field: "SalesTaxDesc", title: "Sales Tax (Name)", width: 250, hidden: true },
                { field: "WHTFee", title: "WHTFee", width: 150, hidden: true },
                { field: "WHTFeeID", title: "WHTFee (ID)", width: 200 },
                { field: "WHTFeeDesc", title: "WHTFee (Name)", width: 250 },
                { field: "IncomeTaxArt23", title: "IncomeTaxArt23", width: 150, hidden: true },
                { field: "IncomeTaxArt23ID", title: "Income Tax Art 23 (ID)", width: 200 },
                { field: "IncomeTaxArt23Desc", title: "Income Tax Art 23 (Name)", width: 250 },
                { field: "EquitySellMethod", title: "EquitySellMethod", width: 150, hidden: true },
                { field: "EquitySellMethodDesc", title: "Equity Sell Method (Name)", width: 250, hidden: true },
                { field: "AveragePriority", title: "AveragePriority", width: 150, hidden: true },
                { field: "AveragePriorityDesc", title: "Average Priority (Name)", width: 250, hidden: true },
                { field: "FixedAsetBuyPPN", title: "FixedAsetBuyPPN", width: 150, hidden: true },
                { field: "FixedAsetBuyPPNID", title: "Fixed Aset Buy PPN (ID)", width: 200, hidden: true },
                { field: "FixedAsetBuyPPNDesc", title: "Fixed Aset Buy PPN (Name)", width: 250, hidden: true },
                { field: "FixedAsetSellPPN", title: "FixedAsetSellPPN", width: 150, hidden: true },
                { field: "FixedAsetSellPPNID", title: "Fixed Aset Sell PPN (ID)", width: 200, hidden: true },
                { field: "FixedAsetSellPPNDesc", title: "Fixed Aset Sell PPN (Name)", width: 250, hidden: true },

                { field: "ARManagementFee", title: "ARManagementFee", width: 150, hidden: true },
                { field: "ARManagementFeeID", title: "ARManagementFee (ID)", width: 200 },
                { field: "ARManagementFeeDesc", title: "ARManagementFee (Name)", width: 250 },
                { field: "ManagementFeeExpense", title: "ManagementFeeExpense", width: 150, hidden: true },
                { field: "ManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 200 },
                { field: "ManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 250 },

                { field: "TaxARManagementFee", title: "ARManagementFee", width: 150, hidden: true },
                { field: "TaxARManagementFeeID", title: "ARManagementFee (ID)", width: 200 },
                { field: "TaxARManagementFeeDesc", title: "ARManagementFee (Name)", width: 250 },
                { field: "TaxManagementFeeExpense", title: "ManagementFeeExpense", width: 150, hidden: true },
                { field: "TaxManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 200 },
                { field: "TaxManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 250 },

                { field: "TotalEquity", title: "TotalEquity", width: 150, hidden: true },
                { field: "TotalEquityID", title: "TotalEquity (ID)", width: 200 },
                { field: "TotalEquityDesc", title: "TotalEquity (Name)", width: 250 },

                { field: "MKBD06Bank", title: "MKBD06Bank", width: 150, hidden: true },
                { field: "MKBD06BankID", title: "MKBD06Bank (ID)", width: 200 },
                { field: "MKBD06BankDesc", title: "MKBD06Bank (Name)", width: 250 },

                { field: "MKBD06PettyCash", title: "MKBD06PettyCash", width: 150, hidden: true },
                { field: "MKBD06PettyCashID", title: "MKBD06PettyCash (ID)", width: 200 },
                { field: "MKBD06PettyCashDesc", title: "MKBD06PettyCash (Name)", width: 250 },

                { field: "UnrealizedAccountSGD", title: "UnrealizedAccountSGD", width: 150, hidden: true },
                { field: "UnrealizedAccountSGDID", title: "UnrealizedAccountSGD (ID)", width: 200 },
                { field: "UnrealizedAccountSGDDesc", title: "UnrealizedAccountSGD (Name)", width: 250 },

                { field: "UnrealizedAccountUSD", title: "UnrealizedAccountUSD", width: 150, hidden: true },
                { field: "UnrealizedAccountUSDID", title: "UnrealizedAccountUSD (ID)", width: 200 },
                { field: "UnrealizedAccountUSDDesc", title: "UnrealizedAccountUSD (Name)", width: 250 },


                { field: "ARSubscriptionFee", title: "ARSubscriptionFee", width: 150, hidden: true },
                { field: "ARSubscriptionFeeID", title: "ARSubscriptionFee (ID)", width: 200 },
                { field: "ARSubscriptionFeeDesc", title: "ARSubscriptionFee (Name)", width: 250 },

                { field: "SubscriptionFeeIncome", title: "SubscriptionFeeIncome", width: 150, hidden: true },
                { field: "SubscriptionFeeIncomeID", title: "SubscriptionFeeIncome (ID)", width: 200 },
                { field: "SubscriptionFeeIncomeDesc", title: "SubscriptionFeeIncome (Name)", width: 250 },

                { field: "ARRedemptionFee", title: "ARRedemptionFee", width: 150, hidden: true },
                { field: "ARRedemptionFeeID", title: "ARRedemptionFee (ID)", width: 200 },
                { field: "ARRedemptionFeeDesc", title: "ARRedemptionFee (Name)", width: 250 },

                { field: "RedemptionFeeIncome", title: "RedemptionFeeIncome", width: 150, hidden: true },
                { field: "RedemptionFeeIncomeID", title: "RedemptionFeeIncome (ID)", width: 200 },
                { field: "RedemptionFeeIncomeDesc", title: "RedemptionFeeIncome (Name)", width: 250 },

                { field: "ARSwitchingFee", title: "ARSwitchingFee", width: 150, hidden: true },
                { field: "ARSwitchingFeeID", title: "ARSwitchingFee (ID)", width: 200 },
                { field: "ARSwitchingFeeDesc", title: "ARSwitchingFee (Name)", width: 250 },

                { field: "SwitchingFeeIncome", title: "SwitchingFeeIncome", width: 150, hidden: true },
                { field: "SwitchingFeeIncomeID", title: "SwitchingFeeIncome (ID)", width: 200 },
                { field: "SwitchingFeeIncomeDesc", title: "SwitchingFeeIncome (Name)", width: 250 },

                { field: "SwitchingFundAcc", title: "SwitchingFundAcc", width: 150, hidden: true },
                { field: "SwitchingFundAccID", title: "SwitchingFundAcc (ID)", width: 200 },
                { field: "SwitchingFundAccDesc", title: "SwitchingFundAcc (Name)", width: 250 },

                { field: "AgentCommissionExpense", title: "AgentCommissionExpense", width: 150, hidden: true },
                { field: "AgentCommissionExpenseID", title: "AgentCommissionExpense (ID)", width: 200 },
                { field: "AgentCommissionExpenseDesc", title: "AgentCommissionExpense (Name)", width: 250 },

                { field: "AgentCommissionPayable", title: "AgentCommissionPayable", width: 150, hidden: true },
                { field: "AgentCommissionPayableID", title: "AgentCommissionPayable (ID)", width: 200 },
                { field: "AgentCommissionPayableDesc", title: "AgentCommissionPayable (Name)", width: 250 },

                { field: "WHTPayablePPH21", title: "WHTPayablePPH21", width: 150, hidden: true },
                { field: "WHTPayablePPH21ID", title: "WHTPayablePPH21 (ID)", width: 200 },
                { field: "WHTPayablePPH21Desc", title: "WHTPayablePPH21 (Name)", width: 250 },

                { field: "WHTPayablePPH23", title: "WHTPayablePPH23", width: 150, hidden: true },
                { field: "WHTPayablePPH23ID", title: "WHTPayablePPH23 (ID)", width: 200 },
                { field: "WHTPayablePPH23Desc", title: "WHTPayablePPH23 (Name)", width: 250 },

                { field: "VatIn", title: "VatIn", width: 150, hidden: true },
                { field: "VatInID", title: "VatIn (ID)", width: 200 },
                { field: "VatInDesc", title: "VatIn (Name)", width: 250 },

                { field: "VatOut", title: "VatOut", width: 150, hidden: true },
                { field: "VatOutID", title: "VatOut (ID)", width: 200 },
                { field: "VatOutDesc", title: "VatOut (Name)", width: 250 },

                { field: "AgentCommissionCash", title: "AgentCommissionCash", width: 150, hidden: true },
                { field: "AgentCommissionCashID", title: "AgentCommissionCash (ID)", width: 200 },
                { field: "AgentCommissionCashDesc", title: "AgentCommissionCash (Name)", width: 250 },

                { field: "AgentCSRExpense", title: "AgentCSRExpense", width: 150, hidden: true },
                { field: "AgentCSRExpenseID", title: "AgentCSRExpense (ID)", width: 200 },
                { field: "AgentCSRExpenseDesc", title: "AgentCSRExpense (Name)", width: 250 },

                { field: "AgentCSRPayable", title: "AgentCSRPayable", width: 150, hidden: true },
                { field: "AgentCSRPayableID", title: "AgentCSRPayable (ID)", width: 200 },
                { field: "AgentCSRPayableDesc", title: "AgentCSRPayable (Name)", width: 250 },

                { field: "DefaultCurrencyPK", title: "DefaultCurrencyPK", hidden: true, width: 100 },
                { field: "DefaultCurrencyID", title: "Default Currency ID", width: 170 },
                { field: "DecimalPlaces", title: "<div style='text-align: right'>Decimal Places", width: 150, attributes: { style: "text-align:right;" } },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                { field: "UpdateUsersID", title: "UpdateID", width: 120 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 120 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                { field: "VoidUsersID", title: "VoidID", width: 120 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabAccountingSetup").kendoTabStrip({
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
                        var AccountingSetupPendingURL = window.location.origin + "/Radsoft/AccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(AccountingSetupPendingURL);
                        $("#gridAccountingSetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: 470,
                            scrollable: {
                                virtual: true
                            },
                            reorderable: true,
                            sortable: true,
                            resizable: true,
                            groupable: true,
                            filterable: true,
                            pageable: true,
                            columnMenu: true,
                            pageable: {
                                input: true,
                                numeric: false
                            },
                            columns: [
                                { command: { text: "Details", click: showDetails }, title: " ", width: 85 },
                                { field: "AccountingSetupPK", title: "SysNo.", filterable: false, width: 85 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },

                                { field: "Income", title: "Income", width: 150, hidden: true },
                                { field: "IncomeID", title: "Income (ID)", width: 200, hidden: true },
                                { field: "IncomeDesc", title: "Income (Name)", width: 250, hidden: true },
                                { field: "Expense", title: "Expense", width: 150, hidden: true },
                                { field: "ExpenseID", title: "Expense (ID)", width: 200, hidden: true },
                                { field: "ExpenseDesc", title: "Expense (Name)", width: 250, hidden: true },
                                { field: "PPHFinal", title: "PPHFinal", width: 150, hidden: true },
                                { field: "PPHFinalID", title: "PPHFinal (ID)", width: 200 },
                                { field: "PPHFinalDesc", title: "PPHFinal (Name)", width: 250 },

                                { field: "InvInBond", title: "InvInBond", width: 150, hidden: true },
                                { field: "InvInBondID", title: "Investment In Bond (ID)", width: 200 },
                                { field: "InvInBondDesc", title: "Investment In Bond (Name)", width: 250 },

                                { field: "InvInBondGovIDR", title: "InvInBondGovIDR", width: 150, hidden: true },
                                { field: "InvInBondGovIDRID", title: "Investment In Bond Goverment IDR (ID)", width: 200, hidden: true },
                                { field: "InvInBondGovIDRDesc", title: "Investment In Bond Goverment IDR (Name)", width: 250, hidden: true },
                                { field: "InvInBondGovUSD", title: "InvInBondGovUSD", width: 150, hidden: true },
                                { field: "InvInBondGovUSDID", title: "Investment In Bond Goverment USD (ID)", width: 200, hidden: true },
                                { field: "InvInBondGovUSDDesc", title: "Investment In Bond Goverment USD (Name)", width: 250, hidden: true },
                                { field: "InvInBondCorpIDR", title: "InvInBondCorpIDR", width: 150, hidden: true },
                                { field: "InvInBondCorpIDRID", title: "Investment In Bond Corporate IDR (ID)", width: 200, hidden: true },
                                { field: "InvInBondCorpIDRDesc", title: "Investment In Bond Corporate IDR (Name)", width: 250, hidden: true },
                                { field: "InvInBondCorpUSD", title: "InvInBondCorpUSD", width: 150, hidden: true },
                                { field: "InvInBondCorpUSDID", title: "Investment In Bond Corporate USD (ID)", width: 200, hidden: true },
                                { field: "InvInBondCorpUSDDesc", title: "Investment In Bond Corporate USD (Name)", width: 250, hidden: true },

                                { field: "InvInEquity", title: "InvInEquity", width: 150, hidden: true },
                                { field: "InvInEquityID", title: "Investment In Equity (ID)", width: 200 },
                                { field: "InvInEquityDesc", title: "Investment In Equity (Name)", width: 250 },
                                { field: "InvInTD", title: "InvInTD", width: 150, hidden: true },
                                { field: "InvInTDID", title: "Investment In TD (ID)", width: 200 },
                                { field: "InvInTDDesc", title: "Investment In TD (Name)", width: 250 },

                                { field: "InvInReksadana", title: "InvInReksadana", width: 150, hidden: true },
                                { field: "InvInReksadanaID", title: "Investment In Reksadana (ID)", width: 200 },
                                { field: "InvInReksadanaDesc", title: "Investment In Reksadana (Name)", width: 250 },

                                { field: "APPurchaseBond", title: "APPurchaseBond", width: 150, hidden: true },
                                { field: "APPurchaseBondID", title: "AP Purchase Bond (ID)", width: 200 },
                                { field: "APPurchaseBondDesc", title: "AP Purchase Bond (Name)", width: 250 },
                                { field: "APPurchaseEquity", title: "APPurchaseEquity", width: 150, hidden: true },
                                { field: "APPurchaseEquityID", title: "AP Purchase Equity (ID)", width: 200 },
                                { field: "APPurchaseEquityDesc", title: "AP Purchase Equity (Name)", width: 250 },
                                { field: "APPurchaseTD", title: "APPurchaseTD", width: 150, hidden: true },
                                { field: "APPurchaseTDID", title: "AP Purchase TD (ID)", width: 200, hidden: true },
                                { field: "APPurchaseTDDesc", title: "AP Purchase TD (Name)", width: 250, hidden: true },
                                { field: "APPurchaseReksadana", title: "AP Purchase Reksadana", width: 150, hidden: true },
                                { field: "APPurchaseReksadanaID", title: "AP Purchase Reksadana (ID)", width: 200, hidden: true },
                                { field: "APPurchaseReksadanaDesc", title: "AP Purchase Reksadana (Name)", width: 250, hidden: true },
                                { field: "ARSellBond", title: "ARSellBond", width: 150, hidden: true },
                                { field: "ARSellBondID", title: "AR Sell Bond (ID)", width: 200 },
                                { field: "ARSellBondDesc", title: "AR Sell Bond (Name)", width: 250 },
                                { field: "ARSellEquity", title: "ARSellEquity", width: 150, hidden: true },
                                { field: "ARSellEquityID", title: "AR Sell Equity (ID)", width: 200 },
                                { field: "ARSellEquityDesc", title: "AR Sell Equity (Name)", width: 250 },
                                { field: "ARSellTD", title: "ARSellTD", width: 150, hidden: true },
                                { field: "ARSellTDID", title: "AR Sell TD (ID)", width: 200, hidden: true },
                                { field: "ARSellTDDesc", title: "AR Sell TD (Name)", width: 250, hidden: true },
                                { field: "ARSellReksadana", title: "AR Sell Reksadana", width: 150, hidden: true },
                                { field: "ARSellReksadanaID", title: "AR Sell Reksadana (ID)", width: 200, hidden: true },
                                { field: "ARSellReksadanaDesc", title: "AR Sell Reksadana (Name)", width: 250, hidden: true },
                                { field: "InterestPurchaseBond", title: "InterestPurchaseBond", width: 150, hidden: true },
                                { field: "InterestPurchaseBondID", title: "Interest Purchase Bond (ID)", width: 200 },
                                { field: "InterestPurchaseBondDesc", title: "Interest Purchase Bond (Name)", width: 250 },
                                { field: "InterestReceivableBond", title: "InterestReceivableBond", width: 150, hidden: true },
                                { field: "InterestReceivableBondID", title: "Interest Receivable Bond (ID)", width: 200 },
                                { field: "InterestReceivableBondDesc", title: "Interest Receivable Bond (Name)", width: 250 },

                                { field: "InterestReceivableBondGovIDR", title: "InterestReceivableBondGovIDR", width: 150, hidden: true },
                                { field: "InterestReceivableBondGovIDRID", title: "Interest Receivable Bond Goverment IDR (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondGovIDRDesc", title: "Interest Receivable Bond Goverment IDR (Name)", width: 250, hidden: true },
                                { field: "InterestReceivableBondCorpIDR", title: "InterestReceivableBondCorpIDR", width: 150, hidden: true },
                                { field: "InterestReceivableBondCorpIDRID", title: "Interest Receivable Bond Corporate IDR (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondCorpIDRDesc", title: "Interest Receivable Bond Corporate IDR (Name)", width: 250, hidden: true },
                                { field: "InterestReceivableBondGovUSD", title: "InterestReceivableBondGovUSD", width: 150, hidden: true },
                                { field: "InterestReceivableBondGovUSDID", title: "Interest Receivable Bond Goverment USD (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondGovUSDDesc", title: "Interest Receivable Bond Goverment USD (Name)", width: 250, hidden: true },
                                { field: "InterestReceivableBondCorpUSD", title: "InterestReceivableBondCorpUSD", width: 150, hidden: true },
                                { field: "InterestReceivableBondCorpUSDID", title: "Interest Receivable Bond Corporate USD (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondCorpUSDDesc", title: "Interest Receivable Bond Corporate USD (Name)", width: 250, hidden: true },

                                { field: "InterestReceivableTD", title: "InterestReceivableTD", width: 150, hidden: true },
                                { field: "InterestReceivableTDID", title: "Interest Receivable TD (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableTDDesc", title: "Interest Receivable TD (Name)", width: 250, hidden: true },
                                { field: "CashBond", title: "Bank for Bond", width: 150, hidden: true },
                                { field: "CashBondID", title: "Bank for Bond (ID)", width: 200, hidden: true },
                                { field: "CashBondDesc", title: "Bank for Bond (Name)", width: 250, hidden: true },
                                { field: "CashEquity", title: "Bank for Equity", width: 150, hidden: true },
                                { field: "CashEquityID", title: "Bank for Equity (ID)", width: 200, hidden: true },
                                { field: "CashEquityDesc", title: "Bank for Equity (Name)", width: 250, hidden: true },
                                { field: "CashTD", title: "Bank for Time Deposit", width: 150, hidden: true },
                                { field: "CashTDID", title: "Bank for Time Deposit (ID)", width: 200, hidden: true },
                                { field: "CashTDDesc", title: "Bank for Time Deposit (Name)", width: 250, hidden: true },
                                { field: "ForeignExchangeRevalAccount", title: "ForeignExchangeRevalAccount", width: 150, hidden: true },
                                { field: "ForeignExchangeRevalAccountID", title: "Foreign Exchange Reval Account(ID)", width: 300, hidden: true },
                                { field: "ForeignExchangeRevalAccountDesc", title: "Foreign Exchange Reval Account (Name)", width: 300, hidden: true },
                                { field: "UnrealisedEquity", title: "UnrealisedEquity", width: 150, hidden: true },
                                { field: "UnrealisedEquityID", title: "Unrealised Gain/Loss from sale Equity (ID)", width: 300 },
                                { field: "UnrealisedEquityDesc", title: "Unrealised Gain/Loss from sale Equity (Name)", width: 300 },
                                { field: "UnrealisedBond", title: "UnrealisedBond", width: 150, hidden: true },
                                { field: "UnrealisedBondID", title: "Unrealised Gain/Loss from sale Bond (ID)", width: 300 },
                                { field: "UnrealisedBondDesc", title: "Unrealised Gain/Loss from sale Bond (Name)", width: 300 },
                                { field: "UnrealisedBondIDUSD", title: "Unrealised Gain/Loss from sale Bond (ID) USD", width: 300 },
                                { field: "UnrealisedBondDescUSD", title: "Unrealised Gain/Loss from sale Bond (Name) USD", width: 300 },
                                { field: "UnrealisedReksadana", title: "UnrealisedReksadana", width: 150, hidden: true },
                                { field: "UnrealisedReksadanaID", title: "Unrealised Gain/Loss from sale Reksadana (ID)", width: 300 },
                                { field: "UnrealisedReksadanaDesc", title: "Unrealised Gain/Loss from sale Reksadana (Name)", width: 350 },
                                { field: "RealisedEquity", title: "RealisedEquity", width: 150, hidden: true },
                                { field: "RealisedEquityID", title: "Realised Gain/Loss from sale Equity (ID)", width: 300 },
                                { field: "RealisedEquityDesc", title: "Realised Gain/Loss from sale Equity (Name)", width: 300 },

                                { field: "RealisedBondGovIDR", title: "RealisedBondGovIDR", width: 150, hidden: true },
                                { field: "RealisedBondGovIDRID", title: "Realised Bond Goverment IDR (ID)", width: 200, hidden: true },
                                { field: "RealisedBondGovIDRDesc", title: "Realised Bond Goverment IDR (Name)", width: 250, hidden: true },
                                { field: "RealisedBondGovUSD", title: "RealisedBondGovUSD", width: 150, hidden: true },
                                { field: "RealisedBondGovUSDID", title: "Realised Bond Goverment USD (ID)", width: 200, hidden: true },
                                { field: "RealisedBondGovUSDDesc", title: "Realised Bond Goverment USD (Name)", width: 250, hidden: true },
                                { field: "RealisedBondCorpIDR", title: "RealisedBondCorpIDR", width: 150, hidden: true },
                                { field: "RealisedBondCorpIDRID", title: "Realised Bond Corporate IDR (ID)", width: 200, hidden: true },
                                { field: "RealisedBondCorpIDRDesc", title: "Realised Bond Corporate IDR (Name)", width: 250, hidden: true },
                                { field: "RealisedBondCorpUSD", title: "RealisedBondCorpUSD", width: 150, hidden: true },
                                { field: "RealisedBondCorpUSDID", title: "Realised Bond Corporate USD (ID)", width: 200, hidden: true },
                                { field: "RealisedBondCorpUSDDesc", title: "Realised Bond Corporate USD (Name)", width: 250, hidden: true },

                                { field: "RealisedBond", title: "RealisedBond", width: 150, hidden: true },
                                { field: "RealisedBondID", title: "Realised Gain/Loss from sale Bond (ID)", width: 300 },
                                { field: "RealisedBondDesc", title: "Realised Gain/Loss from sale Bond (Name)", width: 300 },
                                { field: "RealisedReksadana", title: "RealisedReksadana", width: 150, hidden: true },
                                { field: "RealisedReksadanaID", title: "Realised Gain/Loss from sale Reksadana (ID)", width: 300 },
                                { field: "RealisedReksadanaDesc", title: "Realised Gain/Loss from sale Reksadana (Name)", width: 350 },
                                { field: "RealisedAset", title: "RealisedAset", width: 150, hidden: true },
                                { field: "RealisedAsetID", title: "Realised Gain/Loss from sale Aset (ID)", width: 300, hidden: true },
                                { field: "RealisedAsetDesc", title: "Realised Gain/Loss from sale Aset (Name)", width: 300, hidden: true },
                                { field: "CadanganEquity", title: "CadanganEquity", width: 150, hidden: true },
                                { field: "CadanganEquityID", title: "Cadangan Kenaikan (Penurunan) Equity (ID)", width: 300 },
                                { field: "CadanganEquityDesc", title: "Cadangan Kenaikan (Penurunan) Equity (Name)", width: 350 },
                                { field: "CadanganBond", title: "CadanganBond", width: 150, hidden: true },
                                { field: "CadanganBondID", title: "Cadangan Kenaikan (Penurunan) Bond (ID)", width: 300 },
                                { field: "CadanganBondDesc", title: "Cadangan Kenaikan (Penurunan) Bond (Name)", width: 300 },
                                { field: "CadanganBondIDUSD", title: "Cadangan Kenaikan (Penurunan) Bond (ID) USD", width: 300 },
                                { field: "CadanganBondDescUSD", title: "Cadangan Kenaikan (Penurunan) Bond (Name) USD", width: 300 },
                                { field: "CadanganReksadana", title: "CadanganReksadana", width: 150, hidden: true },
                                { field: "CadanganReksadanaID", title: "Cadangan Kenaikan (Penurunan) Reksadana (ID)", width: 350 },
                                { field: "CadanganReksadanaDesc", title: "Cadangan Kenaikan (Penurunan) Reksadana (Name)", width: 350 },
                                { field: "BrokerageFee", title: "BrokerageFee", width: 150, hidden: true },
                                { field: "BrokerageFeeID", title: "Brokerage Fee (ID)", width: 200 },
                                { field: "BrokerageFeeDesc", title: "Brokerage Fee (Name)", width: 250 },
                                { field: "JSXLevyFee", title: "JSXLevyFee", width: 150, hidden: true },
                                { field: "JSXLevyFeeID", title: "JSX Levy Fee (ID)", width: 200 },
                                { field: "JSXLevyFeeDesc", title: "JSX Levy Fee (Name)", width: 250 },
                                { field: "KPEIFee", title: "KPEIFee", width: 150, hidden: true },
                                { field: "KPEIFeeID", title: "KPE IFee (ID)", width: 200, hidden: true },
                                { field: "KPEIFeeDesc", title: "KPEI Fee (Name)", width: 250, hidden: true },
                                { field: "VATFee", title: "VATFee", width: 150, hidden: true },
                                { field: "VATFeeID", title: "VAT IFee (ID)", width: 200, hidden: true },
                                { field: "VATFeeDesc", title: "VAT Fee (Name)", width: 250, hidden: true },
                                { field: "SalesTax", title: "SalesTax", width: 150, hidden: true },
                                { field: "SalesTaxID", title: "Sales Tax (ID)", width: 200, hidden: true },
                                { field: "SalesTaxDesc", title: "Sales Tax (Name)", width: 250, hidden: true },
                                { field: "WHTFee", title: "WHTFee", width: 150, hidden: true },
                                { field: "WHTFeeID", title: "WHTFee (ID)", width: 200 },
                                { field: "WHTFeeDesc", title: "WHTFee (Name)", width: 250 },
                                { field: "IncomeTaxArt23", title: "IncomeTaxArt23", width: 150, hidden: true },
                                { field: "IncomeTaxArt23ID", title: "Income Tax Art 23 (ID)", width: 200 },
                                { field: "IncomeTaxArt23Desc", title: "Income Tax Art 23 (Name)", width: 250 },
                                { field: "EquitySellMethod", title: "EquitySellMethod", width: 150, hidden: true },
                                { field: "EquitySellMethodDesc", title: "Equity Sell Method (Name)", width: 250, hidden: true },
                                { field: "AveragePriority", title: "AveragePriority", width: 150, hidden: true },
                                { field: "AveragePriorityDesc", title: "Average Priority (Name)", width: 250, hidden: true },
                                { field: "FixedAsetBuyPPN", title: "FixedAsetBuyPPN", width: 150, hidden: true },
                                { field: "FixedAsetBuyPPNID", title: "Fixed Aset Buy PPN (ID)", width: 200, hidden: true },
                                { field: "FixedAsetBuyPPNDesc", title: "Fixed Aset Buy PPN (Name)", width: 250, hidden: true },
                                { field: "FixedAsetSellPPN", title: "FixedAsetSellPPN", width: 150, hidden: true },
                                { field: "FixedAsetSellPPNID", title: "Fixed Aset Sell PPN (ID)", width: 200, hidden: true },
                                { field: "FixedAsetSellPPNDesc", title: "Fixed Aset Sell PPN (Name)", width: 250, hidden: true },

                                { field: "ARManagementFee", title: "ARManagementFee", width: 150, hidden: true },
                                { field: "ARManagementFeeID", title: "ARManagementFee (ID)", width: 200 },
                                { field: "ARManagementFeeDesc", title: "ARManagementFee (Name)", width: 250 },
                                { field: "ManagementFeeExpense", title: "ManagementFeeExpense", width: 150, hidden: true },
                                { field: "ManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 200 },
                                { field: "ManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 250 },

                                { field: "TaxARManagementFee", title: "ARManagementFee", width: 150, hidden: true },
                                { field: "TaxARManagementFeeID", title: "ARManagementFee (ID)", width: 200 },
                                { field: "TaxARManagementFeeDesc", title: "ARManagementFee (Name)", width: 250 },
                                { field: "TaxManagementFeeExpense", title: "ManagementFeeExpense", width: 150, hidden: true },
                                { field: "TaxManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 200 },
                                { field: "TaxManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 250 },

                                { field: "TotalEquity", title: "TotalEquity", width: 150, hidden: true },
                                { field: "TotalEquityID", title: "TotalEquity (ID)", width: 200 },
                                { field: "TotalEquityDesc", title: "TotalEquity (Name)", width: 250 },

                                { field: "MKBD06Bank", title: "MKBD06Bank", width: 150, hidden: true },
                                { field: "MKBD06BankID", title: "MKBD06Bank (ID)", width: 200 },
                                { field: "MKBD06BankDesc", title: "MKBD06Bank (Name)", width: 250 },

                                { field: "MKBD06PettyCash", title: "MKBD06PettyCash", width: 150, hidden: true },
                                { field: "MKBD06PettyCashID", title: "MKBD06PettyCash (ID)", width: 200 },
                                { field: "MKBD06PettyCashDesc", title: "MKBD06PettyCash (Name)", width: 250 },

                                { field: "UnrealizedAccountSGD", title: "UnrealizedAccountSGD", width: 150, hidden: true },
                                { field: "UnrealizedAccountSGDID", title: "UnrealizedAccountSGD (ID)", width: 200 },
                                { field: "UnrealizedAccountSGDDesc", title: "UnrealizedAccountSGD (Name)", width: 250 },

                                { field: "UnrealizedAccountUSD", title: "UnrealizedAccountUSD", width: 150, hidden: true },
                                { field: "UnrealizedAccountUSDID", title: "UnrealizedAccountUSD (ID)", width: 200 },
                                { field: "UnrealizedAccountUSDDesc", title: "UnrealizedAccountUSD (Name)", width: 250 },


                                { field: "ARSubscriptionFee", title: "ARSubscriptionFee", width: 150, hidden: true },
                                { field: "ARSubscriptionFeeID", title: "ARSubscriptionFee (ID)", width: 200 },
                                { field: "ARSubscriptionFeeDesc", title: "ARSubscriptionFee (Name)", width: 250 },

                                { field: "SubscriptionFeeIncome", title: "SubscriptionFeeIncome", width: 150, hidden: true },
                                { field: "SubscriptionFeeIncomeID", title: "SubscriptionFeeIncome (ID)", width: 200 },
                                { field: "SubscriptionFeeIncomeDesc", title: "SubscriptionFeeIncome (Name)", width: 250 },

                                { field: "ARRedemptionFee", title: "ARRedemptionFee", width: 150, hidden: true },
                                { field: "ARRedemptionFeeID", title: "ARRedemptionFee (ID)", width: 200 },
                                { field: "ARRedemptionFeeDesc", title: "ARRedemptionFee (Name)", width: 250 },

                                { field: "RedemptionFeeIncome", title: "RedemptionFeeIncome", width: 150, hidden: true },
                                { field: "RedemptionFeeIncomeID", title: "RedemptionFeeIncome (ID)", width: 200 },
                                { field: "RedemptionFeeIncomeDesc", title: "RedemptionFeeIncome (Name)", width: 250 },

                                { field: "ARSwitchingFee", title: "ARSwitchingFee", width: 150, hidden: true },
                                { field: "ARSwitchingFeeID", title: "ARSwitchingFee (ID)", width: 200 },
                                { field: "ARSwitchingFeeDesc", title: "ARSwitchingFee (Name)", width: 250 },

                                { field: "SwitchingFeeIncome", title: "SwitchingFeeIncome", width: 150, hidden: true },
                                { field: "SwitchingFeeIncomeID", title: "SwitchingFeeIncome (ID)", width: 200 },
                                { field: "SwitchingFeeIncomeDesc", title: "SwitchingFeeIncome (Name)", width: 250 },


                                { field: "SwitchingFundAcc", title: "SwitchingFundAcc", width: 150, hidden: true },
                                { field: "SwitchingFundAccID", title: "SwitchingFundAcc (ID)", width: 200 },
                                { field: "SwitchingFundAccDesc", title: "SwitchingFundAcc (Name)", width: 250 },

                                { field: "AgentCommissionExpense", title: "AgentCommissionExpense", width: 150, hidden: true },
                                { field: "AgentCommissionExpenseID", title: "AgentCommissionExpense (ID)", width: 200 },
                                { field: "AgentCommissionExpenseDesc", title: "AgentCommissionExpense (Name)", width: 250 },

                                { field: "AgentCommissionPayable", title: "AgentCommissionPayable", width: 150, hidden: true },
                                { field: "AgentCommissionPayableID", title: "AgentCommissionPayable (ID)", width: 200 },
                                { field: "AgentCommissionPayableDesc", title: "AgentCommissionPayable (Name)", width: 250 },

                                { field: "WHTPayablePPH21", title: "WHTPayablePPH21", width: 150, hidden: true },
                                { field: "WHTPayablePPH21ID", title: "WHTPayablePPH21 (ID)", width: 200 },
                                { field: "WHTPayablePPH21Desc", title: "WHTPayablePPH21 (Name)", width: 250 },

                                { field: "WHTPayablePPH23", title: "WHTPayablePPH23", width: 150, hidden: true },
                                { field: "WHTPayablePPH23ID", title: "WHTPayablePPH23 (ID)", width: 200 },
                                { field: "WHTPayablePPH23Desc", title: "WHTPayablePPH23 (Name)", width: 250 },

                                { field: "VatIn", title: "VatIn", width: 150, hidden: true },
                                { field: "VatInID", title: "VatIn (ID)", width: 200 },
                                { field: "VatInDesc", title: "VatIn (Name)", width: 250 },

                                { field: "VatOut", title: "VatOut", width: 150, hidden: true },
                                { field: "VatOutID", title: "VatOut (ID)", width: 200 },
                                { field: "VatOutDesc", title: "VatOut (Name)", width: 250 },

                                { field: "AgentCommissionCash", title: "AgentCommissionCash", width: 150, hidden: true },
                                { field: "AgentCommissionCashID", title: "AgentCommissionCash (ID)", width: 200 },
                                { field: "AgentCommissionCashDesc", title: "AgentCommissionCash (Name)", width: 250 },

                                { field: "AgentCSRExpense", title: "AgentCSRExpense", width: 150, hidden: true },
                                { field: "AgentCSRExpenseID", title: "AgentCSRExpense (ID)", width: 200 },
                                { field: "AgentCSRExpenseDesc", title: "AgentCSRExpense (Name)", width: 250 },

                                { field: "AgentCSRPayable", title: "AgentCSRPayable", width: 150, hidden: true },
                                { field: "AgentCSRPayableID", title: "AgentCSRPayable (ID)", width: 200 },
                                { field: "AgentCSRPayableDesc", title: "AgentCSRPayable (Name)", width: 250 },


                                { field: "DefaultCurrencyPK", title: "DefaultCurrencyPK", hidden: true, width: 100 },
                                { field: "DefaultCurrencyID", title: "Default Currency ID", width: 170 },
                                { field: "DecimalPlaces", title: "<div style='text-align: right'>Decimal Places", width: 150, attributes: { style: "text-align:right;" } },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 120 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 120 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "VoidUsersID", title: "VoidID", width: 120 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var AccountingSetupHistoryURL = window.location.origin + "/Radsoft/AccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 3,
                            dataSourceHistory = getDataSource(AccountingSetupHistoryURL);

                        $("#gridAccountingSetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: 470,
                            scrollable: {
                                virtual: true
                            },
                            reorderable: true,
                            sortable: true,
                            resizable: true,
                            groupable: true,
                            filterable: true,
                            pageable: true,
                            columnMenu: true,
                            pageable: {
                                input: true,
                                numeric: false
                            },
                            columns: [
                                { command: { text: "Details", click: showDetails }, title: " ", width: 85 },
                                { field: "AccountingSetupPK", title: "SysNo.", filterable: false, width: 85 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 75 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 75 },
                                { field: "Income", title: "Income", width: 150, hidden: true },
                                { field: "IncomeID", title: "Income (ID)", width: 200, hidden: true },
                                { field: "IncomeDesc", title: "Income (Name)", width: 250, hidden: true },
                                { field: "Expense", title: "Expense", width: 150, hidden: true },
                                { field: "ExpenseID", title: "Expense (ID)", width: 200, hidden: true },
                                { field: "ExpenseDesc", title: "Expense (Name)", width: 250, hidden: true },
                                { field: "PPHFinal", title: "PPHFinal", width: 150, hidden: true },
                                { field: "PPHFinalID", title: "PPHFinal (ID)", width: 200 },
                                { field: "PPHFinalDesc", title: "PPHFinal (Name)", width: 250 },

                                { field: "InvInBond", title: "InvInBond", width: 150, hidden: true },
                                { field: "InvInBondID", title: "Investment In Bond (ID)", width: 200 },
                                { field: "InvInBondDesc", title: "Investment In Bond (Name)", width: 250 },

                                { field: "InvInBondGovIDR", title: "InvInBondGovIDR", width: 150, hidden: true },
                                { field: "InvInBondGovIDRID", title: "Investment In Bond Goverment IDR (ID)", width: 200, hidden: true },
                                { field: "InvInBondGovIDRDesc", title: "Investment In Bond Goverment IDR (Name)", width: 250, hidden: true },
                                { field: "InvInBondGovUSD", title: "InvInBondGovUSD", width: 150, hidden: true },
                                { field: "InvInBondGovUSDID", title: "Investment In Bond Goverment USD (ID)", width: 200, hidden: true },
                                { field: "InvInBondGovUSDDesc", title: "Investment In Bond Goverment USD (Name)", width: 250, hidden: true },
                                { field: "InvInBondCorpIDR", title: "InvInBondCorpIDR", width: 150, hidden: true },
                                { field: "InvInBondCorpIDRID", title: "Investment In Bond Corporate IDR (ID)", width: 200, hidden: true },
                                { field: "InvInBondCorpIDRDesc", title: "Investment In Bond Corporate IDR (Name)", width: 250, hidden: true },
                                { field: "InvInBondCorpUSD", title: "InvInBondCorpUSD", width: 150, hidden: true },
                                { field: "InvInBondCorpUSDID", title: "Investment In Bond Corporate USD (ID)", width: 200, hidden: true },
                                { field: "InvInBondCorpUSDDesc", title: "Investment In Bond Corporate USD (Name)", width: 250, hidden: true },

                                { field: "InvInEquity", title: "InvInEquity", width: 150, hidden: true },
                                { field: "InvInEquityID", title: "Investment In Equity (ID)", width: 200 },
                                { field: "InvInEquityDesc", title: "Investment In Equity (Name)", width: 250 },
                                { field: "InvInTD", title: "InvInTD", width: 150, hidden: true },
                                { field: "InvInTDID", title: "Investment In TD (ID)", width: 200 },
                                { field: "InvInTDDesc", title: "Investment In TD (Name)", width: 250 },

                                { field: "InvInReksadana", title: "InvInReksadana", width: 150, hidden: true },
                                { field: "InvInReksadanaID", title: "Investment In Reksadana (ID)", width: 200 },
                                { field: "InvInReksadanaDesc", title: "Investment In Reksadana (Name)", width: 250 },

                                { field: "APPurchaseBond", title: "APPurchaseBond", width: 150, hidden: true },
                                { field: "APPurchaseBondID", title: "AP Purchase Bond (ID)", width: 200 },
                                { field: "APPurchaseBondDesc", title: "AP Purchase Bond (Name)", width: 250 },
                                { field: "APPurchaseEquity", title: "APPurchaseEquity", width: 150, hidden: true },
                                { field: "APPurchaseEquityID", title: "AP Purchase Equity (ID)", width: 200 },
                                { field: "APPurchaseEquityDesc", title: "AP Purchase Equity (Name)", width: 250 },
                                { field: "APPurchaseTD", title: "APPurchaseTD", width: 150, hidden: true },
                                { field: "APPurchaseTDID", title: "AP Purchase TD (ID)", width: 200, hidden: true },
                                { field: "APPurchaseTDDesc", title: "AP Purchase TD (Name)", width: 250, hidden: true },
                                { field: "APPurchaseReksadana", title: "AP Purchase Reksadana", width: 150, hidden: true },
                                { field: "APPurchaseReksadanaID", title: "AP Purchase Reksadana (ID)", width: 200, hidden: true },
                                { field: "APPurchaseReksadanaDesc", title: "AP Purchase Reksadana (Name)", width: 250, hidden: true },
                                { field: "ARSellBond", title: "ARSellBond", width: 150, hidden: true },
                                { field: "ARSellBondID", title: "AR Sell Bond (ID)", width: 200 },
                                { field: "ARSellBondDesc", title: "AR Sell Bond (Name)", width: 250 },
                                { field: "ARSellEquity", title: "ARSellEquity", width: 150, hidden: true },
                                { field: "ARSellEquityID", title: "AR Sell Equity (ID)", width: 200 },
                                { field: "ARSellEquityDesc", title: "AR Sell Equity (Name)", width: 250 },
                                { field: "ARSellTD", title: "ARSellTD", width: 150, hidden: true },
                                { field: "ARSellTDID", title: "AR Sell TD (ID)", width: 200, hidden: true },
                                { field: "ARSellTDDesc", title: "AR Sell TD (Name)", width: 250, hidden: true },
                                { field: "ARSellReksadana", title: "AR Sell Reksadana", width: 150, hidden: true },
                                { field: "ARSellReksadanaID", title: "AR Sell Reksadana (ID)", width: 200, hidden: true },
                                { field: "ARSellReksadanaDesc", title: "AR Sell Reksadana (Name)", width: 250, hidden: true },
                                { field: "InterestPurchaseBond", title: "InterestPurchaseBond", width: 150, hidden: true },
                                { field: "InterestPurchaseBondID", title: "Interest Purchase Bond (ID)", width: 200 },
                                { field: "InterestPurchaseBondDesc", title: "Interest Purchase Bond (Name)", width: 250 },
                                { field: "InterestReceivableBond", title: "InterestReceivableBond", width: 150, hidden: true },
                                { field: "InterestReceivableBondID", title: "Interest Receivable Bond (ID)", width: 200 },
                                { field: "InterestReceivableBondDesc", title: "Interest Receivable Bond (Name)", width: 250 },

                                { field: "InterestReceivableBondGovIDR", title: "InterestReceivableBondGovIDR", width: 150, hidden: true },
                                { field: "InterestReceivableBondGovIDRID", title: "Interest Receivable Bond Goverment IDR (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondGovIDRDesc", title: "Interest Receivable Bond Goverment IDR (Name)", width: 250, hidden: true },
                                { field: "InterestReceivableBondCorpIDR", title: "InterestReceivableBondCorpIDR", width: 150, hidden: true },
                                { field: "InterestReceivableBondCorpIDRID", title: "Interest Receivable Bond Corporate IDR (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondCorpIDRDesc", title: "Interest Receivable Bond Corporate IDR (Name)", width: 250, hidden: true },
                                { field: "InterestReceivableBondGovUSD", title: "InterestReceivableBondGovUSD", width: 150, hidden: true },
                                { field: "InterestReceivableBondGovUSDID", title: "Interest Receivable Bond Goverment USD (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondGovUSDDesc", title: "Interest Receivable Bond Goverment USD (Name)", width: 250, hidden: true },
                                { field: "InterestReceivableBondCorpUSD", title: "InterestReceivableBondCorpUSD", width: 150, hidden: true },
                                { field: "InterestReceivableBondCorpUSDID", title: "Interest Receivable Bond Corporate USD (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableBondCorpUSDDesc", title: "Interest Receivable Bond Corporate USD (Name)", width: 250, hidden: true },

                                { field: "InterestReceivableTD", title: "InterestReceivableTD", width: 150, hidden: true },
                                { field: "InterestReceivableTDID", title: "Interest Receivable TD (ID)", width: 200, hidden: true },
                                { field: "InterestReceivableTDDesc", title: "Interest Receivable TD (Name)", width: 250, hidden: true },
                                { field: "CashBond", title: "Bank for Bond", width: 150, hidden: true },
                                { field: "CashBondID", title: "Bank for Bond (ID)", width: 200, hidden: true },
                                { field: "CashBondDesc", title: "Bank for Bond (Name)", width: 250, hidden: true },
                                { field: "CashEquity", title: "Bank for Equity", width: 150, hidden: true },
                                { field: "CashEquityID", title: "Bank for Equity (ID)", width: 200, hidden: true },
                                { field: "CashEquityDesc", title: "Bank for Equity (Name)", width: 250, hidden: true },
                                { field: "CashTD", title: "Bank for Time Deposit", width: 150, hidden: true },
                                { field: "CashTDID", title: "Bank for Time Deposit (ID)", width: 200, hidden: true },
                                { field: "CashTDDesc", title: "Bank for Time Deposit (Name)", width: 250, hidden: true },
                                { field: "ForeignExchangeRevalAccount", title: "ForeignExchangeRevalAccount", width: 150, hidden: true },
                                { field: "ForeignExchangeRevalAccountID", title: "Foreign Exchange Reval Account(ID)", width: 300, hidden: true },
                                { field: "ForeignExchangeRevalAccountDesc", title: "Foreign Exchange Reval Account (Name)", width: 300, hidden: true },
                                { field: "UnrealisedEquity", title: "UnrealisedEquity", width: 150, hidden: true },
                                { field: "UnrealisedEquityID", title: "Unrealised Gain/Loss from sale Equity (ID)", width: 300 },
                                { field: "UnrealisedEquityDesc", title: "Unrealised Gain/Loss from sale Equity (Name)", width: 300 },
                                { field: "UnrealisedBond", title: "UnrealisedBond", width: 150, hidden: true },
                                { field: "UnrealisedBondID", title: "Unrealised Gain/Loss from sale Bond (ID)", width: 300 },
                                { field: "UnrealisedBondDesc", title: "Unrealised Gain/Loss from sale Bond (Name)", width: 300 },
                                { field: "UnrealisedBondIDUSD", title: "Unrealised Gain/Loss from sale Bond (ID) USD", width: 300 },
                                { field: "UnrealisedBondDescUSD", title: "Unrealised Gain/Loss from sale Bond (Name) USD", width: 300 },
                                { field: "UnrealisedReksadana", title: "UnrealisedReksadana", width: 150, hidden: true },
                                { field: "UnrealisedReksadanaID", title: "Unrealised Gain/Loss from sale Reksadana (ID)", width: 300 },
                                { field: "UnrealisedReksadanaDesc", title: "Unrealised Gain/Loss from sale Reksadana (Name)", width: 350 },
                                { field: "RealisedEquity", title: "RealisedEquity", width: 150, hidden: true },
                                { field: "RealisedEquityID", title: "Realised Gain/Loss from sale Equity (ID)", width: 300 },
                                { field: "RealisedEquityDesc", title: "Realised Gain/Loss from sale Equity (Name)", width: 300 },

                                { field: "RealisedBondGovIDR", title: "RealisedBondGovIDR", width: 150, hidden: true },
                                { field: "RealisedBondGovIDRID", title: "Realised Bond Goverment IDR (ID)", width: 200, hidden: true },
                                { field: "RealisedBondGovIDRDesc", title: "Realised Bond Goverment IDR (Name)", width: 250, hidden: true },
                                { field: "RealisedBondGovUSD", title: "RealisedBondGovUSD", width: 150, hidden: true },
                                { field: "RealisedBondGovUSDID", title: "Realised Bond Goverment USD (ID)", width: 200, hidden: true },
                                { field: "RealisedBondGovUSDDesc", title: "Realised Bond Goverment USD (Name)", width: 250, hidden: true },
                                { field: "RealisedBondCorpIDR", title: "RealisedBondCorpIDR", width: 150, hidden: true },
                                { field: "RealisedBondCorpIDRID", title: "Realised Bond Corporate IDR (ID)", width: 200, hidden: true },
                                { field: "RealisedBondCorpIDRDesc", title: "Realised Bond Corporate IDR (Name)", width: 250, hidden: true },
                                { field: "RealisedBondCorpUSD", title: "RealisedBondCorpUSD", width: 150, hidden: true },
                                { field: "RealisedBondCorpUSDID", title: "Realised Bond Corporate USD (ID)", width: 200, hidden: true },
                                { field: "RealisedBondCorpUSDDesc", title: "Realised Bond Corporate USD (Name)", width: 250, hidden: true },

                                { field: "RealisedBond", title: "RealisedBond", width: 150, hidden: true },
                                { field: "RealisedBondID", title: "Realised Gain/Loss from sale Bond (ID)", width: 300 },
                                { field: "RealisedBondDesc", title: "Realised Gain/Loss from sale Bond (Name)", width: 300 },
                                { field: "RealisedReksadana", title: "RealisedReksadana", width: 150, hidden: true },
                                { field: "RealisedReksadanaID", title: "Realised Gain/Loss from sale Reksadana (ID)", width: 300 },
                                { field: "RealisedReksadanaDesc", title: "Realised Gain/Loss from sale Reksadana (Name)", width: 350 },
                                { field: "RealisedAset", title: "RealisedAset", width: 150, hidden: true },
                                { field: "RealisedAsetID", title: "Realised Gain/Loss from sale Aset (ID)", width: 300, hidden: true },
                                { field: "RealisedAsetDesc", title: "Realised Gain/Loss from sale Aset (Name)", width: 300, hidden: true },
                                { field: "CadanganEquity", title: "CadanganEquity", width: 150, hidden: true },
                                { field: "CadanganEquityID", title: "Cadangan Kenaikan (Penurunan) Equity (ID)", width: 300 },
                                { field: "CadanganEquityDesc", title: "Cadangan Kenaikan (Penurunan) Equity (Name)", width: 350 },
                                { field: "CadanganBond", title: "CadanganBond", width: 150, hidden: true },
                                { field: "CadanganBondID", title: "Cadangan Kenaikan (Penurunan) Bond (ID)", width: 300 },
                                { field: "CadanganBondDesc", title: "Cadangan Kenaikan (Penurunan) Bond (Name)", width: 300 },
                                { field: "CadanganBondIDUSD", title: "Cadangan Kenaikan (Penurunan) Bond (ID) USD", width: 300 },
                                { field: "CadanganBondDescUSD", title: "Cadangan Kenaikan (Penurunan) Bond (Name) USD", width: 300 },
                                { field: "CadanganReksadana", title: "CadanganReksadana", width: 150, hidden: true },
                                { field: "CadanganReksadanaID", title: "Cadangan Kenaikan (Penurunan) Reksadana (ID)", width: 350 },
                                { field: "CadanganReksadanaDesc", title: "Cadangan Kenaikan (Penurunan) Reksadana (Name)", width: 350 },
                                { field: "BrokerageFee", title: "BrokerageFee", width: 150, hidden: true },
                                { field: "BrokerageFeeID", title: "Brokerage Fee (ID)", width: 200 },
                                { field: "BrokerageFeeDesc", title: "Brokerage Fee (Name)", width: 250 },
                                { field: "JSXLevyFee", title: "JSXLevyFee", width: 150, hidden: true },
                                { field: "JSXLevyFeeID", title: "JSX Levy Fee (ID)", width: 200 },
                                { field: "JSXLevyFeeDesc", title: "JSX Levy Fee (Name)", width: 250 },
                                { field: "KPEIFee", title: "KPEIFee", width: 150, hidden: true },
                                { field: "KPEIFeeID", title: "KPE IFee (ID)", width: 200, hidden: true },
                                { field: "KPEIFeeDesc", title: "KPEI Fee (Name)", width: 250, hidden: true },
                                { field: "VATFee", title: "VATFee", width: 150, hidden: true },
                                { field: "VATFeeID", title: "VAT IFee (ID)", width: 200, hidden: true },
                                { field: "VATFeeDesc", title: "VAT Fee (Name)", width: 250, hidden: true },
                                { field: "SalesTax", title: "SalesTax", width: 150, hidden: true },
                                { field: "SalesTaxID", title: "Sales Tax (ID)", width: 200, hidden: true },
                                { field: "SalesTaxDesc", title: "Sales Tax (Name)", width: 250, hidden: true },
                                { field: "WHTFee", title: "WHTFee", width: 150, hidden: true },
                                { field: "WHTFeeID", title: "WHTFee (ID)", width: 200 },
                                { field: "WHTFeeDesc", title: "WHTFee (Name)", width: 250 },
                                { field: "IncomeTaxArt23", title: "IncomeTaxArt23", width: 150, hidden: true },
                                { field: "IncomeTaxArt23ID", title: "Income Tax Art 23 (ID)", width: 200 },
                                { field: "IncomeTaxArt23Desc", title: "Income Tax Art 23 (Name)", width: 250 },
                                { field: "EquitySellMethod", title: "EquitySellMethod", width: 150, hidden: true },
                                { field: "EquitySellMethodDesc", title: "Equity Sell Method (Name)", width: 250, hidden: true },
                                { field: "AveragePriority", title: "AveragePriority", width: 150, hidden: true },
                                { field: "AveragePriorityDesc", title: "Average Priority (Name)", width: 250, hidden: true },
                                { field: "FixedAsetBuyPPN", title: "FixedAsetBuyPPN", width: 150, hidden: true },
                                { field: "FixedAsetBuyPPNID", title: "Fixed Aset Buy PPN (ID)", width: 200, hidden: true },
                                { field: "FixedAsetBuyPPNDesc", title: "Fixed Aset Buy PPN (Name)", width: 250, hidden: true },
                                { field: "FixedAsetSellPPN", title: "FixedAsetSellPPN", width: 150, hidden: true },
                                { field: "FixedAsetSellPPNID", title: "Fixed Aset Sell PPN (ID)", width: 200, hidden: true },
                                { field: "FixedAsetSellPPNDesc", title: "Fixed Aset Sell PPN (Name)", width: 250, hidden: true },

                                { field: "ARManagementFee", title: "ARManagementFee", width: 150, hidden: true },
                                { field: "ARManagementFeeID", title: "ARManagementFee (ID)", width: 200 },
                                { field: "ARManagementFeeDesc", title: "ARManagementFee (Name)", width: 250 },

                                { field: "ManagementFeeExpense", title: "ManagementFeeExpense", width: 150, hidden: true },
                                { field: "ManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 200 },
                                { field: "ManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 250 },

                                { field: "TaxARManagementFee", title: "ARManagementFee", width: 150, hidden: true },
                                { field: "TaxARManagementFeeID", title: "ARManagementFee (ID)", width: 200 },
                                { field: "TaxARManagementFeeDesc", title: "ARManagementFee (Name)", width: 250 },
                                { field: "TaxManagementFeeExpense", title: "ManagementFeeExpense", width: 150, hidden: true },
                                { field: "TaxManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 200 },
                                { field: "TaxManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 250 },

                                { field: "TotalEquity", title: "TotalEquity", width: 150, hidden: true },
                                { field: "TotalEquityID", title: "TotalEquity (ID)", width: 200 },
                                { field: "TotalEquityDesc", title: "TotalEquity (Name)", width: 250 },

                                { field: "MKBD06Bank", title: "MKBD06Bank", width: 150, hidden: true },
                                { field: "MKBD06BankID", title: "MKBD06Bank (ID)", width: 200 },
                                { field: "MKBD06BankDesc", title: "MKBD06Bank (Name)", width: 250 },

                                { field: "MKBD06PettyCash", title: "MKBD06PettyCash", width: 150, hidden: true },
                                { field: "MKBD06PettyCashID", title: "MKBD06PettyCash (ID)", width: 200 },
                                { field: "MKBD06PettyCashDesc", title: "MKBD06PettyCash (Name)", width: 250 },

                                { field: "UnrealizedAccountSGD", title: "UnrealizedAccountSGD", width: 150, hidden: true },
                                { field: "UnrealizedAccountSGDID", title: "UnrealizedAccountSGD (ID)", width: 200 },
                                { field: "UnrealizedAccountSGDDesc", title: "UnrealizedAccountSGD (Name)", width: 250 },

                                { field: "UnrealizedAccountUSD", title: "UnrealizedAccountUSD", width: 150, hidden: true },
                                { field: "UnrealizedAccountUSDID", title: "UnrealizedAccountUSD (ID)", width: 200 },
                                { field: "UnrealizedAccountUSDDesc", title: "UnrealizedAccountUSD (Name)", width: 250 },

                                { field: "ARSubscriptionFee", title: "ARSubscriptionFee", width: 150, hidden: true },
                                { field: "ARSubscriptionFeeID", title: "ARSubscriptionFee (ID)", width: 200 },
                                { field: "ARSubscriptionFeeDesc", title: "ARSubscriptionFee (Name)", width: 250 },

                                { field: "SubscriptionFeeIncome", title: "SubscriptionFeeIncome", width: 150, hidden: true },
                                { field: "SubscriptionFeeIncomeID", title: "SubscriptionFeeIncome (ID)", width: 200 },
                                { field: "SubscriptionFeeIncomeDesc", title: "SubscriptionFeeIncome (Name)", width: 250 },

                                { field: "ARRedemptionFee", title: "ARRedemptionFee", width: 150, hidden: true },
                                { field: "ARRedemptionFeeID", title: "ARRedemptionFee (ID)", width: 200 },
                                { field: "ARRedemptionFeeDesc", title: "ARRedemptionFee (Name)", width: 250 },

                                { field: "RedemptionFeeIncome", title: "RedemptionFeeIncome", width: 150, hidden: true },
                                { field: "RedemptionFeeIncomeID", title: "RedemptionFeeIncome (ID)", width: 200 },
                                { field: "RedemptionFeeIncomeDesc", title: "RedemptionFeeIncome (Name)", width: 250 },

                                { field: "ARSwitchingFee", title: "ARSwitchingFee", width: 150, hidden: true },
                                { field: "ARSwitchingFeeID", title: "ARSwitchingFee (ID)", width: 200 },
                                { field: "ARSwitchingFeeDesc", title: "ARSwitchingFee (Name)", width: 250 },

                                { field: "SwitchingFeeIncome", title: "SwitchingFeeIncome", width: 150, hidden: true },
                                { field: "SwitchingFeeIncomeID", title: "SwitchingFeeIncome (ID)", width: 200 },
                                { field: "SwitchingFeeIncomeDesc", title: "SwitchingFeeIncome (Name)", width: 250 },

                                { field: "SwitchingFundAcc", title: "SwitchingFundAcc", width: 150, hidden: true },
                                { field: "SwitchingFundAccID", title: "SwitchingFundAcc (ID)", width: 200 },
                                { field: "SwitchingFundAccDesc", title: "SwitchingFundAcc (Name)", width: 250 },

                                { field: "AgentCommissionExpense", title: "AgentCommissionExpense", width: 150, hidden: true },
                                { field: "AgentCommissionExpenseID", title: "AgentCommissionExpense (ID)", width: 200 },
                                { field: "AgentCommissionExpenseDesc", title: "AgentCommissionExpense (Name)", width: 250 },

                                { field: "AgentCommissionPayable", title: "AgentCommissionPayable", width: 150, hidden: true },
                                { field: "AgentCommissionPayableID", title: "AgentCommissionPayable (ID)", width: 200 },
                                { field: "AgentCommissionPayableDesc", title: "AgentCommissionPayable (Name)", width: 250 },

                                { field: "WHTPayablePPH21", title: "WHTPayablePPH21", width: 150, hidden: true },
                                { field: "WHTPayablePPH21ID", title: "WHTPayablePPH21 (ID)", width: 200 },
                                { field: "WHTPayablePPH21Desc", title: "WHTPayablePPH21 (Name)", width: 250 },

                                { field: "WHTPayablePPH23", title: "WHTPayablePPH23", width: 150, hidden: true },
                                { field: "WHTPayablePPH23ID", title: "WHTPayablePPH23 (ID)", width: 200 },
                                { field: "WHTPayablePPH23Desc", title: "WHTPayablePPH23 (Name)", width: 250 },

                                { field: "VatIn", title: "VatIn", width: 150, hidden: true },
                                { field: "VatInID", title: "VatIn (ID)", width: 200 },
                                { field: "VatInDesc", title: "VatIn (Name)", width: 250 },

                                { field: "VatOut", title: "VatOut", width: 150, hidden: true },
                                { field: "VatOutID", title: "VatOut (ID)", width: 200 },
                                { field: "VatOutDesc", title: "VatOut (Name)", width: 250 },

                                { field: "AgentCommissionCash", title: "AgentCommissionCash", width: 150, hidden: true },
                                { field: "AgentCommissionCashID", title: "AgentCommissionCash (ID)", width: 200 },
                                { field: "AgentCommissionCashDesc", title: "AgentCommissionCash (Name)", width: 250 },

                                { field: "AgentCSRExpense", title: "AgentCSRExpense", width: 150, hidden: true },
                                { field: "AgentCSRExpenseID", title: "AgentCSRExpense (ID)", width: 200 },
                                { field: "AgentCSRExpenseDesc", title: "AgentCSRExpense (Name)", width: 250 },

                                { field: "AgentCSRPayable", title: "AgentCSRPayable", width: 150, hidden: true },
                                { field: "AgentCSRPayableID", title: "AgentCSRPayable (ID)", width: 200 },
                                { field: "AgentCSRPayableDesc", title: "AgentCSRPayable (Name)", width: 250 },

                                { field: "DefaultCurrencyPK", title: "DefaultCurrencyPK", hidden: true, width: 100 },
                                { field: "DefaultCurrencyID", title: "Default Currency ID", width: 170 },
                                { field: "DecimalPlaces", title: "<div style='text-align: right'>Decimal Places", width: 150, attributes: { style: "text-align:right;" } },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 120 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 120 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "VoidUsersID", title: "VoidID", width: 120 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 150 }
                            ]
                        });
                    }
                } else {
                    refresh();
                }
            }
        });

    }

    //$("#BtnDownload").click(function () {
    //    
    //    alertify.confirm("Are you sure want to Download data AccountingSetup ?", function (e) {
    //        if (e) {
    //            $.ajax({
    //                url: window.location.origin + "/Radsoft/MasterRpt/DownloadData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/AccountingSetup",
    //                type: 'GET',
    //                contentType: "application/json;charset=utf-8",
    //                success: function (data) {
    //                    window.location = data
    //                },
    //                error: function (data) {
    //                    alertify.alert(data.responseText);
    //                }
    //            });
    //        }
    //    });
    //});

    $("#BtnRefresh").click(function () {
        refresh();
    });

    $("#BtnCancel").click(function () {
        
        alertify.confirm("Are you sure want to cancel and close detail?", function (e) {
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

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasApproved/" + sessionStorage.getItem("user") + "/" + "AccountingSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                var AccountingSetup = {

                                    Income: $('#Income').val(),
                                    Expense: $('#Expense').val(),
                                    PPHFinal: $('#PPHFinal').val(),

                                    InvInBond: $('#InvInBond').val(),

                                    InvInBondGovIDR: $('#InvInBondGovIDR').val(),
                                    InvInBondGovUSD: $('#InvInBondGovUSD').val(),
                                    InvInBondCorpIDR: $('#InvInBondCorpIDR').val(),
                                    InvInBondCorpUSD: $('#InvInBondCorpUSD').val(),

                                    InvInEquity: $('#InvInEquity').val(),
                                    InvInTD: $('#InvInTD').val(),
                                    InvInTDUSD: $('#InvInTDUSD').val(),

                                    InvInReksadana: $('#InvInReksadana').val(),

                                    APPurchaseBond: $('#APPurchaseBond').val(),
                                    APPurchaseEquity: $('#APPurchaseEquity').val(),
                                    APPurchaseTD: $('#APPurchaseTD').val(),
                                    APPurchaseReksadana: $('#APPurchaseReksadana').val(),
                                    ARSellBond: $('#ARSellBond').val(),
                                    ARSellEquity: $('#ARSellEquity').val(),
                                    ARSellTD: $('#ARSellTD').val(),
                                    ARSellReksadana: $('#ARSellReksadana').val(),
                                    InterestPurchaseBond: $('#InterestPurchaseBond').val(),
                                    InterestReceivableBond: $('#InterestReceivableBond').val(),

                                    InterestReceivableBondGovIDR: $('#InterestReceivableBondGovIDR').val(),
                                    InterestReceivableBondCorpIDR: $('#InterestReceivableBondCorpIDR').val(),
                                    InterestReceivableBondGovUSD: $('#InterestReceivableBondGovUSD').val(),
                                    InterestReceivableBondCorpUSD: $('#InterestReceivableBondCorpUSD').val(),

                                    InterestReceivableTD: $('#InterestReceivableTD').val(),
                                    CashBond: $('#CashBond').val(),
                                    CashEquity: $('#CashEquity').val(),
                                    CashTD: $('#CashTD').val(),
                                    ForeignExchangeRevalAccount: $('#ForeignExchangeRevalAccount').val(),
                                    UnrealisedEquity: $('#UnrealisedEquity').val(),
                                    UnrealisedBond: $('#UnrealisedBond').val(),
                                    UnrealisedBondUSD: $('#UnrealisedBondUSD').val(),
                                    UnrealisedReksadana: $('#UnrealisedReksadana').val(),
                                    RealisedEquity: $('#RealisedEquity').val(),

                                    RealisedBondGovIDR: $('#RealisedBondGovIDR').val(),
                                    RealisedBondGovUSD: $('#RealisedBondGovUSD').val(),
                                    RealisedBondCorpIDR: $('#RealisedBondCorpIDR').val(),
                                    RealisedBondCorpUSD: $('#RealisedBondCorpUSD').val(),

                                    RealisedBond: $('#RealisedBond').val(),
                                    RealisedReksadana: $('#RealisedReksadana').val(),
                                    RealisedAset: $('#RealisedAset').val(),
                                    CadanganEquity: $('#CadanganEquity').val(),
                                    CadanganBond: $('#CadanganBond').val(),
                                    CadanganBondUSD: $('#CadanganBondUSD').val(),
                                    CadanganReksadana: $('#CadanganReksadana').val(),
                                    BrokerageFee: $('#BrokerageFee').val(),
                                    JSXLevyFee: $('#JSXLevyFee').val(),
                                    KPEIFee: $('#KPEIFee').val(),
                                    VATFee: $('#VATFee').val(),
                                    SalesTax: $('#SalesTax').val(),
                                    IncomeTaxArt23: $('#IncomeTaxArt23').val(),
                                    EquitySellMethod: $('#EquitySellMethod').val(),
                                    AveragePriority: $('#AveragePriority').val(),
                                    FixedAsetBuyPPN: $('#FixedAsetBuyPPN').val(),
                                    FixedAsetSellPPN: $('#FixedAsetSellPPN').val(),

                                    ARManagementFee: $('#ARManagementFee').val(),
                                    ManagementFeeExpense: $('#ManagementFeeExpense').val(),
                                    TaxARManagementFee: $('#TaxARManagementFee').val(),
                                    TaxManagementFeeExpense: $('#TaxManagementFeeExpense').val(),
                                    MKBD06Bank: $('#MKBD06Bank').val(),
                                    MKBD06PettyCash: $('#MKBD06PettyCash').val(),
                                    WHTFee: $('#WHTFee').val(),
                                    TotalEquity: $('#TotalEquity').val(),

                                    DefaultCurrencyPK: $('#DefaultCurrencyPK').val(),
                                    DecimalPlaces: $('#DecimalPlaces').val(),
                                    UnrealizedAccountSGD: $('#UnrealizedAccountSGD').val(),
                                    UnrealizedAccountUSD: $('#UnrealizedAccountUSD').val(),

                                    ARSubscriptionFee: $('#ARSubscriptionFee').val(),
                                    SubscriptionFeeIncome: $('#SubscriptionFeeIncome').val(),
                                    ARRedemptionFee: $('#ARRedemptionFee').val(),
                                    RedemptionFeeIncome: $('#RedemptionFeeIncome').val(),
                                    ARSwitchingFee: $('#ARSwitchingFee').val(),
                                    SwitchingFeeIncome: $('#SwitchingFeeIncome').val(),
                                    SwitchingFundAcc: $('#SwitchingFundAcc').val(),

                                    AgentCommissionExpense: $('#AgentCommissionExpense').val(),
                                    AgentCommissionPayable: $('#AgentCommissionPayable').val(),
                                    WHTPayablePPH21: $('#WHTPayablePPH21').val(),
                                    WHTPayablePPH23: $('#WHTPayablePPH23').val(),
                                    VatIn: $('#VatIn').val(),
                                    VatOut: $('#VatOut').val(),
                                    AgentCommissionCash: $('#AgentCommissionCash').val(),

                                    AgentCSRExpense: $('#AgentCSRExpense').val(),
                                    AgentCSRPayable: $('#AgentCSRPayable').val(),

                                    EntryUsersID: sessionStorage.getItem("user")

                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingSetup_I",
                                    type: 'POST',
                                    data: JSON.stringify(AccountingSetup),
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
                                alertify.alert("Data has exist, no more addition!");
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountingSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                                var AccountingSetup = {
                                    AccountingSetupPK: $('#AccountingSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Income: $('#Income').val(),
                                    Expense: $('#Expense').val(),
                                    PPHFinal: $('#PPHFinal').val(),

                                    InvInBond: $('#InvInBond').val(),

                                    InvInBondGovIDR: $('#InvInBondGovIDR').val(),
                                    InvInBondGovUSD: $('#InvInBondGovUSD').val(),
                                    InvInBondCorpIDR: $('#InvInBondCorpIDR').val(),
                                    InvInBondCorpUSD: $('#InvInBondCorpUSD').val(),

                                    InvInEquity: $('#InvInEquity').val(),
                                    InvInTD: $('#InvInTD').val(),
                                    InvInTDUSD: $('#InvInTDUSD').val(),

                                    InvInReksadana: $('#InvInReksadana').val(),

                                    APPurchaseBond: $('#APPurchaseBond').val(),
                                    APPurchaseEquity: $('#APPurchaseEquity').val(),
                                    APPurchaseTD: $('#APPurchaseTD').val(),
                                    APPurchaseReksadana: $('#APPurchaseReksadana').val(),
                                    ARSellBond: $('#ARSellBond').val(),
                                    ARSellEquity: $('#ARSellEquity').val(),
                                    ARSellTD: $('#ARSellTD').val(),
                                    ARSellReksadana: $('#ARSellReksadana').val(),
                                    InterestPurchaseBond: $('#InterestPurchaseBond').val(),
                                    InterestReceivableBond: $('#InterestReceivableBond').val(),

                                    InterestReceivableBondGovIDR: $('#InterestReceivableBondGovIDR').val(),
                                    InterestReceivableBondCorpIDR: $('#InterestReceivableBondCorpIDR').val(),
                                    InterestReceivableBondGovUSD: $('#InterestReceivableBondGovUSD').val(),
                                    InterestReceivableBondCorpUSD: $('#InterestReceivableBondCorpUSD').val(),

                                    InterestReceivableTD: $('#InterestReceivableTD').val(),
                                    CashBond: $('#CashBond').val(),
                                    CashEquity: $('#CashEquity').val(),
                                    CashTD: $('#CashTD').val(),
                                    ForeignExchangeRevalAccount: $('#ForeignExchangeRevalAccount').val(),
                                    UnrealisedEquity: $('#UnrealisedEquity').val(),
                                    UnrealisedBond: $('#UnrealisedBond').val(),
                                    UnrealisedBondUSD: $('#UnrealisedBondUSD').val(),
                                    UnrealisedReksadana: $('#UnrealisedReksadana').val(),
                                    RealisedEquity: $('#RealisedEquity').val(),

                                    RealisedBondGovIDR: $('#RealisedBondGovIDR').val(),
                                    RealisedBondGovUSD: $('#RealisedBondGovUSD').val(),
                                    RealisedBondCorpIDR: $('#RealisedBondCorpIDR').val(),
                                    RealisedBondCorpUSD: $('#RealisedBondCorpUSD').val(),

                                    RealisedBond: $('#RealisedBond').val(),
                                    RealisedReksadana: $('#RealisedReksadana').val(),
                                    RealisedAset: $('#RealisedAset').val(),
                                    CadanganEquity: $('#CadanganEquity').val(),
                                    CadanganBond: $('#CadanganBond').val(),
                                    CadanganBondUSD: $('#CadanganBondUSD').val(),
                                    CadanganReksadana: $('#CadanganReksadana').val(),
                                    BrokerageFee: $('#BrokerageFee').val(),
                                    JSXLevyFee: $('#JSXLevyFee').val(),
                                    KPEIFee: $('#KPEIFee').val(),
                                    VATFee: $('#VATFee').val(),
                                    SalesTax: $('#SalesTax').val(),
                                    IncomeTaxArt23: $('#IncomeTaxArt23').val(),
                                    EquitySellMethod: $('#EquitySellMethod').val(),
                                    AveragePriority: $('#AveragePriority').val(),
                                    FixedAsetBuyPPN: $('#FixedAsetBuyPPN').val(),
                                    FixedAsetSellPPN: $('#FixedAsetSellPPN').val(),

                                    ARManagementFee: $('#ARManagementFee').val(),
                                    ManagementFeeExpense: $('#ManagementFeeExpense').val(),
                                    TaxARManagementFee: $('#TaxARManagementFee').val(),
                                    TaxManagementFeeExpense: $('#TaxManagementFeeExpense').val(),
                                    MKBD06Bank: $('#MKBD06Bank').val(),
                                    MKBD06PettyCash: $('#MKBD06PettyCash').val(),
                                    WHTFee: $('#WHTFee').val(),
                                    TotalEquity: $('#TotalEquity').val(),

                                    DefaultCurrencyPK: $('#DefaultCurrencyPK').val(),
                                    DecimalPlaces: $('#DecimalPlaces').val(),
                                    UnrealizedAccountSGD: $('#UnrealizedAccountSGD').val(),
                                    UnrealizedAccountUSD: $('#UnrealizedAccountUSD').val(),

                                    ARSubscriptionFee: $('#ARSubscriptionFee').val(),
                                    SubscriptionFeeIncome: $('#SubscriptionFeeIncome').val(),
                                    ARRedemptionFee: $('#ARRedemptionFee').val(),
                                    RedemptionFeeIncome: $('#RedemptionFeeIncome').val(),
                                    ARSwitchingFee: $('#ARSwitchingFee').val(),
                                    SwitchingFeeIncome: $('#SwitchingFeeIncome').val(),
                                    SwitchingFundAcc: $('#SwitchingFundAcc').val(),

                                    AgentCommissionExpense: $('#AgentCommissionExpense').val(),
                                    AgentCommissionPayable: $('#AgentCommissionPayable').val(),
                                    WHTPayablePPH21: $('#WHTPayablePPH21').val(),
                                    WHTPayablePPH23: $('#WHTPayablePPH23').val(),
                                    VatIn: $('#VatIn').val(),
                                    VatOut: $('#VatOut').val(),
                                    AgentCommissionCash: $('#AgentCommissionCash').val(),

                                    AgentCSRExpense: $('#AgentCSRExpense').val(),
                                    AgentCSRPayable: $('#AgentCSRPayable').val(),

                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/AccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(AccountingSetup),
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
            url: window.location.origin + "/Radsoft/AccountingSetup/GetOldData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#AccountingSetupPK").val(),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data != undefined) {
                    $("#OldData").html("Notes : " + data.Notes +

                        "</br>Investment In Bond (ID) : " + data.InvInBondID +
                        "</br>Investment In Bond (Name) : " + data.InvInBondDesc +
                        "</br>Investment In Equity (ID) : " + data.InvInEquityID +
                        "</br>Investment In Equity (Name) : " + data.InvInEquityDesc +
                        "</br>Investment In Time Deposit (ID) : " + data.InvInTDID +
                        "</br>Investment In Time Deposit (Name) : " + data.InvInTDDesc +
                        "</br>Account Payable Purchase of Bond (ID) : " + data.APPurchaseBondID +
                        "</br>Account Payable Purchase of Bond (Name) : " + data.APPurchaseBondDesc +
                        "</br>Account Payable Purchase of Equity (ID) : " + data.APPurchaseEquityID +
                        "</br>Account Payable Purchase of Equity (Name) : " + data.APPurchaseEquityDesc +
                        "</br>Account Payable Purchase of Time Deposit (ID) : " + data.APPurchaseTDID +
                        "</br>Account Payable Purchase of Time Deposit (Name) : " + data.APPurchaseTDDesc +
                        "</br>Account Receivable Sell of Bond (ID) : " + data.ARSellBondID +
                        "</br>Account Receivable Sell of Bond (Name) : " + data.ARSellBondDesc +
                        "</br>Account Receivable Sell of Equity (ID) : " + data.ARSellEquityID +
                        "</br>Account Receivable Sell of Equity (Name) : " + data.ARSellEquityDesc +
                        "</br>Account Receivable Sell of Time Deposit (ID) : " + data.ARSellTDID +
                        "</br>Account Receivable Sell of Time Deposit (Name) : " + data.ARSellTDDesc +
                        "</br>Interest Purchase of Bond (ID) : " + data.InterestPurchaseBondID +
                        "</br>Interest Purchase of Bond (Name) : " + data.InterestPurchaseBondDesc +
                        "</br>Interest Receivable Bond (ID) : " + data.InterestReceivableBondID +
                        "</br>Interest Receivable Bond (Name) : " + data.InterestReceivableBondDesc +
                        "</br>Interest Receivable of Time Deposit (ID) : " + data.InterestReceivableTDID +
                        "</br>Interest Receivable of Time Deposit (Name) : " + data.InterestReceivableTDDesc +
                        "</br>Bank of Bond (ID) : " + data.CashBondID +
                        "</br>Bank of Bond (Name) : " + data.CashBondDesc +
                        "</br>Bank of Equity (ID) : " + data.CashEquityID +
                        "</br>Bank of Equity (Name) : " + data.CashEquityDesc +
                        "</br>Bank of Time Deposit (ID) : " + data.CashTDID +
                        "</br>Bank of Time Deposit (Name) : " + data.CashTDDesc +
                        "</br>Foreig nExchange Reval Account (ID) : " + data.ForeignExchangeRevalAccountID +
                        "</br>Foreign Exchange Reval Account (Name) : " + data.ForeignExchangeRevalAccountDesc +
                        "</br>Unrealised Gain/Loss from sale Equity (ID) : " + data.UnrealisedEquityID +
                        "</br>Unrealised Gain/Loss from sale Equity (Name) : " + data.UnrealisedEquityDesc +
                        "</br>Unrealised Gain/Loss from sale Bond (ID) : " + data.UnrealisedBondID +
                        "</br>Unrealised Gain/Loss from sale Bond (Name) : " + data.UnrealisedBondDesc +
                        "</br>Unrealised Gain/Loss from sale Bond (ID) USD : " + data.UnrealisedBondIDUSD +
                        "</br>Unrealised Gain/Loss from sale Bond (Name) USD : " + data.UnrealisedBondDescUSD +
                        "</br>Unrealised Gain/Loss from sale Reksadana (ID) : " + data.UnrealisedReksadanaID +
                        "</br>Unrealised Gain/Loss from sale Reksadana (Name) : " + data.UnrealisedReksadanaDesc +
                        "</br>Realised Gain/Loss from sale Equity (ID) : " + data.RealisedEquityID +
                        "</br>Realised Gain/Loss from sale Equity (Name) : " + data.RealisedEquityDesc +
                        "</br>Realised Gain/Loss from sale Bond (ID) : " + data.RealisedBondID +
                        "</br>Realised Gain/Loss from sale Bond (Name) : " + data.RealisedBondDesc +
                        "</br>Realised Gain/Loss from sale Reksadana (ID) : " + data.RealisedReksadanaID +
                        "</br>Realised Gain/Loss from sale Reksadana (Name) : " + data.RealisedReksadanaDesc +
                        "</br>Realised Gain/Loss from sale Aset (ID) : " + data.RealisedAsetID +
                        "</br>Realised Gain/Loss from sale Aset (Name) : " + data.RealisedAsetDesc +
                        "</br>Cadangan Kenaikan (Penurunan) Equity (ID) : " + data.CadanganEquityID +
                        "</br>Cadangan Kenaikan (Penurunan) Equity (Name) : " + data.CadanganEquityDesc +
                        "</br>Cadangan Kenaikan (Penurunan) Bond (ID) : " + data.CadanganBondID +
                        "</br>Cadangan Kenaikan (Penurunan) Bond (Name) : " + data.CadanganBondDesc +
                        "</br>Cadangan Kenaikan (Penurunan) Bond (ID) USD : " + data.CadanganBondIDUSD +
                        "</br>Cadangan Kenaikan (Penurunan) Bond (Name) USD : " + data.CadanganBondDescUSD +
                        "</br>Cadangan Kenaikan (Penurunan) Reksadana (ID) : " + data.CadanganReksadanaID +
                        "</br>Cadangan Kenaikan (Penurunan) Reksadana (Name) : " + data.CadanganReksadanaDesc +
                        "</br>Brokerage Fee (ID) : " + data.BrokerageFeeID +
                        "</br>Brokerage Fee (Name) : " + data.BrokerageFeeDesc +
                        "</br>JSX Levy Fee (ID) : " + data.JSXLevyFeeID +
                        "</br>JSX Levy Fee (Name) : " + data.JSXLevyFeeDesc +
                        "</br>KPEI Fee (ID) : " + data.KPEIFeeID +
                        "</br>KPEI Fee (Name) : " + data.KPEIFeeDesc +
                        "</br>Sales Tax (ID) : " + data.SalesTaxID +
                        "</br>Sales Tax (Name) : " + data.SalesTaxDesc +
                        "</br>Income Tax Art 23 (ID) : " + data.IncomeTaxArt23ID +
                        "</br>Income Tax Art 23 (Name) : " + data.IncomeTaxArt23Desc +
                        "</br>Equity Sell Method (Name) : " + data.EquitySellMethodDesc +
                        "</br>Average Priority (Name) : " + data.AveragePriorityDesc +
                        "</br>Fixed Aset Buy PPN (ID) : " + data.FixedAsetBuyPPN +
                        "</br>Fixed Aset Buy PPN (Name) : " + data.FixedAsetBuyPPNDesc +
                        "</br>Fixed Aset Sell PPN (ID) : " + data.FixedAsetSellPPNID +
                        "</br>Fixed Aset Sell PPN (Name) : " + data.FixedAsetSellPPNDesc +
                        "</br>Default Currency : " + data.DefaultCurrencyPK +
                        "</br>Total Equity (ID) : " + data.TotalEquityID +
                        "</br>Total Equity (Name) : " + data.TotalEquityDesc +

                        "</br>ARSubscriptionFee (ID) : " + data.ARSubscriptionFeeID +
                        "</br>ARSubscriptionFee (Name) : " + data.ARSubscriptionFeeDesc +
                        "</br>SubscriptionFeeIncome (ID) : " + data.SubscriptionFeeIncomeID +
                        "</br>SubscriptionFeeIncome (Name) : " + data.SubscriptionFeeIncomeDesc +
                        "</br>ARRedemptionFee (ID) : " + data.ARRedemptionFeeID +
                        "</br>ARRedemptionFee (Name) : " + data.ARRedemptionFeeDesc +
                        "</br>RedemptionFeeIncome (ID) : " + data.RedemptionFeeIncomeID +
                        "</br>RedemptionFeeIncome (Name) : " + data.RedemptionFeeIncomeDesc +
                        "</br>ARSwitchingFee (ID) : " + data.ARSwitchingFeeID +
                        "</br>ARSwitchingFee (Name) : " + data.ARSwitchingFeeDesc +
                        "</br>SwitchingFeeIncome (ID) : " + data.SwitchingFeeIncomeID +
                        "</br>SwitchingFeeIncome (Name) : " + data.SwitchingFeeIncomeDesc +

                        "</br>SwitchingFundAcc (ID) : " + data.SwitchingFundAccID +
                        "</br>SwitchingFundAcc (Name) : " + data.SwitchingFundAccDesc +

                        "</br>AgentCommissionExpense (ID) : " + data.AgentCommissionExpenseID +
                        "</br>AgentCommissionExpense (Name) : " + data.AgentCommissionExpenseDesc +
                        "</br>AgentCommissionPayable (ID) : " + data.AgentCommissionPayableID +
                        "</br>AgentCommissionPayable (Name) : " + data.AgentCommissionPayableDesc +
                        "</br>WHTPayablePPH21 (ID) : " + data.WHTPayablePPH21ID +
                        "</br>WHTPayablePPH21 (Name) : " + data.WHTPayablePPH21Desc +
                        "</br>WHTPayablePPH23 (ID) : " + data.WHTPayablePPH23ID +
                        "</br>WHTPayablePPH23 (Name) : " + data.WHTPayablePPH23Desc +
                        "</br>VatIn (ID) : " + data.VatInID +
                        "</br>VatIn (Name) : " + data.VatInDesc +
                        "</br>VatOut (ID) : " + data.VatOutID +
                        "</br>VatOut (Name) : " + data.VatOutDesc +
                        "</br>AgentCommissionCash (ID) : " + data.AgentCommissionCashID +
                        "</br>AgentCommissionCash (Name) : " + data.AgentCommissionCashDesc +

                        "</br>AgentCSRExpense (ID) : " + data.AgentCSRExpenseID +
                        "</br>AgentCSRExpense (Name) : " + data.AgentCSRExpenseDesc +
                        "</br>AgentCSRPayable (ID) : " + data.AgentCSRPayableID +
                        "</br>AgentCSRPayable (Name) : " + data.AgentCSRPayableDesc +

                        "</br>EntryUserID : " + data.EntryUsersID +
                        "</br>EntryTime : " + data.EntryTime +
                        "</br>UpdateUserID : " + data.UpdateUsersID +
                        "</br>UpdateTime : " + data.UpdateTime);


                    winOldData.center();
                    winOldData.open();

                } else {
                    alert("Pending come from New data, there's no Old data then");
                }
            },
            error: function (data) {
                alertify.alert(data.responseText);
            }
        });
    });

    $("#BtnApproved").click(function () {
        
        alertify.confirm("Are you sure want to Approved data?", function (e) {
            if (e) {
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#AccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "AccountingSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var AccountingSetup = {
                                AccountingSetupPK: $('#AccountingSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/AccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingSetup_A",
                                type: 'POST',
                                data: JSON.stringify(AccountingSetup),
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
        
        alertify.confirm("Are you sure want to Void data?", function (e) {
            if (e) {
                var AccountingSetup = {
                    AccountingSetupPK: $('#AccountingSetupPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/AccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingSetup_V",
                    type: 'POST',
                    data: JSON.stringify(AccountingSetup),
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
    });

    $("#BtnReject").click(function () {
        
        alertify.confirm("Are you sure want to Reject data?", function (e) {
            if (e) {
                var AccountingSetup = {
                    AccountingSetupPK: $('#AccountingSetupPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    VoidUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/AccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "AccountingSetup_R",
                    type: 'POST',
                    data: JSON.stringify(AccountingSetup),
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
    });


});
