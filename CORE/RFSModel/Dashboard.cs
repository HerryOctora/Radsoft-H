﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Dashboard
    {
        public string TableName { get; set; }
        public int NoSystem { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
    }

    public class Dashboard_TotalPendingTransaction
    {
        public string TableName { get; set; }
        public int TotalPending { get; set; }
     
    }
}