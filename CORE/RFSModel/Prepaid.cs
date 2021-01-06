using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Prepaid
    {
        public int PrepaidPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string PaymentDate { get; set; }
        public string PeriodStartDate { get; set; }
        public string PeriodEndDate { get; set; }
        public int PaymentJournalNo { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public int PrepaidAccount { get; set; }
        public string PrepaidAccountID { get; set; }
        public decimal PrepaidAmount { get; set; }
        public int PrepaidCurrencyPK { get; set; }
        public string PrepaidCurrencyID { get; set; }
        public decimal PrepaidCurrencyRate { get; set; }
        public int DebitAccount1 { get; set; }
        public string DebitAccount1ID { get; set; }
        public decimal DebitAmount1 { get; set; }
        public int DebitCurrencyPk1 { get; set; }
        public string DebitCurrencyPk1ID { get; set; }
        public decimal DebitCurrencyRate1 { get; set; }
        public int DebitAccount2 { get; set; }
        public string DebitAccount2ID { get; set; }
        public decimal DebitAmount2 { get; set; }
        public int DebitCurrencyPk2 { get; set; }
        public string DebitCurrencyPk2ID { get; set; }
        public decimal DebitCurrencyRate2 { get; set; }
        public int DebitAccount3 { get; set; }
        public string DebitAccount3ID { get; set; }
        public decimal DebitAmount3 { get; set; }
        public int DebitCurrencyPk3 { get; set; }
        public string DebitCurrencyPk3ID { get; set; }
        public decimal DebitCurrencyRate3 { get; set; }
        public int DebitAccount4 { get; set; }
        public string DebitAccount4ID { get; set; }
        public decimal DebitAmount4 { get; set; }
        public int DebitCurrencyPk4 { get; set; }
        public string DebitCurrencyPk4ID { get; set; }
        public decimal DebitCurrencyRate4 { get; set; }
        public int CreditAccount1 { get; set; }
        public string CreditAccount1ID { get; set; }
        public decimal CreditAmount1 { get; set; }
        public int CreditCurrencyPk1 { get; set; }
        public string CreditCurrencyPk1ID { get; set; }
        public decimal CreditCurrencyRate1 { get; set; }
        public int CreditAccount2 { get; set; }
        public string CreditAccount2ID { get; set; }
        public decimal CreditAmount2 { get; set; }
        public int CreditCurrencyPk2 { get; set; }
        public string CreditCurrencyPk2ID { get; set; }
        public decimal CreditCurrencyRate2 { get; set; }
        public int CreditAccount3 { get; set; }
        public string CreditAccount3ID { get; set; }
        public decimal CreditAmount3 { get; set; }
        public int CreditCurrencyPk3 { get; set; }
        public string CreditCurrencyPk3ID { get; set; }
        public decimal CreditCurrencyRate3 { get; set; }
        public int CreditAccount4 { get; set; }
        public string CreditAccount4ID { get; set; }
        public decimal CreditAmount4 { get; set; }
        public int CreditCurrencyPk4 { get; set; }
        public string CreditCurrencyPk4ID { get; set; }
        public decimal CreditCurrencyRate4 { get; set; }
        public int PrepaidExpAccount { get; set; }
        public string PrepaidExpAccountID { get; set; }
        public int OfficePK { get; set; }
        public string OfficeID { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedTime { get; set; }
        public bool Revised { get; set; }
        public string RevisedBy { get; set; }
        public string RevisedTime { get; set; }
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

    public class PrepaidAddNew
    {
        public int PrepaidPK { get; set; }
        public long HistoryPK { get; set; }
        public string Message { get; set; }
    }


}