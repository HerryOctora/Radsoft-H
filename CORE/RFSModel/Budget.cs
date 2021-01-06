using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class Budget
    {
        public int BudgetPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }
        public int FundTypePK { get; set; }
        public string FundTypeID { get; set; }
        public int ReportPeriodPK { get; set; }
        public string ReportPeriodID { get; set; }
        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public int DepartmentPK { get; set; }
        public string DepartmentID { get; set; }
        public int AccountPK { get; set; }
        public string AccountID { get; set; }
        public int ItemBudgetPK { get; set; }
        public string ItemBudgetID { get; set; }
        public decimal Amount { get; set; }
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

    public class BudgetRpt
    {

        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string ReportName { get; set; }
        public string CoaNo { get; set; }
        public string CoaName { get; set; }
        public string CostCenter { get; set; }
        public string ItemBudget { get; set; }
        public decimal Amount { get; set; }
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

        public decimal January1 { get; set; }
        public decimal February1 { get; set; }
        public decimal March1 { get; set; }
        public decimal April1 { get; set; }
        public decimal May1 { get; set; }
        public decimal June1 { get; set; }
        public decimal July1 { get; set; }
        public decimal August1 { get; set; }
        public decimal September1 { get; set; }
        public decimal October1 { get; set; }
        public decimal November1 { get; set; }
        public decimal December1 { get; set; }

        public decimal January2 { get; set; }
        public decimal February2 { get; set; }
        public decimal March2 { get; set; }
        public decimal April2 { get; set; }
        public decimal May2 { get; set; }
        public decimal June2 { get; set; }
        public decimal July2 { get; set; }
        public decimal August2 { get; set; }
        public decimal September2 { get; set; }
        public decimal October2 { get; set; }
        public decimal November2 { get; set; }
        public decimal December2 { get; set; }
        public string CurrencyID { get; set; }
        public string RPT { get; set; }
        public string CTG { get; set; }
        public decimal TotalLastYear { get; set; }
        public string BranchName { get; set; }
        public string Category { get; set; }
        public string SalesName { get; set; }
        public decimal MGTFee { get; set; }
        public string InstrumentName { get; set; }
        public decimal BeginningAUMDes { get; set; }
        public string NamaProduk { get; set; }
        public string ProdukKategori { get; set; }
        public decimal YTDSep { get; set; }
        public decimal YTDDes { get; set; }
        public decimal FeeMIRate { get; set; }
        public decimal AUMSeptember { get; set; }

        public decimal AUM { get; set; }

    }

    public class LaporanMIFeeSummary
    {

        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string ReportName { get; set; }
        public string CoaNo { get; set; }
        public string CoaName { get; set; }
        public string CostCenter { get; set; }
        public string ItemBudget { get; set; }
        public decimal Amount { get; set; }
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

        public decimal January1 { get; set; }
        public decimal February1 { get; set; }
        public decimal March1 { get; set; }
        public decimal April1 { get; set; }
        public decimal May1 { get; set; }
        public decimal June1 { get; set; }
        public decimal July1 { get; set; }
        public decimal August1 { get; set; }
        public decimal September1 { get; set; }
        public decimal October1 { get; set; }
        public decimal November1 { get; set; }
        public decimal December1 { get; set; }

        public decimal January2 { get; set; }
        public decimal February2 { get; set; }
        public decimal March2 { get; set; }
        public decimal April2 { get; set; }
        public decimal May2 { get; set; }
        public decimal June2 { get; set; }
        public decimal July2 { get; set; }
        public decimal August2 { get; set; }
        public decimal September2 { get; set; }
        public decimal October2 { get; set; }
        public decimal November2 { get; set; }
        public decimal December2 { get; set; }
        public string CurrencyID { get; set; }
        public string RPT { get; set; }
        public string CTG { get; set; }
        public decimal TotalLastYear { get; set; }
        public string BranchName { get; set; }
        public string Category { get; set; }
        public string SalesName { get; set; }
        public decimal MGTFee { get; set; }
        public string InstrumentName { get; set; }
        public decimal BeginningAUMDes { get; set; }
        public string NamaProduk { get; set; }
        public string ProdukKategori { get; set; }
        public decimal YTDSep { get; set; }
        public decimal YTDDes { get; set; }
        public decimal FeeMIRate { get; set; }
        public decimal AUMSeptember { get; set; }

        public decimal AUM { get; set; }

    }

    public class LaporanMIFeeCabangdanSales
    {

        public int PeriodPK { get; set; }
        public string PeriodID { get; set; }
        public string ReportName { get; set; }
        public string CoaNo { get; set; }
        public string CoaName { get; set; }
        public string CostCenter { get; set; }
        public string ItemBudget { get; set; }
        public decimal Amount { get; set; }
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

        public decimal January1 { get; set; }
        public decimal February1 { get; set; }
        public decimal March1 { get; set; }
        public decimal April1 { get; set; }
        public decimal May1 { get; set; }
        public decimal June1 { get; set; }
        public decimal July1 { get; set; }
        public decimal August1 { get; set; }
        public decimal September1 { get; set; }
        public decimal October1 { get; set; }
        public decimal November1 { get; set; }
        public decimal December1 { get; set; }

        public decimal January2 { get; set; }
        public decimal February2 { get; set; }
        public decimal March2 { get; set; }
        public decimal April2 { get; set; }
        public decimal May2 { get; set; }
        public decimal June2 { get; set; }
        public decimal July2 { get; set; }
        public decimal August2 { get; set; }
        public decimal September2 { get; set; }
        public decimal October2 { get; set; }
        public decimal November2 { get; set; }
        public decimal December2 { get; set; }
        public string CurrencyID { get; set; }
        public string RPT { get; set; }
        public string CTG { get; set; }
        public decimal TotalLastYear { get; set; }
        public string BranchName { get; set; }
        public string Category { get; set; }
        public string SalesName { get; set; }
        public decimal MGTFee { get; set; }
        public string InstrumentName { get; set; }
        public decimal BeginningAUMDes { get; set; }
        public string NamaProduk { get; set; }
        public string ProdukKategori { get; set; }
        public decimal YTDSep { get; set; }
        public decimal YTDDes { get; set; }
        public decimal FeeMIRate { get; set; }
        public decimal AUMSeptember { get; set; }

        public decimal AUM { get; set; }

    }
}