using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class AccountingReportTemplate
    {
        public int AccountingReportTemplatePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ReportName { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string RowType { get; set; } // 1.Header, 2. Child, 3. Total Header 
        public int SourceAccount { get; set; } // master account
        public string SourceAccountName { get; set; }
        public string Operator { get; set; } // 1.+ [plus], 2.- [minus]
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
        //
        public string RecordFrom { get; set; }
        public int NewSourceAccount { get; set; }
    }

    public class ReportNameCombo
    {
        public int AccountingReportTemplatePK { get; set; }
        public int HistoryPK { get; set; }
        public string ReportName { get; set; }
    }
    
    public class RecordFromCombo
    {
        public int AccountingReportTemplatePK { get; set; }
        public int HistoryPK { get; set; }
        public string RecordFrom { get; set; }
    }
}