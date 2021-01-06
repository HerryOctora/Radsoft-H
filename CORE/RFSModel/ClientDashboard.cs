using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class ClientDashboard
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string KTPNo { get; set; }
        public string Date { get; set; }
        public string TransactionType { get; set; }
        public string Fund { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public decimal Amount { get; set; }
        public decimal NAB { get; set; }
        public decimal Unit { get; set; }
        public decimal Saldo { get; set; }

        public decimal Avg { get; set; }
        public decimal TotalBuy { get; set; }
        public decimal MarketValue { get; set; }
        public decimal ProfitLoss { get; set; }
        public decimal ProfitLossPercent { get; set; }

    }
}