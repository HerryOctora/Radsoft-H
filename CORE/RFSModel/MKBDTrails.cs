using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class MKBDTrails
    {
        public int MKBDTrailsPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public bool BitValidate { get; set; }
        public string ValueDate { get; set; }
        public decimal Row113B { get; set; }
        public decimal Row173B { get; set; }
        public decimal Row104G { get; set; }
        public string LogMessages { get; set; }
        public string LastUsersToText { get; set; }
        public string ToTextTime { get; set; }
        public int GenerateToTextCount { get; set; }
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

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string VersionMode { get; set; }
    }
}