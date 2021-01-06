using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RFSModel
{

    public class ParamFFSForOJK
    {
        public int ParamFund { get; set; }
        public DateTime ParamDateFrom { get; set; }
        public DateTime ParamDateTo { get; set; }
        public string EntryUsersID { get; set; }

    }

    public class FFSForOJK
    {
        public int FFSForOJKPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }


        public DateTime Date { get; set; }
        public int FundPK { get; set; }
        public string FundName { get; set; }
        public int TemplateType { get; set; }
        public string TemplateTypeDesc { get; set; }
        public string PeriodePenilaian { get; set; }
        public string DanaKegiatanSosial { get; set; }
        public string AkumulasiDanaCSR { get; set; }
        public string CSR { get; set; }
        public string Resiko1 { get; set; }
        public string Resiko2 { get; set; }
        public string Resiko3 { get; set; }
        public string Resiko4 { get; set; }
        public string Resiko5 { get; set; }
        public string Resiko6 { get; set; }
        public string Resiko7 { get; set; }
        public string Resiko8 { get; set; }
        public string Resiko9 { get; set; }
        public bool BitDistributedIncome { get; set; }
        public int KlasifikasiResiko { get; set; }
        public string KlasifikasiResikoDesc { get; set; }
        public string ManajerInvestasi { get; set; }
        public string TujuanInvestasi { get; set; }
        public string KebijakanInvestasi1 { get; set; }
        public string KebijakanInvestasi2 { get; set; }
        public string KebijakanInvestasi3 { get; set; }
        public string ProfilBankCustodian { get; set; }
        public string AksesProspektus { get; set; }


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


    public class FFSSetup_OJKRpt
    {
        public string DownloadMode { get; set; }
        public int ParamFundPK { get; set; }
        public string ParamListDate { get; set; }

        // SETTING FFSSetup OJK
        public int FFSForOJKPK { get; set; }
        public string Date { get; set; }
        public int FundPK { get; set; }
        public int TemplateType { get; set; }
        public string PeriodePenilaian { get; set; }
        public string DanaKegiatanSosial { get; set; }
        public string AkumulasiDanaCSR { get; set; }
        public string CSR { get; set; }
        public string Resiko1 { get; set; }
        public string Resiko2 { get; set; }
        public string Resiko3 { get; set; }
        public string Resiko4 { get; set; }
        public string Resiko5 { get; set; }
        public string Resiko6 { get; set; }
        public string Resiko7 { get; set; }
        public string Resiko8 { get; set; }
        public string Resiko9 { get; set; }
        public bool BitDistributedIncome { get; set; }
        public int KlasifikasiResiko { get; set; }
        public string ManajerInvestasi { get; set; }
        public string TujuanInvestasi { get; set; }


        public string NamaKebijakanInvestasi1 { get; set; }
        public string NamaKebijakanInvestasi2 { get; set; }
        public string NamaKebijakanInvestasi3 { get; set; }

        public string KebijakanInvestasi1 { get; set; }
        public string KebijakanInvestasi2 { get; set; }
        public string KebijakanInvestasi3 { get; set; }
        public string ProfilBankCustodian { get; set; }
        public string AksesProspektus { get; set; }
        public string KeteranganResiko { get; set; }


        // Tarikan Fund ,Close NAV
        public DateTime FFSDate { get; set; }
        public string FundName { get; set; }
        public decimal AUM { get; set; }
        public decimal Nav { get; set; }
        public decimal CashAmount { get; set; }
        public decimal Unit { get; set; }
        public string EffectiveDate { get; set; }
        public string OJKLetter { get; set; }
        public string IssueDate { get; set; }
        public string CurrencyID { get; set; }
        public decimal MinSubs { get; set; }
        public string MaxUnits { get; set; }
        public decimal SubscriptionFeePercent { get; set; }
        public decimal RedemptionFeePercent { get; set; }
        public decimal SwitchingFeePercent { get; set; }
        public decimal MFeePercent { get; set; }
        public decimal CustodiFeePercent { get; set; }
        public string ISIN { get; set; }
        public string BankCustodian { get; set; }
        public string FundType { get; set; }
        public string IndexID { get; set; }

        public string InstrumentType { get; set; }
        public decimal ExposurePercent { get; set; }

        public string InstrumentID { get; set; }
        public DateTime NavDate { get; set; }
        public decimal RateIndex { get; set; }
        public int TotalBenchmark { get; set; }

        public DateTime DistributedDate { get; set; }

        public int HoldingPeriod { get; set; }

    }


}
