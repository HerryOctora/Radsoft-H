using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class AgentCSRFund
    {
        public int AgentCSRFundPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string Description { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }

        public int FundJournalAccountPK { get; set; }
        public string FundJournalAccountName { get; set; }
        public decimal DanaProgramPercent { get; set; }
        public decimal PPH23Percent { get; set; }
        public int AgentCSRExpenseAccountPK { get; set; }
        public string AgentCSRExpenseAccountName { get; set; }

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

}
