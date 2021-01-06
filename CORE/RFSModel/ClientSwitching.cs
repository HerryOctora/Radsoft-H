using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class ValidateClientSwitching
    {
        public string Reason { get; set; }
        public string Notes { get; set; }
        public int FundClientPK { get; set; }
        public int InsertHighRisk { get; set; }
        public int Validate { get; set; }
        public int No { get; set; }
    }

    public class ClientSwitching
    {
        public int ClientSwitchingPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public string Notes { get; set; }
        public string NAVDate { get; set; }
        public string ValueDate { get; set; }
        public string PaymentDate { get; set; }
        public decimal NAVFundFrom { get; set; }
        public decimal NAVFundTo { get; set; }
        public int FundPKFrom { get; set; }
        public string FundIDFrom { get; set; }
        public int FundPKTo { get; set; }
        public string FundIDTo { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int CashRefPKFrom { get; set; }
        public string CashRefIDFrom { get; set; }
        public int CashRefPKTo { get; set; }
        public string CashRefIDTo { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public int TransferType { get; set; }
        public string TransferTypeDesc { get; set; }
        public string Description { get; set; }
        public string ReferenceSInvest { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public string FeeType { get; set; }
        public decimal SwitchingFeePercent { get; set; }
        public decimal SwitchingFeeAmount { get; set; }
        public decimal TotalCashAmountFundFrom { get; set; }
        public decimal TotalCashAmountFundTo { get; set; }
        public decimal TotalUnitAmountFundFrom { get; set; }
        public decimal TotalUnitAmountFundTo { get; set; }
        public bool BitSwitchingAll { get; set; }
        public int Signature1 { get; set; } 
        public string Signature1Desc { get; set; }
        public int Signature2 { get; set; } 
        public string Signature2Desc { get; set; }
        public int Signature3 { get; set; } 
        public string Signature3Desc { get; set; }
        public int Signature4 { get; set; } 
        public string Signature4Desc { get; set; }
        public int FeeTypeMethod { get; set; }
        public string FeeTypeMethodDesc { get; set; }
        public string FundNameFrom { get; set; }
        public string FundNameTo { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedTime { get; set; }
        public bool Revised { get; set; }
        public string RevisedBy { get; set; }
        public string RevisedTime { get; set; }
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

        public string UserSwitchingPK { get; set; }
        public string TransactionPK { get; set; }
        public string IFUACode { get; set; }
        public string FrontID { get; set; }
        public string ClientSwitchingSelected { get; set; }
        public string UnitRegistrySelected { get; set; }
        public string DownloadMode { get; set; }
    }

    public class ClientSwitchingRecalculate
    {
        public decimal NavFundFrom { get; set; }
        public decimal NavFundTo { get; set; }
        public decimal TotalCashAmountFundFrom { get; set; }
        public decimal TotalCashAmountFundTo { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal TotalUnitAmountFundFrom { get; set; }
        public decimal TotalUnitAmountFundTo { get; set; }
        public decimal SwitchingFeePercent { get; set; }
        public decimal SwitchingFeeAmount { get; set; }
    }

    public class ParamClientSwitchingRecalculate
    {
        public int ClientSwitchingPK { get; set; }
        public int FundPKFrom { get; set; }
        public int FundPKTo { get; set; }
        public int FeeTypeMode { get; set; }
        public string NavDate { get; set; }
        public string FeeType { get; set; }
        public bool BitSwitchingAll { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal SwitchingFeePercent { get; set; }
        public decimal SwitchingFeeAmount { get; set; }
        public string UpdateUsersID { get; set; }
        public string LastUpdate { get; set; }
    }

    public class ClientSwitchingAddNew
    {
        public int ClientSwitchingPK { get; set; }
        public long HistoryPK { get; set; }
        public string Message { get; set; }
    }

  

}