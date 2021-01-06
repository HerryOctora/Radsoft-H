using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class OMSReksadana
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
    }

    public class OMSExposureReksadana
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

    public class OMSReksadanaByInstrument
    {
        public string InstrumentID { get; set; }
        public string SectorID { get; set; }
        public decimal CurrentExposure { get; set; }
        public decimal Lot { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal PriceDifference { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnRealized { get; set; }
        public decimal GainLoss { get; set; }
        public int Status { get; set; }
    }
    public class OMSReksadanaBySector
    {
        public string InstrumentID { get; set; }
        public string SectorID { get; set; }
        public decimal CurrentExposure { get; set; }
        public decimal Lot { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal PriceDifference { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnRealized { get; set; }
        public decimal GainLoss { get; set; }
        public int Status { get; set; }
    }


    public class OMSReksadanaByIndex
    {
        public string InstrumentID { get; set; }
        public string IndexID { get; set; }
        public decimal CurrentExposure { get; set; }
        public decimal Lot { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal PriceDifference { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnRealized { get; set; }
        public decimal GainLoss { get; set; }
        public int Status { get; set; }
    }

    public class OMSReksadanaCashProjection
    {
        public string FSource { get; set; }
        public decimal Amount { get; set; }

    }

    public class OMSReksadanaNavProjection
    {
        public DateTime ValueDate { get; set; }
        public string FundName { get; set; }
        public decimal Nav { get; set; }
        public decimal AUM { get; set; }
        public decimal Compare { get; set; }
    }
}