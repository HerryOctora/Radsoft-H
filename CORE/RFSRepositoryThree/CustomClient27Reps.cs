using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;
using RFSRepository;
using OfficeOpenXml.Drawing.Chart;
using System.Xml;

namespace RFSRepositoryThree
{
    public class CustomClient27Reps
    {
        Host _host = new Host();


        public class PortfolioValuationReportCounterpart
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
            public decimal DailyAccInterestBond { get; set; }
            public int DaysType { get; set; }
            public int HoldingDays { get; set; }
            public string LastCouponDate { get; set; }
            public string NextCouponDate { get; set; }
            public decimal NetInterest { get; set; }
            public decimal HoldingAccrual { get; set; }
            public decimal AvgPrice { get; set; }
            public int DaysCoupon { get; set; }
            public int CounterpartPK { get; set; }
            public string CounterpartName { get; set; }
            public string CounterpartID { get; set; }
        }

        public class PortfolioValuationReportByCurrencyPK
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
            public decimal DailyAccInterestBond { get; set; }
            public int DaysType { get; set; }
            public int HoldingDays { get; set; }
            public string LastCouponDate { get; set; }
            public string NextCouponDate { get; set; }
            public decimal NetInterest { get; set; }
            public decimal HoldingAccrual { get; set; }
            public decimal AvgPrice { get; set; }
            public int DaysCoupon { get; set; }
            public string CurrencyID { get; set; }
        }

        public class PortfolioValuationReportBondRating
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
            public decimal DailyAccInterestBond { get; set; }
            public int DaysType { get; set; }
            public int HoldingDays { get; set; }
            public string LastCouponDate { get; set; }
            public string NextCouponDate { get; set; }
            public decimal NetInterest { get; set; }
            public decimal HoldingAccrual { get; set; }
            public decimal AvgPrice { get; set; }
            public int DaysCoupon { get; set; }
            public string BondRating { get; set; }
        }


        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {
                      
            #region Fund Portfolio Counterpart
            if (_FundAccountingRpt.ReportName.Equals("Fund Portfolio Counterpart"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _paramCounterpart = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "and FP.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = "And CP.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }

                            cmd.CommandText =
                                @"
                                declare @PeriodPK int

select @PeriodPK = PeriodPK from Period Where @valuedate Between DateFrom and DateTo and Status = 2


declare @PVR table
(
FundPK int,
InstrumentTypePK int,
InstrumentPK int,
MaturityDate datetime,
Balance numeric(18,2),
CostValue  numeric(18,2),
InterestDaysType int,
InterestPaymentType int,
InterestPercent numeric(18,4),
AcqDate datetime,
CounterpartPK int
)

Insert into @PVR
select FundPK,InstrumentTypePK,InstrumentPK,isnull(MaturityDate,'') MaturityDate,sum(case when TrxType = 1 then DoneVolume else DoneVolume * - 1 end) Balance,sum(case when TrxType = 1 then DoneAmount else DoneAmount * - 1 end) CostValue,
InterestDaysType,InterestPaymentType,InterestPercent,isnull(AcqDate,'') AcqDate,CounterpartPK  from Investment 
where StatusSettlement = 2 and FundPK = @FundPK 
and PeriodPK = @PeriodPK and InstrumentTypePK not in (5)
group by FundPK,InstrumentTypePK,InstrumentPK,MaturityDate,InterestDaysType,InterestPaymentType,InterestPercent,AcqDate,CounterpartPK



select FP.FundPK ,isnuLL(FP.CostValue/Balance,0) AvgPrice,@valuedate Date,FP.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance Balance,FP.CostValue CostValue,  
isnull(P.ClosePriceValue,0) ClosePrice
,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
,SUM(ISNULL(FP.Balance * FP.InterestPercent / 100 /
CASE WHEN FP.InterestDaysType = 4 then 365 ELSE 360 END,0)) * 0.8  AccrualHarian
,Case when IT.Type =3 then  SUM(ISNULL(FP.Balance * FP.InterestPercent / 100 /
CASE WHEN FP.InterestDaysType = 4 then 365 ELSE 360 END,0)) * 0.8  
* datediff(day,FP.AcqDate,@ValueDate )
else FP.InterestPercent end Accrual
,FP.InterestPercent 
,isnull(case when FP.InstrumentTypePK in (1,4,16) then P.ClosePriceValue else P.ClosePriceValue/100 end,0) * Balance MarketValue,
sum((isnull(case when FP.InstrumentTypePK in (1,4,16) then P.ClosePriceValue else P.ClosePriceValue/100 end,0) * Balance) - FP.CostValue) Unrealised,
case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum(((isnull(case when FP.InstrumentTypePK in (1,4,16) then P.ClosePriceValue else P.ClosePriceValue/100 end,0) * Balance) / 
case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav , sum(FP.Balance/100) Lot ,case when FP.InstrumentTypePK not in (5,6) then  sum(((isnull(case when FP.InstrumentTypePK in (1,4,16) then P.ClosePriceValue else P.ClosePriceValue/100 end,0) * Balance) - FP.CostValue))/FP.CostValue * 100 else 0 end PercentFR 
,Case when IT.Type =3 then  SUM(ISNULL(FP.Balance * FP.InterestPercent / 100 /
CASE WHEN FP.InterestDaysType = 4 then 365 ELSE 360 END,0)) * 0.8 * I.TaxExpensePercent/100
else 0 end TaxAccInterest

,Case when IT.Type not in (1,3,4,12) then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance)
else 0 end AccInterestBond
,Case when IT.Type not in (1,3,4,12) then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) * I.TaxExpensePercent/100
else 0 end TaxAccInterestBond


,'' SInvestID,'' BankName,'' BranchID,FP.AcqDate,CP.Name CounterpartName
from @PVR FP   
left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status in (1,2)   
left join Fund F on FP.FundPK = F.FundPK and F.status in (1,2)   
left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status in (1,2)  
left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status in (1,2)  
left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType'
left join ClosePrice P on FP.InstrumentPK = P.InstrumentPK and P.status in (1,2) and P.Date = @valuedate
left join Counterpart CP on FP.CounterpartPK = CP.CounterpartPK and CP.status in (1,2)  
where FP.Balance <> 0 " + _paramFund + _paramCounterpart + @"
group by FP.FundPK,FP.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
P.ClosePriceValue ,FP.InterestPercent ,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,FP.AcqDate,I.TaxExpensePercent,CP.Name
order by CP.Name
                                ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundPortfolioCounterpart" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "FundPortfolioCounterpart" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PortfolioValuationReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Portfolio Valuation Report Counterpart");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PortfolioValuationReportCounterpart> rList = new List<PortfolioValuationReportCounterpart>();
                                        while (dr0.Read())
                                        {
                                            PortfolioValuationReportCounterpart rSingle = new PortfolioValuationReportCounterpart();
                                            rSingle.SecurityCode = Convert.ToString(dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]));
                                            rSingle.SecurityDescription = Convert.ToString(dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]));
                                            rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentTypeName"]));
                                            rSingle.QtyOfUnit = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.Lot = Convert.ToDecimal(dr0["Lot"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Lot"]));
                                            rSingle.AverageCost = Convert.ToDecimal(dr0["AvgPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AvgPrice"]));
                                            rSingle.BookValue = Convert.ToDecimal(dr0["CostValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CostValue"]));
                                            rSingle.MarketPrice = Convert.ToDecimal(dr0["ClosePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["ClosePrice"]));
                                            rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["MarketValue"]));
                                            rSingle.UnrealizedProfitLoss = Convert.ToDecimal(dr0["Unrealised"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Unrealised"]));
                                            rSingle.PercentFR = Convert.ToDecimal(dr0["PercentFR"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentFR"]));
                                            rSingle.BICode = Convert.ToString(dr0["SInvestID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SInvestID"]));
                                            rSingle.Branch = Convert.ToString(dr0["BranchID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BranchID"]));
                                            rSingle.Nominal = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.TradeDate = Convert.ToString(dr0["AcqDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AcqDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rSingle.RateGross = Convert.ToDecimal(dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]));
                                            rSingle.AccIntTD = Convert.ToDecimal(dr0["AccrualHarian"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccrualHarian"]));
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.InstrumentTypePK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                            rSingle.Date = Convert.ToString(dr0["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Date"]));
                                            rSingle.Fund = Convert.ToString(dr0["FundPK"]);
                                            rSingle.TaxExpensePercent = Convert.ToDecimal(dr0["TaxAccInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TaxAccInterest"]));
                                            rSingle.AccInterestBond = Convert.ToDecimal(dr0["AccInterestBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccInterestBond"]));
                                            rSingle.TaxAccInterestBond = Convert.ToDecimal(dr0["TaxAccInterestBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TaxAccInterestBond"]));
                                            //rSingle.CounterpartPK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                            //rSingle.CounterpartID = Convert.ToString(dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]));
                                            rSingle.CounterpartName = Convert.ToString(dr0["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartName"]));
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.FundName, r.InstrumentTypePK, r.CounterpartName, r.Date ascending
                                         group r by new { r.Fund, r.FundName, r.InstrumentTypeName, r.CounterpartName, r.Date, r.InstrumentTypePK } into rGroup
                                         select rGroup;


                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 3;


                                        int _cash = 0;
                                        int _endCash = 0;



                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundName(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            _cash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 5].Value = "CASH AT BANK :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_TotalAccountBalanceByFundPK(rsHeader.Key.Fund, 2, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "SINVEST CODE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundSinvestCode(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 5].Value = "OUSTANDING PAYMENT :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_OutstandingPaymentByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND TYPE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundType(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            _endCash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 5].Value = "OUSTANDING RECEIVABLE :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_OutstandingReceivableByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;



                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 5].Value = "CASH PROJECTION :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _cash + ":F" + _endCash + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;




                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "INSTRUMENT TYPE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.InstrumentTypeName;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 5].Value = "NAV :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_LastNav(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "COUNTERPART :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CounterpartName;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 5].Value = "AUM :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_LastAUMFromCloseNav(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;

                                            if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16) //Equity
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 5].Value = "Lot";
                                                worksheet.Cells[incRowExcel, 6].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 7].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 10].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 11].Value = "%fr P/L";


                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }                                            

                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 5].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Tax Accrued Interest";
                                                worksheet.Cells[incRowExcel, 7].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 8].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 9].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 10].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 11].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 12].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 13].Value = "%fr P/L";

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            }

                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.Lot;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.PercentFR;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                }

                                                
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.TaxAccInterestBond;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.AccInterestBond;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.PercentFR;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                            }

                                            if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "J" + _endRowDetail + "/G" + _endRowDetail;
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();


                                            }
                                            
                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 13].Formula = "L" + _endRowDetail + "/I" + _endRowDetail;
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 12].Calculate();
                                                worksheet.Cells[incRowExcel, 13].Calculate();
                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                            incRowExcel++;

                                        }




                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 13];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 30;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 22;
                                        worksheet.Column(10).Width = 22;
                                        worksheet.Column(11).Width = 25;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 15;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 PORTFOLIO VALUATION REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderRightText();
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    throw err;
                }
            }

            #endregion

            #region Fund Portfolio By CurrencyPK
            if (_FundAccountingRpt.ReportName.Equals("Fund Portfolio By CurrencyPK"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _paramCurrency = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And FP.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (!_host.findString(_FundAccountingRpt.CurrencyFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CurrencyFrom))
                            {
                                _paramCurrency = "And P.CurrencyPK in ( " + _FundAccountingRpt.CurrencyFrom + " ) ";
                            }
                            else
                            {
                                _paramCurrency = "";
                            }

                            cmd.CommandText =
                                @"
                                declare @InterestDays int
                                SELECT  
                                @InterestDays = DATEDIFF(DAY, valuedate, @valuedate)  
                                FROM enddaytrails  
                                WHERE enddaytrailspk = (SELECT  
                                MAX(enddaytrailspk)  
                                FROM enddaytrails  
                                WHERE valuedate < @valuedate  
                                AND status = 2)  

                                select FP.FundPK , FP.AvgPrice AvgPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
                                F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance Balance,FP.CostValue CostValue,  
                                FP.ClosePrice ClosePrice
                                ,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) / case when @InterestDays = 0 then 1 else  @InterestDays end 
                                else 0 end AccrualHarian
                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) / case when @InterestDays = 0 then 1 else  @InterestDays end 
                                * datediff(day,FP.AcqDate,@ValueDate )
                                else FP.InterestPercent end Accrual
                                ,FP.InterestPercent 
                                ,FP.MarketValue MarketValue,
                                sum(FP.MarketValue - FP.CostValue)Unrealised,case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum((FP.MarketValue / 
                                case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav , sum(FP.Balance/100) Lot ,case when I.InstrumentTypePK not in (5,6) then  sum((FP.MarketValue - FP.CostValue))/FP.CostValue * 100 else 0 end PercentFR 

                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) * I.TaxExpensePercent/100
                                else 0 end TaxAccInterest
                                ,Case when IT.Type not in (1,3,4,12) then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance)
                                else 0 end AccInterestBond
                                ,Case when IT.Type not in (1,3,4,12) then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) * I.TaxExpensePercent/100
                                else 0 end TaxAccInterestBond


                                ,O.SInvestID,O.Name BankName,N.ID BranchID,FP.AcqDate,isnull(P.ID,'') CurrencyID
                                from fundposition FP   
                                left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status in (1,2)   
                                left join Fund F on FP.FundPK = F.FundPK and F.status in (1,2)   
                                left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status in (1,2)  
                                left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status in (1,2)  
                                left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType'
                                left join BankBranch N on FP.BankBranchPK = N.BankBranchPK and N.status in (1,2)
                                left join Bank O on N.BankPK = O.BankPK and O.status in (1,2)
                                left join Currency P on I.CurrencyPK = P.CurrencyPK and P.status in (1,2)
                                where FP.status in (1,2)  and FP.Date = @ValueDate " + _paramFund + _paramCurrency + @"
                                group by FP.FundPK,Fp.AvgPrice,FP.Date,I.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
                                FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,O.SInvestID,O.Name,N.ID,FP.AcqDate,I.TaxExpensePercent,P.ID
                                order by I.ID
                                ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundPortfolioByCurrencyPK" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "FundPortfolioByCurrencyPK" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PortfolioValuationReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Portfolio Valuation Report");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PortfolioValuationReportByCurrencyPK> rList = new List<PortfolioValuationReportByCurrencyPK>();
                                        while (dr0.Read())
                                        {
                                            PortfolioValuationReportByCurrencyPK rSingle = new PortfolioValuationReportByCurrencyPK();
                                            rSingle.SecurityCode = Convert.ToString(dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]));
                                            rSingle.SecurityDescription = Convert.ToString(dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]));
                                            rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentTypeName"]));
                                            rSingle.QtyOfUnit = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.Lot = Convert.ToDecimal(dr0["Lot"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Lot"]));
                                            rSingle.AverageCost = Convert.ToDecimal(dr0["AvgPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AvgPrice"]));
                                            rSingle.BookValue = Convert.ToDecimal(dr0["CostValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CostValue"]));
                                            rSingle.MarketPrice = Convert.ToDecimal(dr0["ClosePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["ClosePrice"]));
                                            rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["MarketValue"]));
                                            rSingle.UnrealizedProfitLoss = Convert.ToDecimal(dr0["Unrealised"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Unrealised"]));
                                            rSingle.PercentFR = Convert.ToDecimal(dr0["PercentFR"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentFR"]));
                                            rSingle.BICode = Convert.ToString(dr0["SInvestID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SInvestID"]));
                                            rSingle.Branch = Convert.ToString(dr0["BranchID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BranchID"]));
                                            rSingle.Nominal = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.TradeDate = Convert.ToString(dr0["AcqDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AcqDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rSingle.RateGross = Convert.ToDecimal(dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]));
                                            rSingle.AccIntTD = Convert.ToDecimal(dr0["AccrualHarian"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccrualHarian"]));
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.InstrumentTypePK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                            rSingle.Date = Convert.ToString(dr0["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Date"]));
                                            rSingle.Fund = Convert.ToString(dr0["FundPK"]);
                                            //rSingle.CurrencyPK = Convert.ToInt32(dr0["CurrencyPK"]);
                                            rSingle.TaxExpensePercent = Convert.ToDecimal(dr0["TaxAccInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TaxAccInterest"]));
                                            rSingle.AccInterestBond = Convert.ToDecimal(dr0["AccInterestBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccInterestBond"]));
                                            rSingle.TaxAccInterestBond = Convert.ToDecimal(dr0["TaxAccInterestBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TaxAccInterestBond"]));

                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.FundName, r.InstrumentTypePK, r.Date ascending
                                         group r by new { r.Fund, r.FundName, r.InstrumentTypeName, r.Date, r.InstrumentTypePK, r.CurrencyID } into rGroup
                                         select rGroup;


                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 3;


                                        int _cash = 0;
                                        int _endCash = 0;



                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundName(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            _cash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 5].Value = "CASH AT BANK :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_TotalAccountBalanceByFundPK(rsHeader.Key.Fund, 2, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "SINVEST CODE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundSinvestCode(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 5].Value = "OUSTANDING PAYMENT :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_OutstandingPaymentByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND TYPE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundType(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            _endCash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 5].Value = "OUSTANDING RECEIVABLE :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_OutstandingReceivableByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;



                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 5].Value = "CASH PROJECTION :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _cash + ":F" + _endCash + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;




                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "INSTRUMENT TYPE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.InstrumentTypeName;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 5].Value = "NAV :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_LastNav(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "CURRENCY :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CurrencyID;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 5].Value = "AUM :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_LastAUMFromCloseNav(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;

                                            if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16) //Equity
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 5].Value = "Lot";
                                                worksheet.Cells[incRowExcel, 6].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 7].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 10].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 11].Value = "%fr P/L";


                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }

                                            else if (rsHeader.Key.InstrumentTypePK == 5) //Deposito
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 1].Value = "No";
                                                worksheet.Cells[incRowExcel, 2].Value = "TIME DEPOSIT";
                                                worksheet.Cells[incRowExcel, 3].Value = "BI CODE";
                                                worksheet.Cells[incRowExcel, 4].Value = "BRANCH";
                                                worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 8].Value = "Tax Accrued Interest";
                                                worksheet.Cells[incRowExcel, 9].Value = "Rate (Gross)";
                                                worksheet.Cells[incRowExcel, 10].Value = "Acc Int.TD";
                                                worksheet.Cells[incRowExcel, 11].Value = "% fr TA";

                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            }

                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 5].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Tax Accrued Interest";
                                                worksheet.Cells[incRowExcel, 7].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 8].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 9].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 10].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 11].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 12].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 13].Value = "%fr P/L";

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            }

                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.Lot;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.PercentFR;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                }

                                                else if (rsHeader.Key.InstrumentTypePK == 5)
                                                {

                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BICode;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.Branch;
                                                    worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.Nominal;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail.TradeDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.TaxExpensePercent;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.RateGross;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.AccIntTD;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.PercentFR;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.TaxAccInterestBond;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.AccInterestBond;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.PercentFR;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                            }

                                            if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "J" + _endRowDetail + "/G" + _endRowDetail;
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();


                                            }
                                            else if (rsHeader.Key.InstrumentTypePK == 5)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 5].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();

                                            }
                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 13].Formula = "L" + _endRowDetail + "/I" + _endRowDetail;
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 12].Calculate();
                                                worksheet.Cells[incRowExcel, 13].Calculate();
                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                            incRowExcel++;

                                        }




                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 13];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 30;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 22;
                                        worksheet.Column(10).Width = 22;
                                        worksheet.Column(11).Width = 25;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 15;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 PORTFOLIO VALUATION REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderRightText();
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    throw err;
                }
            }

            #endregion

            #region Fund Portfolio Bond Rating
            if (_FundAccountingRpt.ReportName.Equals("PVR Bond Rating"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _paramBondRating = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (!_host.findString(_FundAccountingRpt.BondRatingFrom.ToLower(), "'all'", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.BondRatingFrom))
                            {
                                _paramBondRating = "And I.BondRating in ( " + _FundAccountingRpt.BondRatingFrom + " ) ";
                            }
                            else
                            {
                                _paramBondRating = "";
                            }

                            cmd.CommandText =
                                @"
                                declare @InterestDays int

SELECT  
@InterestDays = DATEDIFF(DAY, valuedate, @valuedate)  
FROM enddaytrails  
WHERE enddaytrailspk = (SELECT  
MAX(enddaytrailspk)  
FROM enddaytrails  
WHERE valuedate < @valuedate  
AND status = 2)  

select FP.FundPK , FP.AvgPrice AvgPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance Balance,FP.CostValue CostValue,  
FP.ClosePrice ClosePrice
,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) / case when @InterestDays = 0 then 1 else  @InterestDays end 
else 0 end AccrualHarian
,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) / case when @InterestDays = 0 then 1 else  @InterestDays end 
* datediff(day,FP.AcqDate,@ValueDate )
else FP.InterestPercent end Accrual
,FP.InterestPercent 
,FP.MarketValue MarketValue,
sum(FP.MarketValue - FP.CostValue)Unrealised,case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum((FP.MarketValue / 
case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav , sum(FP.Balance/100) Lot ,case when I.InstrumentTypePK not in (5,6) then  sum((FP.MarketValue - FP.CostValue))/FP.CostValue * 100 else 0 end PercentFR 

,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) * I.TaxExpensePercent/100
else 0 end TaxAccInterest
,Case when IT.Type not in (1,3,4,12) then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance)
else 0 end AccInterestBond
,Case when IT.Type not in (1,3,4,12) then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) * I.TaxExpensePercent/100
else 0 end TaxAccInterestBond


