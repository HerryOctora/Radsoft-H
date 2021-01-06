using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
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


namespace RFSRepositoryTwo
{
    public class CustomClient17Reps
    {
        Host _host = new Host();

        private class ReportCashForecast
        {
            public string ClientName { get; set; }
            public string FundName { get; set; }
            public string Keterangan { get; set; }
            public string Date { get; set; }
            public string INAT { get; set; }


        }
        public class NAVHarian
        {
            public string Date { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }
            public decimal NAV { get; set; }

        }

        public class ReportAccuedInterestDeposito
        {

            public string FundName { get; set; }
            public string BankID { get; set; }
            public string BICode { get; set; }
            public string Branch { get; set; }
            public decimal Nominal { get; set; }
            public string TradeDate { get; set; }
            public string MaturityDate { get; set; }
            public decimal Rate { get; set; }
            public string InterestPaymentType { get; set; }
            public decimal GrossDailyInterest { get; set; }
            public decimal GrossTotalAccruedInterest { get; set; }
            public decimal NetDailyInterest { get; set; }
            public decimal NetTotalAccruedInterest { get; set; }

        }

        public class ReportSettlementListingSummary
        {
            //Equity
            public DateTime TransactionDate { get; set; }
            public DateTime SettlementDate { get; set; }
            public string Fund { get; set; }
            public string BuySell { get; set; }
            public string SecurityCode { get; set; }
            public decimal Quantity { get; set; }
            public decimal PriceShares { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal BrokerageFee { get; set; }
            public decimal VAT { get; set; }
            public decimal Levy { get; set; }
            public decimal SalesTax { get; set; }
            public decimal TotalBeforeTax { get; set; }
            public decimal BrokerageFeeTax { get; set; }
            public decimal TotalPayment { get; set; }
            public string Broker { get; set; }
            public decimal AccruedInterest { get; set; }
            public string InstrumentTypeDesc { get; set; }
            public decimal TaxOnAccruedInterest { get; set; }
            public decimal TaxOnCapitalGain { get; set; }

        }

        public class KYCRiskProfile
        {
            public string JenisClient { get; set; }
            public string ClientName { get; set; }
            public string JenisPekerjaan { get; set; }
            public string TempatTinggal { get; set; }
            public string RiskProfile { get; set; }
            public string Fund { get; set; }
            public decimal Unit { get; set; }
            public decimal NAV { get; set; }
            public decimal Amount { get; set; }

        }

        public class BrokerCommisionSummary
        {
            public string BrokerID { get; set; }
            public string BrokerName { get; set; }
            public decimal Fee { get; set; }
            public decimal NetFee { get; set; }
            public decimal TotalTrading { get; set; }
            public decimal Trading { get; set; }
            public decimal Commission { get; set; }
            public decimal NetCommission { get; set; }
            public decimal Fee1 { get; set; }
            public string Date { get; set; }

        }


        public class ReportPortfolioSummary
        {
            public string Date { get; set; }
            public string Fund { get; set; }
            public string FundName { get; set; }
            public string InstrumentTypeName { get; set; }
            public decimal BeginningCashPosition { get; set; }
            public decimal CashPosition { get; set; }
            public decimal OutstandingReceiveables { get; set; }
            public decimal OutstandingLiabilities { get; set; }
            public decimal CashRemaining { get; set; }
            public string InstrumentID { get; set; }
            public decimal TotalPortfolio { get; set; }
            public decimal PercentPortfolio { get; set; }
            public decimal PercentGainLoss { get; set; }
            public string SectorID { get; set; }
            public string Issuer { get; set; }
            public decimal ToPortfolio { get; set; }
            public decimal CostValue { get; set; }
            public decimal MarketValue { get; set; }
            public decimal GainLoss { get; set; }
            public decimal TotalVolume { get; set; }
            public decimal CostPrice { get; set; }
            public decimal MarketPrice { get; set; }
            public decimal CostValueBySector { get; set; }
            public decimal MarketValueBySector { get; set; }
            public decimal TotalNAB { get; set; }
            public decimal PercentGainLossBySector { get; set; }


        }

        public class InvoiceManagementFee
        {
            public DateTime Date { get; set; }
            public string InstrumentID { get; set; }
            public string InstrumentName { get; set; }
            public int Type { get; set; }
            public decimal Amount { get; set; }
            public int FundTypeInternal { get; set; }

        }

        public class PortfolioBalanceReport
        {
            public string TransactionType { get; set; }
            public string SecuritiesCODE { get; set; }
            public string SecuritiesDescription { get; set; }
            public string BrokerName { get; set; }
            public string FundName { get; set; }
            public decimal HoldingPosition { get; set; }
            public decimal QtyOfUnit { get; set; }
            public decimal DealPrice { get; set; }
            public decimal MarketPrice { get; set; }
            public string TradeDate { get; set; }
            public string SettlementDate { get; set; }

        }

        public class ClientExpenseVsClientRevenue
        {
            public string ClientName { get; set; }
            public decimal ClientExpenseAmount { get; set; }
            public decimal ClientRevenueAmount { get; set; }
        }

        public class NAVAUMUnit
        {
            public DateTime Date { get; set; }
            public string FundName { get; set; }
            public decimal AUM { get; set; }
            public decimal Unit { get; set; }
            public decimal NAVPerUnit { get; set; }

        }

        public class DraftPerformanceReport
        {
            public string FundName { get; set; }
            public string Year { get; set; }
            public string Type { get; set; }
            public DateTime TradeDate { get; set; }
            public DateTime DueDate { get; set; }
            public decimal NetAmount { get; set; }
            public decimal NAVSubsRed { get; set; }
            public decimal NetUnit { get; set; }
            public decimal EndingUnit { get; set; }
            public decimal TargetPerformacePercent { get; set; }
            public decimal TargetNAV { get; set; }
            public decimal TargetAmount { get; set; }
            public decimal TargetPerformanceAmount { get; set; }
            public decimal CurrentNAV { get; set; }
            public decimal DueDateNAV { get; set; }
            public decimal PerformanceFee { get; set; }

        }

        public class DailyDealBoard
        {

            public string BanKID { get; set; }
            public string BankName { get; set; }
            public string FundName { get; set; }
            public string FundCode { get; set; }
            public decimal Equity { get; set; }
            public decimal Bond { get; set; }
            public decimal Deposito { get; set; }

        }

        public class PerformanceReport
        {
            public string FundName { get; set; }
            public string ClientName { get; set; }
            public string CIF { get; set; }
            public string Year { get; set; }
            public string Type { get; set; }
            public DateTime TradeDate { get; set; }
            public decimal NetAmount { get; set; }
            public decimal NAVSubsRed { get; set; }
            public decimal NetUnit { get; set; }
            public decimal EndingUnit { get; set; }
            public decimal CurrentNAV { get; set; }


        }


