using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;


namespace RFSRepository
{
    public class FundAccountingSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"INSERT INTO [dbo].[FundAccountingSetup]
                            ([FundAccountingSetupPK],[HistoryPK],[Status],[FundPK],[Subscription],[PayableSubscriptionFee],[Redemption],[PayableRedemptionFee],[TaxPercentageDividend],[TaxPercentageBond],[TaxPercentageTD],[TaxProvisionPercent],[Switching],[PayableSwitchingFee],[PayableSInvestFee],[SInvestFee],
                            [InvestmentEquity],[InvestmentBond],[InvestmentTimeDeposit],[InterestRecBond],[InterestAccrBond],[InterestAccrTimeDeposit],[InterestAccrGiro],   
                            [PrepaidTaxDividend],[AccountReceivableSaleBond],[AccountReceivableSaleEquity],[AccountReceivableSaleTimeDeposit],[IncomeInterestBond],[IncomeInterestTimeDeposit],[IncomeInterestGiro],
                            [IncomeDividend],[ARDividend],[RevaluationBond],[RevaluationEquity],[PayablePurchaseEquity],[PayablePurRecBond],[PayableManagementFee],[PayableCustodianFee],
                            [PayableAuditFee],[BrokerCommission],[BrokerLevy],[BrokerVat],[BrokerSalesTax],[WithHoldingTaxPPH23],[WHTTaxPayableAccrInterestBond],
                            [ManagementFeeExpense],[CustodianFeeExpense],[AuditFeeExpense],[BankCharges],[TaxExpenseInterestIncomeBond],[TaxExpenseInterestIncomeTimeDeposit],[RealisedEquity],[RealisedBond],
                            [UnrealisedBond],[UnrealisedEquity],[DistributedIncomeAcc],[DistributedIncomePayableAcc],[PendingSubscription],[PendingRedemption],[TaxCapitalGainBond],[BondAmortization],[PayableMovementFee],[MovementFeeExpense],
                            [PayablePurchaseMutualFund],[InvestmentMutualFund],[AccountReceivableSaleMutualFund],[RevaluationMutualFund],[UnrealisedMutualFund],[RealisedMutualFund],[PayableOtherFeeOne],[PayableOtherFeeTwo],[OtherFeeOneExpense],[OtherFeeTwoExpense],[PendingSwitching],
                            [CurrentYearAccount],[PriorYearAccount],[TaxPercentageCapitalGain],[InterestAccruedCurrentAccount],[IncomeCurrentAccount],[InvInTDUSD],
                            [InvestmentSukuk],[InterestRecSukuk],[PayablePurRecSukuk],
                            [WHTTaxPayableAccrInterestSukuk],[InterestAccrSukuk],[RealisedSukuk],[AccountReceivableSaleSukuk],[TaxCapitalGainSukuk],[InvestmentProtectedFund],[PayablePurchaseProtectedFund],[InvestmentPrivateEquityFund],
                            [PayablePurchasePrivateEquityFund],[AccountReceivableSaleProtectedFund],[AccountReceivableSalePrivateEquityFund],
                            [RevaluationSukuk],[UnrealisedSukuk],[CashForMutualFund],[RevaluationProtectedFund],[RevaluationPrivateEquityFund],[UnrealisedProtectedFund],[UnrealisedPrivateEquityFund],
                            [InterestReceivableSellSukuk],                           
                            [InterestRecSellBond],[InterestReceivableBuySukuk],[InterestReceivableSellMutualFund],[InterestReceivableSellProtectedFund],[InterestReceivableSellPrivateEquityFund],
                            [IncomeInterestSukuk],[InterestAccrMutualFund],[InterestAccrProtectedFund],[InterestAccrPrivateEquityFund],[IncomeInterestAccrMutualFund],[IncomeInterestAccrProtectedFund],[IncomeInterestAccrPrivateEquityFund],[APManagementFee],
                            [InvestmentRights],[PayablePurchaseRights],[AccountReceivableSaleRights],[RealisedRights],[RevaluationRights],[UnrealisedRights],[CashForIPO],[BitAccruedInterestGiroDaily],[AveragePriority],[AveragePriorityBond],[PayableOtherFeeThree],[OtherFeeThreeExpense],[TaxInterestBond],[TaxProvision],[PayableTaxProvision],
                            [InvestmentWarrant],[PayablePurchaseWarrant],[AccountReceivableSaleWarrant],[RealisedWarrant],[RevaluationWarrant],[UnrealisedWarrant],[CBESTExpense],[PayableCBEST],[TaxPercentageCapitalGainSell],
                              ";

        string _paramaterCommand = @" @FundPK,@Subscription,@PayableSubscriptionFee,@Redemption,@PayableRedemptionFee,@TaxPercentageDividend,@TaxPercentageBond,@TaxPercentageTD,@TaxProvisionPercent,@Switching,@PayableSwitchingFee,@PayableSInvestFee,@SInvestFee,
                                      @InvestmentEquity,@InvestmentBond,@InvestmentTimeDeposit,@InterestRecBond,@InterestAccrBond,@InterestAccrTimeDeposit,@InterestAccrGiro,
                                      @PrepaidTaxDividend,@AccountReceivableSaleBond,@AccountReceivableSaleEquity,@AccountReceivableSaleTimeDeposit,@IncomeInterestBond,@IncomeInterestTimeDeposit,@IncomeInterestGiro,@IncomeDividend,@ARDividend,
                                      @RevaluationBond,@RevaluationEquity,@PayablePurchaseEquity,@PayablePurRecBond,@PayableManagementFee,@PayableCustodianFee,@PayableAuditFee,@BrokerCommission,@BrokerLevy,@BrokerVat,@BrokerSalesTax,
                                      @WithHoldingTaxPPH23,@WHTTaxPayableAccrInterestBond,@ManagementFeeExpense,@CustodianFeeExpense,@AuditFeeExpense,@BankCharges,@TaxExpenseInterestIncomeBond,@TaxExpenseInterestIncomeTimeDeposit,@RealisedEquity,@RealisedBond,
                                      @UnrealisedBond,@UnrealisedEquity,@DistributedIncomeAcc,@DistributedIncomePayableAcc,@PendingSubscription,@PendingRedemption,@TaxCapitalGainBond,@BondAmortization,@PayableMovementFee,@MovementFeeExpense,@PayablePurchaseMutualFund,
                                      @InvestmentMutualFund,@AccountReceivableSaleMutualFund,@RevaluationMutualFund,@UnrealisedMutualFund,@RealisedMutualFund,@PayableOtherFeeOne,@PayableOtherFeeTwo,@OtherFeeOneExpense,@OtherFeeTwoExpense,@PendingSwitching,@CurrentYearAccount,
                                      @PriorYearAccount,@TaxPercentageCapitalGain,@InterestAccruedCurrentAccount,@IncomeCurrentAccount,@InvInTDUSD,
                                      @InvestmentSukuk,@InterestRecSukuk,@PayablePurRecSukuk,@WHTTaxPayableAccrInterestSukuk,
                                      @InterestAccrSukuk,@RealisedSukuk,@AccountReceivableSaleSukuk,@TaxCapitalGainSukuk,@InvestmentProtectedFund,@PayablePurchaseProtectedFund,@InvestmentPrivateEquityFund,@PayablePurchasePrivateEquityFund,@AccountReceivableSaleProtectedFund,
                                      @AccountReceivableSalePrivateEquityFund,@RevaluationSukuk,@UnrealisedSukuk,@CashForMutualFund,@RevaluationProtectedFund,
                                      @RevaluationPrivateEquityFund,@UnrealisedProtectedFund,@UnrealisedPrivateEquityFund,@InterestReceivableSellSukuk,                                   
                                      @InterestRecSellBond,@InterestReceivableBuySukuk,@InterestReceivableSellMutualFund,
                                      @InterestReceivableSellProtectedFund,@InterestReceivableSellPrivateEquityFund,@IncomeInterestSukuk,@InterestAccrMutualFund,@InterestAccrProtectedFund,@InterestAccrPrivateEquityFund,@IncomeInterestAccrMutualFund,@IncomeInterestAccrProtectedFund,
                                      @IncomeInterestAccrPrivateEquityFund,@APManagementFee,@InvestmentRights,@PayablePurchaseRights,@AccountReceivableSaleRights,@RealisedRights,@RevaluationRights,@UnrealisedRights,@CashForIPO,@BitAccruedInterestGiroDaily,@AveragePriority,@AveragePriorityBond,@PayableOtherFeeThree,@OtherFeeThreeExpense,@TaxInterestBond,@TaxProvision,@PayableTaxProvision,
                                      @InvestmentWarrant,@PayablePurchaseWarrant,@AccountReceivableSaleWarrant,@RealisedWarrant,@RevaluationWarrant,@UnrealisedWarrant,@CBESTExpense,@PayableCBEST,@TaxPercentageCapitalGainSell,
                                        ";

        //2
        private FundAccountingSetup setFundAccountingSetup(SqlDataReader dr)
        {
            FundAccountingSetup M_FundAccountingSetup = new FundAccountingSetup();
            M_FundAccountingSetup.FundAccountingSetupPK = Convert.ToInt32(dr["FundAccountingSetupPK"]);
            M_FundAccountingSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundAccountingSetup.Status = Convert.ToInt32(dr["Status"]);
            M_FundAccountingSetup.StatusDesc = dr["StatusDesc"].ToString();
            M_FundAccountingSetup.Notes = dr["Notes"].ToString();
            M_FundAccountingSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundAccountingSetup.FundID = dr["FundID"].ToString();
            M_FundAccountingSetup.FundName = dr["FundName"].ToString();
            M_FundAccountingSetup.Subscription = Convert.ToInt32(dr["Subscription"]);
            M_FundAccountingSetup.PayableSubscriptionFee = Convert.ToInt32(dr["PayableSubscriptionFee"]);
            M_FundAccountingSetup.Redemption = Convert.ToInt32(dr["Redemption"]);
            M_FundAccountingSetup.TaxInterestBond = Convert.ToInt32(dr["TaxInterestBond"]);
            M_FundAccountingSetup.PayableRedemptionFee = Convert.ToInt32(dr["PayableRedemptionFee"]);
            M_FundAccountingSetup.Switching = Convert.ToInt32(dr["Switching"]);
            M_FundAccountingSetup.PayableSwitchingFee = Convert.ToInt32(dr["PayableSwitchingFee"]);
            M_FundAccountingSetup.InvestmentEquity = Convert.ToInt32(dr["InvestmentEquity"]);
            M_FundAccountingSetup.InvestmentBond = Convert.ToInt32(dr["InvestmentBond"]);
            M_FundAccountingSetup.InvestmentTimeDeposit = Convert.ToInt32(dr["InvestmentTimeDeposit"]);
            M_FundAccountingSetup.InterestRecBond = Convert.ToInt32(dr["InterestRecBond"]);
            M_FundAccountingSetup.InterestAccrBond = Convert.ToInt32(dr["InterestAccrBond"]);
            M_FundAccountingSetup.InterestAccrTimeDeposit = Convert.ToInt32(dr["InterestAccrTimeDeposit"]);
            M_FundAccountingSetup.InterestAccrGiro = Convert.ToInt32(dr["InterestAccrGiro"]);
            M_FundAccountingSetup.PrepaidTaxDividend = Convert.ToInt32(dr["PrepaidTaxDividend"]);
            M_FundAccountingSetup.AccountReceivableSaleBond = Convert.ToInt32(dr["AccountReceivableSaleBond"]);
            M_FundAccountingSetup.AccountReceivableSaleEquity = Convert.ToInt32(dr["AccountReceivableSaleEquity"]);
            M_FundAccountingSetup.AccountReceivableSaleTimeDeposit = Convert.ToInt32(dr["AccountReceivableSaleTimeDeposit"]);
            M_FundAccountingSetup.OtherFeeThreeExpense = Convert.ToInt32(dr["OtherFeeThreeExpense"]);
            M_FundAccountingSetup.IncomeInterestBond = Convert.ToInt32(dr["IncomeInterestBond"]);
            M_FundAccountingSetup.IncomeInterestTimeDeposit = Convert.ToInt32(dr["IncomeInterestTimeDeposit"]);
            M_FundAccountingSetup.IncomeInterestGiro = Convert.ToInt32(dr["IncomeInterestGiro"]);
            M_FundAccountingSetup.IncomeDividend = Convert.ToInt32(dr["IncomeDividend"]);
            M_FundAccountingSetup.ARDividend = Convert.ToInt32(dr["ARDividend"]);
            M_FundAccountingSetup.RevaluationBond = Convert.ToInt32(dr["RevaluationBond"]);
            M_FundAccountingSetup.RevaluationEquity = Convert.ToInt32(dr["RevaluationEquity"]);
            M_FundAccountingSetup.PayablePurchaseEquity = Convert.ToInt32(dr["PayablePurchaseEquity"]);
            M_FundAccountingSetup.PayablePurRecBond = Convert.ToInt32(dr["PayablePurRecBond"]);
            M_FundAccountingSetup.PayableManagementFee = Convert.ToInt32(dr["PayableManagementFee"]);
            M_FundAccountingSetup.PayableCustodianFee = Convert.ToInt32(dr["PayableCustodianFee"]);
            M_FundAccountingSetup.PayableAuditFee = Convert.ToInt32(dr["PayableAuditFee"]);
            M_FundAccountingSetup.PayableMovementFee = dr["PayableMovementFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayableMovementFee"]);
            M_FundAccountingSetup.BrokerCommission = Convert.ToInt32(dr["BrokerCommission"]);
            M_FundAccountingSetup.BrokerLevy = Convert.ToInt32(dr["BrokerLevy"]);
            M_FundAccountingSetup.BrokerVat = Convert.ToInt32(dr["BrokerVat"]);
            M_FundAccountingSetup.BrokerSalesTax = Convert.ToInt32(dr["BrokerSalesTax"]);
            M_FundAccountingSetup.WithHoldingTaxPPH23 = Convert.ToInt32(dr["WithHoldingTaxPPH23"]);
            M_FundAccountingSetup.WHTTaxPayableAccrInterestBond = Convert.ToInt32(dr["WHTTaxPayableAccrInterestBond"]);
            M_FundAccountingSetup.ManagementFeeExpense = Convert.ToInt32(dr["ManagementFeeExpense"]);
            M_FundAccountingSetup.CustodianFeeExpense = Convert.ToInt32(dr["CustodianFeeExpense"]);
            M_FundAccountingSetup.AuditFeeExpense = Convert.ToInt32(dr["AuditFeeExpense"]);
            M_FundAccountingSetup.MovementFeeExpense = dr["MovementFeeExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["MovementFeeExpense"]);
            M_FundAccountingSetup.BankCharges = Convert.ToInt32(dr["BankCharges"]);
            M_FundAccountingSetup.TaxExpenseInterestIncomeBond = Convert.ToInt32(dr["TaxExpenseInterestIncomeBond"]);
            M_FundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit = Convert.ToInt32(dr["TaxExpenseInterestIncomeTimeDeposit"]);
            M_FundAccountingSetup.RealisedEquity = Convert.ToInt32(dr["RealisedEquity"]);
            M_FundAccountingSetup.RealisedBond = Convert.ToInt32(dr["RealisedBond"]);
            M_FundAccountingSetup.UnrealisedBond = Convert.ToInt32(dr["UnrealisedBond"]);
            M_FundAccountingSetup.UnrealisedEquity = Convert.ToInt32(dr["UnrealisedEquity"]);
            M_FundAccountingSetup.DistributedIncomeAcc = Convert.ToInt32(dr["DistributedIncomeAcc"]);
            M_FundAccountingSetup.DistributedIncomePayableAcc = Convert.ToInt32(dr["DistributedIncomePayableAcc"]);
            M_FundAccountingSetup.TaxCapitalGainBond = Convert.ToInt32(dr["TaxCapitalGainBond"]);
            M_FundAccountingSetup.PendingSubscription = Convert.ToInt32(dr["PendingSubscription"]);
            M_FundAccountingSetup.PendingRedemption = Convert.ToInt32(dr["PendingRedemption"]);
            M_FundAccountingSetup.TaxPercentageDividend = Convert.ToDecimal(dr["TaxPercentageDividend"]);
            M_FundAccountingSetup.TaxPercentageBond = Convert.ToDecimal(dr["TaxPercentageBond"]);
            M_FundAccountingSetup.TaxPercentageTD = Convert.ToDecimal(dr["TaxPercentageTD"]);
            M_FundAccountingSetup.TaxProvisionPercent = Convert.ToDecimal(dr["TaxProvisionPercent"]);
            M_FundAccountingSetup.PayableSInvestFee = Convert.ToInt32(dr["PayableSInvestFee"]);
            M_FundAccountingSetup.SInvestFee = Convert.ToInt32(dr["SInvestFee"]);
            M_FundAccountingSetup.BondAmortization = Convert.ToInt32(dr["BondAmortization"]);
            M_FundAccountingSetup.PayablePurchaseMutualFund = dr["PayablePurchaseMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayablePurchaseMutualFund"]);
            M_FundAccountingSetup.InvestmentMutualFund = dr["InvestmentMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvestmentMutualFund"]);
            M_FundAccountingSetup.AccountReceivableSaleMutualFund = dr["AccountReceivableSaleMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccountReceivableSaleMutualFund"]);
            M_FundAccountingSetup.RevaluationMutualFund = dr["RevaluationMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RevaluationMutualFund"]);
            M_FundAccountingSetup.UnrealisedMutualFund = dr["UnrealisedMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedMutualFund"]);
            M_FundAccountingSetup.RealisedMutualFund = dr["RealisedMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedMutualFund"]);
            M_FundAccountingSetup.PayableOtherFeeOne = dr["PayableOtherFeeOne"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayableOtherFeeOne"]);
            M_FundAccountingSetup.PayableOtherFeeTwo = dr["PayableOtherFeeTwo"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayableOtherFeeTwo"]);
            M_FundAccountingSetup.OtherFeeOneExpense = dr["OtherFeeOneExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["OtherFeeOneExpense"]);
            M_FundAccountingSetup.OtherFeeTwoExpense = dr["OtherFeeTwoExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["OtherFeeTwoExpense"]);
            M_FundAccountingSetup.PendingSwitching = dr["PendingSwitching"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PendingSwitching"]);
            M_FundAccountingSetup.CurrentYearAccount = dr["CurrentYearAccount"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CurrentYearAccount"]);
            M_FundAccountingSetup.PriorYearAccount = dr["PriorYearAccount"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PriorYearAccount"]);
            M_FundAccountingSetup.TaxPercentageCapitalGain = Convert.ToInt32(dr["TaxPercentageCapitalGain"]);
            M_FundAccountingSetup.InterestAccruedCurrentAccount = Convert.ToInt32(dr["InterestAccruedCurrentAccount"]);
            M_FundAccountingSetup.IncomeCurrentAccount = Convert.ToInt32(dr["IncomeCurrentAccount"]);
            M_FundAccountingSetup.InvInTDUSD = dr["InvInTDUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInTDUSD"]);
            M_FundAccountingSetup.InvestmentSukuk = dr["InvestmentSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvestmentSukuk"]);
            M_FundAccountingSetup.InterestRecSukuk = dr["InterestRecSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestRecSukuk"]);
            M_FundAccountingSetup.PayablePurRecSukuk = dr["PayablePurRecSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayablePurRecSukuk"]);
            M_FundAccountingSetup.WHTTaxPayableAccrInterestSukuk = dr["WHTTaxPayableAccrInterestSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["WHTTaxPayableAccrInterestSukuk"]);
            M_FundAccountingSetup.InterestAccrSukuk = dr["InterestAccrSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestAccrSukuk"]);
            M_FundAccountingSetup.RealisedSukuk = dr["RealisedSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedSukuk"]);
            M_FundAccountingSetup.AccountReceivableSaleSukuk = dr["AccountReceivableSaleSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccountReceivableSaleSukuk"]);
            M_FundAccountingSetup.TaxCapitalGainSukuk = dr["TaxCapitalGainSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TaxCapitalGainSukuk"]);
            M_FundAccountingSetup.InvestmentProtectedFund = dr["InvestmentProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvestmentProtectedFund"]);
            M_FundAccountingSetup.PayablePurchaseProtectedFund = dr["PayablePurchaseProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayablePurchaseProtectedFund"]);
            M_FundAccountingSetup.InvestmentPrivateEquityFund = dr["InvestmentPrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvestmentPrivateEquityFund"]);
            M_FundAccountingSetup.PayablePurchasePrivateEquityFund = dr["PayablePurchasePrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayablePurchasePrivateEquityFund"]);
            M_FundAccountingSetup.AccountReceivableSaleProtectedFund = dr["AccountReceivableSaleProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccountReceivableSaleProtectedFund"]);
            M_FundAccountingSetup.AccountReceivableSalePrivateEquityFund = dr["AccountReceivableSalePrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccountReceivableSalePrivateEquityFund"]);
            M_FundAccountingSetup.RevaluationSukuk = dr["RevaluationSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RevaluationSukuk"]);
            M_FundAccountingSetup.UnrealisedSukuk = dr["UnrealisedSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedSukuk"]);
            M_FundAccountingSetup.CashForMutualFund = dr["CashForMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CashForMutualFund"]);
            M_FundAccountingSetup.RevaluationProtectedFund = dr["RevaluationProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RevaluationProtectedFund"]);
            M_FundAccountingSetup.RevaluationPrivateEquityFund = dr["RevaluationPrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RevaluationPrivateEquityFund"]);
            M_FundAccountingSetup.UnrealisedProtectedFund = dr["UnrealisedProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedProtectedFund"]);
            M_FundAccountingSetup.UnrealisedPrivateEquityFund = dr["UnrealisedPrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedPrivateEquityFund"]);
            M_FundAccountingSetup.InterestReceivableSellSukuk = dr["InterestReceivableSellSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableSellSukuk"]);
            M_FundAccountingSetup.InterestRecSellBond = dr["InterestRecSellBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestRecSellBond"]);
            M_FundAccountingSetup.InterestReceivableBuySukuk = dr["InterestReceivableBuySukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableBuySukuk"]);
            M_FundAccountingSetup.InterestReceivableSellMutualFund = dr["InterestReceivableSellMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableSellMutualFund"]);
            M_FundAccountingSetup.InterestReceivableSellProtectedFund = dr["InterestReceivableSellProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableSellProtectedFund"]);
            M_FundAccountingSetup.InterestReceivableSellPrivateEquityFund = dr["InterestReceivableSellPrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableSellPrivateEquityFund"]);
            M_FundAccountingSetup.IncomeInterestSukuk = dr["IncomeInterestSukuk"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IncomeInterestSukuk"]);
            M_FundAccountingSetup.InterestAccrMutualFund = dr["InterestAccrMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestAccrMutualFund"]);
            M_FundAccountingSetup.InterestAccrProtectedFund = dr["InterestAccrProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestAccrProtectedFund"]);
            M_FundAccountingSetup.InterestAccrPrivateEquityFund = dr["InterestAccrPrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestAccrPrivateEquityFund"]);
            M_FundAccountingSetup.IncomeInterestAccrMutualFund = dr["IncomeInterestAccrMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IncomeInterestAccrMutualFund"]);
            M_FundAccountingSetup.IncomeInterestAccrProtectedFund = dr["IncomeInterestAccrProtectedFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IncomeInterestAccrProtectedFund"]);
            M_FundAccountingSetup.IncomeInterestAccrPrivateEquityFund = dr["IncomeInterestAccrPrivateEquityFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IncomeInterestAccrPrivateEquityFund"]);
            M_FundAccountingSetup.APManagementFee = dr["APManagementFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["APManagementFee"]);
            M_FundAccountingSetup.InvestmentRights = dr["InvestmentRights"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvestmentRights"]);
            M_FundAccountingSetup.PayablePurchaseRights = dr["PayablePurchaseRights"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayablePurchaseRights"]);
            M_FundAccountingSetup.AccountReceivableSaleRights = dr["AccountReceivableSaleRights"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccountReceivableSaleRights"]);
            M_FundAccountingSetup.RealisedRights = dr["RealisedRights"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedRights"]);
            M_FundAccountingSetup.RevaluationRights = dr["RevaluationRights"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RevaluationRights"]);
            M_FundAccountingSetup.UnrealisedRights = dr["UnrealisedRights"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedRights"]);
            M_FundAccountingSetup.PayableOtherFeeThree = dr["PayableOtherFeeThree"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayableOtherFeeThree"]);
            M_FundAccountingSetup.CashForIPO = Convert.ToInt32(dr["CashForIPO"]);
            M_FundAccountingSetup.PriorYearAccount = dr["PriorYearAccount"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PriorYearAccount"]);
            M_FundAccountingSetup.BitAccruedInterestGiroDaily = dr["BitAccruedInterestGiroDaily"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitAccruedInterestGiroDaily"]);
            M_FundAccountingSetup.AveragePriority = dr["AveragePriority"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AveragePriority"]);
            M_FundAccountingSetup.AveragePriorityBond = dr["AveragePriorityBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AveragePriorityBond"]);
            M_FundAccountingSetup.TaxProvision = dr["TaxProvision"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TaxProvision"]);
            M_FundAccountingSetup.PayableTaxProvision = dr["PayableTaxProvision"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayableTaxProvision"]);
            M_FundAccountingSetup.RealisedMutualFund = dr["RealisedMutualFund"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedMutualFund"]);
            M_FundAccountingSetup.InvestmentWarrant = dr["InvestmentWarrant"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvestmentWarrant"]);
            M_FundAccountingSetup.PayablePurchaseWarrant = dr["PayablePurchaseWarrant"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayablePurchaseWarrant"]);
            M_FundAccountingSetup.AccountReceivableSaleWarrant = dr["AccountReceivableSaleWarrant"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccountReceivableSaleWarrant"]);
            M_FundAccountingSetup.RealisedWarrant = dr["RealisedWarrant"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedWarrant"]);
            M_FundAccountingSetup.RevaluationWarrant = dr["RevaluationWarrant"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RevaluationWarrant"]);
            M_FundAccountingSetup.UnrealisedWarrant = dr["UnrealisedWarrant"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedWarrant"]);
            M_FundAccountingSetup.CBESTExpense = dr["CBESTExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CBESTExpense"]);
            M_FundAccountingSetup.PayableCBEST = dr["PayableCBEST"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PayableCBEST"]);
            M_FundAccountingSetup.TaxPercentageCapitalGainSell = dr["TaxPercentageCapitalGainSell"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TaxPercentageCapitalGainSell"]);

            M_FundAccountingSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundAccountingSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundAccountingSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundAccountingSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundAccountingSetup.EntryTime = dr["EntryTime"].ToString();
            M_FundAccountingSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_FundAccountingSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundAccountingSetup.VoidTime = dr["VoidTime"].ToString();
            M_FundAccountingSetup.DBUserID = dr["DBUserID"].ToString();
            M_FundAccountingSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundAccountingSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_FundAccountingSetup.LastUpdateDB = dr["LastUpdateDB"].ToString();
            return M_FundAccountingSetup;
        }

        public List<FundAccountingSetup> FundAccountingSetup_Select(int _status) 
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundAccountingSetup> L_fundAccountingSetup = new List<FundAccountingSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select  case when F.status=1 then 'PENDING' else Case When F.status = 2 then 'APPROVED' else Case when F.Status = 3 then 
                            'VOID' else 'WAITING' END END END StatusDesc,FU.ID FundID, FU.Name FUndName,
                            F.* from FundAccountingSetup F left join 
                            Fund FU on F.FundPK = FU.FundPK and FU.status = 2 
                            where F.status = @status  order by FundAccountingSetupPK DESC";
                            cmd.Parameters.AddWithValue("@status", _status);
                            cmd.CommandTimeout = 0;
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"Select  case when F.status=1 then 'PENDING' else Case When F.status = 2 then 'APPROVED' else Case when F.Status = 3 then 
                            'VOID' else 'WAITING' END END END StatusDesc,FU.ID FundID, FU.Name FUndName,
                            F.* from FundAccountingSetup F left join                               
                            Fund FU on F.FundPK = FU.FundPK and FU.status = 2 
                            order by FundAccountingSetupPK desc"; 
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_fundAccountingSetup.Add(setFundAccountingSetup(dr));
                                }
                            }
                            return L_fundAccountingSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundAccountingSetup_Add(FundAccountingSetup _fundAccountingSetup, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(FundAccountingSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundAccountingSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundAccountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundAccountingSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundAccountingSetup";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _fundAccountingSetup.FundPK);
                        cmd.Parameters.AddWithValue("@Subscription", _fundAccountingSetup.Subscription);
                        cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _fundAccountingSetup.PayableSubscriptionFee);
                        cmd.Parameters.AddWithValue("@Redemption", _fundAccountingSetup.Redemption);
                        cmd.Parameters.AddWithValue("@PayableRedemptionFee", _fundAccountingSetup.PayableRedemptionFee);
                        cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                        cmd.Parameters.AddWithValue("@PayableSwitchingFee", _fundAccountingSetup.PayableSwitchingFee);
                        cmd.Parameters.AddWithValue("@InvestmentEquity", _fundAccountingSetup.InvestmentEquity);
                        cmd.Parameters.AddWithValue("@InvestmentBond", _fundAccountingSetup.InvestmentBond);
                        cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _fundAccountingSetup.InvestmentTimeDeposit);
                        cmd.Parameters.AddWithValue("@InterestRecBond", _fundAccountingSetup.InterestRecBond);
                        cmd.Parameters.AddWithValue("@InterestAccrBond", _fundAccountingSetup.InterestAccrBond);
                        cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _fundAccountingSetup.InterestAccrTimeDeposit);
                        cmd.Parameters.AddWithValue("@InterestAccrGiro", _fundAccountingSetup.InterestAccrGiro);
                        cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _fundAccountingSetup.PrepaidTaxDividend);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _fundAccountingSetup.AccountReceivableSaleBond);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _fundAccountingSetup.AccountReceivableSaleEquity);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _fundAccountingSetup.AccountReceivableSaleTimeDeposit);
                        cmd.Parameters.AddWithValue("@IncomeInterestBond", _fundAccountingSetup.IncomeInterestBond);
                        cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _fundAccountingSetup.IncomeInterestTimeDeposit);
                        cmd.Parameters.AddWithValue("@IncomeInterestGiro", _fundAccountingSetup.IncomeInterestGiro);
                        cmd.Parameters.AddWithValue("@IncomeDividend", _fundAccountingSetup.IncomeDividend);
                        cmd.Parameters.AddWithValue("@ARDividend", _fundAccountingSetup.ARDividend);
                        cmd.Parameters.AddWithValue("@RevaluationBond", _fundAccountingSetup.RevaluationBond);
                        cmd.Parameters.AddWithValue("@RevaluationEquity", _fundAccountingSetup.RevaluationEquity);
                        cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _fundAccountingSetup.PayablePurchaseEquity);
                        cmd.Parameters.AddWithValue("@PayablePurRecBond", _fundAccountingSetup.PayablePurRecBond);
                        cmd.Parameters.AddWithValue("@PayableManagementFee", _fundAccountingSetup.PayableManagementFee);
                        cmd.Parameters.AddWithValue("@PayableCustodianFee", _fundAccountingSetup.PayableCustodianFee);
                        cmd.Parameters.AddWithValue("@PayableAuditFee", _fundAccountingSetup.PayableAuditFee);
                        cmd.Parameters.AddWithValue("@BrokerCommission", _fundAccountingSetup.BrokerCommission);
                        cmd.Parameters.AddWithValue("@BrokerLevy", _fundAccountingSetup.BrokerLevy);
                        cmd.Parameters.AddWithValue("@BrokerVat", _fundAccountingSetup.BrokerVat);
                        cmd.Parameters.AddWithValue("@BrokerSalesTax", _fundAccountingSetup.BrokerSalesTax);
                        cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _fundAccountingSetup.WithHoldingTaxPPH23);
                        cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _fundAccountingSetup.WHTTaxPayableAccrInterestBond);
                        cmd.Parameters.AddWithValue("@ManagementFeeExpense", _fundAccountingSetup.ManagementFeeExpense);
                        cmd.Parameters.AddWithValue("@CustodianFeeExpense", _fundAccountingSetup.CustodianFeeExpense);
                        cmd.Parameters.AddWithValue("@AuditFeeExpense", _fundAccountingSetup.AuditFeeExpense);
                        cmd.Parameters.AddWithValue("@BankCharges", _fundAccountingSetup.BankCharges);
                        cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _fundAccountingSetup.TaxExpenseInterestIncomeBond);
                        cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _fundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                        cmd.Parameters.AddWithValue("@RealisedBond", _fundAccountingSetup.RealisedBond);
                        cmd.Parameters.AddWithValue("@RealisedEquity", _fundAccountingSetup.RealisedEquity);
                        cmd.Parameters.AddWithValue("@UnrealisedBond", _fundAccountingSetup.UnrealisedBond);
                        cmd.Parameters.AddWithValue("@UnrealisedEquity", _fundAccountingSetup.UnrealisedEquity);
                        cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _fundAccountingSetup.DistributedIncomeAcc);
                        cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _fundAccountingSetup.DistributedIncomePayableAcc);
                        cmd.Parameters.AddWithValue("@PendingSubscription", _fundAccountingSetup.PendingSubscription);
                        cmd.Parameters.AddWithValue("@PendingRedemption", _fundAccountingSetup.PendingRedemption);
                        cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _fundAccountingSetup.TaxCapitalGainBond);
                        cmd.Parameters.AddWithValue("@PayableSInvestFee", _fundAccountingSetup.PayableSInvestFee);
                        cmd.Parameters.AddWithValue("@SInvestFee", _fundAccountingSetup.SInvestFee);

                        cmd.Parameters.AddWithValue("@TaxPercentageDividend", _fundAccountingSetup.TaxPercentageDividend);
                        cmd.Parameters.AddWithValue("@TaxPercentageBond", _fundAccountingSetup.TaxPercentageBond);
                        cmd.Parameters.AddWithValue("@TaxPercentageTD", _fundAccountingSetup.TaxPercentageTD);
                        cmd.Parameters.AddWithValue("@TaxProvisionPercent", _fundAccountingSetup.TaxProvisionPercent);
                        cmd.Parameters.AddWithValue("@BondAmortization", _fundAccountingSetup.BondAmortization);
                        cmd.Parameters.AddWithValue("@PayableMovementFee", _fundAccountingSetup.PayableMovementFee);
                        cmd.Parameters.AddWithValue("@MovementFeeExpense", _fundAccountingSetup.MovementFeeExpense);

                        cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _fundAccountingSetup.PayablePurchaseMutualFund);
                        cmd.Parameters.AddWithValue("@InvestmentMutualFund", _fundAccountingSetup.InvestmentMutualFund);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _fundAccountingSetup.AccountReceivableSaleMutualFund);
                        cmd.Parameters.AddWithValue("@RevaluationMutualFund", _fundAccountingSetup.RevaluationMutualFund);
                        cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _fundAccountingSetup.UnrealisedMutualFund);
                        cmd.Parameters.AddWithValue("@RealisedMutualFund", _fundAccountingSetup.RealisedMutualFund);
                        cmd.Parameters.AddWithValue("@TaxInterestBond", _fundAccountingSetup.TaxInterestBond);
                        cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _fundAccountingSetup.PayableOtherFeeOne);
                        cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _fundAccountingSetup.PayableOtherFeeTwo);
                        cmd.Parameters.AddWithValue("@PayableOtherFeeThree", _fundAccountingSetup.PayableOtherFeeThree);

                        cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _fundAccountingSetup.OtherFeeOneExpense);
                        cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _fundAccountingSetup.OtherFeeTwoExpense);
                        cmd.Parameters.AddWithValue("@OtherFeeThreeExpense", _fundAccountingSetup.OtherFeeThreeExpense);
                        cmd.Parameters.AddWithValue("@PendingSwitching", _fundAccountingSetup.PendingSwitching);
                        cmd.Parameters.AddWithValue("@CurrentYearAccount", _fundAccountingSetup.CurrentYearAccount);
                        cmd.Parameters.AddWithValue("@PriorYearAccount", _fundAccountingSetup.PriorYearAccount);

                        //TAMBAHAN
                        cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _fundAccountingSetup.TaxPercentageCapitalGain);
                        //cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                        cmd.Parameters.AddWithValue("@InterestAccruedCurrentAccount", _fundAccountingSetup.InterestAccruedCurrentAccount);
                        cmd.Parameters.AddWithValue("@IncomeCurrentAccount", _fundAccountingSetup.IncomeCurrentAccount);
                        cmd.Parameters.AddWithValue("@InvInTDUSD", _fundAccountingSetup.InvInTDUSD);

                        cmd.Parameters.AddWithValue("@InvestmentSukuk", _fundAccountingSetup.InvestmentSukuk);
                        cmd.Parameters.AddWithValue("@InterestRecSukuk", _fundAccountingSetup.InterestRecSukuk);
                        cmd.Parameters.AddWithValue("@PayablePurRecSukuk", _fundAccountingSetup.PayablePurRecSukuk);
                        cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestSukuk", _fundAccountingSetup.WHTTaxPayableAccrInterestSukuk);
                        cmd.Parameters.AddWithValue("@InterestAccrSukuk", _fundAccountingSetup.InterestAccrSukuk);

                        cmd.Parameters.AddWithValue("@RealisedSukuk", _fundAccountingSetup.RealisedSukuk);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleSukuk", _fundAccountingSetup.AccountReceivableSaleSukuk);
                        cmd.Parameters.AddWithValue("@TaxCapitalGainSukuk", _fundAccountingSetup.TaxCapitalGainSukuk);
                        cmd.Parameters.AddWithValue("@InvestmentProtectedFund", _fundAccountingSetup.InvestmentProtectedFund);
                        cmd.Parameters.AddWithValue("@PayablePurchaseProtectedFund", _fundAccountingSetup.PayablePurchaseProtectedFund);
                        cmd.Parameters.AddWithValue("@InvestmentPrivateEquityFund", _fundAccountingSetup.InvestmentPrivateEquityFund);
                        cmd.Parameters.AddWithValue("@PayablePurchasePrivateEquityFund", _fundAccountingSetup.PayablePurchasePrivateEquityFund);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleProtectedFund", _fundAccountingSetup.AccountReceivableSaleProtectedFund);
                        cmd.Parameters.AddWithValue("@AccountReceivableSalePrivateEquityFund", _fundAccountingSetup.AccountReceivableSalePrivateEquityFund);

                        cmd.Parameters.AddWithValue("@RevaluationSukuk", _fundAccountingSetup.RevaluationSukuk);

                        cmd.Parameters.AddWithValue("@UnrealisedSukuk", _fundAccountingSetup.UnrealisedSukuk);
                        cmd.Parameters.AddWithValue("@CashForMutualFund", _fundAccountingSetup.CashForMutualFund);
                        cmd.Parameters.AddWithValue("@RevaluationProtectedFund", _fundAccountingSetup.RevaluationProtectedFund);
                        cmd.Parameters.AddWithValue("@RevaluationPrivateEquityFund", _fundAccountingSetup.RevaluationPrivateEquityFund);
                        cmd.Parameters.AddWithValue("@UnrealisedProtectedFund", _fundAccountingSetup.UnrealisedProtectedFund);
                        cmd.Parameters.AddWithValue("@UnrealisedPrivateEquityFund", _fundAccountingSetup.UnrealisedPrivateEquityFund);

                        cmd.Parameters.AddWithValue("@InterestReceivableSellSukuk", _fundAccountingSetup.InterestReceivableSellSukuk);

                        cmd.Parameters.AddWithValue("@InterestRecSellBond", _fundAccountingSetup.InterestRecSellBond);

                        cmd.Parameters.AddWithValue("@InterestReceivableBuySukuk", _fundAccountingSetup.InterestReceivableBuySukuk);
                        cmd.Parameters.AddWithValue("@InterestReceivableSellMutualFund", _fundAccountingSetup.InterestReceivableSellMutualFund);
                        cmd.Parameters.AddWithValue("@InterestReceivableSellProtectedFund", _fundAccountingSetup.InterestReceivableSellProtectedFund);
                        cmd.Parameters.AddWithValue("@InterestReceivableSellPrivateEquityFund", _fundAccountingSetup.InterestReceivableSellPrivateEquityFund);

                        cmd.Parameters.AddWithValue("@IncomeInterestSukuk", _fundAccountingSetup.IncomeInterestSukuk);
                        cmd.Parameters.AddWithValue("@InterestAccrMutualFund", _fundAccountingSetup.InterestAccrMutualFund);
                        cmd.Parameters.AddWithValue("@InterestAccrProtectedFund", _fundAccountingSetup.InterestAccrProtectedFund);
                        cmd.Parameters.AddWithValue("@InterestAccrPrivateEquityFund", _fundAccountingSetup.InterestAccrPrivateEquityFund);
                        cmd.Parameters.AddWithValue("@IncomeInterestAccrMutualFund", _fundAccountingSetup.IncomeInterestAccrMutualFund);
                        cmd.Parameters.AddWithValue("@IncomeInterestAccrProtectedFund", _fundAccountingSetup.IncomeInterestAccrProtectedFund);
                        cmd.Parameters.AddWithValue("@IncomeInterestAccrPrivateEquityFund", _fundAccountingSetup.IncomeInterestAccrPrivateEquityFund);
                        cmd.Parameters.AddWithValue("@APManagementFee", _fundAccountingSetup.APManagementFee);

                        cmd.Parameters.AddWithValue("@InvestmentRights", _fundAccountingSetup.InvestmentRights);
                        cmd.Parameters.AddWithValue("@PayablePurchaseRights", _fundAccountingSetup.PayablePurchaseRights);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleRights", _fundAccountingSetup.AccountReceivableSaleRights);
                        cmd.Parameters.AddWithValue("@RealisedRights", _fundAccountingSetup.RealisedRights);
                        cmd.Parameters.AddWithValue("@RevaluationRights", _fundAccountingSetup.RevaluationRights);
                        cmd.Parameters.AddWithValue("@UnrealisedRights", _fundAccountingSetup.UnrealisedRights);
                        cmd.Parameters.AddWithValue("@CashForIPO", _fundAccountingSetup.CashForIPO);

                        cmd.Parameters.AddWithValue("@BitAccruedInterestGiroDaily", _fundAccountingSetup.BitAccruedInterestGiroDaily);
                        cmd.Parameters.AddWithValue("@AveragePriority", _fundAccountingSetup.AveragePriority);
                        cmd.Parameters.AddWithValue("@AveragePriorityBond", _fundAccountingSetup.AveragePriorityBond);
                        //cmd.Parameters.AddWithValue("@AveragePriorityDesc", _fundAccountingSetup.AveragePriorityDesc);

                        cmd.Parameters.AddWithValue("@TaxProvision", _fundAccountingSetup.TaxProvision);
                        cmd.Parameters.AddWithValue("@PayableTaxProvision", _fundAccountingSetup.PayableTaxProvision);

                        cmd.Parameters.AddWithValue("@InvestmentWarrant", _fundAccountingSetup.InvestmentWarrant);
                        cmd.Parameters.AddWithValue("@PayablePurchaseWarrant", _fundAccountingSetup.PayablePurchaseWarrant);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleWarrant", _fundAccountingSetup.AccountReceivableSaleWarrant);
                        cmd.Parameters.AddWithValue("@RealisedWarrant", _fundAccountingSetup.RealisedWarrant);
                        cmd.Parameters.AddWithValue("@RevaluationWarrant", _fundAccountingSetup.RevaluationWarrant);
                        cmd.Parameters.AddWithValue("@UnrealisedWarrant", _fundAccountingSetup.UnrealisedWarrant);
                        cmd.Parameters.AddWithValue("@CBESTExpense", _fundAccountingSetup.CBESTExpense);
                        cmd.Parameters.AddWithValue("@PayableCBEST", _fundAccountingSetup.PayableCBEST);
                        cmd.Parameters.AddWithValue("@TaxPercentageCapitalGainSell", _fundAccountingSetup.TaxPercentageCapitalGainSell);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _fundAccountingSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundAccountingSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int FundAccountingSetup_Update(FundAccountingSetup _fundAccountingSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_fundAccountingSetup.FundAccountingSetupPK, _fundAccountingSetup.HistoryPK, "FundAccountingSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"Update FundAccountingSetup set status=1,Notes=@Notes,FundPK=@FundPK,Subscription=@Subscription,PayableSubscriptionFee=@PayableSubscriptionFee,TaxPercentageDividend=@TaxPercentageDividend,TaxPercentageBond=@TaxPercentageBond,TaxPercentageTD=@TaxPercentageTD,TaxProvisionPercent=@TaxProvisionPercent,Switching=@Switching,PayableSwitchingFee=@PayableSwitchingFee,
                            Redemption=@Redemption,PayableRedemptionFee=@PayableRedemptionFee, PrepaidTaxDividend=@PrepaidTaxDividend, AccountReceivableSaleBond= @AccountReceivableSaleBond, AccountReceivableSaleEquity=@AccountReceivableSaleEquity, AccountReceivableSaleTimeDeposit=@AccountReceivableSaleTimeDeposit, 
                            IncomeInterestBond= @IncomeInterestBond, IncomeInterestTimeDeposit=@IncomeInterestTimeDeposit, IncomeInterestGiro=@IncomeInterestGiro, IncomeDividend=@IncomeDividend,ARDividend=@ARDividend,
                            RevaluationBond=@RevaluationBond, RevaluationEquity=@RevaluationEquity, PayablePurchaseEquity=@PayablePurchaseEquity, PayablePurRecBond=@PayablePurRecBond, 
                            PayableManagementFee=@PayableManagementFee, PayableCustodianFee=@PayableCustodianFee,
                            PayableAuditFee=@PayableAuditFee, BrokerCommission=@BrokerCommission, BrokerLevy=@BrokerLevy, BrokerVat=@BrokerVat, BrokerSalesTax=@BrokerSalesTax, 
                            WithHoldingTaxPPH23=@WithHoldingTaxPPH23, WHTTaxPayableAccrInterestBond=@WHTTaxPayableAccrInterestBond, ManagementFeeExpense=@ManagementFeeExpense, 
                            CustodianFeeExpense= @CustodianFeeExpense, AuditFeeExpense=@AuditFeeExpense, BankCharges=@BankCharges, TaxExpenseInterestIncomeBond=@TaxExpenseInterestIncomeBond, 
                            TaxExpenseInterestIncomeTimeDeposit=@TaxExpenseInterestIncomeTimeDeposit, RealisedEquity = @RealisedEquity, UnrealisedBond=@UnrealisedBond, UnrealisedEquity=@UnrealisedEquity, 
                            DistributedIncomeAcc=@DistributedIncomeAcc, DistributedIncomePayableAcc=@DistributedIncomePayableAcc, PendingSubscription=@PendingSubscription, PendingRedemption=@PendingRedemption, RealisedBond = @RealisedBond, TaxCapitalGainBond = @TaxCapitalGainBond, PayableSInvestFee = @PayableSInvestFee, SInvestFee = @SInvestFee, BondAmortization = @BondAmortization,                    
                            PayableMovementFee=@PayableMovementFee, MovementFeeExpense=@MovementFeeExpense,  PayablePurchaseMutualFund=@PayablePurchaseMutualFund, 
                            InvestmentMutualFund=@InvestmentMutualFund, AccountReceivableSaleMutualFund=@AccountReceivableSaleMutualFund, RevaluationMutualFund=@RevaluationMutualFund, UnrealisedMutualFund=@UnrealisedMutualFund, RealisedMutualFund=@RealisedMutualFund,
                            PayableOtherFeeOne=@PayableOtherFeeOne,PayableOtherFeeTwo=@PayableOtherFeeTwo,PayableOtherFeeThree=@PayableOtherFeeThree,OtherFeeOneExpense=@OtherFeeOneExpense,OtherFeeTwoExpense=@OtherFeeTwoExpense,OtherFeeThreeExpense=@OtherFeeThreeExpense,PendingSwitching=@PendingSwitching,CurrentYearAccount=@CurrentYearAccount,PriorYearAccount=@PriorYearAccount,
                            TaxPercentageCapitalGain=@TaxPercentageCapitalGain,InterestAccruedCurrentAccount=@InterestAccruedCurrentAccount,IncomeCurrentAccount=@IncomeCurrentAccount,InvInTDUSD=@InvInTDUSD,
                            InvestmentSukuk=@InvestmentSukuk,InterestRecSukuk=@InterestRecSukuk,PayablePurRecSukuk=@PayablePurRecSukuk,WHTTaxPayableAccrInterestSukuk=@WHTTaxPayableAccrInterestSukuk,InterestAccrSukuk=@InterestAccrSukuk,
                            RealisedSukuk=@RealisedSukuk,AccountReceivableSaleSukuk=@AccountReceivableSaleSukuk,TaxCapitalGainSukuk=@TaxCapitalGainSukuk,InvestmentProtectedFund=@InvestmentProtectedFund,PayablePurchaseProtectedFund=@PayablePurchaseProtectedFund,InvestmentPrivateEquityFund=@InvestmentPrivateEquityFund,
                            PayablePurchasePrivateEquityFund=@PayablePurchasePrivateEquityFund,AccountReceivableSaleProtectedFund=@AccountReceivableSaleProtectedFund,AccountReceivableSalePrivateEquityFund=@AccountReceivableSalePrivateEquityFund,
                            RevaluationSukuk=@RevaluationSukuk,
                            UnrealisedSukuk=@UnrealisedSukuk,CashForMutualFund=@CashForMutualFund,RevaluationProtectedFund=@RevaluationProtectedFund,RevaluationPrivateEquityFund=@RevaluationPrivateEquityFund,UnrealisedProtectedFund=@UnrealisedProtectedFund,UnrealisedPrivateEquityFund=@UnrealisedPrivateEquityFund,
                            InterestReceivableSellSukuk=@InterestReceivableSellSukuk,
                            InterestRecSellBond=@InterestRecSellBond,
                            InterestReceivableBuySukuk=@InterestReceivableBuySukuk,
                            InterestReceivableSellMutualFund=@InterestReceivableSellMutualFund,InterestReceivableSellProtectedFund=@InterestReceivableSellProtectedFund,InterestReceivableSellPrivateEquityFund=@InterestReceivableSellPrivateEquityFund,
                            IncomeInterestSukuk=@IncomeInterestSukuk,InterestAccrMutualFund=@InterestAccrMutualFund,InterestAccrProtectedFund=@InterestAccrProtectedFund,InterestAccrPrivateEquityFund=@InterestAccrPrivateEquityFund,IncomeInterestAccrMutualFund=@IncomeInterestAccrMutualFund,
                            IncomeInterestAccrProtectedFund=@IncomeInterestAccrProtectedFund,IncomeInterestAccrPrivateEquityFund=@IncomeInterestAccrPrivateEquityFund,APManagementFee=@APManagementFee,InvestmentRights=@InvestmentRights,PayablePurchaseRights=@PayablePurchaseRights,AccountReceivableSaleRights=@AccountReceivableSaleRights,
                            RealisedRights=@RealisedRights,RevaluationRights=@RevaluationRights,UnrealisedRights=@UnrealisedRights,CashForIPO=@CashForIPO,BitAccruedInterestGiroDaily=@BitAccruedInterestGiroDaily,AveragePriority=@AveragePriority,AveragePriorityBond=@AveragePriorityBond,TaxInterestBond=@TaxInterestBond,TaxProvision=@TaxProvision,PayableTaxProvision=@PayableTaxProvision,
                            InvestmentWarrant=@InvestmentWarrant,PayablePurchaseWarrant=@PayablePurchaseWarrant,AccountReceivableSaleWarrant=@AccountReceivableSaleWarrant,RealisedWarrant=@RealisedWarrant,RevaluationWarrant=@RevaluationWarrant,UnrealisedWarrant=@UnrealisedWarrant,CBESTExpense=@CBESTExpense,PayableCBEST=@PayableCBEST,
                            TaxPercentageCapitalGainSell=@TaxPercentageCapitalGainSell,
                            UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                            where FundAccountingSetupPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _fundAccountingSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _fundAccountingSetup.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _fundAccountingSetup.FundPK);
                            cmd.Parameters.AddWithValue("@Subscription", _fundAccountingSetup.Subscription);
                            cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _fundAccountingSetup.PayableSubscriptionFee);
                            cmd.Parameters.AddWithValue("@Redemption", _fundAccountingSetup.Redemption);
                            cmd.Parameters.AddWithValue("@PayableRedemptionFee", _fundAccountingSetup.PayableRedemptionFee);
                            cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                            cmd.Parameters.AddWithValue("@PayableSwitchingFee", _fundAccountingSetup.PayableSwitchingFee);
                            cmd.Parameters.AddWithValue("@InvestmentEquity", _fundAccountingSetup.InvestmentEquity);
                            cmd.Parameters.AddWithValue("@InvestmentBond", _fundAccountingSetup.InvestmentBond);
                            cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _fundAccountingSetup.InvestmentTimeDeposit);
                            cmd.Parameters.AddWithValue("@InterestRecBond", _fundAccountingSetup.InterestRecBond);
                            cmd.Parameters.AddWithValue("@InterestAccrBond", _fundAccountingSetup.InterestAccrBond);
                            cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _fundAccountingSetup.InterestAccrTimeDeposit);
                            cmd.Parameters.AddWithValue("@InterestAccrGiro", _fundAccountingSetup.InterestAccrGiro);
                            cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _fundAccountingSetup.PrepaidTaxDividend);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _fundAccountingSetup.AccountReceivableSaleBond);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _fundAccountingSetup.AccountReceivableSaleEquity);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _fundAccountingSetup.AccountReceivableSaleTimeDeposit);
                            cmd.Parameters.AddWithValue("@IncomeInterestBond", _fundAccountingSetup.IncomeInterestBond);
                            cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _fundAccountingSetup.IncomeInterestTimeDeposit);
                            cmd.Parameters.AddWithValue("@IncomeInterestGiro", _fundAccountingSetup.IncomeInterestGiro);
                            cmd.Parameters.AddWithValue("@IncomeDividend", _fundAccountingSetup.IncomeDividend);
                            cmd.Parameters.AddWithValue("@ARDividend", _fundAccountingSetup.ARDividend);
                            cmd.Parameters.AddWithValue("@RevaluationBond", _fundAccountingSetup.RevaluationBond);
                            cmd.Parameters.AddWithValue("@RevaluationEquity", _fundAccountingSetup.RevaluationEquity);
                            cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _fundAccountingSetup.PayablePurchaseEquity);
                            cmd.Parameters.AddWithValue("@PayablePurRecBond", _fundAccountingSetup.PayablePurRecBond);
                            cmd.Parameters.AddWithValue("@PayableManagementFee", _fundAccountingSetup.PayableManagementFee);
                            cmd.Parameters.AddWithValue("@PayableCustodianFee", _fundAccountingSetup.PayableCustodianFee);
                            cmd.Parameters.AddWithValue("@PayableAuditFee", _fundAccountingSetup.PayableAuditFee);
                            cmd.Parameters.AddWithValue("@BrokerCommission", _fundAccountingSetup.BrokerCommission);
                            cmd.Parameters.AddWithValue("@BrokerLevy", _fundAccountingSetup.BrokerLevy);
                            cmd.Parameters.AddWithValue("@BrokerVat", _fundAccountingSetup.BrokerVat);
                            cmd.Parameters.AddWithValue("@BrokerSalesTax", _fundAccountingSetup.BrokerSalesTax);
                            cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _fundAccountingSetup.WithHoldingTaxPPH23);
                            cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _fundAccountingSetup.WHTTaxPayableAccrInterestBond);
                            cmd.Parameters.AddWithValue("@ManagementFeeExpense", _fundAccountingSetup.ManagementFeeExpense);
                            cmd.Parameters.AddWithValue("@CustodianFeeExpense", _fundAccountingSetup.CustodianFeeExpense);
                            cmd.Parameters.AddWithValue("@AuditFeeExpense", _fundAccountingSetup.AuditFeeExpense);
                            cmd.Parameters.AddWithValue("@BankCharges", _fundAccountingSetup.BankCharges);
                            cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _fundAccountingSetup.TaxExpenseInterestIncomeBond);
                            cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _fundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                            cmd.Parameters.AddWithValue("@RealisedBond", _fundAccountingSetup.RealisedBond);
                            cmd.Parameters.AddWithValue("@RealisedEquity", _fundAccountingSetup.RealisedEquity);
                            cmd.Parameters.AddWithValue("@UnrealisedBond", _fundAccountingSetup.UnrealisedBond);
                            cmd.Parameters.AddWithValue("@UnrealisedEquity", _fundAccountingSetup.UnrealisedEquity);
                            cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _fundAccountingSetup.DistributedIncomeAcc);
                            cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _fundAccountingSetup.DistributedIncomePayableAcc);
                            cmd.Parameters.AddWithValue("@PendingSubscription", _fundAccountingSetup.PendingSubscription);
                            cmd.Parameters.AddWithValue("@PendingRedemption", _fundAccountingSetup.PendingRedemption);
                            cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _fundAccountingSetup.TaxCapitalGainBond);
                            cmd.Parameters.AddWithValue("@PayableSInvestFee", _fundAccountingSetup.PayableSInvestFee);
                            cmd.Parameters.AddWithValue("@SInvestFee", _fundAccountingSetup.SInvestFee);

                            cmd.Parameters.AddWithValue("@TaxPercentageDividend", _fundAccountingSetup.TaxPercentageDividend);
                            cmd.Parameters.AddWithValue("@TaxPercentageBond", _fundAccountingSetup.TaxPercentageBond);
                            cmd.Parameters.AddWithValue("@TaxPercentageTD", _fundAccountingSetup.TaxPercentageTD);
                            cmd.Parameters.AddWithValue("@TaxProvisionPercent", _fundAccountingSetup.TaxProvisionPercent);
                            cmd.Parameters.AddWithValue("@BondAmortization", _fundAccountingSetup.BondAmortization);
                            cmd.Parameters.AddWithValue("@PayableMovementFee", _fundAccountingSetup.PayableMovementFee);
                            cmd.Parameters.AddWithValue("@MovementFeeExpense", _fundAccountingSetup.MovementFeeExpense);
                            cmd.Parameters.AddWithValue("@TaxInterestBond", _fundAccountingSetup.TaxInterestBond);
                            cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _fundAccountingSetup.PayablePurchaseMutualFund);
                            cmd.Parameters.AddWithValue("@InvestmentMutualFund", _fundAccountingSetup.InvestmentMutualFund);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _fundAccountingSetup.AccountReceivableSaleMutualFund);
                            cmd.Parameters.AddWithValue("@RevaluationMutualFund", _fundAccountingSetup.RevaluationMutualFund);
                            cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _fundAccountingSetup.UnrealisedMutualFund);
                            cmd.Parameters.AddWithValue("@RealisedMutualFund", _fundAccountingSetup.RealisedMutualFund);

                            cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _fundAccountingSetup.PayableOtherFeeOne);
                            cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _fundAccountingSetup.PayableOtherFeeTwo);
                            cmd.Parameters.AddWithValue("@PayableOtherFeeThree", _fundAccountingSetup.PayableOtherFeeThree);
                            cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _fundAccountingSetup.OtherFeeOneExpense);
                            cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _fundAccountingSetup.OtherFeeTwoExpense);
                            cmd.Parameters.AddWithValue("@OtherFeeThreeExpense", _fundAccountingSetup.OtherFeeThreeExpense);

                            cmd.Parameters.AddWithValue("@PendingSwitching", _fundAccountingSetup.PendingSwitching);
                            cmd.Parameters.AddWithValue("@CurrentYearAccount", _fundAccountingSetup.CurrentYearAccount);
                            cmd.Parameters.AddWithValue("@PriorYearAccount", _fundAccountingSetup.PriorYearAccount);

                            //
                            cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _fundAccountingSetup.TaxPercentageCapitalGain);
                            //cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                            cmd.Parameters.AddWithValue("@InterestAccruedCurrentAccount", _fundAccountingSetup.InterestAccruedCurrentAccount);
                            cmd.Parameters.AddWithValue("@IncomeCurrentAccount", _fundAccountingSetup.IncomeCurrentAccount);
                            cmd.Parameters.AddWithValue("@InvInTDUSD", _fundAccountingSetup.InvInTDUSD);

                            cmd.Parameters.AddWithValue("@InvestmentSukuk", _fundAccountingSetup.InvestmentSukuk);
                            cmd.Parameters.AddWithValue("@InterestRecSukuk", _fundAccountingSetup.InterestRecSukuk);
                            cmd.Parameters.AddWithValue("@PayablePurRecSukuk", _fundAccountingSetup.PayablePurRecSukuk);
                            cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestSukuk", _fundAccountingSetup.WHTTaxPayableAccrInterestSukuk);
                            cmd.Parameters.AddWithValue("@InterestAccrSukuk", _fundAccountingSetup.InterestAccrSukuk);

                            cmd.Parameters.AddWithValue("@RealisedSukuk", _fundAccountingSetup.RealisedSukuk);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleSukuk", _fundAccountingSetup.AccountReceivableSaleSukuk);
                            cmd.Parameters.AddWithValue("@TaxCapitalGainSukuk", _fundAccountingSetup.TaxCapitalGainSukuk);
                            cmd.Parameters.AddWithValue("@InvestmentProtectedFund", _fundAccountingSetup.InvestmentProtectedFund);
                            cmd.Parameters.AddWithValue("@PayablePurchaseProtectedFund", _fundAccountingSetup.PayablePurchaseProtectedFund);
                            cmd.Parameters.AddWithValue("@InvestmentPrivateEquityFund", _fundAccountingSetup.InvestmentPrivateEquityFund);
                            cmd.Parameters.AddWithValue("@PayablePurchasePrivateEquityFund", _fundAccountingSetup.PayablePurchasePrivateEquityFund);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleProtectedFund", _fundAccountingSetup.AccountReceivableSaleProtectedFund);
                            cmd.Parameters.AddWithValue("@AccountReceivableSalePrivateEquityFund", _fundAccountingSetup.AccountReceivableSalePrivateEquityFund);

                            cmd.Parameters.AddWithValue("@RevaluationSukuk", _fundAccountingSetup.RevaluationSukuk);

                            cmd.Parameters.AddWithValue("@UnrealisedSukuk", _fundAccountingSetup.UnrealisedSukuk);
                            cmd.Parameters.AddWithValue("@CashForMutualFund", _fundAccountingSetup.CashForMutualFund);
                            cmd.Parameters.AddWithValue("@RevaluationProtectedFund", _fundAccountingSetup.RevaluationProtectedFund);
                            cmd.Parameters.AddWithValue("@RevaluationPrivateEquityFund", _fundAccountingSetup.RevaluationPrivateEquityFund);
                            cmd.Parameters.AddWithValue("@UnrealisedProtectedFund", _fundAccountingSetup.UnrealisedProtectedFund);
                            cmd.Parameters.AddWithValue("@UnrealisedPrivateEquityFund", _fundAccountingSetup.UnrealisedPrivateEquityFund);

                            cmd.Parameters.AddWithValue("@InterestReceivableSellSukuk", _fundAccountingSetup.InterestReceivableSellSukuk);

                            cmd.Parameters.AddWithValue("@InterestRecSellBond", _fundAccountingSetup.InterestRecSellBond);

                            cmd.Parameters.AddWithValue("@InterestReceivableBuySukuk", _fundAccountingSetup.InterestReceivableBuySukuk);
                            cmd.Parameters.AddWithValue("@InterestReceivableSellMutualFund", _fundAccountingSetup.InterestReceivableSellMutualFund);
                            cmd.Parameters.AddWithValue("@InterestReceivableSellProtectedFund", _fundAccountingSetup.InterestReceivableSellProtectedFund);
                            cmd.Parameters.AddWithValue("@InterestReceivableSellPrivateEquityFund", _fundAccountingSetup.InterestReceivableSellPrivateEquityFund);

                            cmd.Parameters.AddWithValue("@IncomeInterestSukuk", _fundAccountingSetup.IncomeInterestSukuk);
                            cmd.Parameters.AddWithValue("@InterestAccrMutualFund", _fundAccountingSetup.InterestAccrMutualFund);
                            cmd.Parameters.AddWithValue("@InterestAccrProtectedFund", _fundAccountingSetup.InterestAccrProtectedFund);
                            cmd.Parameters.AddWithValue("@InterestAccrPrivateEquityFund", _fundAccountingSetup.InterestAccrPrivateEquityFund);
                            cmd.Parameters.AddWithValue("@IncomeInterestAccrMutualFund", _fundAccountingSetup.IncomeInterestAccrMutualFund);
                            cmd.Parameters.AddWithValue("@IncomeInterestAccrProtectedFund", _fundAccountingSetup.IncomeInterestAccrProtectedFund);
                            cmd.Parameters.AddWithValue("@IncomeInterestAccrPrivateEquityFund", _fundAccountingSetup.IncomeInterestAccrPrivateEquityFund);
                            cmd.Parameters.AddWithValue("@APManagementFee", _fundAccountingSetup.APManagementFee);

                            cmd.Parameters.AddWithValue("@InvestmentRights", _fundAccountingSetup.InvestmentRights);
                            cmd.Parameters.AddWithValue("@PayablePurchaseRights", _fundAccountingSetup.PayablePurchaseRights);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleRights", _fundAccountingSetup.AccountReceivableSaleRights);
                            cmd.Parameters.AddWithValue("@RealisedRights", _fundAccountingSetup.RealisedRights);
                            cmd.Parameters.AddWithValue("@RevaluationRights", _fundAccountingSetup.RevaluationRights);
                            cmd.Parameters.AddWithValue("@UnrealisedRights", _fundAccountingSetup.UnrealisedRights);
                            cmd.Parameters.AddWithValue("@CashForIPO", _fundAccountingSetup.CashForIPO);

                            cmd.Parameters.AddWithValue("@BitAccruedInterestGiroDaily", _fundAccountingSetup.BitAccruedInterestGiroDaily);
                            cmd.Parameters.AddWithValue("@AveragePriority", _fundAccountingSetup.AveragePriority);
                            cmd.Parameters.AddWithValue("@AveragePriorityBond", _fundAccountingSetup.AveragePriorityBond);
                            //cmd.Parameters.AddWithValue("@AveragePriorityDesc", _fundAccountingSetup.AveragePriorityDesc);
                            cmd.Parameters.AddWithValue("@TaxProvision", _fundAccountingSetup.TaxProvision);
                            cmd.Parameters.AddWithValue("@PayableTaxProvision", _fundAccountingSetup.PayableTaxProvision);

                            cmd.Parameters.AddWithValue("@InvestmentWarrant", _fundAccountingSetup.InvestmentWarrant);
                            cmd.Parameters.AddWithValue("@PayablePurchaseWarrant", _fundAccountingSetup.PayablePurchaseWarrant);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleWarrant", _fundAccountingSetup.AccountReceivableSaleWarrant);
                            cmd.Parameters.AddWithValue("@RealisedWarrant", _fundAccountingSetup.RealisedWarrant);
                            cmd.Parameters.AddWithValue("@RevaluationWarrant", _fundAccountingSetup.RevaluationWarrant);
                            cmd.Parameters.AddWithValue("@UnrealisedWarrant", _fundAccountingSetup.UnrealisedWarrant);
                            cmd.Parameters.AddWithValue("@CBESTExpense", _fundAccountingSetup.CBESTExpense);
                            cmd.Parameters.AddWithValue("@PayableCBEST", _fundAccountingSetup.PayableCBEST);
                            cmd.Parameters.AddWithValue("@TaxPercentageCapitalGainSell", _fundAccountingSetup.TaxPercentageCapitalGainSell);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _fundAccountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundAccountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundAccountingSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundAccountingSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _fundAccountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        return 0;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update FundAccountingSetup set status=1,Notes=@Notes,FundPK=@FundPK,Subscription=@Subscription,PayableSubscriptionFee=@PayableSubscriptionFee,TaxPercentageDividend=@TaxPercentageDividend,
                                TaxPercentageBond=@TaxPercentageBond,TaxPercentageTD=@TaxPercentageTD,TaxProvisionPercent=@TaxProvisionPercent,Switching=@Switching,PayableSwitchingFee=@PayableSwitchingFee,InvestmentEquity=@InvestmentEquity,
                                InvestmentBond=@InvestmentBond,InvestmentTimeDeposit=@InvestmentTimeDeposit,InterestRecBond=@InterestRecBond,InterestAccrBond=@InterestAccrBond,Redemption=@Redemption,
                                PayableRedemptionFee=@PayableRedemptionFee,PrepaidTaxDividend=@PrepaidTaxDividend, AccountReceivableSaleBond= @AccountReceivableSaleBond, AccountReceivableSaleEquity=@AccountReceivableSaleEquity, 
                                AccountReceivableSaleTimeDeposit=@AccountReceivableSaleTimeDeposit, IncomeInterestBond= @IncomeInterestBond, IncomeInterestTimeDeposit=@IncomeInterestTimeDeposit, 
                                IncomeInterestGiro=@IncomeInterestGiro, IncomeDividend=@IncomeDividend,ARDividend=@ARDividend,RevaluationBond=@RevaluationBond, RevaluationEquity=@RevaluationEquity, 
                                PayablePurchaseEquity=@PayablePurchaseEquity, PayablePurRecBond=@PayablePurRecBond, PayableManagementFee=@PayableManagementFee, PayableCustodianFee=@PayableCustodianFee,
                                PayableAuditFee=@PayableAuditFee, BrokerCommission=@BrokerCommission, BrokerLevy=@BrokerLevy, BrokerVat=@BrokerVat, BrokerSalesTax=@BrokerSalesTax, WithHoldingTaxPPH23=@WithHoldingTaxPPH23, 
                                WHTTaxPayableAccrInterestBond=@WHTTaxPayableAccrInterestBond, ManagementFeeExpense=@ManagementFeeExpense, CustodianFeeExpense= @CustodianFeeExpense, AuditFeeExpense=@AuditFeeExpense, 
                                BankCharges=@BankCharges, TaxExpenseInterestIncomeBond=@TaxExpenseInterestIncomeBond, TaxExpenseInterestIncomeTimeDeposit=@TaxExpenseInterestIncomeTimeDeposit, 
                                RealisedEquity = @RealisedEquity, UnrealisedBond=@UnrealisedBond, UnrealisedEquity=@UnrealisedEquity, DistributedIncomeAcc=@DistributedIncomeAcc, DistributedIncomePayableAcc=@DistributedIncomePayableAcc, 
                                PendingSubscription=@PendingSubscription, PendingRedemption=@PendingRedemption, RealisedBond = @RealisedBond, TaxCapitalGainBond = @TaxCapitalGainBond,  PayableSInvestFee = @PayableSInvestFee, 
                                SInvestFee = @SInvestFee, BondAmortization = @BondAmortization, PayableMovementFee=@PayableMovementFee, MovementFeeExpense=@MovementFeeExpense, PayablePurchaseMutualFund=@PayablePurchaseMutualFund, 
                                InvestmentMutualFund=@InvestmentMutualFund, AccountReceivableSaleMutualFund=@AccountReceivableSaleMutualFund, RevaluationMutualFund=@RevaluationMutualFund, UnrealisedMutualFund=@UnrealisedMutualFund, RealisedMutualFund=@RealisedMutualFund,
                                PayableOtherFeeOne=@PayableOtherFeeOne,PayableOtherFeeTwo=@PayableOtherFeeTwo,PayableOtherFeeThree=@PayableOtherFeeThree,OtherFeeOneExpense=@OtherFeeOneExpense,OtherFeeTwoExpense=@OtherFeeTwoExpense,OtherFeeThreeExpense=@OtherFeeThreeExpense,PendingSwitching=@PendingSwitching,CurrentYearAccount=@CurrentYearAccount,PriorYearAccount=@PriorYearAccount,
                                TaxPercentageCapitalGain=@TaxPercentageCapitalGain,InterestAccruedCurrentAccount=@InterestAccruedCurrentAccount,IncomeCurrentAccount=@IncomeCurrentAccount,InvInTDUSD=@InvInTDUSD,
                                InvestmentSukuk=@InvestmentSukuk,InterestRecSukuk=@InterestRecSukuk,PayablePurRecSukuk=@PayablePurRecSukuk,WHTTaxPayableAccrInterestSukuk=@WHTTaxPayableAccrInterestSukuk,InterestAccrSukuk=@InterestAccrSukuk,
                                RealisedSukuk=@RealisedSukuk,AccountReceivableSaleSukuk=@AccountReceivableSaleSukuk,TaxCapitalGainSukuk=@TaxCapitalGainSukuk,InvestmentProtectedFund=@InvestmentProtectedFund,PayablePurchaseProtectedFund=@PayablePurchaseProtectedFund,InvestmentPrivateEquityFund=@InvestmentPrivateEquityFund,
                                PayablePurchasePrivateEquityFund=@PayablePurchasePrivateEquityFund,AccountReceivableSaleProtectedFund=@AccountReceivableSaleProtectedFund,AccountReceivableSalePrivateEquityFund=@AccountReceivableSalePrivateEquityFund,
                                RevaluationSukuk=@RevaluationSukuk,
                                UnrealisedSukuk=@UnrealisedSukuk,CashForMutualFund=@CashForMutualFund,RevaluationProtectedFund=@RevaluationProtectedFund,RevaluationPrivateEquityFund=@RevaluationPrivateEquityFund,UnrealisedProtectedFund=@UnrealisedProtectedFund,UnrealisedPrivateEquityFund=@UnrealisedPrivateEquityFund,
                                InterestReceivableSellSukuk=@InterestReceivableSellSukuk,
                                InterestRecSellBond=@InterestRecSellBond,
                                InterestReceivableBuySukuk=@InterestReceivableBuySukuk,
                                InterestReceivableSellMutualFund=@InterestReceivableSellMutualFund,InterestReceivableSellProtectedFund=@InterestReceivableSellProtectedFund,InterestReceivableSellPrivateEquityFund=@InterestReceivableSellPrivateEquityFund,
                                IncomeInterestSukuk=@IncomeInterestSukuk,InterestAccrMutualFund=@InterestAccrMutualFund,InterestAccrProtectedFund=@InterestAccrProtectedFund,InterestAccrPrivateEquityFund=@InterestAccrPrivateEquityFund,IncomeInterestAccrMutualFund=@IncomeInterestAccrMutualFund,
                                IncomeInterestAccrProtectedFund=@IncomeInterestAccrProtectedFund,IncomeInterestAccrPrivateEquityFund=@IncomeInterestAccrPrivateEquityFund,APManagementFee=@APManagementFee,InvestmentRights=@InvestmentRights,PayablePurchaseRights=@PayablePurchaseRights,AccountReceivableSaleRights=@AccountReceivableSaleRights,
                                RealisedRights=@RealisedRights,RevaluationRights=@RevaluationRights,UnrealisedRights=@UnrealisedRights,CashForIPO=@CashForIPO,BitAccruedInterestGiroDaily=@BitAccruedInterestGiroDaily,AveragePriority=@AveragePriority,AveragePriorityBond=@AveragePriorityBond,TaxInterestBond=@TaxInterestBond,TaxProvision=@TaxProvision,PayableTaxProvision=@PayableTaxProvision,
                                InvestmentWarrant=@InvestmentWarrant,PayablePurchaseWarrant=@PayablePurchaseWarrant,AccountReceivableSaleWarrant=@AccountReceivableSaleWarrant,RealisedWarrant=@RealisedWarrant,RevaluationWarrant=@RevaluationWarrant,UnrealisedWarrant=@UnrealisedWarrant,CBESTExpense=@CBESTExpense,PayableCBEST=@PayableCBEST,
                                TaxPercentageCapitalGainSell=@TaxPercentageCapitalGainSell,
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate where FundAccountingSetupPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _fundAccountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _fundAccountingSetup.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _fundAccountingSetup.FundPK);
                                cmd.Parameters.AddWithValue("@Subscription", _fundAccountingSetup.Subscription);
                                cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _fundAccountingSetup.PayableSubscriptionFee);
                                cmd.Parameters.AddWithValue("@Redemption", _fundAccountingSetup.Redemption);
                                cmd.Parameters.AddWithValue("@PayableRedemptionFee", _fundAccountingSetup.PayableRedemptionFee);
                                cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                                cmd.Parameters.AddWithValue("@PayableSwitchingFee", _fundAccountingSetup.PayableSwitchingFee);
                                cmd.Parameters.AddWithValue("@InvestmentEquity", _fundAccountingSetup.InvestmentEquity);
                                cmd.Parameters.AddWithValue("@InvestmentBond", _fundAccountingSetup.InvestmentBond);
                                cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _fundAccountingSetup.InvestmentTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestRecBond", _fundAccountingSetup.InterestRecBond);
                                cmd.Parameters.AddWithValue("@InterestAccrBond", _fundAccountingSetup.InterestAccrBond);
                                cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _fundAccountingSetup.InterestAccrTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestAccrGiro", _fundAccountingSetup.InterestAccrGiro);
                                cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _fundAccountingSetup.PrepaidTaxDividend);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _fundAccountingSetup.AccountReceivableSaleBond);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _fundAccountingSetup.AccountReceivableSaleEquity);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _fundAccountingSetup.AccountReceivableSaleTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestBond", _fundAccountingSetup.IncomeInterestBond);
                                cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _fundAccountingSetup.IncomeInterestTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestGiro", _fundAccountingSetup.IncomeInterestGiro);
                                cmd.Parameters.AddWithValue("@IncomeDividend", _fundAccountingSetup.IncomeDividend);
                                cmd.Parameters.AddWithValue("@ARDividend", _fundAccountingSetup.ARDividend);
                                cmd.Parameters.AddWithValue("@RevaluationBond", _fundAccountingSetup.RevaluationBond);
                                cmd.Parameters.AddWithValue("@RevaluationEquity", _fundAccountingSetup.RevaluationEquity);
                                cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _fundAccountingSetup.PayablePurchaseEquity);
                                cmd.Parameters.AddWithValue("@PayablePurRecBond", _fundAccountingSetup.PayablePurRecBond);
                                cmd.Parameters.AddWithValue("@PayableManagementFee", _fundAccountingSetup.PayableManagementFee);
                                cmd.Parameters.AddWithValue("@PayableCustodianFee", _fundAccountingSetup.PayableCustodianFee);
                                cmd.Parameters.AddWithValue("@PayableAuditFee", _fundAccountingSetup.PayableAuditFee);
                                cmd.Parameters.AddWithValue("@BrokerCommission", _fundAccountingSetup.BrokerCommission);
                                cmd.Parameters.AddWithValue("@BrokerLevy", _fundAccountingSetup.BrokerLevy);
                                cmd.Parameters.AddWithValue("@BrokerVat", _fundAccountingSetup.BrokerVat);
                                cmd.Parameters.AddWithValue("@BrokerSalesTax", _fundAccountingSetup.BrokerSalesTax);
                                cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _fundAccountingSetup.WithHoldingTaxPPH23);
                                cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _fundAccountingSetup.WHTTaxPayableAccrInterestBond);
                                cmd.Parameters.AddWithValue("@ManagementFeeExpense", _fundAccountingSetup.ManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@CustodianFeeExpense", _fundAccountingSetup.CustodianFeeExpense);
                                cmd.Parameters.AddWithValue("@AuditFeeExpense", _fundAccountingSetup.AuditFeeExpense);
                                cmd.Parameters.AddWithValue("@BankCharges", _fundAccountingSetup.BankCharges);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _fundAccountingSetup.TaxExpenseInterestIncomeBond);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _fundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                                cmd.Parameters.AddWithValue("@RealisedEquity", _fundAccountingSetup.RealisedEquity);
                                cmd.Parameters.AddWithValue("@UnrealisedBond", _fundAccountingSetup.UnrealisedBond);
                                cmd.Parameters.AddWithValue("@UnrealisedEquity", _fundAccountingSetup.UnrealisedEquity);
                                cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _fundAccountingSetup.DistributedIncomeAcc);
                                cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _fundAccountingSetup.DistributedIncomePayableAcc);
                                cmd.Parameters.AddWithValue("@PendingSubscription", _fundAccountingSetup.PendingSubscription);
                                cmd.Parameters.AddWithValue("@PendingRedemption", _fundAccountingSetup.PendingRedemption);
                                cmd.Parameters.AddWithValue("@RealisedBond", _fundAccountingSetup.RealisedBond);
                                cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _fundAccountingSetup.TaxCapitalGainBond);
                                cmd.Parameters.AddWithValue("@PayableSInvestFee", _fundAccountingSetup.PayableSInvestFee);
                                cmd.Parameters.AddWithValue("@SInvestFee", _fundAccountingSetup.SInvestFee);

                                cmd.Parameters.AddWithValue("@TaxPercentageDividend", _fundAccountingSetup.TaxPercentageDividend);
                                cmd.Parameters.AddWithValue("@TaxPercentageBond", _fundAccountingSetup.TaxPercentageBond);
                                cmd.Parameters.AddWithValue("@TaxPercentageTD", _fundAccountingSetup.TaxPercentageTD);
                                cmd.Parameters.AddWithValue("@TaxProvisionPercent", _fundAccountingSetup.TaxProvisionPercent);
                                cmd.Parameters.AddWithValue("@BondAmortization", _fundAccountingSetup.BondAmortization);
                                cmd.Parameters.AddWithValue("@PayableMovementFee", _fundAccountingSetup.PayableMovementFee);
                                cmd.Parameters.AddWithValue("@MovementFeeExpense", _fundAccountingSetup.MovementFeeExpense);
                                cmd.Parameters.AddWithValue("@TaxInterestBond", _fundAccountingSetup.TaxInterestBond);
                                cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _fundAccountingSetup.PayablePurchaseMutualFund);
                                cmd.Parameters.AddWithValue("@InvestmentMutualFund", _fundAccountingSetup.InvestmentMutualFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _fundAccountingSetup.AccountReceivableSaleMutualFund);
                                cmd.Parameters.AddWithValue("@RevaluationMutualFund", _fundAccountingSetup.RevaluationMutualFund);
                                cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _fundAccountingSetup.UnrealisedMutualFund);
                                cmd.Parameters.AddWithValue("@RealisedMutualFund", _fundAccountingSetup.RealisedMutualFund);

                                cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _fundAccountingSetup.PayableOtherFeeOne);
                                cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _fundAccountingSetup.PayableOtherFeeTwo);
                                cmd.Parameters.AddWithValue("@PayableOtherFeeThree", _fundAccountingSetup.PayableOtherFeeThree);
                                cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _fundAccountingSetup.OtherFeeOneExpense);
                                cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _fundAccountingSetup.OtherFeeTwoExpense);
                                cmd.Parameters.AddWithValue("@OtherFeeThreeExpense", _fundAccountingSetup.OtherFeeThreeExpense);

                                cmd.Parameters.AddWithValue("@PendingSwitching", _fundAccountingSetup.PendingSwitching);
                                cmd.Parameters.AddWithValue("@CurrentYearAccount", _fundAccountingSetup.CurrentYearAccount);
                                cmd.Parameters.AddWithValue("@PriorYearAccount", _fundAccountingSetup.PriorYearAccount);

                                //
                                cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _fundAccountingSetup.TaxPercentageCapitalGain);
                                //cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                                cmd.Parameters.AddWithValue("@InterestAccruedCurrentAccount", _fundAccountingSetup.InterestAccruedCurrentAccount);
                                cmd.Parameters.AddWithValue("@IncomeCurrentAccount", _fundAccountingSetup.IncomeCurrentAccount);
                                cmd.Parameters.AddWithValue("@InvInTDUSD", _fundAccountingSetup.InvInTDUSD);

                                cmd.Parameters.AddWithValue("@InvestmentSukuk", _fundAccountingSetup.InvestmentSukuk);
                                cmd.Parameters.AddWithValue("@InterestRecSukuk", _fundAccountingSetup.InterestRecSukuk);
                                cmd.Parameters.AddWithValue("@PayablePurRecSukuk", _fundAccountingSetup.PayablePurRecSukuk);
                                cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestSukuk", _fundAccountingSetup.WHTTaxPayableAccrInterestSukuk);
                                cmd.Parameters.AddWithValue("@InterestAccrSukuk", _fundAccountingSetup.InterestAccrSukuk);

                                cmd.Parameters.AddWithValue("@RealisedSukuk", _fundAccountingSetup.RealisedSukuk);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleSukuk", _fundAccountingSetup.AccountReceivableSaleSukuk);
                                cmd.Parameters.AddWithValue("@TaxCapitalGainSukuk", _fundAccountingSetup.TaxCapitalGainSukuk);
                                cmd.Parameters.AddWithValue("@InvestmentProtectedFund", _fundAccountingSetup.InvestmentProtectedFund);
                                cmd.Parameters.AddWithValue("@PayablePurchaseProtectedFund", _fundAccountingSetup.PayablePurchaseProtectedFund);
                                cmd.Parameters.AddWithValue("@InvestmentPrivateEquityFund", _fundAccountingSetup.InvestmentPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@PayablePurchasePrivateEquityFund", _fundAccountingSetup.PayablePurchasePrivateEquityFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleProtectedFund", _fundAccountingSetup.AccountReceivableSaleProtectedFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSalePrivateEquityFund", _fundAccountingSetup.AccountReceivableSalePrivateEquityFund);

                                cmd.Parameters.AddWithValue("@RevaluationSukuk", _fundAccountingSetup.RevaluationSukuk);

                                cmd.Parameters.AddWithValue("@UnrealisedSukuk", _fundAccountingSetup.UnrealisedSukuk);
                                cmd.Parameters.AddWithValue("@CashForMutualFund", _fundAccountingSetup.CashForMutualFund);
                                cmd.Parameters.AddWithValue("@RevaluationProtectedFund", _fundAccountingSetup.RevaluationProtectedFund);
                                cmd.Parameters.AddWithValue("@RevaluationPrivateEquityFund", _fundAccountingSetup.RevaluationPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@UnrealisedProtectedFund", _fundAccountingSetup.UnrealisedProtectedFund);
                                cmd.Parameters.AddWithValue("@UnrealisedPrivateEquityFund", _fundAccountingSetup.UnrealisedPrivateEquityFund);

                                cmd.Parameters.AddWithValue("@InterestReceivableSellSukuk", _fundAccountingSetup.InterestReceivableSellSukuk);

                                cmd.Parameters.AddWithValue("@InterestRecSellBond", _fundAccountingSetup.InterestRecSellBond);

                                cmd.Parameters.AddWithValue("@InterestReceivableBuySukuk", _fundAccountingSetup.InterestReceivableBuySukuk);
                                cmd.Parameters.AddWithValue("@InterestReceivableSellMutualFund", _fundAccountingSetup.InterestReceivableSellMutualFund);
                                cmd.Parameters.AddWithValue("@InterestReceivableSellProtectedFund", _fundAccountingSetup.InterestReceivableSellProtectedFund);
                                cmd.Parameters.AddWithValue("@InterestReceivableSellPrivateEquityFund", _fundAccountingSetup.InterestReceivableSellPrivateEquityFund);

                                cmd.Parameters.AddWithValue("@IncomeInterestSukuk", _fundAccountingSetup.IncomeInterestSukuk);
                                cmd.Parameters.AddWithValue("@InterestAccrMutualFund", _fundAccountingSetup.InterestAccrMutualFund);
                                cmd.Parameters.AddWithValue("@InterestAccrProtectedFund", _fundAccountingSetup.InterestAccrProtectedFund);
                                cmd.Parameters.AddWithValue("@InterestAccrPrivateEquityFund", _fundAccountingSetup.InterestAccrPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@IncomeInterestAccrMutualFund", _fundAccountingSetup.IncomeInterestAccrMutualFund);
                                cmd.Parameters.AddWithValue("@IncomeInterestAccrProtectedFund", _fundAccountingSetup.IncomeInterestAccrProtectedFund);
                                cmd.Parameters.AddWithValue("@IncomeInterestAccrPrivateEquityFund", _fundAccountingSetup.IncomeInterestAccrPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@APManagementFee", _fundAccountingSetup.APManagementFee);

                                cmd.Parameters.AddWithValue("@InvestmentRights", _fundAccountingSetup.InvestmentRights);
                                cmd.Parameters.AddWithValue("@PayablePurchaseRights", _fundAccountingSetup.PayablePurchaseRights);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleRights", _fundAccountingSetup.AccountReceivableSaleRights);
                                cmd.Parameters.AddWithValue("@RealisedRights", _fundAccountingSetup.RealisedRights);
                                cmd.Parameters.AddWithValue("@RevaluationRights", _fundAccountingSetup.RevaluationRights);
                                cmd.Parameters.AddWithValue("@UnrealisedRights", _fundAccountingSetup.UnrealisedRights);
                                cmd.Parameters.AddWithValue("@CashForIPO", _fundAccountingSetup.CashForIPO);

                                cmd.Parameters.AddWithValue("@BitAccruedInterestGiroDaily", _fundAccountingSetup.BitAccruedInterestGiroDaily);
                                cmd.Parameters.AddWithValue("@AveragePriority", _fundAccountingSetup.AveragePriority);
                                cmd.Parameters.AddWithValue("@AveragePriorityBond", _fundAccountingSetup.AveragePriorityBond);
                                //cmd.Parameters.AddWithValue("@AveragePriorityDesc", _fundAccountingSetup.AveragePriorityDesc);

                                cmd.Parameters.AddWithValue("@TaxProvision", _fundAccountingSetup.TaxProvision);
                                cmd.Parameters.AddWithValue("@PayableTaxProvision", _fundAccountingSetup.PayableTaxProvision);

                                cmd.Parameters.AddWithValue("@InvestmentWarrant", _fundAccountingSetup.InvestmentWarrant);
                                cmd.Parameters.AddWithValue("@PayablePurchaseWarrant", _fundAccountingSetup.PayablePurchaseWarrant);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleWarrant", _fundAccountingSetup.AccountReceivableSaleWarrant);
                                cmd.Parameters.AddWithValue("@RealisedWarrant", _fundAccountingSetup.RealisedWarrant);
                                cmd.Parameters.AddWithValue("@RevaluationWarrant", _fundAccountingSetup.RevaluationWarrant);
                                cmd.Parameters.AddWithValue("@UnrealisedWarrant", _fundAccountingSetup.UnrealisedWarrant);
                                cmd.Parameters.AddWithValue("@CBESTExpense", _fundAccountingSetup.CBESTExpense);
                                cmd.Parameters.AddWithValue("@PayableCBEST", _fundAccountingSetup.PayableCBEST);
                                cmd.Parameters.AddWithValue("@TaxPercentageCapitalGainSell", _fundAccountingSetup.TaxPercentageCapitalGainSell);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundAccountingSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_fundAccountingSetup.FundAccountingSetupPK, "FundAccountingSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundAccountingSetup where FundAccountingSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundAccountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _fundAccountingSetup.FundPK);
                                cmd.Parameters.AddWithValue("@Subscription", _fundAccountingSetup.Subscription);
                                cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _fundAccountingSetup.PayableSubscriptionFee);
                                cmd.Parameters.AddWithValue("@Redemption", _fundAccountingSetup.Redemption);
                                cmd.Parameters.AddWithValue("@PayableRedemptionFee", _fundAccountingSetup.PayableRedemptionFee);
                                cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                                cmd.Parameters.AddWithValue("@PayableSwitchingFee", _fundAccountingSetup.PayableSwitchingFee);
                                cmd.Parameters.AddWithValue("@InvestmentEquity", _fundAccountingSetup.InvestmentEquity);
                                cmd.Parameters.AddWithValue("@InvestmentBond", _fundAccountingSetup.InvestmentBond);
                                cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _fundAccountingSetup.InvestmentTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestRecBond", _fundAccountingSetup.InterestRecBond);
                                cmd.Parameters.AddWithValue("@InterestAccrBond", _fundAccountingSetup.InterestAccrBond);
                                cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _fundAccountingSetup.InterestAccrTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestAccrGiro", _fundAccountingSetup.InterestAccrGiro);
                                cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _fundAccountingSetup.PrepaidTaxDividend);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _fundAccountingSetup.AccountReceivableSaleBond);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _fundAccountingSetup.AccountReceivableSaleEquity);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _fundAccountingSetup.AccountReceivableSaleTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestBond", _fundAccountingSetup.IncomeInterestBond);
                                cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _fundAccountingSetup.IncomeInterestTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestGiro", _fundAccountingSetup.IncomeInterestGiro);
                                cmd.Parameters.AddWithValue("@IncomeDividend", _fundAccountingSetup.IncomeDividend);
                                cmd.Parameters.AddWithValue("@ARDividend", _fundAccountingSetup.ARDividend);
                                cmd.Parameters.AddWithValue("@RevaluationBond", _fundAccountingSetup.RevaluationBond);
                                cmd.Parameters.AddWithValue("@RevaluationEquity", _fundAccountingSetup.RevaluationEquity);
                                cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _fundAccountingSetup.PayablePurchaseEquity);
                                cmd.Parameters.AddWithValue("@PayablePurRecBond", _fundAccountingSetup.PayablePurRecBond);
                                cmd.Parameters.AddWithValue("@PayableManagementFee", _fundAccountingSetup.PayableManagementFee);
                                cmd.Parameters.AddWithValue("@PayableCustodianFee", _fundAccountingSetup.PayableCustodianFee);
                                cmd.Parameters.AddWithValue("@PayableAuditFee", _fundAccountingSetup.PayableAuditFee);
                                cmd.Parameters.AddWithValue("@BrokerCommission", _fundAccountingSetup.BrokerCommission);
                                cmd.Parameters.AddWithValue("@BrokerLevy", _fundAccountingSetup.BrokerLevy);
                                cmd.Parameters.AddWithValue("@BrokerVat", _fundAccountingSetup.BrokerVat);
                                cmd.Parameters.AddWithValue("@BrokerSalesTax", _fundAccountingSetup.BrokerSalesTax);
                                cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _fundAccountingSetup.WithHoldingTaxPPH23);
                                cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _fundAccountingSetup.WHTTaxPayableAccrInterestBond);
                                cmd.Parameters.AddWithValue("@ManagementFeeExpense", _fundAccountingSetup.ManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@CustodianFeeExpense", _fundAccountingSetup.CustodianFeeExpense);
                                cmd.Parameters.AddWithValue("@AuditFeeExpense", _fundAccountingSetup.AuditFeeExpense);
                                cmd.Parameters.AddWithValue("@BankCharges", _fundAccountingSetup.BankCharges);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _fundAccountingSetup.TaxExpenseInterestIncomeBond);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _fundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                                cmd.Parameters.AddWithValue("@RealisedEquity", _fundAccountingSetup.RealisedEquity);
                                cmd.Parameters.AddWithValue("@UnrealisedBond", _fundAccountingSetup.UnrealisedBond);
                                cmd.Parameters.AddWithValue("@UnrealisedEquity", _fundAccountingSetup.UnrealisedEquity);
                                cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _fundAccountingSetup.DistributedIncomeAcc);
                                cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _fundAccountingSetup.DistributedIncomePayableAcc);
                                cmd.Parameters.AddWithValue("@PendingSubscription", _fundAccountingSetup.PendingSubscription);
                                cmd.Parameters.AddWithValue("@PendingRedemption", _fundAccountingSetup.PendingRedemption);
                                cmd.Parameters.AddWithValue("@RealisedBond", _fundAccountingSetup.RealisedBond);
                                cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _fundAccountingSetup.TaxCapitalGainBond);
                                cmd.Parameters.AddWithValue("@PayableSInvestFee", _fundAccountingSetup.PayableSInvestFee);
                                cmd.Parameters.AddWithValue("@SInvestFee", _fundAccountingSetup.SInvestFee);
                                cmd.Parameters.AddWithValue("@TaxInterestBond", _fundAccountingSetup.TaxInterestBond);
                                cmd.Parameters.AddWithValue("@TaxPercentageDividend", _fundAccountingSetup.TaxPercentageDividend);
                                cmd.Parameters.AddWithValue("@TaxPercentageBond", _fundAccountingSetup.TaxPercentageBond);
                                cmd.Parameters.AddWithValue("@TaxPercentageTD", _fundAccountingSetup.TaxPercentageTD);
                                cmd.Parameters.AddWithValue("@TaxProvisionPercent", _fundAccountingSetup.TaxProvisionPercent);
                                cmd.Parameters.AddWithValue("@BondAmortization", _fundAccountingSetup.BondAmortization);
                                cmd.Parameters.AddWithValue("@PayableMovementFee", _fundAccountingSetup.PayableMovementFee);
                                cmd.Parameters.AddWithValue("@MovementFeeExpense", _fundAccountingSetup.MovementFeeExpense);

                                cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _fundAccountingSetup.PayablePurchaseMutualFund);
                                cmd.Parameters.AddWithValue("@InvestmentMutualFund", _fundAccountingSetup.InvestmentMutualFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _fundAccountingSetup.AccountReceivableSaleMutualFund);
                                cmd.Parameters.AddWithValue("@RevaluationMutualFund", _fundAccountingSetup.RevaluationMutualFund);
                                cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _fundAccountingSetup.UnrealisedMutualFund);
                                cmd.Parameters.AddWithValue("@RealisedMutualFund", _fundAccountingSetup.RealisedMutualFund);

                                cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _fundAccountingSetup.PayableOtherFeeOne);
                                cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _fundAccountingSetup.PayableOtherFeeTwo);
                                cmd.Parameters.AddWithValue("@PayableOtherFeeThree", _fundAccountingSetup.PayableOtherFeeThree);
                                cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _fundAccountingSetup.OtherFeeOneExpense);
                                cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _fundAccountingSetup.OtherFeeTwoExpense);
                                cmd.Parameters.AddWithValue("@OtherFeeThreeExpense", _fundAccountingSetup.OtherFeeThreeExpense);

                                cmd.Parameters.AddWithValue("@PendingSwitching", _fundAccountingSetup.PendingSwitching);
                                cmd.Parameters.AddWithValue("@CurrentYearAccount", _fundAccountingSetup.CurrentYearAccount);
                                cmd.Parameters.AddWithValue("@PriorYearAccount", _fundAccountingSetup.PriorYearAccount);

                                //
                                cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _fundAccountingSetup.TaxPercentageCapitalGain);
                                //cmd.Parameters.AddWithValue("@Switching", _fundAccountingSetup.Switching);
                                cmd.Parameters.AddWithValue("@InterestAccruedCurrentAccount", _fundAccountingSetup.InterestAccruedCurrentAccount);
                                cmd.Parameters.AddWithValue("@IncomeCurrentAccount", _fundAccountingSetup.IncomeCurrentAccount);
                                cmd.Parameters.AddWithValue("@InvInTDUSD", _fundAccountingSetup.InvInTDUSD);

                                cmd.Parameters.AddWithValue("@InvestmentSukuk", _fundAccountingSetup.InvestmentSukuk);
                                cmd.Parameters.AddWithValue("@InterestRecSukuk", _fundAccountingSetup.InterestRecSukuk);
                                cmd.Parameters.AddWithValue("@PayablePurRecSukuk", _fundAccountingSetup.PayablePurRecSukuk);
                                cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestSukuk", _fundAccountingSetup.WHTTaxPayableAccrInterestSukuk);
                                cmd.Parameters.AddWithValue("@InterestAccrSukuk", _fundAccountingSetup.InterestAccrSukuk);

                                cmd.Parameters.AddWithValue("@RealisedSukuk", _fundAccountingSetup.RealisedSukuk);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleSukuk", _fundAccountingSetup.AccountReceivableSaleSukuk);
                                cmd.Parameters.AddWithValue("@TaxCapitalGainSukuk", _fundAccountingSetup.TaxCapitalGainSukuk);
                                cmd.Parameters.AddWithValue("@InvestmentProtectedFund", _fundAccountingSetup.InvestmentProtectedFund);
                                cmd.Parameters.AddWithValue("@PayablePurchaseProtectedFund", _fundAccountingSetup.PayablePurchaseProtectedFund);
                                cmd.Parameters.AddWithValue("@InvestmentPrivateEquityFund", _fundAccountingSetup.InvestmentPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@PayablePurchasePrivateEquityFund", _fundAccountingSetup.PayablePurchasePrivateEquityFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleProtectedFund", _fundAccountingSetup.AccountReceivableSaleProtectedFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSalePrivateEquityFund", _fundAccountingSetup.AccountReceivableSalePrivateEquityFund);

                                cmd.Parameters.AddWithValue("@RevaluationSukuk", _fundAccountingSetup.RevaluationSukuk);

                                cmd.Parameters.AddWithValue("@UnrealisedSukuk", _fundAccountingSetup.UnrealisedSukuk);
                                cmd.Parameters.AddWithValue("@CashForMutualFund", _fundAccountingSetup.CashForMutualFund);
                                cmd.Parameters.AddWithValue("@RevaluationProtectedFund", _fundAccountingSetup.RevaluationProtectedFund);
                                cmd.Parameters.AddWithValue("@RevaluationPrivateEquityFund", _fundAccountingSetup.RevaluationPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@UnrealisedProtectedFund", _fundAccountingSetup.UnrealisedProtectedFund);
                                cmd.Parameters.AddWithValue("@UnrealisedPrivateEquityFund", _fundAccountingSetup.UnrealisedPrivateEquityFund);

                                cmd.Parameters.AddWithValue("@InterestReceivableSellSukuk", _fundAccountingSetup.InterestReceivableSellSukuk);

                                cmd.Parameters.AddWithValue("@InterestRecSellBond", _fundAccountingSetup.InterestRecSellBond);

                                cmd.Parameters.AddWithValue("@InterestReceivableBuySukuk", _fundAccountingSetup.InterestReceivableBuySukuk);
                                cmd.Parameters.AddWithValue("@InterestReceivableSellMutualFund", _fundAccountingSetup.InterestReceivableSellMutualFund);
                                cmd.Parameters.AddWithValue("@InterestReceivableSellProtectedFund", _fundAccountingSetup.InterestReceivableSellProtectedFund);
                                cmd.Parameters.AddWithValue("@InterestReceivableSellPrivateEquityFund", _fundAccountingSetup.InterestReceivableSellPrivateEquityFund);

                                cmd.Parameters.AddWithValue("@IncomeInterestSukuk", _fundAccountingSetup.IncomeInterestSukuk);
                                cmd.Parameters.AddWithValue("@InterestAccrMutualFund", _fundAccountingSetup.InterestAccrMutualFund);
                                cmd.Parameters.AddWithValue("@InterestAccrProtectedFund", _fundAccountingSetup.InterestAccrProtectedFund);
                                cmd.Parameters.AddWithValue("@InterestAccrPrivateEquityFund", _fundAccountingSetup.InterestAccrPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@IncomeInterestAccrMutualFund", _fundAccountingSetup.IncomeInterestAccrMutualFund);
                                cmd.Parameters.AddWithValue("@IncomeInterestAccrProtectedFund", _fundAccountingSetup.IncomeInterestAccrProtectedFund);
                                cmd.Parameters.AddWithValue("@IncomeInterestAccrPrivateEquityFund", _fundAccountingSetup.IncomeInterestAccrPrivateEquityFund);
                                cmd.Parameters.AddWithValue("@APManagementFee", _fundAccountingSetup.APManagementFee);

                                cmd.Parameters.AddWithValue("@InvestmentRights", _fundAccountingSetup.InvestmentRights);
                                cmd.Parameters.AddWithValue("@PayablePurchaseRights", _fundAccountingSetup.PayablePurchaseRights);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleRights", _fundAccountingSetup.AccountReceivableSaleRights);
                                cmd.Parameters.AddWithValue("@RealisedRights", _fundAccountingSetup.RealisedRights);
                                cmd.Parameters.AddWithValue("@RevaluationRights", _fundAccountingSetup.RevaluationRights);
                                cmd.Parameters.AddWithValue("@UnrealisedRights", _fundAccountingSetup.UnrealisedRights);
                                cmd.Parameters.AddWithValue("@CashForIPO", _fundAccountingSetup.CashForIPO);

                                cmd.Parameters.AddWithValue("@BitAccruedInterestGiroDaily", _fundAccountingSetup.BitAccruedInterestGiroDaily);
                                cmd.Parameters.AddWithValue("@AveragePriority", _fundAccountingSetup.AveragePriority);
                                cmd.Parameters.AddWithValue("@AveragePriorityBond", _fundAccountingSetup.AveragePriorityBond);
                                //cmd.Parameters.AddWithValue("@AveragePriorityDesc", _fundAccountingSetup.AveragePriorityDesc);

                                cmd.Parameters.AddWithValue("@TaxProvision", _fundAccountingSetup.TaxProvision);
                                cmd.Parameters.AddWithValue("@PayableTaxProvision", _fundAccountingSetup.PayableTaxProvision);

                                cmd.Parameters.AddWithValue("@InvestmentWarrant", _fundAccountingSetup.InvestmentWarrant);
                                cmd.Parameters.AddWithValue("@PayablePurchaseWarrant", _fundAccountingSetup.PayablePurchaseWarrant);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleWarrant", _fundAccountingSetup.AccountReceivableSaleWarrant);
                                cmd.Parameters.AddWithValue("@RealisedWarrant", _fundAccountingSetup.RealisedWarrant);
                                cmd.Parameters.AddWithValue("@RevaluationWarrant", _fundAccountingSetup.RevaluationWarrant);
                                cmd.Parameters.AddWithValue("@UnrealisedWarrant", _fundAccountingSetup.UnrealisedWarrant);
                                cmd.Parameters.AddWithValue("@CBESTExpense", _fundAccountingSetup.CBESTExpense);
                                cmd.Parameters.AddWithValue("@PayableCBEST", _fundAccountingSetup.PayableCBEST);
                                cmd.Parameters.AddWithValue("@TaxPercentageCapitalGainSell", _fundAccountingSetup.TaxPercentageCapitalGainSell);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundAccountingSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundAccountingSetup set status = 4, Notes=@Notes, " +
                                " lastupdate=@lastupdate where FundAccountingSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _fundAccountingSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundAccountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return _newHisPK;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FundAccountingSetup_Approved(FundAccountingSetup _fundAccountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundAccountingSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FundAccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundAccountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundAccountingSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundAccountingSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundAccountingSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundAccountingSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FundAccountingSetup_Reject(FundAccountingSetup _fundAccountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundAccountingSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FundAccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundAccountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundAccountingSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundAccountingSetup set status= 2,lastupdate=@lastupdate where FundAccountingSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FundAccountingSetup_Void(FundAccountingSetup _fundAccountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundAccountingSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where FundAccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundAccountingSetup.FundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundAccountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundAccountingSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart ) 


        public string Validate_CheckCopyFundAccountingSetup(int _fundTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
                        if exists(select FundPK from FundAccountingSetup where status in (1,2) and FundPK = @FundPK)
                        BEGIN
                        SELECT 'Copy Cancel, Fund Already Exists' as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundTo);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return " ";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string Copy_FundAccountingSetup(ParamFundAccountingSetup _paramFundAccountingSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //if (_paramFundAccountingSetup.BitDefaultFund == true)


                        cmd.CommandText = @"
                        
                        declare @FundAccountingSetupPK int
                        Select @FundAccountingSetupPK = max(FundAccountingSetupPK) + 1 From FundAccountingSetup
                        set @FundAccountingSetupPK = isnull(@FundAccountingSetupPK,1)
                     
INSERT INTO [dbo].[FundAccountingSetup]
           ([FundAccountingSetupPK]
           ,[HistoryPK]
           ,[Status]
           ,[Notes]
           ,[FundPK]
           ,[TaxPercentageDividend]
           ,[TaxPercentageBond]
           ,[TaxPercentageTD]
           ,[TaxPercentageCapitalGain]
           ,[Subscription]
           ,[PayableSwitchingFee]
           ,[PayableSubscriptionFee]
           ,[PayableRedemptionFee]
           ,[Redemption]
           ,[Switching]
           ,[InvestmentEquity]
           ,[InvestmentBond]
           ,[InvestmentTimeDeposit]
           ,[InterestRecBond]
           ,[InterestAccrBond]
           ,[InterestAccrTimeDeposit]
           ,[InterestAccrGiro]
           ,[PrepaidTaxDividend]
           ,[AccountReceivableSaleBond]
           ,[AccountReceivableSaleEquity]
           ,[AccountReceivableSaleTimeDeposit]
           ,[IncomeInterestBond]
           ,[IncomeInterestTimeDeposit]
           ,[IncomeInterestGiro]
           ,[IncomeDividend]
           ,[ARDividend]
           ,[RevaluationBond]
           ,[RevaluationEquity]
           ,[PayablePurchaseEquity]
           ,[PayablePurRecBond]
           ,[PayableManagementFee]
           ,[PayableCustodianFee]
           ,[PayableAuditFee]
           ,[BrokerCommission]
           ,[BrokerLevy]
           ,[BrokerVat]
           ,[BrokerSalesTax]
           ,[WithHoldingTaxPPH23]
           ,[WHTTaxPayableAccrInterestBond]
           ,[ManagementFeeExpense]
           ,[CustodianFeeExpense]
           ,[AuditFeeExpense]
           ,[BankCharges]
           ,[TaxExpenseInterestIncomeBond]
           ,[TaxExpenseInterestIncomeTimeDeposit]
           ,[RealisedEquity]
           ,[RealisedBond]
           ,[UnrealisedBond]
           ,[UnrealisedEquity]
           ,[DistributedIncomeAcc]
           ,[DistributedIncomePayableAcc]
           ,[PendingSubscription]
           ,[TaxCapitalGainBond]
           ,[PendingRedemption]

           ,[PayableSInvestFee]
           ,[SInvestFee]
           ,[BondAmortization]
           ,[PayableMovementFee]
           ,[MovementFeeExpense]
           ,[PayablePurchaseMutualFund]
           ,[InvestmentMutualFund]
           ,[AccountReceivableSaleMutualFund]
           ,[RevaluationMutualFund]
           ,[UnrealisedMutualFund]
           ,[RealisedMutualFund]

           ,[PayableOtherFeeOne]
           ,[PayableOtherFeeTwo]
           ,[OtherFeeOneExpense]
           ,[OtherFeeTwoExpense]

,[InterestAccruedCurrentAccount]
,[IncomeCurrentAccount]
,[InvInTDUSD]
,[InvestmentSukuk]
,[InterestRecSukuk]
,[PayablePurRecSukuk]
,[WHTTaxPayableAccrInterestSukuk]
,[InterestAccrSukuk]
,[RealisedSukuk]
,[AccountReceivableSaleSukuk]
,[TaxCapitalGainSukuk]
,[InvestmentProtectedFund]
,[PayablePurchaseProtectedFund]
,[InvestmentPrivateEquityFund]
,[PayablePurchasePrivateEquityFund]
,[AccountReceivableSaleProtectedFund]
,[AccountReceivableSalePrivateEquityFund]

,[RevaluationSukuk]
,[UnrealisedSukuk]
,[CashForMutualFund]
,[RevaluationProtectedFund]
,[RevaluationPrivateEquityFund]
,[UnrealisedProtectedFund]
,[UnrealisedPrivateEquityFund]
,[InterestReceivableSellSukuk]
,[InterestRecSellBond]
,[InterestReceivableBuySukuk]
,[InterestReceivableSellMutualFund]
,[InterestReceivableSellProtectedFund]
,[InterestReceivableSellPrivateEquityFund]
,[IncomeInterestSukuk]
,[InterestAccrMutualFund]
,[InterestAccrProtectedFund]
,[InterestAccrPrivateEquityFund]
,[IncomeInterestAccrMutualFund]
,[IncomeInterestAccrProtectedFund]
,[IncomeInterestAccrPrivateEquityFund]
,[APManagementFee]

,[InvestmentRights]
,[PayablePurchaseRights]
,[AccountReceivableSaleRights]
,[RealisedRights]
,[RevaluationRights]
,[UnrealisedRights]
,[CashForIPO]
,[BitAccruedInterestGiroDaily]
,[AveragePriority]
,[AveragePriorityBond]
,[TaxInterestBond]
,[PayableOtherFeeThree]
,[OtherFeeThreeExpense]
,[TaxProvision]
,[PayableTaxProvision]

,[PayableCBEST]
,[CBESTExpense]
,[InvestmentWarrant]
,[PayablePurchaseWarrant]
,[AccountReceivableSaleWarrant]
,[RealisedWarrant]
,[RevaluationWarrant]
,[UnrealisedWarrant]
,[TaxPercentageCapitalGainSell]
,[RevaluationEBA]
,[UnrealisedEBA]
,[InvestmentEBA]
,[InterestRecEBA]
,[InterestRecSellEBA]
,[PayablePurRecEBA]
,[AccountReceivableSaleEBA]
,[InterestAccrEBA]
,[RealisedEBA]
,[TaxCapitalGainEBA]
,[WhtTaxPayableAccrInterestEBA]
,[TaxExpenseInterestIncomeEBA]
,[IncomeInterestEBA]
,[OtherReceivable]
,[BuyOTC]
,[SellOTC]
,[BitActDivDays]
,[TaxProvisionPercent]

           ,[PendingSwitching]
           ,[CurrentYearAccount]
           ,[PriorYearAccount]
           ,[EntryUsersID]
           ,[EntryTime]
           ,[LastUpdate]
         )
                        Select @FundAccountingSetupPK,1,1,'',@FundPKTo ,[TaxPercentageDividend]
           ,[TaxPercentageBond]
           ,[TaxPercentageTD]
           ,[TaxPercentageCapitalGain]
           ,[Subscription]
           ,[PayableSwitchingFee]
           ,[PayableSubscriptionFee]
           ,[PayableRedemptionFee]
           ,[Redemption]
           ,[Switching]
           ,[InvestmentEquity]
           ,[InvestmentBond]
           ,[InvestmentTimeDeposit]
           ,[InterestRecBond]
           ,[InterestAccrBond]
           ,[InterestAccrTimeDeposit]
           ,[InterestAccrGiro]
           ,[PrepaidTaxDividend]
           ,[AccountReceivableSaleBond]
           ,[AccountReceivableSaleEquity]
           ,[AccountReceivableSaleTimeDeposit]
           ,[IncomeInterestBond]
           ,[IncomeInterestTimeDeposit]
           ,[IncomeInterestGiro]
           ,[IncomeDividend]
           ,[ARDividend]
           ,[RevaluationBond]
           ,[RevaluationEquity]
           ,[PayablePurchaseEquity]
           ,[PayablePurRecBond]
           ,[PayableManagementFee]
           ,[PayableCustodianFee]
           ,[PayableAuditFee]
           ,[BrokerCommission]
           ,[BrokerLevy]
           ,[BrokerVat]
           ,[BrokerSalesTax]
           ,[WithHoldingTaxPPH23]
           ,[WHTTaxPayableAccrInterestBond]
           ,[ManagementFeeExpense]
           ,[CustodianFeeExpense]
           ,[AuditFeeExpense]
           ,[BankCharges]
           ,[TaxExpenseInterestIncomeBond]
           ,[TaxExpenseInterestIncomeTimeDeposit]
           ,[RealisedEquity]
           ,[RealisedBond]
           ,[UnrealisedBond]
           ,[UnrealisedEquity]
           ,[DistributedIncomeAcc]
           ,[DistributedIncomePayableAcc]
           ,[PendingSubscription]
           ,[TaxCapitalGainBond]
           ,[PendingRedemption]

           ,[PayableSInvestFee]
           ,[SInvestFee]
           ,[BondAmortization]
           ,[PayableMovementFee]
           ,[MovementFeeExpense]
           ,[PayablePurchaseMutualFund]
           ,[InvestmentMutualFund]
           ,[AccountReceivableSaleMutualFund]
           ,[RevaluationMutualFund]
           ,[UnrealisedMutualFund]
           ,[RealisedMutualFund]
           ,[PayableOtherFeeOne]
           ,[PayableOtherFeeTwo]
           ,[OtherFeeOneExpense]
           ,[OtherFeeTwoExpense]

,[InterestAccruedCurrentAccount]
,[IncomeCurrentAccount]
,[InvInTDUSD]
,[InvestmentSukuk]
,[InterestRecSukuk]
,[PayablePurRecSukuk]
,[WHTTaxPayableAccrInterestSukuk]
,[InterestAccrSukuk]
,[RealisedSukuk]
,[AccountReceivableSaleSukuk]
,[TaxCapitalGainSukuk]
,[InvestmentProtectedFund]
,[PayablePurchaseProtectedFund]
,[InvestmentPrivateEquityFund]
,[PayablePurchasePrivateEquityFund]
,[AccountReceivableSaleProtectedFund]
,[AccountReceivableSalePrivateEquityFund]
,[RevaluationSukuk]
,[UnrealisedSukuk]
,[CashForMutualFund]
,[RevaluationProtectedFund]
,[RevaluationPrivateEquityFund]
,[UnrealisedProtectedFund]
,[UnrealisedPrivateEquityFund]
,[InterestReceivableSellSukuk]
,[InterestRecSellBond]
,[InterestReceivableBuySukuk]
,[InterestReceivableSellMutualFund]
,[InterestReceivableSellProtectedFund]
,[InterestReceivableSellPrivateEquityFund]
,[IncomeInterestSukuk]
,[InterestAccrMutualFund]
,[InterestAccrProtectedFund]
,[InterestAccrPrivateEquityFund]
,[IncomeInterestAccrMutualFund]
,[IncomeInterestAccrProtectedFund]
,[IncomeInterestAccrPrivateEquityFund]
,[APManagementFee]

,[InvestmentRights]
,[PayablePurchaseRights]
,[AccountReceivableSaleRights]
,[RealisedRights]
,[RevaluationRights]
,[UnrealisedRights]
,[CashForIPO]
,[BitAccruedInterestGiroDaily]
,[AveragePriority]
,[AveragePriorityBond]
,[TaxInterestBond]
,[PayableOtherFeeThree]
,[OtherFeeThreeExpense]
,[TaxProvision]
,[PayableTaxProvision]

,[PayableCBEST]
,[CBESTExpense]
,[InvestmentWarrant]
,[PayablePurchaseWarrant]
,[AccountReceivableSaleWarrant]
,[RealisedWarrant]
,[RevaluationWarrant]
,[UnrealisedWarrant]
,[TaxPercentageCapitalGainSell]
,[RevaluationEBA]
,[UnrealisedEBA]
,[InvestmentEBA]
,[InterestRecEBA]
,[InterestRecSellEBA]
,[PayablePurRecEBA]
,[AccountReceivableSaleEBA]
,[InterestAccrEBA]
,[RealisedEBA]
,[TaxCapitalGainEBA]
,[WhtTaxPayableAccrInterestEBA]
,[TaxExpenseInterestIncomeEBA]
,[IncomeInterestEBA]
,[OtherReceivable]
,[BuyOTC]
,[SellOTC]
,[BitActDivDays]
,[TaxProvisionPercent]


           ,[PendingSwitching]
           ,[CurrentYearAccount]
           ,[PriorYearAccount]
         ,@EntryUsersID,@TimeNow,@TimeNow
                            from FundAccountingSetup where FundPK = @FundPKFrom and status = 2

                       
                        select 'Copy Fund Accounting Setup Success' as Msg
                          ";
                        cmd.Parameters.AddWithValue("@FundPKFrom", _paramFundAccountingSetup.ParamFundFrom);


                        cmd.Parameters.AddWithValue("@FundPKTo", _paramFundAccountingSetup.ParamFundTo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _paramFundAccountingSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Msg"]);

                            }
                            return "Data Not Copy";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


    }
}