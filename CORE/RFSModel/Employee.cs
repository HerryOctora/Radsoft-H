using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Employee
    {
        public int EmployeePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Name { get; set; }
        public string SKNumber { get; set; }
        public string SKDate { get; set; }
        public string SKExpiredDate { get; set; }
        public int SKFormat { get; set; }
        public string SKFormatDesc { get; set; }
        public int Position { get; set; }
        public string PositionDesc { get; set; }
        public string DateOfWork { get; set; }
        public string BranchCode { get; set; }
        public string BranchCodeDesc { get; set; }
        public int ChiefCode { get; set; }
        public string ChiefCodeDesc { get; set; }
        public string SubRedempCode { get; set; }
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
        public int JenisKelamin { get; set; }
        public string JenisKelaminDesc { get; set; }
        public string TanggalLahir { get; set; }
        public string NIK { get; set; }
        public string NPWP { get; set; }
        public string AlamatSesuaiKTP { get; set; }
        public string AlamatDomisili { get; set; }
        public string NoBPJSKetenagakerjaan { get; set; }
        public string NoBPJSKesehatan { get; set; }
        public string NoRekTabungan { get; set; }
        public int StatusPerkawinan { get; set; }
        public string StatusPerkawinanDesc { get; set; }
        public int JumlahAnak { get; set; }
        public string NamaLisence { get; set; }
        public string ExpiredDateLisence { get; set; }
        public int StatusLisence { get; set; }
        public string StatusLisenceDesc { get; set; }
        public bool BitReportToOJK { get; set; }
    }

    public class EmployeeReport
    {
        public string Nama { get; set; }
        public string NamaLisence { get; set; }
        public string ExpiredDateLisence { get; set; }
    }



}