,O.SInvestID,O.Name BankName,N.ID BranchID,FP.AcqDate,isnull(I.BondRating,'') BondRating
from fundposition FP   
left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status in (1,2)   
left join Fund F on FP.FundPK = F.FundPK and F.status in (1,2)   
left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status in (1,2)  
left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status in (1,2)  
left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType'
left join BankBranch N on FP.BankBranchPK = N.BankBranchPK and N.status in (1,2)
left join Bank O on N.BankPK = O.BankPK and O.status in (1,2)
where FP.status in (1,2)  and FP.Date = @ValueDate and I.InstrumentTypePK not in (1,4,5,6,16) " + _paramFund + _paramBondRating + @"
group by FP.FundPK,Fp.AvgPrice,FP.Date,I.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,O.SInvestID,O.Name,N.ID,FP.AcqDate,I.TaxExpensePercent,I.BondRating
order by I.ID
                                ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "PVRBondRating" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PVRBondRating" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PortfolioValuationReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Portfolio Valuation Report Bond Rating");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PortfolioValuationReportBondRating> rList = new List<PortfolioValuationReportBondRating>();
                                        while (dr0.Read())
                                        {
                                            PortfolioValuationReportBondRating rSingle = new PortfolioValuationReportBondRating();
                                            rSingle.SecurityCode = Convert.ToString(dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]));
                                            rSingle.SecurityDescription = Convert.ToString(dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]));
                                            rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentTypeName"]));
                                            rSingle.QtyOfUnit = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.Lot = Convert.ToDecimal(dr0["Lot"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Lot"]));
                                            rSingle.AverageCost = Convert.ToDecimal(dr0["AvgPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AvgPrice"]));
                                            rSingle.BookValue = Convert.ToDecimal(dr0["CostValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CostValue"]));
                                            rSingle.MarketPrice = Convert.ToDecimal(dr0["ClosePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["ClosePrice"]));
                                            rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["MarketValue"]));
                                            rSingle.UnrealizedProfitLoss = Convert.ToDecimal(dr0["Unrealised"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Unrealised"]));
                                            rSingle.PercentFR = Convert.ToDecimal(dr0["PercentFR"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentFR"]));
                                            rSingle.BICode = Convert.ToString(dr0["SInvestID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SInvestID"]));
                                            rSingle.Branch = Convert.ToString(dr0["BranchID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BranchID"]));
                                            rSingle.Nominal = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.TradeDate = Convert.ToString(dr0["AcqDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AcqDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rSingle.RateGross = Convert.ToDecimal(dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]));
                                            rSingle.AccIntTD = Convert.ToDecimal(dr0["AccrualHarian"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccrualHarian"]));
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.InstrumentTypePK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                            rSingle.Date = Convert.ToString(dr0["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Date"]));
                                            rSingle.Fund = Convert.ToString(dr0["FundPK"]);
                                            rSingle.TaxExpensePercent = Convert.ToDecimal(dr0["TaxAccInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TaxAccInterest"]));
                                            rSingle.AccInterestBond = Convert.ToDecimal(dr0["AccInterestBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccInterestBond"]));
                                            rSingle.TaxAccInterestBond = Convert.ToDecimal(dr0["TaxAccInterestBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TaxAccInterestBond"]));
                                            rSingle.BondRating = dr0["BondRating"].ToString();

                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.FundName, r.InstrumentTypePK, r.Date ascending
                                         group r by new { r.Fund, r.FundName, r.BondRating, r.InstrumentTypeName, r.Date, r.InstrumentTypePK } into rGroup
                                         select rGroup;


                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 3;


                                        int _cash = 0;
                                        int _endCash = 0;



                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundName(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            _cash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 5].Value = "CASH AT BANK :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_TotalAccountBalanceByFundPK(rsHeader.Key.Fund, 2, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "SINVEST CODE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundSinvestCode(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 5].Value = "OUSTANDING PAYMENT :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_OutstandingPaymentByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND TYPE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_FundType(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            _endCash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 5].Value = "OUSTANDING RECEIVABLE :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_OutstandingReceivableByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;



                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 5].Value = "CASH PROJECTION :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _cash + ":F" + _endCash + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;




                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "INSTRUMENT TYPE :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.InstrumentTypeName;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 5].Value = "NAV :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_LastNav(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;


                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "BOND RATING :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.BondRating;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 5].Value = "AUM :";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = _host.Get_LastAUMFromCloseNav(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;

                                            //if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16) //Equity
                                            //{
                                            //    worksheet.Cells[incRowExcel, 1].Value = "No.";
                                            //    worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                            //    worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                            //    worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                            //    worksheet.Cells[incRowExcel, 5].Value = "Lot";
                                            //    worksheet.Cells[incRowExcel, 6].Value = "Average Cost";
                                            //    worksheet.Cells[incRowExcel, 7].Value = "Book Value";
                                            //    worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                            //    worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                            //    worksheet.Cells[incRowExcel, 10].Value = "Unrealized Profit/(Loss)";
                                            //    worksheet.Cells[incRowExcel, 11].Value = "%fr P/L";
                                            //    worksheet.Cells[incRowExcel, 12].Value = "Bond Rating";


                                            //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Font.Bold = true;
                                            //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            //}

                                            //else if (rsHeader.Key.InstrumentTypePK == 5) //Deposito
                                            //{
                                            //    worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            //    worksheet.Cells[incRowExcel, 1].Value = "No";
                                            //    worksheet.Cells[incRowExcel, 2].Value = "TIME DEPOSIT";
                                            //    worksheet.Cells[incRowExcel, 3].Value = "BI CODE";
                                            //    worksheet.Cells[incRowExcel, 4].Value = "BRANCH";
                                            //    worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                            //    worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                            //    worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                            //    worksheet.Cells[incRowExcel, 8].Value = "Tax Accrued Interest";
                                            //    worksheet.Cells[incRowExcel, 9].Value = "Rate (Gross)";
                                            //    worksheet.Cells[incRowExcel, 10].Value = "Acc Int.TD";
                                            //    worksheet.Cells[incRowExcel, 11].Value = "% fr TA";

                                            //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                            //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //}

                                            //else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 5].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Tax Accrued Interest";
                                                worksheet.Cells[incRowExcel, 7].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 8].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 9].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 10].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 11].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 12].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 13].Value = "%fr P/L";
                                                //worksheet.Cells[incRowExcel, 14].Value = "Bond Rating";

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            }

                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                                //{
                                                //    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                //    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                //    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDescription;
                                                //    worksheet.Cells[incRowExcel, 4].Value = rsDetail.QtyOfUnit;
                                                //    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                //    worksheet.Cells[incRowExcel, 5].Value = rsDetail.Lot;
                                                //    worksheet.Cells[incRowExcel, 6].Value = rsDetail.AverageCost;
                                                //    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                //    worksheet.Cells[incRowExcel, 7].Value = rsDetail.BookValue;
                                                //    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                //    worksheet.Cells[incRowExcel, 8].Value = rsDetail.MarketPrice;
                                                //    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                //    worksheet.Cells[incRowExcel, 9].Value = rsDetail.MarketValue;
                                                //    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                //    worksheet.Cells[incRowExcel, 10].Value = rsDetail.UnrealizedProfitLoss;
                                                //    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                //    worksheet.Cells[incRowExcel, 11].Value = rsDetail.PercentFR;
                                                //    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                //}

                                                //else if (rsHeader.Key.InstrumentTypePK == 5)
                                                //{

                                                //    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                //    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                //    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BICode;
                                                //    worksheet.Cells[incRowExcel, 4].Value = rsDetail.Branch;
                                                //    worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                //    worksheet.Cells[incRowExcel, 5].Value = rsDetail.Nominal;
                                                //    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                //    worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail.TradeDate).ToString("dd-MMM-yyyy");
                                                //    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //    worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                //    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                //    worksheet.Cells[incRowExcel, 8].Value = rsDetail.TaxExpensePercent;
                                                //    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                //    worksheet.Cells[incRowExcel, 9].Value = rsDetail.RateGross;
                                                //    worksheet.Cells[incRowExcel, 10].Value = rsDetail.AccIntTD;
                                                //    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                //    worksheet.Cells[incRowExcel, 11].Value = rsDetail.PercentFR;
                                                //    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                //}
                                                //else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.TaxAccInterestBond;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.AccInterestBond;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.PercentFR;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    //worksheet.Cells[incRowExcel, 14].Value = rsDetail.BondRating;
                                                    //worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";


                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                            }

                                            //if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                            //{
                                            //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                            //    worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                            //    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                            //    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            //    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            //    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            //    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                            //    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            //    worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            //    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            //    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            //    worksheet.Cells[incRowExcel, 11].Formula = "J" + _endRowDetail + "/G" + _endRowDetail;
                                            //    worksheet.Cells[incRowExcel, 4].Calculate();
                                            //    worksheet.Cells[incRowExcel, 7].Calculate();
                                            //    worksheet.Cells[incRowExcel, 9].Calculate();
                                            //    worksheet.Cells[incRowExcel, 10].Calculate();
                                            //    worksheet.Cells[incRowExcel, 11].Calculate();


                                            //}
                                            //else if (rsHeader.Key.InstrumentTypePK == 5)
                                            //{
                                            //    worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Font.Bold = true;
                                            //    worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                            //    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            //    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            //    worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            //    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            //    worksheet.Cells[incRowExcel, 5].Calculate();
                                            //    worksheet.Cells[incRowExcel, 10].Calculate();

                                            //}
                                            //else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 13].Formula = "L" + _endRowDetail + "/I" + _endRowDetail;
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 12].Calculate();
                                                worksheet.Cells[incRowExcel, 13].Calculate();
                                                //worksheet.Cells[incRowExcel, 14].Calculate();
                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                            incRowExcel++;

                                        }




                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 13];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 30;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 22;
                                        worksheet.Column(10).Width = 22;
                                        worksheet.Column(11).Width = 25;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 15;
                                        //worksheet.Column(14).Width = 15;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 PORTFOLIO VALUATION REPORT BOND RATING";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderRightText();
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    throw err;
                }
            }

            #endregion


            else
            {
                return false;
            }
        }


    }
}
