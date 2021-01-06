using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Journal
    {
        public int JournalPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string ValueDate { get; set; }
        public int TrxNo { get; set; }
        public string TrxName { get; set; }
        public int RefNo { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public string TypeDesc { get; set; }
        public string Description { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedTime { get; set; }
        public bool Reversed { get; set; }
        public int ReverseNo { get; set; }
        public string ReversedBy { get; set; }
        public string ReversedTime { get; set; }
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
        public string ParamMode { get; set; }
        public string ParamPeriod { get; set; }
        public string ParamPostDate { get; set; }
        public string ParamPostDateFrom { get; set; }
        public string ParamPostDateTo { get; set; }
        public int ParamPostJournalFrom { get; set; }
        public int ParamPostJournalTo { get; set; }
        public string ParamUserID { get; set; }

    }

    public class JournalExport
    {

        public string SourceOfFundDesc { get; set; }
        public string ClientCategory { get; set; }
        public string InvestorType { get; set; }
        public string CompanyID { get; set; }
        public string BeneficiaryBank { get; set; }
        public DateTime PaymentDate { get; set; }
        public int UnitRegistryPK { get; set; }
        public int PKFrom { get; set; }
        public int PKTo { get; set; }
        public string DownloadMode { get; set; }
        public bool PageBreak { get; set; }
        public int Status { get; set; }
        public string ReportName { get; set; }
        public string Type { get; set; }
        public int ClientSubscriptionPK { get; set; }
        public int ClientRedemptionPK { get; set; }
        public int ClientSwitchingPK { get; set; }
        public string ValueDateFrom { get; set; }
        public string ValueDateTo { get; set; }
        public string FundFrom { get; set; }
        public string FundTo { get; set; }
        public string FundClientFrom { get; set; }
        public string FundClientTo { get; set; }
        public string AgentFrom { get; set; }
        public string AgentTo { get; set; }
        public string DepartmentFrom { get; set; }
        public string DepartmentTo { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int FundClientPK { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Nav { get; set; }
        public DateTime NAVDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public decimal CashBalance { get; set; }
        public decimal UnitBalance { get; set; }
        public decimal SumUnitBalance { get; set; }
        public decimal FeePercent { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Message { get; set; }
        public string ContactPerson { get; set; }
        public string FaxNo { get; set; }
        public string BankCustodiID { get; set; }
        public string Remark { get; set; }
        public int SrHolder { get; set; }
        public string RekNo { get; set; }
        public string AgentName { get; set; }
        public string DepartmentName { get; set; }
        public decimal AgentFeeAmount { get; set; }
        public string CurrencyID { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }

        public int JournalPK { get; set; }
        public DateTime ValueDate { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string OfficeID { get; set; }
        public string DepartmentID { get; set; }
        public string AgentID { get; set; }
        public string CounterpartID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string ConsigneeID { get; set; }
        public string DetailDescription { get; set; }
        public string DocRef { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal CurrencyRate { get; set; }
    }

    public class ReferenceFromJournal
    {
        public string Reference;
    }

    public class JournalAddNew
    {
        public int JournalPK { get; set; }
        public int HistoryPK { get; set; }
        public string Message { get; set; }
    }


}