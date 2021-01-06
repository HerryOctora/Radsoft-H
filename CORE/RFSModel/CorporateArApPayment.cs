using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class CorporateArApPayment
    {
        public int CorporateArApPaymentPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public bool Selected { get; set; }
        public string ValueDate { get; set; }
        public int CorporateArApPK { get; set; }
        public string CorporateArApID { get; set; }
        public int CashRefPK { get; set; }
        public string CashRefID { get; set; }
        public string CheckNo { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public decimal LateAmountCharge { get; set; }
        

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
}