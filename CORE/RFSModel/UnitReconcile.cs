using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class UnitReconcile
    {
        public int UnitReconcilePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public Boolean Selected { get; set; }
        public string Notes { get; set; }
        public DateTime ValueDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public decimal UnitSystem { get; set; }
        public decimal UnitSInvest { get; set; }
        public string Description { get; set; }
        public decimal Difference { get; set; }
        public decimal AdjustmentUnit { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string LastUpdate { get; set; }

    }
    public class UnitReconcileTemp
    {
        public int UnitReconcileTempPK { get; set; }
        public string ValueDate { get; set; }
        public string IFUACode { get; set; }
        public string FundCode { get; set; }
        public string CurrencyID { get; set; }
        public string SACode { get; set; }
        public decimal UnitBalance { get; set; }
        public decimal NAV { get; set; }
        public decimal AmountBalance { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
    }

   

}
