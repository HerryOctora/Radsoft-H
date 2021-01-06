using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Agent
    {
        public int AgentPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public decimal AgentFee { get; set; }
        public string NoRek { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxID { get; set; }
        public string BankInformation { get; set; }
        public string BeneficiaryName { get; set; }
        public string Description { get; set; }
        public bool Groups { get; set; }
        public int Levels { get; set; }
        public int ParentPK { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
        public int Depth { get; set; }
        public int ParentPK1 { get; set; }
        public int ParentPK2 { get; set; }
        public int ParentPK3 { get; set; }
        public int ParentPK4 { get; set; }
        public int ParentPK5 { get; set; }
        public int ParentPK6 { get; set; }
        public int ParentPK7 { get; set; }
        public int ParentPK8 { get; set; }
        public int ParentPK9 { get; set; }
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public int CompanyPositionSchemePK { get; set; }
        public string CompanyPositionSchemeID { get; set; }
        public bool BitisAgentBank { get; set; }
        public bool BitIsAgentCSR { get; set; }
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
        public string NPWPNo { get; set; }
        public bool BitPPH23 { get; set; }
        public bool BitPPH21 { get; set; }
        public string JoinDate { get; set; }

        public int MFeeMethod { get; set; }
        public string MFeeMethodDesc { get; set; }
        public int SharingFeeCalculation { get; set; }
        public string SharingFeeCalculationDesc { get; set; }

        public bool BitPPN { get; set; }
        public decimal PPH23Percent { get; set; }

        public string WAPERDNo { get; set; }
        public string WAPERDExpiredDate { get; set; }
        public string AAJINo { get; set; }
        public string AAJIExpiredDate { get; set; }
    }

    public class AgentCombo
    {
        public int AgentPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class AgentLookup
    {
        //10 Field
        public int AgentPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal AgentFee { get; set; }

    }
    public class SetAgentFee
    {
        public int AgentPK { get; set; }
        public int AgentFeeSetupPK { get; set; }
        public string Agent { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string DateAmortize { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
        public int TypeTrx { get; set; }
        public string TypeTrxDesc { get; set; }
        public decimal RangeTo { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal MIFeePercent { get; set; }
        public decimal MIFeeAmount { get; set; }
        public int FundPK { get; set; }
        public string FundName { get; set; }
        public string Date { get; set; }
        public string EntryUsersID { get; set; }
    }

    public class AgentTreeSetup
    {

        public int ChildPK { get; set; }
        public int ParentPK { get; set; }
        public string Date { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
        public int Levels { get; set; }
        public decimal FeePercent { get; set; }
        public string ParamDate { get; set; }
        public string ParamFund { get; set; }
        public string ParamAgent { get; set; }

    }

}