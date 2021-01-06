using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Office
    {
        public int OfficePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityPK { get; set; }
        public string CityDesc { get; set; }
        public string Country { get; set; }
        public string CountryDesc { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string FaxNo { get; set; }
        public string FaxServerName { get; set; }
        public string Email { get; set; }
        public string Manager { get; set; }
        public int CashRefPaymentPK { get; set; }
        public string CashRefPaymentID { get; set; }
        public string PaymentInstruction { get; set; }
        public bool Groups { get; set; }
        public int ParentPK { get; set; }
        public string ParentID { get; set; }
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

    public class OfficeCombo
    {
        public int OfficePK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}