        public string FundClient_GenerateNewClientID(int _clientCategory, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 							
                                
                        Declare @ClientCategory int
                        Declare @CompanyType int
                        Declare @Code  nvarchar(100)   
                        Declare @NewClientID  nvarchar(100)    
                        Declare @MaxClientID  int


                        select @ClientCategory = ClientCategory,@CompanyType = Tipe from FundClient where FundClientPK = @FundClientPK and status = 1
                        select @MaxClientID =  isnull(max(convert(int,right(ID,4))),0)  + 1 from FundClient where  status in (1,2) and FundClientPK > 0 and ClientCategory = @ClientCategory
                        set @MaxClientID = isnull(@MaxClientID,1)
                        IF @ClientCategory = 1
                        BEGIN
	                        select @Code = 'RJ'
                        END
                        ELSE
                        BEGIN
	                        select @Code = 'IJ'
                        END        
					
                        declare @LENdigit int

                        select @LENdigit = LEN(@maxClientID) 
						
                        If @LENdigit = 1
                        BEGIN
                        set @NewClientID =   @Code + '000' + CAST(@MaxClientID as nvarchar) 
                        END
                        If @LENdigit = 2
                        BEGIN
                        set @NewClientID =   @Code + '00' + CAST(@MaxClientID as nvarchar) 
                        END
                        If @LENdigit = 3
                        BEGIN
                        set @NewClientID =   @Code + '0' + CAST(@MaxClientID as nvarchar) 
                        END
                        If @LENdigit = 4
                        BEGIN
                        set @NewClientID =  @Code + CAST(@MaxClientID as nvarchar) 
                        END
                      

                        Select @NewClientID NewClientID
                       ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["NewClientID"]);
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

        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {

            #region NAV Harian
            if (_FundAccountingRpt.ReportName.Equals("NAV Harian"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }



                            cmd.CommandText = @"
                            select A.date Date,B.ID FundID,B.Name FundNAme,A.Nav NAV from closenav A
                            left join Fund B on A.fundPK = B.fundPK and B.status = 2
                            where A.Date between @DateFrom and @DateTo and A.status = 2 " + _paramFundFrom + @" 
                            order by A.FundPK, A.date ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "NAVHarian" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "NAVHarian" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NAV Harian");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<NAVHarian> rList = new List<NAVHarian>();
                                        while (dr0.Read())
                                        {

                                            NAVHarian rSingle = new NAVHarian();
                                            rSingle.Date = Convert.ToString(dr0["Date"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);

                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                orderby r.FundID ascending
                                                group r by new { r.FundID } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE FROM : ";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE TO       : ";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 2; ;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "Date";
                                            worksheet.Cells[incRowExcel, 3].Value = "Fund ID";
                                            worksheet.Cells[incRowExcel, 4].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 5].Value = "NAV";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.Date).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAV;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                            incRowExcel = incRowExcel + 2;
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 5];
                                        worksheet.Column(1).Width = 13;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 47;
                                        worksheet.Column(5).Width = 25;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 NAV Harian";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            #region Report Portfolio Summary
            else if (_FundAccountingRpt.ReportName.Equals("Report Portfolio Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }


                            cmd.CommandText = @"
                            
                            declare @maxDateFCP date
                            select @maxDateFCP = max(date) from CloseNAV where date <= @ValueDate and status = 2


                            select distinct A.FundPK,B.Name FundName ,isnull(A.AUM,0) TotalNAB from CloseNAV A
                            left join Fund B on A.FundPK = B.FundPK and B.status  = 2
                            where A.status in (1,2) and A.Date = @maxDateFCP and B.MaturityDate >= @valuedate
                            " + _paramFund;

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
                                    string filePath = Tools.ReportsPath + "ReportPortfolioSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ReportPortfolioSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "ReportPortfolioSummary";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ReportPortfolioSummary");

                                        int incRowExcel = 1;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToShortDateString(); // + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();                                                                 
                                        //worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ReportPortfolioSummary> rList = new List<ReportPortfolioSummary>();
                                        while (dr0.Read())
                                        {
                                            ReportPortfolioSummary rSingle = new ReportPortfolioSummary();
                                            rSingle.Fund = Convert.ToString(dr0["FundPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.TotalNAB = dr0["TotalNAB"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalNAB"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.Fund, r.TotalNAB } into rGroup
                                                     select rGroup;

                                        decimal _cash = 0;
                                        decimal _endCash = 0;
                                        decimal _cashRemain = 0;
                                        decimal _porto = 0;
                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            //worksheet.Cells[incRowExcel, 1].Value = "Bersama ini kami sampaikan data NAB dan dana kelolaan. ";
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Report Portfolio Summary";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "PT. Jarvis Aset Manajemen";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(rsHeader.Key.Fund);
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Beginning Cash Position";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_TotalCashAmountUntilParamDatePerFundPK(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateTo);
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            _cash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "Cash Position";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_TotalAccountBalanceByFundPK(rsHeader.Key.Fund, 2, _FundAccountingRpt.ValueDateTo);
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Outstanding Receiveables";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_OutstandingReceivableByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateTo); ;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            _endCash = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "Outstanding Liabilities";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_OutstandingPaymentByFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateTo);
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            _cashRemain = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "Cash Remaining";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Formula = "SUM(B" + _cash + ":B" + _endCash + ")";
                                            worksheet.Cells[incRowExcel, 2].Calculate();
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            _porto = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "Portfolio";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_TotalMarketValuePerFundPKByDate(rsHeader.Key.Fund, _FundAccountingRpt.ValueDateTo); ;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Total Portfolio";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Formula = "SUM(B" + _cashRemain + ":B" + _porto + ")";
                                            worksheet.Cells[incRowExcel, 2].Calculate();
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            _porto = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "Total NAB";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TotalNAB;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            int first = incRowExcel;

                                            int no = 1;


                                            incRowExcel = incRowExcel + 2;

                                            // ke 2

                                            using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                            {
                                                DbCon1.Open();
                                                using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                                {



                                                    cmd1.CommandText =

                                                      @"
                                                select Date,FundPK, sum(CostValue) CostValueBySector, sum(MarketValue) MarketValueBySector, sum(PercentGainLoss)/100 PercentGainLossBySector, isnull(SectorID,'') SectorID from (
                                                select FP.FundPK , FP.AvgPrice CostPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
                                                F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance TotalVolume,FP.CostValue CostValue,  
                                                FP.ClosePrice MarketPrice
                                                ,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
                                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) 
                                                else 0 end AccrualHarian
                                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate)
                                                * datediff(day,DATEADD(month, DATEDIFF(month, 0, @ValueDate), 0),@ValueDate )
                                                else FP.InterestPercent end Accrual
                                                ,FP.InterestPercent 
                                                ,FP.MarketValue MarketValue,
                                                sum(FP.MarketValue - FP.CostValue)Unrealised,case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum((FP.MarketValue / 
                                                case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav , sum(FP.Balance/100) Lot, case when I.InstrumentTypePK not in (5,6) then  sum((FP.MarketValue - FP.CostValue))/FP.CostValue * 100 else 0 end PercentGainLoss 
                                                ,isnull(O.SInvestID,'') SInvestID,isnull(O.Name,'') BankName,isnull(N.ID,'') BranchID,FP.AcqDate, isnull(Q.Name,'') SectorID
                                                from fundposition FP   
                                                left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status in (1,2)   
                                                left join Fund F on FP.FundPK = F.FundPK and F.status in (1,2)   
                                                left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status in (1,2)  
                                                left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status in (1,2)  
                                                left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType'
                                                left join BankBranch N on FP.BankBranchPK = N.BankBranchPK and N.status in (1,2)
                                                left join Bank O on N.BankPK = O.BankPK and O.status in (1,2)
                                                left join SubSector P on I.SectorPK = P.SubSectorPK and P.status in (1,2)
                                                left join Sector Q on P.SectorPK = Q.SectorPK and Q.status in (1,2)
                                                where FP.status in (1,2)  and FP.Date = @ValueDate and FP.FundPK = @FundPK
                                                group by FP.FundPK,Fp.AvgPrice,FP.Date,I.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
                                                FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,O.SInvestID,O.Name,N.ID,FP.AcqDate,Q.Name
                                                ) A
                                                Group by Date,FundPK,SectorID ";


                                                    cmd1.CommandTimeout = 0;
                                                    cmd1.Parameters.AddWithValue("@valueDate", _FundAccountingRpt.ValueDateTo);
                                                    cmd1.Parameters.AddWithValue("@FundPK", rsHeader.Key.Fund);
                                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                    {
                                                        if (dr1.HasRows)
                                                        {


                                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                            using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                            {


                                                                //ATUR DATA GROUPINGNYA DULU
                                                                List<ReportPortfolioSummary> rList1 = new List<ReportPortfolioSummary>();
                                                                while (dr1.Read())
                                                                {
                                                                    ReportPortfolioSummary rSingle1 = new ReportPortfolioSummary();
                                                                    rSingle1.Date = Convert.ToString(dr1["Date"]);
                                                                    rSingle1.Fund = Convert.ToString(dr1["FundPK"]);
                                                                    rSingle1.SectorID = Convert.ToString(dr1["SectorID"]);
                                                                    rSingle1.CostValueBySector = Convert.ToDecimal(dr1["CostValueBySector"]);
                                                                    rSingle1.MarketValueBySector = Convert.ToDecimal(dr1["MarketValueBySector"]);
                                                                    rSingle1.PercentGainLossBySector = Convert.ToDecimal(dr1["PercentGainLossBySector"]);
                                                                    rList1.Add(rSingle1);

                                                                }

                                                                var QueryByFundID1 =
                                                                             from r1 in rList1
                                                                             group r1 by new { } into rGroup1
                                                                             select rGroup1;


                                                                foreach (var rsHeader1 in QueryByFundID1)
                                                                {
                                                                    incRowExcel = incRowExcel + 2;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[incRowExcel, 1].Value = "Sector";
                                                                    worksheet.Cells[incRowExcel, 2].Value = "% to NAB";
                                                                    worksheet.Cells[incRowExcel, 3].Value = "Cost Value";
                                                                    worksheet.Cells[incRowExcel, 4].Value = "Market Value";
                                                                    worksheet.Cells[incRowExcel, 5].Value = "Gain/Loss %";


                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.BackgroundColor.SetColor(Color.White);
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                                    incRowExcel = incRowExcel + 1;

                                                                    int first1 = incRowExcel;

                                                                    int no1 = 1;

                                                                    foreach (var rsDetail1 in rsHeader1)
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail1.SectorID;
                                                                        worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 2].Formula = "SUM(" + rsDetail1.MarketValueBySector + ")/SUM(B" + _porto + ")";
                                                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00%";
                                                                        worksheet.Cells[incRowExcel, 2].Calculate();
                                                                        worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.CostValueBySector;
                                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail1.MarketValueBySector;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.PercentGainLossBySector;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00%";
                                                                        worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;


                                                                        incRowExcel++;
                                                                        no++;
                                                                    }
                                                                    int last = incRowExcel - 1;
                                                                    worksheet.Cells[incRowExcel, 1].Value = "TOTAL";
                                                                    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + first1.ToString() + ":D" + last.ToString() + ")";
                                                                    worksheet.Cells[incRowExcel, 4].Calculate();
                                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                                                    worksheet.Cells[incRowExcel, 2].Formula = "=D" + incRowExcel + "/B14";
                                                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00%";
                                                                    worksheet.Cells[incRowExcel, 2, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                                }

                                                            }
                                                        }
                                                    }

                                                }

                                            }

                                            incRowExcel = incRowExcel + 2;

                                            using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                            {
                                                DbCon2.Open();
                                                using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                                {



                                                    cmd2.CommandText =

                                                    @"

                                                select FP.FundPK , FP.AvgPrice CostPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
                                                F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance TotalVolume,FP.CostValue CostValue,  
                                                FP.ClosePrice MarketPrice, isnull(R.Name,'') Issuer
                                                ,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
                                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) 
                                                else 0 end AccrualHarian
                                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate)
                                                * datediff(day,DATEADD(month, DATEDIFF(month, 0, @ValueDate), 0),@ValueDate )
                                                else FP.InterestPercent end Accrual
                                                ,FP.InterestPercent 
                                                ,FP.MarketValue MarketValue,
                                                sum(FP.MarketValue - FP.CostValue)Unrealised,case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum((FP.MarketValue / 
                                                case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav , sum(FP.Balance/100) Lot, 
                                                case when I.InstrumentTypePK not in (5,6) then case when FP.CostValue = 0 then 0 else sum((FP.MarketValue - FP.CostValue))/FP.CostValue end else 0 end  PercentGainLoss 
                                                ,isnull(O.SInvestID,'') SInvestID,isnull(O.Name,'') BankName,isnull(N.ID,'') BranchID,FP.AcqDate, isnull(Q.Name,'') SectorID
                                                from fundposition FP   
                                                left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status in (1,2)   
                                                left join Fund F on FP.FundPK = F.FundPK and F.status in (1,2)   
                                                left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status in (1,2)  
                                                left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status in (1,2)  
                                                left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType'
                                                left join BankBranch N on FP.BankBranchPK = N.BankBranchPK and N.status in (1,2)
                                                left join Bank O on N.BankPK = O.BankPK and O.status in (1,2)
                                                left join SubSector P on I.SectorPK = P.SubSectorPK and P.status in (1,2)
                                                left join Sector Q on P.SectorPK = Q.SectorPK and Q.status in (1,2)
                                                LEFT JOIN Issuer R on I.IssuerPK = R.IssuerPK and R.Status in (1,2)
                                                where FP.status in (1,2)  and FP.Date = @ValueDate 
                                                and Fp.FundPK = @FundPK
                                                group by FP.FundPK,Fp.AvgPrice,FP.Date,I.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
                                                FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,O.SInvestID,O.Name,N.ID,FP.AcqDate,Q.Name,R.Name
                                                order by I.ID
                                                 ";


                                                    cmd2.CommandTimeout = 0;
                                                    cmd2.Parameters.AddWithValue("@valueDate", _FundAccountingRpt.ValueDateTo);
                                                    cmd2.Parameters.AddWithValue("@FundPK", rsHeader.Key.Fund);
                                                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                                    {
                                                        if (dr2.HasRows)
                                                        {


                                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                            using (ExcelPackage package2 = new ExcelPackage(excelFile))
                                                            {


                                                                //ATUR DATA GROUPINGNYA DULU
                                                                List<ReportPortfolioSummary> rList2 = new List<ReportPortfolioSummary>();
                                                                while (dr2.Read())
                                                                {
                                                                    ReportPortfolioSummary rSingle2 = new ReportPortfolioSummary();
                                                                    rSingle2.InstrumentID = dr2["InstrumentID"].ToString();
                                                                    rSingle2.InstrumentTypeName = dr2["InstrumentTypeName"].ToString();
                                                                    rSingle2.SectorID = dr2["SectorID"].ToString();
                                                                    rSingle2.TotalVolume = dr2["TotalVolume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["TotalVolume"]);
                                                                    rSingle2.CostPrice = dr2["CostPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["CostPrice"]);
                                                                    rSingle2.MarketPrice = dr2["MarketPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["MarketPrice"]);
                                                                    rSingle2.CostValue = dr2["CostValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["CostValue"]);
                                                                    rSingle2.MarketValue = dr2["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["MarketValue"]);
                                                                    rSingle2.PercentGainLoss = dr2["PercentGainLoss"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["PercentGainLoss"]);
                                                                    rSingle2.Issuer = dr2["Issuer"].ToString();
                                                                    rList2.Add(rSingle2);
                                                                }

                                                                var QueryByFundID2 =
                                                                             from r2 in rList2
                                                                             group r2 by new { r2.InstrumentTypeName } into rGroup2
                                                                             select rGroup2;



                                                                foreach (var rsHeader2 in QueryByFundID2)
                                                                {

                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[incRowExcel, 1].Value = "Portfolio";
                                                                    worksheet.Cells[incRowExcel, 2].Value = "Issuer";
                                                                    worksheet.Cells[incRowExcel, 3].Value = "Sector";
                                                                    worksheet.Cells[incRowExcel, 4].Value = "% to NAB";
                                                                    worksheet.Cells[incRowExcel, 5].Value = "% to Issuer";
                                                                    worksheet.Cells[incRowExcel, 6].Value = "Total Volume";
                                                                    worksheet.Cells[incRowExcel, 7].Value = "Cost Price";
                                                                    worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                                    worksheet.Cells[incRowExcel, 9].Value = "Cost Value";
                                                                    worksheet.Cells[incRowExcel, 10].Value = "Market Value";
                                                                    worksheet.Cells[incRowExcel, 11].Value = "% Gain/Loss";


                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 11;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Fill.BackgroundColor.SetColor(Color.White);
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;


                                                                    incRowExcel = incRowExcel + 1;

                                                                    int first2 = incRowExcel;

                                                                    int no2 = 1;

                                                                    foreach (var rsDetail2 in rsHeader2)
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail2.InstrumentID;
                                                                        worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail2.Issuer;
                                                                        worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail2.SectorID;
                                                                        worksheet.Cells[incRowExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(" + rsDetail2.MarketValue + ")/SUM(B" + _porto + ")";
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00%";
                                                                        worksheet.Cells[incRowExcel, 4].Calculate();
                                                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                                        if (rsDetail2.Issuer == "")
                                                                            worksheet.Cells[incRowExcel, 5].Value = 0;
                                                                        else
                                                                            worksheet.Cells[incRowExcel, 5].Formula = "SUMIF(B:B,B:B,D:D)";
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00%";
                                                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                                                        worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail2.TotalVolume;
                                                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail2.CostPrice;
                                                                        worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail2.MarketPrice;
                                                                        worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail2.CostValue;
                                                                        worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail2.MarketValue;
                                                                        worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00%";
                                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail2.PercentGainLoss;
                                                                        worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 11;


                                                                        incRowExcel++;
                                                                        no++;
                                                                    }
                                                                    int last = incRowExcel - 1;

                                                                    worksheet.Cells[incRowExcel, 9, incRowExcel, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[incRowExcel, 9].Value = "Total Market Value";
                                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + first2.ToString() + ":J" + last.ToString() + ")";
                                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                                    worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.Font.Bold = true;
                                                                    worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.Font.Size = 12;
                                                                    worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                                    incRowExcel++;

                                                                    incRowExcel = incRowExcel + 2;

                                                                }

                                                            }
                                                        }
                                                    }

                                                }

                                            }




                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        }







                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).AutoFit();
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 25;
                                        worksheet.Column(7).Width = 23;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).AutoFit();
                                        worksheet.Column(11).Width = 15;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 PORTFOLIO SUMMARY ";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

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
                    return false;
                    throw err;
                }
            }
            #endregion

            #region Broker Commission Summary
            else if (_FundAccountingRpt.ReportName.Equals("Broker Commission Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramCounterpart = "";

                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = " And A.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }


                            cmd.CommandText =

                            @"
DECLARE @CCounterpartPK int

DECLARE @CP TABLE
(
	CounterpartPK INT,
	Fee NUMERIC(18,8),
	NetFee NUMERIC(18,8)
)

Declare A Cursor For
	SELECT DISTINCT A.CounterpartPK FROM dbo.CounterpartCommission A WHERE A.status = 2
	  " + _paramCounterpart + @"
Open A
Fetch Next From A
INTO @CCounterpartPK
While @@FETCH_STATUS = 0  
Begin
	INSERT INTO @CP
	        ( CounterpartPK ,
			    NetFee ,
				 Fee
	        )
	SELECT CounterpartPK,CommissionPercent,ISNULL(CommissionPercent,0) 
	+ ISNULL(LevyPercent,0) + ISNULL(KPEIPercent,0) + ISNULL(VATPercent,0) 
	FROM dbo.CounterpartCommission WHERE CounterpartPK = @CCounterpartPK
	AND Date = 
	(
		SELECT MAX(date) FROM dbo.CounterpartCommission WHERE CounterpartPK = @CCounterpartPK AND
        Date  <= @DateTo AND status = 2 	AND BoardType = 1
	) AND status = 2 AND BoardType = 1
	Fetch Next From A 
	into @CCounterpartPK
End	
Close A
Deallocate A

DECLARE @AumTotal NUMERIC(26,4)

SET @AumTotal = 0

SELECT @AumTotal = SUM(ISNULL(A.DoneAmount,0)) FROM dbo.Investment A
LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.Status IN (1,2)
WHERE ValueDate BETWEEN @DateFrom AND @DateTo AND StatusSettlement = 2
AND  B.InstrumentTypePK IN 
(
1,4,16
)


SELECT 
ISNULL(C.ID,'') BrokerID
,ISNULL(C.Name,'') BrokerName
,ISNULL(D.Fee,0) Fee
,ISNULL(D.NetFee,0) NetFee
,SUM(ISNULL(A.DoneAmount,0)) TotalDoneAmount
,CASE WHEN @AumTotal > 0 THEN SUM(ISNULL(A.DoneAmount,0)) / @AumTotal ELSE 0 END PercentTrading
,SUM(ISNULL(A.CommissionAmount,0)) + SUM(ISNULL(A.LevyAmount,0)) + SUM(ISNULL(A.VATAmount,0)) + SUM(ISNULL(A.KPEIAmount,0)) Commission
,SUM(ISNULL(A.CommissionAmount,0)) NetCommission
,CASE WHEN @AumTotal > 0 THEN SUM(ISNULL(A.CommissionAmount,0)) / @AumTotal ELSE 0 END PercentNetCommission
FROM dbo.Investment A
LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
LEFT JOIN dbo.Counterpart C ON A.CounterpartPK = C.CounterpartPK AND C.status IN (1,2)
LEFT JOIN @CP D ON A.CounterpartPK = D.CounterpartPK

WHERE StatusSettlement = 2 AND B.InstrumentTypePK IN 
(
1,4,16
)AND A.ValueDate BETWEEN @DateFrom AND @DateTo  " + _paramCounterpart + @"

GROUP BY ISNULL(C.ID,''),ISNULL(C.Name,''),ISNULL(D.Fee,0) ,ISNULL(D.NetFee,0)
                             ";


                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "BrokerCommissionSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "BrokerCommissionSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BrokerCommisionSummary";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("BrokerCommisionSummary");

                                        int incRowExcel = 1;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToShortDateString(); // + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();                                                                 
                                        //worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BrokerCommisionSummary> rList = new List<BrokerCommisionSummary>();
                                        while (dr0.Read())
                                        {
                                            BrokerCommisionSummary rSingle = new BrokerCommisionSummary();
                                            rSingle.BrokerID = Convert.ToString(dr0["BrokerID"]);
                                            rSingle.BrokerName = Convert.ToString(dr0["BrokerName"]);
                                            rSingle.Fee = Convert.ToDecimal(dr0["Fee"]);
                                            rSingle.NetFee = Convert.ToDecimal(dr0["NetFee"]);
                                            rSingle.TotalTrading = Convert.ToDecimal(dr0["TotalDoneAmount"]);
                                            rSingle.Trading = Convert.ToDecimal(dr0["PercentTrading"]);
                                            rSingle.Commission = Convert.ToDecimal(dr0["Commission"]);
                                            rSingle.NetCommission = Convert.ToDecimal(dr0["NetCommission"]);
                                            rSingle.Fee1 = Convert.ToDecimal(dr0["PercentNetCommission"]);
                                            //rSingle.Date = Convert.ToString(dr0["Date"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            //worksheet.Cells[incRowExcel, 1].Value = "Bersama ini kami sampaikan data NAB dan dana kelolaan. ";
                                            //incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Broker Commission Summary";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "PT. Jarvis Aset Manajemen";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel = incRowExcel + 3;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "Broker ID";
                                            worksheet.Cells[incRowExcel, 2].Value = "Broker Name";
                                            worksheet.Cells[incRowExcel, 3].Value = "Fee";
                                            worksheet.Cells[incRowExcel, 4].Value = "Net Fee";
                                            worksheet.Cells[incRowExcel, 5].Value = "Total Trading";
                                            worksheet.Cells[incRowExcel, 6].Value = "% Trading";
                                            worksheet.Cells[incRowExcel, 7].Value = "Commission";
                                            worksheet.Cells[incRowExcel, 8].Value = "Net Commission";
                                            worksheet.Cells[incRowExcel, 9].Value = "% Fee";


                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Fill.BackgroundColor.SetColor(Color.White);
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BrokerID;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.BrokerName;
                                                worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Fee;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.NetFee;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.TotalTrading;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Trading;
                                                worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Commission;
                                                worksheet.Cells[incRowExcel, 7].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.NetCommission;
                                                worksheet.Cells[incRowExcel, 8].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Fee1 * 100;
                                                worksheet.Cells[incRowExcel, 9].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 11;


                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.Font.Size = 11;

                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 10;
                                        worksheet.Column(2).Width = 40;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 30;
                                        worksheet.Column(7).Width = 25;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 30;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 BROKER COMMISSION SUMMARY ";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

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
                    return false;
                    throw err;
                }
            }
            #endregion

            #region Report Cash Forecast
            else if (_FundAccountingRpt.ReportName.Equals("Report Cash Forecast"))
            {
                try
                {

                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        string _paramFund = "";

                        if (_FundAccountingRpt.FundFrom != "0")
                        {
                            _paramFund = " and FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";

                        }
                        else
                        {
                            _paramFund = "";
                        }

                        string pdfPath = Tools.ReportsPath + "ReportCashForecast" + "_" + _userID + ".pdf";
                        string filePath = Tools.ReportsPath + "ReportCashForecast" + "_" + _userID + ".xlsx";
                        FileInfo excelFile = new FileInfo(filePath);
                        if (excelFile.Exists)
                        {
                            excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                            excelFile = new FileInfo(filePath);
                        }
                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                        using (ExcelPackage package = new ExcelPackage(excelFile))
                        {
                            package.Workbook.Properties.Title = "FundAccountingReport";
                            package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                            package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                            package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                            package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                            package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ReportCashForecast");

                            DbCon.Open();
                            using (SqlCommand cmd001 = DbCon.CreateCommand())
                            {
                                cmd001.CommandText = @"
                                                    select distinct FundPK from Fund where status = 2 and MaturityDate > @Date
						    " + _paramFund;
                                cmd001.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateTo);

                                using (SqlDataReader dr02 = cmd001.ExecuteReader())
                                {

                                    int incRowExcel;
                                    int _endRow = 0;
                                    int _endRowZ = 0;
                                    int incColExcel = 1;
                                    incRowExcel = 1;
                                    int _startRowDetail = incRowExcel;
                                    int _rowCtrp = 0;
                                    int _endRowDetail = 0;
                                    int _endColDetail = 0;
                                    int _totalRow;

                                    if (!dr02.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        List<FundAccountingRpt> rList = new List<FundAccountingRpt>();
                                        while (dr02.Read())
                                        {

                                            FundAccountingRpt rSingle = new FundAccountingRpt();

                                            //rSingle.No = Convert.ToDecimal(dr0["No"]);
                                            rSingle.FundPK = Convert.ToInt32(dr02["FundPK"]);

                                            rList.Add(rSingle);

                                        }


                                        var GroupByReference =
                                                from r in rList
                                                orderby r.FundPK ascending
                                                group r by new { r.FundPK } into rGroup
                                                select rGroup;

                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            using (SqlConnection DbCon01 = new SqlConnection(Tools.conString))
                                            {
                                                DbCon.Close();
                                                DbCon.Open();
                                                using (SqlCommand cmd = DbCon.CreateCommand())
                                                {
                                                    cmd.CommandText = @"  
                            
--DECLARE @Date DATETIME
--DECLARE @FundPK int

--SET @Date = '02/19/19'
--SET @FundPK = 2

--DROP TABLE #FinalResult

DECLARE @DateTo DATETIME

SET @DateTo = DATEADD(DAY,7,@Date)

Declare @DateMinOne datetime

set @DateMinOne = dbo.FWorkingDay(@Date,-1)

DECLARE @Result TABLE
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME
)

DECLARE @CounterDate datetime
SET @CounterDate = @Date


WHILE @CounterDate <= @DateTo
BEGIN

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 1,'Cash Position',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 2,'Placement Time Deposit',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 3,'Time Deposit Mature',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 4,'Settle Buy Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 5,'Settle Sell Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 6,'Settle Buy Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 7,'Settle Sell Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 8,'Settle Buy Reksadana',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 9,'Settle Sell Reksadana',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 10,'Subscription',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 11,'Redemption',@CounterDate

	SET @CounterDate = DATEADD(DAY,1,@CounterDate)
END

CREATE TABle #FinalResult 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME,
	Balance NUMERIC(22,4),
	FundPK INT,
	FundID NVARCHAR(100),
	FundName NVARCHAR(200)
)

INSERT INTO #FinalResult
        ( Position, Name, Date, Balance, FundPK )

SELECT A.Position,A.Name,A.Date,ISNULL(B.TotalAmount,0) Balance,@FundPK 
FROM @Result A

LEFT JOIN 
(

SELECT A.Position,A.ValueDate,SUM(ISNULL(A.totalAmount,0)) TotalAmount FROM
	(
		
		
-- DEPOSITO BUY
		SELECT 2 Position,ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL
	
-- DEPOSITO MATURED
		SELECT 3 Position,case when dbo.CheckTodayIsHoliday(MaturityDate) = 1 then dbo.FWorkingDay(MaturityDate,1) else MaturityDate end ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND MaturityDate > @DateMinOne AND MaturityDate <= @DateTo
		AND FundPK = @FundPK
        AND InstrumentPK NOT IN
        (
            select InstrumentPK from Investment where MaturityDate > @DateMinOne AND MaturityDate <= @DateTo and StatusSettlement = 2 and TrxType = 2 AND FundPK = @FundPK AND InstrumentTypePK = 5
        )


		UNION ALL
	
-- DEPOSITO BREAK
		SELECT 3 Position,ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 2
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- EQUITY BUY
		SELECT 4 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 1
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- EQUITY SELL
		SELECT 5 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

	UNION ALL

-- BOND BUY
		SELECT 6 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 1
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- BOND SELL
		SELECT 7 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- REKSADANA BUY
		SELECT 8 Position,ValueDate,DoneAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 6
		AND TrxType in (1)
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL

-- REKSADANA SELL
		SELECT 9 Position,SettlementDate,DoneAmount TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 6
		AND TrxType in (2)
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL

-- Subscription
		SELECT 10 Position,A.ValueDate,CashAmount TotalAmount FROM dbo.ClientSubscription A
		WHERE status <> 3 
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- Redemption
		SELECT 11 Position,A.PaymentDate,CASE when CashAmount > 0 THEN A.CashAmount * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPK) * -1 END TotalAmount
		FROM dbo.ClientRedemption A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK and TotalCashAmount>0

	)A
	GROUP BY A.Position,A.ValueDate
)B ON A.Date = B.ValueDate AND A.Position = B.Position


DECLARE @FirstCashDate DATETIME
SET @FirstCashDate = dbo.FWorkingDay(@Date,-1)


UPDATE #FinalResult
SET Balance = dbo.FGetGroupAccountFundJournalBalanceByFundPK(@FirstCashDate,2,@FundPK)
WHERE Date = @date AND Position = 1



DECLARE @TotalBalance NUMERIC(22,4)


SET @CounterDate = @Date
WHILE @CounterDate <= @DateTo
BEGIN
	SET @TotalBalance = 0
		
	SELECT @TotalBalance = SUM(ISNULL(Balance,0)) FROM #FinalResult
	WHERE Date = @CounterDate

	UPDATE #FinalResult SET Balance = @TotalBalance
	WHERE Position = 1 AND Date = dateadd(DAY,1,@CounterDate)
	
	SET @CounterDate = DATEADD(DAY,1,@CounterDate)
END


UPDATE A SET A.FundID = B.ID,A.FundName = B.Name FROM #FinalResult A
left JOIN Fund B ON A.FUndPK = B.FundPK AND B.status IN (1,2)



DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(CONVERT(NVARCHAR(12),Date,107) ) +',0) ' + QUOTENAME(CONVERT(NVARCHAR(12),Date,107) ) 
                    from (SELECT DISTINCT Date FROM #FinalResult) A
					order by A.Date
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(CONVERT(NVARCHAR(12),Date,107)) 
                    from #FinalResult
				
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

set @query = 'SELECT Name Keterangan,' + @colsForQuery + ' from 
                                (
                                SELECT Position,Name,Balance,FundPK,FundID,FundName,CONVERT(NVARCHAR(12),Date,107) Date FROM #FinalResult 
                            ) x 
                            pivot 
                            (
                                SUM(Balance)
                                for Date  in (' + @cols + ')
                            ) p 
			                order by Position asc
			                '
                exec(@query)		

                            ";
                                                    cmd.CommandTimeout = 0;
                                                    cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateTo);
                                                    cmd.Parameters.AddWithValue("@FundPK", rsHeader.Key.FundPK);

                                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                                    {
                                                        if (!dr0.HasRows)
                                                        {
                                                            return false;
                                                        }
                                                        else
                                                        {



                                                            worksheet.Cells[incRowExcel, 1].Value = "Report Cash Forecast";
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 14;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Column(incRowExcel).Width = 25;
                                                            incRowExcel++;
                                                            worksheet.Cells[incRowExcel, 1].Value = "PT. Jarvis Asset Manajemen";
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 12;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Column(incRowExcel).Width = 20;
                                                            incRowExcel++;
                                                            worksheet.Cells[incRowExcel, 1].Value = "As of :";
                                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Column(incRowExcel).Width = 20;
                                                            incRowExcel++;
                                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_FundName(rsHeader.Key.FundPK.ToString());
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.Font.Size = 14;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.Font.Bold = true;
                                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Column(incRowExcel).Width = 20;



                                                            incColExcel = 1;
                                                            // ini buat header
                                                            incRowExcel++;
                                                            for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                                            {
                                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                                                worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetName(inc1).ToString();
                                                                worksheet.Column(incRowExcel).Width = 20;
                                                                worksheet.Cells[incRowExcel, incColExcel].Style.Font.Bold = true;
                                                                incColExcel++;
                                                            }
                                                            incRowExcel++;
                                                            while (dr0.Read())
                                                            {
                                                                incColExcel = 1;

                                                                for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                                                {
                                                                    _rowCtrp = incColExcel;
                                                                    //_endRow = dr0.FieldCount + 2;
                                                                    //_endRowZ = dr0.FieldCount + 1;
                                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1);
                                                                    worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                                                    worksheet.Column(incRowExcel).Width = 20;
                                                                    incColExcel++;


                                                                }


                                                                incRowExcel++;
                                                            }





                                                            using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                                            {
                                                                DbCon1.Open();
                                                                using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                                                {


                                                                    cmd1.CommandText = @"  
DECLARE @DateTo DATETIME

SET @DateTo = DATEADD(DAY,7,@Date)


Declare @DateMinOne datetime

set @DateMinOne = dbo.FWorkingDay(@Date,-1)


DECLARE @Result TABLE
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME
)

DECLARE @CounterDate datetime
SET @CounterDate = @Date


WHILE @CounterDate <= @DateTo
BEGIN

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 1,'Cash Position',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 2,'Placement Time Deposit',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 3,'Time Deposit Mature',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 4,'Settle Buy Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 5,'Settle Sell Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 6,'Settle Buy Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 7,'Settle Sell Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 8,'Settle Buy Reksadana',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 9,'Settle Sell Reksadana',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 10,'Subscription',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 11,'Redemption',@CounterDate

	SET @CounterDate = DATEADD(DAY,1,@CounterDate)
END

CREATE TABle #FinalResult 
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME,
	Balance NUMERIC(22,4),
	FundPK INT,
	FundID NVARCHAR(100),
	FundName NVARCHAR(200)
)

INSERT INTO #FinalResult
        ( Position, Name, Date, Balance, FundPK )

SELECT A.Position,A.Name,A.Date,ISNULL(B.TotalAmount,0) Balance,@FundPK 
FROM @Result A

LEFT JOIN 
(

SELECT A.Position,A.ValueDate,SUM(ISNULL(A.totalAmount,0)) TotalAmount FROM
	(
		
		
-- DEPOSITO BUY
		SELECT 2 Position,ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL
	
	
-- DEPOSITO MATURED
		SELECT 3 Position,case when dbo.CheckTodayIsHoliday(MaturityDate) = 1 then dbo.FWorkingDay(MaturityDate,1) else MaturityDate end ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND MaturityDate > @DateMinOne AND MaturityDate <= @DateTo
		AND FundPK = @FundPK
        AND InstrumentPK NOT IN
        (
            select InstrumentPK from Investment where MaturityDate > @DateMinOne AND MaturityDate <= @DateTo and StatusSettlement = 2 and TrxType = 2 AND FundPK = @FundPK AND InstrumentTypePK = 5
        )


		UNION ALL
	
-- DEPOSITO BREAK
		SELECT 3 Position,ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 2
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- EQUITY BUY
		SELECT 4 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 1
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- EQUITY SELL
		SELECT 5 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

	    UNION ALL

-- BOND BUY
		SELECT 6 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 1
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- BOND SELL
		SELECT 7 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 2
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

        UNION ALL

-- REKSADANA BUY
		SELECT 8 Position,ValueDate,DoneAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 6
		AND TrxType in (1)
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL

-- REKSADANA SELL
		SELECT 9 Position,SettlementDate,DoneAmount TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 6
		AND TrxType in (2)
		AND SettlementDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK
        

		UNION ALL
-- Subscription
		SELECT 8 Position,A.ValueDate,CashAmount TotalAmount FROM dbo.ClientSubscription A
		WHERE status <> 3 
		AND ValueDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK

		UNION ALL
-- Redemption
		SELECT 9 Position,A.PaymentDate,CASE when CashAmount > 0 THEN A.CashAmount * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPK) * -1 END TotalAmount
		FROM dbo.ClientRedemption A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @Date AND @DateTo
		AND FundPK = @FundPK and TotalCashAmount>0

	)A
	GROUP BY A.Position,A.ValueDate
)B ON A.Date = B.ValueDate AND A.Position = B.Position


DECLARE @FirstCashDate DATETIME
SET @FirstCashDate = dbo.FWorkingDay(@Date,-1)


UPDATE #FinalResult
SET Balance = dbo.FGetGroupAccountFundJournalBalanceByFundPK(@FirstCashDate,2,@FundPK)
WHERE Date = @date AND Position = 1



DECLARE @TotalBalance NUMERIC(22,4)


SET @CounterDate = @Date
WHILE @CounterDate <= @DateTo
BEGIN
	SET @TotalBalance = 0
		
	SELECT @TotalBalance = SUM(ISNULL(Balance,0)) FROM #FinalResult
	WHERE Date = @CounterDate

	UPDATE #FinalResult SET Balance = @TotalBalance
	WHERE Position = 1 AND Date = dateadd(DAY,1,@CounterDate)
	
	SET @CounterDate = DATEADD(DAY,1,@CounterDate)
END


UPDATE A SET A.FundID = B.ID,A.FundName = B.Name FROM #FinalResult A
left JOIN Fund B ON A.FUndPK = B.FundPK AND B.status IN (1,2)



DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(CONVERT(NVARCHAR(12),Date,107) ) +',0) ' + QUOTENAME(CONVERT(NVARCHAR(12),Date,107) ) 
                    from (SELECT DISTINCT Date FROM #FinalResult) A
					order by A.Date
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(CONVERT(NVARCHAR(12),Date,107)) 
                    from #FinalResult
				
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

set @query = 'SELECT ' + @colsForQuery + ' from 
                                (
                                SELECT sum(Balance) Balance,FundPK,FundID,FundName,CONVERT(NVARCHAR(12),Date,107) Date FROM #FinalResult 
								group by FundPK,FundID,FundName,CONVERT(NVARCHAR(12),Date,107)
                            ) x 
                            pivot 
                            (
                                SUM(Balance)
                                for Date  in (' + @cols + ')
                            ) p 
			                
			                '
                exec(@query)

                            ";
                                                                    cmd1.CommandTimeout = 0;
                                                                    cmd1.Parameters.AddWithValue("@FundPK", rsHeader.Key.FundPK);
                                                                    cmd1.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateTo);

                                                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                                    {
                                                                        if (!dr1.HasRows)
                                                                        {
                                                                            return false;
                                                                        }
                                                                        else
                                                                        {


                                                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                                            using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                                            {

                                                                                // ini buat header

                                                                                while (dr1.Read())
                                                                                {
                                                                                    incColExcel = 2;
                                                                                    worksheet.Cells[incRowExcel, 1].Value = "TOTAL : ";
                                                                                    worksheet.Cells[incRowExcel, incColExcel].Style.Font.Bold = true;
                                                                                    for (int inc1 = 0; inc1 < dr1.FieldCount; inc1++)
                                                                                    {
                                                                                        _rowCtrp = incColExcel;
                                                                                        //_endRow = dr0.FieldCount + 2;
                                                                                        //_endRowZ = dr0.FieldCount + 1;
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Value = dr1.GetValue(inc1);
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                                                                        worksheet.Column(incRowExcel).Width = 20;
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Font.Bold = true;
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                                                                        incColExcel++;


                                                                                    }

                                                                                }


                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                            }




                                                            //}
                                                        }
                                                        incRowExcel++;
                                                        incRowExcel++;
                                                        incRowExcel++;
                                                        incRowExcel++;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                            //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                            worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                            //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 REVENUE PER SALES DETAIl";

                            // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                            worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                            worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                            worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                            worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                            //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                            Image img = Image.FromFile(Tools.ReportImage);
                            worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                            worksheet.HeaderFooter.OddFooter.RightAlignedText =
                            string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                            //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                            worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                            package.Save();
                            if (_FundAccountingRpt.DownloadMode == "PDF")
                            {
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
                            }
                            return true;
                        }
                    }

                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion

            #region AUM Harian
            else if (_FundAccountingRpt.ReportName.Equals("AUM Harian"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            //string _paramCounterpart = "";

                            //if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            //{
                            //    _paramCounterpart = " And A.CounterpartPK in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";

                            //}
                            //else
                            //{
                            //    _paramCounterpart = "";
                            //}

                            cmd.CommandText = @"  
                            
                            --DECLARE @DateFrom DATETIME
                            --DECLARE @DateTo DATETIME

                            --DROP TABLE #result

                            --SET @DateFrom = '02/02/19'
                            --SET @DateTo = '02/28/19'

                            CREATE TABLE #Result
                            (
	                            Name NVARCHAR(200),
	                            AUM NUMERIC(22,4),
	                            Date datetime
                            )

                            INSERT INTO #Result
                                    ( Name, AUM, Date )

                            SELECT B.Name,A.AUM,A.Date FROM CloseNAV A
                            LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
                            WHERE A.Status = 2 
                            and Date BETWEEN @DateFrom AND @DateTo


                            DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
                                @query  AS NVARCHAR(MAX)

                            select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(Name) +',0) ' + QUOTENAME(Name) 
                                                from (SELECT DISTINCT Name FROM #Result) A
					                            order by A.Name
                                        FOR XML PATH(''), TYPE
                                        ).value('.', 'NVARCHAR(MAX)') 
                                    ,1,1,'')

                            select @cols = STUFF((SELECT distinct ',' + QUOTENAME(Name) 
                                                from #Result
				
                                        FOR XML PATH(''), TYPE
                                        ).value('.', 'NVARCHAR(MAX)') 
                                    ,1,1,'')


                            set @query = 'SELECT Date,' + @colsForQuery + ' from 
                                                            (
                                                            SELECT Name,Date,aum FROM #Result
                                                        ) x 
                                                        pivot 
                                                        (
                                                            SUM(aum)
                                                            for Name  in (' + @cols + ')
                                                        ) p 
			                                            order by Date asc
			                                            '
                                            exec(@query)			

                            ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string pdfPath = Tools.ReportsPath + "AUMHarian" + "_" + _userID + ".pdf";
                                    string filePath = Tools.ReportsPath + "AUMHarian" + "_" + _userID + ".xlsx";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }



                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("AUM Harian");
                                        int incRowExcel;
                                        int _endRow = 0;
                                        int _endRowZ = 0;
                                        int incColExcel = 1;
                                        incRowExcel = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _rowCtrp = 0;
                                        int _endRowDetail = 0;
                                        int _endColDetail = 0;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT. Jarvis Asset Manajemen";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Column(incRowExcel).Width = 30;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "DATE FROM  :";
                                        worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateFrom;
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Column(incRowExcel).Width = 30;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "DATE TO   : ";
                                        worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Column(incRowExcel).Width = 30;
                                        //incRowExcel = incRowExcel + 2;
                                        //worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(_FundAccountingRpt.FundFrom);
                                        //worksheet.Cells[incRowExcel, 2, incRowExcel, 9].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 2, incRowExcel, 9].Style.Font.Bold = true;
                                        //worksheet.Cells["B" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        // ini buat header
                                        incRowExcel++; ;
                                        for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                        {

                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetName(inc1).ToString();
                                            worksheet.Column(incRowExcel).Width = 30;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Font.Bold = true;
                                            incColExcel++;
                                        }
                                        incRowExcel++;
                                        while (dr0.Read())
                                        {
                                            incColExcel = 1;

                                            for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                            {
                                                _rowCtrp = incColExcel;
                                                //_endRow = dr0.FieldCount + 2;
                                                //_endRowZ = dr0.FieldCount + 1;
                                                if (incColExcel == 1)
                                                {
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1);
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1);
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                                }
                                                worksheet.Column(incRowExcel).Width = 30;

                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                incColExcel++;


                                            }

                                            //if (_rowCtrp > 2)
                                            //{
                                            //    worksheet.Cells[_endRow, _rowCtrp].Formula = "SUM(" + _host.GetAlphabet(_rowCtrp) + _startRowDetail + ":" + _host.GetAlphabet(_rowCtrp) + incColExcel + ")";
                                            //    worksheet.Cells[_endRow, incColExcel].Style.Font.Bold = true;
                                            //    worksheet.Cells[_endRow, _rowCtrp].Style.Numberformat.Format = "#,##0";
                                            //    worksheet.Cells[_endRow, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells[_endRow, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells[_endRow, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //    worksheet.Cells[_endRow, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //    //worksheet.Cells[16, _rowCtrp].Formula = "SUM(" + _host.GetAlphabet(_rowCtrp) + _startRowDetail + ":" + _host.GetAlphabet(_rowCtrp) + "14) * 100 /" + _totalAmount;
                                            //    worksheet.Cells[_endRow, incColExcel].Style.Font.Bold = true;
                                            //    worksheet.Cells[_endRow, _rowCtrp].Style.Numberformat.Format = "#,##0.00";
                                            //}

                                            worksheet.Cells[4, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[4, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[4, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[4, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            _endColDetail = incColExcel - 1;
                                            _endRowDetail = incRowExcel - 1;
                                            worksheet.Cells[4, incColExcel].Value = "Total AUM";
                                            worksheet.Cells[4, incColExcel].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, incColExcel].Formula = "SUM(B" + incRowExcel + ":" + _host.GetAlphabet(_endColDetail) + incRowExcel + ")";
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Font.Bold = true;
                                            worksheet.Column(incColExcel).Width = 30;
                                   

                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            //worksheet.Cells[15, incColExcel].Value = _totalAmount;
                                            worksheet.Cells[15, incColExcel].Style.Numberformat.Format = "#,##0";
                                            incRowExcel++;
                                        }


                                        incRowExcel = incRowExcel + 2;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 REVENUE PER SALES DETAIl";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        return true;
                                    }
                                }
                            }

                        }
                    }

                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion

            #region Report Accued Interest Deposito
            if (_FundAccountingRpt.ReportName.Equals("Report Accued Interest Deposito"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }



                            cmd.CommandText = @"
                            declare @ParamInstrument table
                            (
                            FundPK int,
                            InstrumentPK int,
                            InterestPaymentType int,
                            AcqDate datetime
                            )

                            declare @TotalAccruedInterest table
                            (
                            FundPK int,
                            InstrumentPK int,
                            Amount numeric(18,4),
                            InterestPaymentType int
                            )

                            insert into @ParamInstrument
                            select distinct A.FundPK,A.InstrumentPK,A.InterestPaymentType,case when A.InterestPaymentType <> 7 then A.AcqDate else  
                            case when DATEPART(DAY,A.MaturityDate) >= DATEPART(DAY,@DateTo) then  cast(cast(DATEPART(mm, DATEADD(mm,-1, @DateTo)) as nvarchar(2)) + '/' + case when cast(DATEPART(DAY,A.MaturityDate) as nvarchar(2)) = 31 then cast(DATEPART(DAY,EOMONTH (@DateTo, -1)) as nvarchar(2)) else cast(DATEPART(DAY,A.MaturityDate) as nvarchar(2)) end + '/' + cast(DATEPART(YEAR,@DateTo) as nvarchar(4)) as datetime)  
                            else cast(cast(DATEPART(mm, DATEADD(mm,0, @DateTo)) as nvarchar(2)) + '/' + case when cast(DATEPART(DAY,A.MaturityDate) as nvarchar(2)) = 31 then cast(DATEPART(DAY,EOMONTH (@DateTo, -1)) as nvarchar(2)) else cast(DATEPART(DAY,A.MaturityDate) as nvarchar(2)) end + '/' + cast(DATEPART(YEAR,@DateTo) as nvarchar(4)) as datetime) END END
                            from FundPosition A
                            left join instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
                            where date = @DateTo and B.InstrumentTypePK = 5  " + _paramFundFrom + @" and B.InstrumentTypePK = 5



                            insert into @TotalAccruedInterest
                            select A.FundPK,A.InstrumentPK,sum(BaseDebit-BaseCredit) * - 1 Amount,C.InterestPaymentType from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2
                            left join @ParamInstrument C on A.InstrumentPK = C.InstrumentPK and A.FundPK = C.FundPK
                            where FundJournalAccountPK = 122 and B.Posted = 1 and B.Reversed = 0 and Valuedate between C.AcqDate and @DateTo
                            group by A.FundPK,A.InstrumentPK,C.InterestPaymentType


                            select A.InstrumentPK,A.InterestDaysType, G.Name FundName, 
                            case when A.InterestDaysType = 4 then A.Balance * A.InterestPercent/100/365 else A.Balance * A.InterestPercent/100/360  end  GrossDailyInterest,isnull(F.Amount,0) TotalAccruedInterest,
                            case when A.InterestDaysType = 4 then A.Balance * A.InterestPercent/100/365 * 0.8 else A.Balance * A.InterestPercent/100/360 * 0.8  end  NetDailyInterest,isnull(F.Amount,0) * 0.8 NetAccruedInterest,
                            D.ID TimeDeposit,D.BICode,'Bank' +' '+ D.ID + ' ' + 'Cab.' +' '+ C.ID Branch,
                            A.Balance Nominal,A.AcqDate TradeDate,A.MaturityDate MaturityDate,A.InterestPercent Rate,
                            isnull(E.DescOne,'') InterestPaymentType
                             from FundPosition A
                            left join instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            left join BankBranch C on A.BankBranchPK = C.BankBranchPK and C.Status = 2
                            left join Bank D on C.BankPK = D.BankPK and D.Status = 2
                            left join MasterValue E on A.InterestPaymentType = E.Code and E.ID = 'InterestPaymentType' and E.status = 2
                            left join @TotalAccruedInterest F on A.FundPK = F.FundPK and A.InstrumentPK = F.InstrumentPK and A.InterestPaymentType = F.InterestPaymentType
                            left join Fund G on A.fundPK = G.fundPK and G.status in (1,2)
                            where date = @DateTo and B.InstrumentTypePK = 5 and A.status = 2 " + _paramFundFrom + @" and B.InstrumentTypePK = 5
                             ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ReportAccuedInterestDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ReportAccuedInterestDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report Accued Interest Deposito");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ReportAccuedInterestDeposito> rList = new List<ReportAccuedInterestDeposito>();
                                        while (dr0.Read())
                                        {

                                            ReportAccuedInterestDeposito rSingle = new ReportAccuedInterestDeposito();

                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.BankID = Convert.ToString(dr0["TimeDeposit"]);
                                            rSingle.BICode = Convert.ToString(dr0["BICode"]);
                                            rSingle.Branch = Convert.ToString(dr0["Branch"]);
                                            rSingle.Nominal = Convert.ToDecimal(dr0["Nominal"]);
                                            rSingle.TradeDate = Convert.ToString(dr0["TradeDate"]);
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"]);
                                            rSingle.Rate = Convert.ToDecimal(dr0["Rate"]);
                                            rSingle.InterestPaymentType = Convert.ToString(dr0["InterestPaymentType"]);
                                            rSingle.GrossDailyInterest = Convert.ToDecimal(dr0["GrossDailyInterest"]);
                                            rSingle.GrossTotalAccruedInterest = Convert.ToDecimal(dr0["TotalAccruedInterest"]);
                                            rSingle.NetDailyInterest = Convert.ToDecimal(dr0["NetDailyInterest"]);
                                            rSingle.NetTotalAccruedInterest = Convert.ToDecimal(dr0["NetAccruedInterest"]);

                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                orderby r.FundName ascending
                                                group r by new { r.FundName } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE TO       : ";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 2; ;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "TIME DEPOSIT";
                                            worksheet.Cells[incRowExcel, 3].Value = "BI CODE";
                                            worksheet.Cells[incRowExcel, 4].Value = "BRANCH";
                                            worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                            worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                            worksheet.Cells[incRowExcel, 8].Value = "Rate (Gross)";
                                            worksheet.Cells[incRowExcel, 9].Value = "Interest Payment Type";
                                            worksheet.Cells[incRowExcel, 10].Value = "Gross Daily Interest";
                                            worksheet.Cells[incRowExcel, 11].Value = "Gross Total Accrued Interest ";
                                            worksheet.Cells[incRowExcel, 12].Value = "Net Daily Interest";
                                            worksheet.Cells[incRowExcel, 13].Value = "Net Total Accrued Interest";


                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.BankID;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BICode;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Branch;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Nominal;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail.TradeDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Rate;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.InterestPaymentType;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.GrossDailyInterest;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.GrossTotalAccruedInterest;
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.NetDailyInterest;
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.NetTotalAccruedInterest;
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                            incRowExcel = incRowExcel + 2;
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 13];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 20;
                                        worksheet.Column(13).Width = 20;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 NAV Harian";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            #region Report Settlement Listing Summary
            if (_FundAccountingRpt.ReportName.Equals("Report Settlement Listing Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {




                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }


                            cmd.CommandText = @"
                            select ValueDate TransactionDate,SettlementDate,B.ID Fund,case when TrxType = 1 then 'B' when TrxType = 2 then 'S' else '' end BuySell,C.ID SecurityCode,A.DoneVolume Quantity,A.DonePrice PriceShares,
                            A.DoneAmount GrossAmount,A.CommissionAmount BrokerageFee,A.VATAmount VAT,A.LevyAmount Levy,A.IncomeTaxSellAmount SalesTax,A.DoneAmount +A.CommissionAmount + A.VATAmount + A.LevyAmount + A.IncomeTaxSellAmount TotalBeforeTax,
                            A.WHTAmount BrokerageFeeTax,isnull(D.ID,'') Broker,A.AccruedInterest,A.IncomeTaxInterestAmount TaxAccruedinterest, A.IncomeTaxGainAmount TaxCapitalGain,case when A.InstrumentTypePK = 1 then 'EQUITY' when A.InstrumentTypePK in (2,3,5,9,13,15) then 'BOND' else '' end InstrumentTypeDesc,
                            case when A.InstrumentTypePK = 1 then A.DoneVolume + A.AccruedInterest - A.IncomeTaxGainAmount - A.IncomeTaxInterestAmount when A.InstrumentTypePk in (2,3,9,13,15) then A.DoneAmount +A.CommissionAmount + A.VATAmount + A.LevyAmount + A.IncomeTaxSellAmount - A.WHTAmount end TotalPayment
                            from Investment A
                            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2)
                            left join Counterpart D on A.CounterpartPK = D.CounterpartPK and D.Status in (1,2)
                            where ValueDate between @datefrom and @dateto and StatusInvestment = 2 and StatusDealing = 2 and StatusSettlement = 2 and A.InstrumentTypePK in (1,2,3,9,13,15) " + _paramFundFrom;

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);




                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ReportSettlementListingSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ReportSettlementListingSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Umum");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ReportSettlementListingSummary> rList = new List<ReportSettlementListingSummary>();
                                        while (dr0.Read())
                                        {

                                            ReportSettlementListingSummary rSingle = new ReportSettlementListingSummary();
                                            rSingle.TransactionDate = Convert.ToDateTime(dr0["TransactionDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.Fund = Convert.ToString(dr0["Fund"]);
                                            rSingle.BuySell = Convert.ToString(dr0["BuySell"]);
                                            rSingle.SecurityCode = Convert.ToString(dr0["SecurityCode"]);
                                            rSingle.Quantity = Convert.ToDecimal(dr0["Quantity"]);
                                            rSingle.PriceShares = Convert.ToDecimal(dr0["PriceShares"]);
                                            rSingle.GrossAmount = Convert.ToDecimal(dr0["GrossAmount"]);
                                            rSingle.BrokerageFee = Convert.ToDecimal(dr0["BrokerageFee"]);
                                            rSingle.VAT = Convert.ToDecimal(dr0["VAT"]);
                                            rSingle.Levy = Convert.ToDecimal(dr0["Levy"]);
                                            rSingle.SalesTax = Convert.ToDecimal(dr0["SalesTax"]);
                                            rSingle.TotalBeforeTax = Convert.ToDecimal(dr0["TotalBeforeTax"]);
                                            rSingle.BrokerageFeeTax = Convert.ToDecimal(dr0["BrokerageFeeTax"]);
                                            rSingle.TotalPayment = Convert.ToDecimal(dr0["TotalPayment"]);
                                            rSingle.AccruedInterest = Convert.ToDecimal(dr0["AccruedInterest"]);
                                            rSingle.TaxOnAccruedInterest = Convert.ToDecimal(dr0["TaxAccruedInterest"]);
                                            rSingle.TaxOnCapitalGain = Convert.ToDecimal(dr0["TaxCapitalGain"]);
                                            rSingle.InstrumentTypeDesc = Convert.ToString(dr0["InstrumentTypeDesc"]);
                                            rSingle.Broker = Convert.ToString(dr0["Broker"]);
                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                        from r in rList
                                        orderby r.InstrumentTypeDesc descending
                                        group r by new { r.Fund, r.InstrumentTypeDesc } into rGroup
                                        select rGroup;

                                        int incRowExcel = 0;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Settlement Listing Summary";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "From: ";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "To: ";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");

                                        incRowExcel++;

                                        foreach (var rsHeader in GroupByReference)
                                        {



                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "No.";
                                            worksheet.Cells[incRowExcel, 2].Value = "Transaction Date";
                                            worksheet.Cells[incRowExcel, 3].Value = "Settlement Date";
                                            worksheet.Cells[incRowExcel, 4].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 5].Value = "B / S";
                                            worksheet.Cells[incRowExcel, 6].Value = "Security Code";
                                            worksheet.Cells[incRowExcel, 7].Value = "Quantity";
                                            worksheet.Cells[incRowExcel, 8].Value = "Price/Shares";
                                            if (rsHeader.Key.InstrumentTypeDesc == "EQUITY")
                                            {
                                                worksheet.Cells[incRowExcel, 9].Value = "Gross Amount";
                                                worksheet.Cells[incRowExcel, 10].Value = "Brokerage Fee (IDR)";
                                                worksheet.Cells[incRowExcel, 11].Value = "VAT (IDR)";
                                                worksheet.Cells[incRowExcel, 12].Value = "Levy (IDR)";
                                                worksheet.Cells[incRowExcel, 13].Value = "Sales Tax (IDR)";
                                                worksheet.Cells[incRowExcel, 14].Value = "Tot.Before Tax";
                                                worksheet.Cells[incRowExcel, 15].Value = "Brokerage Fee Tax 2%";
                                                worksheet.Cells[incRowExcel, 16].Value = "Total Payment (IDR)";
                                                worksheet.Cells[incRowExcel, 17].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 9].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 10].Value = "Tax on Accrued Interest";
                                                worksheet.Cells[incRowExcel, 11].Value = "Tax on Capital Gain";
                                                worksheet.Cells[incRowExcel, 12].Value = "Total Payment (IDR)";
                                                worksheet.Cells[incRowExcel, 13].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }


                                            incRowExcel++;
                                            int RowB = incRowExcel;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;


                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //incRowExcel++;
                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.TransactionDate;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BuySell;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.SecurityCode;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Quantity;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.PriceShares;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                if (rsHeader.Key.InstrumentTypeDesc == "EQUITY")
                                                {

                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.GrossAmount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.BrokerageFee;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.VAT;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.Levy;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.SalesTax;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.TotalBeforeTax;
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.BrokerageFeeTax;
                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.TotalPayment;
                                                    worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 16].Formula = "N" + incRowExcel + "-O" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 16].Calculate();
                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.Broker;
                                                    worksheet.Cells[incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    //worksheet.Cells[incRowExcel, 1, incRowExcel, 17].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.AccruedInterest;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.TaxOnAccruedInterest;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TaxOnCapitalGain;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.TotalPayment;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.Broker;
                                                    worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                    //worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Font.Bold = true;

                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }


                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;





                                            }

                                            if (rsHeader.Key.InstrumentTypeDesc == "EQUITY")
                                                worksheet.Cells["A" + _endRowDetail + ":Q" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            else
                                                worksheet.Cells["A" + _endRowDetail + ":M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        }

                                        worksheet.Row(incRowExcel).PageBreak = true;

                                        string _rangeDetail = "A:E";

                                        using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Size = 11;
                                            r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 15];

                                        worksheet.Column(1).Width = 10;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(4).AutoFit();
                                        worksheet.Column(5).AutoFit();
                                        worksheet.Column(6).AutoFit();
                                        worksheet.Column(7).AutoFit();
                                        worksheet.Column(8).AutoFit();
                                        worksheet.Column(9).AutoFit();
                                        worksheet.Column(10).AutoFit();
                                        worksheet.Column(11).AutoFit();
                                        worksheet.Column(12).AutoFit();
                                        worksheet.Column(13).AutoFit();
                                        worksheet.Column(14).AutoFit();
                                        worksheet.Column(15).AutoFit();
                                        worksheet.Column(16).AutoFit();
                                        worksheet.Column(17).AutoFit();
                                        //worksheet.Column(10).AutoFit();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Rekap Transaksi Harian";



                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            #region Portfolio Balance Report
            if (_FundAccountingRpt.ReportName.Equals("Portfolio Balance Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            string _paramInstrumentFrom = "";

                            if (!_host.findString(_FundAccountingRpt.InstrumentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.InstrumentFrom))
                            {
                                _paramInstrumentFrom = " And A.InstrumentPK  in ( " + _FundAccountingRpt.InstrumentFrom + " ) ";
                            }
                            else
                            {
                                _paramInstrumentFrom = "";
                            }

                            string _paramCounterpartFrom = "";

                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpartFrom = " And A.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpartFrom = "";
                            }


                            cmd.CommandText =

                            @"
                            --declare @DateFrom datetime
                            --declare @DateTo datetime
                            --declare @FundPK int
                            --declare @CounterpartPK int
                            --declare @InstrumentPK int
                            --set @DateFrom = '03/09/20'
                            --set @DateTo = '04/30/20'
                            --set @FundPK = 51
                            --set @CounterpartPK = 65
                            --set @InstrumentPK = 21



                            declare @FundPosition table
                            (

                            FundPK int,
                            InstrumentPK int,
                            InstrumentID nvarchar(50),
                            Balance numeric(18,4),
                            Date datetime

                            )

                            insert into @FundPosition
                            select A.FundPK,A.InstrumentPK,B.ID InstrumentID,A.Balance, A.Date from FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status in (1,2)
                            where C.GroupType = 1 " + _paramFundFrom + _paramInstrumentFrom +
                            @" and A.date between dbo.FWorkingDay(@DateFrom,-1) and @DateTo 
                            and A.status = 2
                            order by A.date



                            select A.FundPK,A.InstrumentPK,A.TrxTypeID TransactionType,B.ID SecuritiesCODE,B.Name SecuritiesDescription,D.Name BrokerName,F.ID FundName, isnull(G.Balance,0) HoldingPosition,
                            isnull(A.DoneVolume,0) QtyOfUnit,isnull(A.DonePrice,0) DealPrice,isnull(E.ClosePriceValue,0) MarketPrice,InstructionDate TradeDate,SettlementDate from Investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status in (1,2)
                            left join Counterpart D on A.CounterpartPK = D.CounterpartPK and D.status in (1,2)
                            left join ClosePrice E on A.InstrumentPK = E.InstrumentPK and E.status = 2 and E.Date = A.InstructionDate
                            left join Fund F on A.FundPK = F.FundPK and F.status = 2
                            left join @FundPosition G on A.InstrumentPK = G.InstrumentPK and G.Date = dbo.FWorkingDay(A.InstructionDate,-1)  and A.FundPK = G.FundPK
                            where A.ValueDate between @DateFrom and @DateTo and C.GroupType = 1 " + _paramFundFrom + _paramInstrumentFrom + _paramCounterpartFrom +
                            @" and A.StatusSettlement = 2                            
                            order by A.InstrumentPK,A.InstructionDate ";


                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "PortfolioBalanceReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PortfolioBalanceReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PortfolioBalanceReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("PortfolioBalanceReport");

                                        int incRowExcel = 1;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToShortDateString(); // + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();                                                                 
                                        //worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PortfolioBalanceReport> rList = new List<PortfolioBalanceReport>();
                                        while (dr0.Read())
                                        {
                                            PortfolioBalanceReport rSingle = new PortfolioBalanceReport();
                                            rSingle.TransactionType = Convert.ToString(dr0["TransactionType"]);
                                            rSingle.SecuritiesCODE = Convert.ToString(dr0["SecuritiesCODE"]);
                                            rSingle.SecuritiesDescription = Convert.ToString(dr0["SecuritiesDescription"]);
                                            rSingle.BrokerName = Convert.ToString(dr0["BrokerName"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.HoldingPosition = Convert.ToDecimal(dr0["HoldingPosition"]);
                                            rSingle.QtyOfUnit = Convert.ToDecimal(dr0["QtyOfUnit"]);
                                            rSingle.DealPrice = Convert.ToDecimal(dr0["DealPrice"]);
                                            rSingle.MarketPrice = Convert.ToDecimal(dr0["MarketPrice"]);
                                            rSingle.TradeDate = Convert.ToString(dr0["TradeDate"]);
                                            rSingle.SettlementDate = Convert.ToString(dr0["SettlementDate"]);


                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.FundName } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            //worksheet.Cells[incRowExcel, 1].Value = "Bersama ini kami sampaikan data NAB dan dana kelolaan. ";
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 1].Value = "Portfolio Balance Report";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From :";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("MM/dd/yyyy");
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To      :";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("MM/dd/yyyy");

                                            incRowExcel = incRowExcel + 2;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "Transaction Type";
                                            worksheet.Cells[incRowExcel, 3].Value = "Securities CODE";
                                            worksheet.Cells[incRowExcel, 4].Value = "Securities Description";
                                            worksheet.Cells[incRowExcel, 5].Value = "Broker Name";
                                            worksheet.Cells[incRowExcel, 6].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 7].Value = "Holding Position";
                                            worksheet.Cells[incRowExcel, 8].Value = "Qty Of Unit";
                                            worksheet.Cells[incRowExcel, 9].Value = "Holding Portfolio";
                                            worksheet.Cells[incRowExcel, 10].Value = "Deal Price";
                                            worksheet.Cells[incRowExcel, 11].Value = "Market Price";
                                            worksheet.Cells[incRowExcel, 12].Value = "Trade Date";
                                            worksheet.Cells[incRowExcel, 13].Value = "Settlement Date";


                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Row(incRowExcel).Height = 28;
                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.TransactionType;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecuritiesCODE;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.SecuritiesDescription;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BrokerName;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.HoldingPosition;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.QtyOfUnit;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                if (rsDetail.TransactionType == "BUY" || rsDetail.TransactionType == "Buy")
                                                {
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.HoldingPosition + rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                }

                                                else if (rsDetail.TransactionType == "SELL" || rsDetail.TransactionType == "Sell")
                                                {
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.HoldingPosition - rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                }

                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.DealPrice;
                                                worksheet.Cells[incRowExcel, 10].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.MarketPrice;
                                                worksheet.Cells[incRowExcel, 11].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 12].Value = Convert.ToDateTime(rsDetail.TradeDate).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 12].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 13].Value = Convert.ToDateTime(rsDetail.SettlementDate).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 13].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Font.Size = 12;


                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + first.ToString() + ":I" + last.ToString() + ")";
                                            //worksheet.Cells[incRowExcel, 9].Calculate();
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + first.ToString() + ":J" + last.ToString() + ")";
                                            //worksheet.Cells[incRowExcel, 10].Calculate();

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Font.Size = 12;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            ////worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            ////worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            ////worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            ////worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 2;
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 13];
                                        worksheet.Column(1).Width = 11;
                                        worksheet.Column(2).Width = 17;
                                        worksheet.Column(3).Width = 16;
                                        worksheet.Column(4).Width = 40;
                                        worksheet.Column(5).Width = 40;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 19;
                                        worksheet.Column(8).Width = 19;
                                        worksheet.Column(9).Width = 19;
                                        worksheet.Column(10).Width = 19;
                                        worksheet.Column(11).Width = 19;
                                        worksheet.Column(12).Width = 20;
                                        worksheet.Column(13).Width = 20;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 REPORT DAILY NAV DAN AUM ";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

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
                    return false;
                    throw err;
                }
            }
            #endregion

            #region NAVAUMUnit
            if (_FundAccountingRpt.ReportName.Equals("NAV AUM Unit Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            //string _paramFundFrom = "";

                            //if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            //{
                            //    _paramFundFrom = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            //}
                            //else
                            //{
                            //    _paramFundFrom = "";
                            //}



                            cmd.CommandText = @"
select A.Date Date,B.Name FundName,A.AUM,A.Nav NAVPerUnit,C.Unit  from closeNAV A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join (
select sum(UnitAmount) Unit, FundPK,dbo.FWorkingDay(date,1) Date from FundClientPosition where Date between dbo.FWorkingDay(@DateFrom,-1) and dbo.FWorkingDay(@DateTo,-1)
group by fundPK, date
) C on A.FundPK = C.FundPK and A.Date = C.Date
where A.date between @DateFrom and @DateTo and A.status = 2
order by A.date asc
                             ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "NAVAUMUnitReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "NAVAUMUnitReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NAV,AUM, & Unit Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<NAVAUMUnit> rList = new List<NAVAUMUnit>();
                                        while (dr0.Read())
                                        {

                                            NAVAUMUnit rSingle = new NAVAUMUnit();
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.FundName = dr0["FundName"].ToString();
                                            rSingle.AUM = Convert.ToDecimal(dr0["AUM"]);
                                            rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                            rSingle.NAVPerUnit = Convert.ToDecimal(dr0["NAVPerUnit"]);

                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { r.Date } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName(); ;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date From    :";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date To         :";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo);
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "Date";
                                            worksheet.Cells[incRowExcel, 2].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 3].Value = "AUM";
                                            worksheet.Cells[incRowExcel, 4].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 5].Value = "NAV/Unit";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.Date;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AUM;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Unit;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVPerUnit;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                no++;
                                                _endRowDetail = incRowExcel;


                                            }
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "Total AUM :";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 3].Calculate();
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;



                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 5];
                                        worksheet.Column(1).Width = 30;
                                        worksheet.Column(2).Width = 30;
                                        worksheet.Column(3).Width = 21;
                                        worksheet.Column(4).Width = 21;
                                        worksheet.Column(5).Width = 21;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Report Equity Summary";


                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        //worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }

                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            #region NAVAUMUnit Per Fund
            if (_FundAccountingRpt.ReportName.Equals("NAV AUM Unit Report Per Fund"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }



                            cmd.CommandText = @"
select A.Date Date,B.Name FundName,A.AUM,A.Nav NAVPerUnit,C.Unit  from closeNAV A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join (
select sum(UnitAmount) Unit, FundPK,dbo.FWorkingDay(date,1) Date from FundClientPosition where Date between dbo.FWorkingDay(@DateFrom,-1) and dbo.FWorkingDay(@DateTo,-1)
group by fundPK, date
) C on A.FundPK = C.FundPK and A.Date = C.Date
where A.date between @DateFrom and @DateTo and A.status = 2 " + _paramFundFrom +
@"order by A.date asc
                             ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "NAVAUMUnitReportPerFund" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "NAVAUMUnitReportPerFund" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NAV,AUM, & Unit Report Per Fund");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<NAVAUMUnit> rList = new List<NAVAUMUnit>();
                                        while (dr0.Read())
                                        {

                                            NAVAUMUnit rSingle = new NAVAUMUnit();
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.FundName = dr0["FundName"].ToString();
                                            rSingle.AUM = Convert.ToDecimal(dr0["AUM"]);
                                            rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                            rSingle.NAVPerUnit = Convert.ToDecimal(dr0["NAVPerUnit"]);

                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { r.FundName } into rGroup
                                                select rGroup;

                                        int _date;
                                        int incRowExcel = 0;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName(); ;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date From    :";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        _date = incRowExcel;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date To         :";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo);
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        _date = incRowExcel;


                                        foreach (var rsHeader in GroupByReference)
                                        {





                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "Date";
                                            worksheet.Cells[incRowExcel, 2].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 3].Value = "AUM";
                                            worksheet.Cells[incRowExcel, 4].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 5].Value = "NAV/Unit";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundName;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AUM;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Unit;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVPerUnit;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                no++;
                                                _endRowDetail = incRowExcel;


                                            }

                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;



                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 5];
                                        worksheet.Column(1).Width = 30;
                                        worksheet.Column(2).Width = 21;
                                        worksheet.Column(3).Width = 21;
                                        worksheet.Column(4).Width = 21;
                                        worksheet.Column(5).Width = 21;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Report Equity Summary";


                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        //worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }

                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            #region Daily Deal Board
            else if (_FundAccountingRpt.ReportName.Equals("Daily Deal Board"))
            {
                try
                {
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _nameSheet = "";
                            string _paramBank = "";

                            if (!_host.findString(_FundAccountingRpt.BankFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.BankFrom))
                            {
                                _paramBank = "And D.BankPK in ( " + _FundAccountingRpt.BankFrom + " ) ";
                            }
                            else
                            {
                                _paramBank = "";
                            }

                            cmd.CommandText = @"select distinct D.ID BanKID from Investment A
                            left join fund B on A.fundPK = B.FundPK and B.status in (1,2)
                            left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.status in (1,2)
                            left join Bank D on C.BankPK = D.BankPK and D.Status = 2
                            where A.ValueDate = @Date " + _paramBank;


                            cmd.CommandTimeout = 0;
                            //cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@date", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyDealBoard" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyDealBoard" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "DailyDealBoard";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = null;

                                        int incRowExcel = 5;



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyDealBoard> rList = new List<DailyDealBoard>();
                                        while (dr0.Read())
                                        {
                                            DailyDealBoard rSingle = new DailyDealBoard();

                                            rSingle.BanKID = Convert.ToString(dr0["BanKID"]);


                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                                        from r in rList
                                                            //orderby r.Code ascending
                                                        group r by new { r.BanKID } into rGroup
                                                        select rGroup;


                                        //int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            worksheet = package.Workbook.Worksheets.Add(rsHeader.Key.BanKID);

                                            //query select detail
                                            //incRowExcel = 1;

                                            using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                            {

                                                DbCon1.Close();
                                                DbCon1.Open();
                                                using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                                {

                                                    _nameSheet = rsHeader.Key.BanKID;


                                                    cmd1.CommandText =
                                                    @"
                                                    --declare @Date datetime
                                                    --declare @FundPK int

                                                    --set @Date = '01/22/2019'
                                                    --set @FundPk = 1


                                                    --drop table #Ending

declare @YesterdayDateFP date
set @YesterdayDateFP =  dbo.FWorkingDay(@Date,-1) 

DECLARE @table TABLE
(
FundPK INT,	
TotalSentEquity INT,
TotalSentBond INT,
TotalSentDeposito INT
)

create table #Ending
(
BankID nvarchar(50),
BankName nvarchar(50),
FundCode nvarchar(50),
FundName nvarchar(100),
Equity numeric(18,2),
Bond numeric(18,2),
Deposito numeric(18,2),
)



DECLARE @totalTrx INT
DECLARE @FundPK INT
SET @totalTrx = 0
---------------------Equity-------------
SELECT @totalTrx =  COUNT(InvestmentPK), @FundPK = A.FundPK FROM dbo.Investment A WHERE StatusSettlement = 2 
AND ValueDate = @Date
AND InstrumentTypePK IN (1,4,16)

--AND FundPK = @FundPK
AND TrxType in (1,2)
group by A.FundPK


INSERT INTO @table
( FundPK, TotalSentEquity,TotalSentBond,TotalSentDeposito )
SELECT @FundPK,@totalTrx,0,0
SET @totalTrx = 0

-----------------Fixed Income---------------

SELECT @totalTrx=COUNT(InvestmentPK), @FundPK = A.FundPK FROM dbo.Investment A WHERE StatusSettlement = 2 
AND ValueDate = @Date
AND InstrumentTypePK IN (2,3,12,13,15)

--AND FundPK = @FundPK
AND TrxType in (1,2)
Group by A.FundPK

INSERT INTO @table
( FundPK, TotalSentEquity,TotalSentBond,TotalSentDeposito )
SELECT @FundPK,0,@totalTrx,0
SET @totalTrx = 0


------------------------------------------Money Market------------------
select @totalTrx=COUNT(A.InvestmentPK), @FundPK = A.FundPK from(
SELECT InvestmentPK,FundPK FROM dbo.Investment A WHERE StatusSettlement = 2 
AND ValueDate = @Date
AND InstrumentTypePK IN (5) --Depo Placement

--AND FundPK = @FundPK
AND TrxType in (1,2,3)

union all
-----------------------------------------------------------
SELECT A.InstrumentPK,FundPK FROM dbo.FundPosition A
LEFT JOIN Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
LEFT JOIN (
select TrxBuy,InstrumentPK FROM dbo.Investment A WHERE StatusSettlement = 2 
AND ValueDate = @Date
AND InstrumentTypePK IN (5) 

--AND FundPK = @FundPK
AND TrxType = 3
)C on A.[Identity] = C.TrxBuy
where A.status = 2 and A.date = @YesterdayDateFP 
--AND A.fundPK = @FundPK 
and A.MaturityDate = @Date --Depo Mature
AND B.InstrumentTypePK IN (5)
AND C.InstrumentPK is null

)A
Group by A.FundPK

INSERT INTO @table
( FundPK, TotalSentEquity,TotalSentBond,TotalSentDeposito )
SELECT @FundPK,0,0,@totalTrx
SET @totalTrx = 0


insert into #Ending(BankID,BankName,FundCode,FundName,Equity,Bond,Deposito)
select D.ID BankID,D.Name BankName,B.SInvestCode FundCode, B.Name FundName,sum(TotalSentEquity) Equity,sum(TotalSentBond) Bond,sum(TotalSentDeposito) Deposito from @Table A
left join fund B on A.FundPK = B.fundPK and B.status in (1,2) 
left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.status = 2
left join Bank D on C.BankPK = D.BankPK and D.Status = 2
--where D.ID = @BanKID
group by B.Name, B.SInvestCode,D.ID,D.Name


select * from #Ending where  BankID = @BanKID
                                                     ";


                                                    cmd1.CommandTimeout = 0;
                                                    cmd1.Parameters.AddWithValue("@date", _FundAccountingRpt.ValueDateTo);
                                                    cmd1.Parameters.AddWithValue("@BanKID", rsHeader.Key.BanKID);


                                                    //set data detail

                                                    using (SqlDataReader dr01 = cmd1.ExecuteReader())
                                                    {
                                                        if (!dr01.HasRows)
                                                        {
                                                            return false;
                                                        }
                                                        else
                                                        {

                                                            //ATUR DATA GROUPINGNYA DULU
                                                            List<DailyDealBoard> rList1 = new List<DailyDealBoard>();
                                                            while (dr01.Read())
                                                            {
                                                                DailyDealBoard rSingle1 = new DailyDealBoard();

                                                                rSingle1.BanKID = Convert.ToString(dr01["BanKID"]);
                                                                rSingle1.BankName = Convert.ToString(dr01["BankName"]);
                                                                rSingle1.FundCode = Convert.ToString(dr01["FundCode"]);
                                                                rSingle1.FundName = Convert.ToString(dr01["FundName"]);
                                                                rSingle1.Equity = Convert.ToDecimal(dr01["Equity"]);
                                                                rSingle1.Bond = Convert.ToDecimal(dr01["Bond"]);
                                                                rSingle1.Deposito = Convert.ToDecimal(dr01["Deposito"]);


                                                                rList1.Add(rSingle1);

                                                            }

                                                            var GroupByReference1 =
                                                                            from r1 in rList1
                                                                                //orderby r1.Urutan ascending
                                                                            group r1 by new { r1.BankName } into r1Group
                                                                            select r1Group;


                                                            foreach (var rsHeader1 in GroupByReference1)
                                                            {
                                                                incRowExcel++;


                                                                worksheet.Cells[incRowExcel, 1].Value = "Daily Deal Board";
                                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Merge = true;
                                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                                incRowExcel = incRowExcel + 2;
                                                                worksheet.Cells[incRowExcel, 1].Value = "Date : ";
                                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd MMMM yyyy");
                                                                incRowExcel++;
                                                                worksheet.Cells[incRowExcel, 1].Value = "To. ";
                                                                worksheet.Cells[incRowExcel, 2].Value = rsHeader1.Key.BankName;
                                                                incRowExcel++;
                                                                worksheet.Cells[incRowExcel, 1].Value = "Attn. ";
                                                                worksheet.Cells[incRowExcel, 2].Value = "Fund Services Team";
                                                                incRowExcel = incRowExcel + 2;
                                                                worksheet.Cells[incRowExcel, 1].Value = "We confirmed today having sent you the following instruction(s)";
                                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Merge = true;
                                                                incRowExcel = incRowExcel + 2;

                                                                int RowA = incRowExcel;
                                                                int RowB = incRowExcel + 1;

                                                                if (_nameSheet == "DEUTSCHE")
                                                                {

                                                                    //worksheet.Cells[incRowExcel, 2].Value = "Rincian Portofolio Deposito";
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Bold = true;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(130, 197, 250));
                                                                    //incRowExcel++;
                                                                    //worksheet.Cells[incRowExcel, 2].Value = rsHeader1.Key.Name;
                                                                    //worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Bold = true;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(248, 196, 126));
                                                                    incRowExcel++;

                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells[RowA, 1, RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    //worksheet.Cells[RowA, 1, RowB, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                                    worksheet.Cells[RowA, 1].Value = "No";
                                                                    worksheet.Cells[RowA, 1].Style.Font.Bold = true;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Merge = true;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowA, 2].Value = "Fund Code";
                                                                    worksheet.Cells[RowA, 2].Style.Font.Bold = true;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Merge = true;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowA, 3].Value = "Fund Name";
                                                                    worksheet.Cells[RowA, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Merge = true;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowA, 4].Value = "No. of Transaction";
                                                                    worksheet.Cells[RowA, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Merge = true;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[RowB, 4].Value = "Equity";
                                                                    worksheet.Cells[RowB, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowB + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowB + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowB, 5].Value = "Fixed Income";
                                                                    worksheet.Cells[RowB, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowB + ":E" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowB + ":E" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowB, 6].Value = "Time Deposit";
                                                                    worksheet.Cells[RowB, 6].Style.Font.Bold = true;
                                                                    worksheet.Cells["F" + RowB + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["F" + RowB + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                                                    //incRowExcel++;
                                                                }
                                                                else if (_nameSheet == "BCA")
                                                                {


                                                                    //worksheet.Cells[incRowExcel, 2].Value = "Rincian Portofolio Deposito";
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Bold = true;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(130, 197, 250));
                                                                    //incRowExcel++;
                                                                    //worksheet.Cells[incRowExcel, 2].Value = rsHeader1.Key.Name;
                                                                    //worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Bold = true;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(248, 196, 126));
                                                                    incRowExcel++;

                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells[RowA, 1, RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    //worksheet.Cells[RowA, 1, RowB, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                                    worksheet.Cells[incRowExcel, 1].Value = "No";
                                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Merge = true;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 2].Value = "Fund Code";
                                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Merge = true;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 3].Value = "Fund Name";
                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Merge = true;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 4].Value = "No. of Transaction";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Merge = true;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[RowB, 4].Value = "Equity";
                                                                    worksheet.Cells[RowB, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowB + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowB + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowB, 5].Value = "Fixed Income";
                                                                    worksheet.Cells[RowB, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowB + ":E" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowB + ":E" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowB, 6].Value = "Time Deposit";
                                                                    worksheet.Cells[RowB, 6].Style.Font.Bold = true;
                                                                    worksheet.Cells["F" + RowB + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["F" + RowB + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                                                    //incRowExcel++;
                                                                }
                                                                else
                                                                {


                                                                    //worksheet.Cells[incRowExcel, 2].Value = "Rincian Portofolio Deposito";
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Bold = true;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(130, 197, 250));
                                                                    //incRowExcel++;
                                                                    //worksheet.Cells[incRowExcel, 2].Value = rsHeader1.Key.Name;
                                                                    //worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Bold = true;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(248, 196, 126));
                                                                    incRowExcel++;

                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells[RowA, 1, RowB, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells[RowA, 1, RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    //worksheet.Cells[RowA, 1, RowB, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                                    worksheet.Cells[incRowExcel, 1].Value = "No";
                                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Merge = true;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["A" + RowA + ":A" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 2].Value = "Fund Code";
                                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Merge = true;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["B" + RowA + ":B" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 3].Value = "Fund Name";
                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Merge = true;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowA + ":C" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 4].Value = "No. of Transaction";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Merge = true;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells["D" + RowA + ":F" + RowA].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[RowB, 4].Value = "Equity";
                                                                    worksheet.Cells[RowB, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowB + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowB + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowB, 5].Value = "Fixed Income";
                                                                    worksheet.Cells[RowB, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowB + ":E" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowB + ":E" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[RowB, 6].Value = "Time Deposit";
                                                                    worksheet.Cells[RowB, 6].Style.Font.Bold = true;
                                                                    worksheet.Cells["F" + RowB + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["F" + RowB + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                                                    //incRowExcel++;
                                                                }



                                                                incRowExcel++;


                                                                string _range = "A" + incRowExcel + ":F" + incRowExcel;

                                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                                {
                                                                    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                                    r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                                    //r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                    r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                                    r.Style.Font.Size = 11;
                                                                    //r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                                    r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                                    r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                                                }

                                                                //incRowExcel++;
                                                                int _no = 1;

                                                                int _startRowDetail = incRowExcel;
                                                                int _endRowDetail = 0;


                                                                //end area header
                                                                foreach (var rsDetail in rsHeader1)
                                                                {

                                                                    if (_nameSheet == "DEUTSCHE")
                                                                    {
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                                        //area detail
                                                                        worksheet.Cells[incRowExcel, 1].Value = _no;
                                                                        worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundCode;
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.Equity;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.Bond;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.Deposito;
                                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                                    }
                                                                    else if (_nameSheet == "BCA")
                                                                    {
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                                        //area detail
                                                                        worksheet.Cells[incRowExcel, 1].Value = _no;
                                                                        worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundCode;
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.Equity;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.Bond;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.Deposito;
                                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                                    }
                                                                    else
                                                                    {
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                                        //area detail
                                                                        worksheet.Cells[incRowExcel, 1].Value = _no;
                                                                        worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundCode;
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.Equity;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.Bond;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.Deposito;
                                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                                    }


                                                                    _endRowDetail = incRowExcel;
                                                                    _no++;
                                                                    incRowExcel++;

                                                                }

                                                                if (_nameSheet == "DEUTSCHE")
                                                                {
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 4].Calculate();
                                                                    worksheet.Cells[incRowExcel, 5].Calculate();
                                                                    worksheet.Cells[incRowExcel, 6].Calculate();
                                                                }
                                                                else if (_nameSheet == "BCA")
                                                                {
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 4].Calculate();
                                                                    worksheet.Cells[incRowExcel, 5].Calculate();
                                                                    worksheet.Cells[incRowExcel, 6].Calculate();
                                                                }
                                                                else
                                                                {
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                                    worksheet.Cells[incRowExcel, 4].Calculate();
                                                                    worksheet.Cells[incRowExcel, 5].Calculate();
                                                                    worksheet.Cells[incRowExcel, 6].Calculate();
                                                                }


                                                                incRowExcel = incRowExcel + 2;

                                                                worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                                                incRowExcel = incRowExcel + 6;
                                                                worksheet.Cells[incRowExcel, 1].Value = "Caterine";


                                                                incRowExcel = incRowExcel + 2;

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            //---------



                                        }


                                        worksheet.Row(incRowExcel).PageBreak = true;

                                        string _rangeDetail = "A7:K";

                                        using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Size = 11;
                                            //r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 6];

                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 25;
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 16;
                                        worksheet.Column(5).Width = 16;
                                        worksheet.Column(6).Width = 16;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Daily Deal Board";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        return true;
                                    }
                                }
                            }
                        }

                    }
                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }

            #endregion

            else
            {
                return false;
            }
        }

        public Boolean GenerateReportAccounting(string _userID, AccountingRpt _accountingRpt)
        {
            #region Balance Sheet Plain
            if (_accountingRpt.ReportName.Equals("Balance Sheet Plain"))
            {
                try
                {
                    //za============================================================
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramData = "";
                            DateTime _compareDate = Convert.ToDateTime("10/28/2015");

                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            if (_accountingRpt.ParamData == 1)
                            {
                                _paramData = " and A.Groups = 1  ";
                            }
                            else if (_accountingRpt.ParamData == 2)
                            {
                                _paramData = " and A.Groups = 0  ";
                            }
                            else if (_accountingRpt.ParamData == 3)
                            {
                                _paramData = " and A.Groups in (0,1)  ";
                            }
                            cmd.CommandText =
                            @"
Declare @PeriodPK int
Select @PeriodPK = PeriodPK from Period where @ValueDateTo between DateFrom and DateTo and status = 2


                            SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],    
                            SUM(B.Balance) AS CurrentBalance FROM Account A, (     
                            SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 1 THEN A.BaseDebit-A.BaseCredit      
                            ELSE A.BaseCredit-A.BaseDebit END) AS Balance,      
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,     
                            C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            FROM [JournalDetail] A      
                            INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK     
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.Status in (1,2)   
                            WHERE  B.ValueDate <= @ValueDateTo and B.PeriodPK = @PeriodPK   
                            " + _status + @"   
                            GROUP BY A.AccountPK, B.Posted, B.Reversed,C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4,      
                            C.ParentPK5, C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            ) AS B     
                            WHERE A.[Type] <= 2 AND A.Show = 1 AND (B.AccountPK = A.AccountPK      
                            OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK       
                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK      
                            OR B.ParentPK5 = A.AccountPK OR B.ParentPK6 = A.AccountPK      
                            OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK       
                            OR B.ParentPK9 = A.AccountPK) and A.Status = 2 and A.Type = 1   
                            " + _paramData + @"      
                            GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    
                            ORDER by A.ID 
                            ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "BalanceSheetPlain" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "BalanceSheetPlain" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "AccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Balance Sheet");




                                        int incRowExcel = 1;
                                        worksheet.Cells["A1:D1"].Value = "BALANCE SHEET PLAIN";
                                        worksheet.Cells["A1:D1"].Merge = true;
                                        worksheet.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;
                                        worksheet.Cells["A2:B2"].Value = "AS OF DATE : ";
                                        worksheet.Cells["A2:B2"].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 3].Value = _accountingRpt.ValueDateTo;
                                        incRowExcel++;
                                        worksheet.Cells["A3:D3"].Value = "ASSETS";
                                        worksheet.Cells["A3:D3"].Merge = true;
                                        worksheet.Cells["A3:D3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells["F2:I2"].Value = "LIABILITIES & EQUITY";
                                        //worksheet.Cells["F2:I2"].Merge = true;
                                        //worksheet.Cells["F2:I2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;
                                        worksheet.Cells["A4:B4"].Value = "ID";
                                        worksheet.Cells["A4:B4"].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "BALANCE";
                                        //worksheet.Cells["F3:G3"].Value = "ID";
                                        //worksheet.Cells["F3:G3"].Merge = true;
                                        //worksheet.Cells[incRowExcel, 8].Value = "NAME";
                                        //worksheet.Cells[incRowExcel, 9].Value = "BALANCE";

                                        incRowExcel++;


                                        int incRowExcelA = 5;
                                        int incRowExcelB = 0;
                                        int _rowA = 0;
                                        int _rowB = 0;
                                        while (dr0.Read())
                                        {


                                            if (Convert.ToInt32(dr0["Type"]) == 1)
                                            {


                                                if (Convert.ToInt32(dr0["Groups"]) == 1)
                                                {
                                                    worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Merge = true;
                                                    worksheet.Cells[incRowExcelA, 1].AutoFitColumns(3);
                                                    worksheet.Cells[incRowExcelA, 1].Value = Convert.ToString(dr0["ID"]);
                                                    worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr0["Name"]);
                                                }
                                                else
                                                {

                                                    worksheet.Cells[incRowExcelA, 2].Value = Convert.ToString(dr0["ID"]);
                                                    worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr0["Name"]);

                                                }
                                                worksheet.Cells[incRowExcelA, 4].Value = Convert.ToDecimal(dr0["CurrentBalance"]);
                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                                }
                                                incRowExcelA = incRowExcelA + 1;

                                            }
                                        }
                                        //==============================================================================

                                        using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon2.Open();
                                            using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                            {
                                                //string _status2 = "";
                                                //string _paramData2 = "";
                                                //DateTime _compareDate2 = Convert.ToDateTime("10/28/2015");

                                                cmd2.CommandText =
                                                @"
                                                    Declare @PeriodPK int
                                                    Select @PeriodPK = PeriodPK from Period where @ValueDateTo between DateFrom and DateTo and status = 2

                                                    SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],    
                                                    SUM(B.Balance) AS CurrentBalance FROM Account A, (     
                                                    SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 1 THEN A.BaseDebit-A.BaseCredit      
                                                    ELSE A.BaseCredit-A.BaseDebit END) AS Balance,      
                                                    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,     
                                                    C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                                    FROM [JournalDetail] A      
                                                    INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK     
                                                    INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.Status in (1,2)   
                                                    WHERE  B.ValueDate <= @ValueDateTo and B.PeriodPK = @PeriodPK   
                                                    " + _status + @"   
                                                    GROUP BY A.AccountPK, B.Posted, B.Reversed,C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4,      
                                                    C.ParentPK5, C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                                    ) AS B     
                                                    WHERE A.[Type] <= 2 AND A.Show = 1 AND (B.AccountPK = A.AccountPK      
                                                    OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK       
                                                    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK      
                                                    OR B.ParentPK5 = A.AccountPK OR B.ParentPK6 = A.AccountPK      
                                                    OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK       
                                                    OR B.ParentPK9 = A.AccountPK) and A.Status = 2 and A.Type = 2   
                                                    " + _paramData + @"      
                                                    GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    
                                                    ORDER by A.ID 
                                                    ";
                                                cmd2.CommandTimeout = 0;
                                                cmd2.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);


                                                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                                {
                                                    if (!dr2.HasRows)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        using (ExcelPackage package2 = new ExcelPackage(excelFile))
                                                        {
                                                            incRowExcelA++;
                                                            //incRowExcelB = incRowExcelA + 1;
                                                            worksheet.Cells[incRowExcelA, 1].Value = "LIABILITIES & EQUITY";
                                                            worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Merge = true;
                                                            worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            incRowExcelA++;
                                                            worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Value = "ID";
                                                            worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Merge = true;
                                                            worksheet.Cells[incRowExcelA, 3].Value = "NAME";
                                                            worksheet.Cells[incRowExcelA, 4].Value = "BALANCE";

                                                            incRowExcelA++;

                                                            while (dr2.Read())
                                                            {
                                                                if (Convert.ToInt32(dr2["Type"]) == 2)
                                                                {

                                                                    if (Convert.ToInt32(dr2["Groups"]) == 1)
                                                                    {
                                                                        if (Convert.ToString(dr2["ID"]) == "02")
                                                                        {
                                                                            _rowA = incRowExcelA;
                                                                        }

                                                                        if (Convert.ToString(dr2["ID"]) == "04")
                                                                        {
                                                                            _rowB = incRowExcelA;
                                                                        }

                                                                        worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Style.Font.Bold = true;
                                                                        worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Merge = true;
                                                                        worksheet.Cells[incRowExcelA, 1].AutoFitColumns(3);
                                                                        worksheet.Cells[incRowExcelA, 1].Value = Convert.ToString(dr2["ID"]);
                                                                        worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr2["Name"]);
                                                                    }
                                                                    else
                                                                    {

                                                                        worksheet.Cells[incRowExcelA, 2].Value = Convert.ToString(dr2["ID"]);
                                                                        worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr2["Name"]);

                                                                    }
                                                                    worksheet.Cells[incRowExcelA, 4].Value = Convert.ToDecimal(dr2["CurrentBalance"]);
                                                                    if (_accountingRpt.DecimalPlaces == 0)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                                                    }
                                                                    else if (_accountingRpt.DecimalPlaces == 2)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                                                    }
                                                                    else if (_accountingRpt.DecimalPlaces == 4)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                                                    }
                                                                    else if (_accountingRpt.DecimalPlaces == 6)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                    }
                                                                    else
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                                                    }
                                                                    incRowExcelA = incRowExcelA + 1;

                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //incRowExcelB++;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells["B:D"].AutoFitColumns();
                                        worksheet.Cells["G:I"].AutoFitColumns();
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 BALANCE SHEET";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        if (_accountingRpt.ValueDateTo <= _compareDate)
                                        {
                                            worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftBatchReport();
                                        }
                                        else
                                        {
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        }

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_accountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        return true;
                                    }

                                }
                            }
                        }
                    }

                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion

            #region Income Statement Plain
            else if (_accountingRpt.ReportName.Equals("Income Statement Plain"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramData = "";
                            DateTime _compareDate = Convert.ToDateTime("10/28/2015");
                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            if (_accountingRpt.ParamData == 1)
                            {
                                _paramData = " and A.Groups = 1  ";
                            }
                            else if (_accountingRpt.ParamData == 2)
                            {
                                _paramData = " and A.Groups = 0  ";
                            }
                            else if (_accountingRpt.ParamData == 3)
                            {
                                _paramData = " and A.Groups in (0,1)  ";
                            }


                            cmd.CommandText = @"
                            Declare @PeriodPK int
                            Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                            SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],      
                            SUM(B.Balance) AS CurrentBalance FROM Account A, (      
                            SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 4 THEN A.BaseDebit-A.BaseCredit       
                            ELSE A.BaseCredit-A.BaseDebit END) AS Balance,       
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,      
                            C.ParentPK7, C.ParentPK8, C.ParentPK9      
                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK    
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.status in (1,2)    
                            WHERE  B.ValueDate between @ValueDateFrom and @ValueDateTo and B.PeriodPK = @PeriodPK      
                            AND C.[Type] >= 3    
                            " + _status + @"       
                            GROUP BY A.AccountPK, B.Posted, B.Reversed, C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4,     
                            C.ParentPK5, C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9      
                            ) AS B      
                            WHERE A.[Type] >= 3 AND A.Show = 1 AND (B.AccountPK = A.AccountPK    
                            OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK    
                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK    
                            OR B.ParentPK5 = A.AccountPK OR B.ParentPK6 = A.AccountPK    
                            OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK    
                            OR B.ParentPK9 = A.AccountPK) and A.status = 2 and A.Type = 3"
                            + _paramData + @"        
                            GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    
                            ORDER by A.ID 
                            ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);




                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "IncomeStatementPlain" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "IncomeStatementPlain" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "AccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Income Statement");




                                        int incRowExcel = 1;
                                        worksheet.Cells["A1:C1"].Value = "INCOME STATEMENT PLAIN";
                                        worksheet.Cells["A1:C1"].Merge = true;
                                        worksheet.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells["A1:C1"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                        incRowExcel++;

                                        worksheet.Cells["A2:B2"].Value = "DATE FROM : ";
                                        worksheet.Cells["A2:B2"].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 3].Value = _accountingRpt.ValueDateFrom;
                                        incRowExcel++;
                                        worksheet.Cells["A3:B3"].Value = "DATE TO : ";
                                        worksheet.Cells["A3:B3"].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 3].Value = _accountingRpt.ValueDateTo;
                                        incRowExcel++;
                                        worksheet.Cells["A4:D4"].Value = "INCOME";
                                        worksheet.Cells["A4:D4"].Merge = true;
                                        worksheet.Cells["A4:D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;
                                        worksheet.Cells["A5:B5"].Value = "ID";
                                        worksheet.Cells["A5:B5"].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "BALANCE";

                                        incRowExcel++;



                                        //int _endRowDetail = 0;
                                        int _A = 0;
                                        int incRowExcelA = 6;

                                        while (dr0.Read())
                                        {

                                            if (Convert.ToInt32(dr0["Type"]) == 3)
                                            {


                                                if (Convert.ToInt32(dr0["Groups"]) == 1)
                                                {
                                                    worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Merge = true;
                                                    worksheet.Cells[incRowExcelA, 1].AutoFitColumns(3);
                                                    worksheet.Cells[incRowExcelA, 1].Value = Convert.ToString(dr0["ID"]);
                                                    worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr0["Name"]);
                                                }
                                                else
                                                {

                                                    worksheet.Cells[incRowExcelA, 2].Value = Convert.ToString(dr0["ID"]);
                                                    worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr0["Name"]);

                                                }
                                                worksheet.Cells[incRowExcelA, 4].Value = Convert.ToDecimal(dr0["CurrentBalance"]);

                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                                }
                                            }
                                            incRowExcelA++;
                                        }



                                        int _startRowDetail = incRowExcel;
                                        _A = incRowExcelA;
                                        worksheet.Cells[incRowExcelA, 1].Value = "TOTAL INCOME :";
                                        worksheet.Cells[incRowExcelA, 4].Formula = "SUM(D" + _startRowDetail + ")";
                                        worksheet.Cells[incRowExcelA, 4].Calculate();
                                        worksheet.Cells[incRowExcelA, 1].Style.Font.Color.SetColor(Color.White);
                                        worksheet.Cells[incRowExcelA, 4].Style.Font.Color.SetColor(Color.White);

                                        if (_accountingRpt.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_accountingRpt.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_accountingRpt.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_accountingRpt.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                        }

                                        //incRowExcelA = incRowExcelA + 1;



                                        //aa=====================================================================================

                                        int _B = 0;

                                        using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon2.Open();
                                            using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                            {
                                                cmd2.CommandText = @"
                                                Declare @PeriodPK int
                                                Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                                                SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],      
                                                SUM(B.Balance) AS CurrentBalance FROM Account A, (      
                                                SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 4 THEN A.BaseDebit-A.BaseCredit       
                                                ELSE A.BaseCredit-A.BaseDebit END) AS Balance,       
                                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,      
                                                C.ParentPK7, C.ParentPK8, C.ParentPK9      
                                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK    
                                                INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.status in (1,2)    
                                                WHERE  B.ValueDate between @ValueDateFrom and @ValueDateTo and B.PeriodPK = @PeriodPK      
                                                AND C.[Type] >= 3    
                                                " + _status + @"       
                                                GROUP BY A.AccountPK, B.Posted, B.Reversed, C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4,     
                                                C.ParentPK5, C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9      
                                                ) AS B      
                                                WHERE A.[Type] >= 3 AND A.Show = 1 AND (B.AccountPK = A.AccountPK    
                                                OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK    
                                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK    
                                                OR B.ParentPK5 = A.AccountPK OR B.ParentPK6 = A.AccountPK    
                                                OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK    
                                                OR B.ParentPK9 = A.AccountPK) and A.status = 2 and A.Type = 4"
                                                + _paramData + @"        
                                                GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    
                                                ORDER by A.ID 
                                                ";
                                                cmd2.CommandTimeout = 0;
                                                cmd2.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                                cmd2.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                                //sini
                                                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                                {
                                                    if (!dr2.HasRows)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                        using (ExcelPackage package2 = new ExcelPackage(excelFile))
                                                        {

                                                            //int incRowExcel2 = 1;
                                                            //worksheet.Cells["A1:C1"].Merge = true;
                                                            incRowExcelA++;

                                                            worksheet.Cells[incRowExcelA, 1].Value = "EXPENSE";
                                                            worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Merge = true;
                                                            worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            incRowExcelA++;
                                                            worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Value = "ID";
                                                            worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Merge = true;
                                                            worksheet.Cells[incRowExcelA, 3].Value = "NAME";
                                                            worksheet.Cells[incRowExcelA, 4].Value = "BALANCE";

                                                            int _startRowDetailB = incRowExcelA + 1;

                                                            while (dr2.Read())
                                                            {
                                                                if (Convert.ToInt32(dr2["Type"]) == 4)
                                                                {
                                                                    incRowExcelA++;
                                                                    if (Convert.ToInt32(dr2["Groups"]) == 1)
                                                                    {
                                                                        worksheet.Cells["A" + incRowExcelA + ":D" + incRowExcelA].Style.Font.Bold = true;
                                                                        worksheet.Cells["A" + incRowExcelA + ":B" + incRowExcelA].Merge = true;
                                                                        worksheet.Cells[incRowExcelA, 1].AutoFitColumns(3);
                                                                        worksheet.Cells[incRowExcelA, 1].Value = Convert.ToString(dr2["ID"]);
                                                                        worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr2["Name"]);
                                                                    }
                                                                    else
                                                                    {

                                                                        worksheet.Cells[incRowExcelA, 2].Value = Convert.ToString(dr2["ID"]);
                                                                        worksheet.Cells[incRowExcelA, 3].Value = Convert.ToString(dr2["Name"]);

                                                                    }
                                                                    worksheet.Cells[incRowExcelA, 4].Value = Convert.ToDecimal(dr2["CurrentBalance"]);
                                                                    if (_accountingRpt.DecimalPlaces == 0)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                                                    }
                                                                    else if (_accountingRpt.DecimalPlaces == 2)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                                                    }
                                                                    else if (_accountingRpt.DecimalPlaces == 4)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                                                    }
                                                                    else if (_accountingRpt.DecimalPlaces == 6)
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                    }
                                                                    else
                                                                    {
                                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                                                    }
                                                                }
                                                            }
                                                            incRowExcelA++;

                                                            _B = incRowExcelA;
                                                            worksheet.Cells[incRowExcelA, 1].Value = "TOTAL EXPENSE :";
                                                            worksheet.Cells[incRowExcelA, 4].Formula = "SUM(D" + _startRowDetailB + ")";
                                                            worksheet.Cells[incRowExcelA, 4].Calculate();
                                                            worksheet.Cells[incRowExcelA, 1].Style.Font.Color.SetColor(Color.White);
                                                            worksheet.Cells[incRowExcelA, 4].Style.Font.Color.SetColor(Color.White);

                                                            if (_accountingRpt.DecimalPlaces == 0)
                                                            {
                                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                                            }
                                                            else if (_accountingRpt.DecimalPlaces == 2)
                                                            {
                                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                                            }
                                                            else if (_accountingRpt.DecimalPlaces == 4)
                                                            {
                                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                                            }
                                                            else if (_accountingRpt.DecimalPlaces == 6)
                                                            {
                                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                                            }
                                                            else
                                                            {
                                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                                            }
                                                        }
                                                        incRowExcelA++;
                                                    }
                                                    worksheet.Cells[incRowExcelA, 1].Value = "NET INCOME (EXPENSE)";
                                                    worksheet.Cells[incRowExcelA, 4].Formula = "(D" + _A + "-D" + _B + ")";
                                                    worksheet.Cells[incRowExcelA, 4].Calculate();
                                                    worksheet.Cells["A" + incRowExcelA + ":C" + incRowExcelA].Merge = true;
                                                    worksheet.Cells[incRowExcelA, 1].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcelA, 4].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcelA, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    if (_accountingRpt.DecimalPlaces == 0)
                                                    {
                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                                    }
                                                    else if (_accountingRpt.DecimalPlaces == 2)
                                                    {
                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                                    }
                                                    else if (_accountingRpt.DecimalPlaces == 4)
                                                    {
                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                                    }
                                                    else if (_accountingRpt.DecimalPlaces == 6)
                                                    {
                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                                    }
                                                }
                                            }
                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells["A:I"].AutoFitColumns();
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 INCOME STATEMENT";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        if (_accountingRpt.ValueDateTo <= _compareDate)
                                        {
                                            worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftBatchReport();
                                        }
                                        else
                                        {
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        }

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_accountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion


            #region Invoice Management Fee
            else if (_accountingRpt.ReportName.Equals("Invoice Management Fee"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramInstrument = "";

                            if (!_host.findString(_accountingRpt.InstrumentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.InstrumentFrom))
                            {
                                _paramInstrument = " and InstrumentPK  in ( " + _accountingRpt.InstrumentFrom + " ) ";
                            }
                            else
                            {
                                _paramInstrument = "";
                            }

                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }


                            cmd.CommandText = @"
                            declare @AccountPK int
                            declare @InstrumentPK int
                            select @AccountPK = ManagementFeeExpense from AccountingSetup where status in (1,2)


                            CREATE TABLE #A
                            (
                            FundTypeInternal int,InstrumentID nvarchar(100),InstrumentName nvarchar(100),Type int,Amount numeric(22,4)
                            )

                            DECLARE A CURSOR FOR 
                            Select InstrumentPK From Instrument where status in (1,2) " + _paramInstrument + @"
        	
                            Open A
                            Fetch Next From A
                            Into @InstrumentPK
        
                            While @@FETCH_STATUS = 0
                            BEGIN

                            INSERT INTO #A
                            select E.FundTypeInternal,E.ID InstrumentID,E.Name InstrumentName,C.Type Type,case when C.Type in (1,4) then isnull(sum(BaseDebit-BaseCredit),0) else isnull(sum(BaseDebit-BaseCredit),0) * -1 end Amount from JournalDetail A 
                            left join Journal B on A.JournalPK = B.JournalPK and B.status = 2
                            left join Account C on A.AccountPK = C.AccountPK and C.status in (1,2)
                            left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.status in (1,2)
                            left join Fund E on D.ID = E.ID and E.status in (1,2)
                            where  A.AccountPK = @AccountPK and A.InstrumentPk = @InstrumentPK " + _status + @"
                            and ValueDate between @DateFrom and @DateTo
                            group by E.FundTypeInternal,E.ID,E.Name,C.Type 

                            Fetch next From A Into @InstrumentPK
                            END
                            Close A
                            Deallocate A 

                            select * from #A
                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _accountingRpt.ValueDateTo);




                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "InvoiceManagementFee" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "InvoiceManagementFee" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "AccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Invoice Management Fee");


                                        List<InvoiceManagementFee> rList = new List<InvoiceManagementFee>();
                                        while (dr0.Read())
                                        {
                                            InvoiceManagementFee rSingle = new InvoiceManagementFee();
                                            rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.Type = Convert.ToInt32(dr0["Type"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.FundTypeInternal = Convert.ToInt32(dr0["FundTypeInternal"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                            group r by new { r.InstrumentID, r.InstrumentName } into rGroup
                                            select rGroup;


                                        int incRowExcel = 5;
                                        int incRowExcelA = 6;

                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Value = "INVOICE";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Size = 30;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Value = "Plaza Asia 2nd floor, Unit D";
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                            worksheet.Cells["D" + incRowExcel].Value = "DATE";
                                            worksheet.Cells["E" + incRowExcel].Value = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy") + " - " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");

                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Value = "Jalan Jendral Sudirman Kav.59 Jakarta Selatan 12110";
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                            worksheet.Cells["D" + incRowExcel].Value = "INVOICE #";

                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Value = Convert.ToString("Phone: 021-50208808");
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                            worksheet.Cells["D" + incRowExcel].Value = "CUSTOMER ID";
                                            worksheet.Cells["E" + incRowExcel].Value = rsHeader.Key.InstrumentID;

                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Value = Convert.ToString("Fax: 021-51400415");
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Value = "Website: jarvisasset.com";
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Value = "BILL TO :";
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Value = rsHeader.Key.InstrumentName;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;







                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Value = "DESCRIPTION";
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                                            worksheet.Cells["E" + incRowExcel].Value = "AMOUNT";
                                            worksheet.Cells["E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["E" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["E" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);
                                            incRowExcel++;

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                int _lineHeader = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Value = "Management Fee Period : " + Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("MMM-yyyy");
                                                worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Value = rsDetail.Amount;

                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00000000";
                                                }

                                                incRowExcel = incRowExcel + 10;
                                                int _startMin = incRowExcel - 1;
                                                int _start = incRowExcel;
                                                worksheet.Cells["D" + incRowExcel].Value = "SUB TOTAL";
                                                worksheet.Cells["E" + incRowExcel].Value = rsDetail.Amount;
                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00000000";
                                                }
                                                incRowExcel++;
                                                worksheet.Cells["D" + incRowExcel].Value = "VAT 10%";
                                                if (rsDetail.FundTypeInternal == 2)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Value = 0;
                                                }
                                                else
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Value = 10;
                                                }
                                                incRowExcel++;
                                                worksheet.Cells["D" + incRowExcel].Value = "TAX ART 23";
                                                if (rsDetail.FundTypeInternal == 2)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Value = 0;
                                                }
                                                else
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Value = rsDetail.Amount / 10 * -1;
                                                }
                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00000000";
                                                }


                                                incRowExcel++;
                                                int _end = incRowExcel;

                                                // GARIS
                                                worksheet.Cells["A" + _lineHeader + ":D" + _startMin].Merge = true;
                                                worksheet.Cells["A" + _lineHeader + ":D" + _startMin].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells["A" + _lineHeader + ":D" + _startMin].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells["E" + _lineHeader + ":E" + _startMin].Merge = true;
                                                worksheet.Cells["E" + _lineHeader + ":E" + _startMin].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells["E" + _lineHeader + ":E" + _startMin].Style.VerticalAlignment = ExcelVerticalAlignment.Top;


                                                worksheet.Cells["D" + incRowExcel].Value = "OTHER";
                                                incRowExcel++;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells["D" + incRowExcel].Value = "TOTAL";
                                                worksheet.Cells["E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells["E" + incRowExcel].Formula = "SUM(E" + _start + ":E" + _end + ")";
                                                worksheet.Cells["E" + incRowExcel].Calculate();
                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells["E" + incRowExcel].Style.Numberformat.Format = "#,##0.00000000";
                                                }

                                                worksheet.Cells["A" + _lineHeader + ":E" + _startMin].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _lineHeader + ":E" + _startMin].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _lineHeader + ":E" + _startMin].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _lineHeader + ":E" + _startMin].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                incRowExcel++;
                                            }

                                            incRowExcel++;


                                            worksheet.Cells["A" + incRowExcel].Value = "COMMENTS";
                                            worksheet.Cells["A" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel].Value = "Please transfer the payment to below account :";
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel].Value = "Account Name : PT. Jarvis Aset Manajemen";
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel].Value = "Account Number : 2024169061";
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel].Value = "Bank : Maybank Indonesia";
                                            incRowExcel++;
                                            incRowExcel++;
                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel].Value = "Sincerely,";

                                            worksheet.Row(incRowExcel).PageBreak = _accountingRpt.PageBreak;

                                            incRowExcel = incRowExcel + 5;
                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A:I"].AutoFitColumns();

                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 7];
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.Column(1).Width = 35;
                                        worksheet.Column(2).Width = 25;
                                        worksheet.Column(3).Width = 10;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 15;
                                        //worksheet.PrinterSettings.FitToWidth = 1;
                                        //worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 INVOICE MANAGEMENT FEE";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);



                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_accountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion

            #region Client Expense Vs Client Revenue
            if (_accountingRpt.ReportName.Equals("Client Expense Vs Client Revenue"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramConsignee = "";

                            if (!_host.findString(_accountingRpt.ConsigneeFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.ConsigneeFrom))
                            {
                                _paramConsignee = "And B.ConsigneePK  in ( " + _accountingRpt.ConsigneeFrom + " ) ";
                            }
                            else
                            {
                                _paramConsignee = "";
                            }

                            //string _paramInstrument = "";

                            //if (!_host.findString(_accountingRpt.InstrumentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.InstrumentFrom))
                            //{
                            //    _paramInstrument = "And B.InstrumentPK  in ( " + _accountingRpt.InstrumentFrom + " ) ";
                            //}
                            //else
                            //{
                            //    _paramInstrument = "";
                            //}



                            cmd.CommandText = @"
select isnull(E.Name,'') ClientName, sum(isnull(B.BaseDebit,0)) ClientExpenseAmount,D.Mfee ClientRevenueAmount from Journal A
left join JournalDetail B on A.JournalPK = B.JournalPK and B.status = 2
--left join Consignee C on B.ConsigneePK = C.ConsigneePK and C.status = 2
left join FundClientConsigneeMapping C on B.ConsigneePK = C.ConsigneePK and C.Status in (1,2)
left join FundClient E on C.FundClientPK = E.FundClientPK and E.Status in (1,2)
left join (
select sum(isnull(Mfee,0)) Mfee, FundClientPK from DailyDataForCommissionRptNew 
where MFeeDate between @DateFrom and @DateTo group by FundClientPK
) D on C.FundClientPK = D.FundClientPK
 where A.status = 2 and A.Posted = 1 and A.Reversed = 0 and B.AccountPK = 335 and B.ConsigneePK <> 0
 and A.ValueDate between @DateFrom and @DateTo " + _paramConsignee + @"
  group by E.Name,D.Mfee

                             ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _accountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ClientExpenseVsClientRevenue" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ClientExpenseVsClientRevenue" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "AccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Client Expense Vs Client Revenue");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ClientExpenseVsClientRevenue> rList = new List<ClientExpenseVsClientRevenue>();
                                        while (dr0.Read())
                                        {

                                            ClientExpenseVsClientRevenue rSingle = new ClientExpenseVsClientRevenue();
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.ClientExpenseAmount = Convert.ToDecimal(dr0["ClientExpenseAmount"]);
                                            rSingle.ClientRevenueAmount = Convert.ToDecimal(dr0["ClientRevenueAmount"]);



                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { r.ClientName } into rGroup
                                                select rGroup;

                                        int _date;
                                        int incRowExcel = 0;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1].Value = "Report Client Expense vs Client Revenue";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;




                                        foreach (var rsHeader in GroupByReference)
                                        {


                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1].Value = "Instrument :";
                                            //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InstrumentID;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From    :";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateFrom);
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            _date = incRowExcel;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To         :";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo);
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            _date = incRowExcel;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Client Name";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.ClientName;
                                            _date = incRowExcel;
                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "Client Expense Amount";
                                            worksheet.Cells[incRowExcel, 2].Value = "Client Revenue Amount";
                                            worksheet.Cells[incRowExcel, 3].Value = "Net Amount";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.ClientExpenseAmount;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientRevenueAmount;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Formula = "=B" + incRowExcel + "-A" + incRowExcel;
                                                worksheet.Cells[incRowExcel, 3].Calculate();
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";


                                                no++;
                                                _endRowDetail = incRowExcel;


                                            }

                                            worksheet.Row(incRowExcel).PageBreak = _accountingRpt.PageBreak;



                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 3];
                                        worksheet.Column(1).AutoFit();
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Report Equity Summary";


                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        //worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_accountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            else
            {
                return false;
            }
        }

        public Boolean GenerateReportCompliance(string _userID, OjkRpt _OjkRpt)
        {


            #region KPD
            if (_OjkRpt.ReportName.Equals("5"))
            {
                #region Txt

                if (_OjkRpt.DownloadMode == "Txt")
                {
                    try
                    {
                        DateTime _datetimeNow = DateTime.Now;
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                string _companyCode = "";

                                cmd.CommandTimeout = 0;
                                //cmd.CommandText = @"

                                //declare @FundPK int

                                //--drop table #Text--
                                //create table #Text(      
                                //[ResultText] [nvarchar](1000)  NULL          
                                //)                        
        
                                //truncate table #Text --         

                                //--drop Table #KPD--
                                //Create Table #KPD
                                //(AUM nvarchar(50),CashAmount nvarchar(50),InstrumentTypePK int,PK int,KodeNasabah nvarchar(50),NamaNasabah nvarchar(50) ,NomorKontrak nvarchar(50),TanggalKontrak nvarchar(50),TanggalJatuhTempo nvarchar(50),
                                //NomorAdendum nvarchar(50), TanggalAdendum nvarchar(50),NilaiInvestasiAwalIDR nvarchar(50), NilaiInvestasiAwalNonIDR nvarchar(50),NilaiInvestasiAkhir nvarchar(50),
                                //NilaiInvestasiAkhirNonIDR nvarchar(50), JenisEfek nvarchar(50), DNatauLN int,JumlahEfek nvarchar(50),NilaiPembelian nvarchar(50), NilaiNominal nvarchar(50),
                                //HPW nvarchar(50), Deposito nvarchar(50), TotalNilai nvarchar(50),KodeBK  nvarchar(50), type int,KodeSaham nvarchar(50),MarketValue nvarchar(50),SID nvarchar(50)
                                //)

                                //DECLARE A CURSOR FOR 
                                //select FundPK
                                //from Fund
                                //where [Status] = 2 and FundTypeInternal = 2
                                //Open A
                                //Fetch Next From A
                                //Into @FundPK

                                //While @@FETCH_STATUS = 0
                                //Begin


                                
                                //Insert into #KPD (AUM,CashAmount,InstrumentTypePK,PK,KodeNasabah ,NamaNasabah ,NomorKontrak,TanggalKontrak,TanggalJatuhTempo,
                                //NomorAdendum, TanggalAdendum,NilaiInvestasiAwalIDR, NilaiInvestasiAwalNonIDR,NilaiInvestasiAkhir,
                                //NilaiInvestasiAkhirNonIDR, JenisEfek, DNatauLN,JumlahEfek,NilaiPembelian, NilaiNominal,
                                //HPW, Deposito, TotalNilai,KodeBK,type,KodeSaham,MarketValue,SID)
                                //select F.AUM,E.CashAmount,InstrumentTypePK,ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,H.ClientCategory KodeNasabah ,H.Name NamaNasabah ,NKPDName NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
                                //KPDNoAdendum NomorAdendum,isnull(CONVERT(VARCHAR(8), A.KPDDateAdendum, 112),0) TanggalAdendum,cast(isnull(TotalUnits,CAST(TotalUnits AS DECIMAL(22, 8))) * isnull(A.Nav,CAST(A.Nav AS DECIMAL(22, 8)))as DECIMAL(22, 4)) NilaiInvestasiAwalIDR,0 NilaiInvestasiAwalNonIDR,CAST(isnull(dbo.FGetTotalMarketValue(@Date,A.FundPK),0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,
                                //0 NilaiInvestasiAkhirNonIDR, C.ID JenisEfek,1 DNatauLN,B.Balance JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 4)) NilaiPembelian,CAST(0 AS DECIMAL(22, 2)) NilaiNominal,
                                //case when C.InstrumentTypePK not in (1,5) then   CAST(B.ClosePrice AS DECIMAL(22, 6))  else CAST(B.ClosePrice AS DECIMAL(22, 0)) end HPW,CAST(0 AS DECIMAL(22, 4)) Deposito,0 TotalNilai,isnull(I.NKPDCode,0) KodeBK,C.InstrumentTypePK type, '1' KodeSaham,CAST(B.MarketValue AS DECIMAL(22, 4)) MarketValue,isnull(H.SID,'') SID from Fund A
                                //left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
                                //left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
                                //left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
                                //left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
                                //left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
                                //left join FundClientposition G on A.FundPK = G.FundPK
                                //left join FundClient H on G.FundClientPK = H.FundClientPK and H.status = 2
                                //left join Bank I on D.BankPK = I.BankPK and I.Status in (1,2)
                                //--left join ClosePrice I on B.InstrumentPK = I.instrumentPK and I.status = 2 and B.Date = @Date
                                //where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
                                //Group By  F.AUM,E.CashAmount,C.ID,H.ClientCategory,H.Name,NKPDName,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,I.NKPDCode,C.InstrumentTypePK,B.MarketValue,A.FundPK,H.SID,A.KPDNoAdendum,KPDDateAdendum
                                //order By C.ID asc

                                //Fetch next From A Into @FundPK
                                //end
                                //Close A
                                //Deallocate A

                                //update #KPD set 
                                //NilaiInvestasiAwalNonIDR = 0, --NilaiInvestasiAkhir = CAST(0 AS DECIMAL(22, 2)),
                                //NilaiInvestasiAkhirNonIDR = 0 where PK <> 1

                                //insert into #Text 

                                //select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') + --1
                                //'|' + isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'')  +    --2
                                //'|' + isnull(RTRIM(LTRIM(isnull(NomorKontrak,''))),'')  +  --3
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalKontrak,'')))),'')  +  --4
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalJatuhTempo,'')))),'')  +  --5
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NomorAdendum,'')))),'')  +  --6
                                //'|' + case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'') = '19000101' then '0' else  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'') end  +  --7
                                //'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,''))),'')  +  --8
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAwalNonIDR,'')))),'')  + --9
                                //'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhir,''))),'')  + --10
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAkhirNonIDR,'')))),'')  +  --11
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  --12
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DNatauLN,'')))),'')  + --13
                                //'|' + case when type = 5 then '0.0000' else case when type in (1,6) then isnull(RTRIM(LTRIM(isnull(JumlahEfek,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeSaham,'')))),'') else '' end end end + --14
                                //--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  --14
                                //'|' + case when type = 5 then '0.0000' else case when type in (1,6) then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(JumlahEfek,''))),'') else '' end end end + --15
                                //--'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  --15
                                //--'|' + case when type  in (1,5,6) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --16
                                //'|' + '0.0000'  +  --16
                                //--'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(HPW,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(PriceBond,''))),'') else '' end end +
                                //'|' + case when Type = 5 then '0.0000' else isnull(RTRIM(LTRIM(isnull(HPW,''))),'') end +  --17
                                //'|' + case when Type = 5 then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else isnull(RTRIM(LTRIM(isnull(Deposito,''))),'') end +  --18
                                //'|' + case when type in (1,5,6) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'')  else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'')  else '' end end + --19
                                //--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')  +   --19
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  + --21
                                //'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
                                //from #KPD

                                //select * from #text

                                //     ";

                                cmd.CommandText = @"

                                declare @FundPK int

                                --drop table #Text--
                                create table #Text(      
                                [ResultText] [nvarchar](1000)  NULL          
                                )                        
        
                                truncate table #Text --         

                                --drop Table #KPD--
                                Create Table #KPD
                                (AUM nvarchar(50),CashAmount nvarchar(50),InstrumentTypePK int,PK int,KodeNasabah nvarchar(50),NamaNasabah nvarchar(50) ,NomorKontrak nvarchar(50),TanggalKontrak nvarchar(50),TanggalJatuhTempo nvarchar(50),
                                NomorAdendum nvarchar(50), TanggalAdendum nvarchar(50),NilaiInvestasiAwalIDR nvarchar(50), NilaiInvestasiAwalNonIDR nvarchar(50),NilaiInvestasiAkhir nvarchar(50),
                                NilaiInvestasiAkhirNonIDR nvarchar(50), JenisEfek nvarchar(50), DNatauLN int,JumlahEfek nvarchar(50),NilaiPembelian nvarchar(50), NilaiNominal nvarchar(50),
                                HPW nvarchar(50), Deposito nvarchar(50), TotalNilai nvarchar(50),KodeBK  nvarchar(50), type int,KodeSaham nvarchar(50),MarketValue nvarchar(50),SID nvarchar(50)
                                )

								declare @TableSubs table (
								FundClientPK int,
								FundPK int,
								TotalSubs numeric(32,8)
							)

                                DECLARE A CURSOR FOR 
                                select FundPK
                                from Fund
                                where [Status] = 2 and FundTypeInternal = 2
                                Open A
                                Fetch Next From A
                                Into @FundPK

                                While @@FETCH_STATUS = 0
                                Begin

								insert into @TableSubs(FundClientPK,FundPK,TotalSubs)
								select FundClientPK, FundPK, sum(TotalCashAmount) from ClientSubscription where status = 2 and Posted = 1 and fundpk = @FundPK
								group by FundClientPK, FundPK
                                
                                Insert into #KPD (AUM,CashAmount,InstrumentTypePK,PK,KodeNasabah ,NamaNasabah ,NomorKontrak,TanggalKontrak,TanggalJatuhTempo,
                                NomorAdendum, TanggalAdendum,NilaiInvestasiAwalIDR, NilaiInvestasiAwalNonIDR,NilaiInvestasiAkhir,
                                NilaiInvestasiAkhirNonIDR, JenisEfek, DNatauLN,JumlahEfek,NilaiPembelian, NilaiNominal,
                                HPW, Deposito, TotalNilai,KodeBK,type,KodeSaham,MarketValue,SID)
                                select F.AUM,E.CashAmount,InstrumentTypePK,ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,H.ClientCategory KodeNasabah ,H.Name NamaNasabah ,NKPDName NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
                                KPDNoAdendum NomorAdendum,isnull(CONVERT(VARCHAR(8), A.KPDDateAdendum, 112),0) TanggalAdendum,J.TotalSubs NilaiInvestasiAwalIDR,0 NilaiInvestasiAwalNonIDR,CAST(isnull(dbo.FGetTotalMarketValue(@Date,A.FundPK),0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,
                                0 NilaiInvestasiAkhirNonIDR,case when C.InstrumentTypePK = 5 then I.PTPCode else C.ID end JenisEfek,1 DNatauLN,B.Balance JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 4)) NilaiPembelian,CAST(0 AS DECIMAL(22, 2)) NilaiNominal,
                                case when C.InstrumentTypePK not in (1,5) then   CAST(B.ClosePrice AS DECIMAL(22, 6))  else CAST(B.ClosePrice AS DECIMAL(22, 0)) end HPW,CAST(0 AS DECIMAL(22, 4)) Deposito,0 TotalNilai,isnull(I.NKPDCode,0) KodeBK,C.InstrumentTypePK type, '1' KodeSaham,CAST(B.MarketValue AS DECIMAL(22, 4)) MarketValue,isnull(H.SID,'') SID from Fund A
                                left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
                                left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
                                left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
                                left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
                                left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
                                left join FundClientposition G on A.FundPK = G.FundPK
                                left join FundClient H on G.FundClientPK = H.FundClientPK and H.status = 2
                                left join Bank I on D.BankPK = I.BankPK and I.Status in (1,2)
								left join @TableSubs J on G.FundClientPK = J.FundClientPK and G.FundPK = G.FundPK
                                --left join ClosePrice I on B.InstrumentPK = I.instrumentPK and I.status = 2 and B.Date = @Date
                                where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
                                Group By  F.AUM,E.CashAmount,C.ID,H.ClientCategory,H.Name,NKPDName,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,I.NKPDCode,C.InstrumentTypePK,B.MarketValue,A.FundPK,H.SID,A.KPDNoAdendum,KPDDateAdendum,J.TotalSubs,I.PTPCode
                                order By C.ID asc

                                Fetch next From A Into @FundPK
                                end
                                Close A
                                Deallocate A

                                update #KPD set 
                                NilaiInvestasiAwalNonIDR = 0, --NilaiInvestasiAkhir = CAST(0 AS DECIMAL(22, 2)),
                                NilaiInvestasiAkhirNonIDR = 0 where PK <> 1


								Declare @CFundPK int
                            DECLARE @KPD Table 
                            (
	                            KodeNasabah NVARCHAR(5),
	                            NamaNasabah NVARCHAR(200),
	                            NoKontrak NVARCHAR(100),
	                            TglKontrakFrom NVARCHAR(50),
	                            TglKontrakTo NVARCHAR(50),
	                            NoAdendum NVARCHAR(100),
	                            TglAdendum NVARCHAR(100),
	                            NilaiInvestasiAwalIDR NUMERIC(22,4),
	                            NilaiInvestasiAwalNonIDR NUMERIC(22,4),
	                            NilaiInvestasiAkhirIDR NUMERIC(22,4),
	                            NilaiInvestasiAkhirNonIDR NUMERIC(22,4),
	                            JenisEfek NVARCHAR(100),
	                            DnLn NVARCHAR(2),
	                            JumlahEfek NUMERIC(22,4),
	                            NilaiPembelian NUMERIC(22,4),
	                            NilaiNominal NUMERIC(22,4),
	                            HPW NUMERIC(22,4),
	                            Deposito NUMERIC(22,4),
	                            TotalNilai NUMERIC(22,4),
	                            KodeBK NVARCHAR(10),
	                            Keterangan NVARCHAR(50),
	                            SID NVARCHAR(100),
	                            Ascending int
                            )

                            DECLARE B CURSOR FOR 
                            SELECT distinct A.FundPK
                            from dbo.FundClientPosition A
                            LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
                            where A.Date = @Date and B.FundTypeInternal = 2 
                            AND A.UnitAmount > 1 
                            Open B
                            Fetch Next From B
                            Into @CFundPK
                            While @@FETCH_STATUS = 0
                            Begin
	
	                            INSERT INTO @KPD
	                                    ( KodeNasabah ,
	                                      NamaNasabah ,
	                                      NoKontrak ,
	                                      TglKontrakFrom ,
	                                      TglKontrakTo ,
	                                      NoAdendum ,
	                                      TglAdendum ,
	                                      NilaiInvestasiAwalIDR ,
	                                      NilaiInvestasiAwalNonIDR ,
	                                      NilaiInvestasiAkhirIDR ,
	                                      NilaiInvestasiAkhirNonIDR ,
	                                      JenisEfek ,
	                                      DnLn ,
	                                      JumlahEfek ,
	                                      NilaiPembelian ,
	                                      NilaiNominal ,
	                                      HPW ,
	                                      Deposito ,
	                                      TotalNilai ,
	                                      KodeBK ,
	                                      Keterangan ,
	                                      SID,
			                              Ascending
	                                    )
	
	                            SELECT  ISNULL(B.ClientCategory,1) 
	                            ,B.Name
	                            ,ISNULL(C.KPDNoContract,'') 
	                            ,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	                            ,isnull(CONVERT(VARCHAR(8), C.MaturityDate, 112),0)
	                            ,isnull(RTRIM(LTRIM(isnull(C.KPDNoAdendum,''))),'')  NomorAdendum 
	                            ,case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') = '19000101' then '0' else isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') end TanggalAdendum
	                            ,G.TotalSubs NilaiInvestasiAwalIDR
	                            ,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAwalNonIDR,CAST(isnull(F.AUM,0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAkhirNonIDR
	                            ,'998'
	                            ,'1'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	                            ,ISNULL(J.NKPDCode,'') KodeBK
	                            ,'998'
	                            ,ISNULL(B.SID,'') SID, 1
	                            FROM dbo.FundClientPosition A
	                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	                            LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	                            LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	                            LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	                            left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
								left join @TableSubs G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
	                            WHERE A.Date = @Date AND A.FundPK = @CFundPK
	

	                            UNION ALL

	                            SELECT  ISNULL(B.ClientCategory,1) 
	                            ,B.Name
	                            ,ISNULL(C.KPDNoContract,'') 
	                            ,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	                            ,isnull(CONVERT(VARCHAR(8), C.MaturityDate, 112),0)
	                            ,isnull(RTRIM(LTRIM(isnull(C.KPDNoAdendum,''))),'')  NomorAdendum 
	                            ,case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') = '19000101' then '0' else isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') end TanggalAdendum
	                            ,G.TotalSubs NilaiInvestasiAwalIDR
	                            ,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAwalNonIDR,CAST(isnull(F.AUM,0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAkhirNonIDR
	                            ,'997'
	                            ,'1'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0' 
	                            ,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,24,@CFundPK) + [dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	                            ,ISNULL(J.NKPDCode,'') KodeBK
	                            ,'997'
	                            ,ISNULL(B.SID,'') SID, 2
	                            FROM dbo.FundClientPosition A
	                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	                            LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	                            LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	                            LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	                            left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
								left join @TableSubs G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
	                            WHERE A.Date = @Date AND A.FundPK = @CFundPK

	                            UNION ALL

	                            SELECT  ISNULL(B.ClientCategory,1) 
	                            ,B.Name
	                            ,ISNULL(C.KPDNoContract,'') 
	                            ,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	                            ,isnull(CONVERT(VARCHAR(8), C.MaturityDate, 112),0)
	                            ,isnull(RTRIM(LTRIM(isnull(C.KPDNoAdendum,''))),'')  NomorAdendum 
								,case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') = '19000101' then '0' else isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') end TanggalAdendum
	                            ,G.TotalSubs NilaiInvestasiAwalIDR
	                            ,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAwalNonIDR,CAST(isnull(F.AUM,0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAkhirNonIDR
	                            ,'996'
	                            ,'1'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	                            ,ISNULL(J.NKPDCode,'') KodeBK
	                            ,'996'
	                            ,ISNULL(B.SID,'') SID,3
	                            FROM dbo.FundClientPosition A
	                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	                            LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	                            LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	                            LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	                            left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
								left join @TableSubs G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
	                            WHERE A.Date = @Date AND A.FundPK = @CFundPK



                            Fetch next From B Into @CFundPK
                            end
                            Close B
                            Deallocate B

                                insert into #Text 

                                select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') + --1
                                '|' + isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'')  +    --2
                                '|' + isnull(RTRIM(LTRIM(isnull(NomorKontrak,''))),'')  +  --3
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalKontrak,'')))),'')  +  --4
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalJatuhTempo,'')))),'')  +  --5
                                '|' + isnull(RTRIM(LTRIM(isnull(NomorAdendum,''))),'')  +  --6
                                '|' + case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'') = '19000101' then '0' else  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'') end  +  --7
                                '|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,''))),'')  +  --8
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAwalNonIDR,'')))),'')  + --9
                                '|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhir,''))),'')  + --10
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAkhirNonIDR,'')))),'')  +  --11
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  --12
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DNatauLN,'')))),'')  + --13
                                '|' + case when type = 5 then '0.0000' else case when type in (1,6) then isnull(RTRIM(LTRIM(isnull(JumlahEfek,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeSaham,'')))),'') else '' end end end + --14
                                --'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  --14
                                '|' + case when type = 5 then '0.0000' else case when type in (1,6) then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(JumlahEfek,''))),'') else '' end end end + --15
                                --'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  --15
                                --'|' + case when type  in (1,5,6) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --16
                                '|' + '0.0000'  +  --16
                                --'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(HPW,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(PriceBond,''))),'') else '' end end +
                                '|' + case when Type = 5 then '0.0000' else isnull(RTRIM(LTRIM(isnull(HPW,''))),'') end +  --17
                                '|' + case when Type = 5 then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else isnull(RTRIM(LTRIM(isnull(Deposito,''))),'') end +  --18
                                '|' + case when type in (1,5,6) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'')  else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'')  else '' end end + --19
                                --'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')  +   --19
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  + --21
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
                                from #KPD

								
								union all

								select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') + --1
                                '|' + isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'')  +    --2
                                '|' + isnull(RTRIM(LTRIM(isnull(NoKontrak,''))),'')  +  --3
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakFrom,'')))),'')  +  --4
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakTo,'')))),'')  +  --5
                                '|' + isnull(RTRIM(LTRIM(isnull(NoAdendum,''))),'')  +  --6
                                '|' + case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglAdendum,'')))),'') = '19000101' then '0' else  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglAdendum,'')))),'') end  +  --7
                                '|' + isnull(RTRIM(LTRIM(isnull(cast(NilaiInvestasiAwalIDR as nvarchar),''))),'')  +  --8
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(NilaiInvestasiAwalNonIDR as nvarchar),'')))),'')  + --9
                                '|' + isnull(RTRIM(LTRIM(isnull(cast(NilaiInvestasiAkhirIDR as nvarchar),''))),'')  + --10
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(NilaiInvestasiAkhirNonIDR as nvarchar),'')))),'')  +  --11
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  --12
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DnLn,'')))),'')  + --13
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(JumlahEfek as nvarchar),'')))),'')  +  --14
                                '|' + isnull(RTRIM(LTRIM(isnull(cast(NilaiPembelian as nvarchar),''))),'')  +  --15
                                '|' + isnull(RTRIM(LTRIM(isnull(cast(NilaiNominal as nvarchar),''))),'')  +  --16
                                '|' + isnull(RTRIM(LTRIM(isnull(cast(HPW as nvarchar),''))),'') +  --17
                                '|' + isnull(RTRIM(LTRIM(isnull(cast(Deposito as nvarchar),''))),'') +  --18
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(TotalNilai as nvarchar),'')))),'')  +   --19
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  + --21
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
                                from @KPD

                                select * from #text

                                     ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _OjkRpt.Fund);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {


                                        _companyCode = _host.Get_CompanyID();
                                        string filePath = Tools.ARIATextPath + _companyCode + "KPD.txt";
                                        FileInfo txtFile = new FileInfo(filePath);
                                        if (txtFile.Exists)
                                        {
                                            txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        }

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                        {
                                            while (dr.Read())
                                            {
                                                file.WriteLine(Convert.ToString(dr["ResultText"]));
                                            }
                                            return true;
                                            //return Tools.HtmlARIATextPath + _companyCode + "KPD.txt";
                                        }

                                    }
                                    return true;
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

                #region Excel

                if (_OjkRpt.DownloadMode == "Excel")
                {
                    try
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.CommandTimeout = 0;

                                cmd.CommandText =
                                                                @"
                            declare @FundPK int

                            --drop table #Text
                            create table #Text(      
                            [ResultText] [nvarchar](1000)  NULL          
                            )                        
        
							declare @TableSubs table (
								FundClientPK int,
								FundPK int,
								TotalSubs numeric(32,8)
							)

                            --drop Table #KPD--
                            Create Table #KPD
                            (AUM nvarchar(50),
                            CashAmount nvarchar(50),
                            InstrumentTypePK int,
                            PK int,
                            KodeNasabah nvarchar(50),
                            NamaNasabah nvarchar(50) COLLATE DATABASE_DEFAULT,
                            NomorKontrak nvarchar(50) COLLATE DATABASE_DEFAULT,
                            TanggalKontrak nvarchar(50),
                            TanggalJatuhTempo nvarchar(50),
                            NomorAdendum nvarchar(50) COLLATE DATABASE_DEFAULT, 
                            TanggalAdendum nvarchar(50) COLLATE DATABASE_DEFAULT,
                            NilaiInvestasiAwalIDR nvarchar(50),
                             NilaiInvestasiAwalNonIDR nvarchar(50),
                             NilaiInvestasiAkhir nvarchar(50),
                            NilaiInvestasiAkhirNonIDR nvarchar(50), 
                            JenisEfek nvarchar(50), 
                            DNatauLN int,
                            JumlahEfek nvarchar(50),
                            NilaiPembelian nvarchar(50), 
                            NilaiNominal nvarchar(50),
                            HPW nvarchar(50),
                             Deposito nvarchar(50), 
                             TotalNilai nvarchar(50),
                             KodeBK  nvarchar(50), 
                             type int,
                             KodeSaham nvarchar(50),
                             MarketValue nvarchar(50),
                             SID nvarchar(50),
                             Ascending int
                            )

                            DECLARE A CURSOR FOR 
                            select FundPK
                            from Fund
                            where [Status] = 2 and FundTypeInternal = 2 
                            Open A
                            Fetch Next From A
                            Into @FundPK

                            While @@FETCH_STATUS = 0
                            Begin

							insert into @TableSubs(FundClientPK,FundPK,TotalSubs)
							select FundClientPK, FundPK, sum(TotalCashAmount) from ClientSubscription where status = 2 and Posted = 1 and fundpk = @FundPK
							group by FundClientPK, FundPK

                            Insert into #KPD (AUM,CashAmount,InstrumentTypePK,PK,KodeNasabah ,NamaNasabah ,NomorKontrak,TanggalKontrak,TanggalJatuhTempo,
                            NomorAdendum, TanggalAdendum,NilaiInvestasiAwalIDR, NilaiInvestasiAwalNonIDR,NilaiInvestasiAkhir,
                            NilaiInvestasiAkhirNonIDR, JenisEfek, DNatauLN,JumlahEfek,NilaiPembelian, NilaiNominal,
                            HPW, Deposito, TotalNilai,KodeBK,type,KodeSaham,MarketValue,SID,Ascending)
                            select F.AUM,E.CashAmount,InstrumentTypePK,ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,H.ClientCategory KodeNasabah ,H.Name NamaNasabah ,NKPDName NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
                            KPDNoAdendum NomorAdendum,isnull(CONVERT(VARCHAR(8), A.KPDDateAdendum, 112),0) TanggalAdendum,J.TotalSubs NilaiInvestasiAwalIDR,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAwalNonIDR,CAST(isnull(F.AUM,0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,
                            CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAkhirNonIDR,case when C.InstrumentTypePK = 5 then I.PTPCode else C.ID end JenisEfek,1 DNatauLN,B.Balance JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 4)) NilaiPembelian,CAST(0 AS DECIMAL(22, 4)) NilaiNominal,
                            case when C.InstrumentTypePK not in (1,5) then   CAST(isnull(B.ClosePrice,0) AS DECIMAL(22, 6))  else CAST(isnull(B.ClosePrice,0) AS DECIMAL(22, 0)) end HPW,CAST(0 AS DECIMAL(22, 4)) Deposito,0 TotalNilai,isnull(I.NKPDCode,0) KodeBK,C.InstrumentTypePK type, '1' KodeSaham,CAST(isnull(B.MarketValue,0) AS DECIMAL(22, 4)) MarketValue,isnull(H.SID,'') SID,0 from Fund A
                            left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
                            left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
                            left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
                            left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
                            left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
                            left join FundClientposition G on A.FundPK = G.FundPK
                            left join FundClient H on G.FundClientPK = H.FundClientPK and H.status = 2
                            left join Bank I on D.BankPK = I.BankPK and I.Status in (1,2)
                            left join @TableSubs J on G.FundClientPK = J.FundClientPK and G.FundPK = G.FundPK
                            --left join ClosePrice I on B.InstrumentPK = I.instrumentPK and I.status = 2 and B.Date = @Date
                            where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
                            Group By  F.AUM,E.CashAmount,C.ID,H.ClientCategory,H.Name,NKPDName,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,I.NKPDCode,C.InstrumentTypePK,B.MarketValue,A.FundPK,H.SID,A.KPDNoAdendum,KPDDateAdendum,I.PTPCode,J.TotalSubs
                            order By C.ID asc


                            Fetch next From A Into @FundPK
                            end
                            Close A
                            Deallocate A


                            update #KPD set  NilaiInvestasiAwalNonIDR = 0, --NilaiInvestasiAkhir = CAST(0 AS DECIMAL(22, 2)),
                            NilaiInvestasiAkhirNonIDR = 0 where PK <> 1


                            Declare @CFundPK int
                            DECLARE @KPD Table 
                            (
	                            KodeNasabah NVARCHAR(5),
	                            NamaNasabah NVARCHAR(200),
	                            NoKontrak NVARCHAR(100),
	                            TglKontrakFrom NVARCHAR(50),
	                            TglKontrakTo NVARCHAR(50),
	                            NoAdendum NVARCHAR(100),
	                            TglAdendum NVARCHAR(100),
	                            NilaiInvestasiAwalIDR NUMERIC(22,4),
	                            NilaiInvestasiAwalNonIDR NUMERIC(22,4),
	                            NilaiInvestasiAkhirIDR NUMERIC(22,4),
	                            NilaiInvestasiAkhirNonIDR NUMERIC(22,4),
	                            JenisEfek NVARCHAR(100),
	                            DnLn NVARCHAR(2),
	                            JumlahEfek NUMERIC(22,4),
	                            NilaiPembelian NUMERIC(22,4),
	                            NilaiNominal NUMERIC(22,4),
	                            HPW NUMERIC(22,4),
	                            Deposito NUMERIC(22,4),
	                            TotalNilai NUMERIC(22,4),
	                            KodeBK NVARCHAR(10),
	                            Keterangan NVARCHAR(50),
	                            SID NVARCHAR(100),
	                            Ascending int
                            )

                            DECLARE B CURSOR FOR 
                            SELECT distinct A.FundPK
                            from dbo.FundClientPosition A
                            LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
                            where A.Date = @Date and B.FundTypeInternal = 2 
                            AND A.UnitAmount > 1 
                            Open B
                            Fetch Next From B
                            Into @CFundPK
                            While @@FETCH_STATUS = 0
                            Begin
	
	                            INSERT INTO @KPD
	                                    ( KodeNasabah ,
	                                      NamaNasabah ,
	                                      NoKontrak ,
	                                      TglKontrakFrom ,
	                                      TglKontrakTo ,
	                                      NoAdendum ,
	                                      TglAdendum ,
	                                      NilaiInvestasiAwalIDR ,
	                                      NilaiInvestasiAwalNonIDR ,
	                                      NilaiInvestasiAkhirIDR ,
	                                      NilaiInvestasiAkhirNonIDR ,
	                                      JenisEfek ,
	                                      DnLn ,
	                                      JumlahEfek ,
	                                      NilaiPembelian ,
	                                      NilaiNominal ,
	                                      HPW ,
	                                      Deposito ,
	                                      TotalNilai ,
	                                      KodeBK ,
	                                      Keterangan ,
	                                      SID,
			                              Ascending
	                                    )
	
	                            SELECT  ISNULL(B.ClientCategory,1) 
	                            ,B.Name
	                            ,ISNULL(C.KPDNoContract,'') 
	                            ,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	                            ,isnull(CONVERT(VARCHAR(8), C.MaturityDate, 112),0)
	                            ,isnull(RTRIM(LTRIM(isnull(C.KPDNoAdendum,''))),'')  NomorAdendum 
	                            ,case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') = '19000101' then '0' else isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') end TanggalAdendum
	                            ,G.TotalSubs NilaiInvestasiAwalIDR
	                            ,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAwalNonIDR,CAST(isnull(F.AUM,0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAkhirNonIDR
	                            ,'998'
	                            ,'1'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	                            ,ISNULL(J.NKPDCode,'') KodeBK
	                            ,'998'
	                            ,ISNULL(B.SID,'') SID, 1
	                            FROM dbo.FundClientPosition A
	                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	                            LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	                            LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	                            LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	                            left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
								left join @TableSubs G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
	                            WHERE A.Date = @Date AND A.FundPK = @CFundPK
	

	                            UNION ALL

	                            SELECT  ISNULL(B.ClientCategory,1) 
	                            ,B.Name
	                            ,ISNULL(C.KPDNoContract,'') 
	                            ,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	                            ,isnull(CONVERT(VARCHAR(8), C.MaturityDate, 112),0)
	                            ,isnull(RTRIM(LTRIM(isnull(C.KPDNoAdendum,''))),'')  NomorAdendum 
	                            ,case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') = '19000101' then '0' else isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') end TanggalAdendum
	                            ,G.TotalSubs NilaiInvestasiAwalIDR
	                            ,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAwalNonIDR,CAST(isnull(F.AUM,0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAkhirNonIDR
	                            ,'997'
	                            ,'1'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0' 
	                            ,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,24,@CFundPK) + [dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	                            ,ISNULL(J.NKPDCode,'') KodeBK
	                            ,'997'
	                            ,ISNULL(B.SID,'') SID, 2
	                            FROM dbo.FundClientPosition A
	                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	                            LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	                            LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	                            LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	                            left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
								left join @TableSubs G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
	                            WHERE A.Date = @Date AND A.FundPK = @CFundPK

	                            UNION ALL

	                            SELECT  ISNULL(B.ClientCategory,1) 
	                            ,B.Name
	                            ,ISNULL(C.KPDNoContract,'') 
	                            ,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	                            ,isnull(CONVERT(VARCHAR(8), C.MaturityDate, 112),0)
	                            ,isnull(RTRIM(LTRIM(isnull(C.KPDNoAdendum,''))),'')  NomorAdendum 
								,case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') = '19000101' then '0' else isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),'')))),'') end TanggalAdendum
	                            ,G.TotalSubs NilaiInvestasiAwalIDR
	                            ,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAwalNonIDR,CAST(isnull(F.AUM,0) AS DECIMAL(22, 4))  NilaiInvestasiAkhir,CAST(0 AS DECIMAL(22, 4)) NilaiInvestasiAkhirNonIDR
	                            ,'996'
	                            ,'1'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,'0'
	                            ,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	                            ,ISNULL(J.NKPDCode,'') KodeBK
	                            ,'996'
	                            ,ISNULL(B.SID,'') SID,3
	                            FROM dbo.FundClientPosition A
	                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	                            LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	                            LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	                            LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	                            left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
								left join @TableSubs G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
	                            WHERE A.Date = @Date AND A.FundPK = @CFundPK



                            Fetch next From B Into @CFundPK
                            end
                            Close B
                            Deallocate B







                            select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') KodeNasabah --1
                            , isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'') NamaNasabah     --2
                            , isnull(RTRIM(LTRIM(isnull(NomorKontrak,''))),'')  NomorKontrak  --3
                            , isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalKontrak,'')))),'')  TanggalKontrak  --4
                            , isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalJatuhTempo,'')))),'')  TanggalJatuhTempo  --5
                            , isnull(RTRIM(LTRIM(isnull(NomorAdendum,''))),'')  NomorAdendum  --6
                            , case when isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'') = '19000101' then '0' else isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'') end  TanggalAdendum  --7
                            , isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,''))),'') NilaiInvestasiAwalIDR   --8
                            , isnull(RTRIM(LTRIM(isnull(0.0000,''))),'') NilaiInvestasiAwalNonIDR  --9
                            , isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhir,''))),'') NilaiInvestasiAkhir  --10
                            , isnull(RTRIM(LTRIM(isnull(0.0000,''))),'') NilaiInvestasiAkhirNonIDR   --11
                            , isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  JenisEfek  --12
                            , isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DNatauLN,'')))),'') KodeKategoriEfek  --13
                            , case when Type = 5 then '0.0000' else case when type in (1,6) then isnull(JumlahEfek,'') else case when type not in (1) then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeSaham,'')))),'') else '' end end end JumlahEfek --14
                            --'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  --14
                            , case when Type = 5 then '0.0000' else case when type in (1,6) then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else case when type not in (1) then isnull(RTRIM(LTRIM(isnull(JumlahEfek,''))),'') else '' end end end NilaiPembelian --15
                            --'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  --15
                            --'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --16
                            , isnull(RTRIM(LTRIM(isnull(0.0000,''))),'')  NilaiNominal  --16
                            --'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(HPW,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(PriceBond,''))),'') else '' end end +
                            , case when Type = 5 then '0.0000' else isnull(RTRIM(LTRIM(isnull(HPW,''))),'') end HPW   --17
                            , case when Type = 5 then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else isnull(RTRIM(LTRIM(isnull(Deposito,''))),'') end  Deposito  --18
                            , case when type in (1,5,6) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end TotalInvestasi --19
                            --'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')    --19
                            , isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') KodeBK --20
                            , isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'') Keterangan --21
                            , isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') SID --22
                            ,Ascending
                            from #KPD

                            union all

                            SELECT * FROM @KPD

                            order by SID,Ascending
 ";


                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);


                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "KPD" + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "KPD" + "_" + _userID + ".pdf";
                                        FileInfo excelFile = new FileInfo(filePath);
                                        if (excelFile.Exists)
                                        {
                                            excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                            excelFile = new FileInfo(filePath);
                                        }


                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                        using (ExcelPackage package = new ExcelPackage(excelFile))
                                        {
                                            package.Workbook.Properties.Title = "KPDReport";
                                            package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                            package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                            package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                            package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                            package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KPD Report");


                                            //ATUR DATA GROUPINGNYA DULU
                                            List<KPD> rList = new List<KPD>();
                                            while (dr0.Read())
                                            {
                                                KPD rSingle = new KPD();
                                                rSingle.KodeNasabah = Convert.ToString(dr0["KodeNasabah"]);
                                                rSingle.NamaNasabah = Convert.ToString(dr0["NamaNasabah"]);
                                                rSingle.NomorKontrak = Convert.ToString(dr0["NomorKontrak"]);
                                                rSingle.TanggalKontrak = Convert.ToString(dr0["TanggalKontrak"]);
                                                rSingle.TanggalJatuhTempo = Convert.ToString(dr0["TanggalJatuhTempo"]);
                                                rSingle.NomorAdendum = Convert.ToString(dr0["NomorAdendum"]);
                                                rSingle.TanggalAdendum = Convert.ToString(dr0["TanggalAdendum"]);
                                                rSingle.NilaiInvestasiAwalIDR = Convert.ToString(dr0["NilaiInvestasiAwalIDR"]);
                                                rSingle.NilaiInvestasiAwalNonIDR = Convert.ToString(dr0["NilaiInvestasiAwalNonIDR"]);
                                                rSingle.NilaiInvestasiAkhir = Convert.ToString(dr0["NilaiInvestasiAkhir"]);
                                                rSingle.NilaiInvestasiAkhirNonIDR = Convert.ToString(dr0["NilaiInvestasiAkhirNonIDR"]);
                                                rSingle.JenisEfek = Convert.ToString(dr0["JenisEfek"]);
                                                rSingle.KodeKategoriEfek = Convert.ToInt32(dr0["KodeKategoriEfek"]);
                                                rSingle.JumlahEfek = Convert.ToString(dr0["JumlahEfek"]);
                                                rSingle.NilaiPembelian = Convert.ToString(dr0["NilaiPembelian"]);
                                                rSingle.NilaiNominal = Convert.ToString(dr0["NilaiNominal"]);
                                                rSingle.HPW = Convert.ToString(dr0["HPW"]);
                                                rSingle.Deposito = Convert.ToString(dr0["Deposito"]);
                                                rSingle.TotalInvestasi = Convert.ToString(dr0["TotalInvestasi"]);
                                                rSingle.KodeBK = Convert.ToString(dr0["KodeBK"]);
                                                rSingle.Keterangan = Convert.ToString(dr0["Keterangan"]);
                                                rSingle.SID = Convert.ToString(dr0["SID"]);
                                                rList.Add(rSingle);

                                            }

                                            var QueryByClientID =
                                             from r in rList
                                             group r by new { } into rGroup
                                             select rGroup;

                                            int incRowExcel = 0;
                                            int _startRowDetail = 0;
                                            foreach (var rsHeader in QueryByClientID)
                                            {

                                                incRowExcel++;
                                                //Row A = 2
                                                int RowA = incRowExcel;
                                                int RowB = incRowExcel + 1;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.WrapText = true;

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

                                                worksheet.Cells[incRowExcel, 1].Value = "Kode Nasabah";
                                                worksheet.Cells[incRowExcel, 2].Value = "Nama Nasabah";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nomer Kontrak";
                                                worksheet.Cells[incRowExcel, 4].Value = "Tanggal Kontrak";
                                                worksheet.Cells[incRowExcel, 5].Value = "Tanggal Jatuh Tempo";
                                                worksheet.Cells[incRowExcel, 6].Value = "Nomer Adendum";
                                                worksheet.Cells[incRowExcel, 7].Value = "Tanggal Adendum";
                                                worksheet.Cells[incRowExcel, 8].Value = "Nilai Investasi Awal IDR";
                                                worksheet.Cells[incRowExcel, 9].Value = "Nilai Investasi Awal Non IDR";
                                                worksheet.Cells[incRowExcel, 10].Value = "Nilai investtasi Akhir IDR";
                                                worksheet.Cells[incRowExcel, 11].Value = "Nilai investasi Akhir Non IDR";
                                                worksheet.Cells[incRowExcel, 12].Value = "Kode Efek";
                                                worksheet.Cells[incRowExcel, 13].Value = "Kode Kategori Efek";
                                                worksheet.Cells[incRowExcel, 14].Value = "Jumlah Efek";
                                                worksheet.Cells[incRowExcel, 15].Value = "Nilai Pembelian";
                                                worksheet.Cells[incRowExcel, 16].Value = "Nilai Nominal";
                                                worksheet.Cells[incRowExcel, 17].Value = "HPW";
                                                worksheet.Cells[incRowExcel, 18].Value = "Deposito";
                                                worksheet.Cells[incRowExcel, 19].Value = "Total Investtasi";
                                                worksheet.Cells[incRowExcel, 20].Value = "Kode BK";
                                                worksheet.Cells[incRowExcel, 21].Value = "Keterangan";
                                                worksheet.Cells[incRowExcel, 22].Value = "SID";

                                                //area header
                                                int _endRowDetail = 0;
                                                int _startRow = incRowExcel;
                                                incRowExcel++;
                                                _startRowDetail = incRowExcel;
                                                foreach (var rsDetail in rsHeader)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.KodeNasabah;
                                                    worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaNasabah;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.NomorKontrak;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.TanggalKontrak;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.TanggalJatuhTempo;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NomorAdendum;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.TanggalAdendum;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.NilaiInvestasiAwalIDR;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.NilaiInvestasiAwalNonIDR;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.NilaiInvestasiAkhir;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.NilaiInvestasiAkhirNonIDR;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.JenisEfek;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.KodeKategoriEfek;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.JumlahEfek;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.NilaiPembelian;
                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.NilaiNominal;
                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.HPW;
                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.Deposito;
                                                    worksheet.Cells[incRowExcel, 19].Value = rsDetail.TotalInvestasi;
                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.KodeBK;
                                                    worksheet.Cells[incRowExcel, 21].Value = rsDetail.Keterangan;
                                                    worksheet.Cells[incRowExcel, 22].Value = rsDetail.SID;

                                                    _endRowDetail = incRowExcel;

                                                    incRowExcel++;


                                                }

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                                                //worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                                //worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                                //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Calculate();
                                                //worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Calculate();
                                                //worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Calculate();
                                                //worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Calculate();
                                                //worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Calculate();
                                                //worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Calculate();
                                                //worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Calculate();
                                                //worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Calculate();
                                                //worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                                //worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Calculate();
                                                //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Calculate();
                                                //worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Calculate();
                                                //worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Calculate();
                                                //worksheet.Cells["N" + incRowExcel + ":O" + incRowExcel].Calculate();
                                                //worksheet.Cells["O" + incRowExcel + ":P" + incRowExcel].Calculate();
                                                //worksheet.Cells["P" + incRowExcel + ":Q" + incRowExcel].Calculate();
                                                //worksheet.Cells["Q" + incRowExcel + ":R" + incRowExcel].Calculate();
                                                //worksheet.Cells["R" + incRowExcel + ":S" + incRowExcel].Calculate();
                                                //worksheet.Cells["S" + incRowExcel + ":T" + incRowExcel].Calculate();
                                                //worksheet.Cells["T" + incRowExcel + ":U" + incRowExcel].Calculate();
                                                //worksheet.Cells["U" + incRowExcel + ":V" + incRowExcel].Calculate();
                                                //worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                //worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Font.Bold = true;

                                                worksheet.Cells["A" + _startRow + ":V" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRow + ":V" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRow + ":V" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                            }



                                            worksheet.PrinterSettings.FitToPage = true;
                                            worksheet.PrinterSettings.FitToWidth = 1;
                                            worksheet.PrinterSettings.FitToHeight = 1;
                                            worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 22];
                                            worksheet.Column(1).Width = 9;
                                            worksheet.Column(2).Width = 35;
                                            worksheet.Column(3).Width = 20;
                                            worksheet.Column(4).Width = 20;
                                            worksheet.Column(5).Width = 20;
                                            worksheet.Column(6).Width = 20;
                                            worksheet.Column(7).Width = 20;
                                            worksheet.Column(8).Width = 20;
                                            worksheet.Column(9).Width = 20;
                                            worksheet.Column(10).Width = 20;
                                            worksheet.Column(11).Width = 20;
                                            worksheet.Column(12).Width = 20;
                                            worksheet.Column(13).Width = 20;
                                            worksheet.Column(14).Width = 20;
                                            worksheet.Column(15).Width = 20;
                                            worksheet.Column(16).Width = 20;
                                            worksheet.Column(17).Width = 20;
                                            worksheet.Column(18).Width = 20;
                                            worksheet.Column(19).Width = 20;
                                            worksheet.Column(20).Width = 20;
                                            worksheet.Column(21).Width = 20;
                                            worksheet.Column(22).Width = 20;



                                            // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                            //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                            worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                            worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                            worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 KPD REPORT";

                                            // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                            worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                            worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                            worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                            worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                            //Image img = Image.FromFile(Tools.ReportImage);
                                            //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                            //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                            worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                            string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                            ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                            //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                            package.Save();
                                            return true;
                                        }
                                    }
                                }

                            }

                        }
                    }
                    catch (Exception err)
                    {
                        return false;
                        throw err;
                    }

                }



                #endregion

                return true;
            }//else if
            #endregion

            #region KYC Risk Profile
            if (_OjkRpt.ReportName.Equals("18"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            //string _paramFundFrom = "";

                            //if (!_host.findString(_CustodianRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_CustodianRpt.FundFrom))
                            //{
                            //    _paramFundFrom = "And A.FundPK  in ( " + _CustodianRpt.FundFrom + " ) ";
                            //}
                            //else
                            //{
                            //    _paramFundFrom = "";
                            //}



                            cmd.CommandText = @"
                            declare @yesterday date
                            --declare @TotalSID int
                            set @yesterday = dbo.FWorkingDay(@DateTo,-1)

                            if object_id('tempdb..#FundUnitPosition', 'u') is not null drop table #FundUnitPosition 

                            create table #FundUnitPosition
                            (
	                            TotalUnit NUMERIC(22,4),
	                            FundClientPK int,
	                            FundPK int
                            )
                            CREATE CLUSTERED INDEX indx_FundUnitPosition ON #FundUnitPosition (FundPK);

                            if object_id('tempdb..#CloseNAVPosition', 'u') is not null drop table #CloseNAVPosition
                            create table #CloseNAVPosition
                            (
	                            NAV NUMERIC(22,4),
	                            FundPK int
                            )
                            CREATE CLUSTERED INDEX indx_CloseNAVPosition ON #CloseNAVPosition (FundPK);

                            INSERT INTO #FundUnitPosition
                                    (  TotalUnit,FundClientPK,FundPK )
		                            SELECT sum(isnull(UnitAmount,0)),A.FundClientPK,FundPK FROM dbo.FundClientPosition A
		                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
		                            WHERE Date = @yesterday and isnull(UnitAmount,0) != 0 
		                            group by A.FundClientPK,FundPK


                            INSERT INTO #CloseNAVPosition
                                    ( NAV,FundPK )
		                            SELECT case when B.CurrencyPK = 1 then A.NAV else A.Nav * isnull(H.Rate,1) end,A.FundPK FROM dbo.CloseNAV A
		                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
		                            left join CurrencyRate H on B.CurrencyPK = H.CurrencyPK and H.status in (1,2) and H.date = (select max(B.date) from Currency A 
		                            left join CurrencyRate B on A.CurrencyPK = B.CurrencyPK where date <= @DateTo and B.CurrencyPK = A.CurrencyPK and A.status in (1,2) and B.status in (1,2))
		                            WHERE A.Date = @DateTo and A.status = 2

                            select D.Name ClientName,G.DescOne JenisClient,case when D.ClientCategory = 1 then F.DescOne else H.DescOne end JanisPekerjaan,
                            case when D.ClientCategory = 1 then isnull(D.AlamatInd1,D.AlamatInd2) else isnull(I.DescOne,'') end TempatTinggal,isnull(E.DescOne,'') KYCRiskProfile,
                            isnull(C.ID,'') NamaReksaDana,isnull(A.TotalUnit,0) Unit, isnull(B.NAV,0) NAV,sum(isnull(A.TotalUnit,0) * isnull(B.NAV,0)) Amount 
                            from #FundUnitPosition A
                            left join #CloseNAVPosition B on A.FundPK = B.FundPK
                            left join Fund C on A.FundPK = C.FundPK and C.Status in (1,2)
                            left join FundClient D on A.FundClientPK = D.FundClientPK and D.status in (1,2)
                            left join MasterValue E on D.KYCRiskProfile = E.Code and E.ID = 'KYCRiskProfile' and E.status in (1,2)
                            left join MasterValue F on D.Pekerjaan = F.Code and F.ID = 'Occupation' and F.status in (1,2)
                            left join MasterValue G on D.ClientCategory = G.Code and G.ID = 'ClientCategory' and G.status in (1,2)
                            left join MasterValue H on D.Tipe = H.Code and H.ID = 'CompanyType' and H.status in (1,2)
                            left join MasterValue I on D.CompanyCityName = I.Code and I.ID = 'CityRHB' and I.status in (1,2)
                            group by D.Name,G.DescOne,D.ClientCategory,D.AlamatInd1,D.CompanyCityName,D.AlamatInd2,C.ID,A.TotalUnit,B.NAV,F.DescOne,H.DescOne,I.DescOne,E.DescOne                              
                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateTo", _OjkRpt.ValueDateFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "KYCRiskProfile" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "KYCRiskProfile" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "ComplianceReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KYC Risk Profile");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<KYCRiskProfile> rList = new List<KYCRiskProfile>();
                                        while (dr0.Read())
                                        {

                                            KYCRiskProfile rSingle = new KYCRiskProfile();
                                            rSingle.JenisClient = Convert.ToString(dr0["JenisClient"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.JenisPekerjaan = Convert.ToString(dr0["JanisPekerjaan"]);
                                            rSingle.TempatTinggal = Convert.ToString(dr0["TempatTinggal"]);
                                            rSingle.RiskProfile = Convert.ToString(dr0["KYCRiskProfile"]);
                                            rSingle.Fund = Convert.ToString(dr0["NamaReksaDana"]);
                                            rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);





                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { r } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Report KYC Risk Profile";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "As of";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_OjkRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                        worksheet.Cells[incRowExcel, 1].Value = "Jenis Client";
                                        worksheet.Cells[incRowExcel, 2].Value = "Client Name";
                                        worksheet.Cells[incRowExcel, 3].Value = "Jenis Pekerjaan";
                                        worksheet.Cells[incRowExcel, 4].Value = "TempatTinggal";
                                        worksheet.Cells[incRowExcel, 5].Value = "KYC Risk Profile";
                                        worksheet.Cells[incRowExcel, 6].Value = "Fund";
                                        worksheet.Cells[incRowExcel, 7].Value = "Unit";
                                        worksheet.Cells[incRowExcel, 8].Value = "NAV";
                                        worksheet.Cells[incRowExcel, 9].Value = "Amoutn";

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                        worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                        foreach (var rsHeader in GroupByReference)
                                        {





                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.JenisClient;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.JenisPekerjaan;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.TempatTinggal;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.RiskProfile;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Unit;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.NAV;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                                no++;
                                                _endRowDetail = incRowExcel;

                                            }



                                            //worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                            //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 5].Calculate();
                                            //int last = incRowExcel - 1;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                                            //foreach (var rsHeader in GroupByReference)
                                            //{

                                        }




                                        //worksheet.Cells[incRowExcel, 5, incRowExcel, 8].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 5, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 5, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //incRowExcel++;

                                        //incRowExcel++;



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel + 8, 8];
                                        worksheet.Column(1).Width = 21;
                                        worksheet.Column(2).Width = 21;
                                        worksheet.Column(3).Width = 21;
                                        worksheet.Column(4).Width = 21;
                                        worksheet.Column(5).Width = 21;
                                        worksheet.Column(6).Width = 21;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 21;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 KYC Risk Profile";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_OjkRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }

                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            else
            {
                return false;
            }
        }

        public int Investment_ApproveDealingBySelected(Investment _investment)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramFundA = "";
                        string _paramCounterpart = "";
                        string _paramCounterpartA = "";
                        string _paramInstrumentType = "";
                        string _paramTrxType = "";
                        string _paramDealingPK = "";

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramDealingPK = " And DealingPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramDealingPK = " And DealingPK in (0) ";
                        }
                        if (_investment.InstrumentTypePK == 2)
                        {
                            _paramInstrumentType = " And InstrumentTypePK in (2,3,9,13,15)  ";
                        }
                        else
                        {
                            _paramInstrumentType = " And InstrumentTypePK = @InstrumentTypePK ";
                        }
                        if (_investment.InstrumentTypePK == 5 && _investment.TrxType == 1)
                        {
                            _paramTrxType = " and TrxType in (1,3) ";
                        }
                        else
                        {
                            _paramTrxType = " and TrxType = @TrxType ";
                        }
                        if (_investment.InstrumentTypePK == 1)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                         	DECLARE @InvestmentPK int
    declare @DealingPK int
    declare @HistoryPK INT
    DECLARE @ValueDate DATETIME
    

    declare @Comm numeric (19,2)
    declare @Levy numeric (19,2)
    declare @KPEI numeric (19,2)
    declare @VAT numeric (19,2)
    declare @WHT numeric (19,2)
    declare @OTC numeric (19,2)
    declare @TaxInterest numeric (19,2)
    declare @TaxGain numeric (19,2)
    declare @TaxSell numeric (19,2)
    declare @TotalAmount numeric (19,2)

    declare @FundCashRefPK int 
    declare @SettlementPK int
    declare @CommissionPercent numeric (19,12),@RoundingCommission int,@DecimalCommission int,@VATPercent numeric (22,4),@RoundingVAT int,@DecimalVAT int,@LevyPercent numeric (22,4),@RoundingLevy int,@DecimalLevy int,
    @KPEIPercent numeric (22,4),@RoundingKPEI int,@DecimalKPEI int,@WHTPercent numeric (22,4),@RoundingWHT int,@DecimalWHT int,@OTCPercent numeric (22,4),@RoundingOTC int,@DecimalOTC int,
    @IncomeTaxInterestPercent numeric (22,4),@RoundingTaxInterest int,@DecimalTaxInterest int,@IncomeTaxGainPercent numeric (22,4),@RoundingTaxGain int,@DecimalTaxGain int,@IncomeTaxSellPercent numeric (22,4),@RoundingTaxSell int,@DecimalTaxSell int,@BoardType int,@FundPK int,@CounterpartPK int


	DECLARE @CounterpartCommissionTemp TABLE
    (
			[CounterpartCommissionPK] [int] NOT NULL,
			[HistoryPK] [int] NOT NULL,
			[Status] [int] NOT NULL,
			[Notes] [nvarchar](1000) NULL,
			[Date] [datetime] NULL,
			[CounterpartPK] [int] NULL,
			[BitIncludeTax] [bit] NOT NULL,
			[BoardType] [int] NULL,
			[CommissionPercent] [numeric](19, 8) NULL,
			[LevyPercent] [numeric](19, 8) NULL,
			[KPEIPercent] [numeric](19, 8) NULL,
			[VATPercent] [numeric](19, 8) NULL,
			[WHTPercent] [numeric](19, 8) NULL,
			[OTCPercent] [numeric](19, 8) NULL,
			[IncomeTaxSellPercent] [numeric](19, 8) NULL,
			[IncomeTaxInterestPercent] [numeric](19, 8) NULL,
			[IncomeTaxGainPercent] [numeric](19, 8) NULL,
			[EntryUsersID] [nvarchar](70) NULL,
			[EntryTime] [datetime] NULL,
			[UpdateUsersID] [nvarchar](70) NULL,
			[UpdateTime] [datetime] NULL,
			[ApprovedUsersID] [nvarchar](70) NULL,
			[ApprovedTime] [datetime] NULL,
			[VoidUsersID] [nvarchar](70) NULL,
			[VoidTime] [datetime] NULL,
			[DBUserID] [nvarchar](50) NULL,
			[DBTerminalID] [nvarchar](50) NULL,
			[LastUpdate] [datetime] NULL,
			[LastUpdateDB] [datetime] NULL,
			[FundPK] [int] NOT NULL,
			[RoundingCommission] [int] NULL,
			[DecimalCommission] [int] NULL,
			[RoundingLevy] [int] NULL,
			[DecimalLevy] [int] NULL,
			[RoundingKPEI] [int] NULL,
			[DecimalKPEI] [int] NULL,
			[RoundingVAT] [int] NULL,
			[DecimalVAT] [int] NULL,
			[RoundingWHT] [int] NULL,
			[DecimalWHT] [int] NULL,
			[RoundingOTC] [int] NULL,
			[DecimalOTC] [int] NULL,
			[RoundingTaxSell] [int] NULL,
			[DecimalTaxSell] [int] NULL,
			[RoundingTaxInterest] [int] NULL,
			[DecimalTaxInterest] [int] NULL,
			[RoundingTaxGain] [int] NULL,
			[DecimalTaxGain] [int] NULL
	)

	
                            
    DECLARE A CURSOR FOR 
	
    select InstrumentTypePK,TrxType,FundPK,BoardType,CounterpartPK,ValueDate from Investment 
    where statusInvestment = 2 
    and statusDealing = 1 and OrderStatus in ('O','P') and ValueDate between @DateFrom and @DateFrom and InstrumentTypePK = 1  
	" + _paramDealingPK + _paramTrxType + _paramFund + _paramCounterpart + @"

    Open A
    Fetch Next From A
    Into @InstrumentTypePK,@TrxType,@FundPK,@BoardType,@CounterpartPK,@ValueDate

    While @@FETCH_STATUS = 0
    BEGIN    

		
	delete  @CounterpartCommissionTemp
		

		
		if EXISTS(
				SELECT * FROM dbo.CounterpartCommission WHERE fundPK = @FundPK AND BoardType = @BoardType
				AND CounterpartPK = @CounterpartPK AND Date <= @ValueDate
				AND status = 2
			)
			BEGIN
	
				INSERT INTO @CounterpartCommissionTemp	
		        ( CounterpartCommissionPK ,
		          HistoryPK ,
		          Status ,
		          Notes ,
		          Date ,
		          CounterpartPK ,
		          BitIncludeTax ,
		          BoardType ,
		          CommissionPercent ,
		          LevyPercent ,
		          KPEIPercent ,
		          VATPercent ,
		          WHTPercent ,
		          OTCPercent ,
		          IncomeTaxSellPercent ,
		          IncomeTaxInterestPercent ,
		          IncomeTaxGainPercent ,
		          EntryUsersID ,
		          EntryTime ,
		          UpdateUsersID ,
		          UpdateTime ,
		          ApprovedUsersID ,
		          ApprovedTime ,
		          VoidUsersID ,
		          VoidTime ,
		          DBUserID ,
		          DBTerminalID ,
		          LastUpdate ,
		          LastUpdateDB ,
		          FundPK ,
		          RoundingCommission ,
		          DecimalCommission ,
		          RoundingLevy ,
		          DecimalLevy ,
		          RoundingKPEI ,
		          DecimalKPEI ,
		          RoundingVAT ,
		          DecimalVAT ,
		          RoundingWHT ,
		          DecimalWHT ,
		          RoundingOTC ,
		          DecimalOTC ,
		          RoundingTaxSell ,
		          DecimalTaxSell ,
		          RoundingTaxInterest ,
		          DecimalTaxInterest ,
		          RoundingTaxGain ,
		          DecimalTaxGain
		        )
				SELECT TOP 1 CounterpartCommissionPK ,
		          HistoryPK ,
		          Status ,
		          Notes ,
		          Date ,
		          CounterpartPK ,
		          BitIncludeTax ,
		          BoardType ,
		          CommissionPercent ,
		          LevyPercent ,
		          KPEIPercent ,
		          VATPercent ,
		          WHTPercent ,
		          OTCPercent ,
		          IncomeTaxSellPercent ,
		          IncomeTaxInterestPercent ,
		          IncomeTaxGainPercent ,
		          EntryUsersID ,
		          EntryTime ,
		          UpdateUsersID ,
		          UpdateTime ,
		          ApprovedUsersID ,
		          ApprovedTime ,
		          VoidUsersID ,
		          VoidTime ,
		          DBUserID ,
		          DBTerminalID ,
		          LastUpdate ,
		          LastUpdateDB ,
		          FundPK ,
		          RoundingCommission ,
		          DecimalCommission ,
		          RoundingLevy ,
		          DecimalLevy ,
		          RoundingKPEI ,
		          DecimalKPEI ,
		          RoundingVAT ,
		          DecimalVAT ,
		          RoundingWHT ,
		          DecimalWHT ,
		          RoundingOTC ,
		          DecimalOTC ,
		          RoundingTaxSell ,
		          DecimalTaxSell ,
		          RoundingTaxInterest ,
		          DecimalTaxInterest ,
		          RoundingTaxGain ,
		          DecimalTaxGain FROM dbo.CounterpartCommission WHERE fundPK = @FundPK AND BoardType = @BoardType
				AND CounterpartPK = @CounterpartPK AND Date = (
					SELECT MAX(Date) FROM dbo.CounterpartCommission WHERE Date <= @ValueDate 
					AND fundPK = @FundPK AND BoardType = @BoardType
					AND CounterpartPK = @CounterpartPK AND Status = 2
				) AND status = 2
			END
			ELSE
			BEGIN

				INSERT INTO @CounterpartCommissionTemp	
		        ( CounterpartCommissionPK ,
		          HistoryPK ,
		          Status ,
		          Notes ,
		          Date ,
		          CounterpartPK ,
		          BitIncludeTax ,
		          BoardType ,
		          CommissionPercent ,
		          LevyPercent ,
		          KPEIPercent ,
		          VATPercent ,
		          WHTPercent ,
		          OTCPercent ,
		          IncomeTaxSellPercent ,
		          IncomeTaxInterestPercent ,
		          IncomeTaxGainPercent ,
		          EntryUsersID ,
		          EntryTime ,
		          UpdateUsersID ,
		          UpdateTime ,
		          ApprovedUsersID ,
		          ApprovedTime ,
		          VoidUsersID ,
		          VoidTime ,
		          DBUserID ,
		          DBTerminalID ,
		          LastUpdate ,
		          LastUpdateDB ,
		          FundPK ,
		          RoundingCommission ,
		          DecimalCommission ,
		          RoundingLevy ,
		          DecimalLevy ,
		          RoundingKPEI ,
		          DecimalKPEI ,
		          RoundingVAT ,
		          DecimalVAT ,
		          RoundingWHT ,
		          DecimalWHT ,
		          RoundingOTC ,
		          DecimalOTC ,
		          RoundingTaxSell ,
		          DecimalTaxSell ,
		          RoundingTaxInterest ,
		          DecimalTaxInterest ,
		          RoundingTaxGain ,
		          DecimalTaxGain
		        )
				SELECT TOP 1 CounterpartCommissionPK ,
		          HistoryPK ,
		          Status ,
		          Notes ,
		          Date ,
		          CounterpartPK ,
		          BitIncludeTax ,
		          BoardType ,
		          CommissionPercent ,
		          LevyPercent ,
		          KPEIPercent ,
		          VATPercent ,
		          WHTPercent ,
		          OTCPercent ,
		          IncomeTaxSellPercent ,
		          IncomeTaxInterestPercent ,
		          IncomeTaxGainPercent ,
		          EntryUsersID ,
		          EntryTime ,
		          UpdateUsersID ,
		          UpdateTime ,
		          ApprovedUsersID ,
		          ApprovedTime ,
		          VoidUsersID ,
		          VoidTime ,
		          DBUserID ,
		          DBTerminalID ,
		          LastUpdate ,
		          LastUpdateDB ,
		          FundPK ,
		          RoundingCommission ,
		          DecimalCommission ,
		          RoundingLevy ,
		          DecimalLevy ,
		          RoundingKPEI ,
		          DecimalKPEI ,
		          RoundingVAT ,
		          DecimalVAT ,
		          RoundingWHT ,
		          DecimalWHT ,
		          RoundingOTC ,
		          DecimalOTC ,
		          RoundingTaxSell ,
		          DecimalTaxSell ,
		          RoundingTaxInterest ,
		          DecimalTaxInterest ,
		          RoundingTaxGain ,
		          DecimalTaxGain FROM dbo.CounterpartCommission WHERE fundPK = 0 AND BoardType = @BoardType
				AND CounterpartPK = @CounterpartPK  AND Date = (
					SELECT MAX(Date) FROM dbo.CounterpartCommission WHERE Date <= @ValueDate 
					AND fundPK = 0 AND BoardType = @BoardType
					AND CounterpartPK = @CounterpartPK AND Status = 2
				) AND status = 2
			END

	    DECLARE B CURSOR FOR 
	                            
        Select C.FundCashRefPK,InvestmentPK,DealingPK,HistoryPK,CommissionPercent ,LevyPercent,KPEIPercent,VATPercent,
        WHTPercent,OTCPercent,IncomeTaxInterestPercent,IncomeTaxGainPercent,IncomeTaxSellPercent,
        C.Comm,C.Levy,C.KPEI,C.VAT,C.WHT,C.OTC,C.TaxInterest,C.TaxGain,C.TaxSell,
        case when C.TrxType = 1 then sum(C.DoneAmount + C.Comm + C.Levy + C.KPEI + C.OTC + C.VAT - C.TaxGain - C.WHT) 
        Else sum(C.DoneAmount - (C.Comm + C.Levy + C.KPEI + C.OTC + C.VAT + C.TaxSell - C.TaxGain - C.WHT)) End TotalAmount    
        from (
        select B.RoundingCommission,B.DecimalCommission,A.TrxType,D.FundCashRefPK,A.InvestmentPK,A.DealingPK,A.HistoryPK,A.DoneAmount,
        B.CommissionPercent ,B.LevyPercent,B.KPEIPercent,B.VATPercent,
        B.WHTPercent,B.OTCPercent,B.IncomeTaxInterestPercent,B.IncomeTaxGainPercent,B.IncomeTaxSellPercent,
        --Comm
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.CommissionPercent/100),B.DecimalCommission),0)) as numeric(19,2)) Comm,
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.LevyPercent/100),B.DecimalLevy),0)) as numeric(19,2)) Levy,
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.KPEIPercent/100),B.DecimalKPEI),0)) as numeric(19,2)) KPEI,
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.VATPercent/100),B.DecimalVAT),0)) as numeric(19,2)) VAT,
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.WHTPercent/100),B.DecimalWHT),0)) as numeric(19,2)) WHT,
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.OTCPercent/100),B.DecimalOTC),0)) as numeric(19,2)) OTC,
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.IncomeTaxInterestPercent/100),B.DecimalTaxInterest),0)) as numeric(19,2)) TaxInterest,
        cast(Sum(isnull(ROUND(A.DoneAmount * (B.IncomeTaxGainPercent/100),B.DecimalTaxGain),0)) as numeric(19,2)) TaxGain,
        case when TrxType = 1 then 0 else cast(Sum(isnull(ROUND(A.DoneAmount * (B.IncomeTaxSellPercent/100),B.DecimalTaxSell),0)) as numeric(19,2)) End TaxSell
        from Investment A
        left join @CounterpartCommissionTemp B on A.CounterpartPK = B.CounterpartPK and A.BoardType = B.BoardType  and B.Status = 2 
        left join FundCashRef D on D.FundPK = A.FundPK and D.Status = 2 and bitdefaultinvestment = 1
        where statusInvestment = 2 and statusDealing = 1 and OrderStatus  in ('O','P') and ValueDate between @DateFrom and @DateTo " + _paramDealingPK + @" and InstrumentTypePK = @InstrumentTypePK and A.BoardType = @BoardType and A.FundPK = @FundPK  and A.TrxType = @TrxType and B.CommissionPercent is not null
        Group By D.FundCashRefPK,A.InvestmentPK,A.DealingPK,A.HistoryPK,A.DoneAmount,A.TrxType,A.DoneAmount,
        B.CommissionPercent,B.LevyPercent,B.KPEIPercent,B.VATPercent,
        B.WHTPercent,B.OTCPercent,B.IncomeTaxInterestPercent,B.IncomeTaxGainPercent,B.IncomeTaxSellPercent,B.RoundingCommission,B.DecimalCommission,B.RoundingLevy,B.DecimalLevy,B.RoundingKPEI,B.DecimalKPEI,B.RoundingVAT,B.DecimalVAT,
        B.RoundingWHT,B.DecimalWHT,B.RoundingOTC,B.DecimalOTC,B.RoundingTaxInterest,B.DecimalTaxInterest,B.RoundingTaxGain,B.DecimalTaxGain,B.RoundingTaxSell,B.DecimalTaxSell
        ) C
        Group By  C.TrxType,C.FundCashRefPK,C.InvestmentPK,C.DealingPK,C.HistoryPK,C.Comm,C.Levy,C.KPEI,C.VAT,C.WHT,C.OTC,C.TaxInterest,C.TaxGain,C.TaxSell,C.DoneAmount,
        C.CommissionPercent,C.LevyPercent,C.KPEIPercent,C.VATPercent,
        C.WHTPercent,C.OTCPercent,C.IncomeTaxInterestPercent,C.IncomeTaxGainPercent,C.IncomeTaxSellPercent

	    Open B
	    Fetch Next From B
	    Into @FundCashRefPK,@InvestmentPK,@DealingPK,@HistoryPK,@CommissionPercent ,@LevyPercent,@KPEIPercent,@VATPercent,@WHTPercent,@OTCPercent,@IncomeTaxInterestPercent,@IncomeTaxGainPercent,@IncomeTaxSellPercent,
	    @Comm,@Levy,@KPEI,@VAT,@WHT,@OTC,@TaxInterest,@TaxGain,@TaxSell,@TotalAmount

	    While @@FETCH_STATUS = 0
	    BEGIN          
			Select @SettlementPK = max(SettlementPK) + 1 From investment
