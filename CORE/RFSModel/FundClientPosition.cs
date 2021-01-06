using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundClientPosition
    {
        public int FundClientPositionPK { get; set; }
        public string Date { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public decimal UnitAmount { get; set; }
        public string SID { get; set; }
        public string IFUA { get; set; }
        public decimal AUM { get; set; }
        public decimal AvgNAV { get; set; }
        public decimal CostValue { get; set; }
        public decimal Unrealized { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }


    public class SchedulerChecking
    {
        public DateTime Date { get; set; }
        public string DataProblemDesc { get; set; }
        public int FundPK { get; set; }
        public int FundClientPK { get; set; }
        public string FundName { get; set; }
        public string FundClientName { get; set; }
        public string Reason { get; set; }
    }


}