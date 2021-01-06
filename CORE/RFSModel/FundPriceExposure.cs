using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundPriceExposure
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int Validate { get; set; }
        public string Result { get; set; }
    }
}