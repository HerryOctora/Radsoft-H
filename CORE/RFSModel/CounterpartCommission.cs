using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class CounterpartCommission
    {
        public int CounterpartCommissionPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public int BoardType { get; set; }
        public string BoardTypeDesc { get; set; }
        public bool BitIncludeTax { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal LevyPercent { get; set; }
        public decimal KPEIPercent { get; set; }
        public decimal VATPercent { get; set; }
        public decimal WHTPercent { get; set; }
        public decimal OTCPercent { get; set; }
        public decimal IncomeTaxSellPercent { get; set; }
        public decimal IncomeTaxInterestPercent { get; set; }
        public decimal IncomeTaxGainPercent { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal RoundingCommission { get; set; }
        public string RoundingCommissionDesc { get; set; }
        public decimal DecimalCommission { get; set; }
        public decimal RoundingLevy { get; set; }
        public string RoundingLevyDesc { get; set; }
        public decimal DecimalLevy { get; set; }
        public decimal RoundingKPEI { get; set; }
        public string RoundingKPEIDesc { get; set; }
        public decimal DecimalKPEI { get; set; }
        public decimal RoundingVAT { get; set; }
        public string RoundingVATDesc { get; set; }
        public decimal DecimalVAT { get; set; }
        public decimal RoundingWHT { get; set; }
        public string RoundingWHTDesc { get; set; }
        public decimal DecimalWHT { get; set; }
        public decimal RoundingOTC { get; set; }
        public string RoundingOTCDesc { get; set; }
        public decimal DecimalOTC { get; set; }
        public decimal RoundingTaxSell { get; set; }
        public string RoundingTaxSellDesc { get; set; }
        public decimal DecimalTaxSell { get; set; }
        public decimal RoundingTaxInterest { get; set; }
        public string RoundingTaxInterestDesc { get; set; }
        public decimal DecimalTaxInterest { get; set; }
        public decimal RoundingTaxGain { get; set; }
        public string RoundingTaxGainDesc { get; set; }
        public decimal DecimalTaxGain { get; set; }
        public decimal OTCAmount { get; set; }
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
        public bool BitNoCapitalGainTax { get; set; }
        public decimal BrokerFeePercent { get; set; }
        public decimal TotalSumFee { get; set; }
    }


}