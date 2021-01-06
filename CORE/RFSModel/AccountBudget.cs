using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class AccountBudget
    {
        public int AccountBudgetPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Version { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public int AccountPK { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public int OfficePK { get; set; }
        public string OfficeID { get; set; }
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal Balance { get; set; }
        public int Month { get; set; }
        public string MonthDesc { get; set; }
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
        public int IPeriodPK { get; set; }

        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public string AgentName { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }

        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public int ConsigneePK { get; set; }
        public string ConsigneeID { get; set; }
        public string ConsigneeName { get; set; }

    }

  

    public class AccountBudgetCombo
    {
        public int AccountBudgetPK { get; set; }
        public string Version { get; set; }
    }
}
