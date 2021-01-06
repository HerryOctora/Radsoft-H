using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Account
    {
        public int AccountPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public bool Groups { get; set; }
        public int Levels { get; set; }
        public int ParentPK { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
        public int Depth { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public int OfficePK { get; set; }
        public string OfficeID { get; set; }
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public bool Show { get; set; }
        public int ParentPK1 { get; set; }
        public int ParentPK2 { get; set; }
        public int ParentPK3 { get; set; }
        public int ParentPK4 { get; set; }
        public int ParentPK5 { get; set; }
        public int ParentPK6 { get; set; }
        public int ParentPK7 { get; set; }
        public int ParentPK8 { get; set; }
        public int ParentPK9 { get; set; }
        public int MKBDMapping { get; set; }
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

    public class AccountCombo
    {
        public int AccountPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
    }
}

