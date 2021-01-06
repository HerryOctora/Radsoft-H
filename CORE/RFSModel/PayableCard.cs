using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RFSModel
{
    public class PayableCard
    {
        public int PayableCardPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int ConsigneePK { get; set; }
        public string ConsigneeID { get; set; }
        public string PoNo { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public string TermDesc1 { get; set; }
        public decimal AmountTerm1 { get; set; }
        public string TermDesc2 { get; set; }
        public decimal AmountTerm2 { get; set; }
        public string TermDesc3 { get; set; }
        public decimal AmountTerm3 { get; set; }
        public string TermDesc4 { get; set; }
        public decimal AmountTerm4 { get; set; }
        public string TermDesc5 { get; set; }
        public decimal AmountTerm5 { get; set; }
        public decimal TotalPaid { get; set; }
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

    public class TotalPaid
    {

        public decimal TotalAmount { get; set; }
        public decimal AmountTerm1 { get; set; }
        public decimal AmountTerm2 { get; set; }
        public decimal AmountTerm3 { get; set; }
        public decimal AmountTerm4 { get; set; }
        public decimal AmountTerm5 { get; set; }

    }
}