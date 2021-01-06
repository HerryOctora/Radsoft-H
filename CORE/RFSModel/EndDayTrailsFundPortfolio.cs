using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class EndDayTrailsFundPortfolio
    {
        public int EndDayTrailsFundPortfolioPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public bool BitValidate { get; set; }
        public string ValueDate { get; set; }
        public string LogMessages { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
        public string FundFrom { get; set; }
        public string stringEndDayTrailsFundPortfolioFrom { get; set; }
        public int Result { get; set; }


    }


    public class NavProjection
    {
        public DateTime ValueDate { get; set; }
        public string FundName { get; set; }
        public decimal Nav { get; set; }
        public decimal AUM { get; set; }
        public decimal Compare { get; set; }
        public decimal ChangeNav { get; set; }
        public decimal ChangeNavPercent { get; set; }
        
            
    }

    public class AccountJournal
    {
        public decimal TotalAccount1 { get; set; }
        public decimal TotalAccount63 { get; set; }
        public decimal AUM { get; set; }
        public decimal Unit { get; set; }
        public decimal NAV { get; set; }
        public decimal NAVYesterday { get; set; }
        public decimal ChangeNAV { get; set; }
        public decimal ChangeNAVPercent { get; set; }
        public string LastNavDate { get; set; }

    }



    public class UpdateEndDayTrailsFundPortfolio
    {
        public int FundPK { get; set; }
        public int InstrumentPK { get; set; }
        public string ValueDate { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public string MaturityDate { get; set; }
        public decimal Balance { get; set; }
        public string AcqDate { get; set; }
        public decimal InterestPercent { get; set; }
        public string Category { get; set; }
        public string InterestDaysTypeDesc { get; set; }
        public string InterestPaymentTypeDesc { get; set; }
        public string PaymentModeOnMaturityDesc { get; set; }
        public string UpdateUsersID { get; set; }

    }
}