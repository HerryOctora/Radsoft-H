using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class FundExposure
    {
        public int FundExposurePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public int Parameter { get; set; }
        public string ParameterDesc { get; set; }
        public decimal MinExposurePercent { get; set; }
        public decimal MaxExposurePercent { get; set; }
        public decimal WarningMinExposurePercent { get; set; }
        public decimal WarningMaxExposurePercent { get; set; }

        public decimal MaxValue { get; set; }
        public decimal MinValue { get; set; }
        public decimal WarningMaxValue { get; set; }
        public decimal WarningMinValue { get; set; }
        public string ExposureID { get; set; }
        public int AlertExposure { get; set; }
        public decimal ExposurePercent { get; set; }
        public decimal MarketValue { get; set; }
        public string CounterpartName { get; set; }
        public int ValidateAmount { get; set; }

        public decimal Amount { get; set; }

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

    public class FundExposureCombo
    {
        public int FundExposurePK { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public int Parameter { get; set; }
        public string ParameterDesc { get; set; }
        public int FundPK { get; set; }
        public string Name { get; set; }
    }

    public class FundExposureComboPreTrade
    {
        public string ExposureType { get; set; }
        public string ExposureID { get; set; }
        public decimal MarketValue { get; set; }
        public decimal ExposurePercent { get; set; }
        public bool AlertMaxExposure { get; set; }
        public bool AlertMinExposure { get; set; }
        public bool WarningMinExposure { get; set; }
        public bool WarningMaxExposure { get; set; }
    }


    public class ValidateFundExposure
    {
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
        public string FundID { get; set; }

    }





}
