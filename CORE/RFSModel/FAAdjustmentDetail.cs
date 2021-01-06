using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FAAdjustmentDetail
    {
        public int FAAdjustmentPK { get; set; }
        public int AutoNo { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int FACOAAdjustmentPK { get; set; }
        public string FACOAAdjustmentID { get; set; }
        public string FACOAAdjustmentName { get; set; }
        public int FundJournalAccountPK { get; set; }
        public string FundJournalAccountID { get; set; }
        public string FundJournalAccountName { get; set; }
        public string DebitCredit { get; set; }
        public decimal Amount { get; set; }
        public string LastUsersID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }
}




