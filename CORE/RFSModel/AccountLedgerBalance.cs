using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class AccountLedgerTrialBalance
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

    }
    public class AccountLedgerBalance
    {
        public string Date { get; set; }
        public int AccountLedgerBalancePK { get; set; }
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

    public class AccountLedgerBalanceActivity
    {
        public string Date { get; set; }
        public int AccountPK { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public string UsersID { get; set; }

    }

 
}
