using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FixedAsset
    {
        public int FixedAssetPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string BuyValueDate { get; set; }
        public int BuyJournalNo { get; set; }
        public string BuyReference { get; set; }
        public string BuyDescription { get; set; }
        public int FixedAssetAccount { get; set; }
        public string FixedAssetAccountID { get; set; }
        public int AccountBuyCredit { get; set; }
        public string AccountBuyCreditID { get; set; }
        public decimal BuyAmount { get; set; }
        public int BitWithPPNBuy { get; set; }
        public int BitWithPPNSell { get; set; }
        public decimal AmountPPNBuy { get; set; }
        public decimal AmountPPNSell { get; set; }
        public int DepreciationMode { get; set; }
        public string DepreciationModeDesc { get; set; }
        public int JournalInterval { get; set; }
        public string JournalIntervalDesc { get; set; }
        public string IntervalSpecificDate { get; set; }
        public int DepreciationPeriod { get; set; }
        public string PeriodUnit { get; set; }
        public string PeriodUnitDesc { get; set; }
        public int DepreciationExpAccount { get; set; }
        public string DepreciationExpAccountID { get; set; }
        public int AccumulatedDeprAccount { get; set; }
        public string AccumulatedDeprAccountID { get; set; }
        public int OfficePK { get; set; }
        public string OfficeID { get; set; }
        public string SellValueDate { get; set; }
        public string SellReference { get; set; }
        public string SellDescription { get; set; }
        public int SellJournalNo { get; set; }
        public decimal SellAmount { get; set; }
        public int AccountSellDebit { get; set; }
        public string AccountSellDebitID { get; set; }
        public bool BitSold { get; set; }
        public int BuyDebitCurrencyPK { get; set; }
        public string BuyDebitCurrencyID { get; set; }
        public int BuyCreditCurrencyPK { get; set; }
        public string BuyCreditCurrencyID { get; set; }
        public int BuyDebitRate { get; set; }
        public int BuyCreditRate { get; set; }
        public int SellDebitCurrencyPK { get; set; }
        public string SellDebitCurrencyID { get; set; }
        public int SellCreditCurrencyPK { get; set; }
        public string SellCreditCurrencyID { get; set; }
        public int SellDebitRate { get; set; }
        public int SellCreditRate { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedTime { get; set; }
        public bool Revised { get; set; }
        public string RevisedBy { get; set; }
        public string RevisedTime { get; set; }
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
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public int ConsigneePK { get; set; }
        public string ConsigneeID { get; set; }
        public int TypeOfAssetsPK { get; set; }
        public string TypeOfAssetsID { get; set; }
        public decimal ResiduAmount { get; set; }
        public string Location { get; set; }
        public decimal VATPercent { get; set; }

        public DateTime ValueDateFrom { get; set; }
        public DateTime ValueDateTo { get; set; }
        public string Period { get; set; } 

    }

    public class FixedAssetAddNew
    {
        public int FixedAssetPK { get; set; }
        public long HistoryPK { get; set; }
        public string Message { get; set; }
    }


    public class SetFixedAsset
    {
        public int FixedAssetPK { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public decimal CostValue { get; set; }
        public decimal Depreciation { get; set; }
        public decimal TotalDepreciation { get; set; }
        public decimal BookValue { get; set; }
    }


}