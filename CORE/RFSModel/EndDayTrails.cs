using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{


    public class ParamEndDayTrailsBySelected
    {
        public string FundFrom { get; set; }
    }

    public class EndDayTrails
    {
        public int EndDayTrailsPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public bool BitValidate { get; set; }
        public string ValueDate { get; set; }
        public string LogMessages { get; set; }
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
        public string FundClientFrom { get; set; }
        public string ValueDateFrom { get; set; }
        public string ValueDateTo { get; set; }
        public string FundText { get; set; }
        public string FundClientText { get; set; }
        public int Result { get; set; }
        public string stringEndDayTrailsFrom { get; set; }

        public string FundID { get; set; }
        public string AccountName { get; set; }
    }

}