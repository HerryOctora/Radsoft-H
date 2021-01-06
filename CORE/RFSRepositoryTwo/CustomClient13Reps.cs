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
using System.Data.OleDb;using RFSRepository;

namespace RFSRepositoryTwo
{
    public class CustomClient13Reps
    {
        Host _host = new Host();

        private class ClientPerformance
        {
            public string ClientName { get; set; }
            public string InvestmentDate { get; set; }
            public string LastDate { get; set; }
            public string FundName { get; set; }
            public decimal InvestmentAmount { get; set; }
            public decimal NAVInvestment { get; set; }
            public decimal LastNAV { get; set; }
            public decimal Unit { get; set; }
            public decimal BalanceUnit { get; set; }
            public string CurrencyID { get; set; }
            public decimal BalanceCurrency { get; set; }
            public decimal GainLoss { get; set; }
            public decimal YieldPercent { get; set; }
            public string SalesID { get; set; }




        }

        private class RiskAnalyzeReport
        {

            public string Month { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public int Legality { get; set; }
            public string InvestorsRiskProfileName { get; set; }
            public DateTime RenewingDate { get; set; }
            public string UpdateUsersID { get; set; }
            public string ApprovedUsersID { get; set; }
            public string Notes { get; set; }
            public string Date { get; set; }

        }

        private class CouponBonds
        {
            public string FundName { get; set; }
            public string Date { get; set; }
            public string Transaction { get; set; }
            public string AcqDate { get; set; }
            public string BondsCode { get; set; }
            public string Instrument { get; set; }
            public decimal Nominal { get; set; }
            public decimal AVGPrice { get; set; }
            public string CouponOrDividend { get; set; }
            public string LastPayment { get; set; }
            public string NextPayment { get; set; }
            public decimal AccruedDays { get; set; }
            public decimal AccruedCoupon { get; set; }
            public decimal Proceeds { get; set; }
            public decimal NettProceeds { get; set; }

        }

        private class DailyTransaction
        {
            public string FundName { get; set; }
            public string SecurityCode { get; set; }
            public string SecurityDesc { get; set; }
            public DateTime TradeDate { get; set; }
            public DateTime SettlementDate { get; set; }
            public string Category { get; set; }
            public string Broker { get; set; }
            public string Status { get; set; }
            public decimal TotalShares { get; set; }
            public decimal Lot { get; set; }
            public decimal PriceShares { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal BrokerageFee { get; set; }
            public decimal VAT { get; set; }
            public decimal Levy { get; set; }
            public decimal SalesTax { get; set; }
            public decimal TotalBeforeTax { get; set; }
            public decimal TaxBrokerageFee { get; set; }
            public decimal TotalPayment { get; set; }

            public string Instrument { get; set; }
            public DateTime InstructionDate { get; set; }
            public DateTime LastCouponDate { get; set; }
            public DateTime NextCouponDate { get; set; }
            public DateTime MaturityDate { get; set; }
            public decimal FaceValue { get; set; }
            public decimal PricePercent { get; set; }
            public decimal PriceAmount { get; set; }
            public int AccruedInterestDays { get; set; }
            public decimal AccruedInterestPercent { get; set; }
            public decimal AccruedInterestAmount { get; set; }
            public decimal TaxCapitalGain { get; set; }
            public decimal TaxAccruedInt { get; set; }
            public decimal TaxGainInt { get; set; }
            public decimal CouponFrequency { get; set; }

            public string Bank { get; set; }
            public string BankBranch { get; set; }
            public string TransactionType { get; set; }
            public decimal Volume { get; set; }
            public DateTime PlacementDate { get; set; }
            public string InterestDays { get; set; }
            public decimal Interest { get; set; }



        }

        public class DailyTransactionFormBonds
        {
            public DateTime Date { get; set; }
            public string RefNo { get; set; }
            public string Untuk { get; set; }
            public string Attention { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string FundID { get; set; }
            public DateTime TradeDate { get; set; }
            public DateTime SettleDate { get; set; }
            public DateTime AcqDate { get; set; }
            public decimal AcqPrice { get; set; }
            public string TrxType { get; set; }

            public string Bonds { get; set; }
            public string BondsCode { get; set; }
            public string ISIN { get; set; }
            public DateTime MaturityDate { get; set; }
            public decimal CouponRate { get; set; }
            public DateTime LastCouponDate { get; set; }
            public DateTime NextCouponDate { get; set; }
            public decimal Nominal { get; set; }
            public decimal Price { get; set; } //Percent
            public decimal Proceed { get; set; }
            public decimal AccruedInterest { get; set; }
            public string DaysCount { get; set; }
            public decimal TaxAccruedInterest { get; set; }
            public decimal TaxCapitalGain { get; set; }
        }


        public class DailyTransactionFormDeposito
        {
            public string BankTo { get; set; }
            public string AttentionTo { get; set; }
            public string FaxTo { get; set; }
            public string TelpTo { get; set; }

            public string BankCc { get; set; }
            public string AttentionCc { get; set; }
            public string FaxCc { get; set; }
            public string TelpCc { get; set; }

            public string FundName { get; set; }
            public string ACNo { get; set; }
            public string RefNo { get; set; }
            public DateTime InstructionDate { get; set; }

            public string BankName { get; set; }
            public string RTGSCode { get; set; }
            public string TargetAccName { get; set; }
            public string AccountNo { get; set; }
            public decimal Amount { get; set; }
            public decimal InterestRate { get; set; }
            public decimal InterestAmount { get; set; }
            public DateTime ValueDate { get; set; }
            public string Tenor { get; set; }
            public DateTime MaturityDate { get; set; }
            public string DaysCount { get; set; }
            public string TrxType { get; set; }
            public string BitBreakable { get; set; }
        }

        public class DailyTransactionFormEquity
        {
            public string Attention { get; set; }
            public string FundName { get; set; }
            public string CustodianBank { get; set; }
            public string TrxType { get; set; }
            public string BrokerName { get; set; }
            public string FaxNumber { get; set; }
            public string RefNumber { get; set; }
            public DateTime TradeDate { get; set; }
            public DateTime SettleDate { get; set; }
            public string BrokerCode { get; set; }
            public string SecuritiesCode { get; set; }
            public string SecuritiesName { get; set; }
            public decimal Quantity { get; set; }
            public decimal PriceShare { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal Commission { get; set; }
            public decimal Levy { get; set; }
            public decimal KPEI { get; set; }
            public decimal VAT { get; set; }
            public decimal IncomeTaxSell { get; set; }
            public decimal WHT { get; set; }
            public decimal TotalAmount { get; set; }
        }

        public Boolean GenerateReportCompliance(string _userID, OjkRpt _OjkRpt)
        {

            #region Risk Analyze Report
            if (_OjkRpt.ReportName.Equals("19"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =

                            @"
                            select datename(MONTH, RenewingDate) Month,A.ID, Name,B.DescOne InvestorsRiskProfileName,RenewingDate,A.UpdateUsersID,A.ApprovedUsersID,A.Notes,Convert(date, RenewingDate) Date from FundClient A
                            left join MasterValue B on A.Legality = B.Code and B.ID = 'InvestorsRiskProfile' and B.status = 2
                            where A.status in (1,2) and Convert(date, RenewingDate) <= DateAdd(month, 12, Convert(date, GetDate())) 
                            order by date asc";



                            cmd.CommandTimeout = 0;
                            //cmd.Parameters.AddWithValue("@RenewingDateTo", _unitRegistryRpt.ValueDateTo);
                            //cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            //cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "RiskAnalyzeReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "RiskAnalyzeReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "Risk Analyze Report Tahunan";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("RiskAnalyzeReport");

                                        int incRowExcel = 1;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString(); // + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 20;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RiskAnalyzeReport> rList = new List<RiskAnalyzeReport>();
                                        while (dr0.Read())
                                        {
                                            RiskAnalyzeReport rSingle = new RiskAnalyzeReport();
                                            rSingle.Month = dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]);
                                            rSingle.ID = dr0["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ID"]);
                                            rSingle.Name = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.InvestorsRiskProfileName = dr0["InvestorsRiskProfileName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InvestorsRiskProfileName"]);
                                            rSingle.RenewingDate = Convert.ToDateTime(dr0["RenewingDate"]);
                                            rSingle.UpdateUsersID = dr0["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UpdateUsersID"]);
                                            rSingle.ApprovedUsersID = dr0["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ApprovedUsersID"]);
                                            rSingle.Notes = dr0["Notes"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Notes"]);
                                            rSingle.Date = dr0["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Date"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.Month } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "MONTH : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Month;
                                            //worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Size = 16;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "CLIENT ID";
                                            worksheet.Cells[incRowExcel, 2].Value = "CLIENT NAME";
                                            //worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 3].Value = "LEGALITY";
                                            worksheet.Cells[incRowExcel, 3].Value = "RISK PROFILE";
                                            worksheet.Cells[incRowExcel, 4].Value = "RENEWINGDATE";
                                            worksheet.Cells[incRowExcel, 5].Value = "UPDATE USERS ID";
                                            worksheet.Cells[incRowExcel, 6].Value = "APPRUVED USERS ID";
                                            worksheet.Cells[incRowExcel, 7].Value = "NOTES";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            //int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                //worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InvestorsRiskProfileName;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.RenewingDate;
                                                //worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.UpdateUsersID;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.ApprovedUsersID;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Notes;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 16;


                                                incRowExcel++;
                                                //no++;
                                            }

                                            int last = incRowExcel - 1;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 1;

                                            //worksheet.Cells[incRowExcel, 4].Value = "TOTAL UNIT PENYERTAAN :";
                                            //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            //worksheet.Cells[incRowExcel, 5].Calculate();

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 16;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            //worksheet.Cells[incRowExcel, 6, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            //worksheet.Cells[incRowExcel, 6, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                            //worksheet.Cells.Calculate();
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 2;
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 7];
                                        worksheet.Column(1).Width = 16;
                                        worksheet.Column(2).Width = 88;
                                        worksheet.Column(3).Width = 21;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 27;
                                        worksheet.Column(7).Width = 25;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&35 LAPORAN RISK PROFILE TAHUNAN \n " + "&30 REKSA DANA PT EMCO ASSET MANAGEMANT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        Image thumb = img.GetThumbnailImage(Tools.imgWidth, Tools.imgHeight, null, IntPtr.Zero);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

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
            #endregion //

