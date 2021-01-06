using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class OMSTimeDeposit
    {
        public string Name { get; set; }
        public decimal CurrBalance { get; set; }
        public decimal CurrNAVPercent { get; set; }
        public decimal Movement { get; set; }
        public decimal AfterBalance { get; set; }
        public decimal AfterNAVPercent { get; set; }
        public decimal MovementTOne { get; set; }
        public decimal AfterTOne { get; set; }
        public decimal AfterTOneNAVPercent { get; set; }
        public decimal MovementTTwo { get; set; }
        public decimal AfterTTwo { get; set; }
        public decimal AfterTTwoNAVPercent { get; set; }
        public bool BitRollOverInterest { get; set; }
        public bool StatutoryType { get; set; }
    }

    public class OMSExposureDeposito
    {
        public string FundID { get; set; }
        public string ExposureType { get; set; }
        public string Parameter { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal PotentialValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal DifferenceValue { get; set; }
        public decimal CurrentPercentage { get; set; }
        public decimal PotentialPercentage { get; set; }
        public decimal MaxPercentage { get; set; }
        public decimal DifferencePercentage { get; set; }
        public decimal DifferenceAmount { get; set; }

    }

    public class OMSDepositoInstrumentDetailPerFundPerBank
    {
        public string InstrumentID { get; set; }
        public decimal InterestPercent { get; set; }
        public string MaturityDate { get; set; }
        public decimal Balance { get; set; }

    }

    public class OMSTimeDepositRpt
    {

        public string FundID { get; set; }
        public string InstrumentID { get; set; }
        public string BankID { get; set; }
        public DateTime SettlementDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Volume { get; set; }
        public decimal InterestPercent { get; set; }
        public decimal TaxPercentage { get; set; }
        public int IsBreakable { get; set; }
        public string Reference { get; set; }
        public string BankAccountNo { get; set; }
        public string ContractRate { get; set; }
        public string InterestFreq { get; set; }
        public string Remarks { get; set; }

    }
    public class OMSRHBBankBranchBridging
    {
        public int PK { get; set; }
        public string BankBranchCode { get; set; }
        public string BankDescription { get; set; }
    }

    public class OMSTimeDepositByInstrument
    {
        public DateTime ValueDate { get; set; }
        public int FundPK { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string BankID { get; set; }
        public string Category { get; set; }
        public decimal CurrentExposure { get; set; }
        public decimal Volume { get; set; }
        public decimal InterestPercent { get; set; }
        public int InterestDays { get; set; }
        public DateTime AcqDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal TaxPercent { get; set; }
        public DateTime NisbahDateFrom { get; set; }
        public DateTime NisbahDateTo { get; set; }
        public int Tenor { get; set; }
        public decimal Net { get; set; }
        public decimal RealizedAmount { get; set; }
        public decimal Difference { get; set; }
        public decimal RealInterestPercent { get; set; }
        
    }

    public class NetOMSTimeDeposit
    {
        public decimal GrossBilyet { get; set; }
        public decimal FeeBilyet { get; set; }
        public decimal NetBilyet { get; set; }
        public decimal NetPercentBilyet { get; set; }
        public decimal GrossReal { get; set; }
        public decimal FeeReal { get; set; }
        public decimal NetReal { get; set; }
        public decimal NetPercentReal { get; set; }
    }

    public class OMSTimeDepositByMaturity
    {
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public decimal Balance { get; set; }
        public DateTime AcqDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal InterestPercent { get; set; }
        public string Category { get; set; }
        public string BankBranchID { get; set; }
    }
}