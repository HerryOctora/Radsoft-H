using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundFee
    {
        public int FundFeePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string PaymentDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal ManagementFeePercent { get; set; }
        public decimal CustodiFeePercent { get; set; }
        public decimal AuditFeeAmount { get; set; }
        public int ManagementFeeDays { get; set; }
        public int CustodiFeeDays { get; set; }
        public int AuditFeeDays { get; set; }
        public int SInvestFeeDays { get; set; }
        public int PaymentModeOnMaturity { get; set; }
        public string PaymentModeOnMaturityDesc { get; set; }
        public decimal SwitchingFeePercent { get; set; }
        public int FeeTypeManagement { get; set; }
        public string FeeTypeManagementDesc { get; set; }
        public int FeeTypeSubscription { get; set; }
        public string FeeTypeSubscriptionDesc { get; set; }
        public int FeeTypeRedemption { get; set; }
        public string FeeTypeRedemptionDesc { get; set; }
        public int FeeTypeSwitching { get; set; }
        public string FeeTypeSwitchingDesc { get; set; }
        public decimal DateOfPayment { get; set; }
        public decimal SinvestMoneyMarketFeePercent { get; set; }
        public decimal SinvestBondFeePercent { get; set; }
        public decimal SinvestEquityFeePercent { get; set; }
        public bool BitPendingSubscription { get; set; }
        public bool BitPendingSwitchIn { get; set; }
        public int FundFeeSetupPK { get; set; }
        public string Agent { get; set; }
        public bool Selected { get; set; }
        public string DateAmortize { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
        public decimal RangeTo { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal MiFeePercent { get; set; }
        public decimal MiFeeAmount { get; set; }
        public string ValueDateCopy { get; set; }
        public bool Range { get; set; }
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
        public decimal MovementFeeAmount { get; set; }
        public decimal OtherFeeOneAmount { get; set; }
        public decimal OtherFeeTwoAmount { get; set; }
        public decimal OtherFeeThreeAmount { get; set; }
        public decimal CBESTEquityAmount { get; set; }
        public decimal CBESTCorpBondAmount { get; set; }
        public decimal CBESTGovBondAmount { get; set; }
        public int AccruedInterestCalculation { get; set; }
        public string AccruedInterestCalculationDesc { get; set; }
        public bool TaxInterestDeposit { get; set; }

        public bool BitActDivDays { get; set; }
 
    }

    public class SetFundFeeSetup
    {
        public int FundFeeSetupPK { get; set; }
        public string Agent { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string DateAmortize { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
        public decimal RangeTo { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal MiFeePercent { get; set; }
        public decimal MiFeeAmount { get; set; }
        public int FundPK { get; set; }
        public string FundName { get; set; }
        public string Date { get; set; }
        public string EntryUsersID { get; set; }
    }

    public class CheckRangeTo
    {
        public bool RangeTo { get; set; }
        public int FundPK { get; set; }
        public string Date { get; set; }
        public int FeeType { get; set; }
    }
}