set @SettlementPK = isnull(@SettlementPK,1)
			Update Investment set SettlementPK = @SettlementPK, StatusDealing = 2, StatusSettlement = 1,OrderStatus = 'M',FundCashRefPK = @FundCashRefPK, ApprovedDealingID = @ApprovedDealingID,ApprovedDealingTime = @ApprovedDealingTime,CommissionPercent = @CommissionPercent,LevyPercent=@LevyPercent,KPEIPercent=@KPEIPercent,VATPercent=@VATPercent,WHTPercent=@WHTPercent,OTCPercent=@OTCPercent,IncomeTaxInterestPercent=@IncomeTaxInterestPercent,IncomeTaxGainPercent=@IncomeTaxGainPercent,IncomeTaxSellPercent = @IncomeTaxSellPercent,
			CommissionAmount = @Comm,LevyAmount=@Levy,KPEIAmount = @KPEI, VATAmount=@VAT,WHTAmount = @WHT,OTCAmount=@OTC,IncomeTaxInterestAmount=@TaxInterest,IncomeTaxGainAmount = @TaxGain,IncomeTaxSellAmount = @TaxSell,TotalAmount = @TotalAmount
			where InvestmentPK = @InvestmentPK and DealingPK = @DealingPK and HistoryPK = @HistoryPK

	    Fetch next From B Into @FundCashRefPK,@InvestmentPK,@DealingPK,@HistoryPK,@CommissionPercent ,@LevyPercent,@KPEIPercent,@VATPercent,@WHTPercent,@OTCPercent,@IncomeTaxInterestPercent,@IncomeTaxGainPercent,@IncomeTaxSellPercent,
	    @Comm,@Levy,@KPEI,@VAT,@WHT,@OTC,@TaxInterest,@TaxGain,@TaxSell,@TotalAmount
	    END
	    Close B
	    Deallocate B



    Fetch next From A Into @InstrumentTypePK,@TrxType,@FundPK,@BoardType,@CounterpartPK,@ValueDate
	END
    Close A
    Deallocate A
	
    --Update Investment set SelectedDealing  = 0 
                            ";



                        }
                        else if (_investment.InstrumentTypePK == 5)
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        declare @InvestmentPK int
                        declare @DealingPK int
                        declare @HistoryPK int
                        declare @TotalAmount numeric (22,4)
                        declare @DoneAccruedInterest numeric (22,2)
                        declare @IncomeTaxInterestAmount numeric (22,2)
                        declare @IncomeTaxGainAmount numeric (22,2)

                        declare @FundCashRefPK int 
                        declare @SettlementPK int

                        Declare @TaxPercentageDep numeric(8,4)
                        select @TaxPercentageDep = TaxPercentageTD from FundAccountingSetup where status = 2
                            
                        DECLARE A CURSOR FOR 
	                    Select C.FundCashRefPK,InvestmentPK,DealingPK,HistoryPK,DoneAmount,isnull(DoneAccruedInterest,0),isnull(IncomeTaxInterestAmount,0),isnull(IncomeTaxGainAmount,0)   
	                    from (
	                    select A.TrxType,D.FundCashRefPK,A.InvestmentPK,A.DealingPK,A.HistoryPK,A.DoneAmount,A.DoneAccruedInterest,A.IncomeTaxInterestAmount,A.IncomeTaxGainAmount
	                    from Investment A
	                    left join FundCashRef D on D.FundPK = A.FundPK and D.Status = 2 and bitdefaultinvestment = 1
	                    where statusInvestment = 2 and statusDealing = 1 and ValueDate between @DateFrom and @DateTo " + _paramDealingPK + _paramInstrumentType + _paramTrxType + _paramFund + _paramCounterpart +
                        @"Group By A.TrxType,D.FundCashRefPK,A.InvestmentPK,A.DealingPK,A.HistoryPK,A.DoneAmount,A.DoneAccruedInterest,A.IncomeTaxInterestAmount,A.IncomeTaxGainAmount
	                    ) C
	                    Group By  C.TrxType,C.FundCashRefPK,C.InvestmentPK,C.DealingPK,C.HistoryPK,C.DoneAmount,C.DoneAccruedInterest,C.IncomeTaxInterestAmount,C.IncomeTaxGainAmount
	
                        Open A
                        Fetch Next From A
                        Into @FundCashRefPK,@InvestmentPK,@DealingPK,@HistoryPK,@TotalAmount,@DoneAccruedInterest,@IncomeTaxInterestAmount,@IncomeTaxGainAmount

                        While @@FETCH_STATUS = 0
                        BEGIN              
                        Select @SettlementPK = max(SettlementPK) + 1 From investment
                        set @SettlementPK = isnull(@SettlementPK,1)

                        Update Investment set SettlementPK = @SettlementPK, StatusDealing = 2, StatusSettlement = 1,OrderStatus = 'M',TotalAmount=@TotalAmount + @DoneAccruedInterest - @IncomeTaxInterestAmount - @IncomeTaxGainAmount,FundCashRefPK = @FundCashRefPK, 
                        SettlementMode = Case when TrxType  = 1 then 1 else 2 end,ApprovedDealingID = @ApprovedDealingID,ApprovedDealingTime=@ApprovedDealingTime,EntrySettlementID=@ApprovedDealingID,EntrySettlementTime=@ApprovedDealingTime,LastUpdate=@LastUpdate,
                        TaxExpensePercent = isnull(@TaxPercentageDep,TaxExpensePercent)
                        where InvestmentPK = @InvestmentPK and DealingPK = @DealingPK and HistoryPK = @HistoryPK

                        Fetch next From A Into @FundCashRefPK,@InvestmentPK,@DealingPK,@HistoryPK,@TotalAmount,@DoneAccruedInterest,@IncomeTaxInterestAmount,@IncomeTaxGainAmount
                        END
                        Close A
                        Deallocate A

                        --Update Investment set SelectedDealing  = 0 ";
                        }

                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"
                        declare @InvestmentPK int
                        declare @DealingPK int
                        declare @HistoryPK int
                        declare @TotalAmount numeric (22,4)
                        declare @DoneAccruedInterest numeric (22,2)
                        declare @IncomeTaxInterestAmount numeric (22,2)
                        declare @IncomeTaxGainAmount numeric (22,2)

                        declare @FundCashRefPK int 
                        declare @SettlementPK int
           

                            
                        DECLARE A CURSOR FOR 
	                    Select C.FundCashRefPK,InvestmentPK,DealingPK,HistoryPK,DoneAmount,isnull(DoneAccruedInterest,0),isnull(IncomeTaxInterestAmount,0),isnull(IncomeTaxGainAmount,0)   
	                    from (
	                    select A.TrxType,D.FundCashRefPK,A.InvestmentPK,A.DealingPK,A.HistoryPK,A.DoneAmount,A.DoneAccruedInterest,A.IncomeTaxInterestAmount,A.IncomeTaxGainAmount
	                    from Investment A
	                    left join FundCashRef D on D.FundPK = A.FundPK and D.Status = 2 and bitdefaultinvestment = 1
	                    where statusInvestment = 2 and statusDealing = 1 and isnull(OrderStatus,'O') = 'O' and ValueDate between @DateFrom and @DateTo " + _paramDealingPK + _paramInstrumentType + _paramTrxType +
                        @"Group By A.TrxType,D.FundCashRefPK,A.InvestmentPK,A.DealingPK,A.HistoryPK,A.DoneAmount,A.DoneAccruedInterest,A.IncomeTaxInterestAmount,A.IncomeTaxGainAmount
	                    ) C
	                    Group By  C.TrxType,C.FundCashRefPK,C.InvestmentPK,C.DealingPK,C.HistoryPK,C.DoneAmount,C.DoneAccruedInterest,C.IncomeTaxInterestAmount,C.IncomeTaxGainAmount
	
                        Open A
                        Fetch Next From A
                        Into @FundCashRefPK,@InvestmentPK,@DealingPK,@HistoryPK,@TotalAmount,@DoneAccruedInterest,@IncomeTaxInterestAmount,@IncomeTaxGainAmount

                        While @@FETCH_STATUS = 0
                        BEGIN              
                        Select @SettlementPK = max(SettlementPK) + 1 From investment
                        set @SettlementPK = isnull(@SettlementPK,1)
                        Update Investment set SettlementPK = @SettlementPK, StatusDealing = 2, StatusSettlement = 1,OrderStatus = 'M',FundCashRefPK = @FundCashRefPK, 
                        SettlementMode = Case when TrxType  = 1 then 1 else 2 end,ApprovedDealingID = @ApprovedDealingID,ApprovedDealingTime=@ApprovedDealingTime,EntrySettlementID=@ApprovedDealingID,EntrySettlementTime=@ApprovedDealingTime,LastUpdate=@LastUpdate
                  
                        where InvestmentPK = @InvestmentPK and DealingPK = @DealingPK and HistoryPK = @HistoryPK

                        Fetch next From A Into @FundCashRefPK,@InvestmentPK,@DealingPK,@HistoryPK,@TotalAmount,@DoneAccruedInterest,@IncomeTaxInterestAmount,@IncomeTaxGainAmount
                        END
                        Close A
                        Deallocate A

                        --Update Investment set SelectedDealing  = 0 ";
                        }


                        if (_investment.InstrumentTypePK != 2)
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@ApprovedDealingID", _investment.ApprovedDealingID);
                        cmd.Parameters.AddWithValue("@ApprovedDealingTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["PK"]);
                                }
                            }
                            return 0;
                        }

                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int EndDayTrailsFundPortfolio_GenerateWithParamFund(string _usersID, DateTime _valueDate, EndDayTrailsFundPortfolio _edt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_edt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_edt.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _edt.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
--testing dari sql
--declare @ValueDate date
--declare @UsersID nvarchar(20)
--declare @LastUpdate datetime
--declare @ClientCode nvarchar(20)

--set @ValueDate = '2020-01-30'
--set @UsersID = 'admin'
--set @lastupdate = getdate()
--set @ClientCode = '20'



Declare @CFundPK int
declare @EndDayTrailsFundPortfolioPK int
Create Table #ZFundPosition                  
(                  
InstrumentPK int,     
InstrumentTypePK int,                  
InstrumentID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
FundPK int,                  
FundID nvarchar(100) COLLATE DATABASE_DEFAULT,                  
AvgPrice numeric(38,12),                  
LastVolume numeric(38,4),                  
ClosePrice numeric(38,12),                  
TrxAmount numeric(38,6),              
AcqDate datetime,              
MaturityDate datetime,              
InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
BitBreakable bit
)                  

CREATE CLUSTERED INDEX indx_ZFundPosition ON #ZFundPosition (FundPK,InstrumentPK,InstrumentTypePK,BankPK,BankBranchPK);
    
Create Table #ZLogicFundPosition              
(              
BuyVolume numeric(38,4),              
SellVolume numeric(38,4),              
BuyAmount numeric(38,4),       
SellAmount numeric(38,4),            
FundPK int,              
InstrumentPK int,              
SettlementDate datetime,              
MaturityDate datetime,              
InterestPercent numeric(38,8),
CurrencyPK int,
Category nvarchar(200) COLLATE DATABASE_DEFAULT,
TaxExpensePercent numeric(19, 8),
MarketPK int,
InterestDaysType int,
InterestPaymentType int,
PaymentModeOnMaturity   int,
PaymentInterestSpecificDate datetime,
BankPK int,
BankBranchPK int,
PriceMode int,
BitIsAmortized bit,
AcqDate datetime,
BitBreakable bit
)      

CREATE CLUSTERED INDEX indx_ZLogicFundPosition ON #ZLogicFundPosition (FundPK,InstrumentPK,BankPK,BankBranchPK);

Create Table #ZDividenSaham                  
(                  
InstrumentPK int,     
FundPK int,                  
LastVolume numeric(18,4)     
)      

CREATE CLUSTERED INDEX indx_ZDividenSaham ON #ZDividenSaham (FundPK,InstrumentPK);

Create table #StaticClosePrice
(
	FundPK int,
	InstrumentPK int,
	maxDate datetime
)

CREATE CLUSTERED INDEX indx_StaticClosePrice ON #StaticClosePrice (FundPK,InstrumentPK);

Create Table #ZFundFrom                  
(                   
	FundPK int,
	EndDayTrailsFundPortfolioPK int
)      

CREATE CLUSTERED INDEX indx_ZFundFrom  ON #ZFundFrom (FundPK,EndDayTrailsFundPortfolioPK);


declare @AvgPriceEx table
(
FundPK int,
InstrumentPK int,
Price numeric(18,12)

)

Declare @PeriodPK    int                  
Declare @maxEndDayTrailsFundPortfolioPK int       

                
Select @maxEndDayTrailsFundPortfolioPK = max(ISNULL(EndDayTrailsFundPortfolioPK,0)) + 1 from EndDayTrailsFundPortfolio    
set @maxEndDayTrailsFundPortfolioPK = isnull(@maxEndDayTrailsFundPortfolioPK,1)     


insert into #ZFundFrom(FundPK,EndDayTrailsFundPortfolioPK)
--select FundPK from Fund where status in (1,2)  " + _paramFund + @" and MaturityDate >= @ValueDate
--select FundPK,row_number() over (order by FundPK desc) + @EndDayTrailsFundPortfolioPK from Fund where status in (1,2) and MaturityDate >= @ValueDate
select FundPK,row_number() over (order by FundPK desc) + @maxEndDayTrailsFundPortfolioPK from Fund where status in (1,2) " + _paramFund + @" 
and (MaturityDate >= @ValueDate or MaturityDate = '01/01/1900')
--and FundPK = 9

--PARAM FUND



update FundPosition set status = 3,LastUpdate=@lastUpdate where Date = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)
update EndDayTrailsFundPortfolio set status = 3,VoidUsersID = @UsersID,VoidTime = @lastUpdate,LastUpdate=@lastUpdate
where ValueDate = @ValueDate and status = 2 and FundPK in (select FundPK from #ZFundFrom)          

UPDATE Investment set PriceMode = 1 where InstrumentTypePK  in (1,4,16)  and ValueDate = @ValueDate
update Investment set MarketPK = 1  where ValueDate = @ValueDate
update Investment set Category = null where InstrumentTypePK  <> 5  and ValueDate = @ValueDate
        
Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo  

Insert into EndDayTrailsFundPortfolio  (EndDayTrailsFundPortfolioPK,HistoryPK,Status,ValueDate,FundPK,BitValidate
,LogMessages,EntryUsersID,EntryTime,LastUpdate)                    
Select EndDayTrailsFundPortfolioPK,1,2,@ValueDate,A.FundPK,0
,B.ID + ' - ' + B.Name ,@UsersID,@LastUpdate,@LastUpdate  
from #ZFundFrom A  
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)   
   



