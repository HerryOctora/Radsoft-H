using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class JournalDetail
    {
        public int JournalPK { get; set; }
        public int AutoNo { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public int AccountPK { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public int OfficePK { get; set; }
        public string OfficeID { get; set; }
        public string OfficeName { get; set; }
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public string AgentName { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public int ConsigneePK { get; set; }
        public string ConsigneeID { get; set; }
        public string ConsigneeName { get; set; }
        public string DetailDescription { get; set; }
        public string DocRef { get; set; }
        public string DebitCredit { get; set; }
        public decimal Amount { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal BaseDebit { get; set; }
        public decimal BaseCredit { get; set; }
        public string LastUsersID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }
}




