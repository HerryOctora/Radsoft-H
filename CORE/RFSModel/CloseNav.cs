using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class CloseNav
    {
        public int CloseNavPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public bool Selected { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal Nav { get; set; }
        public decimal AUM { get; set; }
        public decimal OutstandingUnit { get; set; }

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
        public string FundFrom { get; set; }
        public string CloseNavSelected { get; set; }
        public bool Approved1 { get; set; }
        public bool Approved2 { get; set; }

        public int Result { get; set; }
    }

    public class CloseNavByDateAndFundPK
    {
       public decimal Nav { get; set; }
    }

    public class NavReconcile
    {
        public int NavReconcilePK { get; set; }
        public bool Selected { get; set; }
        public string ValueDate { get; set; }
        public string FundName { get; set; }
        public decimal NavSystem { get; set; }
        public decimal NavRecon { get; set; }
        public int Type { get; set; }
        public string EntryUsersID { get; set; }
    }

    public class CloseNavByDate
    {
        public DateTime Date { get; set; }
        public decimal Nav { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }

}