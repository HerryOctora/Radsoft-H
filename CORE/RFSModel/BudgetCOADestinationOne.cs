using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class BudgetCOADestinationOne
    {
        public int BudgetCOADestinationOnePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int COADestinationOnePK { get; set; }
        public string COADestinationOneID { get; set; }
        public string COADestinationOneName { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public decimal Amount { get; set; }
        public int MISCostCenterPK { get; set; }
        public string MISCostCenterID { get; set; }
        public string MISCostCenterName { get; set; }
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