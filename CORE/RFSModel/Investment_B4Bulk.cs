using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class Investment
    {
        public int InvestmentPK { get; set; }
        public int HistoryPK { get; set; }
        public int BankBranchPK { get; set; }
        public string BankBranchID { get; set; }
        public string PTPCode { get; set; }
        public int BankPK { get; set; }
        public bool SelectedInvestment { get; set; }
        public bool SelectedDealing { get; set; }
        public bool SelectedSettlement { get; set; }
        public int StatusInvestment { get; set; }
        public int StatusDealing { get; set; }
        public int StatusSettlement { get; set; }
        public string StatusDesc { get; set; }
        public string OrderStatusDesc { get; set; }
        public int OrderStatusNo { get; set; }
        public string Notes { get; set; }
        public int DealingPK { get; set; }
        public int SettlementPK { get; set; }
        public int InvestmentTrType { get; set; }
        public string InvestmentTrTypeDesc { get; set; }
        public string ValueDate { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string stringInvestmentFrom { get; set; }
        public string InstructionDate { get; set; }
        public int RefNo { get; set; }
        public string Reference { get; set; }
        public int InstrumentTypePK { get; set; }
        public string InstrumentTypeID { get; set; }
        public int TrxType { get; set; }
        public string TrxTypeID { get; set; }
        public int CounterpartPK { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public int MarketPK { get; set; }
        public string MarketID { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int FundCashRefPK { get; set; }
        public string FundCashRefID { get; set; }
        public string FundCashRefName { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal Lot { get; set; }
        public decimal LotInShare { get; set; }
        public string RangePrice { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestPercent { get; set; }
        public decimal BreakInterestPercent { get; set; }
        public decimal AccruedInterest { get; set; }
        public decimal DoneAccruedInterest { get; set; }
        public decimal AmountToTransfer { get; set; }
        public string LastCouponDate { get; set; }
        public string NextCouponDate { get; set; }
        public string MaturityDate { get; set; }
        public string SettledDate { get; set; }
        public string InvestmentNotes { get; set; }
        public decimal DoneLot { get; set; }
        public decimal DoneVolume { get; set; }
        public decimal DonePrice { get; set; }
        public decimal DoneAmount { get; set; }
        public string ClearingCode { get; set; }
        public string RTGSCode { get; set; }
        public int Tenor { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal LevyPercent { get; set; }
        public decimal KPEIPercent { get; set; }
        public decimal VATPercent { get; set; }
        public decimal WHTPercent { get; set; }
        public decimal OTCPercent { get; set; }
        public decimal IncomeTaxSellPercent { get; set; }
        public decimal IncomeTaxInterestPercent { get; set; }
        public decimal IncomeTaxGainPercent { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal LevyAmount { get; set; }
        public decimal KPEIAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal OTCAmount { get; set; }
        public decimal IncomeTaxSellAmount { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CurrencyRate { get; set; }
        public int SettlementMode { get; set; }
        public string SettlementModeDesc { get; set; }
        public int BoardType { get; set; }
        public string BoardTypeDesc { get; set; }
        public decimal TaxExpensePercent { get; set; }

        //ACQ
        public decimal AcqPrice { get; set; }
        public decimal AcqVolume { get; set; }
        public string AcqDate { get; set; }
        public decimal AcqPrice1 { get; set; }
        public decimal AcqVolume1 { get; set; }
        public string AcqDate1 { get; set; }
        public decimal AcqPrice2 { get; set; }
        public decimal AcqVolume2 { get; set; }
        public string AcqDate2 { get; set; }
        public decimal AcqPrice3 { get; set; }
        public decimal AcqVolume3 { get; set; }
        public string AcqDate3 { get; set; }
        public decimal AcqPrice4 { get; set; }
        public decimal AcqVolume4 { get; set; }
        public string AcqDate4 { get; set; }
        public decimal AcqPrice5 { get; set; }
        public decimal AcqVolume5 { get; set; }
        public string AcqDate5 { get; set; }
        public decimal AcqPrice6 { get; set; }
        public decimal AcqVolume6 { get; set; }
        public string AcqDate6 { get; set; }
        public decimal AcqPrice7 { get; set; }
        public decimal AcqVolume7 { get; set; }
        public string AcqDate7 { get; set; }
        public decimal AcqPrice8 { get; set; }
        public decimal AcqVolume8 { get; set; }
        public string AcqDate8 { get; set; }
        public decimal AcqPrice9 { get; set; }
        public decimal AcqVolume9 { get; set; }
        public string AcqDate9 { get; set; }

        public string Category { get; set; }
        public int CurrencyPK { get; set; }
        public string CurrencyID { get; set; }

        public string DateFrom { get; set; }
        public string DateTo { get; set; }


        public decimal DoneLot1 { get; set; }
        public decimal DoneLot2 { get; set; }
        public decimal DoneLot3 { get; set; }
        public decimal DoneLot4 { get; set; }
        public decimal DoneLot5 { get; set; }

        public decimal DonePrice1 { get; set; }
        public decimal DonePrice2 { get; set; }
        public decimal DonePrice3 { get; set; }
        public decimal DonePrice4 { get; set; }
        public decimal DonePrice5 { get; set; }

        public int InterestDaysType { get; set; }
        public int InterestPaymentType { get; set; }
        public int PaymentModeOnMaturity { get; set; }
        public string PaymentInterestSpecificDate { get; set; }
        public string InterestDaysTypeDesc { get; set; }

        public int PriceMode { get; set; }

        public bool BitIsAmortized { get; set; }

        public int TrxBuy { get; set; }
        public string TrxBuyType { get; set; }
        public int MethodType { get; set; }
        public decimal YieldPercent { get; set; }
        public bool BitIsRounding { get; set; }
        public decimal AccruedHoldingAmount { get; set; }

        public string BankID { get; set; }
        public bool Selected { get; set; }
        public bool BitBreakable { get; set; }
        public int CrossFundFromPK { get; set; }
        public string CrossFundFromID { get; set; }
        public int DepNo { get; set; }

        public int PurposeOfTransaction { get; set; }
        public string PurposeOfTransactionDesc { get; set; }
        public int StatutoryType { get; set; }
        public bool BitForeignTrx { get; set; }
        public string CPSafekeepingAccNumber { get; set; }
        public string PlaceOfSettlement { get; set; }
        public string FundSafekeepingAccountNumber { get; set; }
        public int SecurityCodeType { get; set; }
        public string SecurityCodeTypeDesc { get; set; }

        public bool BitHTM { get; set; }

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
        public string EntryDealingID { get; set; }
        public string EntryDealingTime { get; set; }
        public string UpdateDealingID { get; set; }
        public string UpdateDealingTime { get; set; }
        public string ApprovedDealingID { get; set; }
        public string ApprovedDealingTime { get; set; }
        public string VoidDealingID { get; set; }
        public string VoidDealingTime { get; set; }
        public string EntrySettlementID { get; set; }
        public string EntrySettlementTime { get; set; }
        public string UpdateSettlementID { get; set; }
        public string UpdateSettlementTime { get; set; }
        public string ApprovedSettlementID { get; set; }
        public string ApprovedSettlementTime { get; set; }
        public string VoidSettlementID { get; set; }
        public string VoidSettlementTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public decimal AvgPrice { get; set; }

        public bool BitRollOverInterest { get; set; }
        public decimal LotReksadana { get; set; }

    }

    public class CounterpartExposure
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal TotalPerBrokerFee { get; set; }
        public decimal AllBrokerFee { get; set; }
        public decimal Exposure { get; set; }
    }

    public class InvestmentDataAcq
    {
        public int InvestmentPK { get; set; }
        public int AcqNo { get; set; }
        public string AcqDate { get; set; }
        public decimal AcqVolume { get; set; }
        public decimal AcqPrice { get; set; }
        public int DaysOfHoldingInterest { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal TotalTaxIncomeAmount { get; set; }
        public decimal TaxAmount { get; set; }
    }


    public class InvestmentListing
    {
        public string ParamDateFrom { get; set; }
        public string ParamDateTo { get; set; }
        public string ParamListDate { get; set; }
        public string ParamFundID { get; set; }
        public string ParamFundIDFrom { get; set; }
        public string ParamFundIDTo { get; set; }
        public string ParamCounterpartIDFrom { get; set; }
        public string FundFromByAll { get; set; }
        public string CounterpartFromByAll { get; set; }
        public string ParamReferenceFrom { get; set; }
        public string ParamReferenceTo { get; set; }
        public string ParamReferenceText { get; set; }
        public string ParamInstType { get; set; }
        public string stringInvestmentFrom { get; set; }
        public DateTime InstructionDate { get; set; }
        public DateTime ValueDate { get; set; }
        public string FundID { get; set; }
        public string BoardTypeID { get; set; }
        public string InvestmentNotes { get; set; }
        public string FundName { get; set; }
        public string Reference { get; set; }
        public int RefNo { get; set; }
        public string InstrumentType { get; set; }
        public int InstrumentTypePK { get; set; }
        public string TrxTypeID { get; set; }
        public int InvestmentPK { get; set; }
        public int DealingPK { get; set; }
        public int SettlementPK { get; set; }
        public string BankName { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public string BankCustodianName { get; set; }
        public string ContactPerson { get; set; }
        public string BankCustodianContactPerson { get; set; }
        public string FaxNo { get; set; }
        public string Phone { get; set; }
        public string BankCustodianFaxNo { get; set; }
        public string BankCustodianPhone { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNo { get; set; }
        public string ClearingCode { get; set; }
        public string RTGSCode { get; set; }
        public int Tenor { get; set; }
        public int TenorDays { get; set; }
        public int HoldingPeriod { get; set; }
        public decimal AcqPrice { get; set; }
        public decimal Lot { get; set; }
        public decimal Volume { get; set; }
        public decimal OrderPrice { get; set; }
        public string RangePrice { get; set; }
        public decimal InterestPercent { get; set; }
        public decimal Amount { get; set; }
        public decimal DoneLot { get; set; }
        public decimal DoneVolume { get; set; }
        public decimal DonePrice { get; set; }
        public decimal DoneAmount { get; set; }
        public DateTime LastCouponDate { get; set; }
        public DateTime NextCouponDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public DateTime AcqDateDeposito { get; set; }
        public DateTime AcqDate { get; set; }
        public decimal AccruedInterest { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal LevyAmount { get; set; }
        public decimal KPEIAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal OTCAmount { get; set; }
        public decimal IncomeTaxSellAmount { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal DoneAccruedInterest { get; set; }
        public decimal TotalAmount { get; set; }
        public int SettlementMode { get; set; }
        public string SettlementModeDesc { get; set; }
        public string SettlementModeDescTwo { get; set; }
        public decimal AcqPrice1 { get; set; }
        public decimal AcqVolume1 { get; set; }
        public DateTime AcqDate1 { get; set; }
        public decimal AcqPrice2 { get; set; }
        public decimal AcqVolume2 { get; set; }
        public DateTime AcqDate2 { get; set; }
        public decimal AcqPrice3 { get; set; }
        public decimal AcqVolume3 { get; set; }
        public DateTime AcqDate3 { get; set; }
        public decimal AcqPrice4 { get; set; }
        public decimal AcqVolume4 { get; set; }
        public DateTime AcqDate4 { get; set; }
        public decimal AcqPrice5 { get; set; }
        public decimal AcqVolume5 { get; set; }
        public DateTime AcqDate5 { get; set; }
        public string Notes { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }
        public bool PageBreak { get; set; }
        public string Message { get; set; }
        public string DownloadMode { get; set; }
        public string BoardTypeDesc { get; set; }
        public string BoardTypeDescTwo { get; set; }
        public string SettlementModeDescOne { get; set; }
        public bool BitIsMature { get; set; }
        public string BankBranchName { get; set; }
        public string EntryDealingID { get; set; }
        public string PTPCode { get; set; }
        public string BankBranchID { get; set; }
        public string BankBranchAccountNo { get; set; }
        public decimal IncomeTaxInterestPercent { get; set; }
        public int TrxType { get; set; }

        public int Signature1 { get; set; }
        public string Signature1Desc { get; set; }
        public int Signature2 { get; set; }
        public string Signature2Desc { get; set; }
        public int Signature3 { get; set; }
        public string Signature3Desc { get; set; }
        public int Signature4 { get; set; }
        public string Signature4Desc { get; set; }
        public string BankCustodianID { get; set; }

        public string CounterpartBank { get; set; }
        public DateTime SettleDate { get; set; }
        public decimal Nominal { get; set; }
        public decimal Price { get; set; }
        public decimal CouponRate { get; set; }
        public string BankID { get; set; }
        public string BankType { get; set; }

        //---- Masih Belum diisi ----//
        public string Attn { get; set; }
        public string NoRekCustody { get; set; }
        public DateTime DistributedDate { get; set; }
        public string SubRegCode { get; set; }
        public string NoCTP { get; set; }
        public string FundRekName { get; set; }
        public string Telp { get; set; }
        public string CustodianID { get; set; }
        public string FaxNo2 { get; set; }
        public string Telp2 { get; set; }

        public string CustomerCode { get; set; }
        public string AccountCode { get; set; }
        public string BusinessArea { get; set; }
        public string ISIN { get; set; }
        public decimal IncomeTaxGainPercent { get; set; }
        public decimal NetProceeds { get; set; }
        public decimal CouponTax { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetSettled { get; set; }
        public string BankCustodian { get; set; }
        public DateTime TglPenempatan { get; set; }
        public decimal Rate { get; set; }

        public string Category { get; set; }
        public int Bulan { get; set; }
        public string ContactBankBranch { get; set; }
        public string FaxBankBranch { get; set; }
        public string PhoneBankBranch { get; set; }
        public string EmailBankBranch { get; set; }

        public string Phone1 { get; set; }
        public string CityDesc { get; set; }
        public string BankBranchAccountName { get; set; }
        public string Email1 { get; set; }
        public string ParamTrxType { get; set; }
        public bool BitSyariah { get; set; }
        public string AccountSAP { get; set; }
        public string BusinesArea { get; set; }

        public string InterestDaysType { get; set; }
        public string InterestPaymentType { get; set; }

        public string Breakable { get; set; }
    }

    public class InvestmentCommission
    {
        public decimal CommissionAmount { get; set; }
        public decimal LevyAmount { get; set; }
        public decimal KPEIAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal OTCAmount { get; set; }
        public decimal IncomeTaxSellAmount { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal TotalAmount { get; set; }

    }

    public class InvestmentGetInterest
    {
        public decimal InterestAmount { get; set; }
        public int Tenor { get; set; }
        public decimal IncomeTaxInterestAmount { get; set; }
        public decimal IncomeTaxGainAmount { get; set; }
        public decimal TotalAmount { get; set; }

    }

    public class SettlementRecalculate
    {
        public int InvestmentPK { get; set; }
        public int DealingPK { get; set; }
        public string OrderStatus { get; set; }
        public int CounterpartPK { get; set; }
        public int Mode { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal LevyPercent { get; set; }
        public decimal KPEIPercent { get; set; }
        public decimal VATPercent { get; set; }
        public decimal WHTPercent { get; set; }
        public decimal OTCPercent { get; set; }
        public decimal TaxSellPercent { get; set; }
        public decimal TaxInterestPercent { get; set; }
        public decimal TaxGainPercent { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DoneAmount { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal LevyAmount { get; set; }
        public decimal KPEIAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal OTCAmount { get; set; }
        public decimal TaxSellAmount { get; set; }
        public decimal TaxInterestAmount { get; set; }
        public decimal TaxGainAmount { get; set; }
        public int TrxType { get; set; }
        public int BoardType { get; set; }

    }

    public class InvestmentRpt
    {
        public string DownloadMode { get; set; }
        public bool PageBreak { get; set; }
        public int Status { get; set; }
        public int FundPK { get; set; }
        public string ReportName { get; set; }
        public DateTime ValueDateFrom { get; set; }
        public DateTime ValueDateTo { get; set; }
        public string ParamInstType { get; set; }
        public string ParamFundID { get; set; }
        public string ParamListDate { get; set; }
        public bool BitIsMature { get; set; }
        public string Message { get; set; }
        public string FundFrom { get; set; }
        public int Signature1 { get; set; }
        public int Signature2 { get; set; }
        public int Signature3 { get; set; }
        public int Signature4 { get; set; }
        public string NoSurat { get; set; }
        public int IndexPK { get; set; }

    }
    public class InvestmentAvgPriceByTrx
    {
        public DateTime Date { get; set; }
        public bool Result { get; set; }
        public int FundPK { get; set; }
        public int CounterpartPK { get; set; }
        public int InstrumentPK { get; set; }
        public string InstrumentID { get; set; }
        public string stringInvestmentFrom { get; set; }

    }

    public class BrokerRpt
    {
        public string Broker { get; set; }
        public string Stock { get; set; }
        public string Status { get; set; }
        public string FundName { get; set; }
        public decimal Lot { get; set; }
        public decimal Nominal { get; set; }
        public decimal Price { get; set; }
        public decimal Lot2 { get; set; }
        public decimal Share { get; set; }
        public decimal NominalVolume { get; set; }

    }


    public class KertasKerja
    {
        public DateTime Date { get; set; }
        public string Reksadana { get; set; }
        public string BuySell { get; set; }
        public int TrxType { get; set; } //Buy/Sell
        public string TrxTypeID { get; set; }
        public string Securities { get; set; }
        public int InstrumentTypePK { get; set; } //Securities
        public string InstrumentTypeID { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public string Counterpart { get; set; }
        public int CounterpartPK { get; set; } //Counterpart
        public string CounterpartID { get; set; }
        public string CounterpartName { get; set; }
        public string Notes { get; set; }
        public string Dealer { get; set; }
        public string Settlement { get; set; }


        public string Buyer { get; set; }
        public string Seller { get; set; }
        public decimal Size { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public string TypeOfTransaction { get; set; }

        public string Type { get; set; }
        public string Bank { get; set; }
        public decimal Nominal { get; set; }
        public decimal InterestRate { get; set; }
        public string Tenor { get; set; }

        public string Fund { get; set; }
        public string Security { get; set; }
        public int InstructionNo { get; set; }
        public string Broker { get; set; }
        public string AffYN { get; set; }
        public string SecurityName { get; set; }
        public decimal Lot { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Comm { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal TotalLevy { get; set; }
        public decimal VATBrokerage { get; set; }
        public decimal Amount { get; set; }
        public string FundManager { get; set; }

        public int Signature1 { get; set; }
        public string Signature1Desc { get; set; }
        public int Signature2 { get; set; }
        public string Signature2Desc { get; set; }
        public int Signature3 { get; set; }
        public string Signature3Desc { get; set; }
        public int Signature4 { get; set; }
        public string Signature4Desc { get; set; }

        public string NamaFund { get; set; }
        public string InstrumentName { get; set; }
        public string BondName { get; set; }
        public string ShortCode { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime LastCouponPayment { get; set; }
        public decimal CurrentCoupon { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal AccruedInterest { get; set; }
        public decimal TaxAccruedInterest { get; set; }
        public decimal TaxCapitalGainLoss { get; set; }
        public decimal TaxIncome { get; set; }
        public decimal TotalTaxIncome { get; set; }
        public decimal NetProceeds { get; set; }
        public decimal AcqPrice { get; set; }
        public DateTime AcqDate { get; set; }
        public decimal AcqVolume { get; set; }
        public int RefNo { get; set; }
        public string Reference { get; set; }
        public string Attention { get; set; }

        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Cc { get; set; }
        public string NamaBank { get; set; }
        public string BankBranch { get; set; }
        public string IFO { get; set; }
        public string Issuer { get; set; }
        public string NB { get; set; }
        public decimal Pokok { get; set; }
        public decimal Bunga { get; set; }
        public decimal Total { get; set; }
        public decimal Rate { get; set; }
        public int Days { get; set; }
        public string Periode { get; set; }

        public DateTime ValueDate { get; set; }
        public int DaysCount { get; set; }
        public decimal Principal { get; set; }
        public string NoAC { get; set; }
        public string Message { get; set; }
        public string Untuk { get; set; }
        public string AccountName { get; set; }
        public decimal Interest { get; set; }
        public string Re { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public decimal PrincipalInterest { get; set; }

    }




}