using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class UsersAccessTrail
    {
        public int UsersPK { get; set; }
        public string UsersID { get; set; }
        public string LoginSuccessTimeLast { get; set; }
        public string LoginFailTimeLast { get; set; }
        public string LogoutTimeLast { get; set; }
        public string ChangePasswordTimeLast { get; set; }
        public int LoginSuccessFreq { get; set; }
        public int LoginFailFreq { get; set; }
        public int LogoutFreq { get; set; }
        public int ChangePasswordFreq { get; set; }        
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
    }

  
}

