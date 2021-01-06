using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundTransferInterBank
    {
        public int FundTransferInterBankPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public DateTime ValueDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int FundCashRefPKFrom { get; set; }
        public string FundCashRefIDFrom { get; set; }
        public int FundCashRefPKTo { get; set; }
        public string FundCashRefIDTo { get; set; }
        public string InstructionNo { get; set; }
        public string InformationTo { get; set; }
        public string CCTo { get; set; }
        public string Subject { get; set; }
        public int Signature1 { get; set; }
        public string SignatureName1 { get; set; }
        public int Signature2 { get; set; }
        public string SignatureName2 { get; set; }
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
        public string FundCashRefAccountFrom { get; set; }
        public string FundCashRefAccountTo { get; set; } 
        public string FundCashRefAccountNoFrom { get; set; }
        public string FundCashRefAccountNoTo { get; set; }
        public string BankFrom { get; set; }
        public string BankTo { get; set; } 
    }

}
