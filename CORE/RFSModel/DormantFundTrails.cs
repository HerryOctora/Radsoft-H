using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class DormantFundTrails
    {
        public int DormantFundTrailsPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ValueDate { get; set; }
        public bool BitDormant { get; set; }
        public string ActivateDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string DormantDate { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }

        public string FundFrom { get; set; }
    }

    public class ActivateFund
    {
            public int FundPK { get; set; }
            public string ID { get; set; }
            public string FundID { get; set; }
            public string Name { get; set; }
            public bool StatusDormant { get; set; }
        
    }
}
