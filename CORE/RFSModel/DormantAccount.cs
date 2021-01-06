using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class DormantAccount
    {
        public int FundClientPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public bool BitIsSuspend { get; set; }
        public string LastHoldingDate { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; } 
        public string LastUpdate { get; set; }
        public decimal CashAmount { get; set; }

        public string IFUACode { get; set; }
        public string FundName { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Nav { get; set; }
        public string LastNavDate { get; set; }

        // Parameters

        public int ParamMonth { get; set; }
        public DateTime ValueDate { get; set; }
    }

}
