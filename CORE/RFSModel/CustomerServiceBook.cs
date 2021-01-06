using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class CustomerServiceBook
    {
        public int CustomerServiceBookPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientDesc { get; set; }
        public int ClientType { get; set; }
        public string ClientTypeDesc { get; set; }
        public int AskLine { get; set; }
        public string AskLineDesc { get; set; }
        public string Message { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Solution { get; set; }
        public string Phone { get; set; }
        public int StatusMessage { get; set; }
        public string StatusMessageDesc { get; set; }
        public string InternalComment { get; set; }
        public bool IsNeedToReport { get; set; }
        public int Param { get; set; }
        public string DoneUsersID { get; set; }
        public string DoneTime { get; set; }
        public string UnDoneUsersID { get; set; }
        public string UnDoneTime { get; set; }
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

    public class GetCustomerHistory
    {
        public int CustomerServiceBookPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientDesc { get; set; }
        public int ClientType { get; set; }
        public string ClientTypeDesc { get; set; }
        public int AskLine { get; set; }
        public string AskLineDesc { get; set; }
        public string Message { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Solution { get; set; }
        public string Phone { get; set; }
        public int StatusMessage { get; set; }
        public string StatusMessageDesc { get; set; }
        public string InternalComment { get; set; }
        public string DoneUsersID { get; set; }
        public string DoneTime { get; set; }
        public string UnDoneUsersID { get; set; }
        public string UnDoneTime { get; set; }
        public string EntryTime { get; set; }
        public string EntryUsersID { get; set; }
    }
}