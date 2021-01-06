using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class HighRiskValueSetup
    {
        public int MasterValuePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string DescOne { get; set; }
        public string DescTwo { get; set; }
        public int Priority { get; set; }
        public bool IsHighRisk { get; set; }
        public int Score { get; set; }
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
        public int RiskCDD { get; set; }
        public string RiskCDDDesc { get; set; }
    }

    public class HighRiskValueSetupCombo
    {
        public int MasterValuePK { get; set; }
        public string Code { get; set; }
        public string DescOne { get; set; }
    }
}