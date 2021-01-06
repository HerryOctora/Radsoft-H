using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class AdvisoryFee
    {
        public int AdvisoryFeePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }

        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remarks { get; set; }
        public decimal ProjectValue { get; set; }
        public string Status1Project { get; set; }
        public DateTime Status1ProjectDate { get; set; }
        public string Status2Project { get; set; }
        public DateTime Status2ProjectDate { get; set; }
        public string Status3Project { get; set; }
        public DateTime Status3ProjectDate { get; set; }
        public string Status4Project { get; set; }
        public DateTime Status4ProjectDate { get; set; }
        public string Status5Project { get; set; }
        public DateTime Status5ProjectDate { get; set; }

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




    public class AdvisoryFeeAddNew
    {
        public int AdvisoryFeePK { get; set; }
        public int HistoryPK { get; set; }
        public string Message { get; set; }
    }
}



