﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class RL504
    {
        public int RL504PK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int MKBDTrailsPK { get; set; }
        public string MKBDTrailsDesc { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public int ReksadanaTypePK { get; set; }
        public string ReksadanaTypeID { get; set; }
        public decimal MKBD01 { get; set; }
        public decimal MKBD02 { get; set; }
        public decimal MKBD03 { get; set; }
        public decimal MKBD09 { get; set; }
        public decimal Volume { get; set; }
        public decimal CloseNAV { get; set; }
        public decimal TotalNAVReksadana { get; set; }
        public decimal MarketValue { get; set; }
        public decimal HaircutPercent { get; set; }
        public decimal HaircutAmount { get; set; }
        public decimal AfterHaircutAmount { get; set; }
        public decimal ConcentrationLimit { get; set; }

        public decimal ConcentrationRisk { get; set; }
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
        public bool Affiliated { get; set; }
    }
}