insert into @AvgPriceEx
Select A.FundPK,A.InstrumentPK,[dbo].[FgetAvgpriceExercise](@ValueDate,A.FundPK,A.InstrumentPK) from Exercise A
left join FundPosition B on A.InstrumentRightsPK = B.InstrumentPK and A.FundPK = B.FundPK and B.Status = 2 
and B.Date  = 
(select Max(Date) from FundPosition C where C.Date < A.Date and C.FundPK = B.FundPK and C.status = 2)
where DistributionDate  <= @ValueDate and A.status = 2   and A.FundPK in (select FundPK from #ZFundFrom)
group by A.FundPK,A.InstrumentPK


-- TARIK JUAL BELI DARI INVESTMENT              
Insert into #ZLogicFundPosition	(BuyVolume,SellVolume,BuyAmount,SellAmount,FundPK,InstrumentPK,SettlementDate,MaturityDate,
InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate
,BankPK,BankBranchPK,PriceMode,BitIsAmortized,AcqDate,BitBreakable)               

Select SUM(BuyVolume) BuyVolume,SUM(SellVolume) SellVolume,SUM(BuyAmount) BuyAmount,SUM(SellAmount) SellAmount,B.FundPK,B.InstrumentPK,B.SettlementDate,B.MaturityDate,
B.InterestPercent,B.CurrencyPK,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate
,B.BankPK,B.BankBranchPK,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable
From               
(               
Select sum(isnull(A.BuyVolume,0)) BuyVolume, sum(isnull(A.SellVolume,0)) SellVolume,sum(isnull(A.BuyAmount,0)) BuyAmount
,sum(isnull(A.SellAmount,0)) SellAmount,A.FundPK,A.InstrumentPK,              
isnull(A.SettlementDate,'') SettlementDate,isnull(A.MaturityDate,'') MaturityDate,isnull(A.InterestPercent,0) InterestPercent,
isnull(A.CurrencyPK,'') CurrencyPK, A.Category, isnull(A.TaxExpensePercent,0) TaxExpensePercent,isnull(A.MarketPK,0) MarketPK,
isnull(A.InterestDaysType,0) InterestDaysType,isnull(A.InterestPaymentType,0) InterestPaymentType,isnull(A.PaymentModeOnMaturity,0) PaymentModeOnMaturity,isnull(A.PaymentInterestSpecificDate,0) PaymentInterestSpecificDate,isnull(A.BankPK,0) BankPK
,isnull(A.BankBranchPK,0) BankBranchPK,A.PriceMode,A.BitIsAmortized,
A.AcqDate,A.BitBreakable
from (                 
	
select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type = 1 then null else AcqDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable         
from Investment A 
Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2   
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2           
where ValueDate <= @ValueDate and trxType = 1 and StatusSettlement = 2   and A.PeriodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)      
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
  
UNION ALL                  

select A.InstrumentPK,0 BuyVolume,sum(DoneVolume) SellVolume,0 BuyAmount,SUM(DoneAmount) SellAmount, FundPK,               
Case when C.Type = 1 then null else AcqDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,        
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2      
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                   
where ValueDate <= @ValueDate and trxType = 2 and StatusSettlement = 2   and A.PeriodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)            
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent,ValueDate
,A.InstrumentTypePK,B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK
			
