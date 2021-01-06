using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Consignee
    {
        public int ConsigneePK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string NoRek { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxID { get; set; }
        public string BankInformation { get; set; }
        public string BeneficiaryName { get; set; }
        public string Description { get; set; }
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

    public class ConsigneeCombo
    {
        public int ConsigneePK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class TypeOfAssetsCombo
    {
        public int TypeOfAssetsPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}