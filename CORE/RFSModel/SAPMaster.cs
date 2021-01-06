using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class SAPMaster
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int AccountType { get; set; }
        public string AccountTypeID { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }

    public class SAPMSCustomerCombo
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class SAPMSAccountCombo
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class SAPBridgeJournal
    {
        public int ZSAP_BridgeJournalPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundJournalAccountPK { get; set; }
        public string FundJournalAccountID { get; set; }
        public string SAPAccountID { get; set; }
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

    public class ReturnAccount
    {
        public int FundClientPK { get; set; }
        public string FOID { get; set; }
    }




    public class InterfaceJournalSAP
    {
        public int FundJournalPK { get; set; }
        public int InvestmentPK { get; set; }
        public bool Selected { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int AutoNo { get; set; }
        public string No { get; set; }
        public string Date { get; set; }
        public string FundJournalDate { get; set; }
        public string BuySell { get; set; }
        public string Description { get; set; }
        public string JournalReference { get; set; }
        public string DocFrom { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string DetailDescription { get; set; }
        public decimal BaseDebit { get; set; }
        public decimal BaseCredit { get; set; }
        public string FundJournalType { get; set; }
        public string DOCSAP { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public decimal Total { get; set; }
    }

    public class SAPJournalModel
    {
     
        // HEADER
        public string HEADER_TXT { get; set; }
        public string COMP_CODE { get; set; }
        public string DOC_DATE { get; set; }
        public string PSTNG_DATE { get; set; }
        public string FISC_YEAR { get; set; }
        public string FIS_PERIOD { get; set; }
        public string REF_DOC_NO { get; set; }
        public string DOC_TYPE { get; set; }
        public string GROUP_DOC { get; set; }

        public string ACC_TYPE { get; set; }

        //DETAIL
        public string ITEMNO_ACC { get; set; }
        public string GL_ACCOUNT { get; set; }
        public string BUS_AREA { get; set; }
        
        public string ITEM_TEXT { get; set; }
     
        public string ORDERID { get; set; }
        public string FUNDS_CTR { get; set; }
        public string COSTCENTER { get; set; }
        public string PROFIT_CTR { get; set; }
        public string CUSTOMER { get; set; }

        //Amount
        public string CURRENCY { get; set; }
        public decimal AMT_DOCCUR { get; set; }
        public string DEBIT_CREDIT { get; set; }

        

    }

    public class SendSAPMessage
    {
        public string SAPDocNumber { get; set; }
        public string RadsoftDocNumber { get; set; }
        public string Message { get; set; }
    }

    public class ReportLPTI
    {
        public string ParamListDate { get; set; }
        public string DownloadMode { get; set; }
        public string FundName { get; set; }
        public int InvestmentPK { get; set; }
        public int InstrumentTypePK { get; set; }
        public int TrxType { get; set; }
        public string TrxTypeID { get; set; }
        public DateTime InstructionDate { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public string KodeCustomer { get; set; }
        public string KodeAkun { get; set; }
        public string BusinessArea { get; set; }
        public decimal Amount { get; set; }
        public string CounterpartName { get; set; }
        public string BankAccountNo { get; set; }
        public DateTime SettlementDate { get; set; }

    }


}