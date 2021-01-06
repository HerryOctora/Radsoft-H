using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class TrxPortfolio
    {
        public int TrxPortfolioPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public DateTime ValueDate { get; set; }
        public string Reference { get; set; }
        public int InstrumentTypePK { get; set; }
        public int Type { get; set; }
        public string InstrumentTypeID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public int BankPK { get; set; }
        public string BankID { get; set; }
        public string BankName { get; set; }
        public int BankBranchPK { get; set; }
        public string BankBranchID { get; set; }
        public decimal Price { get; set; }
        public decimal Lot { get; set; }
        public decimal LotInShare { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal LevyAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal KPEIAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal OTCAmount { get; set; }
        public decimal IncomeTaxSellAmount { get; set; }
        public decimal RealisedAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int TrxType { get; set; }
        public string TrxTypeID { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public string SettledDate { get; set; }
        public decimal AcqPrice { get; set; }
        public string AcqDate { get; set; }
        public decimal AcqVolume { get; set; }
        public decimal AcqPrice1 { get; set; }
        public string AcqDate1 { get; set; }
        public decimal AcqVolume1 { get; set; }
        public decimal AcqPrice2 { get; set; }
        public string AcqDate2 { get; set; }
        public decimal AcqVolume2 { get; set; }
        public decimal AcqPrice3 { get; set; }
        public string AcqDate3 { get; set; }
        public decimal AcqVolume3 { get; set; }
        public decimal AcqPrice4 { get; set; }
        public string AcqDate4 { get; set; }
        public decimal AcqVolume4 { get; set; }
        public decimal AcqPrice5 { get; set; }
        public string AcqDate5 { get; set; }
        public decimal AcqVolume5 { get; set; }
        public decimal AcqPrice6 { get; set; }
        public string AcqDate6 { get; set; }
        public decimal AcqVolume6 { get; set; }
        public decimal AcqPrice7 { get; set; }
        public string AcqDate7 { get; set; }
        public decimal AcqVolume7 { get; set; }
        public decimal AcqPrice8 { get; set; }
        public string AcqDate8 { get; set; }
        public decimal AcqVolume8 { get; set; }
        public decimal AcqPrice9 { get; set; }
        public string AcqDate9 { get; set; }
        public decimal AcqVolume9 { get; set; }
        public string LastCouponDate { get; set; }
        public string NextCouponDate { get; set; }
        public string MaturityDate { get; set; }
        public decimal InterestPercent { get; set; }
        public int CompanyAccountTradingPK { get; set; }
        public string CompanyAccountTradingID { get; set; }
        public string CompanyAccountTradingName { get; set; }
        public int CashRefPK { get; set; }
        public string CashRefID { get; set; }
        public decimal BrokerageFeePercent { get; set; }
        public decimal BrokerageFeeAmount { get; set; }
        public string Category { get; set; }
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

    }

    public class TrxPortfolioReport
    {
        
        public int TrxPortfolioPK { get; set; }
        public DateTime SettledDate { get; set; }
        public DateTime ValueDate { get; set; }
        public string Reference { get; set; }
        public string InstrumentID { get; set; }
        public decimal Rate { get; set; }
        public string TrxTypeID { get; set; }
        public decimal Price { get; set; }
        public decimal AcqPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastCouponDate { get; set; }
        public decimal SettleAmount { get; set; }

    }

    public class TrxPortfolioForNetAmount
    {
        public string ValueDate { get; set; }
        public int InstrumentPK { get; set; }
        public int InstrumentTypePK { get; set; }
        public int TrxType { get; set; }
        public int CounterpartPK { get; set; }
        public DateTime SettledDate { get; set; }
        public string NextCouponDate { get; set; }
        public string LastCouponDate { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public decimal AcqPrice { get; set; }
        public string AcqDate { get; set; }
        public decimal AcqVolume { get; set; }
        public decimal AcqPrice1 { get; set; }
        public string AcqDate1 { get; set; }
        public decimal AcqVolume1 { get; set; }
        public decimal AcqPrice2 { get; set; }
        public string AcqDate2 { get; set; }
        public decimal AcqVolume2 { get; set; }
        public decimal AcqPrice3 { get; set; }
        public string AcqDate3 { get; set; }
        public decimal AcqVolume3 { get; set; }
        public decimal AcqPrice4 { get; set; }
        public string AcqDate4 { get; set; }
        public decimal AcqVolume4 { get; set; }
        public decimal AcqPrice5 { get; set; }
        public string AcqDate5 { get; set; }
        public decimal AcqVolume5 { get; set; }
        public decimal AcqPrice6 { get; set; }
        public string AcqDate6 { get; set; }
        public decimal AcqVolume6 { get; set; }
        public decimal AcqPrice7 { get; set; }
        public string AcqDate7 { get; set; }
        public decimal AcqVolume7 { get; set; }
        public decimal AcqPrice8 { get; set; }
        public string AcqDate8 { get; set; }
        public decimal AcqVolume8 { get; set; }
        public decimal AcqPrice9 { get; set; }
        public string AcqDate9 { get; set; }
        public decimal AcqVolume9 { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal LevyAmount { get; set; }
        public decimal KPEIAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal OTCAmount { get; set; }
        public decimal IncomeTaxSellAmount { get; set; }
        public decimal RealisedAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int CompanyAccountTradingPK { get; set; }
    }

    public class ReferenceFromTrxPortfolio
    {
        public string Reference;
    }

}