using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class ValidateClientRedemption
    {
        public string Reason { get; set; }
        public string Notes { get; set; }
        public int FundClientPK { get; set; }
        public int InsertHighRisk { get; set; }
        public int Validate { get; set; }
        public int No { get; set; }
    }

    public class ClientRedemption
    {
        public int ClientRedemptionPK { get; set; }
        public int HistoryPK { get; set; }
        public bool Selected { get; set; }
        public string ClientRedemptionSelected { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public int Type { get; set; }
        public string TypeDesc { get; set; }
        public bool BitRedemptionAll { get; set; }
        public string Notes { get; set; }
        public string NAVDate { get; set; }
        public string ValueDate { get; set; }
        public string PaymentDate { get; set; }
        public decimal NAV { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string FundClientName { get; set; }
        public int CashRefPK { get; set; }
        public string CashRefID { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }
        public string Description { get; set; }
        public string ReferenceSInvest { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal TotalCashAmount { get; set; }
        public decimal TotalUnitAmount { get; set; }
        public decimal RedemptionFeePercent { get; set; }
        public decimal RedemptionFeeAmount { get; set; }
        public int AgentPK { get; set; }
        public string AgentID { get; set; }
        public string AgentName { get; set; }
        public decimal AgentFeePercent { get; set; }
        public decimal AgentFeeAmount { get; set; }
        public decimal UnitPosition { get; set; }
        public int BankRecipientPK { get; set; }
        public string BankRecipientDesc { get; set; }
        public int Signature1 { get; set; }
        public string Signature1Desc { get; set; }
        public int Signature2 { get; set; }
        public string Signature2Desc { get; set; }
        public int Signature3 { get; set; }
        public string Signature3Desc { get; set; }
        public int Signature4 { get; set; }
        public string Signature4Desc { get; set; }
        public int TransferType { get; set; }
        public string TransferTypeDesc { get; set; }
        public int FeeType { get; set; }
        public string FeeTypeDesc { get; set; }
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
        public int ParamRedempType { get; set; }
        public string ParamPaymentDateFrom { get; set; }
        public string ParamPaymentDateTo { get; set; }
        public string ParamFundIDFrom { get; set; }
        public string ParamFundIDTo { get; set; }
        public string DownloadMode { get; set; }
        public bool HideDate { get; set; }
        public string TransactionPK { get; set; }
        public string IFUACode { get; set; }
        public string FrontID { get; set; }
        public string UnitRegistrySelected { get; set; }
    }

    public class CashInstruction
    {
        public string RekeningPengirim { get; set; }
        public string RekeningTujuan { get; set; }
        public decimal Amount { get; set; }
        public string Keterangan { get; set; }
    }

    public class ClientRedemptionRecalculate
    {
        public decimal Nav { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal RedemptionFeePercent { get; set; }
        public decimal AgentFeePercent { get; set; }
        public decimal RedemptionFeeAmount { get; set; }
        public decimal AgentFeeAmount { get; set; }
        public decimal TotalCashAmount { get; set; }
        public decimal TotalUnitAmount { get; set; }
    }

    public class ParamClientRedemptionRecalculate
    {
        public int ClientRedemptionPK { get; set; }
        public int FeeType { get; set; }
        public int FundPK { get; set; }
        public string NavDate { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal RedemptionFeePercent { get; set; }
        public decimal AgentFeePercent { get; set; }
        public decimal RedemptionFeeAmount { get; set; }
        public decimal AgentFeeAmount { get; set; }
        public decimal TotalCashAmount { get; set; }
        public decimal TotalUnitAmount { get; set; }
        public string UpdateUsersID { get; set; }
        public string LastUpdate { get; set; }
    }

    public class ClientRedemptionHoldingPeriod
    {
        public string FundID { get; set; }
        public int FundPK { get; set; }
        public int FundClientPK { get; set; }
        public int CashRefPK { get; set; }
        public string FundClientName { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime RedempDate { get; set; }
        public int HoldingPeriod { get; set; }
        public decimal TotalSubs { get; set; }
        public decimal TakenOut { get; set; }
        public decimal Remaining { get; set; }
        public decimal TotalFeeAmount { get; set; }
        public decimal RedempFeePercent { get; set; }
        public decimal RedempUnit { get; set; }
        public int UnitDecimalPlaces { get; set; }
        public string FundFrom { get; set; }
        public string FundClientFrom { get; set; }

    }

    public class ClientRedemptionBatchForm
    {

        public string NamaFund { get; set; }
        public string Ref { get; set; }
        public DateTime Date { get; set; }
        public string ToNamaBank { get; set; }
        public string ToAlamat { get; set; }
        public string ToAttention { get; set; }
        public string FaxNumber { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public int FormNo { get; set; }
        public string CustomerID { get; set; }
        public string CustomerSIDNumber { get; set; }
        public decimal RedeemedNominalAmount { get; set; }
        public decimal RedeemedNoOfUnits { get; set; }
        public string Name { get; set; }
        public decimal NominalAmount { get; set; }
        public decimal FeePercent { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal NAV { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime DateFundToBeTransferred { get; set; }
        public string UnitRegistrySelected { get; set; }

        public string BankRecipient { get; set; }

    }




}