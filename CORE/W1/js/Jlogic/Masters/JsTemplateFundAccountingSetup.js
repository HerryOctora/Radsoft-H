$(document).ready(function () {
    document.title = 'FORM FUND ACCOUNTING SETUP';
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

        $("#BtnWinCopyTemplateFundAccountingSetup").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    function initWindow() {

        win = $("#WinTemplateFundAccountingSetup").kendoWindow({
            height: 550,
            title: "Fund Accounting Setup Detail",
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

        WinCopyTemplateFundAccountingSetup = $("#WinCopyTemplateFundAccountingSetup").kendoWindow({
            height: 200,
            title: "* Copy Fund Accounting Setup",
            visible: false,
            width: 600,
            open: function (e) {
                this.wrapper.css({ top: 80 })
            },
            modal: true,
        }).data("kendoWindow");

    }

    var GlobValidator = $("#WinTemplateFundAccountingSetup").kendoValidator().data("kendoValidator");

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
        if (e == null) {
            $("#BtnUpdate").hide();
            $("#BtnVoid").hide();
            $("#BtnApproved").hide();
            $("#BtnReject").hide();
            $("#BtnOldData").hide();
            $("#StatusHeader").val("NEW");
        } else {
            var dataItemX = this.dataItem($(e.currentTarget).closest("tr"));
            if (dataItemX.Status == 1) {

                $("#StatusHeader").val("PENDING");
                $("#BtnVoid").hide();
                $("#BtnAdd").hide();
                $("#BtnUpdate").show();
            }
            if (dataItemX.Status == 2) {
                $("#StatusHeader").val("APPROVED");
                $("#BtnApproved").hide();
                $("#BtnReject").hide();
                $("#BtnAdd").hide();
                $("#BtnOldData").hide();
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

            $("#TemplateFundAccountingSetupPK").val(dataItemX.TemplateFundAccountingSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
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
        //1.combo box AccountPK
        $.ajax({
            url: window.location.origin + "/Radsoft/FundJournalAccount/GetFundJournalAccountComboChildOnly/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#Subscription").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbSubscription()
                });
                $("#PayableSubscriptionFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableSubscriptionFee()
                });

                $("#PayableRedemptionFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableRedemptionFee()
                });


                $("#PayablePurchaseMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurchaseMutualFund()
                });

                $("#PayableOtherFeeOne").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableOtherFeeOne()
                });

                $("#PayableOtherFeeTwo").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableOtherFeeTwo()
                });

                $("#Redemption").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRedemption()
                });


                $("#Switching").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbSwitching()
                });
                $("#PayableSwitchingFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableSwitchingFee()
                });

                $("#PayableSInvestFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableSInvestFee()
                });
                $("#SInvestFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbSInvestFee()
                });
                $("#InvestmentEquity").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentEquity()
                });

                $("#InvestmentBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentBond()
                });

                $("#InvestmentTimeDeposit").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentTimeDeposit()
                });

                $("#InvestmentMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentMutualFund()
                });

                $("#InterestRecBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestRecBond()
                });

                $("#InterestAccrBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccrBond()
                });

                $("#InterestAccrTimeDeposit").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccrTimeDeposit()
                });

                $("#InterestAccrGiro").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccrGiro()
                });

                $("#PrepaidTaxDividend").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPrepaidTaxDividend()
                });

                $("#AccountReceivableSaleBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleBond()
                });

                $("#AccountReceivableSaleEquity").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleEquity()
                });

                $("#AccountReceivableSaleTimeDeposit").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleTimeDeposit()
                });

                $("#AccountReceivableSaleMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleMutualFund()
                });

                $("#IncomeInterestBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeInterestBond()
                });

                $("#IncomeInterestTimeDeposit").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeInterestTimeDeposit()
                });

                $("#IncomeInterestGiro").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeInterestGiro()
                });

                $("#IncomeDividend").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeDividend()
                });

                $("#ARDividend").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbARDividend()
                });

                $("#RevaluationBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationBond()
                });

                $("#RevaluationEquity").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationEquity()
                });

                $("#RevaluationMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationMutualFund()
                });

                $("#PayablePurchaseEquity").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurchaseEquity()
                });

                $("#PayablePurRecBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurRecBond()
                });

                $("#PayableManagementFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableManagementFee()
                });

                $("#PayableCustodianFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableCustodianFee()
                });

                $("#PayableAuditFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableAuditFee()
                });

                $("#PayableMovementFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableMovementFee()
                });

                $("#BrokerCommission").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbBrokerCommission()
                });

                $("#BrokerLevy").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbBrokerLevy()
                });

                $("#BrokerVat").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbBrokerVat()
                });

                $("#BrokerSalesTax").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbBrokerSalesTax()
                });

                $("#WithHoldingTaxPPH23").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbWithHoldingTaxPPH23()
                });

                $("#WHTTaxPayableAccrInterestBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbWHTTaxPayableAccrInterestBond()
                });

                $("#ManagementFeeExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbManagementFeeExpense()
                });

                $("#CustodianFeeExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbCustodianFeeExpense()
                });

                $("#AuditFeeExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAuditFeeExpense()
                });

                $("#MovementFeeExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbMovementFeeExpense()
                });

                $("#OtherFeeOneExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbOtherFeeOneExpense()
                });

                $("#OtherFeeTwoExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbOtherFeeTwoExpense()
                });

                $("#BankCharges").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbBankCharges()
                });

                $("#TaxExpenseInterestIncomeBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbTaxExpenseInterestIncomeBond()
                });

                $("#TaxExpenseInterestIncomeTimeDeposit").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbTaxExpenseInterestIncomeTimeDeposit()
                });

                $("#RealisedEquity").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRealisedEquity()
                });

                $("#RealisedBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRealisedBond()
                });

                $("#RealisedMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRealisedMutualFund()
                });

                $("#UnrealisedBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedBond()
                });

                $("#UnrealisedMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedMutualFund()
                });

                $("#TaxCapitalGainBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbTaxCapitalGainBond()
                });

                $("#UnrealisedEquity").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedEquity()
                });

                $("#DistributedIncomeAcc").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbDistributedIncomeAcc()
                });

                $("#DistributedIncomePayableAcc").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbDistributedIncomePayableAcc()
                });

                $("#PendingSubscription").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPendingSubscription()
                });

                $("#PendingRedemption").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPendingRedemption()
                });


                $("#BondAmortization").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbBondAmortization()
                });

                $("#PendingSwitching").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPendingSwitching()
                });

                $("#CurrentYearAccount").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbCurrentYearAccount()
                });

                $("#PriorYearAccount").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPriorYearAccount()
                });

                $("#TaxPercentageDividend").kendoNumericTextBox({
                    format: "##.#### \\%",
                    decimals: 4,
                    value: setTaxPercentageDividend()
                });
                function setTaxPercentageDividend() {
                    if (e == null) {
                        return 0;
                    } else {
                        return dataItemX.TaxPercentageDividend;
                    }
                }

                $("#TaxPercentageBond").kendoNumericTextBox({
                    format: "##.#### \\%",
                    decimals: 4,
                    value: setTaxPercentageBond()
                });
                function setTaxPercentageBond() {
                    if (e == null) {
                        return 0;
                    } else {
                        return dataItemX.TaxPercentageBond;
                    }
                }

                $("#TaxPercentageCapitalGain").kendoNumericTextBox({
                    format: "##.#### \\%",
                    decimals: 4,
                    value: setTaxPercentageCapitalGain()
                });
                function setTaxPercentageCapitalGain() {
                    if (e == null) {
                        return 0;
                    } else {
                        return dataItemX.TaxPercentageCapitalGain;
                    }
                }
                

                $("#TaxPercentageTD").kendoNumericTextBox({
                    format: "##.#### \\%",
                    decimals: 4,
                    value: setTaxPercentageTD()
                });
                function setTaxPercentageTD() {
                    if (e == null) {
                        return 0;
                    } else {
                        return dataItemX.TaxPercentageTD;
                    }
                }

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }

        });



        function OnChangeFundJournalAccountPK() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }


        function setCmbSubscription() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Subscription == 0) {
                    return "";
                } else {
                    return dataItemX.Subscription;
                }
            }
        }

        function setCmbPayableSubscriptionFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableSubscriptionFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableSubscriptionFee;
                }
            }
        }

        function setCmbRedemption() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Redemption == 0) {
                    return "";
                } else {
                    return dataItemX.Redemption;
                }
            }
        }

        function setCmbPayableRedemptionFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableRedemptionFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableRedemptionFee;
                }
            }
        }

        function setCmbPayableOtherFeeOne() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableOtherFeeOne == 0) {
                    return "";
                } else {
                    return dataItemX.PayableOtherFeeOne;
                }
            }
        }

        function setCmbPayableOtherFeeTwo() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableOtherFeeTwo == 0) {
                    return "";
                } else {
                    return dataItemX.PayableOtherFeeTwo;
                }
            }
        }

        function setCmbSwitching() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.Switching == 0) {
                    return "";
                } else {
                    return dataItemX.Switching;
                }
            }
        }

        function setCmbPayableSwitchingFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableSwitchingFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableSwitchingFee;
                }
            }
        }
        function setCmbPayableSInvestFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableSInvestFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableSInvestFee;
                }
            }
        }

        function setCmbSInvestFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.SInvestFee == 0) {
                    return "";
                } else {
                    return dataItemX.SInvestFee;
                }
            }
        }

        function setCmbInvestmentEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentEquity == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentEquity;
                }
            }
        }

        function setCmbInvestmentBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentBond == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentBond;
                }
            }
        }

        function setCmbInvestmentTimeDeposit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentTimeDeposit == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentTimeDeposit;
                }
            }
        }

        function setCmbInterestRecBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestRecBond == 0) {
                    return "";
                } else {
                    return dataItemX.InterestRecBond;
                }
            }
        }

        function setCmbInterestAccrBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccrBond == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccrBond;
                }
            }
        }

        function setCmbInterestAccrTimeDeposit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccrTimeDeposit == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccrTimeDeposit;
                }
            }
        }


        function setCmbInterestAccrGiro() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccrGiro == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccrGiro;
                }
            }
        }

        function setCmbPrepaidTaxDividend() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PrepaidTaxDividend == 0) {
                    return "";
                } else {
                    return dataItemX.PrepaidTaxDividend;
                }
            }
        }

        function setCmbAccountReceivableSaleBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleBond == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleBond;
                }
            }
        }

        function setCmbAccountReceivableSaleEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleEquity == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleEquity;
                }
            }
        }

        function setCmbAccountReceivableSaleTimeDeposit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleTimeDeposit == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleTimeDeposit;
                }
            }
        }

        function setCmbIncomeInterestBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeInterestBond == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeInterestBond;
                }
            }
        }

        function setCmbIncomeInterestTimeDeposit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeInterestTimeDeposit == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeInterestTimeDeposit;
                }
            }
        }

        function setCmbIncomeInterestGiro() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeInterestGiro == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeInterestGiro;
                }
            }
        }

        function setCmbIncomeDividend() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeDividend == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeDividend;
                }
            }
        }

        function setCmbARDividend() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.ARDividend == 0) {
                    return "";
                } else {
                    return dataItemX.ARDividend;
                }
            }
        }

        function setCmbRevaluationBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationBond == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationBond;
                }
            }
        }

        function setCmbRevaluationEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationEquity == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationEquity;
                }
            }
        }

        function setCmbPayablePurchaseEquity() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurchaseEquity == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurchaseEquity;
                }
            }
        }

        function setCmbPayablePurRecBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurRecBond == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurRecBond;
                }
            }
        }

        function setCmbPayableManagementFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableManagementFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableManagementFee;
                }
            }
        }


        function setCmbPayableCustodianFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableCustodianFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableCustodianFee;
                }
            }
        }

        function setCmbPayableAuditFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableAuditFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableAuditFee;
                }
            }
        }

        function setCmbPayableMovementFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableMovementFee == 0) {
                    return "";
                } else {
                    return dataItemX.PayableMovementFee;
                }
            }
        }

        function setCmbBrokerCommission() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BrokerCommission == 0) {
                    return "";
                } else {
                    return dataItemX.BrokerCommission;
                }
            }
        }

        function setCmbBrokerLevy() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BrokerLevy == 0) {
                    return "";
                } else {
                    return dataItemX.BrokerLevy;
                }
            }
        }

        function setCmbBrokerVat() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BrokerVat == 0) {
                    return "";
                } else {
                    return dataItemX.BrokerVat;
                }
            }
        }

        function setCmbBrokerSalesTax() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BrokerSalesTax == 0) {
                    return "";
                } else {
                    return dataItemX.BrokerSalesTax;
                }
            }
        }

        function setCmbWithHoldingTaxPPH23() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.WithHoldingTaxPPH23 == 0) {
                    return "";
                } else {
                    return dataItemX.WithHoldingTaxPPH23;
                }
            }
        }

        function setCmbWHTTaxPayableAccrInterestBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.WHTTaxPayableAccrInterestBond == 0) {
                    return "";
                } else {
                    return dataItemX.WHTTaxPayableAccrInterestBond;
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


        function setCmbCustodianFeeExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CustodianFeeExpense == 0) {
                    return "";
                } else {
                    return dataItemX.CustodianFeeExpense;
                }
            }
        }

        function setCmbAuditFeeExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AuditFeeExpense == 0) {
                    return "";
                } else {
                    return dataItemX.AuditFeeExpense;
                }
            }
        }

        function setCmbMovementFeeExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.MovementFeeExpense == 0) {
                    return "";
                } else {
                    return dataItemX.MovementFeeExpense;
                }
            }
        }

        function setCmbOtherFeeOneExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OtherFeeOneExpense == 0) {
                    return "";
                } else {
                    return dataItemX.OtherFeeOneExpense;
                }
            }
        }

        function setCmbOtherFeeTwoExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OtherFeeTwoExpense == 0) {
                    return "";
                } else {
                    return dataItemX.OtherFeeTwoExpense;
                }
            }
        }

        function setCmbBankCharges() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BankCharges == 0) {
                    return "";
                } else {
                    return dataItemX.BankCharges;
                }
            }
        }

        function setCmbTaxExpenseInterestIncomeBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxExpenseInterestIncomeBond == 0) {
                    return "";
                } else {
                    return dataItemX.TaxExpenseInterestIncomeBond;
                }
            }
        }

        function setCmbTaxExpenseInterestIncomeTimeDeposit() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxExpenseInterestIncomeTimeDeposit == 0) {
                    return "";
                } else {
                    return dataItemX.TaxExpenseInterestIncomeTimeDeposit;
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

        function setCmbTaxCapitalGainBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxCapitalGainBond == 0) {
                    return "";
                } else {
                    return dataItemX.TaxCapitalGainBond;
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

        function setCmbDistributedIncomeAcc() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DistributedIncomeAcc == 0) {
                    return "";
                } else {
                    return dataItemX.DistributedIncomeAcc;
                }
            }
        }

        function setCmbDistributedIncomePayableAcc() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.DistributedIncomePayableAcc == 0) {
                    return "";
                } else {
                    return dataItemX.DistributedIncomePayableAcc;
                }
            }
        }

        function setCmbPendingSubscription() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PendingSubscription == 0) {
                    return "";
                } else {
                    return dataItemX.PendingSubscription;
                }
            }
        }

        function setCmbPendingRedemption() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PendingRedemption == 0) {
                    return "";
                } else {
                    return dataItemX.PendingRedemption;
                }
            }
        }

        function setCmbBondAmortization() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.BondAmortization == 0) {
                    return "";
                } else {
                    return dataItemX.BondAmortization;
                }
            }
        }

        function setCmbPayablePurchaseMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurchaseMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurchaseMutualFund;
                }
            }
        }

        function setCmbInvestmentMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentMutualFund;
                }
            }
        }

        function setCmbAccountReceivableSaleMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleMutualFund;
                }
            }
        }

        function setCmbRevaluationMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationMutualFund;
                }
            }
        }

        function setCmbUnrealisedMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedMutualFund;
                }
            }
        }

        function setCmbRealisedMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedMutualFund;
                }
            }
        }

        function setCmbPendingSwitching() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PendingSwitching == 0) {
                    return "";
                } else {
                    return dataItemX.PendingSwitching;
                }
            }
        }

        function setCmbCurrentYearAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CurrentYearAccount == 0) {
                    return "";
                } else {
                    return dataItemX.CurrentYearAccount;
                }
            }
        }

        function setCmbPriorYearAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PriorYearAccount == 0) {
                    return "";
                } else {
                    return dataItemX.PriorYearAccount;
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
        $("#TemplateFundAccountingSetupPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#Subscription").val("");
        $("#PayableSubscriptionFee").val("");
        $("#Redemption").val("");
        $("#PayableRedemptionFee").val("");
        $("#Switching").val("");
        $("#PayableSwitchingFee").val("");
        $("#PayableSInvestFee").val("");
        $("#InvestmentEquity").val("");
        $("#InvestmentBond").val("");
        $("#InvestmentTimeDeposit").val("");
        $("#InterestRecBond").val("");
        $("#InterestAccrBond").val("");
        $("#InterestAccrTimeDeposit").val("");
        $("#InterestAccrGiro").val("");
        $("#PrepaidTaxDividend").val("");
        $("#AccountReceivableSaleBond").val("");
        $("#AccountReceivableSaleEquity").val("");
        $("#AccountReceivableSaleTimeDeposit").val("");
        $("#IncomeInterestBond").val("");
        $("#IncomeInterestTimeDeposit").val("");
        $("#IncomeInterestGiro").val("");
        $("#IncomeDividend").val("");
        $("#ARDividend").val("");
        $("#RevaluationBond").val("");
        $("#RevaluationEquity").val("");
        $("#PayablePurchaseEquity").val("");
        $("#PayablePurRecBond").val("");
        $("#PayableManagementFee").val("");
        $("#PayableMovementFee").val("");

        $("#PayableCustodianFee").val("");
        $("#PayableAuditFee").val("");
        $("#BrokerCommission").val("");
        $("#BrokerLevy").val("");
        $("#BrokerVat").val("");
        $("#BrokerSalesTax").val("");
        $("#WithHoldingTaxPPH23").val("");
        $("#WHTTaxPayableAccrInterestBond").val("");
        $("#ManagementFeeExpense").val("");
        $("#CustodianFeeExpense").val("");
        $("#AuditFeeExpense").val("");
        $("#MovementFeeExpense").val("");
        $("#BankCharges").val("");
        $("#TaxExpenseInterestIncomeBond").val("");
        $("#TaxExpenseInterestIncomeTimeDeposit").val("");

        $("#RealisedEquity").val("");
        $("#RealisedBond").val("");
        $("#UnrealisedBond").val("");
        $("#UnrealisedEquity").val("");
        $("#DistributedIncomeAcc").val("");
        $("#DistributedIncomePayableAcc").val("");
        $("#PendingSubscription").val("");
        $("#PendingRedemption").val("");
        $("#TaxPercentageDividend").val("");
        $("#TaxPercentageBond").val("");
        $("#TaxPercentageTD").val("");
        $("#TaxCapitalGainBond").val("");
        $("#TaxPercentageCapitalGain").val("");

        $("#BondAmortization").val("");
        $("#PayablePurchaseMutualFund").val("");
        $("#InvestmentMutualFund").val("");
        $("#AccountReceivableSaleMutualFund").val("");
        $("#RevaluationMutualFund").val("");
        $("#UnrealisedMutualFund").val("");
        $("#RealisedMutualFund").val("");

        $("#PayableOtherFeeOne").val("");
        $("#PayableOtherFeeTwo").val("");
        $("#OtherFeeOneExpense").val("");
        $("#OtherFeeTwoExpense").val("");

        $("#PendingSwitching").val("");
        $("#CurrentYearAccount").val("");
        $("#PriorYearAccount").val("");
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
                             TemplateFundAccountingSetupPK: { type: "number" },
                             HistoryPK: { type: "number" },
                             Status: { type: "number" },
                             StatusDesc: { type: "string" },
                             Notes: { type: "string" },
                             Subscription: { type: "number" },
                             SubscriptionID: { type: "string" },
                             SubscriptionDesc: { type: "string" },
                             PayableSubscriptionFee: { type: "number" },
                             PayableSubscriptionFeeID: { type: "string" },
                             PayableSubscriptionFeeDesc: { type: "string" },
                             Redemption: { type: "number" },
                             RedemptionID: { type: "string" },
                             RedemptionDesc: { type: "string" },
                             PayableRedemptionFee: { type: "number" },
                             PayableRedemptionFeeID: { type: "string" },
                             PayableRedemptionFeeDesc: { type: "string" },
                             Switching: { type: "number" },
                             SwitchingID: { type: "string" },
                             SwitchingDesc: { type: "string" },
                             PayableSwitchingFee: { type: "number" },
                             PayableSwitchingFeeID: { type: "string" },
                             PayableSwitchingFeeDesc: { type: "string" },
                             PayableSInvestFee: { type: "number" },
                             PayableSInvestFeeID: { type: "string" },
                             PayableSInvestFeeDesc: { type: "string" },
                             SInvestFee: { type: "number" },
                             SInvestFeeID: { type: "string" },
                             SInvestFeeDesc: { type: "string" },
                             InvestmentEquity: { type: "number" },
                             InvestmentEquityID: { type: "string" },
                             InvestmentEquityDesc: { type: "string" },
                             InvestmentBond: { type: "number" },
                             InvestmentBondID: { type: "string" },
                             InvestmentBondDesc: { type: "string" },
                             InvestmentTimeDeposit: { type: "number" },
                             InvestmentTimeDepositID: { type: "string" },
                             InvestmentTimeDepositDesc: { type: "string" },
                             InterestRecBond: { type: "number" },
                             InterestRecBondID: { type: "string" },
                             InterestRecBondDesc: { type: "string" },
                             InterestAccrBond: { type: "number" },
                             InterestAccrBondID: { type: "string" },
                             InterestAccrBondDesc: { type: "string" },
                             InterestAccrTimeDeposit: { type: "number" },
                             InterestAccrTimeDepositID: { type: "string" },
                             InterestAccrTimeDepositDesc: { type: "string" },
                             InterestAccrGiro: { type: "number" },
                             InterestAccrGiroID: { type: "string" },
                             InterestAccrGiroDesc: { type: "string" },
                             PrepaidTaxDividend: { type: "number" },
                             PrepaidTaxDividendID: { type: "string" },
                             PrepaidTaxDividendDesc: { type: "string" },
                             AccountReceivableSaleBond: { type: "number" },
                             AccountReceivableSaleBondID: { type: "string" },
                             AccountReceivableSaleBondDesc: { type: "string" },
                             AccountReceivableSaleEquity: { type: "number" },
                             AccountReceivableSaleEquityID: { type: "string" },
                             AccountReceivableSaleEquityDesc: { type: "string" },
                             AccountReceivableSaleTimeDeposit: { type: "number" },
                             AccountReceivableSaleTimeDepositID: { type: "string" },
                             AccountReceivableSaleTimeDepositDesc: { type: "string" },
                             IncomeInterestBond: { type: "number" },
                             IncomeInterestBondID: { type: "string" },
                             IncomeInterestBondDesc: { type: "string" },
                             IncomeInterestTimeDeposit: { type: "number" },
                             IncomeInterestTimeDepositID: { type: "string" },
                             IncomeInterestTimeDepositDesc: { type: "string" },
                             IncomeInterestGiro: { type: "number" },
                             IncomeInterestGiroID: { type: "string" },
                             IncomeInterestGiroDesc: { type: "string" },
                             IncomeDividend: { type: "number" },
                             IncomeDividendID: { type: "string" },
                             IncomeDividendDesc: { type: "string" },
                             ARDividend: { type: "number" },
                             ARDividendID: { type: "string" },
                             ARDividendDesc: { type: "string" },
                             RevaluationBond: { type: "number" },
                             RevaluationBondID: { type: "string" },
                             RevaluationBondDesc: { type: "string" },
                             RevaluationEquity: { type: "number" },
                             RevaluationEquityID: { type: "string" },
                             RevaluationEquityDesc: { type: "string" },
                             PayablePurchaseEquity: { type: "number" },
                             PayablePurchaseEquityID: { type: "string" },
                             PayablePurchaseEquityDesc: { type: "string" },
                             PayablePurRecBond: { type: "number" },
                             PayablePurRecBondID: { type: "string" },
                             PayablePurRecBondDesc: { type: "string" },
                             PayableManagementFee: { type: "number" },
                             PayableManagementFeeID: { type: "string" },
                             PayableManagementFeeDesc: { type: "string" },
                             PayableCustodianFee: { type: "number" },
                             PayableCustodianFeeID: { type: "string" },
                             PayableCustodianFeeDesc: { type: "string" },
                             PayableAuditFee: { type: "number" },
                             PayableAuditFeeID: { type: "string" },
                             PayableAuditFeeDesc: { type: "string" },
                             PayableMovementFee: { type: "number" },
                             PayableMovementFeeID: { type: "string" },
                             PayableMovementFeeDesc: { type: "string" },
                             BrokerCommission: { type: "number" },
                             BrokerCommissionID: { type: "string" },
                             BrokerCommissionDesc: { type: "string" },
                             BrokerLevy: { type: "number" },
                             BrokerLevyID: { type: "string" },
                             BrokerLevyDesc: { type: "string" },
                             TaxCapitalGainBond: { type: "number" },
                             TaxCapitalGainBondID: { type: "string" },
                             TaxCapitalGainBondDesc: { type: "string" },

                             BrokerVat: { type: "number" },
                             BrokerVatID: { type: "string" },
                             BrokerVatDesc: { type: "string" },
                             BrokerSalesTax: { type: "number" },
                             BrokerSalesTaxID: { type: "string" },
                             BrokerSalesTaxDesc: { type: "string" },
                             WithHoldingTaxPPH23: { type: "number" },
                             WithHoldingTaxPPH23ID: { type: "string" },
                             WithHoldingTaxPPH23Desc: { type: "string" },
                             WHTTaxPayableAccrInterestBond: { type: "number" },
                             WHTTaxPayableAccrInterestBondID: { type: "string" },
                             WHTTaxPayableAccrInterestBondDesc: { type: "string" },
                             ManagementFeeExpense: { type: "number" },
                             ManagementFeeExpenseID: { type: "string" },
                             ManagementFeeExpenseDesc: { type: "string" },
                             CustodianFeeExpense: { type: "number" },
                             CustodianFeeExpenseID: { type: "string" },
                             CustodianFeeExpenseDesc: { type: "string" },
                             AuditFeeExpense: { type: "number" },
                             AuditFeeExpenseID: { type: "string" },
                             AuditFeeExpenseDesc: { type: "string" },
                             MovementFeeExpense: { type: "number" },
                             MovementFeeExpenseID: { type: "string" },
                             MovementFeeExpenseDesc: { type: "string" },
                             BankCharges: { type: "number" },
                             BankChargesID: { type: "string" },
                             BankChargesDesc: { type: "string" },

                             TaxExpenseInterestIncomeBond: { type: "number" },
                             TaxExpenseInterestIncomeBondID: { type: "string" },
                             TaxExpenseInterestIncomeBondDesc: { type: "string" },
                             TaxExpenseInterestIncomeTimeDeposit: { type: "number" },
                             TaxExpenseInterestIncomeTimeDepositID: { type: "string" },
                             TaxExpenseInterestIncomeTimeDepositDesc: { type: "string" },
                             RealisedEquity: { type: "number" },
                             RealisedEquityID: { type: "string" },
                             RealisedEquityDesc: { type: "string" },
                             RealisedBond: { type: "number" },
                             RealisedBondID: { type: "string" },
                             RealisedBondDesc: { type: "string" },
                             UnrealisedBond: { type: "number" },
                             UnrealisedBondID: { type: "string" },
                             UnrealisedBondDesc: { type: "string" },

                             UnrealisedEquity: { type: "number" },
                             UnrealisedEquityID: { type: "string" },
                             UnrealisedEquityDesc: { type: "string" },
                             DistributedIncomeAcc: { type: "number" },
                             DistributedIncomeAccID: { type: "string" },
                             DistributedIncomeAccDesc: { type: "string" },
                             DistributedIncomePayableAcc: { type: "number" },
                             DistributedIncomePayableAccID: { type: "string" },
                             DistributedIncomePayableAccDesc: { type: "string" },
                             PendingSubscription: { type: "number" },
                             PendingSubscriptionID: { type: "string" },
                             PendingSubscriptionDesc: { type: "string" },
                             PendingRedemption: { type: "number" },
                             PendingRedemptionID: { type: "string" },
                             PendingRedemptionDesc: { type: "string" },

                             BondAmortization: { type: "number" },
                             BondAmortizationID: { type: "string" },
                             BondAmortizationDesc: { type: "string" },

                             PayablePurchaseMutualFund: { type: "number" },
                             PayablePurchaseMutualFundID: { type: "string" },
                             PayablePurchaseMutualFundDesc: { type: "string" },

                             InvestmentMutualFund: { type: "number" },
                             InvestmentMutualFundID: { type: "string" },
                             InvestmentMutualFundDesc: { type: "string" },

                             AccountReceivableSaleMutualFund: { type: "number" },
                             AccountReceivableSaleMutualFundID: { type: "string" },
                             AccountReceivableSaleMutualFundDesc: { type: "string" },

                             RevaluationMutualFund: { type: "number" },
                             RevaluationMutualFundID: { type: "string" },
                             RevaluationMutualFundDesc: { type: "string" },

                             UnrealisedMutualFund: { type: "number" },
                             UnrealisedMutualFundID: { type: "string" },
                             UnrealisedMutualFundDesc: { type: "string" },

                             RealisedMutualFund: { type: "number" },
                             RealisedMutualFundID: { type: "string" },
                             RealisedMutualFundDesc: { type: "string" },

                             PayableOtherFeeOne: { type: "number" },
                             PayableOtherFeeOneID: { type: "string" },
                             PayableOtherFeeOneDesc: { type: "string" },

                             PayableOtherFeeTwo: { type: "number" },
                             PayableOtherFeeTwoID: { type: "string" },
                             PayableOtherFeeTwoDesc: { type: "string" },

                             OtherFeeOneExpense: { type: "number" },
                             OtherFeeOneExpenseID: { type: "string" },
                             OtherFeeOneExpenseDesc: { type: "string" },

                             OtherFeeTwoExpense: { type: "number" },
                             OtherFeeTwoExpenseID: { type: "string" },
                             OtherFeeTwoExpenseDesc: { type: "string" },

                             PendingSwitching: { type: "number" },
                             PendingSwitchingID: { type: "string" },
                             PendingSwitchingDesc: { type: "string" },

                             CurrentYearAccount: { type: "number" },
                             CurrentYearAccountID: { type: "string" },
                             CurrentYearAccountDesc: { type: "string" },

                             PriorYearAccount: { type: "number" },
                             PriorYearAccountID: { type: "string" },
                             PriorYearAccountDesc: { type: "string" },

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
            var gridApproved = $("#gridTemplateFundAccountingSetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridTemplateFundAccountingSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridTemplateFundAccountingSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var TemplateFundAccountingSetupApprovedURL = window.location.origin + "/Radsoft/TemplateFundAccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
          dataSourceApproved = getDataSource(TemplateFundAccountingSetupApprovedURL);

        $("#gridTemplateFundAccountingSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Template Fund Accounting Setup"
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
                { field: "TemplateFundAccountingSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                {
                    field: "TaxPercentageDividend", title: "Tax Percentage Dividend", width: 200,
                    template: "#: TaxPercentageDividend  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "TaxPercentageBond", title: "Tax Percentage Bond", width: 200,
                    template: "#: TaxPercentageBond  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "TaxPercentageTD", title: "Tax Percentage TD", width: 200,
                    template: "#: TaxPercentageTD  # %",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "TaxPercentageCapitalGain", title: "Tax Percentage Capital Gain", width: 200,
                    template: "#: TaxPercentageCapitalGain  # %",
                    attributes: { style: "text-align:right;" }
                }, 
                { field: "SubscriptionID", title: "Subscription (ID)", width: 300 },
                { field: "SubscriptionDesc", title: "Subscription (Name)", width: 400 },
                { field: "PayableSubscriptionFeeID", title: "PayableSubscriptionFee (ID)", width: 300 },
                { field: "PayableSubscriptionFeeDesc", title: "PayableSubscriptionFee (Name)", width: 400 },
                { field: "RedemptionID", title: "Redemption (ID)", width: 300 },
                { field: "RedemptionDesc", title: "Redemption (Name)", width: 400 },
                { field: "PayableRedemptionFeeID", title: "PayableRedemptionFee (ID)", width: 300 },
                { field: "PayableRedemptionFeeDesc", title: "PayableRedemptionFee (Name)", width: 400 },
                { field: "PayableSwitchingFeeID", title: "PayableSwitchingFee (ID)", width: 300 },
                { field: "PayableSwitchingFeeDesc", title: "PayableSwitchingFee (Name)", width: 400 },
                { field: "PayableSInvestFeeID", title: "PayableSInvestFee (ID)", width: 300 },
                { field: "PayableSInvestFeeDesc", title: "PayableSInvestFee (Name)", width: 400 },
                { field: "InvestmentEquityID", title: "InvestmentEquity (ID)", width: 300 },
                { field: "InvestmentEquityDesc", title: "InvestmentEquity (Name)", width: 400 },
                { field: "InvestmentBondID", title: "InvestmentBond (ID)", width: 300 },
                { field: "InvestmentBondDesc", title: "InvestmentBond (Name)", width: 400 },
                { field: "InvestmentTimeDepositID", title: "InvestmentTimeDeposit (ID)", width: 300 },
                { field: "InvestmentTimeDepositDesc", title: "InvestmentTimeDeposit (Name)", width: 400 },
                { field: "InterestRecBondID", title: "InterestRecBond (ID)", width: 300 },
                { field: "InterestRecBondDesc", title: "InterestRecBond (Name)", width: 400 },
                { field: "InterestAccrBondID", title: "InterestAccrBond (ID)", width: 300 },
                { field: "InterestAccrBondDesc", title: "InterestAccrBond (Name)", width: 400 },
                { field: "TaxCapitalGainBondID", title: "TaxCapitalGainBond (ID)", width: 300 },
                { field: "TaxCapitalGainBondDesc", title: "TaxCapitalGainBond (Name)", width: 400 },

                { field: "InterestAccrTimeDepositID", title: "InterestAccrTimeDeposit (ID)", width: 300 },
                { field: "InterestAccrTimeDepositDesc", title: "InterestAccrTimeDeposit (Name)", width: 400 },
                { field: "InterestAccrGiroID", title: "InterestAccrGiro (ID)", width: 300 },
                { field: "InterestAccrGiroDesc", title: "InterestAccrGiro (Name)", width: 400 },
                { field: "PrepaidTaxDividendID", title: "PrepaidTaxDividend (ID)", width: 300 },
                { field: "PrepaidTaxDividendDesc", title: "PrepaidTaxDividend (Name)", width: 400 },
                { field: "AccountReceivableSaleBondID", title: "AccountReceivableSaleBond (ID)", width: 300 },
                { field: "AccountReceivableSaleBondDesc", title: "AccountReceivableSaleBond (Name)", width: 400 },
                { field: "AccountReceivableSaleEquityID", title: "AccountReceivableSaleEquity (ID)", width: 300 },
                { field: "AccountReceivableSaleEquityDesc", title: "AccountReceivableSaleEquity (Name)", width: 400 },
                { field: "AccountReceivableSaleTimeDepositID", title: "AccountReceivableSaleTimeDeposit (ID)", width: 300 },
                { field: "AccountReceivableSaleTimeDepositDesc", title: "AccountReceivableSaleTimeDeposit (Name)", width: 400 },
                { field: "IncomeInterestBondID", title: "IncomeInterestBond (ID)", width: 300 },
                { field: "IncomeInterestBondDesc", title: "IncomeInterestBond (Name)", width: 400 },
                { field: "IncomeInterestTimeDepositID", title: "IncomeInterestTimeDeposit (ID)", width: 300 },
                { field: "IncomeInterestTimeDepositDesc", title: "IncomeInterestTimeDeposit (Name)", width: 400 },
                { field: "PayableSubscriptionFeeID", title: "Payable Subscription Fee (ID)", width: 300 },
                { field: "PayableSubscriptionFeeDesc", title: "Payable Subscription Fee (Name)", width: 400 },
                { field: "IncomeInterestGiroID", title: "IncomeInterestGiro (ID)", width: 300 },
                { field: "IncomeInterestGiroDesc", title: "IncomeInterestGiro (Name)", width: 400 },
                { field: "IncomeDividendID", title: "IncomeDividend (ID)", width: 300 },
                { field: "IncomeDividendDesc", title: "IncomeDividend (Name)", width: 400 },
                { field: "ARDividendID", title: "ARDividend (ID)", width: 300 },
                { field: "ARDividendDesc", title: "ARDividend (Name)", width: 400 },

                { field: "RevaluationBondID", title: "RevaluationBond (ID)", width: 300 },
                { field: "RevaluationBondDesc", title: "RevaluationBond (Name)", width: 400 },
                { field: "RevaluationEquityID", title: "RevaluationEquity (ID)", width: 300 },
                { field: "RevaluationEquityDesc", title: "RevaluationEquity (Name)", width: 400 },
                { field: "PayablePurchaseEquityID", title: "PayablePurchaseEquity (ID)", width: 300 },
                { field: "PayablePurchaseEquityDesc", title: "PayablePurchaseEquity (Name)", width: 400 },
                { field: "PayablePurRecBondID", title: "PayablePurRecBond (ID)", width: 300 },
                { field: "PayablePurRecBondDesc", title: "PayablePurRecBond (Name)", width: 400 },
                { field: "PayableManagementFeeID", title: "PayableManagementFee (ID)", width: 300 },
                { field: "PayableManagementFeeDesc", title: "PayableManagementFee (Name)", width: 400 },
                { field: "PayableCustodianFeeID", title: "PayableCustodianFee (ID)", width: 300 },
                { field: "PayableCustodianFeeDesc", title: "PayableCustodianFee (Name)", width: 400 },
                { field: "PayableAuditFeeID", title: "PayableAuditFee (ID)", width: 300 },
                { field: "PayableAuditFeeDesc", title: "PayableAuditFee (Name)", width: 400 },
                { field: "PayableMovementFeeID", title: "PayableMovementFee (ID)", width: 300 },
                { field: "PayableMovementFeeDesc", title: "PayableMovementFee (Name)", width: 400 },
                { field: "BrokerCommissionID", title: "BrokerCommission (ID)", width: 300 },
                { field: "BrokerCommissionDesc", title: "BrokerCommission (Name)", width: 400 },
                { field: "BrokerLevyID", title: "BrokerLevy (ID)", width: 300 },
                { field: "BrokerLevyDesc", title: "BrokerLevy (Name)", width: 400 },
                { field: "BrokerVatID", title: "BrokerVat (ID)", width: 300 },
                { field: "BrokerVatDesc", title: "BrokerVat (Name)", width: 400 },
                { field: "BrokerSalesTaxID", title: "BrokerSalesTax (ID)", width: 300 },
                { field: "BrokerSalesTaxDesc", title: "BrokerSalesTax (Name)", width: 400 },
                { field: "WithHoldingTaxPPH23ID", title: "WithHoldingTaxPPH23 (ID)", width: 300 },
                { field: "WithHoldingTaxPPH23Desc", title: "WithHoldingTaxPPH23 (Name)", width: 400 },
                { field: "WHTTaxPayableAccrInterestBondID", title: "WHTTaxPayableAccrInterestBond (ID)", width: 300 },
                { field: "WHTTaxPayableAccrInterestBondDesc", title: "WHTTaxPayableAccrInterestBond (Name)", width: 400 },

                { field: "ManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 300 },
                { field: "ManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 400 },
                { field: "CustodianFeeExpenseID", title: "CustodianFeeExpense (ID)", width: 300 },
                { field: "CustodianFeeExpenseDesc", title: "CustodianFeeExpense (Name)", width: 400 },
                { field: "AuditFeeExpenseID", title: "AuditFeeExpense (ID)", width: 300 },
                { field: "AuditFeeExpenseDesc", title: "AuditFeeExpense (Name)", width: 400 },
                { field: "MovementFeeExpenseID", title: "MovementFeeExpense (ID)", width: 300 },
                { field: "MovementFeeExpenseDesc", title: "MovementFeeExpense (Name)", width: 400 },
                { field: "BankChargesID", title: "BankCharges (ID)", width: 300 },
                { field: "BankChargesDesc", title: "BankCharges (Name)", width: 400 },
                { field: "TaxExpenseInterestIncomeBondID", title: "TaxExpenseInterestIncomeBond (ID)", width: 300 },
                { field: "TaxExpenseInterestIncomeBondDesc", title: "TaxExpenseInterestIncomeBond (Name)", width: 400 },
                { field: "TaxExpenseInterestIncomeTimeDepositID", title: "TaxExpenseInterestIncomeTimeDeposit (ID)", width: 300 },
                { field: "TaxExpenseInterestIncomeTimeDepositDesc", title: "TaxExpenseInterestIncomeTimeDeposit (Name)", width: 400 },
                { field: "RealisedEquityID", title: "RealisedEquity (ID)", width: 300 },
                { field: "RealisedEquityDesc", title: "RealisedEquity (Name)", width: 400 },
                { field: "RealisedBondID", title: "RealisedBond (ID)", width: 300 },
                { field: "RealisedBondDesc", title: "RealisedBond (Name)", width: 400 },
                { field: "UnrealisedBondID", title: "UnrealisedBond (ID)", width: 300 },
                { field: "UnrealisedBondDesc", title: "UnrealisedBond (Name)", width: 400 },
                { field: "UnrealisedEquityID", title: "UnrealisedEquity (ID)", width: 300 },
                { field: "UnrealisedEquityDesc", title: "UnrealisedEquity (Name)", width: 400 },
                { field: "DistributedIncomeAccID", title: "DistributedIncomeAcc (ID)", width: 300 },
                { field: "DistributedIncomeAccDesc", title: "DistributedIncomeAcc (Name)", width: 400 },
                { field: "DistributedIncomePayableAccID", title: "DistributedIncomePayableAcc (ID)", width: 300 },
                { field: "DistributedIncomePayableAccDesc", title: "DistributedIncomePayableAcc (Name)", width: 400 },
                { field: "PendingSubscriptionID", title: "PendingSubscription (ID)", width: 300 },
                { field: "PendingSubscriptionDesc", title: "PendingSubscription (Name)", width: 400 },
                { field: "PendingRedemptionID", title: "PendingRedemption (ID)", width: 300 },
                { field: "PendingRedemptionDesc", title: "PendingRedemption (Name)", width: 400 },
                { field: "PendingSwitchingID", title: "PendingSwitchingID (ID)", width: 300 },
                { field: "PendingSwitchingIDDesc", title: "PendingSwitchingID (Name)", width: 400 },
                { field: "CurrentYearAccountID", title: "CurrentYearAccount (ID)", width: 300 },
                { field: "CurrentYearAccountDesc", title: "CurrentYearAccount (Name)", width: 400 },
                { field: "PriorYearAccountID", title: "PriorYearAccount (ID)", width: 300 },
                { field: "PriorYearAccountDesc", title: "PriorYearAccount (Name)", width: 400 },

                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "SwitchingID", title: "Switching (ID)", width: 300 },
                { field: "SwitchingDesc", title: "Switching (Name)", width: 400 },
            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabTemplateFundAccountingSetup").kendoTabStrip({
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
                        var TemplateFundAccountingSetupPendingURL = window.location.origin + "/Radsoft/TemplateFundAccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                        dataSourcePending = getDataSource(TemplateFundAccountingSetupPendingURL);
                        $("#gridTemplateFundAccountingSetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Template Fund Accounting Setup"
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
                                { field: "TemplateFundAccountingSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                {
                                    field: "TaxPercentageDividend", title: "Tax Percentage Dividend", width: 200,
                                    template: "#: TaxPercentageDividend  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TaxPercentageBond", title: "Tax Percentage Bond", width: 200,
                                    template: "#: TaxPercentageBond  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TaxPercentageTD", title: "Tax Percentage TD", width: 200,
                                    template: "#: TaxPercentageTD  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TaxPercentageCapitalGain", title: "Tax Percentage Capital Gain", width: 200,
                                    template: "#: TaxPercentageCapitalGain  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "SubscriptionID", title: "Subscription (ID)", width: 300 },
                                { field: "SubscriptionDesc", title: "Subscription (Name)", width: 400 },
                                { field: "PayableSubscriptionFeeID", title: "Payable Subscription Fee (ID)", width: 300 },
                                { field: "PayableSubscriptionFeeDesc", title: "Payable Subscription Fee (Name)", width: 400 },
                                { field: "RedemptionID", title: "Redemption (ID)", width: 300 },
                                { field: "RedemptionDesc", title: "Redemption (Name)", width: 400 },
                                { field: "PayableRedemptionFeeID", title: "Payable Redemption Fee (ID)", width: 300 },
                                { field: "PayableRedemptionFeeDesc", title: "Payable Redemption Fee (Name)", width: 400 },
                                { field: "PayableSwitchingFeeID", title: "PayableSwitchingFee (ID)", width: 300 },
                                { field: "PayableSwitchingFeeDesc", title: "PayableSwitchingFee (Name)", width: 400 },
                                { field: "PayableSInvestFeeID", title: "PayableSInvestFee (ID)", width: 300 },
                                { field: "PayableSInvestFeeDesc", title: "PayableSInvestFee (Name)", width: 400 },
                                { field: "InvestmentEquityID", title: "InvestmentEquity (ID)", width: 300 },
                                { field: "InvestmentEquityDesc", title: "InvestmentEquity (Name)", width: 400 },
                                { field: "InvestmentBondID", title: "InvestmentBond (ID)", width: 300 },
                                { field: "InvestmentBondDesc", title: "InvestmentBond (Name)", width: 400 },
                                { field: "InvestmentTimeDepositID", title: "InvestmentTimeDeposit (ID)", width: 300 },
                                { field: "InvestmentTimeDepositDesc", title: "InvestmentTimeDeposit (Name)", width: 400 },
                                { field: "InterestRecBondID", title: "InterestRecBond (ID)", width: 300 },
                                { field: "InterestRecBondDesc", title: "InterestRecBond (Name)", width: 400 },
                                { field: "InterestAccrBondID", title: "InterestAccrBond (ID)", width: 300 },
                                { field: "InterestAccrBondDesc", title: "InterestAccrBond (Name)", width: 400 },

                                { field: "InterestAccrTimeDepositID", title: "InterestAccrTimeDeposit (ID)", width: 300 },
                                { field: "InterestAccrTimeDepositDesc", title: "InterestAccrTimeDeposit (Name)", width: 400 },
                                { field: "InterestAccrGiroID", title: "InterestAccrGiro (ID)", width: 300 },
                                { field: "InterestAccrGiroDesc", title: "InterestAccrGiro (Name)", width: 400 },
                                { field: "PrepaidTaxDividendID", title: "PrepaidTaxDividend (ID)", width: 300 },
                                { field: "PrepaidTaxDividendDesc", title: "PrepaidTaxDividend (Name)", width: 400 },
                                { field: "AccountReceivableSaleBondID", title: "AccountReceivableSaleBond (ID)", width: 300 },
                                { field: "AccountReceivableSaleBondDesc", title: "AccountReceivableSaleBond (Name)", width: 400 },
                                { field: "AccountReceivableSaleEquityID", title: "AccountReceivableSaleEquity (ID)", width: 300 },
                                { field: "AccountReceivableSaleEquityDesc", title: "AccountReceivableSaleEquity (Name)", width: 400 },
                                { field: "AccountReceivableSaleTimeDepositID", title: "AccountReceivableSaleTimeDeposit (ID)", width: 300 },
                                { field: "AccountReceivableSaleTimeDepositDesc", title: "AccountReceivableSaleTimeDeposit (Name)", width: 400 },
                                { field: "IncomeInterestBondID", title: "IncomeInterestBond (ID)", width: 300 },
                                { field: "IncomeInterestBondDesc", title: "IncomeInterestBond (Name)", width: 400 },
                                { field: "IncomeInterestTimeDepositID", title: "IncomeInterestTimeDeposit (ID)", width: 300 },
                                { field: "IncomeInterestTimeDepositDesc", title: "IncomeInterestTimeDeposit (Name)", width: 400 },
                                { field: "PayableSubscriptionFeeID", title: "Payable Subscription Fee (ID)", width: 300 },
                                { field: "PayableSubscriptionFeeDesc", title: "Payable Subscription Fee (Name)", width: 400 },
                                { field: "IncomeInterestGiroID", title: "IncomeInterestGiro (ID)", width: 300 },
                                { field: "IncomeInterestGiroDesc", title: "IncomeInterestGiro (Name)", width: 400 },
                                { field: "IncomeDividendID", title: "IncomeDividend (ID)", width: 300 },
                                { field: "IncomeDividendDesc", title: "IncomeDividend (Name)", width: 400 },
                                { field: "ARDividendID", title: "ARDividend (ID)", width: 300 },
                                { field: "ARDividendDesc", title: "ARDividend (Name)", width: 400 },
                                { field: "TaxCapitalGainBondID", title: "TaxCapitalGainBond (ID)", width: 300 },
                                { field: "TaxCapitalGainBondDesc", title: "TaxCapitalGainBond (Name)", width: 400 },

                                { field: "RevaluationBondID", title: "RevaluationBond (ID)", width: 300 },
                                { field: "RevaluationBondDesc", title: "RevaluationBond (Name)", width: 400 },
                                { field: "RevaluationEquityID", title: "RevaluationEquity (ID)", width: 300 },
                                { field: "RevaluationEquityDesc", title: "RevaluationEquity (Name)", width: 400 },
                                { field: "PayablePurchaseEquityID", title: "PayablePurchaseEquity (ID)", width: 300 },
                                { field: "PayablePurchaseEquityDesc", title: "PayablePurchaseEquity (Name)", width: 400 },
                                { field: "PayablePurRecBondID", title: "PayablePurRecBond (ID)", width: 300 },
                                { field: "PayablePurRecBondDesc", title: "PayablePurRecBond (Name)", width: 400 },
                                { field: "PayableManagementFeeID", title: "PayableManagementFee (ID)", width: 300 },
                                { field: "PayableManagementFeeDesc", title: "PayableManagementFee (Name)", width: 400 },
                                { field: "PayableCustodianFeeID", title: "PayableCustodianFee (ID)", width: 300 },
                                { field: "PayableCustodianFeeDesc", title: "PayableCustodianFee (Name)", width: 400 },
                                { field: "PayableAuditFeeID", title: "PayableAuditFee (ID)", width: 300 },
                                { field: "PayableAuditFeeDesc", title: "PayableAuditFee (Name)", width: 400 },
                                { field: "PayableMovementFeeID", title: "PayableMovementFee (ID)", width: 300 },
                                { field: "PayableMovementFeeDesc", title: "PayableMovementFee (Name)", width: 400 },
                                { field: "BrokerCommissionID", title: "BrokerCommission (ID)", width: 300 },
                                { field: "BrokerCommissionDesc", title: "BrokerCommission (Name)", width: 400 },
                                { field: "BrokerLevyID", title: "BrokerLevy (ID)", width: 300 },
                                { field: "BrokerLevyDesc", title: "BrokerLevy (Name)", width: 400 },
                                { field: "BrokerVatID", title: "BrokerVat (ID)", width: 300 },
                                { field: "BrokerVatDesc", title: "BrokerVat (Name)", width: 400 },
                                { field: "BrokerSalesTaxID", title: "BrokerSalesTax (ID)", width: 300 },
                                { field: "BrokerSalesTaxDesc", title: "BrokerSalesTax (Name)", width: 400 },
                                { field: "WithHoldingTaxPPH23ID", title: "WithHoldingTaxPPH23 (ID)", width: 300 },
                                { field: "WithHoldingTaxPPH23Desc", title: "WithHoldingTaxPPH23 (Name)", width: 400 },
                                { field: "WHTTaxPayableAccrInterestBondID", title: "WHTTaxPayableAccrInterestBond (ID)", width: 300 },
                                { field: "WHTTaxPayableAccrInterestBondDesc", title: "WHTTaxPayableAccrInterestBond (Name)", width: 400 },

                                { field: "ManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 300 },
                                { field: "ManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 400 },
                                { field: "CustodianFeeExpenseID", title: "CustodianFeeExpense (ID)", width: 300 },
                                { field: "CustodianFeeExpenseDesc", title: "CustodianFeeExpense (Name)", width: 400 },
                                { field: "AuditFeeExpenseID", title: "AuditFeeExpense (ID)", width: 300 },
                                { field: "AuditFeeExpenseDesc", title: "AuditFeeExpense (Name)", width: 400 },
                                { field: "MovementFeeExpenseID", title: "MovementFeeExpense (ID)", width: 300 },
                                { field: "MovementFeeExpenseDesc", title: "MovementFeeExpense (Name)", width: 400 },
                                { field: "BankChargesID", title: "BankCharges (ID)", width: 300 },
                                { field: "BankChargesDesc", title: "BankCharges (Name)", width: 400 },
                                { field: "TaxExpenseInterestIncomeBondID", title: "TaxExpenseInterestIncomeBond (ID)", width: 300 },
                                { field: "TaxExpenseInterestIncomeBondDesc", title: "TaxExpenseInterestIncomeBond (Name)", width: 400 },
                                { field: "TaxExpenseInterestIncomeTimeDepositID", title: "TaxExpenseInterestIncomeTimeDeposit (ID)", width: 300 },
                                { field: "TaxExpenseInterestIncomeTimeDepositDesc", title: "TaxExpenseInterestIncomeTimeDeposit (Name)", width: 400 },
                                { field: "RealisedEquityID", title: "RealisedEquity (ID)", width: 300 },
                                { field: "RealisedEquityDesc", title: "RealisedEquity (Name)", width: 400 },
                                { field: "RealisedBondID", title: "RealisedBond (ID)", width: 300 },
                                { field: "RealisedBondDesc", title: "RealisedBond (Name)", width: 400 },
                                { field: "UnrealisedBondID", title: "UnrealisedBond (ID)", width: 300 },
                                { field: "UnrealisedBondDesc", title: "UnrealisedBond (Name)", width: 400 },
                                { field: "UnrealisedEquityID", title: "UnrealisedEquity (ID)", width: 300 },
                                { field: "UnrealisedEquityDesc", title: "UnrealisedEquity (Name)", width: 400 },
                                { field: "DistributedIncomeAccID", title: "DistributedIncomeAcc (ID)", width: 300 },
                                { field: "DistributedIncomeAccDesc", title: "DistributedIncomeAcc (Name)", width: 400 },
                                { field: "DistributedIncomePayableAccID", title: "DistributedIncomePayableAcc (ID)", width: 300 },
                                { field: "DistributedIncomePayableAccDesc", title: "DistributedIncomePayableAcc (Name)", width: 400 },
                                { field: "PendingSubscriptionID", title: "PendingSubscription (ID)", width: 300 },
                                { field: "PendingSubscriptionDesc", title: "PendingSubscription (Name)", width: 400 },
                                { field: "PendingRedemptionID", title: "PendingRedemption (ID)", width: 300 },
                                { field: "PendingRedemptionDesc", title: "PendingRedemption (Name)", width: 400 },
                                { field: "PendingSwitchingID", title: "PendingSwitchingID (ID)", width: 300 },
                                { field: "PendingSwitchingIDDesc", title: "PendingSwitchingID (Name)", width: 400 },
                                { field: "CurrentYearAccountID", title: "CurrentYearAccount (ID)", width: 300 },
                                { field: "CurrentYearAccountDesc", title: "CurrentYearAccount (Name)", width: 400 },
                                { field: "PriorYearAccountID", title: "PriorYearAccount (ID)", width: 300 },
                                { field: "PriorYearAccountDesc", title: "PriorYearAccount (Name)", width: 400 },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "SwitchingID", title: "Switching (ID)", width: 300 },
                                { field: "SwitchingDesc", title: "Switching (Name)", width: 400 },
                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var TemplateFundAccountingSetupHistoryURL = window.location.origin + "/Radsoft/TemplateFundAccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                          dataSourceHistory = getDataSource(TemplateFundAccountingSetupHistoryURL);

                        $("#gridTemplateFundAccountingSetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Template Fund Accounting Setup"
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
                                { field: "TemplateFundAccountingSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },

                                {
                                    field: "TaxPercentageDividend", title: "Tax Percentage Dividend", width: 200,
                                    template: "#: TaxPercentageDividend  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TaxPercentageBond", title: "Tax Percentage Bond", width: 200,
                                    template: "#: TaxPercentageBond  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TaxPercentageTD", title: "Tax Percentage TD", width: 200,
                                    template: "#: TaxPercentageTD  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                {
                                    field: "TaxPercentageCapitalGain", title: "Tax Percentage Capital Gain", width: 200,
                                    template: "#: TaxPercentageCapitalGain  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                { field: "SubscriptionID", title: "Subscription (ID)", width: 300 },
                                { field: "SubscriptionDesc", title: "Subscription (Name)", width: 400 },
                                { field: "PayableSubscriptionFeeID", title: "Payable Subscription Fee (ID)", width: 300 },
                                { field: "PayableSubscriptionFeeDesc", title: "Payable Subscription Fee (Name)", width: 400 },
                                { field: "RedemptionID", title: "Redemption (ID)", width: 300 },
                                { field: "RedemptionDesc", title: "Redemption (Name)", width: 400 },
                                { field: "PayableRedemptionFeeID", title: "Payable Redemption Fee (ID)", width: 300 },
                                { field: "PayableRedemptionFeeDesc", title: "Payable Redemption Fee (Name)", width: 400 },
                                { field: "PayableSwitchingFeeID", title: "PayableSwitchingFee (ID)", width: 300 },
                                { field: "PayableSwitchingFeeDesc", title: "PayableSwitchingFee (Name)", width: 400 },
                                { field: "PayableSInvestFeeID", title: "PayableSInvestFee (ID)", width: 300 },
                                { field: "PayableSInvestFeeDesc", title: "PayableSInvestFee (Name)", width: 400 },
                                { field: "InvestmentEquityID", title: "InvestmentEquity (ID)", width: 300 },
                                { field: "InvestmentEquityDesc", title: "InvestmentEquity (Name)", width: 400 },
                                { field: "InvestmentBondID", title: "InvestmentBond (ID)", width: 300 },
                                { field: "InvestmentBondDesc", title: "InvestmentBond (Name)", width: 400 },
                                { field: "InvestmentTimeDepositID", title: "InvestmentTimeDeposit (ID)", width: 300 },
                                { field: "InvestmentTimeDepositDesc", title: "InvestmentTimeDeposit (Name)", width: 400 },
                                { field: "InterestRecBondID", title: "InterestRecBond (ID)", width: 300 },
                                { field: "InterestRecBondDesc", title: "InterestRecBond (Name)", width: 400 },
                                { field: "InterestAccrBondID", title: "InterestAccrBond (ID)", width: 300 },
                                { field: "InterestAccrBondDesc", title: "InterestAccrBond (Name)", width: 400 },

                                { field: "InterestAccrTimeDepositID", title: "InterestAccrTimeDeposit (ID)", width: 300 },
                                { field: "InterestAccrTimeDepositDesc", title: "InterestAccrTimeDeposit (Name)", width: 400 },
                                { field: "InterestAccrGiroID", title: "InterestAccrGiro (ID)", width: 300 },
                                { field: "InterestAccrGiroDesc", title: "InterestAccrGiro (Name)", width: 400 },
                                { field: "PrepaidTaxDividendID", title: "PrepaidTaxDividend (ID)", width: 300 },
                                { field: "PrepaidTaxDividendDesc", title: "PrepaidTaxDividend (Name)", width: 400 },
                                { field: "AccountReceivableSaleBondID", title: "AccountReceivableSaleBond (ID)", width: 300 },
                                { field: "AccountReceivableSaleBondDesc", title: "AccountReceivableSaleBond (Name)", width: 400 },
                                { field: "AccountReceivableSaleEquityID", title: "AccountReceivableSaleEquity (ID)", width: 300 },
                                { field: "AccountReceivableSaleEquityDesc", title: "AccountReceivableSaleEquity (Name)", width: 400 },
                                { field: "AccountReceivableSaleTimeDepositID", title: "AccountReceivableSaleTimeDeposit (ID)", width: 300 },
                                { field: "AccountReceivableSaleTimeDepositDesc", title: "AccountReceivableSaleTimeDeposit (Name)", width: 400 },
                                { field: "IncomeInterestBondID", title: "IncomeInterestBond (ID)", width: 300 },
                                { field: "IncomeInterestBondDesc", title: "IncomeInterestBond (Name)", width: 400 },
                                { field: "IncomeInterestTimeDepositID", title: "IncomeInterestTimeDeposit (ID)", width: 300 },
                                { field: "IncomeInterestTimeDepositDesc", title: "IncomeInterestTimeDeposit (Name)", width: 400 },
                                { field: "PayableSubscriptionFeeID", title: "Payable Subscription Fee (ID)", width: 300 },
                                { field: "PayableSubscriptionFeeDesc", title: "Payable Subscription Fee (Name)", width: 400 },
                                { field: "IncomeInterestGiroID", title: "IncomeInterestGiro (ID)", width: 300 },
                                { field: "IncomeInterestGiroDesc", title: "IncomeInterestGiro (Name)", width: 400 },
                                { field: "IncomeDividendID", title: "IncomeDividend (ID)", width: 300 },
                                { field: "IncomeDividendDesc", title: "IncomeDividend (Name)", width: 400 },
                                { field: "ARDividendID", title: "ARDividend (ID)", width: 300 },
                                { field: "ARDividendDesc", title: "ARDividend (Name)", width: 400 },
                                { field: "TaxCapitalGainBondID", title: "TaxCapitalGainBond (ID)", width: 300 },
                                { field: "TaxCapitalGainBondDesc", title: "TaxCapitalGainBond (Name)", width: 400 },

                                { field: "RevaluationBondID", title: "RevaluationBond (ID)", width: 300 },
                                { field: "RevaluationBondDesc", title: "RevaluationBond (Name)", width: 400 },
                                { field: "RevaluationEquityID", title: "RevaluationEquity (ID)", width: 300 },
                                { field: "RevaluationEquityDesc", title: "RevaluationEquity (Name)", width: 400 },
                                { field: "PayablePurchaseEquityID", title: "PayablePurchaseEquity (ID)", width: 300 },
                                { field: "PayablePurchaseEquityDesc", title: "PayablePurchaseEquity (Name)", width: 400 },
                                { field: "PayablePurRecBondID", title: "PayablePurRecBond (ID)", width: 300 },
                                { field: "PayablePurRecBondDesc", title: "PayablePurRecBond (Name)", width: 400 },
                                { field: "PayableManagementFeeID", title: "PayableManagementFee (ID)", width: 300 },
                                { field: "PayableManagementFeeDesc", title: "PayableManagementFee (Name)", width: 400 },
                                { field: "PayableCustodianFeeID", title: "PayableCustodianFee (ID)", width: 300 },
                                { field: "PayableCustodianFeeDesc", title: "PayableCustodianFee (Name)", width: 400 },
                                { field: "PayableAuditFeeID", title: "PayableAuditFee (ID)", width: 300 },
                                { field: "PayableAuditFeeDesc", title: "PayableAuditFee (Name)", width: 400 },
                                { field: "PayableMovementFeeID", title: "PayableMovementFee (ID)", width: 300 },
                                { field: "PayableMovementFeeDesc", title: "PayableMovementFee (Name)", width: 400 },
                                { field: "BrokerCommissionID", title: "BrokerCommission (ID)", width: 300 },
                                { field: "BrokerCommissionDesc", title: "BrokerCommission (Name)", width: 400 },
                                { field: "BrokerLevyID", title: "BrokerLevy (ID)", width: 300 },
                                { field: "BrokerLevyDesc", title: "BrokerLevy (Name)", width: 400 },
                                { field: "BrokerVatID", title: "BrokerVat (ID)", width: 300 },
                                { field: "BrokerVatDesc", title: "BrokerVat (Name)", width: 400 },
                                { field: "BrokerSalesTaxID", title: "BrokerSalesTax (ID)", width: 300 },
                                { field: "BrokerSalesTaxDesc", title: "BrokerSalesTax (Name)", width: 400 },
                                { field: "WithHoldingTaxPPH23ID", title: "WithHoldingTaxPPH23 (ID)", width: 300 },
                                { field: "WithHoldingTaxPPH23Desc", title: "WithHoldingTaxPPH23 (Name)", width: 400 },
                                { field: "WHTTaxPayableAccrInterestBondID", title: "WHTTaxPayableAccrInterestBond (ID)", width: 300 },
                                { field: "WHTTaxPayableAccrInterestBondDesc", title: "WHTTaxPayableAccrInterestBond (Name)", width: 400 },

                                { field: "ManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 300 },
                                { field: "ManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 400 },
                                { field: "CustodianFeeExpenseID", title: "CustodianFeeExpense (ID)", width: 300 },
                                { field: "CustodianFeeExpenseDesc", title: "CustodianFeeExpense (Name)", width: 400 },
                                { field: "AuditFeeExpenseID", title: "AuditFeeExpense (ID)", width: 300 },
                                { field: "AuditFeeExpenseDesc", title: "AuditFeeExpense (Name)", width: 400 },
                                { field: "MovementFeeExpenseID", title: "MovementFeeExpense (ID)", width: 300 },
                                { field: "MovementFeeExpenseDesc", title: "MovementFeeExpense (Name)", width: 400 },
                                { field: "BankChargesID", title: "BankCharges (ID)", width: 300 },
                                { field: "BankChargesDesc", title: "BankCharges (Name)", width: 400 },
                                { field: "TaxExpenseInterestIncomeBondID", title: "TaxExpenseInterestIncomeBond (ID)", width: 300 },
                                { field: "TaxExpenseInterestIncomeBondDesc", title: "TaxExpenseInterestIncomeBond (Name)", width: 400 },
                                { field: "TaxExpenseInterestIncomeTimeDepositID", title: "TaxExpenseInterestIncomeTimeDeposit (ID)", width: 300 },
                                { field: "TaxExpenseInterestIncomeTimeDepositDesc", title: "TaxExpenseInterestIncomeTimeDeposit (Name)", width: 400 },
                                { field: "RealisedEquityID", title: "RealisedEquity (ID)", width: 300 },
                                { field: "RealisedEquityDesc", title: "RealisedEquity (Name)", width: 400 },
                                { field: "RealisedBondID", title: "RealisedBond (ID)", width: 300 },
                                { field: "RealisedBondDesc", title: "RealisedBond (Name)", width: 400 },
                                { field: "UnrealisedBondID", title: "UnrealisedBond (ID)", width: 300 },
                                { field: "UnrealisedBondDesc", title: "UnrealisedBond (Name)", width: 400 },
                                { field: "UnrealisedEquityID", title: "UnrealisedEquity (ID)", width: 300 },
                                { field: "UnrealisedEquityDesc", title: "UnrealisedEquity (Name)", width: 400 },
                                { field: "DistributedIncomeAccID", title: "DistributedIncomeAcc (ID)", width: 300 },
                                { field: "DistributedIncomeAccDesc", title: "DistributedIncomeAcc (Name)", width: 400 },
                                { field: "DistributedIncomePayableAccID", title: "DistributedIncomePayableAcc (ID)", width: 300 },
                                { field: "DistributedIncomePayableAccDesc", title: "DistributedIncomePayableAcc (Name)", width: 400 },
                                { field: "PendingSubscriptionID", title: "PendingSubscription (ID)", width: 300 },
                                { field: "PendingSubscriptionDesc", title: "PendingSubscription (Name)", width: 400 },
                                { field: "PendingRedemptionID", title: "PendingRedemption (ID)", width: 300 },
                                { field: "PendingRedemptionDesc", title: "PendingRedemption (Name)", width: 400 },

                                { field: "PendingSwitchingID", title: "PendingSwitchingID (ID)", width: 300 },
                                { field: "PendingSwitchingIDDesc", title: "PendingSwitchingID (Name)", width: 400 },
                                { field: "CurrentYearAccountID", title: "CurrentYearAccount (ID)", width: 300 },
                                { field: "CurrentYearAccountDesc", title: "CurrentYearAccount (Name)", width: 400 },
                                { field: "PriorYearAccountID", title: "PriorYearAccount (ID)", width: 300 },
                                { field: "PriorYearAccountDesc", title: "PriorYearAccount (Name)", width: 400 },
                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "SwitchingID", title: "Switching (ID)", width: 300 },
                                { field: "SwitchingDesc", title: "Switching (Name)", width: 400 },

                            ]
                        });
                    }
                } else {
                    refresh();
                }
            }
        });
    }

    function gridHistoryDataBound() {
        var grid = $("#gridTemplateFundAccountingSetupHistory").data("kendoGrid");
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

    $("#BtnAdd").click(function () {
        var val = validateData();
        if (val == 1) {

            alertify.confirm("Are you sure want to Add data?", function (e) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/TemplateFundAccountingSetup/CheckAlreadyHasExist/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                var TemplateFundAccountingSetup = {
                                    Subscription: $('#Subscription').val(),
                                    PayableSubscriptionFee: $('#PayableSubscriptionFee').val(),
                                    Redemption: $('#Redemption').val(),
                                    PayableRedemptionFee: $('#PayableRedemptionFee').val(),
                                    Switching: $('#Switching').val(),
                                    PayableSwitchingFee: $('#PayableSwitchingFee').val(),
                                    PayableSInvestFee: $('#PayableSInvestFee').val(),
                                    SInvestFee: $('#SInvestFee').val(),
                                    InvestmentEquity: $("#InvestmentEquity").val(),
                                    InvestmentBond: $("#InvestmentBond").val(),
                                    InvestmentTimeDeposit: $("#InvestmentTimeDeposit").val(),
                                    InterestRecBond: $("#InterestRecBond").val(),
                                    InterestAccrBond: $("#InterestAccrBond").val(),
                                    InterestAccrTimeDeposit: $("#InterestAccrTimeDeposit").val(),
                                    InterestAccrGiro: $("#InterestAccrGiro").val(),
                                    PrepaidTaxDividend: $("#PrepaidTaxDividend").val(),
                                    AccountReceivableSaleBond: $("#AccountReceivableSaleBond").val(),
                                    AccountReceivableSaleEquity: $("#AccountReceivableSaleEquity").val(),
                                    AccountReceivableSaleTimeDeposit: $("#AccountReceivableSaleTimeDeposit").val(),
                                    IncomeInterestBond: $("#IncomeInterestBond").val(),
                                    IncomeInterestTimeDeposit: $("#IncomeInterestTimeDeposit").val(),
                                    IncomeInterestGiro: $("#IncomeInterestGiro").val(),
                                    IncomeDividend: $("#IncomeDividend").val(),
                                    ARDividend: $("#ARDividend").val(),
                                    RevaluationBond: $("#RevaluationBond").val(),
                                    RevaluationEquity: $("#RevaluationEquity").val(),
                                    PayablePurchaseEquity: $("#PayablePurchaseEquity").val(),
                                    PayablePurRecBond: $("#PayablePurRecBond").val(),
                                    PayableManagementFee: $("#PayableManagementFee").val(),

                                    PayableCustodianFee: $("#PayableCustodianFee").val(),
                                    PayableAuditFee: $("#PayableAuditFee").val(),
                                    PayableMovementFee: $("#PayableMovementFee").val(),
                                    MovementFeeExpense: $("#MovementFeeExpense").val(),
                                    BrokerCommission: $("#BrokerCommission").val(),
                                    BrokerLevy: $("#BrokerLevy").val(),
                                    BrokerVat: $("#BrokerVat").val(),
                                    BrokerSalesTax: $("#BrokerSalesTax").val(),
                                    WithHoldingTaxPPH23: $("#WithHoldingTaxPPH23").val(),
                                    WHTTaxPayableAccrInterestBond: $("#WHTTaxPayableAccrInterestBond").val(),
                                    ManagementFeeExpense: $("#ManagementFeeExpense").val(),
                                    CustodianFeeExpense: $("#CustodianFeeExpense").val(),
                                    AuditFeeExpense: $("#AuditFeeExpense").val(),
                                    BankCharges: $("#BankCharges").val(),
                                    TaxExpenseInterestIncomeBond: $("#TaxExpenseInterestIncomeBond").val(),
                                    TaxExpenseInterestIncomeTimeDeposit: $("#TaxExpenseInterestIncomeTimeDeposit").val(),
                                    TaxCapitalGainBond: $("#TaxCapitalGainBond").val(),

                                    RealisedBond: $("#RealisedBond").val(),
                                    RealisedEquity: $("#RealisedEquity").val(),
                                    UnrealisedBond: $("#UnrealisedBond").val(),
                                    UnrealisedEquity: $("#UnrealisedEquity").val(),
                                    DistributedIncomeAcc: $("#DistributedIncomeAcc").val(),
                                    DistributedIncomePayableAcc: $("#DistributedIncomePayableAcc").val(),
                                    PendingSubscription: $("#PendingSubscription").val(),
                                    PendingRedemption: $("#PendingRedemption").val(),
                                    TaxPercentageDividend: $("#TaxPercentageDividend").val(),
                                    TaxPercentageBond: $("#TaxPercentageBond").val(),
                                    TaxPercentageTD: $("#TaxPercentageTD").val(),
                                    TaxPercentageCapitalGain: $("#TaxPercentageCapitalGain").val(),
                                    BondAmortization: $("#BondAmortization").val(),

                                    PayablePurchaseMutualFund: $("#PayablePurchaseMutualFund").val(),
                                    InvestmentMutualFund: $("#InvestmentMutualFund").val(),
                                    AccountReceivableSaleMutualFund: $("#AccountReceivableSaleMutualFund").val(),
                                    RevaluationMutualFund: $("#RevaluationMutualFund").val(),
                                    UnrealisedMutualFund: $("#UnrealisedMutualFund").val(),
                                    RealisedMutualFund: $("#RealisedMutualFund").val(),

                                    PayableOtherFeeOne: $("#PayableOtherFeeOne").val(),
                                    PayableOtherFeeTwo: $("#PayableOtherFeeTwo").val(),
                                    OtherFeeOneExpense: $("#OtherFeeOneExpense").val(),
                                    OtherFeeTwoExpense: $("#OtherFeeTwoExpense").val(),

                                    PendingSwitching: $("#PendingSwitching").val(),
                                    CurrentYearAccount: $("#CurrentYearAccount").val(),
                                    PriorYearAccount: $("#PriorYearAccount").val(),

                                    EntryUsersID: sessionStorage.getItem("user")

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/TemplateFundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateFundAccountingSetup_I",
                                    type: 'POST',
                                    data: JSON.stringify(TemplateFundAccountingSetup),
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
            })
        }
    });

    $("#BtnUpdate").click(function () {
        var val = validateData();
        if (val == 1) {
            alertify.prompt("Are you sure want to Update, please give notes:", "", function (e, str) {
                if (e) {
                    $.ajax({
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateFundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateFundAccountingSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var TemplateFundAccountingSetup = {
                                    TemplateFundAccountingSetupPK: $('#TemplateFundAccountingSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    Subscription: $('#Subscription').val(),
                                    PayableSubscriptionFee: $('#PayableSubscriptionFee').val(),
                                    Redemption: $('#Redemption').val(),
                                    PayableRedemptionFee: $('#PayableRedemptionFee').val(),
                                    Switching: $('#Switching').val(),
                                    PayableSwitchingFee: $('#PayableSwitchingFee').val(),
                                    Switching: $('#Switching').val(),
                                    PayableSwitchingFee: $('#PayableSwitchingFee').val(),
                                    PayableSInvestFee: $('#PayableSInvestFee').val(),
                                    SInvestFee: $('#SInvestFee').val(),
                                    InvestmentEquity: $("#InvestmentEquity").val(),
                                    InvestmentBond: $("#InvestmentBond").val(),
                                    InvestmentTimeDeposit: $("#InvestmentTimeDeposit").val(),
                                    InterestRecBond: $("#InterestRecBond").val(),
                                    InterestAccrBond: $("#InterestAccrBond").val(),
                                    InterestAccrTimeDeposit: $("#InterestAccrTimeDeposit").val(),
                                    InterestAccrGiro: $("#InterestAccrGiro").val(),
                                    PrepaidTaxDividend: $("#PrepaidTaxDividend").val(),
                                    AccountReceivableSaleBond: $("#AccountReceivableSaleBond").val(),
                                    AccountReceivableSaleEquity: $("#AccountReceivableSaleEquity").val(),
                                    AccountReceivableSaleTimeDeposit: $("#AccountReceivableSaleTimeDeposit").val(),
                                    IncomeInterestBond: $("#IncomeInterestBond").val(),
                                    IncomeInterestTimeDeposit: $("#IncomeInterestTimeDeposit").val(),
                                    IncomeInterestGiro: $("#IncomeInterestGiro").val(),
                                    IncomeDividend: $("#IncomeDividend").val(),
                                    ARDividend: $("#ARDividend").val(),
                                    RevaluationBond: $("#RevaluationBond").val(),
                                    RevaluationEquity: $("#RevaluationEquity").val(),
                                    PayablePurchaseEquity: $("#PayablePurchaseEquity").val(),
                                    PayablePurRecBond: $("#PayablePurRecBond").val(),
                                    PayableManagementFee: $("#PayableManagementFee").val(),

                                    PayableCustodianFee: $("#PayableCustodianFee").val(),
                                    PayableAuditFee: $("#PayableAuditFee").val(),
                                    PayableMovementFee: $("#PayableMovementFee").val(),
                                    MovementFeeExpense: $("#MovementFeeExpense").val(),
                                    BrokerCommission: $("#BrokerCommission").val(),
                                    BrokerLevy: $("#BrokerLevy").val(),
                                    BrokerVat: $("#BrokerVat").val(),
                                    BrokerSalesTax: $("#BrokerSalesTax").val(),
                                    WithHoldingTaxPPH23: $("#WithHoldingTaxPPH23").val(),
                                    WHTTaxPayableAccrInterestBond: $("#WHTTaxPayableAccrInterestBond").val(),
                                    ManagementFeeExpense: $("#ManagementFeeExpense").val(),
                                    CustodianFeeExpense: $("#CustodianFeeExpense").val(),
                                    AuditFeeExpense: $("#AuditFeeExpense").val(),
                                    BankCharges: $("#BankCharges").val(),
                                    TaxExpenseInterestIncomeBond: $("#TaxExpenseInterestIncomeBond").val(),
                                    TaxExpenseInterestIncomeTimeDeposit: $("#TaxExpenseInterestIncomeTimeDeposit").val(),
                                    TaxCapitalGainBond: $("#TaxCapitalGainBond").val(),

                                    RealisedBond: $("#RealisedBond").val(),
                                    RealisedEquity: $("#RealisedEquity").val(),
                                    UnrealisedBond: $("#UnrealisedBond").val(),
                                    UnrealisedEquity: $("#UnrealisedEquity").val(),
                                    DistributedIncomeAcc: $("#DistributedIncomeAcc").val(),
                                    DistributedIncomePayableAcc: $("#DistributedIncomePayableAcc").val(),
                                    PendingSubscription: $("#PendingSubscription").val(),
                                    PendingRedemption: $("#PendingRedemption").val(),
                                    TaxPercentageDividend: $("#TaxPercentageDividend").val(),
                                    TaxPercentageBond: $("#TaxPercentageBond").val(),
                                    TaxPercentageTD: $("#TaxPercentageTD").val(),
                                    TaxPercentageCapitalGain: $("#TaxPercentageCapitalGain").val(),

                                    BondAmortization: $("#BondAmortization").val(),

                                    PayablePurchaseMutualFund: $("#PayablePurchaseMutualFund").val(),
                                    InvestmentMutualFund: $("#InvestmentMutualFund").val(),
                                    AccountReceivableSaleMutualFund: $("#AccountReceivableSaleMutualFund").val(),
                                    RevaluationMutualFund: $("#RevaluationMutualFund").val(),
                                    UnrealisedMutualFund: $("#UnrealisedMutualFund").val(),
                                    RealisedMutualFund: $("#RealisedMutualFund").val(),

                                    PayableOtherFeeOne: $("#PayableOtherFeeOne").val(),
                                    PayableOtherFeeTwo: $("#PayableOtherFeeTwo").val(),
                                    OtherFeeOneExpense: $("#OtherFeeOneExpense").val(),
                                    OtherFeeTwoExpense: $("#OtherFeeTwoExpense").val(),

                                    PendingSwitching: $("#PendingSwitching").val(),
                                    CurrentYearAccount: $("#CurrentYearAccount").val(),
                                    PriorYearAccount: $("#PriorYearAccount").val(),
                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/TemplateFundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateFundAccountingSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(TemplateFundAccountingSetup),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateFundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateFundAccountingSetup",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "TemplateFundAccountingSetup" + "/" + $("#TemplateFundAccountingSetupPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateFundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateFundAccountingSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var TemplateFundAccountingSetup = {
                                TemplateFundAccountingSetupPK: $('#TemplateFundAccountingSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TemplateFundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateFundAccountingSetup_A",
                                type: 'POST',
                                data: JSON.stringify(TemplateFundAccountingSetup),
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
                $.ajax({
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateFundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateFundAccountingSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var TemplateFundAccountingSetup = {
                                TemplateFundAccountingSetupPK: $('#TemplateFundAccountingSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TemplateFundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateFundAccountingSetup_V",
                                type: 'POST',
                                data: JSON.stringify(TemplateFundAccountingSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#TemplateFundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "TemplateFundAccountingSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var TemplateFundAccountingSetup = {
                                TemplateFundAccountingSetupPK: $('#TemplateFundAccountingSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/TemplateFundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "TemplateFundAccountingSetup_R",
                                type: 'POST',
                                data: JSON.stringify(TemplateFundAccountingSetup),
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

});
