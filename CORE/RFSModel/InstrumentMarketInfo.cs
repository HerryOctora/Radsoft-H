using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class InstrumentMarketInfo
    {
        public string InstrumentID { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}