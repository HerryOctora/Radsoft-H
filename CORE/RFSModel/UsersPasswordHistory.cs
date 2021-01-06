using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class UsersPasswordHistory
    {
        public int UsersPasswordHistoryPK { get; set; }
        public string Date { get; set; }
        public string Password { get; set; }
        public int UsersPK { get; set; }
        public string UsersID { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }

    }
}