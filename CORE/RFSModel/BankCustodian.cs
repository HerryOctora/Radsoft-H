using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class BankCustodian
    {
        public int BankCustodianPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public string Address { get; set; }
        public string Branch { get; set; }    
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
        public string BICode { get; set; }
        public int FundAccountPK { get; set; }
        public string FundAccountID { get; set; }
        public string FundAccountName { get; set; }
        public string ClearingCode { get; set; }
        public string RTGSCode { get; set; }
        public bool BitRDN { get; set; }
        public bool BitSyariah { get; set; }
        public decimal FeeLLG { get; set; }
        public decimal FeeRTGS { get; set; }
        public decimal MinforRTGS { get; set; }
        public int JournalRoundingMode { get; set; }
        public string JournalRoundingModeDesc { get; set; }
        public int JournalDecimalPlaces { get; set; }
        public string JournalDecimalPlacesDesc { get; set; }
        //public int NAVRoundingMode { get; set; }
        //public string NAVRoundingModeDesc { get; set; }
        //public int NAVDecimalPlaces { get; set; }
        //public string NAVDecimalPlacesDesc { get; set; }
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


    public class BankCustodianCombo
    {
        public int BankCustodianPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }

    }

}