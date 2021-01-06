using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class SwitchingFund
    {
        public int SwitchingFundPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundFromPK { get; set; }
        public string FundFromID { get; set; }
        public int FundToPK { get; set; }
        public string FundToID { get; set; }
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

    public class SwitchingFundLookup
    {
        //10 Field
        public int FundPKFrom { get; set; }
        public string FundIDFrom { get; set; }
        public string FundNameFrom { get; set; }
        public int FundPKTo { get; set; }
        public string FundIDTo { get; set; }
        public string FundNameTo { get; set; }
       
    }

}