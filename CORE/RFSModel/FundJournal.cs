using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundJournal
    {
        public int FundJournalPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string ValueDate { get; set; }
        public int TrxNo { get; set; }
        public string TrxName { get; set; }
        public int RefNo { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public string TypeDesc { get; set; }
        public string Description { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedTime { get; set; }
        public bool Reversed { get; set; }
        public int ReverseNo { get; set; }
        public string ReversedBy { get; set; }
        public string ReversedTime { get; set; }
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
        public string ParamMode { get; set; }
        public string ParamPeriod { get; set; }
        public string ParamPostDate { get; set; }
        public string ParamPostDateFrom { get; set; }
        public string ParamPostDateTo { get; set; }
        public int ParamPostFundJournalFrom { get; set; }
        public int ParamPostFundJournalTo { get; set; }
        public string ParamUserID { get; set; }

    }

    public class FundJournalAddNew
    {
        public int FundJournalPK { get; set; }
        public long HistoryPK { get; set; }
        public string Message { get; set; }
    }


}