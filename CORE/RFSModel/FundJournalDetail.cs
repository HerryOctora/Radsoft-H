using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundJournalDetail
    {
        public int FundJournalPK { get; set; }
        public int AutoNo { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public int FundJournalAccountPK { get; set; }
        public string FundJournalAccountID { get; set; }
        public string FundJournalAccountName { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public string DetailDescription { get; set; }
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




