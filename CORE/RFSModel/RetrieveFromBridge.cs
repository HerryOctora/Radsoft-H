using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class RetrieveFromBridge
    {
        public int RetrieveFromBridgePK { get; set; }
        public int HistoryPK { get; set; }
        public string ValueDate { get; set; }
        public string CheckManagementFee { get; set; }
        public string CheckUnitRegistry { get; set; }
        public string Date { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal SUBAmount { get; set; }
        public decimal SUBFeeAmount { get; set; }
        public decimal REDAmount { get; set; }
        public decimal REDFeeAmount { get; set; }
        public decimal REDUnit { get; set; }
        public string REDPaymentDate { get; set; }
        public decimal SWIOutAmount { get; set; }
        public decimal SWIOutUnit { get; set; }
        public string SWIOutPaymentDate { get; set; }
        public decimal SWIInAmount { get; set; }
        public decimal SWIInFeeAmount { get; set; }
        public string RetrieveFundFrom { get; set; }
    }

    public class RetrieveUnitRegistry
    {
        public int RetrieveFromBridgePK { get; set; }
        public int HistoryPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal SUBAmount { get; set; }
        public decimal SUBFeeAmount { get; set; }
        public decimal REDAmount { get; set; }
        public decimal REDFeeAmount { get; set; }
        public decimal REDUnit { get; set; }
        public decimal REDFeeUnit { get; set; }
        public string REDPaymentDate { get; set; }
        public decimal SWIOutAmount { get; set; }
        public decimal SWIOutFeeAmount { get; set; }
        public decimal SWIOutUnit { get; set; }
        public decimal SWIOutFeeUnit { get; set; }
        public string SWIOutPaymentDate { get; set; }
        public decimal SWIInAmount { get; set; }
        public decimal SWIInFeeAmount { get; set; }

    }

  

}