UNION ALL

select A.InstrumentPK,sum(DoneVolume) BuyVolume,0 SellVolume,SUM(DoneAmount) BuyAmount,0 SellAmount, FundPK,               
Case when C.Type = 1 then null else SettlementDate end SettlementDate,              
Case when C.Type = 1 then null else A.MaturityDate end MaturityDate,              
Case when C.Type = 1 then null else A.InterestPercent end InterestPercent,ValueDate,
B.CurrencyPK,A.Category,A.TaxExpensePercent,A.MarketPK
,case when B.InstrumentTypePK = 5 then A.InterestDaysType else B.InterestDaysType end InterestDaysType
,case when B.InstrumentTypePK = 5 then A.InterestPaymentType else B.InterestPaymentType end InterestPaymentType
,A.PaymentModeOnMaturity
,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK
,A.PriceMode,A.BitIsAmortized,Case when C.Type = 1 then null else A.AcqDate end AcqDate,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable       
from Investment A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                       
where ValueDate <= @ValueDate and trxType = 3 and StatusSettlement = 2     and A.PeriodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)         
Group By A.InstrumentPK,FundPK,SettlementDate,A.MaturityDate,A.InterestPercent
,ValueDate,A.InstrumentTypePK,B.CurrencyPK,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,C.Type,A.AcqDate,B.InstrumentTypePK,B.InterestDaysType,B.InterestPaymentType,A.BitBreakable,C.InstrumentTypePK

