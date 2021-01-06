using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class AccountingSetup
    {

        public int AccountingSetupPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public int Income { get; set; }
        public string IncomeID { get; set; }
        public string IncomeDesc { get; set; }
        public int Expense { get; set; }
        public string ExpenseID { get; set; }
        public string ExpenseDesc { get; set; }
        public int PPHFinal { get; set; }
        public string PPHFinalID { get; set; }
        public string PPHFinalDesc { get; set; }
        public int InvInBond { get; set; }
        public string InvInBondID { get; set; }
        public string InvInBondDesc { get; set; }
        public int InvInBondGovIDR { get; set; }
        public string InvInBondGovIDRID { get; set; }
        public string InvInBondGovIDRDesc { get; set; }
        public int InvInBondGovUSD { get; set; }
        public string InvInBondGovUSDID { get; set; }
        public string InvInBondGovUSDDesc { get; set; }
        public int InvInBondCorpIDR { get; set; }
        public string InvInBondCorpIDRID { get; set; }
        public string InvInBondCorpIDRDesc { get; set; }
        public int InvInBondCorpUSD { get; set; }
        public string InvInBondCorpUSDID { get; set; }
        public string InvInBondCorpUSDDesc { get; set; }
        public int InvInEquity { get; set; }
        public string InvInEquityID { get; set; }
        public string InvInEquityDesc { get; set; }
        public int InvInTD { get; set; }

        public string InvInTDID { get; set; }
        public string InvInTDDesc { get; set; }
        public int InvInReksadana { get; set; }
        public string InvInReksadanaID { get; set; }
        public string InvInReksadanaDesc { get; set; }
        public int APPurchaseBond { get; set; }
        public string APPurchaseBondID { get; set; }
        public string APPurchaseBondDesc { get; set; }
        public int APPurchaseEquity { get; set; }
        public string APPurchaseEquityID { get; set; }
        public string APPurchaseEquityDesc { get; set; }
        public int APPurchaseTD { get; set; }
        public string APPurchaseTDID { get; set; }
        public string APPurchaseTDDesc { get; set; }
        public int ARSellBond { get; set; }
        public string ARSellBondID { get; set; }
        public string ARSellBondDesc { get; set; }
        public int ARSellEquity { get; set; }
        public string ARSellEquityID { get; set; }
        public string ARSellEquityDesc { get; set; }
        public int ARSellTD { get; set; }
        public string ARSellTDID { get; set; }
        public string ARSellTDDesc { get; set; }
        public int InterestPurchaseBond { get; set; }
        public string InterestPurchaseBondID { get; set; }
        public string InterestPurchaseBondDesc { get; set; }
        public int InterestReceivableBond { get; set; }
        public string InterestReceivableBondID { get; set; }
        public string InterestReceivableBondDesc { get; set; }
        public int InterestReceivableBondGovIDR { get; set; }
        public string InterestReceivableBondGovIDRID { get; set; }
        public string InterestReceivableBondGovIDRDesc { get; set; }
        public int InterestReceivableBondCorpIDR { get; set; }
        public string InterestReceivableBondCorpIDRID { get; set; }
        public string InterestReceivableBondCorpIDRDesc { get; set; }
        public int InterestReceivableBondGovUSD { get; set; }
        public string InterestReceivableBondGovUSDID { get; set; }
        public string InterestReceivableBondGovUSDDesc { get; set; }
        public int InterestReceivableBondCorpUSD { get; set; }
        public string InterestReceivableBondCorpUSDID { get; set; }
        public string InterestReceivableBondCorpUSDDesc { get; set; }
        public int InterestReceivableTD { get; set; }
        public string InterestReceivableTDID { get; set; }
        public string InterestReceivableTDDesc { get; set; }
        public int CashBond { get; set; }
        public string CashBondID { get; set; }
        public string CashBondDesc { get; set; }
        public int CashEquity { get; set; }
        public string CashEquityID { get; set; }
        public string CashEquityDesc { get; set; }
        public int CashTD { get; set; }
        public string CashTDID { get; set; }
        public string CashTDDesc { get; set; }
        public int ForeignExchangeRevalAccount { get; set; }
        public string ForeignExchangeRevalAccountID { get; set; }
        public string ForeignExchangeRevalAccountDesc { get; set; }
        public int UnrealisedEquity { get; set; }
        public string UnrealisedEquityID { get; set; }
        public string UnrealisedEquityDesc { get; set; }
        public int UnrealisedBond { get; set; }
        public string UnrealisedBondID { get; set; }
        public string UnrealisedBondDesc { get; set; }
        public int UnrealisedReksadana { get; set; }
        public string UnrealisedReksadanaID { get; set; }
        public string UnrealisedReksadanaDesc { get; set; }
        public int RealisedEquity { get; set; }
        public string RealisedEquityID { get; set; }
        public string RealisedEquityDesc { get; set; }
        public int RealisedBondGovIDR { get; set; }
        public string RealisedBondGovIDRID { get; set; }
        public string RealisedBondGovIDRDesc { get; set; }
        public int RealisedBondGovUSD { get; set; }
        public string RealisedBondGovUSDID { get; set; }
        public string RealisedBondGovUSDDesc { get; set; }
        public int RealisedBondCorpIDR { get; set; }
        public string RealisedBondCorpIDRID { get; set; }
        public string RealisedBondCorpIDRDesc { get; set; }
        public int RealisedBondCorpUSD { get; set; }
        public string RealisedBondCorpUSDID { get; set; }
        public string RealisedBondCorpUSDDesc { get; set; }
        public int RealisedBond { get; set; }
        public string RealisedBondID { get; set; }
        public string RealisedBondDesc { get; set; }
        public int RealisedReksadana { get; set; }
        public string RealisedReksadanaID { get; set; }
        public string RealisedReksadanaDesc { get; set; }
        public int RealisedAset { get; set; }
        public string RealisedAsetID { get; set; }
        public string RealisedAsetDesc { get; set; }
        public int CadanganEquity { get; set; }
        public string CadanganEquityID { get; set; }
        public string CadanganEquityDesc { get; set; }
        public int CadanganBond { get; set; }
        public string CadanganBondID { get; set; }
        public string CadanganBondDesc { get; set; }
        public int CadanganReksadana { get; set; }
        public string CadanganReksadanaID { get; set; }
        public string CadanganReksadanaDesc { get; set; }
        public int BrokerageFee { get; set; }
        public string BrokerageFeeID { get; set; }
        public string BrokerageFeeDesc { get; set; }
        public int JSXLevyFee { get; set; }
        public string JSXLevyFeeID { get; set; }
        public string JSXLevyFeeDesc { get; set; }
        public int KPEIFee { get; set; }
        public string KPEIFeeID { get; set; }
        public string KPEIFeeDesc { get; set; }
        public int VATFee { get; set; }
        public string VATFeeID { get; set; }
        public string VATFeeDesc { get; set; }
        public int SalesTax { get; set; }
        public string SalesTaxID { get; set; }
        public string SalesTaxDesc { get; set; }
        public int IncomeTaxArt23 { get; set; }
        public string IncomeTaxArt23ID { get; set; }
        public string IncomeTaxArt23Desc { get; set; }
        public int EquitySellMethod { get; set; }
        public string EquitySellMethodDesc { get; set; }
        public int AveragePriority { get; set; }
        public string AveragePriorityDesc { get; set; }
        public int FixedAsetBuyPPN { get; set; }
        public string FixedAsetBuyPPNID { get; set; }
        public string FixedAsetBuyPPNDesc { get; set; }
        public int FixedAsetSellPPN { get; set; }
        public string FixedAsetSellPPNID { get; set; }
        public string FixedAsetSellPPNDesc { get; set; }
        public int DefaultCurrencyPK { get; set; }
        public string DefaultCurrencyID { get; set; }
        public int DecimalPlaces { get; set; }

        //------------------
        public int WHTFee { get; set; }
        public string WHTFeeID { get; set; }
        public string WHTFeeDesc { get; set; }
        public int ARManagementFee { get; set; }
        public string ARManagementFeeID { get; set; }
        public string ARManagementFeeDesc { get; set; }
        public int ManagementFeeExpense { get; set; }
        public string ManagementFeeExpenseID { get; set; }
        public string ManagementFeeExpenseDesc { get; set; }
        public int TaxARManagementFee { get; set; }
        public string TaxARManagementFeeID { get; set; }
        public string TaxARManagementFeeDesc { get; set; }
        public int TaxManagementFeeExpense { get; set; }
        public string TaxManagementFeeExpenseID { get; set; }
        public string TaxManagementFeeExpenseDesc { get; set; }
        public int MKBD06Bank { get; set; }
        public string MKBD06BankID { get; set; }
        public string MKBD06BankDesc { get; set; }
        public int MKBD06PettyCash { get; set; }
        public string MKBD06PettyCashID { get; set; }
        public string MKBD06PettyCashDesc { get; set; }
        public int APPurchaseReksadana { get; set; }
        public string APPurchaseReksadanaID { get; set; }
        public string APPurchaseReksadanaDesc { get; set; }
        public int ARSellReksadana { get; set; }
        public string ARSellReksadanaID { get; set; }
        public string ARSellReksadanaDesc { get; set; }

        public int TotalEquity { get; set; }
        public string TotalEquityID { get; set; }
        public string TotalEquityDesc { get; set; }

        public int UnrealisedBondUSD { get; set; }
        public string UnrealisedBondIDUSD { get; set; }
        public string UnrealisedBondDescUSD { get; set; }

        public int CadanganBondUSD { get; set; }
        public string CadanganBondIDUSD { get; set; }
        public string CadanganBondDescUSD { get; set; }

        public int UnrealizedAccountSGD { get; set; }
        public string UnrealizedAccountSGDID { get; set; }
        public string UnrealizedAccountSGDDesc { get; set; }

        public int UnrealizedAccountUSD { get; set; }
        public string UnrealizedAccountUSDID { get; set; }
        public string UnrealizedAccountUSDDesc { get; set; }

        //
        public int InvInTDUSD { get; set; }
        public string InvInTDUSDID { get; set; }
        public string InvInTDUSDDesc { get; set; }

        public int ARSubscriptionFee { get; set; }
        public string ARSubscriptionFeeID { get; set; }
        public string ARSubscriptionFeeDesc { get; set; }

        public int SubscriptionFeeIncome { get; set; }
        public string SubscriptionFeeIncomeID { get; set; }
        public string SubscriptionFeeIncomeDesc { get; set; }

        public int ARRedemptionFee { get; set; }
        public string ARRedemptionFeeID { get; set; }
        public string ARRedemptionFeeDesc { get; set; }

        public int RedemptionFeeIncome { get; set; }
        public string RedemptionFeeIncomeID { get; set; }
        public string RedemptionFeeIncomeDesc { get; set; }

        public int ARSwitchingFee { get; set; }
        public string ARSwitchingFeeID { get; set; }
        public string ARSwitchingFeeDesc { get; set; }

        public int SwitchingFeeIncome { get; set; }
        public string SwitchingFeeIncomeID { get; set; }
        public string SwitchingFeeIncomeDesc { get; set; }
        public int AgentCommissionExpense { get; set; }
        public string AgentCommissionExpenseID { get; set; }
        public string AgentCommissionExpenseDesc { get; set; }

        public int AgentCommissionPayable { get; set; }
        public string AgentCommissionPayableID { get; set; }
        public string AgentCommissionPayableDesc { get; set; }

        public int WHTPayablePPH21 { get; set; }
        public string WHTPayablePPH21ID { get; set; }
        public string WHTPayablePPH21Desc { get; set; }

        public int WHTPayablePPH23 { get; set; }
        public string WHTPayablePPH23ID { get; set; }
        public string WHTPayablePPH23Desc { get; set; }

        public int VatIn { get; set; }
        public string VatInID { get; set; }
        public string VatInDesc { get; set; }

        public int VatOut { get; set; }
        public string VatOutID { get; set; }
        public string VatOutDesc { get; set; }


        public int AgentCommissionCash { get; set; }
        public string AgentCommissionCashID { get; set; }
        public string AgentCommissionCashDesc { get; set; }


        public int SwitchingFundAcc { get; set; }

        public int AgentCSRExpense { get; set; }
        public string AgentCSRExpenseID { get; set; }
        public string AgentCSRExpenseDesc { get; set; }

        public int AgentCSRPayable { get; set; }
        public string AgentCSRPayableID { get; set; }
        public string AgentCSRPayableDesc { get; set; }

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

    }

}