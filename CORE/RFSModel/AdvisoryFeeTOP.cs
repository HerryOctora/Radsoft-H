using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class AdvisoryFeeTOP
    {
        public int AdvisoryFeeTOPPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }

        public int AdvisoryFeePK { get; set; }
        public decimal TOPPercent { get; set; }
        public DateTime TOPDate { get; set; }



        public string LastUsersID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }
}
