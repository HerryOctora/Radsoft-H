using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class ExposureMonitoringDetail
    {
        public int ExposureMonitoringDetailPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public int FundPK { get; set; }
        public string FundName { get; set; }
        public string BankCustodian { get; set; }
        public string KebijakanInvestasi { get; set; }
        public string Exposure { get; set; }
        public DateTime TanggalPelanggaran { get; set; }
        public string Batasan { get; set; }
        public string NoSurat { get; set; }
        public DateTime TanggalSurat { get; set; }
        public DateTime TanggalTerimaSurat { get; set; }
        public int Remedy { get; set; }
        public string RemedyDesc { get; set; }
        public DateTime ExDate { get; set; }
        public int StatusExposure { get; set; }
        public string Remarks { get; set; }

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
