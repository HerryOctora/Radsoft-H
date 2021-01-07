using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Reports
    {
    }


    public class SAManagementFeeReport
    {
        public int FeeType { get; set; }
        public int AgentPK { get; set; }
        public int FundPK { get; set; }
        public string FundName { get; set; }
        public string CSRName { get; set; }
        public decimal ManagementFeeRate { get; set; }
        public decimal CSRFee { get; set; }
        public string ManagementFeeType { get; set; }
        public string SharingFeeType { get; set; }
        public string Currency { get; set; }
        public string Date { get; set; }
        public decimal NAV { get; set; }
        public decimal unit { get; set; }
        public decimal AUM { get; set; }
        public decimal MFee { get; set; }
        public decimal GrossShare { get; set; }
        public decimal Pph23 { get; set; }

    }

    public class ClientVsBlackListName
    {
        public string ClientName { get; set; }
        public string SID { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }

    }

    public class EmailRpt
    {
        public string DownloadMode { get; set; }
        public bool PageBreak { get; set; }
        public int Status { get; set; }
        public int FundPK { get; set; }
        public string ReportName { get; set; }
        public DateTime ValueDateFrom { get; set; }
        public DateTime ValueDateTo { get; set; }
        public string Message { get; set; }
        public string FundFrom { get; set; }
        public int IndexPK { get; set; }
    }

    public class Email
    {
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string ExpType { get; set; }
        public string ID { get; set; }
        public decimal CurrentPercent { get; set; }
        public decimal MinimumPercent { get; set; }
        public decimal MaximumPercent { get; set; }
    }

    public class SubsVSRedempt
    {
        public decimal NetValue { get; set; }
        public int Year { get; set; }
        public int Monthcalc { get; set; }
        public string Monthyear { get; set; }
        public int NumberofRecords { get; set; }
        public string Bu { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string Month { get; set; }
        public string OrderIndex { get; set; }
        public decimal Redemption { get; set; }
        public decimal Subscription { get; set; }

    }

    public class TransferDanaOverBooking
    {
        public DateTime DateTo { get; set; }
        public string RefNo { get; set; }
        public string FundCashRef { get; set; }
        public string BankBranch { get; set; }
        public string fax { get; set; }
        public string BankCustodi { get; set; }
        public string BankBranch1 { get; set; }
        public string tlp1 { get; set; }
        public string fax1 { get; set; }
        public string BankBranchUp { get; set; }
        public string Perihal { get; set; }
        public string FundName { get; set; }
        public string ServerDate { get; set; }
        public decimal Amount { get; set; }
        public string AccDebet { get; set; }
        public string NorekDebet { get; set; }
        public string bankDebet { get; set; }
        public string AccCredit { get; set; }
        public string NorekCredit { get; set; }
        public string BankCredit { get; set; }
        public string BankTo { get; set; }

    }

    public class PieChart
    {
        //public int FundPK { get; set; }
        public int PortfolioBn { get; set; }
        public int NumberOfRecords { get; set; }
        public string Category { get; set; }
        public string Company { get; set; }
        public DateTime Date { get; set; }
        public string Item { get; set; }
        public int Percentage { get; set; }
        public string Period { get; set; }
        public decimal Portfolio { get; set; }
        public string Fund { get; set; }
    }


    public class PortfolioValuationReport
    {
        public string SecurityCode { get; set; }
        public string SecurityDescription { get; set; }
        public string TimeDeposit { get; set; }
        public string BICode { get; set; }
        public string Branch { get; set; }
        public string ISINCode { get; set; }
        public string InstrumentTypeName { get; set; }
        public decimal QtyOfUnit { get; set; }
        public decimal Lot { get; set; }
        public decimal AverageCost { get; set; }
        public decimal BookValue { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnrealizedProfitLoss { get; set; }
        public decimal PercentFR { get; set; }
        public decimal Nominal { get; set; }
        public decimal RateGross { get; set; }
        public decimal AccIntTD { get; set; }
        public string TradeDate { get; set; }
        public string MaturityDate { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int InstrumentTypePK { get; set; }
        public string Date { get; set; }
        public string Fund { get; set; }
        public decimal TaxExpensePercent { get; set; }
        public decimal AccInterestBond { get; set; }
        public decimal TaxAccInterestBond { get; set; }
        public decimal AccrualInterestDeposito { get; set; }
        public decimal PercentOfAUM { get; set; }
        public decimal PercentPorto { get; set; }
        public decimal DailyAccInterestBond { get; set; }
        public int DaysType { get; set; }
        public int HoldingDays { get; set; }
        public string LastCouponDate { get; set; }
        public string NextCouponDate { get; set; }
        public decimal NetInterest { get; set; }
        public decimal HoldingAccrual { get; set; }
        public decimal AvgPrice { get; set; }
        public int DaysCoupon { get; set; }
        public decimal PercentOfNav { get; set; }
        public string NameSheet { get; set; }
        public int FundPK { get; set; }


    }

    public class MISRpt
    {
        public string ReportName { get; set; }
        public DateTime ValueDate { get; set; }

        public string MISCostCenterFrom { get; set; }
    }
    public class FinanceRpt
    {
        public bool  PageBreak { get; set; }
        public string ReportName { get; set; }
        public DateTime ValueDateFrom { get; set; }
        public DateTime ValueDateTo { get; set; }
        public string BankFrom { get; set; }
        public string BankTo { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        public string CashierType { get; set; }
        public string ReferenceFrom { get; set; }
        public string ReferenceTo { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public int Status { get; set; }
        public int ParamData { get; set; }
        public string DownloadMode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class SharingFeeReport
    {
        public string FundName { get; set; }
        public string FundClientName { get; set; }
        public string AgentName { get; set; }
        public string SellingAgentPK { get; set; }
        public decimal ManagementFeePercent { get; set; }
        public string Currency { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Date { get; set; }
        public string MfeeDate { get; set; }
        public string NAVDate { get; set; }
        public decimal Nav { get; set; }
        public decimal SubsUnit { get; set; }
        public decimal SubsAmount { get; set; }
        public decimal RedempUnit { get; set; }
        public decimal RedempAmount { get; set; }
        public decimal SwitchInUnit { get; set; }
        public decimal SwitchInAmount { get; set; }
        public decimal SwitchOutUnit { get; set; }
        public decimal SwitchOutAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal AUM { get; set; }
        public decimal MGTFee { get; set; }
        public decimal FeeShare { get; set; }
        public decimal SharingFee { get; set; }
        public decimal RebateFeePercent { get; set; }
        public decimal Percentage { get; set; }
        public decimal PungutanOJK { get; set; }
        public string FundType { get; set; }
        public bool BitisAgentBank { get; set; }
        public string MFeeType { get; set; }
        public string SharingFeeType { get; set; }

        public decimal OJKFee { get; set; }
        public decimal SharingGross { get; set; }
        public decimal PPN { get; set; }
        public decimal PPH23 { get; set; }
        public decimal SharingNet { get; set; }
        public decimal StartBalance { get; set; }

    }


    public class AgentSharingFeeReport
    {
        public string FundName { get; set; }
        public string AgentName { get; set; }
        public string SellingAgentPK { get; set; }
        public decimal ManagementFeePercent { get; set; }
        public string Currency { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Date { get; set; }
        public decimal Nav { get; set; }
        public decimal SubsUnit { get; set; }
        public decimal SubsAmount { get; set; }
        public decimal RedempUnit { get; set; }
        public decimal RedempAmount { get; set; }
        public decimal SwitchInUnit { get; set; }
        public decimal SwitchInAmount { get; set; }
        public decimal SwitchOutUnit { get; set; }
        public decimal SwitchOutAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal AUM { get; set; }
        public decimal MGTFee { get; set; }
        public decimal FeeShare { get; set; }
        public decimal SharingFee { get; set; }
        public decimal RebateFeePercent { get; set; }
        public decimal Percentage { get; set; }
        public string FundType { get; set; }
        public bool BitisAgentBank { get; set; }
        public string MFeeType { get; set; }
        public string SharingFeeType { get; set; }

    }

    public class AccountingRpt
    {
        public string FundFrom { get; set; }
        public string DownloadMode { get; set; }
        public string Profile { get; set; }
        public int PeriodPK { get; set; }
        public int GroupsPK { get; set; }
        public string Groups { get; set; }
        public bool PageBreak { get; set; }
        public bool ShowNullData { get; set; }
        public int Status { get; set; }
        public string ReportName { get; set; }
        public DateTime ValueDateFrom { get; set; }
        public DateTime ValueDateTo { get; set; }
        public string ReferenceFrom { get; set; }
        public string ReferenceTo { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        public string OfficeFrom { get; set; }
        public string OfficeTo { get; set; }
        public string DepartmentFrom { get; set; }
        public string DepartmentTo { get; set; }
        public string AgentFrom { get; set; }
        public string AgentTo { get; set; }
        public string ConsigneeFrom { get; set; }
        public string ConsigneeTo { get; set; }
        public string InstrumentFrom { get; set; }
        public string InstrumentTo { get; set; }
        public string CounterpartFrom { get; set; }
        public string CounterpartTo { get; set; }
        public string RowFrom { get; set; }
        public string RowTo { get; set; }
        public int ParamData { get; set; }
        public string CurrencyFrom { get; set; } 
        public string CurrencyTo { get; set; }
        public decimal Nominal { get; set; }
        public DateTime Date { get; set; }

        public string ID { get; set; }
        public string Name { get; set; }
        public decimal PreviousBaseBalance { get; set; }
        public decimal BaseDebitMutasi { get; set; }
        public decimal BaseCreditMutasi { get; set; }
        public decimal CurrentBaseBalance { get; set; }

        public decimal PrevBalance { get; set; }
        public decimal CurrBalance { get; set; }
        public decimal YtdBalance { get; set; }
        public string Period { get; set; }
        public int AccountBy { get; set; }
        public decimal Balance { get; set; }

        public string Month { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string DepartmentID { get; set; }
        public decimal Version { get; set; }
        public decimal Actual { get; set; }
        public decimal Difference { get; set; }
        public int DecimalPlaces { get; set; }

    }


    public class FundAccountingRpt
    {
        public string DownloadMode { get; set; }
        public string BondRatingFrom { get; set; }
        public string Profile { get; set; }
        public string Groups { get; set; }
        public bool PageBreak { get; set; }
        public bool ShowNullData { get; set; }
        public int Status { get; set; }
        public int PeriodPK { get; set; }
        public string ReportName { get; set; }
        public DateTime ValueDateFrom { get; set; }
        public DateTime ValueDateTo { get; set; }
        public string ReferenceFrom { get; set; }
        public string ReferenceTo { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        public string FundClientFrom { get; set; }
        public string FundClientTo { get; set; }
        public string FundFrom { get; set; }
        public string FundTo { get; set; }
        public string InstrumentFrom { get; set; }
        public string InstrumentTo { get; set; }
        public string RowFrom { get; set; }
        public string RowTo { get; set; }
        public int ParamData { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public string CounterpartFrom { get; set; }
        public string InstrumentTypeFrom { get; set; }
        public string TrxType { get; set; }
        public string FundName { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public decimal UnitQuantity { get; set; }
        public decimal Lot { get; set; }
        public decimal AverageCost { get; set; }
        public decimal BookValue { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnrealizedProfitLoss { get; set; }
        public decimal PercentProfilLoss { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Sector { get; set; }
        public decimal PercentTA { get; set; }
        public decimal PercentJCI { get; set; }
        public decimal Beta { get; set; }
        public decimal PercentSegment { get; set; }
        public decimal Compliance { get; set; }
        public string TimeDeposit { get; set; }
        public string BICode { get; set; }
        public string Branch { get; set; }
        public decimal Nominal { get; set; }
        public string TradeDate { get; set; }
        public string MaturityDate { get; set; }
        public decimal Rate { get; set; }
        public decimal AccTD { get; set; }
        public string MaturityAlert { get; set; }
        public string PortfolioEfek { get; set; }
        public string InsType { get; set; }
        public decimal PercentInvestment { get; set; }
        public string Sectors { get; set; }
        public decimal AlokasiSector { get; set; }
        public int FundPK { get; set; }
        public string BankFrom { get; set; }
        public int Signature1 { get; set; }
        public int Signature2 { get; set; }
        public bool ZeroBalance { get; set; }
        public string TrxTypeDeposito { get; set; }
        public string AgentFrom { get; set; }
        public int Signature11 { get; set; }
        public int Signature22 { get; set; }
        public int Signature33 { get; set; }
        public int Signature44 { get; set; }
    }


    public class CashierVoucher
    {
        public DateTime ValueDate { get; set; }
        public string Reference { get; set; }
        public string CashierID { get; set; }
        public string Description { get; set; }
        public int RefNo { get; set; }
        public string BankID { get; set; }
        public string BankName { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string BankCurrencyID { get; set; }
        public string DebitCredit { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal BaseDebit { get; set; }
        public decimal BaseCredit { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal BaseAmount { get; set; }
        public string OfficeID { get; set; }
        public string DepartmentID { get; set; }
        public string AgentID { get; set; }
        public string ConsigneeID { get; set; }
        public string InstrumentID { get; set; }
        public string CheckedBy { get; set; }
        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string DocRef { get; set; }
        public string ReceiverName { get; set; }
    }

    public class JournalVoucher
    {
        public DateTime ValueDate { get; set; }
        public int RefNo { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string BankCurrencyID { get; set; }
        public string DebitCredit { get; set; }
        public decimal Amount { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Rate { get; set; }
        public decimal BaseDebit { get; set; }
        public decimal BaseCredit { get; set; }
        public string OfficeID { get; set; }
        public string DepartmentID { get; set; }
        public string AgentID { get; set; }
        public string ConsigneeID { get; set; }
        public string InstrumentID { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }

    }

    public class AccountActivity
    {
        public decimal StartBalance { get; set; }
        public decimal StartBalanceUSD { get; set; }
        public decimal StartRate { get; set; }
        public int journalPK { get; set; }
        public int FundJournalPK { get; set; }
        public int CurrencyInstrument { get; set; }
        public DateTime ValueDate { get; set; }
        public string Reference { get; set; }
        public string CashierID { get; set; }
        public int RefNo { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public int AccountType { get; set; }
        public string DetailDescription { get; set; }
        public string DebitCredit { get; set; }
        public decimal Amount { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Rate { get; set; }
        public decimal BaseDebit { get; set; }
        public decimal BaseCredit { get; set; }
        public string CurrencyID { get; set; }
        public string OfficeID { get; set; }
        public string DepartmentID { get; set; }
        public string AgentID { get; set; }
        public string ConsigneeID { get; set; }
        public string InstrumentID { get; set; }
        public string FundID { get; set; }
        public string FundClientID { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string DocRef { get; set; }
        public string ConsigneeName { get; set; }
        public string NoReference { get; set; }

    }

    public class BalanceSheetCustomized
    {
        public string Row { get; set; }
        public string Type { get; set; }
        public string ItemName { get; set; }
        public string CurrencyID { get; set; }
        public decimal CurrBalance { get; set; }
        public decimal PrevBalance { get; set; }

    }

    public class TrialBalanceGroupsRpt
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal PreviousBaseBalance { get; set; }
        public decimal BaseDebitMutasi { get; set; }
        public decimal BaseCreditMutasi { get; set; }
        public decimal CurrentBaseBalance { get; set; }
        public string InstrumentID { get; set; }
        public string DepartmentID { get; set; }
        public string OfficeID { get; set; }
        public string AgentID { get; set; }
     

    }

    public class IncomeStatementGroupsRpt
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Groups { get; set; }
        public decimal CurrentBalance { get; set; }
        public string InstrumentID { get; set; }
        public string DepartmentID { get; set; }
        public string ConsigneeID { get; set; }
        public string OfficeID { get; set; }
        public string AgentID { get; set; }


    }

    public class AuditTrailRpt
    {
        public bool PageBreak { get; set; }
        public string ReportName { get; set; }
        public string UsersFrom { get; set; }
        public string UsersTo { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string TableName { get; set; }
        public string PKFrom { get; set; }
        public string PKTo { get; set; }
    }

     public class UsersAccessTrailRpt
    {
        public string UsersID { get; set; }
        public string Name { get; set; }
        public string LoginSuccessTimeLast { get; set; }
        public string LoginFailTimeLast { get; set; }
        public string LogoutTimeLast { get; set; }
        public string ChangePasswordTimeLast { get; set; }
        public int LoginSuccessFreq { get; set; }
        public int LoginFailFreq { get; set; }
        public int LogoutFreq { get; set; }
        public int ChangePasswordFreq { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }
      }

     public class UsersLogRpt
     {
         public string UsersID { get; set; }
         public string Name { get; set; }
         public string Time { get; set; }
         public int Status { get; set; }
         public string Message { get; set; }
         public string StackTrace { get; set; }
         public string Source { get; set; }
         public string Permission { get; set; }
         public string CheckedBy { get; set; }
         public string ApprovedBy { get; set; }
     }

     public class FundClientRpt
     {
         public bool PageBreak { get; set; }
         public string ReportName { get; set; }
         public string ClientFrom { get; set; }
         public string ClientTo { get; set; }
         public string Type { get; set; }
         public string Date { get; set; }
         public int InvestorType { get; set; }
         public string DateFrom { get; set; }
         public string DateTo { get; set; }
         public string ParamDate { get; set; }
         public string ParamReligion { get; set; }
         public string ParamMonth { get; set; }
     }

     public class FundClientRptByInvestorType
     {
         public bool PageBreak { get; set; }
         public string FundClientID { get; set; }
         public string OldID { get; set; }
         public string InvestorType { get; set; }
         public string InternalCategory { get; set; }
         public string InternalName { get; set; }
         public string SellingAgent { get; set; }
         public string SID { get; set; }
         public string IFUACode { get; set; }
         public string InvestorsRiskProfile { get; set; }
         public string KYCRiskProfile { get; set; }
         public string DormantDate { get; set; }
         public string Affiliated { get; set; }
         public string AffiliatedWith { get; set; }
         public string Suspended { get; set; }
         public string NPWP { get; set; }
         public string RegistrationNPWP { get; set; }
         public string Email { get; set; }
         public string PhoneNumber { get; set; }
         public string MobilePhone { get; set; }
         public string BusinessPhone { get; set; }
         public string CityOfEstablishment { get; set; }
         public string SellingAgentCode { get; set; }
         public string CountryofDomicile { get; set; }
         public string Fax { get; set; }
         public string Country { get; set; }
         public string Nationality { get; set; }
         public string BankRDN { get; set; }
         public string RDNAccountName { get; set; }
         public string RDNAccountNumber { get; set; }
         public string BankName1 { get; set; }
         public string BankAccountName1 { get; set; }
         public string BankAccountNumber1 { get; set; }
         public string BankBranchName1 { get; set; }
         public string BankCountry1 { get; set; }
         public string Currency1 { get; set; }
         public string BIMemberCode1 { get; set; }
         public string BICCode1 { get; set; }
         public string BankName2 { get; set; }
         public string BankAccountName2 { get; set; }
         public string BankAccountNumber2 { get; set; }
         public string BankBranchName2 { get; set; }
         public string BankCountry2 { get; set; }
         public string Currency2 { get; set; }
         public string BIMemberCode2 { get; set; }
         public string BICCode2 { get; set; }
         public string BankName3 { get; set; }
         public string BankAccountName3 { get; set; }
         public string BankAccountNumber3 { get; set; }
         public string BankBranchName3 { get; set; }
         public string BankCountry3 { get; set; }
         public string Currency3 { get; set; }
         public string BIMemberCode3 { get; set; }
         public string BICCode3 { get; set; }
         public string AssetOwner { get; set; }
         public string StatementType { get; set; }
         public string FATCAStatus { get; set; }
         public string TIN { get; set; }
         public string TINIssuanceCountry { get; set; }
         public string GIIN { get; set; }
         public string SubstantialOwnerName { get; set; }
         public string SubstantialOwnerAddress { get; set; }
         public string SubstantialOwnerTIN { get; set; }
         public string FirstNameInd { get; set; }
         public string MiddleNameInd { get; set; }
         public string LastNameInd { get; set; }
         public string BirthPlace { get; set; }
         public string CountryOfBirth { get; set; }
         public string DOB { get; set; }
         public string GenderSex { get; set; }
         public string MaritalStatus { get; set; }
         public string Occupation { get; set; }
         public string Education { get; set; }
         public string Religion { get; set; }
         public string IncomePerAnnum { get; set; }
         public string SourceOfFunds { get; set; }
         public string InvestmentObjectives { get; set; }
         public string MotherMaidenName { get; set; }
         public string SpouseName { get; set; }
         public string SpouseOccupation { get; set; }
         public string Heir { get; set; }
         public string HeirRelation { get; set; }
         public string NatureOfBusiness { get; set; }
         public string NatureOfBusinessDesc { get; set; }
         public string PoliticallyExposed { get; set; }
         public string PoliticallyExposedDesc { get; set; }
         public string OtherHomePhone { get; set; }
         public string OtherCellPhone { get; set; }
         public string OtherFax { get; set; }
         public string OtherEmail { get; set; }
         public string CorrespondenceAddress { get; set; }
         public string CorrespondenceCity { get; set; }
         public string CorrespondenceZipCode { get; set; }
         public string CountryofCorrespondence { get; set; }
         public string DomicileAddress { get; set; }
         public string DomicileCity { get; set; }
         public string DomicileZipCode { get; set; }
         public string IdentityAddress1 { get; set; }
         public string IdentityCity1 { get; set; }
         public string IdentityZipCode1 { get; set; }
         public string IdentityProvince1 { get; set; }
         public string Propinsi { get; set; }
         public string IdentityCountry1 { get; set; }
         public string IdentityAddress2 { get; set; }
         public string IdentityCity2 { get; set; }
         public string IdentityZipCode2 { get; set; }
         public string IdentityProvince2 { get; set; }
         public string IdentityCountry2 { get; set; }
         public string IdentityAddress3 { get; set; }
         public string IdentityCity3 { get; set; }
         public string IdentityZipCode3 { get; set; }
         public string IdentityProvince3 { get; set; }
         public string IdentityCountry3 { get; set; }
         public string IdentityType1 { get; set; }
         public string IdentityNumber1 { get; set; }
         public string RegistrationDateIdentitasInd1 { get; set; }
         public string ExpiredDateIdentitasInd1 { get; set; }
         public string IdentityType2 { get; set; }
         public string IdentityNumber2 { get; set; }
         public string RegistrationDateIdentitasInd2 { get; set; }
         public string ExpiredDateIdentitasInd2 { get; set; }
         public string IdentityType3 { get; set; }
         public string IdentityNumber3 { get; set; }
         public string RegistrationDateIdentitasInd3 { get; set; }
         public string ExpiredDateIdentitasInd3 { get; set; }
         public string CompanyName { get; set; }
         public string CompanyAddress { get; set; }
         public string CompanyZipCode { get; set; }
         public string CompanyCity { get; set; }
         public string CompanyLegalDomicile { get; set; }
         public string EstablishmentDate { get; set; }
         public string EstablishmentPlace { get; set; }
         public string CountryofEstablishment { get; set; }
         public string SKDNumber { get; set; }
         public string ExpiredDateSKD { get; set; }
         public string ArticleOfAssociation { get; set; }
         public string SIUPNumber { get; set; }
         public string SIUPExpirationDate { get; set; }
         public string AssetFor1Year { get; set; }
         public string AssetFor2Year { get; set; }
         public string AssetFor3Year { get; set; }
         public string OperatingProfitFor1Year { get; set; }
         public string OperatingProfitFor2Year { get; set; }
         public string OperatingProfitFor3Year { get; set; }
         public string CompanyType { get; set; }
         public string CompanyCharacteristic { get; set; }
         public string CompanyIncomePerAnnum { get; set; }
         public string CompanySourceOfFunds { get; set; }
         public string CompanyInvestmentObjective { get; set; }
         public string CountryofCompany { get; set; }
         public string CompanyCityName { get; set; }
         public string Province { get; set; }
         public string FirstNameOfficer1 { get; set; }
         public string MiddleNameOfficer1 { get; set; }
         public string LastNameOfficer1 { get; set; }
         public string PositionOfficer1 { get; set; }
         public string PhoneNumberOfficer1 { get; set; }
         public string EmailOfficer1 { get; set; }
         public string IDType1Officer1 { get; set; }
         public string IDNumber1Officer1 { get; set; }
         public string IDRegDate1Officer1 { get; set; }
         public string IDExpireDate1Officer1 { get; set; }
         public string IDType2Officer1 { get; set; }
         public string IDNumber2Officer1 { get; set; }
         public string IDRegDate2Officer1 { get; set; }
         public string IDExpireDate2Officer1 { get; set; }
         public string IDType3Officer1 { get; set; }
         public string IDNumber3Officer1 { get; set; }
         public string IDRegDate3Officer1 { get; set; }
         public string IDExpireDate3Officer1 { get; set; }
         public string IDType4Officer1 { get; set; }
         public string IDNumber4Officer1 { get; set; }
         public string IDRegDate4Officer1 { get; set; }
         public string IDExpireDate4Officer1 { get; set; }
         public string FirstNameOfficer2 { get; set; }
         public string MiddleNameOfficer2 { get; set; }
         public string LastNameOfficer2 { get; set; }
         public string PositionOfficer2 { get; set; }
         public string IDType1Officer2 { get; set; }
         public string IDNumber1Officer2 { get; set; }
         public string IDRegDate1Officer2 { get; set; }
         public string IDExpireDate1Officer2 { get; set; }
         public string IDType2Officer2 { get; set; }
         public string IDNumber2Officer2 { get; set; }
         public string IDRegDate2Officer2 { get; set; }
         public string IDExpireDate2Officer2 { get; set; }
         public string IDType3Officer2 { get; set; }
         public string IDNumber3Officer2 { get; set; }
         public string IDRegDate3Officer2 { get; set; }
         public string IDExpireDate3Officer2 { get; set; }
         public string IDType4Officer2 { get; set; }
         public string IDNumber4Officer2 { get; set; }
         public string IDRegDate4Officer2 { get; set; }
         public string IDExpireDate4Officer2 { get; set; }
         public string FirstNameOfficer3 { get; set; }
         public string MiddleNameOfficer3 { get; set; }
         public string LastNameOfficer3 { get; set; }
         public string PositionOfficer3 { get; set; }
         public string IDType1Officer3 { get; set; }
         public string IDNumber1Officer3 { get; set; }
         public string IDRegDate1Officer3 { get; set; }
         public string IDExpireDate1Officer3 { get; set; }
         public string IDType2Officer3 { get; set; }
         public string IDNumber2Officer3 { get; set; }
         public string IDRegDate2Officer3 { get; set; }
         public string IDExpireDate2Officer3 { get; set; }
         public string IDType3Officer3 { get; set; }
         public string IDNumber3Officer3 { get; set; }
         public string IDRegDate3Officer3 { get; set; }
         public string IDExpireDate3Officer3 { get; set; }
         public string IDType4Officer3 { get; set; }
         public string IDNumber4Officer3 { get; set; }
         public string IDRegDate4Officer3 { get; set; }
         public string IDExpireDate4Officer3 { get; set; }
         public string FirstNameOfficer4 { get; set; }
         public string MiddleNameOfficer4 { get; set; }
         public string LastNameOfficer4 { get; set; }
         public string PositionOfficer4 { get; set; }
         public string IDType1Officer4 { get; set; }
         public string IDNumber1Officer4 { get; set; }
         public string IDRegDate1Officer4 { get; set; }
         public string IDExpireDate1Officer4 { get; set; }
         public string IDType2Officer4 { get; set; }
         public string IDNumber2Officer4 { get; set; }
         public string IDRegDate2Officer4 { get; set; }
         public string IDExpireDate2Officer4 { get; set; }
         public string IDType3Officer4 { get; set; }
         public string IDNumber3Officer4 { get; set; }
         public string IDRegDate3Officer4 { get; set; }
         public string IDExpireDate3Officer4 { get; set; }
         public string IDType4Officer4 { get; set; }
         public string IDNumber4Officer4 { get; set; }
         public string IDRegDate4Officer4 { get; set; }
         public string IDExpireDate4Officer4 { get; set; }
         public string Description { get; set; }

         public string AlamatKantorInd { get; set; }
         public string KodeKotaKantorInd { get; set; }
         public string KodePosKantorInd { get; set; }
         public string KodePropinsiKantorInd { get; set; }
         public string KodeCountryofKantor { get; set; }
         public string CorrespondenceRT { get; set; }
         public string CorrespondenceRW { get; set; }
         public string DomicileRT { get; set; }
         public string DomicileRW { get; set; }
         public string Identity1RT { get; set; }
         public string Identity1RW { get; set; }
         public string KodeDomisiliPropinsi { get; set; }
         public string AlamatInd1 { get; set; }
         public decimal Unit { get; set; }
         public string EntryUsersID { get; set; }
         public string EntryTime { get; set; }
         public string UpdateUsersID { get; set; }
         public string UpdateTime { get; set; }
         public string ApprovedUsersID { get; set; }
         public string ApprovedTime { get; set; }
         public string VoidUsersID { get; set; }
         public string VoidTime { get; set; }
         public string SuspendBy { get; set; }
         public string SuspendTime { get; set; }
         public string UnSuspendBy { get; set; }
         public string UnSuspendTime { get; set; }
     }

     public class ARIARpt
     {
         public string ID { get; set; }
         public string Name { get; set; }
         //INDIVIDUAL
         public string NamaDepanInd { get; set; }
         public string NamaTengahInd { get; set; }
         public string NamaBelakangInd { get; set; }
         public string TempatLahir { get; set; }
         public string TanggalLahir { get; set; }
         public string JenisKelaminDesc { get; set; }
         public string StatusPerkawinanDesc { get; set; }
         public string PekerjaanDesc { get; set; }
         public string PendidikanDesc { get; set; }
         public string AgamaDesc { get; set; }
         public string PenghasilanIndDesc { get; set; }
         public string SumberDanaIndDesc { get; set; }
         public string MaksudTujuanIndDesc { get; set; }
         public string AlamatInd1 { get; set; }
         public string KodeKotaInd1Desc { get; set; }
         public int KodePosInd1 { get; set; }
         public string AlamatInd2 { get; set; }
         public string KodeKotaInd2Desc { get; set; }
         public int KodePosInd2 { get; set; }
         //INSTITUTION
         public string NamaPerusahaan { get; set; }
         public string DomisiliDesc { get; set; }
         public string TipeDesc { get; set; }
         public string KarakteristikDesc { get; set; }
         public string NoSKD { get; set; }
         public string PenghasilanInstitusiDesc { get; set; }
         public string SumberDanaInstitusiDesc { get; set; }
         public string MaksudTujuanInstitusiDesc { get; set; }
         public string AlamatPerusahaan { get; set; }
         public string KodeKotaInsDesc { get; set; }
         public int KodePosIns { get; set; }
         public string CheckedBy { get; set; }
         public string ApprovedBy { get; set; }
     }

     public class MasterDataRpt
     {
         public string Title { get; set; }
         public string ID { get; set; }
         public string Name { get; set; }
     }

     public class AuditAccountingRpt
     {
         public string Profile { get; set; }
         public string Groups { get; set; }
         public bool PageBreak { get; set; }
         public bool ShowNullData { get; set; }
         public int Status { get; set; }
         public string ReportName { get; set; }
         public DateTime ValueDateFrom { get; set; }
         public DateTime ValueDateTo { get; set; }
         public string ReferenceFrom { get; set; }
         public string ReferenceTo { get; set; }
         public string AccountFrom { get; set; }
         public string AccountTo { get; set; }
         public string OfficeFrom { get; set; }
         public string OfficeTo { get; set; }
         public string DepartmentFrom { get; set; }
         public string DepartmentTo { get; set; }
         public string AgentFrom { get; set; }
         public string AgentTo { get; set; }
         public string ConsigneeFrom { get; set; }
         public string ConsigneeTo { get; set; }
         public string InstrumentFrom { get; set; }
         public string InstrumentTo { get; set; }
         public int ParamData { get; set; }
         public string CurrencyFrom { get; set; }
         public string CurrencyTo { get; set; }
     }

     public class OSKMonthlyMappingReport
     {

         public string ID { get; set; }
         public string Row { get; set; }

     }

     public class OSKMonthlyReportDetail
     {
         public int AccountPK { get; set; }
         public int Row { get; set; }
         public string ID { get; set; }
         public string Name { get; set; }
         public decimal PreviousBaseBalance { get; set; }
         public decimal BaseDebitMutasi { get; set; }
         public decimal BaseCreditMutasi { get; set; }
         public decimal CurrentBaseBalance { get; set; }

     }

     public class AccountPerMonth
     {
         public int Code { get; set; }
         public string Month { get; set; }
         public string AccountID { get; set; }
         public string AccountName { get; set; }
         public string InstrumentID { get; set; }
         public string AgentID { get; set; }
         public string CostCenter { get; set; }
         public decimal Total { get; set; }

     }

     public class AUM4Pillar
     {
         public int Code { get; set; }
         public string Month { get; set; }
         public string InstrumentID { get; set; }
         public string DepartmentID { get; set; }
         public decimal ManagementFee { get; set; }
         public decimal SubsFee { get; set; }
         public decimal SellingAgentFee { get; set; }
         public decimal CommissionFee { get; set; }

     }

    public class UnitRegistryRpt
    {
        public string SourceOfFundDesc { get; set; }
        public string ClientCategory { get; set; }
        public string InvestorType { get; set; }
        public string CompanyID { get; set; }
        public string BeneficiaryBank { get; set; }
        public DateTime PaymentDate { get; set; }
        public int UnitRegistryPK { get; set; }
        public int PKFrom { get; set; }
        public int BegDate { get; set; }
        public int PKTo { get; set; }
        public string DownloadMode { get; set; }
        public bool PageBreak { get; set; }
        public bool BitIsAgent { get; set; }
        public bool BitIsAgentLevelOne { get; set; }
        public int Status { get; set; }
        public string ReportName { get; set; }
        public string Type { get; set; }
        public int ClientSubscriptionPK { get; set; }
        public int ClientRedemptionPK { get; set; }
        public int ClientSwitchingPK { get; set; }
        public string ValueDateFrom { get; set; }
        public string ValueDateTo { get; set; }
        public string FundFrom { get; set; }
        public string FundTo { get; set; }
        public string FundClientFrom { get; set; }
        public string FundClientTo { get; set; }
        public string AgentFrom { get; set; }
        public string AgentTo { get; set; }
        public string AgentLevelOneFrom { get; set; }
        public string DepartmentFrom { get; set; }
        public string DepartmentTo { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string Sales { get; set; }
        public int FundClientPK { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Nav { get; set; }
        public DateTime NAVDate { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public decimal CashBalance { get; set; }
        public decimal UnitBalance { get; set; }
        public decimal SumUnitBalance { get; set; }
        public decimal EndBalanceUSD { get; set; }
        public decimal FeePercent { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Message { get; set; }
        public string ContactPerson { get; set; }
        public string FaxNo { get; set; }
        public string BankCustodiID { get; set; }
        public string Remark { get; set; }
        public int SrHolder { get; set; }
        public string RekNo { get; set; }
        public string AgentName { get; set; }
        public string DepartmentName { get; set; }
        public decimal AgentFeeAmount { get; set; }
        public string CurrencyID { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public string Signature { get; set; }
        public string Description { get; set; }
        public decimal SUBUnit { get; set; }
        public decimal SUBAmount { get; set; }
        public decimal REDUnit { get; set; }
        public decimal REDAmount { get; set; }
        public DateTime AutoDebitDate { get; set; }
        public decimal EndBalance { get; set; }
        public string CIF { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal SalesUnit { get; set; }
        public decimal SwitchingAmount { get; set; }
        public decimal SwitchingUnit { get; set; }
        public decimal RedemptionAmount { get; set; }
        public decimal RedemptionUnit { get; set; }
        public decimal Charge { get; set; }
        public decimal Commission { get; set; }
        public decimal ManagementFeeAmount { get; set; }
        public decimal TrailFeeAmount { get; set; }
        public decimal OtherFee { get; set; }
        public string NPWP { get; set; }
        public decimal AvgNav { get; set; }
        public decimal CloseNav { get; set; }
        public decimal FundValue { get; set; }
        public decimal MarketValue { get; set; }
        public decimal Unrealized { get; set; }
        public decimal UnrealizedPercent { get; set; }
        public string Agent { get; set; }
        public string Mgt { get; set; }
        public string FundByAll { get; set; }
        public string SID { get; set; }
        public string FundIDFrom { get; set; }
        public string FundIDTo { get; set; }
        public string FundNameFrom { get; set; }
        public string FundNameTo { get; set; }
        public decimal NAVFundFrom { get; set; }
        public decimal NAVFundTo { get; set; }
        public decimal TotalUnitFundFrom { get; set; }
        public decimal CashFundFrom { get; set; }
        public decimal TotalCashFundFrom { get; set; }
        public decimal TotalCashFundTo { get; set; }
        public string CashRefDesc { get; set; }
        public string BitSwitchingAllDesc { get; set; }
        public string NegaraDesc { get; set; }
        public string SegmentClassName { get; set; }
        public string ParentSegmentClass { get; set; }
        public string InternalCategoryName { get; set; }
        public string ParentInternalCategory { get; set; }
        public int SellingAgentPK { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal UnRealized { get; set; }
        public decimal NAV { get; set; }
        public decimal AvgNAV { get; set; }
        public decimal BegBalance { get; set; }

        public string FundToID { get; set; }
        public string FundToName { get; set; }
        public decimal NAVFrom { get; set; }
        public decimal NAVTo { get; set; }
        public decimal TotalCashAmountFundFrom { get; set; }
        public decimal TotalCashAmountFundTo { get; set; }
        public decimal TotalUnitAmountFundFrom { get; set; }
        public decimal TotalUnitAmountFundTo { get; set; }
        public string FeeType { get; set; }
        public string InvestorName { get; set; }
        public decimal UnitTransaction { get; set; }
        public string CCY { get; set; }
        public decimal GrossAmount { get; set; }
        public string ManagerApproval { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string IFUA { get; set; }
        public string CustodyAddress { get; set; }
        public string SellingAgentID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Subscription { get; set; }
        public decimal Redemption { get; set; }
        public decimal Netting { get; set; }
        public string Switching { get; set; }
        public string FundType { get; set; }
        public string SellingAgentName { get; set; }
        public string GroupBy { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InternalCategory { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal TransactionUnit { get; set; }
        public string TransactionType { get; set; }

        public string TransactionCurrency { get; set; }
        public string Source { get; set; }
        public string Partial { get; set; }
        public string Flag { get; set; }
        public string SACode { get; set; }
        public decimal BankAccountNo { get; set; }


        public string AccountNameDebet { get; set; }
        public string AccountNumberDebet { get; set; }
        public string BankDebet { get; set; }
        public string AccountNameKredit { get; set; }
        public string AccountNumberKredit { get; set; }
        public string BankKredit { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }


        public int Signature1 { get; set; }
        public int Signature2 { get; set; }
        public int Signature3 { get; set; }
        public int Signature4 { get; set; }
        public string NoSurat { get; set; }
        public decimal CurrentNAV { get; set; }

        public string Nationality { get; set; }
        public string HighRiskStatus { get; set; }
        public decimal Unit { get; set; }
        public string RiskProfileScore { get; set; }
        public string AnnualIncome { get; set; }
        public string UnitHolderNo { get; set; }
        public string InvestmentObject { get; set; }
        public string MotherMaidenName { get; set; }
        public string Occupation { get; set; }
        public string Score { get; set; }
        public string ScoreDesc { get; set; }
        public string SIDName { get; set; }
        public string Address { get; set; }
        public string Account { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal MovementBalance { get; set; }
        public decimal Ending { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal Realized { get; set; }

        public string ProdID { get; set; }
        public string ProdCCY { get; set; }
        public decimal AumMMI { get; set; }
        public decimal UnitMMI { get; set; }
        public decimal NavProduct { get; set; }
        public decimal NavAmount { get; set; }
        public decimal UnitProduct { get; set; }
        public decimal PercentProduct { get; set; }
        public string TopUp { get; set; }
        public bool ZeroBalance { get; set; }
        public decimal Amount { get; set; }
        public string KantorCabang { get; set; }
        public decimal NilaiPenjualan { get; set; }
        public decimal USD { get; set; }
        public string FundTypeFrom { get; set; }

        public string CompanyTypeFrom { get; set; }
        public decimal AUM { get; set; }
        public decimal OutstandingUnits { get; set; }
        public string SettlementDateForTrx { get; set; }
        public string NAVDateForTrx { get; set; }
        public decimal AUMFrom { get; set; }
        public decimal AUMTo { get; set; }
        public string FundText { get; set; }
        public string FundClientSIDFrom { get; set; }
        public int ClientTypeRHB { get; set; }
        public string FundClientInternalCategoryFrom { get; set; }
        public string Marketing { get; set; }
        public string Branch { get; set; }

        public int RoundingNav { get; set; }
        public int WithAdjustment { get; set; }
        public int ClientType { get; set; }
    }

    public class MonthlyTransactionbyFundandInvestorTypeRpt
     {
         public string FundID { get; set; }
         public Decimal Subscription { get; set; }
         public Decimal Redemption { get; set; }
         public Decimal RetailSubscription { get; set; }
         public Decimal RetailRedemption { get; set; }
         public Decimal InstiSubscription { get; set; }
         public Decimal InstiRedemption { get; set; }

     }
     public class FundJournalVoucher
     {
         public DateTime ValueDate { get; set; }
         public int RefNo { get; set; }
         public string Reference { get; set; }
         public string TrxName { get; set; }
         public string Description { get; set; }
         public string FundJournalAccountID { get; set; }
         public string FundJournalAccountName { get; set; }
         public string CurrencyID { get; set; }
         public string DebitCredit { get; set; }
         public decimal Amount { get; set; }
         public decimal Debit { get; set; }
         public decimal Credit { get; set; }
         public decimal Rate { get; set; }
         public decimal BaseDebit { get; set; }
         public decimal BaseCredit { get; set; }
         public string FundID { get; set; }
         public string FundName { get; set; }
         public string FundClientID { get; set; }
         public string FundClientName { get; set; }
         public string InstrumentID { get; set; }
         public string InstrumentName { get; set; }
         public string CheckedBy { get; set; }
         public string ApprovedBy { get; set; }

     }

     public class FundAccountActivity
     {
         public decimal StartBalance { get; set; }
         public int FundJournalPK { get; set; }
         public DateTime ValueDate { get; set; }
         public string Reference { get; set; }
         public int RefNo { get; set; }
         public string FundJournalAccountID { get; set; }
         public string FundJournalAccountName { get; set; }
         public int FundJournalAccountType { get; set; }
         public string DetailDescription { get; set; }
         public string DebitCredit { get; set; }
         public decimal Amount { get; set; }
         public decimal Debit { get; set; }
         public decimal Credit { get; set; }
         public decimal Rate { get; set; }
         public decimal BaseDebit { get; set; }
         public decimal BaseCredit { get; set; }
         public string CurrencyID { get; set; }
         public string FundClientID { get; set; }
         public string FundID { get; set; }
         public string FundName { get; set; }
         public string InstrumentID { get; set; }
         public string CheckedBy { get; set; }
         public string ApprovedBy { get; set; }

     }

     public class FundPortfolio
     {
         public DateTime Date { get; set; }
         public int InstrumentTypePK { get; set; }
         public string InstrumentTypeName { get; set; }
         public string FundID { get; set; }
         public string FundName { get; set; }
         public string InstrumentID { get; set; }
         public string InstrumentName { get; set; }
         public DateTime MaturityDate { get; set; }
         public decimal Balance { get; set; }
         public decimal CostValue { get; set; }
         public decimal AvgPrice { get; set; }
         public decimal ClosePrice { get; set; }
         public decimal MarketValue { get; set; }
         public decimal Unrealised { get; set; }
         public decimal PercentOfNav { get; set; }

         public decimal InterestPercent { get; set; }
         public string PeriodeActual { get; set; }
         public decimal AccrualHarian { get; set; }
         public decimal Accrual { get; set; }
         public string CheckedBy { get; set; }
         public string ApprovedBy { get; set; }
         public string CompanyAccountTradingName { get; set; }
         

     }

     public class FundExposureRpt
     {
         public decimal AUMPrevDay { get; set; }
         public string FundID { get; set; }
         public string ExposureType { get; set; }
         public string ExposureID { get; set; }
         public decimal ExposurePercent { get; set; }
         public decimal MinExposurePercent { get; set; }
         public decimal MaxExposurePercent { get; set; }
         public decimal WarningMinExposurePercent { get; set; }
         public decimal WarningMaxExposurePercent { get; set; }
         public bool AlertMinExposure { get; set; }
         public bool AlertMaxExposure { get; set; }
         public bool WarningMinExposure { get; set; }
         public bool WarningMaxExposure { get; set; }
     }

     public class NavReportListing
     {
         public int FundPK { get; set; }
         public string Date { get; set; }
         public int Row { get; set; }
         public string CurrencyID { get; set; }
         public string Description { get; set; }
         public decimal Amount { get; set; }
         public decimal AmountBefore { get; set; }

     }

     public class FundTrialBalance
     {
         public string DateFrom { get; set; }
         public int No { get; set; }
         public string DateTo { get; set; }
         public string ID { get; set; }
         public string Name { get; set; }
         public decimal PreviousBaseBalance { get; set; }
         public decimal BaseDebitMutasi { get; set; }
         public decimal BaseCreditMutasi { get; set; }
         public decimal CurrentBaseBalance { get; set; }
         public string CurrencyID { get; set; }
         public int Groups { get; set; }
         public int ParentPK { get; set; }
         public string FundID { get; set; }
         public string FundName { get; set; }
         public string InstrumentID { get; set; }
         public string DepartmentID { get; set; }
         public string OfficeID { get; set; }
         public string AgentID { get; set; }


     }

     public class SInvestRpt
     {
         public int FundFrom { get; set; }
         public string Fund { get; set; }
         public string Date { get; set; }
         public string ReportName { get; set; }
         public string ValueDate { get; set; }
         public string ParamDate { get; set; }
         public string ParamUnitRegistry { get; set; }
         public string ParamCategory { get; set; }
        public string ParamInvestorType { get; set; }
        public string DownloadMode { get; set; }
     }

    public class CommissionRpt
    {
        public string DownloadMode { get; set; }
        public bool PageBreak { get; set; }
        public int Status { get; set; }
        public int BegDate { get; set; }
        public int WithTax { get; set; }
        public string ReportName { get; set; }
        public string ValueDateFrom { get; set; }
        public string ValueDateTo { get; set; }
        public string FundFrom { get; set; }
        public string FundFromByAll { get; set; }
        public string FundClientFrom { get; set; }
        public string FundClientFromByAll { get; set; }
        public string AgentFrom { get; set; }
        public string AgentFeeType { get; set; }
        public string FundFeeType { get; set; }
        public string AgentFromByAll { get; set; }
        public string DepartmentFrom { get; set; }
        public string DepartmentFromByAll { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string FundID { get; set; }
        public decimal Nav { get; set; }
        public DateTime NAVDate { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public decimal SubsInUnit { get; set; }
        public decimal SubsInAmount { get; set; }
        public decimal RedempInUnit { get; set; }
        public decimal RedempInAmount { get; set; }
        public decimal Unit { get; set; }
        public decimal AUM { get; set; }
        public decimal ManagementFeeAmount { get; set; }
        public decimal TrailFeeAmount { get; set; }
        public string FundName { get; set; }
        public decimal TotalManagementFee { get; set; }
        public decimal TotalSharingFee { get; set; }
        public bool AgentOnly { get; set; }
        public string ClientStatementType { get; set; }
        public string CurrencyType { get; set; }
        public string FundType { get; set; }
        public DateTime ParamDate { get; set; }

        public string PeriodFrom { get; set; }
    }

    public class ClientTrackingRpt
    {
        public string ClientCategory { get; set; }
        public string InvestorType { get; set; }
        public string DownloadMode { get; set; }
        public string ReportName { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public int FundClientPK { get; set; }
        public string FundClientID { get; set; }
        public string ClientName { get; set; }
        public string AgentName { get; set; }
        public string CurrencyID { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal SalesUnit { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string CIF { get; set; }
        public decimal Charge { get; set; }
        public decimal Commission { get; set; }
        public decimal UnitBalance { get; set; }
        public decimal CashBalance { get; set; }
        public decimal SwitchingAmount { get; set; }
        public decimal SwitchingUnit { get; set; }
        public decimal RedemptionAmount { get; set; }
        public decimal RedemptionUnit { get; set; }
        public decimal ManagementFeeAmount { get; set; }
        public decimal TrailFeeAmount { get; set; }
        public string SharingFeeType { get; set; }
        public decimal OtherFee { get; set; }
        public decimal LastNAV { get; set; }
        public decimal OldManagementFee { get; set; }
        public decimal AUM { get; set; }
        public string SID { get; set; }
        public string SIDName { get; set; }
        public string FundType { get; set; }
        public string MFeeType { get; set; }
        public decimal SwitchingInAmount { get; set; }
        public decimal SwitchingInUnit { get; set; }
        public decimal SwitchingOutAmount { get; set; }
        public decimal SwitchingOutUnit { get; set; }
        public decimal AUMDateFrom { get; set; }
        public string Level1Name { get; set; }
        public string Level2Name { get; set; }
        public string Level3Name { get; set; }
        public string Level4Name { get; set; }
        public string Agent { get; set; }


    }

    public class ClientStatementReps
     {
         public int UnitRegistryPK { get; set; }
         public int PKFrom { get; set; }
         public int PKTo { get; set; }
         public string DownloadMode { get; set; }
         public bool PageBreak { get; set; }
         public int Status { get; set; }
         public string ReportName { get; set; }
         public string Type { get; set; }
         public int ClientSubscriptionPK { get; set; }
         public int ClientRedemptionPK { get; set; }
         public string ValueDateFrom { get; set; }
         public string ValueDateTo { get; set; }
         public string FundFrom { get; set; }
         public string FundTo { get; set; }
         public string FundClientFrom { get; set; }
         public string FundClientTo { get; set; }
         public string AgentFrom { get; set; }
         public string AgentTo { get; set; }
         public string DepartmentFrom { get; set; }
         public string DepartmentTo { get; set; }
         public int FundPK { get; set; }
         public string FundID { get; set; }
         public string FundName { get; set; }
         public int FundClientPK { get; set; }
         public string ClientID { get; set; }
         public string ClientName { get; set; }
         public DateTime Date { get; set; }
         public DateTime DateFrom { get; set; }
         public DateTime DateTo { get; set; }
         public decimal Nav { get; set; }
         public DateTime NAVDate { get; set; }
         public DateTime ValueDate { get; set; }
         public DateTime SettlementDate { get; set; }
         public string VarMonth { get; set; }
         public int IntMonth { get; set; }
         public string SubsValueDate { get; set; }
         public decimal SubsUnits { get; set; }
         public decimal SubsNAV { get; set; }
         public decimal SubsAmount { get; set; }
         public string RedValueDate { get; set; }
         public decimal RedUnits { get; set; }
         public decimal RedNAV { get; set; }
         public decimal RedAmount { get; set; }
         public decimal PosUnits { get; set; }
         public decimal PosNAV { get; set; }
         public decimal PosAmount { get; set; }
         public decimal MonthlyReturn { get; set; }
         public decimal Benchmark { get; set; }
         public decimal BenchmarkPercent { get; set; }
         public decimal InvestmentIncome { get; set; } 
     }

     public class ClientSubscriptionRept_SAM
     { 
        public string Type { get; set; }
        public int ClientSubscriptionPK { get; set; }
        public string ContactPerson { get; set; }
        public string BankBranchID  { get; set; }  
        public string FaxNo{ get; set; }
        public DateTime Date { get; set; }
        public DateTime NAVDate { get; set; }
        public string FundName { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public decimal NAVAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal FeePercent { get; set; }
        public decimal FeeAmount { get; set; }
        public string Remark { get; set; }
        public decimal NetAmount { get; set; }
        public string NPWP { get; set; }
        public string Agent { get; set; }
        public string Mgt { get; set; }
        public decimal SubsAmount { get; set; }
        public decimal RedAmount { get; set; }
        public string CompanyID { get; set; }

     }

     public class RekapManagementFeeIndividuInstitusi
     {
         public string FundID { get; set; }
         public decimal NAV { get; set; }
         public string ClientName { get; set; }
         public string Referal { get; set; }
         public string Cabang { get; set; }
         public decimal UnitAmount { get; set; }
         public decimal AUM { get; set; }
         public decimal ManagementFee { get; set; }
         public string InvestorType { get; set; }
     }

     public class RekapManagementFeeMarketing
     {
         public string FundID { get; set; }
         public string Referal { get; set; }
         public string Cabang { get; set; }
         public decimal TotalUnit { get; set; }
         public decimal TotalAUM { get; set; }
         public decimal TotalManagementFee { get; set; }
     }

     public class RekapManagementFeeFund
     {
         public string FundID { get; set; }
         public string Type { get; set; }
         public int TotalNasabah { get; set; }
         public decimal TotalUnit { get; set; }
         public decimal TotalAUM { get; set; }
         public decimal TotalManagementFee { get; set; }
     }

     public class RekapTransactionFee
     {
         public string ClientName { get; set; }
         public string FundID { get; set; }
         public string Description { get; set; }
         public DateTime ValueDate { get; set; }
         public string Cabang { get; set; }
         public string Referral { get; set; }
         public decimal CashAmount { get; set; }
         public decimal FeePercent { get; set; }
         public decimal FeeAmount { get; set; }
         public decimal NetFee { get; set; }
     }

     public class SubscriptionRpt
     {
         public int SubscriptionPK { get; set; }
         public string TransactionDate { get; set; }
         public int ID { get; set; }
         public string InvestorName { get; set; }
         public decimal GrossAmount { get; set; }
         public decimal SubsFeePercentage { get; set; }
         public decimal SubsFee { get; set; }
         public decimal NettAmount { get; set; }
         public decimal NAV { get; set; }
         public decimal Units { get; set; }
         public string Fund { get; set; }
         public string CashRefDesc { get; set; }
         public string AgentName { get; set; }
     }

     public class RedemptionRpt
     {
         public int RedemptionPK { get; set; }
         public string TransactionDate { get; set; }
         public int ID { get; set; }
         public string InvestorName { get; set; }
         public decimal AmountRedeem { get; set; }
         public decimal UnitRedeem { get; set; }
         public decimal SubsFeePercentage { get; set; }
         public decimal RedemptionFeePercent { get; set; }
         public decimal SubsFee { get; set; }
         public decimal RedemptionFeeAmount { get; set; }
         public decimal NettAmount { get; set; }
         public decimal NAV { get; set; }
         public decimal Units { get; set; }
         public string PaymentDate { get; set; }
         public string Fund { get; set; }
         public string BeneficiaryBank { get; set; }
         public bool BitRedemptionAll { get; set; }
         public string BitRedemptionAllDesc { get; set; }
         public string AgentName { get; set; }
     }

     public class Portofolio
     {
         public string NamaReksaDana { get; set; }
         public decimal Saldo { get; set; }
         public decimal Unit { get; set; }
         public decimal HargaPasar { get; set; }
         public decimal UnitRataRata { get; set; }
         public decimal NilaiRataRata { get; set; }
         public decimal LabaRugi { get; set; }
         public string SID { get; set; }
         public string Name { get; set; }
         public string Address { get; set; }
         public string AlamatInd1 { get; set; }
     }

     public class ReksaDana
     {
         public int ReksaDanaPK { get; set; }
         public string NamaReksaDana { get; set; }
         public string Deskripsi { get; set; }
         public string Type { get; set; }
         public string Fund { get; set; }
         public int FundPK { get; set; }
         public int FundClientPK { get; set; }
         public string TransactionDate { get; set; }
         public decimal TotalCashAmount { get; set; }
         public decimal SubscriptionFeeAmount { get; set; }
         public decimal RedemptionFeeAmount { get; set; }
         public decimal FeeAmount { get; set; }
         public decimal NAV { get; set; }
         public decimal TotalUnitAmount { get; set; }
         public decimal HargaNetto { get; set; }
         public decimal BiayaTransaksi { get; set; }
         public decimal NAB { get; set; }
         public decimal Unit { get; set; }
         public decimal Balance { get; set; }
         public string SID { get; set; }
         public string Name { get; set; }
         public string AlamatInd1 { get; set; }
         public string Address { get; set; }
     }


    public class RevenuePerSalesRpt
    {

        public string ClientName { get; set; }
        public string SID { get; set; }
        public string IFUACode { get; set; }
        public string SalesName { get; set; }
        public string Department { get; set; }
        public string FundID { get; set; }
        public string FundType { get; set; }
        public decimal AUM { get; set; }
        public decimal Fee { get; set; }
        public decimal Revenue { get; set; }
        public string AgentID { get; set; }
        public string AgentParent { get; set; }
        public decimal NetTransaction { get; set; }
        public decimal Subs { get; set; }
        public decimal Redemp { get; set; }
        public decimal SwitchIn { get; set; }
        public decimal SwitchOut { get; set; }

    }



    public class RekapUnitPenyertaan
     {

         public string Fund { get; set; }
         public int FundPK { get; set; }
         public int FundClientPK { get; set; }
         public decimal Unit { get; set; }
         public string SID { get; set; }
         public string IFUACode { get; set; }
         public string InvestorName { get; set; }
         public decimal UnitAmount { get; set; }

     }

     public class OjkRpt
     {
         public DateTime Date { get; set; }
         public DateTime ValueDateFrom { get; set; }
         public DateTime ValueDateTo { get; set; }
         public string FundClientFrom { get; set; }
         public string ID { get; set; }
         public string ClientName { get; set; }
         public string CountryRisk { get; set; }
         public string CountryRiskDesc { get; set; }
         public string OccupationRisk { get; set; }
         public string OccupationRiskDesc { get; set; }
         public string PoliticallyRisk { get; set; }
         public string PoliticallyRiskDesc { get; set; }
         public string BusinessRisk { get; set; }
         public string BusinessRiskDesc { get; set; }
         public string ClientNameRisk { get; set; }
         public string ClientNameRiskDesc { get; set; }
         public string SpouseNameRisk { get; set; }
         public string SpouseNameRiskDesc { get; set; }
         public string RdnNameRisk { get; set; }
         public string RdnNameRiskDesc { get; set; }
         public string ReportName { get; set; }
         public string DownloadMode { get; set; }
         //public string ValueDateFrom { get; set; }
         public bool PageBreak { get; set; }

         public string FundFrom { get; set; }
         public string MKBDCode { get; set; }
         public string Address { get; set; }
         public string Name { get; set; }
         public string DirectorOne { get; set; }
         public string JenisPeriode { get; set; }
         public string JabatanDireksi { get; set; }
         public string KodeEmiten { get; set; }
         public string NamaEmiten { get; set; }
         public string SektorEkonomi { get; set; }
         public string Kategori { get; set; }
         public decimal SaldoSAK { get; set; }
         public decimal AYD { get; set; }
         public decimal SaldoSAKLancar { get; set; }
         public string Keterangan { get; set; }
         public string SeriObligasi { get; set; }
         public string Peringkat { get; set; }
         public string Klaster { get; set; }
         public decimal SelisihSAKdanSAP { get; set; }
         public string NamaSuratBerharga { get; set; }
         public string KategoriSuratBerharga { get; set; }
         public string NamaReksadana { get; set; }
         public string JenisReksadana { get; set; }
         public string ManagerInvestasi { get; set; }
         public decimal KasDiterima { get; set; }
         public decimal Piutang { get; set; }
         public decimal Unrealized { get; set; }
         public int PeriodPK { get; set; }
         public string ParamCategory { get; set; }
         public string Fund { get; set; }
         public string ParamAPERD { get; set; }
         public string FundCombo { get; set; }
         public string ParamInvestorType { get; set; }

     }

     public class LaporanRekapTotalNAB
     {

         public string Fund { get; set; }
         public int FundPK { get; set; }
         public int FundClientPK { get; set; }
         public decimal Unit { get; set; }
         public string SID { get; set; }
         public string IFUACode { get; set; }
         public string InvestorName { get; set; }
         public decimal UnitAmount { get; set; }
         public decimal NABPerUnit { get; set; }
         public decimal TotalNAB { get; set; }

     }

     public class BrokerFeeRpt
     {
         public int Year { get; set; }
         public int Quarter { get; set; }
         public string CounterpartName { get; set; }
         public decimal Percentage { get; set; }

     }

     public class AUMandRevenuePerformance
     {

         public string TypeFund { get; set; }
         public decimal RetailAUM { get; set; }
         public decimal RetailNetTransaction { get; set; }
         public decimal RetailNetTransactionYTD { get; set; }
         public decimal RetailMIFee { get; set; }
         public decimal RetailRebate { get; set; }
         public decimal RetailNetFee { get; set; }
         public decimal RetailMgtFeeTYD { get; set; }
         public decimal RetailNetFeeTYD { get; set; }
         public decimal InsAUM { get; set; }
         public decimal InsNetTransaction { get; set; }
         public decimal InsNetTransactionYTD { get; set; }
         public decimal InsMIFee { get; set; }
         public decimal InsRebate { get; set; }
         public decimal InsNetFee { get; set; }
         public decimal InsMgtFeeTYD { get; set; }
         public decimal InsNetFeeTYD { get; set; }


     }

     public class RevenueFundType
     {

         public string TypeFund { get; set; }
         public decimal AUM { get; set; }
         public decimal NetTransactionMTD { get; set; }
         public decimal NetTransactionYTD { get; set; }
         public decimal Revenue { get; set; }
         public decimal RevenueYTD { get; set; }



     }

     public class CounterpartTransaction
     {
         public decimal CommissionPercent { get; set; }
         public decimal CommissionAmount { get; set; }
         public string CounterpartID { get; set; }
         public string CounterpartName { get; set; }
         public decimal TotalYTD { get; set; }
         public decimal TargetAllocation { get; set; }
         public decimal Total { get; set; }
         public decimal percent { get; set; }
         public decimal Selisih { get; set; }
     }


     public class PerhitunganFeeMIdanFeeBK
     {
         public int FundPK { get; set; }
         public string FundName { get; set; }
         public decimal AUM { get; set; }
         public decimal NAV { get; set; }
         public string Date { get; set; }
         public decimal TotalDanaKelolaan { get; set; }

     } // MNC


     public class TransactionSummary //MNC
     {

         public string CIF { get; set; }
         public string Broker { get; set; }
         public decimal UnitBalance { get; set; }
         public decimal CashBalance { get; set; }
         public decimal ValueAmount { get; set; }
         public decimal CostPortofolio { get; set; }
         public decimal Lot { get; set; }
         public decimal Profit { get; set; }
         public decimal MarketValue { get; set; }
         public decimal PriceAvg { get; set; }
         public string ForReksaDana { get; set; }
         public string StockCode { get; set; }
         public string BuySell { get; set; }
         public DateTime TransactionDate { get; set; }
         public DateTime SettlementDate { get; set; }
         public decimal TOTALYTD { get; set; }
         public decimal TaegetAllocation { get; set; }
         public decimal SELISIH { get; set; }

     }

     public class DailyTransactions
     {
         public string EndDayTrails { get; set; }
         public string FundPosition { get; set; }
         public string NAV { get; set; }
         public string ClosePrice { get; set; }
         public string ClientSubscription { get; set; }
         public string ClientRedemption { get; set; }
         public string ClientSwitching { get; set; }
         public string OMSTD { get; set; }
         public string OMSEquity { get; set; }
         public string OMSBond { get; set; }
         public string Dealing { get; set; }
         public string Settlement { get; set; }
         public string LastEndDayTrails { get; set; }
         public string LastEndDayFundPosition { get; set; }
         public string LastCloseNAV { get; set; }
         public string LastClosePrice { get; set; }
     }

     public class FundFactSheetRpt
     {
         public string ValueDate { get; set; }
         public string PeriodPK { get; set; }
         public string FundPK { get; set; }
         public string ClosePrice { get; set; }
         public string ClientSubscription { get; set; }
         public string ClientRedemption { get; set; }
         public string ClientSwitching { get; set; }
         public string OMSTD { get; set; }
         public string OMSEquity { get; set; }
         public string OMSBond { get; set; }
         public string Dealing { get; set; }
         public string Settlement { get; set; }
         public string LastEndDayTrails { get; set; }
         public string LastEndDayFundPosition { get; set; }
         public string LastCloseNAV { get; set; }
         public string LastClosePrice { get; set; }
     }

     public class AccountActivityByGroups
     {
         public decimal StartBalance { get; set; }
         public int journalPK { get; set; }
         public DateTime ValueDate { get; set; }
         public string Reference { get; set; }
         public int RefNo { get; set; }
         public string AccountID { get; set; }
         public string AccountName { get; set; }
         public int AccountType { get; set; }
         public string DetailDescription { get; set; }
         public string DebitCredit { get; set; }
         public decimal Amount { get; set; }
         public decimal Debit { get; set; }
         public decimal Credit { get; set; }
         public decimal Rate { get; set; }
         public decimal BaseDebit { get; set; }
         public decimal BaseCredit { get; set; }
         public string CurrencyID { get; set; }
         public string OfficeID { get; set; }
         public string DepartmentID { get; set; }
         public string AgentID { get; set; }
         public string ConsigneeID { get; set; }
         public string InstrumentID { get; set; }
         public string CounterpartID { get; set; }
         public string CheckedBy { get; set; }
         public string ApprovedBy { get; set; }

     }

     public class SipesatRpt
     {
         public DateTime ValueDateFrom { get; set; }
         public DateTime ValueDateTo { get; set; }
         public int PeriodPK { get; set; }
         public string IDPJK { get; set; }
         public int InvestorType { get; set; }
         public string TempatLahir { get; set; }
         public string ClientName { get; set; }
         public string TanggalLahir { get; set; }
         public string Alamat { get; set; }
         public string NoKTP { get; set; }
         public string NoIDLain { get; set; }
         public string ID { get; set; }
         public string NPWP { get; set; }


     }

     public class ManajemenFeeHarian
     {
         public string Date { get; set; }
         public string FundID { get; set; }
         public string FundName { get; set; }
         public decimal ManagementFeeAmount { get; set; }

     }


     public class SettlementInstructionRpt
     {
         public string DownloadMode { get; set; }
         public int Status { get; set; }
         public int FundPK { get; set; }
         public string ReportName { get; set; }
         public DateTime ValueDateFrom { get; set; }
         public string ParamInstType { get; set; }
         public string ParamFundID { get; set; }
         public string ParamListDate { get; set; }
         public bool BitIsMature { get; set; }
         public string Message { get; set; }
     }


     public class ListInstrumentByAccountRpt
        {
            public string InstrumentID { get; set; }
            public string InstrumentName { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public decimal PreviousBaseBalance { get; set; }
            public decimal BaseDebitMutasi { get; set; }
            public decimal BaseCreditMutasi { get; set; }
            public decimal CurrentBaseBalance { get; set; }

        }


     public class ListDepartmentByAccountRpt
        {
            public string DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public decimal PreviousBaseBalance { get; set; }
            public decimal BaseDebitMutasi { get; set; }
            public decimal BaseCreditMutasi { get; set; }
            public decimal CurrentBaseBalance { get; set; }
        }

     public class ProductSummaryRpt
        {
            public string FundName { get; set; }
            public string CurrencyID { get; set; }
            public decimal Unit { get; set; }
            public decimal AUM { get; set; }
            public decimal ManagementFee { get; set; }
            public decimal SharingFee { get; set; }
            public decimal MFeePercent { get; set; }
            public string SInvestCode { get; set; }
            public string BankCustody { get; set; }
            public string FundType { get; set; }
            public string MFeeType { get; set; }
         
        }


     public class SellingAgentSummaryRpt
     {
         public string FundName { get; set; }
         public string CurrencyID { get; set; }
         public decimal Unit { get; set; }
         public decimal AUM { get; set; }
         public decimal ManagementFee { get; set; }
         public decimal SharingFee { get; set; }
         public decimal MFeePercent { get; set; }
         public string SInvestCode { get; set; }
         public string BankCustody { get; set; }
         public string FundType { get; set; }
         public string SellingAgentName { get; set; }
         public decimal SFeePercent { get; set; }
         public string MFeeType { get; set; }
         public string SharingFeeType { get; set; }

     }


     public class DistributedSIDRpt
     {
         public string JmlNasabah { get; set; }
         public string BulanTerakhir { get; set; }
         public string JmlDibentukSID { get; set; }
         public string JmlTelahDistribusi { get; set; }
         public string JmlBlmDistribusi { get; set; }
         public string Media { get; set; }
         public string Alasan { get; set; }
     }

     public class TaxAmnestyRpt
     {
         public DateTime ValueDateFrom { get; set; }
         public DateTime ValueDateTo { get; set; }
         public int PeriodPK { get; set; }
         public string ReportName { get; set; }
         public string DownloadMode { get; set; }
         public bool PageBreak { get; set; }
     }

     public class ApolloRpt
     {
         public DateTime ValueDateFrom { get; set; }
         public DateTime ValueDateTo { get; set; }
         public string PrintDate { get; set; }
         public string TotalRow12 { get; set; }
         public string JumlahNasabah12 { get; set; }
         public string TotalRow14 { get; set; }
         public string JumlahNasabah14 { get; set; }
         public string TotalRow18 { get; set; }
         public string JumlahNasabah18 { get; set; }
         public string TotalSID { get; set; }
         public string TotalRekeningEfek { get; set; }
     }

     public class DirjenRpt
     {
         public string PeriodPelaporan { get; set; }
         public string NamaGateway { get; set; }
         public string Nama { get; set; }
         public string NPWP { get; set; }
         public string NoRekKhusus { get; set; }
         public string NamaInvestasi { get; set; }
         public string KodeMataUang { get; set; }
         public decimal NilaiPerolehanSelainRupiah { get; set; }
         public decimal NilaiPasarSelainRupiah { get; set; }
         public decimal NilaiPerolehanRupiah { get; set; }
         public decimal NilaiPasarRupiah { get; set; }
         public decimal PosisiSaldoSelainRupiah { get; set; }
         public decimal PosisiSaldoRupiah { get; set; }

         public string TanggalTransaksi { get; set; }
         public string NomorRekeningKhusus { get; set; }
         public decimal NilaiPemasukan { get; set; }
         public decimal NilaiPengeluaran { get; set; }
         public decimal SaldoRekeningKhusus { get; set; }
         public string KodeTransaksi { get; set; }
         public string NomorRekening { get; set; }
         public string Alamat { get; set; }
     }


    public class TBMonthonMonth
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal January { get; set; }
        public decimal February { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal August { get; set; }
        public decimal September { get; set; }
        public decimal October { get; set; }
        public decimal November { get; set; }
        public decimal December { get; set; }

    }

    public class DailyComplianceReport
    {
        public string Date { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public decimal Amount { get; set; }
        public decimal NAVPercent { get; set; }
        public string Type { get; set; }
        public int InstrumentType { get; set; }
        public string InstrumentID { get; set; }
        public decimal TotalAUM { get; set; }

        public decimal DepositoAmount { get; set; }
        public decimal DEPPercentOfNav { get; set; }
        public decimal BondAmount { get; set; }
        public decimal BondPercentOfNav { get; set; }
        public decimal EquityAmount { get; set; }
        public decimal EQPercentOfNav { get; set; }
        public decimal TotalPercent { get; set; }
        public decimal Under10Bio { get; set; }
    }


    public class CustomerDashBoardRpt
    {
        public DateTime Date { get; set; }
        public string SID { get; set; }
        public string IFUACode { get; set; }
        public string FundClientID { get; set; }
        public string InternalCategory { get; set; }
        public string FundClientName { get; set; }
        public string AgentName { get; set; }
        public string BankName { get; set; }
        public string BankAccNo { get; set; }
        public string BankAccName { get; set; }
        public string FundID { get; set; }
        public decimal Amount { get; set; }
        public decimal Unit { get; set; }
        public decimal LastNav { get; set; }
        public string LastNavDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public string DateOfBirth { get; set; }
        public string IDType { get; set; }
        public string KYCRiskAppetite { get; set; }
        public string IDNo { get; set; }
        public string MotherName { get; set; }
        public string HighRisk { get; set; }
        public string RegDate { get; set; }
        public string OnlineFLag { get; set; }
        public bool CantSub { get; set; }
        public bool CantRed { get; set; }
        public string LastKYCUpdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

    }


    public class KPD
    {
        public string KodeNasabah { get; set; }
        public string NamaNasabah { get; set; }
        public string NomorKontrak { get; set; }
        public string TanggalKontrak { get; set; }
        public string TanggalJatuhTempo { get; set; }
        public string NomorAdendum { get; set; }
        public string TanggalAdendum { get; set; }
        public string NilaiInvestasiAwalIDR { get; set; }
        public string NilaiInvestasiAwalNonIDR { get; set; }
        public string NilaiInvestasiAkhir { get; set; }
        public string NilaiInvestasiAkhirNonIDR { get; set; }
        public string JenisEfek { get; set; }
        public int KodeKategoriEfek { get; set; }
        public string JumlahEfek { get; set; }
        public string NilaiPembelian { get; set; }
        public string NilaiNominal { get; set; }
        public string HPW { get; set; }
        public string Deposito { get; set; }
        public string TotalInvestasi { get; set; }
        public string KodeBK { get; set; }
        public string Keterangan { get; set; }
        public string SID { get; set; }


    }


    public class ListConsigneeByAccountRpt
    {
        public string ConsigneeID { get; set; }
        public string ConsigneeName { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal PreviousBaseBalance { get; set; }
        public decimal BaseDebitMutasi { get; set; }
        public decimal BaseCreditMutasi { get; set; }
        public decimal CurrentBaseBalance { get; set; }

    }


    public class CustodianRpt
    {
        public string DownloadMode { get; set; }
        public string Profile { get; set; }
        public string Groups { get; set; }
        public bool PageBreak { get; set; }
        public bool ShowNullData { get; set; }
        public int Status { get; set; }
        public string ReportName { get; set; }
        public DateTime ValueDateFrom { get; set; }
        public DateTime ValueDateTo { get; set; }
        public string ReferenceFrom { get; set; }
        public string ReferenceTo { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        public string FundClientFrom { get; set; }
        public string FundClientTo { get; set; }
        public string FundFrom { get; set; }
        public string FundTo { get; set; }
        public string InstrumentFrom { get; set; }
        public string InstrumentTo { get; set; }
        public string RowFrom { get; set; }
        public string RowTo { get; set; }
        public int ParamData { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public string CounterpartFrom { get; set; }
        public string InstrumentTypeFrom { get; set; }

        public string FundName { get; set; }
        public string InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public decimal UnitQuantity { get; set; }
        public decimal Lot { get; set; }
        public decimal AverageCost { get; set; }
        public decimal BookValue { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal MarketValue { get; set; }
        public decimal UnrealizedProfitLoss { get; set; }
        public decimal PercentProfilLoss { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Sector { get; set; }
        public decimal PercentTA { get; set; }
        public decimal PercentJCI { get; set; }
        public decimal Beta { get; set; }
        public decimal PercentSegment { get; set; }
        public decimal Compliance { get; set; }

        public string TimeDeposit { get; set; }
        public string BICode { get; set; }
        public string Branch { get; set; }
        public decimal Nominal { get; set; }
        public string TradeDate { get; set; }
        public string MaturityDate { get; set; }
        public decimal Rate { get; set; }
        public decimal AccTD { get; set; }
        public string MaturityAlert { get; set; }
        public string PortfolioEfek { get; set; }
        public string InsType { get; set; }
        public decimal PercentInvestment { get; set; }
        public string Sectors { get; set; }
        public decimal AlokasiSector { get; set; }
        public int FundPK { get; set; }
        public string BankFrom { get; set; }
        public int Signature1 { get; set; }
        public int Signature2 { get; set; }

    }

    public class AktivitasBankCustodianSummary
    {
        public decimal Saham { get; set; }
        public decimal Obligasi { get; set; }
        public decimal Reksadana { get; set; }
        public decimal Lainnya { get; set; }

    }

    public class AktivitasBankCustodianFund
    {
        public string Efek { get; set; }
        public decimal Frekuensi { get; set; }
        public decimal VolumeJutaDalamUnit { get; set; }
        public decimal NilaiMilyarDalamRupiah { get; set; }
        public string StatusI { get; set; }
        public string StatusA { get; set; }
        public decimal KonfirmasiInvestorTepatWaktu { get; set; }

    }

    public class NAVperPeriodeRekapitulasi_NAB
    {
        public decimal No { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Date { get; set; }
        public decimal NABUnit { get; set; }
        public decimal NABIDR { get; set; }
        public decimal CustodianFeeExclPPNIDR { get; set; }
        public decimal TotalReksaDana { get; set; }
        public decimal TotalKPD { get; set; }
        public decimal TotalSwakelola { get; set; }
        public decimal TotalAUCIDR { get; set; }



    }

    public class AktivitasBankCustodianKonvensional
    {
        public string Fund { get; set; }
        public decimal FrekuensiBeli { get; set; }
        public decimal VolumeJutaBeli { get; set; }
        public decimal NilaiMilyarBeli { get; set; }
        public string StatusIBeli { get; set; }
        public string StatusABeli { get; set; }
        public string KonfirmasiTepatWaktuBeli { get; set; }
        public decimal FrekuensiJual { get; set; }
        public decimal VolumeJutaJual { get; set; }
        public decimal NilaiMilyarJual { get; set; }
        public string StatusIJual { get; set; }
        public string StatusAJual { get; set; }
        public string KonfirmasiTepatWaktuJual { get; set; }

    }

    public class SIDClientSummary
    {
        public string HeaderName { get; set; }
        public string FundName { get; set; }
        public string SID { get; set; }
        public string Name { get; set; }
        public string InternalCategory { get; set; }
        public decimal Unit { get; set; }
        public decimal NAV { get; set; }
        public decimal AUM { get; set; }
        public string ClientName { get; set; }
        public int FundPK { get; set; }
        public int Flag { get; set; }

    }

    public class HistorySettlement
    {

        public string TanggalTransaksi { get; set; }
        public string SettlementDate { get; set; }
        public string StockId { get; set; }
        public string StockName { get; set; }
        public string Fund { get; set; }
        public string Broker { get; set; }
        public string Type { get; set; }
        public string TrxTypeID { get; set; }
        public decimal DonePrice { get; set; }
        public decimal DoneLot { get; set; }
        public decimal Amount { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal LevyAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal IncomeTaxSellAmount { get; set; }
        public decimal TotalAmount { get; set; }






    }

    public class InstrumentBySectorAndIndex
    {
        public int FundPK { get; set; }
        public string FundName { get; set; }
        public string MainSector { get; set; }
        public string Description { get; set; }
        public string PortofolioCode { get; set; }
        public decimal NoOfShare { get; set; }
        public decimal MarketValue { get; set; }
        public decimal Percentage { get; set; }
        public decimal UnrealizedGainLoss { get; set; }
        public decimal LQ45 { get; set; }
        public decimal IDX30 { get; set; }
        public decimal IHSG { get; set; }
        public decimal SRIKEHATI { get; set; }
        public decimal ISSI { get; set; }
        public decimal JII { get; set; }
        public string GroupType { get; set; }

    }

}