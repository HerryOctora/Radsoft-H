using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class UpdateClosePrice
    {

        public int UpdateClosePricePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string DateImport { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public decimal ClosePriceValue { get; set; }
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
        public string UpdateClosePriceSelected { get; set; }


    }

    //public class UpdateClosePriceCombo
    //{
    //    public int UpdateClosePricePK { get; set; }
    //    public string Date { get; set; }
    //    public int InstrumentPK { get; set; }
    //    public string InstrumentID { get; set; }
    //    public decimal ClosePriceValue { get; set; }
    //}



}