            #region Tata Kelola MI Report
            if (_OjkRpt.ReportName.Equals("21"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =

                            @"
                            select * from CorporateGovAM where status in (1,2)";



                            cmd.CommandTimeout = 0;
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    //string filePath = Tools.ReportsPath + "RiskAnalyzeReport" + "_" + _userID + ".xlsx";
                                    //string pdfPath = Tools.ReportsPath + "RiskAnalyzeReport" + "_" + _userID + ".pdf";
                                    //FileInfo excelFile = new FileInfo(filePath);
                                    //if (excelFile.Exists)
                                    //{
                                    //    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    //    excelFile = new FileInfo(filePath);
                                    //}

                                    string filePath = Tools.ReportsPath + "TataKelolaMI" + "_" + _userID + ".xlsx";
                                    File.Copy(Tools.ReportsTemplatePath + "TATA KELOLA MI.xlsx", filePath, true);
                                    FileInfo excelFile = new FileInfo(filePath);

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                                        int incRowExcel = 2;

                                        ////worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString(); // + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        ////worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        ////worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        ////worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 20;
                                        ////worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        ////worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        while (dr0.Read())
                                        {
                                            int incColExcel = 5;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row1"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row2"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row3"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row4"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row5"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row6"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row7"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row8"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row9"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row10"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row11"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row12"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row13"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row14"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row15"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row16"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row17"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row18"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row19"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row20"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row21"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row22"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row23"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row24"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row25"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row26"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row27"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row28"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row29"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row30"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row31"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row32"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row33"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row34"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row35"]);
                                            incRowExcel = 38;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row36"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row37"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row38"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row39"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row40"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row41"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row42"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row43"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row44"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row45"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row46"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row47"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row48"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row49"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row50"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row51"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row52"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row53"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row54"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row55"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row56"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row57"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row58"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row59"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row60"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row61"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row62"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row63"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row64"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row65"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row66"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row67"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row68"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row69"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row70"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row71"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row72"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row73"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row74"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row75"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row76"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row77"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row78"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row79"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row80"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row81"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row82"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row83"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row84"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row85"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row86"]);
                                            incRowExcel=91;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row87"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row88"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row89"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row90"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row91"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row92"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row93"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row94"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row95"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row96"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row97"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row98"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row99"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row100"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row101"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row102"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row103"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row104"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row105"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row106"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row107"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row108"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row109"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row110"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row111"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row112"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row113"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row114"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row115"]);
                                            incRowExcel=121;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row116"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row117"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row118"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row119"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row120"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row121"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row122"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row123"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row124"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row125"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row126"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row127"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row128"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row129"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row130"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row131"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row132"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row133"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row134"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row135"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row136"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row137"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row138"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row139"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row140"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row141"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row142"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row143"]);
                                            incRowExcel=150;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row144"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row145"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row146"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row147"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row148"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row149"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row150"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row151"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row152"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row153"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row154"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row155"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row156"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row157"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row158"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row159"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row160"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row161"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row162"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row163"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row164"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row165"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row166"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row167"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row168"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row169"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row170"]);
                                            incRowExcel = 178;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row171"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row172"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row173"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row174"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row175"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row176"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row177"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row178"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row179"]);
                                            incRowExcel=188;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row180"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row181"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row182"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row183"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row184"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row185"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row186"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row187"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row188"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row189"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row190"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row191"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row192"]);
                                            incRowExcel=202;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row193"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row194"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row195"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row196"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row197"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row198"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row199"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row200"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row201"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row202"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row203"]);
                                            incRowExcel=214;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row204"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row205"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row206"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row207"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row208"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row209"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row210"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row211"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row212"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row213"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row214"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row215"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row216"]);
                                            incRowExcel=228;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row217"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row218"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row219"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row220"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row221"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row222"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row223"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row224"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row225"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row226"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row227"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row228"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row229"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row230"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row231"]);
                                            incRowExcel=244;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row232"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row233"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row234"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row235"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row236"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row237"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row238"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row239"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row240"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row241"]);
                                            incRowExcel=255;

                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row242"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row243"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row244"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row245"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row246"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row247"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row248"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row249"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row250"]);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr0["Row251"]);
                                        }

