$(document).ready(function () {
    document.title = 'FORM FUND ACCOUNTING SETUP';
    //Global Variabel
    var win;
    var winOldData;
    var tabindex, subtabindex;
    var gridHeight = screen.height - 300;
    var GlobTabStrip = $("#TabSub").kendoTabStrip().data("kendoTabStrip");
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

        $("#BtnWinCopyFundAccountingSetup").kendoButton({
            imageUrl: "../../Images/Icon/IcImport.png"
        });
    }

    
    function initWindow() {

        win = $("#WinFundAccountingSetup").kendoWindow({
            height: 1000,
            title: "Fund Accounting Setup Detail",
            visible: false,
            width: 1300,
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

        WinCopyFundAccountingSetup = $("#WinCopyFundAccountingSetup").kendoWindow({
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

    var GlobValidator = $("#WinFundAccountingSetup").kendoValidator().data("kendoValidator");

    function validateData() {
        

        if (GlobValidator.validate()) {
            return 1;
        }
        else {
            alertify.alert("Validation not Pass");
            return 0;
        }
    }



    function ShowAllTab() { 
        //console.log(_GlobClientCode);
        $(GlobTabStrip.items()[0]).show();
        $(GlobTabStrip.items()[1]).show();
        $(GlobTabStrip.items()[2]).show();
        $(GlobTabStrip.items()[3]).show();
        $(GlobTabStrip.items()[4]).show();
        $(GlobTabStrip.items()[5]).show();
        $(GlobTabStrip.items()[6]).show();
        $(GlobTabStrip.items()[7]).show();
        $(GlobTabStrip.items()[8]).show();
        $(GlobTabStrip.items()[9]).show();
        $(GlobTabStrip.items()[10]).show();
       
    }

    function ResetTab() {
        $(GlobTabStrip.items()[0]).hide();
        $(GlobTabStrip.items()[1]).hide();
        $(GlobTabStrip.items()[2]).hide();
        $(GlobTabStrip.items()[3]).hide();
        $(GlobTabStrip.items()[4]).hide();
        $(GlobTabStrip.items()[5]).hide();
        $(GlobTabStrip.items()[6]).hide();
        $(GlobTabStrip.items()[7]).hide();
        $(GlobTabStrip.items()[8]).hide();
        $(GlobTabStrip.items()[9]).hide();
        $(GlobTabStrip.items()[10]).hide();
       
    }

    function showDetails(e) {
        $.blockUI({});
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



            $("#FundAccountingSetupPK").val(dataItemX.FundAccountingSetupPK);
            $("#HistoryPK").val(dataItemX.HistoryPK);
            $("#Notes").val(dataItemX.Notes);
            $("#BitAccruedInterestGiroDaily").prop('checked', dataItemX.BitAccruedInterestGiroDaily);
            $("#EntryUsersID").val(dataItemX.EntryUsersID);
            $("#UpdateUsersID").val(dataItemX.UpdateUsersID);
            $("#ApprovedUsersID").val(dataItemX.ApprovedUsersID);
            $("#VoidUsersID").val(dataItemX.VoidUsersID);
            $("#EntryTime").val(kendo.toString(kendo.parseDate(dataItemX.EntryTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#UpdateTime").val(kendo.toString(kendo.parseDate(dataItemX.UpdateTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#ApprovedTime").val(kendo.toString(kendo.parseDate(dataItemX.ApprovedTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#VoidTime").val(kendo.toString(kendo.parseDate(dataItemX.VoidTime), 'dd/MMM/yyyy HH:mm:ss'));
            $("#LastUpdate").val(kendo.toString(kendo.parseDate(dataItemX.LastUpdate), 'dd/MMM/yyyy HH:mm:ss'));

            ResetTab();
            RefreshTab(0);
            $(GlobTabStrip.items()[0]).show();
            $(GlobTabStrip.items()[1]).show();
            $(GlobTabStrip.items()[2]).show();
            $(GlobTabStrip.items()[3]).show();
            $(GlobTabStrip.items()[4]).show();
            $(GlobTabStrip.items()[5]).show();
            $(GlobTabStrip.items()[6]).show();
            $(GlobTabStrip.items()[7]).show();
            $(GlobTabStrip.items()[8]).show();
            $(GlobTabStrip.items()[9]).show();
            $(GlobTabStrip.items()[10]).show();
        }




        //Tab Sub Data
        var loadedTabs = [0];
        $("#TabSub").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            },
            activate: function (e) {
                subtabindex = $(e.item).index();
                //alert(subtabindex);
            }
        }).data("kendoTabStrip").select(0);


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

                $("#PayableOtherFeeThree").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableOtherFeeThree()
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

                $("#OtherFeeThreeExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbOtherFeeThreeExpense()
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

                $("#TaxInterestBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbTaxInterestBond()
                });


                //

                //$("#TaxPercentageCapitalGain").kendoComboBox({
                //    dataValueField: "FundJournalAccountPK",
                //    dataTextField: "ID",
                //    dataSource: data,
                //    filter: "contains",
                //    suggest: true,
                //    change: OnChangeFundJournalAccountPK,
                //    value: setCmbTaxPercentageCapitalGain()
                //});
                $("#TaxPercentageCapitalGain").kendoNumericTextBox({
                    format: "##.#### \\%",
                    decimals: 4,
                    value: setTaxPercentageCapitalGain()
                });
                $("#TaxPercentageCapitalGainSell").kendoNumericTextBox({
                    format: "##.#### \\%",
                    decimals: 4,
                    value: setTaxPercentageCapitalGainSell()
                });

                //$("#Switching").kendoComboBox({
                //    dataValueField: "FundJournalAccountPK",
                //    dataTextField: "ID",
                //    dataSource: data,
                //    filter: "contains",
                //    suggest: true,
                //    change: OnChangeFundJournalAccountPK,
                //    value: setCmbSwitching()
                //});
                $("#InterestAccruedCurrentAccount").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccruedCurrentAccount()
                });
                $("#IncomeCurrentAccount").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeCurrentAccount()
                });
                $("#InvInTDUSD").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvInTDUSD()
                });


                $("#InvestmentSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentSukuk()
                });
                $("#InterestRecSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestRecSukuk()
                });
                $("#PayablePurRecSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurRecSukuk()
                });
                $("#WHTTaxPayableAccrInterestSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbWHTTaxPayableAccrInterestSukuk()
                });
                $("#InterestAccrSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccrSukuk()
                });


                $("#RealisedSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRealisedSukuk()
                });
                $("#AccountReceivableSaleSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleSukuk()
                });
                $("#TaxCapitalGainSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbTaxCapitalGainSukuk()
                });
                $("#InvestmentProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentProtectedFund()
                });
                $("#PayablePurchaseProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurchaseProtectedFund()
                });
                $("#InvestmentPrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentPrivateEquityFund()
                });
                $("#PayablePurchasePrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurchasePrivateEquityFund()
                });
                $("#AccountReceivableSaleProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleProtectedFund()
                });
                $("#AccountReceivableSalePrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSalePrivateEquityFund()
                });






                $("#RevaluationSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationSukuk()
                });

                $("#UnrealisedSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedSukuk()
                });
                $("#CashForMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbCashForMutualFund()
                });
                $("#RevaluationProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationProtectedFund()
                });
                $("#RevaluationPrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationPrivateEquityFund()
                });
                $("#UnrealisedProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedProtectedFund()
                });
                $("#UnrealisedPrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedPrivateEquityFund()
                });


                $("#InterestReceivableSellSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestReceivableSellSukuk()
                });








                $("#InterestRecSellBond").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestRecSellBond()
                });


                $("#InterestReceivableBuySukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestReceivableBuySukuk()
                });
                $("#InterestReceivableSellMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestReceivableSellMutualFund()
                });
                $("#InterestReceivableSellProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestReceivableSellProtectedFund()
                });
                $("#InterestReceivableSellPrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestReceivableSellPrivateEquityFund()
                });


                $("#IncomeInterestSukuk").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeInterestSukuk()
                });
                $("#InterestAccrMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccrMutualFund()
                });
                $("#InterestAccrProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccrProtectedFund()
                });
                $("#InterestAccrPrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInterestAccrPrivateEquityFund()
                });
                $("#IncomeInterestAccrMutualFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeInterestAccrMutualFund()
                });
                $("#IncomeInterestAccrProtectedFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeInterestAccrProtectedFund()
                });
                $("#IncomeInterestAccrPrivateEquityFund").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbIncomeInterestAccrPrivateEquityFund()
                });
                $("#APManagementFee").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAPManagementFee()
                });
                $("#InvestmentRights").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentRights()
                });
                $("#PayablePurchaseRights").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurchaseRights()
                });
                $("#AccountReceivableSaleRights").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleRights()
                });
                $("#RealisedRights").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRealisedRights()
                });
                $("#RevaluationRights").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationRights()
                });
                $("#UnrealisedRights").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedRights()
                });
                $("#CashForIPO").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbCashForIPO()
                });
                $("#TaxProvision").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbTaxProvision()
                });
                $("#PayableTaxProvision").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableTaxProvision()
                });
                //------------------------
                $("#InvestmentWarrant").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbInvestmentWarrant()
                });
                $("#PayablePurchaseWarrant").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayablePurchaseWarrant()
                });
                $("#AccountReceivableSaleWarrant").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbAccountReceivableSaleWarrant()
                });
                $("#RealisedWarrant").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRealisedWarrant()
                });
                $("#RevaluationWarrant").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbRevaluationWarrant()
                });
                $("#UnrealisedWarrant").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbUnrealisedWarrant()
                });
                $("#CBESTExpense").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbCBESTExpense()
                });
                $("#PayableCBEST").kendoComboBox({
                    dataValueField: "FundJournalAccountPK",
                    dataTextField: "ID",
                    dataSource: data,
                    filter: "contains",
                    suggest: true,
                    change: OnChangeFundJournalAccountPK,
                    value: setCmbPayableCBEST()
                });





                $("#AveragePriority").kendoComboBox({
                    dataTextField: "text",
                    dataValueField: "value",
                    dataSource: [
                        { text: "Buy", value: 1 },
                        { text: "Sell", value: 2 },
                        { text: "Transaction", value: 3 }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangAveragePriority,
                    value: setCmbAveragePriority()
                });
                $("#AveragePriorityBond").kendoComboBox({
                    dataTextField: "text",
                    dataValueField: "value",
                    dataSource: [
                        { text: "Buy", value: 1 },
                        { text: "Sell", value: 2 },
                        { text: "Transaction", value: 3 }
                    ],
                    filter: "contains",
                    suggest: true,
                    change: OnChangAveragePriorityBond,
                    value: setCmbAveragePriorityBond()
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

                $("#TaxProvisionPercent").kendoNumericTextBox({
                    format: "##.#### \\%",
                    decimals: 4,
                    value: setTaxProvisionPercent()
                });
                function setTaxProvisionPercent() {
                    if (e == null) {
                        return 0;
                    } else {
                        return dataItemX.TaxProvisionPercent;
                    }
                }

                $.unblockUI();

            },
            error: function (data) {
                alertify.alert(data.responseText);
            }

        });



        function OnChangAveragePriority() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function OnChangAveragePriorityBond() {
            if (this.value() && this.selectedIndex == -1) {
                var dt = this.dataSource._data[0];
                this.text('');
            }
        }
        function setCmbAveragePriority() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.AveragePriority;
            }
        }
        function setCmbAveragePriorityBond() {
            if (e == null) {
                return 1;
            } else {
                return dataItemX.AveragePriorityBond;
            }
        }

        function setCmbOtherFeeThreeExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.OtherFeeThreeExpense == 0) {
                    return "";
                } else {
                    return dataItemX.OtherFeeThreeExpense;
                }
            }
        }

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

        function setCmbPayableOtherFeeThree() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableOtherFeeThree == 0) {
                    return "";
                } else {
                    return dataItemX.PayableOtherFeeThree;
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

        function setCmbTaxInterestBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxInterestBond == 0) {
                    return "";
                } else {
                    return dataItemX.TaxInterestBond;
                }
            }
        }

        // // // // // // // //
        //function setCmbTaxPercentageCapitalGain() {
        //    if (e == null) {
        //        return 0;
        //    } else {
        //        if (dataItemX.TaxPercentageCapitalGain == 0) {
        //            return 0;
        //        } else {
        //            return dataItemX.TaxPercentageCapitalGain;
        //        }
        //    }
        //}
        function setTaxPercentageCapitalGain() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.TaxPercentageCapitalGain;
            }
        }
        function setTaxPercentageCapitalGainSell() {
            if (e == null) {
                return 0;
            } else {
                return dataItemX.TaxPercentageCapitalGainSell;
            }
        }
        //function setCmbSwitching() {
        //    if (e == null) {
        //        return "";
        //    } else {
        //        if (dataItemX.Switching == 0) {
        //            return "";
        //        } else {
        //            return dataItemX.Switching;
        //        }
        //    }
        //}
        function setCmbInterestAccruedCurrentAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccruedCurrentAccount == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccruedCurrentAccount;
                }
            }
        }
        function setCmbIncomeCurrentAccount() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeCurrentAccount == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeCurrentAccount;
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


        function setCmbInvestmentSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentSukuk;
                }
            }
        }
        function setCmbInterestRecSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestRecSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.InterestRecSukuk;
                }
            }
        }
        function setCmbPayablePurRecSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurRecSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurRecSukuk;
                }
            }
        }
        function setCmbWHTTaxPayableAccrInterestSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.WHTTaxPayableAccrInterestSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.WHTTaxPayableAccrInterestSukuk;
                }
            }
        }
        function setCmbInterestAccrSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccrSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccrSukuk;
                }
            }
        }


        function setCmbRealisedSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedSukuk;
                }
            }
        }
        function setCmbAccountReceivableSaleSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleSukuk;
                }
            }
        }
        function setCmbTaxCapitalGainSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxCapitalGainSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.TaxCapitalGainSukuk;
                }
            }
        }
        function setCmbInvestmentProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentProtectedFund;
                }
            }
        }
        function setCmbPayablePurchaseProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurchaseProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurchaseProtectedFund;
                }
            }
        }
        function setCmbInvestmentPrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentPrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentPrivateEquityFund;
                }
            }
        }
        function setCmbPayablePurchasePrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurchasePrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurchasePrivateEquityFund;
                }
            }
        }
        function setCmbAccountReceivableSaleProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleProtectedFund;
                }
            }
        }
        function setCmbAccountReceivableSalePrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSalePrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSalePrivateEquityFund;
                }
            }
        }

        function setCmbRevaluationSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationSukuk;
                }
            }
        }

        function setCmbUnrealisedSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedSukuk;
                }
            }
        }
        function setCmbCashForMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashForMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.CashForMutualFund;
                }
            }
        }
        function setCmbRevaluationProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationProtectedFund;
                }
            }
        }
        function setCmbRevaluationPrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationPrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationPrivateEquityFund;
                }
            }
        }
        function setCmbUnrealisedProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedProtectedFund;
                }
            }
        }
        function setCmbUnrealisedPrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedPrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedPrivateEquityFund;
                }
            }
        }

        function setCmbInterestReceivableSellSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableSellSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableSellSukuk;
                }
            }
        }





        function setCmbInterestRecSellBond() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestRecSellBond == 0) {
                    return "";
                } else {
                    return dataItemX.InterestRecSellBond;
                }
            }
        }

        function setCmbInterestReceivableBuySukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableBuySukuk == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableBuySukuk;
                }
            }
        }
        function setCmbInterestReceivableSellMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableSellMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableSellMutualFund;
                }
            }
        }
        function setCmbInterestReceivableSellProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableSellProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableSellProtectedFund;
                }
            }
        }
        function setCmbInterestReceivableSellPrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestReceivableSellPrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.InterestReceivableSellPrivateEquityFund;
                }
            }
        }

        function setCmbIncomeInterestSukuk() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeInterestSukuk == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeInterestSukuk;
                }
            }
        }
        function setCmbInterestAccrMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccrMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccrMutualFund;
                }
            }
        }
        function setCmbInterestAccrProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccrProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccrProtectedFund;
                }
            }
        }
        function setCmbInterestAccrPrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InterestAccrPrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.InterestAccrPrivateEquityFund;
                }
            }
        }
        function setCmbIncomeInterestAccrMutualFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeInterestAccrMutualFund == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeInterestAccrMutualFund;
                }
            }
        }
        function setCmbIncomeInterestAccrProtectedFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeInterestAccrProtectedFund == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeInterestAccrProtectedFund;
                }
            }
        }
        function setCmbIncomeInterestAccrPrivateEquityFund() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.IncomeInterestAccrPrivateEquityFund == 0) {
                    return "";
                } else {
                    return dataItemX.IncomeInterestAccrPrivateEquityFund;
                }
            }
        }
        function setCmbAPManagementFee() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.APManagementFee == 0) {
                    return "";
                } else {
                    return dataItemX.APManagementFee;
                }
            }
        }

        function setCmbInvestmentRights() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentRights == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentRights;
                }
            }
        }
        function setCmbPayablePurchaseRights() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurchaseRights == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurchaseRights;
                }
            }
        }
        function setCmbAccountReceivableSaleRights() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleRights == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleRights;
                }
            }
        }
        function setCmbRealisedRights() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedRights == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedRights;
                }
            }
        }
        function setCmbRevaluationRights() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationRights == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationRights;
                }
            }
        }
        function setCmbUnrealisedRights() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedRights == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedRights;
                }
            }
        }
        function setCmbCashForIPO() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CashForIPO == 0) {
                    return "";
                } else {
                    return dataItemX.CashForIPO;
                }
            }
        }
        function setCmbTaxProvision() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.TaxProvision == 0) {
                    return "";
                } else {
                    return dataItemX.TaxProvision;
                }
            }
        }
        function setCmbPayableTaxProvision() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableTaxProvision == 0) {
                    return "";
                } else {
                    return dataItemX.PayableTaxProvision;
                }
            }
        }

        function setCmbInvestmentWarrant() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.InvestmentWarrant == 0) {
                    return "";
                } else {
                    return dataItemX.InvestmentWarrant;
                }
            }
        }

        function setCmbPayablePurchaseWarrant() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayablePurchaseWarrant == 0) {
                    return "";
                } else {
                    return dataItemX.PayablePurchaseWarrant;
                }
            }
        }

        function setCmbAccountReceivableSaleWarrant() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.AccountReceivableSaleWarrant == 0) {
                    return "";
                } else {
                    return dataItemX.AccountReceivableSaleWarrant;
                }
            }
        }

        function setCmbRealisedWarrant() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RealisedWarrant == 0) {
                    return "";
                } else {
                    return dataItemX.RealisedWarrant;
                }
            }
        }

        function setCmbRevaluationWarrant() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.RevaluationWarrant == 0) {
                    return "";
                } else {
                    return dataItemX.RevaluationWarrant;
                }
            }
        }

        function setCmbUnrealisedWarrant() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.UnrealisedWarrant == 0) {
                    return "";
                } else {
                    return dataItemX.UnrealisedWarrant;
                }
            }
        }

        function setCmbCBESTExpense() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.CBESTExpense == 0) {
                    return "";
                } else {
                    return dataItemX.CBESTExpense;
                }
            }
        }

        function setCmbPayableCBEST() {
            if (e == null) {
                return "";
            } else {
                if (dataItemX.PayableCBEST == 0) {
                    return "";
                } else {
                    return dataItemX.PayableCBEST;
                }
            }
        }



        // FundPK
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

    function onPopUpClose() {
        GlobValidator.hideMessages();
        clearData();
        showButton();
        refresh();
    }

    function clearData() {
        $("#FundAccountingSetupPK").val("");
        $("#HistoryPK").val("");
        $("#Notes").val("");
        $("#FundPK").val("");
        $("#Subscription").val("");
        $("#PayableSubscriptionFee").val("");
        $("#Redemption").val("");
        $("#PayableRedemptionFee").val("");
        $("#Switching").val("");
        $("#PayableSwitchingFee").val("");
        $("#PayableSInvestFee").val("");
        $("#SInvestFee").val("");
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
        $("#TaxInterestBond").val("");
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
        $("#PayableOtherFeeThree").val("");
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
        $("#TaxProvisionPercent").val("");
        $("#TaxCapitalGainBond").val("");
        $("#BondAmortization").val("");
        $("#PayablePurchaseMutualFund").val("");
        $("#InvestmentMutualFund").val("");
        $("#AccountReceivableSaleMutualFund").val("");
        $("#RevaluationMutualFund").val("");
        $("#UnrealisedMutualFund").val("");
        $("#RealisedMutualFund").val("");
        $("#OtherFeeThreeExpense").val("");
        $("#PayableOtherFeeOne").val("");
        $("#PayableOtherFeeTwo").val("");
        $("#OtherFeeOneExpense").val("");
        $("#OtherFeeTwoExpense").val("");

        $("#PendingSwitching").val("");
        $("#CurrentYearAccount").val("");
        $("#PriorYearAccount").val("");
        $("#TaxPercentageCapitalGain").val("");
        //$("#Switching").val("");
        $("#InterestAccruedCurrentAccount").val("");
        $("#IncomeCurrentAccount").val("");
        $("#InvInTDUSD").val("");


        $("#InvestmentSukuk").val("");
        $("#InterestRecSukuk").val("");
        $("#PayablePurRecSukuk").val("");
        $("#WHTTaxPayableAccrInterestSukuk").val("");
        $("#InterestAccrSukuk").val("");


        $("#RealisedSukuk").val("");
        $("#AccountReceivableSaleSukuk").val("");
        $("#TaxCapitalGainSukuk").val("");
        $("#InvestmentProtectedFund").val("");
        $("#PayablePurchaseProtectedFund").val("");
        $("#InvestmentPrivateEquityFund").val("");
        $("#PayablePurchasePrivateEquityFund").val("");
        $("#AccountReceivableSaleProtectedFund").val("");
        $("#AccountReceivableSalePrivateEquityFund").val("");

        $("#RevaluationSukuk").val("");

        $("#UnrealisedSukuk").val("");
        $("#CashForMutualFund").val("");
        $("#RevaluationProtectedFund").val("");
        $("#RevaluationPrivateEquityFund").val("");
        $("#UnrealisedProtectedFund").val("");
        $("#UnrealisedPrivateEquityFund").val("");

        $("#InterestReceivableSellSukuk").val("");





        $("#InterestRecSellBond").val("");

        $("#InterestReceivableBuySukuk").val("");
        $("#InterestReceivableSellMutualFund").val("");
        $("#InterestReceivableSellProtectedFund").val("");
        $("#InterestReceivableSellPrivateEquityFund").val("");

        $("#IncomeInterestSukuk").val("");
        $("#InterestAccrMutualFund").val("");
        $("#InterestAccrProtectedFund").val("");
        $("#InterestAccrPrivateEquityFund").val("");
        $("#IncomeInterestAccrMutualFund").val("");
        $("#IncomeInterestAccrProtectedFund").val("");
        $("#IncomeInterestAccrPrivateEquityFund").val("");
        $("#APManagementFee").val("");

        $("#InvestmentRights").val("");
        $("#PayablePurchaseRights").val("");
        $("#AccountReceivableSaleRights").val("");
        $("#RealisedRights").val("");
        $("#RevaluationRights").val("");
        $("#UnrealisedRights").val("");
        $("#CashForIPO").val("");
        $("#TaxProvision").val("");
        $("#PayableTaxProvision").val("");

        $("#InvestmentWarrant").val("");
        $("#PayablePurchaseWarrant").val("");
        $("#AccountReceivableSaleWarrant").val("");
        $("#RealisedWarrant").val("");
        $("#RevaluationWarrant").val("");
        $("#UnrealisedWarrant").val("");
        $("#CBESTExpense").val("");
        $("#PayableCBEST").val("");

        $("#AveragePriority").val("");
        $("#AveragePriorityBond").val("");
        $("#BitAccruedInterestGiroDaily").prop('checked', false);
        //$("#").val("");

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
                            FundAccountingSetupPK: { type: "number" },
                            HistoryPK: { type: "number" },
                            Status: { type: "number" },
                            StatusDesc: { type: "string" },
                            Notes: { type: "string" },
                            FundPK: { type: "number" },
                            FundID: { type: "string" },
                            FundName: { type: "string" },
                            Subscription: { type: "number" },
                            //SubscriptionID: { type: "string" },
                            //SubscriptionDesc: { type: "string" },
                            PayableSubscriptionFee: { type: "number" },
                            //PayableSubscriptionFeeID: { type: "string" },
                            //PayableSubscriptionFeeDesc: { type: "string" },
                            Redemption: { type: "number" },
                            //RedemptionID: { type: "string" },
                            //RedemptionDesc: { type: "string" },
                            PayableRedemptionFee: { type: "number" },
                            //PayableRedemptionFeeID: { type: "string" },
                            //PayableRedemptionFeeDesc: { type: "string" },
                            Switching: { type: "number" },
                            //SwitchingID: { type: "string" },
                            //SwitchingDesc: { type: "string" },
                            PayableSwitchingFee: { type: "number" },
                            //PayableSwitchingFeeID: { type: "string" },
                            //PayableSwitchingFeeDesc: { type: "string" },
                            PayableSInvestFee: { type: "number" },
                            //PayableSInvestFeeID: { type: "string" },
                            //PayableSInvestFeeDesc: { type: "string" }, 
                            SInvestFee: { type: "number" },
                            //SInvestFeeID: { type: "string" },
                            //SInvestFeeDesc: { type: "string" },
                            InvestmentEquity: { type: "number" },
                            //InvestmentEquityID: { type: "string" },
                            //InvestmentEquityDesc: { type: "string" },
                            InvestmentBond: { type: "number" },
                            //InvestmentBondID: { type: "string" },
                            //InvestmentBondDesc: { type: "string" },
                            InvestmentTimeDeposit: { type: "number" },
                            //InvestmentTimeDepositID: { type: "string" },
                            //InvestmentTimeDepositDesc: { type: "string" },
                            InterestRecBond: { type: "number" },
                            //InterestRecBondID: { type: "string" },
                            //InterestRecBondDesc: { type: "string" },
                            InterestAccrBond: { type: "number" },
                            //InterestAccrBondID: { type: "string" },
                            //InterestAccrBondDesc: { type: "string" },
                            InterestAccrTimeDeposit: { type: "number" },
                            //InterestAccrTimeDepositID: { type: "string" },
                            //InterestAccrTimeDepositDesc: { type: "string" },
                            InterestAccrGiro: { type: "number" },
                            //InterestAccrGiroID: { type: "string" },
                            //InterestAccrGiroDesc: { type: "string" },
                            PrepaidTaxDividend: { type: "number" },
                            //PrepaidTaxDividendID: { type: "string" },
                            //PrepaidTaxDividendDesc: { type: "string" },
                            AccountReceivableSaleBond: { type: "number" },
                            //AccountReceivableSaleBondID: { type: "string" },
                            //AccountReceivableSaleBondDesc: { type: "string" },
                            AccountReceivableSaleEquity: { type: "number" },
                            //AccountReceivableSaleEquityID: { type: "string" },
                            //AccountReceivableSaleEquityDesc: { type: "string" },
                            AccountReceivableSaleTimeDeposit: { type: "number" },
                            //AccountReceivableSaleTimeDepositID: { type: "string" },
                            //AccountReceivableSaleTimeDepositDesc: { type: "string" },
                            IncomeInterestBond: { type: "number" },
                            //IncomeInterestBondID: { type: "string" },
                            //IncomeInterestBondDesc: { type: "string" },
                            IncomeInterestTimeDeposit: { type: "number" },
                            //IncomeInterestTimeDepositID: { type: "string" },
                            //IncomeInterestTimeDepositDesc: { type: "string" },
                            IncomeInterestGiro: { type: "number" },
                            //IncomeInterestGiroID: { type: "string" },
                            //IncomeInterestGiroDesc: { type: "string" },
                            IncomeDividend: { type: "number" },
                            //IncomeDividendID: { type: "string" },
                            //IncomeDividendDesc: { type: "string" },
                            ARDividend: { type: "number" },
                            //ARDividendID: { type: "string" },
                            //ARDividendDesc: { type: "string" },
                            RevaluationBond: { type: "number" },
                            //RevaluationBondID: { type: "string" },
                            //RevaluationBondDesc: { type: "string" },
                            RevaluationEquity: { type: "number" },
                            //RevaluationEquityID: { type: "string" },
                            //RevaluationEquityDesc: { type: "string" },
                            PayablePurchaseEquity: { type: "number" },
                            //PayablePurchaseEquityID: { type: "string" },
                            //PayablePurchaseEquityDesc: { type: "string" },
                            PayablePurRecBond: { type: "number" },
                            //PayablePurRecBondID: { type: "string" },
                            //PayablePurRecBondDesc: { type: "string" },
                            PayableManagementFee: { type: "number" },
                            //PayableManagementFeeID: { type: "string" },
                            //PayableManagementFeeDesc: { type: "string" },
                            PayableCustodianFee: { type: "number" },
                            //PayableCustodianFeeID: { type: "string" },
                            //PayableCustodianFeeDesc: { type: "string" },
                            PayableAuditFee: { type: "number" },
                            //PayableAuditFeeID: { type: "string" },
                            //PayableAuditFeeDesc: { type: "string" },

                            PayableMovementFee: { type: "number" },
                            //PayableMovementFeeID: { type: "string" },
                            //PayableMovementFeeDesc: { type: "string" },

                            BrokerCommission: { type: "number" },
                            //BrokerCommissionID: { type: "string" },
                            //BrokerCommissionDesc: { type: "string" },
                            BrokerLevy: { type: "number" },
                            //BrokerLevyID: { type: "string" },
                            //BrokerLevyDesc: { type: "string" },
                            TaxCapitalGainBond: { type: "number" },
                            //TaxCapitalGainBondID: { type: "string" },
                            //TaxCapitalGainBondDesc: { type: "string" },

                            BrokerVat: { type: "number" },
                            //BrokerVatID: { type: "string" },
                            //BrokerVatDesc: { type: "string" },
                            BrokerSalesTax: { type: "number" },
                            //BrokerSalesTaxID: { type: "string" },
                            //BrokerSalesTaxDesc: { type: "string" },
                            WithHoldingTaxPPH23: { type: "number" },
                            //WithHoldingTaxPPH23ID: { type: "string" },
                            //WithHoldingTaxPPH23Desc: { type: "string" },
                            WHTTaxPayableAccrInterestBond: { type: "number" },
                            //WHTTaxPayableAccrInterestBondID: { type: "string" },
                            //WHTTaxPayableAccrInterestBondDesc: { type: "string" },
                            ManagementFeeExpense: { type: "number" },
                            //ManagementFeeExpenseID: { type: "string" },
                            //ManagementFeeExpenseDesc: { type: "string" },
                            CustodianFeeExpense: { type: "number" },
                            //CustodianFeeExpenseID: { type: "string" },
                            //CustodianFeeExpenseDesc: { type: "string" },
                            AuditFeeExpense: { type: "number" },
                            //AuditFeeExpenseID: { type: "string" },
                            //AuditFeeExpenseDesc: { type: "string" },

                            MovementFeeExpense: { type: "number" },
                            //MovementFeeExpenseID: { type: "string" },
                            //MovementFeeExpenseDesc: { type: "string" },

                            BankCharges: { type: "number" },
                            //BankChargesID: { type: "string" },
                            //BankChargesDesc: { type: "string" },

                            TaxExpenseInterestIncomeBond: { type: "number" },
                            //TaxExpenseInterestIncomeBondID: { type: "string" },
                            //TaxExpenseInterestIncomeBondDesc: { type: "string" },
                            TaxExpenseInterestIncomeTimeDeposit: { type: "number" },
                            //TaxExpenseInterestIncomeTimeDepositID: { type: "string" },
                            //TaxExpenseInterestIncomeTimeDepositDesc: { type: "string" },
                            RealisedEquity: { type: "number" },
                            //RealisedEquityID: { type: "string" },
                            //RealisedEquityDesc: { type: "string" },
                            RealisedBond: { type: "number" },
                            //RealisedBondID: { type: "string" },
                            //RealisedBondDesc: { type: "string" }, 
                            UnrealisedBond: { type: "number" },
                            //UnrealisedBondID: { type: "string" },
                            //UnrealisedBondDesc: { type: "string" },

                            UnrealisedEquity: { type: "number" },
                            //UnrealisedEquityID: { type: "string" },
                            //UnrealisedEquityDesc: { type: "string" },
                            DistributedIncomeAcc: { type: "number" },
                            //DistributedIncomeAccID: { type: "string" },
                            //DistributedIncomeAccDesc: { type: "string" },
                            DistributedIncomePayableAcc: { type: "number" },
                            //DistributedIncomePayableAccID: { type: "string" },
                            //DistributedIncomePayableAccDesc: { type: "string" },
                            PendingSubscription: { type: "number" },
                            //PendingSubscriptionID: { type: "string" },
                            //PendingSubscriptionDesc: { type: "string" },
                            PendingRedemption: { type: "number" },
                            //PendingRedemptionID: { type: "string" },
                            //PendingRedemptionDesc: { type: "string" },

                            BondAmortization: { type: "number" },
                            //BondAmortizationID: { type: "string" },
                            //BondAmortizationDesc: { type: "string" },

                            PayablePurchaseMutualFund: { type: "number" },
                            //PayablePurchaseMutualFundID: { type: "string" },
                            //PayablePurchaseMutualFundDesc: { type: "string" },

                            InvestmentMutualFund: { type: "number" },
                            //InvestmentMutualFundID: { type: "string" },
                            //InvestmentMutualFundDesc: { type: "string" },

                            AccountReceivableSaleMutualFund: { type: "number" },
                            //AccountReceivableSaleMutualFundID: { type: "string" },
                            //AccountReceivableSaleMutualFundDesc: { type: "string" },

                            RevaluationMutualFund: { type: "number" },
                            //RevaluationMutualFundID: { type: "string" },
                            //RevaluationMutualFundDesc: { type: "string" },

                            UnrealisedMutualFund: { type: "number" },
                            //UnrealisedMutualFundID: { type: "string" },
                            //UnrealisedMutualFundDesc: { type: "string" },

                            RealisedMutualFund: { type: "number" },
                            //RealisedMutualFundID: { type: "string" },
                            //RealisedMutualFundDesc: { type: "string" },

                            PayableOtherFeeOne: { type: "number" },
                            //PayableOtherFeeOneID: { type: "string" },
                            //PayableOtherFeeOneDesc: { type: "string" },

                            PayableOtherFeeTwo: { type: "number" },
                            //PayableOtherFeeTwoID: { type: "string" },
                            //PayableOtherFeeTwoDesc: { type: "string" },

                            OtherFeeOneExpense: { type: "number" },
                            //OtherFeeOneExpenseID: { type: "string" },
                            //OtherFeeOneExpenseDesc: { type: "string" },

                            OtherFeeTwoExpense: { type: "number" },
                            //OtherFeeTwoExpenseID: { type: "string" },
                            //OtherFeeTwoExpenseDesc: { type: "string" },

                            PendingSwitching: { type: "number" },
                            //PendingSwitchingID: { type: "string" },
                            //PendingSwitchingDesc: { type: "string" },

                            CurrentYearAccount: { type: "number" },
                            //CurrentYearAccountID: { type: "string" },
                            //CurrentYearAccountDesc: { type: "string" },

                            PriorYearAccount: { type: "number" },
                            //PriorYearAccountID: { type: "string" },
                            //PriorYearAccountDesc: { type: "string" },

                            TaxPercentageCapitalGain: { type: "number" },
                            //TaxPercentageCapitalGainID: { type: "string" },
                            //TaxPercentageCapitalGainDesc: { type: "string" },

                            //Switching: { type: "number" },
                            //SwitchingID: { type: "string" },
                            //SwitchingDesc: { type: "string" },

                            InterestAccruedCurrentAccount: { type: "number" },
                            //InterestAccruedCurrentAccountID: { type: "string" },
                            //InterestAccruedCurrentAccountDesc: { type: "string" },

                            IncomeCurrentAccount: { type: "number" },
                            //IncomeCurrentAccountID: { type: "string" },
                            //IncomeCurrentAccountDesc: { type: "string" },

                            InvInTDUSD: { type: "number" },
                            //InvInTDUSDID: { type: "string" },
                            //InvInTDUSDDesc: { type: "string" },

                            AccountReceivableSaleSukuk: { type: "number" },
                            //AccountReceivableSaleSukukID: { type: "string" },
                            //AccountReceivableSaleSukukDesc: { type: "string" },
                            TaxCapitalGainSukuk: { type: "number" },
                            //TaxCapitalGainSukukID: { type: "string" },
                            //TaxCapitalGainSukukDesc: { type: "string" },
                            InvestmentProtectedFund: { type: "number" },
                            //InvestmentProtectedFundID: { type: "string" },
                            //InvestmentProtectedFundDesc: { type: "string" },
                            PayablePurchaseProtectedFund: { type: "number" },
                            //PayablePurchaseProtectedFundID: { type: "string" },
                            //PayablePurchaseProtectedFundDesc: { type: "string" },
                            InvestmentPrivateEquityFund: { type: "number" },
                            //InvestmentPrivateEquityFundID: { type: "string" },
                            //InvestmentPrivateEquityFundDesc: { type: "string" },
                            PayablePurchasePrivateEquityFund: { type: "number" },
                            //PayablePurchasePrivateEquityFundID: { type: "string" },
                            //PayablePurchasePrivateEquityFundDesc: { type: "string" },
                            AccountReceivableSaleProtectedFund: { type: "number" },
                            //AccountReceivableSaleProtectedFundID: { type: "string" },
                            //AccountReceivableSaleProtectedFundDesc: { type: "string" },
                            AccountReceivableSalePrivateEquityFund: { type: "number" },
                            //AccountReceivableSalePrivateEquityFundID: { type: "string" },
                            //AccountReceivableSalePrivateEquityFundDesc: { type: "string" },

                            RevaluationSukuk: { type: "number" },
                            //RevaluationSukukID: { type: "string" },
                            //RevaluationSukukDesc: { type: "string" },

                            UnrealisedSukuk: { type: "number" },
                            //UnrealisedSukukID: { type: "string" },
                            //UnrealisedSukukDesc: { type: "string" },
                            CashForMutualFund: { type: "number" },
                            //CashForMutualFundID: { type: "string" },
                            //CashForMutualFundDesc: { type: "string" },
                            RevaluationProtectedFund: { type: "number" },
                            //RevaluationProtectedFundID: { type: "string" },
                            //RevaluationProtectedFundDesc: { type: "string" },
                            RevaluationPrivateEquityFund: { type: "number" },
                            //RevaluationPrivateEquityFundID: { type: "string" },
                            //RevaluationPrivateEquityFundDesc: { type: "string" },
                            UnrealisedProtectedFund: { type: "number" },
                            //UnrealisedProtectedFundID: { type: "string" },
                            //UnrealisedProtectedFundDesc: { type: "string" },
                            UnrealisedPrivateEquityFund: { type: "number" },
                            //UnrealisedPrivateEquityFundID: { type: "string" },
                            //UnrealisedPrivateEquityFundDesc: { type: "string" },

                            InterestReceivableSellSukuk: { type: "number" },
                            //InterestReceivableSellSukukID: { type: "string" },
                            //InterestReceivableSellSukukDesc: { type: "string" },





                            InterestRecSellBond: { type: "number" },
                            //InterestRecSellBondID: { type: "string" },
                            //InterestRecSellBondDesc: { type: "string" },

                            InterestReceivableBuySukuk: { type: "number" },
                            //InterestReceivableBuySukukID: { type: "string" },
                            //InterestReceivableBuySukukDesc: { type: "string" },
                            InterestReceivableSellMutualFund: { type: "number" },
                            //InterestReceivableSellMutualFundID: { type: "string" },
                            //InterestReceivableSellMutualFundDesc: { type: "string" },
                            InterestReceivableSellProtectedFund: { type: "number" },
                            //InterestReceivableSellProtectedFundID: { type: "string" },
                            //InterestReceivableSellProtectedFundDesc: { type: "string" },
                            InterestReceivableSellPrivateEquityFund: { type: "number" },
                            //InterestReceivableSellPrivateEquityFundID: { type: "string" },
                            //InterestReceivableSellPrivateEquityFundDesc: { type: "string" },

                            IncomeInterestSukuk: { type: "number" },
                            //IncomeInterestSukukID: { type: "string" },
                            //IncomeInterestSukukDesc: { type: "string" },
                            InterestAccrMutualFund: { type: "number" },
                            //InterestAccrMutualFundID: { type: "string" },
                            //InterestAccrMutualFundDesc: { type: "string" },
                            InterestAccrProtectedFund: { type: "number" },
                            //InterestAccrProtectedFundID: { type: "string" },
                            //InterestAccrProtectedFundDesc: { type: "string" },
                            InterestAccrPrivateEquityFund: { type: "number" },
                            //InterestAccrPrivateEquityFundID: { type: "string" },
                            //InterestAccrPrivateEquityFundDesc: { type: "string" },
                            IncomeInterestAccrMutualFund: { type: "number" },
                            //IncomeInterestAccrMutualFundID: { type: "string" },
                            //IncomeInterestAccrMutualFundDesc: { type: "string" },
                            IncomeInterestAccrProtectedFund: { type: "number" },
                            //IncomeInterestAccrProtectedFundID: { type: "string" },
                            //IncomeInterestAccrProtectedFundDesc: { type: "string" },
                            IncomeInterestAccrPrivateEquityFund: { type: "number" },
                            //IncomeInterestAccrPrivateEquityFundID: { type: "string" },
                            //IncomeInterestAccrPrivateEquityFundDesc: { type: "string" },
                            APManagementFee: { type: "number" },
                            //APManagementFeeID: { type: "string" },
                            //APManagementFeeDesc: { type: "string" },

                            InvestmentRights: { type: "number" },
                            //InvestmentRightsID: { type: "string" },
                            //InvestmentRightsDesc: { type: "string" },
                            PayablePurchaseRights: { type: "number" },
                            //PayablePurchaseRightsID: { type: "string" },
                            //PayablePurchaseRightsDesc: { type: "string" },
                            AccountReceivableSaleRights: { type: "number" },
                            //AccountReceivableSaleRightsID: { type: "string" },
                            //AccountReceivableSaleRightsDesc: { type: "string" },
                            RealisedRights: { type: "number" },
                            //RealisedRightsID: { type: "string" },
                            //RealisedRightsDesc: { type: "string" },
                            RevaluationRights: { type: "number" },
                            //RevaluationRightsID: { type: "string" },
                            //RevaluationRightsDesc: { type: "string" },
                            UnrealisedRights: { type: "number" },
                            //UnrealisedRightsID: { type: "string" },
                            //UnrealisedRightsDesc: { type: "string" },

                            CashForIPO: { type: "number" },
                            //CashForIPOID: { type: "string" },
                            //CashForIPODesc: { type: "string" },
                            TaxProvision: { type: "number" },
                            PayableTaxProvision: { type: "number" },

                            InvestmentWarrant: { type: "number" },
                            PayablePurchaseWarrant: { type: "number" },
                            AccountReceivableSaleWarrant: { type: "number" },
                            RealisedWarrant: { type: "number" },
                            RevaluationWarrant: { type: "number" },
                            UnrealisedWarrant: { type: "number" },
                            CBESTExpense: { type: "number" },
                            PayableCBEST: { type: "number" },

                            BitAccruedInterestGiroDaily: { type: "bool" },


                            AveragePriority: { type: "number" },
                            AveragePriorityBond: { type: "number" },
                            //AveragePriorityDesc: { type: "string" },

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
            var gridApproved = $("#gridFundAccountingSetupApproved").data("kendoGrid");
            gridApproved.dataSource.read();
        }
        if (tabindex == 1) {
            var gridPending = $("#gridFundAccountingSetupPending").data("kendoGrid");
            gridPending.dataSource.read();
        }
        if (tabindex == 2) {
            var gridHistory = $("#gridFundAccountingSetupHistory").data("kendoGrid");
            gridHistory.dataSource.read();
        }
    }

    function initGrid() {
        var FundAccountingSetupApprovedURL = window.location.origin + "/Radsoft/FundAccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 2,
            dataSourceApproved = getDataSource(FundAccountingSetupApprovedURL);

        $("#gridFundAccountingSetupApproved").kendoGrid({
            dataSource: dataSourceApproved,
            height: gridHeight,
            scrollable: {
                virtual: true
            },
            groupable: {
                messages: {
                    empty: "Form Fund Accounting Setup"
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
                { field: "FundAccountingSetupPK", title: "SysNo.", width: 95 },
                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                { field: "FundID", title: "Fund ID", width: 250 },
                { field: "FundName", title: "Fund Name", width: 400 },
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
                    field: "TaxProvisionPercent", title: "Tax Provision Percent", width: 200,
                    template: "#: TaxProvisionPercent  # %",
                    attributes: { style: "text-align:right;" }
                },


                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "VoidUsersID", title: "VoidID", width: 200 },
                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },





                //SAMPE SINI

            ]
        });

        //load data pada saat mengganti tab
        var loadedTabs = [0];
        $("#TabFundAccountingSetup").kendoTabStrip({
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
                        var FundAccountingSetupPendingURL = window.location.origin + "/Radsoft/FundAccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 1,
                            dataSourcePending = getDataSource(FundAccountingSetupPendingURL);
                        $("#gridFundAccountingSetupPending").kendoGrid({
                            dataSource: dataSourcePending,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Accounting Setup"
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
                                { field: "FundAccountingSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 250 },
                                { field: "FundName", title: "Fund Name", width: 400 },
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
                                    field: "TaxProvisionPercent", title: "Tax Provision Percent", width: 200,
                                    template: "#: TaxProvisionPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "LastUpdate", title: "LastUpdate", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },


                                //
                                //{ field: "TaxPercentageCapitalGainID", title: "TaxPercentageCapitalGain (ID)", width: 300 },
                                //{ field: "TaxPercentageCapitalGainDesc", title: "TaxPercentageCapitalGain (Name)", width: 400 }

                            ]
                        });
                    }
                    if (tabindex == 2) {

                        var FundAccountingSetupHistoryURL = window.location.origin + "/Radsoft/FundAccountingSetup/GetData/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + 9,
                            dataSourceHistory = getDataSource(FundAccountingSetupHistoryURL);

                        $("#gridFundAccountingSetupHistory").kendoGrid({
                            dataSource: dataSourceHistory,
                            height: gridHeight,
                            scrollable: {
                                virtual: true
                            },
                            groupable: {
                                messages: {
                                    empty: "Form Fund Accounting Setup"
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
                                { field: "FundAccountingSetupPK", title: "SysNo.", width: 95 },
                                { field: "Status", title: "Status", hidden: true, filterable: false, width: 120 },
                                { field: "StatusDesc", title: "Status", width: 200 },
                                { field: "HistoryPK", title: "HisNo.", filterable: false, hidden: true, width: 120 },
                                { field: "FundID", title: "Fund ID", width: 250 },
                                { field: "FundName", title: "Fund Name", width: 400 },

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
                                    field: "TaxProvisionPercent", title: "Tax Provision Percent", width: 200,
                                    template: "#: TaxProvisionPercent  # %",
                                    attributes: { style: "text-align:right;" }
                                },
                                //{ field: "SubscriptionID", title: "Subscription (ID)", width: 300 },
                                //{ field: "SubscriptionDesc", title: "Subscription (Name)", width: 400 },
                                //{ field: "PayableSubscriptionFeeID", title: "Payable Subscription Fee (ID)", width: 300 },
                                //{ field: "PayableSubscriptionFeeDesc", title: "Payable Subscription Fee (Name)", width: 400 },
                                //{ field: "RedemptionID", title: "Redemption (ID)", width: 300 },
                                //{ field: "RedemptionDesc", title: "Redemption (Name)", width: 400 },
                                //{ field: "PayableRedemptionFeeID", title: "Payable Redemption Fee (ID)", width: 300 },
                                //{ field: "PayableRedemptionFeeDesc", title: "Payable Redemption Fee (Name)", width: 400 },
                                //{ field: "SwitchingID", title: "Switching (ID)", width: 300 },
                                //{ field: "SwitchingDesc", title: "Switching (Name)", width: 400 },
                                //{ field: "PayableSwitchingFeeID", title: "PayableSwitchingFee (ID)", width: 300 },
                                //{ field: "PayableSwitchingFeeDesc", title: "PayableSwitchingFee (Name)", width: 400 },
                                //{ field: "PayableSInvestFeeID", title: "PayableSInvestFee (ID)", width: 300 },
                                //{ field: "PayableSInvestFeeDesc", title: "PayableSInvestFee (Name)", width: 400 },
                                //{ field: "PayablePurchaseMutualFundID", title: "PayablePurchaseMutualFund (ID)", width: 300 },
                                //{ field: "PayablePurchaseMutualFundDesc", title: "PayablePurchaseMutualFund (Name)", width: 400 },
                                //{ field: "SInvestFeeID", title: "SInvestFee (ID)", width: 300 },
                                //{ field: "SInvestFeeDesc", title: "SInvestFee (Name)", width: 400 },
                                //{ field: "InvestmentEquityID", title: "InvestmentEquity (ID)", width: 300 },
                                //{ field: "InvestmentEquityDesc", title: "InvestmentEquity (Name)", width: 400 },
                                //{ field: "InvestmentBondID", title: "InvestmentBond (ID)", width: 300 },
                                //{ field: "InvestmentBondDesc", title: "InvestmentBond (Name)", width: 400 },
                                //{ field: "InvestmentTimeDepositID", title: "InvestmentTimeDeposit (ID)", width: 300 },
                                //{ field: "InvestmentTimeDepositDesc", title: "InvestmentTimeDeposit (Name)", width: 400 },
                                //{ field: "InvestmentMutualFundID", title: "InvestmentMutualFund (ID)", width: 300 },
                                //{ field: "InvestmentMutualFundDesc", title: "InvestmentMutualFund (Name)", width: 400 },
                                //{ field: "InterestRecBondID", title: "InterestRecBond (ID)", width: 300 },
                                //{ field: "InterestRecBondDesc", title: "InterestRecBond (Name)", width: 400 },
                                //{ field: "InterestAccrBondID", title: "InterestAccrBond (ID)", width: 300 },
                                //{ field: "InterestAccrBondDesc", title: "InterestAccrBond (Name)", width: 400 },

                                //{ field: "InterestAccrTimeDepositID", title: "InterestAccrTimeDeposit (ID)", width: 300 },
                                //{ field: "InterestAccrTimeDepositDesc", title: "InterestAccrTimeDeposit (Name)", width: 400 },
                                //{ field: "InterestAccrGiroID", title: "InterestAccrGiro (ID)", width: 300 },
                                //{ field: "InterestAccrGiroDesc", title: "InterestAccrGiro (Name)", width: 400 },
                                //{ field: "PrepaidTaxDividendID", title: "PrepaidTaxDividend (ID)", width: 300 },
                                //{ field: "PrepaidTaxDividendDesc", title: "PrepaidTaxDividend (Name)", width: 400 },
                                //{ field: "AccountReceivableSaleBondID", title: "AccountReceivableSaleBond (ID)", width: 300 },
                                //{ field: "AccountReceivableSaleBondDesc", title: "AccountReceivableSaleBond (Name)", width: 400 },
                                //{ field: "AccountReceivableSaleEquityID", title: "AccountReceivableSaleEquity (ID)", width: 300 },
                                //{ field: "AccountReceivableSaleEquityDesc", title: "AccountReceivableSaleEquity (Name)", width: 400 },
                                //{ field: "AccountReceivableSaleTimeDepositID", title: "AccountReceivableSaleTimeDeposit (ID)", width: 300 },
                                //{ field: "AccountReceivableSaleTimeDepositDesc", title: "AccountReceivableSaleTimeDeposit (Name)", width: 400 },
                                //{ field: "AccountReceivableSaleMutualFundID", title: "AccountReceivableSaleMutualFund (ID)", width: 300 },
                                //{ field: "AccountReceivableSaleMutualFundDesc", title: "AccountReceivableSaleMutualFund (Name)", width: 400 },
                                //{ field: "IncomeInterestBondID", title: "IncomeInterestBond (ID)", width: 300 },
                                //{ field: "IncomeInterestBondDesc", title: "IncomeInterestBond (Name)", width: 400 },
                                //{ field: "IncomeInterestTimeDepositID", title: "IncomeInterestTimeDeposit (ID)", width: 300 },
                                //{ field: "IncomeInterestTimeDepositDesc", title: "IncomeInterestTimeDeposit (Name)", width: 400 },
                                //{ field: "PayableSubscriptionFeeID", title: "Payable Subscription Fee (ID)", width: 300 },
                                //{ field: "PayableSubscriptionFeeDesc", title: "Payable Subscription Fee (Name)", width: 400 },
                                //{ field: "IncomeInterestGiroID", title: "IncomeInterestGiro (ID)", width: 300 },
                                //{ field: "IncomeInterestGiroDesc", title: "IncomeInterestGiro (Name)", width: 400 },
                                //{ field: "IncomeDividendID", title: "IncomeDividend (ID)", width: 300 },
                                //{ field: "IncomeDividendDesc", title: "IncomeDividend (Name)", width: 400 },
                                //{ field: "ARDividendID", title: "ARDividend (ID)", width: 300 },
                                //{ field: "ARDividendDesc", title: "ARDividend (Name)", width: 400 },
                                //{ field: "TaxCapitalGainBondID", title: "TaxCapitalGainBond (ID)", width: 300 },
                                //{ field: "TaxCapitalGainBondDesc", title: "TaxCapitalGainBond (Name)", width: 400 },

                                //{ field: "RevaluationBondID", title: "RevaluationBond (ID)", width: 300 },
                                //{ field: "RevaluationBondDesc", title: "RevaluationBond (Name)", width: 400 },
                                //{ field: "RevaluationEquityID", title: "RevaluationEquity (ID)", width: 300 },
                                //{ field: "RevaluationEquityDesc", title: "RevaluationEquity (Name)", width: 400 },
                                //{ field: "RevaluationMutualFundID", title: "RevaluationMutualFund (ID)", width: 300 },
                                //{ field: "RevaluationMutualFundDesc", title: "RevaluationMutualFund (Name)", width: 400 },
                                //{ field: "PayablePurchaseEquityID", title: "PayablePurchaseEquity (ID)", width: 300 },
                                //{ field: "PayablePurchaseEquityDesc", title: "PayablePurchaseEquity (Name)", width: 400 },
                                //{ field: "PayablePurRecBondID", title: "PayablePurRecBond (ID)", width: 300 },
                                //{ field: "PayablePurRecBondDesc", title: "PayablePurRecBond (Name)", width: 400 },
                                //{ field: "PayableManagementFeeID", title: "PayableManagementFee (ID)", width: 300 },
                                //{ field: "PayableManagementFeeDesc", title: "PayableManagementFee (Name)", width: 400 },
                                //{ field: "PayableCustodianFeeID", title: "PayableCustodianFee (ID)", width: 300 },
                                //{ field: "PayableCustodianFeeDesc", title: "PayableCustodianFee (Name)", width: 400 },
                                //{ field: "PayableAuditFeeID", title: "PayableAuditFee (ID)", width: 300 },
                                //{ field: "PayableAuditFeeDesc", title: "PayableAuditFee (Name)", width: 400 },
                                //{ field: "PayableMovementFeeID", title: "PayableMovementFee (ID)", width: 300 },
                                //{ field: "PayableMovementFeeDesc", title: "PayableMovementFee (Name)", width: 400 },
                                //{ field: "BrokerCommissionID", title: "BrokerCommission (ID)", width: 300 },
                                //{ field: "BrokerCommissionDesc", title: "BrokerCommission (Name)", width: 400 },
                                //{ field: "BrokerLevyID", title: "BrokerLevy (ID)", width: 300 },
                                //{ field: "BrokerLevyDesc", title: "BrokerLevy (Name)", width: 400 },
                                //{ field: "BrokerVatID", title: "BrokerVat (ID)", width: 300 },
                                //{ field: "BrokerVatDesc", title: "BrokerVat (Name)", width: 400 },
                                //{ field: "BrokerSalesTaxID", title: "BrokerSalesTax (ID)", width: 300 },
                                //{ field: "BrokerSalesTaxDesc", title: "BrokerSalesTax (Name)", width: 400 },
                                //{ field: "WithHoldingTaxPPH23ID", title: "WithHoldingTaxPPH23 (ID)", width: 300 },
                                //{ field: "WithHoldingTaxPPH23Desc", title: "WithHoldingTaxPPH23 (Name)", width: 400 },
                                //{ field: "WHTTaxPayableAccrInterestBondID", title: "WHTTaxPayableAccrInterestBond (ID)", width: 300 },
                                //{ field: "WHTTaxPayableAccrInterestBondDesc", title: "WHTTaxPayableAccrInterestBond (Name)", width: 400 },

                                //{ field: "ManagementFeeExpenseID", title: "ManagementFeeExpense (ID)", width: 300 },
                                //{ field: "ManagementFeeExpenseDesc", title: "ManagementFeeExpense (Name)", width: 400 },
                                //{ field: "CustodianFeeExpenseID", title: "CustodianFeeExpense (ID)", width: 300 },
                                //{ field: "CustodianFeeExpenseDesc", title: "CustodianFeeExpense (Name)", width: 400 },
                                //{ field: "AuditFeeExpenseID", title: "AuditFeeExpense (ID)", width: 300 },
                                //{ field: "AuditFeeExpenseDesc", title: "AuditFeeExpense (Name)", width: 400 },
                                //{ field: "MovementFeeExpenseID", title: "MovementFeeExpense (ID)", width: 300 },
                                //{ field: "MovementFeeExpenseDesc", title: "MovementFeeExpense (Name)", width: 400 },
                                //{ field: "BankChargesID", title: "BankCharges (ID)", width: 300 },
                                //{ field: "BankChargesDesc", title: "BankCharges (Name)", width: 400 },
                                //{ field: "TaxExpenseInterestIncomeBondID", title: "TaxExpenseInterestIncomeBond (ID)", width: 300 },
                                //{ field: "TaxExpenseInterestIncomeBondDesc", title: "TaxExpenseInterestIncomeBond (Name)", width: 400 },
                                //{ field: "TaxExpenseInterestIncomeTimeDepositID", title: "TaxExpenseInterestIncomeTimeDeposit (ID)", width: 300 },
                                //{ field: "TaxExpenseInterestIncomeTimeDepositDesc", title: "TaxExpenseInterestIncomeTimeDeposit (Name)", width: 400 },
                                //{ field: "RealisedEquityID", title: "RealisedEquity (ID)", width: 300 },
                                //{ field: "RealisedEquityDesc", title: "RealisedEquity (Name)", width: 400 },
                                //{ field: "RealisedBondID", title: "RealisedBond (ID)", width: 300 },
                                //{ field: "RealisedBondDesc", title: "RealisedBond (Name)", width: 400 },
                                //{ field: "RealisedMutualFundID", title: "RealisedMutualFund (ID)", width: 300 },
                                //{ field: "RealisedMutualFundDesc", title: "RealisedMutualFund (Name)", width: 400 },
                                //{ field: "UnrealisedBondID", title: "UnrealisedBond (ID)", width: 300 },
                                //{ field: "UnrealisedBondDesc", title: "UnrealisedBond (Name)", width: 400 },
                                //{ field: "UnrealisedEquityID", title: "UnrealisedEquity (ID)", width: 300 },
                                //{ field: "UnrealisedEquityDesc", title: "UnrealisedEquity (Name)", width: 400 },
                                //{ field: "UnrealisedMutualFundID", title: "UnrealisedMutualFund (ID)", width: 300 },
                                //{ field: "UnrealisedMutualFundDesc", title: "UnrealisedMutualFund (Name)", width: 400 },
                                //{ field: "DistributedIncomeAccID", title: "DistributedIncomeAcc (ID)", width: 300 },
                                //{ field: "DistributedIncomeAccDesc", title: "DistributedIncomeAcc (Name)", width: 400 },
                                //{ field: "DistributedIncomePayableAccID", title: "DistributedIncomePayableAcc (ID)", width: 300 },
                                //{ field: "DistributedIncomePayableAccDesc", title: "DistributedIncomePayableAcc (Name)", width: 400 },
                                //{ field: "PendingSubscriptionID", title: "PendingSubscription (ID)", width: 300 },
                                //{ field: "PendingSubscriptionDesc", title: "PendingSubscription (Name)", width: 400 },
                                //{ field: "PendingRedemptionID", title: "PendingRedemption (ID)", width: 300 },
                                //{ field: "PendingRedemptionDesc", title: "PendingRedemption (Name)", width: 400 },
                                //{ field: "PendingSwitchingID", title: "PendingSwitchingID (ID)", width: 300 },
                                //{ field: "PendingSwitchingIDDesc", title: "PendingSwitchingID (Name)", width: 400 },
                                //{ field: "CurrentYearAccountID", title: "CurrentYearAccount (ID)", width: 300 },
                                //{ field: "CurrentYearAccountDesc", title: "CurrentYearAccount (Name)", width: 400 },
                                //{ field: "PriorYearAccountID", title: "PriorYearAccount (ID)", width: 300 },
                                //{ field: "PriorYearAccountDesc", title: "PriorYearAccount (Name)", width: 400 },
                                //{ field: "BondAmortizationID", title: "BondAmortization (ID)", width: 300 },
                                //{ field: "BondAmortizationDesc", title: "BondAmortization (Name)", width: 400 },

                                //{ field: "BitAccruedInterestGiroDaily", title: "Accrued Interest Giro Daily", width: 150, template: "#= BitAccruedInterestGiroDaily ? 'Yes' : 'No' #" },
                                //{ field: "AveragePriority", title: "AveragePriority", width: 200, template: "#= AveragePriority ? 'Yes' : 'No' #" },

                                { field: "EntryUsersID", title: "Entry ID", width: 200 },
                                { field: "EntryTime", title: "E.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "UpdateUsersID", title: "UpdateID", width: 200 },
                                { field: "UpdateTime", title: "U.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "ApprovedUsersID", title: "ApprovedID", width: 200 },
                                { field: "ApprovedTime", title: "A.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },
                                { field: "VoidUsersID", title: "VoidID", width: 200 },
                                { field: "VoidTime", title: "V.Time", format: "{0:dd/MMM/yyyy HH:mm:ss}", width: 180 },

                                //TAMBAHAN
                                //{ field: "TaxPercentageCapitalGainID", title: "TaxPercentageCapitalGain (ID)", width: 300 },
                                //{ field: "TaxPercentageCapitalGainDesc", title: "TaxPercentageCapitalGain (Name)", width: 400 }

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
        var grid = $("#gridFundAccountingSetupHistory").data("kendoGrid");
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
                        url: window.location.origin + "/Radsoft/Host/CheckAlreadyHasExist/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingSetup" + "/" + $('#FundPK').val() + "/" + "Insert",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if (data == true) {
                                var FundAccountingSetup = {
                                    FundPK: $('#FundPK').val(),
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
                                    TaxInterestBond: $("#TaxInterestBond").val(),
                                    PayableCustodianFee: $("#PayableCustodianFee").val(),
                                    PayableAuditFee: $("#PayableAuditFee").val(),
                                    PayableMovementFee: $("#PayableMovementFee").val(),
                                    BrokerCommission: $("#BrokerCommission").val(),
                                    BrokerLevy: $("#BrokerLevy").val(),
                                    BrokerVat: $("#BrokerVat").val(),
                                    BrokerSalesTax: $("#BrokerSalesTax").val(),
                                    WithHoldingTaxPPH23: $("#WithHoldingTaxPPH23").val(),
                                    WHTTaxPayableAccrInterestBond: $("#WHTTaxPayableAccrInterestBond").val(),
                                    ManagementFeeExpense: $("#ManagementFeeExpense").val(),
                                    CustodianFeeExpense: $("#CustodianFeeExpense").val(),
                                    AuditFeeExpense: $("#AuditFeeExpense").val(),
                                    MovementFeeExpense: $("#MovementFeeExpense").val(),
                                    BankCharges: $("#BankCharges").val(),
                                    TaxExpenseInterestIncomeBond: $("#TaxExpenseInterestIncomeBond").val(),
                                    TaxExpenseInterestIncomeTimeDeposit: $("#TaxExpenseInterestIncomeTimeDeposit").val(),
                                    TaxCapitalGainBond: $("#TaxCapitalGainBond").val(),
                                    PayableOtherFeeThree: $("#PayableOtherFeeThree").val(),
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
                                    TaxProvisionPercent: $("#TaxProvisionPercent").val(),
                                    BondAmortization: $("#BondAmortization").val(),
                                    OtherFeeThreeExpense: $("#OtherFeeThreeExpense").val(),
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

                                    //TAMBAHAN
                                    TaxPercentageCapitalGain: $("#TaxPercentageCapitalGain").val(),
                                    //Switching: $("#Switching").val(),
                                    InterestAccruedCurrentAccount: $("#InterestAccruedCurrentAccount").val(),
                                    IncomeCurrentAccount: $("#IncomeCurrentAccount").val(),
                                    InvInTDUSD: $("#InvInTDUSD").val(),


                                    InvestmentSukuk: $("#InvestmentSukuk").val(),
                                    InterestRecSukuk: $("#InterestRecSukuk").val(),
                                    PayablePurRecSukuk: $("#PayablePurRecSukuk").val(),
                                    WHTTaxPayableAccrInterestSukuk: $("#WHTTaxPayableAccrInterestSukuk").val(),
                                    InterestAccrSukuk: $("#InterestAccrSukuk").val(),


                                    RealisedSukuk: $("#RealisedSukuk").val(),
                                    AccountReceivableSaleSukuk: $("#AccountReceivableSaleSukuk").val(),
                                    TaxCapitalGainSukuk: $("#TaxCapitalGainSukuk").val(),
                                    InvestmentProtectedFund: $("#InvestmentProtectedFund").val(),
                                    PayablePurchaseProtectedFund: $("#PayablePurchaseProtectedFund").val(),
                                    InvestmentPrivateEquityFund: $("#InvestmentPrivateEquityFund").val(),
                                    PayablePurchasePrivateEquityFund: $("#PayablePurchasePrivateEquityFund").val(),
                                    AccountReceivableSaleProtectedFund: $("#AccountReceivableSaleProtectedFund").val(),
                                    AccountReceivableSalePrivateEquityFund: $("#AccountReceivableSalePrivateEquityFund").val(),

                                    RevaluationSukuk: $("#RevaluationSukuk").val(),

                                    UnrealisedSukuk: $("#UnrealisedSukuk").val(),
                                    CashForMutualFund: $("#CashForMutualFund").val(),
                                    RevaluationProtectedFund: $("#RevaluationProtectedFund").val(),
                                    RevaluationPrivateEquityFund: $("#RevaluationPrivateEquityFund").val(),
                                    UnrealisedProtectedFund: $("#UnrealisedProtectedFund").val(),
                                    UnrealisedPrivateEquityFund: $("#UnrealisedPrivateEquityFund").val(),

                                    InterestReceivableSellSukuk: $("#InterestReceivableSellSukuk").val(),



                                    InterestRecSellBond: $("#InterestRecSellBond").val(),

                                    InterestReceivableBuySukuk: $("#InterestReceivableBuySukuk").val(),
                                    InterestReceivableSellMutualFund: $("#InterestReceivableSellMutualFund").val(),
                                    InterestReceivableSellProtectedFund: $("#InterestReceivableSellProtectedFund").val(),
                                    InterestReceivableSellPrivateEquityFund: $("#InterestReceivableSellPrivateEquityFund").val(),

                                    IncomeInterestSukuk: $("#IncomeInterestSukuk").val(),
                                    InterestAccrMutualFund: $("#InterestAccrMutualFund").val(),
                                    InterestAccrProtectedFund: $("#InterestAccrProtectedFund").val(),
                                    InterestAccrPrivateEquityFund: $("#InterestAccrPrivateEquityFund").val(),
                                    IncomeInterestAccrMutualFund: $("#IncomeInterestAccrMutualFund").val(),
                                    IncomeInterestAccrProtectedFund: $("#IncomeInterestAccrProtectedFund").val(),
                                    IncomeInterestAccrPrivateEquityFund: $("#IncomeInterestAccrPrivateEquityFund").val(),
                                    APManagementFee: $("#APManagementFee").val(),

                                    InvestmentRights: $("#InvestmentRights").val(),
                                    PayablePurchaseRights: $("#PayablePurchaseRights").val(),
                                    AccountReceivableSaleRights: $("#AccountReceivableSaleRights").val(),
                                    RealisedRights: $("#RealisedRights").val(),
                                    RevaluationRights: $("#RevaluationRights").val(),
                                    UnrealisedRights: $("#UnrealisedRights").val(),
                                    CashForIPO: $("#CashForIPO").val(),
                                    TaxProvision: $("#TaxProvision").val(),
                                    PayableTaxProvision: $("#PayableTaxProvision").val(),

                                    InvestmentWarrant: $("#InvestmentWarrant").val(),
                                    PayablePurchaseWarrant: $("#PayablePurchaseWarrant").val(),
                                    AccountReceivableSaleWarrant: $("#AccountReceivableSaleWarrant").val(),
                                    RealisedWarrant: $("#RealisedWarrant").val(),
                                    RevaluationWarrant: $("#RevaluationWarrant").val(),
                                    UnrealisedWarrant: $("#UnrealisedWarrant").val(),
                                    CBESTExpense: $("#CBESTExpense").val(),
                                    PayableCBEST: $("#PayableCBEST").val(),

                                    BitAccruedInterestGiroDaily: $('#BitAccruedInterestGiroDaily').is(":checked"),
                                    AveragePriority: $('#AveragePriority').val(),
                                    AveragePriorityBond: $('#AveragePriorityBond').val(),



                                    EntryUsersID: sessionStorage.getItem("user")

                                };

                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingSetup_I",
                                    type: 'POST',
                                    data: JSON.stringify(FundAccountingSetup),
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
                        url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundAccountingSetup",
                        type: 'GET',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {

                                var FundAccountingSetup = {
                                    FundAccountingSetupPK: $('#FundAccountingSetupPK').val(),
                                    HistoryPK: $('#HistoryPK').val(),
                                    FundPK: $('#FundPK').val(),
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
                                    RevaluationBond: $("#RevaluationBond").val(),
                                    RevaluationEquity: $("#RevaluationEquity").val(),
                                    PayablePurchaseEquity: $("#PayablePurchaseEquity").val(),
                                    PayablePurRecBond: $("#PayablePurRecBond").val(),
                                    PayableManagementFee: $("#PayableManagementFee").val(),
                                    ARDividend: $("#ARDividend").val(),
                                    PayableCustodianFee: $("#PayableCustodianFee").val(),
                                    PayableAuditFee: $("#PayableAuditFee").val(),
                                    PayableMovementFee: $("#PayableMovementFee").val(),
                                    BrokerCommission: $("#BrokerCommission").val(),
                                    BrokerLevy: $("#BrokerLevy").val(),
                                    BrokerVat: $("#BrokerVat").val(),
                                    BrokerSalesTax: $("#BrokerSalesTax").val(),
                                    WithHoldingTaxPPH23: $("#WithHoldingTaxPPH23").val(),
                                    WHTTaxPayableAccrInterestBond: $("#WHTTaxPayableAccrInterestBond").val(),
                                    ManagementFeeExpense: $("#ManagementFeeExpense").val(),
                                    CustodianFeeExpense: $("#CustodianFeeExpense").val(),
                                    AuditFeeExpense: $("#AuditFeeExpense").val(),
                                    MovementFeeExpense: $("#MovementFeeExpense").val(),
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
                                    TaxProvisionPercent: $("#TaxProvisionPercent").val(),
                                    BondAmortization: $("#BondAmortization").val(),
                                    TaxInterestBond: $("#TaxInterestBond").val(),
                                    PayablePurchaseMutualFund: $("#PayablePurchaseMutualFund").val(),
                                    InvestmentMutualFund: $("#InvestmentMutualFund").val(),
                                    AccountReceivableSaleMutualFund: $("#AccountReceivableSaleMutualFund").val(),
                                    RevaluationMutualFund: $("#RevaluationMutualFund").val(),
                                    UnrealisedMutualFund: $("#UnrealisedMutualFund").val(),
                                    RealisedMutualFund: $("#RealisedMutualFund").val(),
                                    PayableOtherFeeThree: $("#PayableOtherFeeThree").val(),
                                    PayableOtherFeeOne: $("#PayableOtherFeeOne").val(),
                                    PayableOtherFeeTwo: $("#PayableOtherFeeTwo").val(),
                                    OtherFeeOneExpense: $("#OtherFeeOneExpense").val(),
                                    OtherFeeTwoExpense: $("#OtherFeeTwoExpense").val(),
                                    OtherFeeThreeExpense: $("#OtherFeeThreeExpense").val(),
                                    PendingSwitching: $("#PendingSwitching").val(),
                                    CurrentYearAccount: $("#CurrentYearAccount").val(),
                                    PriorYearAccount: $("#PriorYearAccount").val(),
                                    //TAMBAHAN 
                                    TaxPercentageCapitalGain: $("#TaxPercentageCapitalGain").val(),
                                    //Switching: $("#Switching").val(),
                                    InterestAccruedCurrentAccount: $("#InterestAccruedCurrentAccount").val(),
                                    IncomeCurrentAccount: $("#IncomeCurrentAccount").val(),
                                    InvInTDUSD: $("#InvInTDUSD").val(),


                                    InvestmentSukuk: $("#InvestmentSukuk").val(),
                                    InterestRecSukuk: $("#InterestRecSukuk").val(),
                                    PayablePurRecSukuk: $("#PayablePurRecSukuk").val(),
                                    WHTTaxPayableAccrInterestSukuk: $("#WHTTaxPayableAccrInterestSukuk").val(),
                                    InterestAccrSukuk: $("#InterestAccrSukuk").val(),


                                    RealisedSukuk: $("#RealisedSukuk").val(),
                                    AccountReceivableSaleSukuk: $("#AccountReceivableSaleSukuk").val(),
                                    TaxCapitalGainSukuk: $("#TaxCapitalGainSukuk").val(),
                                    InvestmentProtectedFund: $("#InvestmentProtectedFund").val(),
                                    PayablePurchaseProtectedFund: $("#PayablePurchaseProtectedFund").val(),
                                    InvestmentPrivateEquityFund: $("#InvestmentPrivateEquityFund").val(),
                                    PayablePurchasePrivateEquityFund: $("#PayablePurchasePrivateEquityFund").val(),
                                    AccountReceivableSaleProtectedFund: $("#AccountReceivableSaleProtectedFund").val(),
                                    AccountReceivableSalePrivateEquityFund: $("#AccountReceivableSalePrivateEquityFund").val(),

                                    RevaluationSukuk: $("#RevaluationSukuk").val(),

                                    UnrealisedSukuk: $("#UnrealisedSukuk").val(),
                                    CashForMutualFund: $("#CashForMutualFund").val(),
                                    RevaluationProtectedFund: $("#RevaluationProtectedFund").val(),
                                    RevaluationPrivateEquityFund: $("#RevaluationPrivateEquityFund").val(),
                                    UnrealisedProtectedFund: $("#UnrealisedProtectedFund").val(),
                                    UnrealisedPrivateEquityFund: $("#UnrealisedPrivateEquityFund").val(),

                                    InterestReceivableSellSukuk: $("#InterestReceivableSellSukuk").val(),





                                    InterestRecSellBond: $("#InterestRecSellBond").val(),

                                    InterestReceivableBuySukuk: $("#InterestReceivableBuySukuk").val(),
                                    InterestReceivableSellMutualFund: $("#InterestReceivableSellMutualFund").val(),
                                    InterestReceivableSellProtectedFund: $("#InterestReceivableSellProtectedFund").val(),
                                    InterestReceivableSellPrivateEquityFund: $("#InterestReceivableSellPrivateEquityFund").val(),

                                    IncomeInterestSukuk: $("#IncomeInterestSukuk").val(),
                                    InterestAccrMutualFund: $("#InterestAccrMutualFund").val(),
                                    InterestAccrProtectedFund: $("#InterestAccrProtectedFund").val(),
                                    InterestAccrPrivateEquityFund: $("#InterestAccrPrivateEquityFund").val(),
                                    IncomeInterestAccrMutualFund: $("#IncomeInterestAccrMutualFund").val(),
                                    IncomeInterestAccrProtectedFund: $("#IncomeInterestAccrProtectedFund").val(),
                                    IncomeInterestAccrPrivateEquityFund: $("#IncomeInterestAccrPrivateEquityFund").val(),
                                    APManagementFee: $("#APManagementFee").val(),

                                    InvestmentRights: $("#InvestmentRights").val(),
                                    PayablePurchaseRights: $("#PayablePurchaseRights").val(),
                                    AccountReceivableSaleRights: $("#AccountReceivableSaleRights").val(),
                                    RealisedRights: $("#RealisedRights").val(),
                                    RevaluationRights: $("#RevaluationRights").val(),
                                    UnrealisedRights: $("#UnrealisedRights").val(),
                                    CashForIPO: $("#CashForIPO").val(),
                                    TaxProvision: $("#TaxProvision").val(),
                                    PayableTaxProvision: $("#PayableTaxProvision").val(),

                                    InvestmentWarrant: $("#InvestmentWarrant").val(),
                                    PayablePurchaseWarrant: $("#PayablePurchaseWarrant").val(),
                                    AccountReceivableSaleWarrant: $("#AccountReceivableSaleWarrant").val(),
                                    RealisedWarrant: $("#RealisedWarrant").val(),
                                    RevaluationWarrant: $("#RevaluationWarrant").val(),
                                    UnrealisedWarrant: $("#UnrealisedWarrant").val(),
                                    CBESTExpense: $("#CBESTExpense").val(),
                                    PayableCBEST: $("#PayableCBEST").val(),

                                    BitAccruedInterestGiroDaily: $('#BitAccruedInterestGiroDaily').is(":checked"),
                                    AveragePriority: $('#AveragePriority').val(),
                                    AveragePriorityBond: $('#AveragePriorityBond').val(),
                                    TaxPercentageCapitalGainSell: $("#TaxPercentageCapitalGainSell").val(),


                                    Notes: str,
                                    EntryUsersID: sessionStorage.getItem("user")
                                };
                                $.ajax({
                                    url: window.location.origin + "/Radsoft/FundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingSetup_U",
                                    type: 'POST',
                                    data: JSON.stringify(FundAccountingSetup),
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
            url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundAccountingSetup",
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
                                                url: window.location.origin + "/Radsoft/Host/GetCompareData/" + "FundAccountingSetup" + "/" + $("#FundAccountingSetupPK").val(),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundAccountingSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundAccountingSetup = {
                                FundAccountingSetupPK: $('#FundAccountingSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                ApprovedUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingSetup_A",
                                type: 'POST',
                                data: JSON.stringify(FundAccountingSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundAccountingSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundAccountingSetup = {
                                FundAccountingSetupPK: $('#FundAccountingSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingSetup_V",
                                type: 'POST',
                                data: JSON.stringify(FundAccountingSetup),
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
                    url: window.location.origin + "/Radsoft/Host/GetLastUpdate/" + sessionStorage.getItem("user") + "/" + $("#FundAccountingSetupPK").val() + "/" + $("#HistoryPK").val() + "/" + "FundAccountingSetup",
                    type: 'GET',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if ($("#LastUpdate").val() == kendo.toString(kendo.parseDate(data), 'dd/MMM/yyyy HH:mm:ss')) {
                            var FundAccountingSetup = {
                                FundAccountingSetupPK: $('#FundAccountingSetupPK').val(),
                                HistoryPK: $('#HistoryPK').val(),
                                VoidUsersID: sessionStorage.getItem("user")
                            };
                            $.ajax({
                                url: window.location.origin + "/Radsoft/FundAccountingSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingSetup_R",
                                type: 'POST',
                                data: JSON.stringify(FundAccountingSetup),
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

    function RefreshTab(_index) {
        //$("#TabSub").data("kendoTabStrip").select(_index);
        GlobTabStrip.select(_index);
    }

    $("#BtnCopyFundAccountingSetup").click(function () {
        alertify.confirm("Are you sure want to Copy data?", function () {
                var FundAccountingSetup = {
                    FundAccountingSetupPK: $('#FundAccountingSetupPK').val(),
                    FundPK: $('#FundPK').val(),
                    HistoryPK: $('#HistoryPK').val(),
                    ApprovedUsersID: sessionStorage.getItem("user")
                };
                $.ajax({
                    url: window.location.origin + "/Radsoft/FundAccountSetup/Action/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + "FundAccountingSetup_A",
                    type: 'POST',
                    data: JSON.stringify(FundAccountingSetup),
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

          
        }, function ()
        {
            WinCopyFundAccountingSetup.close();
        });
    });

    $("#BtnWinCopyFundAccountingSetup").click(function () { 

        showWinCopyFundAccountingSetup();
    });

    function showWinCopyFundAccountingSetup(e) {
        
        $.ajax({
            url: window.location.origin + "/Radsoft/Fund/GetFundCombo/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#ParamFundFrom").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
                });

                $("#ParamFundTo").kendoComboBox({
                    dataValueField: "FundPK",
                    dataTextField: "ID",
                    filter: "contains",
                    suggest: true,
                    dataSource: data,
                    change: OnChangeFundPK,
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



        WinCopyFundAccountingSetup.center();
        WinCopyFundAccountingSetup.open();

    }



    $("#BtnOkCopyFundAccountingSetup").click(function () {
        $.blockUI({});


        alertify.confirm("Are you sure want to Copy Data Fund Accounting Setup ?", function (e) {


            $.ajax({
                url: window.location.origin + "/Radsoft/FundAccountingSetup/ValidateCheckCopyFundAccountingSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id") + "/" + $("#ParamFundTo").val(),
                type: 'GET',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data == "") {

                        var FundAccountinSetup = {
                            ParamFundFrom: $('#ParamFundFrom').val(),
                            ParamFundTo: $('#ParamFundTo').val(),
                            //BitDefaultFund: $('#BitDefaultFund').is(":checked"),
                            EntryUsersID: sessionStorage.getItem("user")

                        };
                        $.ajax({
                            url: window.location.origin + "/Radsoft/FundAccountingSetup/CopyFundAccountingSetup/" + sessionStorage.getItem("user") + "/" + sessionStorage.getItem("id"),
                            type: 'POST',
                            data: JSON.stringify(FundAccountinSetup),
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {

                                refresh();
                                $.unblockUI();
                                WinCopyFundAccountingSetup.close();
                                alertify.alert(data);

                            },
                            error: function (data) {
                                alertify.alert(data.responseText);
                                $.unblockUI();
                            }
                        });

                    } else {
                        alertify.alert(data);
                        $.unblockUI();
                    }
                },
                error: function (data) {
                    alertify.alert(data.responseText);
                    $.unblockUI();
                }
            });

        }, function () {
                WinCopyFundAccountingSetup.close();
                $.unblockUI();
            });
    });

    $("#BtnCancelCopyFundAccountingSetup").click(function () {
        
        alertify.confirm("Are you sure want to Copy Data Fund Accounting Setup ?", function (e) {
            if (e) {
                WinCopyFundAccountingSetup.close();
                alertify.alert("Cancel Copy Fund Accounting Setup ");
            }
        });
    });
});
