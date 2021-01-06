using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundWindowRedemption
    {
        public int FundWindowRedemptionPK { get; set; }
        public bool Selected { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string FirstRedemptionDate { get; set; }
        public string FirstDivDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int PaymentPeriod { get; set; }
        public string PaymentPeriodDesc { get; set; }
        public int VariableDate { get; set; }
        public int PaymentDate { get; set; }
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
        public string FundCashRefAccountFrom { get; set; }
        public string FundCashRefAccountTo { get; set; }
        public string FundCashRefAccountNoFrom { get; set; }
        public string FundCashRefAccountNoTo { get; set; }
        public string BankFrom { get; set; }
        public string BankTo { get; set; }
        public string ParamDateFrom { get; set; }
        public string ParamDateTo { get; set; }

        public string Description { get; set; }
    }



    public class SetWindowRedemptionDetail
    {
        public int FundWindowRedemptionPK { get; set; }
        public string MaxRedemptionDate { get; set; }
        public string DividenDate { get; set; }
        public string PaymentDate { get; set; }
        public string EntryUsersID { get; set; }
        public string LastUpdate { get; set; }
    }

}
