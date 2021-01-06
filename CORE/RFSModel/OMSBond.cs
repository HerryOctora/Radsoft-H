using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class OMSBond
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
        public decimal MovementTThree { get; set; }
        public decimal AfterTThree { get; set; }
        public decimal AfterTThreeNAVPercent { get; set; }
        public DateTime SettledDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal InterestRate { get; set; }
        public decimal CostPrice { get; set; }
        public int PaymentPeriod { get; set; }
        public int InterestDaysType { get; set; }
    }

    public class OMSExposureBond
    {
        public string FundID { get; set; }
        public string ExposureType { get; set; }
        public string ExposureID { get; set; }
        public decimal MarketValue { get; set; }
        public decimal ExposurePercent { get; set; }
        public decimal MinExposurePercent { get; set; }
        public decimal MaxExposurePercent { get; set; }
        public decimal WarningMinExposurePercent { get; set; }
        public decimal WarningMaxExposurePercent { get; set; }
        public bool AlertMinExposure { get; set; }
        public bool AlertWarningMinExposure { get; set; }
        public bool AlertMaxExposure { get; set; }
        public bool AlertWarningMaxExposure { get; set; }
    }

    public class OMSBondByInstrument
    {
        public string InstrumentTypeID { get; set; }
        public string InstrumentID { get; set; }
        public string SectorID { get; set; }
        public decimal CurrentExposure { get; set; }
        public decimal Volume { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal PriceDifference { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnRealized { get; set; }
        public decimal GainLoss { get; set; }
        public DateTime NextCouponDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int Status { get; set; }
    }

    public class OMSBondBySector
    {
        public string InstrumentID { get; set; }
        public string SectorID { get; set; }
        public decimal CurrentExposure { get; set; }
        public decimal Volume { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal PriceDifference { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnRealized { get; set; }
        public decimal GainLoss { get; set; }
    }


    public class OMSBondByMaturity
    {
        public string InstrumentTypeID { get; set; }
        public string InstrumentID { get; set; }
        public string SectorID { get; set; }
        public decimal CurrentExposure { get; set; }
        public decimal Volume { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal PriceDifference { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnRealized { get; set; }
        public decimal GainLoss { get; set; }
        public DateTime LastCouponDate { get; set; }
        public DateTime NextCouponDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int Status { get; set; }
        public decimal InterestPercent { get; set; }
    }

    public class OMSBondCashProjection
    {
        public string FSource { get; set; }
        public decimal Amount { get; set; }
    }

    public class OMSBondNavProjection
    {
        public DateTime ValueDate { get; set; }
        public string FundName { get; set; }
        public decimal Nav { get; set; }
        public decimal AUM { get; set; }
        public decimal Compare { get; set; }
    }



    public class OMSBondForNetAmount
    {
        public int DealingPK { get; set; }
        public int HistoryPK { get; set; }
        public string ValueDate { get; set; }
        public int InstrumentPK { get; set; }
        public int InstrumentTypePK { get; set; }
        public int TrxType { get; set; }
        public int CounterpartPK { get; set; }
        public string SettledDate { get; set; }
        public string NextCouponDate { get; set; }
        public string LastCouponDate { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public decimal AcqPrice { get; set; }
        public string AcqDate { get; set; }
        public decimal AcqVolume { get; set; }
        public decimal AcqPrice1 { get; set; }
        public string AcqDate1 { get; set; }
        public decimal AcqVolume1 { get; set; }
        public decimal AcqPrice2 { get; set; }
        public string AcqDate2 { get; set; }
        public decimal AcqVolume2 { get; set; }
        public decimal AcqPrice3 { get; set; }
        public string AcqDate3 { get; set; }
        public decimal AcqVolume3 { get; set; }
        public decimal AcqPrice4 { get; set; }
        public string AcqDate4 { get; set; }
        public decimal AcqVolume4 { get; set; }
        public decimal AcqPrice5 { get; set; }
        public string AcqDate5 { get; set; }
        public decimal AcqVolume5 { get; set; }
        public decimal AcqPrice6 { get; set; }
        public string AcqDate6 { get; set; }
        public decimal AcqVolume6 { get; set; }
        public decimal AcqPrice7 { get; set; }
        public string AcqDate7 { get; set; }
        public decimal AcqVolume7 { get; set; }
        public decimal AcqPrice8 { get; set; }
        public string AcqDate8 { get; set; }
        public decimal AcqVolume8 { get; set; }
        public decimal AcqPrice9 { get; set; }
        public string AcqDate9 { get; set; }
        public decimal AcqVolume9 { get; set; }
        public decimal IncomeTaxSellAmount { get; set; }
        public decimal RealisedAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }

        public decimal TaxCapitaGainPercent { get; set; }
        public decimal TaxInterestPercent { get; set; }
        public bool BitIsRounding { get; set; }
        public string FundID { get; set; }
        public string CounterpartID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string stringInvestmentFrom { get; set; }
        public decimal DonePrice { get; set; }
        public decimal IncomeTaxGainPercent { get; set; }
        public decimal IncomeTaxInterestPercent { get; set; }

    }
}