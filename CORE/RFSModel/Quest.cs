using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Quest
    {
        public string UsersID { get; set; }
        public string Password { get; set; }
        public string SessionID { get; set; }
        public string IpAddress { get; set; }
    }

    public class ChangePassword
    {
        public string NewPassword { get; set; }
    }
}