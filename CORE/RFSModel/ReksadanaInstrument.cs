using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class ReksadanaInstrument
    {
        public int ReksadanaInstrumentPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int ReksadanaPK { get; set; }
        public string ReksadanaID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string ExpiredDate { get; set; }

        //tambahan
        public decimal InterestPercent { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal BuyVolume { get; set; }
        public decimal SellVolume1 { get; set; }
        public decimal SellVolume2 { get; set; }
        public decimal SellVolume3 { get; set; }
        public decimal SellVolume4 { get; set; }
        public decimal SellVolume5 { get; set; }
        public decimal SellVolume6 { get; set; }
        public decimal SellPrice1 { get; set; }
        public decimal SellPrice2 { get; set; }
        public decimal SellPrice3 { get; set; }
        public decimal SellPrice4 { get; set; }
        public decimal SellPrice5 { get; set; }
        public decimal SellPrice6 { get; set; }
        public decimal EndVolume { get; set; }
        public int Totaldays { get; set; }

        //tambahan
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
