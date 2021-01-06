using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class BankBranch
    {
        public int BankBranchPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int BankPK { get; set; }
        public string BankID { get; set; }
        public string ID { get; set; }
        public int Type { get; set; }
        public int InterestDaysType { get; set; }
        public string TypeDesc { get; set; }
        public string Address { get; set; }
        public int City { get; set; }
        public string CityDesc { get; set; }
        public string ContactPerson { get; set; }
        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string Fax3 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Attn1 { get; set; }
        public string Attn2 { get; set; }
        public string Attn3 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string PTPCode { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNo { get; set; }
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


        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public bool BitIsEnabled { get; set; }


    }


    public class BankBranchCombo
    {
        public int BankBranchPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }

    }

}