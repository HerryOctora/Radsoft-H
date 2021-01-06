using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class ParamCashierBySelected
    {
        public string stringCashierPaymentSelected { get; set; }
    }


    public class Cashier
    {
        public int CashierPK { get; set; }
        public long CashierID { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string ValueDate { get; set; }
        public string Type { get; set; }
        public int RefNo { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string DebitCredit { get; set; }
        public int DebitCurrencyPK { get; set; }
        public string DebitCurrencyID { get; set; }
        public int CreditCurrencyPK { get; set; }
        public string CreditCurrencyID { get; set; }
        public int DebitCashRefPK { get; set; }
        public string DebitCashRefID { get; set; }
        public string DebitCashRefName { get; set; }
        public int CreditCashRefPK { get; set; }
        public string CreditCashRefID { get; set; }
        public string CreditCashRefName { get; set; }
        public int DebitAccountPK { get; set; }
        public string DebitAccountID { get; set; }
        public string DebitAccountName { get; set; }
        public int CreditAccountPK { get; set; }
        public string CreditAccountID { get; set; }
        public string CreditAccountName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal DebitCurrencyRate { get; set; }
        public decimal CreditCurrencyRate { get; set; }
        public decimal BaseDebit { get; set; }
        public decimal BaseCredit { get; set; }
        public decimal PercentAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public int OfficePK { get; set; }
        public string OfficeID { get; set; }
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public int ConsigneePK { get; set; }
        public string ConsigneeID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }   
        public int JournalNo { get; set; }
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
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string ParamMode { get; set; }
        public string ParamReference { get; set; }
        public string ParamReferenceFrom { get; set; }
        public string ParamReferenceTo { get; set; }
        public string ParamUserID { get; set; }
        public string DocRef { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class ReferenceFromCashier
    {
        public string Reference;
    }

    public class CashierIDFromCashier
    {
        public long CashierID;
        public string Reference;
    }

    public class CashierAddNew
    {
        public int CashierPK { get; set; }
        public int HistoryPK { get; set; }
        public string Message { get; set; }
    }

    public class ReferenceDetail
    {
        public string ValueDate { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public string DebitCredit { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string DepartmentID { get; set; }
    }
}