using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Counterpart
    {
        public int CounterpartPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string NPWP { get; set; }
        public int CounterpartType { get; set; }
        public string CounterpartTypeDesc { get; set; }
        public string Description { get; set; }
        public bool BitSuspended { get; set; }
        public bool BitRegistered { get; set; }

        //Penambahan Field
        public string CBestAccount { get; set; }
        public int MarketPK { get; set; }
        public string MarketID { get; set; }
        public int TDays { get; set; }
        public int CashRefPaymentPK { get; set; }
        public string CashRefPaymentID { get; set; }
        public int BankPK { get; set; }
        public string BankAccountID { get; set; }
        public string BankAccountOffice { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountRecipient { get; set; }
        public string SInvestCode { get; set; }
        public int DecimalPlaces { get; set; }
        public int RoundingMode { get; set; }
        public string RoundingModeDesc { get; set; }
        public decimal LevyVATPercent { get; set; }
        public decimal TargetAllocationPercentage { get; set; }
        public int APERDMFeeMethod { get; set; }
        public string APERDMFeeMethodID { get; set; }

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
        public string Date { get; set; }        
        public string DateAmortize { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
        public decimal RangeTo { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal APERDPortionPercent { get; set; }
        public decimal APERDPortionAmount { get; set; }


    }

    public class CounterpartCombo
    {
        public int CounterpartPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }


    public class SetCounterpartFeeSetup
    {
        public int CounterpartFeeSetupPK { get; set; }
        public string Agent { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string DateAmortize { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
        public decimal RangeTo { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal APERDPortionPercent { get; set; }
        public decimal APERDPortionAmount { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartName { get; set; }
        public string Date { get; set; }
        public string EntryUsersID { get; set; }
    }
}