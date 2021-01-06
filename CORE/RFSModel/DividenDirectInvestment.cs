using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFSModel
{
    public class DividenDirectInvestment
    {
        public int DividenDirectInvestmentPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }

        public int DirectInvestmentPK { get; set; }
        public decimal DividenRatio { get; set; }
        public decimal Profit { get; set; }
        public decimal Dividen { get; set; }
        public DateTime DateOfDeclaration { get; set; }
        public DateTime DateOfBooking { get; set; }


        public string LastUsersID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }

      

      
    }
}
