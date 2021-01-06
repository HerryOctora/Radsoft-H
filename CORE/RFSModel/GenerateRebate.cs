using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class GenerateRebate
    {
        public int GenerateRebatePK { get; set; }
        public int GenerateRebateTempPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public bool Selected { get; set; }
        public string JournalDate { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientName { get; set; }
        public string FundName { get; set; }
        public int FundPK { get; set; }
        public decimal FeeRebate { get; set; }
        public decimal ManagementFee { get; set; }
        public decimal AUM { get; set; }
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