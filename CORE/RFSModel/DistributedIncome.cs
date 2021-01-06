﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class DistributedIncome
    {
        public int DistributedIncomePK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime ExDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public int Policy { get; set; }
        public string FundName { get; set; }
        public string PolicyDesc { get; set; }
        public int DistributedIncomeType { get; set; }
        public string DistributedIncomeTypeDesc { get; set; }
        public int DistributedType { get; set; }
        public string DistributedTypeDesc { get; set; }
        public decimal Nav { get; set; }
        public decimal CashAmount { get; set; }
        public decimal DistributedIncomePerUnit { get; set; }
        public decimal VariableAmount { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedTime { get; set; }
        public bool Revised { get; set; }
        public string RevisedBy { get; set; }
        public string RevisedTime { get; set; }
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


        public string FundClientName { get; set; }
        public string IFUA { get; set; }
        public string SID { get; set; }
        public decimal BegUnit { get; set; }
        public decimal DistributeCash { get; set; }
        public decimal DistributeUnit { get; set; }

        public string UnitRegistrySelected { get; set; }
    }

}