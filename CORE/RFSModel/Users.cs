using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Users
    {
        public int UsersPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string UserClientModeDesc { get; set; }
        public string Notes { get; set; }
        public string SessionID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public int OfficePK { get; set; }
        public string OfficeID { get; set; }
        public string OfficeName { get; set; }
        public string ExpireUsersDate { get; set; }
        public string ExpirePasswordDate { get; set; }
        public string ExpireSessionTime { get; set; }
        public int LoginRetry { get; set; }
        public bool BitPasswordReset { get; set; }
        public bool BitEnabled { get; set; }
        public int UserClientMode { get; set; }
        public string PrevPassword1 { get; set; }
        public string PrevPassword2 { get; set; }
        public string PrevPassword3 { get; set; }
        public string PrevPassword4 { get; set; }
        public string PrevPassword5 { get; set; }
        public string PrevPassword6 { get; set; }
        public string PrevPassword7 { get; set; }
        public string PrevPassword8 { get; set; }
        public string PrevPassword9 { get; set; }
        public string PrevPassword10 { get; set; }
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
    
    public class UsersCombo
    {
        public int UsersPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}