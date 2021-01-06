using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RFSModel
{

    public class BondInformationFromIBPA
    {
        public int BondInformationFromIBPAPK { get; set; }
        public DateTime Date { get; set; }
        public string Series { get; set; }
        public string ISINCode { get; set; }
        public string BondName { get; set; }
        public string Rating { get; set; }
        public decimal CouponRate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal TTM { get; set; }
        public decimal TodayYield { get; set; }
        public decimal TodayLowPrice { get; set; }
        public decimal TodayFairPrice { get; set; }
        public decimal TodayHighPrice { get; set; }
        public decimal Change { get; set; }
        public decimal YesterdayYield { get; set; }
        public decimal YesterdayPrice { get; set; }
        public decimal LastWeekYield { get; set; }
        public decimal LastWeekPrice { get; set; }
        public decimal LastMonthYield { get; set; }
        public decimal LastMonthPrice { get; set; }


        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }

}