UNION ALL
-- EXERCISE
select A.InstrumentPK,sum(BalanceExercise) BuyVolume,0 SellVolume,SUM(BalanceExercise * Price) BuyAmount,0 SellAmount, FundPK,               
null SettlementDate,              
null MaturityDate,              
null InterestPercent,DistributionDate,
B.CurrencyPK,null,0,1
,0 InterestDaysType
,0 InterestPaymentType
,0
,null
,0,0
,1,0,null AcqDate,0 BitBreakable       
from Exercise A Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2  
left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status = 2                       
where DistributionDate <= @ValueDate  and A.Status = 2 and year(Date) = year(@ValueDate)   and A.FundPK in (select FundPK from #ZFundFrom)          
Group By A.InstrumentPK,FundPK,B.CurrencyPK,DistributionDate



)A                
Group By A.InstrumentPK,A.FundPK,A.SettlementDate,A.MaturityDate,A.InterestPercent
,A.ValueDate,A.CurrencyPK ,A.Category ,A.TaxExpensePercent,A.MarketPK,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate
,A.BankPK,A.BankBranchPK,A.PriceMode,A.BitIsAmortized,A.AcqDate,A.BitBreakable
)B     
Group By B.FundPK,B.InstrumentPK,B.SettlementDate,B.MaturityDate,B.InterestPercent,B.CurrencyPK
,B.Category,B.TaxExpensePercent,B.MarketPK,B.InterestDaysType,B.InterestPaymentType,B.PaymentModeOnMaturity,B.PaymentInterestSpecificDate,B.BankPK,B.BankBranchPK
,B.PriceMode,B.BitIsAmortized,B.AcqDate,B.BitBreakable




