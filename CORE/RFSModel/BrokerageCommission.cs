using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class MSSales
    {
        public string Code { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class MSTransaction
    {
        public string Date { get; set; }
        public string Sales { get; set; }
        public string No_Cust { get; set; }
        public string Name { get; set; }
        public string RecBy { get; set; }
        public decimal Buying { get; set; }
        public decimal Selling { get; set; }
        public decimal Netting { get; set; }
        public decimal Total_Trans { get; set; }
        public decimal Comm { get; set; }
        public decimal Vat { get; set; }
        public decimal Levy { get; set; }
        public decimal Pph { get; set; }
        public decimal Rebate { get; set; }
        public decimal Other { get; set; }
        public decimal Net_Reb { get; set; }
        public decimal Expense { get; set; }
        public decimal Vol_Buy { get; set; }
        public decimal Vol_Sell { get; set; }
    }
    
}