using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class TBReconcileStatus
    {
        public string FundID { get; set; }
        public string StatusReconcile { get; set; }
        public string StatusOpen { get; set; }
        public string UsersID { get; set; }
    }


    public class TrialBalance
    {
        public string Header { get; set; }
        public string ID { get; set; }
        public string Name {get; set; }
        public string ParentName { get; set; }
        public string Currency { get; set; }
        public bool BitIsChange { get; set; }
        public bool BitIsGroups { get; set; }
        public decimal PreviousBaseBalance { get; set; }
        public decimal Movement { get; set; }
        public decimal CurrentBaseBalance { get; set; }
        public string BKBalance { get; set; }

    }
    public class TBReconcileTemp
    {
        public string Date { get; set; }
        public int TBReconcileTempPK { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int AccountPK { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountImportData { get; set; }
        public decimal AmountSystem { get; set; }
        public decimal Difference { get; set; }
        public string UsersID { get; set; }
       
    }


    public class TBReconcileTempRpt
    {
        public string ValueDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string Header { get; set; }
        public decimal Asset { get; set; }
        public decimal Liabilities { get; set; }
        public decimal Aum { get; set; }
        public decimal Unit { get; set; }
        public decimal Nav { get; set; }
        public string Name { get; set; }
        public decimal PrevBalance { get; set; }
        public decimal Movement { get; set; }
        public decimal EndBalance { get; set; }
        public string DownloadMode { get; set; }

    }

    public class DetailMovement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BaseCredit { get; set; }
        public decimal BaseDebit { get; set; }

    }


    public class TBReconcileLog
    {
        public string Date { get; set; }
        public int FundPK { get; set; }
        public int UsersID { get; set; }
        public string LockTime { get; set; }
        public string ReleaseTime { get; set; }
    }

}
