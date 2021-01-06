using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Instrument
    {
        public int InstrumentPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Affiliated { get; set; }
        public int InstrumentTypePK { get; set; }
        public string InstrumentTypeID { get; set; }
        public int ReksadanaTypePK { get; set; }
        public string ReksadanaTypeID { get; set; }
        public int DepositoTypePK { get; set; }
        public string DepositoTypeID { get; set; }
        public string ISIN { get; set; }
        public int BankPK { get; set; }
        public string BankID { get; set; }
        public string BankName { get; set; }
        public int IssuerPK { get; set; }
        public string IssuerID { get; set; }
        public string IssuerName { get; set; }
        public int SectorPK { get; set; }
        public string SectorID { get; set; }
        public string SectorName { get; set; }
        public int HoldingPK { get; set; }
        public string HoldingID { get; set; }
        public string HoldingName { get; set; }
        public int MarketPK { get; set; }
        public string MarketID { get; set; }
        public string IssueDate { get; set; }
        public string MaturityDate { get; set; }
        public decimal InterestPercent { get; set; }
        public int InterestPaymentType { get; set; }
        public string InterestPaymentTypeDesc { get; set; }
        public int InterestType { get; set; }
        public string InterestTypeDesc { get; set; }
        public decimal LotInShare { get; set; }
        public bool BitIsSuspend { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public decimal RegulatorHaircut { get; set; }
        public decimal Liquidity { get; set; }
        public decimal NAWCHaircut { get; set; }
        public decimal CompanyHaircut { get; set; }
        public string BondRating { get; set; }
        public string BondRatingDesc { get; set; }
        public bool BitIsShortSell { get; set; }
        public bool BitIsMargin { get; set; }
        public bool BitIsScriptless { get; set; }
        public decimal TaxExpensePercent { get; set; }
        public int InterestDaysType { get; set; }
        public string InterestDaysTypeDesc { get; set; }
        public string BloombergCode { get; set; }
        public bool BitIsForeign { get; set; }
        public string FirstCouponDate { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public string BankAccountNo { get; set; }
        public string SAPCustID { get; set; }
        public string SAPCustDesc { get; set; }

        public int InstrumentCompanyTypePK { get; set; }
        public string InstrumentCompanyTypeID { get; set; }
        public string InstrumentCompanyTypeName { get; set; }

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
        public string AnotherRating { get; set; }

        public string BloombergSecID { get; set; }
        public string ShortName { get; set; }
        public string BloombergISIN { get; set; }
    }


    public class InstrumentForInvestment
    {
        public int InstrumentPK { get; set; }
        public string ID { get; set; }
        public string BankID { get; set; }
        public string Name { get; set; }
        public int InstrumentTypePK { get; set; }
        public string ValueDate { get; set; }
        public string SettledDate { get; set; }
        public string MaturityDate { get; set; }
        public decimal InterestPercent { get; set; }
        public string LastCouponDate { get; set; }
        public string DateDiff { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public string AcqDate { get; set; }
        public decimal Amount { get; set; }
        public double Tenor { get; set; }
        public int TrxType { get; set; }
        public string Category { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public int BankPK { get; set; }
        public int TrxBuy { get; set; }
        public string TrxBuyType { get; set; }
        public int InterestDaysType { get; set; }
        public int InterestPaymentType { get; set; }
        public int PaymentModeOnMaturity { get; set; }
        public int BankBranchPK { get; set; }
        public int NoFP { get; set; }
        public decimal BegBalance { get; set; }
        public decimal MovBalance { get; set; }
        public int PaymentType { get; set; }
        public bool BitBreakable { get; set; }
        public int Flag { get; set; }
        public int PaymentPeriod { get; set; }
    }

    public class InstrumentForOmsDeposito
    {
        public int BankPK { get; set; }
        public decimal InterestPercent { get; set; }
        public string AcqDate { get; set; }
        public string MaturityDate { get; set; }
        public int CurrencyPK { get; set; }
        public string Category { get; set; }
    }

    public class InstrumentCombo
    {
        public int InstrumentPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int InstrumentTypePK { get; set; }
    }

    public class InstrumentGetCouponDate
    {
        public int InstrumentPK { get; set; }
        public string LastCouponDate { get; set; }
        public string NextCouponDate { get; set; }
    }

    public class InstrumentRightsCombo
    {
        public int InstrumentRightsPK { get; set; }
        public string InstrumentRightsID { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public decimal Balance { get; set; }
    }

    public class InstrumentForTrxPortfolio
    {
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public int BankPK { get; set; }
        public int BankBranchPK { get; set; }
        public decimal BegBalance { get; set; }
        public decimal MovBalance { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyID { get; set; }
        public string AcqDate { get; set; }
        public decimal InterestPercent { get; set; }
        public string MaturityDate { get; set; }
        public int TrxBuy { get; set; }
        public decimal Price { get; set; }

    }

    public class InstrumentExport
    {
        public string ValueDateFrom { get; set; }
        public string ValueDateTo { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Affiliated { get; set; }
        public int InstrumentTypePK { get; set; }
        public int ReksadanaTypePK { get; set; }
        public int DepositoTypePK { get; set; }
        public string ISIN { get; set; }
        public int BankPK { get; set; }
        public int InstrumentPK { get; set; }
        public int IssuerPK { get; set; }
        public int SectorPK { get; set; }
        public int HoldingPK { get; set; }
        public int MarketPK { get; set; }
        public string IssueDate { get; set; }
        public string MaturityDate { get; set; }
        public string FirstCouponDate { get; set; }
        public decimal InterestPercent { get; set; }
        public decimal LotInShare { get; set; }
        public string BitIsSuspend { get; set; }
        public int CurrencyPK { get; set; }
        public decimal RegulatorHaircut { get; set; }
        public decimal Liquidity { get; set; }
        public decimal NAWCHaircut { get; set; }
        public decimal CompanyHaircut { get; set; }
        public string BondRating { get; set; }
        public string BitIsShortSell { get; set; }
        public string BitIsMargin { get; set; }
        public string BitIsScriptless { get; set; }
        public decimal TaxExpensePercent { get; set; }
        public int InterestDaysType { get; set; }
        public string DownloadMode { get; set; }
        public int InterestType { get; set; }
        public int InterestPaymentType { get; set; }
        public string BloombergCode { get; set; }
        public string BitIsForeign { get; set; }
        public int CounterpartPK { get; set; }
        public string BankAccountNo { get; set; }
        public string SAPCustID { get; set; }
        public int InstrumentCompanyTypePK { get; set; }
        public string AnotherRating { get; set; }

    }


}
