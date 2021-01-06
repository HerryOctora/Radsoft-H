using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFSModel
{
    public class BloombergEquity
    {
        public int BloombergEquityPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public string TickerName { get; set; }
        public string TickerCode { get; set; }
        public string FundName { get; set; }
        public int Weight { get; set; }
        public int Shares { get; set; }
        public int Price { get; set; }
        public int MarketCap { get; set; }
        public int PercentWeight { get; set; }
        public string GICSSector { get; set; }
        public string IndustryGroupIndex { get; set; }
        public string Y1 { get; set; }
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
