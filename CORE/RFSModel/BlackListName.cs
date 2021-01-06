using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class BlackListName
    {
        public int BlackListNamePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string NoDoc { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public string Name { get; set; }
        public string NoIDQ { get; set; }
        public string NameAlias { get; set; }
        public string NoID { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public string Kewarganegaraan { get; set; }
        public string Alamat { get; set; }
        public string Description { get; set; }
        public string Pekerjaan { get; set; }
        public string NomorPasport { get; set; }
        public string JenisKelamin { get; set; }
        public string Agama { get; set; }
        public string NoKTP { get; set; }
        public string NoNPWP { get; set; }
        public string Tanggal { get; set; }
        public string SumberData { get; set; }
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