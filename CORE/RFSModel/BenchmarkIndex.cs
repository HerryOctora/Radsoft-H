using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class BenchmarkIndex
    {
        public int BenchmarkIndexPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public bool Selected { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int IndexPK { get; set; }
        public string IndexID { get; set; }
        public decimal OpenInd { get; set; }
        public decimal HighInd { get; set; }
        public decimal LowInd { get; set; }
        public decimal CloseInd { get; set; }
        public decimal Volume { get; set; }
        public decimal Value { get; set; }
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
        public string BenchmarkIndexSelected { get; set; }
        public bool Approved1 { get; set; }

    }

    public class BenchmarkIndexCombo
    {
        public int BenchmarkIndexPK { get; set; }
        public decimal CloseInd { get; set; }
    }

    public class BIRate
    {
        public int BenchmarkIndexPK { get; set; }
        public decimal CloseInd { get; set; }
    }

}