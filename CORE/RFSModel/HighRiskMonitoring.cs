using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class ValidateUnitRegistry
    {
        public string Reason { get; set; }
        public string Notes { get; set; }
        public int FundClientPK { get; set; }
    }


    public class HighRiskMonitoring
    {
        public int HighRiskMonitoringPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public int InvestmentPK { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int InvestmentNo { get; set; }
        public int KYCRiskProfile { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string NAVDate { get; set; }
        public decimal CashAmount { get; set; }
        public int FundPK { get; set; }
        public string ClientType { get; set; }
        public string Reason { get; set; }
        public int Result { get; set; }
        public int HighRiskType { get; set; }
        public string Description { get; set; }
        public bool BitIsSuspend { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string UnApprovedUsersID { get; set; }
        public string UnApprovedTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
        public string UpdateUsersID { get; set; }
        public string UpdateTime { get; set; }

        public decimal Amount { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public int HighRiskMonStatus { get; set; }
        public string HighRiskMonStatusDesc { get; set; }
    }

}