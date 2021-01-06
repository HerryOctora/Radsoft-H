using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FFSSetup_02
    {
        public int FFSSetup_02PK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string TujuanStrategiInvestasi { get; set; }
        public string ValueDate { get; set; }
        public string MarketReview { get; set; }
        public string UngkapanSanggahan { get; set; }

        public string TanggalPerdana { get; set; }
        public decimal NilaiAktivaBersih { get; set; }
        public decimal TotalUnitPenyertaan { get; set; }
        public decimal NilaiAktivaBersihUnit { get; set; }
        public string FaktorResikoYangUtama { get; set; }
        public decimal ImbalJasaManajerInvestasi { get; set; }
        public decimal ImbalJasaBankKustodian { get; set; }
        public decimal BiayaPembelian { get; set; }
        public decimal BiayaPenjualan { get; set; }
        public decimal BiayaPengalihan { get; set; }
        public string ManfaatInvestasi { get; set; } 
        public int BankKustodianPK { get; set; }
        public string BankKustodianID { get; set; }
        public string KebijakanInvestasi { get; set; }
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