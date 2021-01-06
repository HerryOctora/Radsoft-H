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
    public class TemplateFundAccountingSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"INSERT INTO [dbo].[TemplateFundAccountingSetup]
                            ([TemplateFundAccountingSetupPK],[HistoryPK],[Status],[Subscription],[PayableSubscriptionFee],[Redemption],[PayableRedemptionFee],[TaxPercentageDividend],[TaxPercentageBond],[TaxPercentageTD],[Switching],[PayableSwitchingFee],[PayableSInvestFee],[SInvestFee],
                            [InvestmentEquity],[InvestmentBond],[InvestmentTimeDeposit],[InterestRecBond],[InterestAccrBond],[InterestAccrTimeDeposit],[InterestAccrGiro],   
                            [PrepaidTaxDividend],[AccountReceivableSaleBond],[AccountReceivableSaleEquity],[AccountReceivableSaleTimeDeposit],[IncomeInterestBond],[IncomeInterestTimeDeposit],[IncomeInterestGiro],
                            [IncomeDividend],[ARDividend],[RevaluationBond],[RevaluationEquity],[PayablePurchaseEquity],[PayablePurRecBond],[PayableManagementFee],[PayableCustodianFee],
                            [PayableAuditFee],[BrokerCommission],[BrokerLevy],[BrokerVat],[BrokerSalesTax],[WithHoldingTaxPPH23],[WHTTaxPayableAccrInterestBond],
                            [ManagementFeeExpense],[CustodianFeeExpense],[AuditFeeExpense],[BankCharges],[TaxExpenseInterestIncomeBond],[TaxExpenseInterestIncomeTimeDeposit],[RealisedEquity],[RealisedBond],
                            [UnrealisedBond],[UnrealisedEquity],[DistributedIncomeAcc],[DistributedIncomePayableAcc],[PendingSubscription],[PendingRedemption],[TaxCapitalGainBond],[TaxPercentageCapitalGain],[PayableMovementFee],[MovementFeeExpense],
                            [PayablePurchaseMutualFund],[InvestmentMutualFund],[AccountReceivableSaleMutualFund],[RevaluationMutualFund],[UnrealisedMutualFund],[RealisedMutualFund],[PayableOtherFeeOne],[PayableOtherFeeTwo],[OtherFeeOneExpense],[OtherFeeTwoExpense],[BondAmortization],[PendingSwitching],[CurrentYearAccount],[PriorYearAccount],";

        string _paramaterCommand = @" @Subscription,@PayableSubscriptionFee,@Redemption,@PayableRedemptionFee,@TaxPercentageDividend,@TaxPercentageBond,@TaxPercentageTD,@Switching,@PayableSwitchingFee,@PayableSInvestFee,@SInvestFee,@InvestmentEquity,@InvestmentBond,@InvestmentTimeDeposit,@InterestRecBond,@InterestAccrBond,@InterestAccrTimeDeposit,@InterestAccrGiro,
                                      @PrepaidTaxDividend,@AccountReceivableSaleBond,@AccountReceivableSaleEquity,@AccountReceivableSaleTimeDeposit,@IncomeInterestBond,@IncomeInterestTimeDeposit,@IncomeInterestGiro,@IncomeDividend,@ARDividend,
                                      @RevaluationBond,@RevaluationEquity,@PayablePurchaseEquity,@PayablePurRecBond,@PayableManagementFee,@PayableCustodianFee,@PayableAuditFee,@BrokerCommission,@BrokerLevy,@BrokerVat,@BrokerSalesTax,
                                      @WithHoldingTaxPPH23,@WHTTaxPayableAccrInterestBond,@ManagementFeeExpense,@CustodianFeeExpense,@AuditFeeExpense,@BankCharges,@TaxExpenseInterestIncomeBond,@TaxExpenseInterestIncomeTimeDeposit,@RealisedEquity,@RealisedBond,
                                      @UnrealisedBond,@UnrealisedEquity,@DistributedIncomeAcc,@DistributedIncomePayableAcc,@PendingSubscription,@PendingRedemption,@TaxCapitalGainBond,@TaxPercentageCapitalGain,@PayableMovementFee,@MovementFeeExpense,@PayablePurchaseMutualFund,
                                      @InvestmentMutualFund,@AccountReceivableSaleMutualFund,@RevaluationMutualFund,@UnrealisedMutualFund,@RealisedMutualFund,@PayableOtherFeeOne,@PayableOtherFeeTwo,@OtherFeeOneExpense,@OtherFeeTwoExpense,@BondAmortization,@PendingSwitching,@CurrentYearAccount,@PriorYearAccount,";
        //2
        private TemplateFundAccountingSetup setTemplateFundAccountingSetup(SqlDataReader dr)
        {
            TemplateFundAccountingSetup M_TemplateFundAccountingSetup = new TemplateFundAccountingSetup();
            M_TemplateFundAccountingSetup.TemplateFundAccountingSetupPK = Convert.ToInt32(dr["TemplateFundAccountingSetupPK"]);
            M_TemplateFundAccountingSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TemplateFundAccountingSetup.Status = Convert.ToInt32(dr["Status"]);
            M_TemplateFundAccountingSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TemplateFundAccountingSetup.Notes = Convert.ToString(dr["Notes"]);

            M_TemplateFundAccountingSetup.Subscription = Convert.ToInt32(dr["Subscription"]);
            M_TemplateFundAccountingSetup.SubscriptionID = Convert.ToString(dr["SubscriptionID"]);
            M_TemplateFundAccountingSetup.SubscriptionDesc = Convert.ToString(dr["SubscriptionDesc"]);
            M_TemplateFundAccountingSetup.PayableSubscriptionFee = Convert.ToInt32(dr["PayableSubscriptionFee"]);
            M_TemplateFundAccountingSetup.PayableSubscriptionFeeID = Convert.ToString(dr["PayableSubscriptionFeeID"]);
            M_TemplateFundAccountingSetup.PayableSubscriptionFeeDesc = Convert.ToString(dr["PayableSubscriptionFeeDesc"]);
            M_TemplateFundAccountingSetup.Redemption = Convert.ToInt32(dr["Redemption"]);
            M_TemplateFundAccountingSetup.RedemptionID = Convert.ToString(dr["RedemptionID"]);
            M_TemplateFundAccountingSetup.RedemptionDesc = Convert.ToString(dr["RedemptionDesc"]);

            M_TemplateFundAccountingSetup.PayableRedemptionFee = Convert.ToInt32(dr["PayableRedemptionFee"]);
            M_TemplateFundAccountingSetup.PayableRedemptionFeeID = Convert.ToString(dr["PayableRedemptionFeeID"]);
            M_TemplateFundAccountingSetup.PayableRedemptionFeeDesc = Convert.ToString(dr["PayableRedemptionFeeDesc"]);
            //M_TemplateFundAccountingSetup.Switching = Convert.ToInt32(dr["Switching"]);
            //M_TemplateFundAccountingSetup.SwitchingID = Convert.ToString(dr["SwitchingID"]);
            //M_TemplateFundAccountingSetup.SwitchingDesc = Convert.ToString(dr["SwitchingDesc"]);
            M_TemplateFundAccountingSetup.PayableSwitchingFee = Convert.ToInt32(dr["PayableSwitchingFee"]);
            M_TemplateFundAccountingSetup.PayableSwitchingFeeID = Convert.ToString(dr["PayableSwitchingFeeID"]);
            M_TemplateFundAccountingSetup.PayableSwitchingFeeDesc = Convert.ToString(dr["PayableSwitchingFeeDesc"]);
            M_TemplateFundAccountingSetup.InvestmentEquity = Convert.ToInt32(dr["InvestmentEquity"]);
            M_TemplateFundAccountingSetup.InvestmentEquityID = Convert.ToString(dr["InvestmentEquityID"]);
            M_TemplateFundAccountingSetup.InvestmentEquityDesc = Convert.ToString(dr["InvestmentEquityDesc"]);
            M_TemplateFundAccountingSetup.InvestmentBond = Convert.ToInt32(dr["InvestmentBond"]);
            M_TemplateFundAccountingSetup.InvestmentBondID = Convert.ToString(dr["InvestmentBondID"]);
            M_TemplateFundAccountingSetup.InvestmentBondDesc = Convert.ToString(dr["InvestmentBondDesc"]);
            M_TemplateFundAccountingSetup.InvestmentTimeDeposit = Convert.ToInt32(dr["InvestmentTimeDeposit"]);
            M_TemplateFundAccountingSetup.InvestmentTimeDepositID = Convert.ToString(dr["InvestmentTimeDepositID"]);
            M_TemplateFundAccountingSetup.InvestmentTimeDepositDesc = Convert.ToString(dr["InvestmentTimeDepositDesc"]);
            M_TemplateFundAccountingSetup.InterestRecBond = Convert.ToInt32(dr["InterestRecBond"]);
            M_TemplateFundAccountingSetup.InterestRecBondID = Convert.ToString(dr["InterestRecBondID"]);
            M_TemplateFundAccountingSetup.InterestRecBondDesc = Convert.ToString(dr["InterestRecBondDesc"]);
            M_TemplateFundAccountingSetup.InterestAccrBond = Convert.ToInt32(dr["InterestAccrBond"]);
            M_TemplateFundAccountingSetup.InterestAccrBondID = Convert.ToString(dr["InterestAccrBondID"]);
            M_TemplateFundAccountingSetup.InterestAccrBondDesc = Convert.ToString(dr["InterestAccrBondDesc"]);

            M_TemplateFundAccountingSetup.InterestAccrTimeDeposit = Convert.ToInt32(dr["InterestAccrTimeDeposit"]);
            M_TemplateFundAccountingSetup.InterestAccrTimeDepositID = Convert.ToString(dr["InterestAccrTimeDepositID"]);
            M_TemplateFundAccountingSetup.InterestAccrTimeDepositDesc = Convert.ToString(dr["InterestAccrTimeDepositDesc"]);
            M_TemplateFundAccountingSetup.InterestAccrGiro = Convert.ToInt32(dr["InterestAccrGiro"]);
            M_TemplateFundAccountingSetup.InterestAccrGiroID = Convert.ToString(dr["InterestAccrGiroID"]);
            M_TemplateFundAccountingSetup.InterestAccrGiroDesc = Convert.ToString(dr["InterestAccrGiroDesc"]);
            M_TemplateFundAccountingSetup.PrepaidTaxDividend = Convert.ToInt32(dr["PrepaidTaxDividend"]);
            M_TemplateFundAccountingSetup.PrepaidTaxDividendID = Convert.ToString(dr["PrepaidTaxDividendID"]);
            M_TemplateFundAccountingSetup.PrepaidTaxDividendDesc = Convert.ToString(dr["PrepaidTaxDividendDesc"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleBond = Convert.ToInt32(dr["AccountReceivableSaleBond"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleBondID = Convert.ToString(dr["AccountReceivableSaleBondID"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleBondDesc = Convert.ToString(dr["AccountReceivableSaleBondDesc"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleEquity = Convert.ToInt32(dr["AccountReceivableSaleEquity"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleEquityID = Convert.ToString(dr["AccountReceivableSaleEquityID"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleEquityDesc = Convert.ToString(dr["AccountReceivableSaleEquityDesc"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleTimeDeposit = Convert.ToInt32(dr["AccountReceivableSaleTimeDeposit"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleTimeDepositID = Convert.ToString(dr["AccountReceivableSaleTimeDepositID"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleTimeDepositDesc = Convert.ToString(dr["AccountReceivableSaleTimeDepositDesc"]);

            M_TemplateFundAccountingSetup.IncomeInterestBond = Convert.ToInt32(dr["IncomeInterestBond"]);
            M_TemplateFundAccountingSetup.IncomeInterestBondID = Convert.ToString(dr["IncomeInterestBondID"]);
            M_TemplateFundAccountingSetup.IncomeInterestBondDesc = Convert.ToString(dr["IncomeInterestBondDesc"]);
            M_TemplateFundAccountingSetup.IncomeInterestTimeDeposit = Convert.ToInt32(dr["IncomeInterestTimeDeposit"]);
            M_TemplateFundAccountingSetup.IncomeInterestTimeDepositID = Convert.ToString(dr["IncomeInterestTimeDepositID"]);
            M_TemplateFundAccountingSetup.IncomeInterestTimeDepositDesc = Convert.ToString(dr["IncomeInterestTimeDepositDesc"]);
            M_TemplateFundAccountingSetup.IncomeInterestGiro = Convert.ToInt32(dr["IncomeInterestGiro"]);
            M_TemplateFundAccountingSetup.IncomeInterestGiroID = Convert.ToString(dr["IncomeInterestGiroID"]);
            M_TemplateFundAccountingSetup.IncomeInterestGiroDesc = Convert.ToString(dr["IncomeInterestGiroDesc"]);
            M_TemplateFundAccountingSetup.IncomeDividend = Convert.ToInt32(dr["IncomeDividend"]);
            M_TemplateFundAccountingSetup.IncomeDividendID = Convert.ToString(dr["IncomeDividendID"]);
            M_TemplateFundAccountingSetup.IncomeDividendDesc = Convert.ToString(dr["IncomeDividendDesc"]);
            M_TemplateFundAccountingSetup.ARDividend = Convert.ToInt32(dr["ARDividend"]);
            M_TemplateFundAccountingSetup.ARDividendID = Convert.ToString(dr["ARDividendID"]);
            M_TemplateFundAccountingSetup.ARDividendDesc = Convert.ToString(dr["ARDividendDesc"]);
            M_TemplateFundAccountingSetup.RevaluationBond = Convert.ToInt32(dr["RevaluationBond"]);
            M_TemplateFundAccountingSetup.RevaluationBondID = Convert.ToString(dr["RevaluationBondID"]);
            M_TemplateFundAccountingSetup.RevaluationBondDesc = Convert.ToString(dr["RevaluationBondDesc"]);
            M_TemplateFundAccountingSetup.RevaluationEquity = Convert.ToInt32(dr["RevaluationEquity"]);
            M_TemplateFundAccountingSetup.RevaluationEquityID = Convert.ToString(dr["RevaluationEquityID"]);
            M_TemplateFundAccountingSetup.RevaluationEquityDesc = Convert.ToString(dr["RevaluationEquityDesc"]);
            M_TemplateFundAccountingSetup.PayablePurchaseEquity = Convert.ToInt32(dr["PayablePurchaseEquity"]);
            M_TemplateFundAccountingSetup.PayablePurchaseEquityID = Convert.ToString(dr["PayablePurchaseEquityID"]);
            M_TemplateFundAccountingSetup.PayablePurchaseEquityDesc = Convert.ToString(dr["PayablePurchaseEquityDesc"]);

            M_TemplateFundAccountingSetup.PayablePurRecBond = Convert.ToInt32(dr["PayablePurRecBond"]);
            M_TemplateFundAccountingSetup.PayablePurRecBondID = Convert.ToString(dr["PayablePurRecBondID"]);
            M_TemplateFundAccountingSetup.PayablePurRecBondDesc = Convert.ToString(dr["PayablePurRecBondDesc"]);
            M_TemplateFundAccountingSetup.PayableManagementFee = Convert.ToInt32(dr["PayableManagementFee"]);
            M_TemplateFundAccountingSetup.PayableManagementFeeID = Convert.ToString(dr["PayableSubscriptionFeeID"]);
            M_TemplateFundAccountingSetup.PayableManagementFeeDesc = Convert.ToString(dr["PayableManagementFeeDesc"]);
            M_TemplateFundAccountingSetup.PayableCustodianFee = Convert.ToInt32(dr["PayableCustodianFee"]);
            M_TemplateFundAccountingSetup.PayableCustodianFeeID = Convert.ToString(dr["PayableCustodianFeeID"]);
            M_TemplateFundAccountingSetup.PayableCustodianFeeDesc = Convert.ToString(dr["PayableCustodianFeeDesc"]);
            M_TemplateFundAccountingSetup.PayableAuditFee = Convert.ToInt32(dr["PayableAuditFee"]);
            M_TemplateFundAccountingSetup.PayableAuditFeeID = Convert.ToString(dr["PayableAuditFeeID"]);
            M_TemplateFundAccountingSetup.PayableAuditFeeDesc = Convert.ToString(dr["PayableAuditFeeDesc"]);
            M_TemplateFundAccountingSetup.PayableMovementFee = Convert.ToInt32(dr["PayableMovementFee"]);
            M_TemplateFundAccountingSetup.PayableMovementFeeID = dr["PayableMovementFeeID"].ToString();
            M_TemplateFundAccountingSetup.PayableMovementFeeDesc = dr["PayableMovementFeeDesc"].ToString();
            M_TemplateFundAccountingSetup.BrokerCommission = Convert.ToInt32(dr["BrokerCommission"]);
            M_TemplateFundAccountingSetup.BrokerCommissionID = Convert.ToString(dr["BrokerCommissionID"]);
            M_TemplateFundAccountingSetup.BrokerCommissionDesc = Convert.ToString(dr["BrokerCommissionDesc"]);
            M_TemplateFundAccountingSetup.BrokerLevy = Convert.ToInt32(dr["BrokerLevy"]);
            M_TemplateFundAccountingSetup.BrokerLevyID = Convert.ToString(dr["BrokerLevyID"]);
            M_TemplateFundAccountingSetup.BrokerLevyDesc = Convert.ToString(dr["BrokerLevyDesc"]);
            M_TemplateFundAccountingSetup.BrokerVat = Convert.ToInt32(dr["BrokerVat"]);
            M_TemplateFundAccountingSetup.BrokerVatID = Convert.ToString(dr["BrokerVatID"]);
            M_TemplateFundAccountingSetup.BrokerVatDesc = Convert.ToString(dr["BrokerVatDesc"]);

            M_TemplateFundAccountingSetup.BrokerSalesTax = Convert.ToInt32(dr["BrokerSalesTax"]);
            M_TemplateFundAccountingSetup.BrokerSalesTaxID = Convert.ToString(dr["BrokerSalesTaxID"]);
            M_TemplateFundAccountingSetup.BrokerSalesTaxDesc = Convert.ToString(dr["BrokerSalesTaxDesc"]);
            M_TemplateFundAccountingSetup.WithHoldingTaxPPH23 = Convert.ToInt32(dr["WithHoldingTaxPPH23"]);
            M_TemplateFundAccountingSetup.WithHoldingTaxPPH23ID = Convert.ToString(dr["WithHoldingTaxPPH23ID"]);
            M_TemplateFundAccountingSetup.WithHoldingTaxPPH23Desc = Convert.ToString(dr["WithHoldingTaxPPH23Desc"]);
            M_TemplateFundAccountingSetup.WHTTaxPayableAccrInterestBond = Convert.ToInt32(dr["WHTTaxPayableAccrInterestBond"]);
            M_TemplateFundAccountingSetup.WHTTaxPayableAccrInterestBondID = Convert.ToString(dr["WHTTaxPayableAccrInterestBondID"]);
            M_TemplateFundAccountingSetup.WHTTaxPayableAccrInterestBondDesc = Convert.ToString(dr["WHTTaxPayableAccrInterestBondDesc"]);
            M_TemplateFundAccountingSetup.ManagementFeeExpense = Convert.ToInt32(dr["ManagementFeeExpense"]);
            M_TemplateFundAccountingSetup.ManagementFeeExpenseID = Convert.ToString(dr["ManagementFeeExpenseID"]);
            M_TemplateFundAccountingSetup.ManagementFeeExpenseDesc = Convert.ToString(dr["ManagementFeeExpenseDesc"]);
            M_TemplateFundAccountingSetup.CustodianFeeExpense = Convert.ToInt32(dr["CustodianFeeExpense"]);
            M_TemplateFundAccountingSetup.CustodianFeeExpenseID = Convert.ToString(dr["CustodianFeeExpenseID"]);
            M_TemplateFundAccountingSetup.CustodianFeeExpenseDesc = Convert.ToString(dr["CustodianFeeExpenseDesc"]);
            M_TemplateFundAccountingSetup.AuditFeeExpense = Convert.ToInt32(dr["AuditFeeExpense"]);
            M_TemplateFundAccountingSetup.AuditFeeExpenseID = Convert.ToString(dr["AuditFeeExpenseID"]);
            M_TemplateFundAccountingSetup.AuditFeeExpenseDesc = Convert.ToString(dr["AuditFeeExpenseDesc"]);
            M_TemplateFundAccountingSetup.MovementFeeExpense = Convert.ToInt32(dr["MovementFeeExpense"]);
            M_TemplateFundAccountingSetup.MovementFeeExpenseID = dr["MovementFeeExpenseID"].ToString();
            M_TemplateFundAccountingSetup.MovementFeeExpenseDesc = dr["MovementFeeExpenseDesc"].ToString();
            M_TemplateFundAccountingSetup.BankCharges = Convert.ToInt32(dr["BankCharges"]);
            M_TemplateFundAccountingSetup.BankChargesID = Convert.ToString(dr["BankChargesID"]);
            M_TemplateFundAccountingSetup.BankChargesDesc = Convert.ToString(dr["BankChargesDesc"]);
            M_TemplateFundAccountingSetup.TaxExpenseInterestIncomeBond = Convert.ToInt32(dr["TaxExpenseInterestIncomeBond"]);
            M_TemplateFundAccountingSetup.TaxExpenseInterestIncomeBondID = Convert.ToString(dr["TaxExpenseInterestIncomeBondID"]);
            M_TemplateFundAccountingSetup.TaxExpenseInterestIncomeBondDesc = Convert.ToString(dr["TaxExpenseInterestIncomeBondDesc"]);

            M_TemplateFundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit = Convert.ToInt32(dr["TaxExpenseInterestIncomeTimeDeposit"]);
            M_TemplateFundAccountingSetup.TaxExpenseInterestIncomeTimeDepositID = Convert.ToString(dr["TaxExpenseInterestIncomeTimeDepositID"]);
            M_TemplateFundAccountingSetup.TaxExpenseInterestIncomeTimeDepositDesc = Convert.ToString(dr["TaxExpenseInterestIncomeTimeDepositDesc"]);
            M_TemplateFundAccountingSetup.RealisedEquity = Convert.ToInt32(dr["RealisedEquity"]);
            M_TemplateFundAccountingSetup.RealisedEquityID = Convert.ToString(dr["RealisedEquityID"]);
            M_TemplateFundAccountingSetup.RealisedEquityDesc = Convert.ToString(dr["RealisedEquityDesc"]);
            M_TemplateFundAccountingSetup.RealisedBond = Convert.ToInt32(dr["RealisedBond"]);
            M_TemplateFundAccountingSetup.RealisedBondID = Convert.ToString(dr["RealisedBondID"]);
            M_TemplateFundAccountingSetup.RealisedBondDesc = Convert.ToString(dr["RealisedBondDesc"]);
            M_TemplateFundAccountingSetup.UnrealisedBond = Convert.ToInt32(dr["UnrealisedBond"]);
            M_TemplateFundAccountingSetup.UnrealisedBondID = Convert.ToString(dr["UnrealisedBondID"]);
            M_TemplateFundAccountingSetup.UnrealisedBondDesc = Convert.ToString(dr["UnrealisedBondDesc"]);
            M_TemplateFundAccountingSetup.UnrealisedEquity = Convert.ToInt32(dr["UnrealisedEquity"]);
            M_TemplateFundAccountingSetup.UnrealisedEquityID = Convert.ToString(dr["UnrealisedEquityID"]);
            M_TemplateFundAccountingSetup.UnrealisedEquityDesc = Convert.ToString(dr["UnrealisedEquityDesc"]);
            M_TemplateFundAccountingSetup.DistributedIncomeAcc = Convert.ToInt32(dr["DistributedIncomeAcc"]);
            M_TemplateFundAccountingSetup.DistributedIncomeAccID = Convert.ToString(dr["DistributedIncomeAccID"]);
            M_TemplateFundAccountingSetup.DistributedIncomeAccDesc = Convert.ToString(dr["DistributedIncomeAccDesc"]);
            M_TemplateFundAccountingSetup.DistributedIncomePayableAcc = Convert.ToInt32(dr["DistributedIncomePayableAcc"]);
            M_TemplateFundAccountingSetup.DistributedIncomePayableAccID = Convert.ToString(dr["DistributedIncomePayableAccID"]);
            M_TemplateFundAccountingSetup.DistributedIncomePayableAccDesc = Convert.ToString(dr["DistributedIncomePayableAccDesc"]);

            M_TemplateFundAccountingSetup.TaxCapitalGainBond = Convert.ToInt32(dr["TaxCapitalGainBond"]);
            M_TemplateFundAccountingSetup.TaxCapitalGainBondID = Convert.ToString(dr["TaxCapitalGainBondID"]);
            M_TemplateFundAccountingSetup.TaxCapitalGainBondDesc = Convert.ToString(dr["TaxCapitalGainBondDesc"]);

            M_TemplateFundAccountingSetup.PendingSubscription = Convert.ToInt32(dr["PendingSubscription"]);
            M_TemplateFundAccountingSetup.PendingSubscriptionID = Convert.ToString(dr["PendingSubscriptionID"]);
            M_TemplateFundAccountingSetup.PendingSubscriptionDesc = Convert.ToString(dr["PendingSubscriptionDesc"]);
            M_TemplateFundAccountingSetup.PendingRedemption = Convert.ToInt32(dr["PendingRedemption"]);
            M_TemplateFundAccountingSetup.PendingRedemptionID = Convert.ToString(dr["PendingRedemptionID"]);
            M_TemplateFundAccountingSetup.PendingRedemptionDesc = Convert.ToString(dr["PendingRedemptionDesc"]);
            M_TemplateFundAccountingSetup.TaxPercentageDividend = Convert.ToDecimal(dr["TaxPercentageDividend"]);
            M_TemplateFundAccountingSetup.TaxPercentageBond = Convert.ToDecimal(dr["TaxPercentageBond"]);
            M_TemplateFundAccountingSetup.TaxPercentageTD = Convert.ToDecimal(dr["TaxPercentageTD"]);
            M_TemplateFundAccountingSetup.TaxPercentageCapitalGain = Convert.ToDecimal(dr["TaxPercentageCapitalGain"]);

            M_TemplateFundAccountingSetup.PayableSInvestFee = Convert.ToInt32(dr["PayableSInvestFee"]);
            M_TemplateFundAccountingSetup.PayableSInvestFeeID = dr["PayableSInvestFeeID"].ToString();
            M_TemplateFundAccountingSetup.PayableSInvestFeeDesc = dr["PayableSInvestFeeDesc"].ToString();

            M_TemplateFundAccountingSetup.SInvestFee = Convert.ToInt32(dr["SInvestFee"]);
            M_TemplateFundAccountingSetup.SInvestFeeID = dr["SInvestFeeID"].ToString();
            M_TemplateFundAccountingSetup.SInvestFeeDesc = dr["SInvestFeeDesc"].ToString();

            M_TemplateFundAccountingSetup.BondAmortization = Convert.ToInt32(dr["BondAmortization"]);
            M_TemplateFundAccountingSetup.BondAmortizationID = dr["BondAmortizationID"].ToString();
            M_TemplateFundAccountingSetup.BondAmortizationDesc = dr["BondAmortizationDesc"].ToString();

            M_TemplateFundAccountingSetup.PayablePurchaseMutualFund = Convert.ToInt32(dr["PayablePurchaseMutualFund"]);
            M_TemplateFundAccountingSetup.PayablePurchaseMutualFundID = dr["PayablePurchaseMutualFundID"].ToString();
            M_TemplateFundAccountingSetup.PayablePurchaseMutualFundDesc = dr["PayablePurchaseMutualFundDesc"].ToString();

            M_TemplateFundAccountingSetup.InvestmentMutualFund = Convert.ToInt32(dr["InvestmentMutualFund"]);
            M_TemplateFundAccountingSetup.InvestmentMutualFundID = dr["InvestmentMutualFundID"].ToString();
            M_TemplateFundAccountingSetup.InvestmentMutualFundDesc = dr["InvestmentMutualFundDesc"].ToString();

            M_TemplateFundAccountingSetup.AccountReceivableSaleMutualFund = Convert.ToInt32(dr["AccountReceivableSaleMutualFund"]);
            M_TemplateFundAccountingSetup.AccountReceivableSaleMutualFundID = dr["AccountReceivableSaleMutualFundID"].ToString();
            M_TemplateFundAccountingSetup.AccountReceivableSaleMutualFundDesc = dr["AccountReceivableSaleMutualFundDesc"].ToString();

            M_TemplateFundAccountingSetup.RevaluationMutualFund = Convert.ToInt32(dr["RevaluationMutualFund"]);
            M_TemplateFundAccountingSetup.RevaluationMutualFundID = dr["RevaluationMutualFundID"].ToString();
            M_TemplateFundAccountingSetup.RevaluationMutualFundDesc = dr["RevaluationMutualFundDesc"].ToString();

            M_TemplateFundAccountingSetup.UnrealisedMutualFund = Convert.ToInt32(dr["UnrealisedMutualFund"]);
            M_TemplateFundAccountingSetup.UnrealisedMutualFundID = dr["UnrealisedMutualFundID"].ToString();
            M_TemplateFundAccountingSetup.UnrealisedMutualFundDesc = dr["UnrealisedMutualFundDesc"].ToString();

            M_TemplateFundAccountingSetup.RealisedMutualFund = Convert.ToInt32(dr["RealisedMutualFund"]);
            M_TemplateFundAccountingSetup.RealisedMutualFundID = dr["RealisedMutualFundID"].ToString();
            M_TemplateFundAccountingSetup.RealisedMutualFundDesc = dr["RealisedMutualFundDesc"].ToString();

            M_TemplateFundAccountingSetup.PayableOtherFeeOne = Convert.ToInt32(dr["PayableOtherFeeOne"]);
            M_TemplateFundAccountingSetup.PayableOtherFeeOneID = dr["PayableOtherFeeOneID"].ToString();
            M_TemplateFundAccountingSetup.PayableOtherFeeOneDesc = dr["PayableOtherFeeOneDesc"].ToString();

            M_TemplateFundAccountingSetup.PayableOtherFeeTwo = Convert.ToInt32(dr["PayableOtherFeeTwo"]);
            M_TemplateFundAccountingSetup.PayableOtherFeeTwoID = dr["PayableOtherFeeTwoID"].ToString();
            M_TemplateFundAccountingSetup.PayableOtherFeeTwoDesc = dr["PayableOtherFeeTwoDesc"].ToString();

            M_TemplateFundAccountingSetup.OtherFeeOneExpense = Convert.ToInt32(dr["OtherFeeOneExpense"]);
            M_TemplateFundAccountingSetup.OtherFeeOneExpenseID = dr["OtherFeeOneExpenseID"].ToString();
            M_TemplateFundAccountingSetup.OtherFeeOneExpenseDesc = dr["OtherFeeOneExpenseDesc"].ToString();

            M_TemplateFundAccountingSetup.OtherFeeTwoExpense = Convert.ToInt32(dr["OtherFeeTwoExpense"]);
            M_TemplateFundAccountingSetup.OtherFeeTwoExpenseID = dr["OtherFeeTwoExpenseID"].ToString();
            M_TemplateFundAccountingSetup.OtherFeeTwoExpenseDesc = dr["OtherFeeTwoExpenseDesc"].ToString();

            M_TemplateFundAccountingSetup.PendingSwitching = Convert.ToInt32(dr["PendingSwitching"]);
            M_TemplateFundAccountingSetup.PendingSwitchingID = dr["PendingSwitchingID"].ToString();
            M_TemplateFundAccountingSetup.PendingSwitchingDesc = dr["PendingSwitchingDesc"].ToString();

            M_TemplateFundAccountingSetup.CurrentYearAccount = Convert.ToInt32(dr["CurrentYearAccount"]);
            M_TemplateFundAccountingSetup.CurrentYearAccountID = dr["CurrentYearAccountID"].ToString();
            M_TemplateFundAccountingSetup.CurrentYearAccountDesc = dr["CurrentYearAccountDesc"].ToString();

            M_TemplateFundAccountingSetup.PriorYearAccount = Convert.ToInt32(dr["PriorYearAccount"]);
            M_TemplateFundAccountingSetup.PriorYearAccountID = dr["PriorYearAccountID"].ToString();
            M_TemplateFundAccountingSetup.PriorYearAccountDesc = dr["PriorYearAccountDesc"].ToString();

            M_TemplateFundAccountingSetup.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_TemplateFundAccountingSetup.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_TemplateFundAccountingSetup.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_TemplateFundAccountingSetup.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_TemplateFundAccountingSetup.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_TemplateFundAccountingSetup.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_TemplateFundAccountingSetup.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_TemplateFundAccountingSetup.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_TemplateFundAccountingSetup.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_TemplateFundAccountingSetup.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_TemplateFundAccountingSetup.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_TemplateFundAccountingSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_TemplateFundAccountingSetup;
        }

        public List<TemplateFundAccountingSetup> TemplateFundAccountingSetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TemplateFundAccountingSetup> L_TemplateFundAccountingSetup = new List<TemplateFundAccountingSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select  case when F.status=1 then 'PENDING' else Case When F.status = 2 then 'APPROVED' else Case when F.Status = 3 then 
                               'VOID' else 'WAITING' END END END StatusDesc,
                               A1.FundJournalAccountPK Subscription,A1.ID SubscriptionID, A1.Name SubscriptionDesc,
                               A2.FundJournalAccountPK PayableSubscriptionFee,A2.ID PayableSubscriptionFeeID,A2.Name PayableSubscriptionFeeDesc,
                               A3.FundJournalAccountPK Redemption,A3.ID RedemptionID,A3.Name RedemptionDesc,
                               A6.FundJournalAccountPK PayableRedemptionFee,A6.ID PayableRedemptionFeeID,A6.Name PayableRedemptionFeeDesc,
                               isnull(A4.FundJournalAccountPK,0) Switching,isnull(A4.ID,'') SwitchingID,isnull(A4.Name,'') SwitchingDesc,
                               isnull(A5.FundJournalAccountPK,0) PayableSwitchingFee,isnull(A5.ID,'') PayableSwitchingFeeID,isnull(A5.Name,'') PayableSwitchingFeeDesc,
                               A7.FundJournalAccountPK InvestmentEquity ,A7.ID InvestmentEquityID, A7.Name InvestmentEquityDesc,
                               A8.FundJournalAccountPK InvestmentBond ,A8.ID InvestmentBondID,A8.Name InvestmentBondDesc,
                               A9.FundJournalAccountPK InvestmentTimeDeposit,A9.ID InvestmentTimeDepositID,A9.Name InvestmentTimeDepositDesc,
                               A10.FundJournalAccountPK InterestBond,A10.ID InterestRecBondID,A10.Name InterestRecBondDesc,
                               A11.FundJournalAccountPK InterestAccrBond,A11.ID InterestAccrBondID,A11.Name InterestAccrBondDesc,
                               A12.FundJournalAccountPK InterestAccrTimeDeposit,A12.ID InterestAccrTimeDepositID,A12.Name InterestAccrTimeDepositDesc,
                               A13.FundJournalAccountPK InterestAccrGiro,A13.ID InterestAccrGiroID,A13.Name InterestAccrGiroDesc,
                               A14.FundJournalAccountPK PrepaidTaxDividend,A14.ID PrepaidTaxDividendID,A14.Name PrepaidTaxDividendDesc,
                               A15.FundJournalAccountPK AccountReceivableSaleBond,A15.ID AccountReceivableSaleBondID,A15.Name AccountReceivableSaleBondDesc,
                               A16.FundJournalAccountPK AccountReceivableSaleEquity,A16.ID AccountReceivableSaleEquityID,A16.Name AccountReceivableSaleEquityDesc,
                               A17.FundJournalAccountPK AccountReceivableSaleTimeDeposit,A17.ID AccountReceivableSaleTimeDepositID,A17.Name AccountReceivableSaleTimeDepositDesc,
                               A18.FundJournalAccountPK IncomeInterestBond,A18.ID IncomeInterestBondID,A18.Name IncomeInterestBondDesc,
                               A19.FundJournalAccountPK IncomeInterestTimeDeposit,A19.ID IncomeInterestTimeDepositID,A19.Name IncomeInterestTimeDepositDesc,
                               A20.FundJournalAccountPK IncomeInterestGiro,A20.ID IncomeInterestGiroID,A20.Name IncomeInterestGiroDesc,
                               A21.FundJournalAccountPK IncomeDividend,A21.ID IncomeDividendID,A21.Name IncomeDividendDesc,
                               A22.FundJournalAccountPK RevaluationBond,A22.ID RevaluationBondID,A22.Name RevaluationBondDesc,
                               A23.FundJournalAccountPK RevaluationEquity,A23.ID RevaluationEquityID,A23.Name RevaluationEquityDesc,
                               A24.FundJournalAccountPK PayablePurchaseEquity,A24.ID PayablePurchaseEquityID,A24.Name PayablePurchaseEquityDesc,
                               A25.FundJournalAccountPK PayablePurRecBond,A25.ID PayablePurRecBondID,A3.Name PayablePurRecBondDesc,
                               A26.FundJournalAccountPK PayableManagementFee,A26.ID PayableManagementFeeID,A26.Name PayableManagementFeeDesc,
                               A27.FundJournalAccountPK PayableCustodianFee,A27.ID PayableCustodianFeeID,A27.Name PayableCustodianFeeDesc,
                               A28.FundJournalAccountPK PayableAuditFee,A28.ID PayableAuditFeeID,A28.Name PayableAuditFeeDesc,
                               A29.FundJournalAccountPK BrokerCommission,A29.ID BrokerCommissionID,A29.Name BrokerCommissionDesc,
                               A30.FundJournalAccountPK BrokerLevy,A30.ID BrokerLevyID,A30.Name BrokerLevyDesc,
                               A31.FundJournalAccountPK BrokerVat,A31.ID BrokerVatID,A31.Name BrokerVatDesc,
                               A32.FundJournalAccountPK BrokerSalesTax,A32.ID BrokerSalesTaxID,A32.Name BrokerSalesTaxDesc,
                               A33.FundJournalAccountPK WithHoldingTaxPPH23,A33.ID WithHoldingTaxPPH23ID,A3.Name WithHoldingTaxPPH23Desc,
                               A34.FundJournalAccountPK WHTTaxPayableAccrInterestBond,A34.ID WHTTaxPayableAccrInterestBondID,A34.Name WHTTaxPayableAccrInterestBondDesc,
                               A35.FundJournalAccountPK ManagementFeeExpense,A35.ID ManagementFeeExpenseID,A35.Name ManagementFeeExpenseDesc,
                               A36.FundJournalAccountPK CustodianFeeExpense,A36.ID CustodianFeeExpenseID,A36.Name CustodianFeeExpenseDesc,
                               A37.FundJournalAccountPK AuditFeeExpense,A37.ID AuditFeeExpenseID,A37.Name AuditFeeExpenseDesc,
                               A38.FundJournalAccountPK BankCharges,A38.ID BankChargesID,A38.Name BankChargesDesc,
                               A39.FundJournalAccountPK TaxExpenseInterestIncomeBond,A39.ID TaxExpenseInterestIncomeBondID,A39.Name TaxExpenseInterestIncomeBondDesc,
                               A40.FundJournalAccountPK TaxExpenseInterestIncomeTimeDeposit,A40.ID TaxExpenseInterestIncomeTimeDepositID,A40.Name TaxExpenseInterestIncomeTimeDepositDesc,
                               A41.FundJournalAccountPK RealisedEquity,A41.ID RealisedEquityID,A41.Name RealisedEquityDesc,
                               A42.FundJournalAccountPK UnrealisedBond,A42.ID UnrealisedBondID,A42.Name UnrealisedBondDesc,
                               A43.FundJournalAccountPK UnrealisedEquity,A43.ID UnrealisedEquityID,A43.Name UnrealisedEquityDesc,
                               A44.FundJournalAccountPK DistributedIncomeAcc,A44.ID DistributedIncomeAccID,A44.Name DistributedIncomeAccDesc,
                               A45.FundJournalAccountPK DistributedIncomePayableAcc,A45.ID DistributedIncomePayableAccID,A45.Name DistributedIncomePayableAccDesc,
                               A46.FundJournalAccountPK PendingSubscription,A46.ID PendingSubscriptionID,A46.Name PendingSubscriptionDesc,
                               A47.FundJournalAccountPK PendingRedemption,A47.ID PendingRedemptionID,A47.Name PendingRedemptionDesc,
                               A48.FundJournalAccountPK ARDividend,A48.ID ARDividendID,A48.Name ARDividendDesc,
                               isnull(A49.FundJournalAccountPK,0) RealisedBond,isnull(A49.ID,'') RealisedBondID,isnull(A49.Name,'') RealisedBondDesc,
                               isnull(A50.FundJournalAccountPK,0) TaxCapitalGainBond,isnull(A50.ID,'') TaxCapitalGainBondID,isnull(A50.Name,'') TaxCapitalGainBondDesc,
                               isnull(A51.FundJournalAccountPK,0) PayableSInvestFee,isnull(A51.ID,'') PayableSInvestFeeID,isnull(A51.Name,'') PayableSInvestFeeDesc,
                               isnull(A52.FundJournalAccountPK,0) SInvestFee,isnull(A52.ID,'') SInvestFeeID,isnull(A52.Name,'') SInvestFeeDesc,
                               isnull(A53.FundJournalAccountPK,0) BondAmortization,isnull(A53.ID,'') BondAmortizationID,isnull(A53.Name,'') BondAmortizationDesc,
                               isnull(A54.FundJournalAccountPK,0) PayableMovementFee,isnull(A54.ID,'') PayableMovementFeeID,isnull(A54.Name,'') PayableMovementFeeDesc,
                               isnull(A55.FundJournalAccountPK,0) MovementFeeExpense,isnull(A55.ID,'') MovementFeeExpenseID,isnull(A55.Name,'') MovementFeeExpenseDesc,

                               isnull(A56.FundJournalAccountPK,0) PayablePurchaseMutualFund,isnull(A56.ID,'') PayablePurchaseMutualFundID,isnull(A56.Name,'') PayablePurchaseMutualFundDesc,
                               isnull(A57.FundJournalAccountPK,0) InvestmentMutualFund,isnull(A57.ID,'') InvestmentMutualFundID,isnull(A57.Name,'') InvestmentMutualFundDesc,
                               isnull(A58.FundJournalAccountPK,0) AccountReceivableSaleMutualFund,isnull(A58.ID,'') AccountReceivableSaleMutualFundID,isnull(A58.Name,'') AccountReceivableSaleMutualFundDesc,
                               isnull(A59.FundJournalAccountPK,0) RevaluationMutualFund,isnull(A59.ID,'') RevaluationMutualFundID,isnull(A59.Name,'') RevaluationMutualFundDesc,
                               isnull(A60.FundJournalAccountPK,0) UnrealisedMutualFund,isnull(A60.ID,'') UnrealisedMutualFundID,isnull(A60.Name,'') UnrealisedMutualFundDesc,
                               isnull(A61.FundJournalAccountPK,0) RealisedMutualFund,isnull(A61.ID,'') RealisedMutualFundID,isnull(A61.Name,'') RealisedMutualFundDesc,

                               isnull(A62.FundJournalAccountPK,0) PayableOtherFeeOne,isnull(A62.ID,'') PayableOtherFeeOneID,isnull(A62.Name,'') PayableOtherFeeOneDesc,
                               isnull(A63.FundJournalAccountPK,0) PayableOtherFeeTwo,isnull(A63.ID,'') PayableOtherFeeTwoID,isnull(A63.Name,'') PayableOtherFeeTwoDesc,
                               isnull(A64.FundJournalAccountPK,0) OtherFeeOneExpense,isnull(A64.ID,'') OtherFeeOneExpenseID,isnull(A64.Name,'') OtherFeeOneExpenseDesc,
                               isnull(A65.FundJournalAccountPK,0) OtherFeeTwoExpense,isnull(A65.ID,'') OtherFeeTwoExpenseID,isnull(A65.Name,'') OtherFeeTwoExpenseDesc,

                                isnull(A66.FundJournalAccountPK,0) PendingSwitching,isnull(A66.ID,'') PendingSwitchingID,isnull(A66.Name,'') PendingSwitchingDesc,
                                isnull(A67.FundJournalAccountPK,0) CurrentYearAccount,isnull(A67.ID,'') CurrentYearAccountID,isnull(A67.Name,'') CurrentYearAccountDesc,
                                isnull(A68.FundJournalAccountPK,0) PriorYearAccount,isnull(A68.ID,'') PriorYearAccountID,isnull(A68.Name,'') PriorYearAccountDesc,
                               F.* from TemplateFundAccountingSetup F left join 
                                   
                               FundJournalAccount A1 on F.Subscription = A1.FundJournalAccountPK and A1.status = 2  left join         
                               FundJournalAccount A2 on F.PayableSubscriptionFee = A2.FundJournalAccountPK and A2.status = 2  left join         
                               FundJournalAccount A3 on F.Redemption = A3.FundJournalAccountPK and A3.status = 2  left join               
                               FundJournalAccount A6 on F.PayableRedemptionFee = A6.FundJournalAccountPK and A6.status = 2   left join         
                               FundJournalAccount A4 on F.Switching = A4.FundJournalAccountPK and A4.status = 2  left join               
                               FundJournalAccount A5 on F.PayableSwitchingFee = A5.FundJournalAccountPK and A5.status = 2   left join       
                               FundJournalAccount A7 on F.InvestmentEquity = A7.FundJournalAccountPK and A7.status = 2  left join         
                               FundJournalAccount A8 on F.InvestmentBond = A8.FundJournalAccountPK and A8.status = 2  left join         
                               FundJournalAccount A9 on F.InvestmentTimeDeposit = A9.FundJournalAccountPK and A9.status = 2   left join     
                               FundJournalAccount A10 on F.InterestRecBond = A10.FundJournalAccountPK and A10.status = 2  left join         
                               FundJournalAccount A11 on F.InterestAccrBond = A11.FundJournalAccountPK and A11.status = 2  left join         
                               FundJournalAccount A12 on F.InterestAccrTimeDeposit = A12.FundJournalAccountPK and A12.status = 2   left join     
                               FundJournalAccount A13 on F.InterestAccrGiro = A13.FundJournalAccountPK and A13.status = 2  left join         
                               FundJournalAccount A14 on F.PrepaidTaxDividend = A14.FundJournalAccountPK and A14.status = 2  left join         
                               FundJournalAccount A15 on F.AccountReceivableSaleBond = A15.FundJournalAccountPK and A15.status = 2   left join     
                               FundJournalAccount A16 on F.AccountReceivableSaleEquity = A16.FundJournalAccountPK and A16.status = 2  left join         
                               FundJournalAccount A17 on F.AccountReceivableSaleTimeDeposit = A17.FundJournalAccountPK and A17.status = 2  left join         
                               FundJournalAccount A18 on F.IncomeInterestBond = A18.FundJournalAccountPK and A18.status = 2   left join     
                               FundJournalAccount A19 on F.IncomeInterestTimeDeposit = A19.FundJournalAccountPK and A19.status = 2  left join         
                               FundJournalAccount A20 on F.IncomeInterestGiro = A20.FundJournalAccountPK and A20.status = 2  left join         
                               FundJournalAccount A21 on F.IncomeDividend = A21.FundJournalAccountPK and A21.status = 2   left join     
                               FundJournalAccount A22 on F.RevaluationBond = A22.FundJournalAccountPK and A22.status = 2  left join         
                               FundJournalAccount A23 on F.RevaluationEquity = A23.FundJournalAccountPK and A23.status = 2  left join         
                               FundJournalAccount A24 on F.PayablePurchaseEquity = A24.FundJournalAccountPK and A24.status = 2    left join    
                               FundJournalAccount A25 on F.PayablePurRecBond = A25.FundJournalAccountPK and A25.status = 2  left join         
                               FundJournalAccount A26 on F.PayableManagementFee = A26.FundJournalAccountPK and A26.status = 2  left join         
                               FundJournalAccount A27 on F.PayableCustodianFee = A27.FundJournalAccountPK and A27.status = 2   left join     
                               FundJournalAccount A28 on F.PayableAuditFee = A28.FundJournalAccountPK and A28.status = 2  left join         
                               FundJournalAccount A29 on F.BrokerCommission = A29.FundJournalAccountPK and A29.status = 2  left join         
                               FundJournalAccount A30 on F.BrokerLevy = A30.FundJournalAccountPK and A30.status = 2    left join    
                               FundJournalAccount A31 on F.BrokerVat = A31.FundJournalAccountPK and A31.status = 2  left join         
                               FundJournalAccount A32 on F.BrokerSalesTax = A32.FundJournalAccountPK and A32.status = 2  left join         
                               FundJournalAccount A33 on F.WithHoldingTaxPPH23 = A33.FundJournalAccountPK and A33.status = 2    left join    
                               FundJournalAccount A34 on F.WHTTaxPayableAccrInterestBond = A34.FundJournalAccountPK and A34.status = 2  left join         
                               FundJournalAccount A35 on F.ManagementFeeExpense = A35.FundJournalAccountPK and A35.status = 2  left join         
                               FundJournalAccount A36 on F.CustodianFeeExpense = A36.FundJournalAccountPK and A36.status = 2   left join     
                               FundJournalAccount A37 on F.AuditFeeExpense = A37.FundJournalAccountPK and A37.status = 2  left join         
                               FundJournalAccount A38 on F.BankCharges = A38.FundJournalAccountPK and A38.status = 2  left join         
                               FundJournalAccount A39 on F.TaxExpenseInterestIncomeBond = A39.FundJournalAccountPK and A39.status = 2   left join     
                               FundJournalAccount A40 on F.TaxExpenseInterestIncomeTimeDeposit = A40.FundJournalAccountPK and A40.status = 2  left join         
                               FundJournalAccount A41 on F.RealisedEquity = A41.FundJournalAccountPK and A41.status = 2  left join         
                               FundJournalAccount A42 on F.UnrealisedBond = A42.FundJournalAccountPK and A42.status = 2   left join     
                               FundJournalAccount A43 on F.UnrealisedEquity = A43.FundJournalAccountPK and A43.status = 2  left join         
                               FundJournalAccount A44 on F.DistributedIncomeAcc = A44.FundJournalAccountPK and A44.status = 2  left join         
                               FundJournalAccount A45 on F.DistributedIncomePayableAcc = A45.FundJournalAccountPK and A45.status = 2  left join      
                               FundJournalAccount A46 on F.PendingSubscription = A46.FundJournalAccountPK and A46.status = 2  left join         
                               FundJournalAccount A47 on F.PendingRedemption = A47.FundJournalAccountPK and A47.status = 2   left join  
                               FundJournalAccount A48 on F.ARDividend = A48.FundJournalAccountPK and A48.status = 2  left join 
                               FundJournalAccount A49 on F.RealisedBond = A49.FundJournalAccountPK and A49.status = 2 left join
                               FundJournalAccount A50 on F.TaxCapitalGainBond = A50.FundJournalAccountPK and A50.status = 2 left join          
                               FundJournalAccount A51 on F.PayableSInvestFee = A51.FundJournalAccountPK and A51.status in (1,2) left join          
                               FundJournalAccount A52 on F.SInvestFee = A52.FundJournalAccountPK and A52.status in (1,2) left join
                               FundJournalAccount A53 on F.BondAmortization = A53.FundJournalAccountPK and A53.status in (1,2) left join  
                                FundJournalAccount A54 on F.PayableMovementFee = A54.FundJournalAccountPK and A54.status in (1,2) left join      
                                FundJournalAccount A55 on F.MovementFeeExpense = A55.FundJournalAccountPK and A55.status in (1,2) left join          
                                FundJournalAccount A56 on F.PayablePurchaseMutualFund = A56.FundJournalAccountPK and A56.status in (1,2) left join        
                                FundJournalAccount A57 on F.InvestmentMutualFund = A57.FundJournalAccountPK and A57.status in (1,2) left join        
                                FundJournalAccount A58 on F.AccountReceivableSaleMutualFund = A58.FundJournalAccountPK and A58.status in (1,2) left join        
                                FundJournalAccount A59 on F.RevaluationMutualFund = A59.FundJournalAccountPK and A59.status in (1,2) left join  
                                FundJournalAccount A60 on F.UnrealisedMutualFund = A60.FundJournalAccountPK and A60.status in (1,2) left join   
                                FundJournalAccount A61 on F.RealisedMutualFund = A61.FundJournalAccountPK and A61.status in (1,2)   left join

                                FundJournalAccount A62 on F.PayableOtherFeeOne = A62.FundJournalAccountPK and A62.status in (1,2)   left join 
                                FundJournalAccount A63 on F.PayableOtherFeeTwo = A63.FundJournalAccountPK and A63.status in (1,2)   left join 
                                FundJournalAccount A64 on F.OtherFeeOneExpense = A64.FundJournalAccountPK and A64.status in (1,2)   left join 
                                FundJournalAccount A65 on F.OtherFeeTwoExpense = A65.FundJournalAccountPK and A65.status in (1,2)   left join
                                FundJournalAccount A66 on F.PendingSwitching = A66.FundJournalAccountPK and A66.status in (1,2)   left join
                                FundJournalAccount A67 on F.CurrentYearAccount = A67.FundJournalAccountPK and A67.status in (1,2)   left join
                                FundJournalAccount A68 on F.PriorYearAccount = A68.FundJournalAccountPK and A68.status in (1,2)   
 
                               where F.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select  case when F.status=1 then 'PENDING' else Case When F.status = 2 then 'APPROVED' else Case when F.Status = 3 then 
                               'VOID' else 'WAITING' END END END StatusDesc,
                               A1.FundJournalAccountPK Subscription,A1.ID SubscriptionID, A1.Name SubscriptionDesc,
                               A2.FundJournalAccountPK PayableSubscriptionFee,A2.ID PayableSubscriptionFeeID,A2.Name PayableSubscriptionFeeDesc,
                               A3.FundJournalAccountPK Redemption,A3.ID RedemptionID,A3.Name RedemptionDesc,
                               A6.FundJournalAccountPK PayableRedemptionFee,A6.ID PayableRedemptionFeeID,A6.Name PayableRedemptionFeeDesc,
                               isnull(A4.FundJournalAccountPK,0) Switching,isnull(A4.ID,'') SwitchingID,isnull(A4.Name,'') SwitchingDesc,
                               isnull(A5.FundJournalAccountPK,0) PayableSwitchingFee,isnull(A5.ID,'') PayableSwitchingFeeID,isnull(A5.Name,'') PayableSwitchingFeeDesc,
                               A7.FundJournalAccountPK InvestmentEquity ,A7.ID InvestmentEquityID, A7.Name InvestmentEquityDesc,
                               A8.FundJournalAccountPK InvestmentBond ,A8.ID InvestmentBondID,A8.Name InvestmentBondDesc,
                               A9.FundJournalAccountPK InvestmentTimeDeposit,A9.ID InvestmentTimeDepositID,A9.Name InvestmentTimeDepositDesc,
                               A10.FundJournalAccountPK InterestBond,A10.ID InterestRecBondID,A10.Name InterestRecBondDesc,
                               A11.FundJournalAccountPK InterestAccrBond,A11.ID InterestAccrBondID,A11.Name InterestAccrBondDesc,
                               A12.FundJournalAccountPK InterestAccrTimeDeposit,A12.ID InterestAccrTimeDepositID,A12.Name InterestAccrTimeDepositDesc,
                               A13.FundJournalAccountPK InterestAccrGiro,A13.ID InterestAccrGiroID,A13.Name InterestAccrGiroDesc,
                               A14.FundJournalAccountPK PrepaidTaxDividend,A14.ID PrepaidTaxDividendID,A14.Name PrepaidTaxDividendDesc,
                               A15.FundJournalAccountPK AccountReceivableSaleBond,A15.ID AccountReceivableSaleBondID,A15.Name AccountReceivableSaleBondDesc,
                               A16.FundJournalAccountPK AccountReceivableSaleEquity,A16.ID AccountReceivableSaleEquityID,A16.Name AccountReceivableSaleEquityDesc,
                               A17.FundJournalAccountPK AccountReceivableSaleTimeDeposit,A17.ID AccountReceivableSaleTimeDepositID,A17.Name AccountReceivableSaleTimeDepositDesc,
                               A18.FundJournalAccountPK IncomeInterestBond,A18.ID IncomeInterestBondID,A18.Name IncomeInterestBondDesc,
                               A19.FundJournalAccountPK IncomeInterestTimeDeposit,A19.ID IncomeInterestTimeDepositID,A19.Name IncomeInterestTimeDepositDesc,
                               A20.FundJournalAccountPK IncomeInterestGiro,A20.ID IncomeInterestGiroID,A20.Name IncomeInterestGiroDesc,
                               A21.FundJournalAccountPK IncomeDividend,A21.ID IncomeDividendID,A21.Name IncomeDividendDesc,
                               A22.FundJournalAccountPK RevaluationBond,A22.ID RevaluationBondID,A22.Name RevaluationBondDesc,
                               A23.FundJournalAccountPK RevaluationEquity,A23.ID RevaluationEquityID,A23.Name RevaluationEquityDesc,
                               A24.FundJournalAccountPK PayablePurchaseEquity,A24.ID PayablePurchaseEquityID,A24.Name PayablePurchaseEquityDesc,
                               A25.FundJournalAccountPK PayablePurRecBond,A25.ID PayablePurRecBondID,A3.Name PayablePurRecBondDesc,
                               A26.FundJournalAccountPK PayableManagementFee,A26.ID PayableManagementFeeID,A26.Name PayableManagementFeeDesc,
                               A27.FundJournalAccountPK PayableCustodianFee,A27.ID PayableCustodianFeeID,A27.Name PayableCustodianFeeDesc,
                               A28.FundJournalAccountPK PayableAuditFee,A28.ID PayableAuditFeeID,A28.Name PayableAuditFeeDesc,
                               A29.FundJournalAccountPK BrokerCommission,A29.ID BrokerCommissionID,A29.Name BrokerCommissionDesc,
                               A30.FundJournalAccountPK BrokerLevy,A30.ID BrokerLevyID,A30.Name BrokerLevyDesc,
                               A31.FundJournalAccountPK BrokerVat,A31.ID BrokerVatID,A31.Name BrokerVatDesc,
                               A32.FundJournalAccountPK BrokerSalesTax,A32.ID BrokerSalesTaxID,A32.Name BrokerSalesTaxDesc,
                               A33.FundJournalAccountPK WithHoldingTaxPPH23,A33.ID WithHoldingTaxPPH23ID,A3.Name WithHoldingTaxPPH23Desc,
                               A34.FundJournalAccountPK WHTTaxPayableAccrInterestBond,A34.ID WHTTaxPayableAccrInterestBondID,A34.Name WHTTaxPayableAccrInterestBondDesc,
                               A35.FundJournalAccountPK ManagementFeeExpense,A35.ID ManagementFeeExpenseID,A35.Name ManagementFeeExpenseDesc,
                               A36.FundJournalAccountPK CustodianFeeExpense,A36.ID CustodianFeeExpenseID,A36.Name CustodianFeeExpenseDesc,
                               A37.FundJournalAccountPK AuditFeeExpense,A37.ID AuditFeeExpenseID,A37.Name AuditFeeExpenseDesc,
                               A38.FundJournalAccountPK BankCharges,A38.ID BankChargesID,A38.Name BankChargesDesc,
                               A39.FundJournalAccountPK TaxExpenseInterestIncomeBond,A39.ID TaxExpenseInterestIncomeBondID,A39.Name TaxExpenseInterestIncomeBondDesc,
                               A40.FundJournalAccountPK TaxExpenseInterestIncomeTimeDeposit,A40.ID TaxExpenseInterestIncomeTimeDepositID,A40.Name TaxExpenseInterestIncomeTimeDepositDesc,
                               A41.FundJournalAccountPK RealisedEquity,A41.ID RealisedEquityID,A41.Name RealisedEquityDesc,
                               A42.FundJournalAccountPK UnrealisedBond,A42.ID UnrealisedBondID,A42.Name UnrealisedBondDesc,
                               A43.FundJournalAccountPK UnrealisedEquity,A43.ID UnrealisedEquityID,A43.Name UnrealisedEquityDesc,
                               A44.FundJournalAccountPK DistributedIncomeAcc,A44.ID DistributedIncomeAccID,A44.Name DistributedIncomeAccDesc,
                               A45.FundJournalAccountPK DistributedIncomePayableAcc,A45.ID DistributedIncomePayableAccID,A45.Name DistributedIncomePayableAccDesc,
                               A46.FundJournalAccountPK PendingSubscription,A46.ID PendingSubscriptionID,A46.Name PendingSubscriptionDesc,
                               A47.FundJournalAccountPK PendingRedemption,A47.ID PendingRedemptionID,A47.Name PendingRedemptionDesc,
                               A48.FundJournalAccountPK ARDividend,A48.ID ARDividendID,A48.Name ARDividendDesc,
                               isnull(A49.FundJournalAccountPK,0) RealisedBond,isnull(A49.ID,'') RealisedBondID,isnull(A49.Name,'') RealisedBondDesc,
                                isnull(A50.FundJournalAccountPK,0) TaxCapitalGainBond,isnull(A50.ID,'') TaxCapitalGainBondID,isnull(A50.Name,'') TaxCapitalGainBondDesc,
                                isnull(A51.FundJournalAccountPK,0) PayableSInvestFee,isnull(A51.ID,'') PayableSInvestFeeID,isnull(A51.Name,'') PayableSInvestFeeDesc,
                                isnull(A52.FundJournalAccountPK,0) SInvestFee,isnull(A52.ID,'') SInvestFeeID,isnull(A52.Name,'') SInvestFeeDesc,
                                isnull(A53.FundJournalAccountPK,0) BondAmortization,isnull(A53.ID,'') BondAmortizationID,isnull(A53.Name,'') BondAmortizationDesc,
                                isnull(A54.FundJournalAccountPK,0) PayableMovementFee,isnull(A54.ID,'') PayableMovementFeeID,isnull(A54.Name,'') PayableMovementFeeDesc,
                                isnull(A55.FundJournalAccountPK,0) MovementFeeExpense,isnull(A55.ID,'') MovementFeeExpenseID,isnull(A55.Name,'') MovementFeeExpenseDesc,
                                isnull(A56.FundJournalAccountPK,0) PayablePurchaseMutualFund,isnull(A56.ID,'') PayablePurchaseMutualFundID,isnull(A56.Name,'') PayablePurchaseMutualFundDesc,
                                isnull(A57.FundJournalAccountPK,0) InvestmentMutualFund,isnull(A57.ID,'') InvestmentMutualFundID,isnull(A57.Name,'') InvestmentMutualFundDesc,
                                isnull(A58.FundJournalAccountPK,0) AccountReceivableSaleMutualFund,isnull(A58.ID,'') AccountReceivableSaleMutualFundID,isnull(A58.Name,'') AccountReceivableSaleMutualFundDesc,
                                isnull(A59.FundJournalAccountPK,0) RevaluationMutualFund,isnull(A59.ID,'') RevaluationMutualFundID,isnull(A59.Name,'') RevaluationMutualFundDesc,
                                isnull(A60.FundJournalAccountPK,0) UnrealisedMutualFund,isnull(A60.ID,'') UnrealisedMutualFundID,isnull(A60.Name,'') UnrealisedMutualFundDesc,
                                isnull(A61.FundJournalAccountPK,0) RealisedMutualFund,isnull(A61.ID,'') RealisedMutualFundID,isnull(A61.Name,'') RealisedMutualFundDesc,
                                isnull(A62.FundJournalAccountPK,0) PayableOtherFeeOne,isnull(A62.ID,'') PayableOtherFeeOneID,isnull(A62.Name,'') PayableOtherFeeOneDesc,
                                isnull(A63.FundJournalAccountPK,0) PayableOtherFeeTwo,isnull(A63.ID,'') PayableOtherFeeTwoID,isnull(A63.Name,'') PayableOtherFeeTwoDesc,
                                isnull(A64.FundJournalAccountPK,0) OtherFeeOneExpense,isnull(A64.ID,'') OtherFeeOneExpenseID,isnull(A64.Name,'') OtherFeeOneExpenseDesc,
                                isnull(A65.FundJournalAccountPK,0) OtherFeeTwoExpense,isnull(A65.ID,'') OtherFeeTwoExpenseID,isnull(A65.Name,'') OtherFeeTwoExpenseDesc,
                                isnull(A66.FundJournalAccountPK,0) PendingSwitching,isnull(A66.ID,'') PendingSwitchingID,isnull(A66.Name,'') PendingSwitchingDesc,
                                isnull(A67.FundJournalAccountPK,0) CurrentYearAccount,isnull(A67.ID,'') CurrentYearAccountID,isnull(A67.Name,'') CurrentYearAccountDesc,
                                isnull(A68.FundJournalAccountPK,0) PriorYearAccount,isnull(A68.ID,'') PriorYearAccountID,isnull(A68.Name,'') PriorYearAccountDesc,
                                F.* from TemplateFundAccountingSetup F left join 
                                   
                               FundJournalAccount A1 on F.Subscription = A1.FundJournalAccountPK and A1.status = 2  left join         
                               FundJournalAccount A2 on F.PayableSubscriptionFee = A2.FundJournalAccountPK and A2.status = 2  left join         
                               FundJournalAccount A3 on F.Redemption = A3.FundJournalAccountPK and A3.status = 2  left join               
                               FundJournalAccount A6 on F.PayableRedemptionFee = A6.FundJournalAccountPK and A6.status = 2   left join         
                               FundJournalAccount A4 on F.Switching = A4.FundJournalAccountPK and A4.status = 2  left join               
                               FundJournalAccount A5 on F.PayableSwitchingFee = A5.FundJournalAccountPK and A5.status = 2   left join       
                               FundJournalAccount A7 on F.InvestmentEquity = A7.FundJournalAccountPK and A7.status = 2  left join         
                               FundJournalAccount A8 on F.InvestmentBond = A8.FundJournalAccountPK and A8.status = 2  left join         
                               FundJournalAccount A9 on F.InvestmentTimeDeposit = A9.FundJournalAccountPK and A9.status = 2   left join     
                               FundJournalAccount A10 on F.InterestRecBond = A10.FundJournalAccountPK and A10.status = 2  left join         
                               FundJournalAccount A11 on F.InterestAccrBond = A11.FundJournalAccountPK and A11.status = 2  left join         
                               FundJournalAccount A12 on F.InterestAccrTimeDeposit = A12.FundJournalAccountPK and A12.status = 2   left join     
                               FundJournalAccount A13 on F.InterestAccrGiro = A13.FundJournalAccountPK and A13.status = 2  left join         
                               FundJournalAccount A14 on F.PrepaidTaxDividend = A14.FundJournalAccountPK and A14.status = 2  left join         
                               FundJournalAccount A15 on F.AccountReceivableSaleBond = A15.FundJournalAccountPK and A15.status = 2   left join     
                               FundJournalAccount A16 on F.AccountReceivableSaleEquity = A16.FundJournalAccountPK and A16.status = 2  left join         
                               FundJournalAccount A17 on F.AccountReceivableSaleTimeDeposit = A17.FundJournalAccountPK and A17.status = 2  left join         
                               FundJournalAccount A18 on F.IncomeInterestBond = A18.FundJournalAccountPK and A18.status = 2   left join     
                               FundJournalAccount A19 on F.IncomeInterestTimeDeposit = A19.FundJournalAccountPK and A19.status = 2  left join         
                               FundJournalAccount A20 on F.IncomeInterestGiro = A20.FundJournalAccountPK and A20.status = 2  left join         
                               FundJournalAccount A21 on F.IncomeDividend = A21.FundJournalAccountPK and A21.status = 2   left join     
                               FundJournalAccount A22 on F.RevaluationBond = A22.FundJournalAccountPK and A22.status = 2  left join         
                               FundJournalAccount A23 on F.RevaluationEquity = A23.FundJournalAccountPK and A23.status = 2  left join         
                               FundJournalAccount A24 on F.PayablePurchaseEquity = A24.FundJournalAccountPK and A24.status = 2    left join    
                               FundJournalAccount A25 on F.PayablePurRecBond = A25.FundJournalAccountPK and A25.status = 2  left join         
                               FundJournalAccount A26 on F.PayableManagementFee = A26.FundJournalAccountPK and A26.status = 2  left join         
                               FundJournalAccount A27 on F.PayableCustodianFee = A27.FundJournalAccountPK and A27.status = 2   left join     
                               FundJournalAccount A28 on F.PayableAuditFee = A28.FundJournalAccountPK and A28.status = 2  left join         
                               FundJournalAccount A29 on F.BrokerCommission = A29.FundJournalAccountPK and A29.status = 2  left join         
                               FundJournalAccount A30 on F.BrokerLevy = A30.FundJournalAccountPK and A30.status = 2    left join    
                               FundJournalAccount A31 on F.BrokerVat = A31.FundJournalAccountPK and A31.status = 2  left join         
                               FundJournalAccount A32 on F.BrokerSalesTax = A32.FundJournalAccountPK and A32.status = 2  left join         
                               FundJournalAccount A33 on F.WithHoldingTaxPPH23 = A33.FundJournalAccountPK and A33.status = 2    left join    
                               FundJournalAccount A34 on F.WHTTaxPayableAccrInterestBond = A34.FundJournalAccountPK and A34.status = 2  left join         
                               FundJournalAccount A35 on F.ManagementFeeExpense = A35.FundJournalAccountPK and A35.status = 2  left join         
                               FundJournalAccount A36 on F.CustodianFeeExpense = A36.FundJournalAccountPK and A36.status = 2   left join     
                               FundJournalAccount A37 on F.AuditFeeExpense = A37.FundJournalAccountPK and A37.status = 2  left join         
                               FundJournalAccount A38 on F.BankCharges = A38.FundJournalAccountPK and A38.status = 2  left join         
                               FundJournalAccount A39 on F.TaxExpenseInterestIncomeBond = A39.FundJournalAccountPK and A39.status = 2   left join     
                               FundJournalAccount A40 on F.TaxExpenseInterestIncomeTimeDeposit = A40.FundJournalAccountPK and A40.status = 2  left join         
                               FundJournalAccount A41 on F.RealisedEquity = A41.FundJournalAccountPK and A41.status = 2  left join         
                               FundJournalAccount A42 on F.UnrealisedBond = A42.FundJournalAccountPK and A42.status = 2   left join     
                               FundJournalAccount A43 on F.UnrealisedEquity = A43.FundJournalAccountPK and A43.status = 2  left join         
                               FundJournalAccount A44 on F.DistributedIncomeAcc = A44.FundJournalAccountPK and A44.status = 2  left join         
                               FundJournalAccount A45 on F.DistributedIncomePayableAcc = A45.FundJournalAccountPK and A45.status = 2  left join      
                               FundJournalAccount A46 on F.PendingSubscription = A46.FundJournalAccountPK and A46.status = 2  left join         
                               FundJournalAccount A47 on F.PendingRedemption = A47.FundJournalAccountPK and A47.status = 2   left join  
                               FundJournalAccount A48 on F.ARDividend = A48.FundJournalAccountPK and A48.status = 2  left join 
                               FundJournalAccount A49 on F.RealisedBond = A49.FundJournalAccountPK and A49.status = 2 left join
                               FundJournalAccount A50 on F.TaxCapitalGainBond = A50.FundJournalAccountPK and A50.status = 2  left join
                               FundJournalAccount A51 on F.PayableSInvestFee = A51.FundJournalAccountPK and A51.status in (1,2) left join          
                               FundJournalAccount A52 on F.SInvestFee = A52.FundJournalAccountPK and A52.status in (1,2)  left join
                               FundJournalAccount A53 on F.BondAmortization = A53.FundJournalAccountPK and A53.status in (1,2) left join
                                FundJournalAccount A54 on F.PayableMovementFee = A54.FundJournalAccountPK and A54.status in (1,2) left join      
                                FundJournalAccount A55 on F.MovementFeeExpense = A55.FundJournalAccountPK and A55.status in (1,2) left join   
                                FundJournalAccount A56 on F.PayablePurchaseMutualFund = A56.FundJournalAccountPK and A56.status in (1,2) left join        
                                FundJournalAccount A57 on F.InvestmentMutualFund = A57.FundJournalAccountPK and A57.status in (1,2) left join        
                                FundJournalAccount A58 on F.AccountReceivableSaleMutualFund = A58.FundJournalAccountPK and A58.status in (1,2) left join        
                                FundJournalAccount A59 on F.RevaluationMutualFund = A59.FundJournalAccountPK and A59.status in (1,2) left join  
                                FundJournalAccount A60 on F.UnrealisedMutualFund = A60.FundJournalAccountPK and A60.status in (1,2) left join   
                                FundJournalAccount A61 on F.RealisedMutualFund = A61.FundJournalAccountPK and A61.status in (1,2) left join

                                FundJournalAccount A62 on F.PayableOtherFeeOne = A62.FundJournalAccountPK and A62.status in (1,2)   left join 
                                FundJournalAccount A63 on F.PayableOtherFeeTwo = A63.FundJournalAccountPK and A63.status in (1,2)   left join 
                                FundJournalAccount A64 on F.OtherFeeOneExpense = A64.FundJournalAccountPK and A64.status in (1,2)   left join 
                                FundJournalAccount A65 on F.OtherFeeTwoExpense = A65.FundJournalAccountPK and A65.status in (1,2)   left join
                                FundJournalAccount A66 on F.PendingSwitching = A66.FundJournalAccountPK and A66.status in (1,2)   left join
                                FundJournalAccount A67 on F.CurrentYearAccount = A67.FundJournalAccountPK and A67.status in (1,2)   left join
                                FundJournalAccount A68 on F.PriorYearAccount = A68.FundJournalAccountPK and A68.status in (1,2)   
 
                               Order By TemplateFundAccountingSetupPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TemplateFundAccountingSetup.Add(setTemplateFundAccountingSetup(dr));
                                }
                            }
                            return L_TemplateFundAccountingSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int TemplateFundAccountingSetup_Add(TemplateFundAccountingSetup _TemplateFundAccountingSetup, bool _havePrivillege)
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
                                 "Select isnull(max(TemplateFundAccountingSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from TemplateFundAccountingSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TemplateFundAccountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(TemplateFundAccountingSetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from TemplateFundAccountingSetup";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Subscription", _TemplateFundAccountingSetup.Subscription);
                        cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _TemplateFundAccountingSetup.PayableSubscriptionFee);
                        cmd.Parameters.AddWithValue("@Redemption", _TemplateFundAccountingSetup.Redemption);
                        cmd.Parameters.AddWithValue("@PayableRedemptionFee", _TemplateFundAccountingSetup.PayableRedemptionFee);
                        cmd.Parameters.AddWithValue("@Switching", _TemplateFundAccountingSetup.Switching);
                        cmd.Parameters.AddWithValue("@PayableSwitchingFee", _TemplateFundAccountingSetup.PayableSwitchingFee);
                        cmd.Parameters.AddWithValue("@InvestmentEquity", _TemplateFundAccountingSetup.InvestmentEquity);
                        cmd.Parameters.AddWithValue("@InvestmentBond", _TemplateFundAccountingSetup.InvestmentBond);
                        cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _TemplateFundAccountingSetup.InvestmentTimeDeposit);
                        cmd.Parameters.AddWithValue("@InterestRecBond", _TemplateFundAccountingSetup.InterestRecBond);
                        cmd.Parameters.AddWithValue("@InterestAccrBond", _TemplateFundAccountingSetup.InterestAccrBond);
                        cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _TemplateFundAccountingSetup.InterestAccrTimeDeposit);
                        cmd.Parameters.AddWithValue("@InterestAccrGiro", _TemplateFundAccountingSetup.InterestAccrGiro);
                        cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _TemplateFundAccountingSetup.PrepaidTaxDividend);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _TemplateFundAccountingSetup.AccountReceivableSaleBond);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _TemplateFundAccountingSetup.AccountReceivableSaleEquity);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _TemplateFundAccountingSetup.AccountReceivableSaleTimeDeposit);
                        cmd.Parameters.AddWithValue("@IncomeInterestBond", _TemplateFundAccountingSetup.IncomeInterestBond);
                        cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _TemplateFundAccountingSetup.IncomeInterestTimeDeposit);
                        cmd.Parameters.AddWithValue("@IncomeInterestGiro", _TemplateFundAccountingSetup.IncomeInterestGiro);
                        cmd.Parameters.AddWithValue("@IncomeDividend", _TemplateFundAccountingSetup.IncomeDividend);
                        cmd.Parameters.AddWithValue("@ARDividend", _TemplateFundAccountingSetup.ARDividend);
                        cmd.Parameters.AddWithValue("@RevaluationBond", _TemplateFundAccountingSetup.RevaluationBond);
                        cmd.Parameters.AddWithValue("@RevaluationEquity", _TemplateFundAccountingSetup.RevaluationEquity);
                        cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _TemplateFundAccountingSetup.PayablePurchaseEquity);
                        cmd.Parameters.AddWithValue("@PayablePurRecBond", _TemplateFundAccountingSetup.PayablePurRecBond);
                        cmd.Parameters.AddWithValue("@PayableManagementFee", _TemplateFundAccountingSetup.PayableManagementFee);
                        cmd.Parameters.AddWithValue("@PayableCustodianFee", _TemplateFundAccountingSetup.PayableCustodianFee);
                        cmd.Parameters.AddWithValue("@PayableAuditFee", _TemplateFundAccountingSetup.PayableAuditFee);
                        cmd.Parameters.AddWithValue("@BrokerCommission", _TemplateFundAccountingSetup.BrokerCommission);
                        cmd.Parameters.AddWithValue("@BrokerLevy", _TemplateFundAccountingSetup.BrokerLevy);
                        cmd.Parameters.AddWithValue("@BrokerVat", _TemplateFundAccountingSetup.BrokerVat);
                        cmd.Parameters.AddWithValue("@BrokerSalesTax", _TemplateFundAccountingSetup.BrokerSalesTax);
                        cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _TemplateFundAccountingSetup.WithHoldingTaxPPH23);
                        cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _TemplateFundAccountingSetup.WHTTaxPayableAccrInterestBond);
                        cmd.Parameters.AddWithValue("@ManagementFeeExpense", _TemplateFundAccountingSetup.ManagementFeeExpense);
                        cmd.Parameters.AddWithValue("@CustodianFeeExpense", _TemplateFundAccountingSetup.CustodianFeeExpense);
                        cmd.Parameters.AddWithValue("@AuditFeeExpense", _TemplateFundAccountingSetup.AuditFeeExpense);
                        cmd.Parameters.AddWithValue("@BankCharges", _TemplateFundAccountingSetup.BankCharges);
                        cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeBond);
                        cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                        cmd.Parameters.AddWithValue("@RealisedBond", _TemplateFundAccountingSetup.RealisedBond);
                        cmd.Parameters.AddWithValue("@RealisedEquity", _TemplateFundAccountingSetup.RealisedEquity);
                        cmd.Parameters.AddWithValue("@UnrealisedBond", _TemplateFundAccountingSetup.UnrealisedBond);
                        cmd.Parameters.AddWithValue("@UnrealisedEquity", _TemplateFundAccountingSetup.UnrealisedEquity);
                        cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _TemplateFundAccountingSetup.DistributedIncomeAcc);
                        cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _TemplateFundAccountingSetup.DistributedIncomePayableAcc);
                        cmd.Parameters.AddWithValue("@PendingSubscription", _TemplateFundAccountingSetup.PendingSubscription);
                        cmd.Parameters.AddWithValue("@PendingRedemption", _TemplateFundAccountingSetup.PendingRedemption);
                        cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _TemplateFundAccountingSetup.TaxCapitalGainBond);
                        cmd.Parameters.AddWithValue("@PayableSInvestFee", _TemplateFundAccountingSetup.PayableSInvestFee);
                        cmd.Parameters.AddWithValue("@SInvestFee", _TemplateFundAccountingSetup.SInvestFee);

                        cmd.Parameters.AddWithValue("@TaxPercentageDividend", _TemplateFundAccountingSetup.TaxPercentageDividend);
                        cmd.Parameters.AddWithValue("@TaxPercentageBond", _TemplateFundAccountingSetup.TaxPercentageBond);
                        cmd.Parameters.AddWithValue("@TaxPercentageTD", _TemplateFundAccountingSetup.TaxPercentageTD);
                        cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _TemplateFundAccountingSetup.TaxPercentageCapitalGain);
                        cmd.Parameters.AddWithValue("@BondAmortization", _TemplateFundAccountingSetup.BondAmortization);
                        cmd.Parameters.AddWithValue("@PayableMovementFee", _TemplateFundAccountingSetup.PayableMovementFee);
                        cmd.Parameters.AddWithValue("@MovementFeeExpense", _TemplateFundAccountingSetup.MovementFeeExpense);

                        cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _TemplateFundAccountingSetup.PayablePurchaseMutualFund);
                        cmd.Parameters.AddWithValue("@InvestmentMutualFund", _TemplateFundAccountingSetup.InvestmentMutualFund);
                        cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _TemplateFundAccountingSetup.AccountReceivableSaleMutualFund);
                        cmd.Parameters.AddWithValue("@RevaluationMutualFund", _TemplateFundAccountingSetup.RevaluationMutualFund);
                        cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _TemplateFundAccountingSetup.UnrealisedMutualFund);
                        cmd.Parameters.AddWithValue("@RealisedMutualFund", _TemplateFundAccountingSetup.RealisedMutualFund);

                        cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _TemplateFundAccountingSetup.PayableOtherFeeOne);
                        cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _TemplateFundAccountingSetup.PayableOtherFeeTwo);
                        cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _TemplateFundAccountingSetup.OtherFeeOneExpense);
                        cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _TemplateFundAccountingSetup.OtherFeeTwoExpense);

                        cmd.Parameters.AddWithValue("@PendingSwitching", _TemplateFundAccountingSetup.PendingSwitching);
                        cmd.Parameters.AddWithValue("@CurrentYearAccount", _TemplateFundAccountingSetup.CurrentYearAccount);
                        cmd.Parameters.AddWithValue("@PriorYearAccount", _TemplateFundAccountingSetup.PriorYearAccount);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _TemplateFundAccountingSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "TemplateFundAccountingSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int TemplateFundAccountingSetup_Update(TemplateFundAccountingSetup _TemplateFundAccountingSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_TemplateFundAccountingSetup.TemplateFundAccountingSetupPK, _TemplateFundAccountingSetup.HistoryPK, "TemplateFundAccountingSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"Update TemplateFundAccountingSetup set status=1,Notes=@Notes,Subscription=@Subscription,PayableSubscriptionFee=@PayableSubscriptionFee,TaxPercentageDividend=@TaxPercentageDividend,TaxPercentageBond=@TaxPercentageBond,TaxPercentageTD=@TaxPercentageTD,Switching=@Switching,PayableSwitchingFee=@PayableSwitchingFee,
                            Redemption=@Redemption,PayableRedemptionFee=@PayableRedemptionFee, PrepaidTaxDividend=@PrepaidTaxDividend, AccountReceivableSaleBond= @AccountReceivableSaleBond, AccountReceivableSaleEquity=@AccountReceivableSaleEquity, AccountReceivableSaleTimeDeposit=@AccountReceivableSaleTimeDeposit, 
                            IncomeInterestBond= @IncomeInterestBond, IncomeInterestTimeDeposit=@IncomeInterestTimeDeposit, IncomeInterestGiro=@IncomeInterestGiro, IncomeDividend=@IncomeDividend,ARDividend=@ARDividend,
                            RevaluationBond=@RevaluationBond, RevaluationEquity=@RevaluationEquity, PayablePurchaseEquity=@PayablePurchaseEquity, PayablePurRecBond=@PayablePurRecBond, 
                            PayableManagementFee=@PayableManagementFee, PayableCustodianFee=@PayableCustodianFee,
                            PayableAuditFee=@PayableAuditFee, BrokerCommission=@BrokerCommission, BrokerLevy=@BrokerLevy, BrokerVat=@BrokerVat, BrokerSalesTax=@BrokerSalesTax, 
                            WithHoldingTaxPPH23=@WithHoldingTaxPPH23, WHTTaxPayableAccrInterestBond=@WHTTaxPayableAccrInterestBond, ManagementFeeExpense=@ManagementFeeExpense, 
                            CustodianFeeExpense= @CustodianFeeExpense, AuditFeeExpense=@AuditFeeExpense, BankCharges=@BankCharges, TaxExpenseInterestIncomeBond=@TaxExpenseInterestIncomeBond, 
                            TaxExpenseInterestIncomeTimeDeposit=@TaxExpenseInterestIncomeTimeDeposit, RealisedEquity = @RealisedEquity, UnrealisedBond=@UnrealisedBond, UnrealisedEquity=@UnrealisedEquity, 
                            DistributedIncomeAcc=@DistributedIncomeAcc, DistributedIncomePayableAcc=@DistributedIncomePayableAcc, PendingSubscription=@PendingSubscription, PendingRedemption=@PendingRedemption, RealisedBond = @RealisedBond, TaxCapitalGainBond = @TaxCapitalGainBond, TaxPercentageCapitalGain=@TaxPercentageCapitalGain,  PayableSInvestFee = @PayableSInvestFee, SInvestFee = @SInvestFee, BondAmortization = @BondAmortization,                              
                            PayableMovementFee=@PayableMovementFee, MovementFeeExpense=@MovementFeeExpense, PayablePurchaseMutualFund=@PayablePurchaseMutualFund, 
                            InvestmentMutualFund=@InvestmentMutualFund, AccountReceivableSaleMutualFund=@AccountReceivableSaleMutualFund, RevaluationMutualFund=@RevaluationMutualFund, UnrealisedMutualFund=@UnrealisedMutualFund, RealisedMutualFund=@RealisedMutualFund,
                            PayableOtherFeeOne=@PayableOtherFeeOne,PayableOtherFeeTwo=@PayableOtherFeeTwo,OtherFeeOneExpense=@OtherFeeOneExpense,OtherFeeTwoExpense=@OtherFeeTwoExpense, PendingSwitching=@PendingSwitching,CurrentYearAccount=@CurrentYearAccount,PriorYearAccount=@PriorYearAccount,
                            UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                            where TemplateFundAccountingSetupPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _TemplateFundAccountingSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _TemplateFundAccountingSetup.Notes);
                            cmd.Parameters.AddWithValue("@Subscription", _TemplateFundAccountingSetup.Subscription);
                            cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _TemplateFundAccountingSetup.PayableSubscriptionFee);
                            cmd.Parameters.AddWithValue("@Redemption", _TemplateFundAccountingSetup.Redemption);
                            cmd.Parameters.AddWithValue("@PayableRedemptionFee", _TemplateFundAccountingSetup.PayableRedemptionFee);
                            cmd.Parameters.AddWithValue("@Switching", _TemplateFundAccountingSetup.Switching);
                            cmd.Parameters.AddWithValue("@PayableSwitchingFee", _TemplateFundAccountingSetup.PayableSwitchingFee);
                            cmd.Parameters.AddWithValue("@InvestmentEquity", _TemplateFundAccountingSetup.InvestmentEquity);
                            cmd.Parameters.AddWithValue("@InvestmentBond", _TemplateFundAccountingSetup.InvestmentBond);
                            cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _TemplateFundAccountingSetup.InvestmentTimeDeposit);
                            cmd.Parameters.AddWithValue("@InterestRecBond", _TemplateFundAccountingSetup.InterestRecBond);
                            cmd.Parameters.AddWithValue("@InterestAccrBond", _TemplateFundAccountingSetup.InterestAccrBond);
                            cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _TemplateFundAccountingSetup.InterestAccrTimeDeposit);
                            cmd.Parameters.AddWithValue("@InterestAccrGiro", _TemplateFundAccountingSetup.InterestAccrGiro);
                            cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _TemplateFundAccountingSetup.PrepaidTaxDividend);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _TemplateFundAccountingSetup.AccountReceivableSaleBond);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _TemplateFundAccountingSetup.AccountReceivableSaleEquity);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _TemplateFundAccountingSetup.AccountReceivableSaleTimeDeposit);
                            cmd.Parameters.AddWithValue("@IncomeInterestBond", _TemplateFundAccountingSetup.IncomeInterestBond);
                            cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _TemplateFundAccountingSetup.IncomeInterestTimeDeposit);
                            cmd.Parameters.AddWithValue("@IncomeInterestGiro", _TemplateFundAccountingSetup.IncomeInterestGiro);
                            cmd.Parameters.AddWithValue("@IncomeDividend", _TemplateFundAccountingSetup.IncomeDividend);
                            cmd.Parameters.AddWithValue("@ARDividend", _TemplateFundAccountingSetup.ARDividend);
                            cmd.Parameters.AddWithValue("@RevaluationBond", _TemplateFundAccountingSetup.RevaluationBond);
                            cmd.Parameters.AddWithValue("@RevaluationEquity", _TemplateFundAccountingSetup.RevaluationEquity);
                            cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _TemplateFundAccountingSetup.PayablePurchaseEquity);
                            cmd.Parameters.AddWithValue("@PayablePurRecBond", _TemplateFundAccountingSetup.PayablePurRecBond);
                            cmd.Parameters.AddWithValue("@PayableManagementFee", _TemplateFundAccountingSetup.PayableManagementFee);
                            cmd.Parameters.AddWithValue("@PayableCustodianFee", _TemplateFundAccountingSetup.PayableCustodianFee);
                            cmd.Parameters.AddWithValue("@PayableAuditFee", _TemplateFundAccountingSetup.PayableAuditFee);
                            cmd.Parameters.AddWithValue("@BrokerCommission", _TemplateFundAccountingSetup.BrokerCommission);
                            cmd.Parameters.AddWithValue("@BrokerLevy", _TemplateFundAccountingSetup.BrokerLevy);
                            cmd.Parameters.AddWithValue("@BrokerVat", _TemplateFundAccountingSetup.BrokerVat);
                            cmd.Parameters.AddWithValue("@BrokerSalesTax", _TemplateFundAccountingSetup.BrokerSalesTax);
                            cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _TemplateFundAccountingSetup.WithHoldingTaxPPH23);
                            cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _TemplateFundAccountingSetup.WHTTaxPayableAccrInterestBond);
                            cmd.Parameters.AddWithValue("@ManagementFeeExpense", _TemplateFundAccountingSetup.ManagementFeeExpense);
                            cmd.Parameters.AddWithValue("@CustodianFeeExpense", _TemplateFundAccountingSetup.CustodianFeeExpense);
                            cmd.Parameters.AddWithValue("@AuditFeeExpense", _TemplateFundAccountingSetup.AuditFeeExpense);
                            cmd.Parameters.AddWithValue("@BankCharges", _TemplateFundAccountingSetup.BankCharges);
                            cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeBond);
                            cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                            cmd.Parameters.AddWithValue("@RealisedBond", _TemplateFundAccountingSetup.RealisedBond);
                            cmd.Parameters.AddWithValue("@RealisedEquity", _TemplateFundAccountingSetup.RealisedEquity);
                            cmd.Parameters.AddWithValue("@UnrealisedBond", _TemplateFundAccountingSetup.UnrealisedBond);
                            cmd.Parameters.AddWithValue("@UnrealisedEquity", _TemplateFundAccountingSetup.UnrealisedEquity);
                            cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _TemplateFundAccountingSetup.DistributedIncomeAcc);
                            cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _TemplateFundAccountingSetup.DistributedIncomePayableAcc);
                            cmd.Parameters.AddWithValue("@PendingSubscription", _TemplateFundAccountingSetup.PendingSubscription);
                            cmd.Parameters.AddWithValue("@PendingRedemption", _TemplateFundAccountingSetup.PendingRedemption);
                            cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _TemplateFundAccountingSetup.TaxCapitalGainBond);
                            cmd.Parameters.AddWithValue("@PayableSInvestFee", _TemplateFundAccountingSetup.PayableSInvestFee);
                            cmd.Parameters.AddWithValue("@SInvestFee", _TemplateFundAccountingSetup.SInvestFee);

                            cmd.Parameters.AddWithValue("@TaxPercentageDividend", _TemplateFundAccountingSetup.TaxPercentageDividend);
                            cmd.Parameters.AddWithValue("@TaxPercentageBond", _TemplateFundAccountingSetup.TaxPercentageBond);
                            cmd.Parameters.AddWithValue("@TaxPercentageTD", _TemplateFundAccountingSetup.TaxPercentageTD);
                            cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _TemplateFundAccountingSetup.TaxPercentageCapitalGain);
                            cmd.Parameters.AddWithValue("@BondAmortization", _TemplateFundAccountingSetup.BondAmortization);
                            cmd.Parameters.AddWithValue("@PayableMovementFee", _TemplateFundAccountingSetup.PayableMovementFee);
                            cmd.Parameters.AddWithValue("@MovementFeeExpense", _TemplateFundAccountingSetup.MovementFeeExpense);

                            cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _TemplateFundAccountingSetup.PayablePurchaseMutualFund);
                            cmd.Parameters.AddWithValue("@InvestmentMutualFund", _TemplateFundAccountingSetup.InvestmentMutualFund);
                            cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _TemplateFundAccountingSetup.AccountReceivableSaleMutualFund);
                            cmd.Parameters.AddWithValue("@RevaluationMutualFund", _TemplateFundAccountingSetup.RevaluationMutualFund);
                            cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _TemplateFundAccountingSetup.UnrealisedMutualFund);
                            cmd.Parameters.AddWithValue("@RealisedMutualFund", _TemplateFundAccountingSetup.RealisedMutualFund);

                            cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _TemplateFundAccountingSetup.PayableOtherFeeOne);
                            cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _TemplateFundAccountingSetup.PayableOtherFeeTwo);
                            cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _TemplateFundAccountingSetup.OtherFeeOneExpense);
                            cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _TemplateFundAccountingSetup.OtherFeeTwoExpense);

                            cmd.Parameters.AddWithValue("@PendingSwitching", _TemplateFundAccountingSetup.PendingSwitching);
                            cmd.Parameters.AddWithValue("@CurrentYearAccount", _TemplateFundAccountingSetup.CurrentYearAccount);
                            cmd.Parameters.AddWithValue("@PriorYearAccount", _TemplateFundAccountingSetup.PriorYearAccount);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _TemplateFundAccountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TemplateFundAccountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TemplateFundAccountingSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TemplateFundAccountingSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateFundAccountingSetup.EntryUsersID);
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
                                cmd.CommandText = @"Update TemplateFundAccountingSetup set status=1,Notes=@Notes,Subscription=@Subscription,PayableSubscriptionFee=@PayableSubscriptionFee,TaxPercentageDividend=@TaxPercentageDividend,TaxPercentageBond=@TaxPercentageBond,TaxPercentageTD=@TaxPercentageTD,Switching=@Switching,PayableSwitchingFee=@PayableSwitchingFee,
                            Redemption=@Redemption,PayableRedemptionFee=@PayableRedemptionFee, PrepaidTaxDividend=@PrepaidTaxDividend, AccountReceivableSaleBond= @AccountReceivableSaleBond, AccountReceivableSaleEquity=@AccountReceivableSaleEquity, AccountReceivableSaleTimeDeposit=@AccountReceivableSaleTimeDeposit, 
                            IncomeInterestBond= @IncomeInterestBond, IncomeInterestTimeDeposit=@IncomeInterestTimeDeposit, IncomeInterestGiro=@IncomeInterestGiro, IncomeDividend=@IncomeDividend,ARDividend=@ARDividend,
                            RevaluationBond=@RevaluationBond, RevaluationEquity=@RevaluationEquity, PayablePurchaseEquity=@PayablePurchaseEquity, PayablePurRecBond=@PayablePurRecBond, 
                            PayableManagementFee=@PayableManagementFee, PayableCustodianFee=@PayableCustodianFee,
                            PayableAuditFee=@PayableAuditFee, BrokerCommission=@BrokerCommission, BrokerLevy=@BrokerLevy, BrokerVat=@BrokerVat, BrokerSalesTax=@BrokerSalesTax, 
                            WithHoldingTaxPPH23=@WithHoldingTaxPPH23, WHTTaxPayableAccrInterestBond=@WHTTaxPayableAccrInterestBond, ManagementFeeExpense=@ManagementFeeExpense, 
                            CustodianFeeExpense= @CustodianFeeExpense, AuditFeeExpense=@AuditFeeExpense, BankCharges=@BankCharges, TaxExpenseInterestIncomeBond=@TaxExpenseInterestIncomeBond, 
                            TaxExpenseInterestIncomeTimeDeposit=@TaxExpenseInterestIncomeTimeDeposit, RealisedEquity = @RealisedEquity, UnrealisedBond=@UnrealisedBond, UnrealisedEquity=@UnrealisedEquity, 
                            DistributedIncomeAcc=@DistributedIncomeAcc, DistributedIncomePayableAcc=@DistributedIncomePayableAcc, PendingSubscription=@PendingSubscription, PendingRedemption=@PendingRedemption, RealisedBond = @RealisedBond, TaxCapitalGainBond = @TaxCapitalGainBond,  TaxPercentageCapitalGain=@TaxPercentageCapitalGain, PayableSInvestFee = @PayableSInvestFee, SInvestFee = @SInvestFee,  BondAmortization = @BondAmortization,   
                            PayableMovementFee=@PayableMovementFee, MovementFeeExpense=@MovementFeeExpense, PayablePurchaseMutualFund=@PayablePurchaseMutualFund, 
                            InvestmentMutualFund=@InvestmentMutualFund, AccountReceivableSaleMutualFund=@AccountReceivableSaleMutualFund, RevaluationMutualFund=@RevaluationMutualFund, UnrealisedMutualFund=@UnrealisedMutualFund, RealisedMutualFund=@RealisedMutualFund,
                            PayableOtherFeeOne=@PayableOtherFeeOne,PayableOtherFeeTwo=@PayableOtherFeeTwo,OtherFeeOneExpense=@OtherFeeOneExpense,OtherFeeTwoExpense=@OtherFeeTwoExpense,PendingSwitching=@PendingSwitching,CurrentYearAccount=@CurrentYearAccount,PriorYearAccount=@PriorYearAccount,                          
                            UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                            where TemplateFundAccountingSetupPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _TemplateFundAccountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _TemplateFundAccountingSetup.Notes);
                                cmd.Parameters.AddWithValue("@Subscription", _TemplateFundAccountingSetup.Subscription);
                                cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _TemplateFundAccountingSetup.PayableSubscriptionFee);
                                cmd.Parameters.AddWithValue("@Redemption", _TemplateFundAccountingSetup.Redemption);
                                cmd.Parameters.AddWithValue("@PayableRedemptionFee", _TemplateFundAccountingSetup.PayableRedemptionFee);
                                cmd.Parameters.AddWithValue("@Switching", _TemplateFundAccountingSetup.Switching);
                                cmd.Parameters.AddWithValue("@PayableSwitchingFee", _TemplateFundAccountingSetup.PayableSwitchingFee);
                                cmd.Parameters.AddWithValue("@InvestmentEquity", _TemplateFundAccountingSetup.InvestmentEquity);
                                cmd.Parameters.AddWithValue("@InvestmentBond", _TemplateFundAccountingSetup.InvestmentBond);
                                cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _TemplateFundAccountingSetup.InvestmentTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestRecBond", _TemplateFundAccountingSetup.InterestRecBond);
                                cmd.Parameters.AddWithValue("@InterestAccrBond", _TemplateFundAccountingSetup.InterestAccrBond);
                                cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _TemplateFundAccountingSetup.InterestAccrTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestAccrGiro", _TemplateFundAccountingSetup.InterestAccrGiro);
                                cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _TemplateFundAccountingSetup.PrepaidTaxDividend);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _TemplateFundAccountingSetup.AccountReceivableSaleBond);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _TemplateFundAccountingSetup.AccountReceivableSaleEquity);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _TemplateFundAccountingSetup.AccountReceivableSaleTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestBond", _TemplateFundAccountingSetup.IncomeInterestBond);
                                cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _TemplateFundAccountingSetup.IncomeInterestTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestGiro", _TemplateFundAccountingSetup.IncomeInterestGiro);
                                cmd.Parameters.AddWithValue("@IncomeDividend", _TemplateFundAccountingSetup.IncomeDividend);
                                cmd.Parameters.AddWithValue("@ARDividend", _TemplateFundAccountingSetup.ARDividend);
                                cmd.Parameters.AddWithValue("@RevaluationBond", _TemplateFundAccountingSetup.RevaluationBond);
                                cmd.Parameters.AddWithValue("@RevaluationEquity", _TemplateFundAccountingSetup.RevaluationEquity);
                                cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _TemplateFundAccountingSetup.PayablePurchaseEquity);
                                cmd.Parameters.AddWithValue("@PayablePurRecBond", _TemplateFundAccountingSetup.PayablePurRecBond);
                                cmd.Parameters.AddWithValue("@PayableManagementFee", _TemplateFundAccountingSetup.PayableManagementFee);
                                cmd.Parameters.AddWithValue("@PayableCustodianFee", _TemplateFundAccountingSetup.PayableCustodianFee);
                                cmd.Parameters.AddWithValue("@PayableAuditFee", _TemplateFundAccountingSetup.PayableAuditFee);
                                cmd.Parameters.AddWithValue("@BrokerCommission", _TemplateFundAccountingSetup.BrokerCommission);
                                cmd.Parameters.AddWithValue("@BrokerLevy", _TemplateFundAccountingSetup.BrokerLevy);
                                cmd.Parameters.AddWithValue("@BrokerVat", _TemplateFundAccountingSetup.BrokerVat);
                                cmd.Parameters.AddWithValue("@BrokerSalesTax", _TemplateFundAccountingSetup.BrokerSalesTax);
                                cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _TemplateFundAccountingSetup.WithHoldingTaxPPH23);
                                cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _TemplateFundAccountingSetup.WHTTaxPayableAccrInterestBond);
                                cmd.Parameters.AddWithValue("@ManagementFeeExpense", _TemplateFundAccountingSetup.ManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@CustodianFeeExpense", _TemplateFundAccountingSetup.CustodianFeeExpense);
                                cmd.Parameters.AddWithValue("@AuditFeeExpense", _TemplateFundAccountingSetup.AuditFeeExpense);
                                cmd.Parameters.AddWithValue("@BankCharges", _TemplateFundAccountingSetup.BankCharges);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeBond);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                                cmd.Parameters.AddWithValue("@RealisedEquity", _TemplateFundAccountingSetup.RealisedEquity);
                                cmd.Parameters.AddWithValue("@UnrealisedBond", _TemplateFundAccountingSetup.UnrealisedBond);
                                cmd.Parameters.AddWithValue("@UnrealisedEquity", _TemplateFundAccountingSetup.UnrealisedEquity);
                                cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _TemplateFundAccountingSetup.DistributedIncomeAcc);
                                cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _TemplateFundAccountingSetup.DistributedIncomePayableAcc);
                                cmd.Parameters.AddWithValue("@PendingSubscription", _TemplateFundAccountingSetup.PendingSubscription);
                                cmd.Parameters.AddWithValue("@PendingRedemption", _TemplateFundAccountingSetup.PendingRedemption);
                                cmd.Parameters.AddWithValue("@RealisedBond", _TemplateFundAccountingSetup.RealisedBond);
                                cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _TemplateFundAccountingSetup.TaxCapitalGainBond);
                                cmd.Parameters.AddWithValue("@PayableSInvestFee", _TemplateFundAccountingSetup.PayableSInvestFee);
                                cmd.Parameters.AddWithValue("@SInvestFee", _TemplateFundAccountingSetup.SInvestFee);

                                cmd.Parameters.AddWithValue("@TaxPercentageDividend", _TemplateFundAccountingSetup.TaxPercentageDividend);
                                cmd.Parameters.AddWithValue("@TaxPercentageBond", _TemplateFundAccountingSetup.TaxPercentageBond);
                                cmd.Parameters.AddWithValue("@TaxPercentageTD", _TemplateFundAccountingSetup.TaxPercentageTD);
                                cmd.Parameters.AddWithValue("@BondAmortization", _TemplateFundAccountingSetup.BondAmortization);
                                cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _TemplateFundAccountingSetup.TaxPercentageCapitalGain);
                                cmd.Parameters.AddWithValue("@PayableMovementFee", _TemplateFundAccountingSetup.PayableMovementFee);
                                cmd.Parameters.AddWithValue("@MovementFeeExpense", _TemplateFundAccountingSetup.MovementFeeExpense);

                                cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _TemplateFundAccountingSetup.PayablePurchaseMutualFund);
                                cmd.Parameters.AddWithValue("@InvestmentMutualFund", _TemplateFundAccountingSetup.InvestmentMutualFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _TemplateFundAccountingSetup.AccountReceivableSaleMutualFund);
                                cmd.Parameters.AddWithValue("@RevaluationMutualFund", _TemplateFundAccountingSetup.RevaluationMutualFund);
                                cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _TemplateFundAccountingSetup.UnrealisedMutualFund);
                                cmd.Parameters.AddWithValue("@RealisedMutualFund", _TemplateFundAccountingSetup.RealisedMutualFund);

                                cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _TemplateFundAccountingSetup.PayableOtherFeeOne);
                                cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _TemplateFundAccountingSetup.PayableOtherFeeTwo);
                                cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _TemplateFundAccountingSetup.OtherFeeOneExpense);
                                cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _TemplateFundAccountingSetup.OtherFeeTwoExpense);

                                cmd.Parameters.AddWithValue("@PendingSwitching", _TemplateFundAccountingSetup.PendingSwitching);
                                cmd.Parameters.AddWithValue("@CurrentYearAccount", _TemplateFundAccountingSetup.CurrentYearAccount);
                                cmd.Parameters.AddWithValue("@PriorYearAccount", _TemplateFundAccountingSetup.PriorYearAccount);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TemplateFundAccountingSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_TemplateFundAccountingSetup.TemplateFundAccountingSetupPK, "TemplateFundAccountingSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From TemplateFundAccountingSetup where TemplateFundAccountingSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TemplateFundAccountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Subscription", _TemplateFundAccountingSetup.Subscription);
                                cmd.Parameters.AddWithValue("@PayableSubscriptionFee", _TemplateFundAccountingSetup.PayableSubscriptionFee);
                                cmd.Parameters.AddWithValue("@Redemption", _TemplateFundAccountingSetup.Redemption);
                                cmd.Parameters.AddWithValue("@PayableRedemptionFee", _TemplateFundAccountingSetup.PayableRedemptionFee);
                                cmd.Parameters.AddWithValue("@Switching", _TemplateFundAccountingSetup.Switching);
                                cmd.Parameters.AddWithValue("@PayableSwitchingFee", _TemplateFundAccountingSetup.PayableSwitchingFee);
                                cmd.Parameters.AddWithValue("@InvestmentEquity", _TemplateFundAccountingSetup.InvestmentEquity);
                                cmd.Parameters.AddWithValue("@InvestmentBond", _TemplateFundAccountingSetup.InvestmentBond);
                                cmd.Parameters.AddWithValue("@InvestmentTimeDeposit", _TemplateFundAccountingSetup.InvestmentTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestRecBond", _TemplateFundAccountingSetup.InterestRecBond);
                                cmd.Parameters.AddWithValue("@InterestAccrBond", _TemplateFundAccountingSetup.InterestAccrBond);
                                cmd.Parameters.AddWithValue("@InterestAccrTimeDeposit", _TemplateFundAccountingSetup.InterestAccrTimeDeposit);
                                cmd.Parameters.AddWithValue("@InterestAccrGiro", _TemplateFundAccountingSetup.InterestAccrGiro);
                                cmd.Parameters.AddWithValue("@PrepaidTaxDividend", _TemplateFundAccountingSetup.PrepaidTaxDividend);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleBond", _TemplateFundAccountingSetup.AccountReceivableSaleBond);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleEquity", _TemplateFundAccountingSetup.AccountReceivableSaleEquity);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleTimeDeposit", _TemplateFundAccountingSetup.AccountReceivableSaleTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestBond", _TemplateFundAccountingSetup.IncomeInterestBond);
                                cmd.Parameters.AddWithValue("@IncomeInterestTimeDeposit", _TemplateFundAccountingSetup.IncomeInterestTimeDeposit);
                                cmd.Parameters.AddWithValue("@IncomeInterestGiro", _TemplateFundAccountingSetup.IncomeInterestGiro);
                                cmd.Parameters.AddWithValue("@IncomeDividend", _TemplateFundAccountingSetup.IncomeDividend);
                                cmd.Parameters.AddWithValue("@ARDividend", _TemplateFundAccountingSetup.ARDividend);
                                cmd.Parameters.AddWithValue("@RevaluationBond", _TemplateFundAccountingSetup.RevaluationBond);
                                cmd.Parameters.AddWithValue("@RevaluationEquity", _TemplateFundAccountingSetup.RevaluationEquity);
                                cmd.Parameters.AddWithValue("@PayablePurchaseEquity", _TemplateFundAccountingSetup.PayablePurchaseEquity);
                                cmd.Parameters.AddWithValue("@PayablePurRecBond", _TemplateFundAccountingSetup.PayablePurRecBond);
                                cmd.Parameters.AddWithValue("@PayableManagementFee", _TemplateFundAccountingSetup.PayableManagementFee);
                                cmd.Parameters.AddWithValue("@PayableCustodianFee", _TemplateFundAccountingSetup.PayableCustodianFee);
                                cmd.Parameters.AddWithValue("@PayableAuditFee", _TemplateFundAccountingSetup.PayableAuditFee);
                                cmd.Parameters.AddWithValue("@BrokerCommission", _TemplateFundAccountingSetup.BrokerCommission);
                                cmd.Parameters.AddWithValue("@BrokerLevy", _TemplateFundAccountingSetup.BrokerLevy);
                                cmd.Parameters.AddWithValue("@BrokerVat", _TemplateFundAccountingSetup.BrokerVat);
                                cmd.Parameters.AddWithValue("@BrokerSalesTax", _TemplateFundAccountingSetup.BrokerSalesTax);
                                cmd.Parameters.AddWithValue("@WithHoldingTaxPPH23", _TemplateFundAccountingSetup.WithHoldingTaxPPH23);
                                cmd.Parameters.AddWithValue("@WHTTaxPayableAccrInterestBond", _TemplateFundAccountingSetup.WHTTaxPayableAccrInterestBond);
                                cmd.Parameters.AddWithValue("@ManagementFeeExpense", _TemplateFundAccountingSetup.ManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@CustodianFeeExpense", _TemplateFundAccountingSetup.CustodianFeeExpense);
                                cmd.Parameters.AddWithValue("@AuditFeeExpense", _TemplateFundAccountingSetup.AuditFeeExpense);
                                cmd.Parameters.AddWithValue("@BankCharges", _TemplateFundAccountingSetup.BankCharges);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeBond", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeBond);
                                cmd.Parameters.AddWithValue("@TaxExpenseInterestIncomeTimeDeposit", _TemplateFundAccountingSetup.TaxExpenseInterestIncomeTimeDeposit);
                                cmd.Parameters.AddWithValue("@RealisedEquity", _TemplateFundAccountingSetup.RealisedEquity);
                                cmd.Parameters.AddWithValue("@UnrealisedBond", _TemplateFundAccountingSetup.UnrealisedBond);
                                cmd.Parameters.AddWithValue("@UnrealisedEquity", _TemplateFundAccountingSetup.UnrealisedEquity);
                                cmd.Parameters.AddWithValue("@DistributedIncomeAcc", _TemplateFundAccountingSetup.DistributedIncomeAcc);
                                cmd.Parameters.AddWithValue("@DistributedIncomePayableAcc", _TemplateFundAccountingSetup.DistributedIncomePayableAcc);
                                cmd.Parameters.AddWithValue("@PendingSubscription", _TemplateFundAccountingSetup.PendingSubscription);
                                cmd.Parameters.AddWithValue("@PendingRedemption", _TemplateFundAccountingSetup.PendingRedemption);
                                cmd.Parameters.AddWithValue("@RealisedBond", _TemplateFundAccountingSetup.RealisedBond);
                                cmd.Parameters.AddWithValue("@TaxCapitalGainBond", _TemplateFundAccountingSetup.TaxCapitalGainBond);
                                cmd.Parameters.AddWithValue("@PayableSInvestFee", _TemplateFundAccountingSetup.PayableSInvestFee);
                                cmd.Parameters.AddWithValue("@SInvestFee", _TemplateFundAccountingSetup.SInvestFee);

                                cmd.Parameters.AddWithValue("@TaxPercentageDividend", _TemplateFundAccountingSetup.TaxPercentageDividend);
                                cmd.Parameters.AddWithValue("@TaxPercentageBond", _TemplateFundAccountingSetup.TaxPercentageBond);
                                cmd.Parameters.AddWithValue("@TaxPercentageTD", _TemplateFundAccountingSetup.TaxPercentageTD);
                                cmd.Parameters.AddWithValue("@TaxPercentageCapitalGain", _TemplateFundAccountingSetup.TaxPercentageCapitalGain);
                                cmd.Parameters.AddWithValue("@BondAmortization", _TemplateFundAccountingSetup.BondAmortization);
                                cmd.Parameters.AddWithValue("@PayableMovementFee", _TemplateFundAccountingSetup.PayableMovementFee);
                                cmd.Parameters.AddWithValue("@MovementFeeExpense", _TemplateFundAccountingSetup.MovementFeeExpense);

                                cmd.Parameters.AddWithValue("@PayablePurchaseMutualFund", _TemplateFundAccountingSetup.PayablePurchaseMutualFund);
                                cmd.Parameters.AddWithValue("@InvestmentMutualFund", _TemplateFundAccountingSetup.InvestmentMutualFund);
                                cmd.Parameters.AddWithValue("@AccountReceivableSaleMutualFund", _TemplateFundAccountingSetup.AccountReceivableSaleMutualFund);
                                cmd.Parameters.AddWithValue("@RevaluationMutualFund", _TemplateFundAccountingSetup.RevaluationMutualFund);
                                cmd.Parameters.AddWithValue("@UnrealisedMutualFund", _TemplateFundAccountingSetup.UnrealisedMutualFund);
                                cmd.Parameters.AddWithValue("@RealisedMutualFund", _TemplateFundAccountingSetup.RealisedMutualFund);

                                cmd.Parameters.AddWithValue("@PayableOtherFeeOne", _TemplateFundAccountingSetup.PayableOtherFeeOne);
                                cmd.Parameters.AddWithValue("@PayableOtherFeeTwo", _TemplateFundAccountingSetup.PayableOtherFeeTwo);
                                cmd.Parameters.AddWithValue("@OtherFeeOneExpense", _TemplateFundAccountingSetup.OtherFeeOneExpense);
                                cmd.Parameters.AddWithValue("@OtherFeeTwoExpense", _TemplateFundAccountingSetup.OtherFeeTwoExpense);

                                cmd.Parameters.AddWithValue("@PendingSwitching", _TemplateFundAccountingSetup.PendingSwitching);
                                cmd.Parameters.AddWithValue("@CurrentYearAccount", _TemplateFundAccountingSetup.CurrentYearAccount);
                                cmd.Parameters.AddWithValue("@PriorYearAccount", _TemplateFundAccountingSetup.PriorYearAccount);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TemplateFundAccountingSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TemplateFundAccountingSetup set status = 4, Notes=@Notes, " +
                                " lastupdate=@lastupdate where TemplateFundAccountingSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _TemplateFundAccountingSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TemplateFundAccountingSetup.HistoryPK);
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

        public void TemplateFundAccountingSetup_Approved(TemplateFundAccountingSetup _TemplateFundAccountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TemplateFundAccountingSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where TemplateFundAccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TemplateFundAccountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _TemplateFundAccountingSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TemplateFundAccountingSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TemplateFundAccountingSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateFundAccountingSetup.ApprovedUsersID);
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

        public void TemplateFundAccountingSetup_Reject(TemplateFundAccountingSetup _TemplateFundAccountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TemplateFundAccountingSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where TemplateFundAccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TemplateFundAccountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateFundAccountingSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TemplateFundAccountingSetup set status= 2,lastupdate=@lastupdate where TemplateFundAccountingSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
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

        public void TemplateFundAccountingSetup_Void(TemplateFundAccountingSetup _TemplateFundAccountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TemplateFundAccountingSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where TemplateFundAccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TemplateFundAccountingSetup.TemplateFundAccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TemplateFundAccountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TemplateFundAccountingSetup.VoidUsersID);
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

        public bool Get_CheckAlreadyHasExist()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from templatefundaccountingsetup where templatefundaccountingsetupPK !=0  and status = 2";
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;

                            }
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