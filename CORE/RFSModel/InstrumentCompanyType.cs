using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFSModel
{
    public class InstrumentCompanyType
    {
        public int InstrumentCompanyTypePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
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

        public string Companies { get; set; }
        public string Company { get; set; }
        public string CompanyType { get; set; }
        public string CostValue { get; set; }
        public DateTime Date { get; set; }
        public string Fund { get; set; }
        public string MarketValue { get; set; }
        public string Sector { get; set; }
        public string Unrealized { get; set; }

    }


    public class InstrumentCompanyTypeCombo
    {
        public int InstrumentCompanyTypePK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
    }

}
