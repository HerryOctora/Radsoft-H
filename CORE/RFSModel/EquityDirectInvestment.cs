using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class EquityDirectInvestment
    {
        public int EquityDirectInvestmentPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }

        public int DirectInvestmentPK { get; set; }
        public decimal Profit { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }

        
        public string LastUsersID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }
}
