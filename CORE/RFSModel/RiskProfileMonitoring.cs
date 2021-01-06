using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class RiskProfileMonitoring
    {
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int FundRiskProfilePK { get; set; }
        public string FundRiskProfile { get; set; }
        public int InvestorRiskProfilePK { get; set; }
        public string InvestorRiskProfile { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public decimal UnitPosition { get; set; }
        public decimal CashPosition { get; set; }
        public string SID { get; set; }
        public string IFUA { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string LastUpdate { get; set; }
    }



}