--INSERT INVESTMENT + BEG BALANCE SELAIN DEPOSITO ( INVESTMENT + BEG BALANCE )
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType
,PriceMode,BitIsAmortized)                  
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID,A.FundPK,C.ID,
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,             
isnull(A.BuyAmount,0) - (case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end 
* isnull(A.SellVolume,0) / case when D.InstrumentTypePK in (2,3,8,14,13,9,15) then 100 else 1 end )  + isnull(B.TrxAmount,0)	TrxAmount
,A.AcqDate,A.MaturityDate,A.InterestPercent,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK,D.InterestDaysType,D.InterestPaymentType
,A.PriceMode,A.BitIsAmortized
From #ZLogicFundPosition A              
Left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2                
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2  
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2               
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK  
and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')                
and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (1,2,4,5,14,9)  and A.FundPK in (select FundPK from #ZFundFrom)



-- INSERT INVESTMENT + BEG BALANCE DEPOSITO ONLY ( INVESTMENT + BEG BALANCE )              
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
select InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,avg(AvgPrice)
,sum(LastVolume),avg(ClosePrice),TrxAmount,AcqDate,MaturityDate,InterestPercent
,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable
from (
Select  A.InstrumentPK,D.InstrumentTypePK,D.ID InstrumentID,A.FundPK,C.ID FundID, 
1 AvgPrice,              
isnull(A.BuyVolume,0) - isnull(A.SellVolume,0) + isnull(B.Volume,0) LastVolume,                  
1 ClosePrice,                  
isnull(A.BuyAmount,0) - (1 * isnull(A.SellVolume,0))  + isnull		(B.TrxAmount,0) TrxAmount,              
A.AcqDate,A.MaturityDate,A.InterestPercent,D.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.PaymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when D.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From #ZLogicFundPosition A              
LEft join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2     
Left join InstrumentType E on D.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
Left join Fund C on A.FundPK = c.FundPK and C.Status = 2                 
left Join FundEndYearPortfolio B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK 
and isnull(A.Maturitydate,'01/01/1900') = isnull(B.MaturityDate,'01/01/1900')    
and isnull(A.InterestPercent,0) = isnull(B.InterestPercent,0)
and isnull(A.MarketPK,0) = isnull(B.MarketPK,0)
and B.PeriodPK = @PeriodPK where E.Type in (3)  and A.FundPK in (select FundPK from #ZFundFrom)
)A  
group by InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,TrxAmount,AcqDate
,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable




-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT SELAIN DEPOSITO
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,PriceMode,BitIsAmortized
,InterestDaysType,InterestPaymentType,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end AvgPrice,              
isnull(A.Volume,0) LastVolume,                  
dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK) ClosePrice,                  
isnull(A.TrxAmount,0) TrxAmount,              
isnull(A.AcqDate,'01/01/1900'),isnull(A.MaturityDate,'01/01/1900'),isnull(A.InterestPercent,0),C.CurrencyPK, A.Category,isnull(A.TaxExpensePercent,0),A.MarketPK
,A.PriceMode,A.BitIsAmortized,isnull(C.InterestDaysType,2),isnull(C.InterestPaymentType,1),Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2              
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2           
left join Fund D on A.FundPK = D.FundPK and D.status = 2   
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2           
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZFundPosition B on A.InstrumentPK = B.InstrumentPK 
and A.FundPK = B.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')              
where A.PeriodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)            
) and E.Type in (1,2,4,5,14,9) and A.periodPK = @PeriodPK    and A.FundPK in (select FundPK from #ZFundFrom)        

-- AMBIL BEG BALANCE DARI FUND END YEAR, YANG GA PERNAH ADA MUTASI SAMPAI HARI INI DI INVESTMENT DEPOSITO ONLY             
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK,BitBreakable)                  
Select A.InstrumentPK,C.InstrumentTypePK,C.ID,A.FundPK,D.ID, 1 AvgPrice,isnull(A.Volume,0) LastVolume,                  
1 ClosePrice, isnull(A.TrxAmount,0) TrxAmount,              
A.AcqDate,A.MaturityDate,A.InterestPercent,C.CurrencyPK, A.Category,A.TaxExpensePercent,A.MarketPK
,A.InterestDaysType,A.InterestPaymentType,A.paymentModeOnMaturity,A.PaymentInterestSpecificDate,A.BankPK,A.BankBranchPK,Case when C.InstrumentTypePK = 5 then A.BitBreakable else 0 end BitBreakable
From FundEndYearPortfolio A              
left join Instrument C on A.InstrumentPk = C.instrumentPK and C.status = 2     
Left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2             
left join Fund D on A.FundPK = D.FundPK and D.status = 2              
where FundEndYearPortfolioPK not in              
(              
Select FundEndYearPortfolioPK From FundEndYearPortfolio A              
inner join #ZLogicFundPosition B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK  and A.MaturityDate = B.MaturityDate         
where A.PeriodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)           
) and E.Type in (3) and A.periodPK = @PeriodPK   and A.FundPK in (select FundPK from #ZFundFrom)         



      
-- CORPORATE ACTION DIVIDEN SAHAM



-- CORPORATE ACTION STOCK SPLIT / REVERSE STOCK SPLIT
	
DELETE CorporateActionResult where Date = @ValueDate  and FundPK in (select FundPK from #ZFundFrom)

-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.ExDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.ExDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)
and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ExDate,B.FundPK,A.InstrumentPK,isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 4 and A.Status = 2 and A.ExDate = @ValueDate   and B.FundPK in (select FundPK from #ZFundFrom)



-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)
and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.ValueDate,B.FundPK,A.InstrumentPK,B.LastVolume / A.Hold * A.Earn DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)


-- CORPORATE ACTION DIVIDEN RIGHTS
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)
and C.FundPK is null and C.InstrumentPK is null

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-R' and D.status = 2
where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)


-- CORPORATE ACTION DIVIDEN WARRANT
truncate table #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)

Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
, SettlementDate, ValueDate 
from Investment where statusSettlement = 2
and InstrumentTypePK = 1  and FundPK in (select FundPK from #ZFundFrom)
Group by InstrumentPK,FundPK,SettlementDate,ValueDate
)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)
and C.FundPK is null and C.InstrumentPK is null

Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A 
left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
left join Instrument D on D.ID = C.ID + '-W' and D.status = 2
where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)


-- PROSES EXERCISE YANG DAH DI DISTRIBUTION DATE
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select ExercisePK,2,@ValueDate,FundPK,InstrumentPK,BalanceExercise,Price,0 from Exercise 
where DistributionDate  = @ValueDate and status = 2  and FundPK in (select FundPK from #ZFundFrom)


-- CORPORATE ACTION BOND AMORTIZEN
TRUNCATE TABLE #ZDividenSaham
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance  LastBalance
--B.Balance + C.BalanceFromInv LastBalance
From CorporateAction A 
left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK  in (2,3,9,15)
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)

