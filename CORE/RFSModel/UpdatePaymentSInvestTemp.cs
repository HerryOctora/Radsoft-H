using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class UpdatePaymentSInvestTemp
    {
        public int UpdatePaymentSInvestTempPK { get; set; }
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
        public string UpdatePaymentSInvestTempSelected { get; set; }

        //Switch

        public string InFundCode { get; set; }
        public string InFundName { get; set; }
        public decimal InAmountCash { get; set; }
    }

    public class UpdatePaymentSInvestTemp_SAandFund
    {
        public string SA { get; set; }
        public string Fund { get; set; }
    }

    public class UpdatePaymentSInvestTempCombo
    {
        public string Result { get; set; }
        public string Fund { get; set; }
        public string FundClient { get; set; }
        public string TrxType { get; set; }
    }
 
}
