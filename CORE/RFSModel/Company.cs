using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Company
    {
        public int CompanyPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string NPWP { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string DirectorOne { get; set; }
        public string DirectorTwo { get; set; }
        public string DirectorThree { get; set; }
        public decimal PPEMinimalMKBD { get; set; }
        public decimal MIMinimalMKBD { get; set; }
        public int DefaultCurrencyPK { get; set; }
        public string DefaultCurrencyID { get; set; }
        public string MKBDCode { get; set; }
        public string NoIDPJK { get; set; }
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