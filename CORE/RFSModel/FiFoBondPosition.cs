﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFSModel
{
    public class FiFoBondPosition
    {
        public int FiFoBondPositionPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public string StatusDesc { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public decimal AcqVolume { get; set; }
        public decimal AcqPrice { get; set; }
        public string AcqDate { get; set; }
        public string CutoffDate { get; set; }
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

        public DateTime Date { get; set; }
    }

    public class setFifoBondPosition
    {
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string Instrument { get; set; }
        public decimal Volume { get; set; }
        public decimal AcqPrice { get; set; }
        public DateTime AcqDate { get; set; }
    }

    public class setHistorical
    {
        public DateTime Date { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string Instrument { get; set; }
        public DateTime AcqDate { get; set; }
        public decimal AcqPrice { get; set; }
        public decimal AcqVolume { get; set; }
    }



}
