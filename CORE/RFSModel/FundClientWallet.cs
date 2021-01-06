using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundClientWallet
    {
        public int FundClientWalletPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public string WalletPK { get; set; }
        //public string WalletID { get; set; }
        public int BankPK { get; set; }
        public string BankID { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; } 
        public int WalletTransactionType { get; set; }
        public int WalletTransactionStatus { get; set; }
        public bool IsProcessed { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
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