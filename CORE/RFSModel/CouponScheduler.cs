using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class CouponScheduler
    {
        public int AutoNo { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public string CouponFromDate { get; set; }
        public string CouponToDate { get; set; }
        public decimal CouponRate { get; set; }
        public int CouponDays { get; set; }
        public string Description { get; set; }
        public string UsersID { get; set; }
        public string LastUpdate { get; set; }
    }
    
}