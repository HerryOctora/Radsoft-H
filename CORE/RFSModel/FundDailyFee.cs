using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundDailyFee
    {
        public string Date { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal ManagementFeeAmount { get; set; }
        public decimal CustodiFeeAmount { get; set; }
        public decimal SubscriptionFeeAmount { get; set; }
        public decimal RedemptionFeeAmount { get; set; }
        public decimal SwitchingFeeAmount { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }
}