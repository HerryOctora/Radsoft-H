using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Revenue
    {
        public int RevenuePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int ReksadanaTypePK { get; set; }
        public string ReksadanaTypeID { get; set; }
        public int ReportPeriodPK { get; set; }
        public string ReportPeriodID { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public int Characteristic { get; set; }
        public string CharacteristicID { get; set; }
        public bool New { get; set; }
        public decimal MGTFee { get; set; }
        public decimal January { get; set; }
        public decimal February { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal August { get; set; }
        public decimal September { get; set; }
        public decimal October { get; set; }
        public decimal November { get; set; }
        public decimal December { get; set; }
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