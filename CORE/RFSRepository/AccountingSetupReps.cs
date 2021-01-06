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
    public class AccountingSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AccountingSetup] " +
                            "([AccountingSetupPK],[HistoryPK],[Status],[Income],[Expense],[PPHFinal],[InvInBond],[InvInBondGovIDR],[InvInBondGovUSD],[InvInBondCorpIDR],[InvInBondCorpUSD],[InvInEquity],[InvInTD],[InvInReksadana],[APPurchaseBond],[APPurchaseEquity],[APPurchaseTD],[APPurchaseReksadana],[ARSellBond]," +
                            "[ARSellEquity],[ARSellTD],[ARSellReksadana],[InterestPurchaseBond],[InterestReceivableBond],[InterestReceivableBondGovIDR],[InterestReceivableBondCorpIDR],[InterestReceivableBondGovUSD],[InterestReceivableBondCorpUSD],[InterestReceivableTD],[CashBond],[CashEquity],[CashTD],[ForeignExchangeRevalAccount], " +
                            "[UnrealisedEquity],[UnrealisedBond],[UnrealisedReksadana],[RealisedEquity],[RealisedBondGovIDR],[RealisedBondGovUSD],[RealisedBondCorpIDR],[RealisedBondCorpUSD],[RealisedBond],[RealisedReksadana],[RealisedAset],[CadanganEquity], " +
                            "[CadanganBond],[CadanganReksadana],[BrokerageFee],[JSXLevyFee],[KPEIFee],[SalesTax],[IncomeTaxArt23],[EquitySellMethod],[AveragePriority],[FixedAsetBuyPPN],[FixedAsetSellPPN],[DefaultCurrencyPK],[DecimalPlaces],[ARManagementFee],[ManagementFeeExpense],[MKBD06Bank],[MKBD06PettyCash],[WHTFee],[VATFee],[TaxARManagementFee],[TaxManagementFeeExpense],[TotalEquity],[UnrealisedBondUSD],[CadanganBondUSD],[UnrealizedAccountSGD],[UnrealizedAccountUSD],[InvInTDUSD],[ARSubscriptionFee],[SubscriptionFeeIncome],[ARRedemptionFee],[RedemptionFeeIncome],[ARSwitchingFee],[SwitchingFeeIncome]," +
                            "[AgentCommissionExpense],[AgentCommissionPayable],[WHTPayablePPH21],[WHTPayablePPH23],[VatIn],[VatOut],[AgentCommissionCash],[SwitchingFundAcc],[AgentCSRExpense],[AgentCSRPayable],";

        string _paramaterCommand = "@Income,@Expense,@PPHFinal,@InvInBond,@InvInBondGovIDR,@InvInBondGovUSD,@InvInBondCorpIDR,@InvInBondCorpUSD,@InvInEquity,@InvInTD,@InvInReksadana,@APPurchaseBond,@APPurchaseEquity,@APPurchaseTD,@APPurchaseReksadana,@ARSellBond," +
                            "@ARSellEquity,@ARSellTD,@ARSellReksadana,@InterestPurchaseBond,@InterestReceivableBond,@InterestReceivableBondGovIDR,@InterestReceivableBondCorpIDR,@InterestReceivableBondGovUSD,@InterestReceivableBondCorpUSD,@InterestReceivableTD,@CashBond,@CashEquity,@CashTD,@ForeignExchangeRevalAccount, " +
                            "@UnrealisedEquity,@UnrealisedBond,@UnrealisedReksadana,@RealisedEquity,@RealisedBondGovIDR,@RealisedBondGovUSD,@RealisedBondCorpIDR,@RealisedBondCorpUSD,@RealisedBond,@RealisedReksadana,@RealisedAset,@CadanganEquity, " +
                            "@CadanganBond,@CadanganReksadana,@BrokerageFee,@JSXLevyFee,@KPEIFee,@SalesTax,@IncomeTaxArt23,@EquitySellMethod,@AveragePriority,@FixedAsetBuyPPN,@FixedAsetSellPPN,@DefaultCurrencyPK,@DecimalPlaces,@ARManagementFee,@ManagementFeeExpense,@MKBD06Bank,@MKBD06PettyCash,@WHTFee,@VATFee,@TaxARManagementFee,@TaxManagementFeeExpense,@TotalEquity,@UnrealisedBondUSD,@CadanganBondUSD,@UnrealizedAccountSGD,@UnrealizedAccountUSD,@InvInTDUSD,@ARSubscriptionFee,@SubscriptionFeeIncome,@ARRedemptionFee,@RedemptionFeeIncome,@ARSwitchingFee,@SwitchingFeeIncome," +
                            "@AgentCommissionExpense,@AgentCommissionPayable,@WHTPayablePPH21,@WHTPayablePPH23,@VatIn,@VatOut,@AgentCommissionCash,@SwitchingFundAcc,@AgentCSRExpense,@AgentCSRPayable,";

        //2
        private AccountingSetup setAccountingSetup(SqlDataReader dr)
        {
            AccountingSetup M_AccountingSetup = new AccountingSetup();
            M_AccountingSetup.AccountingSetupPK = Convert.ToInt32(dr["AccountingSetupPK"]);
            M_AccountingSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AccountingSetup.Status = Convert.ToInt32(dr["Status"]);
            M_AccountingSetup.Notes = Convert.ToString(dr["Notes"]);

            M_AccountingSetup.Income = dr["Income"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Income"]);
            M_AccountingSetup.IncomeID = dr["IncomeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["IncomeID"]);
            M_AccountingSetup.IncomeDesc = dr["IncomeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["IncomeDesc"]);
            M_AccountingSetup.Expense = dr["Expense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Expense"]);
            M_AccountingSetup.ExpenseID = dr["ExpenseID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExpenseID"]);
            M_AccountingSetup.ExpenseDesc = dr["ExpenseDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExpenseDesc"]);
            M_AccountingSetup.PPHFinal = dr["PPHFinal"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PPHFinal"]);
            M_AccountingSetup.PPHFinalID = dr["PPHFinalID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PPHFinalID"]);
            M_AccountingSetup.PPHFinalDesc = dr["PPHFinalDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PPHFinalDesc"]);

            M_AccountingSetup.InvInBond = dr["InvInBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInBond"]);
            M_AccountingSetup.InvInBondID = dr["InvInBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondID"]);
            M_AccountingSetup.InvInBondDesc = dr["InvInBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondDesc"]);

            M_AccountingSetup.InvInBondGovIDR = dr["InvInBondGovIDR"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInBondGovIDR"]);
            M_AccountingSetup.InvInBondGovIDRID = dr["InvInBondGovIDRID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondGovIDRID"]);
            M_AccountingSetup.InvInBondGovIDRDesc = dr["InvInBondGovIDRDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondGovIDRDesc"]);
            M_AccountingSetup.InvInBondGovUSD = dr["InvInBondGovUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInBondGovUSD"]);
            M_AccountingSetup.InvInBondGovUSDID = dr["InvInBondGovUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondGovUSDID"]);
            M_AccountingSetup.InvInBondGovUSDDesc = dr["InvInBondGovUSDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondGovUSDDesc"]);
            M_AccountingSetup.InvInBondCorpIDR = dr["InvInBondCorpIDR"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInBondCorpIDR"]);
            M_AccountingSetup.InvInBondCorpIDRID = dr["InvInBondCorpIDRID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondCorpIDRID"]);
            M_AccountingSetup.InvInBondCorpIDRDesc = dr["InvInBondCorpIDRDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondCorpIDRDesc"]);
            M_AccountingSetup.InvInBondCorpUSD = dr["InvInBondCorpUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInBondCorpUSD"]);
            M_AccountingSetup.InvInBondCorpUSDID = dr["InvInBondCorpUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondCorpUSDID"]);
            M_AccountingSetup.InvInBondCorpUSDDesc = dr["InvInBondCorpUSDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInBondCorpUSDDesc"]);

            M_AccountingSetup.InvInEquity = dr["InvInEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInEquity"]);
            M_AccountingSetup.InvInEquityID = dr["InvInEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInEquityID"]);
            M_AccountingSetup.InvInEquityDesc = dr["InvInEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInEquityDesc"]);
            M_AccountingSetup.InvInTD = dr["InvInTD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInTD"]);
            M_AccountingSetup.InvInTDID = dr["InvInTDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInTDID"]);
            M_AccountingSetup.InvInTDDesc = dr["InvInTDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInTDDesc"]);

            M_AccountingSetup.InvInReksadana = dr["InvInReksadana"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInReksadana"]);
            M_AccountingSetup.InvInReksadanaID = dr["InvInReksadanaID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInReksadanaID"]);
            M_AccountingSetup.InvInReksadanaDesc = dr["InvInReksadanaDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInReksadanaDesc"]);

            M_AccountingSetup.APPurchaseBond = dr["APPurchaseBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["APPurchaseBond"]);
            M_AccountingSetup.APPurchaseBondID = dr["APPurchaseBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseBondID"]);
            M_AccountingSetup.APPurchaseBondDesc = dr["APPurchaseBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseBondDesc"]);
            M_AccountingSetup.APPurchaseEquity = dr["APPurchaseEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["APPurchaseEquity"]);
            M_AccountingSetup.APPurchaseEquityID = dr["APPurchaseEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseEquityID"]);
            M_AccountingSetup.APPurchaseEquityDesc = dr["APPurchaseEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseEquityDesc"]);
            M_AccountingSetup.APPurchaseTD = dr["APPurchaseTD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["APPurchaseTD"]);
            M_AccountingSetup.APPurchaseTDID = dr["APPurchaseTDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseTDID"]);
            M_AccountingSetup.APPurchaseTDDesc = dr["APPurchaseTDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseTDDesc"]);
            M_AccountingSetup.APPurchaseReksadana = dr["APPurchaseReksadana"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["APPurchaseReksadana"]);
            M_AccountingSetup.APPurchaseReksadanaID = dr["APPurchaseReksadanaID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseReksadanaID"]);
            M_AccountingSetup.APPurchaseReksadanaDesc = dr["APPurchaseReksadanaDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["APPurchaseReksadanaDesc"]);
            M_AccountingSetup.ARSellBond = dr["ARSellBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARSellBond"]);
            M_AccountingSetup.ARSellBondID = dr["ARSellBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellBondID"]);
            M_AccountingSetup.ARSellBondDesc = dr["ARSellBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellBondDesc"]);
            M_AccountingSetup.ARSellEquity = dr["ARSellEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARSellEquity"]);
            M_AccountingSetup.ARSellEquityID = dr["ARSellEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellEquityID"]);
            M_AccountingSetup.ARSellEquityDesc = dr["ARSellEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellEquityDesc"]);
            M_AccountingSetup.ARSellTD = dr["ARSellTD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARSellTD"]);
            M_AccountingSetup.ARSellTDID = dr["ARSellTDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellTDID"]);
            M_AccountingSetup.ARSellTDDesc = dr["ARSellTDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellTDDesc"]);
            M_AccountingSetup.ARSellReksadana = dr["ARSellReksadana"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARSellReksadana"]);
            M_AccountingSetup.ARSellReksadanaID = dr["ARSellReksadanaID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellReksadanaID"]);
            M_AccountingSetup.ARSellReksadanaDesc = dr["ARSellReksadanaDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSellReksadanaDesc"]);
            M_AccountingSetup.InterestPurchaseBond = dr["InterestPurchaseBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestPurchaseBond"]);
            M_AccountingSetup.InterestPurchaseBondID = dr["InterestPurchaseBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestPurchaseBondID"]);
            M_AccountingSetup.InterestPurchaseBondDesc = dr["InterestPurchaseBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestPurchaseBondDesc"]);
            M_AccountingSetup.InterestReceivableBond = dr["InterestReceivableBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableBond"]);
            M_AccountingSetup.InterestReceivableBondID = dr["InterestReceivableBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondID"]);
            M_AccountingSetup.InterestReceivableBondDesc = dr["InterestReceivableBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondDesc"]);

            M_AccountingSetup.InterestReceivableBondGovIDR = dr["InterestReceivableBondGovIDR"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableBondGovIDR"]);
            M_AccountingSetup.InterestReceivableBondGovIDRID = dr["InterestReceivableBondGovIDRID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondGovIDRID"]);
            M_AccountingSetup.InterestReceivableBondGovIDRDesc = dr["InterestReceivableBondGovIDRDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondGovIDRDesc"]);
            M_AccountingSetup.InterestReceivableBondCorpIDR = dr["InterestReceivableBondCorpIDR"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableBondCorpIDR"]);
            M_AccountingSetup.InterestReceivableBondCorpIDRID = dr["InterestReceivableBondCorpIDRID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondCorpIDRID"]);
            M_AccountingSetup.InterestReceivableBondCorpIDRDesc = dr["InterestReceivableBondCorpIDRDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondCorpIDRDesc"]);
            M_AccountingSetup.InterestReceivableBondGovUSD = dr["InterestReceivableBondGovUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableBondGovUSD"]);
            M_AccountingSetup.InterestReceivableBondGovUSDID = dr["InterestReceivableBondGovUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondGovUSDID"]);
            M_AccountingSetup.InterestReceivableBondGovUSDDesc = dr["InterestReceivableBondGovUSDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondGovUSDDesc"]);
            M_AccountingSetup.InterestReceivableBondCorpUSD = dr["InterestReceivableBondCorpUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableBondCorpUSD"]);
            M_AccountingSetup.InterestReceivableBondCorpUSDID = dr["InterestReceivableBondCorpUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondCorpUSDID"]);
            M_AccountingSetup.InterestReceivableBondCorpUSDDesc = dr["InterestReceivableBondCorpUSDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableBondCorpUSDDesc"]);

            M_AccountingSetup.InterestReceivableTD = dr["InterestReceivableTD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestReceivableTD"]);
            M_AccountingSetup.InterestReceivableTDID = dr["InterestReceivableTDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableTDID"]);
            M_AccountingSetup.InterestReceivableTDDesc = dr["InterestReceivableTDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InterestReceivableTDDesc"]);
            M_AccountingSetup.CashBond = dr["CashBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CashBond"]);
            M_AccountingSetup.CashBondID = dr["CashBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashBondID"]);
            M_AccountingSetup.CashBondDesc = dr["CashBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashBondDesc"]);
            M_AccountingSetup.CashEquity = dr["CashEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CashEquity"]);
            M_AccountingSetup.CashEquityID = dr["CashEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashEquityID"]);
            M_AccountingSetup.CashEquityDesc = dr["CashEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashEquityDesc"]);
            M_AccountingSetup.CashTD = dr["CashTD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CashTD"]);
            M_AccountingSetup.CashTDID = dr["CashTDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashTDID"]);
            M_AccountingSetup.CashTDDesc = dr["CashTDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashTDDesc"]);
            M_AccountingSetup.ForeignExchangeRevalAccount = dr["ForeignExchangeRevalAccount"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ForeignExchangeRevalAccount"]);
            M_AccountingSetup.ForeignExchangeRevalAccountID = dr["ForeignExchangeRevalAccountID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ForeignExchangeRevalAccountID"]);
            M_AccountingSetup.ForeignExchangeRevalAccountDesc = dr["ForeignExchangeRevalAccountDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ForeignExchangeRevalAccountDesc"]);
            M_AccountingSetup.UnrealisedEquity = dr["UnrealisedEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedEquity"]);
            M_AccountingSetup.UnrealisedEquityID = dr["UnrealisedEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedEquityID"]);
            M_AccountingSetup.UnrealisedEquityDesc = dr["UnrealisedEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedEquityDesc"]);
            M_AccountingSetup.UnrealisedBond = dr["UnrealisedBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedBond"]);
            M_AccountingSetup.UnrealisedBondID = dr["UnrealisedBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedBondID"]);
            M_AccountingSetup.UnrealisedBondDesc = dr["UnrealisedBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedBondDesc"]);
            M_AccountingSetup.UnrealisedBondUSD = dr["UnrealisedBondUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedBondUSD"]);
            M_AccountingSetup.UnrealisedBondIDUSD = dr["UnrealisedBondIDUSD"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedBondIDUSD"]);
            M_AccountingSetup.UnrealisedBondDescUSD = dr["UnrealisedBondDescUSD"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedBondDescUSD"]);
            M_AccountingSetup.UnrealisedReksadana = dr["UnrealisedReksadana"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealisedReksadana"]);
            M_AccountingSetup.UnrealisedReksadanaID = dr["UnrealisedReksadanaID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedReksadanaID"]);
            M_AccountingSetup.UnrealisedReksadanaDesc = dr["UnrealisedReksadanaDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealisedReksadanaDesc"]);
            M_AccountingSetup.RealisedEquity = dr["RealisedEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedEquity"]);
            M_AccountingSetup.RealisedEquityID = dr["RealisedEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedEquityID"]);
            M_AccountingSetup.RealisedEquityDesc = dr["RealisedEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedEquityDesc"]);

            M_AccountingSetup.RealisedBondGovIDR = dr["RealisedBondGovIDR"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedBondGovIDR"]);
            M_AccountingSetup.RealisedBondGovIDRID = dr["RealisedBondGovIDRID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondGovIDRID"]);
            M_AccountingSetup.RealisedBondGovIDRDesc = dr["RealisedBondGovIDRDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondGovIDRDesc"]);
            M_AccountingSetup.RealisedBondGovUSD = dr["RealisedBondGovUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedBondGovUSD"]);
            M_AccountingSetup.RealisedBondGovUSDID = dr["RealisedBondGovUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondGovUSDID"]);
            M_AccountingSetup.RealisedBondGovUSDDesc = dr["RealisedBondGovUSDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondGovUSDDesc"]);
            M_AccountingSetup.RealisedBondCorpIDR = dr["RealisedBondCorpIDR"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedBondCorpIDR"]);
            M_AccountingSetup.RealisedBondCorpIDRID = dr["RealisedBondCorpIDRID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondCorpIDRID"]);
            M_AccountingSetup.RealisedBondCorpIDRDesc = dr["RealisedBondCorpIDRDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondCorpIDRDesc"]);
            M_AccountingSetup.RealisedBondCorpUSD = dr["RealisedBondCorpUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedBondCorpUSD"]);
            M_AccountingSetup.RealisedBondCorpUSDID = dr["RealisedBondCorpUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondCorpUSDID"]);
            M_AccountingSetup.RealisedBondCorpUSDDesc = dr["RealisedBondCorpUSDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondCorpUSDDesc"]);

            M_AccountingSetup.RealisedBond = dr["RealisedBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedBond"]);
            M_AccountingSetup.RealisedBondID = dr["RealisedBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondID"]);
            M_AccountingSetup.RealisedBondDesc = dr["RealisedBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedBondDesc"]);
            M_AccountingSetup.RealisedReksadana = dr["RealisedReksadana"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedReksadana"]);
            M_AccountingSetup.RealisedReksadanaID = dr["RealisedReksadanaID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedReksadanaID"]);
            M_AccountingSetup.RealisedReksadanaDesc = dr["RealisedReksadanaDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedReksadanaDesc"]);
            M_AccountingSetup.RealisedAset = dr["RealisedAset"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RealisedAset"]);
            M_AccountingSetup.RealisedAsetID = dr["RealisedAsetID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedAsetID"]);
            M_AccountingSetup.RealisedAsetDesc = dr["RealisedAsetDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RealisedAsetDesc"]);
            M_AccountingSetup.CadanganEquity = dr["CadanganEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CadanganEquity"]);
            M_AccountingSetup.CadanganEquityID = dr["CadanganEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganEquityID"]);
            M_AccountingSetup.CadanganEquityDesc = dr["CadanganEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganEquityDesc"]);
            M_AccountingSetup.CadanganBond = dr["CadanganBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CadanganBond"]);
            M_AccountingSetup.CadanganBondID = dr["CadanganBondID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganBondID"]);
            M_AccountingSetup.CadanganBondDesc = dr["CadanganBondDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganBondDesc"]);
            M_AccountingSetup.CadanganBondUSD = dr["CadanganBondUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CadanganBondUSD"]);
            M_AccountingSetup.CadanganBondIDUSD = dr["CadanganBondIDUSD"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganBondIDUSD"]);
            M_AccountingSetup.CadanganBondDescUSD = dr["CadanganBondDescUSD"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganBondDescUSD"]);
            M_AccountingSetup.CadanganReksadana = dr["CadanganReksadana"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CadanganReksadana"]);
            M_AccountingSetup.CadanganReksadanaID = dr["CadanganReksadanaID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganReksadanaID"]);
            M_AccountingSetup.CadanganReksadanaDesc = dr["CadanganReksadanaDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CadanganReksadanaDesc"]);
            M_AccountingSetup.BrokerageFee = dr["BrokerageFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BrokerageFee"]);
            M_AccountingSetup.BrokerageFeeID = dr["BrokerageFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["BrokerageFeeID"]);
            M_AccountingSetup.BrokerageFeeDesc = dr["BrokerageFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["BrokerageFeeDesc"]);
            M_AccountingSetup.JSXLevyFee = dr["JSXLevyFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["JSXLevyFee"]);
            M_AccountingSetup.JSXLevyFeeID = dr["JSXLevyFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["JSXLevyFeeID"]);
            M_AccountingSetup.JSXLevyFeeDesc = dr["JSXLevyFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["JSXLevyFeeDesc"]);
            M_AccountingSetup.KPEIFee = dr["KPEIFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["KPEIFee"]);
            M_AccountingSetup.KPEIFeeID = dr["KPEIFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["KPEIFeeID"]);
            M_AccountingSetup.KPEIFeeDesc = dr["KPEIFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["KPEIFeeDesc"]);
            M_AccountingSetup.VATFee = dr["VATFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["VATFee"]);
            M_AccountingSetup.VATFeeID = dr["VATFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VATFeeID"]);
            M_AccountingSetup.VATFeeDesc = dr["VATFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VATFeeDesc"]);
            M_AccountingSetup.SalesTax = dr["SalesTax"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SalesTax"]);
            M_AccountingSetup.SalesTaxID = dr["SalesTaxID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SalesTaxID"]);
            M_AccountingSetup.SalesTaxDesc = dr["SalesTaxDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SalesTaxDesc"]);
            M_AccountingSetup.IncomeTaxArt23 = dr["IncomeTaxArt23"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IncomeTaxArt23"]);
            M_AccountingSetup.IncomeTaxArt23ID = dr["IncomeTaxArt23ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["IncomeTaxArt23ID"]);
            M_AccountingSetup.IncomeTaxArt23Desc = dr["IncomeTaxArt23Desc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["IncomeTaxArt23Desc"]);
            M_AccountingSetup.EquitySellMethod = dr["EquitySellMethod"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["EquitySellMethod"]);
            M_AccountingSetup.EquitySellMethodDesc = dr["EquitySellMethodDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EquitySellMethodDesc"]);
            M_AccountingSetup.AveragePriority = dr["AveragePriority"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AveragePriority"]);
            M_AccountingSetup.AveragePriorityDesc = dr["AveragePriorityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AveragePriorityDesc"]);
            M_AccountingSetup.FixedAsetBuyPPN = dr["FixedAsetBuyPPN"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FixedAsetBuyPPN"]);
            M_AccountingSetup.FixedAsetBuyPPNID = dr["FixedAsetBuyPPNID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FixedAsetBuyPPNID"]);
            M_AccountingSetup.FixedAsetBuyPPNDesc = dr["FixedAsetBuyPPNDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FixedAsetBuyPPNDesc"]);
            M_AccountingSetup.FixedAsetSellPPN = dr["FixedAsetSellPPN"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FixedAsetSellPPN"]);
            M_AccountingSetup.FixedAsetSellPPNID = dr["FixedAsetSellPPNID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FixedAsetSellPPNID"]);
            M_AccountingSetup.FixedAsetSellPPNDesc = dr["FixedAsetSellPPNDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FixedAsetSellPPNDesc"]);
            M_AccountingSetup.DefaultCurrencyPK = dr["DefaultCurrencyPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["DefaultCurrencyPK"]);
            M_AccountingSetup.DefaultCurrencyID = dr["DefaultCurrencyID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DefaultCurrencyID"]);
            M_AccountingSetup.DecimalPlaces = dr["DecimalPlaces"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["DecimalPlaces"]);


            //---------
            M_AccountingSetup.ARManagementFee = dr["ARManagementFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARManagementFee"]);
            M_AccountingSetup.ARManagementFeeID = dr["ARManagementFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARManagementFeeID"]);
            M_AccountingSetup.ARManagementFeeDesc = dr["ARManagementFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARManagementFeeDesc"]);
            M_AccountingSetup.ManagementFeeExpense = dr["ManagementFeeExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ManagementFeeExpense"]);
            M_AccountingSetup.ManagementFeeExpenseID = dr["ManagementFeeExpenseID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ManagementFeeExpenseID"]);
            M_AccountingSetup.ManagementFeeExpenseDesc = dr["ManagementFeeExpenseDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ManagementFeeExpenseDesc"]);
            M_AccountingSetup.TaxARManagementFee = dr["TaxARManagementFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TaxARManagementFee"]);
            M_AccountingSetup.TaxARManagementFeeID = dr["TaxARManagementFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TaxARManagementFeeID"]);
            M_AccountingSetup.TaxARManagementFeeDesc = dr["TaxARManagementFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TaxARManagementFeeDesc"]);
            M_AccountingSetup.TaxManagementFeeExpense = dr["TaxManagementFeeExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TaxManagementFeeExpense"]);
            M_AccountingSetup.TaxManagementFeeExpenseID = dr["TaxManagementFeeExpenseID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TaxManagementFeeExpenseID"]);
            M_AccountingSetup.TaxManagementFeeExpenseDesc = dr["TaxManagementFeeExpenseDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TaxManagementFeeExpenseDesc"]);

            M_AccountingSetup.TotalEquity = dr["TotalEquity"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TotalEquity"]);
            M_AccountingSetup.TotalEquityID = dr["TotalEquityID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TotalEquityID"]);
            M_AccountingSetup.TotalEquityDesc = dr["TotalEquityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TotalEquityDesc"]);

            M_AccountingSetup.MKBD06Bank = dr["MKBD06Bank"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["MKBD06Bank"]);
            M_AccountingSetup.MKBD06BankID = dr["MKBD06BankID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["MKBD06BankID"]);
            M_AccountingSetup.MKBD06BankDesc = dr["MKBD06BankDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["MKBD06BankDesc"]);
            M_AccountingSetup.MKBD06PettyCash = dr["MKBD06PettyCash"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["MKBD06PettyCash"]);
            M_AccountingSetup.MKBD06PettyCashID = dr["MKBD06PettyCashID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["MKBD06PettyCashID"]);
            M_AccountingSetup.MKBD06PettyCashDesc = dr["MKBD06PettyCashDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["MKBD06PettyCashDesc"]);
            M_AccountingSetup.WHTFee = dr["WHTFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["WHTFee"]);
            M_AccountingSetup.WHTFeeID = dr["WHTFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["WHTFeeID"]);
            M_AccountingSetup.WHTFeeDesc = dr["WHTFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["WHTFeeDesc"]);

            M_AccountingSetup.UnrealizedAccountSGD = dr["UnrealizedAccountSGD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealizedAccountSGD"]);
            M_AccountingSetup.UnrealizedAccountSGDID = dr["UnrealizedAccountSGDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealizedAccountSGDID"]);
            M_AccountingSetup.UnrealizedAccountSGDDesc = dr["UnrealizedAccountSGDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealizedAccountSGDDesc"]);

            M_AccountingSetup.UnrealizedAccountUSD = dr["UnrealizedAccountUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UnrealizedAccountUSD"]);
            M_AccountingSetup.UnrealizedAccountUSDID = dr["UnrealizedAccountUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealizedAccountUSDID"]);
            M_AccountingSetup.UnrealizedAccountUSDDesc = dr["WHTFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UnrealizedAccountUSDDesc"]);

            M_AccountingSetup.EntryUsersID = dr["EntryUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryUsersID"]);
            M_AccountingSetup.UpdateUsersID = dr["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateUsersID"]);
            M_AccountingSetup.ApprovedUsersID = dr["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedUsersID"]);
            M_AccountingSetup.VoidUsersID = dr["VoidUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidUsersID"]);
            M_AccountingSetup.EntryTime = dr["EntryTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryTime"]);
            M_AccountingSetup.UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateTime"]);
            M_AccountingSetup.ApprovedTime = dr["ApprovedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedTime"]);
            M_AccountingSetup.VoidTime = dr["VoidTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidTime"]);
            M_AccountingSetup.DBUserID = dr["DBUserID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBUserID"]);
            M_AccountingSetup.DBTerminalID = dr["DBTerminalID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBTerminalID"]);
            M_AccountingSetup.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdate"]);
            M_AccountingSetup.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);

            //tambahan
            M_AccountingSetup.InvInTDUSD = dr["InvInTDUSD"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvInTDUSD"]);
            M_AccountingSetup.InvInTDUSDID = dr["InvInTDUSDID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInTDUSDID"]);
            M_AccountingSetup.InvInTDUSDDesc = dr["InvInTDUSDDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["InvInTDUSDDesc"]);


            //tambahan
            M_AccountingSetup.ARSubscriptionFee = dr["ARSubscriptionFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARSubscriptionFee"]);
            M_AccountingSetup.ARSubscriptionFeeID = dr["ARSubscriptionFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSubscriptionFeeID"]);
            M_AccountingSetup.ARSubscriptionFeeDesc = dr["ARSubscriptionFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSubscriptionFeeDesc"]);

            M_AccountingSetup.SubscriptionFeeIncome = dr["SubscriptionFeeIncome"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SubscriptionFeeIncome"]);
            M_AccountingSetup.SubscriptionFeeIncomeID = dr["SubscriptionFeeIncomeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SubscriptionFeeIncomeID"]);
            M_AccountingSetup.SubscriptionFeeIncomeDesc = dr["SubscriptionFeeIncomeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SubscriptionFeeIncomeDesc"]);

            M_AccountingSetup.ARRedemptionFee = dr["ARRedemptionFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARRedemptionFee"]);
            M_AccountingSetup.ARRedemptionFeeID = dr["ARRedemptionFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARRedemptionFeeID"]);
            M_AccountingSetup.ARRedemptionFeeDesc = dr["ARRedemptionFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARRedemptionFeeDesc"]);

            M_AccountingSetup.RedemptionFeeIncome = dr["RedemptionFeeIncome"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["RedemptionFeeIncome"]);
            M_AccountingSetup.RedemptionFeeIncomeID = dr["RedemptionFeeIncomeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RedemptionFeeIncomeID"]);
            M_AccountingSetup.RedemptionFeeIncomeDesc = dr["RedemptionFeeIncomeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RedemptionFeeIncomeDesc"]);

            M_AccountingSetup.ARSwitchingFee = dr["ARSwitchingFee"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ARSwitchingFee"]);
            M_AccountingSetup.ARSwitchingFeeID = dr["ARSwitchingFeeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSwitchingFeeID"]);
            M_AccountingSetup.ARSwitchingFeeDesc = dr["ARSwitchingFeeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ARSwitchingFeeDesc"]);

            M_AccountingSetup.SwitchingFeeIncome = dr["SwitchingFeeIncome"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SwitchingFeeIncome"]);
            M_AccountingSetup.SwitchingFeeIncomeID = dr["SwitchingFeeIncomeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SwitchingFeeIncomeID"]);
            M_AccountingSetup.SwitchingFeeIncomeDesc = dr["SwitchingFeeIncomeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SwitchingFeeIncomeDesc"]);

            M_AccountingSetup.AgentCommissionExpense = dr["AgentCommissionExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentCommissionExpense"]);
            M_AccountingSetup.AgentCommissionExpenseID = dr["AgentCommissionExpenseID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCommissionExpenseID"]);
            M_AccountingSetup.AgentCommissionExpenseDesc = dr["AgentCommissionExpenseDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCommissionExpenseDesc"]);

            M_AccountingSetup.AgentCommissionPayable = dr["AgentCommissionPayable"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentCommissionPayable"]);
            M_AccountingSetup.AgentCommissionPayableID = dr["AgentCommissionPayableID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCommissionPayableID"]);
            M_AccountingSetup.AgentCommissionPayableDesc = dr["AgentCommissionPayableDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCommissionPayableDesc"]);

            M_AccountingSetup.WHTPayablePPH21 = dr["WHTPayablePPH21"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["WHTPayablePPH21"]);
            M_AccountingSetup.WHTPayablePPH21ID = dr["WHTPayablePPH21ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["WHTPayablePPH21ID"]);
            M_AccountingSetup.WHTPayablePPH21Desc = dr["WHTPayablePPH21Desc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["WHTPayablePPH21Desc"]);

            M_AccountingSetup.WHTPayablePPH23 = dr["WHTPayablePPH23"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["WHTPayablePPH23"]);
            M_AccountingSetup.WHTPayablePPH23ID = dr["WHTPayablePPH23ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["WHTPayablePPH23ID"]);
            M_AccountingSetup.WHTPayablePPH23Desc = dr["WHTPayablePPH23Desc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["WHTPayablePPH23Desc"]);

            M_AccountingSetup.VatIn = dr["VatIn"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["VatIn"]);
            M_AccountingSetup.VatInID = dr["VatInID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VatInID"]);
            M_AccountingSetup.VatInDesc = dr["VatInDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VatInDesc"]);

            M_AccountingSetup.VatOut = dr["VatOut"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["VatOut"]);
            M_AccountingSetup.VatOutID = dr["VatOutID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VatOutID"]);
            M_AccountingSetup.VatOutDesc = dr["VatOutDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VatOutDesc"]);

            M_AccountingSetup.AgentCommissionCash = dr["AgentCommissionCash"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentCommissionCash"]);
            M_AccountingSetup.AgentCommissionCashID = dr["AgentCommissionCashID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCommissionCashID"]);
            M_AccountingSetup.AgentCommissionCashDesc = dr["AgentCommissionCashDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCommissionCashDesc"]);



            if (_host.CheckColumnIsExist(dr, "SwitchingFundAcc"))
            {
                M_AccountingSetup.SwitchingFundAcc = dr["SwitchingFundAcc"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SwitchingFundAcc"]);
            }

            M_AccountingSetup.AgentCSRExpense = dr["AgentCSRExpense"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentCSRExpense"]);
            M_AccountingSetup.AgentCSRExpenseID = dr["AgentCSRExpenseID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCSRExpenseID"]);
            M_AccountingSetup.AgentCSRExpenseDesc = dr["AgentCSRExpenseDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCSRExpenseDesc"]);

            M_AccountingSetup.AgentCSRPayable = dr["AgentCSRPayable"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentCSRPayable"]);
            M_AccountingSetup.AgentCSRPayableID = dr["AgentCSRPayableID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCSRPayableID"]);
            M_AccountingSetup.AgentCSRPayableDesc = dr["AgentCSRPayableDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentCSRPayableDesc"]);

            return M_AccountingSetup;
        }

        //3
        public List<AccountingSetup> AccountingSetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountingSetup> L_accountingSetup = new List<AccountingSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              //" Select  A1.AccountPK InvInBond,A1.ID InvInBondID,A1.Name InvInBondDesc,A2.AccountPK InvInEquity,A2.ID InvInEquityID,A2.Name InvInEquityDesc,A3.AccountPK InvInTD,A3.ID InvInTDID,A3.Name InvInTDDesc,       " +
                              //" A4.AccountPK APPurchaseBond,A4.ID APPurchaseBondID,A4.Name APPurchaseBondDesc,A5.AccountPK APPurchaseEquity,A5.ID APPurchaseEquityID,A5.Name APPurchaseEquityDesc,A6.AccountPK APPurchaseTD,A6.ID APPurchaseTDID,A6.Name APPurchaseTDDesc,       " +
                              //" A7.AccountPK ARSellBond,A7.ID ARSellBondID,A7.Name ARSellBondDesc,A8.AccountPK ARSellEquity,A8.ID ARSellEquityID,A8.Name ARSellEquityDesc,A9.AccountPK ARSellTD,A9.ID ARSellTDID,A9.Name ARSellTDDesc,       " +
                              //" A10.AccountPK InterestPurchaseBond,A10.ID InterestPurchaseBondID,A10.Name InterestPurchaseBondDesc,A11.AccountPK InterestReceivableBond,A11.ID InterestReceivableBondID,A11.Name InterestReceivableBondDesc, " +
                              //" A12.AccountPK InterestReceivableTD,A12.ID InterestReceivableTDID,A12.Name InterestReceivableTDDesc, A13.AccountPK ForeignExchangeRevalAccount,A13.ID ForeignExchangeRevalAccountID,A13.Name ForeignExchangeRevalAccountDesc,      " +
                              //" A14.AccountPK UnrealisedEquity,A14.ID UnrealisedEquityID,A14.Name UnrealisedEquityDesc, A15.AccountPK UnrealisedBond,A15.ID UnrealisedBondID,A15.Name UnrealisedBondDesc,      " +
                              //" A16.AccountPK UnrealisedReksadana,A16.ID UnrealisedReksadanaID,A16.Name UnrealisedReksadanaDesc, A17.AccountPK RealisedEquity,A17.ID RealisedEquityID,A17.Name RealisedEquityDesc,      " +
                              //" A18.AccountPK RealisedBond,A18.ID RealisedBondID,A18.Name RealisedBondDesc, A19.AccountPK RealisedReksadana,A19.ID RealisedReksadanaID,A19.Name RealisedReksadanaDesc,      " +
                              //" A20.AccountPK RealisedAset,A20.ID RealisedAsetID,A20.Name RealisedAsetDesc, A21.AccountPK CadanganEquity,A21.ID CadanganEquityID,A21.Name CadanganEquityDesc,      " +
                              //" A22.AccountPK CadanganBond,A22.ID CadanganBondID,A22.Name CadanganBondDesc, A23.AccountPK CadanganReksadana,A23.ID CadanganReksadanaID,A23.Name CadanganReksadanaDesc,      " +
                              //" A24.AccountPK BrokerageFee,A24.ID BrokerageFeeID,A24.Name BrokerageFeeDesc, A25.AccountPK JSXLevyFee,A25.ID JSXLevyFeeID,A25.Name JSXLevyFeeDesc,      " +
                              //" A26.AccountPK KPEIFee,A26.ID KPEIFeeID,A26.Name KPEIFeeDesc, A27.AccountPK SalesTax,A27.ID SalesTaxID,A27.Name SalesTaxDesc,A28.AccountPK IncomeTaxArt23,A28.ID IncomeTaxArt23ID,A28.Name IncomeTaxArt23Desc,       " +
                              //" A29.AccountPK FixedAsetBuyPPN,A29.ID FixedAsetBuyPPNID,A29.Name FixedAsetBuyPPNDesc, A30.AccountPK FixedAsetSellPPN,A30.ID FixedAsetSellPPNID,A30.Name FixedAsetSellPPNDesc,       " +
                              //" MV1.DescOne EquitySellMethodDesc,MV2.DescOne AveragePriorityDesc, " +
                              //" C1.CashRefPK CashBond,C1.ID CashBondID,C1.Name CashBondDesc,C2.CashRefPK CashEquity,C2.ID CashEquityID,C2.Name CashEquityDesc,C3.CashRefPK CashTD,C3.ID CashTDID,C3.Name CashTDDesc, CR.ID DefaultCurrencyID,     " +
                              //" A.* from AccountingSetup A left join        " +
                              //" Account A1 on A.InvInBond = A1.AccountPK and A1.status = 2  left join        " +
                              //" Account A2 on A.InvInEquity = A2.AccountPK and A2.status = 2  left join        " +
                              //" Account A3 on A.InvInTD = A3.AccountPK and A3.status = 2  left join        " +
                              //" Account A4 on A.APPurchaseBond = A4.AccountPK and A4.status = 2  left join        " +
                              //" Account A5 on A.APPurchaseEquity = A5.AccountPK and A5.status = 2  left join        " +
                              //" Account A6 on A.APPurchaseTD = A6.AccountPK and A6.status = 2  left join        " +
                              //" Account A7 on A.ARSellBond = A7.AccountPK and A7.status = 2  left join        " +
                              //" Account A8 on A.ARSellEquity = A8.AccountPK and A8.status = 2  left join        " +
                              //" Account A9 on A.ARSellTD = A9.AccountPK and A9.status = 2  left join        " +
                              //" Account A10 on A.InterestPurchaseBond = A10.AccountPK and A10.status = 2  left join        " +
                              //" Account A11 on A.InterestReceivableBond = A11.AccountPK and A11.status = 2  left join       " +
                              //" Account A12 on A.InterestReceivableTD = A12.AccountPK and A12.status = 2  left join      " +
                              //" Account A13 on A.ForeignExchangeRevalAccount = A13.AccountPK and A13.status = 2  left join      " +
                              //" Account A14 on A.UnrealisedEquity = A14.AccountPK and A14.status = 2  left join        " +
                              //" Account A15 on A.UnrealisedBond = A15.AccountPK and A15.status = 2  left join       " +
                              //" Account A16 on A.UnrealisedReksadana = A16.AccountPK and A16.status = 2  left join      " +
                              //" Account A17 on A.RealisedEquity = A17.AccountPK and A17.status = 2  left join      " +
                              //" Account A18 on A.RealisedBond = A18.AccountPK and A18.status = 2  left join        " +
                              //" Account A19 on A.RealisedReksadana = A19.AccountPK and A19.status = 2  left join       " +
                              //" Account A20 on A.RealisedAset = A20.AccountPK and A20.status = 2  left join      " +
                              //" Account A21 on A.CadanganEquity = A21.AccountPK and A21.status = 2  left join      " +
                              //" Account A22 on A.CadanganBond = A22.AccountPK and A22.status = 2  left join        " +
                              //" Account A23 on A.CadanganReksadana = A23.AccountPK and A23.status = 2  left join       " +
                              //" Account A24 on A.BrokerageFee = A24.AccountPK and A24.status = 2  left join      " +
                              //" Account A25 on A.JSXLevyFee = A25.AccountPK and A25.status = 2  left join      " +
                              //" Account A26 on A.KPEIFee = A26.AccountPK and A26.status = 2  left join      " +
                              //" Account A27 on A.SalesTax = A27.AccountPK and A27.status = 2  left join      " +
                              //" Account A28 on A.IncomeTaxArt23 = A28.AccountPK and A28.status = 2  left join      " +
                              //" Account A29 on A.FixedAsetBuyPPN = A29.AccountPK and A29.status = 2  left join      " +
                              //" Account A30 on A.FixedAsetSellPPN = A30.AccountPK and A30.status = 2  left join      " +
                              //" MasterValue MV1 on A.EquitySellMethod = MV1.Code and MV1.status = 2  and MV1.ID ='EquitySellMethod' left join      " +
                              //" MasterValue MV2 on A.AveragePriority = MV2.Code and MV2.status = 2  and MV2.ID ='AveragePriority' left join      " +
                              //" CashRef C1 on A.CashBond = C1.CashRefPK and C1.status = 2  left join     " +
                              //" CashRef C2 on A.CashEquity = C2.CashRefPK and C2.status = 2  left join   " +
                              //" CashRef C3 on A.CashTD = C3.CashRefPK and C3.status = 2  left join " +
                              //" Currency CR on A.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2 " +
                              //" where A.status = @status  ";

                              @"Select  A1.AccountPK InvInBond,A1.ID InvInBondID,A1.Name InvInBondDesc,A2.AccountPK InvInEquity,A2.ID InvInEquityID,A2.Name InvInEquityDesc,A3.AccountPK InvInTD,A3.ID InvInTDID,A3.Name InvInTDDesc,        
                                A4.AccountPK APPurchaseBond,A4.ID APPurchaseBondID,A4.Name APPurchaseBondDesc,A5.AccountPK APPurchaseEquity,A5.ID APPurchaseEquityID,A5.Name APPurchaseEquityDesc,A6.AccountPK APPurchaseTD,A6.ID APPurchaseTDID,A6.Name APPurchaseTDDesc,        
                                A7.AccountPK ARSellBond,A7.ID ARSellBondID,A7.Name ARSellBondDesc,A8.AccountPK ARSellEquity,A8.ID ARSellEquityID,A8.Name ARSellEquityDesc,A9.AccountPK ARSellTD,A9.ID ARSellTDID,A9.Name ARSellTDDesc,        
                                A10.AccountPK InterestPurchaseBond,A10.ID InterestPurchaseBondID,A10.Name InterestPurchaseBondDesc,A11.AccountPK InterestReceivableBond,A11.ID InterestReceivableBondID,A11.Name InterestReceivableBondDesc,  
                                A12.AccountPK InterestReceivableTD,A12.ID InterestReceivableTDID,A12.Name InterestReceivableTDDesc, A13.AccountPK ForeignExchangeRevalAccount,A13.ID ForeignExchangeRevalAccountID,A13.Name ForeignExchangeRevalAccountDesc,       
                                A14.AccountPK UnrealisedEquity,A14.ID UnrealisedEquityID,A14.Name UnrealisedEquityDesc, A15.AccountPK UnrealisedBond,A15.ID UnrealisedBondID,A15.Name UnrealisedBondDesc,       
                                A16.AccountPK UnrealisedReksadana,A16.ID UnrealisedReksadanaID,A16.Name UnrealisedReksadanaDesc, A17.AccountPK RealisedEquity,A17.ID RealisedEquityID,A17.Name RealisedEquityDesc,       
                                A18.AccountPK RealisedBond,A18.ID RealisedBondID,A18.Name RealisedBondDesc, A19.AccountPK RealisedReksadana,A19.ID RealisedReksadanaID,A19.Name RealisedReksadanaDesc,       
                                A20.AccountPK RealisedAset,A20.ID RealisedAsetID,A20.Name RealisedAsetDesc, A21.AccountPK CadanganEquity,A21.ID CadanganEquityID,A21.Name CadanganEquityDesc,       
                                A22.AccountPK CadanganBond,A22.ID CadanganBondID,A22.Name CadanganBondDesc, A23.AccountPK CadanganReksadana,A23.ID CadanganReksadanaID,A23.Name CadanganReksadanaDesc,       
                                A24.AccountPK BrokerageFee,A24.ID BrokerageFeeID,A24.Name BrokerageFeeDesc, A25.AccountPK JSXLevyFee,A25.ID JSXLevyFeeID,A25.Name JSXLevyFeeDesc,       
                                A26.AccountPK KPEIFee,A26.ID KPEIFeeID,A26.Name KPEIFeeDesc, A27.AccountPK SalesTax,A27.ID SalesTaxID,A27.Name SalesTaxDesc,A28.AccountPK IncomeTaxArt23,A28.ID IncomeTaxArt23ID,A28.Name IncomeTaxArt23Desc,        
                                A29.AccountPK FixedAsetBuyPPN,A29.ID FixedAsetBuyPPNID,A29.Name FixedAsetBuyPPNDesc, A30.AccountPK FixedAsetSellPPN,A30.ID FixedAsetSellPPNID,A30.Name FixedAsetSellPPNDesc,
                                A31.AccountPK Income,A31.ID IncomeID,A31.Name IncomeDesc,
                                A32.AccountPK Expense,A32.ID ExpenseID,A32.Name ExpenseDesc,
                                A33.AccountPK PPHFinal,A33.ID PPHFinalID,A33.Name PPHFinalDesc,
                                A34.AccountPK InvInBondGovIDR,A34.ID InvInBondGovIDRID,A34.Name InvInBondGovIDRDesc,
                                A35.AccountPK InvInBondGovUSD,A35.ID InvInBondGovUSDID,A35.Name InvInBondGovUSDDesc,
                                A36.AccountPK InvInBondCorpIDR,A36.ID InvInBondCorpIDRID,A36.Name InvInBondCorpIDRDesc,
                                A37.AccountPK InvInBondCorpUSD,A37.ID InvInBondCorpUSDID,A37.Name InvInBondCorpUSDDesc,
                                A38.AccountPK InvInReksadana,A38.ID InvInReksadanaID,A38.Name InvInReksadanaDesc,
                                A39.AccountPK InterestReceivableBondGovIDR,A39.ID InterestReceivableBondGovIDRID,A39.Name InterestReceivableBondGovIDRDesc,
                                A40.AccountPK InterestReceivableBondCorpIDR,A40.ID InterestReceivableBondCorpIDRID,A40.Name InterestReceivableBondCorpIDRDesc,
                                A41.AccountPK InterestReceivableBondGovUSD,A41.ID InterestReceivableBondGovUSDID,A41.Name InterestReceivableBondGovUSDDesc,
                                A42.AccountPK InterestReceivableBondCorpUSD,A42.ID InterestReceivableBondCorpUSDID,A42.Name InterestReceivableBondCorpUSDDesc,
                                A43.AccountPK RealisedBondGovIDR,A43.ID RealisedBondGovIDRID,A43.Name RealisedBondGovIDRDesc,
                                A44.AccountPK RealisedBondGovUSD,A44.ID RealisedBondGovUSDID,A44.Name RealisedBondGovUSDDesc,
                                A45.AccountPK RealisedBondCorpIDR,A45.ID RealisedBondCorpIDRID,A45.Name RealisedBondCorpIDRDesc,
                                A46.AccountPK RealisedBondCorpUSD,A46.ID RealisedBondCorpUSDID,A46.Name RealisedBondCorpUSDDesc,  

                                A47.AccountPK ARManagementFee,A47.ID ARManagementFeeID,A47.Name ARManagementFeeDesc,  
                                A48.AccountPK ManagementFeeExpense,A48.ID ManagementFeeExpenseID,A48.Name ManagementFeeExpenseDesc,  
                                A49.AccountPK MKBD06Bank,A49.ID MKBD06BankID,A49.Name MKBD06BankDesc,  
                                A50.AccountPK MKBD06PettyCash,A50.ID MKBD06PettyCashID,A50.Name MKBD06PettyCashDesc,  
                                A51.AccountPK WHTFee,A51.ID WHTFeeID,A51.Name WHTFeeDesc, 
                                A52.AccountPK VATFee,A52.ID VATFeeID,A52.Name VATFeeDesc,     

                                A53.AccountPK TaxARManagementFee,A53.ID TaxARManagementFeeID,A53.Name TaxARManagementFeeDesc,  
                                A54.AccountPK TaxManagementFeeExpense,A54.ID TaxManagementFeeExpenseID,A54.Name TaxManagementFeeExpenseDesc,  
                                A55.AccountPK APPurchaseReksadana,A55.ID APPurchaseReksadanaID,A55.Name APPurchaseReksadanaDesc,  
                                A56.AccountPK ARSellReksadana,A56.ID ARSellReksadanaID,A56.Name ARSellReksadanaDesc, 
                                A57.AccountPK TotalEquity,A57.ID TotalEquityID,A57.Name TotalEquityDesc, 

                                A58.AccountPK UnrealisedBondUSD,A58.ID UnrealisedBondIDUSD,A58.Name UnrealisedBondDescUSD, 
                                A59.AccountPK CadanganBondUSD,A59.ID CadanganBondIDUSD,A59.Name CadanganBondDescUSD, 
                                A60.AccountPK UnrealizedAccountSGD,A60.ID UnrealizedAccountSGDID,A60.Name UnrealizedAccountSGDDesc, 
                                A61.AccountPK UnrealizedAccountUSD,A61.ID UnrealizedAccountUSDID,A61.Name UnrealizedAccountUSDDesc,
                                --tambahan
                                A62.AccountPK InvInTDUSD,A62.ID InvInTDUSDID,A62.Name InvInTDUSDDesc,

                                --tambahan
                                A63.AccountPK ARSubscriptionFee,A63.ID ARSubscriptionFeeID,A63.Name ARSubscriptionFeeDesc,
                                A64.AccountPK SubscriptionFeeIncome,A64.ID SubscriptionFeeIncomeID,A64.Name SubscriptionFeeIncomeDesc,
                                A65.AccountPK ARRedemptionFee,A65.ID ARRedemptionFeeID,A65.Name ARRedemptionFeeDesc,
                                A66.AccountPK RedemptionFeeIncome,A66.ID RedemptionFeeIncomeID,A66.Name RedemptionFeeIncomeDesc,
                                A67.AccountPK ARSwitchingFee,A67.ID ARSwitchingFeeID,A67.Name ARSwitchingFeeDesc,
                                A68.AccountPK SwitchingFeeIncome,A68.ID SwitchingFeeIncomeID,A68.Name SwitchingFeeIncomeDesc,
                                
                                --tambahan
                                A69.AccountPK AgentCommissionExpense,A69.ID AgentCommissionExpenseID,A69.Name AgentCommissionExpenseDesc,
                                A70.AccountPK AgentCommissionPayable,A70.ID AgentCommissionPayableID,A70.Name AgentCommissionPayableDesc,
                                A71.AccountPK WHTPayablePPH21,A71.ID WHTPayablePPH21ID,A71.Name WHTPayablePPH21Desc,
                                A72.AccountPK WHTPayablePPH23,A72.ID WHTPayablePPH23ID,A72.Name WHTPayablePPH23Desc,
                                A73.AccountPK VatIn,A73.ID VatInID,A73.Name VatInDesc,
                                A74.AccountPK VatOut,A74.ID VatOutID,A74.Name VatOutDesc,
                                A75.AccountPK AgentCommissionCash,A75.ID AgentCommissionCashID,A75.Name AgentCommissionCashDesc,
                                
                                A76.AccountPK SwitchingFundAcc,A75.ID SwitchingFundAccID,A75.Name SwitchingFundAccDesc,
                                
                                A77.AccountPK AgentCSRExpense,A77.ID AgentCSRExpenseID,A77.Name AgentCSRExpenseDesc,
                                A78.AccountPK AgentCSRPayable,A78.ID AgentCSRPayableID,A78.Name AgentCSRPayableDesc,

                                MV1.DescOne EquitySellMethodDesc,MV2.DescOne AveragePriorityDesc,  
                                C1.CashRefPK CashBond,C1.ID CashBondID,C1.Name CashBondDesc,C2.CashRefPK CashEquity,C2.ID CashEquityID,C2.Name CashEquityDesc,C3.CashRefPK CashTD,C3.ID CashTDID,C3.Name CashTDDesc, CR.ID DefaultCurrencyID,      
                                A.* from AccountingSetup A left join         
                                Account A1 on A.InvInBond = A1.AccountPK and A1.status = 2  left join         
                                Account A2 on A.InvInEquity = A2.AccountPK and A2.status = 2  left join         
                                Account A3 on A.InvInTD = A3.AccountPK and A3.status = 2  left join         
                                Account A4 on A.APPurchaseBond = A4.AccountPK and A4.status = 2  left join         
                                Account A5 on A.APPurchaseEquity = A5.AccountPK and A5.status = 2  left join         
                                Account A6 on A.APPurchaseTD = A6.AccountPK and A6.status = 2  left join         
                                Account A7 on A.ARSellBond = A7.AccountPK and A7.status = 2  left join         
                                Account A8 on A.ARSellEquity = A8.AccountPK and A8.status = 2  left join         
                                Account A9 on A.ARSellTD = A9.AccountPK and A9.status = 2  left join         
                                Account A10 on A.InterestPurchaseBond = A10.AccountPK and A10.status = 2  left join         
                                Account A11 on A.InterestReceivableBond = A11.AccountPK and A11.status = 2  left join        
                                Account A12 on A.InterestReceivableTD = A12.AccountPK and A12.status = 2  left join       
                                Account A13 on A.ForeignExchangeRevalAccount = A13.AccountPK and A13.status = 2  left join       
                                Account A14 on A.UnrealisedEquity = A14.AccountPK and A14.status = 2  left join         
                                Account A15 on A.UnrealisedBond = A15.AccountPK and A15.status = 2  left join        
                                Account A16 on A.UnrealisedReksadana = A16.AccountPK and A16.status = 2  left join       
                                Account A17 on A.RealisedEquity = A17.AccountPK and A17.status = 2  left join       
                                Account A18 on A.RealisedBond = A18.AccountPK and A18.status = 2  left join         
                                Account A19 on A.RealisedReksadana = A19.AccountPK and A19.status = 2  left join        
                                Account A20 on A.RealisedAset = A20.AccountPK and A20.status = 2  left join       
                                Account A21 on A.CadanganEquity = A21.AccountPK and A21.status = 2  left join       
                                Account A22 on A.CadanganBond = A22.AccountPK and A22.status = 2  left join         
                                Account A23 on A.CadanganReksadana = A23.AccountPK and A23.status = 2  left join        
                                Account A24 on A.BrokerageFee = A24.AccountPK and A24.status = 2  left join       
                                Account A25 on A.JSXLevyFee = A25.AccountPK and A25.status = 2  left join       
                                Account A26 on A.KPEIFee = A26.AccountPK and A26.status = 2  left join       
                                Account A27 on A.SalesTax = A27.AccountPK and A27.status = 2  left join       
                                Account A28 on A.IncomeTaxArt23 = A28.AccountPK and A28.status = 2  left join       
                                Account A29 on A.FixedAsetBuyPPN = A29.AccountPK and A29.status = 2  left join       
                                Account A30 on A.FixedAsetSellPPN = A30.AccountPK and A30.status = 2  left join 
                                Account A31 on A.Income = A31.AccountPK and A31.status = 2  left join 
                                Account A32 on A.Expense = A32.AccountPK and A32.status = 2  left join 
                                Account A33 on A.PPHFinal = A33.AccountPK and A33.status = 2  left join 
                                Account A34 on A.InvInBondGovIDR = A34.AccountPK and A34.status = 2  left join 
                                Account A35 on A.InvInBondGovUSD = A35.AccountPK and A35.status = 2  left join 
                                Account A36 on A.InvInBondCorpIDR = A36.AccountPK and A36.status = 2  left join 
                                Account A37 on A.InvInBondCorpUSD = A37.AccountPK and A37.status = 2  left join 
                                Account A38 on A.InvInReksadana = A38.AccountPK and A38.status = 2  left join 
                                Account A39 on A.InterestReceivableBondGovIDR = A39.AccountPK and A39.status = 2  left join 
                                Account A40 on A.InterestReceivableBondCorpIDR = A40.AccountPK and A40.status = 2  left join 
                                Account A41 on A.InterestReceivableBondGovUSD = A41.AccountPK and A41.status = 2  left join 
                                Account A42 on A.InterestReceivableBondCorpUSD = A42.AccountPK and A42.status = 2  left join 
                                Account A43 on A.RealisedBondGovIDR = A43.AccountPK and A43.status = 2  left join 
                                Account A44 on A.RealisedBondGovUSD = A44.AccountPK and A44.status = 2  left join 
                                Account A45 on A.RealisedBondCorpIDR = A45.AccountPK and A45.status = 2  left join 
                                Account A46 on A.RealisedBondCorpUSD = A46.AccountPK and A46.status = 2  left join  

                                Account A47 on A.ARManagementFee = A47.AccountPK and A47.status = 2  left join  
                                Account A48 on A.ManagementFeeExpense = A48.AccountPK and A48.status = 2  left join  
                                Account A49 on A.MKBD06Bank = A49.AccountPK and A49.status = 2  left join  
                                Account A50 on A.MKBD06PettyCash = A50.AccountPK and A50.status = 2  left join  
                                Account A51 on A.WHTFee = A51.AccountPK and A51.status = 2  left join  
                                Account A52 on A.VATFee = A52.AccountPK and A52.status = 2  left join 
                                Account A53 on A.TaxARManagementFee = A53.AccountPK and A53.status = 2  left join  
                                Account A54 on A.TaxManagementFeeExpense = A54.AccountPK and A54.status = 2  left join   
                                Account A55 on A.APPurchaseReksadana = A55.AccountPK and A55.status = 2  left join  
                                Account A56 on A.ARSellReksadana = A56.AccountPK and A56.status = 2  left join  
                                Account A57 on A.TotalEquity = A57.AccountPK and A57.status = 2  left join  
                                Account A58 on A.UnrealisedBondUSD = A58.AccountPK and A58.status = 2  left join  
                                Account A59 on A.CadanganBondUSD = A59.AccountPK and A59.status = 2  left join  
                                Account A60 on A.UnrealizedAccountSGD = A60.AccountPK and A60.status = 2  left join  
                                Account A61 on A.UnrealizedAccountUSD = A61.AccountPK and A61.status = 2  left join
                                --tambahan
                                Account A62 on A.InvInTDUSD = A62.AccountPK and A62.status = 2  left join  

                                --tambahan
                                Account A63 on A.ARSubscriptionFee = A63.AccountPK and A63.status = 2  left join  
                                Account A64 on A.SubscriptionFeeIncome = A64.AccountPK and A64.status = 2  left join  
                                Account A65 on A.ARRedemptionFee = A65.AccountPK and A65.status = 2  left join  
                                Account A66 on A.RedemptionFeeIncome = A66.AccountPK and A66.status = 2  left join  
                                Account A67 on A.ARSwitchingFee = A67.AccountPK and A67.status = 2  left join  
                                Account A68 on A.SwitchingFeeIncome = A68.AccountPK and A68.status = 2  left join  

                                --tambahan
                                Account A69 on A.AgentCommissionExpense = A69.AccountPK and A69.status = 2  left join  
                                Account A70 on A.AgentCommissionPayable = A70.AccountPK and A70.status = 2  left join  
                                Account A71 on A.WHTPayablePPH21 = A71.AccountPK and A71.status = 2  left join  
                                Account A72 on A.WHTPayablePPH23 = A72.AccountPK and A72.status = 2  left join  
                                Account A73 on A.VatIn = A73.AccountPK and A73.status = 2  left join  
                                Account A74 on A.VatOut = A74.AccountPK and A74.status = 2  left join  
                                Account A75 on A.AgentCommissionCash = A75.AccountPK and A75.status = 2  left join  

                                Account A76 on A.SwitchingFundAcc = A76.AccountPK and A76.status = 2  left join                                  

                                Account A77 on A.AgentCSRExpense = A77.AccountPK and A77.status = 2  left join 
                                Account A78 on A.AgentCSRPayable = A78.AccountPK and A78.status = 2  left join 

                                MasterValue MV1 on A.EquitySellMethod = MV1.Code and MV1.status = 2  and MV1.ID ='EquitySellMethod' left join       
                                MasterValue MV2 on A.AveragePriority = MV2.Code and MV2.status = 2  and MV2.ID ='AveragePriority' left join       
                                CashRef C1 on A.CashBond = C1.CashRefPK and C1.status = 2  left join      
                                CashRef C2 on A.CashEquity = C2.CashRefPK and C2.status = 2  left join    
                                CashRef C3 on A.CashTD = C3.CashRefPK and C3.status = 2  left join  
                                Currency CR on A.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2   
                                where A.status = @status";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from AccountingSetup";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_accountingSetup.Add(setAccountingSetup(dr));
                                }
                            }
                            return L_accountingSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //4
        public AccountingSetup AccountingSetup_SelectByAccountingSetupPK(int _accountingSetupPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {



                        cmd.CommandText =
                            //  " Select  A1.AccountPK InvInBond,A1.ID InvInBondID,A1.Name InvInBondDesc,A2.AccountPK InvInEquity,A2.ID InvInEquityID,A2.Name InvInEquityDesc,A3.AccountPK InvInTD,A3.ID InvInTDID,A3.Name InvInTDDesc,       " +
                            //  " A4.AccountPK APPurchaseBond,A4.ID APPurchaseBondID,A4.Name APPurchaseBondDesc,A5.AccountPK APPurchaseEquity,A5.ID APPurchaseEquityID,A5.Name APPurchaseEquityDesc,A6.AccountPK APPurchaseTD,A6.ID APPurchaseTDID,A6.Name APPurchaseTDDesc,       " +
                            //  " A7.AccountPK ARSellBond,A7.ID ARSellBondID,A7.Name ARSellBondDesc,A8.AccountPK ARSellEquity,A8.ID ARSellEquityID,A8.Name ARSellEquityDesc,A9.AccountPK ARSellTD,A9.ID ARSellTDID,A9.Name ARSellTDDesc,       " +
                            //  " A10.AccountPK InterestPurchaseBond,A10.ID InterestPurchaseBondID,A10.Name InterestPurchaseBondDesc,A11.AccountPK InterestReceivableBond,A11.ID InterestReceivableBondID,A11.Name InterestReceivableBondDesc, " +
                            //  " A12.AccountPK InterestReceivableTD,A12.ID InterestReceivableTDID,A12.Name InterestReceivableTDDesc, A13.AccountPK ForeignExchangeRevalAccount,A13.ID ForeignExchangeRevalAccountID,A13.Name ForeignExchangeRevalAccountDesc,      " +
                            //  " A14.AccountPK UnrealisedEquity,A14.ID UnrealisedEquityID,A14.Name UnrealisedEquityDesc, A15.AccountPK UnrealisedBond,A15.ID UnrealisedBondID,A15.Name UnrealisedBondDesc,      " +
                            //  " A16.AccountPK UnrealisedReksadana,A16.ID UnrealisedReksadanaID,A16.Name UnrealisedReksadanaDesc, A17.AccountPK RealisedEquity,A17.ID RealisedEquityID,A17.Name RealisedEquityDesc,      " +
                            //  " A18.AccountPK RealisedBond,A18.ID RealisedBondID,A18.Name RealisedBondDesc, A19.AccountPK RealisedReksadana,A19.ID RealisedReksadanaID,A19.Name RealisedReksadanaDesc,      " +
                            //  " A20.AccountPK RealisedAset,A20.ID RealisedAsetID,A20.Name RealisedAsetDesc, A21.AccountPK CadanganEquity,A21.ID CadanganEquityID,A21.Name CadanganEquityDesc,      " +
                            //  " A22.AccountPK CadanganBond,A22.ID CadanganBondID,A22.Name CadanganBondDesc, A23.AccountPK CadanganReksadana,A23.ID CadanganReksadanaID,A23.Name CadanganReksadanaDesc,      " +
                            //  " A24.AccountPK BrokerageFee,A24.ID BrokerageFeeID,A24.Name BrokerageFeeDesc, A25.AccountPK JSXLevyFee,A25.ID JSXLevyFeeID,A25.Name JSXLevyFeeDesc,      " +
                            //  " A26.AccountPK KPEIFee,A26.ID KPEIFeeID,A26.Name KPEIFeeDesc, A27.AccountPK SalesTax,A27.ID SalesTaxID,A27.Name SalesTaxDesc,A28.AccountPK IncomeTaxArt23,A28.ID IncomeTaxArt23ID,A28.Name IncomeTaxArt23Desc,       " +
                            //  " A29.AccountPK FixedAsetBuyPPN,A29.ID FixedAsetBuyPPNID,A29.Name FixedAsetBuyPPNDesc, A30.AccountPK FixedAsetSellPPN,A30.ID FixedAsetSellPPNID,A30.Name FixedAsetSellPPNDesc,       " +
                            //  " MV1.DescOne EquitySellMethodDesc,MV2.DescOne AveragePriorityDesc, " +
                            //  " C1.CashRefPK CashBond,C1.ID CashBondID,C1.Name CashBondDesc,C2.CashRefPK CashEquity,C2.ID CashEquityID,C2.Name CashEquityDesc,C3.CashRefPK CashTD,C3.ID CashTDID,C3.Name CashTDDesc, CR.ID DefaultCurrencyID,     " +
                            //  " A.* from AccountingSetup A left join        " +
                            //  " Account A1 on A.InvInBond = A1.AccountPK and A1.status = 2  left join        " +
                            //  " Account A2 on A.InvInEquity = A2.AccountPK and A2.status = 2  left join        " +
                            //  " Account A3 on A.InvInTD = A3.AccountPK and A3.status = 2  left join        " +
                            //  " Account A4 on A.APPurchaseBond = A4.AccountPK and A4.status = 2  left join        " +
                            //  " Account A5 on A.APPurchaseEquity = A5.AccountPK and A5.status = 2  left join        " +
                            //  " Account A6 on A.APPurchaseTD = A6.AccountPK and A6.status = 2  left join        " +
                            //  " Account A7 on A.ARSellBond = A7.AccountPK and A7.status = 2  left join        " +
                            //  " Account A8 on A.ARSellEquity = A8.AccountPK and A8.status = 2  left join        " +
                            //  " Account A9 on A.ARSellTD = A9.AccountPK and A9.status = 2  left join        " +
                            //  " Account A10 on A.InterestPurchaseBond = A10.AccountPK and A10.status = 2  left join        " +
                            //  " Account A11 on A.InterestReceivableBond = A11.AccountPK and A11.status = 2  left join       " +
                            //  " Account A12 on A.InterestReceivableTD = A12.AccountPK and A12.status = 2  left join      " +
                            //  " Account A13 on A.ForeignExchangeRevalAccount = A13.AccountPK and A13.status = 2  left join      " +
                            //  " Account A14 on A.UnrealisedEquity = A14.AccountPK and A14.status = 2  left join        " +
                            //  " Account A15 on A.UnrealisedBond = A15.AccountPK and A15.status = 2  left join       " +
                            //  " Account A16 on A.UnrealisedReksadana = A16.AccountPK and A16.status = 2  left join      " +
                            //  " Account A17 on A.RealisedEquity = A17.AccountPK and A17.status = 2  left join      " +
                            //  " Account A18 on A.RealisedBond = A18.AccountPK and A18.status = 2  left join        " +
                            //  " Account A19 on A.RealisedReksadana = A19.AccountPK and A19.status = 2  left join       " +
                            //  " Account A20 on A.RealisedAset = A20.AccountPK and A20.status = 2  left join      " +
                            //  " Account A21 on A.CadanganEquity = A21.AccountPK and A21.status = 2  left join      " +
                            //  " Account A22 on A.CadanganBond = A22.AccountPK and A22.status = 2  left join        " +
                            //  " Account A23 on A.CadanganReksadana = A23.AccountPK and A23.status = 2  left join       " +
                            //  " Account A24 on A.BrokerageFee = A24.AccountPK and A24.status = 2  left join      " +
                            //  " Account A25 on A.JSXLevyFee = A25.AccountPK and A25.status = 2  left join      " +
                            //  " Account A26 on A.KPEIFee = A26.AccountPK and A26.status = 2  left join      " +
                            //  " Account A27 on A.SalesTax = A27.AccountPK and A27.status = 2  left join      " +
                            //  " Account A28 on A.IncomeTaxArt23 = A28.AccountPK and A28.status = 2  left join      " +
                            //  " Account A29 on A.FixedAsetBuyPPN = A29.AccountPK and A29.status = 2  left join      " +
                            //  " Account A30 on A.FixedAsetSellPPN = A30.AccountPK and A30.status = 2  left join      " +
                            //  " MasterValue MV1 on A.EquitySellMethod = MV1.Code and MV1.status = 2  and MV1.ID ='EquitySellMethod' left join      " +
                            //  " MasterValue MV2 on A.AveragePriority = MV2.Code and MV2.status = 2  and MV2.ID ='AveragePriority' left join      " +
                            //  " CashRef C1 on A.CashBond = C1.CashRefPK and C1.status = 2  left join     " +
                            //  " CashRef C2 on A.CashEquity = C2.CashRefPK and C2.status = 2  left join   " +
                            //  " CashRef C3 on A.CashTD = C3.CashRefPK and C3.status = 2  left join " +
                            //  " Currency CR on A.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2 " +
                            //" where A.AccountingSetupPK = @AccountingSetupPK and A.status = 4 ";

                        @"Select  A1.AccountPK InvInBond,A1.ID InvInBondID,A1.Name InvInBondDesc,A2.AccountPK InvInEquity,A2.ID InvInEquityID,A2.Name InvInEquityDesc,A3.AccountPK InvInTD,A3.ID InvInTDID,A3.Name InvInTDDesc,        
                        A4.AccountPK APPurchaseBond,A4.ID APPurchaseBondID,A4.Name APPurchaseBondDesc,A5.AccountPK APPurchaseEquity,A5.ID APPurchaseEquityID,A5.Name APPurchaseEquityDesc,A6.AccountPK APPurchaseTD,A6.ID APPurchaseTDID,A6.Name APPurchaseTDDesc,        
                        A7.AccountPK ARSellBond,A7.ID ARSellBondID,A7.Name ARSellBondDesc,A8.AccountPK ARSellEquity,A8.ID ARSellEquityID,A8.Name ARSellEquityDesc,A9.AccountPK ARSellTD,A9.ID ARSellTDID,A9.Name ARSellTDDesc,        
                        A10.AccountPK InterestPurchaseBond,A10.ID InterestPurchaseBondID,A10.Name InterestPurchaseBondDesc,A11.AccountPK InterestReceivableBond,A11.ID InterestReceivableBondID,A11.Name InterestReceivableBondDesc,  
                        A12.AccountPK InterestReceivableTD,A12.ID InterestReceivableTDID,A12.Name InterestReceivableTDDesc, A13.AccountPK ForeignExchangeRevalAccount,A13.ID ForeignExchangeRevalAccountID,A13.Name ForeignExchangeRevalAccountDesc,       
                        A14.AccountPK UnrealisedEquity,A14.ID UnrealisedEquityID,A14.Name UnrealisedEquityDesc, A15.AccountPK UnrealisedBond,A15.ID UnrealisedBondID,A15.Name UnrealisedBondDesc,       
                        A16.AccountPK UnrealisedReksadana,A16.ID UnrealisedReksadanaID,A16.Name UnrealisedReksadanaDesc, A17.AccountPK RealisedEquity,A17.ID RealisedEquityID,A17.Name RealisedEquityDesc,       
                        A18.AccountPK RealisedBond,A18.ID RealisedBondID,A18.Name RealisedBondDesc, A19.AccountPK RealisedReksadana,A19.ID RealisedReksadanaID,A19.Name RealisedReksadanaDesc,       
                        A20.AccountPK RealisedAset,A20.ID RealisedAsetID,A20.Name RealisedAsetDesc, A21.AccountPK CadanganEquity,A21.ID CadanganEquityID,A21.Name CadanganEquityDesc,       
                        A22.AccountPK CadanganBond,A22.ID CadanganBondID,A22.Name CadanganBondDesc, A23.AccountPK CadanganReksadana,A23.ID CadanganReksadanaID,A23.Name CadanganReksadanaDesc,       
                        A24.AccountPK BrokerageFee,A24.ID BrokerageFeeID,A24.Name BrokerageFeeDesc, A25.AccountPK JSXLevyFee,A25.ID JSXLevyFeeID,A25.Name JSXLevyFeeDesc,       
                        A26.AccountPK KPEIFee,A26.ID KPEIFeeID,A26.Name KPEIFeeDesc, A27.AccountPK SalesTax,A27.ID SalesTaxID,A27.Name SalesTaxDesc,A28.AccountPK IncomeTaxArt23,A28.ID IncomeTaxArt23ID,A28.Name IncomeTaxArt23Desc,        
                        A29.AccountPK FixedAsetBuyPPN,A29.ID FixedAsetBuyPPNID,A29.Name FixedAsetBuyPPNDesc, A30.AccountPK FixedAsetSellPPN,A30.ID FixedAsetSellPPNID,A30.Name FixedAsetSellPPNDesc,

                        A31.AccountPK Income,A31.ID IncomeID,A31.Name IncomeDesc,
                        A32.AccountPK Expense,A32.ID ExpenseID,A32.Name ExpenseDesc,
                        A33.AccountPK PPHFinal,A33.ID PPHFinalID,A33.Name PPHFinalDesc,
                        A34.AccountPK InvInBondGovIDR,A34.ID InvInBondGovIDRID,A34.Name InvInBondGovIDRDesc,
                        A35.AccountPK InvInBondGovUSD,A35.ID InvInBondGovUSDID,A35.Name InvInBondGovUSDDesc,
                        A36.AccountPK InvInBondCorpIDR,A36.ID InvInBondCorpIDRID,A36.Name InvInBondCorpIDRDesc,
                        A37.AccountPK InvInBondCorpUSD,A37.ID InvInBondCorpUSDID,A37.Name InvInBondCorpUSDDesc,
                        A38.AccountPK InvInReksadana,A38.ID InvInReksadanaID,A38.Name InvInReksadanaDesc,
                        A39.AccountPK InterestReceivableBondGovIDR,A39.ID InterestReceivableBondGovIDRID,A39.Name InterestReceivableBondGovIDRDesc,
                        A40.AccountPK InterestReceivableBondCorpIDR,A40.ID InterestReceivableBondCorpIDRID,A40.Name InterestReceivableBondCorpIDRDesc,
                        A41.AccountPK InterestReceivableBondGovUSD,A41.ID InterestReceivableBondGovUSDID,A41.Name InterestReceivableBondGovUSDDesc,
                        A42.AccountPK InterestReceivableBondCorpUSD,A42.ID InterestReceivableBondCorpUSDID,A42.Name InterestReceivableBondCorpUSDDesc,
                        A43.AccountPK RealisedBondGovIDR,A43.ID RealisedBondGovIDRID,A43.Name RealisedBondGovIDRDesc,
                        A44.AccountPK RealisedBondGovUSD,A44.ID RealisedBondGovUSDID,A44.Name RealisedBondGovUSDDesc,
                        A45.AccountPK RealisedBondCorpIDR,A45.ID RealisedBondCorpIDRID,A45.Name RealisedBondCorpIDRDesc,
                        A46.AccountPK RealisedBondCorpUSD,A46.ID RealisedBondCorpUSDID,A46.Name RealisedBondCorpUSDDesc,  
                        A47.AccountPK ARManagementFee,A47.ID ARManagementFeeID,A47.Name ARManagementFeeDesc,  
                        A48.AccountPK ManagementFeeExpense,A48.ID ManagementFeeExpenseID,A48.Name ManagementFeeExpenseDesc,  
                        A49.AccountPK MKBD06Bank,A49.ID MKBD06BankID,A49.Name MKBD06BankDesc,  
                        A50.AccountPK MKBD06PettyCash,A50.ID MKBD06PettyCashID,A50.Name MKBD06PettyCashDesc,  
                        A51.AccountPK WHTFee,A51.ID WHTFeeID,A51.Name WHTFeeDesc,     
                        A52.AccountPK VATFee,A52.ID VATFeeID,A52.Name VATFeeDesc, 
                        A53.AccountPK TaxARManagementFee,A53.ID TaxARManagementFeeID,A53.Name TaxARManagementFeeDesc,  
                        A54.AccountPK TaxManagementFeeExpense,A54.ID TaxManagementFeeExpenseID,A54.Name TaxManagementFeeExpenseDesc,
                        A55.AccountPK APPurchaseReksadana,A55.ID APPurchaseReksadanaID,A55.Name APPurchaseReksadanaDesc,  
                        A56.AccountPK ARSellReksadana,A56.ID ARSellReksadanaID,A56.Name ARSellReksadanaDesc,  
                        A57.AccountPK TotalEquity,A57.ID TotalEquityID,A57.Name TotalEquityDesc,  
                        A58.AccountPK UnrealisedBondUSD,A58.ID UnrealisedBondIDUSD,A58.Name UnrealisedBondUSD, 
                        A59.AccountPK CadanganBondUSD,A59.ID CadanganBondIDUSD,A59.Name CadanganBondDescUSD,
                        A60.AccountPK UnrealizedAccountSGD,A60.ID UnrealizedAccountSGDID,A60.Name UnrealizedAccountSGDDesc, 
                        A61.AccountPK UnrealizedAccountUSD,A61.ID UnrealizedAccountUSDID,A61.Name UnrealizedAccountUSDDesc, 

                        A62.AccountPK SwitchingFundAcc,A61.ID SwitchingFundAccID,A61.Name SwitchingFundAccDesc, 

                        MV1.DescOne EquitySellMethodDesc,MV2.DescOne AveragePriorityDesc,  
                        C1.CashRefPK CashBond,C1.ID CashBondID,C1.Name CashBondDesc,C2.CashRefPK CashEquity,C2.ID CashEquityID,C2.Name CashEquityDesc,C3.CashRefPK CashTD,C3.ID CashTDID,C3.Name CashTDDesc, CR.ID DefaultCurrencyID,      
                        A.* from AccountingSetup A left join         
                        Account A1 on A.InvInBond = A1.AccountPK and A1.status = 2  left join         
                        Account A2 on A.InvInEquity = A2.AccountPK and A2.status = 2  left join         
                        Account A3 on A.InvInTD = A3.AccountPK and A3.status = 2  left join         
                        Account A4 on A.APPurchaseBond = A4.AccountPK and A4.status = 2  left join         
                        Account A5 on A.APPurchaseEquity = A5.AccountPK and A5.status = 2  left join         
                        Account A6 on A.APPurchaseTD = A6.AccountPK and A6.status = 2  left join         
                        Account A7 on A.ARSellBond = A7.AccountPK and A7.status = 2  left join         
                        Account A8 on A.ARSellEquity = A8.AccountPK and A8.status = 2  left join         
                        Account A9 on A.ARSellTD = A9.AccountPK and A9.status = 2  left join         
                        Account A10 on A.InterestPurchaseBond = A10.AccountPK and A10.status = 2  left join         
                        Account A11 on A.InterestReceivableBond = A11.AccountPK and A11.status = 2  left join        
                        Account A12 on A.InterestReceivableTD = A12.AccountPK and A12.status = 2  left join       
                        Account A13 on A.ForeignExchangeRevalAccount = A13.AccountPK and A13.status = 2  left join       
                        Account A14 on A.UnrealisedEquity = A14.AccountPK and A14.status = 2  left join         
                        Account A15 on A.UnrealisedBond = A15.AccountPK and A15.status = 2  left join        
                        Account A16 on A.UnrealisedReksadana = A16.AccountPK and A16.status = 2  left join       
                        Account A17 on A.RealisedEquity = A17.AccountPK and A17.status = 2  left join       
                        Account A18 on A.RealisedBond = A18.AccountPK and A18.status = 2  left join         
                        Account A19 on A.RealisedReksadana = A19.AccountPK and A19.status = 2  left join        
                        Account A20 on A.RealisedAset = A20.AccountPK and A20.status = 2  left join       
                        Account A21 on A.CadanganEquity = A21.AccountPK and A21.status = 2  left join       
                        Account A22 on A.CadanganBond = A22.AccountPK and A22.status = 2  left join         
                        Account A23 on A.CadanganReksadana = A23.AccountPK and A23.status = 2  left join        
                        Account A24 on A.BrokerageFee = A24.AccountPK and A24.status = 2  left join       
                        Account A25 on A.JSXLevyFee = A25.AccountPK and A25.status = 2  left join       
                        Account A26 on A.KPEIFee = A26.AccountPK and A26.status = 2  left join       
                        Account A27 on A.SalesTax = A27.AccountPK and A27.status = 2  left join       
                        Account A28 on A.IncomeTaxArt23 = A28.AccountPK and A28.status = 2  left join       
                        Account A29 on A.FixedAsetBuyPPN = A29.AccountPK and A29.status = 2  left join       
                        Account A30 on A.FixedAsetSellPPN = A30.AccountPK and A30.status = 2  left join 
 
                        Account A31 on A.Income = A31.AccountPK and A31.status = 2  left join 
                        Account A32 on A.Expense = A32.AccountPK and A32.status = 2  left join 
                        Account A33 on A.PPHFinal = A33.AccountPK and A33.status = 2  left join 
                        Account A34 on A.InvInBondGovIDR = A34.AccountPK and A34.status = 2  left join 
                        Account A35 on A.InvInBondGovUSD = A35.AccountPK and A35.status = 2  left join 
                        Account A36 on A.InvInBondCorpIDR = A36.AccountPK and A36.status = 2  left join 
                        Account A37 on A.InvInBondCorpUSD = A37.AccountPK and A37.status = 2  left join 
                        Account A38 on A.InvInReksadana = A38.AccountPK and A38.status = 2  left join 
                        Account A39 on A.InterestReceivableBondGovIDR = A39.AccountPK and A39.status = 2  left join 
                        Account A40 on A.InterestReceivableBondCorpIDR = A40.AccountPK and A40.status = 2  left join 
                        Account A41 on A.InterestReceivableBondGovUSD = A41.AccountPK and A41.status = 2  left join 
                        Account A42 on A.InterestReceivableBondCorpUSD = A42.AccountPK and A42.status = 2  left join 
                        Account A43 on A.RealisedBondGovIDR = A43.AccountPK and A43.status = 2  left join 
                        Account A44 on A.RealisedBondGovUSD = A44.AccountPK and A44.status = 2  left join 
                        Account A45 on A.RealisedBondCorpIDR = A45.AccountPK and A45.status = 2  left join 
                        Account A46 on A.RealisedBondCorpUSD = A46.AccountPK and A46.status = 2  left join  
      
                        Account A47 on A.ARManagementFee = A47.AccountPK and A47.status = 2  left join  
                        Account A48 on A.ManagementFeeExpense = A48.AccountPK and A48.status = 2  left join  
                        Account A49 on A.MKBD06Bank = A49.AccountPK and A49.status = 2  left join  
                        Account A50 on A.MKBD06PettyCash = A50.AccountPK and A50.status = 2  left join  
                        Account A51 on A.WHTFee = A51.AccountPK and A51.status = 2  left join  
                        Account A52 on A.VATFee = A52.AccountPK and A52.status = 2  left join 
                        Account A53 on A.TaxARManagementFee = A53.AccountPK and A53.status = 2  left join  
                        Account A54 on A.TaxManagementFeeExpense = A54.AccountPK and A54.status = 2  left join
                        Account A55 on A.APPurchaseReksadana = A55.AccountPK and A55.status = 2  left join  
                        Account A56 on A.ARSellReksadana = A56.AccountPK and A56.status = 2  left join   
                        Account A57 on A.TotalEquity = A57.AccountPK and A57.status = 2  left join
                        Account A58 on A.UnrealisedBondUSD = A58.AccountPK and A58.status = 2  left join  
                        Account A59 on A.CadanganBondUSD = A59.AccountPK and A59.status = 2  left join  
                        Account A60 on A.UnrealizedAccountSGD = A60.AccountPK and A60.status = 2  left join  
                        Account A61 on A.UnrealizedAccountUSD = A61.AccountPK and A61.status = 2  left join  

                         Account A62 on A.SwitchingFundAcc = A61.AccountPK and A61.status = 2  left join                          

                        MasterValue MV1 on A.EquitySellMethod = MV1.Code and MV1.status = 2  and MV1.ID ='EquitySellMethod' left join       
                        MasterValue MV2 on A.AveragePriority = MV2.Code and MV2.status = 2  and MV2.ID ='AveragePriority' left join       
                        CashRef C1 on A.CashBond = C1.CashRefPK and C1.status = 2  left join      
                        CashRef C2 on A.CashEquity = C2.CashRefPK and C2.status = 2  left join    
                        CashRef C3 on A.CashTD = C3.CashRefPK and C3.status = 2  left join  
                        Currency CR on A.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2  
                        where A.AccountingSetupPK = @AccountingSetupPK and A.status = 4";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@AccountingSetupPK", _accountingSetupPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setAccountingSetup(dr);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //5
        public int AccountingSetup_Add(AccountingSetup _accountingSetup, bool _havePrivillege)
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
                                 "Select isnull(max(AccountingSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from AccountingSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _accountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(AccountingSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from AccountingSetup";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Income", _accountingSetup.Income);
                        cmd.Parameters.AddWithValue("@Expense", _accountingSetup.Expense);
                        cmd.Parameters.AddWithValue("@PPHFinal", _accountingSetup.PPHFinal);
                        cmd.Parameters.AddWithValue("@InvInBond", _accountingSetup.InvInBond);
                        cmd.Parameters.AddWithValue("@InvInBondGovIDR", _accountingSetup.InvInBondGovIDR);
                        cmd.Parameters.AddWithValue("@InvInBondGovUSD", _accountingSetup.InvInBondGovUSD);
                        cmd.Parameters.AddWithValue("@InvInBondCorpIDR", _accountingSetup.InvInBondCorpIDR);
                        cmd.Parameters.AddWithValue("@InvInBondCorpUSD", _accountingSetup.InvInBondCorpUSD);
                        cmd.Parameters.AddWithValue("@InvInEquity", _accountingSetup.InvInEquity);
                        cmd.Parameters.AddWithValue("@InvInTD", _accountingSetup.InvInTD);
                        cmd.Parameters.AddWithValue("@InvInTDUSD", _accountingSetup.InvInTDUSD);
                        cmd.Parameters.AddWithValue("@InvInReksadana", _accountingSetup.InvInReksadana);
                        cmd.Parameters.AddWithValue("@APPurchaseBond", _accountingSetup.APPurchaseBond);
                        cmd.Parameters.AddWithValue("@APPurchaseEquity", _accountingSetup.APPurchaseEquity);
                        cmd.Parameters.AddWithValue("@APPurchaseTD", _accountingSetup.APPurchaseTD);
                        cmd.Parameters.AddWithValue("@ARSellBond", _accountingSetup.ARSellBond);
                        cmd.Parameters.AddWithValue("@ARSellEquity", _accountingSetup.ARSellEquity);
                        cmd.Parameters.AddWithValue("@ARSellTD", _accountingSetup.ARSellTD);
                        cmd.Parameters.AddWithValue("@InterestPurchaseBond", _accountingSetup.InterestPurchaseBond);
                        cmd.Parameters.AddWithValue("@InterestReceivableBond", _accountingSetup.InterestReceivableBond);
                        cmd.Parameters.AddWithValue("@InterestReceivableBondGovIDR", _accountingSetup.InterestReceivableBondGovIDR);
                        cmd.Parameters.AddWithValue("@InterestReceivableBondCorpIDR", _accountingSetup.InterestReceivableBondCorpIDR);
                        cmd.Parameters.AddWithValue("@InterestReceivableBondGovUSD", _accountingSetup.InterestReceivableBondGovUSD);
                        cmd.Parameters.AddWithValue("@InterestReceivableBondCorpUSD", _accountingSetup.InterestReceivableBondCorpUSD);
                        cmd.Parameters.AddWithValue("@InterestReceivableTD", _accountingSetup.InterestReceivableTD);
                        cmd.Parameters.AddWithValue("@CashBond", _accountingSetup.CashBond);
                        cmd.Parameters.AddWithValue("@CashEquity", _accountingSetup.CashEquity);
                        cmd.Parameters.AddWithValue("@CashTD", _accountingSetup.CashTD);
                        cmd.Parameters.AddWithValue("@ForeignExchangeRevalAccount", _accountingSetup.ForeignExchangeRevalAccount);
                        cmd.Parameters.AddWithValue("@UnrealisedEquity", _accountingSetup.UnrealisedEquity);
                        cmd.Parameters.AddWithValue("@UnrealisedBond", _accountingSetup.UnrealisedBond);
                        cmd.Parameters.AddWithValue("@UnrealisedReksadana", _accountingSetup.UnrealisedReksadana);
                        cmd.Parameters.AddWithValue("@RealisedEquity", _accountingSetup.RealisedEquity);
                        cmd.Parameters.AddWithValue("@RealisedBondGovIDR", _accountingSetup.RealisedBondGovIDR);
                        cmd.Parameters.AddWithValue("@RealisedBondGovUSD", _accountingSetup.RealisedBondGovUSD);
                        cmd.Parameters.AddWithValue("@RealisedBondCorpIDR", _accountingSetup.RealisedBondCorpIDR);
                        cmd.Parameters.AddWithValue("@RealisedBondCorpUSD", _accountingSetup.RealisedBondCorpUSD);
                        cmd.Parameters.AddWithValue("@RealisedBond", _accountingSetup.RealisedBond);
                        cmd.Parameters.AddWithValue("@RealisedReksadana", _accountingSetup.RealisedReksadana);
                        cmd.Parameters.AddWithValue("@RealisedAset", _accountingSetup.RealisedAset);
                        cmd.Parameters.AddWithValue("@CadanganEquity", _accountingSetup.CadanganEquity);
                        cmd.Parameters.AddWithValue("@CadanganBond", _accountingSetup.CadanganBond);
                        cmd.Parameters.AddWithValue("@CadanganReksadana", _accountingSetup.CadanganReksadana);
                        cmd.Parameters.AddWithValue("@BrokerageFee", _accountingSetup.BrokerageFee);
                        cmd.Parameters.AddWithValue("@JSXLevyFee", _accountingSetup.JSXLevyFee);
                        cmd.Parameters.AddWithValue("@KPEIFee", _accountingSetup.KPEIFee);
                        cmd.Parameters.AddWithValue("@VATFee", _accountingSetup.VATFee);
                        cmd.Parameters.AddWithValue("@SalesTax", _accountingSetup.SalesTax);
                        cmd.Parameters.AddWithValue("@IncomeTaxArt23", _accountingSetup.IncomeTaxArt23);
                        cmd.Parameters.AddWithValue("@EquitySellMethod", _accountingSetup.EquitySellMethod);
                        cmd.Parameters.AddWithValue("@AveragePriority", _accountingSetup.AveragePriority);
                        cmd.Parameters.AddWithValue("@FixedAsetBuyPPN", _accountingSetup.FixedAsetBuyPPN);
                        cmd.Parameters.AddWithValue("@FixedAsetSellPPN", _accountingSetup.FixedAsetSellPPN);
                        cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _accountingSetup.DefaultCurrencyPK);
                        cmd.Parameters.AddWithValue("@DecimalPlaces", _accountingSetup.DecimalPlaces);
                        cmd.Parameters.AddWithValue("@TotalEquity", _accountingSetup.TotalEquity);

                        cmd.Parameters.AddWithValue("@ARManagementFee", _accountingSetup.ARManagementFee);
                        cmd.Parameters.AddWithValue("@ManagementFeeExpense", _accountingSetup.ManagementFeeExpense);
                        cmd.Parameters.AddWithValue("@TaxARManagementFee", _accountingSetup.TaxARManagementFee);
                        cmd.Parameters.AddWithValue("@TaxManagementFeeExpense", _accountingSetup.TaxManagementFeeExpense);
                        cmd.Parameters.AddWithValue("@MKBD06Bank", _accountingSetup.MKBD06Bank);
                        cmd.Parameters.AddWithValue("@MKBD06PettyCash", _accountingSetup.MKBD06PettyCash);
                        cmd.Parameters.AddWithValue("@WHTFee", _accountingSetup.WHTFee);
                        cmd.Parameters.AddWithValue("@APPurchaseReksadana", _accountingSetup.APPurchaseReksadana);
                        cmd.Parameters.AddWithValue("@ARSellReksadana", _accountingSetup.ARSellReksadana);
                        cmd.Parameters.AddWithValue("@UnrealisedBondUSD", _accountingSetup.UnrealisedBondUSD);
                        cmd.Parameters.AddWithValue("@CadanganBondUSD", _accountingSetup.CadanganBondUSD);

                        cmd.Parameters.AddWithValue("@UnrealizedAccountSGD", _accountingSetup.UnrealizedAccountSGD);
                        cmd.Parameters.AddWithValue("@UnrealizedAccountUSD", _accountingSetup.UnrealizedAccountUSD);

                        cmd.Parameters.AddWithValue("@ARSubscriptionFee", _accountingSetup.ARSubscriptionFee);
                        cmd.Parameters.AddWithValue("@SubscriptionFeeIncome", _accountingSetup.SubscriptionFeeIncome);
                        cmd.Parameters.AddWithValue("@ARRedemptionFee", _accountingSetup.ARRedemptionFee);
                        cmd.Parameters.AddWithValue("@RedemptionFeeIncome", _accountingSetup.RedemptionFeeIncome);
                        cmd.Parameters.AddWithValue("@ARSwitchingFee", _accountingSetup.ARSwitchingFee);
                        cmd.Parameters.AddWithValue("@SwitchingFeeIncome", _accountingSetup.SwitchingFeeIncome);

                        cmd.Parameters.AddWithValue("@AgentCommissionExpense", _accountingSetup.AgentCommissionExpense);
                        cmd.Parameters.AddWithValue("@AgentCommissionPayable", _accountingSetup.AgentCommissionPayable);
                        cmd.Parameters.AddWithValue("@WHTPayablePPH21", _accountingSetup.WHTPayablePPH21);
                        cmd.Parameters.AddWithValue("@WHTPayablePPH23", _accountingSetup.WHTPayablePPH23);
                        cmd.Parameters.AddWithValue("@VatIn", _accountingSetup.VatIn);
                        cmd.Parameters.AddWithValue("@VatOut", _accountingSetup.VatOut);
                        cmd.Parameters.AddWithValue("@AgentCommissionCash", _accountingSetup.AgentCommissionCash);

                        cmd.Parameters.AddWithValue("@SwitchingFundAcc", _accountingSetup.SwitchingFundAcc);

                        cmd.Parameters.AddWithValue("@AgentCSRExpense", _accountingSetup.AgentCSRExpense);
                        cmd.Parameters.AddWithValue("@AgentCSRPayable", _accountingSetup.AgentCSRPayable);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _accountingSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AccountingSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        //6
        public int AccountingSetup_Update(AccountingSetup _accountingSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_accountingSetup.AccountingSetupPK, _accountingSetup.HistoryPK, "AccountingSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update AccountingSetup set status=2,Notes=@Notes,Income=@Income,Expense=@Expense,PPHFinal=@PPHFinal,InvInBond=@InvInBond,InvInBondGovIDR=@InvInBondGovIDR,InvInBondGovUSD=@InvInBondGovUSD,InvInBondCorpIDR=@InvInBondCorpIDR,InvInBondCorpUSD=@InvInBondCorpUSD,InvInEquity=@InvInEquity,InvInTD=@InvInTD," +
                                "InvInReksadana=@InvInReksadana,APPurchaseBond=@APPurchaseBond,APPurchaseEquity=@APPurchaseEquity,APPurchaseTD=@APPurchaseTD,ARSellBond=@ARSellBond,ARSellEquity=@ARSellEquity," +
                                "ARSellTD=@ARSellTD,InterestPurchaseBond=@InterestPurchaseBond,InterestReceivableBond=@InterestReceivableBond,InterestReceivableBondGovIDR=@InterestReceivableBondGovIDR,InterestReceivableBondCorpIDR=@InterestReceivableBondCorpIDR,InterestReceivableBondGovUSD=@InterestReceivableBondGovUSD,InterestReceivableBondCorpUSD=@InterestReceivableBondCorpUSD," +
                                "InterestReceivableTD=@InterestReceivableTD,CashBond=@CashBond,CashEquity=@CashEquity,CashTD=@CashTD,ForeignExchangeRevalAccount=@ForeignExchangeRevalAccount, " +
                                "UnrealisedEquity=@UnrealisedEquity,UnrealisedBond=@UnrealisedBond,UnrealisedReksadana=@UnrealisedReksadana,RealisedEquity=@RealisedEquity,RealisedBondGovIDR=@RealisedBondGovIDR,RealisedBondGovUSD=@RealisedBondGovUSD,RealisedBondCorpIDR=@RealisedBondCorpIDR,RealisedBondCorpUSD=@RealisedBondCorpUSD,RealisedBond=@RealisedBond, " +
                                "RealisedReksadana=@RealisedReksadana,RealisedAset=@RealisedAset,CadanganEquity=@CadanganEquity,CadanganBond=@CadanganBond,CadanganReksadana=@CadanganReksadana, " +
                                "BrokerageFee=@BrokerageFee,JSXLevyFee=@JSXLevyFee,KPEIFee=@KPEIFee,SalesTax=@SalesTax,IncomeTaxArt23=@IncomeTaxArt23, " +
                                "EquitySellMethod=@EquitySellMethod,AveragePriority=@AveragePriority,FixedAsetBuyPPN=@FixedAsetBuyPPN,FixedAsetSellPPN=@FixedAsetSellPPN, " +
                                "DefaultCurrencyPK=@DefaultCurrencyPK,DecimalPlaces=@DecimalPlaces,ARManagementFee=@ARManagementFee,ManagementFeeExpense=@ManagementFeeExpense,MKBD06Bank=@MKBD06Bank,MKBD06PettyCash=@MKBD06PettyCash,WHTFee=@WHTFee,VATFee=@VATFee, TaxARManagementFee=@TaxARManagementFee,TaxManagementFeeExpense=@TaxManagementFeeExpense,APPurchaseReksadana=@APPurchaseReksadana,ARSellReksadana=@ARSellReksadana,TotalEquity=@TotalEquity,UnrealisedBondUSD=@UnrealisedBondUSD,CadanganBondUSD=@CadanganBondUSD,UnrealizedAccountSGD=@UnrealizedAccountSGD,UnrealizedAccountUSD=@UnrealizedAccountUSD," +
                                "InvInTDUSD=@InvInTDUSD,ARSubscriptionFee=@ARSubscriptionFee,SubscriptionFeeIncome=@SubscriptionFeeIncome,ARRedemptionFee=@ARRedemptionFee,RedemptionFeeIncome=@RedemptionFeeIncome,ARSwitchingFee=@ARSwitchingFee,SwitchingFeeIncome=@SwitchingFeeIncome,AgentCommissionExpense=@AgentCommissionExpense,AgentCommissionPayable=@AgentCommissionPayable,WHTPayablePPH21=@WHTPayablePPH21,WHTPayablePPH23=@WHTPayablePPH23,VatIn=@VatIn,VatOut=@VatOut,AgentCommissionCash=@AgentCommissionCash,SwitchingFundAcc=@SwitchingFundAcc,AgentCSRExpense=@AgentCSRExpense,AgentCSRPayable=@AgentCSRPayable," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where AccountingSetupPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _accountingSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _accountingSetup.Notes);
                            cmd.Parameters.AddWithValue("@Income", _accountingSetup.Income);
                            cmd.Parameters.AddWithValue("@Expense", _accountingSetup.Expense);
                            cmd.Parameters.AddWithValue("@PPHFinal", _accountingSetup.PPHFinal);
                            cmd.Parameters.AddWithValue("@InvInBond", _accountingSetup.InvInBond);
                            cmd.Parameters.AddWithValue("@InvInBondGovIDR", _accountingSetup.InvInBondGovIDR);
                            cmd.Parameters.AddWithValue("@InvInBondGovUSD", _accountingSetup.InvInBondGovUSD);
                            cmd.Parameters.AddWithValue("@InvInBondCorpIDR", _accountingSetup.InvInBondCorpIDR);
                            cmd.Parameters.AddWithValue("@InvInBondCorpUSD", _accountingSetup.InvInBondCorpUSD);
                            cmd.Parameters.AddWithValue("@InvInEquity", _accountingSetup.InvInEquity);
                            cmd.Parameters.AddWithValue("@InvInTD", _accountingSetup.InvInTD);
                            cmd.Parameters.AddWithValue("@InvInTDUSD", _accountingSetup.InvInTDUSD);
                            cmd.Parameters.AddWithValue("@InvInReksadana", _accountingSetup.InvInReksadana);
                            cmd.Parameters.AddWithValue("@APPurchaseBond", _accountingSetup.APPurchaseBond);
                            cmd.Parameters.AddWithValue("@APPurchaseEquity", _accountingSetup.APPurchaseEquity);
                            cmd.Parameters.AddWithValue("@APPurchaseTD", _accountingSetup.APPurchaseTD);
                            cmd.Parameters.AddWithValue("@ARSellBond", _accountingSetup.ARSellBond);
                            cmd.Parameters.AddWithValue("@ARSellEquity", _accountingSetup.ARSellEquity);
                            cmd.Parameters.AddWithValue("@ARSellTD", _accountingSetup.ARSellTD);
                            cmd.Parameters.AddWithValue("@InterestPurchaseBond", _accountingSetup.InterestPurchaseBond);
                            cmd.Parameters.AddWithValue("@InterestReceivableBond", _accountingSetup.InterestReceivableBond);
                            cmd.Parameters.AddWithValue("@InterestReceivableBondGovIDR", _accountingSetup.InterestReceivableBondGovIDR);
                            cmd.Parameters.AddWithValue("@InterestReceivableBondCorpIDR", _accountingSetup.InterestReceivableBondCorpIDR);
                            cmd.Parameters.AddWithValue("@InterestReceivableBondGovUSD", _accountingSetup.InterestReceivableBondGovUSD);
                            cmd.Parameters.AddWithValue("@InterestReceivableBondCorpUSD", _accountingSetup.InterestReceivableBondCorpUSD);
                            cmd.Parameters.AddWithValue("@InterestReceivableTD", _accountingSetup.InterestReceivableTD);
                            cmd.Parameters.AddWithValue("@CashBond", _accountingSetup.CashBond);
                            cmd.Parameters.AddWithValue("@CashEquity", _accountingSetup.CashEquity);
                            cmd.Parameters.AddWithValue("@CashTD", _accountingSetup.CashTD);
                            cmd.Parameters.AddWithValue("@ForeignExchangeRevalAccount", _accountingSetup.ForeignExchangeRevalAccount);
                            cmd.Parameters.AddWithValue("@UnrealisedEquity", _accountingSetup.UnrealisedEquity);
                            cmd.Parameters.AddWithValue("@UnrealisedBond", _accountingSetup.UnrealisedBond);
                            cmd.Parameters.AddWithValue("@UnrealisedReksadana", _accountingSetup.UnrealisedReksadana);
                            cmd.Parameters.AddWithValue("@RealisedEquity", _accountingSetup.RealisedEquity);
                            cmd.Parameters.AddWithValue("@RealisedBondGovIDR", _accountingSetup.RealisedBondGovIDR);
                            cmd.Parameters.AddWithValue("@RealisedBondGovUSD", _accountingSetup.RealisedBondGovUSD);
                            cmd.Parameters.AddWithValue("@RealisedBondCorpIDR", _accountingSetup.RealisedBondCorpIDR);
                            cmd.Parameters.AddWithValue("@RealisedBondCorpUSD", _accountingSetup.RealisedBondCorpUSD);
                            cmd.Parameters.AddWithValue("@RealisedBond", _accountingSetup.RealisedBond);
                            cmd.Parameters.AddWithValue("@RealisedReksadana", _accountingSetup.RealisedReksadana);
                            cmd.Parameters.AddWithValue("@RealisedAset", _accountingSetup.RealisedAset);
                            cmd.Parameters.AddWithValue("@CadanganEquity", _accountingSetup.CadanganEquity);
                            cmd.Parameters.AddWithValue("@CadanganBond", _accountingSetup.CadanganBond);
                            cmd.Parameters.AddWithValue("@CadanganReksadana", _accountingSetup.CadanganReksadana);
                            cmd.Parameters.AddWithValue("@BrokerageFee", _accountingSetup.BrokerageFee);
                            cmd.Parameters.AddWithValue("@JSXLevyFee", _accountingSetup.JSXLevyFee);
                            cmd.Parameters.AddWithValue("@KPEIFee", _accountingSetup.KPEIFee);
                            cmd.Parameters.AddWithValue("@VATFee", _accountingSetup.VATFee);
                            cmd.Parameters.AddWithValue("@SalesTax", _accountingSetup.SalesTax);
                            cmd.Parameters.AddWithValue("@IncomeTaxArt23", _accountingSetup.IncomeTaxArt23);
                            cmd.Parameters.AddWithValue("@EquitySellMethod", _accountingSetup.EquitySellMethod);
                            cmd.Parameters.AddWithValue("@AveragePriority", _accountingSetup.AveragePriority);
                            cmd.Parameters.AddWithValue("@FixedAsetBuyPPN", _accountingSetup.FixedAsetBuyPPN);
                            cmd.Parameters.AddWithValue("@FixedAsetSellPPN", _accountingSetup.FixedAsetSellPPN);
                            cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _accountingSetup.DefaultCurrencyPK);
                            cmd.Parameters.AddWithValue("@DecimalPlaces", _accountingSetup.DecimalPlaces);

                            cmd.Parameters.AddWithValue("@ARManagementFee", _accountingSetup.ARManagementFee);
                            cmd.Parameters.AddWithValue("@ManagementFeeExpense", _accountingSetup.ManagementFeeExpense);
                            cmd.Parameters.AddWithValue("@TaxARManagementFee", _accountingSetup.TaxARManagementFee);
                            cmd.Parameters.AddWithValue("@TaxManagementFeeExpense", _accountingSetup.TaxManagementFeeExpense);
                            cmd.Parameters.AddWithValue("@MKBD06Bank", _accountingSetup.MKBD06Bank);
                            cmd.Parameters.AddWithValue("@MKBD06PettyCash", _accountingSetup.MKBD06PettyCash);
                            cmd.Parameters.AddWithValue("@WHTFee", _accountingSetup.WHTFee);
                            cmd.Parameters.AddWithValue("@APPurchaseReksadana", _accountingSetup.APPurchaseReksadana);
                            cmd.Parameters.AddWithValue("@ARSellReksadana", _accountingSetup.ARSellReksadana);
                            cmd.Parameters.AddWithValue("@TotalEquity", _accountingSetup.TotalEquity);
                            cmd.Parameters.AddWithValue("@UnrealisedBondUSD", _accountingSetup.UnrealisedBondUSD);
                            cmd.Parameters.AddWithValue("@CadanganBondUSD", _accountingSetup.CadanganBondUSD);

                            cmd.Parameters.AddWithValue("@UnrealizedAccountSGD", _accountingSetup.UnrealizedAccountSGD);
                            cmd.Parameters.AddWithValue("@UnrealizedAccountUSD", _accountingSetup.UnrealizedAccountUSD);

                            cmd.Parameters.AddWithValue("@ARSubscriptionFee", _accountingSetup.ARSubscriptionFee);
                            cmd.Parameters.AddWithValue("@SubscriptionFeeIncome", _accountingSetup.SubscriptionFeeIncome);
                            cmd.Parameters.AddWithValue("@ARRedemptionFee", _accountingSetup.ARRedemptionFee);
                            cmd.Parameters.AddWithValue("@RedemptionFeeIncome", _accountingSetup.RedemptionFeeIncome);
                            cmd.Parameters.AddWithValue("@ARSwitchingFee", _accountingSetup.ARSwitchingFee);
                            cmd.Parameters.AddWithValue("@SwitchingFeeIncome", _accountingSetup.SwitchingFeeIncome);

                            cmd.Parameters.AddWithValue("@AgentCommissionExpense", _accountingSetup.AgentCommissionExpense);
                            cmd.Parameters.AddWithValue("@AgentCommissionPayable", _accountingSetup.AgentCommissionPayable);
                            cmd.Parameters.AddWithValue("@WHTPayablePPH21", _accountingSetup.WHTPayablePPH21);
                            cmd.Parameters.AddWithValue("@WHTPayablePPH23", _accountingSetup.WHTPayablePPH23);
                            cmd.Parameters.AddWithValue("@VatIn", _accountingSetup.VatIn);
                            cmd.Parameters.AddWithValue("@VatOut", _accountingSetup.VatOut);
                            cmd.Parameters.AddWithValue("@AgentCommissionCash", _accountingSetup.AgentCommissionCash);

                            cmd.Parameters.AddWithValue("@SwitchingFundAcc", _accountingSetup.SwitchingFundAcc);

                            cmd.Parameters.AddWithValue("@AgentCSRExpense", _accountingSetup.AgentCSRExpense);
                            cmd.Parameters.AddWithValue("@AgentCSRPayable", _accountingSetup.AgentCSRPayable);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _accountingSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AccountingSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where AccountingSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _accountingSetup.EntryUsersID);
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
                                cmd.CommandText = "Update AccountingSetup set Notes=@Notes,Income=@Income,Expense=@Expense,PPHFinal=@PPHFinal,InvInBond=@InvInBond,InvInBondGovIDR=@InvInBondGovIDR,InvInBondGovUSD=@InvInBondGovUSD,InvInBondCorpIDR=@InvInBondCorpIDR,InvInBondCorpUSD=@InvInBondCorpUSD,InvInEquity=@InvInEquity,InvInTD=@InvInTD," +
                                "InvInReksadana=@InvInReksadana,APPurchaseBond=@APPurchaseBond,APPurchaseEquity=@APPurchaseEquity,APPurchaseTD=@APPurchaseTD,ARSellBond=@ARSellBond,ARSellEquity=@ARSellEquity," +
                                "ARSellTD=@ARSellTD,InterestPurchaseBond=@InterestPurchaseBond,InterestReceivableBond=@InterestReceivableBond,InterestReceivableBondGovIDR=@InterestReceivableBondGovIDR,InterestReceivableBondCorpIDR=@InterestReceivableBondCorpIDR,InterestReceivableBondGovUSD=@InterestReceivableBondGovUSD,InterestReceivableBondCorpUSD=@InterestReceivableBondCorpUSD," +
                                "InterestReceivableTD=@InterestReceivableTD,CashBond=@CashBond,CashEquity=@CashEquity,CashTD=@CashTD,ForeignExchangeRevalAccount=@ForeignExchangeRevalAccount, " +
                                "UnrealisedEquity=@UnrealisedEquity,UnrealisedBond=@UnrealisedBond,UnrealisedReksadana=@UnrealisedReksadana,RealisedEquity=@RealisedEquity,RealisedBondGovIDR=@RealisedBondGovIDR,RealisedBondGovUSD=@RealisedBondGovUSD,RealisedBondCorpIDR=@RealisedBondCorpIDR,RealisedBondCorpUSD=@RealisedBondCorpUSD,RealisedBond=@RealisedBond, " +
                                "RealisedReksadana=@RealisedReksadana,RealisedAset=@RealisedAset,CadanganEquity=@CadanganEquity,CadanganBond=@CadanganBond,CadanganReksadana=@CadanganReksadana, " +
                                "BrokerageFee=@BrokerageFee,JSXLevyFee=@JSXLevyFee,KPEIFee=@KPEIFee,SalesTax=@SalesTax,IncomeTaxArt23=@IncomeTaxArt23, " +
                                "EquitySellMethod=@EquitySellMethod,AveragePriority=@AveragePriority,FixedAsetBuyPPN=@FixedAsetBuyPPN,FixedAsetSellPPN=@FixedAsetSellPPN, " +
                                "DefaultCurrencyPK=@DefaultCurrencyPK,DecimalPlaces=@DecimalPlaces,ARManagementFee=@ARManagementFee,ManagementFeeExpense=@ManagementFeeExpense,MKBD06Bank=@MKBD06Bank,MKBD06PettyCash=@MKBD06PettyCash,WHTFee=@WHTFee,VATFee=@VATFee, TaxARManagementFee=@TaxARManagementFee,TaxManagementFeeExpense=@TaxManagementFeeExpense,APPurchaseReksadana=@APPurchaseReksadana,ARSellReksadana=@ARSellReksadana,TotalEquity=@TotalEquity,UnrealisedBondUSD=@UnrealisedBondUSD,CadanganBondUSD=@CadanganBondUSD,UnrealizedAccountSGD=@UnrealizedAccountSGD,UnrealizedAccountUSD=@UnrealizedAccountUSD," +
                                "InvInTDUSD=@InvInTDUSD,ARSubscriptionFee=@ARSubscriptionFee,SubscriptionFeeIncome=@SubscriptionFeeIncome,ARRedemptionFee=@ARRedemptionFee,RedemptionFeeIncome=@RedemptionFeeIncome,ARSwitchingFee=@ARSwitchingFee,SwitchingFeeIncome=@SwitchingFeeIncome,AgentCommissionExpense=@AgentCommissionExpense,AgentCommissionPayable=@AgentCommissionPayable,WHTPayablePPH21=@WHTPayablePPH21,WHTPayablePPH23=@WHTPayablePPH23,VatIn=@VatIn,VatOut=@VatOut,AgentCommissionCash=@AgentCommissionCash,SwitchingFundAcc=@SwitchingFundAcc,AgentCSRExpense=@AgentCSRExpense,AgentCSRPayable=@AgentCSRPayable," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where AccountingSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _accountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _accountingSetup.Notes);
                                cmd.Parameters.AddWithValue("@Income", _accountingSetup.Income);
                                cmd.Parameters.AddWithValue("@Expense", _accountingSetup.Expense);
                                cmd.Parameters.AddWithValue("@PPHFinal", _accountingSetup.PPHFinal);
                                cmd.Parameters.AddWithValue("@InvInBond", _accountingSetup.InvInBond);
                                cmd.Parameters.AddWithValue("@InvInBondGovIDR", _accountingSetup.InvInBondGovIDR);
                                cmd.Parameters.AddWithValue("@InvInBondGovUSD", _accountingSetup.InvInBondGovUSD);
                                cmd.Parameters.AddWithValue("@InvInBondCorpIDR", _accountingSetup.InvInBondCorpIDR);
                                cmd.Parameters.AddWithValue("@InvInBondCorpUSD", _accountingSetup.InvInBondCorpUSD);
                                cmd.Parameters.AddWithValue("@InvInEquity", _accountingSetup.InvInEquity);
                                cmd.Parameters.AddWithValue("@InvInTD", _accountingSetup.InvInTD);
                                cmd.Parameters.AddWithValue("@InvInTDUSD", _accountingSetup.InvInTDUSD);
                                cmd.Parameters.AddWithValue("@InvInReksadana", _accountingSetup.InvInReksadana);
                                cmd.Parameters.AddWithValue("@APPurchaseBond", _accountingSetup.APPurchaseBond);
                                cmd.Parameters.AddWithValue("@APPurchaseEquity", _accountingSetup.APPurchaseEquity);
                                cmd.Parameters.AddWithValue("@APPurchaseTD", _accountingSetup.APPurchaseTD);
                                cmd.Parameters.AddWithValue("@ARSellBond", _accountingSetup.ARSellBond);
                                cmd.Parameters.AddWithValue("@ARSellEquity", _accountingSetup.ARSellEquity);
                                cmd.Parameters.AddWithValue("@ARSellTD", _accountingSetup.ARSellTD);
                                cmd.Parameters.AddWithValue("@InterestPurchaseBond", _accountingSetup.InterestPurchaseBond);
                                cmd.Parameters.AddWithValue("@InterestReceivableBond", _accountingSetup.InterestReceivableBond);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondGovIDR", _accountingSetup.InterestReceivableBondGovIDR);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondCorpIDR", _accountingSetup.InterestReceivableBondCorpIDR);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondGovUSD", _accountingSetup.InterestReceivableBondGovUSD);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondCorpUSD", _accountingSetup.InterestReceivableBondCorpUSD);
                                cmd.Parameters.AddWithValue("@InterestReceivableTD", _accountingSetup.InterestReceivableTD);
                                cmd.Parameters.AddWithValue("@CashBond", _accountingSetup.CashBond);
                                cmd.Parameters.AddWithValue("@CashEquity", _accountingSetup.CashEquity);
                                cmd.Parameters.AddWithValue("@CashTD", _accountingSetup.CashTD);
                                cmd.Parameters.AddWithValue("@ForeignExchangeRevalAccount", _accountingSetup.ForeignExchangeRevalAccount);
                                cmd.Parameters.AddWithValue("@UnrealisedEquity", _accountingSetup.UnrealisedEquity);
                                cmd.Parameters.AddWithValue("@UnrealisedBond", _accountingSetup.UnrealisedBond);
                                cmd.Parameters.AddWithValue("@UnrealisedReksadana", _accountingSetup.UnrealisedReksadana);
                                cmd.Parameters.AddWithValue("@RealisedEquity", _accountingSetup.RealisedEquity);
                                cmd.Parameters.AddWithValue("@RealisedBondGovIDR", _accountingSetup.RealisedBondGovIDR);
                                cmd.Parameters.AddWithValue("@RealisedBondGovUSD", _accountingSetup.RealisedBondGovUSD);
                                cmd.Parameters.AddWithValue("@RealisedBondCorpIDR", _accountingSetup.RealisedBondCorpIDR);
                                cmd.Parameters.AddWithValue("@RealisedBondCorpUSD", _accountingSetup.RealisedBondCorpUSD);
                                cmd.Parameters.AddWithValue("@RealisedBond", _accountingSetup.RealisedBond);
                                cmd.Parameters.AddWithValue("@RealisedReksadana", _accountingSetup.RealisedReksadana);
                                cmd.Parameters.AddWithValue("@RealisedAset", _accountingSetup.RealisedAset);
                                cmd.Parameters.AddWithValue("@CadanganEquity", _accountingSetup.CadanganEquity);
                                cmd.Parameters.AddWithValue("@CadanganBond", _accountingSetup.CadanganBond);
                                cmd.Parameters.AddWithValue("@CadanganReksadana", _accountingSetup.CadanganReksadana);
                                cmd.Parameters.AddWithValue("@BrokerageFee", _accountingSetup.BrokerageFee);
                                cmd.Parameters.AddWithValue("@JSXLevyFee", _accountingSetup.JSXLevyFee);
                                cmd.Parameters.AddWithValue("@KPEIFee", _accountingSetup.KPEIFee);
                                cmd.Parameters.AddWithValue("@VATFee", _accountingSetup.VATFee);
                                cmd.Parameters.AddWithValue("@SalesTax", _accountingSetup.SalesTax);
                                cmd.Parameters.AddWithValue("@IncomeTaxArt23", _accountingSetup.IncomeTaxArt23);
                                cmd.Parameters.AddWithValue("@EquitySellMethod", _accountingSetup.EquitySellMethod);
                                cmd.Parameters.AddWithValue("@AveragePriority", _accountingSetup.AveragePriority);
                                cmd.Parameters.AddWithValue("@FixedAsetBuyPPN", _accountingSetup.FixedAsetBuyPPN);
                                cmd.Parameters.AddWithValue("@FixedAsetSellPPN", _accountingSetup.FixedAsetSellPPN);
                                cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _accountingSetup.DefaultCurrencyPK);
                                cmd.Parameters.AddWithValue("@DecimalPlaces", _accountingSetup.DecimalPlaces);
                                cmd.Parameters.AddWithValue("@TotalEquity", _accountingSetup.TotalEquity);

                                cmd.Parameters.AddWithValue("@ARManagementFee", _accountingSetup.ARManagementFee);
                                cmd.Parameters.AddWithValue("@ManagementFeeExpense", _accountingSetup.ManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@TaxARManagementFee", _accountingSetup.TaxARManagementFee);
                                cmd.Parameters.AddWithValue("@TaxManagementFeeExpense", _accountingSetup.TaxManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@MKBD06Bank", _accountingSetup.MKBD06Bank);
                                cmd.Parameters.AddWithValue("@MKBD06PettyCash", _accountingSetup.MKBD06PettyCash);
                                cmd.Parameters.AddWithValue("@WHTFee", _accountingSetup.WHTFee);
                                cmd.Parameters.AddWithValue("@APPurchaseReksadana", _accountingSetup.APPurchaseReksadana);
                                cmd.Parameters.AddWithValue("@ARSellReksadana", _accountingSetup.ARSellReksadana);
                                cmd.Parameters.AddWithValue("@UnrealisedBondUSD", _accountingSetup.UnrealisedBondUSD);
                                cmd.Parameters.AddWithValue("@CadanganBondUSD", _accountingSetup.CadanganBondUSD);

                                cmd.Parameters.AddWithValue("@UnrealizedAccountSGD", _accountingSetup.UnrealizedAccountSGD);
                                cmd.Parameters.AddWithValue("@UnrealizedAccountUSD", _accountingSetup.UnrealizedAccountUSD);

                                cmd.Parameters.AddWithValue("@ARSubscriptionFee", _accountingSetup.ARSubscriptionFee);
                                cmd.Parameters.AddWithValue("@SubscriptionFeeIncome", _accountingSetup.SubscriptionFeeIncome);
                                cmd.Parameters.AddWithValue("@ARRedemptionFee", _accountingSetup.ARRedemptionFee);
                                cmd.Parameters.AddWithValue("@RedemptionFeeIncome", _accountingSetup.RedemptionFeeIncome);
                                cmd.Parameters.AddWithValue("@ARSwitchingFee", _accountingSetup.ARSwitchingFee);
                                cmd.Parameters.AddWithValue("@SwitchingFeeIncome", _accountingSetup.SwitchingFeeIncome);

                                cmd.Parameters.AddWithValue("@AgentCommissionExpense", _accountingSetup.AgentCommissionExpense);
                                cmd.Parameters.AddWithValue("@AgentCommissionPayable", _accountingSetup.AgentCommissionPayable);
                                cmd.Parameters.AddWithValue("@WHTPayablePPH21", _accountingSetup.WHTPayablePPH21);
                                cmd.Parameters.AddWithValue("@WHTPayablePPH23", _accountingSetup.WHTPayablePPH23);
                                cmd.Parameters.AddWithValue("@VatIn", _accountingSetup.VatIn);
                                cmd.Parameters.AddWithValue("@VatOut", _accountingSetup.VatOut);
                                cmd.Parameters.AddWithValue("@AgentCommissionCash", _accountingSetup.AgentCommissionCash);

                                cmd.Parameters.AddWithValue("@SwitchingFundAcc", _accountingSetup.SwitchingFundAcc);

                                cmd.Parameters.AddWithValue("@AgentCSRExpense", _accountingSetup.AgentCSRExpense);
                                cmd.Parameters.AddWithValue("@AgentCSRPayable", _accountingSetup.AgentCSRPayable);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _accountingSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_accountingSetup.AccountingSetupPK, "AccountingSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AccountingSetup where AccountingSetupPK =@PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _accountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Income", _accountingSetup.Income);
                                cmd.Parameters.AddWithValue("@Expense", _accountingSetup.Expense);
                                cmd.Parameters.AddWithValue("@PPHFinal", _accountingSetup.PPHFinal);
                                cmd.Parameters.AddWithValue("@InvInBond", _accountingSetup.InvInBond);
                                cmd.Parameters.AddWithValue("@InvInBondGovIDR", _accountingSetup.InvInBondGovIDR);
                                cmd.Parameters.AddWithValue("@InvInBondGovUSD", _accountingSetup.InvInBondGovUSD);
                                cmd.Parameters.AddWithValue("@InvInBondCorpIDR", _accountingSetup.InvInBondCorpIDR);
                                cmd.Parameters.AddWithValue("@InvInBondCorpUSD", _accountingSetup.InvInBondCorpUSD);
                                cmd.Parameters.AddWithValue("@InvInEquity", _accountingSetup.InvInEquity);
                                cmd.Parameters.AddWithValue("@InvInTD", _accountingSetup.InvInTD);
                                cmd.Parameters.AddWithValue("@InvInTDUSD", _accountingSetup.InvInTDUSD);
                                cmd.Parameters.AddWithValue("@InvInReksadana", _accountingSetup.InvInReksadana);
                                cmd.Parameters.AddWithValue("@APPurchaseBond", _accountingSetup.APPurchaseBond);
                                cmd.Parameters.AddWithValue("@APPurchaseEquity", _accountingSetup.APPurchaseEquity);
                                cmd.Parameters.AddWithValue("@APPurchaseTD", _accountingSetup.APPurchaseTD);
                                cmd.Parameters.AddWithValue("@ARSellBond", _accountingSetup.ARSellBond);
                                cmd.Parameters.AddWithValue("@ARSellEquity", _accountingSetup.ARSellEquity);
                                cmd.Parameters.AddWithValue("@ARSellTD", _accountingSetup.ARSellTD);
                                cmd.Parameters.AddWithValue("@InterestPurchaseBond", _accountingSetup.InterestPurchaseBond);
                                cmd.Parameters.AddWithValue("@InterestReceivableBond", _accountingSetup.InterestReceivableBond);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondGovIDR", _accountingSetup.InterestReceivableBondGovIDR);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondCorpIDR", _accountingSetup.InterestReceivableBondCorpIDR);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondGovUSD", _accountingSetup.InterestReceivableBondGovUSD);
                                cmd.Parameters.AddWithValue("@InterestReceivableBondCorpUSD", _accountingSetup.InterestReceivableBondCorpUSD);
                                cmd.Parameters.AddWithValue("@InterestReceivableTD", _accountingSetup.InterestReceivableTD);
                                cmd.Parameters.AddWithValue("@CashBond", _accountingSetup.CashBond);
                                cmd.Parameters.AddWithValue("@CashEquity", _accountingSetup.CashEquity);
                                cmd.Parameters.AddWithValue("@CashTD", _accountingSetup.CashTD);
                                cmd.Parameters.AddWithValue("@ForeignExchangeRevalAccount", _accountingSetup.ForeignExchangeRevalAccount);
                                cmd.Parameters.AddWithValue("@UnrealisedEquity", _accountingSetup.UnrealisedEquity);
                                cmd.Parameters.AddWithValue("@UnrealisedBond", _accountingSetup.UnrealisedBond);
                                cmd.Parameters.AddWithValue("@UnrealisedReksadana", _accountingSetup.UnrealisedReksadana);
                                cmd.Parameters.AddWithValue("@RealisedEquity", _accountingSetup.RealisedEquity);
                                cmd.Parameters.AddWithValue("@RealisedBondGovIDR", _accountingSetup.RealisedBondGovIDR);
                                cmd.Parameters.AddWithValue("@RealisedBondGovUSD", _accountingSetup.RealisedBondGovUSD);
                                cmd.Parameters.AddWithValue("@RealisedBondCorpIDR", _accountingSetup.RealisedBondCorpIDR);
                                cmd.Parameters.AddWithValue("@RealisedBondCorpUSD", _accountingSetup.RealisedBondCorpUSD);
                                cmd.Parameters.AddWithValue("@RealisedBond", _accountingSetup.RealisedBond);
                                cmd.Parameters.AddWithValue("@RealisedReksadana", _accountingSetup.RealisedReksadana);
                                cmd.Parameters.AddWithValue("@RealisedAset", _accountingSetup.RealisedAset);
                                cmd.Parameters.AddWithValue("@CadanganEquity", _accountingSetup.CadanganEquity);
                                cmd.Parameters.AddWithValue("@CadanganBond", _accountingSetup.CadanganBond);
                                cmd.Parameters.AddWithValue("@CadanganReksadana", _accountingSetup.CadanganReksadana);
                                cmd.Parameters.AddWithValue("@BrokerageFee", _accountingSetup.BrokerageFee);
                                cmd.Parameters.AddWithValue("@JSXLevyFee", _accountingSetup.JSXLevyFee);
                                cmd.Parameters.AddWithValue("@KPEIFee", _accountingSetup.KPEIFee);
                                cmd.Parameters.AddWithValue("@VATFee", _accountingSetup.VATFee);
                                cmd.Parameters.AddWithValue("@SalesTax", _accountingSetup.SalesTax);
                                cmd.Parameters.AddWithValue("@IncomeTaxArt23", _accountingSetup.IncomeTaxArt23);
                                cmd.Parameters.AddWithValue("@EquitySellMethod", _accountingSetup.EquitySellMethod);
                                cmd.Parameters.AddWithValue("@AveragePriority", _accountingSetup.AveragePriority);
                                cmd.Parameters.AddWithValue("@FixedAsetBuyPPN", _accountingSetup.FixedAsetBuyPPN);
                                cmd.Parameters.AddWithValue("@FixedAsetSellPPN", _accountingSetup.FixedAsetSellPPN);
                                cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _accountingSetup.DefaultCurrencyPK);
                                cmd.Parameters.AddWithValue("@DecimalPlaces", _accountingSetup.DecimalPlaces);

                                cmd.Parameters.AddWithValue("@ARManagementFee", _accountingSetup.ARManagementFee);
                                cmd.Parameters.AddWithValue("@ManagementFeeExpense", _accountingSetup.ManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@TaxARManagementFee", _accountingSetup.TaxARManagementFee);
                                cmd.Parameters.AddWithValue("@TaxManagementFeeExpense", _accountingSetup.TaxManagementFeeExpense);
                                cmd.Parameters.AddWithValue("@MKBD06Bank", _accountingSetup.MKBD06Bank);
                                cmd.Parameters.AddWithValue("@MKBD06PettyCash", _accountingSetup.MKBD06PettyCash);
                                cmd.Parameters.AddWithValue("@WHTFee", _accountingSetup.WHTFee);
                                cmd.Parameters.AddWithValue("@APPurchaseReksadana", _accountingSetup.APPurchaseReksadana);
                                cmd.Parameters.AddWithValue("@ARSellReksadana", _accountingSetup.ARSellReksadana);
                                cmd.Parameters.AddWithValue("@TotalEquity", _accountingSetup.TotalEquity);
                                cmd.Parameters.AddWithValue("@UnrealisedBondUSD", _accountingSetup.UnrealisedBondUSD);
                                cmd.Parameters.AddWithValue("@CadanganBondUSD", _accountingSetup.CadanganBondUSD);

                                cmd.Parameters.AddWithValue("@UnrealizedAccountSGD", _accountingSetup.UnrealizedAccountSGD);
                                cmd.Parameters.AddWithValue("@UnrealizedAccountUSD", _accountingSetup.UnrealizedAccountUSD);

                                cmd.Parameters.AddWithValue("@ARSubscriptionFee", _accountingSetup.ARSubscriptionFee);
                                cmd.Parameters.AddWithValue("@SubscriptionFeeIncome", _accountingSetup.SubscriptionFeeIncome);
                                cmd.Parameters.AddWithValue("@ARRedemptionFee", _accountingSetup.ARRedemptionFee);
                                cmd.Parameters.AddWithValue("@RedemptionFeeIncome", _accountingSetup.RedemptionFeeIncome);
                                cmd.Parameters.AddWithValue("@ARSwitchingFee", _accountingSetup.ARSwitchingFee);
                                cmd.Parameters.AddWithValue("@SwitchingFeeIncome", _accountingSetup.SwitchingFeeIncome);

                                cmd.Parameters.AddWithValue("@AgentCommissionExpense", _accountingSetup.AgentCommissionExpense);
                                cmd.Parameters.AddWithValue("@AgentCommissionPayable", _accountingSetup.AgentCommissionPayable);
                                cmd.Parameters.AddWithValue("@WHTPayablePPH21", _accountingSetup.WHTPayablePPH21);
                                cmd.Parameters.AddWithValue("@WHTPayablePPH23", _accountingSetup.WHTPayablePPH23);
                                cmd.Parameters.AddWithValue("@VatIn", _accountingSetup.VatIn);
                                cmd.Parameters.AddWithValue("@VatOut", _accountingSetup.VatOut);
                                cmd.Parameters.AddWithValue("@AgentCommissionCash", _accountingSetup.AgentCommissionCash);

                                cmd.Parameters.AddWithValue("@SwitchingFundAcc", _accountingSetup.SwitchingFundAcc);

                                cmd.Parameters.AddWithValue("@AgentCSRExpense", _accountingSetup.AgentCSRExpense);
                                cmd.Parameters.AddWithValue("@AgentCSRPayable", _accountingSetup.AgentCSRPayable);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _accountingSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AccountingSetup set status = 4, Notes=@Notes, " +
                                " UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime, lastupdate=@lastupdate where AccountingSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _accountingSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _accountingSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _accountingSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
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

        //7
        public void AccountingSetup_Approved(AccountingSetup _accountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AccountingSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where AccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _accountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _accountingSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AccountingSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where AccountingSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _accountingSetup.ApprovedUsersID);
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

        //8
        public void AccountingSetup_Reject(AccountingSetup _accountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AccountingSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where AccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _accountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _accountingSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AccountingSetup set status= 2,lastupdate=@lastupdate where AccountingSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
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

        //9
        public void AccountingSetup_Void(AccountingSetup _accountingSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AccountingSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where AccountingSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _accountingSetup.AccountingSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _accountingSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _accountingSetup.VoidUsersID);
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

        //10 AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public AccountingSetup AccountingSetup_SelectByPK(int _accountingSetupPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            //" Select  A1.AccountPK InvInBond,A1.ID InvInBondID,A1.Name InvInBondDesc,A2.AccountPK InvInEquity,A2.ID InvInEquityID,A2.Name InvInEquityDesc,A3.AccountPK InvInTD,A3.ID InvInTDID,A3.Name InvInTDDesc,       " +
                            //" A4.AccountPK APPurchaseBond,A4.ID APPurchaseBondID,A4.Name APPurchaseBondDesc,A5.AccountPK APPurchaseEquity,A5.ID APPurchaseEquityID,A5.Name APPurchaseEquityDesc,A6.AccountPK APPurchaseTD,A6.ID APPurchaseTDID,A6.Name APPurchaseTDDesc,       " +
                            //" A7.AccountPK ARSellBond,A7.ID ARSellBondID,A7.Name ARSellBondDesc,A8.AccountPK ARSellEquity,A8.ID ARSellEquityID,A8.Name ARSellEquityDesc,A9.AccountPK ARSellTD,A9.ID ARSellTDID,A9.Name ARSellTDDesc,       " +
                            //" A10.AccountPK InterestPurchaseBond,A10.ID InterestPurchaseBondID,A10.Name InterestPurchaseBondDesc,A11.AccountPK InterestReceivableBond,A11.ID InterestReceivableBondID,A11.Name InterestReceivableBondDesc, " +
                            //" A12.AccountPK InterestReceivableTD,A12.ID InterestReceivableTDID,A12.Name InterestReceivableTDDesc, A13.AccountPK ForeignExchangeRevalAccount,A13.ID ForeignExchangeRevalAccountID,A13.Name ForeignExchangeRevalAccountDesc,      " +
                            //" A14.AccountPK UnrealisedEquity,A14.ID UnrealisedEquityID,A14.Name UnrealisedEquityDesc, A15.AccountPK UnrealisedBond,A15.ID UnrealisedBondID,A15.Name UnrealisedBondDesc,      " +
                            //" A16.AccountPK UnrealisedReksadana,A16.ID UnrealisedReksadanaID,A16.Name UnrealisedReksadanaDesc, A17.AccountPK RealisedEquity,A17.ID RealisedEquityID,A17.Name RealisedEquityDesc,      " +
                            //" A18.AccountPK RealisedBond,A18.ID RealisedBondID,A18.Name RealisedBondDesc, A19.AccountPK RealisedReksadana,A19.ID RealisedReksadanaID,A19.Name RealisedReksadanaDesc,      " +
                            //" A20.AccountPK RealisedAset,A20.ID RealisedAsetID,A20.Name RealisedAsetDesc, A21.AccountPK CadanganEquity,A21.ID CadanganEquityID,A21.Name CadanganEquityDesc,      " +
                            //" A22.AccountPK CadanganBond,A22.ID CadanganBondID,A22.Name CadanganBondDesc, A23.AccountPK CadanganReksadana,A23.ID CadanganReksadanaID,A23.Name CadanganReksadanaDesc,      " +
                            //" A24.AccountPK BrokerageFee,A24.ID BrokerageFeeID,A24.Name BrokerageFeeDesc, A25.AccountPK JSXLevyFee,A25.ID JSXLevyFeeID,A25.Name JSXLevyFeeDesc,      " +
                            //" A26.AccountPK KPEIFee,A26.ID KPEIFeeID,A26.Name KPEIFeeDesc, A27.AccountPK SalesTax,A27.ID SalesTaxID,A27.Name SalesTaxDesc,A28.AccountPK IncomeTaxArt23,A28.ID IncomeTaxArt23ID,A28.Name IncomeTaxArt23Desc,       " +
                            //" A29.AccountPK FixedAsetBuyPPN,A29.ID FixedAsetBuyPPNID,A29.Name FixedAsetBuyPPNDesc, A30.AccountPK FixedAsetSellPPN,A30.ID FixedAsetSellPPNID,A30.Name FixedAsetSellPPNDesc,       " +
                            //" MV1.DescOne EquitySellMethodDesc,MV2.DescOne AveragePriorityDesc, " +
                            //" C1.CashRefPK CashBond,C1.ID CashBondID,C1.Name CashBondDesc,C2.CashRefPK CashEquity,C2.ID CashEquityID,C2.Name CashEquityDesc,C3.CashRefPK CashTD,C3.ID CashTDID,C3.Name CashTDDesc, CR.ID DefaultCurrencyID,     " +
                            //" A.* from AccountingSetup A left join        " +
                            //" Account A1 on A.InvInBond = A1.AccountPK and A1.status = 2  left join        " +
                            //" Account A2 on A.InvInEquity = A2.AccountPK and A2.status = 2  left join        " +
                            //" Account A3 on A.InvInTD = A3.AccountPK and A3.status = 2  left join        " +
                            //" Account A4 on A.APPurchaseBond = A4.AccountPK and A4.status = 2  left join        " +
                            //" Account A5 on A.APPurchaseEquity = A5.AccountPK and A5.status = 2  left join        " +
                            //" Account A6 on A.APPurchaseTD = A6.AccountPK and A6.status = 2  left join        " +
                            //" Account A7 on A.ARSellBond = A7.AccountPK and A7.status = 2  left join        " +
                            //" Account A8 on A.ARSellEquity = A8.AccountPK and A8.status = 2  left join        " +
                            //" Account A9 on A.ARSellTD = A9.AccountPK and A9.status = 2  left join        " +
                            //" Account A10 on A.InterestPurchaseBond = A10.AccountPK and A10.status = 2  left join        " +
                            //" Account A11 on A.InterestReceivableBond = A11.AccountPK and A11.status = 2  left join       " +
                            //" Account A12 on A.InterestReceivableTD = A12.AccountPK and A12.status = 2  left join      " +
                            //" Account A13 on A.ForeignExchangeRevalAccount = A13.AccountPK and A13.status = 2  left join      " +
                            //" Account A14 on A.UnrealisedEquity = A14.AccountPK and A14.status = 2  left join        " +
                            //" Account A15 on A.UnrealisedBond = A15.AccountPK and A15.status = 2  left join       " +
                            //" Account A16 on A.UnrealisedReksadana = A16.AccountPK and A16.status = 2  left join      " +
                            //" Account A17 on A.RealisedEquity = A17.AccountPK and A17.status = 2  left join      " +
                            //" Account A18 on A.RealisedBond = A18.AccountPK and A18.status = 2  left join        " +
                            //" Account A19 on A.RealisedReksadana = A19.AccountPK and A19.status = 2  left join       " +
                            //" Account A20 on A.RealisedAset = A20.AccountPK and A20.status = 2  left join      " +
                            //" Account A21 on A.CadanganEquity = A21.AccountPK and A21.status = 2  left join      " +
                            //" Account A22 on A.CadanganBond = A22.AccountPK and A22.status = 2  left join        " +
                            //" Account A23 on A.CadanganReksadana = A23.AccountPK and A23.status = 2  left join       " +
                            //" Account A24 on A.BrokerageFee = A24.AccountPK and A24.status = 2  left join      " +
                            //" Account A25 on A.JSXLevyFee = A25.AccountPK and A25.status = 2  left join      " +
                            //" Account A26 on A.KPEIFee = A26.AccountPK and A26.status = 2  left join      " +
                            //" Account A27 on A.SalesTax = A27.AccountPK and A27.status = 2  left join      " +
                            //" Account A28 on A.IncomeTaxArt23 = A28.AccountPK and A28.status = 2  left join      " +
                            //" Account A29 on A.FixedAsetBuyPPN = A29.AccountPK and A29.status = 2  left join      " +
                            //" Account A30 on A.FixedAsetSellPPN = A30.AccountPK and A30.status = 2  left join      " +
                            //" MasterValue MV1 on A.EquitySellMethod = MV1.Code and MV1.status = 2  and MV1.ID ='EquitySellMethod' left join      " +
                            //" MasterValue MV2 on A.AveragePriority = MV2.Code and MV2.status = 2  and MV2.ID ='AveragePriority' left join      " +
                            //" CashRef C1 on A.CashBond = C1.CashRefPK and C1.status = 2  left join     " +
                            //" CashRef C2 on A.CashEquity = C2.CashRefPK and C2.status = 2  left join   " +
                            //" CashRef C3 on A.CashTD = C3.CashRefPK and C3.status = 2  left join " +
                            //" Currency CR on A.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2 " +
                            //" where A.AccountingSetupPK = @AccountingSetupPK ";

                            @"Select  A1.AccountPK InvInBond,A1.ID InvInBondID,A1.Name InvInBondDesc,A2.AccountPK InvInEquity,A2.ID InvInEquityID,A2.Name InvInEquityDesc,A3.AccountPK InvInTD,A3.ID InvInTDID,A3.Name InvInTDDesc,        
                            A4.AccountPK APPurchaseBond,A4.ID APPurchaseBondID,A4.Name APPurchaseBondDesc,A5.AccountPK APPurchaseEquity,A5.ID APPurchaseEquityID,A5.Name APPurchaseEquityDesc,A6.AccountPK APPurchaseTD,A6.ID APPurchaseTDID,A6.Name APPurchaseTDDesc,        
                            A7.AccountPK ARSellBond,A7.ID ARSellBondID,A7.Name ARSellBondDesc,A8.AccountPK ARSellEquity,A8.ID ARSellEquityID,A8.Name ARSellEquityDesc,A9.AccountPK ARSellTD,A9.ID ARSellTDID,A9.Name ARSellTDDesc,        
                            A10.AccountPK InterestPurchaseBond,A10.ID InterestPurchaseBondID,A10.Name InterestPurchaseBondDesc,A11.AccountPK InterestReceivableBond,A11.ID InterestReceivableBondID,A11.Name InterestReceivableBondDesc,  
                            A12.AccountPK InterestReceivableTD,A12.ID InterestReceivableTDID,A12.Name InterestReceivableTDDesc, A13.AccountPK ForeignExchangeRevalAccount,A13.ID ForeignExchangeRevalAccountID,A13.Name ForeignExchangeRevalAccountDesc,       
                            A14.AccountPK UnrealisedEquity,A14.ID UnrealisedEquityID,A14.Name UnrealisedEquityDesc, A15.AccountPK UnrealisedBond,A15.ID UnrealisedBondID,A15.Name UnrealisedBondDesc,       
                            A16.AccountPK UnrealisedReksadana,A16.ID UnrealisedReksadanaID,A16.Name UnrealisedReksadanaDesc, A17.AccountPK RealisedEquity,A17.ID RealisedEquityID,A17.Name RealisedEquityDesc,       
                            A18.AccountPK RealisedBond,A18.ID RealisedBondID,A18.Name RealisedBondDesc, A19.AccountPK RealisedReksadana,A19.ID RealisedReksadanaID,A19.Name RealisedReksadanaDesc,       
                            A20.AccountPK RealisedAset,A20.ID RealisedAsetID,A20.Name RealisedAsetDesc, A21.AccountPK CadanganEquity,A21.ID CadanganEquityID,A21.Name CadanganEquityDesc,       
                            A22.AccountPK CadanganBond,A22.ID CadanganBondID,A22.Name CadanganBondDesc, A23.AccountPK CadanganReksadana,A23.ID CadanganReksadanaID,A23.Name CadanganReksadanaDesc,       
                            A24.AccountPK BrokerageFee,A24.ID BrokerageFeeID,A24.Name BrokerageFeeDesc, A25.AccountPK JSXLevyFee,A25.ID JSXLevyFeeID,A25.Name JSXLevyFeeDesc,       
                            A26.AccountPK KPEIFee,A26.ID KPEIFeeID,A26.Name KPEIFeeDesc, A27.AccountPK SalesTax,A27.ID SalesTaxID,A27.Name SalesTaxDesc,A28.AccountPK IncomeTaxArt23,A28.ID IncomeTaxArt23ID,A28.Name IncomeTaxArt23Desc,        
                            A29.AccountPK FixedAsetBuyPPN,A29.ID FixedAsetBuyPPNID,A29.Name FixedAsetBuyPPNDesc, A30.AccountPK FixedAsetSellPPN,A30.ID FixedAsetSellPPNID,A30.Name FixedAsetSellPPNDesc,

                            A31.AccountPK Income,A31.ID IncomeID,A31.Name IncomeDesc,
                            A32.AccountPK Expense,A32.ID ExpenseID,A32.Name ExpenseDesc,
                            A33.AccountPK PPHFinal,A33.ID PPHFinalID,A33.Name PPHFinalDesc,
                            A34.AccountPK InvInBondGovIDR,A34.ID InvInBondGovIDRID,A34.Name InvInBondGovIDRDesc,
                            A35.AccountPK InvInBondGovUSD,A35.ID InvInBondGovUSDID,A35.Name InvInBondGovUSDDesc,
                            A36.AccountPK InvInBondCorpIDR,A36.ID InvInBondCorpIDRID,A36.Name InvInBondCorpIDRDesc,
                            A37.AccountPK InvInBondCorpUSD,A37.ID InvInBondCorpUSDID,A37.Name InvInBondCorpUSDDesc,
                            A38.AccountPK InvInReksadana,A38.ID InvInReksadanaID,A38.Name InvInReksadanaDesc,
                            A39.AccountPK InterestReceivableBondGovIDR,A39.ID InterestReceivableBondGovIDRID,A39.Name InterestReceivableBondGovIDRDesc,
                            A40.AccountPK InterestReceivableBondCorpIDR,A40.ID InterestReceivableBondCorpIDRID,A40.Name InterestReceivableBondCorpIDRDesc,
                            A41.AccountPK InterestReceivableBondGovUSD,A41.ID InterestReceivableBondGovUSDID,A41.Name InterestReceivableBondGovUSDDesc,
                            A42.AccountPK InterestReceivableBondCorpUSD,A42.ID InterestReceivableBondCorpUSDID,A42.Name InterestReceivableBondCorpUSDDesc,
                            A43.AccountPK RealisedBondGovIDR,A43.ID RealisedBondGovIDRID,A43.Name RealisedBondGovIDRDesc,
                            A44.AccountPK RealisedBondGovUSD,A44.ID RealisedBondGovUSDID,A44.Name RealisedBondGovUSDDesc,
                            A45.AccountPK RealisedBondCorpIDR,A45.ID RealisedBondCorpIDRID,A45.Name RealisedBondCorpIDRDesc,
                            A46.AccountPK RealisedBondCorpUSD,A46.ID RealisedBondCorpUSDID,A46.Name RealisedBondCorpUSDDesc,  
      
                            A47.AccountPK ARManagementFee,A47.ID ARManagementFeeID,A47.Name ARManagementFeeDesc,  
                            A48.AccountPK ManagementFeeExpense,A48.ID ManagementFeeExpenseID,A48.Name ManagementFeeExpenseDesc,  
                            A49.AccountPK MKBD06Bank,A49.ID MKBD06BankID,A49.Name MKBD06BankDesc,  
                            A50.AccountPK MKBD06PettyCash,A50.ID MKBD06PettyCashID,A50.Name MKBD06PettyCashDesc,  
                            A51.AccountPK WHTFee,A51.ID WHTFeeID,A51.Name WHTFeeDesc,  
                            A52.AccountPK VATFee,A52.ID VATFeeID,A52.Name VATFeeDesc,  
                            A53.AccountPK TaxARManagementFee,A53.ID TaxARManagementFeeID,A53.Name TaxARManagementFeeDesc,  
                            A54.AccountPK TaxManagementFeeExpense,A54.ID TaxManagementFeeExpenseID,A54.Name TaxManagementFeeExpenseDesc,
                            A55.AccountPK APPurchaseReksadana,A55.ID APPurchaseReksadanaID,A55.Name APPurchaseReksadanaDesc,  
                            A56.AccountPK ARSellReksadana,A56.ID ARSellReksadanaID,A56.Name ARSellReksadanaDesc, 
                            A57.AccountPK TotalEquity,A57.ID TotalEquityID,A57.Name TotalEquityDesc, 
                            A58.AccountPK UnrealisedBondUSD,A58.ID UnrealisedBondIDUSD,A58.Name UnrealisedBondUSD, 
                            A59.AccountPK CadanganBondUSD,A59.ID CadanganBondIDUSD,A59.Name CadanganBondDescUSD, 
                            A60.AccountPK UnrealizedAccountSGD,A60.ID UnrealizedAccountSGDID,A60.Name UnrealizedAccountSGDDesc, 
                            A61.AccountPK UnrealizedAccountUSD,A61.ID UnrealizedAccountUSDID,A61.Name UnrealizedAccountUSDDesc, 

                            A62.AccountPK ARSubscriptionFee,A62.ID ARSubscriptionFeeID,A62.Name ARSubscriptionFeeDesc, 
                            A63.AccountPK SubscriptionFeeIncome,A63.ID SubscriptionFeeIncomeID,A63.Name SubscriptionFeeIncomeDesc, 
                            A64.AccountPK ARRedemptionFee,A64.ID ARRedemptionFeeID,A64.Name ARRedemptionFeeDesc, 
                            A65.AccountPK RedemptionFeeIncome,A65.ID RedemptionFeeIncomeID,A65.Name RedemptionFeeIncomeDesc, 
                            A66.AccountPK ARSwitchingFee,A66.ID ARSwitchingFeeID,A66.Name ARSwitchingFeeDesc, 
                            A67.AccountPK SwitchingFeeIncome,A67.ID SwitchingFeeIncomeID,A67.Name SwitchingFeeIncomeDesc,  
                            
                            A68.AccountPK SwitchingFundAcc,A67.ID SwitchingFundAccID,A67.Name SwitchingFundAccDesc,                            

                            MV1.DescOne EquitySellMethodDesc,MV2.DescOne AveragePriorityDesc,  
                            C1.CashRefPK CashBond,C1.ID CashBondID,C1.Name CashBondDesc,C2.CashRefPK CashEquity,C2.ID CashEquityID,C2.Name CashEquityDesc,C3.CashRefPK CashTD,C3.ID CashTDID,C3.Name CashTDDesc, CR.ID DefaultCurrencyID,      
                            A.* from AccountingSetup A left join         
                            Account A1 on A.InvInBond = A1.AccountPK and A1.status = 2  left join         
                            Account A2 on A.InvInEquity = A2.AccountPK and A2.status = 2  left join         
                            Account A3 on A.InvInTD = A3.AccountPK and A3.status = 2  left join         
                            Account A4 on A.APPurchaseBond = A4.AccountPK and A4.status = 2  left join         
                            Account A5 on A.APPurchaseEquity = A5.AccountPK and A5.status = 2  left join         
                            Account A6 on A.APPurchaseTD = A6.AccountPK and A6.status = 2  left join         
                            Account A7 on A.ARSellBond = A7.AccountPK and A7.status = 2  left join         
                            Account A8 on A.ARSellEquity = A8.AccountPK and A8.status = 2  left join         
                            Account A9 on A.ARSellTD = A9.AccountPK and A9.status = 2  left join         
                            Account A10 on A.InterestPurchaseBond = A10.AccountPK and A10.status = 2  left join         
                            Account A11 on A.InterestReceivableBond = A11.AccountPK and A11.status = 2  left join        
                            Account A12 on A.InterestReceivableTD = A12.AccountPK and A12.status = 2  left join       
                            Account A13 on A.ForeignExchangeRevalAccount = A13.AccountPK and A13.status = 2  left join       
                            Account A14 on A.UnrealisedEquity = A14.AccountPK and A14.status = 2  left join         
                            Account A15 on A.UnrealisedBond = A15.AccountPK and A15.status = 2  left join        
                            Account A16 on A.UnrealisedReksadana = A16.AccountPK and A16.status = 2  left join       
                            Account A17 on A.RealisedEquity = A17.AccountPK and A17.status = 2  left join       
                            Account A18 on A.RealisedBond = A18.AccountPK and A18.status = 2  left join         
                            Account A19 on A.RealisedReksadana = A19.AccountPK and A19.status = 2  left join        
                            Account A20 on A.RealisedAset = A20.AccountPK and A20.status = 2  left join       
                            Account A21 on A.CadanganEquity = A21.AccountPK and A21.status = 2  left join       
                            Account A22 on A.CadanganBond = A22.AccountPK and A22.status = 2  left join         
                            Account A23 on A.CadanganReksadana = A23.AccountPK and A23.status = 2  left join        
                            Account A24 on A.BrokerageFee = A24.AccountPK and A24.status = 2  left join       
                            Account A25 on A.JSXLevyFee = A25.AccountPK and A25.status = 2  left join       
                            Account A26 on A.KPEIFee = A26.AccountPK and A26.status = 2  left join       
                            Account A27 on A.SalesTax = A27.AccountPK and A27.status = 2  left join       
                            Account A28 on A.IncomeTaxArt23 = A28.AccountPK and A28.status = 2  left join       
                            Account A29 on A.FixedAsetBuyPPN = A29.AccountPK and A29.status = 2  left join       
                            Account A30 on A.FixedAsetSellPPN = A30.AccountPK and A30.status = 2  left join 
 
                            Account A31 on A.Income = A31.AccountPK and A31.status = 2  left join 
                            Account A32 on A.Expense = A32.AccountPK and A32.status = 2  left join 
                            Account A33 on A.PPHFinal = A33.AccountPK and A33.status = 2  left join 
                            Account A34 on A.InvInBondGovIDR = A34.AccountPK and A34.status = 2  left join 
                            Account A35 on A.InvInBondGovUSD = A35.AccountPK and A35.status = 2  left join 
                            Account A36 on A.InvInBondCorpIDR = A36.AccountPK and A36.status = 2  left join 
                            Account A37 on A.InvInBondCorpUSD = A37.AccountPK and A37.status = 2  left join 
                            Account A38 on A.InvInReksadana = A38.AccountPK and A38.status = 2  left join 
                            Account A39 on A.InterestReceivableBondGovIDR = A39.AccountPK and A39.status = 2  left join 
                            Account A40 on A.InterestReceivableBondCorpIDR = A40.AccountPK and A40.status = 2  left join 
                            Account A41 on A.InterestReceivableBondGovUSD = A41.AccountPK and A41.status = 2  left join 
                            Account A42 on A.InterestReceivableBondCorpUSD = A42.AccountPK and A42.status = 2  left join 
                            Account A43 on A.RealisedBondGovIDR = A43.AccountPK and A43.status = 2  left join 
                            Account A44 on A.RealisedBondGovUSD = A44.AccountPK and A44.status = 2  left join 
                            Account A45 on A.RealisedBondCorpIDR = A45.AccountPK and A45.status = 2  left join 
                            Account A46 on A.RealisedBondCorpUSD = A46.AccountPK and A46.status = 2  left join  

                            Account A47 on A.ARManagementFee = A47.AccountPK and A47.status = 2  left join  
                            Account A48 on A.ManagementFeeExpense = A48.AccountPK and A48.status = 2  left join  
                            Account A49 on A.MKBD06Bank = A49.AccountPK and A49.status = 2  left join  
                            Account A50 on A.MKBD06PettyCash = A50.AccountPK and A50.status = 2  left join 
                            Account A51 on A.WHTFee = A51.AccountPK and A51.status = 2  left join    
                            Account A52 on A.VATFee = A52.AccountPK and A52.status = 2  left join  
                            Account A53 on A.TaxARManagementFee = A53.AccountPK and A53.status = 2  left join  
                            Account A54 on A.TaxManagementFeeExpense = A54.AccountPK and A54.status = 2  left join
                            Account A55 on A.APPurchaseReksadana = A55.AccountPK and A55.status = 2  left join  
                            Account A56 on A.ARSellReksadana = A56.AccountPK and A56.status = 2  left join 
                            Account A57 on A.TotalEquity = A57.AccountPK and A57.status = 2  left join   
                            Account A58 on A.UnrealisedBondUSD = A58.AccountPK and A58.status = 2  left join  
                            Account A59 on A.CadanganBondUSD = A59.AccountPK and A59.status = 2  left join    
                            Account A60 on A.UnrealizedAccountSGD = A60.AccountPK and A60.status = 2  left join  
                            Account A61 on A.UnrealizedAccountUSD = A61.AccountPK and A61.status = 2  left join  

                            Account A62 on A.ARSubscriptionFee = A62.AccountPK and A62.status = 2  left join  
                            Account A63 on A.SubscriptionFeeIncome = A63.AccountPK and A63.status = 2  left join  
                            Account A64 on A.ARRedemptionFee = A64.AccountPK and A64.status = 2  left join  
                            Account A65 on A.RedemptionFeeIncome = A65.AccountPK and A65.status = 2  left join  
                            Account A66 on A.ARSwitchingFee = A66.AccountPK and A66.status = 2  left join  
                            Account A67 on A.SwitchingFeeIncome = A67.AccountPK and A67.status = 2  left join 

                            Account A68 on A.SwitchingFundAcc = A67.AccountPK and A67.status = 2  left join   

                            MasterValue MV1 on A.EquitySellMethod = MV1.Code and MV1.status = 2  and MV1.ID ='EquitySellMethod' left join       
                            MasterValue MV2 on A.AveragePriority = MV2.Code and MV2.status = 2  and MV2.ID ='AveragePriority' left join       
                            CashRef C1 on A.CashBond = C1.CashRefPK and C1.status = 2  left join      
                            CashRef C2 on A.CashEquity = C2.CashRefPK and C2.status = 2  left join    
                            CashRef C3 on A.CashTD = C3.CashRefPK and C3.status = 2  left join  
                            Currency CR on A.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2 
                            where A.AccountingSetupPK = @AccountingSetupPK";

                        cmd.Parameters.AddWithValue("@AccountingSetupPK", _accountingSetupPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setAccountingSetup(dr);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }





        public AccountingSetup AccountingSetup_SelectByAccountingSetupID(string _accountingSetupID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select * From AccountingSetup " +
                            "Where status=2";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setAccountingSetup(dr);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int Get_JournalDecimalPlaces()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " SELECT  isnull(DecimalPlaces,0) DecimalPlaces FROM [AccountingSetup]  where status = 2 ";


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToInt32(dr["DecimalPlaces"]);
                                }
                            }
                            return 0;
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