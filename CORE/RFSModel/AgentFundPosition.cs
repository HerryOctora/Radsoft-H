using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class AgentFundPosition
    {
        public int AgentFundPositionPK { get; set; }
        public string Date { get; set; }
        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public string AgentName { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public decimal UnitAmount { get; set; }
        public string EntryUsersID { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
        public string AgentFrom { get; set; }
        public string FundFrom { get; set; }
        public string ValueDateFrom { get; set; }
        public string ValueDateTo { get; set; }
    }

  

}