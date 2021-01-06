using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundAccountingSetup
    {
        public int FundAccountingSetupPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int Subscription { get; set; }
        public string SubscriptionID { get; set; }
        public string SubscriptionDesc { get; set; }
        public int PayableSubscriptionFee { get; set; }
        public string PayableSubscriptionFeeID { get; set; }
        public string PayableSubscriptionFeeDesc { get; set; }
        public int PayableRedemptionFee { get; set; }
        public string PayableRedemptionFeeID { get; set; }
        public string PayableRedemptionFeeDesc { get; set; }
        public int Redemption { get; set; }
        public string RedemptionID { get; set; }
        public string RedemptionDesc { get; set; }

        public int PayableSInvestFee { get; set; }
        public string PayableSInvestFeeID { get; set; }
        public string PayableSInvestFeeDesc { get; set; }

        public int SInvestFee { get; set; }
        public string SInvestFeeID { get; set; }
        public string SInvestFeeDesc { get; set; }
        public int PayableSwitchingFee { get; set; }
        public string PayableSwitchingFeeID { get; set; }
        public string PayableSwitchingFeeDesc { get; set; }
        public int InvestmentEquity { get; set; }
        public string InvestmentEquityID { get; set; }
        public string InvestmentEquityDesc { get; set; }
        public int InvestmentBond { get; set; }
        public string InvestmentBondID { get; set; }
        public string InvestmentBondDesc { get; set; }
        public int InvestmentTimeDeposit { get; set; }
        public string InvestmentTimeDepositID { get; set; }
        public string InvestmentTimeDepositDesc { get; set; }
        public int InterestRecBond { get; set; }
        public string InterestRecBondID { get; set; }
        public string InterestRecBondDesc { get; set; }
        public int InterestAccrBond { get; set; }
        public string InterestAccrBondID { get; set; }
        public string InterestAccrBondDesc { get; set; }
        public int InterestAccrTimeDeposit { get; set; }
        public string InterestAccrTimeDepositID { get; set; }
        public string InterestAccrTimeDepositDesc { get; set; }
        public int InterestAccrGiro { get; set; }
        public string InterestAccrGiroID { get; set; }
        public string InterestAccrGiroDesc { get; set; }
        public int PrepaidTaxDividend { get; set; }
        public string PrepaidTaxDividendID { get; set; }
        public string PrepaidTaxDividendDesc { get; set; }
        public int AccountReceivableSaleBond { get; set; }
        public string AccountReceivableSaleBondID { get; set; }
        public string AccountReceivableSaleBondDesc { get; set; }
        public int AccountReceivableSaleEquity { get; set; }
        public string AccountReceivableSaleEquityID { get; set; }
        public string AccountReceivableSaleEquityDesc { get; set; }
        public int AccountReceivableSaleTimeDeposit { get; set; }
        public string AccountReceivableSaleTimeDepositID { get; set; }
        public string AccountReceivableSaleTimeDepositDesc { get; set; }
        public int IncomeInterestBond { get; set; }
        public string IncomeInterestBondID { get; set; }
        public string IncomeInterestBondDesc { get; set; }
        public int IncomeInterestTimeDeposit { get; set; }
        public string IncomeInterestTimeDepositID { get; set; }
        public string IncomeInterestTimeDepositDesc { get; set; }
        public int IncomeInterestGiro { get; set; }
        public string IncomeInterestGiroID { get; set; }
        public string IncomeInterestGiroDesc { get; set; }
        public int IncomeDividend { get; set; }
        public string IncomeDividendID { get; set; }
        public string IncomeDividendDesc { get; set; }
        public int ARDividend { get; set; }
        public string ARDividendID { get; set; }
        public string ARDividendDesc { get; set; }
        public int RevaluationBond { get; set; }
        public string RevaluationBondID { get; set; }
        public string RevaluationBondDesc { get; set; }
        public int RevaluationEquity { get; set; }
        public string RevaluationEquityID { get; set; }
        public string RevaluationEquityDesc { get; set; }
        public int PayablePurchaseEquity { get; set; }
        public string PayablePurchaseEquityID { get; set; }
        public string PayablePurchaseEquityDesc { get; set; }
        public int PayablePurRecBond { get; set; }
        public string PayablePurRecBondID { get; set; }
        public string PayablePurRecBondDesc { get; set; }
        public int PayableManagementFee { get; set; }
        public string PayableManagementFeeID { get; set; }
        public string PayableManagementFeeDesc { get; set; }
        public int PayableCustodianFee { get; set; }
        public string PayableCustodianFeeID { get; set; }
        public string PayableCustodianFeeDesc { get; set; }
        public int PayableAuditFee { get; set; }
        public string PayableAuditFeeID { get; set; }
        public string PayableAuditFeeDesc { get; set; }
        public int BrokerCommission { get; set; }
        public string BrokerCommissionID { get; set; }
        public string BrokerCommissionDesc { get; set; }
        public int BrokerLevy { get; set; }
        public string BrokerLevyID { get; set; }
        public string BrokerLevyDesc { get; set; }
        public int BrokerVat { get; set; }
        public string BrokerVatID { get; set; }
        public string BrokerVatDesc { get; set; }
        public int BrokerSalesTax { get; set; }
        public string BrokerSalesTaxID { get; set; }
        public string BrokerSalesTaxDesc { get; set; }
        public int WithHoldingTaxPPH23 { get; set; }
        public string WithHoldingTaxPPH23ID { get; set; }
        public string WithHoldingTaxPPH23Desc { get; set; }
        public int WHTTaxPayableAccrInterestBond { get; set; }
        public string WHTTaxPayableAccrInterestBondID { get; set; }
        public string WHTTaxPayableAccrInterestBondDesc { get; set; }
        public int ManagementFeeExpense { get; set; }
        public string ManagementFeeExpenseID { get; set; }
        public string ManagementFeeExpenseDesc { get; set; }
        public int CustodianFeeExpense { get; set; }
        public string CustodianFeeExpenseID { get; set; }
        public string CustodianFeeExpenseDesc { get; set; }
        public int AuditFeeExpense { get; set; }
        public string AuditFeeExpenseID { get; set; }
        public string AuditFeeExpenseDesc { get; set; }
        public int BankCharges { get; set; }
        public string BankChargesID { get; set; }
        public string BankChargesDesc { get; set; }
        public int TaxExpenseInterestIncomeBond { get; set; }
        public string TaxExpenseInterestIncomeBondID { get; set; }
        public string TaxExpenseInterestIncomeBondDesc { get; set; }
        public int TaxExpenseInterestIncomeTimeDeposit { get; set; }
        public string TaxExpenseInterestIncomeTimeDepositID { get; set; }
        public string TaxExpenseInterestIncomeTimeDepositDesc { get; set; }
        public int RealisedEquity { get; set; }
        public string RealisedEquityID { get; set; }
        public string RealisedEquityDesc { get; set; }
        public int RealisedBond { get; set; }
        public string RealisedBondID { get; set; }
        public string RealisedBondDesc { get; set; }
        public int UnrealisedBond { get; set; }
        public string UnrealisedBondID { get; set; }
        public string UnrealisedBondDesc { get; set; }

        public int TaxCapitalGainBond { get; set; }
        public string TaxCapitalGainBondID { get; set; }
        public string TaxCapitalGainBondDesc { get; set; }
        public int UnrealisedEquity { get; set; }
        public string UnrealisedEquityID { get; set; }
        public string UnrealisedEquityDesc { get; set; }
        public int DistributedIncomeAcc { get; set; }
        public string DistributedIncomeAccID { get; set; }
        public string DistributedIncomeAccDesc { get; set; }
        public int DistributedIncomePayableAcc { get; set; }
        public string DistributedIncomePayableAccID { get; set; }
        public string DistributedIncomePayableAccDesc { get; set; }
        public int PendingSubscription { get; set; }
        public string PendingSubscriptionID { get; set; }
        public string PendingSubscriptionDesc { get; set; }
        public int PendingRedemption { get; set; }
        public string PendingRedemptionID { get; set; }
        public string PendingRedemptionDesc { get; set; }

        public int BondAmortization { get; set; }
        public string BondAmortizationID { get; set; }
        public string BondAmortizationDesc { get; set; }

        public decimal TaxPercentageDividend { get; set; }
        public decimal TaxPercentageBond { get; set; }
        public decimal TaxPercentageTD { get; set; }
        public decimal TaxProvisionPercent { get; set; }

        public int PayablePurchaseMutualFund { get; set; }
        public string PayablePurchaseMutualFundID { get; set; }
        public string PayablePurchaseMutualFundDesc { get; set; }

        public int InvestmentMutualFund { get; set; }
        public string InvestmentMutualFundID { get; set; }
        public string InvestmentMutualFundDesc { get; set; }

        public int AccountReceivableSaleMutualFund { get; set; }
        public string AccountReceivableSaleMutualFundID { get; set; }
        public string AccountReceivableSaleMutualFundDesc { get; set; }

        public int RevaluationMutualFund { get; set; }
        public string RevaluationMutualFundID { get; set; }
        public string RevaluationMutualFundDesc { get; set; }

        public int UnrealisedMutualFund { get; set; }
        public string UnrealisedMutualFundID { get; set; }
        public string UnrealisedMutualFundDesc { get; set; }

        public int RealisedMutualFund { get; set; }
        public string RealisedMutualFundID { get; set; }
        public string RealisedMutualFundDesc { get; set; }

        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string UpdateUsersID { get; set; }
        public string UpdateTime { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
        public int PayableMovementFee { get; set; }
        public string PayableMovementFeeID { get; set; }
        public string PayableMovementFeeDesc { get; set; }
        public int MovementFeeExpense { get; set; }
        public string MovementFeeExpenseID { get; set; }
        public string MovementFeeExpenseDesc { get; set; }

        public int PayableOtherFeeOne { get; set; }
        public string PayableOtherFeeOneID { get; set; }
        public string PayableOtherFeeOneDesc { get; set; }

        public int PayableOtherFeeTwo { get; set; }
        public string PayableOtherFeeTwoID { get; set; }
        public string PayableOtherFeeTwoDesc { get; set; }

        public int OtherFeeOneExpense { get; set; }
        public string OtherFeeOneExpenseID { get; set; }
        public string OtherFeeOneExpenseDesc { get; set; }

        public int OtherFeeTwoExpense { get; set; }
        public string OtherFeeTwoExpenseID { get; set; }
        public string OtherFeeTwoExpenseDesc { get; set; }

        public int PendingSwitching { get; set; }
        public string PendingSwitchingID { get; set; }
        public string PendingSwitchingDesc { get; set; }

        public int CurrentYearAccount { get; set; }
        public string CurrentYearAccountID { get; set; }
        public string CurrentYearAccountDesc { get; set; }

        public int PriorYearAccount { get; set; }
        public string PriorYearAccountID { get; set; }
        public string PriorYearAccountDesc { get; set; }


        //TAMBAHAN 
        public int TaxPercentageCapitalGain { get; set; }
        public int Switching { get; set; }
        public int InterestAccruedCurrentAccount { get; set; }
        public int IncomeCurrentAccount { get; set; }
        public int InvInTDUSD { get; set; }


        public int InvestmentSukuk { get; set; }
        public int InterestRecSukuk { get; set; }
        public int PayablePurRecSukuk { get; set; }
        public int WHTTaxPayableAccrInterestSukuk { get; set; }
        public int InterestAccrSukuk { get; set; }

        public int RealisedSukuk { get; set; }
        public int AccountReceivableSaleSukuk { get; set; }
        public int TaxCapitalGainSukuk { get; set; }
        public int InvestmentProtectedFund { get; set; }
        public int PayablePurchaseProtectedFund { get; set; }
        public int InvestmentPrivateEquityFund { get; set; }
        public int PayablePurchasePrivateEquityFund { get; set; }
        public int AccountReceivableSaleProtectedFund { get; set; }
        public int AccountReceivableSalePrivateEquityFund { get; set; }


        public int RevaluationSukuk { get; set; }

        public int UnrealisedSukuk { get; set; }
        public int CashForMutualFund { get; set; }
        public int RevaluationProtectedFund { get; set; }
        public int RevaluationPrivateEquityFund { get; set; }
        public int UnrealisedProtectedFund { get; set; }
        public int UnrealisedPrivateEquityFund { get; set; }

        public int InterestReceivableSellSukuk { get; set; }
        public int InterestRecSellBond { get; set; }
        public int InterestReceivableBuySukuk { get; set; }
        public int InterestReceivableSellMutualFund { get; set; }
        public int InterestReceivableSellProtectedFund { get; set; }
        public int InterestReceivableSellPrivateEquityFund { get; set; }

        public int IncomeInterestSukuk { get; set; }
        public int InterestAccrMutualFund { get; set; }
        public int InterestAccrProtectedFund { get; set; }
        public int InterestAccrPrivateEquityFund { get; set; }
        public int IncomeInterestAccrMutualFund { get; set; }
        public int IncomeInterestAccrProtectedFund { get; set; }
        public int IncomeInterestAccrPrivateEquityFund { get; set; }
        public int APManagementFee { get; set; }

        public int InvestmentRights { get; set; }
        public int PayablePurchaseRights { get; set; }
        public int AccountReceivableSaleRights { get; set; }
        public int RealisedRights { get; set; }
        public int RevaluationRights { get; set; }
        public int UnrealisedRights { get; set; }
        public int CashForIPO { get; set; }


        //TAMBAHAN ID
        public string TaxPercentageCapitalGainID { get; set; }
        public string SwitchingID { get; set; }
        public string InterestAccruedCurrentAccountID { get; set; }
        public string IncomeCurrentAccountID { get; set; }
        public string InvInTDUSDID { get; set; }

        public string InvestmentSukukID { get; set; }
        public string InterestRecSukukID { get; set; }
        public string PayablePurRecSukukID { get; set; }
        public string WHTTaxPayableAccrInterestSukukID { get; set; }
        public string InterestAccrSukukID { get; set; }

        public string RealisedSukukID { get; set; }
        public string AccountReceivableSaleSukukID { get; set; }
        public string TaxCapitalGainSukukID { get; set; }
        public string InvestmentProtectedFundID { get; set; }
        public string PayablePurchaseProtectedFundID { get; set; }
        public string InvestmentPrivateEquityFundID { get; set; }
        public string PayablePurchasePrivateEquityFundID { get; set; }
        public string AccountReceivableSaleProtectedFundID { get; set; }
        public string AccountReceivableSalePrivateEquityFundID { get; set; }


        public string RevaluationSukukID { get; set; }
        public string UnrealisedSukukID { get; set; }
        public string CashForMutualFundID { get; set; }
        public string RevaluationProtectedFundID { get; set; }
        public string RevaluationPrivateEquityFundID { get; set; }
        public string UnrealisedProtectedFundID { get; set; }
        public string UnrealisedPrivateEquityFundID { get; set; }
        public string InterestReceivableSellSukukID { get; set; }
        public string InterestRecSellBondID { get; set; }
        public string InterestReceivableBuySukukID { get; set; }
        public string InterestReceivableSellMutualFundID { get; set; }
        public string InterestReceivableSellProtectedFundID { get; set; }
        public string InterestReceivableSellPrivateEquityFundID { get; set; }

        public string IncomeInterestSukukID { get; set; }
        public string InterestAccrMutualFundID { get; set; }
        public string InterestAccrProtectedFundID { get; set; }
        public string InterestAccrPrivateEquityFundID { get; set; }
        public string IncomeInterestAccrMutualFundID { get; set; }
        public string IncomeInterestAccrProtectedFundID { get; set; }
        public string IncomeInterestAccrPrivateEquityFundID { get; set; }
        public string APManagementFeeID { get; set; }

        public string InvestmentRightsID { get; set; }
        public string PayablePurchaseRightsID { get; set; }
        public string AccountReceivableSaleRightsID { get; set; }
        public string RealisedRightsID { get; set; }
        public string RevaluationRightsID { get; set; }
        public string UnrealisedRightsID { get; set; }
        public string CashForIPOID { get; set; }


        //TAMBAHAN Desc
        public string TaxPercentageCapitalGainDesc { get; set; }
        public string SwitchingDesc { get; set; }
        public string InterestAccruedCurrentAccountDesc { get; set; }
        public string IncomeCurrentAccountDesc { get; set; }
        public string InvInTDUSDDesc { get; set; }

        public string InvestmentSukukDesc { get; set; }
        public string InterestRecSukukDesc { get; set; }
        public string PayablePurRecSukukDesc { get; set; }
        public string WHTTaxPayableAccrInterestSukukDesc { get; set; }
        public string InterestAccrSukukDesc { get; set; }

        public string RealisedSukukDesc { get; set; }
        public string AccountReceivableSaleSukukDesc { get; set; }
        public string TaxCapitalGainSukukDesc { get; set; }
        public string InvestmentProtectedFundDesc { get; set; }
        public string PayablePurchaseProtectedFundDesc { get; set; }
        public string InvestmentPrivateEquityFundDesc { get; set; }
        public string PayablePurchasePrivateEquityFundDesc { get; set; }
        public string AccountReceivableSaleProtectedFundDesc { get; set; }
        public string AccountReceivableSalePrivateEquityFundDesc { get; set; }

        public string RevaluationSukukDesc { get; set; }
        public string UnrealisedSukukDesc { get; set; }
        public string CashForMutualFundDesc { get; set; }
        public string RevaluationProtectedFundDesc { get; set; }
        public string RevaluationPrivateEquityFundDesc { get; set; }
        public string UnrealisedProtectedFundDesc { get; set; }
        public string UnrealisedPrivateEquityFundDesc { get; set; }
        public string InterestReceivableSellSukukDesc { get; set; }
        public string InterestRecSellBondDesc { get; set; }
        public string InterestReceivableBuySukukDesc { get; set; }
        public string InterestReceivableSellMutualFundDesc { get; set; }
        public string InterestReceivableSellProtectedFundDesc { get; set; }
        public string InterestReceivableSellPrivateEquityFundDesc { get; set; }

        public string IncomeInterestSukukDesc { get; set; }
        public string InterestAccrMutualFundDesc { get; set; }
        public string InterestAccrProtectedFundDesc { get; set; }
        public string InterestAccrPrivateEquityFundDesc { get; set; }
        public string IncomeInterestAccrMutualFundDesc { get; set; }
        public string IncomeInterestAccrProtectedFundDesc { get; set; }
        public string IncomeInterestAccrPrivateEquityFundDesc { get; set; }
        public string APManagementFeeDesc { get; set; }
        public int PayableOtherFeeThree { get; set; }
        public string PayableOtherFeeThreeID { get; set; }
        public string PayableOtherFeeThreeDesc { get; set; }

        public int OtherFeeThreeExpense { get; set; }
        public string OtherFeeThreeExpenseID { get; set; }
        public string OtherFeeThreeExpenseDesc { get; set; }

        public int TaxInterestBond { get; set; }
        public string InvestmentRightsDesc { get; set; }
        public string PayablePurchaseRightsDesc { get; set; }
        public string AccountReceivableSaleRightsDesc { get; set; }
        public string RealisedRightsDesc { get; set; }
        public string RevaluationRightsDesc { get; set; }
        public string UnrealisedRightsDesc { get; set; }

        public string CashForIPODesc { get; set; }
        public bool BitAccruedInterestGiroDaily { get; set; }
        public int AveragePriority { get; set; }
        public int AveragePriorityBond { get; set; }
        public string AveragePriorityDesc { get; set; }

        public int TaxProvision { get; set; }
        public string TaxProvisionID { get; set; }
        public string TaxProvisionDesc { get; set; }

        public int PayableTaxProvision { get; set; }
        public string PayableTaxProvisionID { get; set; }
        public string PayableTaxProvisionDesc { get; set; }

        public int InvestmentWarrant { get; set; }
        public string InvestmentWarrantID { get; set; }
        public string InvestmentWarrantDesc { get; set; }

        public int PayablePurchaseWarrant { get; set; }
        public string PayablePurchaseWarrantID { get; set; }
        public string PayablePurchaseWarrantDesc { get; set; }

        public int AccountReceivableSaleWarrant { get; set; }
        public string AccountReceivableSaleWarrantID { get; set; }
        public string AccountReceivableSaleWarrantDesc { get; set; }

        public int RealisedWarrant { get; set; }
        public string RealisedWarrantID { get; set; }
        public string RealisedWarrantDesc { get; set; }

        public int RevaluationWarrant { get; set; }
        public string RevaluationWarrantID { get; set; }
        public string RevaluationWarrantDesc { get; set; }

        public int UnrealisedWarrant { get; set; }
        public string UnrealisedWarrantID { get; set; }
        public string UnrealisedWarrantDesc { get; set; }

        public int CBESTExpense { get; set; }
        public string CBESTExpenseID { get; set; }
        public string CBESTExpenseDesc { get; set; }

        public int PayableCBEST { get; set; }
        public string PayableCBESTID { get; set; }
        public string PayableCBESTDesc { get; set; }
        public int TaxPercentageCapitalGainSell { get; set; }

    }

    public class ParamFundAccountingSetup
    {
        public int ParamFundFrom { get; set; }
        public int ParamFundTo { get; set; }
        public bool BitDefaultFund { get; set; }
        public string EntryUsersID { get; set; }

    }
}