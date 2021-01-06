using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFSModel
{
    public class PPH21Setup
    {
        public int PPH21SetupPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public string StatusDesc { get; set; }
        public string Date { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        public decimal Percentage { get; set; }
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
}