--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
--from CorporateAction A
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
--and B.ValueDate >= A.ValueDate
--left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
--where A.Type = 2 and A.Status = 2 and A.PaymentDate = @ValueDate
--and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price,PeriodPK)
Select A.CorporateActionPK,2,A.PaymentDate,B.FundPK,A.InstrumentPK,B.LastVolume * A.Earn / A.Hold * -1,0,dbo.FgetPeriod(A.ValueDate)
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate  and B.FundPK in (select FundPK from #ZFundFrom)




-- UPDATE POSISI ZFUNDPOSITION + CORPORATE ACTION	
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
select FundPK,A.InstrumentPK,Price, sum(Balance) Balance,A.status
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
and B.ID not like '%-W' and B.ID not like '%-R'
WHERE A.Date <= @ValueDate and PeriodPK = @PeriodPK  and A.FundPK in (select FundPK from #ZFundFrom)

Group By FundPK,A.InstrumentPK,Price,A.status
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
left join instrumentType C on A.InstrumentTypePK = C.InstrumentTypePK and C.status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
where C.Type in (1,9,2,5,14)  and A.FundPK in (select FundPK from #ZFundFrom)
--AND A.LastVolume > 0


                
---- EXERCISE BELUM ADA DI FUNDPOSITION, UNTUK RIGHTS
--IF NOT EXISTS
--(
--Select * from #ZFundPosition A 
--left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
--where Date >= @ValueDate and DistributionDate > @valuedate and status = 2
--)
--BEGIN
--Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice,
--TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK,InterestDaysType,InterestPaymentType,PriceMode,BitIsAmortized)
--Select A.InstrumentRightsPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,A.Price,BalanceExercise,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
--A.Price * BalanceExercise,null,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK,0,0,1,0 from Exercise A
--left join Instrument B on A.InstrumentRightsPK = B.InstrumentPK and B.status in (1,2)
--left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
--where Date >= @ValueDate and DistributionDate > @valuedate and A.status = 2
--END





--INSERT INSTRUMENT YANG ADA DI CORPORATE ACTION RESULT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,16,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2

where A.status = 2 and B.ID like '%-W' and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 

Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  
Select A.InstrumentPK,4,B.ID,A.FundPK,C.ID, 
[dbo].[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),
A.Balance,dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
Balance*Price,Date,'01/01/1900',0,B.CurrencyPK,NULL,0,B.MarketPK
from CorporateActionResult A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
 
where A.status = 2 and B.ID like '%-R' and PeriodPK = @PeriodPK and A.FundPK in (select FundPK from #ZFundFrom) 


                       
-- UPDATE POSISI ZFUNDPOSITION + FUND POSITION ADJUSTMENT
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.AvgPrice = case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
A.TrxAmount = A.TrxAmount + isnull(B.Price * B.Balance,0)
from #ZFundPosition A
left join 
(
SELECT FundPK,A.InstrumentPK,case when sum(balance) = 0 then 0 else sum(Price*Balance) / SUM(balance) end Price, sum(Balance) Balance,A.status,
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  THEN A.AcqDate ELSE NULL END AcqDate
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
WHERE A.status = 2 AND A.Date <= @ValueDate and PeriodPK = @PeriodPK  and A.FundPK in (select FundPK from #ZFundFrom)
Group By FundPK,A.InstrumentPK,A.status,B.InstrumentTypePK,A.AcqDate
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2
AND isnull(A.AcqDate,'01/01/1900') = isnull(B.AcqDate,'01/01/1900')
left join Fund C on A.FundPK = C.FundPK and C.status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
where  A.FundPK in (select FundPK from #ZFundFrom)


--INSERT INSTRUMENT YANG ADA DI FUND POSITION ADJUSTMENT TAPI GA ADA IN ZFUNDPOSITION
Insert into #ZFundPosition(InstrumentPK,InstrumentTypePK,InstrumentID,FundPK,FundID,AvgPrice,LastVolume,ClosePrice
,TrxAmount,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK)                  

SELECT A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID, 
case when F.AveragePriority = 3 then isnull(dbo.[FgetlastavgfrominvestmentByInvestmentPK] (@ValueDate,A.InstrumentPK,A.FundPK),0) else isnull(dbo.[FGetLastAvgFromInvestment] (@ValueDate,A.InstrumentPK,A.FundPK),0) end,
SUM(A.Balance),dbo.FGetLastClosePriceForFundPosition(@ValueDate,A.InstrumentPK),
SUM(Balance*Price),
case when B.InstrumentTypePK in (2,3,8,14,13,9,15)  then  A.AcqDate else null End ,
B.MaturityDate,B.InterestPercent,B.CurrencyPK,NULL,B.TaxExpensePercent,B.MarketPK
from dbo.FundPositionAdjustment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Fund C on A.FundPK = C.FundPK and C.status = 2
left join FundAccountingSetup F on A.FundPK = F.FundPK and F.Status = 2
where A.status = 2 AND A.AcqDate <= @ValueDate and PeriodPK = @PeriodPK  and A.FundPK in (select FundPK from #ZFundFrom)
and NOT EXISTS 
(SELECT * FROM #ZFundPosition C WHERE A.InstrumentPK = C.InstrumentPK AND A.FundPK = C.FundPK and isnull(A.AcqDate,'01/01/1900') = isnull(C.AcqDate,'01/01/1900'))
GROUP BY A.InstrumentPK,B.InstrumentTypePK,B.ID,A.FundPK,C.ID,
B.MaturityDate,B.InterestPercent,B.CurrencyPK,B.TaxExpensePercent,B.MarketPK,A.AcqDate,F.AveragePriority



-- DELETE RIGHTS AND WARRANT YANG EXPIRED
Delete A From #ZFundPosition A
Inner join 
(
Select C.InstrumentPK from CorporateAction A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Instrument C on C.ID = B.ID + '-R' and C.status = 2
where ExpiredDate = @ValueDate and A.Status = 2 and A.Type = 3
)B on A.InstrumentPK = B.InstrumentPK

Delete A From #ZFundPosition A
Inner join 
(
Select C.InstrumentPK from CorporateAction A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
left join Instrument C on C.ID = B.ID + '-W' and C.status = 2
where ExpiredDate = @ValueDate and A.Status = 2 and A.Type = 5
)B on A.InstrumentPK = B.InstrumentPK






-- UPDATE AVGPRICE UNTUK EXERCISE 
IF  EXISTS
(
Select * from #ZFundPosition A 
left join Exercise B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where DistributionDate <= @valuedate and status = 2  and A.FundPK in (select FundPK from #ZFundFrom)
)
BEGIN
	Update A set A.AvgPrice = isnull(B.Price,A.AvgPrice),TrxAmount = isnull(B.Price,A.AvgPrice) * LastVolume from #ZFundPosition A
	left join @AvgPriceEx B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
	where  A.FundPK in (select FundPK from #ZFundFrom)
END


-- UPDATE POSISI ZFUNDPOSITION EQUITY + (RIGHT / WARRANT) YG SUDAH DI EXERCISE -- ni ga berkurang
update A set 
A.LastVolume = A.LastVolume + isnull(B.Balance,0),
A.TrxAmount = A.AvgPrice * (A.TrxAmount + isnull(B.Balance,0))
from #ZFundPosition A
left join 
(
SELECT FundPK,A.InstrumentRightsPK, sum(BalanceExercise) * -1 Balance
from dbo.Exercise A
WHERE A.status = 2 AND A.DistributionDate <= @valuedate  and A.FundPK in (select FundPK from #ZFundFrom)
Group By FundPK,A.InstrumentRightsPK
) B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentRightsPK
where B.Balance is not null




--select * from #ZLogicFundPosition where FundPK = 7 and InstrumentPK in (3761,3762)
--select * from #ZFundPosition where FundPK = 7 and InstrumentPK in (3761,3762)


Insert into FundPosition(FundPositionPK,TrailsPK,HistoryPK,Status,Notes,Date,FundPK,FundID,                  
InstrumentPK,InstrumentID,AvgPrice,Balance,CostValue,ClosePrice,TrxAmount,MarketValue
,AcqDate,MaturityDate,InterestPercent,CurrencyPK,Category,TaxExpensePercent,MarketPK
,InterestDaysType,InterestPaymentType,PaymentModeOnMaturity,PaymentInterestSpecificDate,BankPK,BankBranchPK
,EntryUsersID,EntryTime,LastUpdate,PriceMode,BitIsAmortized,BitBreakable)                  
Select C.EndDayTrailsFundPortfolioPK,C.EndDayTrailsFundPortfolioPK,1,2,'',@ValueDate,A.FundPK, FundID,                  
A.InstrumentPK,InstrumentID,CONVERT(decimal(18,12),AvgPrice),LastVolume
,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then CONVERT(decimal(18,12),AvgPrice)/100 else CONVERT(decimal(18,12),AvgPrice) End * LastVolume CostValue
, ClosePrice,TrxAmount
,case when InstrumentTypePK in (2,3,8,14,13,9,15)  then ClosePrice/100 else ClosePrice End * LastVolume MarketValue,                  
AcqDate,A.MaturityDate,InterestPercent,A.CurrencyPK, Category,TaxExpensePercent,A.MarketPK
,isnull(InterestDaysType,0),isnull(InterestPaymentType,0),isnull(PaymentModeOnMaturity,0),PaymentInterestSpecificDate,isnull(A.BankPK,0),isnull(A.BankBranchPK,0)
,@UsersID,@LastUpdate,@LastUpdate,isnull(PriceMode,0),isnull(BitIsAmortized,0),isnull(BitBreakable,0)
From #ZFundPosition  A WITH (NOLOCK)
left join Fund B on A.FundPK = B.FundPK
inner join #ZFundFrom C on A.FundPK = C.FundPK
where A.LastVolume > 0 and B.status in (1,2)  and A.FundPK in (select FundPK from #ZFundFrom) 
  

Delete FP From FundPosition FP Left Join Instrument I on FP.InstrumentPK = I.InstrumentPK
left join #ZFundFrom B on FP.FundPK = B.FundPK 
Where FundPositionPK = B.EndDayTrailsFundPortfolioPK and I.InstrumentTypePK not in (1,4,6,16)
and FP.MaturityDate <= @ValueDate and FP.MaturityDate Is Not Null  and FP.FundPK in (select FundPK from #ZFundFrom)  


---------PROSES AMORTIZED DAN PRICE MODE------------------------------
update A set A.ClosePrice =  Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
when A.PriceMode = 2 then LowPriceValue
when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
else  
dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			 
end 
, A.MarketValue = A.Balance * Case when A.BitIsAmortized = 0 
then Case when A.PriceMode = 1 then ClosePriceValue 
when A.PriceMode = 2 then LowPriceValue
when A.PriceMode = 3 then HighPriceValue else isnull(ClosePriceValue,1) end
else  
dbo.FgetAmortize(@ValueDate,A.AcqDate,A.MaturityDate,A.AvgPrice)
			  
end / Case when D.InstrumentTypePK in (2,3,8,14,13,9,15)  then 100 else 1 end
from FundPosition A 
left join 
(
select InstrumentPK,LowPriceValue,ClosePriceValue,HighPriceValue From ClosePrice where Date =
(
Select max(Date) From ClosePrice where date <= @ValueDate and status = 2
) and status = 2
)B on A.InstrumentPK = B.InstrumentPK 
left join instrument C on A.InstrumentPK = C.instrumentPK and C.Status = 2
left join InstrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
where A.TrailsPK = @maxEndDayTrailsFundPortfolioPK  and A.FundPK in (select FundPK from #ZFundFrom)

-- STATIC CLOSEPRICE


Declare @StaticClosePrice table
(
InstrumentPK int,
InstrumentTypePK int,
ClosePrice numeric(18,8),
FundPK int
)

Declare @FFundPK int

Declare A Cursor For
Select FundPK from Fund where status = 2  and FundPK in (select FundPK from #ZFundFrom)
Open A
Fetch next From A
Into @FFundPK
WHILE @@FETCH_STATUS = 0  
BEGIN
			

Declare @CInstrumentPK int

Declare B cursor For
Select distinct InstrumentPK from updateclosePrice where status = 2
Open B
Fetch Next From B
Into @CInstrumentPK
While @@Fetch_Status = 0
BEGIN
IF EXISTS(select * from UpdateClosePrice where status = 2 and InstrumentPK = @CInstrumentPK and FundPK = @FFundPK and Date = @ValueDate)
BEGIN

insert into @StaticClosePrice
Select A.InstrumentPK,InstrumentTypePK,A.ClosePriceValue,@FFundPK from UpdateClosePrice A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
where A.status = 2 and A.InstrumentPK = @CInstrumentPK 
and Date = (
Select Max(Date) From UpdateClosePrice where status = 2 and InstrumentPK = @CInstrumentPK
and Date <= @ValueDate and FundPK = @FFundPK
)  and FundPK = @FFundPK

END

FETCH NEXT FROM B INTO @CInstrumentPK  
END
Close B
Deallocate B


		
Update A set ClosePrice = B.ClosePrice, MarketValue = A.Balance * case when B.InstrumentTypePK not in (1,4,6,16) then B.ClosePrice/100 else B.ClosePrice end from FundPosition A
left join @StaticClosePrice B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
where A.Date = @ValueDate and A.TrailsPK = @maxEndDayTrailsFundPortfolioPK
and A.InstrumentPK in(
select instrumentPK From @StaticClosePrice where FundPK = @FFundPK
) and A.FundPK = @FFundPK and A.status = 1

	
FETCH NEXT FROM A 
INTO @FFundPK
END 

CLOSE A;  
DEALLOCATE A;





-- update TrxBuy di Investment untuk Sell / Rollover

declare @DTrxBuy int
declare @DInvestmentPK int
declare @DInstrumentPK int
declare @DFundPK int
declare @DDate datetime
declare @DNewIdentity bigint
declare @DAcqDate datetime

DECLARE C CURSOR FOR 
select TrxBuy,InvestmentPK,B.InstrumentPK,B.FundPK,B.Date,B.AcqDate from Investment A
left join FundPosition B on A.TrxBuy = B.[Identity]
where B.Date = @valuedate and InstrumentTypePK in (5,10) and StatusSettlement in (1,2) and TrxType in (2,3)  and A.FundPK in (select FundPK from #ZFundFrom)
Open C
Fetch Next From C
Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate  
While @@FETCH_STATUS = 0
BEGIN   

set @DNewIdentity = 0
select @DNewIdentity = [Identity] from FundPosition where InstrumentPK = @DInstrumentPK and FundPK = @DFundPK and Date = @DDate and AcqDate = @DAcqDate and status in (1,2)

update Investment set TrxBuy = @DNewIdentity where InvestmentPK = @DInvestmentPK and StatusSettlement in (1,2)


Fetch next From C Into @DTrxBuy,@DInvestmentPK,@DInstrumentPK,@DFundPK,@DDate,@DAcqDate                  
END
Close C
Deallocate C    


	
Update EndDayTrailsFundPortfolio set BitValidate = 1 where EndDayTrailsFundPortfolioPK = @maxEndDayTrailsFundPortfolioPK and Status = 2        

Select @maxEndDayTrailsFundPortfolioPK LastPK
            
            
            
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["LastPK"]);

                            }
                            return 0;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {
            #region Performance Report
            if (_unitRegistryRpt.ReportName.Equals("Performance Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";
                            string _paramFundClient = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }


                            cmd.CommandText = @"


--declare @datefrom date
--declare @dateto date

--set @datefrom = '2019-01-28'
--set @dateto = '2020-10-14'

declare @tableSelect table (
	year nvarchar(100),
	FundPK int,
	FundClientPK int,
	Type nvarchar(100),
	TradeDate date,
	NetAmount numeric(22,2),
	NAVSubsRed numeric(22,4),
	NetUnit numeric(22,4),
	EndingUnit numeric(22,4),
	CurrentNAV numeric(22,4),
	orderby int
)

declare @tableYear table (
	NoYear int,
	year int
)
declare @Firstyear int

insert into @tableYear(NoYear,year)
select ROW_NUMBER() over (order by year), year from (
select distinct year(valuedate) year from ClientSubscription where ValueDate <= @dateto and status = 2
union all 
select distinct year(valuedate) from ClientRedemption where ValueDate <= @dateto and status = 2
)A
group by year

set @Firstyear = (select top 1 year from @tableYear order by year)

update @tableYear set NoYear = 2 where year = @Firstyear


insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'SUBSCRIPTION',A.ValueDate,A.CashAmount,A.NAV,A.UnitAmount,A.CashAmount,A.nav,1 from ClientSubscription A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
inner join Fund C on A.FundPK = C.FundPK and C.Status = 2 
where A.ValueDate <= @dateto and A.status = 2 and B.FundPK is null and A.CashAmount > 1 and C.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'SUBSCRIPTION',A.ValueDate,A.CashAmount,A.NAV,A.UnitAmount,A.CashAmount,A.nav,1 from ClientSubscription A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
inner join Fund C on A.FundPK = C.FundPK and C.Status = 2 
where A.ValueDate <= @dateto and A.status = 2 and A.ValueDate <= B.ValueDate and A.CashAmount > 1 and C.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'TOTAL',A.ValueDate,0,0,0,0,0,2 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 
inner join Fund C on A.FundPK = C.FundPK and C.Status = 2
where A.ValueDate <= @dateto and A.status = 2 and A.CashAmount > 1 and A.ValueDate = B.ValueDate and C.Type = 8
order by fundpk, fundclientpk, A.ValueDate

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'RED (PAY OUT)',A.ValueDate,A.CashAmount * -1,A.NAV,A.UnitAmount * -1,0,A.nav,3 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 
inner join Fund C on A.FundPK = C.FundPK and C.Status = 2
where A.ValueDate <= @dateto and A.status = 2 and A.CashAmount > 1 and A.ValueDate = B.ValueDate and C.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select C.NoYear,A.FundPK,A.FundClientPK,'NEW BALANCED',A.ValueDate,0,A.NAV,0,0,A.nav,4 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join (
	select * from @tableYear
)C on year(A.ValueDate) = C.year
inner join Fund D on A.FundPK = D.FundPK and D.Status = 2 and D.Type = 8
where A.ValueDate <= @dateto and A.status = 2 and A.CashAmount > 1 and A.ValueDate = B.ValueDate

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select C.NoYear,A.FundPK,A.FundClientPK,'SUBSCRIPTION',A.ValueDate,A.CashAmount,A.NAV,A.UnitAmount,0,A.nav,5 from ClientSubscription A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join (
	select * from @tableYear
)C on year(A.ValueDate) = C.year
inner join Fund D on A.FundPK = D.FundPK and D.Status = 2 
where A.ValueDate <= @dateto and A.status = 2 and A.ValueDate > B.ValueDate and A.CashAmount > 1 and D.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select C.NoYear,A.FundPK,A.FundClientPK,'RED (PAY OUT)',A.ValueDate,A.CashAmount * -1,A.NAV,A.UnitAmount * -1,0,A.nav,5 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join (
	select * from @tableYear
)C on year(A.ValueDate) = C.year
inner join Fund D on A.FundPK = D.FundPK and D.Status = 2 
where A.ValueDate <= @dateto and A.status = 2 and A.ValueDate > B.ValueDate and A.CashAmount > 1 and D.Type = 8

declare @RunningAmount numeric(26,4)
declare @FundPK int
declare @FundClientPK int

Declare A Cursor For
	select A.FundPK,A.FundClientPK from @tableSelect A
	left join (
	select FundPK,FundCLientPK from @tableSelect where orderby = 5
	) B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
	where B.FundPK is null and orderby = 4
Open A
Fetch Next From A
Into @FundPK,@FundClientPK

While @@FETCH_STATUS = 0  
BEGIN	
	delete @tableSelect where fundpk = @FundPK and FundClientPK = @FundClientPK and orderby = 4

	Fetch Next From A 
	into @FundPK,@FundClientPK
End	
Close A
Deallocate A


Declare A Cursor For
	Select Distinct  FundPK,FundClientPK from @tableSelect 
Open A
Fetch Next From A
Into @FundPK,@FundClientPK

While @@FETCH_STATUS = 0  
BEGIN	
	set @RunningAmount = 0;


	WITH q AS (
		select top 1000000000 * from @tableSelect
		where  FundPK = @FundPK and FundClientPK = @FundClientPK
		order by year,fundpk,fundclientpk,orderby,TradeDate
	) 
	UPDATE q 
	SET    @RunningAmount = EndingUnit = @RunningAmount + NetUnit
	
	 

	Fetch Next From A 
	into @FundPK,@FundClientPK
End	
Close A
Deallocate A

update A set A.NetAmount = isnull(B.NetAmount,0), A.NetUnit = isnull(B.NetUnit,0)
from @tableSelect A
left join (
	select FundPK,FundClientPK,sum(NetAmount) NetAmount, sum(NetUnit) NetUnit from @tableSelect where orderby = 1
	group by FundPK,FundClientPK
) B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where orderby = 2

update A set A.NetAmount = isnull(B.NetAmount,0),  A.NetUnit = isnull(B.NetUnit,0)
from @tableSelect A
left join (
	select FundPK,FundClientPK,sum(NetAmount) NetAmount, sum(NetUnit) NetUnit from @tableSelect where orderby in (2,3)
	group by FundPK,FundClientPK
) B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where orderby = 4


select year,C.Name ClientName, B.Name FundName,C.ID CIF,A.Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,isnull(D.NAV,0) CurrentNAV from @tableSelect A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2
left join CloseNAV D on A.FundPK = D.FundPK and D.status = 2 and D.Date = @dateto 
where 1 = 1 " + _paramFundFrom + _paramFundClient + @"
order by A.fundpk,A.fundclientpk,orderby,TradeDate,year

";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "PerformanceReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PerformanceReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "UnitRegistrReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Performance Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PerformanceReport> rList = new List<PerformanceReport>();
                                        while (dr0.Read())
                                        {

                                            PerformanceReport rSingle = new PerformanceReport();
                                            rSingle.FundName = dr0["FundName"].ToString();
                                            rSingle.ClientName = dr0["ClientName"].ToString();
                                            rSingle.CIF = dr0["CIF"].ToString();
                                            rSingle.Year = dr0["Year"].ToString();
                                            rSingle.Type = dr0["Type"].ToString();
                                            rSingle.TradeDate = Convert.ToDateTime(dr0["TradeDate"]);
                                            rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rSingle.NAVSubsRed = Convert.ToDecimal(dr0["NAVSubsRed"]);
                                            rSingle.NetUnit = Convert.ToDecimal(dr0["NetUnit"]);
                                            rSingle.EndingUnit = Convert.ToDecimal(dr0["EndingUnit"]);
                                            rSingle.CurrentNAV = Convert.ToDecimal(dr0["CurrentNAV"]);
                                            //rSingle.CurrentAmount = Convert.ToDecimal(dr0["CurrentAmount"]);
                                            //rSingle.RL = Convert.ToDecimal(dr0["RL"]);
                                            //rSingle.RLPercent = Convert.ToDecimal(dr0["RLPercent"]);
                                            rList.Add(rSingle);
                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { r.FundName, r.ClientName, r.CIF, r.Year } into rGroup
                                                select rGroup;

                                        int incRowExcel = 2;

                                        string _clientName = "";

                                        foreach (var rsHeader in GroupByReference)
                                        {


                                            if (_clientName != rsHeader.Key.ClientName)
                                            {
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 3].Value = ": " + rsHeader.Key.FundName;

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 1].Value = "Client Name";
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 3].Value = ": " + rsHeader.Key.ClientName;

                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 6].Value = "Performance Report - " + rsHeader.Key.ClientName;

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 1].Value = "As of date";
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 3].Value = ": " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 1].Value = "CIF";
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 14;
                                                worksheet.Cells[incRowExcel, 3].Value = ": " + rsHeader.Key.CIF;
                                                incRowExcel++;
                                            }


                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "Year";
                                            worksheet.Cells[incRowExcel, 2].Value = "Type";
                                            worksheet.Cells[incRowExcel, 3].Value = "Trade Date";
                                            worksheet.Cells[incRowExcel, 4].Value = "Net Amount (IDR)";
                                            worksheet.Cells[incRowExcel, 5].Value = "NAV Subs / Red";
                                            worksheet.Cells[incRowExcel, 6].Value = "Net Unit";
                                            worksheet.Cells[incRowExcel, 7].Value = "Ending                 UNIT";
                                            worksheet.Cells[incRowExcel, 8].Value = "Current NAV";
                                            worksheet.Cells[incRowExcel, 9].Value = "Current                      Amount";
                                            worksheet.Cells[incRowExcel, 10].Value = "R/L";
                                            worksheet.Cells[incRowExcel, 11].Value = "% R/L";
                                            worksheet.Row(incRowExcel).Height = 36;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.WrapText = true;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel + 1;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                if (rsDetail.Type == "NEW BALANCED")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.Year;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.TradeDate;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVSubsRed;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.EndingUnit;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";


                                                }

                                                else if (rsDetail.Type == "SUBSCRIPTION")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.Year;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.TradeDate;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVSubsRed;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.EndingUnit;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                }

                                                else if (rsDetail.Type == "TOTAL")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.Year;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                                }

                                                else if (rsDetail.Type == "RED (PAY OUT)")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.Year;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.TradeDate;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                                    if (Convert.ToDecimal(rsDetail.NetAmount) < 0)
                                                    {
                                                        worksheet.Cells[incRowExcel, 4].Style.Font.Color.SetColor(Color.Red);
                                                    }

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVSubsRed;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                                    if (Convert.ToDecimal(rsDetail.NAVSubsRed) < 0)
                                                    {
                                                        worksheet.Cells[incRowExcel, 5].Style.Font.Color.SetColor(Color.Red);
                                                    }

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                    if (Convert.ToDecimal(rsDetail.NetUnit) < 0)
                                                    {
                                                        worksheet.Cells[incRowExcel, 6].Style.Font.Color.SetColor(Color.Red);
                                                    }


                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.EndingUnit;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.CurrentNAV;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                    int rowM = incRowExcel - 1;
                                                    worksheet.Cells[incRowExcel, 9].Formula = "=F" + rowM + "*H" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 10].Formula = "=I" + incRowExcel + "-D" + rowM;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 11].Formula = "=J" + incRowExcel + "/D" + rowM;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#0\\.00%";
                                                }

                                                no++;
                                                _endRowDetail = incRowExcel + 1;


                                            }

                                            incRowExcel++;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Merge = true;
                                            worksheet.Cells[_startRowDetail, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            _clientName = rsHeader.Key.ClientName;
                                            //worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;



                                        }
                                        incRowExcel = incRowExcel + 3;
                                        worksheet.Cells[incRowExcel, 10].Value = "Date : " + Convert.ToDateTime(DateTime.Now).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 10, incRowExcel, 11].Merge = true;
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Disclaimer";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "PT Jarvis Aset Manajemen is registered and regulated by the Otoritas Jasa Keuangan (OJK) - the Indonesian Financial Service Authority. All of its product-offering shall be sold by registered and professional selling agents and supervised by OJK.";
                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 11].Merge = true;
                                        worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                        worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Row(incRowExcel).Height = 36;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Every Investment Instruments bears some risks and previous performance is not indicative of future performance.";
                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 11].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "This report publish by PT Jarvis Aset Manajemen. If there is a difference, then the reference is the report from the custodian bank.";
                                        worksheet.Cells[incRowExcel, 2, incRowExcel, 11].Merge = true;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 11];
                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 22;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 21;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 22;
                                        worksheet.Column(9).Width = 23;
                                        worksheet.Column(10).Width = 19;
                                        worksheet.Column(11).Width = 10;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Report Equity Summary";


                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        //worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }

                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            #region Draft Performance Report
            else if (_unitRegistryRpt.ReportName.Equals("Draft Performance Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";
                            //string _paramFundClient = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }


                            cmd.CommandText = @"
                             


declare @tableSelect table (
	year nvarchar(100),
	FundPK int,
	FundClientPK int,
	Type nvarchar(100),
	TradeDate date,
	NetAmount numeric(22,2),
	NAVSubsRed numeric(22,4),
	NetUnit numeric(22,4),
	EndingUnit numeric(22,4),
	CurrentNAV numeric(22,4),
	orderby int
)

declare @tableYear table (
	NoYear int,
	year int
)
declare @Firstyear int

insert into @tableYear(NoYear,year)
select ROW_NUMBER() over (order by year), year from (
select distinct year(valuedate) year from ClientSubscription where ValueDate <= @dateto and status = 2
union all 
select distinct year(valuedate) from ClientRedemption where ValueDate <= @dateto and status = 2
)A
group by year

set @Firstyear = (select top 1 year from @tableYear order by year)

update @tableYear set NoYear = 2 where year = @Firstyear

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'SUB',A.ValueDate,A.CashAmount,A.NAV,A.UnitAmount,A.CashAmount,A.nav,1 from ClientSubscription A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join Fund C on A.FundPK = C.FundPK and C.Status = 2 
where A.ValueDate <= @dateto and A.status = 2 and A.CashAmount > 1 and B.FundPK is null and C.Type = 8

--redemption awal
insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'SUB',A.ValueDate,A.CashAmount,A.NAV,A.UnitAmount,A.CashAmount,A.nav,1 from ClientSubscription A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
inner join Fund C on A.FundPK = C.FundPK and C.Status = 2 and C.Type = 8
where A.ValueDate <= @dateto and A.status = 2 and A.ValueDate <= B.ValueDate and A.CashAmount > 1 and C.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'TOTAL',A.ValueDate,0,0,0,0,0,2 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 
inner join Fund C on A.FundPK = C.FundPK and C.Status = 2 and C.Type = 8
where A.ValueDate <= @dateto and A.status = 2 and A.CashAmount > 1 and A.ValueDate = B.ValueDate and C.Type = 8
order by fundpk, fundclientpk, A.ValueDate

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select 1,A.FundPK,A.FundClientPK,'RED',A.ValueDate,A.CashAmount * -1,A.NAV,A.UnitAmount * -1,0,A.nav,3 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 
inner join Fund C on A.FundPK = C.FundPK and C.Status = 2 and C.Type = 8
where A.ValueDate <= @dateto and A.status = 2 and A.CashAmount > 1 and A.ValueDate = B.ValueDate and C.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select C.NoYear,A.FundPK,A.FundClientPK,'BEGINNING BALANCED',A.ValueDate,0,A.NAV,0,0,A.nav,4 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
 ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join (
	select * from @tableYear
)C on year(A.ValueDate) = C.year
inner join Fund D on A.FundPK = D.FundPK and D.Status = 2 and D.Type = 8
where A.ValueDate <= @dateto and A.status = 2 and A.CashAmount > 1 and A.ValueDate = B.ValueDate and D.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select C.NoYear,A.FundPK,A.FundClientPK,'SUB',A.ValueDate,A.CashAmount,A.NAV,A.UnitAmount,0,A.nav,5 from ClientSubscription A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join (
	select * from @tableYear
)C on year(A.ValueDate) = C.year
inner join Fund D on A.FundPK = D.FundPK and D.Status = 2 and D.Type = 8
where A.ValueDate <= @dateto and A.status = 2 and A.ValueDate > B.ValueDate and A.CashAmount > 1 and D.Type = 8

insert into @tableSelect(year,fundpk,FundClientPK,Type,TradeDate,NetAmount,NAVSubsRed,NetUnit,EndingUnit,CurrentNAV,orderby)
select C.NoYear,A.FundPK,A.FundClientPK,'RED',A.ValueDate,A.CashAmount * -1,A.NAV,A.UnitAmount * -1,0,A.nav,5 from ClientRedemption A
left join (
	select E.*
	from
	(select distinct fundpk,fundclientpk from ClientRedemption where ValueDate <= @dateto and status = 2 and type <> 3 ) D
	cross apply (
	  select top(1) * from ClientRedemption
	  where FundPK=D.FundPK and FundClientPK = D.FundClientPK and status = 2 and type <> 3
	  order by ValueDate
	 ) E
) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join (
	select * from @tableYear
)C on year(A.ValueDate) = C.year
inner join Fund D on A.FundPK = D.FundPK and D.Status = 2 and D.Type = 8
where A.ValueDate <= @dateto and A.status = 2 and A.ValueDate > B.ValueDate and A.CashAmount > 1 and D.Type = 8

declare @RunningAmount numeric(26,4)
declare @FundPK int
declare @FundClientPK int

Declare A Cursor For
	select A.FundPK,A.FundClientPK from @tableSelect A
	left join (
	select FundPK,FundCLientPK from @tableSelect where orderby = 5
	) B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
	where B.FundPK is null and orderby = 4
Open A
Fetch Next From A
Into @FundPK,@FundClientPK

While @@FETCH_STATUS = 0  
BEGIN	
	delete @tableSelect where fundpk = @FundPK and FundClientPK = @FundClientPK and orderby = 4

	Fetch Next From A 
	into @FundPK,@FundClientPK
End	
Close A
Deallocate A


Declare A Cursor For
	Select Distinct  FundPK,FundClientPK from @tableSelect 
Open A
Fetch Next From A
Into @FundPK,@FundClientPK

While @@FETCH_STATUS = 0  
BEGIN	
	set @RunningAmount = 0;


	WITH q AS (
		select top 1000000000 * from @tableSelect
		where  FundPK = @FundPK and FundClientPK = @FundClientPK
		order by year,fundpk,fundclientpk,orderby,TradeDate
	) 
	UPDATE q 
	SET    @RunningAmount = EndingUnit = @RunningAmount + NetUnit
	
	 

	Fetch Next From A 
	into @FundPK,@FundClientPK
End	
Close A
Deallocate A

update A set A.NetAmount = isnull(B.NetAmount,0), A.NetUnit = isnull(B.NetUnit,0)
from @tableSelect A
left join (
	select FundPK,FundClientPK,sum(NetAmount) NetAmount, sum(NetUnit) NetUnit from @tableSelect where orderby = 1
	group by FundPK,FundClientPK
) B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where orderby = 2

update A set A.NetAmount = isnull(B.NetAmount,0),  A.NetUnit = isnull(B.NetUnit,0)
from @tableSelect A
left join (
	select FundPK,FundClientPK,sum(NetAmount) NetAmount, sum(NetUnit) NetUnit from @tableSelect where orderby in (2,3)
	group by FundPK,FundClientPK
) B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where orderby = 4


select year,B.Name FundName,A.Type,TradeDate,cast(case when dbo.CheckTodayIsHoliday(dateadd(year,1,TradeDate)) = 1 then dbo.FWorkingDay(dateadd(year,1,TradeDate),-1) else dateadd(year,1,TradeDate) end as date) DueDate,
NetAmount,NAVSubsRed,NetUnit,EndingUnit,20 TargetPerformacePercent,1.2 * A.NAVSubsRed TargetNAV,NetUnit * 1.2 * A.NAVSubsRed TargetAmount,A.NetAmount * 0.2 TargetPerformanceAmount,isnull(C.NAV,0) CurrentNAV,isnull(D.Nav,0) DueDateNAV,0 PerformanceFee from @tableSelect A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join CloseNAV C on A.FundPK = C.FundPK and C.Status = 2 and C.Date = @dateto
left join CloseNAV D on A.FundPK = D.FundPK and D.Status = 2 and D.Date = cast(case when dbo.CheckTodayIsHoliday(dateadd(year,1,TradeDate)) = 1 then dbo.FWorkingDay(dateadd(year,1,TradeDate),-1) else dateadd(year,1,TradeDate) end as date)
where 1 = 1 " + _paramFundFrom + @"
order by A.fundpk,A.fundclientpk,orderby,TradeDate,year

                             ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DraftPerformanceReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DraftPerformanceReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "UnitRegistryReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = null;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DraftPerformanceReport> rList = new List<DraftPerformanceReport>();
                                        while (dr0.Read())
                                        {

                                            DraftPerformanceReport rSingle = new DraftPerformanceReport();
                                            rSingle.FundName = dr0["FundName"].ToString();
                                            rSingle.Year = dr0["Year"].ToString();
                                            rSingle.Type = dr0["Type"].ToString();
                                            rSingle.TradeDate = Convert.ToDateTime(dr0["TradeDate"]);
                                            rSingle.DueDate = Convert.ToDateTime(dr0["DueDate"]);
                                            rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rSingle.NAVSubsRed = Convert.ToDecimal(dr0["NAVSubsRed"]);
                                            rSingle.NetUnit = Convert.ToDecimal(dr0["NetUnit"]);
                                            rSingle.EndingUnit = Convert.ToDecimal(dr0["EndingUnit"]);
                                            rSingle.TargetPerformacePercent = Convert.ToDecimal(dr0["TargetPerformacePercent"]);
                                            rSingle.TargetNAV = Convert.ToDecimal(dr0["TargetNAV"]);
                                            rSingle.TargetAmount = Convert.ToDecimal(dr0["TargetAmount"]);
                                            rSingle.TargetPerformanceAmount = Convert.ToDecimal(dr0["TargetPerformanceAmount"]);
                                            rSingle.CurrentNAV = Convert.ToDecimal(dr0["CurrentNAV"]);
                                            rSingle.DueDateNAV = Convert.ToDecimal(dr0["DueDateNAV"]);
                                            rSingle.PerformanceFee = Convert.ToDecimal(dr0["PerformanceFee"]);


                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { r.FundName, r.Year } into rGroup
                                                select rGroup;


                                        int incRowExcel = 0;
                                        int RowB = 0;
                                        int RowG = 0;
                                        string _fundName = "";
                                        int no = 1;
                                        foreach (var rsHeader in GroupByReference)
                                        {


                                            if (_fundName != rsHeader.Key.FundName)
                                            {
                                                worksheet = package.Workbook.Worksheets.Add(rsHeader.Key.FundName);
                                                incRowExcel = 1;
                                                no = 1;

                                                incRowExcel++;


                                                RowB = incRowExcel;
                                                RowG = incRowExcel + 1;
                                                worksheet.Cells[incRowExcel, 1, RowG, 19].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, RowG, 19].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, RowG, 19].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, RowG, 19].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = "No";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":A" + RowG].Merge = true;
                                                worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "Year";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + incRowExcel + ":B" + RowG].Merge = true;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = "Type";
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells["C" + incRowExcel + ":C" + RowG].Merge = true;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Trade Date";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells["D" + incRowExcel + ":D" + RowG].Merge = true;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Due Date";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells["E" + incRowExcel + ":E" + RowG].Merge = true;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Net Amount (IDR)";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells["F" + incRowExcel + ":F" + RowG].Merge = true;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "NAV SUBS / RED";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "NET UNIT";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "ENDING UNIT";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Target Performance %";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Target NAV";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "Target AMOUNT";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "Target Performance (Amount)";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = "Current NAV";
                                                worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 15].Value = "Due Date NAV";
                                                worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Merge = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 16].Value = "Current Amount";
                                                worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                                worksheet.Cells["P" + RowB + ":P" + RowG].Merge = true;
                                                worksheet.Cells["P" + RowB + ":P" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["P" + RowB + ":P" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 17].Value = "R/L";
                                                worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                                worksheet.Cells["Q" + RowB + ":Q" + RowG].Merge = true;
                                                worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 18].Value = "% R/L";
                                                worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                                worksheet.Cells["R" + RowB + ":R" + RowG].Merge = true;
                                                worksheet.Cells["R" + RowB + ":R" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["R" + RowB + ":R" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 19].Value = "Performance Fee";
                                                worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                                worksheet.Cells["S" + RowB + ":S" + RowG].Merge = true;
                                                worksheet.Cells["S" + RowB + ":S" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["S" + RowB + ":S" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.WrapText = true;

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Font.Size = 12;
                                                incRowExcel++;

                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 0;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 19];
                                                worksheet.Column(1).Width = 8;
                                                worksheet.Column(2).Width = 8;
                                                worksheet.Column(3).Width = 22;
                                                worksheet.Column(4).Width = 12;
                                                worksheet.Column(5).Width = 12;
                                                worksheet.Column(6).Width = 24;
                                                worksheet.Column(7).Width = 17;
                                                worksheet.Column(8).Width = 20;
                                                worksheet.Column(9).Width = 20;
                                                worksheet.Column(10).Width = 17;
                                                worksheet.Column(11).Width = 18;
                                                worksheet.Column(12).Width = 18;
                                                worksheet.Column(13).Width = 23;
                                                worksheet.Column(14).Width = 17;
                                                worksheet.Column(15).Width = 17;
                                                worksheet.Column(16).Width = 18;
                                                worksheet.Column(17).Width = 18;
                                                worksheet.Column(18).Width = 9;
                                                worksheet.Column(19).Width = 18;



                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                                //worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                                                                               //worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Report Equity Summary";


                                                //Image img = Image.FromFile(Tools.ReportImage);
                                                //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                                //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                                //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                                //worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                                //Image img = Image.FromFile(Tools.ReportImage);
                                                //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);

                                                //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                                //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                                //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                                worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();
                                            }

                                            else
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                                no++;
                                            }

                                            int first = incRowExcel;


                                            int _startRowDetail = incRowExcel + 1;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                if (rsDetail.Type == "BEGINNING BALANCED")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = no;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Year;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.TradeDate;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.DueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAVSubsRed;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.EndingUnit;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.TargetPerformacePercent;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#0\\.00%";

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TargetNAV;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.TargetAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TargetPerformanceAmount;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                }

                                                else if (rsDetail.Type == "SUB")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = no;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Year;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.TradeDate;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.DueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAVSubsRed;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.EndingUnit;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.TargetPerformacePercent;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#0\\.00%";

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TargetNAV;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.TargetAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TargetPerformanceAmount;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.CurrentNAV;
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.DueDateNAV;
                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";

                                                    int rowM = incRowExcel - 1;

                                                    worksheet.Cells[incRowExcel, 16].Formula = "=H" + incRowExcel + "*N" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 17].Formula = "=P" + incRowExcel + "-F" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 18].Formula = "=Q" + incRowExcel + "/F" + incRowExcel + " * 100";
                                                    worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#0\\.00%";

                                                    worksheet.Cells[incRowExcel, 19].Formula = "=IF(0.3 * (Q" + incRowExcel + "-M" + incRowExcel + ") < 0 ,0,0.3 * (Q" + incRowExcel + "-M" + incRowExcel + "))";
                                                    worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 19].Calculate();
                                                }

                                                else if (rsDetail.Type == "TOTAL")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.DueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAVSubsRed;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.TargetPerformacePercent;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#0\\.00%";

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TargetNAV;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.TargetAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.CurrentNAV;
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.DueDateNAV;
                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 16].Formula = "=H" + incRowExcel + "*N" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";

                                                }

                                                else if (rsDetail.Type == "RED")
                                                {

                                                    worksheet.Cells[incRowExcel, 1].Value = no;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Year;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.TradeDate;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.DueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetAmount;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                    if (Convert.ToDecimal(rsDetail.NetAmount) < 0)
                                                    {
                                                        worksheet.Cells[incRowExcel, 6].Style.Font.Color.SetColor(Color.Red);
                                                    }


                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAVSubsRed;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                    if (Convert.ToDecimal(rsDetail.NAVSubsRed) < 0)
                                                    {
                                                        worksheet.Cells[incRowExcel, 7].Style.Font.Color.SetColor(Color.Red);
                                                    }


                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.NetUnit;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    if (Convert.ToDecimal(rsDetail.NetUnit) < 0)
                                                    {
                                                        worksheet.Cells[incRowExcel, 7].Style.Font.Color.SetColor(Color.Red);
                                                    }


                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.EndingUnit;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.TargetPerformacePercent;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#0\\.00%";

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TargetNAV;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.TargetAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TargetPerformanceAmount;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                }

                                                _endRowDetail = incRowExcel + 1;


                                            }

                                            incRowExcel++;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Merge = true;
                                            worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Merge = true;
                                            worksheet.Cells[_startRowDetail, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[_startRowDetail, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            _fundName = rsHeader.Key.FundName;
                                            //worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;



                                        }

                                        //worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.PrinterSettings.FitToWidth = 1;
                                        //worksheet.PrinterSettings.FitToHeight = 0;
                                        //worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 19];
                                        //worksheet.Column(1).Width = 8;
                                        //worksheet.Column(2).Width = 8;
                                        //worksheet.Column(3).Width = 22;
                                        //worksheet.Column(4).Width = 12;
                                        //worksheet.Column(5).Width = 12;
                                        //worksheet.Column(6).Width = 24;
                                        //worksheet.Column(7).Width = 16;
                                        //worksheet.Column(8).Width = 20;
                                        //worksheet.Column(9).Width = 20;
                                        //worksheet.Column(10).Width = 15;
                                        //worksheet.Column(11).Width = 18;
                                        //worksheet.Column(12).Width = 18;
                                        //worksheet.Column(13).Width = 23;
                                        //worksheet.Column(14).Width = 11;
                                        //worksheet.Column(15).Width = 11;
                                        //worksheet.Column(16).Width = 18;
                                        //worksheet.Column(17).Width = 18;
                                        //worksheet.Column(18).Width = 9;
                                        //worksheet.Column(19).Width = 18;




                                        //// BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        ////worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        ////worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        ////worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Report Equity Summary";


                                        ////Image img = Image.FromFile(Tools.ReportImage);
                                        ////worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //// BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        ////ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        ////worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        ////worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        ////worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        //worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        //worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        //worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        ////Image img = Image.FromFile(Tools.ReportImage);
                                        ////worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);

                                        ////worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        ////worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }

                catch (Exception err)
                {
                    return false;
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