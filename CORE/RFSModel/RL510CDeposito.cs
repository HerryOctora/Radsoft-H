using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class RL510CDeposito
    {
        public int RL510CDepositoPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public int MKBDTrailsPK { get; set; }
        public string MKBDTrailsID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public int DepositoTypePK { get; set; }
        public string DepositoTypeID { get; set; }
        public string IsLiquidated { get; set; }
        public decimal MKBD01 { get; set; }
        public decimal MKBD02 { get; set; }
        public decimal MKBD03 { get; set; }
        public decimal MKBD09 { get; set; }
        public decimal IsUnderDuedateAmount { get; set; }
        public decimal NotUnderDuedateAmount { get; set; }
        public decimal QuaranteedAmount { get; set; }
        public decimal NotLiquidatedAmount { get; set; }
        public decimal LiquidatedAmount { get; set; }
        public decimal MarketValue { get; set; }
        public decimal HaircutPercent { get; set; }
        public decimal HaircutAmount { get; set; }
        public decimal AfterHaircutAmount { get; set; }
        public decimal RankingLiabilities { get; set; }

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
}