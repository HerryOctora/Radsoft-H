using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class ManageURTransaction
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string FundFrom { get; set; }
        public string FundClientFrom { get; set; }
        public string FundText { get; set; }
        public string FundClientText { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public int Status { get; set; }
        public int TrxType { get; set; }
        public string TrxTypeDesc { get; set; }
        public int SysNo { get; set; }
        public DateTime Date { get; set; }
        public bool Selected { get; set; }
        public string FundClientName { get; set; }
        public string FundID { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }

     

    }

  

}
