using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundUnitPosition
    {
        public int FundUnitPositionPK { get; set; }
        public string Date { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string CurrencyID { get; set; }
        public string FundName { get; set; }
        public string NAVDate { get; set; }
        public decimal UnitAmount { get; set; }
        public string SID { get; set; }
        public string IFUA { get; set; }
        public decimal AUM { get; set; }
        public decimal NAV { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }



}