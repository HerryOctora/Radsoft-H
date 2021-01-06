using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class CustodiFeeSetup
    {
        public int CustodiFeeSetupPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal FeePercent { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
        public int CustodiFeeType { get; set; }
        public string CustodiFeeTypeDesc { get; set; }
        public decimal AUMTo { get; set; }
        public decimal AUMFrom { get; set; }
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

    public class SetCustodiFeeSetup
    {
        public int CustodiFeeSetupPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
        public int CustodiFeeType { get; set; }
        public string CustodiFeeTypeDesc { get; set; }
        public decimal FeePercent { get; set; }
        public decimal AUMTo { get; set; }
        public decimal AUMFrom { get; set; }
        public int FundPK { get; set; }
        public string FundName { get; set; }
        public string Date { get; set; }
        public string EntryUsersID { get; set; }
    }

    public class CheckAUMTo
    {
        public bool AUMTo { get; set; }
        public int FundPK { get; set; }
        public string Date { get; set; }
        public int FeeType { get; set; }
    }

}