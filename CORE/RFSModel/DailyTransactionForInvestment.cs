using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class DailyTransactionForInvestment
    {
        public int DailyTransactionForInvestmentPK { get; set; }
        public Boolean Selected { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDate { get; set; }
        public string RefNumber { get; set; }
        public string SellingAgentCode { get; set; }
        public string SellingAgentName { get; set; }
        public string IFUA { get; set; }
        public string FundCode { get; set; }
        public string FundName { get; set; }
        public decimal AmountCash { get; set; }
        public decimal AmountUnit { get; set; }
        public decimal FeePercent { get; set; }
        public string BICCode { get; set; }
        public string BankAcc { get; set; }
        public string PaymentDate { get; set; }
        public string TransferType { get; set; }
        public string TransferTypeDesc { get; set; }
        public string ReferenceNumber { get; set; }
        public string EntryUsersID { get; set; }
        public string DailyTransactionForInvestmentSelected { get; set; }
        public string InFundCode { get; set; }
        public string InFundName { get; set; }
        public decimal InAmountCash { get; set; }
    }

    public class DailyTransactionForInvestment_SAandFund
    {
        public string SA { get; set; }
        public string Fund { get; set; }
    }

    public class DailyTransactionForInvestmentCombo
    {
        public string Result { get; set; }
        public string Fund { get; set; }
        public string FundClient { get; set; }
        public string TrxType { get; set; }
    }

    public class DailyTransactionForInvestmentReport
    {
        public string NamaSellingAgent { get; set; }
        public decimal SASubscription { get; set; }
        public decimal SARedemption { get; set; }
        public decimal SANET { get; set; }
        public string ReksaDana { get; set; }
        public decimal RDSubscription { get; set; }
        public decimal RDRedemption { get; set; }
        public decimal RDNET { get; set; }

    }

    public class KhususTimOpsDailyTransactionForInvestment
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string AgentName { get; set; }
        public string IFUAName { get; set; }
        public string FundName { get; set; }
        public string FundCCY { get; set; }
        public decimal AmountNominal { get; set; }
        public decimal AmountUnit { get; set; }
        public decimal AmountAllUnit { get; set; }
        public decimal FeeNominal { get; set; }
        public decimal FeePercent { get; set; }
        public string REDMPaymentBankName { get; set; }
        public string REDMPaymentACNo { get; set; }
        public string REDMPaymentACName { get; set; }
        public DateTime PaymentDate { get; set; }
    }


}
