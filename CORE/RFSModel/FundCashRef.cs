using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundCashRef
    {
        public int FundCashRefPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public bool IsPublic { get; set; }
        public bool bitdefaultinvestment { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public int FundJournalAccountPK { get; set; }
        public string FundJournalAccountName { get; set; }
        public string FundJournalAccountID { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public int BankBranchPK { get; set; }
        public string BankBranchID { get; set; }
        public string BankBranchName { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
        public string SafeKeepingAccountNo { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string Remark { get; set; }
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

    public class FundCashRefCombo
    {
        public int FundCashRefPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int FundJournalAccountPK { get; set; }
        public int FundPK { get; set; }
    }


}