                                        worksheet.Calculate();

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 7];


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&35 LAPORAN RISK PROFILE TAHUNAN \n " + "&30 REKSA DANA PT EMCO ASSET MANAGEMANT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        Image thumb = img.GetThumbnailImage(Tools.imgWidth, Tools.imgHeight, null, IntPtr.Zero);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        //if (_OjkRpt.DownloadMode == "PDF")
                                        //{
                                        //    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        //}
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


            else
            {
                return false;
            }
        }

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {
            #region Client Performance (Yield %)
            if (_unitRegistryRpt.ReportName.Equals("Client Performance"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramFund = "";
                            string _paramFundClient = "";
                            string _paramAgent = "";


                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = " And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }

                            cmd.CommandText =
                                //-- Query butuh bantuan ini masih ngasal
                            @"
                            Select isnull(B.Name,'') ClientName 
                            ,A.Date LastDate
                            ,isnull(C.Name,'') FundName
                            ,dbo.FGetAVGForFundClientPosition(A.DAte,A.FundClientPK,A.FundPK) * A.UnitAmount
                            InvestmentAmount
                            ,dbo.FGetAVGForFundClientPosition(A.DAte,A.FundClientPK,A.FundPK) NAVInvestment
                            ,D.NAV LastNAV, A.UnitAmount BalanceUnit
                            ,isnull(E.ID,'') CurrencyID
                            ,isnull(D.NAV * A.UnitAmount * case when C.CurrencyPK = 1 then 1 else
                            F.Rate end,0) BalanceCurrency
                            ,(dbo.FGetAVGForFundClientPosition(A.DAte,A.FundClientPK,A.FundPK) * A.UnitAmount)
                            - isnull(D.NAV * A.UnitAmount * case when C.CurrencyPK = 1 then 1 else
                            F.Rate end,0) GainLoss
                            ,0 YieldPersen -- Pake Rumus
                            ,Isnull(G.ID,'') SalesID
                            from FundClientPosition A
                            left join FundClient B on A.FundClientPK = B.FundclientPK and B.status in (1,2)
                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2) 
                            left join CloseNAV D on A.FundPK = D.FundPK and D.status in (1,2) and D.date = A.Date
                            left join Currency E on C.CurrencyPK = E.CurrencyPK and E.status in (1,2)
                            left join CurrencyRate F on E.CurrencyPK = F.CurrencyPK and F.status in (1,2)
                            and F.Date = A.Date
                            left join Agent G on B.SellingAgentPK = G.AgentPK and G.status in (1,2)
                            where A.Date = @ValuedateFrom " + _paramFund + _paramFundClient;


                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValuedateFrom", _unitRegistryRpt.ValueDateFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ClientPerformence" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ClientPerformence" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Client Performence");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ClientPerformance> rList = new List<ClientPerformance>();
                                        while (dr0.Read())
                                        {
                                            ClientPerformance rSingle = new ClientPerformance();
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            //rSingle.InvestmentDate = Convert.ToString(dr0["InvestmentDate"]);
                                            rSingle.LastDate = Convert.ToString(dr0["LastDate"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.InvestmentAmount = Convert.ToDecimal(dr0["InvestmentAmount"]);
                                            rSingle.NAVInvestment = Convert.ToDecimal(dr0["NAVInvestment"]);
                                            rSingle.LastNAV = Convert.ToDecimal(dr0["LastNAV"]);
                                            //rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                            rSingle.BalanceUnit = Convert.ToDecimal(dr0["BalanceUnit"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.BalanceCurrency = Convert.ToDecimal(dr0["BalanceCurrency"]);
                                            rSingle.GainLoss = Convert.ToDecimal(dr0["GainLoss"]);
                                            rSingle.YieldPercent = Convert.ToDecimal(dr0["YieldPersen"]);
                                            rSingle.SalesID = Convert.ToString(dr0["SalesID"]);


                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel = 0;
                                        //int _rowEndBalance = 0;

                                        foreach (var rsHeader in GroupByCategory)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Performance Report Client Mutual Fund RAHA Aset Manajemen";
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Merge = true;

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Client Name";
                                            worksheet.Cells[incRowExcel, 2].Value = "Last Date";//print date
                                            worksheet.Cells[incRowExcel, 3].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 4].Value = "Investment Amount (IDR)";
                                            worksheet.Cells[incRowExcel, 5].Value = "Last NAV";
                                            worksheet.Cells[incRowExcel, 6].Value = "Balance Unit";
                                            worksheet.Cells[incRowExcel, 7].Value = "Currency";
                                            worksheet.Cells[incRowExcel, 8].Value = "Balance Currency";
                                            worksheet.Cells[incRowExcel, 9].Value = "Gain / Loss";
                                            worksheet.Cells[incRowExcel, 10].Value = "Yield Percent";
                                            worksheet.Cells[incRowExcel, 11].Value = "Sales ID";
                                            //add currency

                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;

                                            worksheet.Row(incRowExcel).Height = 5;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = incRowExcel;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.ClientName;

                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.LastDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.InvestmentAmount;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.LastNAV;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.BalanceUnit;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.CurrencyID;

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.BalanceCurrency;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.GainLoss;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                                //worksheet.Cells[incRowExcel, 10].Value = rsDetail.YieldPercent;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0%";
                                                worksheet.Cells[incRowExcel, 10].Formula = "IFERROR(SUM(I" + incRowExcel + "/D" + incRowExcel + "),0)";

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.SalesID;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";


                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;

                                            }
                                            //int _endRowDetailTotal = incRowExcel;
                                            //worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }

                                        string _rangeDetail = "A:M";

                                        using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Size = 12;
                                            r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        }



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                        worksheet.Column(1).Width = 35;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 35;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 17;
                                        worksheet.Column(7).Width = 13;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 18;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 25;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 UNIT TRUST REPORT";

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //Image thumb = img.GetThumbnailImage(175, 100, null, IntPtr.Zero);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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

            else
            {
                return false;
            }
        }

        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {

            //TRIAL BALANCE 

            #region Daily Transaction Form Equity
            if (_FundAccountingRpt.ReportName.Equals("Daily Transaction Form Equity"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramCounterpart = "";
                            string _paramTrxType = "";

                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = "And A.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }

                            if (_FundAccountingRpt.TrxType == "1")
                            {
                                _paramTrxType = "and A.TrxType in (1) ";
                            }
                            else if (_FundAccountingRpt.TrxType == "2")
                            {
                                _paramTrxType = "and A.TrxType in (2) ";
                            }
                            else
                            {
                                _paramTrxType = "";
                            }

                            cmd.CommandText = @"
                            select D.ContactPerson Attention,
	                               B.Name FundName,
	                               E.Name CustodianBank,
	                               A.TrxTypeID TrxType,
	                               F.Name BrokerName,
	                               D.Fax1 FaxNumber,
	                               A.Reference RefNumber,
	                               A.ValueDate TradeDate,
	                               A.SettlementDate SettleDate,
	                               F.ID BrokerCode,
	                               G.ID SecuritiesCode,
	                               G.Name SecuritiesName,
	                               A.DoneVolume Quantity,
	                               A.DonePrice PriceShare,
	                               A.Amount GrossAmount,
	                               A.CommissionAmount Commission,
	                               A.LevyAmount Levy,
	                               A.KPEIAmount KPEi,
	                               A.VATAmount VAT,
	                               A.IncomeTaxSellAmount IncomeTaxSell,
	                               A.WHTAmount WHT,
	                               A.TotalAmount TotalAmount

	                                from Investment A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            left join FundCashRef C on A.FundPK = C.FundPK and C.Status in (1,2) 
                            left join BankBranch D on C.BankBranchPK = D.BankBranchPK and D.status in(1,2) and D.Type = 2
                            left join Bank E on D.BankPK = E.BankPK and E.Status in (1,2)
                            left join Counterpart F on A.CounterpartPK = F.CounterpartPK and F.Status in (1,2)
                            left join Instrument G on A.InstrumentPK = G.InstrumentPK and G.Status in (1,2)
                            Where A.ValueDate between @ValueDateFrom and @ValueDateTo " + _paramCounterpart + _paramTrxType + @" and A.StatusInvestment = 2 ";



                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValuedateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyTransactionFormEquity" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionFormEquity" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "DailyTransactionFormEquity";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Form Equity Dealing Saham");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyTransactionFormEquity> rList = new List<DailyTransactionFormEquity>();
                                        while (dr0.Read())
                                        {

                                            DailyTransactionFormEquity rSingle = new DailyTransactionFormEquity();
                                            rSingle.Attention = Convert.ToString(dr0["Attention"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.CustodianBank = Convert.ToString(dr0["CustodianBank"]);
                                            rSingle.TrxType = Convert.ToString(dr0["TrxType"]);
                                            rSingle.BrokerName = Convert.ToString(dr0["BrokerName"]);
                                            rSingle.FaxNumber = Convert.ToString(dr0["FaxNumber"]);
                                            rSingle.RefNumber = Convert.ToString(dr0["RefNumber"]);
                                            rSingle.TradeDate = Convert.ToDateTime(dr0["TradeDate"]);
                                            rSingle.SettleDate = Convert.ToDateTime(dr0["SettleDate"]);
                                            rSingle.BrokerCode = Convert.ToString(dr0["BrokerCode"]);
                                            rSingle.SecuritiesCode = Convert.ToString(dr0["SecuritiesCode"]);
                                            rSingle.SecuritiesName = Convert.ToString(dr0["SecuritiesName"]);
                                            rSingle.Quantity = Convert.ToDecimal(dr0["Quantity"]);
                                            rSingle.PriceShare = Convert.ToDecimal(dr0["PriceShare"]);
                                            rSingle.GrossAmount = Convert.ToDecimal(dr0["GrossAmount"]);
                                            rSingle.Commission = Convert.ToDecimal(dr0["Commission"]);
                                            rSingle.Levy = Convert.ToDecimal(dr0["Levy"]);
                                            rSingle.KPEI = Convert.ToDecimal(dr0["KPEI"]);
                                            rSingle.VAT = Convert.ToDecimal(dr0["VAT"]);
                                            rSingle.IncomeTaxSell = Convert.ToDecimal(dr0["IncomeTaxSell"]);
                                            rSingle.WHT = Convert.ToDecimal(dr0["WHT"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);




                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                orderby r.FundName, r.TrxType ascending
                                                group r by new { r.FundName, r.TrxType, r.Attention, r.FaxNumber, r.CustodianBank, r.TradeDate, r.SettleDate, r.BrokerName, r.BrokerCode } into rGroup
                                                select rGroup;

                                        int incRowExcel = 2;
                                        worksheet.Row(incRowExcel).Height = 30;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 7].Value = "DAILY TRANSACTION FORM";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Size = 28;
                                        worksheet.Cells[incRowExcel, 7, incRowExcel, 13].Merge = true;
                                        incRowExcel = incRowExcel + 2;



                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Attention";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Attention;
                                            worksheet.Cells[incRowExcel, 8].Value = "FaxNumber";
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.FaxNumber;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "FundName";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 8].Value = "Ref Number";
                                            worksheet.Cells[incRowExcel, 9].Value = "";
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Custodian Bank";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CustodianBank;
                                            worksheet.Cells[incRowExcel, 8].Value = "Trade Date";
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.TradeDate;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Transaction Type";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TrxType;
                                            worksheet.Cells[incRowExcel, 8].Value = "Settle Date";
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.SettleDate;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Broker Name";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BrokerName;
                                            worksheet.Cells[incRowExcel, 8].Value = "Broker Code";
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.BrokerCode;



                                            incRowExcel = incRowExcel + 2; ;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "Securities Code";
                                            worksheet.Cells[incRowExcel, 3].Value = "Securities Name";
                                            worksheet.Cells[incRowExcel, 4].Value = "Quantity";
                                            worksheet.Cells[incRowExcel, 5].Value = "Price/Share";
                                            worksheet.Cells[incRowExcel, 6].Value = "Gross Amount";
                                            worksheet.Cells[incRowExcel, 7].Value = "Commission";
                                            worksheet.Cells[incRowExcel, 8].Value = "Levy";
                                            worksheet.Cells[incRowExcel, 9].Value = "KPEI";
                                            worksheet.Cells[incRowExcel, 10].Value = "VAT";
                                            worksheet.Cells[incRowExcel, 11].Value = "Income Tax Sell";
                                            worksheet.Cells[incRowExcel, 12].Value = "WHT";
                                            worksheet.Cells[incRowExcel, 13].Value = "Total Amount";





                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;









                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int no_ = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = no_;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecuritiesCode;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecuritiesName;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Quantity;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.PriceShare;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.GrossAmount;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Commission;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Levy;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.KPEI;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.VAT;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.IncomeTaxSell;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.WHT;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalAmount;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                _endRowDetail = incRowExcel;
                                                no_++;
                                                incRowExcel++;




                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                            incRowExcel++;

                                        }

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Prepare By : ";
                                        worksheet.Cells[incRowExcel, 5].Value = "Approval By : ";
                                        incRowExcel = incRowExcel + 3;
                                        worksheet.Cells[incRowExcel, 1].Value = "Authorized Person";
                                        worksheet.Cells[incRowExcel, 5].Value = "Authorized Person";


                                        //worksheet.Row(incRowExcel).PageBreak = true;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 13];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 16;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 18;
                                        worksheet.Column(7).Width = 14;
                                        worksheet.Column(8).Width = 16;
                                        worksheet.Column(9).Width = 14;
                                        worksheet.Column(10).Width = 14;
                                        worksheet.Column(11).Width = 14;
                                        worksheet.Column(12).Width = 14;
                                        worksheet.Column(13).Width = 28;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Daily Transaction Form Equity Dealing Saham";


                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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

            #region Daily Transaction Form Bonds
            if (_FundAccountingRpt.ReportName.Equals("Daily Transaction Form Bonds"))
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
                                _paramFund = "And B.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }




                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = "And A.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }

                            cmd.CommandText = @"
                            select A.ValueDate Date,
	  --RefNo,
      D.BankAccountName Untuk,
	  D.Attn1 Attention,
	  D.Phone1 Phone,
	  D.Fax1 Fax,
	  B.ID FundID,
	  A.InstructionDate TradeDate,
	  A.SettlementDate SettleDate,
	  A.AcqDate AcqDate,
	  A.AcqPrice AcqPrice,
	  A.TrxTypeID TrxType,
	  C.Name Bonds,
	  C.ID BondsCode,
	  C.ISIN ISIN,
	  C.MaturityDate MaturityDate,
	  A.InterestPercent CouponRate,
	 isnull(A.LastCouponDate,'') LastCouponDate,
	  isnull(A.NextCouponDate,'') NextCouponDate,
	  A.DoneVolume Nominal,
	  A.OrderPrice Price,
	  A.Amount Proceed

From Investment A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) --BankCustody
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2)
left join BankBranch D on B.BankBranchPK = D.BankBranchPK and D.status in(1,2)
left join Bank E on D.BankPK = E.BankPK and E.Status in (1,2)
Where A.ValueDate between @ValueDateFrom and @ValueDateTo and A.StatusInvestment = 2 and A.InstrumentTypePK in (2,8,9,12,13,14,15)
                            ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValuedateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyTransactionFormBonds" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionFormBonds" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "DailyTransactionFormBonds";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Gov_Bonds");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyTransactionFormBonds> rList = new List<DailyTransactionFormBonds>();
                                        while (dr0.Read())
                                        {

                                            DailyTransactionFormBonds rSingle = new DailyTransactionFormBonds();
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            //rSingle.RefNo = Convert.ToString(dr0["RefNo"]);
                                            rSingle.Untuk = Convert.ToString(dr0["Untuk"]);
                                            rSingle.Attention = Convert.ToString(dr0["Attention"]);
                                            rSingle.Phone = Convert.ToString(dr0["Phone"]);
                                            rSingle.Fax = Convert.ToString(dr0["Fax"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.TradeDate = Convert.ToDateTime(dr0["TradeDate"]);
                                            rSingle.SettleDate = Convert.ToDateTime(dr0["SettleDate"]);
                                            rSingle.AcqDate = Convert.ToDateTime(dr0["AcqDate"]);
                                            rSingle.AcqPrice = Convert.ToDecimal(dr0["AcqPrice"]);
                                            rSingle.TrxType = Convert.ToString(dr0["TrxType"]);
                                            rSingle.Bonds = Convert.ToString(dr0["Bonds"]);
                                            rSingle.BondsCode = Convert.ToString(dr0["BondsCode"]);
                                            rSingle.ISIN = Convert.ToString(dr0["ISIN"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            rSingle.CouponRate = Convert.ToDecimal(dr0["CouponRate"]);
                                            rSingle.LastCouponDate = Convert.ToDateTime(dr0["LastCouponDate"]);
                                            rSingle.NextCouponDate = Convert.ToDateTime(dr0["NextCouponDate"]);
                                            rSingle.Nominal = Convert.ToDecimal(dr0["Nominal"]);
                                            rSingle.Price = Convert.ToDecimal(dr0["Price"]);
                                            rSingle.Proceed = Convert.ToDecimal(dr0["Proceed"]);
                                            //rSingle.AccruedInterest = Convert.ToDecimal(dr0["AccruedInterest"]);
                                            //rSingle.DaysCount = Convert.ToString(dr0["DaysCount"]);
                                            //rSingle.TaxAccruedInterest = Convert.ToDecimal(dr0["TaxAccruedInterest"]);
                                            //rSingle.TaxCapitalGain = Convert.ToDecimal(dr0["TaxCapitalGain"]);





                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r.FundName, r.TrxType ascending
                                                group r by new { } into rGroup
                                                select rGroup;

                                        int incRowExcel = 1;
                                        int _maturity, _SettledDate, _lastCoupon, _nextCoupon, _accruedInterest, _taxOnAccrued, _TaxOnCapital;
                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            //incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "DAILY TRANSACTION FORM";
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 28;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Merge = true;
                                                incRowExcel = incRowExcel + 3;

                                                worksheet.Cells[incRowExcel, 1].Value = "Date";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 6].Value = "FundID";
                                                worksheet.Cells[incRowExcel, 7].Value = ":";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Ref Number";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.RefNo;
                                                worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                                worksheet.Cells[incRowExcel, 7].Value = ":";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.TradeDate;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "To";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Untuk;
                                                worksheet.Cells[incRowExcel, 6].Value = "Settle Date";
                                                worksheet.Cells[incRowExcel, 7].Value = ":";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.SettleDate;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                _SettledDate = incRowExcel;

                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Attention;
                                                worksheet.Cells[incRowExcel, 6].Value = "Acquisition Date";
                                                worksheet.Cells[incRowExcel, 7].Value = ":";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.AcqDate;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Ph Number";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Phone;
                                                worksheet.Cells[incRowExcel, 6].Value = "Acquisition Price";
                                                worksheet.Cells[incRowExcel, 7].Value = ":";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.AcqPrice;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Fax Number";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Fax;
                                                worksheet.Cells[incRowExcel, 6].Value = "Transaction Type";
                                                worksheet.Cells[incRowExcel, 7].Value = ":";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.TrxType;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Bonds";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Bonds;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "BondsCode";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BondsCode;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "ISIN";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ISIN;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "MaturityDate";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _maturity = incRowExcel;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Coupon Rate (%$)";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.CouponRate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Last Coupon Date";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.LastCouponDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _lastCoupon = incRowExcel;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Next Coupon Date";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.NextCouponDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _nextCoupon = incRowExcel;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Nominal;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Price";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Price;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Proceeds";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Proceed;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Formula = "=(" + rsDetail.Nominal + "/1000000)*(ROUND((1000000*" + rsDetail.CouponRate + "*(H" + _SettledDate + "-C" + _lastCoupon + "))/((C" + _nextCoupon + "-C" + _lastCoupon + ")*2),0))";
                                                worksheet.Cells[incRowExcel, 3].Calculate();
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6].Value = "Days";
                                                worksheet.Cells[incRowExcel, 7].Value = ":";
                                                worksheet.Cells[incRowExcel, 8].Formula = "=H" + _SettledDate + "-C" + _lastCoupon;
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _accruedInterest = incRowExcel;

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Tax On Accrued Interest";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Formula = "=ROUND((-C" + _accruedInterest + "*5%),2)";
                                                worksheet.Cells[incRowExcel, 3].Calculate();
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _taxOnAccrued = incRowExcel;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Tax On Capital Gain ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Formula = "=ROUND((-" + rsDetail.Price + "+" + rsDetail.AcqPrice + ")*(" + rsDetail.Nominal + ")*(5%),2)";
                                                worksheet.Cells[incRowExcel, 3].Calculate();
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _TaxOnCapital = incRowExcel;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Total Nett Proceed";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Formula = "=" + rsDetail.Proceed + "+C" + _accruedInterest + "+C" + _taxOnAccrued + "+C" + _TaxOnCapital;

                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Prepare By : ";
                                                worksheet.Cells[incRowExcel, 5].Value = "Approve By : ";
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 1].Value = "Authorized Person";
                                                worksheet.Cells[incRowExcel, 5].Value = "Authorized Person";


                                                worksheet.Row(incRowExcel).PageBreak = true;
                                            }

                                        }





                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                        worksheet.Column(1).Width = 23;
                                        worksheet.Column(2).Width = 2;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 10;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 2;
                                        worksheet.Column(8).Width = 16;
                                        worksheet.Column(9).Width = 14;
                                        worksheet.Column(10).Width = 12;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Daily Transaction Form Bonds Dealing Bonds";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        Image thumb = img.GetThumbnailImage(175, 100, null, IntPtr.Zero);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        //worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        //worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        //Corp_Bonds
                                        worksheet = package.Workbook.Worksheets.Add("Corp_Bond");
                                        DbCon.Close();
                                        DbCon.Open();
                                        using (SqlCommand cmd1 = DbCon.CreateCommand())
                                        {


                                            cmd1.CommandText = @"
                                             select A.ValueDate Date,
	  --RefNo,
      D.BankAccountName Untuk,
	  D.Attn1 Attention,
	  D.Phone1 Phone,
	  D.Fax1 Fax,
	  B.ID FundID,
	  A.InstructionDate TradeDate,
	  A.SettlementDate SettleDate,
	  A.AcqDate AcqDate,
	  A.AcqPrice AcqPrice,
	  A.TrxTypeID TrxType,
	  C.Name Bonds,
	  C.ID BondsCode,
	  C.ISIN ISIN,
	  C.MaturityDate MaturityDate,
	  A.InterestPercent CouponRate,
	 isnull(A.LastCouponDate,'') LastCouponDate,
	  isnull(A.NextCouponDate,'') NextCouponDate,
	  A.DoneVolume Nominal,
	  A.OrderPrice Price,
	  A.Amount Proceed

From Investment A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) --BankCustody
left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2)
left join BankBranch D on B.BankBranchPK = D.BankBranchPK and D.status in(1,2)
left join Bank E on D.BankPK = E.BankPK and E.Status in (1,2)
Where A.ValueDate between @ValueDateFrom and @ValueDateTo and A.StatusInvestment = 2 and A.InstrumentTypePK in (3)

                                            ";

                                            cmd1.CommandTimeout = 0;
                                            cmd1.Parameters.AddWithValue("@ValuedateFrom", _FundAccountingRpt.ValueDateFrom);
                                            cmd1.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);

                                            using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                            {
                                                if (!dr1.HasRows)
                                                {
                                                    return false;
                                                }

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<DailyTransactionFormBonds> rList1 = new List<DailyTransactionFormBonds>();
                                                while (dr1.Read())
                                                {

                                                    DailyTransactionFormBonds rSingle1 = new DailyTransactionFormBonds();
                                                    rSingle1.Date = Convert.ToDateTime(dr1["Date"]);
                                                    //rSingle1.RefNo = Convert.ToString(dr1["RefNo"]);
                                                    rSingle1.Untuk = Convert.ToString(dr1["Untuk"]);
                                                    rSingle1.Attention = Convert.ToString(dr1["Attention"]);
                                                    rSingle1.Phone = Convert.ToString(dr1["Phone"]);
                                                    rSingle1.Fax = Convert.ToString(dr1["Fax"]);
                                                    rSingle1.FundID = Convert.ToString(dr1["FundID"]);
                                                    rSingle1.TradeDate = Convert.ToDateTime(dr1["TradeDate"]);
                                                    rSingle1.SettleDate = Convert.ToDateTime(dr1["SettleDate"]);
                                                    rSingle1.AcqDate = Convert.ToDateTime(dr1["AcqDate"]);
                                                    rSingle1.AcqPrice = Convert.ToDecimal(dr1["AcqPrice"]);
                                                    rSingle1.TrxType = Convert.ToString(dr1["TrxType"]);
                                                    rSingle1.Bonds = Convert.ToString(dr1["Bonds"]);
                                                    rSingle1.BondsCode = Convert.ToString(dr1["BondsCode"]);
                                                    rSingle1.ISIN = Convert.ToString(dr1["ISIN"]);
                                                    rSingle1.MaturityDate = Convert.ToDateTime(dr1["MaturityDate"]);
                                                    rSingle1.CouponRate = Convert.ToDecimal(dr1["CouponRate"]);
                                                    rSingle1.LastCouponDate = Convert.ToDateTime(dr1["LastCouponDate"]);
                                                    rSingle1.NextCouponDate = Convert.ToDateTime(dr1["NextCouponDate"]);
                                                    rSingle1.Nominal = Convert.ToDecimal(dr1["Nominal"]);
                                                    rSingle1.Price = Convert.ToDecimal(dr1["Price"]);
                                                    rSingle1.Proceed = Convert.ToDecimal(dr1["Proceed"]);
                                                    //rSingle1.AccruedInterest = Convert.ToDecimal(dr1["AccruedInterest"]);
                                                    //rSingle1.DaysCount = Convert.ToString(dr1["DaysCount"]);
                                                    //rSingle1.TaxAccruedInterest = Convert.ToDecimal(dr1["TaxAccruedInterest"]);
                                                    //rSingle1.TaxCapitalGain = Convert.ToDecimal(dr1["TaxCapitalGain"]);




                                                    rList1.Add(rSingle1);

                                                }



                                                var GroupByReference1 =
                                                from r in rList1
                                                    //orderby r.FundName ascending
                                                group r by new { } into rGroup
                                                select rGroup;

                                                incRowExcel = 2;


                                                foreach (var rsHeader1 in GroupByReference1)
                                                {
                                                    int _startRowDetail = incRowExcel;
                                                    int _endRowDetail = 0;





                                                    //end area header
                                                    foreach (var rsDetail1 in rsHeader1)
                                                    {
                                                        worksheet.Row(incRowExcel).Height = 30;
                                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                        worksheet.Cells[incRowExcel, 4].Value = "DAILY TRANSACTION FORM";
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 4].Style.Font.Size = 28;
                                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 8].Merge = true;
                                                        incRowExcel = incRowExcel + 3;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Date";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Date;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[incRowExcel, 6].Value = "FundID";
                                                        worksheet.Cells[incRowExcel, 7].Value = ":";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.FundID;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Ref Number";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.RefNo;
                                                        worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                                        worksheet.Cells[incRowExcel, 7].Value = ":";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.TradeDate;
                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "To";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Untuk;
                                                        worksheet.Cells[incRowExcel, 6].Value = "Settle Date";
                                                        worksheet.Cells[incRowExcel, 7].Value = ":";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.SettleDate;
                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        _SettledDate = incRowExcel;

                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Attention;
                                                        worksheet.Cells[incRowExcel, 6].Value = "Acquisition Date";
                                                        worksheet.Cells[incRowExcel, 7].Value = ":";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.AcqDate;
                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Ph Number";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Phone;
                                                        worksheet.Cells[incRowExcel, 6].Value = "Acquisition Price";
                                                        worksheet.Cells[incRowExcel, 7].Value = ":";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.AcqPrice;
                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Fax Number";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Fax;
                                                        worksheet.Cells[incRowExcel, 6].Value = "Transaction Type";
                                                        worksheet.Cells[incRowExcel, 7].Value = ":";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.TrxType;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Bonds";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Bonds;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "BondsCode";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.BondsCode;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "ISIN";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.ISIN;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "MaturityDate";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.MaturityDate;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        _maturity = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Coupon Rate";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.CouponRate;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Last Coupon Date";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.LastCouponDate;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        _lastCoupon = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Next Coupon Date";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.NextCouponDate;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        _nextCoupon = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Nominal";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Nominal;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Price";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Price;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Proceeds";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Proceed;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Accrued Interest";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Formula = "=(" + rsDetail1.Nominal + "/1000000)*(ROUND((1000000*" + rsDetail1.CouponRate + "*(H" + _SettledDate + "-C" + _lastCoupon + "))/((C" + _nextCoupon + "-C" + _lastCoupon + ")*2),0))";
                                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 6].Value = "Days";
                                                        worksheet.Cells[incRowExcel, 7].Value = ":";
                                                        worksheet.Cells[incRowExcel, 8].Formula = "=H" + _SettledDate + "-C" + _lastCoupon;
                                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        _accruedInterest = incRowExcel;

                                                        incRowExcel++;
                                                        worksheet.Cells[incRowExcel, 1].Value = "Tax On Accrued Interest";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Formula = "=ROUND((-C" + _accruedInterest + "*5%),2)";
                                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        _taxOnAccrued = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Tax On Capital Gain ";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Formula = "=ROUND((-" + rsDetail1.Price + "+" + rsDetail1.AcqPrice + ")*(" + rsDetail1.Nominal + ")*(5%),2)";
                                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        _TaxOnCapital = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = "Total Nett Proceed";
                                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                                        worksheet.Cells[incRowExcel, 3].Formula = "=" + rsDetail1.Proceed + "+C" + _accruedInterest + "+C" + _taxOnAccrued + "+C" + _TaxOnCapital;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel++;

                                                        incRowExcel = incRowExcel + 2;
                                                        worksheet.Cells[incRowExcel, 1].Value = "Prepare By : ";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Approve By : ";
                                                        incRowExcel = incRowExcel + 5;
                                                        worksheet.Cells[incRowExcel, 1].Value = "Authorized Person";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Authorized Person";





                                                        worksheet.Row(incRowExcel).PageBreak = true;
                                                        incRowExcel = incRowExcel + 2;




                                                    }


                                                }


                                                //string _rangeDetail = "A:E";

                                                //using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                //{
                                                //    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                //    r.Style.Font.Size = 11;
                                                //    r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                //}

                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN

                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 0;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                                worksheet.Column(1).Width = 15;
                                                worksheet.Column(2).Width = 2;
                                                worksheet.Column(3).Width = 40;
                                                worksheet.Column(4).Width = 10;
                                                worksheet.Column(5).Width = 15;
                                                worksheet.Column(6).Width = 20;
                                                worksheet.Column(7).Width = 2;
                                                worksheet.Column(8).Width = 16;
                                                worksheet.Column(9).Width = 14;
                                                worksheet.Column(10).Width = 12;


                                                //worksheet.Column(10).AutoFit();
                                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                //worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Daily Transaction Form Bonds Dealing Bonds";

                                                //Image img = Image.FromFile(Tools.ReportImage);
                                                //Image thumb = img.GetThumbnailImage(175, 100, null, IntPtr.Zero);
                                                worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                                //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                                //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                                //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                                //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();




                                            }
                                        }

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

            #region Daily Transaction Form Deposito
            if (_FundAccountingRpt.ReportName.Equals("Daily Transaction Form Deposito"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramTrxType = "";
                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And B.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (_FundAccountingRpt.TrxTypeDeposito == "1")
                            {
                                _paramTrxType = "and A.TrxType in (1) ";
                            }
                            else if (_FundAccountingRpt.TrxTypeDeposito == "2")
                            {
                                _paramTrxType = "and A.TrxType in (2) ";
                            }
                            else if (_FundAccountingRpt.TrxTypeDeposito == "3")
                            {
                                _paramTrxType = "and A.TrxType in (3) ";
                            }
                            else
                            {
                                _paramTrxType = "";
                            }

                            cmd.CommandText = @"
select  E.Name BankCc,
		D.Attn1 AttentionCc,
		D.Fax1 FaxCc,
        D.Phone1 TelpCc,
		isnull(I.Name,'') BankTo,
		isnull(H.Attn1,'') AttentionTo,
		isnull(H.Fax1,'') FaxTo,
        isnull(H.Phone1 ,'')TelpTo,
		B.Name FundName,
		D.BankAccountNo ACNo,
		A.InstructionDate InstructionDate,
		A.Reference RefNo,
		E.Name BankName,
		E.BICode RTGSCode,
		D.BankAccountName TargetAccName,
		D.BankAccountNo AccountNo,
		A.DoneAmount Amount,
		A.InterestPercent InterestRate,
		A.ValueDate ValueDate,
		isnull(DATEDIFF(day,A.ValueDate,A.MaturityDate),0) Tenor,
		isnull(A.MaturityDate,'') MaturityDate,
		DATEDIFF(day,  cast(cast(year(@ValueDateFrom) as char(4)) as date),  cast(cast(year(@ValueDateFrom)+1 as char(4)) as date)) DaysCount,
		A.TrxType TrxType

 from Investment A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) --BankCustody
left join BankBranch D on B.BankBranchPK = D.BankBranchPK and D.status in(1,2)
left join Bank E on D.BankPK = E.BankPK and E.Status in (1,2)



left join Counterpart F on A.CounterpartPK = F.CounterpartPK and F.Status in (1,2)
left join Instrument G on A.InstrumentPK = G.InstrumentPK and G.Status in (1,2)


left join BankBranch H on A.BankBranchPK = H.BankBranchPK and H.status in(1,2) --Investment
left join Bank I on H.BankPK = I.BankPK and I.Status in (1,2) 

Where A.ValueDate between @ValueDateFrom and @ValueDateTo " + _paramFund + _paramTrxType + @" and A.StatusInvestment = 2 

                            ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValuedateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyTransactionFormDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionFormDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "DailyTransactionFormDeposito";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Form Deposito Dealing Saham");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyTransactionFormDeposito> rList = new List<DailyTransactionFormDeposito>();
                                        while (dr0.Read())
                                        {

                                            DailyTransactionFormDeposito rSingle = new DailyTransactionFormDeposito();
                                            rSingle.BankTo = Convert.ToString(dr0["BankTo"]);
                                            rSingle.AttentionTo = Convert.ToString(dr0["AttentionTo"]);
                                            rSingle.FaxTo = Convert.ToString(dr0["FaxTo"]);
                                            rSingle.TelpTo = Convert.ToString(dr0["TelpTo"]);
                                            rSingle.BankCc = Convert.ToString(dr0["BankCc"]);
                                            rSingle.AttentionCc = Convert.ToString(dr0["AttentionCc"]);

                                            rSingle.TelpCc = Convert.ToString(dr0["TelpCc"]);
                                            rSingle.FaxCc = Convert.ToString(dr0["FaxCc"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.ACNo = Convert.ToString(dr0["ACNo"]);
                                            rSingle.RefNo = Convert.ToString(dr0["RefNo"]);
                                            rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);


                                            rSingle.BankName = Convert.ToString(dr0["BankName"]);
                                            rSingle.RTGSCode = Convert.ToString(dr0["RTGSCode"]);
                                            rSingle.TargetAccName = Convert.ToString(dr0["TargetAccName"]);
                                            rSingle.AccountNo = Convert.ToString(dr0["AccountNo"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.InterestRate = Convert.ToDecimal(dr0["InterestRate"]);



                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.Tenor = Convert.ToString(dr0["Tenor"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            rSingle.DaysCount = Convert.ToString(dr0["DaysCount"]);
                                            rSingle.TrxType = Convert.ToString(dr0["TrxType"]);





                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r.FundName, r.TrxType ascending
                                                group r by new { } into rGroup
                                                select rGroup;

                                        int incRowExcel = 2;

                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            //incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int no_ = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "DAILY TRANSACTION FORM";
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 28;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Merge = true;
                                                incRowExcel = incRowExcel + 3;




                                                worksheet.Cells[incRowExcel, 1].Value = "TO";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankTo;

                                                worksheet.Cells["A" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AttentionTo;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 3].Value = "Fax : " + rsDetail.FaxTo;
                                                worksheet.Cells[incRowExcel, 5].Value = "Telp : " + rsDetail.TelpTo;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Cc";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCc;

                                                worksheet.Cells["A" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AttentionCc;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 3].Value = "Fax : " + rsDetail.FaxCc;
                                                worksheet.Cells[incRowExcel, 5].Value = "Telp : " + rsDetail.TelpCc;

                                                worksheet.Cells["A" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "From";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 5].Value = "A/C No.";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.AccountNo;

                                                worksheet.Cells["A" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Date";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstructionDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 5].Value = "Ref No";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.RefNo;


                                                worksheet.Cells["A" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "PLEASE EXECUTE THE FOLLOWING INSTRUCTION";
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "BankName";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankName;
                                                worksheet.Cells[incRowExcel, 4].Value = "RTGS Code";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Merge = true;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.RTGSCode;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 7].Merge = true;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 7].Merge = true;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Target Acc.Name ";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.TargetAccName;
                                                worksheet.Cells[incRowExcel, 4].Value = "Account No.";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 6].Merge = true;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.AccountNo;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Amount (Rp)";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = "Interest Rate";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Merge = true;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestRate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 4].Value = "Interest Amount (Rp)";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Merge = true;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Formula = "=(" + rsDetail.Amount + "*" + rsDetail.InterestRate + "*" + rsDetail.Tenor + "* 0.8) /" + rsDetail.DaysCount;
                                                worksheet.Cells[incRowExcel, 6].Calculate();
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                incRowExcel = incRowExcel + 2;


                                                worksheet.Cells[incRowExcel, 1].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 4].Value = "Tenor";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Merge = true;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Tenor;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.BitBreakable;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 4].Value = "# of Days";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Merge = true;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.DaysCount;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                incRowExcel = incRowExcel + 2;

                                                if (rsDetail.TrxType == "1")
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = "X";
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 3].Value = "New Placement";
                                                    worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 4].Value = "";
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Roll Over";

                                                    worksheet.Cells[incRowExcel, 6].Value = "";
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 7].Value = "Liquidate";
                                                }

                                                else if (rsDetail.TrxType == "3")
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = "";
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 3].Value = "New Placement";

                                                    worksheet.Cells[incRowExcel, 4].Value = "X";
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Roll Over";
                                                    worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 6].Value = "";
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 7].Value = "Liquidate";
                                                }
                                                else if (rsDetail.TrxType == "2")
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = "";
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 3].Value = "New Placement";

                                                    worksheet.Cells[incRowExcel, 4].Value = "";
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Roll Over";

                                                    worksheet.Cells[incRowExcel, 6].Value = "X";
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 7].Value = "Liquidate";
                                                    worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                }



                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Upon  Maturity, if there is no further instruction, please transfer the Principal plus interest to :";
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Bank Name";
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.BankTo;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 4].Merge = true;

                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Account Name";
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 4].Merge = true;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "Account No";
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ACNo;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 4].Merge = true;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["B" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["D" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel = incRowExcel + 3;

                                                worksheet.Cells[incRowExcel, 1].Value = "Please kindly confirm and sign this instruction for confirmation and fax it back to Custodian Bank, Jakarta Branch immediately";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "At Fax No. #";

                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Prepare By :";
                                                worksheet.Cells[incRowExcel, 3].Value = "Approval By :";

                                                incRowExcel = incRowExcel + 5;

                                                worksheet.Cells[incRowExcel, 1].Value = "Authorized Person";
                                                worksheet.Cells[incRowExcel, 3].Value = "Authorized Person :";



                                                _endRowDetail = incRowExcel;
                                                no_++;
                                                incRowExcel++;

                                                worksheet.Row(incRowExcel).PageBreak = true;
                                            }

                                        }





                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 7];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 10;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 14;
                                        worksheet.Column(8).Width = 16;
                                        worksheet.Column(9).Width = 14;
                                        worksheet.Column(10).Width = 12;
                                        worksheet.Column(11).Width = 14;
                                        worksheet.Column(12).Width = 12;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Daily Transaction Form Deposito Dealing Deposito";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        Image thumb = img.GetThumbnailImage(175, 100, null, IntPtr.Zero);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        //worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                        //worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        // worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

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







            #region Fund Portfolio
            if (_FundAccountingRpt.ReportName.Equals("Fund Portfolio"))
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
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText =
                                @"
                                select FP.FundPK , FP.AvgPrice AvgPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance Balance,FP.CostValue CostValue,  
