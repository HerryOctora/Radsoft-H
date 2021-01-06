using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundClientCashRef
    {
        public int FundClientCashRefPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int FundCashRefPK { get; set; }
        public string FundCashRefID { get; set; }
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

    public class FundClientCashRefCombo
    {
        public int FundClientCashRefPK { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int FundCashRefPK { get; set; }
        public string FundCashRefID { get; set; }
 
    }
  
}