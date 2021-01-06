using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FixedAssetAllocation
    {
        public int FixedAssetPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public int AutoNo { get; set; }
        public string Notes { get; set; }
        public int DepartmentPK { get; set; }
        public decimal AllocationPercent { get; set; }
        public string DepartmentID { get; set; }
        public string LastUsersID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }

    }
}