FP.ClosePrice ClosePrice
,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
,Case when IT.Type =3 and @ValueDate > FP.AcqDate then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) 
else 0 end AccrualHarian
,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate)
* datediff(day,DATEADD(month, DATEDIFF(month, 0, @ValueDate), 0),@ValueDate )
else FP.InterestPercent end Accrual
,FP.InterestPercent 
,FP.MarketValue MarketValue,
sum(FP.MarketValue - FP.CostValue)Unrealised,case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum((FP.MarketValue / 
case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav , sum(FP.Balance/100) Lot ,case when I.InstrumentTypePK not in (5,6) then  sum((FP.MarketValue - FP.CostValue))/FP.CostValue * 100 else 0 end PercentFR 

,Case when IT.Type =3 and @ValueDate > FP.AcqDate then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) * I.TaxExpensePercent/100
else 0 end TaxAccInterest

,Case when IT.Type = 5 then  FP.Balance * FP.InterestPercent/100 / (Datediff(day,dbo.FgetNextCouponDate(@ValueDate,FP.InstrumentPK),dbo.FgetLastCouponDate(@ValueDate,FP.InstrumentPK))*2)*-1 
else Case when IT.Type = 2 then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance)	
else dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) end end DailyAccInterestBond 

,Case when IT.Type not in (1,3,4,12) then  dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) * I.TaxExpensePercent/100
else 0 end TaxAccInterestBond
,O.SInvestID,O.Name BankName,N.ID BranchID,FP.AcqDate

