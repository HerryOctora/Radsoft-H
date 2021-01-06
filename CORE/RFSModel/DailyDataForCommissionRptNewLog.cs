using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace RFSModel
{
    public class DailyDataForCommissionRptNewLog
    {

        public int DailyDataForCommissionRptNewLogPK { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string UsersID { get; set; }
        public string GenerateTime { get; set; }
        public string Fund { get; set; }
        public string Client { get; set; }
        public string LastUpdate { get; set; }

    }
}
