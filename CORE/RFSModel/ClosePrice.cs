using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class ClosePrice
    {
        public int ClosePricePK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public decimal ClosePriceValue { get; set; }
        public decimal LowPriceValue { get; set; }
        public decimal HighPriceValue { get; set; }
        public decimal LiquidityPercent { get; set; }
        public decimal HaircutPercent { get; set; }
        public decimal CloseNAV { get; set; }
        public decimal TotalNAVReksadana { get; set; }
        public decimal NAWCHaircut { get; set; }
        public string BondRating { get; set; }
        public string BondRatingDesc { get; set; }
        public string Type { get; set; }
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


}
