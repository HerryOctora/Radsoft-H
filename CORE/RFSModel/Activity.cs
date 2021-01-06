using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Activity
    {
        public long ActivityPK { get; set; }
        public string Time { get; set; }
        public string PermissionID { get; set; }
        public string ObjectTable { get; set; }
        public int ObjectTablePK { get; set; }
        public long ObjectNo { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string IPAddress { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string UsersID { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate  { get; set; }
        public string LastUpdateDB { get; set; }
    }
}