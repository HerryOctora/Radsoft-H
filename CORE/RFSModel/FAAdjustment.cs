using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FAAdjustment
    {
        public int FAAdjustmentPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string ValueDate { get; set; }
        public int RefNo { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
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

    public class FAAdjustmentAddNew
    {
        public int FAAdjustmentPK { get; set; }
        public int HistoryPK { get; set; }
        public string Message { get; set; }
    }


    public class NavForecast
    {
        public DateTime ValueDate { get; set; }
        public string FundName { get; set; }
        public decimal Nav { get; set; }
        public decimal AUM { get; set; }
    }

    public class TBForecast
    {
        public DateTime ValueDate { get; set; }
        public string FundName { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
    }

}