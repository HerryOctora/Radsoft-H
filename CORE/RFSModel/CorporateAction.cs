using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class CorporateAction
    {

        public int CorporateActionPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public string ValueDate { get; set; }
        public string ExDate { get; set; }
        public string RecordingDate { get; set; }
        public string PaymentDate { get; set; }
        public string ExpiredDate { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Earn { get; set; }
        public decimal Hold { get; set; }
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
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int TaxDueDate { get; set; }
        public string TaxDueDateDesc { get; set; }
        
    }

   
}
