using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundJournalReference
    {
        public int FundJournalReferencePK { get; set; }
        public int PeriodPK { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public Int64 No { get; set; }
    }
}