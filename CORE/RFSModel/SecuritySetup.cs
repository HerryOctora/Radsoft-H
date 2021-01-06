using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class SecuritySetup
    {
        public int SecuritySetupPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int MaxLoginRetry { get; set; }
        public int PasswordCharacterType { get; set; }
        public string PasswordCharacterTypeDesc { get; set; }
        public int MinimumPasswordLength { get; set; }
        public bool BitChangePasswordAtReset { get; set; }
        public int PasswordExpireDays { get; set; }
        public int UsersExpireDays { get; set; }
        public bool BitReusedPassword { get; set; }
        public int HoursChangePassword { get; set; }
        public int PasswordExpireLevel { get; set; }
        public string DefaultPassword { get; set; }
        public int IdleTimeMinutes { get; set; }
        public int ExpireSessionTime { get; set; }
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