using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Signature
    {
        public int SignaturePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool BitIsDefault1 { get; set; }
        public bool BitIsDefault2 { get; set; }
        public bool BitIsDefault3 { get; set; }
        public bool BitIsDefault4 { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string UpdateUsersID { get; set; }
        public string UpdateTime { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }

    public class SignatureCombo
    {
        public int SignaturePK { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}
