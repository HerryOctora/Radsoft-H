using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RFSModel
{
    public class ManageInvestment
    {

        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PK { get; set; }
        public int FundPK { get; set; }
        public int InstrumentPK { get; set; }
        public string FundFrom { get; set; }
        public string FundID { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public int InstrumentTypePK { get; set; }
        public string InstrumentTypeID { get; set; }
        public string FundText { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public int SysNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime SettledDate { get; set; }
        public bool Selected { get; set; }
        public string TrxTypeID { get; set; }
        public decimal DoneVolume { get; set; }
        public decimal DonePrice { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
