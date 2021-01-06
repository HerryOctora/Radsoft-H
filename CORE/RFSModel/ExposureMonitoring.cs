using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
 

    public class ExposureMonitoring
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
        public int Exposure { get; set; }
        public string ExposureName { get; set; }
    }



    public class DataExposureMonitoring
    {
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int Exposure { get; set; }
        public string ExposureID { get; set; }
        public int Parameter { get; set; }
        public string ParameterDesc { get; set; }
        public int AlertExposure { get; set; }
        public decimal ExposurePercent { get; set; }
        public decimal WarningMaxExposure { get; set; }
        public decimal MaxExposurePercent { get; set; }
        public decimal WarningMinExposure { get; set; }
        public decimal MinExposurePercent { get; set; }
        public decimal MarketValue { get; set; }
        public decimal WarningMaxValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal WarningMinValue { get; set; }
        public decimal MinValue { get; set; }
        public bool AlertMaxExposure { get; set; }
        public bool AlertWarningMaxExposure { get; set; }
        public bool AlertMinExposure { get; set; }
        public bool AlertWarningMinExposure { get; set; }
        public bool AlertMaxValue { get; set; }
        public bool AlertWarningMaxValue { get; set; }
        public bool AlertMinValue { get; set; }
        public bool AlertWarningMinValue { get; set; }

    }

    public class DataDetailExposureMonitoring
    {
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int Exposure { get; set; }
        public string ExposureID { get; set; }
        public int Parameter { get; set; }
        public string ParameterDesc { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public decimal MarketValue { get; set; }
        public decimal AUM { get; set; }
        public decimal ExposurePercent { get; set; }
    

    }
  
}