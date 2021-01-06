using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class EndYearClosing
    {
        public int EndYearClosingPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public int Mode { get; set; }
        public string ModeDesc { get; set; }
        public string LogMessages { get; set; }
        public int FundPK { get; set; }
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

        // Corporate Journal

        public int AccountPKFrom { get; set; }
        public int AccountPKTo { get; set; }
        public DateTime Date { get; set; }

        // Fund Journal

        public int FundJournalAccountPKFrom { get; set; }
        public int FundJournalAccountPKTo { get; set; }
    }
}