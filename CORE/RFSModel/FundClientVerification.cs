using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundClientVerification
    {
        public int FundClientVerificationPK { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public int FundClientPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string NAVDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int BankPK { get; set; }
        public string BankID { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionID { get; set; }
        public string ImgOri { get; set; }
        public string ImgThumb { get; set; }
        public int BankCode { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public int TrxPK { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string LastUpdate { get; set; }
    }
}