,Case when IT.Type = 5 then  ((Datediff(day,dbo.FgetLastCouponDate(@ValueDate,FP.InstrumentPK),@ValueDate)) * FP.Balance * FP.InterestPercent/100 / (Datediff(day,dbo.FgetNextCouponDate(@ValueDate,FP.InstrumentPK),dbo.FgetLastCouponDate(@ValueDate,FP.InstrumentPK))*2)*-1) - (dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) * I.TaxExpensePercent/100)  
else Case when IT.Type = 2 then  ((Datediff(day,dbo.FgetLastCouponDate(@ValueDate,FP.InstrumentPK),@ValueDate)) * dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance)) - (dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) * I.TaxExpensePercent/100) 
else ((Datediff(day,dbo.FgetLastCouponDate(@ValueDate,FP.InstrumentPK),@ValueDate)) * dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance)) - (dbo.[FGetBondInterestAccrued](@ValueDate,FP.InstrumentPK,Balance) * I.TaxExpensePercent/100)   end end AccInterestBond

,isnull(FP.MarketValue,0) / isnull(CN.AUM,0) * 100 PercentOfAUM

,Case when IT.Type =3 and @ValueDate > FP.AcqDate then  (Datediff(day,FP.AcqDate,@ValueDate)) * dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) - (dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) * I.TaxExpensePercent/100)
else 0 end AccrualInterestDeposito
from fundposition FP   
left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status in (1,2)   
left join Fund F on FP.FundPK = F.FundPK and F.status in (1,2)   
left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status in (1,2)  
left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status in (1,2)  
left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType'
left join BankBranch N on FP.BankBranchPK = N.BankBranchPK and N.status in (1,2)
left join Bank O on N.BankPK = O.BankPK and O.status in (1,2)
where FP.status in (1,2)  and FP.Date = @ValueDate  " + _paramFund + @"
group by FP.FundPK,Fp.AvgPrice,FP.Date,I.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,O.SInvestID,O.Name,N.ID,FP.AcqDate,I.TaxExpensePercent
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
                                    string filePath = Tools.ReportsPath + "FundPortfolio" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "FundPortfolio" + "_" + _userID + ".pdf";
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
                                        List<PortfolioValuationReport> rList = new List<PortfolioValuationReport>();
                                        while (dr0.Read())
                                        {
                                            PortfolioValuationReport rSingle = new PortfolioValuationReport();
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
                                            rSingle.AccrualInterestDeposito = Convert.ToDecimal(dr0["AccrualInterestDeposito"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccrualInterestDeposito"]));
                                            rSingle.PercentOfAUM = Convert.ToDecimal(dr0["PercentOfAUM"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentOfAUM"]));
                                            rSingle.DailyAccInterestBond = Convert.ToDecimal(dr0["DailyAccInterestBond"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DailyAccInterestBond"]));
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.FundName, r.InstrumentTypePK, r.Date ascending
                                         group r by new { r.Fund, r.FundName, r.InstrumentTypeName, r.Date, r.InstrumentTypePK } into rGroup
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
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 1].Value = "No";
                                                worksheet.Cells[incRowExcel, 2].Value = "TIME DEPOSIT";
                                                worksheet.Cells[incRowExcel, 3].Value = "BI CODE";
                                                worksheet.Cells[incRowExcel, 4].Value = "BRANCH";
                                                worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 8].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 9].Value = "Tax Accrued Interest";
                                                worksheet.Cells[incRowExcel, 10].Value = "Rate (Gross)";
                                                worksheet.Cells[incRowExcel, 11].Value = "Acc Int.TD (Net)";
                                                worksheet.Cells[incRowExcel, 12].Value = "%AUM";

                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            }

                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 5].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Tax Accrued Interest";
                                                worksheet.Cells[incRowExcel, 7].Value = "Daily Accrued Interest";
                                                worksheet.Cells[incRowExcel, 8].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 9].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 10].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 11].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 12].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 13].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 14].Value = "%AUM";

                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;




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

                                                else if (rsHeader.Key.InstrumentTypePK == 5) //Deposito
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

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.AccrualInterestDeposito;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.TaxExpensePercent;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.RateGross;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.AccIntTD;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.PercentOfAUM;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
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


                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.DailyAccInterestBond;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.AccInterestBond;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.PercentOfAUM;
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
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
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();


                                            }
                                            else if (rsHeader.Key.InstrumentTypePK == 5) //Deposito
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 5].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();

                                            }
                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 12].Calculate();
                                                worksheet.Cells[incRowExcel, 13].Calculate();
                                                worksheet.Cells[incRowExcel, 14].Calculate();
                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                            incRowExcel++;

                                        }




                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 14];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 30;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 22;
                                        worksheet.Column(10).Width = 22;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 25;
                                        worksheet.Column(14).Width = 10;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n \n \n &12 PORTFOLIO VALUATION REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
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

            #region Coupon Bonds
            if (_FundAccountingRpt.ReportName.Equals("Coupon Bonds"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramFund = "";
                            string _paramFundClient = "";
                            string _paramAgent = "";


                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText =
                            //-- Query butuh bantuan ini masih ngasal
                            @"
                            if object_id('tempdb..#TempReport', 'u') is not null drop table #TempReport 
                            create table #TempReport
                            (
	                            InstrumentPK int,
	                            InstrumentName nvarchar(500),
	                            FundPK int,
	                            FundName nvarchar(500),
	                            BondsCode nvarchar(50),
	                            InterestPercent numeric(22,4),
	                            ValueDate date,
	                            LastCouponDate date,
	                            NextCouponDate date,
	                            InstrumentTypePK int,
	                            Nominal numeric(22,4),
	                            AccruedDays int,
	                            AccruedCoupon numeric(22,4),
	                            TaxAccruedCoupon numeric(22,4)
                            )

                            CREATE CLUSTERED INDEX indx_TempReport ON #TempReport (FundPK,InstrumentPK,InstrumentTypePK);

                            if object_id('tempdb..#tempScheduler', 'u') is not null drop table #tempScheduler                       
                            create table #tempScheduler
                            (
	                            [AutoNo] [int] NOT NULL identity,
	                            [InstrumentPK] [int] NOT NULL,
	                            [CouponFromDate] [date] NOT NULL,
	                            [CouponToDate] [date] NOT NULL
                            )

                            CREATE CLUSTERED INDEX indx_tempScheduler ON #tempScheduler ([InstrumentPK],[AutoNo]);

                            DECLARE 
                                @InstrumentPK int,
	                            @CouponFrom date,
	                            @CouponTo date,
	                            @CouponFromDate date,
	                            @CouponToDate date,
	                            @CouponRate numeric(8,4),
	                            @CouponFrequently int,
	                            @endDate date,
	                            @startDate date

 
                            DECLARE A CURSOR
                            FOR 
	                            SELECT InstrumentPK from Instrument where status in (1,2) and InstrumentTypePK in (2,3,8,9,12,13,14,15) and FirstCouponDate is not null and MaturityDate is not null
 
                            OPEN A;
 
                            FETCH NEXT FROM A INTO 
                                @InstrumentPK
 
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
                                    if exists (select * from Instrument where InstrumentPK = @InstrumentPK and [Status] in (1,2))
		                            begin
			
			                            select
				                            @CouponFrequently	= InterestPaymentType,
				                            @CouponFromDate		= FirstCouponDate,
				                            @CouponToDate		= MaturityDate,
				                            @CouponRate			= InterestPercent
			                            from Instrument
			                            where InstrumentPK = @InstrumentPK and [Status] in (1,2)

			                            set @CouponFrom = @CouponFromDate
			                            set @CouponTo = @CouponToDate

			                            while @CouponFromDate < @CouponToDate
			                            begin

				                            if @CouponFrequently in (7,8,9) --> Monthly / Per 1 Bulan Sekali
				                            begin
					                            set @endDate = dateadd(month, 1, @CouponFromDate)
					                            if @endDate >= @ValueDateTo
						                            break
					                            set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end
					                            set @CouponFrom = @CouponFromDate
					                            set @CouponTo = @endDate
					
				                            end

				                            if @CouponFrequently in (10,11,12,13,14,15) --> Quarterly / Per 3 Bulan Sekali
				                            begin
					
					                            set @endDate = dateadd(month, 3, @CouponFromDate)
					                            if @endDate >= @ValueDateTo
						                            break
					                            set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end
					                            set @CouponFrom = @CouponFromDate
					                            set @CouponTo = @endDate
				                            end

				                            if @CouponFrequently in (16,17,18) --> Semi Annual / Per 6 Bulan Sekali
				                            begin
					                            set @endDate = dateadd(month, 6, @CouponFromDate)
					                            if @endDate >= @ValueDateTo
						                            break
					                            set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end
					                            set @CouponFrom = @CouponFromDate
					                            set @CouponTo = @endDate
					
				                            end

				                            if @CouponFrequently  in (19,20,21) --> Yearly / Per 1 Tahun Sekali
				                            begin
					                            set @endDate = dateadd(year, 1, @CouponFromDate)
					                            if @endDate >= @ValueDateTo
						                            break
					                            set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end
					                            set @CouponFrom = @CouponFromDate
					                            set @CouponTo = @endDate
					
				                            end

				                            set @CouponFromDate = @endDate
			                            end

		                            end

		                            if @CouponFrom is not null and @CouponTo is not null and @CouponTo between @ValuedateFrom and @ValueDateTo
		                            insert into #tempScheduler(InstrumentPK,CouponFromDate,CouponToDate)
		                            select @InstrumentPK,@CouponFrom,@CouponTo


		                            FETCH NEXT FROM A INTO @InstrumentPK
                                END;
 
                            CLOSE A;
 
                            DEALLOCATE A;

                            insert into #TempReport (InstrumentPK,InstrumentName,BondsCode,InterestPercent,LastCouponDate,NextCouponDate,InstrumentTypePK,FundPK,FundName)
                            select A.InstrumentPK,B.Name InstrumentName,B.ID BondsCode,B.InterestPercent,CouponFromDate,CouponToDate,B.InstrumentTypePK,C.FundPK,D.Name from #tempScheduler A
                            inner join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
                            inner join FundPosition C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            inner join Fund D on C.FundPK = D.FundPK and D.Status in (1,2)
                            where  B.InstrumentTypePK in (2,3,8,9,12,13,14,15) 
                            --param Fund
                            --And C.FundPK in (@FundPK) 
                            " + _paramFund + @"
                            group by A.InstrumentPK,B.Name,B.ID,B.InterestPercent,B.InstrumentTypePK,C.FundPK,D.Name,CouponFromDate,CouponToDate

                            update A set A.ValueDate = B.valuedate,A.Nominal = Balance from #TempReport A 
                            inner join (
                            select max(AcqDate) valuedate,A.InstrumentPK,A.FundPK from FundPosition A
                            inner join #TempReport B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK
                            where status in (1, 2)
                            group by A.InstrumentPK,A.FundPK
                            )B on A.InstrumentPK = B.InstrumentPK and A.FundPK = B.FundPK
                            inner join FundPosition C on A.InstrumentPK = C.InstrumentPK and A.FundPK = C.FundPK and B.valuedate = C.AcqDate and C.Status in (1, 2)

                            update A set AccruedDays = case when InstrumentTypePK = 3 then 
                            case when valuedate <= lastcoupondate then datediff(d, lastcoupondate, nextcoupondate) 
                            else datediff(d, lastcoupondate, valuedate) end 
                            else case when valuedate <= lastcoupondate then 
                            case when datediff(d, lastcoupondate, nextcoupondate) > 30
                            then
                            datediff(m, lastcoupondate, nextcoupondate) *30 
                            else
                            datediff(d, lastcoupondate, nextcoupondate)
                            end

                            else datediff(d, lastcoupondate, valuedate) end end
                            from #TempReport A

                            update A set AccruedCoupon = case when InstrumentTypePK = 3 then 
                            case when DATEDIFF(d, lastcoupondate, nextcoupondate) != 0 then (Nominal * InterestPercent / 100 * AccruedDays / ((DATEDIFF(d, lastcoupondate, nextcoupondate) * 2))) 
                            else 0 end else (Nominal * InterestPercent / 100 * AccruedDays / 360) end
                            from #TempReport A

                            update A set TaxAccruedCoupon = AccruedCoupon * 0.05
                            from #TempReport A

                            select FundName,BondsCode,InstrumentName,InterestPercent,ValueDate,LastCouponDate,NextCouponDate,Nominal,AccruedDays,AccruedCoupon,TaxAccruedCoupon from #TempReport
                            order by LastCouponDate

";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValuedateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "CouponBonds" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CouponBonds" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAdminReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Coupon Bonds");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<CouponBonds> rList = new List<CouponBonds>();
                                        while (dr0.Read())
                                        {
                                            CouponBonds rSingle = new CouponBonds();
                                            rSingle.FundName = dr0["FundName"].ToString();
                                            rSingle.Date = dr0["ValueDate"].ToString();
                                            rSingle.AcqDate = dr0["ValueDate"].ToString();
                                            rSingle.BondsCode = dr0["BondsCode"].ToString();
                                            rSingle.Instrument = dr0["InstrumentName"].ToString();
                                            rSingle.Nominal = Convert.ToDecimal(dr0["Nominal"]);
                                            rSingle.CouponOrDividend = dr0["InterestPercent"].ToString();
                                            rSingle.LastPayment = dr0["LastCouponDate"].ToString();
                                            rSingle.NextPayment = dr0["NextCouponDate"].ToString();
                                            rSingle.AccruedDays = Convert.ToDecimal(dr0["AccruedDays"]);
                                            rSingle.AccruedCoupon = Convert.ToDecimal(dr0["AccruedCoupon"]);
                                            rSingle.Proceeds = Convert.ToDecimal(dr0["TaxAccruedCoupon"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         orderby r.FundName ascending
                                         group r by new { r.FundName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 0;
                                        //int _rowEndBalance = 0;

                                        foreach (var rsHeader in GroupByCategory)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "COUPON BONDS PORTO FOLIO";
                                            worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            incRowExcel = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundName;
                                            worksheet.Cells["B" + incRowExcel + ":O" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date";
                                            worksheet.Cells[incRowExcel, 2].Value = "Transaction";
                                            worksheet.Cells[incRowExcel, 3].Value = "Acq Date";
                                            worksheet.Cells[incRowExcel, 4].Value = "Bonds Code";
                                            worksheet.Cells[incRowExcel, 5].Value = "Instruments";
                                            worksheet.Cells[incRowExcel, 6].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 7].Value = "Average Price (%)";
                                            worksheet.Cells[incRowExcel, 8].Value = "Coupon / Dividend (%)";
                                            worksheet.Cells[incRowExcel, 9].Value = "Last Payment";
                                            worksheet.Cells[incRowExcel, 10].Value = "Next Payment";
                                            worksheet.Cells[incRowExcel, 11].Value = "Accrued Days";
                                            worksheet.Cells[incRowExcel, 12].Value = "Accrued Coupon";
                                            worksheet.Cells[incRowExcel, 13].Value = "Proceeds";
                                            worksheet.Cells[incRowExcel, 14].Value = "Nett Proceeds";

                                            //add currency

                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Font.Bold = true;
                                            //incRowExcel++;

                                            //worksheet.Row(incRowExcel).Height = 5;
                                            //worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            //worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = incRowExcel;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.AcqDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "Coupont Payment";
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsDetail.AcqDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.BondsCode;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Instrument;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Nominal;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 7].Value = "";
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CouponOrDividend;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 9].Value = Convert.ToDateTime(rsDetail.LastPayment).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = Convert.ToDateTime(rsDetail.NextPayment).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.AccruedDays;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.AccruedCoupon;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.AccruedCoupon;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.AccruedCoupon - rsDetail.Proceeds;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";

                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.AcqDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "Tax Coupont Payment (5%)";
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsDetail.AcqDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.BondsCode;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Instrument;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Nominal;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 7].Value = "-5.000";
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CouponOrDividend;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 9].Value = Convert.ToDateTime(rsDetail.LastPayment).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = Convert.ToDateTime(rsDetail.NextPayment).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.AccruedDays;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.AccruedCoupon;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.Proceeds * -1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 14].Value = "";
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                _endRowDetail = incRowExcel;
                                                incRowExcel = incRowExcel + 2;


                                            }
                                            worksheet.Cells["A" + _startRowDetail + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _startRowDetail + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _startRowDetail + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _startRowDetail + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 13].Value = "TOTAL";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 14].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            int _endRowDetailTotal = incRowExcel;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        }

                                        string _rangeDetail = "A:O";

                                        using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Size = 12;
                                            r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        }



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 14];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 27;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 80;
                                        worksheet.Column(6).Width = 25;
                                        worksheet.Column(7).Width = 25;
                                        worksheet.Column(8).Width = 25;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 15;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 25;
                                        worksheet.Column(14).Width = 25;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 UNIT TRUST REPORT";

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //Image thumb = img.GetThumbnailImage(175, 100, null, IntPtr.Zero);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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

            #region Daily Transaction Report
            if (_FundAccountingRpt.ReportName.Equals("Daily Transaction Report"))
            {
                try
                {
                    int counter = 3;
                    string _rangeDetail;
                    int incRowExcel = 1;
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            //string _paramFundFrom = "";

                            //if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            //{
                            //    _paramFundFrom = "And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            //}
                            //else
                            //{
                            //    _paramFundFrom = "";
                            //}


                            cmd.CommandText = @"
select isnull(D.Name,'') FundName,isnull(B.ID,'') SecurityCode,isnull(B.Name,'') SecurityName,A.ValueDate TradeDate,A.SettlementDate SettlementDate,
isnull(A.TrxTypeID,1) TransactionType,isnull(C.Name,'') Broker,case when A.StatusSettlement = 0 then 'Contact Dealer' when A.StatusSettlement = 1
then 'Pending' when A.StatusSettlement = 2  then 'Approved' when A.StatusSettlement = 3 then 'Reject' end as Status,
isnull(A.DoneVolume,0) Quantity,isnull(A.DoneVolume,0)/100 QuantityinLot,isnull(A.DonePrice,0) PriceShares,
isnull(A.DoneAmount,0) GrossAmount,isnull(A.CommissionAmount,0) BrokerageFee,isnull(A.VATAmount,0) VAT,isnull(A.LevyAmount,0) Levy,
isnull(A.IncomeTaxSellAmount,0) SalesTax,isnull(A.DoneAmount + A.CommissionAmount + A.VATAmount + A.LevyAmount,0) TotalBeforeTax,
isnull(0.02*CommissionAmount,0) TaxBrokerageFee,isnull(A.DoneAmount + A.CommissionAmount + A.VATAmount + A.LevyAmount - (0.02*A.CommissionAmount),0) TotalPayment
 from Investment A
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status in (1,2)
left join Fund D on A.FundPK = D.FundPK and D.Status in (1,2)
left join FundCashRef E on A.FundPK = E.FundPK and E.Status in (1,2)
left join BankBranch F on E.BankBranchPK = F.BankBranchPK and F.Status in (1,2)
left join Bank G on F.BankPK = G.BankPK and G.Status in (1,2)
where  A.ValueDate between @datefrom and @dateto and A.InstrumentTypePK in (1) and A.StatusSettlement in (1,2) order by A.EntrySettlementTime                                
";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {


                                string filePath = Tools.ReportsPath + "DailyTransactionReport" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "DailyTransactionReport" + "_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                                    if (!dr0.HasRows)
                                    {
                                        counter = counter - 1;
                                    }
                                    else
                                    {

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyTransaction> rList = new List<DailyTransaction>();
                                        while (dr0.Read())
                                        {

                                            DailyTransaction rSingle = new DailyTransaction();
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.SecurityCode = Convert.ToString(dr0["SecurityCode"]);
                                            rSingle.SecurityDesc = Convert.ToString(dr0["SecurityName"]);
                                            rSingle.TradeDate = Convert.ToDateTime(dr0["TradeDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.Category = Convert.ToString(dr0["TransactionType"]);
                                            rSingle.Broker = Convert.ToString(dr0["Broker"]);
                                            rSingle.Status = Convert.ToString(dr0["Status"]);
                                            rSingle.TotalShares = Convert.ToDecimal(dr0["Quantity"]);
                                            rSingle.Lot = Convert.ToDecimal(dr0["QuantityinLot"]);
                                            rSingle.PriceShares = Convert.ToDecimal(dr0["PriceShares"]);
                                            rSingle.GrossAmount = Convert.ToDecimal(dr0["GrossAmount"]);
                                            rSingle.BrokerageFee = Convert.ToDecimal(dr0["BrokerageFee"]);
                                            rSingle.VAT = Convert.ToDecimal(dr0["VAT"]);
                                            rSingle.Levy = Convert.ToDecimal(dr0["Levy"]);
                                            rSingle.SalesTax = Convert.ToDecimal(dr0["SalesTax"]);
                                            rSingle.TotalBeforeTax = Convert.ToDecimal(dr0["TotalBeforeTax"]);
                                            rSingle.TaxBrokerageFee = Convert.ToDecimal(dr0["TaxBrokerageFee"]);
                                            rSingle.TotalPayment = Convert.ToDecimal(dr0["TotalPayment"]);

                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                        from r in rList
                                            //orderby r ascending
                                        group r by new { } into rGroup
                                        select rGroup;


                                        worksheet.Cells[incRowExcel, 3].Value = "Date From";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Value = "Date To";
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;



                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Equity";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "Date";
                                        worksheet.Cells[incRowExcel, 3].Value = (_FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";

                                        worksheet.Cells[incRowExcel, 4].Value = (_FundAccountingRpt.ValueDateTo);
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";




                                        foreach (var rsHeader in GroupByReference)
                                        {



                                            incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;






                                            worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Security Code";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "Security Name";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Trade Date";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "SettlementDate";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Category";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Broker";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Status";
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Purchased/Selling Quantity (Total Number Of shares)";
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 10].Value = "Purchased/Selling Quantity (Lot)";
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = "Price Shares";
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "Gross Amount";
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 13].Value = "BrokerageFee";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 14].Value = "VAT";
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 15].Value = "Levy";
                                            worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 16].Value = "Sales Tax";
                                            worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 17].Value = "Total Before Tax";
                                            worksheet.Cells[incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 18].Value = "Tax BrokerageFee";
                                            worksheet.Cells[incRowExcel, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 19].Value = "Total Payment";
                                            worksheet.Cells[incRowExcel, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 19].Style.Font.Bold = true;

                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel++;
                                            //Row B = 3 fungsinya untuk bikin garis
                                            int RowB = incRowExcel;
                                            //int RowG = incRowExcel + 1;
                                            //string _range = "";
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;


                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {



                                                //ThickBox Border HEADER



                                                //incRowExcel++;
                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDesc;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.TradeDate;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.SettlementDate;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Category;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Broker;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Status;
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.TotalShares;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Lot;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.PriceShares;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.GrossAmount;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.BrokerageFee;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.VAT;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.Levy;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.SalesTax;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.TotalBeforeTax;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.TaxBrokerageFee;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 19].Value = rsDetail.TotalPayment;
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;





                                            }




                                        }


                                        _rangeDetail = "A:G";

                                        using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Size = 12;
                                            r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 20];
                                        worksheet.Column(1).AutoFit();
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(4).AutoFit();
                                        worksheet.Column(5).Width = 26;
                                        worksheet.Column(6).Width = 26;
                                        worksheet.Column(7).Width = 26;
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
                                        worksheet.Column(18).AutoFit();
                                        worksheet.Column(19).AutoFit();

                                        //worksheet.Column(10).AutoFit();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Laporan NAV";



                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();
                                    }
                                    //Reksa Dana
                                    DbCon.Close();
                                    DbCon.Open();
                                    using (SqlCommand cmd1 = DbCon.CreateCommand())
                                    {


                                        cmd1.CommandText = @"select isnull(D.Name,'') FundName,isnull(B.ID,'') InstrumentID,ISNULL (B.ISIN,'') ISIN,ISNULL(B.Name,'') SecurityDescription,isnull(A.TrxTypeID,'') BS, A.ValueDate TradeDate,A.SettlementDate ,isnull(A.LastCouponDate,'') LastCouponDate,
							isnull(A.NextCouponDate,'') NextCouponDate,isnull(A.MaturityDate,'') MaturityDate,isnull(A.DoneVolume,0) FaceValue,isnull(A.DonePrice,0) PricePercent,isnull(A.DoneAmount,0) PriceAmount,
							isnull(datediff(d,A.LastCouponDate,A.NextCouponDate),0) AccruedInterestDays,isnull(A.InterestPercent,0) AccruedInterestPercent,isnull(A.DoneAccruedInterest,0) AccruedInterestIDR,
							isnull(A.IncomeTaxGainAmount,0) TaxCapitalGain, isnull(A.IncomeTaxinterestAmount,0) TaxAccruedInt,isnull (A.IncomeTaxGainAmount+A.IncomeTaxinterestAmount,0) TotalTaxAmount ,
							isnull(A.DoneAmount + A.DoneAccruedInterest - A.IncomeTaxGainAmount - A.IncomeTaxinterestAmount,0) TotalPayment,case when IC.InterestPaymentType = 13 then '4' when IC.InterestPaymentType = 16 then '2' else '1' end CouponFrequency
							from Investment A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
                            left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status in (1,2)
                            left join Fund D on A.FundPK = D.FundPK and D.Status in (1,2)
                            left join FundCashRef E on A.FundPK = E.FundPK and E.Status in (1,2)
                            left join MasterValue MV on A.SettlementMode = MV.Code and MV.Status in (1,2) and MV.ID = 'SettlementMode'
							left join Instrument IC on A.InstrumentPK = IC.InstrumentPK and IC.Status in (1,2)
                            where A.ValueDate between @datefrom and @dateto and A.StatusSettlement in (1,2) and A.InstrumentTypePK in (2,3,9,15)
                                                                        ";

                                        cmd1.CommandTimeout = 0;
                                        cmd1.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                                        cmd1.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);

                                        using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                        {
                                            if (!dr1.HasRows)
                                            {
                                                counter = counter - 1;
                                            }
                                            else
                                            {
                                                //ATUR DATA GROUPINGNYA DULU
                                                List<DailyTransaction> rList1 = new List<DailyTransaction>();
                                                while (dr1.Read())
                                                {

                                                    DailyTransaction rSingle1 = new DailyTransaction();
                                                    rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                    rSingle1.Instrument = Convert.ToString(dr1["InstrumentID"]);
                                                    rSingle1.SecurityDesc = Convert.ToString(dr1["ISIN"]);
                                                    //rSingle1.Category = Convert.ToString(dr1["Category"]);
                                                    rSingle1.InstructionDate = Convert.ToDateTime(dr1["TradeDate"]);
                                                    rSingle1.SettlementDate = Convert.ToDateTime(dr1["SettlementDate"]);
                                                    rSingle1.LastCouponDate = Convert.ToDateTime(dr1["LastCouponDate"]);
                                                    rSingle1.NextCouponDate = Convert.ToDateTime(dr1["NextCouponDate"]);
                                                    rSingle1.MaturityDate = Convert.ToDateTime(dr1["MaturityDate"]);
                                                    rSingle1.FaceValue = Convert.ToDecimal(dr1["FaceValue"]);
                                                    rSingle1.PricePercent = Convert.ToDecimal(dr1["PricePercent"]);
                                                    rSingle1.PriceAmount = Convert.ToDecimal(dr1["PriceAmount"]);
                                                    rSingle1.AccruedInterestDays = Convert.ToInt32(dr1["AccruedInterestDays"]);
                                                    rSingle1.AccruedInterestPercent = Convert.ToDecimal(dr1["AccruedInterestPercent"]);
                                                    rSingle1.AccruedInterestAmount = Convert.ToDecimal(dr1["AccruedInterestIDR"]);
                                                    rSingle1.TaxCapitalGain = Convert.ToDecimal(dr1["TaxCapitalGain"]);
                                                    rSingle1.TaxAccruedInt = Convert.ToDecimal(dr1["TaxAccruedInt"]);
                                                    rSingle1.TaxGainInt = Convert.ToDecimal(dr1["TotalTaxAmount"]);
                                                    rSingle1.TotalPayment = Convert.ToDecimal(dr1["TotalPayment"]);
                                                    rSingle1.CouponFrequency = Convert.ToDecimal(dr1["CouponFrequency"]);
                                                    rList1.Add(rSingle1);

                                                }



                                                var GroupByReference1 =
                                                from r in rList1
                                                    //orderby r.FundName ascending
                                                    group r by new { } into rGroup
                                                select rGroup;

                                                incRowExcel++;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 3].Value = "Date From";
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Value = "Date To";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;



                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bonds";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "Date";
                                                worksheet.Cells[incRowExcel, 3].Value = (_FundAccountingRpt.ValueDateFrom);
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";

                                                worksheet.Cells[incRowExcel, 4].Value = (_FundAccountingRpt.ValueDateTo);
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";

                                                foreach (var rsHeader1 in GroupByReference1)
                                                {

                                                    incRowExcel++;



                                                    int RowB = incRowExcel;
                                                    int RowG = incRowExcel + 1;





                                                    worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = "Instrument";
                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = "Security Description";
                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 4].Value = "Category";
                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 5].Value = "Instruction Date";
                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 6].Value = "Settlement Date";
                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 7].Value = "Last Coupon Date";
                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 8].Value = "Next Coupon Date";
                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 9].Value = "Maturity Date";
                                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 10].Value = "Face Value";
                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                    worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                                    worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 11].Value = "Price Percent";
                                                    worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                    worksheet.Cells["K" + RowB + ":L" + RowB].Merge = true;
                                                    worksheet.Cells["K" + RowB + ":K" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[RowG, 11].Value = "%";
                                                    worksheet.Cells["K" + RowG + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[RowG, 12].Value = "IDR";
                                                    worksheet.Cells["L" + RowG + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 13].Value = "Accrued Interest";
                                                    worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                    worksheet.Cells["M" + RowB + ":O" + RowB].Merge = true;
                                                    worksheet.Cells["M" + RowB + ":O" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[RowG, 13].Value = "Days";
                                                    worksheet.Cells["M" + RowG + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[RowG, 14].Value = "Interest Percent";
                                                    worksheet.Cells["N" + RowG + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[RowG, 15].Value = "IDR";
                                                    worksheet.Cells["O" + RowG + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 16].Value = "Tax Capital Gain";
                                                    worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                                    worksheet.Cells["P" + RowB + ":P" + RowG].Merge = true;
                                                    worksheet.Cells["P" + RowB + ":P" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["P" + RowB + ":P" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 17].Value = "Tax Accrued Int";
                                                    worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                                    worksheet.Cells["Q" + RowB + ":Q" + RowG].Merge = true;
                                                    worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 18].Value = "Tax Gain & Int";
                                                    worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                                    worksheet.Cells["R" + RowB + ":R" + RowG].Merge = true;
                                                    worksheet.Cells["R" + RowB + ":R" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["R" + RowB + ":R" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 19].Value = "Total Payment";
                                                    worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                                    worksheet.Cells["S" + RowB + ":S" + RowG].Merge = true;
                                                    worksheet.Cells["S" + RowB + ":S" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["S" + RowB + ":S" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 20].Value = "Coupon Frequency";
                                                    worksheet.Cells[incRowExcel, 20].Style.Font.Bold = true;
                                                    worksheet.Cells["T" + RowB + ":T" + RowG].Merge = true;
                                                    worksheet.Cells["T" + RowB + ":T" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["T" + RowB + ":T" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                                    incRowExcel = incRowExcel + 2;
                                                    int _startRowDetail = incRowExcel;
                                                    int _endRowDetail = 0;
                                                    int _no = 1;


                                                    //end area header
                                                    foreach (var rsDetail1 in rsHeader1)
                                                    {
                                                        //incRowExcel++;
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail1.FundName;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail1.Instrument;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.SecurityDesc;
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail1.Category;
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.InstructionDate;
                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail1.SettlementDate;
                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail1.LastCouponDate;
                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.NextCouponDate;
                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail1.MaturityDate;
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail1.FaceValue;
                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail1.PricePercent;
                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0%";
                                                        worksheet.Cells[incRowExcel, 12].Value = rsDetail1.PriceAmount;
                                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 13].Value = rsDetail1.AccruedInterestDays;
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail1.AccruedInterestPercent;
                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0%";
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail1.AccruedInterestAmount;
                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 16].Value = rsDetail1.TaxCapitalGain;
                                                        worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "##,##0.00";
                                                        worksheet.Cells[incRowExcel, 17].Value = rsDetail1.TaxAccruedInt;
                                                        worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 18].Value = rsDetail1.TaxGainInt;
                                                        worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 19].Value = rsDetail1.TotalPayment;
                                                        worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 20].Value = rsDetail1.CouponFrequency;
                                                        worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0.00";

                                                        worksheet.Cells[incRowExcel, 10, incRowExcel, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                        worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                        _no++;
                                                        _endRowDetail = incRowExcel;
                                                        incRowExcel++;



                                                    }



                                                }


                                                _rangeDetail = "A:E";

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
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 20];
                                                worksheet.Column(1).AutoFit();
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
                                                worksheet.Column(18).AutoFit();
                                                worksheet.Column(19).AutoFit();
                                                worksheet.Column(20).AutoFit();


                                                //worksheet.Column(10).AutoFit();
                                                worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                                                                               // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Laporan NAV";



                                                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                                //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                                worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();


                                            }

                                        }
                                    }

                                    //Khusus Tim Ops


                                    DbCon.Close();
                                    DbCon.Open();
                                    using (SqlCommand cmd2 = DbCon.CreateCommand())
                                    {


                                        cmd2.CommandText = @"
                                       select B.Name FundName,C.Name Instrument, d.Name Bank, E.ID BankBranch,TrxTypeID TransactionType, A.Category, A.DoneVolume Volume, A.ValueDate PlacementDate, A.MaturityDate, F.DescOne InterestDays,A.InterestPercent Interest from investment A
LEFT JOIN FUND B ON A.FUNDPK = B.FundPK AND B.Status IN (1,2)
LEFT JOIN Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.Status IN (1,2)
LEFT JOIN BANK D ON A.BankPK = D.BankPK AND D.Status IN (1,2)
LEFT JOIN BankBranch E ON A.BankBranchPK = E.BankBranchPK AND E.Status IN (1,2)
LEFT JOIN MasterValue F ON A.InterestDaysType = F.Code AND F.ID = 'InterestDaysType' and F.Status in (1,2)
where valuedate between @datefrom and @dateto and StatusSettlement in (1,2) and A.InstrumentTypePK in (5,10)
";

                                        cmd2.CommandTimeout = 0;
                                        cmd2.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                                        cmd2.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);


                                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                        {
                                            if (!dr2.HasRows)
                                            {
                                                counter = counter - 1;
                                            }
                                            else
                                            {
                                                //ATUR DATA GROUPINGNYA DULU
                                                List<DailyTransaction> rList2 = new List<DailyTransaction>();
                                                while (dr2.Read())
                                                {

                                                    DailyTransaction rSingle2 = new DailyTransaction();
                                                    rSingle2.FundName = Convert.ToString(dr2["FundName"]);
                                                    rSingle2.Instrument = Convert.ToString(dr2["Instrument"]);
                                                    rSingle2.Bank = Convert.ToString(dr2["Bank"]);
                                                    rSingle2.BankBranch = Convert.ToString(dr2["BankBranch"]);
                                                    rSingle2.TransactionType = Convert.ToString(dr2["TransactionType"]);
                                                    rSingle2.Category = Convert.ToString(dr2["Category"]);
                                                    rSingle2.Volume = Convert.ToDecimal(dr2["Volume"]);
                                                    rSingle2.PlacementDate = Convert.ToDateTime(dr2["PlacementDate"]);
                                                    rSingle2.MaturityDate = Convert.ToDateTime(dr2["MaturityDate"]);
                                                    rSingle2.InterestDays = Convert.ToString(dr2["InterestDays"]);
                                                    rSingle2.Interest = Convert.ToDecimal(dr2["Interest"]);


                                                    rList2.Add(rSingle2);

                                                }



                                                var GroupByReference2 =
                                                from r in rList2
                                                    //orderby r.FundName ascending
                                                    group r by new { } into rGroup
                                                select rGroup;

                                                incRowExcel++;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 3].Value = "Date From";
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Value = "Date To";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;



                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Deposito";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "Date";
                                                worksheet.Cells[incRowExcel, 3].Value = (_FundAccountingRpt.ValueDateFrom);
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";

                                                worksheet.Cells[incRowExcel, 4].Value = (_FundAccountingRpt.ValueDateTo);
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";

                                                foreach (var rsHeader2 in GroupByReference2)
                                                {



                                                    incRowExcel++;


                                                    int RowB = incRowExcel;
                                                    int RowG = incRowExcel + 1;


                                                    worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = "Instrument";
                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = "Bank";
                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 4].Value = "BankBranch";
                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 5].Value = "Transaction Type";
                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 6].Value = "Category";
                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 7].Value = "Volume";
                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 8].Value = "Placement Date";
                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 9].Value = "Maturity Date";
                                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 10].Value = "InterestDays";
                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                    worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                                    worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 11].Value = "InterestDays";
                                                    worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                    worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                                    worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 1, RowG, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;




                                                    incRowExcel = incRowExcel + 2;
                                                    int _startRowDetail = incRowExcel;
                                                    int _endRowDetail = 0;
                                                    int _no = 1;


                                                    //end area header
                                                    foreach (var rsDetail2 in rsHeader2)
                                                    {

                                                        //incRowExcel++;

                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail2.FundName;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail2.Instrument;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail2.Bank;
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail2.BankBranch;
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail2.TransactionType;
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail2.Category;
                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail2.Volume;
                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail2.PlacementDate;
                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail2.MaturityDate;
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail2.InterestDays;
                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail2.Interest;
                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";


                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                        _no++;
                                                        _endRowDetail = incRowExcel;
                                                        incRowExcel++;



                                                    }






                                                    incRowExcel = incRowExcel + 2;



                                                }


                                                _rangeDetail = "A:O";

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
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 20];
                                                worksheet.Column(1).AutoFit();
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




                                                //worksheet.Column(10).AutoFit();
                                                worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                                                                               // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Laporan NAV";



                                                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                                //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                                worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();


                                            }

                                        }
                                    }




                                    package.Save();
                                    if (_FundAccountingRpt.DownloadMode == "PDF")
                                    {
                                        Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    }

                                }

                            }

                        }
                        if (counter == 0)
                            return false;
                        else
                            return true;
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

                        select @MaxClientID =  isnull(max(convert(int,right(ID,3))),0)  + 1 from FundClient where  status in (1,2) and FundClientPK > 0 and ClientCategory = @ClientCategory
                        set @MaxClientID = isnull(@MaxClientID,1)
                        IF @ClientCategory = 1
                        BEGIN
                        select @Code = 'RAMIND'
                        END
                        ELSE
                        BEGIN
                        select @Code = 'RAMINS'
                        END        
					
                        declare @LENdigit int

                        select @LENdigit = LEN(@maxClientID) 
						
                        If @LENdigit = 1
                        BEGIN
                        set @NewClientID =   @Code + '0000' + CAST(@MaxClientID as nvarchar) 
                        END
                        If @LENdigit = 2
                        BEGIN
                        set @NewClientID =   @Code + '000' + CAST(@MaxClientID as nvarchar) 
                        END
                        If @LENdigit = 3
                        BEGIN
                        set @NewClientID =   @Code + '00' + CAST(@MaxClientID as nvarchar) 
                        END
                        If @LENdigit = 4
                        BEGIN
                        set @NewClientID =  @Code + '0' + CAST(@MaxClientID as nvarchar) 
                        END
                        If @LENdigit = 5
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
    }
}