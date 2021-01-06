using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Bank
    {
        public int BankPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string BICode { get; set; }
        public string ClearingCode { get; set; }
        public string RTGSCode { get; set; }
        public string PTPCode { get; set; }
        public string USDPTPCode { get; set; }
        public string Country { get; set; }
        public string CountryDesc { get; set; }
        public bool BitRDN { get; set; }
        public bool BitSyariah { get; set; }
        public decimal FeeLLG { get; set; }
        public decimal FeeRTGS { get; set; }
        public decimal MinforRTGS { get; set; }
        public int InterestDays { get; set; }

        public int JournalRoundingMode { get; set; }
        public string JournalRoundingModeDesc { get; set; }

        public string SinvestID { get; set; }
        public int JournalDecimalPlaces { get; set; }
        public string JournalDecimalPlacesDesc { get; set; }

        public int InterestDaysType { get; set; }
        public int InterestPaymentType { get; set; }
        public int PaymentModeOnMaturity { get; set; }

        public string PaymentInterestSpecificDate { get; set; }
        public string NKPDCode { get; set; }
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
        public int IssuerPK { get; set; }
        public string IssuerID { get; set; }
        public int CapitalClassification { get; set; }
        public string CapitalClassificationDesc { get; set; }
    }


    public class BankCombo
    {
        public int BankPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string BICode { get; set; }
        public string Country { get; set; }
        public string CountryDesc { get; set; }
        public string SInvestID { get; set; }

    }


    public class BankInterestInformation 
    {
        public int InterestDaysType { get; set; }
        public int InterestPaymentType { get; set; }
        public int PaymentModeOnMaturity { get; set; }
    }

}