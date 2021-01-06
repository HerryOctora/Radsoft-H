using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Fund
    {
        public int FundPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public int BankBranchPK { get; set; }
        public string BankBranchID { get; set; }
        public decimal MaxUnits { get; set; }
        public decimal TotalUnits { get; set; }
        public decimal Nav { get; set; }
        public string EffectiveDate { get; set; }
        public string MaturityDate { get; set; }
        public decimal CustodyFeePercent { get; set; }
        public decimal ManagementFeePercent { get; set; }
        public decimal SubscriptionFeePercent { get; set; }
        public decimal RedemptionFeePercent { get; set; }
        public decimal SwitchingFeePercent { get; set; }
        public int NavRoundingMode { get; set; }
        public string NavRoundingModeDesc { get; set; }
        public int NavDecimalPlaces { get; set; }
        public string NavDecimalPlacesDesc { get; set; }
        public int UnitDecimalPlaces { get; set; }
        public string UnitDecimalPlacesDesc { get; set; }
        public int UnitRoundingMode { get; set; }
        public string UnitRoundingModeDesc { get; set; }
        public string NKPDName { get; set; }
        public string SInvestCode { get; set; }
        public string BloombergCode { get; set; }
        public int FundTypeInternal { get; set; }
        public string FundTypeInternalDesc { get; set; }
        public bool BitMarketPrice { get; set; }
        public decimal BondAmortizationPercent { get; set; }
        public decimal MinSwitch { get; set; }
        public decimal MinBalSwitchAmt { get; set; }
        public decimal MinBalSwitchUnit { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartName { get; set; }
        public decimal MinSubs { get; set; }
        public decimal MinReds { get; set; }
        public decimal MinBalRedsAmt { get; set; }
        public decimal MinBalRedsUnit { get; set; }
        public bool IsPublic { get; set; }
        public int WHTDueDate { get; set; }
        public string WHTDueDateDesc { get; set; }
        public int MFeeMethod { get; set; }
        public string MFeeMethodDesc { get; set; }
        public int SharingFeeCalculation { get; set; }
        public string SharingFeeCalculationDesc { get; set; }
        public string IssueDate { get; set; }
        public decimal RemainingBalanceUnit { get; set; }
        public int DefaultPaymentDate { get; set; }
        public bool BitNeedRecon { get; set; }
        public string DividenDate { get; set; }
        public string OJKLetter { get; set; }
        public string NPWP { get; set; }
        public bool BitSinvestFee { get; set; }
        public bool BitInternalClosePrice { get; set; }
        public bool BitInvestmentHighRisk { get; set; }

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
        public string KPDNoContract { get; set; }
        public string KPDDateFromContract { get; set; }
        public string KPDDateToContract { get; set; }
        public string KPDNoAdendum { get; set; }
        public string KPDDateAdendum { get; set; }
        public string CutOffTime { get; set; }
        public string ProspectusUrl { get; set; }
        public string FactsheetUrl { get; set; }
        public string ImageUrl { get; set; }
        public string ISIN { get; set; }
        public string EntryApproveTimeCutoff { get; set; }
        public string OJKEffectiveStatementLetterDate { get; set; }
        public decimal MinBalSwitchToAmt { get; set; }
        public bool BitSyariahFund { get; set; }
    }


    public class FundCombo
    {
        public int FundPK { get; set; }
        public string ID { get; set; }
        public string FundID { get; set; }
        public string Name { get; set; }
    }

    public class FundLookup
    {
        //10 Field
        public int FundPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public decimal CustodyFeePercent { get; set; }
        public decimal ManagementFeePercent { get; set; }
        public decimal SubscriptionFeePercent { get; set; }
        public decimal RedemptionFeePercent { get; set; }
        public decimal SwitchingFeePercent { get; set; }
    }


    public class UnitRegistryFund
    {
        public string UnitRegistrySelected { get; set; }
        public string UnitRegistryType { get; set; }
        public string ApprovedUsersID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }


}