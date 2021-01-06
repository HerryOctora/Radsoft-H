using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class OrderFI
    {
        public int OrderFIPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public int OrderStatus { get; set; }
        public string OrderStatusDesc { get; set; }
        public string Notes { get; set; }
        public string ValueDate { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public int InstrumentTypePK { get; set; }
        public string InstrumentTypeID { get; set; }
        public int TrxType { get; set; }
        public string TrxTypeDesc { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; } 
        public decimal RangePriceFrom { get; set; }
        public decimal RangePriceTo { get; set; }
        public decimal Volume { get; set; }
        public decimal DoneVolume { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string UpdateUsersID { get; set; }
        public string UpdateTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }



}