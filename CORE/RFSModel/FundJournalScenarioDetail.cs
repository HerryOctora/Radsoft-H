using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundJournalScenarioDetail
    {
        public int FundJournalScenarioPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public int FundJournalAccountPK { get; set; }
        public string FundJournalAccountID { get; set; }
        public string FundJournalAccountName { get; set; }
        public string DebitCredit { get; set; }
        public string LastUsersID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }
}




