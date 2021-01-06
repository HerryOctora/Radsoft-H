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


namespace RFSRepositoryOne
{
    public class CustomClient06Reps
    {
        Host _host = new Host();

        public class ReportDataBank
        {
            public DateTime ValueDate { get; set; }
            public decimal Amount { get; set; }
            public string DetailDescription { get; set; }
            public string AccountID { get; set; }
            public string AccountName { get; set; }
            public string DebitCredit { get; set; }
            public decimal BaseDebit { get; set; }
            public decimal BaseCredit { get; set; }

        }
        public class MIFeeDetailMonthly
        {
            public DateTime Periode { get; set; }
            public decimal ManagemnetFeeAmount { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }


        }
        public class ManajemenFeeHarian
        {
            public string Date { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }
            public decimal ManagementFeeAmount { get; set; }

        }
        public class BalanceSheet
        {
            public int AccountPK { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public int Type { get; set; }
            public int Groups { get; set; }
            public decimal CurrentBalance { get; set; }
            public decimal PrevAmount { get; set; }

        }

        public class ManagementFee
        {
            public DateTime Date { get; set; }
            public string FundName { get; set; }
            public string FundClientName { get; set; }
            public decimal NavPerUnit { get; set; }
            public decimal Units { get; set; }
            public decimal OSUnits { get; set; }
            public decimal FUM { get; set; }
            public decimal Gross { get; set; }
            public decimal PPH23 { get; set; }
            public decimal Nett { get; set; }
            public decimal TransactionAmount { get; set; }

        }

        public string FundClient_GenerateNewClientID(int _investorType, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        //INDOASIA 
                        cmd.CommandText =
                        @" Declare @xInvestorType  nvarchar(100)    
                         Declare @NewClientID  nvarchar(100)     
                         Declare @MaxClientName  nvarchar(100)   
                         Declare @Period int                                
                         Declare @LENdigit int                 
                         Declare @NewDigit Nvarchar(20)                    
                        
                         select @MaxClientName =   SUBSTRING ( ID ,11 , 3 )+ 1   from FundClient         
                         where InvestorType=@InvestorType order by ID                  
   
                         select @Period = (RIGHT(CONVERT(VARCHAR(8), getdate(), 3), 2))               
       
                         select @xInvestorType = MAX(left(DescOne,3)) from FundClient F left join MasterValue MV on F.InvestorType = MV.Code  
                         and MV.ID ='InvestorType' and MV.status = 2  where InvestorType = @InvestorType  
      
                         select @LENdigit = LEN(@MaxClientName)                     
                          
                         if @LENdigit = 1                     
                         BEGIN                     
                         set @NewDigit = '00'+  CAST(@MaxClientName as nvarchar)                     
                         END                    
                         if @LENdigit = 2                    
                         BEGIN                     
                         set @NewDigit = '0' + CAST(@MaxClientName as nvarchar)                     
                         END               
   
                         set @NewClientID =  CAST(@Period as nvarchar) + 'IAM' + CAST(@xInvestorType as nvarchar) + CAST(@NewDigit as nvarchar)   
   
                         Select @NewClientID   NewClientID ";

                        cmd.Parameters.AddWithValue("@InvestorType", _investorType);

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

            //TRIAL BALANCE 
            #region Nav Report
            if (_FundAccountingRpt.ReportName.Equals("Nav Report"))
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
                                _paramFund = "And FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText = @" 
                               

                            DECLARE A CURSOR FOR 
                            select FundPK from Fund 
                            where status = 2  " + _paramFund + @"


                            Open A
                            Fetch Next From A
                            Into @FundPK

                            While @@FETCH_STATUS = 0 
                            BEGIN    

                            Select @Date Date,ROW,Description,sum(dbo.FGetGroupFundJournalAccountBalanceByFundPK(@Date,A.FundJournalAccountPK,@FundPK)) Amount  From NAVmappingReport A  
                            left join FundJournalAccount B on  A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status	 =  2  
                            Group By ROW,Description
                            order by Row

                            Fetch next From A Into @FundPK
                            END
                            Close A
                            Deallocate A ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "NavReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "NavReport" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Nav Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<NavReportListing> rList = new List<NavReportListing>();
                                        while (dr0.Read())
                                        {

                                            NavReportListing rSingle = new NavReportListing();
                                            rSingle.Date = Convert.ToString(dr0["Date"]);
                                            rSingle.Row = Convert.ToInt32(dr0["Row"]);
                                            rSingle.Description = Convert.ToString(dr0["Description"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                            orderby r.Row ascending
                                            group r by new { r.Date, r.CurrencyID } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Formulir Nomor : ";
                                            worksheet.Cells[incRowExcel, 2].Value = "X.D.1-1";
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Manager Investasi : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyName();
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Bank Kustodian : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_BankCustodianName(_FundAccountingRpt.FundFrom);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Nama Reksadana : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(_FundAccountingRpt.FundFrom);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Jenis Reksadana : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundType(_FundAccountingRpt.FundFrom);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Tanggal : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Mata Uang : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CurrencyID(_FundAccountingRpt.FundFrom);
                                            incRowExcel++;


                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[RowB, 1].Style.Font.Bold = true;
                                            worksheet.Cells[RowB, 1].Value = "LAPORAN AKTIVA DAN KEWAJIBAN";
                                            worksheet.Cells[RowB, 2].Style.Font.Bold = true;
                                            worksheet.Cells[RowB, 2].Value = "AMOUNT";
                                            worksheet.Cells[RowB + 1, 1].Style.Font.Bold = true;
                                            worksheet.Cells[RowB + 1, 1].Value = "AKTIVA";
                                            worksheet.Cells[22, 1].Style.Font.Bold = true;
                                            worksheet.Cells[22, 1].Value = "KEWAJIBAN";

                                            string _range = "A" + incRowExcel + ":B" + incRowExcel;

                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 11;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                            }

                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            var _description = "";
                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {



                                                //ThickBox Border HEADER

                                                worksheet.Cells["A" + RowB + ":B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":B" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":B" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":B" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                //area detail

                                                //if (_description != rsDetail.Description && rsDetail.Amount != 0)
                                                //{
                                                worksheet.Cells[rsDetail.Row, 1].Value = rsDetail.Description;
                                                worksheet.Cells[rsDetail.Row, 2].Value = rsDetail.Amount;
                                                worksheet.Cells[rsDetail.Row, 2].Style.Numberformat.Format = "#,####0.00";
                                                //}

                                                _endRowDetail = incRowExcel;

                                                _no++;
                                                incRowExcel++;
                                                _description = rsDetail.Description;





                                            }

                                            decimal _totalAktiva = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 20);
                                            decimal _totalKewajiban = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 25);


                                            //decimal _subs = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 29);
                                            //decimal _redemp = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 30);
                                            //decimal _retainEarning = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 31);
                                            //decimal _dividen = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 32);
                                            //decimal _realised = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 33);
                                            //decimal _unrealised = _host.Get_AccountBalanceForNav(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo, 34);
                                            decimal _totalUnit = _host.Get_FundUnitPosition(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateTo);
                                            decimal _nav;
                                            if (_totalUnit == 0)
                                            {
                                                _nav = 1000;
                                            }
                                            else
                                            {
                                                _nav = (_totalAktiva - _totalKewajiban) / _totalUnit;
                                            }

                                            //worksheet.Cells[27, 1].Value = "TOTAL AKTIVA BERSIH";
                                            //worksheet.Cells[27, 2].Value = _totalAktiva - _totalKewajiban;
                                            //worksheet.Cells[27, 2].Style.Numberformat.Format = "#,####0";
                                            //worksheet.Cells[33, 2].Value = (_realised);
                                            //worksheet.Cells[33, 2].Style.Numberformat.Format = "#,####0";
                                            //worksheet.Cells[34, 2].Value = (_unrealised);
                                            //worksheet.Cells[34, 2].Style.Numberformat.Format = "#,####0";
                                            //worksheet.Cells[35, 1].Value = "PENDAPATAN INVESTASI BERSIH";
                                            //worksheet.Cells[35, 2].Value = (_totalAktiva - _totalKewajiban) - (_subs + _redemp + _retainEarning + _dividen + _realised + _unrealised);
                                            //worksheet.Cells[35, 2].Style.Numberformat.Format = "#,####0";

                                            worksheet.Cells[_endRowDetail + 6, 1].Value = "TOTAL SAHAM/UNIT PENYERTAAN DAN LABA/RUGI";
                                            worksheet.Cells[_endRowDetail + 6, 2].Value = _totalAktiva - _totalKewajiban;
                                            worksheet.Cells[_endRowDetail + 6, 2].Style.Numberformat.Format = "#,####0.0000";

                                            worksheet.Cells[_endRowDetail + 8, 1].Value = "JUMLAH SAHAM/UNIT PENYERTAAN YANG BEREDAR";
                                            worksheet.Cells[_endRowDetail + 8, 2].Value = _totalUnit;
                                            worksheet.Cells[_endRowDetail + 8, 2].Style.Numberformat.Format = "#,####0.0000";




                                            worksheet.Cells[_endRowDetail + 9, 1].Value = "NILAI AKTIVA BERSIH PER SAHAM/UNIT PENYERTAAN";
                                            worksheet.Cells[_endRowDetail + 9, 2].Value = _nav;
                                            worksheet.Cells[_endRowDetail + 9, 2].Style.Numberformat.Format = "#,####0.0000";

                                            int RowF = _endRowDetail + 7;
                                            int RowH = _endRowDetail + 9;
                                            worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowH].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowH].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowH].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowH].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowH].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowH].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                            //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";

                                            incRowExcel = incRowExcel + 10;

                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        }

                                        string _rangeDetail = "A:B";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 3];
                                        worksheet.Column(3).Width = 1;
                                        worksheet.Column(1).Width = 60;
                                        worksheet.Column(2).AutoFit();

                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 NAV REPORT";


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

            #region MI Fee Detail Monthly
            if (_FundAccountingRpt.ReportName.Equals("MI Fee Detail Monthly"))
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
                                _paramFund = "And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText =
                            @" 
                            select B.ID FundID,B.Name FundName, @DateTo Periode,sum(A.ManagementFeeAmount) ManagementFeeAmount from FundDailyFee A
                            left join fund B on A.fundpk = B.FundPK and B.status = 2
                            where A.Date between @DateFrom and @DateTo and A.fundPK <> 6 " + _paramFund + @"
                            group by A.FundPK,B.ID,B.Name   
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
                                    string filePath = Tools.ReportsPath + "MIFeeDetailMonthly" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "MIFeeDetailMonthly" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FinanceReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("MI Fee Detail Monthly");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<MIFeeDetailMonthly> rList = new List<MIFeeDetailMonthly>();
                                        while (dr0.Read())
                                        {
                                            MIFeeDetailMonthly rSingle = new MIFeeDetailMonthly();
                                            rSingle.Periode = Convert.ToDateTime(dr0["Periode"]);
                                            rSingle.ManagemnetFeeAmount = Convert.ToDecimal(dr0["ManagementFeeAmount"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByBank =
                                             from r in rList
                                             orderby r.FundID ascending
                                             group r by new { } into rGroup
                                             select rGroup;

                                        int incRowExcel = 0;
                                        //int _rowEndBalance = 0;
                                        foreach (var rsHeader in GroupByBank)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "DATEFROM :";
                                            worksheet.Cells[incRowExcel, 3].Value = _FundAccountingRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "DATETO :";
                                            worksheet.Cells[incRowExcel, 3].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";

                                            //_rowEndBalance = incRowExcel;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "FUND ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "FUND NAME";
                                            worksheet.Cells[incRowExcel, 4].Value = "AMOUNT";
                                            worksheet.Cells[incRowExcel, 5].Value = "PERIODE";

                                            string _range = "A" + incRowExcel + ":E" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 11;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();

                                            }

                                            _range = "A" + incRowExcel + ":E" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }
                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                _range = "A" + incRowExcel + ":E" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 12;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                                }
                                                //area detail

                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.ManagemnetFeeAmount;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Periode;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "MMM-yyyy";

                                                _range = "A" + incRowExcel + ":E" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 12;
                                                }


                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }
                      
                                            worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            //incRowExcel++;
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        }

                                        string _rangeDetail = "A:E";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 5];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
        
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Management Fee Amount";


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

            #region Manajemen Fee Harian
            else if (_FundAccountingRpt.ReportName.Equals("Manajemen Fee Harian"))
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
                            select A.date Date,B.ID FundID,B.Name FundNAme,A.ManagementFeeAmount from FundDailyFee A
                            left join Fund B on A.fundPK = B.fundPK and B.status = 2
                            where A.Date between @DateFrom and @DateTo and A.fundPK <> 6 " + _paramFundFrom + @" 
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
                                    string filePath = Tools.ReportsPath + "ManajemenFeeHarian" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ManajemenFeeHarian" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Manajemen Fee Harian");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ManajemenFeeHarian> rList = new List<ManajemenFeeHarian>();
                                        while (dr0.Read())
                                        {

                                            ManajemenFeeHarian rSingle = new ManajemenFeeHarian();
                                            rSingle.Date = Convert.ToString(dr0["Date"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.ManagementFeeAmount = Convert.ToDecimal(dr0["ManagementFeeAmount"]);

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
                                            incRowExcel ++;
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
                                            worksheet.Cells[incRowExcel, 5].Value = "Manajemen Fee Amount";

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
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.ManagementFeeAmount;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                               
                                                no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                           

                                            worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            int last = incRowExcel - 1;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            incRowExcel++;

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
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Manajemen Fee Harian";


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

            else
            {
                return false;
            }
        }


        public string Retrieve_ManagementFee(DateTime _date, string _usersID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                        declare @FundPK int
                        declare @InstrumentPK int 
    
                        Declare @ARManagementFee int 
                        Declare @TaxARManagementFee int 
                        Declare @ManagementFeeExpense int 


                        declare @ManagementFeeAmount numeric(22,6)

                        declare @ARManagementFeeAmount numeric(22,6)
                        declare @TaxARManagementFeeAmount numeric(22,6)
                        declare @ManagementFeeExpenseAmount numeric(22,6)

                        create table #ReferenceTemp
                        (Reference nvarchar(50))

                        Declare @JourHeader int  
                        set @JourHeader = 0    
                        Declare @JournalPK int 
                        Declare @PeriodPK int 
                        Declare @Reference nvarchar(50)    
                        Select @PeriodPK = PeriodPK From Period Where DateFrom <= @Date and Dateto >= @Date and Status = 2  
                        Select @JournalPK = isnull(Max(JournalPK),0) from Journal   

                        insert into #ReferenceTemp (Reference)
                        exec getJournalReference @Date,'ADJ',@Reference out  
 
                        set @JournalPK = @JournalPK + 1   
                        INSERT INTO [Journal]  
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        ,[PostedTime],[EntryUsersID],[EntryTime]  
                        ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
                        SELECT @JournalPK,1,2,'Management Fee',@PeriodPK, @Date
                        ,0,'',@Reference,1,'Management Fee',1,@UsersID
                        ,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow 


                        Declare @AutoNo int 
                        set @AutoNo = 0   
                        DECLARE A CURSOR FOR 
                        select FundPK,ManagementFeeAmount from FundDailyFee where Date = @Date
                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount   
                        WHILE @@FETCH_STATUS = 0  
                        BEGIN 


                        select @InstrumentPK = InstrumentPK From Instrument A 
                        left join Fund B on A.ID = B.ID and B.Status = 2 where A.Status = 2 and B.FundPK = @FundPK         
 
                        Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  TaxARManagementFee,
                        @ManagementFeeExpense = ManagementFeeExpense 
                        From AccountingSetup Where Status = 2


                        IF (@ManagementFeeAmount) > 0
                        BEGIN

                        set @AutoNo = @AutoNo + 1   


                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
                        set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeAmount
                        set @ARManagementFeeAmount = @ManagementFeeAmount - @TaxARManagementFeeAmount

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ARManagementFeeAmount), abs(@ARManagementFeeAmount),0,1,abs(@ARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@TaxARManagementFeeAmount), abs(@TaxARManagementFeeAmount),0,1,abs(@TaxARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ManagementFeeExpenseAmount), 0,abs(@ManagementFeeExpenseAmount),1,0,abs(@ManagementFeeExpenseAmount),@UsersID,@TimeNow 
                        END

                        ELSE

                        BEGIN
                        set @AutoNo = @AutoNo + 1   


                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
                        set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeAmount
                        set @ARManagementFeeAmount = @ManagementFeeAmount - @TaxARManagementFeeAmount

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ARManagementFeeAmount), 0,abs(@ARManagementFeeAmount),1,0,abs(@ARManagementFeeAmount),@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@TaxARManagementFeeAmount) ,0,abs(@TaxARManagementFeeAmount),1,0,abs(@TaxARManagementFeeAmount),@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ManagementFeeExpenseAmount),abs(@ManagementFeeExpenseAmount), 0,1,abs(@ManagementFeeExpenseAmount), 0,@UsersID,@TimeNow 
                        END
                        
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount 
                        END 
                        CLOSE A  
                        DEALLOCATE A

                        DECLARE @combinedString NVARCHAR(500)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #ReferenceTemp
                        IF (@combinedString is null)
                        BEGIN
                            SELECT 'No Data Retrieve, Please Check Fund Daily Fee' as Result
                        END
                        ELSE
                        BEGIN
                        SELECT 'Retrieve Management Fee Success ! Reference is : ' + @combinedString as Result
                        END
                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean GenerateReportAccounting(string _userID, AccountingRpt _accountingRpt)
        {

            #region Report Data Bank
            if (_accountingRpt.ReportName.Equals("Report Data Bank"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramAccount = "";
                            string _status = "";

                            if (!_host.findString(_accountingRpt.AccountFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AccountFrom))
                            {
                                _paramAccount = "And B.AccountPK in ( " + _accountingRpt.AccountFrom + " ) ";
                            }
                            else
                            {
                                _paramAccount = "";
                            }

                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and A.Status = 2 and A.Posted = 0 and A.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and A.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (A.Status = 2 or A.Posted = 1) and A.Reversed = 0 and A.status not in (3,4) ";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Reversed = 0 and A.status not in (3,4) ";
                            }

                            cmd.CommandText =
                            @" 
                            select A.ValueDate,C.ID AccountID,C.Name AccountName,B.DetailDescription,B.Amount,B.DebitCredit,B.BaseDebit,B.BaseCredit from journal A
                            left join JournalDetail B on A.JournalPK = B.JournalPK 
                            left join Account C on B.AccountPK = c.AccountPK and C.status in (1,2)
                            where A.JournalPK in
                            (
	                            select A.journalPK from journal A
	                            left join JournalDetail B on A.JournalPK = B.JournalPK 
	                            Where ValueDate between @DateFrom and @DateTo " + _paramAccount + _status + @" 
                            )
                            Order by A.JournalPK asc   
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
                                    string filePath = Tools.ReportsPath + "ReportDataBank" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ReportDataBank" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FinanceReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report Data Bank");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ReportDataBank> rList = new List<ReportDataBank>();
                                        while (dr0.Read())
                                        {
                                            ReportDataBank rSingle = new ReportDataBank();
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.DetailDescription = Convert.ToString(dr0["DetailDescription"]);
                                            rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                            rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                            rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                            rSingle.BaseDebit = Convert.ToDecimal(dr0["BaseDebit"]);
                                            rSingle.BaseCredit = Convert.ToDecimal(dr0["BaseCredit"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByBank =
                                             from r in rList
                                             orderby r.ValueDate ascending
                                             group r by new { } into rGroup
                                             select rGroup;

                                        int incRowExcel = 0;
                                        //int _rowEndBalance = 0;
                                        foreach (var rsHeader in GroupByBank)
                                        {

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "BANK : ";
                                            worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_AccountName(_accountingRpt.AccountFrom);
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "DATEFROM :";
                                            worksheet.Cells[incRowExcel, 3].Value = _accountingRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "DATETO :";
                                            worksheet.Cells[incRowExcel, 3].Value = _accountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";

                                            //_rowEndBalance = incRowExcel;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "DATE";
                                            worksheet.Cells[incRowExcel, 3].Value = "ACCOUNT ID";
                                            worksheet.Cells[incRowExcel, 4].Value = "ACCOUNT NAME";
                                            worksheet.Cells[incRowExcel, 5].Value = "DESC";
                                            worksheet.Cells[incRowExcel, 6].Value = "AMOUNT";
                                            worksheet.Cells[incRowExcel, 7].Value = "BASE DEBIT";
                                            worksheet.Cells[incRowExcel, 8].Value = "BASE CREDIT";
                                            string _range = "A" + incRowExcel + ":H" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 11;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();

                                            }

                                            _range = "A" + incRowExcel + ":H" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }
                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            //int _endRowDetail = 0;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                _range = "A" + incRowExcel + ":H" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                                }
                                                //area detail

                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountID;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AccountName;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DetailDescription;
                                                //worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.BaseDebit;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.BaseCredit;


                                                incRowExcel++;
                                                _range = "A" + incRowExcel + ":H" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                }


                                                _no++;
                                                //incRowExcel++;

                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _accountingRpt.PageBreak;

                                        }
                                        string _rangeDetail = "A:H";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).AutoFit();
                                        worksheet.Column(5).AutoFit();
                                        worksheet.Column(6).AutoFit();
                                        worksheet.Column(7).AutoFit();
                                        worksheet.Column(8).AutoFit();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 BANK RECONCILE";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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

            #region Balance Sheet
            else if (_accountingRpt.ReportName.Equals("Balance Sheet"))
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
                            cmd.CommandText =
                            @"
                            Declare @DateFrom datetime
                            --declare @ValueDateTo datetime
                            --set @ValueDateTo = '01/31/18'
                            Declare @PeriodPK int
                            Select @PeriodPK = PeriodPK from Period where @ValueDateTo between DateFrom and DateTo and status = 2

                            SELECT @DateFrom =  DATEADD(DAY,(DATEPART(DAY,@ValueDateTo)-1)*(-1),@ValueDateTo)

                            Select A.iD,A.Name,A.Type,A.Groups,sum(A.CurrentBalance) CurrentBalance, sum(A.prevBalance) PrevBalance from (
                            SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],    
                            SUM(B.Balance) AS CurrentBalance , 0 PrevBalance FROM Account A, (     
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
                            OR B.ParentPK9 = A.AccountPK) and A.Status = 2    
                            " + _paramData + @"      
                            GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    

                            UNION ALL



                            SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],    
                            0 AS CurrentBalance ,SUM(B.Balance) AS PrevBalance FROM Account A, (     
                            SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 1 THEN A.BaseDebit-A.BaseCredit      
                            ELSE A.BaseCredit-A.BaseDebit END) AS Balance,      
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,     
                            C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            FROM [JournalDetail] A      
                            INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK     
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.Status in (1,2)   
                            WHERE  B.ValueDate < @DateFrom and B.PeriodPK = @PeriodPK   
                            " + _status + @"   
                            GROUP BY A.AccountPK, B.Posted, B.Reversed,C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4,      
                            C.ParentPK5, C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            ) AS B     
                            WHERE A.[Type] <= 2 AND A.Show = 1 AND (B.AccountPK = A.AccountPK      
                            OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK       
                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK      
                            OR B.ParentPK5 = A.AccountPK OR B.ParentPK6 = A.AccountPK      
                            OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK       
                            OR B.ParentPK9 = A.AccountPK) and A.Status = 2    
                            " + _paramData + @"      
                            GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    
                            )A
                            group by A.ID,A.Name,A.Type,A.Groups  
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
                                    string filePath = Tools.ReportsPath + "BalanceSheet" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "BalanceSheet" + "_" + _userID + ".pdf";
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

                                        List<BalanceSheet> rList = new List<BalanceSheet>();
                                    while (dr0.Read())
                                    {
                                        BalanceSheet rSingle = new BalanceSheet();
                                        //rSingle.AccountPK = Convert.ToInt32(dr0["AccountPK"]);
                                        rSingle.ID = Convert.ToString(dr0["ID"]);
                                        rSingle.Name = Convert.ToString(dr0["Name"]);
                                        rSingle.Type = Convert.ToInt32(dr0["Type"]);
                                        rSingle.Groups = Convert.ToInt32(dr0["Groups"]);
                                        rSingle.CurrentBalance = Convert.ToDecimal(dr0["CurrentBalance"]);
                                        rSingle.PrevAmount = Convert.ToDecimal(dr0["PrevBalance"]);

                                        rList.Add(rSingle);

                                    }
                                    var GroupByReference =
                                                from r in rList
                                                orderby r.Type ascending
                                                group r by new { r.Type } into rGroup
                                                select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Statement Of Financial Position";
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Size = 20;

                                        incRowExcel++;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "As of : ";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 4].Value = _accountingRpt.ValueDateTo;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        incRowExcel++;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "Period ID :";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 4].Value = _accountingRpt.ValueDateTo;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "yyyy";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                         incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        if (rsHeader.Key.Type == 1)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            worksheet.Cells[incRowExcel, 2].Value = "DESCRIPTION";
                                            worksheet.Cells[incRowExcel, 3].Value = "CURRENT MONTH";
                                            worksheet.Cells[incRowExcel, 4].Value = "PREVIOUS MONTH";

                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            worksheet.Cells[incRowExcel, 2].Value = "DESCRIPTION";
                                            worksheet.Cells[incRowExcel, 3].Value = "CURRENT MONTH";
                                            worksheet.Cells[incRowExcel, 4].Value = "PREVIOUS MONTH";
                                        }
                                        string _range = "A" + incRowExcel + ":J" + incRowExcel;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        incRowExcel++;
                                   

                                        int _startRowDetail = incRowExcel;
                                        int _startRowDetailB = incRowExcel;
                                        int _endRowDetail = 0;
                                        //int _A = 0;
                                         //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            if (rsHeader.Key.Type == 1)
                                            {
                                                
                                                if (rsDetail.Groups == 1)
                                                {
                                     
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                                }
                                                
                                            }
                                            else 
                                            {
                                                if(rsDetail.Groups == 1)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
   
                                                }
                                                
                                            }
                                            _endRowDetail = incRowExcel;

                                            //_no++;
                                            incRowExcel++;
                                        }
                                        int _A = 6;
                                        int _B = 0;

                                        int RowF = incRowExcel - 1;
                                        worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        if (rsHeader.Key.Type == 1)
                                        {
                                            _A = incRowExcel;
                                            worksheet.Cells[incRowExcel, 2].Value = "TOTAL ASSETS :";
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ")";
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        else
                                        {
                                            _B = incRowExcel;
                                            worksheet.Cells[incRowExcel, 2].Value = "TOTAL LIABILITIES & EQUITY :";
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetailB + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetailB + ")";
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //incRowExcel = incRowExcel + 2;
                                            //worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                            //worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _A + ":C" + _B + ")";
                                            //worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 3].Calculate();
                                            //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";

                                        }
                                        incRowExcel = incRowExcel + 2;

                                     
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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 4];
                                    worksheet.Column(1).Width = 20;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                   
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 FUND PORTFOLIO";


                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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

            #region Profit And Lost
            else if (_accountingRpt.ReportName.Equals("Profit And Lost"))
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
                            cmd.CommandText =
                            @"
                            declare @ValueDateFromLM datetime
                            declare @ValueDateToLM datetime

                            Select @ValueDateFromLM = dateadd(month,-1,@ValueDateFrom)

                            Select @ValueDateToLM = DATEADD(DAY,(DATEPART(DAY,@ValueDateTo)-1)*(-1),@ValueDateTo)

                            Declare @PeriodPKLM int

                            select @PeriodPKLM = PeriodPK from period where status = 2 and @ValueDateFromLM between DateFrom and DateTo

                            Declare @PeriodPK int
                            Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                            Select A.iD,A.Name,A.Type,A.Groups,sum(A.CurrentBalance) CurrentBalance, sum(A.prevBalance) PrevBalance from(
                            SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],      
                            SUM(B.Balance) AS CurrentBalance, 0 PrevBalance FROM Account A, (      
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
                            OR B.ParentPK9 = A.AccountPK) and A.status = 2 
                            " + _paramData + @"        
                            GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    

                            UNION ALL

                            SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],      
                            0 AS CurrentBalance,SUM(B.Balance) AS PrevBalance FROM Account A, (      
                            SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 4 THEN A.BaseDebit-A.BaseCredit       
                            ELSE A.BaseCredit-A.BaseDebit END) AS Balance,       
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,      
                            C.ParentPK7, C.ParentPK8, C.ParentPK9      
                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK    
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.status in (1,2)    
                            WHERE  B.ValueDate >= @ValueDateFromLM  and B.ValueDate < @ValueDateToLM and B.PeriodPK = @PeriodPKLM      
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
                            OR B.ParentPK9 = A.AccountPK) and A.status = 2 
                            " + _paramData + @"        
                            GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    
                            )A
                            group by A.ID,A.Name,A.Type,A.Groups
  
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
                                    string filePath = Tools.ReportsPath + "ProfitAndLost" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ProfitAndLost" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Profit And Lost");

                                        List<BalanceSheet> rList = new List<BalanceSheet>();
                                        while (dr0.Read())
                                        {
                                            BalanceSheet rSingle = new BalanceSheet();
                                            //rSingle.AccountPK = Convert.ToInt32(dr0["AccountPK"]);
                                            rSingle.ID = Convert.ToString(dr0["ID"]);
                                            rSingle.Name = Convert.ToString(dr0["Name"]);
                                            rSingle.Type = Convert.ToInt32(dr0["Type"]);
                                            rSingle.Groups = Convert.ToInt32(dr0["Groups"]);
                                            rSingle.CurrentBalance = Convert.ToDecimal(dr0["CurrentBalance"]);
                                            rSingle.PrevAmount = Convert.ToDecimal(dr0["prevBalance"]);

                                            rList.Add(rSingle);

                                        }
                                        var GroupByReference =
                                                    from r in rList
                                                    orderby r.Type ascending
                                                    group r by new { r.Type } into rGroup
                                                    select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Profit & Loss Statement";
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                             
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "As of : ";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4].Value = _accountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Period ID :";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4].Value = _accountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "yyyy";
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;

                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            if (rsHeader.Key.Type == 3)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "ID";
                                                worksheet.Cells[incRowExcel, 2].Value = "DESCRIPTION";
                                                worksheet.Cells[incRowExcel, 3].Value = "CURRENT MONTH";
                                                worksheet.Cells[incRowExcel, 4].Value = "YEAR TO DATE";

                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "ID";
                                                worksheet.Cells[incRowExcel, 2].Value = "DESCRIPTION";
                                                worksheet.Cells[incRowExcel, 3].Value = "CURRENT MONTH";
                                                worksheet.Cells[incRowExcel, 4].Value = "YEAR TO DATE";
                                            }
                                            string _range = "A" + incRowExcel + ":J" + incRowExcel;

                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 11;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                            }

                                            incRowExcel++;


                                            int _startRowDetail = incRowExcel;
                                            int _startRowDetailB = incRowExcel;
                                            int _endRowDetail = 0;
                                            //int _A = 0;
                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                if (rsHeader.Key.Type == 1)
                                                {

                                                    if (rsDetail.Groups == 3)
                                                    {

                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;

                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                        //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                                    }

                                                }
                                                else
                                                {
                                                    if (rsDetail.Groups == 1)
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.CurrentBalance;
                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.PrevAmount;
                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                        //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                                    }

                                                }
                                                _endRowDetail = incRowExcel;

                                                //_no++;
                                                incRowExcel++;
                                            }

                                            int _A = 6;
                                            int _B = 0;

                                            int RowF = incRowExcel - 1;
                                            worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            if (rsHeader.Key.Type == 3)
                                            {
                                                _A = incRowExcel;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL INCOME :";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ")";
                                                worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }
                                            else
                                            {
                                                _B = incRowExcel;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL EXPENSES :";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetailB + ")";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetailB + ")";
                                                worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel = incRowExcel + 1;
                                                worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 2].Value = "PROFIT / LOST :";
                                                worksheet.Cells[incRowExcel, 3].Formula = "(C" + _A + "-C" + _B + ")";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Formula = "(D" + _A + "-D" + _B + ")";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                            }
                                           
                                            //worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _A + "+G" + _rows + ")";

                                            incRowExcel = incRowExcel + 2;
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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 4];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 30;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 FUND PORTFOLIO";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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

        public Boolean Payment_Voucher(string _userID, string _periodPK, string _cashierID, CashierVoucher _CashierVoucher)
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
                            Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
                        DebitCredit,(Case When DebitCredit ='D' then BaseDebit else 0 end) Debit,(Case When DebitCredit ='D' then 0 else BaseCredit end) Credit,F.ID DepartmentID,Case When DebitCredit ='D' then 1 else 2 end Row       
                        from Cashier C       
                        left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)         
                        UNION ALL       
                        Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                        'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID ,3 Row          
                        from Cashier C       
                        left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID  and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)    
                        group by C.EntryUsersID, Valuedate,A.ID, A.Name,C.Reference     
                        Order By row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FinanceReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Payment Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Report ini ditujukan untuk";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + _CashierVoucher.ReceiverName;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : " + Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        //worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 3].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 4].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 5].Value = "CREDIT";
                                        string _range = "A" + incRowExcel + ":E" + incRowExcel;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;


                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {



                                            //ThickBox Border HEADER

                                            worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Credit;
                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;





                                        }

                                        int RowF = incRowExcel - 1;
                                        worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 4].Calculate();
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 4].Value = worksheet.Cells[incRowExcel - 1, 4].Value;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel - 1, 4].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "APPROVED";
                                        worksheet.Cells[incRowExcel, 3].Value = "FINANCE";
                                        worksheet.Cells[incRowExcel, 4].Value = "ACCOUNTING";
                                        worksheet.Cells[incRowExcel, 5].Value = "PAYOR/BENEFICIARY";
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 2].Value = "(                                          )";
                                        worksheet.Cells[incRowExcel, 3].Value = "(                                          )";
                                        worksheet.Cells[incRowExcel, 4].Value = "(                                          )";
                                        worksheet.Cells[incRowExcel, 5].Value = "(                                          )";
                                        worksheet.Row(incRowExcel).PageBreak = true;


                                    }

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 5];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 Receipt VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 PAYMENT VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                return true;
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

        public Boolean OMSEquity_ListingRpt(string _userID, InvestmentListing _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (0) ";
                        }

                        if (_listing.ParamFundID != "All")
                        {
                            _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo
,isnull(IV.EntryDealingID,'') DealingID
,isnull(J.PTPCode,'') PTPCode
,isnull(K.ID,'') CounterpartID
,IV.valueDate
,I.ID InstrumentID
,I.Name InstrumentName
,F.ID FundID,IT.Name  InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,   
IV.Amount,IV.Notes, IV.RangePrice, IV.*   
from Investment IV       
left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3  and IT.Type = 1
                                            " + _paramInvestmentPK + _paramFund + @" order by IV.InvestmentPK ";


                        cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                        if (_listing.ParamFundID != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                        }

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "OMSEquityListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "OMSEquityListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "InvestmentReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Investment Listing");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<InvestmentListing> rList = new List<InvestmentListing>();
                                    while (dr0.Read())
                                    {
                                        InvestmentListing rSingle = new InvestmentListing();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.InstrumentType = Convert.ToString(dr0["InstrumentType"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.InvestmentPK = Convert.ToInt32(dr0["InvestmentPK"]);
                                        if (rSingle.InstrumentType == "Equity Reguler")
                                        {
                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);

                                        }
                                        else if (rSingle.InstrumentType == "Corporate Bond" || rSingle.InstrumentType == "Government Bond")
                                        {

                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        }
                                        else
                                        {

                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            if (rSingle.TrxTypeID != "PLACEMENT")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                        }
                                        rSingle.Volume = Convert.ToDecimal(dr0["DoneVolume"]);
                                        rSingle.Amount = Convert.ToDecimal(dr0["DoneAmount"]);
                                        rSingle.Notes = Convert.ToString(dr0["Notes"]);
                                        rSingle.EntryDealingID = Convert.ToString(dr0["DealingID"]);
                                        rSingle.CounterpartID = Convert.ToString(dr0["CounterpartID"]);
                                        rSingle.PTPCode = Convert.ToString(dr0["PTPCode"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                         from r in rList
                                         orderby r.FundID, r.InstrumentType
                                         group r by new { r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                         select rGroup;



                                    int incRowExcel = 1;
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                    worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                    worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_listing.ParamListDate);
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;



                                    foreach (var rsHeader in GroupByFundID)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundID;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "INS. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InstrumentType;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "TRX. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TrxTypeID;
                                        incRowExcel = incRowExcel + 1;
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Volume / Shares";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                            worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                            worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                        }
                                        else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 4].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 5].Value = "Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                            worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                            worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                        }
                                        else
                                        {
                                            if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Acq Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                                worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                                worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                            }

                                        }

                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":J" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 14;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _range = "A" + incRowExcel + ":J" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 14;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                            }
                                            //area detail
                                            if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                //worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EntryDealingID;
                                                //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.CounterpartID;
                                                //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.PTPCode;
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                            }


                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                                if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.AcqDateDeposito;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                }


                                            }





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        _range = "A" + incRowExcel + ":G" + incRowExcel;
                                        using (ExcelRange s = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            s.Style.Font.Size = 14;
                                            s.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                        }

                                        //THICK BOX DETAIL
                                        _endRowDetail = incRowExcel - 1;
                                        worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler" || rsHeader.Key.InstrumentType == "Deposito Money Market")
                                        {
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        }
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                    }

                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                    worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                    worksheet.Cells["B" + incRowExcel + ":G" + (incRowExcel + 7)].Merge = true;
                                    int _rowNotes = incRowExcel + 10;
                                    worksheet.Cells["B" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + _rowNotes + ":G" + _rowNotes].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + incRowExcel + ":B" + _rowNotes].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["G" + incRowExcel + ":G" + _rowNotes].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                    incRowExcel = incRowExcel + 13;

                                    worksheet.Cells[incRowExcel, 1].Value = "Prepare By";
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 4].Value = "Approval";
                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    incRowExcel = incRowExcel + 5;
                                    worksheet.Cells[incRowExcel, 1].Value = "(    ";
                                    worksheet.Cells[incRowExcel, 3].Value = ")";
                                    worksheet.Cells[incRowExcel, 4].Value = "(    ";
                                    worksheet.Cells[incRowExcel, 6].Value = ")";
                                    worksheet.Row(incRowExcel).PageBreak = true;

                                    string _rangeA = "A1:J" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 11];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 25;
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&34 INVESTMENT APPROVAL";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    if (_listing.DownloadMode == "PDF")
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

        public Boolean OMSTimeDeposit_ListingRpt(string _userID, InvestmentListing _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _bitIsMature = "";
                        string _paramInvestmentPK = "";
                        string _paramMatureInvestmentPk = "";

                        if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
                            _paramMatureInvestmentPk = " InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (0) ";
                            _paramMatureInvestmentPk = " InvestmentPK in (0) ";
                        }

                        if (_listing.ParamFundID != "All")
                        {
                            _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        if (_listing.BitIsMature == true)
                        {
                            _bitIsMature = @"union all
                            select Reference, RefNo,ValueDate,InstrumentID,InstrumentName,    
                            FundID,InstrumentType,InvestmentPK,Volume,OrderPrice,InterestPercent,TrxTypeID,   
                            Amount,Notes,RangePrice ,MaturityDate ,DoneVolume,DoneAmount,'',AcqDate  ,'' DealingID,'' PTPCode
                            from InvestmentMature where " + _paramMatureInvestmentPk;
                        }
                        else
                        {
                            _bitIsMature = "";
                        }


                        cmd.CommandText = @"
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ParamListDate
                        )
                        and status = 2

                        Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                        F.ID FundID,IT.Name InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,   
                        IV.Amount,IV.Notes, IV.RangePrice,IV.MaturityDate,IV.DoneVolume,IV.DoneAmount,IV.Notes,IV.AcqDate
                        ,isnull(IV.EntryDealingID,'') DealingID,isnull(J.PTPCode,'') PTPCode
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2      
                        left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2    
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3  and IT.Type = 3  " + _paramInvestmentPK + _paramFund + _bitIsMature +
                        @"
                        order by FundID
                        ";


                        cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                        if (_listing.ParamFundID != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                        }

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "OMSTimeDepositListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "OMSTimeDepositListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "InvestmentReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Investment Listing");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<InvestmentListing> rList = new List<InvestmentListing>();
                                    while (dr0.Read())
                                    {
                                        InvestmentListing rSingle = new InvestmentListing();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.InstrumentType = Convert.ToString(dr0["InstrumentType"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.InvestmentPK = Convert.ToInt32(dr0["InvestmentPK"]);
                                        if (rSingle.InstrumentType == "EQUITY")
                                        {
                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);

                                        }
                                        else if (rSingle.InstrumentType == "BOND")
                                        {

                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        }
                                        else
                                        {

                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            if (rSingle.TrxTypeID != "PLACEMENT")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                        }
                                        rSingle.Volume = Convert.ToDecimal(dr0["DoneVolume"]);
                                        rSingle.Amount = Convert.ToDecimal(dr0["DoneAmount"]);
                                        rSingle.Notes = Convert.ToString(dr0["Notes"]);
                                        rSingle.EntryDealingID = Convert.ToString(dr0["DealingID"]);
                                        rSingle.PTPCode = Convert.ToString(dr0["PTPCode"]);
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                         from r in rList
                                         orderby r.FundID, r.InstrumentType
                                         group r by new { r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                         select rGroup;



                                    int incRowExcel = 1;
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                    worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",  dd-MMM-yyyy";
                                    worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_listing.ParamListDate);
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;



                                    foreach (var rsHeader in GroupByFundID)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundID;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "INS. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InstrumentType;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "TRX. TYPE :";
                                        if (rsHeader.Key.TrxTypeID == "MATURE")
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "LIQUIDATE";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TrxTypeID;
                                        }
                                        incRowExcel = incRowExcel + 1;
                                        if (rsHeader.Key.InstrumentType == "EQUITY")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Volume / Shares";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                        }
                                        else if (rsHeader.Key.InstrumentType == "BOND")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 4].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 5].Value = "Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                        }
                                        else
                                        {
                                            if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Acq Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                                worksheet.Cells[incRowExcel, 9].Value = "DealerID";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTPCode";

                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                                worksheet.Cells[incRowExcel, 9].Value = "DealerID";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTPCode";
                                            }

                                        }

                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":J" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 14;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _range = "A" + incRowExcel + ":J" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 14;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                            }
                                            //area detail
                                            if (rsHeader.Key.InstrumentType == "EQUITY")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                            }
                                            else if (rsHeader.Key.InstrumentType == "BOND")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                            }


                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                                if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.AcqDateDeposito;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.EntryDealingID;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.EntryDealingID;
                                                }


                                            }





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        _range = "A" + incRowExcel + ":J" + incRowExcel;
                                        using (ExcelRange s = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            s.Style.Font.Size = 14;
                                            s.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                        }

                                        //THICK BOX DETAIL
                                        _endRowDetail = incRowExcel - 1;
                                        worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        if (rsHeader.Key.InstrumentType != "BOND")
                                        {
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        }
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                    }

                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                    worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                    worksheet.Cells["B" + incRowExcel + ":G" + (incRowExcel + 7)].Merge = true;
                                    int _rowNotes = incRowExcel + 10;
                                    worksheet.Cells["B" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + _rowNotes + ":G" + _rowNotes].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + incRowExcel + ":B" + _rowNotes].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["G" + incRowExcel + ":G" + _rowNotes].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                    incRowExcel = incRowExcel + 13;

                                    worksheet.Cells[incRowExcel, 1].Value = "Prepare By";
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 4].Value = "Approval";
                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    incRowExcel = incRowExcel + 5;
                                    worksheet.Cells[incRowExcel, 1].Value = "(    ";
                                    worksheet.Cells[incRowExcel, 3].Value = ")";
                                    worksheet.Cells[incRowExcel, 4].Value = "(    ";
                                    worksheet.Cells[incRowExcel, 6].Value = ")";
                                    worksheet.Row(incRowExcel).PageBreak = _listing.PageBreak;

                                    string _rangeA = "A1:J" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 25;
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&34 INVESTMENT APPROVAL";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    if (_listing.DownloadMode == "PDF")
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

        public Boolean OMSBond_ListingRpt(string _userID, InvestmentListing _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (" + _listing.stringInvestmentFrom + ")";
                        }
                        else
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (0) ";
                        }

                        if (_listing.ParamFundID != "All")
                        {
                            _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo
                                        ,isnull(IV.EntryDealingID,'') DealingID
                                        ,isnull(J.PTPCode,'') PTPCode
                                        ,isnull(K.ID,'') CounterpartID
                                            ,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                                          F.ID FundID,IT.Name  InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,   
                                          IV.Amount,IV.Notes, IV.RangePrice, IV.*   
                                          from Investment IV       
                                          left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                                          left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                                          left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2
                                          left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                                          left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                                          Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3 and IT.Type in (2,5,14)
                                          " + _paramInvestmentPK + _paramFund + @" order by IV.InvestmentPK ";


                        cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                        if (_listing.ParamFundID != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                        }

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "OMSBondListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "OMSBondListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "InvestmentReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Investment Listing");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<InvestmentListing> rList = new List<InvestmentListing>();
                                    while (dr0.Read())
                                    {
                                        InvestmentListing rSingle = new InvestmentListing();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.InstrumentType = Convert.ToString(dr0["InstrumentType"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.InvestmentPK = Convert.ToInt32(dr0["InvestmentPK"]);
                                        if (rSingle.InstrumentType == "Equity Reguler")
                                        {
                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);

                                        }
                                        else if (rSingle.InstrumentType == "Corporate Bond" || rSingle.InstrumentType == "Government Bond")
                                        {

                                            rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        }
                                        else
                                        {

                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            if (rSingle.TrxTypeID != "PLACEMENT")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                        }
                                        rSingle.Volume = Convert.ToDecimal(dr0["DoneVolume"]);
                                        rSingle.Amount = Convert.ToDecimal(dr0["DoneAmount"]);
                                        rSingle.Notes = Convert.ToString(dr0["Notes"]);
                                        rSingle.EntryDealingID = Convert.ToString(dr0["DealingID"]);
                                        rSingle.PTPCode = Convert.ToString(dr0["PTPCode"]);
                                        rSingle.CounterpartID = Convert.ToString(dr0["CounterpartID"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                         from r in rList
                                         orderby r.FundID, r.InstrumentType
                                         group r by new { r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                         select rGroup;



                                    int incRowExcel = 1;
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                    worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                    worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_listing.ParamListDate);
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;



                                    foreach (var rsHeader in GroupByFundID)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundID;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "INS. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InstrumentType;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "TRX. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TrxTypeID;
                                        incRowExcel = incRowExcel + 1;
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Volume / Shares";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                            worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                            worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                        }
                                        else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 4].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 5].Value = "Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                            worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                            worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                        }
                                        else
                                        {
                                            if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Acq Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                                worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Dealer";
                                                worksheet.Cells[incRowExcel, 9].Value = "Broker";
                                                worksheet.Cells[incRowExcel, 10].Value = "PTP S-Invest";
                                            }

                                        }

                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":J" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 14;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _range = "A" + incRowExcel + ":J" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 14;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                            }
                                            //area detail
                                            if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                //worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EntryDealingID;
                                                //worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.CounterpartID;
                                                //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.PTPCode;
                                            }


                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Volume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                                if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.AcqDateDeposito;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                }


                                            }





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        _range = "A" + incRowExcel + ":J" + incRowExcel;
                                        using (ExcelRange s = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            s.Style.Font.Size = 14;
                                            s.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                        }

                                        //THICK BOX DETAIL
                                        _endRowDetail = incRowExcel - 1;
                                        worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler" || rsHeader.Key.InstrumentType == "Deposito Money Market")
                                        {
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        }
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                    }

                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                    worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                    worksheet.Cells["B" + incRowExcel + ":G" + (incRowExcel + 7)].Merge = true;
                                    int _rowNotes = incRowExcel + 10;
                                    worksheet.Cells["B" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + _rowNotes + ":G" + _rowNotes].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + incRowExcel + ":B" + _rowNotes].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["G" + incRowExcel + ":G" + _rowNotes].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                    incRowExcel = incRowExcel + 13;

                                    worksheet.Cells[incRowExcel, 1].Value = "Prepare By";
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 4].Value = "Approval";
                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    incRowExcel = incRowExcel + 5;
                                    worksheet.Cells[incRowExcel, 1].Value = "(    ";
                                    worksheet.Cells[incRowExcel, 3].Value = ")";
                                    worksheet.Cells[incRowExcel, 4].Value = "(    ";
                                    worksheet.Cells[incRowExcel, 6].Value = ")";
                                    worksheet.Row(incRowExcel).PageBreak = true;

                                    string _rangeA = "A1:J" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 11];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 25;
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&34 INVESTMENT APPROVAL";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    if (_listing.DownloadMode == "PDF")
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

        public Boolean Dealing_ListingRpt(string _userID, InvestmentListing _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _bitIsMature = "";

                        if (_listing.ParamFundID != "All")
                        {
                            _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        if (_listing.BitIsMature == true)
                        {
                            _bitIsMature = @" 
                            union all
                            Select Reference,RefNo,ValueDate,InstrumentID,InstrumentName,    
                            FundID,InstrumentType,InvestmentPK,Volume,OrderPrice,InterestPercent,TrxTypeID,DonePrice,   
                            Amount,Notes,RangePrice ,MaturityDate ,DoneVolume,DoneAmount,'',AcqDate,DealingPK,InvestmentPK,CounterpartID  
                            from InvestmentMature where Selected = 1

                        ";
                        }
                        else
                        {
                            _bitIsMature = "";
                        }

                        cmd.CommandText = cmd.CommandText = @"
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ParamListDate
                        )
                        and status = 2

                        Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                        F.ID FundID,IT.Name InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,isnull(IV.DonePrice,0) DonePrice,    
                        isnull(IV.Amount,0) Amount,IV.Notes, IV.RangePrice,IV.MaturityDate,isnull(IV.DoneVolume,0) DoneVolume
                        ,isnull(IV.DoneAmount,0) DoneAmount,IV.Notes,IV.AcqDate,IV.DealingPK,IV.InvestmentPK,isnull(C.ID,'') CounterpartID 
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2 
                        left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2         
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment = 2 and IV.StatusDealing <> 3  and IV.SelectedDealing = 1 " + _paramFund + _bitIsMature +
                        @"
                        order by FundID
                        ";


                        cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                        if (_listing.ParamFundID != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                        }

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "DealingListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "DealingListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "InvestmentReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Dealing Listing");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<InvestmentListing> rList = new List<InvestmentListing>();
                                    while (dr0.Read())
                                    {
                                        InvestmentListing rSingle = new InvestmentListing();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.InstrumentType = Convert.ToString(dr0["InstrumentType"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.InvestmentPK = Convert.ToInt32(dr0["InvestmentPK"]);
                                        rSingle.DealingPK = Convert.ToInt32(dr0["DealingPK"]);
                                        if (rSingle.InstrumentType != "Deposito Money Market")
                                        {
                                            rSingle.DoneVolume = Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["OrderPrice"]);
                                            rSingle.DonePrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        }
                                        else
                                        {
                                            rSingle.DoneVolume = Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            if (rSingle.TrxTypeID != "PLACEMENT")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                        }
                                        rSingle.DoneAmount = Convert.ToDecimal(dr0["DoneAmount"]);
                                        rSingle.Notes = Convert.ToString(dr0["Notes"]);
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);
                                        rSingle.CounterpartID = Convert.ToString(dr0["CounterpartID"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                         from r in rList
                                         orderby r.FundID, r.InstrumentType
                                         group r by new { r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                         select rGroup;



                                    int incRowExcel = 1;
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                    worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                    worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_listing.ParamListDate);
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                    foreach (var rsHeader in GroupByFundID)
                                    {

                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundID;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "INS. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InstrumentType;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "TRX. TYPE :";
                                        if (rsHeader.Key.TrxTypeID == "MATURE")
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = "LIQUIDATE";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TrxTypeID;
                                        }
                                        incRowExcel = incRowExcel + 1;
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Volume / Shares";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Done Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Done Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Counterpart";
                                        }
                                        else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond"
                                                 || rsHeader.Key.InstrumentType == "Sukuk" || rsHeader.Key.InstrumentType == "SBSN")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Done Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 7].Value = "Done Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Counterpart";
                                        }
                                        else
                                        {
                                            if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Acq Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Done Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Done Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Int.Percent";
                                            }

                                        }

                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":H" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 14;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _range = "A" + incRowExcel + ":H" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 14;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                            }
                                            //area detail
                                            if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.DealingPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CounterpartID;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond"
                                                     || rsHeader.Key.InstrumentType == "Sukuk" || rsHeader.Key.InstrumentType == "SBSN")
                                            { //
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.DealingPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CounterpartID;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }


                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.DealingPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                                if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.AcqDateDeposito;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;

                                                }


                                            }





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        _range = "A" + incRowExcel + ":H" + incRowExcel;
                                        using (ExcelRange s = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            s.Style.Font.Size = 14;
                                            s.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                        }

                                        //THICK BOX DETAIL
                                        _endRowDetail = incRowExcel - 1;
                                        worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                    }

                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                    worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                    worksheet.Cells["B" + incRowExcel + ":G" + (incRowExcel + 7)].Merge = true;
                                    int _rowNotes = incRowExcel + 10;
                                    worksheet.Cells["B" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + _rowNotes + ":G" + _rowNotes].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["B" + incRowExcel + ":B" + _rowNotes].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells["G" + incRowExcel + ":G" + _rowNotes].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                    incRowExcel = incRowExcel + 13;


                                    int _RowA = incRowExcel;
                                    int _RowB = incRowExcel + 7;
                                    worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Bold = true;
                                    worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Size = 15;

                                    incRowExcel = incRowExcel + 13;

                                    worksheet.Cells["A" + _RowA + ":B" + _RowA].Merge = true;
                                    worksheet.Cells[_RowA, 1].Value = "PrepareBy";
                                    worksheet.Cells[_RowA, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells["A" + _RowB + ":B" + _RowB].Merge = true;
                                    worksheet.Cells[_RowB, 1].Value = "(                                  )";
                                    worksheet.Cells[_RowB, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells["E" + _RowA + ":F" + _RowA].Merge = true;
                                    worksheet.Cells[_RowA, 5].Value = "Approval";
                                    worksheet.Cells[_RowA, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells["E" + _RowB + ":F" + _RowB].Merge = true;
                                    worksheet.Cells[_RowB, 5].Value = "(                               )";
                                    worksheet.Cells[_RowB, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //if (_listing.Signature1 != 0)
                                    //{
                                    //    worksheet.Cells[_RowA, 1].Value = _host.Get_PositionSignature(_listing.Signature1);
                                    //    worksheet.Cells[_RowA, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 1].Value = "( " + _host.Get_SignatureName(_listing.Signature1) + " )";
                                    //    worksheet.Cells[_RowB, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                    //}
                                    //else
                                    //{
                                    //    worksheet.Cells[_RowA, 1].Value = "";
                                    //    worksheet.Cells[_RowA, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 1].Value = "";
                                    //    worksheet.Cells[_RowB, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //}


                                    //if (_listing.Signature2 != 0)
                                    //{
                                    //    worksheet.Cells[_RowA, 3].Value = _host.Get_PositionSignature(_listing.Signature2);
                                    //    worksheet.Cells[_RowA, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 3].Value = "( " + _host.Get_SignatureName(_listing.Signature2) + " )";
                                    //    worksheet.Cells[_RowB, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //}
                                    //else
                                    //{
                                    //    worksheet.Cells[_RowA, 3].Value = _host.Get_PositionSignature(_listing.Signature2);
                                    //    worksheet.Cells[_RowA, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 3].Value = "";
                                    //    worksheet.Cells[_RowB, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //}

                                    //if (_listing.Signature3 != 0)
                                    //{
                                    //    worksheet.Cells[_RowA, 5].Value = _host.Get_PositionSignature(_listing.Signature3);
                                    //    worksheet.Cells[_RowA, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 5].Value = "( " + _host.Get_SignatureName(_listing.Signature3) + " )";
                                    //    worksheet.Cells[_RowB, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //}
                                    //else
                                    //{
                                    //    worksheet.Cells[_RowA, 5].Value = "";
                                    //    worksheet.Cells[_RowA, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 5].Value = "";
                                    //    worksheet.Cells[_RowB, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //}

                                    //if (_listing.Signature4 != 0)
                                    //{
                                    //    worksheet.Cells[_RowA, 7].Value = _host.Get_PositionSignature(_listing.Signature4);
                                    //    worksheet.Cells[_RowA, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 7].Value = "( " + _host.Get_SignatureName(_listing.Signature4) + " )";
                                    //    worksheet.Cells[_RowB, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                    //}
                                    //else
                                    //{
                                    //    worksheet.Cells[_RowA, 7].Value = "";
                                    //    worksheet.Cells[_RowA, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //    worksheet.Cells[_RowB, 7].Value = "";
                                    //    worksheet.Cells[_RowB, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //}
                                    incRowExcel = incRowExcel + 8;
                                    worksheet.Row(incRowExcel).PageBreak = true;

                                    string _rangeA = "A1:H" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 25;
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&34 DEALING TICKET";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
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


        public Boolean Settlement_ListingRpt(string _userID, InvestmentListing _listing)
        {
            #region Listing Bond
            if (_listing.ParamInstType == "2")
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (_listing.ParamFundID != "All")
                            {
                                _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText = @"Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,MV.DescOne SettlementModeDesc,MV.DescTwo SettlementModeDescTwo,BB.Name BankCustodianName,BC.ContactPerson BankCustodianContactPerson,BC.Fax1 BankCustodianFaxNo,BC.Phone1 BankCustodianPhone,C.ContactPerson,C.Fax FaxNo,C.Name CounterpartName,I.Name InstrumentName,F.Name FundName,IV.EntryUsersID CheckedBy,IV.ApprovedUsersID ApprovedBy,IV.valueDate,I.ID InstrumentID,   
                                            F.ID FundID,IT.Name InstrumentType,isnull( AcqDate1,'') AcqDate1,isnull(AcqDate2,'') AcqDate2,isnull(AcqDate3,'') AcqDate3,isnull(AcqDate4,'') AcqDate4,isnull(AcqDate5,'') AcqDate5,IV.*  
                                            from Investment IV   
                                            left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2  
                                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2  
                                            left join FundCashRef FC on IV.FundCashRefPK = FC.FundCashRefPK and FC.status = 2 
                                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2  
                                            left join MasterValue MV on IV.SettlementMode = MV.Code and MV.ID ='SettlementMode' and MV.status = 2  
                                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                                            Where  IV.ValueDate = @ParamListDate and IV.InstrumentTypePK in (2,3,9,13,15)  and IV.statusSettlement = 2  and IV.SelectedSettlement = 1
                                            " + _paramFund + @" order by RefNo ";

                            cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                            if (_listing.ParamFundID != "All")
                            {
                                cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                            }
                            cmd.Parameters.AddWithValue("@ParamInstType", _listing.ParamInstType);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SettlementListingBond" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SettlementListingBond" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "InvestmentReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Settlement Listing Bond");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<InvestmentListing> rList = new List<InvestmentListing>();
                                        while (dr0.Read())
                                        {
                                            InvestmentListing rSingle = new InvestmentListing();
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.TrxTypeID = dr0["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            rSingle.Amount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.DoneVolume = dr0["DoneVolume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.DonePrice = dr0["DonePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.DoneAmount = dr0["DoneAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.CounterpartName = dr0["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartName"]);
                                            rSingle.BankCustodianName = dr0["BankCustodianName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianName"]);
                                            rSingle.BankCustodianContactPerson = dr0["BankCustodianContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianContactPerson"]);
                                            rSingle.BankCustodianFaxNo = dr0["BankCustodianFaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianFaxNo"]);
                                            rSingle.BankCustodianPhone = dr0["BankCustodianPhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianPhone"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                            rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                            rSingle.AcqPrice = dr0["AcqPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice"]);
                                            rSingle.AcqDate = Convert.ToDateTime(dr0["AcqDate"]);
                                            rSingle.AcqPrice1 = dr0["AcqPrice1"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice1"]);
                                            rSingle.AcqDate1 = Convert.ToDateTime(dr0["AcqDate1"]);
                                            rSingle.AcqPrice2 = dr0["AcqPrice2"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice2"]);
                                            rSingle.AcqDate2 = Convert.ToDateTime(dr0["AcqDate2"]);
                                            rSingle.AcqPrice3 = dr0["AcqPrice3"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice3"]);
                                            rSingle.AcqDate3 = Convert.ToDateTime(dr0["AcqDate3"]);
                                            rSingle.AcqPrice4 = dr0["AcqPrice4"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice4"]);
                                            rSingle.AcqDate4 = Convert.ToDateTime(dr0["AcqDate4"]);
                                            rSingle.AcqPrice5 = dr0["AcqPrice5"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice5"]);
                                            rSingle.AcqDate5 = Convert.ToDateTime(dr0["AcqDate5"]);
                                            rSingle.IncomeTaxGainAmount = Convert.ToDecimal(dr0["IncomeTaxGainAmount"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);
                                            rSingle.AccruedInterest = dr0["DoneAccruedInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAccruedInterest"]);
                                            rSingle.SettlementModeDesc = dr0["SettlementModeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SettlementModeDesc"]);
                                            rSingle.SettlementModeDescTwo = dr0["SettlementModeDescTwo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SettlementModeDescTwo"]);
                                            //rSingle.TaxCapitalGainLoss = Convert.ToDecimal(dr0["TaxCapitalGainLoss"]);
                                            //rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                             from r in rList
                                             group r by new { r.FundName, r.InstrumentID } into rGroup
                                             select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            //int _rowHeader = incRowExcel;
                                            int _rowLine1 = 0;
                                            int _rowLine2 = 0;

                                            int _endRowDetail = incRowExcel;

                                            //incRowExcel++;

                                            //area header

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;


                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();

                                                int RowE = incRowExcel + 1;
                                                decimal _purchaseAmount = rsDetail.Amount;
                                                decimal _recalNetProceeds = rsDetail.TotalAmount;
                                                incRowExcel++;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = "Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstructionDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                _rowLine1 = incRowExcel;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attention ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianContactPerson;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Fax No ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianFaxNo;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "From ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyName();
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Reference ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Reference;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Re ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = "Bond Transaction -	" + rsDetail.InstrumentID;
                                                _rowLine2 = incRowExcel;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dear Sir,";
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Here with we would like to confirm having bond transaction with following details :";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bond Name ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Type of Transaction ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementModeDesc + "(" + rsDetail.SettlementModeDescTwo + ")";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Maturity Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nominal (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Price (%) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Current Coupon (%) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "CounterParty ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.CounterpartName;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                //worksheet.Cells[incRowExcel, 1].Value = "care of ";
                                                //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                //worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                //worksheet.Cells[incRowExcel, 3].Value = rsDetail.CounterpartName;
                                                //worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                //incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Trade Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Settlement Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                int RowF = incRowExcel;
                                                worksheet.Cells[incRowExcel, 1].Value = "Acq Price (%) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqPrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Acq Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                if (rsDetail.AcqPrice1 != 0)
                                                {

                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Price 1 (%) ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqPrice1;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Date 1 ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate1;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                }

                                                if (rsDetail.AcqPrice2 != 0)
                                                {

                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Price 2 (%) ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqPrice2;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Date 2 ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate2;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                }
                                                if (rsDetail.AcqPrice3 != 0)
                                                {
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Price 3 (%) ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqPrice3;
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Date 3 ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqDate3;
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;

                                                }
                                                if (rsDetail.AcqPrice4 != 0)
                                                {
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Price 4 (%) ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqPrice4;
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Date 4 ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqDate4;
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                }
                                                if (rsDetail.AcqPrice5 != 0)
                                                {
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Price 5 (%) ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqPrice5;
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Date 5 ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqDate5;
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                }

                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Purchase Amount (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = _purchaseAmount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Interest Amount (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccruedInterest;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Tax. Capital Gain (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                if (rsDetail.SettlementModeDesc == "RVP")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.IncomeTaxGainAmount * -1;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.IncomeTaxGainAmount;
                                                }

                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Net Proceeds (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = _recalNetProceeds;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                                worksheet.Cells[incRowExcel, 3].Value = _listing.Message;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells["C" + incRowExcel + ":F" + (incRowExcel + 4)].Merge = true;
                                                incRowExcel = incRowExcel + 6;
                                                worksheet.Cells[incRowExcel, 1].Value = "Please Confirm Upon Settlement";
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Check By";
                                                //worksheet.Cells["D" + incRowExcel + ":E" + (incRowExcel)].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = "Approved By";
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 1].Value = "(    ";
                                                worksheet.Cells[incRowExcel, 2].Value = ")";
                                                //worksheet.Cells[incRowExcel, 3].Value = "(    ";
                                                worksheet.Cells[incRowExcel, 4].Value = "  (";
                                                worksheet.Cells[incRowExcel, 6].Value = ")";
                                                incRowExcel++;

                                                worksheet.Cells["A" + _rowLine1 + ":G" + _rowLine1].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLine2 + ":G" + _rowLine2].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel = incRowExcel + 4;
                                                worksheet.Row(incRowExcel).PageBreak = true;
                                            }

                                            //incRowExcel++;


                                        }



                                        string _rangeA = "A1:G" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 18;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 7];
                                        worksheet.Column(1).Width = 35;
                                        worksheet.Column(2).Width = 4;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 4;
                                        worksheet.Column(5).Width = 35;
                                        worksheet.Column(6).Width = 4;
                                        worksheet.Column(7).Width = 35;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&34 SETTLEMENT LISTING BOND";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_listing.DownloadMode == "PDF")
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

            #region Listing Equity
            else if (_listing.ParamInstType == "1")
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";

                            if (_listing.ParamFundID != "All")
                            {
                                _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,C.Name CounterpartName,I.Name InstrumentName,F.Name FundName,IV.valueDate,I.ID InstrumentID,I.Name InstrumentName, 
                                            F.ID FundID,IT.Name InstrumentType,C.ID CounterpartID,BB.Name BankCustodianName,BC.ContactPerson ContactPerson,BC.Fax1 FaxNo,FC.BankAccountNo,FC.BankAccountName,IV.* 
                                            from Investment IV  
                                            left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2  
                                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2  
                                            left join FundCashRef FC on IV.FundCashRefPK = FC.FundCashRefPK and FC.status = 2 
                                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2  
                                            left join MasterValue MV on IV.SettlementMode = MV.Code and MV.ID ='SettlementMode' and MV.status = 2  
                                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2  
                                            Where  IV.ValueDate = @ParamListDate and IV.InstrumentTypePK = 1 and IV.statusSettlement = 2  and IV.SelectedSettlement = 1
                                            " + _paramFund + @" order by Refno ";

                            cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                            if (_listing.ParamFundID != "All")
                            {
                                cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                            }
                            cmd.Parameters.AddWithValue("@ParamInstType", _listing.ParamInstType);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SettlementListingEquity" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SettlementListingEquity" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "InvestmentReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Settlement Listing Equity");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<InvestmentListing> rList = new List<InvestmentListing>();
                                        while (dr0.Read())
                                        {
                                            InvestmentListing rSingle = new InvestmentListing();
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.TrxTypeID = dr0["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);
                                            rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                            rSingle.FundID = dr0["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundID"]);
                                            rSingle.InstrumentType = dr0["InstrumentType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentType"]);
                                            rSingle.CounterpartID = dr0["CounterpartID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartID"]);
                                            rSingle.ContactPerson = dr0["ContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.BankCustodianName = dr0["BankCustodianName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianName"]);
                                            rSingle.FaxNo = dr0["FaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxNo"]);
                                            rSingle.BankAccountName = dr0["BankAccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName"]);
                                            rSingle.BankAccountNo = dr0["BankAccountNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNo"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.DoneVolume = dr0["DoneVolume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.DoneAmount = dr0["DoneAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.DonePrice = dr0["DonePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.CounterpartName = dr0["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartName"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            //rSingle.PurchaseAmount = Convert.ToDecimal(dr0["PurchaseAmount"]);
                                            rSingle.AccruedInterest = dr0["AccruedInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccruedInterest"]);
                                            //rSingle.TaxCapitalGainLoss = Convert.ToDecimal(dr0["TaxCapitalGainLoss"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);
                                            rSingle.CommissionAmount = dr0["CommissionAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CommissionAmount"]);
                                            rSingle.LevyAmount = dr0["LevyAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["LevyAmount"]);
                                            rSingle.KPEIAmount = dr0["KPEIAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["KPEIAmount"]);
                                            rSingle.VATAmount = dr0["VATAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["VATAmount"]);
                                            rSingle.WHTAmount = dr0["WHTAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["WHTAmount"]);
                                            rSingle.IncomeTaxSellAmount = dr0["IncomeTaxSellAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["IncomeTaxSellAmount"]);
                                            rList.Add(rSingle);

                                        }


                                        var GroupByValueDate =
                                               from r in rList
                                               group r by new { r.ValueDate, r.FundID, r.CounterpartID, r.InstrumentType, r.TrxTypeID, r.SettlementDate, r.ContactPerson, r.FaxNo, r.BankAccountNo, r.BankAccountName, r.BankCustodianName } into rGroup
                                               select rGroup;

                                        int incRowExcel = 0;


                                        foreach (var rsHeader in GroupByValueDate)
                                        {
                                            incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1].Value = "REFERENCE";
                                            //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Reference;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 3].Value = (rsHeader.Key.ValueDate);
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Securities Acc No ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Value = rsHeader.Key.BankAccountName;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "To :";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.BankCustodianName;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Cash Acc No ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Value = rsHeader.Key.BankAccountNo;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Attention ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ContactPerson;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Instrument Type ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Value = rsHeader.Key.InstrumentType;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fax no ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FaxNo;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Trade Date ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 14].Value = (rsHeader.Key.ValueDate);
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "From ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundID;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Settlement Date ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 14].Value = (rsHeader.Key.SettlementDate);
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Re ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "SETTLEMENT INSTRUCTION";
                                            incRowExcel++;




                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Counterpart";
                                            worksheet.Cells[incRowExcel, 2].Value = "B / S";
                                            worksheet.Cells[incRowExcel, 3].Value = "Instrument";
                                            worksheet.Cells[incRowExcel, 4].Value = "Reference";
                                            worksheet.Cells[incRowExcel, 5].Value = "Quantity";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Total Price";
                                            worksheet.Cells[incRowExcel, 8].Value = "Comm.";
                                            worksheet.Cells[incRowExcel, 9].Value = "Levy";
                                            worksheet.Cells[incRowExcel, 10].Value = "VAT";
                                            worksheet.Cells[incRowExcel, 11].Value = "KPEI";
                                            if (rsHeader.Key.TrxTypeID == "BUY")
                                            {
                                                worksheet.Cells[incRowExcel, 12].Value = "Total Settle";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 12].Value = "Income Tax";
                                            }

                                            worksheet.Cells[incRowExcel, 13].Value = "WHT";
                                            worksheet.Cells[incRowExcel, 14].Value = "Total Payment";

                                            //THICK BOX HEADER
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            string _range = "A" + incRowExcel + ":N" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 18;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }
                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;



                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                decimal _totalSettleForBuy = 0;
                                                //decimal _totalSettleForSell = 0;
                                                _totalSettleForBuy = rsDetail.DoneAmount + rsDetail.CommissionAmount + rsDetail.LevyAmount + rsDetail.VATAmount + rsDetail.KPEIAmount;
                                                //_totalSettleForSell = rsDetail.DoneAmount + rsDetail.WHTAmount - rsDetail.CommissionAmount - rsDetail.LevyAmount - rsDetail.VATAmount - rsDetail.KPEIAmount - rsDetail.IncomeTaxSellAmount;
                                                _range = "A" + incRowExcel + ":N" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 18;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                                }





                                                //area detail

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.CounterpartID;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                if (rsDetail.TrxTypeID == "BUY")
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = "B";

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = "S";

                                                }
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Reference;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DoneVolume;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CommissionAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.LevyAmount;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.VATAmount;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.KPEIAmount;
                                                if (rsDetail.TrxTypeID == "BUY")
                                                {
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                    //worksheet.Cells[incRowExcel, 12].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 12].Value = _totalSettleForBuy;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.IncomeTaxSellAmount;
                                                }

                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.WHTAmount;
                                                if (rsDetail.TrxTypeID == "BUY")
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.TotalAmount;
                                                    //worksheet.Cells[incRowExcel, 14].Value = (_totalSettleForBuy - rsDetail.WHTAmount);
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.TotalAmount;
                                                    //worksheet.Cells[incRowExcel, 14].Value = _totalSettleForSell;
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;




                                            }
                                            //THICK BOX DETAIL
                                            _endRowDetail = incRowExcel - 1;
                                            worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["K" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["K" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["L" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + _startRowDetail + ":L" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["L" + _startRowDetail + ":L" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["L" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["M" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["M" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["N" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                            worksheet.Cells["N" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 3].Value = "Total " + rsHeader.Key.TrxTypeID + " (IDR) :";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";

                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 12].Value = "Net Proceeds " + rsHeader.Key.TrxTypeID + " (IDR) :";
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                            worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells["B" + incRowExcel + ":N" + (incRowExcel + 2)].Merge = true;
                                            incRowExcel = incRowExcel + 4;
                                            worksheet.Cells[incRowExcel, 3].Value = "Check By";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells["F" + incRowExcel + ":M" + incRowExcel].Merge = true;
                                            incRowExcel = incRowExcel + 5;
                                            worksheet.Cells[incRowExcel, 3].Value = "(    ";
                                            worksheet.Cells[incRowExcel, 5].Value = ")";
                                            //worksheet.Cells[incRowExcel, 6].Value = "(    ";
                                            worksheet.Cells[incRowExcel, 8].Value = "(";
                                            worksheet.Cells[incRowExcel, 11].Value = ")";
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            //worksheet.Cells[incRowExcel, 14].Value = ")";
                                            worksheet.Row(incRowExcel).PageBreak = true;





                                        }


                                        string _rangeA = "A1:N" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 18;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 15];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 25;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).Width = 15;
                                        worksheet.Column(11).Width = 15;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 30;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&34 SETTLEMENT LISTING EQUITY";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_listing.DownloadMode == "PDF")
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

            #region Listing Deposito
            else if (_listing.ParamInstType == "3")
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _bitIsMature = "";
                            string _paramFund = "";

                            if (_listing.ParamFundID != "All")
                            {
                                _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (_listing.BitIsMature == true)
                            {
                                _bitIsMature = @"union all

                                Select  Reference,Tenor,ClearingCode,BankAccountNo,BankCustodianName,BankCustodianContactPerson,BankCustodianFaxNo,BankCustodianPhone,ContactPerson,FaxNo,Phone,InstrumentName,FundName,valueDate,InstrumentID,  
                                FundID,InstrumentType,BankBranchName ,TrxTypeID,InstructionDate,MaturityDate,Amount,DoneAmount,OrderPrice,InterestPercent,ValueDate,AcqDate,AccruedInterest,SettlementDate 
                                from InvestmentMature where Selected = 1 ";
                            }
                            else
                            {
                                _bitIsMature = "";
                            }


                            cmd.CommandText = @"
                            Declare @TrailsPK int
                            Declare @MaxDateEndDayFP datetime

                            select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ParamListDate
                            )
                            and status = 2

                            Select Reference, DATEDIFF (day,IV.ValueDate ,IV.MaturityDate ) Tenor,BB.ClearingCode,BC.BankAccountNo,BB.Name BankCustodianName,BC.ContactPerson BankCustodianContactPerson,BC.Fax1 BankCustodianFaxNo,BC.Phone1 BankCustodianPhone,B.ContactPerson,B.Fax1 FaxNo,B.Phone1 Phone,I.Name InstrumentName,F.Name FundName,IV.valueDate,I.ID InstrumentID,  
                            F.ID FundID,IT.Name InstrumentType,C.Name BankBranchName,IV.TrxTypeID,IV.InstructionDate,IV.MaturityDate,IV.Amount,IV.DoneAmount,IV.OrderPrice,IV.InterestPercent,IV.ValueDate,IV.AcqDate,IV.AccruedInterest ,IV.SettlementDate 
                            from Investment IV   
                            left join BankBranch B on IV.BankBranchPK = B.BankBranchPK and B.status = 2   
                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2  
                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2  
                            left join MasterValue MV on IV.SettlementMode = MV.Code and MV.ID ='SettlementMode' and MV.status = 2  
                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2
                            left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                            Where  IV.ValueDate = @ParamListDate and IV.InstrumentTypePK = 5 and IV.statusSettlement = 2  
                            " + _paramFund + @" 
                            and IV.selectedSettlement = 1 
                             " + _bitIsMature;

                            cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                            if (_listing.ParamFundID != "All")
                            {
                                cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                            }
                            cmd.Parameters.AddWithValue("@ParamInstType", _listing.ParamInstType);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SettlementListingDeposito" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SettlementListingDeposito" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "InvestmentReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Settlement Listing Deposito");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<InvestmentListing> rList = new List<InvestmentListing>();
                                        while (dr0.Read())
                                        {
                                            InvestmentListing rSingle = new InvestmentListing();
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.TrxTypeID = dr0["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);
                                            rSingle.Reference = dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.InstrumentType = dr0["InstrumentType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentType"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            rSingle.Amount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.DoneAmount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.OrderPrice = dr0["OrderPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["OrderPrice"]);
                                            rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.BankBranchName = dr0["BankBranchName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName"]);
                                            rSingle.ContactPerson = dr0["ContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.FaxNo = dr0["FaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxNo"]);
                                            rSingle.Phone = dr0["Phone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Phone"]);
                                            rSingle.BankCustodianName = dr0["BankCustodianName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianName"]);
                                            rSingle.BankCustodianContactPerson = dr0["BankCustodianContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianContactPerson"]);
                                            rSingle.BankCustodianFaxNo = dr0["BankCustodianFaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianFaxNo"]);
                                            rSingle.BankCustodianPhone = dr0["BankCustodianPhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianPhone"]);
                                            rSingle.BankAccountNo = dr0["BankAccountNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNo"]);
                                            rSingle.ClearingCode = dr0["ClearingCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ClearingCode"]);
                                            rSingle.Tenor = dr0["Tenor"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["Tenor"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            if (rSingle.TrxTypeID == "LIQUIDATE")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                            else if (rSingle.TrxTypeID == "ROLLOVER" || rSingle.TrxTypeID == "PLACEMENT")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["ValueDate"]);
                                            }
                                            else
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                            //rSingle.PurchaseAmount = Convert.ToDecimal(dr0["PurchaseAmount"]);
                                            rSingle.AccruedInterest = dr0["AccruedInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccruedInterest"]);
                                            //rSingle.TaxCapitalGainLoss = Convert.ToDecimal(dr0["TaxCapitalGainLoss"]);
                                            //rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundName, r.InstrumentID } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {


                                            int _rowLine1 = 0;
                                            int _rowLine2 = 0;
                                            int _rowLineA = 0;
                                            int _rowLineB = 0;
                                            int _rowLineC = 0;
                                            int _rowLineD = 0;
                                            int _rowLineE = 0;
                                            int _rowLineF = 0;
                                            int _rowLineG = 0;
                                            int _endRowDetail = incRowExcel;

                                            //incRowExcel++;



                                            //area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                incRowExcel++;
                                                int _rowHeader = incRowExcel;
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                decimal _purchaseAmount = (rsDetail.Amount * rsDetail.OrderPrice);
                                                decimal _recalNetProceeds = (_purchaseAmount + rsDetail.AccruedInterest);

                                                //worksheet.Cells["A" + _rowHeader + ":F" + _rowHeader].Merge = true;               
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;

                                                worksheet.Cells["A" + _rowHeader + ":F" + _rowHeader].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowHeader + ":F" + _rowHeader].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowHeader + ":A" + _rowHeader].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + _rowHeader + ":F" + _rowHeader].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                incRowExcel++;
                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianContactPerson;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Fax / Telp ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianFaxNo;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.BankCustodianPhone;
                                                _rowLine1 = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankBranchName;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ContactPerson;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Fax / Telp ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                //worksheet.Cells[incRowExcel, 3].Value = rsDetail.FaxNo;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Phone;
                                                _rowLine2 = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel = incRowExcel + 1;
                                                worksheet.Cells[incRowExcel, 1].Value = "From ";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyName();
                                                worksheet.Cells[incRowExcel, 5].Value = "Reference : ";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Reference;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Date ";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "TIME DEPOSIT";
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Please execute the following instruction :";
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Benefary Bank ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankBranchName;
                                                worksheet.Cells[incRowExcel, 5].Value = "Clearing Code";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.ClearingCode;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                //worksheet.Row(incRowExcel).Height = 22;
                                                _rowLineA = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "In Favoring of ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                //worksheet.Row(incRowExcel).Height = 22;
                                                _rowLineB = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Amount (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                //worksheet.Row(incRowExcel).Height = 22;
                                                _rowLineC = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Value Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                if (rsDetail.TrxTypeID == "PLACEMENT")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementDate;
                                                }
                                                else if (rsDetail.TrxTypeID == "LIQUIDATE")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDateDeposito;
                                                }
                                                else if (rsDetail.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDateDeposito;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDateDeposito;
                                                }

                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5].Value = "Interest (%) ";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Row(incRowExcel).Height = 22;
                                                _rowLineD = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Maturity Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5].Value = "Tenor Days ";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Tenor;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Row(incRowExcel).Height = 22;
                                                _rowLineE = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                if (rsDetail.TrxTypeID == "PLACEMENT")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "NEW PLACEMENT";
                                                }
                                                else if (rsDetail.TrxTypeID == "LIQUIDATE")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "LIQUIDATE";
                                                }
                                                else if (rsDetail.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "ROLLOVER";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "LIQUIDATE";
                                                }

                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Notes ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells["C" + incRowExcel + ":F" + (incRowExcel + 4)].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _listing.Message;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;

                                                var _principalText = "";
                                                if (rsDetail.TrxTypeID == "LIQUIDATE")
                                                {
                                                    _principalText = "Principal and Interest";
                                                }
                                                else
                                                {
                                                    _principalText = "Interest";
                                                }
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Upon maturity, please transfer the " + _principalText + " to : ";
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bank Name ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                _rowLineF = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Account Name ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Account Number ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankAccountNo;
                                                _rowLineG = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                incRowExcel = incRowExcel + 4;
                                                worksheet.Cells[incRowExcel, 1].Value = "Sincerely Yours";
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = "Acknowledged by";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = "Confirmed by";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = "Verified by";
                                                worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                incRowExcel = incRowExcel + 7;
                                                worksheet.Cells[incRowExcel, 1].Value = "     (                                  )          ";
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = "  (                                           )    ";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = "      (                                     )      ";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5].Value = "          (";
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6].Value = "                  )";
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                worksheet.Cells["A" + _rowLine1 + ":F" + _rowLine1].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLine2 + ":F" + _rowLine2].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //BORDER 
                                                worksheet.Cells["A" + _rowLineA + ":F" + _rowLineA].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineA + ":F" + _rowLineA].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineB + ":F" + _rowLineB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineC + ":F" + _rowLineC].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineD + ":F" + _rowLineD].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineE + ":F" + _rowLineE].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells["A" + _rowLineA + ":A" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["C" + _rowLineA + ":C" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["D" + _rowLineA + ":D" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["E" + _rowLineA + ":E" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + _rowLineA + ":F" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["G" + _rowLineA + ":G" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells["C" + _rowLineF + ":E" + _rowLineF].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["C" + _rowLineF + ":C" + _rowLineG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["E" + _rowLineF + ":E" + _rowLineG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["C" + _rowLineG + ":E" + _rowLineG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                                worksheet.Row(incRowExcel).PageBreak = true;

                                                //incRowExcel++;
                                            }






                                            string _rangeA = "A1:F" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 16;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            }
                                        }




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 6];
                                        worksheet.Column(1).Width = 35;
                                        worksheet.Column(2).Width = 4;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 35;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&34 SETTLEMENT LISTING BOND";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_listing.DownloadMode == "PDF")
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

        public Boolean Print_Ojk(string _userID, OjkRpt _OjkRpt)
        {
            int rowcell = 0;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    //buat generate report dimasukin ke table FundClientRpt
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFundClient = "";
                        //if (_OjkRpt.FundClientFrom != "0")
                        //{
                        //    _paramFundClient = "and FundClientPK in ( " + _OjkRpt.FundClientFrom + " ) ";
                        //}
                        //else
                        //{
                        //    _paramFundClient = "";
                        //}

                        if (!_host.findString(_OjkRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_OjkRpt.FundClientFrom))
                        {
                            _paramFundClient = "And FundClientPK in ( " + _OjkRpt.FundClientFrom + " ) ";
                        }
                        else
                        {
                            _paramFundClient = "";
                        }


                        cmd.CommandText = @"Declare @tempTable table
                                            (
                    	                        FundClientPK int,
                    	                        Date Datetime,
                    	                        Reason nvarchar(max)
                                            )
                    
                                            Declare @tempTableBlackList table
                                            (
                    	                        FieldName nvarchar(100),
                    	                        Name nvarchar(200),
                    	                        HighRiskName nvarchar(200),
                    	                        HighRiskDesc nvarchar(1000),
                    	                        Percentage numeric(18,0)
                                            )
                    
                                            Declare @FundClientPK int
                                            Declare @CheckInsert int
                                            Declare @StatusCountryRisk int
                                            Declare @StatusOccupationRisk int
                                            Declare @StatusPoliticallyRisk int
                                            Declare @StatusBusinessRisk int
                                            Declare @StatusClientNameRisk int
                                            Declare @StatusSpouseNameRisk int
                                            Declare @StatusRDNNameRisk int
                                            Declare @ClientID nvarchar(1000)
                                            Declare @CountryRiskDesc nvarchar(1000)
                                            Declare @OccupationRiskDesc nvarchar(1000)
                                            Declare @PoliticallyRiskDesc nvarchar(1000)
                                            Declare @BusinessRiskDesc nvarchar(1000)
                                            Declare @ClientNameRiskDesc nvarchar(1000)
                                            Declare @SpouseNameRiskDesc nvarchar(1000)
                                            Declare @RDNNameRiskDesc nvarchar(1000)
                                            Declare @HrBusinessCode int
                                            Declare @HrPEPCode int
                                            Declare @HrOccupation int
                                            Declare @CountryCode nvarchar(10)
                                            Declare @BlackListName nvarchar(200)
                                            Declare @BlackListDesc nvarchar(1000)
                                            Declare @Name nvarchar(200)
                                            Declare @InvestorFirstName nvarchar(200)
                                            Declare @InvestorMiddleName nvarchar(200)
                                            Declare @InvestorLastName nvarchar(200)
                                            Declare @InvestorSpouseName nvarchar(200)
                                            Declare @investorAuthorizedPersonFirstName1 nvarchar(200)
                                            Declare @InvestorAuthorizedPersonMiddleName1 nvarchar(200)
                                            Declare @InvestorAuthorizedPersonLastName1 nvarchar(200)
                                            Declare @FinalReason nvarchar(max)
                                            set @FinalReason = 'Rincian :'
                                            set @CheckInsert = 0;
                                            set @StatusBusinessRisk = 0;
                                            set @StatusClientNameRisk = 0;
                                            set @StatusCountryRisk = 0;
                                            set @StatusOccupationRisk = 0;
                                            set @StatusPoliticallyRisk = 0;
                                            set @StatusRDNNameRisk = 0;
                                            set @StatusSpouseNameRisk = 0
                                            set @CountryRiskDesc = ''
                                            set @OccupationRiskDesc = ''
                                            set @PoliticallyRiskDesc = ''
                                            set @BusinessRiskDesc = ''
                                            set @ClientNameRiskDesc = ''
                                            set @SpouseNameRiskDesc = ''
                                            set @RDNNameRiskDesc = ''
                    
                                            truncate table FundClientRpt
                    
                                            Declare BAB cursor For
                    	                        Select FundClientPK
                    	                        From FundClient
                    	                        where Status in (1,2) " + _paramFundClient + @"
                                            Open BAB
                                            Fetch Next from BAB
                                            into @FundClientPK
                    
                                            WHILE @@FETCH_STATUS = 0  
                                            BEGIN 
                    
                    	                        set @FinalReason = 'Rincian :'
                    	                        set @CheckInsert = 0;
                    	                        set @StatusBusinessRisk = 0;
                    	                        set @StatusClientNameRisk = 0;
                    	                        set @StatusCountryRisk = 0;
                    	                        set @StatusOccupationRisk = 0;
                    	                        set @StatusPoliticallyRisk = 0;
                    	                        set @StatusRDNNameRisk = 0;
                    	                        set @StatusSpouseNameRisk = 0
                    	                        set @CountryRiskDesc = ''
                    	                        set @OccupationRiskDesc = ''
                    	                        set @PoliticallyRiskDesc = ''
                    	                        set @BusinessRiskDesc = ''
                    	                        set @ClientNameRiskDesc = ''
                    	                        set @SpouseNameRiskDesc = ''
                    	                        set @RDNNameRiskDesc = ''
                    
                    	                        Select @HrBusinessCode = NatureOfBusiness ,@HrPEPCode = Politis,@CountryCode = Negara ,@Name = Name,
                    	                        @InvestorFirstName = NamaDepanInd,@InvestorMiddleName = NamaTengahInd,@InvestorLastName = NamaBelakangInd,
                    	                        @InvestorSpouseName = SpouseName,@investorAuthorizedPersonFirstName1 = NamaDepanIns1,
                    	                        @InvestorAuthorizedPersonMiddleName1 = NamaTengahIns1,@InvestorAuthorizedPersonLastName1 = NamaBelakangIns1,
                    	                        @HrOccupation = Pekerjaan,@ClientID = ID
                    	                        From FundClient Where FundClientPK = @FundClientPK
                    	                        and Status in (1,2)
                    
                    	                        IF @HrBusinessCode <> 99
                    	                        BEGIN
                    		                        select @FinalReason = @FinalReason + CHAR(10) + 'Masuk dalam category High risk Business : ' + left(DescOne,len(DescOne)),@BusinessRiskDesc = left(DescOne,len(DescOne)),@CheckInsert = @CheckInsert + 1,@StatusBusinessRisk = 1 from MasterValue where Status = 2 and ID = 'HrBusiness' and Code = @HrBusinessCode
                    	                        END			
                    
                    	                        IF @HrPEPCode <> 99
                    	                        BEGIN
                    		                        select @FinalReason = @FinalReason + CHAR(10) + 'Masuk dalam category High risk Politically Exposed Person : ' + left(DescOne,len(DescOne)),@PoliticallyRiskDesc = left(DescOne,len(DescOne)),@CheckInsert = @CheckInsert + 1,@StatusPoliticallyRisk = 1 from MasterValue where Status = 2 and ID = 'HrPEP' and Code = @HrPEPCode
                    	                        END			
                    
                    	                        IF @HrOccupation = 5
                    	                        BEGIN
                    		                        select @FinalReason = @FinalReason + CHAR(10) + 'Masuk dalam category High risk Occupation : ' + left(DescOne,len(DescOne)),@OccupationRiskDesc = left(DescOne,len(DescOne)),@CheckInsert = @CheckInsert + 1,@StatusOccupationRisk = 1 from MasterValue where Status = 2 and ID = 'Occupation' and Code = @HrBusinessCode
                    	                        END	 
                    
                    	                        IF Exists(Select * from MasterValue where Status = 2 and ID = 'KSEICountry' and Code = @CountryCode and IsHighRisk = 1)
                    	                        BEGIN
                    		                        select @FinalReason = @FinalReason + CHAR(10) + 'Masuk dalam category High risk Country : ' + left(DescOne,len(DescOne)),@CountryRiskDesc = left(DescOne,len(DescOne)),@CheckInsert = @CheckInsert + 1,@StatusCountryRisk = 1 from MasterValue where Status = 2 and ID = 'KSEICountry' and Code = @CountryCode and IsHighRisk = 1
                    	                        END
                    
                    	                        delete from @tempTableBlackList
                    
                    	                        Declare A Cursor For 
                    		                            Select NameAlias,Description + ' || ' + NoDoc From BlackListName Where status = 2 and len(rtrim(ltrim(NameAlias))) > 1
                                                        UNION ALL
                                                        Select Name,Description + ' || ' + NoDoc From BlackListName A Where status = 2 and len(rtrim(ltrim(Name))) > 1 and isnull(A.Name,'') <> '' and len(rtrim(ltrim(a.Description))) > 3 and A.Type <> 0
                    
                    	                        Open A
                    
                    	                        Fetch Next From A
                    	                        into @BlackListName,@BlackListDesc
                    
                    	                        WHILE @@FETCH_STATUS = 0
                    	                        BEGIN
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'name' FieldName,@Name,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@Name,@BlackListName) Percentage
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'First name' FieldName,@InvestorFirstName,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@InvestorFirstName,@BlackListName) Percentage
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'Middle name' FieldName,@InvestorMiddleName,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@InvestorMiddleName,@BlackListName) Percentage
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'Last name' FieldName,@InvestorLastName,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@InvestorLastName,@BlackListName) Percentage 
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'Spouse name' FieldName,@InvestorSpouseName,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@InvestorSpouseName,@BlackListName) Percentage 
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'Authorized First name' FieldName,@investorAuthorizedPersonFirstName1,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@investorAuthorizedPersonFirstName1,@BlackListName) Percentage 
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'Authorized Middle name' FieldName,@InvestorAuthorizedPersonMiddleName1,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@InvestorAuthorizedPersonMiddleName1,@BlackListName) Percentage 
                    
                    		                        Insert into @tempTableBlackList
                    		                        select'Authorized Last name' FieldName,@InvestorAuthorizedPersonLastName1,@BlackListName, @BlackListDesc, dbo.[FGetPercentageOfTwoStringMatching](@InvestorAuthorizedPersonLastName1,@BlackListName) Percentage 
                    
                    		                        FETCH NEXT FROM A 
                    		                        INTO @BlackListName,@BlackListDesc
                    
                    	                        END 
                    	                        CLOSE A;
                    	                        DEALLOCATE A;
                    
                    	                        select @FinalReason = @FinalReason + CHAR(10) + 'Masuk dalam category High Risk ' + FieldName + ' | Fund Client Name : ' + Name + ' | High Risk Name : ' + HighRiskName + ' | Keterangan : ' + HighRiskDesc + ' | Percentage : '
                    	                        + cast(Percentage as nvarchar(10)) + '%  '   
                    	                        from @tempTableBlackList
                    	                        where Percentage > 70
                    
                    	                        select @SpouseNameRiskDesc = 'High Risk Name : ' + HighRiskName + ' | Keterangan : ' + HighRiskDesc + ' | Percentage : '
                    	                        + cast(Percentage as nvarchar(10)) + '% ' ,@CheckInsert = @CheckInsert + 1,@StatusSpouseNameRisk = 1 from @tempTableBlackList where Percentage > 70 and FieldName = 'Spouse name'
                    	                        select @ClientNameRiskDesc = @ClientNameRiskDesc + CHAR(10) + 'Masuk dalam category High Risk : ' + FieldName + ' | High Risk Name : ' + HighRiskName + ' | Keterangan : ' + HighRiskDesc + ' | Percentage : '
                    	                        + cast(Percentage as nvarchar(10)) + '% ' ,@CheckInsert = @CheckInsert + 1,@StatusClientNameRisk = 1 from @tempTableBlackList where Percentage > 70 and FieldName not in ('Spouse name')
                    
                    	                        Declare @HighRiskMonitoringPK int
                    	                        Select @HighRiskMonitoringPK = Max(HighRiskMonitoringPK) + 1 From HighRiskMonitoring 
                    
                    	                        set @HighRiskMonitoringPK = isnull(@HighRiskMonitoringPK,1)
                    
                    	                        if (@CheckInsert > 0)
                    	                        begin
                    		                        insert into FundClientRpt (ClientName,ID,CountryRisk,CountryRiskDesc,OccupationRisk,OccupationRiskDesc,PoliticallyRisk,
                    		                        PoliticallyRiskDesc,BusinessRisk,BusinessRiskDesc,ClientNameRisk,ClientNameRiskDesc,SpouseNameRisk,SpouseNameRiskDesc,RDNNameRisk,RDNNameRiskDesc)
                    		                        select @Name ClientName,@ClientID ClientID,
                    		                        case when @StatusCountryRisk = 0 then '' else 'X' end CountryRisk, @CountryRiskDesc CountryRiskDesc,
                    		                        case when @StatusOccupationRisk = 0 then '' else 'X' end OccupationRisk, @OccupationRiskDesc OccupationRiskDesc,
                    		                        case when @StatusPoliticallyRisk = 0 then '' else 'X' end PoliticallyRisk, @PoliticallyRiskDesc PoliticallyRiskDesc,
                    		                        case when @StatusBusinessRisk = 0 then '' else 'X' end BusinessRisk, @BusinessRiskDesc BusinessRiskDesc,
                    		                        case when @StatusClientNameRisk = 0 then '' else 'X' end ClientNameRisk, @ClientNameRiskDesc ClientNameRiskDesc,
                    		                        case when @StatusSpouseNameRisk = 0 then '' else 'X' end SpouseNameRisk, @SpouseNameRiskDesc SpouseNameRiskDesc,
                    		                        case when @StatusRDNNameRisk = 0 then '' else 'X' end RDNNameRisk, @RDNNameRiskDesc RDNNameRiskDesc
                    	                        end
                    	
                    	
                    	                        FETCH NEXT FROM BAB  
                                                INTO @FundClientPK
                                            END   
                                            CLOSE BAB
                                            DEALLOCATE BAB";
                        //cmd.ExecuteNonQuery();
                        cmd.CommandTimeout = 0;
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select * from FundCLientRpt";
                        cmd.CommandTimeout = 0;
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "Ojk_" + _userID + ".xlsx";
                                int incRowExcel = 0;
                                int _startRowDetail, _endRowDetail;
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "Ojk";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Cash Projection");

                                    List<OjkRpt> rList = new List<OjkRpt>();
                                    while (dr0.Read())
                                    {
                                        OjkRpt rSingle = new OjkRpt();

                                        rSingle.ID = Convert.ToString(dr0["ID"]);
                                        rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                        rSingle.CountryRisk = Convert.ToString(dr0["CountryRisk"]);
                                        rSingle.CountryRiskDesc = Convert.ToString(dr0["CountryRiskDesc"]);
                                        rSingle.OccupationRisk = Convert.ToString(dr0["OccupationRisk"]);
                                        rSingle.OccupationRiskDesc = Convert.ToString(dr0["OccupationRiskDesc"]);
                                        rSingle.PoliticallyRisk = Convert.ToString(dr0["PoliticallyRisk"]);
                                        rSingle.PoliticallyRiskDesc = Convert.ToString(dr0["PoliticallyRiskDesc"]);
                                        rSingle.BusinessRisk = Convert.ToString(dr0["BusinessRisk"]);
                                        rSingle.BusinessRiskDesc = Convert.ToString(dr0["BusinessRiskDesc"]);
                                        rSingle.ClientNameRisk = Convert.ToString(dr0["ClientNameRisk"]);
                                        rSingle.ClientNameRiskDesc = Convert.ToString(dr0["ClientNameRiskDesc"]);
                                        rSingle.SpouseNameRisk = Convert.ToString(dr0["SpouseNameRisk"]);
                                        rSingle.SpouseNameRiskDesc = Convert.ToString(dr0["SpouseNameRiskDesc"]);
                                        rSingle.RdnNameRisk = Convert.ToString(dr0["RdnNameRisk"]);
                                        rSingle.RdnNameRiskDesc = Convert.ToString(dr0["RdnNameRiskDesc"]);

                                        rList.Add(rSingle);
                                    }

                                    var GroupByReference =
                                        from r in rList
                                        group r by new { r.ID, r.ClientName } into rGroup
                                        select rGroup;
                                    foreach (var rsHeader in GroupByReference)
                                    {

                                        incRowExcel++;
                                        _startRowDetail = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Plaza Mutiara Mega Kuningan, 12th Floor Suite 1203";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jl. DR. Ide Anak Agung Gde Agung Kav E.1.2 No 1 & 2";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jakarta 12950, Indonesia";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Tel : (+6221) 2903 8990";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fax : (+6221) 2903 8991";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        _endRowDetail = incRowExcel;

                                        worksheet.Cells[_startRowDetail, 4].Value = "CLIENT VALIDATION HIGH RISK";
                                        worksheet.Cells["D" + _startRowDetail + ":F" + _endRowDetail].Merge = true;
                                        worksheet.Cells[_startRowDetail, 4].Style.WrapText = true;
                                        worksheet.Cells[_startRowDetail, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[_startRowDetail, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        string _range = "D" + _startRowDetail + ":F" + _endRowDetail;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Size = 22;
                                        }
                                        incRowExcel++;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Investor Code";
                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ID;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Value = "Client Name";
                                        worksheet.Cells[incRowExcel, 5].Value = ":";
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.ClientName;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;

                                        incRowExcel++;


                                        //string _range = "A" + incRowExcel + ":I" + incRowExcel;
                                        //using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        //{
                                        //    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        //    r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                        //    r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                        //    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        //    r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                        //    r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                        //    r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                        //    r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                        //    r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        //}
                                        incRowExcel++;

                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _startRowDetail = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "CountryRisk";
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.CountryRisk;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.CountryRiskDesc;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "OccupationRisk";
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.OccupationRisk;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.OccupationRiskDesc;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "PoliticallyRisk";
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.PoliticallyRisk;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.PoliticallyRiskDesc;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "BusinessRisk";
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.BusinessRisk;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.BusinessRiskDesc;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "ClientNameRisk";
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientNameRisk;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientNameRiskDesc;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Row(incRowExcel).Height = 150;
                                            worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "SpouseNameRisk";
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.SpouseNameRisk;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.SpouseNameRiskDesc;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Row(incRowExcel).Height = 80;
                                            worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "RDNNameRisk : ";
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.RdnNameRisk;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.RdnNameRiskDesc;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;


                                            worksheet.Cells["A" + _startRowDetail + ":B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail + ":B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + _startRowDetail + ":C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _endRowDetail + ":C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + _startRowDetail + ":F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail + ":F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }
                                        incRowExcel++;
                                        rowcell = incRowExcel;
                                        worksheet.Row(incRowExcel).PageBreak = true;
                                    }

                                    //string _range = "A" + incRowExcel + ":C" + incRowExcel;
                                    //using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    //{
                                    //    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                    //    r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                    //    r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                    //    r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                    //}
                                    //incRowExcel++;




                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, rowcell, 6];
                                    worksheet.Cells.AutoFitColumns(0);
                                    worksheet.Column(1).Width = 17;
                                    worksheet.Column(2).Width = 5;
                                    worksheet.Column(3).Width = 24;
                                    worksheet.Column(4).Width = 20;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Client Validation High Risk";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

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

        public string FundClient_GenerateNKPD(DateTime _date)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
             declare @FinalDate datetime
set @FinalDate = dbo.FWorkingDay(@Date ,-1)

--drop table #Text --
create table #Text(                    
[ResultText] [nvarchar](1000)  NULL                    
)                     
                
insert into #Text   
                 

SELECT  RTRIM(LTRIM(isnull(FU.NKPDName,'')))             
+ '|' + RTRIM(LTRIM(isnull(AAA.NKPDCode,'')))         
+ '|' + RTRIM(LTRIM(isnull(A.jumlahPerorangan,0)))
+ '|' + CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
+ '|' + RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    
+ '|' + CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    
+ '|' + CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
+ '|' + RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    
+ '|' + CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(I.jumlahBank,0)))        
+ '|' + CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
+ '|' + RTRIM(LTRIM(isnull(K.jumlahPT,0)))     
+ '|' + CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                    
+ '|' + RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     
+ '|' + CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
+ '|' + RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        
+ '|' + CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
+ '|' + RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  
+ '|' + CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     
+ '|' + CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
+ '|' + RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0)))
+ '|' + CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))             
            
------ASING            
+ '|' + RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0)))      
+ '|' + CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     
+ '|' + CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0)))  
+ '|' + CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        
+ '|' + CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
+ '|' + RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   
+ '|' + CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                        
+ '|' + RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) 
+ '|' + CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                         
+ '|' + RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    
+ '|' + CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   
+ '|' + CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
+ '|' + RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  
+ '|' + CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
+ '|' + RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       
+ '|' + CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           
+ '|' + CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) 
+ '|' +   CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30))    
	       
+ '|' + CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30))  
             
FROM FundClientPosition FCP (NOLOCK)                      
LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in (1,2)         
LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status in (1,2)
LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status in (1,2)
----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status in (1,2)       
LEFT JOIN             
(            
select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
left join FundClient g            
on CS.FundClientPK = g.FundClientPK   and g.Status in (1,2)       
where g.InvestorType = 1 and g.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 1          
and g.SACode = ''            
group by CS.FundPK            
) A On FCP.FundPK = A.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG        
on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)      
where CG.InvestorType = 1 and CG.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0   
and CG.SACode = ''            
            
group by CS.FundPK            
) B On FCP.FundPK = B.FundPK            
             
LEFT JOIN             
(            
----------EFEK----------------        
select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
and CG.SACode = ''                 
            
group by CS.FundPK            
) C On FCP.FundPK = C.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''                
            
group by CS.FundPK            
) D On FCP.FundPK = D.FundPK            
             
             
LEFT JOIN             
(            
---------DANA PENSIUN-------------        
select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
and CG.SACode = ''              
            
group by CS.FundPK            
) E On FCP.FundPK = E.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
and CG.SACode = ''                  
            
group by CS.FundPK            
) F On FCP.FundPK = F.FundPK            
             
LEFT JOIN             
(            
----------ASURANSI-----------        
select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
and CG.SACode = ''                  
            
group by CS.FundPK            
) G On FCP.FundPK = G.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)    
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4          
and CG.SACode = ''              
            
group by CS.FundPK            
) H On FCP.FundPK = H.FundPK            
             
LEFT JOIN             
(            
------------BANK-----------        
select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)           
and CG.SACode = ''              
            
group by CS.FundPK            
) I On FCP.FundPK = I.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)          
and CG.SACode = ''              
            
group by CS.FundPK            
) J On FCP.FundPK = J.FundPK            
             
LEFT JOIN             
(            
--------PEURSAHAAN SWASTA-----------        
select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)          
and CG.SACode = ''            
            
group by CS.FundPK            
) K On FCP.FundPK = K.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)       
and CG.SACode = ''             
            
group by CS.FundPK            
) L On FCP.FundPK = L.FundPK            
             
LEFT JOIN             
(            
---------------BUMN----------------        
select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1           
and CG.SACode = ''            
            
group by CS.FundPK            
) M On FCP.FundPK = M.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1 
and CG.SACode = ''                         
            
group by CS.FundPK            
) N On FCP.FundPK = N.FundPK            
             
LEFT JOIN             
(            
-------------BUMD-------------        
select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8      
and CG.SACode = ''                   
            
group by CS.FundPK            
) O On FCP.FundPK = O.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8    
and CG.SACode = ''                     
            
group by CS.FundPK            
) P On FCP.FundPK = P.FundPK            
             
LEFT JOIN             
(            
-----YAYASAN-----------        
select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2            
and CG.SACode = ''            
            
group by CS.FundPK            
) Q On FCP.FundPK = Q.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2       
and CG.SACode = ''                
            
group by CS.FundPK            
) R On FCP.FundPK = R.FundPK            
             
LEFT JOIN             
(            
------------KOPERASI--------------        
select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''                     
            
group by CS.FundPK            
) S On FCP.FundPK = S.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''                   
            
group by CS.FundPK            
) T On FCP.FundPK = T.FundPK    

					
------------LEMBAGA LAINNYA--------------            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8           
and CG.SACode = ''             
            
group by CS.FundPK            
) U On FCP.FundPK = U.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8         
and CG.SACode = ''               
            
group by CS.FundPK            
) V On FCP.FundPK = V.FundPK            
             
----ASING            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0            
and CG.SACode = ''            
            
group by CS.FundPK            
) AA On FCP.FundPK = AA.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)       
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0             
and CG.SACode = ''            
            
group by CS.FundPK            
) AB On FCP.FundPK = AB.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)   
and CG.SACode = ''                    
            
group by CS.FundPK          
) AC On FCP.FundPK = AC.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)    
and CG.SACode = ''                  
            
group by CS.FundPK            
) AD On FCP.FundPK = AD.FundPK            
             
             
LEFT JOIN             
(            
select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)    
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6           
and CG.SACode = ''            
            
group by CS.FundPK            
) AE On FCP.FundPK = AE.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6     
and CG.SACode = ''                  
            
group by CS.FundPK            
) AF On FCP.FundPK = AF.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
and CG.SACode = ''                   
            
group by CS.FundPK            
) AG On FCP.FundPK = AG.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4       
and CG.SACode = ''                 
            
group by CS.FundPK            
) AH On FCP.FundPK = AH.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)         
and CG.SACode = ''             
            
group by CS.FundPK            
) AI On FCP.FundPK = AI.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)
and CG.SACode = ''                        
            
group by CS.FundPK            
) AJ On FCP.FundPK = AJ.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7) 
and CG.SACode = ''                      
            
group by CS.FundPK            
) AK On FCP.FundPK = AK.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)  
and CG.SACode = ''                       
            
group by CS.FundPK            
) AL On FCP.FundPK = AL.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK   and CG.Status in (1,2)       
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1            
and CG.SACode = ''             
            
group by CS.FundPK            
) AM On FCP.FundPK = AM.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1           
and CG.SACode = ''            
            
group by CS.FundPK            
) AN On FCP.FundPK = AN.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<>'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8  
and CG.SACode = ''                   
            
group by CS.FundPK            
) AO On FCP.FundPK = AO.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8      
and CG.SACode = ''                  
            
group by CS.FundPK            
) AP On FCP.FundPK = AP.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'          
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2      
and CG.SACode = ''                 
            
group by CS.FundPK            
) AQ On FCP.FundPK = AQ.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2       
and CG.SACode = ''                 
            
group by CS.FundPK            
) AR On FCP.FundPK = AR.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8 
and CG.SACode = ''                        
            
group by CS.FundPK            
) SS On FCP.FundPK = SS.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''                      
            
group by CS.FundPK            
) AT On FCP.FundPK = AT.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8            
and CG.SACode = ''            
            
group by CS.FundPK            
) AU On FCP.FundPK = AU.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8         
and CG.SACode = ''               
            
group by CS.FundPK            
) AV On FCP.FundPK = AV.FundPK            
left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in (1,2)       
WHERE FCP.Date = @date
and Z.FundTypeInternal <> 2 and FCP.FundPK <> 6  -- BUKAN KPD        
GROUP BY FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
B.UnitAmount           
,C.jumlahPerusahaanEfek,            
D.UnitAmount ,E.jumlahDanaPensiun,            
F.UnitAmount ,G.jumlahAsuransi,            
H.UnitAmount ,I.jumlahBank,           
J.UnitAmount ,K.jumlahPT,            
L.UnitAmount ,M.jumlahBUMN,            
N.UnitAmount ,O.jumlahBUMD,            
P.UnitAmount ,Q.jumlahYayasan,            
R.UnitAmount ,S.jumlahKoperasi,            
T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
V.UnitAmount ,            
----asing            
AA.jumlahPeroranganAsing,            
AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
AF.UnitAmount ,AG.jumlahAsuransiAsing,            
AH.UnitAmount ,AI.jumlahBankAsing,            
AJ.UnitAmount ,AK.jumlahPTAsing,            
AL.UnitAmount ,AM.jumlahBUMNAsing,            
AN.UnitAmount ,AO.jumlahBUMDAsing,            
AP.UnitAmount ,AQ.jumlahYayasanAsing,            
AR.UnitAmount ,SS.jumlahKoperasiAsing,            
AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
AV.UnitAmount , FCP.FundPK
             
select * from #text

    ";
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {


                                string filePath = Tools.ARIATextPath + "NKPD01.txt";
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
                                    return Tools.HtmlARIATextPath + "NKPD01.txt";
                                }

                            }
                            return null;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean GenerateReportNKPD(string _userID, SInvestRpt _sInvestRpt)
        {
            #region NKPD
            if (_sInvestRpt.ReportName.Equals("3"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            _paramFund = "left(@FundFrom,charindex('-',@FundFrom) - 1) ";


                            cmd.CommandText =
                            @"
                             Declare @FinalDate datetime
   set @FinalDate =dbo.FWorkingDay(@Date ,-1)

                    SELECT  RTRIM(LTRIM(isnull(FU.Name,''))) FundName,  
					RTRIM(LTRIM(isnull(FU.NKPDName,''))) KodeProduk            
, RTRIM(LTRIM(isnull(AAA.NKPDCode,''))) KodeBK         
, RTRIM(LTRIM(isnull(A.jumlahPerorangan,0))) JmlNasabahPerorangan
, CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahPerorangan                   
, RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    JmlNasabahLembagaPE
, CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))  DanaNasabahLembagaPE                   
, RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    JmlNasabahLembagaDAPEN
, CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaDAPEN                 
, RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    JmlNasabahLembagaAsuransi
, CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaAsuransi                  
, RTRIM(LTRIM(isnull(I.jumlahBank,0)))        JmlNasabahLembagaBank
, CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBank             
, RTRIM(LTRIM(isnull(K.jumlahPT,0)))     JmlNasabahLembagaSwasta
, CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaSwasta                   
, RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     JmlNasabahLembagaBUMN
, CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMN                 
, RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        JmlNasabahLembagaBUMD
, CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMD             
, RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  JmlNasabahLembagaYayasan
, CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaYayasan                    
, RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     JmlNasabahLembagaKoperasi
, CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaKoperasi                
, RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0))) JmlNasabahLembagaLainnya
, CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaLainnya            
            
------ASING            
, RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0))) JmlAsingPerorangan     
, CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingPerorangan                  
, RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     JmlAsingLembagaPE
, CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaPE                    
, RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0))) JmlAsingLembagaDAPEN 
, CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaDAPEN                  
, RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        JmlAsingLembagaAsuransi
, CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaAsuransi                
, RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   JmlAsingLembagaBank
, CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBank                       
, RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) JmlAsingLembagaSwasta
, CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaSwasta                        
, RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    JmlAsingLembagaBUMN
, CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMN                     
, RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   JmlAsingLembagaBUMD
, CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMD                      
, RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  JmlAsingLembagaYayasan
, CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaYayasan                      
, RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       JmlAsingLembagaKoperasi
, CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaKoperasi                  
, RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           JmlAsingLembagaLainnya
, CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaLainnya


, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiDN



, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiLN
	         
             
FROM FundClientPosition FCP (NOLOCK)                      
LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in  (1,2)           
LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status = 2  
LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status = 2  
----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status = 2         
LEFT JOIN             
(            
select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
left join FundClient g            
on CS.FundClientPK = g.FundClientPK   and g.Status in  (1,2)         
where g.ClientCategory = 1 and g.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 1     
and g.SACode = ''      
            
group by CS.FundPK            
) A On FCP.FundPK = A.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG        
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)       
where CG.ClientCategory = 1 and CG.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0   
and CG.SACode = ''           
            
group by CS.FundPK            
) B On FCP.FundPK = B.FundPK            
             
LEFT JOIN             
(            
----------EFEK----------------        
select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''               
            
group by CS.FundPK            
) C On FCP.FundPK = C.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
and CG.SACode = ''                
            
group by CS.FundPK            
) D On FCP.FundPK = D.FundPK            
             
             
LEFT JOIN             
(            
---------DANA PENSIUN-------------        
select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6   
and CG.SACode = ''            
            
group by CS.FundPK            
) E On FCP.FundPK = E.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
and CG.SACode = ''          
            
group by CS.FundPK            
) F On FCP.FundPK = F.FundPK            
             
LEFT JOIN             
(            
----------ASURANSI-----------        
select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
and CG.SACode = ''           
            
group by CS.FundPK            
) G On FCP.FundPK = G.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)     
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''             
            
group by CS.FundPK            
) H On FCP.FundPK = H.FundPK            
             
LEFT JOIN             
(            
------------BANK-----------        
select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)   
and CG.SACode = ''              
            
group by CS.FundPK            
) I On FCP.FundPK = I.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''              
            
group by CS.FundPK            
) J On FCP.FundPK = J.FundPK            
             
LEFT JOIN             
(            
--------PEURSAHAAN SWASTA-----------        
select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''          
            
group by CS.FundPK            
) K On FCP.FundPK = K.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) L On FCP.FundPK = L.FundPK            
             
LEFT JOIN             
(            
---------------BUMN----------------        
select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1     
and CG.SACode = ''          
            
group by CS.FundPK            
) M On FCP.FundPK = M.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1   
and CG.SACode = ''               
            
group by CS.FundPK            
) N On FCP.FundPK = N.FundPK            
             
LEFT JOIN             
(            
-------------BUMD-------------        
select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''              
            
group by CS.FundPK            
) O On FCP.FundPK = O.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8  
and CG.SACode = ''               
            
group by CS.FundPK            
) P On FCP.FundPK = P.FundPK            
             
LEFT JOIN             
(            
-----YAYASAN-----------        
select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''             
            
group by CS.FundPK            
) Q On FCP.FundPK = Q.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)            
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''            
            
group by CS.FundPK            
) R On FCP.FundPK = R.FundPK            
             
LEFT JOIN             
(            
------------KOPERASI--------------        
select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) S On FCP.FundPK = S.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''          
            
group by CS.FundPK            
) T On FCP.FundPK = T.FundPK    

					
------------LEMBAGA LAINNYA--------------            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)  
and CG.SACode = ''              
            
group by CS.FundPK            
) U On FCP.FundPK = U.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) V On FCP.FundPK = V.FundPK            
             
----ASING            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.ClientCategory = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0     
and CG.SACode = ''           
            
group by CS.FundPK            
) AA On FCP.FundPK = AA.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)         
where CG.ClientCategory = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0    
and CG.SACode = ''             
            
group by CS.FundPK            
) AB On FCP.FundPK = AB.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)     
and CG.SACode = ''        
            
group by CS.FundPK          
) AC On FCP.FundPK = AC.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) AD On FCP.FundPK = AD.FundPK            
             
             
LEFT JOIN             
(            
select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)     
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6        
and CG.SACode = ''     
            
group by CS.FundPK            
) AE On FCP.FundPK = AE.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
and CG.SACode = ''           
            
group by CS.FundPK            
) AF On FCP.FundPK = AF.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
and CG.SACode = ''        
            
group by CS.FundPK            
) AG On FCP.FundPK = AG.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''           
            
group by CS.FundPK            
) AH On FCP.FundPK = AH.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''          
            
group by CS.FundPK            
) AI On FCP.FundPK = AI.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''            
            
group by CS.FundPK            
) AJ On FCP.FundPK = AJ.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''          
            
group by CS.FundPK            
) AK On FCP.FundPK = AK.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)        
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''           
            
group by CS.FundPK            
) AL On FCP.FundPK = AL.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK   and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1   
and CG.SACode = ''            
            
group by CS.FundPK            
) AM On FCP.FundPK = AM.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1      
and CG.SACode = ''       
            
group by CS.FundPK            
) AN On FCP.FundPK = AN.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<>'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''        
            
group by CS.FundPK            
) AO On FCP.FundPK = AO.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8 
and CG.SACode = ''             
            
group by CS.FundPK            
) AP On FCP.FundPK = AP.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'          
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2    
and CG.SACode = ''         
            
group by CS.FundPK            
) AQ On FCP.FundPK = AQ.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2   
and CG.SACode = ''           
            
group by CS.FundPK            
) AR On FCP.FundPK = AR.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''            
            
group by CS.FundPK            
) SS On FCP.FundPK = SS.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) AT On FCP.FundPK = AT.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)   
and CG.SACode = ''           
            
group by CS.FundPK            
) AU On FCP.FundPK = AU.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) AV On FCP.FundPK = AV.FundPK            
left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in  (1,2)        
WHERE FCP.Date =@FinalDate
and Z.FundTypeInternal <> 2  and FCP.FundPK <> 6 -- BUKAN KPD        
AND FCP.UnitAmount > 10
GROUP BY FU.Name,FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
B.UnitAmount           
,C.jumlahPerusahaanEfek,            
D.UnitAmount ,E.jumlahDanaPensiun,            
F.UnitAmount ,G.jumlahAsuransi,            
H.UnitAmount ,I.jumlahBank,           
J.UnitAmount ,K.jumlahPT,            
L.UnitAmount ,M.jumlahBUMN,            
N.UnitAmount ,O.jumlahBUMD,            
P.UnitAmount ,Q.jumlahYayasan,            
R.UnitAmount ,S.jumlahKoperasi,            
T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
V.UnitAmount ,            
----asing            
AA.jumlahPeroranganAsing,            
AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
AF.UnitAmount ,AG.jumlahAsuransiAsing,            
AH.UnitAmount ,AI.jumlahBankAsing,            
AJ.UnitAmount ,AK.jumlahPTAsing,            
AL.UnitAmount ,AM.jumlahBUMNAsing,            
AN.UnitAmount ,AO.jumlahBUMDAsing,            
AP.UnitAmount ,AQ.jumlahYayasanAsing,            
AR.UnitAmount ,SS.jumlahKoperasiAsing,            
AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
AV.UnitAmount , FCP.FundPK ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@date", _sInvestRpt.ParamDate);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "NKPDReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NKPD Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<NKPD> rList = new List<NKPD>();
                                        while (dr0.Read())
                                        {
                                            NKPD rSingle = new NKPD();
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.KodeProduk = Convert.ToString(dr0["KodeProduk"]);
                                            rSingle.KodeBK = Convert.ToString(dr0["KodeBK"]);
                                            rSingle.JmlNasabahPerorangan = Convert.ToInt32(dr0["JmlNasabahPerorangan"]);
                                            rSingle.DanaNasabahPerorangan = Convert.ToDecimal(dr0["DanaNasabahPerorangan"]);
                                            rSingle.JmlNasabahLembagaPE = Convert.ToInt32(dr0["JmlNasabahLembagaPE"]);
                                            rSingle.DanaNasabahLembagaPE = Convert.ToDecimal(dr0["DanaNasabahLembagaPE"]);
                                            rSingle.JmlNasabahLembagaDAPEN = Convert.ToInt32(dr0["JmlNasabahLembagaDAPEN"]);
                                            rSingle.DanaNasabahLembagaDAPEN = Convert.ToDecimal(dr0["DanaNasabahLembagaDAPEN"]);
                                            rSingle.JmlNasabahLembagaAsuransi = Convert.ToInt32(dr0["JmlNasabahLembagaAsuransi"]);
                                            rSingle.DanaNasabahLembagaAsuransi = Convert.ToDecimal(dr0["DanaNasabahLembagaAsuransi"]);
                                            rSingle.JmlNasabahLembagaBank = Convert.ToInt32(dr0["JmlNasabahLembagaBank"]);
                                            rSingle.DanaNasabahLembagaBank = Convert.ToDecimal(dr0["DanaNasabahLembagaBank"]);
                                            rSingle.JmlNasabahLembagaSwasta = Convert.ToInt32(dr0["JmlNasabahLembagaSwasta"]);
                                            rSingle.DanaNasabahLembagaSwasta = Convert.ToDecimal(dr0["DanaNasabahLembagaSwasta"]);
                                            rSingle.JmlNasabahLembagaBUMN = Convert.ToInt32(dr0["JmlNasabahLembagaBUMN"]);
                                            rSingle.DanaNasabahLembagaBUMN = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMN"]);
                                            rSingle.JmlNasabahLembagaBUMD = Convert.ToInt32(dr0["JmlNasabahLembagaBUMD"]);
                                            rSingle.DanaNasabahLembagaBUMD = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMD"]);
                                            rSingle.JmlNasabahLembagaYayasan = Convert.ToInt32(dr0["JmlNasabahLembagaYayasan"]);
                                            rSingle.DanaNasabahLembagaYayasan = Convert.ToDecimal(dr0["DanaNasabahLembagaYayasan"]);
                                            rSingle.JmlNasabahLembagaKoperasi = Convert.ToInt32(dr0["JmlNasabahLembagaKoperasi"]);
                                            rSingle.DanaNasabahLembagaKoperasi = Convert.ToDecimal(dr0["DanaNasabahLembagaKoperasi"]);
                                            rSingle.JmlNasabahLembagaLainnya = Convert.ToInt32(dr0["JmlNasabahLembagaLainnya"]);
                                            rSingle.DanaNasabahLembagaLainnya = Convert.ToDecimal(dr0["DanaNasabahLembagaLainnya"]);
                                            rSingle.JmlAsingPerorangan = Convert.ToInt32(dr0["JmlAsingPerorangan"]);
                                            rSingle.DanaAsingPerorangan = Convert.ToDecimal(dr0["DanaAsingPerorangan"]);
                                            rSingle.JmlAsingLembagaPE = Convert.ToInt32(dr0["JmlAsingLembagaPE"]);
                                            rSingle.DanaAsingLembagaPE = Convert.ToDecimal(dr0["DanaAsingLembagaPE"]);
                                            rSingle.JmlAsingLembagaDAPEN = Convert.ToInt32(dr0["JmlAsingLembagaDAPEN"]);
                                            rSingle.DanaAsingLembagaDAPEN = Convert.ToDecimal(dr0["DanaAsingLembagaDAPEN"]);
                                            rSingle.JmlAsingLembagaAsuransi = Convert.ToInt32(dr0["JmlAsingLembagaAsuransi"]);
                                            rSingle.DanaAsingLembagaAsuransi = Convert.ToDecimal(dr0["DanaAsingLembagaAsuransi"]);
                                            rSingle.JmlAsingLembagaBank = Convert.ToInt32(dr0["JmlAsingLembagaBank"]);
                                            rSingle.DanaAsingLembagaBank = Convert.ToDecimal(dr0["DanaAsingLembagaBank"]);
                                            rSingle.JmlAsingLembagaSwasta = Convert.ToInt32(dr0["JmlAsingLembagaSwasta"]);
                                            rSingle.DanaAsingLembagaSwasta = Convert.ToDecimal(dr0["DanaAsingLembagaSwasta"]);
                                            rSingle.JmlAsingLembagaBUMN = Convert.ToInt32(dr0["JmlAsingLembagaBUMN"]);
                                            rSingle.DanaAsingLembagaBUMN = Convert.ToDecimal(dr0["DanaAsingLembagaBUMN"]);
                                            rSingle.JmlAsingLembagaBUMD = Convert.ToInt32(dr0["JmlAsingLembagaBUMD"]);
                                            rSingle.DanaAsingLembagaBUMD = Convert.ToDecimal(dr0["DanaAsingLembagaBUMD"]);
                                            rSingle.JmlAsingLembagaYayasan = Convert.ToInt32(dr0["JmlAsingLembagaYayasan"]);
                                            rSingle.DanaAsingLembagaYayasan = Convert.ToDecimal(dr0["DanaAsingLembagaYayasan"]);
                                            rSingle.JmlAsingLembagaKoperasi = Convert.ToInt32(dr0["JmlAsingLembagaKoperasi"]);
                                            rSingle.DanaAsingLembagaKoperasi = Convert.ToDecimal(dr0["DanaAsingLembagaKoperasi"]);
                                            rSingle.JmlAsingLembagaLainnya = Convert.ToInt32(dr0["JmlAsingLembagaLainnya"]);
                                            rSingle.DanaAsingLembagaLainnya = Convert.ToDecimal(dr0["DanaAsingLembagaLainnya"]);
                                            rSingle.InvestasiDN = Convert.ToDecimal(dr0["InvestasiDN"]);
                                            rSingle.InvestasiLN = Convert.ToDecimal(dr0["InvestasiLN"]);
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
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.WrapText = true;

                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

                                            worksheet.Cells[incRowExcel, 1].Value = "Nama Produk";
                                            worksheet.Cells[incRowExcel, 2].Value = "Kode Produk";
                                            worksheet.Cells[incRowExcel, 3].Value = "Kode BK";
                                            worksheet.Cells[incRowExcel, 4].Value = "Jumlah Nasabah Nasional Perorangan";
                                            worksheet.Cells[incRowExcel, 5].Value = "Dana Kelolaan Nasabah Nasional Perorangan";
                                            worksheet.Cells[incRowExcel, 6].Value = "Jumlah Nasabah Nasional Lembaga PE";
                                            worksheet.Cells[incRowExcel, 7].Value = "Dana Kelolaan Nasabah Nasional Lembaga PE";
                                            worksheet.Cells[incRowExcel, 8].Value = "Jumlah Nasabah Nasional Lembaga DAPEN";
                                            worksheet.Cells[incRowExcel, 9].Value = "Dana Kelolaan Nasabah Nasional Lembaga DAPEN";
                                            worksheet.Cells[incRowExcel, 10].Value = "Jumlah Nasabah Nasional Lembaga Asuransi";
                                            worksheet.Cells[incRowExcel, 11].Value = "Dana Kelolaan Nasabah Nasional Lembaga Asuransi";
                                            worksheet.Cells[incRowExcel, 12].Value = "Jumlah Nasabah Nasional Lembaga Bank";
                                            worksheet.Cells[incRowExcel, 13].Value = "Dana Kelolaan Nasabah Nasional Lembaga Bank";
                                            worksheet.Cells[incRowExcel, 14].Value = "Jumlah Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                            worksheet.Cells[incRowExcel, 15].Value = "Dana Kelolaan Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                            worksheet.Cells[incRowExcel, 16].Value = "Jumlah Nasabah Nasional Lembaga BUMN";
                                            worksheet.Cells[incRowExcel, 17].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMN";
                                            worksheet.Cells[incRowExcel, 18].Value = "Jumlah Nasabah Nasional Lembaga BUMD";
                                            worksheet.Cells[incRowExcel, 19].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMD";
                                            worksheet.Cells[incRowExcel, 20].Value = "Jumlah Nasabah Nasional Lembaga Yayasan";
                                            worksheet.Cells[incRowExcel, 21].Value = "Dana Kelolaan Nasabah Nasional Lembaga Yayasan";
                                            worksheet.Cells[incRowExcel, 22].Value = "Jumlah Nasabah Nasional Lembaga Koperasi";
                                            worksheet.Cells[incRowExcel, 23].Value = "Dana Kelolaan Nasabah Nasional Lembaga Koperasi";
                                            worksheet.Cells[incRowExcel, 24].Value = "Jumlah Nasabah Nasional Lembaga Lainnya";
                                            worksheet.Cells[incRowExcel, 25].Value = "Dana Kelolaan Nasabah Nasional Lembaga Lainnya";
                                            worksheet.Cells[incRowExcel, 26].Value = "Jumlah Nasabah Asing Perorangan";
                                            worksheet.Cells[incRowExcel, 27].Value = "Dana Kelolaan Nasabah Asing Perorangan";
                                            worksheet.Cells[incRowExcel, 28].Value = "Jumlah Nasabah Asing Lembaga PE";
                                            worksheet.Cells[incRowExcel, 29].Value = "Dana Kelolaan Nasabah Asing Lembaga PE";
                                            worksheet.Cells[incRowExcel, 30].Value = "Jumlah Nasabah Asing Lembaga DAPEN";
                                            worksheet.Cells[incRowExcel, 31].Value = "Dana Kelolaan Nasabah Asing Lembaga DAPEN";
                                            worksheet.Cells[incRowExcel, 32].Value = "Jumlah Nasabah Asing Lembaga Asuransi";
                                            worksheet.Cells[incRowExcel, 33].Value = "Dana Kelolaan Nasabah Asing Lembaga Asuransi";
                                            worksheet.Cells[incRowExcel, 34].Value = "Jumlah Nasabah Asing Lembaga Bank";
                                            worksheet.Cells[incRowExcel, 35].Value = "Dana Kelolaan Nasabah Asing Lembaga Bank";
                                            worksheet.Cells[incRowExcel, 36].Value = "Jumlah Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                            worksheet.Cells[incRowExcel, 37].Value = "Dana Kelolaan Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                            worksheet.Cells[incRowExcel, 38].Value = "Jumlah Nasabah Asing Lembaga BUMN";
                                            worksheet.Cells[incRowExcel, 39].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMN";
                                            worksheet.Cells[incRowExcel, 40].Value = "Jumlah Nasabah Asing Lembaga BUMD";
                                            worksheet.Cells[incRowExcel, 41].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMD";
                                            worksheet.Cells[incRowExcel, 42].Value = "Jumlah Nasabah Asing Lembaga Yayasan";
                                            worksheet.Cells[incRowExcel, 43].Value = "Dana Kelolaan Nasabah Asing Lembaga Yayasan";
                                            worksheet.Cells[incRowExcel, 44].Value = "Jumlah Nasabah Asing Lembaga Koperasi";
                                            worksheet.Cells[incRowExcel, 45].Value = "Dana Kelolaan Nasabah Asing Lembaga Koperasi";
                                            worksheet.Cells[incRowExcel, 46].Value = "Jumlah Nasabah Asing Lembaga Lainnya";
                                            worksheet.Cells[incRowExcel, 47].Value = "Dana Kelolaan Nasabah Asing Lembaga Lainnya";
                                            worksheet.Cells[incRowExcel, 48].Value = "Investasi DN";
                                            worksheet.Cells[incRowExcel, 49].Value = "Investasi LN";
                                            worksheet.Cells[incRowExcel, 50].Value = "Total";

                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Value = "(1)";
                                            worksheet.Cells[incRowExcel, 2].Value = "(2)";
                                            worksheet.Cells[incRowExcel, 3].Value = "(3)";
                                            worksheet.Cells[incRowExcel, 4].Value = "(4)";
                                            worksheet.Cells[incRowExcel, 5].Value = "(5)";
                                            worksheet.Cells[incRowExcel, 6].Value = "(6)";
                                            worksheet.Cells[incRowExcel, 7].Value = "(7)";
                                            worksheet.Cells[incRowExcel, 8].Value = "(8)";
                                            worksheet.Cells[incRowExcel, 9].Value = "(9)";
                                            worksheet.Cells[incRowExcel, 10].Value = "(10)";
                                            worksheet.Cells[incRowExcel, 11].Value = "(11)";
                                            worksheet.Cells[incRowExcel, 12].Value = "(12)";
                                            worksheet.Cells[incRowExcel, 13].Value = "(13)";
                                            worksheet.Cells[incRowExcel, 14].Value = "(14)";
                                            worksheet.Cells[incRowExcel, 15].Value = "(15)";
                                            worksheet.Cells[incRowExcel, 16].Value = "(16)";
                                            worksheet.Cells[incRowExcel, 17].Value = "(17)";
                                            worksheet.Cells[incRowExcel, 18].Value = "(18)";
                                            worksheet.Cells[incRowExcel, 19].Value = "(19)";
                                            worksheet.Cells[incRowExcel, 20].Value = "(20)";
                                            worksheet.Cells[incRowExcel, 21].Value = "(21)";
                                            worksheet.Cells[incRowExcel, 22].Value = "(22)";
                                            worksheet.Cells[incRowExcel, 23].Value = "(23)";
                                            worksheet.Cells[incRowExcel, 24].Value = "(24)";
                                            worksheet.Cells[incRowExcel, 25].Value = "(25)";
                                            worksheet.Cells[incRowExcel, 26].Value = "(26)";
                                            worksheet.Cells[incRowExcel, 27].Value = "(27)";
                                            worksheet.Cells[incRowExcel, 28].Value = "(28)";
                                            worksheet.Cells[incRowExcel, 29].Value = "(29)";
                                            worksheet.Cells[incRowExcel, 30].Value = "(30)";
                                            worksheet.Cells[incRowExcel, 31].Value = "(31)";
                                            worksheet.Cells[incRowExcel, 32].Value = "(32)";
                                            worksheet.Cells[incRowExcel, 33].Value = "(33)";
                                            worksheet.Cells[incRowExcel, 34].Value = "(34)";
                                            worksheet.Cells[incRowExcel, 35].Value = "(35)";
                                            worksheet.Cells[incRowExcel, 36].Value = "(36)";
                                            worksheet.Cells[incRowExcel, 37].Value = "(37)";
                                            worksheet.Cells[incRowExcel, 38].Value = "(38)";
                                            worksheet.Cells[incRowExcel, 39].Value = "(39)";
                                            worksheet.Cells[incRowExcel, 40].Value = "(40)";
                                            worksheet.Cells[incRowExcel, 41].Value = "(41)";
                                            worksheet.Cells[incRowExcel, 42].Value = "(42)";
                                            worksheet.Cells[incRowExcel, 43].Value = "(43)";
                                            worksheet.Cells[incRowExcel, 44].Value = "(44)";
                                            worksheet.Cells[incRowExcel, 45].Value = "(45)";
                                            worksheet.Cells[incRowExcel, 46].Value = "(46)";
                                            worksheet.Cells[incRowExcel, 47].Value = "(47)";
                                            worksheet.Cells[incRowExcel, 48].Value = "(48)";

                                            //area header
                                            int _endRowDetail = 0;
                                            int _startRow = incRowExcel;
                                            incRowExcel++;
                                            _startRowDetail = incRowExcel;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.KodeProduk;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.KodeBK;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.JmlNasabahPerorangan;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DanaNasabahPerorangan;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.JmlNasabahLembagaPE;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DanaNasabahLembagaPE;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.JmlNasabahLembagaDAPEN;
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.DanaNasabahLembagaDAPEN;
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.JmlNasabahLembagaAsuransi;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.DanaNasabahLembagaAsuransi;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.JmlNasabahLembagaBank;
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.DanaNasabahLembagaBank;
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.JmlNasabahLembagaSwasta;
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.DanaNasabahLembagaSwasta;
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.JmlNasabahLembagaBUMN;
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.DanaNasabahLembagaBUMN;
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.JmlNasabahLembagaBUMD;
                                                worksheet.Cells[incRowExcel, 19].Value = rsDetail.DanaNasabahLembagaBUMD;
                                                worksheet.Cells[incRowExcel, 20].Value = rsDetail.JmlNasabahLembagaYayasan;
                                                worksheet.Cells[incRowExcel, 21].Value = rsDetail.DanaNasabahLembagaYayasan;
                                                worksheet.Cells[incRowExcel, 22].Value = rsDetail.JmlNasabahLembagaKoperasi;
                                                worksheet.Cells[incRowExcel, 23].Value = rsDetail.DanaNasabahLembagaKoperasi;
                                                worksheet.Cells[incRowExcel, 24].Value = rsDetail.JmlNasabahLembagaLainnya;
                                                worksheet.Cells[incRowExcel, 25].Value = rsDetail.DanaNasabahLembagaLainnya;
                                                worksheet.Cells[incRowExcel, 26].Value = rsDetail.JmlAsingPerorangan;
                                                worksheet.Cells[incRowExcel, 27].Value = rsDetail.DanaAsingPerorangan;
                                                worksheet.Cells[incRowExcel, 28].Value = rsDetail.JmlAsingLembagaPE;
                                                worksheet.Cells[incRowExcel, 29].Value = rsDetail.DanaAsingLembagaPE;
                                                worksheet.Cells[incRowExcel, 30].Value = rsDetail.JmlAsingLembagaDAPEN;
                                                worksheet.Cells[incRowExcel, 31].Value = rsDetail.DanaAsingLembagaDAPEN;
                                                worksheet.Cells[incRowExcel, 32].Value = rsDetail.JmlAsingLembagaAsuransi;
                                                worksheet.Cells[incRowExcel, 33].Value = rsDetail.DanaAsingLembagaAsuransi;
                                                worksheet.Cells[incRowExcel, 34].Value = rsDetail.JmlAsingLembagaBank;
                                                worksheet.Cells[incRowExcel, 35].Value = rsDetail.DanaAsingLembagaBank;
                                                worksheet.Cells[incRowExcel, 36].Value = rsDetail.JmlAsingLembagaSwasta;
                                                worksheet.Cells[incRowExcel, 37].Value = rsDetail.DanaAsingLembagaSwasta;
                                                worksheet.Cells[incRowExcel, 38].Value = rsDetail.JmlAsingLembagaBUMN;
                                                worksheet.Cells[incRowExcel, 39].Value = rsDetail.DanaAsingLembagaBUMN;
                                                worksheet.Cells[incRowExcel, 40].Value = rsDetail.JmlAsingLembagaBUMD;
                                                worksheet.Cells[incRowExcel, 41].Value = rsDetail.DanaAsingLembagaBUMD;
                                                worksheet.Cells[incRowExcel, 42].Value = rsDetail.JmlAsingLembagaYayasan;
                                                worksheet.Cells[incRowExcel, 43].Value = rsDetail.DanaAsingLembagaYayasan;
                                                worksheet.Cells[incRowExcel, 44].Value = rsDetail.JmlAsingLembagaKoperasi;
                                                worksheet.Cells[incRowExcel, 45].Value = rsDetail.DanaAsingLembagaKoperasi;
                                                worksheet.Cells[incRowExcel, 46].Value = rsDetail.JmlAsingLembagaLainnya;
                                                worksheet.Cells[incRowExcel, 47].Value = rsDetail.DanaAsingLembagaLainnya;
                                                worksheet.Cells[incRowExcel, 48].Value = rsDetail.InvestasiDN;
                                                worksheet.Cells[incRowExcel, 49].Value = rsDetail.InvestasiLN;
                                                worksheet.Cells[incRowExcel, 50].Formula =
                                                "SUM(E" + incRowExcel + "+G" + incRowExcel + "+I" + incRowExcel + "+K" + incRowExcel + "+M" + incRowExcel +
                                                "+O" + incRowExcel + "+Q" + incRowExcel + "+S" + incRowExcel + "+U" + incRowExcel + "+W" + incRowExcel + "+Y" + incRowExcel +
                                                "+AA" + incRowExcel + "+AC" + incRowExcel + "+AE" + incRowExcel + "+AG" + incRowExcel + "+AI" + incRowExcel + "+AK" + incRowExcel +
                                                "+AM" + incRowExcel + "+AO" + incRowExcel + "+AQ" + incRowExcel + "+AS" + incRowExcel + "+AU" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 50].Calculate();

                                                _endRowDetail = incRowExcel;

                                                incRowExcel++;


                                            }

                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                                            worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 23].Formula = "SUM(W" + _startRowDetail + ":W" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 24].Formula = "SUM(X" + _startRowDetail + ":X" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 25].Formula = "SUM(Y" + _startRowDetail + ":Y" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 26].Formula = "SUM(Z" + _startRowDetail + ":Z" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 27].Formula = "SUM(AA" + _startRowDetail + ":AA" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 28].Formula = "SUM(AB" + _startRowDetail + ":AB" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 29].Formula = "SUM(AC" + _startRowDetail + ":AC" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 30].Formula = "SUM(AD" + _startRowDetail + ":AD" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 31].Formula = "SUM(AE" + _startRowDetail + ":AE" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 32].Formula = "SUM(AF" + _startRowDetail + ":AF" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 33].Formula = "SUM(AG" + _startRowDetail + ":AG" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 34].Formula = "SUM(AH" + _startRowDetail + ":AH" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 35].Formula = "SUM(AI" + _startRowDetail + ":AI" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 36].Formula = "SUM(AJ" + _startRowDetail + ":AJ" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 37].Formula = "SUM(AK" + _startRowDetail + ":AK" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 38].Formula = "SUM(AL" + _startRowDetail + ":AL" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 39].Formula = "SUM(AM" + _startRowDetail + ":AM" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 40].Formula = "SUM(AN" + _startRowDetail + ":AN" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 41].Formula = "SUM(AO" + _startRowDetail + ":AO" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 42].Formula = "SUM(AP" + _startRowDetail + ":AP" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 43].Formula = "SUM(AQ" + _startRowDetail + ":AQ" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 44].Formula = "SUM(AR" + _startRowDetail + ":AR" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 45].Formula = "SUM(AS" + _startRowDetail + ":AS" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 46].Formula = "SUM(AT" + _startRowDetail + ":AT" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 47].Formula = "SUM(AU" + _startRowDetail + ":AU" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 48].Formula = "SUM(AV" + _startRowDetail + ":AV" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 49].Formula = "SUM(AW" + _startRowDetail + ":AW" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 50].Formula = "SUM(AX" + _startRowDetail + ":AX" + _endRowDetail + ")";
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Calculate();
                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Calculate();
                                            worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Calculate();
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Calculate();
                                            worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Calculate();
                                            worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Calculate();
                                            worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Calculate();
                                            worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Calculate();
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                            worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Calculate();
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Calculate();
                                            worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Calculate();
                                            worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Calculate();
                                            worksheet.Cells["N" + incRowExcel + ":O" + incRowExcel].Calculate();
                                            worksheet.Cells["O" + incRowExcel + ":P" + incRowExcel].Calculate();
                                            worksheet.Cells["P" + incRowExcel + ":Q" + incRowExcel].Calculate();
                                            worksheet.Cells["Q" + incRowExcel + ":R" + incRowExcel].Calculate();
                                            worksheet.Cells["R" + incRowExcel + ":S" + incRowExcel].Calculate();
                                            worksheet.Cells["S" + incRowExcel + ":T" + incRowExcel].Calculate();
                                            worksheet.Cells["T" + incRowExcel + ":U" + incRowExcel].Calculate();
                                            worksheet.Cells["U" + incRowExcel + ":V" + incRowExcel].Calculate();
                                            worksheet.Cells["V" + incRowExcel + ":W" + incRowExcel].Calculate();
                                            worksheet.Cells["W" + incRowExcel + ":X" + incRowExcel].Calculate();
                                            worksheet.Cells["X" + incRowExcel + ":Y" + incRowExcel].Calculate();
                                            worksheet.Cells["Z" + incRowExcel + ":Z" + incRowExcel].Calculate();
                                            worksheet.Cells["AA" + incRowExcel + ":AA" + incRowExcel].Calculate();
                                            worksheet.Cells["AB" + incRowExcel + ":AB" + incRowExcel].Calculate();
                                            worksheet.Cells["AC" + incRowExcel + ":AC" + incRowExcel].Calculate();
                                            worksheet.Cells["AD" + incRowExcel + ":AD" + incRowExcel].Calculate();
                                            worksheet.Cells["AE" + incRowExcel + ":AE" + incRowExcel].Calculate();
                                            worksheet.Cells["AF" + incRowExcel + ":AF" + incRowExcel].Calculate();
                                            worksheet.Cells["AG" + incRowExcel + ":AG" + incRowExcel].Calculate();
                                            worksheet.Cells["AH" + incRowExcel + ":AH" + incRowExcel].Calculate();
                                            worksheet.Cells["AI" + incRowExcel + ":AI" + incRowExcel].Calculate();
                                            worksheet.Cells["AJ" + incRowExcel + ":AJ" + incRowExcel].Calculate();
                                            worksheet.Cells["AK" + incRowExcel + ":AK" + incRowExcel].Calculate();
                                            worksheet.Cells["AL" + incRowExcel + ":AL" + incRowExcel].Calculate();
                                            worksheet.Cells["AM" + incRowExcel + ":AM" + incRowExcel].Calculate();
                                            worksheet.Cells["AN" + incRowExcel + ":AN" + incRowExcel].Calculate();
                                            worksheet.Cells["AO" + incRowExcel + ":AO" + incRowExcel].Calculate();
                                            worksheet.Cells["AP" + incRowExcel + ":AP" + incRowExcel].Calculate();
                                            worksheet.Cells["AQ" + incRowExcel + ":AQ" + incRowExcel].Calculate();
                                            worksheet.Cells["AR" + incRowExcel + ":AR" + incRowExcel].Calculate();
                                            worksheet.Cells["AS" + incRowExcel + ":AS" + incRowExcel].Calculate();
                                            worksheet.Cells["AT" + incRowExcel + ":AT" + incRowExcel].Calculate();
                                            worksheet.Cells["AU" + incRowExcel + ":AU" + incRowExcel].Calculate();
                                            worksheet.Cells["AV" + incRowExcel + ":AV" + incRowExcel].Calculate();
                                            worksheet.Cells["AW" + incRowExcel + ":AW" + incRowExcel].Calculate();
                                            worksheet.Cells["AX" + incRowExcel + ":AX" + incRowExcel].Calculate();
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Font.Bold = true;

                                            worksheet.Cells["A" + _startRow + ":AX" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                                            worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            incRowExcel++;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 1;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 50];
                                        worksheet.Column(1).Width = 45;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 10;
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
                                        worksheet.Column(23).Width = 20;
                                        worksheet.Column(24).Width = 20;
                                        worksheet.Column(25).Width = 20;
                                        worksheet.Column(26).Width = 20;
                                        worksheet.Column(27).Width = 20;
                                        worksheet.Column(28).Width = 20;
                                        worksheet.Column(29).Width = 20;
                                        worksheet.Column(30).Width = 20;
                                        worksheet.Column(31).Width = 20;
                                        worksheet.Column(32).Width = 20;
                                        worksheet.Column(33).Width = 20;
                                        worksheet.Column(34).Width = 20;
                                        worksheet.Column(35).Width = 20;
                                        worksheet.Column(36).Width = 20;
                                        worksheet.Column(37).Width = 20;
                                        worksheet.Column(38).Width = 20;
                                        worksheet.Column(39).Width = 20;
                                        worksheet.Column(40).Width = 20;
                                        worksheet.Column(41).Width = 20;
                                        worksheet.Column(42).Width = 20;
                                        worksheet.Column(43).Width = 20;
                                        worksheet.Column(44).Width = 20;
                                        worksheet.Column(45).Width = 20;
                                        worksheet.Column(46).Width = 20;
                                        worksheet.Column(47).Width = 20;
                                        worksheet.Column(48).Width = 20;
                                        worksheet.Column(49).Width = 20;
                                        worksheet.Column(50).Width = 20;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 NKPD REPORT";

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

            else
            {
                return false;
            }
        }

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {

            #region Client Unit Position
            if (_unitRegistryRpt.ReportName.Equals("Client Unit Position"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _paramFundClient = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPk in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }


                            cmd.CommandText =
                                    @" Select B.Name ClientName,B.ClientCategory clientcategory1,mv.DescOne ClientCategory2, C.ID FundID,@Date Date ,isnull(CNA.Nav,0) NavAmount,A.UnitAmount,A.CashAmount, isnull(sum(CNA.NAv * A.UnitAmount),0) EndBalance ,
case when B.ClientCategory = 2 then mv1.DescOne else 'INDIVIDUAL' end ClientCategory
From FundClientPosition A   
                                     Left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2   
                                     Left join Fund C on A.FundPK = C.FundPK and C.Status = 2
									 left join MasterValue mv on b.ClientCategory = mv.Code  and mv.ID = 'ClientCategory'  
									  left join MasterValue mv1 on b.Tipe = mv1.Code  and mv1.ID = 'CompanyType' 
                                     left Join    
                                    (   
                                         select B.ID FundID, MAX(CN.Nav) NAV    
                                         from CloseNAV CN   
                                         Left join Fund B on B.FundPK = CN.FundPK and B.Status =2  
                                         where CN.Date = @Date   
                                         group by B.ID   
                                     ) CNA on CNA.FundID = C.ID   

                                     left Join    
                                     (   
                                         select B.ID FundID, MAX(A.Date) MaxDate    
                                         from FundClientPosition A   
                                         Left join Fund B on B.FundPK = A.FundPK and B.Status =2  
                                         where A.Date <= @Date  
                                         group by B.ID   
                                     ) MFCP on MFCP.FundID = C.ID   
                                     where where A.date = dbo.FWorkingDay(@Date,-1)  and A.UnitAmount <> 0         
									  " + _paramFund + _paramFundClient + @"
                                     group by B.Name,B.Clientcategory ,mv.DescOne , C.ID ,CNA.Nav,A.UnitAmount,A.CashAmount,mv1.DescOne";

                            cmd.Parameters.AddWithValue("@Date", _unitRegistryRpt.ValueDateFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ClientUnitPosition" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ClientUnitPosition" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Client Unit Position");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["NavAmount"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.ClientCategory = Convert.ToString(dr0["ClientCategory"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.EndBalance = Convert.ToDecimal(dr0["EndBalance"]);

                                            rList.Add(rSingle);

                                        }
                                        //mekel
                                        var QueryByClientID =
                                         from r in rList
                                         group r by new { r.ClientName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 6;

                                        worksheet.Cells[incRowExcel, 1].Value = "Date :";
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString();
                                        incRowExcel++;
                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Client :";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.ClientName;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "No.";
                                            worksheet.Cells[incRowExcel, 3].Value = "Client Category";
                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 4].Value = "NAV/Unit";
                                            worksheet.Cells[incRowExcel, 5].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 6].Value = "Cash";
                                            string _range = "A" + incRowExcel + ":F" + incRowExcel;

                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 11;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                            }

                                            _range = "A" + incRowExcel + ":F" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }

                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            //int _endRowDetail = 0;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                _range = "A" + incRowExcel + ":F" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                                }
                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientCategory;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Nav;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Nav * rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";


                                                incRowExcel++;
                                                _range = "A" + incRowExcel + ":F" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                }

                                                //_endRowDetail = incRowExcel;
                                                _no++;
                                                //incRowExcel++;

                                            }
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;
                                            incRowExcel = incRowExcel + 6;

                                        }




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells["A3:F14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 CLIENT UNIT POSITION";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
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
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
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

        public Boolean GenerateReportCompliance(string _userID, OjkRpt _OjkRpt)
        {

            #region SiPesat
            if (_OjkRpt.ReportName.Equals("4"))
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

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                        
                        DECLARE @Name nvarchar(100),@ID nvarchar(50)
                        DECLARE @NoIDPJK nvarchar(30)


                        select @NoIDPJK = NoIDPJK  from Company where Status in(1,2)
                        set @NoIDPJK = isnull(@NoIDPJK,'')

                        create table #Text(
                        [ResultText] [nvarchar] (1000) NULL
                        )

                        DECLARE @Table TABLE
                        (
	                        FundClientPK INT,
	                        Date Datetime
                        )

                        DECLARE @CFundClientPK INT

                        Declare A Cursor FOR
	                        SELECT DISTINCT fundclientpK FROM dbo.ClientSubscription WHERE status <> 3 AND Posted = 1 AND Revised = 0
                        Open A
                        Fetch Next From A
                        INTO @CFundClientPK

                        While @@FETCH_STATUS = 0  
                        Begin
	
	                        INSERT INTO @Table
                                ( FundClientPK, Date )
                            SELECT TOP 1 FundClientPK,ValueDate FROM dbo.ClientSubscription 
                            WHERE FundClientPK = @CFundClientPK AND status <> 3 AND posted = 1 AND Revised = 0
                            ORDER BY ValueDate asc

	                        Fetch Next From A 
	                        into @CFundClientPK
                        End	
                        Close A
                        Deallocate A

                        Declare @A Table
                        (
                        IDPJK nvarchar(30),InvestorType int,TempatLahir nvarchar(50),
                        Name nvarchar(100),TanggalLahir nvarchar(100), Alamat nvarchar(500),
                        NoKTP nvarchar(50),NoIDlain nvarchar(50),ID nvarchar(50),NPWP nvarchar(50)
                        )


                        Insert Into @A
                        select @NoIDPJK IDPJK ,isnull(InvestorType,'') InvestorType,ISNULL(TempatLahir,'') TempatLahir,
                        ISNULL(Name,'') Name,
                        Case when InvestorType = 1 then convert(nvarchar(15),isnull(TanggalLahir,''),105 ) else '' end  TanggalLahir,
                        Case when InvestorType = 1 then ISNULL(AlamatInd1,'') else AlamatPerusahaan end Alamat,
                        CASE when InvestorType = 1 and IdentitasInd1 in (1,7) then NoIdentitasInd1 else  ' ' end NoKTP,
                        case when InvestorType = 1 and IdentitasInd1 in (2,3,4,5,6) then NoIdentitasInd1 else ' ' end NoIDLain,
                        ISNULL(SID,'') ID,
                        ISNULL(NPWP,'') NPWP
                        from FundClient A
                        LEFT JOIN @Table B ON A.FundClientPK = B.FundClientPK
                        WHERE B.Date between @ValueDateFrom and @ValueDateTo  and A.SACode = ''  and status in (1,2) 
                        AND B.FundClientPK IS NOT null



                        Declare Z Cursor For 

                        select Name,ID from @A
                        group by Name,ID 
                        order by Name asc

                        Open Z                  
                        Fetch Next From Z                  
                        Into @Name,@ID
                        While @@FETCH_STATUS = 0                  
                        Begin 

                        Update @A set Name = @Name where ID = @ID

                        Fetch next From Z                   
                        Into @Name,@ID
                        END                  
                        Close Z                  
                        Deallocate z


                        insert into #Text

                        select distinct IDPJK + '|' + RTRIM(LTRIM(isnull(InvestorType,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(TempatLahir,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(Name,''))) + '|' +
                        isnull(TanggalLahir,'') + '|' + 
                        convert(nvarchar(15),ISNULL(Alamat,'')) + '|' +
                        CAST(NoKTP as nvarchar(30)) + '|' +
                        CAST(NoIDLain as nvarchar(30)) + '|' +
                        RTRIM(LTRIM(ISNULL(ID,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(NPWP,'')))
                        from @A


                        select * from #Text ";
                                cmd.Parameters.AddWithValue("@ValueDateFrom", _OjkRpt.ValueDateFrom);
                                cmd.Parameters.AddWithValue("@ValueDateTo", _OjkRpt.ValueDateTo);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {


                                        string filePath = Tools.ARIATextPath + "Sipesat.txt";
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
                                        }

                                    }
                                    return false;
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
                else
                {
                    if (_OjkRpt.ReportName.Equals("4"))
                    {
                        int rowcell = 0;
                        try
                        {
                            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                            {
                                DbCon.Open();
                                using (SqlCommand cmd = DbCon.CreateCommand())
                                {
                                    cmd.CommandText = @"
                        
                        DECLARE @Name nvarchar(100),@ID nvarchar(50)
                        DECLARE @NoIDPJK nvarchar(30)


                        select @NoIDPJK = NoIDPJK  from Company where Status in(1,2)
                        set @NoIDPJK = isnull(@NoIDPJK,'')

                        create table #Text(
                        [ResultText] [nvarchar] (1000) NULL
                        )

                        DECLARE @Table TABLE
                        (
	                        FundClientPK INT,
	                        Date Datetime
                        )

                        DECLARE @CFundClientPK INT

                        Declare A Cursor FOR
	                        SELECT DISTINCT fundclientpK FROM dbo.ClientSubscription WHERE status <> 3 AND Posted = 1 AND Revised = 0
                        Open A
                        Fetch Next From A
                        INTO @CFundClientPK

                        While @@FETCH_STATUS = 0  
                        Begin
	
	                        INSERT INTO @Table
                                ( FundClientPK, Date )
                            SELECT TOP 1 FundClientPK,ValueDate FROM dbo.ClientSubscription 
                            WHERE FundClientPK = @CFundClientPK AND status <> 3 AND posted = 1 AND Revised = 0
                            ORDER BY ValueDate asc

	                        Fetch Next From A 
	                        into @CFundClientPK
                        End	
                        Close A
                        Deallocate A

                        Declare @A Table
                        (
                        IDPJK nvarchar(30),InvestorType int,TempatLahir nvarchar(50),
                        Name nvarchar(100),TanggalLahir nvarchar(100), Alamat nvarchar(500),
                        NoKTP nvarchar(50),NoIDlain nvarchar(50),ID nvarchar(50),NPWP nvarchar(50)
                        )


                        Insert Into @A
                        select @NoIDPJK IDPJK ,isnull(InvestorType,'') InvestorType,ISNULL(TempatLahir,'') TempatLahir,
                        ISNULL(Name,'') Name,
                        Case when InvestorType = 1 then convert(nvarchar(15),isnull(TanggalLahir,''),106 ) else '' end  TanggalLahir,
                        Case when InvestorType = 1 then ISNULL(AlamatInd1,'') else AlamatPerusahaan end Alamat,
                        CASE when InvestorType = 1 and IdentitasInd1 in (1,7) then NoIdentitasInd1 else  ' ' end NoKTP,
                        case when InvestorType = 1 and IdentitasInd1 in (2,3,4,5,6) then NoIdentitasInd1 else ' ' end NoIDLain,
                        ISNULL(SID,'') ID,
                        ISNULL(NPWP,'') NPWP
                        from FundClient A
                        LEFT JOIN @Table B ON A.FundClientPK = B.FundClientPK
                        WHERE B.Date between @ValueDateFrom and @ValueDateTo  and A.SACode = ''  and status in (1,2) 
                        AND B.FundClientPK IS NOT null



                        Declare Z Cursor For 

                        select Name,ID from @A
                        group by Name,ID 
                        order by Name asc

                        Open Z                  
                        Fetch Next From Z                  
                        Into @Name,@ID
                        While @@FETCH_STATUS = 0                  
                        Begin 

                        Update @A set Name = @Name where ID = @ID

                        Fetch next From Z                   
                        Into @Name,@ID
                        END                  
                        Close Z                  
                        Deallocate z

                        select distinct IDPJK,InvestorType,TempatLahir,Name,TanggalLahir,Alamat,NoKTP,NoIDLain,ID,NPWP from @A 

                        ";
                                    cmd.CommandTimeout = 0;
                                    cmd.Parameters.AddWithValue("@ValueDateFrom", _OjkRpt.ValueDateFrom);
                                    cmd.Parameters.AddWithValue("@ValueDateTo", _OjkRpt.ValueDateTo);
                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                    {
                                        if (!dr0.HasRows)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            string filePath = Tools.ReportsPath + "Sipesat_" + _userID + ".xlsx";
                                            int incRowExcel = 0;
                                            int _startRowDetail, _endRowDetail;
                                            FileInfo excelFile = new FileInfo(filePath);
                                            if (excelFile.Exists)
                                            {
                                                excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                                excelFile = new FileInfo(filePath);
                                            }

                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                            using (ExcelPackage package = new ExcelPackage(excelFile))
                                            {
                                                package.Workbook.Properties.Title = "Sipesat";
                                                package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                                package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                                package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                                package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("SiPesatReport");

                                                List<SipesatRpt> rList = new List<SipesatRpt>();
                                                while (dr0.Read())
                                                {
                                                    SipesatRpt rSingle = new SipesatRpt();

                                                    rSingle.InvestorType = Convert.ToInt32(dr0["InvestorType"]);
                                                    rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                                    rSingle.TempatLahir = Convert.ToString(dr0["TempatLahir"]);
                                                    rSingle.TanggalLahir = Convert.ToString(dr0["TanggalLahir"]);
                                                    rSingle.Alamat = Convert.ToString(dr0["Alamat"]);
                                                    rSingle.NoKTP = Convert.ToString(dr0["NoKTP"]);
                                                    rSingle.NoIDLain = Convert.ToString(dr0["NoIDLain"]);
                                                    rSingle.ID = Convert.ToString(dr0["ID"]);
                                                    rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                                    rSingle.IDPJK = Convert.ToString(dr0["IDPJK"]);

                                                    rList.Add(rSingle);
                                                }

                                                var GroupByReference =
                                                    from r in rList
                                                    group r by new { } into rGroup
                                                    select rGroup;
                                                foreach (var rsHeader in GroupByReference)
                                                {

                                                    incRowExcel++;
                                                    _startRowDetail = incRowExcel;
                                                    worksheet.Cells[incRowExcel, 1].Value = "ID PJK";
                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = "Kode Nasabah";
                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 3].Value = "Nama Nasabah";
                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 4].Value = "Tempat Lahir";
                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Tanggal Lahir";
                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 6].Value = "Alamat";
                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 7].Value = "KTP";
                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Value = "Identitas Lain";
                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 9].Value = "CIF/Kepesertaan";
                                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 10].Value = "NPWP";
                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    incRowExcel++;

                                                    //end area header
                                                    foreach (var rsDetail in rsHeader)
                                                    {

                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.IDPJK;
                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.InvestorType;
                                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.TempatLahir;
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.TanggalLahir;
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.Alamat;
                                                        worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        //worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail.NoKTP;
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail.NoIDLain;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail.ID;
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail.NPWP;
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                        _endRowDetail = incRowExcel;
                                                        incRowExcel++;


                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _startRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                    }
                                                    int _endRow = incRowExcel - 1;
                                                    worksheet.Cells["A" + _endRow + ":J" + _endRow].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    incRowExcel++;
                                                    worksheet.Row(incRowExcel).PageBreak = true;
                                                }


                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 0;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                                worksheet.Cells.AutoFitColumns(0);
                                                worksheet.Column(1).Width = 20;
                                                worksheet.Column(2).Width = 20;
                                                worksheet.Column(3).Width = 40;
                                                worksheet.Column(4).Width = 20;
                                                worksheet.Column(5).Width = 20;
                                                worksheet.Column(6).Width = 40;
                                                worksheet.Column(7).Width = 20;
                                                worksheet.Column(8).Width = 20;
                                                worksheet.Column(9).Width = 20;
                                                worksheet.Column(10).Width = 20;
                                                worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                // worksheet.PrinterSettings.FitToPage = true;
                                                //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                worksheet.HeaderFooter.OddHeader.CenteredText = "&14 SiPesat Report";
                                                worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderLeftText();

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

                }

                #endregion

                return true;
            }//else if
            #endregion

            #region KPD
            else if (_OjkRpt.ReportName.Equals("5"))
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


                                string _paramFund = "";

                                if (!_host.findString(_OjkRpt.Fund.ToLower(), "0", ",") && !string.IsNullOrEmpty(_OjkRpt.Fund))
                                {
                                    _paramFund = "And A.FundPK in ( " + _OjkRpt.Fund + " ) ";
                                }
                                else
                                {
                                    _paramFund = "";
                                }

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

                        create table #Text(      
                        [ResultText] [nvarchar](1000)  NULL          
                        )                        
        
                        truncate table #Text --     

          DECLARE @CFundPK INT
DECLARE @CFundClientPK INT

DECLARE @NAV TABLE
(
	FundPK INT,
	NAV NUMERIC(22,4)
)

INSERT INTO @NAV
        ( FundPK, NAV )
SELECT FundPK,ISNULL(A.Nav,0) FROM dbo.CloseNAV A
WHERE Date = (
SELECT MAX(date) FROM closeNAV WHERE status = 2 AND Date <= @Date
) " + _paramFund + @"


DECLARE @Text table (      
[ResultText] [nvarchar](1000)  NULL          
)                        

--drop Table @KPD--
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
	SID NVARCHAR(100)
)

DECLARE A CURSOR FOR 
SELECT distinct A.FundPK,A.FundClientPK 
from dbo.FundClientPosition A
LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
where A.Date = @Date and B.FundTypeInternal = 2
AND A.UnitAmount > 1  " + _paramFund + @"
Open A
Fetch Next From A
Into @CFundPK,@CFundClientPK
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
	          SID
	        )
	SELECT ISNULL(B.InvestorType,1) 
	,ISNULL(B.Name,'') 
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,isnull(CONVERT(VARCHAR(8), C.KPDDateToContract, 112),0)
	,ISNULL(C.KPDNoAdendum,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),0)
	,ISNULL(D.TotalInvestasiAwalIDR,0)
	,ISNULL(E.TotalInvestasiAwalNonIDR,0)
	,ISNULL(D.TotalUnitIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(E.TotalUnitNonIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(G.InstrumentID,'') JenisEfek
	,1 DnLn
	,ISNULL(G.Balance,0) JumlahEfek
	,ISNULL(G.CostValue,0) NilaiPembelian
	,0 NilaiNominal
	,ISNULL(G.ClosePrice,0) HPW
	,ISNULL(H.BalanceDeposito,0) Deposito
	,ISNULL(G.Balance,0) * ISNULL(G.ClosePrice,0) TotalNilai
	,ISNULL(J.NKPDCode,'') KodeBK
	,ISNULL(G.InstrumentID,'') Keterangan
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalIDR,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitIDR FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK = 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)D ON A.FundClientPK = D.FundClientPK
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalNonIDR 
		,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitNonIDR
		FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK <> 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)E ON A.FundClientPK = D.FundClientPK
	LEFT JOIN @NAV F ON A.FundPK = F.FundPK
	LEFT JOIN
    (
		SELECT A.FundPK,A.InstrumentID,ISNULL(A.Balance,0) Balance 
		,ISNULL(A.CostValue,0) CostValue
		,ISNULL(A.ClosePrice,0) ClosePrice
		FROM dbo.FundPosition A
		WHERE A.Date = @Date
	)G ON A.FundPK = G.FundPK
	LEFT JOIN 
	(
		SELECT FundPK, SUM(ISNULL(A.Balance,0)) BalanceDeposito
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		WHERE A.Date = @Date AND B.InstrumentTypePK = 5
		GROUP BY A.FundPK
	)H ON A.FundPK = H.FundPK
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	
	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'998'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK
	

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'997'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'996'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK



Fetch next From A Into @CFundPK,@CFundClientPK
end
Close A
Deallocate A

insert into #Text
 
select 
isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') + --1
'|' + isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'')  +    --2
'|' + isnull(RTRIM(LTRIM(isnull(NoKontrak,''))),'')  +  --3
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakFrom,'')))),'')  +  --4
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakTo,'')))),'')  +  --5
'|' + isnull(RTRIM(LTRIM(isnull(NoAdendum,''))),'')  +  --6
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglAdendum,'')))),'')  +  --7
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,0))),'')  +  --8
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalNonIDR,0))),'')  + --9
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhirIDR,0))),'')  + --10
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhirNonIDR,0))),'')  +  --11
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  --12
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DnLn,'')))),'')  + --13
'|' + isnull(RTRIM(LTRIM(isnull(JumlahEfek,0))),'')  + --14
'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,0))),'')  +  --15
'|' + isnull(RTRIM(LTRIM(isnull(NilaiNominal,0))),'')  +  --16
'|' + isnull(RTRIM(LTRIM(isnull(HPW,0))),'')  +  --17
'|' + isnull(RTRIM(LTRIM(isnull(Deposito,0))),'')  +  --18
'|' + isnull(RTRIM(LTRIM(isnull(TotalNilai,0))),'')  +  --19
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Keterangan,'')))),'') + --21
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
from @KPD

select * from #text

                         ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _Fund);

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
                                            //return Tools.HtmlARIATextPath + _companyCode + "KPD.txt";
                                            return true;
                                        }

                                    }
                                    return false;
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

                                string _paramFund = "";

                                if (!_host.findString(_OjkRpt.Fund.ToLower(), "0", ",") && !string.IsNullOrEmpty(_OjkRpt.Fund))
                                {
                                    _paramFund = "And A.FundPK in ( " + _OjkRpt.Fund + " ) ";
                                }
                                else
                                {
                                    _paramFund = "";
                                }

                                cmd.CommandTimeout = 0;
                                cmd.CommandText =
                                @"

DECLARE @CFundPK INT
DECLARE @CFundClientPK INT

DECLARE @NAV TABLE
(
	FundPK INT,
	NAV NUMERIC(22,4)
)

INSERT INTO @NAV
        ( FundPK, NAV )
SELECT FundPK,ISNULL(A.Nav,0) FROM dbo.CloseNAV A
WHERE Date = (
SELECT MAX(date) FROM closeNAV WHERE status = 2 AND Date <= @Date
) " + _paramFund + @"


DECLARE @Text table (      
[ResultText] [nvarchar](1000)  NULL          
)                        

--drop Table @KPD--
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
	SID NVARCHAR(100)
)

DECLARE A CURSOR FOR 
SELECT distinct A.FundPK,A.FundClientPK 
from dbo.FundClientPosition A
LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
where A.Date = @Date and B.FundTypeInternal = 2 
AND A.UnitAmount > 1 " + _paramFund + @"
Open A
Fetch Next From A
Into @CFundPK,@CFundClientPK
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
	          SID
	        )
	SELECT ISNULL(B.InvestorType,1) 
	,ISNULL(B.Name,'') 
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,isnull(CONVERT(VARCHAR(8), C.KPDDateToContract, 112),0)
	,ISNULL(C.KPDNoAdendum,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),0)
	,ISNULL(D.TotalInvestasiAwalIDR,0)
	,ISNULL(E.TotalInvestasiAwalNonIDR,0)
	,ISNULL(D.TotalUnitIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(E.TotalUnitNonIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(G.InstrumentID,'') JenisEfek
	,1 DnLn
	,ISNULL(G.Balance,0) JumlahEfek
	,ISNULL(G.CostValue,0) NilaiPembelian
	,0 NilaiNominal
	,ISNULL(G.ClosePrice,0) HPW
	,ISNULL(H.BalanceDeposito,0) Deposito
	,ISNULL(G.Balance,0) * ISNULL(G.ClosePrice,0) TotalNilai
	,ISNULL(J.NKPDCode,'') KodeBK
	,ISNULL(G.InstrumentID,'') Keterangan
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalIDR,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitIDR FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK = 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)D ON A.FundClientPK = D.FundClientPK
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalNonIDR 
		,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitNonIDR
		FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK <> 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)E ON A.FundClientPK = D.FundClientPK
	LEFT JOIN @NAV F ON A.FundPK = F.FundPK
	LEFT JOIN
    (
		SELECT A.FundPK,A.InstrumentID,ISNULL(A.Balance,0) Balance 
		,ISNULL(A.CostValue,0) CostValue
		,ISNULL(A.ClosePrice,0) ClosePrice
		FROM dbo.FundPosition A
		WHERE A.Date = @Date
	)G ON A.FundPK = G.FundPK
	LEFT JOIN 
	(
		SELECT FundPK, SUM(ISNULL(A.Balance,0)) BalanceDeposito
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		WHERE A.Date = @Date AND B.InstrumentTypePK = 5
		GROUP BY A.FundPK
	)H ON A.FundPK = H.FundPK
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	
	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'998'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK
	

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'997'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'996'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK



Fetch next From A Into @CFundPK,@CFundClientPK
end
Close A
Deallocate A

SELECT * FROM @KPD
                            ";


                                //cmd.Parameters.AddWithValue("@date", _sInvestRpt.ParamDate);

                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _sInvestRpt.FundFrom);

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
                                                rSingle.NomorKontrak = Convert.ToString(dr0["NoKontrak"]);
                                                rSingle.TanggalKontrak = Convert.ToString(dr0["TglKontrakFrom"]);
                                                rSingle.TanggalJatuhTempo = Convert.ToString(dr0["TglKontrakTo"]);
                                                rSingle.NomorAdendum = Convert.ToString(dr0["NoAdendum"]);
                                                rSingle.TanggalAdendum = Convert.ToString(dr0["TglAdendum"]);
                                                rSingle.NilaiInvestasiAwalIDR = Convert.ToString(dr0["NilaiInvestasiAwalIDR"]);
                                                rSingle.NilaiInvestasiAwalNonIDR = Convert.ToString(dr0["NilaiInvestasiAwalNonIDR"]);
                                                rSingle.NilaiInvestasiAkhir = Convert.ToString(dr0["NilaiInvestasiAkhirIDR"]);
                                                rSingle.NilaiInvestasiAkhirNonIDR = Convert.ToString(dr0["NilaiInvestasiAkhirNonIDR"]);
                                                rSingle.JenisEfek = Convert.ToString(dr0["JenisEfek"]);
                                                rSingle.KodeKategoriEfek = Convert.ToInt32(dr0["DnLn"]);
                                                rSingle.JumlahEfek = Convert.ToString(dr0["JumlahEfek"]);
                                                rSingle.NilaiPembelian = Convert.ToString(dr0["NilaiPembelian"]);
                                                rSingle.NilaiNominal = Convert.ToString(dr0["NilaiNominal"]);
                                                rSingle.HPW = Convert.ToString(dr0["HPW"]);
                                                rSingle.Deposito = Convert.ToString(dr0["Deposito"]);
                                                rSingle.TotalInvestasi = Convert.ToString(dr0["TotalNilai"]);
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
                                                worksheet.Cells[incRowExcel, 10].Value = "Nilai investasi Akhir IDR";
                                                worksheet.Cells[incRowExcel, 11].Value = "Nilai investasi Akhir Non IDR";
                                                worksheet.Cells[incRowExcel, 12].Value = "Kode Efek";
                                                worksheet.Cells[incRowExcel, 13].Value = "Kode Kategori Efek";
                                                worksheet.Cells[incRowExcel, 14].Value = "Jumlah Efek";
                                                worksheet.Cells[incRowExcel, 15].Value = "Nilai Pembelian";
                                                worksheet.Cells[incRowExcel, 16].Value = "Nilai Nominal";
                                                worksheet.Cells[incRowExcel, 17].Value = "HPW";
                                                worksheet.Cells[incRowExcel, 18].Value = "Deposito";
                                                worksheet.Cells[incRowExcel, 19].Value = "Total Investasi";
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

            #region NKPD
            else if (_OjkRpt.ReportName.Equals("3"))
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

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                          declare @FinalDate datetime
set @FinalDate = dbo.FWorkingDay(@Date ,-1)

--drop table #Text --
create table #Text(                    
[ResultText] [nvarchar](1000)  NULL                    
)                     
                
insert into #Text   
                 

SELECT  RTRIM(LTRIM(isnull(FU.NKPDName,'')))             
+ '|' + RTRIM(LTRIM(isnull(AAA.NKPDCode,'')))         
+ '|' + RTRIM(LTRIM(isnull(A.jumlahPerorangan,0)))
+ '|' + CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
+ '|' + RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    
+ '|' + CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    
+ '|' + CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
+ '|' + RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    
+ '|' + CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(I.jumlahBank,0)))        
+ '|' + CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
+ '|' + RTRIM(LTRIM(isnull(K.jumlahPT,0)))     
+ '|' + CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                    
+ '|' + RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     
+ '|' + CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
+ '|' + RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        
+ '|' + CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
+ '|' + RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  
+ '|' + CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     
+ '|' + CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
+ '|' + RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0)))
+ '|' + CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))             
            
------ASING            
+ '|' + RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0)))      
+ '|' + CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     
+ '|' + CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0)))  
+ '|' + CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        
+ '|' + CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
+ '|' + RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   
+ '|' + CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                        
+ '|' + RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) 
+ '|' + CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                         
+ '|' + RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    
+ '|' + CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
+ '|' + RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   
+ '|' + CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
+ '|' + RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  
+ '|' + CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
+ '|' + RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       
+ '|' + CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
+ '|' + RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           
+ '|' + CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) 
+ '|' +   CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30))    
	       
+ '|' + CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30))  
             
FROM FundClientPosition FCP (NOLOCK)                      
LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in (1,2)         
LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status in (1,2)
LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status in (1,2)
----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status in (1,2)       
LEFT JOIN             
(            
select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
left join FundClient g            
on CS.FundClientPK = g.FundClientPK   and g.Status in (1,2)       
where g.InvestorType = 1 and g.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 1          
and g.SACode = ''            
group by CS.FundPK            
) A On FCP.FundPK = A.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG        
on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)      
where CG.InvestorType = 1 and CG.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0   
and CG.SACode = ''            
            
group by CS.FundPK            
) B On FCP.FundPK = B.FundPK            
             
LEFT JOIN             
(            
----------EFEK----------------        
select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
and CG.SACode = ''                 
            
group by CS.FundPK            
) C On FCP.FundPK = C.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''                
            
group by CS.FundPK            
) D On FCP.FundPK = D.FundPK            
             
             
LEFT JOIN             
(            
---------DANA PENSIUN-------------        
select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
and CG.SACode = ''              
            
group by CS.FundPK            
) E On FCP.FundPK = E.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
and CG.SACode = ''                  
            
group by CS.FundPK            
) F On FCP.FundPK = F.FundPK            
             
LEFT JOIN             
(            
----------ASURANSI-----------        
select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
and CG.SACode = ''                  
            
group by CS.FundPK            
) G On FCP.FundPK = G.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)    
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4          
and CG.SACode = ''              
            
group by CS.FundPK            
) H On FCP.FundPK = H.FundPK            
             
LEFT JOIN             
(            
------------BANK-----------        
select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)           
and CG.SACode = ''              
            
group by CS.FundPK            
) I On FCP.FundPK = I.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)          
and CG.SACode = ''              
            
group by CS.FundPK            
) J On FCP.FundPK = J.FundPK            
             
LEFT JOIN             
(            
--------PEURSAHAAN SWASTA-----------        
select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)          
and CG.SACode = ''            
            
group by CS.FundPK            
) K On FCP.FundPK = K.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)       
and CG.SACode = ''             
            
group by CS.FundPK            
) L On FCP.FundPK = L.FundPK            
             
LEFT JOIN             
(            
---------------BUMN----------------        
select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1           
and CG.SACode = ''            
            
group by CS.FundPK            
) M On FCP.FundPK = M.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1 
and CG.SACode = ''                         
            
group by CS.FundPK            
) N On FCP.FundPK = N.FundPK            
             
LEFT JOIN             
(            
-------------BUMD-------------        
select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8      
and CG.SACode = ''                   
            
group by CS.FundPK            
) O On FCP.FundPK = O.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8    
and CG.SACode = ''                     
            
group by CS.FundPK            
) P On FCP.FundPK = P.FundPK            
             
LEFT JOIN             
(            
-----YAYASAN-----------        
select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2            
and CG.SACode = ''            
            
group by CS.FundPK            
) Q On FCP.FundPK = Q.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2       
and CG.SACode = ''                
            
group by CS.FundPK            
) R On FCP.FundPK = R.FundPK            
             
LEFT JOIN             
(            
------------KOPERASI--------------        
select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''                     
            
group by CS.FundPK            
) S On FCP.FundPK = S.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''                   
            
group by CS.FundPK            
) T On FCP.FundPK = T.FundPK    

					
------------LEMBAGA LAINNYA--------------            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8           
and CG.SACode = ''             
            
group by CS.FundPK            
) U On FCP.FundPK = U.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8         
and CG.SACode = ''               
            
group by CS.FundPK            
) V On FCP.FundPK = V.FundPK            
             
----ASING            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0            
and CG.SACode = ''            
            
group by CS.FundPK            
) AA On FCP.FundPK = AA.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)       
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0             
and CG.SACode = ''            
            
group by CS.FundPK            
) AB On FCP.FundPK = AB.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)   
and CG.SACode = ''                    
            
group by CS.FundPK          
) AC On FCP.FundPK = AC.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)    
and CG.SACode = ''                  
            
group by CS.FundPK            
) AD On FCP.FundPK = AD.FundPK            
             
             
LEFT JOIN             
(            
select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)    
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6           
and CG.SACode = ''            
            
group by CS.FundPK            
) AE On FCP.FundPK = AE.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6     
and CG.SACode = ''                  
            
group by CS.FundPK            
) AF On FCP.FundPK = AF.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
and CG.SACode = ''                   
            
group by CS.FundPK            
) AG On FCP.FundPK = AG.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4       
and CG.SACode = ''                 
            
group by CS.FundPK            
) AH On FCP.FundPK = AH.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)         
and CG.SACode = ''             
            
group by CS.FundPK            
) AI On FCP.FundPK = AI.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)
and CG.SACode = ''                        
            
group by CS.FundPK            
) AJ On FCP.FundPK = AJ.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7) 
and CG.SACode = ''                      
            
group by CS.FundPK            
) AK On FCP.FundPK = AK.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)  
and CG.SACode = ''                       
            
group by CS.FundPK            
) AL On FCP.FundPK = AL.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK   and CG.Status in (1,2)       
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1            
and CG.SACode = ''             
            
group by CS.FundPK            
) AM On FCP.FundPK = AM.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1           
and CG.SACode = ''            
            
group by CS.FundPK            
) AN On FCP.FundPK = AN.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<>'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8  
and CG.SACode = ''                   
            
group by CS.FundPK            
) AO On FCP.FundPK = AO.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8      
and CG.SACode = ''                  
            
group by CS.FundPK            
) AP On FCP.FundPK = AP.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'          
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2      
and CG.SACode = ''                 
            
group by CS.FundPK            
) AQ On FCP.FundPK = AQ.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2       
and CG.SACode = ''                 
            
group by CS.FundPK            
) AR On FCP.FundPK = AR.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8 
and CG.SACode = ''                        
            
group by CS.FundPK            
) SS On FCP.FundPK = SS.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''                      
            
group by CS.FundPK            
) AT On FCP.FundPK = AT.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8            
and CG.SACode = ''            
            
group by CS.FundPK            
) AU On FCP.FundPK = AU.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8         
and CG.SACode = ''               
            
group by CS.FundPK            
) AV On FCP.FundPK = AV.FundPK            
left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in (1,2)       
WHERE FCP.Date = @date
and Z.FundTypeInternal <> 2 and FCP.FundPK <> 6  -- BUKAN KPD        
GROUP BY FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
B.UnitAmount           
,C.jumlahPerusahaanEfek,            
D.UnitAmount ,E.jumlahDanaPensiun,            
F.UnitAmount ,G.jumlahAsuransi,            
H.UnitAmount ,I.jumlahBank,           
J.UnitAmount ,K.jumlahPT,            
L.UnitAmount ,M.jumlahBUMN,            
N.UnitAmount ,O.jumlahBUMD,            
P.UnitAmount ,Q.jumlahYayasan,            
R.UnitAmount ,S.jumlahKoperasi,            
T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
V.UnitAmount ,            
----asing            
AA.jumlahPeroranganAsing,            
AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
AF.UnitAmount ,AG.jumlahAsuransiAsing,            
AH.UnitAmount ,AI.jumlahBankAsing,            
AJ.UnitAmount ,AK.jumlahPTAsing,            
AL.UnitAmount ,AM.jumlahBUMNAsing,            
AN.UnitAmount ,AO.jumlahBUMDAsing,            
AP.UnitAmount ,AQ.jumlahYayasanAsing,            
AR.UnitAmount ,SS.jumlahKoperasiAsing,            
AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
AV.UnitAmount , FCP.FundPK
             
select * from #text

                                 ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {


                                        string filePath = Tools.ARIATextPath + "NKPD01.txt";
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
                                            //return Tools.HtmlARIATextPath + "NKPD01.txt";
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

                } // bates

                #endregion

                #region Excel
                else
                {

                    if (_OjkRpt.ReportName.Equals("3"))
                    {
                        try
                        {
                            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                            {
                                DbCon.Open();
                                using (SqlCommand cmd = DbCon.CreateCommand())
                                {

                                    string _paramFund = "";
                                    _paramFund = "left(@FundFrom,charindex('-',@FundFrom) - 1) ";


                                    cmd.CommandText =
                                    @"
                             Declare @FinalDate datetime
   set @FinalDate =dbo.FWorkingDay(@Date ,-1)

                    SELECT  RTRIM(LTRIM(isnull(FU.Name,''))) FundName,  
					RTRIM(LTRIM(isnull(FU.NKPDName,''))) KodeProduk            
, RTRIM(LTRIM(isnull(AAA.NKPDCode,''))) KodeBK         
, RTRIM(LTRIM(isnull(A.jumlahPerorangan,0))) JmlNasabahPerorangan
, CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahPerorangan                   
, RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    JmlNasabahLembagaPE
, CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))  DanaNasabahLembagaPE                   
, RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    JmlNasabahLembagaDAPEN
, CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaDAPEN                 
, RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    JmlNasabahLembagaAsuransi
, CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaAsuransi                  
, RTRIM(LTRIM(isnull(I.jumlahBank,0)))        JmlNasabahLembagaBank
, CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBank             
, RTRIM(LTRIM(isnull(K.jumlahPT,0)))     JmlNasabahLembagaSwasta
, CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaSwasta                   
, RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     JmlNasabahLembagaBUMN
, CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMN                 
, RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        JmlNasabahLembagaBUMD
, CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMD             
, RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  JmlNasabahLembagaYayasan
, CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaYayasan                    
, RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     JmlNasabahLembagaKoperasi
, CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaKoperasi                
, RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0))) JmlNasabahLembagaLainnya
, CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaLainnya            
            
------ASING            
, RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0))) JmlAsingPerorangan     
, CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingPerorangan                  
, RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     JmlAsingLembagaPE
, CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaPE                    
, RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0))) JmlAsingLembagaDAPEN 
, CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaDAPEN                  
, RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        JmlAsingLembagaAsuransi
, CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaAsuransi                
, RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   JmlAsingLembagaBank
, CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBank                       
, RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) JmlAsingLembagaSwasta
, CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaSwasta                        
, RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    JmlAsingLembagaBUMN
, CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMN                     
, RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   JmlAsingLembagaBUMD
, CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMD                      
, RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  JmlAsingLembagaYayasan
, CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaYayasan                      
, RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       JmlAsingLembagaKoperasi
, CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaKoperasi                  
, RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           JmlAsingLembagaLainnya
, CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaLainnya


, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiDN



, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiLN
	         
             
FROM FundClientPosition FCP (NOLOCK)                      
LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in  (1,2)           
LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status = 2  
LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status = 2  
----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status = 2         
LEFT JOIN             
(            
select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
left join FundClient g            
on CS.FundClientPK = g.FundClientPK   and g.Status in  (1,2)         
where g.InvestorType = 1 and g.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 1     
and g.SACode = ''      
            
group by CS.FundPK            
) A On FCP.FundPK = A.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG        
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)       
where CG.InvestorType = 1 and CG.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0   
and CG.SACode = ''           
            
group by CS.FundPK            
) B On FCP.FundPK = B.FundPK            
             
LEFT JOIN             
(            
----------EFEK----------------        
select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''               
            
group by CS.FundPK            
) C On FCP.FundPK = C.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
and CG.SACode = ''                
            
group by CS.FundPK            
) D On FCP.FundPK = D.FundPK            
             
             
LEFT JOIN             
(            
---------DANA PENSIUN-------------        
select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6   
and CG.SACode = ''            
            
group by CS.FundPK            
) E On FCP.FundPK = E.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
and CG.SACode = ''          
            
group by CS.FundPK            
) F On FCP.FundPK = F.FundPK            
             
LEFT JOIN             
(            
----------ASURANSI-----------        
select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
and CG.SACode = ''           
            
group by CS.FundPK            
) G On FCP.FundPK = G.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)     
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''             
            
group by CS.FundPK            
) H On FCP.FundPK = H.FundPK            
             
LEFT JOIN             
(            
------------BANK-----------        
select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)   
and CG.SACode = ''              
            
group by CS.FundPK            
) I On FCP.FundPK = I.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''              
            
group by CS.FundPK            
) J On FCP.FundPK = J.FundPK            
             
LEFT JOIN             
(            
--------PEURSAHAAN SWASTA-----------        
select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''          
            
group by CS.FundPK            
) K On FCP.FundPK = K.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) L On FCP.FundPK = L.FundPK            
             
LEFT JOIN             
(            
---------------BUMN----------------        
select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1     
and CG.SACode = ''          
            
group by CS.FundPK            
) M On FCP.FundPK = M.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1   
and CG.SACode = ''               
            
group by CS.FundPK            
) N On FCP.FundPK = N.FundPK            
             
LEFT JOIN             
(            
-------------BUMD-------------        
select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''              
            
group by CS.FundPK            
) O On FCP.FundPK = O.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8  
and CG.SACode = ''               
            
group by CS.FundPK            
) P On FCP.FundPK = P.FundPK            
             
LEFT JOIN             
(            
-----YAYASAN-----------        
select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''             
            
group by CS.FundPK            
) Q On FCP.FundPK = Q.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''            
            
group by CS.FundPK            
) R On FCP.FundPK = R.FundPK            
             
LEFT JOIN             
(            
------------KOPERASI--------------        
select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) S On FCP.FundPK = S.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''          
            
group by CS.FundPK            
) T On FCP.FundPK = T.FundPK    

					
------------LEMBAGA LAINNYA--------------            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)  
and CG.SACode = ''              
            
group by CS.FundPK            
) U On FCP.FundPK = U.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) V On FCP.FundPK = V.FundPK            
             
----ASING            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0     
and CG.SACode = ''           
            
group by CS.FundPK            
) AA On FCP.FundPK = AA.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)         
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0    
and CG.SACode = ''             
            
group by CS.FundPK            
) AB On FCP.FundPK = AB.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)     
and CG.SACode = ''        
            
group by CS.FundPK          
) AC On FCP.FundPK = AC.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) AD On FCP.FundPK = AD.FundPK            
             
             
LEFT JOIN             
(            
select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)     
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6        
and CG.SACode = ''     
            
group by CS.FundPK            
) AE On FCP.FundPK = AE.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
and CG.SACode = ''           
            
group by CS.FundPK            
) AF On FCP.FundPK = AF.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
and CG.SACode = ''        
            
group by CS.FundPK            
) AG On FCP.FundPK = AG.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''           
            
group by CS.FundPK            
) AH On FCP.FundPK = AH.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''          
            
group by CS.FundPK            
) AI On FCP.FundPK = AI.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''            
            
group by CS.FundPK            
) AJ On FCP.FundPK = AJ.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''          
            
group by CS.FundPK            
) AK On FCP.FundPK = AK.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''           
            
group by CS.FundPK            
) AL On FCP.FundPK = AL.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK   and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1   
and CG.SACode = ''            
            
group by CS.FundPK            
) AM On FCP.FundPK = AM.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1      
and CG.SACode = ''       
            
group by CS.FundPK            
) AN On FCP.FundPK = AN.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<>'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''        
            
group by CS.FundPK            
) AO On FCP.FundPK = AO.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8 
and CG.SACode = ''             
            
group by CS.FundPK            
) AP On FCP.FundPK = AP.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'          
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2    
and CG.SACode = ''         
            
group by CS.FundPK            
) AQ On FCP.FundPK = AQ.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2   
and CG.SACode = ''           
            
group by CS.FundPK            
) AR On FCP.FundPK = AR.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''            
            
group by CS.FundPK            
) SS On FCP.FundPK = SS.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) AT On FCP.FundPK = AT.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)   
and CG.SACode = ''           
            
group by CS.FundPK            
) AU On FCP.FundPK = AU.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) AV On FCP.FundPK = AV.FundPK            
left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in  (1,2)        
WHERE FCP.Date =@FinalDate
and Z.FundTypeInternal <> 2   -- BUKAN KPD        
AND FCP.UnitAmount > 10
GROUP BY FU.Name,FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
B.UnitAmount           
,C.jumlahPerusahaanEfek,            
D.UnitAmount ,E.jumlahDanaPensiun,            
F.UnitAmount ,G.jumlahAsuransi,            
H.UnitAmount ,I.jumlahBank,           
J.UnitAmount ,K.jumlahPT,            
L.UnitAmount ,M.jumlahBUMN,            
N.UnitAmount ,O.jumlahBUMD,            
P.UnitAmount ,Q.jumlahYayasan,            
R.UnitAmount ,S.jumlahKoperasi,            
T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
V.UnitAmount ,            
----asing            
AA.jumlahPeroranganAsing,            
AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
AF.UnitAmount ,AG.jumlahAsuransiAsing,            
AH.UnitAmount ,AI.jumlahBankAsing,            
AJ.UnitAmount ,AK.jumlahPTAsing,            
AL.UnitAmount ,AM.jumlahBUMNAsing,            
AN.UnitAmount ,AO.jumlahBUMDAsing,            
AP.UnitAmount ,AQ.jumlahYayasanAsing,            
AR.UnitAmount ,SS.jumlahKoperasiAsing,            
AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
AV.UnitAmount , FCP.FundPK ";

                                    cmd.CommandTimeout = 0;
                                    cmd.Parameters.AddWithValue("@date", _OjkRpt.Date);


                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                    {
                                        if (!dr0.HasRows)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            string filePath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".xlsx";
                                            string pdfPath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".pdf";
                                            FileInfo excelFile = new FileInfo(filePath);
                                            if (excelFile.Exists)
                                            {
                                                excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                                excelFile = new FileInfo(filePath);
                                            }


                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                            using (ExcelPackage package = new ExcelPackage(excelFile))
                                            {
                                                package.Workbook.Properties.Title = "NKPDReport";
                                                package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                                package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                                package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                                package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NKPD Report");


                                                //ATUR DATA GROUPINGNYA DULU
                                                List<NKPD> rList = new List<NKPD>();
                                                while (dr0.Read())
                                                {
                                                    NKPD rSingle = new NKPD();
                                                    rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                                    rSingle.KodeProduk = Convert.ToString(dr0["KodeProduk"]);
                                                    rSingle.KodeBK = Convert.ToString(dr0["KodeBK"]);
                                                    rSingle.JmlNasabahPerorangan = Convert.ToInt32(dr0["JmlNasabahPerorangan"]);
                                                    rSingle.DanaNasabahPerorangan = Convert.ToDecimal(dr0["DanaNasabahPerorangan"]);
                                                    rSingle.JmlNasabahLembagaPE = Convert.ToInt32(dr0["JmlNasabahLembagaPE"]);
                                                    rSingle.DanaNasabahLembagaPE = Convert.ToDecimal(dr0["DanaNasabahLembagaPE"]);
                                                    rSingle.JmlNasabahLembagaDAPEN = Convert.ToInt32(dr0["JmlNasabahLembagaDAPEN"]);
                                                    rSingle.DanaNasabahLembagaDAPEN = Convert.ToDecimal(dr0["DanaNasabahLembagaDAPEN"]);
                                                    rSingle.JmlNasabahLembagaAsuransi = Convert.ToInt32(dr0["JmlNasabahLembagaAsuransi"]);
                                                    rSingle.DanaNasabahLembagaAsuransi = Convert.ToDecimal(dr0["DanaNasabahLembagaAsuransi"]);
                                                    rSingle.JmlNasabahLembagaBank = Convert.ToInt32(dr0["JmlNasabahLembagaBank"]);
                                                    rSingle.DanaNasabahLembagaBank = Convert.ToDecimal(dr0["DanaNasabahLembagaBank"]);
                                                    rSingle.JmlNasabahLembagaSwasta = Convert.ToInt32(dr0["JmlNasabahLembagaSwasta"]);
                                                    rSingle.DanaNasabahLembagaSwasta = Convert.ToDecimal(dr0["DanaNasabahLembagaSwasta"]);
                                                    rSingle.JmlNasabahLembagaBUMN = Convert.ToInt32(dr0["JmlNasabahLembagaBUMN"]);
                                                    rSingle.DanaNasabahLembagaBUMN = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMN"]);
                                                    rSingle.JmlNasabahLembagaBUMD = Convert.ToInt32(dr0["JmlNasabahLembagaBUMD"]);
                                                    rSingle.DanaNasabahLembagaBUMD = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMD"]);
                                                    rSingle.JmlNasabahLembagaYayasan = Convert.ToInt32(dr0["JmlNasabahLembagaYayasan"]);
                                                    rSingle.DanaNasabahLembagaYayasan = Convert.ToDecimal(dr0["DanaNasabahLembagaYayasan"]);
                                                    rSingle.JmlNasabahLembagaKoperasi = Convert.ToInt32(dr0["JmlNasabahLembagaKoperasi"]);
                                                    rSingle.DanaNasabahLembagaKoperasi = Convert.ToDecimal(dr0["DanaNasabahLembagaKoperasi"]);
                                                    rSingle.JmlNasabahLembagaLainnya = Convert.ToInt32(dr0["JmlNasabahLembagaLainnya"]);
                                                    rSingle.DanaNasabahLembagaLainnya = Convert.ToDecimal(dr0["DanaNasabahLembagaLainnya"]);
                                                    rSingle.JmlAsingPerorangan = Convert.ToInt32(dr0["JmlAsingPerorangan"]);
                                                    rSingle.DanaAsingPerorangan = Convert.ToDecimal(dr0["DanaAsingPerorangan"]);
                                                    rSingle.JmlAsingLembagaPE = Convert.ToInt32(dr0["JmlAsingLembagaPE"]);
                                                    rSingle.DanaAsingLembagaPE = Convert.ToDecimal(dr0["DanaAsingLembagaPE"]);
                                                    rSingle.JmlAsingLembagaDAPEN = Convert.ToInt32(dr0["JmlAsingLembagaDAPEN"]);
                                                    rSingle.DanaAsingLembagaDAPEN = Convert.ToDecimal(dr0["DanaAsingLembagaDAPEN"]);
                                                    rSingle.JmlAsingLembagaAsuransi = Convert.ToInt32(dr0["JmlAsingLembagaAsuransi"]);
                                                    rSingle.DanaAsingLembagaAsuransi = Convert.ToDecimal(dr0["DanaAsingLembagaAsuransi"]);
                                                    rSingle.JmlAsingLembagaBank = Convert.ToInt32(dr0["JmlAsingLembagaBank"]);
                                                    rSingle.DanaAsingLembagaBank = Convert.ToDecimal(dr0["DanaAsingLembagaBank"]);
                                                    rSingle.JmlAsingLembagaSwasta = Convert.ToInt32(dr0["JmlAsingLembagaSwasta"]);
                                                    rSingle.DanaAsingLembagaSwasta = Convert.ToDecimal(dr0["DanaAsingLembagaSwasta"]);
                                                    rSingle.JmlAsingLembagaBUMN = Convert.ToInt32(dr0["JmlAsingLembagaBUMN"]);
                                                    rSingle.DanaAsingLembagaBUMN = Convert.ToDecimal(dr0["DanaAsingLembagaBUMN"]);
                                                    rSingle.JmlAsingLembagaBUMD = Convert.ToInt32(dr0["JmlAsingLembagaBUMD"]);
                                                    rSingle.DanaAsingLembagaBUMD = Convert.ToDecimal(dr0["DanaAsingLembagaBUMD"]);
                                                    rSingle.JmlAsingLembagaYayasan = Convert.ToInt32(dr0["JmlAsingLembagaYayasan"]);
                                                    rSingle.DanaAsingLembagaYayasan = Convert.ToDecimal(dr0["DanaAsingLembagaYayasan"]);
                                                    rSingle.JmlAsingLembagaKoperasi = Convert.ToInt32(dr0["JmlAsingLembagaKoperasi"]);
                                                    rSingle.DanaAsingLembagaKoperasi = Convert.ToDecimal(dr0["DanaAsingLembagaKoperasi"]);
                                                    rSingle.JmlAsingLembagaLainnya = Convert.ToInt32(dr0["JmlAsingLembagaLainnya"]);
                                                    rSingle.DanaAsingLembagaLainnya = Convert.ToDecimal(dr0["DanaAsingLembagaLainnya"]);
                                                    rSingle.InvestasiDN = Convert.ToDecimal(dr0["InvestasiDN"]);
                                                    rSingle.InvestasiLN = Convert.ToDecimal(dr0["InvestasiLN"]);
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
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.WrapText = true;

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

                                                    worksheet.Cells[incRowExcel, 1].Value = "Nama Produk";
                                                    worksheet.Cells[incRowExcel, 2].Value = "Kode Produk";
                                                    worksheet.Cells[incRowExcel, 3].Value = "Kode BK";
                                                    worksheet.Cells[incRowExcel, 4].Value = "Jumlah Nasabah Nasional Perorangan";
                                                    worksheet.Cells[incRowExcel, 5].Value = "Dana Kelolaan Nasabah Nasional Perorangan";
                                                    worksheet.Cells[incRowExcel, 6].Value = "Jumlah Nasabah Nasional Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 7].Value = "Dana Kelolaan Nasabah Nasional Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 8].Value = "Jumlah Nasabah Nasional Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 9].Value = "Dana Kelolaan Nasabah Nasional Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 10].Value = "Jumlah Nasabah Nasional Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 11].Value = "Dana Kelolaan Nasabah Nasional Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 12].Value = "Jumlah Nasabah Nasional Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 13].Value = "Dana Kelolaan Nasabah Nasional Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 14].Value = "Jumlah Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 15].Value = "Dana Kelolaan Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 16].Value = "Jumlah Nasabah Nasional Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 17].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 18].Value = "Jumlah Nasabah Nasional Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 19].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 20].Value = "Jumlah Nasabah Nasional Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 21].Value = "Dana Kelolaan Nasabah Nasional Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 22].Value = "Jumlah Nasabah Nasional Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 23].Value = "Dana Kelolaan Nasabah Nasional Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 24].Value = "Jumlah Nasabah Nasional Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 25].Value = "Dana Kelolaan Nasabah Nasional Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 26].Value = "Jumlah Nasabah Asing Perorangan";
                                                    worksheet.Cells[incRowExcel, 27].Value = "Dana Kelolaan Nasabah Asing Perorangan";
                                                    worksheet.Cells[incRowExcel, 28].Value = "Jumlah Nasabah Asing Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 29].Value = "Dana Kelolaan Nasabah Asing Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 30].Value = "Jumlah Nasabah Asing Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 31].Value = "Dana Kelolaan Nasabah Asing Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 32].Value = "Jumlah Nasabah Asing Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 33].Value = "Dana Kelolaan Nasabah Asing Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 34].Value = "Jumlah Nasabah Asing Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 35].Value = "Dana Kelolaan Nasabah Asing Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 36].Value = "Jumlah Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 37].Value = "Dana Kelolaan Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 38].Value = "Jumlah Nasabah Asing Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 39].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 40].Value = "Jumlah Nasabah Asing Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 41].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 42].Value = "Jumlah Nasabah Asing Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 43].Value = "Dana Kelolaan Nasabah Asing Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 44].Value = "Jumlah Nasabah Asing Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 45].Value = "Dana Kelolaan Nasabah Asing Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 46].Value = "Jumlah Nasabah Asing Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 47].Value = "Dana Kelolaan Nasabah Asing Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 48].Value = "Investasi DN";
                                                    worksheet.Cells[incRowExcel, 49].Value = "Investasi LN";
                                                    worksheet.Cells[incRowExcel, 50].Value = "Total";

                                                    incRowExcel++;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 1].Value = "(1)";
                                                    worksheet.Cells[incRowExcel, 2].Value = "(2)";
                                                    worksheet.Cells[incRowExcel, 3].Value = "(3)";
                                                    worksheet.Cells[incRowExcel, 4].Value = "(4)";
                                                    worksheet.Cells[incRowExcel, 5].Value = "(5)";
                                                    worksheet.Cells[incRowExcel, 6].Value = "(6)";
                                                    worksheet.Cells[incRowExcel, 7].Value = "(7)";
                                                    worksheet.Cells[incRowExcel, 8].Value = "(8)";
                                                    worksheet.Cells[incRowExcel, 9].Value = "(9)";
                                                    worksheet.Cells[incRowExcel, 10].Value = "(10)";
                                                    worksheet.Cells[incRowExcel, 11].Value = "(11)";
                                                    worksheet.Cells[incRowExcel, 12].Value = "(12)";
                                                    worksheet.Cells[incRowExcel, 13].Value = "(13)";
                                                    worksheet.Cells[incRowExcel, 14].Value = "(14)";
                                                    worksheet.Cells[incRowExcel, 15].Value = "(15)";
                                                    worksheet.Cells[incRowExcel, 16].Value = "(16)";
                                                    worksheet.Cells[incRowExcel, 17].Value = "(17)";
                                                    worksheet.Cells[incRowExcel, 18].Value = "(18)";
                                                    worksheet.Cells[incRowExcel, 19].Value = "(19)";
                                                    worksheet.Cells[incRowExcel, 20].Value = "(20)";
                                                    worksheet.Cells[incRowExcel, 21].Value = "(21)";
                                                    worksheet.Cells[incRowExcel, 22].Value = "(22)";
                                                    worksheet.Cells[incRowExcel, 23].Value = "(23)";
                                                    worksheet.Cells[incRowExcel, 24].Value = "(24)";
                                                    worksheet.Cells[incRowExcel, 25].Value = "(25)";
                                                    worksheet.Cells[incRowExcel, 26].Value = "(26)";
                                                    worksheet.Cells[incRowExcel, 27].Value = "(27)";
                                                    worksheet.Cells[incRowExcel, 28].Value = "(28)";
                                                    worksheet.Cells[incRowExcel, 29].Value = "(29)";
                                                    worksheet.Cells[incRowExcel, 30].Value = "(30)";
                                                    worksheet.Cells[incRowExcel, 31].Value = "(31)";
                                                    worksheet.Cells[incRowExcel, 32].Value = "(32)";
                                                    worksheet.Cells[incRowExcel, 33].Value = "(33)";
                                                    worksheet.Cells[incRowExcel, 34].Value = "(34)";
                                                    worksheet.Cells[incRowExcel, 35].Value = "(35)";
                                                    worksheet.Cells[incRowExcel, 36].Value = "(36)";
                                                    worksheet.Cells[incRowExcel, 37].Value = "(37)";
                                                    worksheet.Cells[incRowExcel, 38].Value = "(38)";
                                                    worksheet.Cells[incRowExcel, 39].Value = "(39)";
                                                    worksheet.Cells[incRowExcel, 40].Value = "(40)";
                                                    worksheet.Cells[incRowExcel, 41].Value = "(41)";
                                                    worksheet.Cells[incRowExcel, 42].Value = "(42)";
                                                    worksheet.Cells[incRowExcel, 43].Value = "(43)";
                                                    worksheet.Cells[incRowExcel, 44].Value = "(44)";
                                                    worksheet.Cells[incRowExcel, 45].Value = "(45)";
                                                    worksheet.Cells[incRowExcel, 46].Value = "(46)";
                                                    worksheet.Cells[incRowExcel, 47].Value = "(47)";
                                                    worksheet.Cells[incRowExcel, 48].Value = "(48)";

                                                    //area header
                                                    int _endRowDetail = 0;
                                                    int _startRow = incRowExcel;
                                                    incRowExcel++;
                                                    _startRowDetail = incRowExcel;
                                                    foreach (var rsDetail in rsHeader)
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundName;
                                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.KodeProduk;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.KodeBK;
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.JmlNasabahPerorangan;
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.DanaNasabahPerorangan;
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.JmlNasabahLembagaPE;
                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail.DanaNasabahLembagaPE;
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail.JmlNasabahLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail.DanaNasabahLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail.JmlNasabahLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail.DanaNasabahLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 12].Value = rsDetail.JmlNasabahLembagaBank;
                                                        worksheet.Cells[incRowExcel, 13].Value = rsDetail.DanaNasabahLembagaBank;
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail.JmlNasabahLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail.DanaNasabahLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 16].Value = rsDetail.JmlNasabahLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 17].Value = rsDetail.DanaNasabahLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 18].Value = rsDetail.JmlNasabahLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 19].Value = rsDetail.DanaNasabahLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 20].Value = rsDetail.JmlNasabahLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 21].Value = rsDetail.DanaNasabahLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 22].Value = rsDetail.JmlNasabahLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 23].Value = rsDetail.DanaNasabahLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 24].Value = rsDetail.JmlNasabahLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 25].Value = rsDetail.DanaNasabahLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 26].Value = rsDetail.JmlAsingPerorangan;
                                                        worksheet.Cells[incRowExcel, 27].Value = rsDetail.DanaAsingPerorangan;
                                                        worksheet.Cells[incRowExcel, 28].Value = rsDetail.JmlAsingLembagaPE;
                                                        worksheet.Cells[incRowExcel, 29].Value = rsDetail.DanaAsingLembagaPE;
                                                        worksheet.Cells[incRowExcel, 30].Value = rsDetail.JmlAsingLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 31].Value = rsDetail.DanaAsingLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 32].Value = rsDetail.JmlAsingLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 33].Value = rsDetail.DanaAsingLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 34].Value = rsDetail.JmlAsingLembagaBank;
                                                        worksheet.Cells[incRowExcel, 35].Value = rsDetail.DanaAsingLembagaBank;
                                                        worksheet.Cells[incRowExcel, 36].Value = rsDetail.JmlAsingLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 37].Value = rsDetail.DanaAsingLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 38].Value = rsDetail.JmlAsingLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 39].Value = rsDetail.DanaAsingLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 40].Value = rsDetail.JmlAsingLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 41].Value = rsDetail.DanaAsingLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 42].Value = rsDetail.JmlAsingLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 43].Value = rsDetail.DanaAsingLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 44].Value = rsDetail.JmlAsingLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 45].Value = rsDetail.DanaAsingLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 46].Value = rsDetail.JmlAsingLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 47].Value = rsDetail.DanaAsingLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 48].Value = rsDetail.InvestasiDN;
                                                        worksheet.Cells[incRowExcel, 49].Value = rsDetail.InvestasiLN;
                                                        worksheet.Cells[incRowExcel, 50].Formula =
                                                        "SUM(E" + incRowExcel + "+G" + incRowExcel + "+I" + incRowExcel + "+K" + incRowExcel + "+M" + incRowExcel +
                                                        "+O" + incRowExcel + "+Q" + incRowExcel + "+S" + incRowExcel + "+U" + incRowExcel + "+W" + incRowExcel + "+Y" + incRowExcel +
                                                        "+AA" + incRowExcel + "+AC" + incRowExcel + "+AE" + incRowExcel + "+AG" + incRowExcel + "+AI" + incRowExcel + "+AK" + incRowExcel +
                                                        "+AM" + incRowExcel + "+AO" + incRowExcel + "+AQ" + incRowExcel + "+AS" + incRowExcel + "+AU" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 50].Calculate();

                                                        _endRowDetail = incRowExcel;

                                                        incRowExcel++;


                                                    }

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                                                    worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 23].Formula = "SUM(W" + _startRowDetail + ":W" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 24].Formula = "SUM(X" + _startRowDetail + ":X" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 25].Formula = "SUM(Y" + _startRowDetail + ":Y" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 26].Formula = "SUM(Z" + _startRowDetail + ":Z" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 27].Formula = "SUM(AA" + _startRowDetail + ":AA" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 28].Formula = "SUM(AB" + _startRowDetail + ":AB" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 29].Formula = "SUM(AC" + _startRowDetail + ":AC" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 30].Formula = "SUM(AD" + _startRowDetail + ":AD" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 31].Formula = "SUM(AE" + _startRowDetail + ":AE" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 32].Formula = "SUM(AF" + _startRowDetail + ":AF" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 33].Formula = "SUM(AG" + _startRowDetail + ":AG" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 34].Formula = "SUM(AH" + _startRowDetail + ":AH" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 35].Formula = "SUM(AI" + _startRowDetail + ":AI" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 36].Formula = "SUM(AJ" + _startRowDetail + ":AJ" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 37].Formula = "SUM(AK" + _startRowDetail + ":AK" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 38].Formula = "SUM(AL" + _startRowDetail + ":AL" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 39].Formula = "SUM(AM" + _startRowDetail + ":AM" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 40].Formula = "SUM(AN" + _startRowDetail + ":AN" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 41].Formula = "SUM(AO" + _startRowDetail + ":AO" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 42].Formula = "SUM(AP" + _startRowDetail + ":AP" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 43].Formula = "SUM(AQ" + _startRowDetail + ":AQ" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 44].Formula = "SUM(AR" + _startRowDetail + ":AR" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 45].Formula = "SUM(AS" + _startRowDetail + ":AS" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 46].Formula = "SUM(AT" + _startRowDetail + ":AT" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 47].Formula = "SUM(AU" + _startRowDetail + ":AU" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 48].Formula = "SUM(AV" + _startRowDetail + ":AV" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 49].Formula = "SUM(AW" + _startRowDetail + ":AW" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 50].Formula = "SUM(AX" + _startRowDetail + ":AX" + _endRowDetail + ")";
                                                    worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Calculate();
                                                    worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Calculate();
                                                    worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Calculate();
                                                    worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Calculate();
                                                    worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Calculate();
                                                    worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Calculate();
                                                    worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Calculate();
                                                    worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Calculate();
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                                    worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Calculate();
                                                    worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Calculate();
                                                    worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Calculate();
                                                    worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Calculate();
                                                    worksheet.Cells["N" + incRowExcel + ":O" + incRowExcel].Calculate();
                                                    worksheet.Cells["O" + incRowExcel + ":P" + incRowExcel].Calculate();
                                                    worksheet.Cells["P" + incRowExcel + ":Q" + incRowExcel].Calculate();
                                                    worksheet.Cells["Q" + incRowExcel + ":R" + incRowExcel].Calculate();
                                                    worksheet.Cells["R" + incRowExcel + ":S" + incRowExcel].Calculate();
                                                    worksheet.Cells["S" + incRowExcel + ":T" + incRowExcel].Calculate();
                                                    worksheet.Cells["T" + incRowExcel + ":U" + incRowExcel].Calculate();
                                                    worksheet.Cells["U" + incRowExcel + ":V" + incRowExcel].Calculate();
                                                    worksheet.Cells["V" + incRowExcel + ":W" + incRowExcel].Calculate();
                                                    worksheet.Cells["W" + incRowExcel + ":X" + incRowExcel].Calculate();
                                                    worksheet.Cells["X" + incRowExcel + ":Y" + incRowExcel].Calculate();
                                                    worksheet.Cells["Z" + incRowExcel + ":Z" + incRowExcel].Calculate();
                                                    worksheet.Cells["AA" + incRowExcel + ":AA" + incRowExcel].Calculate();
                                                    worksheet.Cells["AB" + incRowExcel + ":AB" + incRowExcel].Calculate();
                                                    worksheet.Cells["AC" + incRowExcel + ":AC" + incRowExcel].Calculate();
                                                    worksheet.Cells["AD" + incRowExcel + ":AD" + incRowExcel].Calculate();
                                                    worksheet.Cells["AE" + incRowExcel + ":AE" + incRowExcel].Calculate();
                                                    worksheet.Cells["AF" + incRowExcel + ":AF" + incRowExcel].Calculate();
                                                    worksheet.Cells["AG" + incRowExcel + ":AG" + incRowExcel].Calculate();
                                                    worksheet.Cells["AH" + incRowExcel + ":AH" + incRowExcel].Calculate();
                                                    worksheet.Cells["AI" + incRowExcel + ":AI" + incRowExcel].Calculate();
                                                    worksheet.Cells["AJ" + incRowExcel + ":AJ" + incRowExcel].Calculate();
                                                    worksheet.Cells["AK" + incRowExcel + ":AK" + incRowExcel].Calculate();
                                                    worksheet.Cells["AL" + incRowExcel + ":AL" + incRowExcel].Calculate();
                                                    worksheet.Cells["AM" + incRowExcel + ":AM" + incRowExcel].Calculate();
                                                    worksheet.Cells["AN" + incRowExcel + ":AN" + incRowExcel].Calculate();
                                                    worksheet.Cells["AO" + incRowExcel + ":AO" + incRowExcel].Calculate();
                                                    worksheet.Cells["AP" + incRowExcel + ":AP" + incRowExcel].Calculate();
                                                    worksheet.Cells["AQ" + incRowExcel + ":AQ" + incRowExcel].Calculate();
                                                    worksheet.Cells["AR" + incRowExcel + ":AR" + incRowExcel].Calculate();
                                                    worksheet.Cells["AS" + incRowExcel + ":AS" + incRowExcel].Calculate();
                                                    worksheet.Cells["AT" + incRowExcel + ":AT" + incRowExcel].Calculate();
                                                    worksheet.Cells["AU" + incRowExcel + ":AU" + incRowExcel].Calculate();
                                                    worksheet.Cells["AV" + incRowExcel + ":AV" + incRowExcel].Calculate();
                                                    worksheet.Cells["AW" + incRowExcel + ":AW" + incRowExcel].Calculate();
                                                    worksheet.Cells["AX" + incRowExcel + ":AX" + incRowExcel].Calculate();
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Font.Bold = true;

                                                    worksheet.Cells["A" + _startRow + ":AX" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                                                    worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    incRowExcel++;
                                                }



                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 1;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 50];
                                                worksheet.Column(1).Width = 45;
                                                worksheet.Column(2).Width = 20;
                                                worksheet.Column(3).Width = 10;
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
                                                worksheet.Column(23).Width = 20;
                                                worksheet.Column(24).Width = 20;
                                                worksheet.Column(25).Width = 20;
                                                worksheet.Column(26).Width = 20;
                                                worksheet.Column(27).Width = 20;
                                                worksheet.Column(28).Width = 20;
                                                worksheet.Column(29).Width = 20;
                                                worksheet.Column(30).Width = 20;
                                                worksheet.Column(31).Width = 20;
                                                worksheet.Column(32).Width = 20;
                                                worksheet.Column(33).Width = 20;
                                                worksheet.Column(34).Width = 20;
                                                worksheet.Column(35).Width = 20;
                                                worksheet.Column(36).Width = 20;
                                                worksheet.Column(37).Width = 20;
                                                worksheet.Column(38).Width = 20;
                                                worksheet.Column(39).Width = 20;
                                                worksheet.Column(40).Width = 20;
                                                worksheet.Column(41).Width = 20;
                                                worksheet.Column(42).Width = 20;
                                                worksheet.Column(43).Width = 20;
                                                worksheet.Column(44).Width = 20;
                                                worksheet.Column(45).Width = 20;
                                                worksheet.Column(46).Width = 20;
                                                worksheet.Column(47).Width = 20;
                                                worksheet.Column(48).Width = 20;
                                                worksheet.Column(49).Width = 20;
                                                worksheet.Column(50).Width = 20;



                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 NKPD REPORT";

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

                }
                #endregion

                return true;
            }//else if
            #endregion

            else
            {
                return false;
            }
        }

        public Boolean GenerateReportCommission(string _userID, CommissionRpt _commissionRpt)
        {
            #region Report Management Fee
            if (_commissionRpt.ReportName.Equals("Report Management Fee"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_commissionRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_commissionRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _commissionRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            string _paramFundSwitchFrom = "";

                            if (!_host.findString(_commissionRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_commissionRpt.FundFrom))
                            {
                                _paramFundSwitchFrom = "And A.FundPKFrom  in ( " + _commissionRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundSwitchFrom = "";
                            }


                            string _paramFundSwitchTo = "";

                            if (!_host.findString(_commissionRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_commissionRpt.FundFrom))
                            {
                                _paramFundSwitchTo = "And A.FundPKTo  in ( " + _commissionRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundSwitchTo = "";
                            }

                            string _paramFundClientFrom = "";

                            if (!_host.findString(_commissionRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_commissionRpt.FundClientFrom))
                            {
                                _paramFundClientFrom = "And A.FundClientPK  in ( " + _commissionRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClientFrom = "";
                            }


                            cmd.CommandText = @"
                            
                            DECLARE @FCP TABLE
                            (
                            Date datetime,
                            FundPK int,
                            FundClientPK int,
                            UnitAmount numeric(19,8)
                            )

                            insert into @FCP
                            select Date,FundPK,FundClientPK,isnull(UnitAmount,0) from FundClientPosition A
                            where 1 = 1 " + _paramFundFrom + _paramFundClientFrom + @" and Date between @datefrom and @DateTo


                            DECLARE @CloseNav TABLE
                            (
                            Date datetime,
                            FundPK int,
                            Nav numeric(19,8)
                            )

                            insert into @CloseNav
                            select Date,FundPK,isnull(Nav,0) from CloseNAV A
                            where 1 = 1 " + _paramFundFrom + @" and A.Date between dbo.FWorkingDay(@datefrom,-1) and @DateTo and status = 2


                            DECLARE @Trx TABLE
                            (
                            Date datetime,
                            FundPK int,
                            FundClientPK int,
                            UnitAmount numeric(19,8),
                            CashAmount numeric(19,4)
                            )

                            insert into @Trx
                            select ValueDate,FundPK,FundClientPK,isnull(sum(TotalUnitAmount),0),isnull(sum(TotalCashAmount),0)
                            from (
                            select ValueDate,FundPK,FundClientPK,TotalUnitAmount,TotalCashAmount from ClientSubscription A
                            where  1 = 1 " + _paramFundFrom + _paramFundClientFrom + @"  and ValueDate between @DateFrom and @DateTo and Status = 2
                            union all
                            select ValueDate,FundPK,FundClientPK,TotalUnitAmount * -1,TotalCashAmount * -1 from ClientRedemption A
                            where  1 = 1 " + _paramFundFrom + _paramFundClientFrom + @"  and ValueDate between @DateFrom and @DateTo and Status = 2
                            union all
                            select ValueDate,FundPKTo,FundClientPK,TotalUnitAmountFundTo,TotalCashAmountFundTo from ClientSwitching A
                            where  1 = 1 " + _paramFundSwitchTo + _paramFundClientFrom + @"  and ValueDate between @DateFrom and @DateTo and Status = 2
                            union all
                            select ValueDate,FundPKFrom,FundClientPK,TotalUnitAmountFundFrom * -1,TotalCashAmountFundFrom * -1 from ClientSwitching A
                            where  1 = 1 " + _paramFundSwitchFrom + _paramFundClientFrom + @"  and ValueDate between @DateFrom and @DateTo and Status = 2
                            ) A
                            Group by ValueDate,FundPK,FundClientPK



                            DECLARE @FundFee TABLE
                            (
                            FundPK int,
                            MFeePercent numeric(19,8),
                            MFeeDays int
                            )

                            insert into @FundFee
                            select A.FundPK,MiFeePercent/100,B.ManagementFeeDays from FundFeeSetup A
                            left join FundFee  B on A.FundPK = B.FundPK and B.status in (1,2)
                            where A.Date = (
                            Select max(date) from FundFeeSetup C where C.date	<= @DateTo  and C.status = 2 and A.FundPK = C.FundPK
                            ) and A.status in (1,2) " + _paramFundFrom + @"


                            SELECT B.Date,H.Name FundName,E.Name FundClientName,isnull(C.Nav,0) NavPerUnit,isnull(D.UnitAmount,0) Units,isnull(A.UnitAmount,0) OSUnits, isnull(D.CashAmount,0) TransactionAmount,
                            isnull(C.Nav,0) * isnull(A.UnitAmount,0) FUM,isnull(isnull(C.Nav,0) * isnull(A.UnitAmount,0) * (isnull(G.MFeePercent,0) * isnull(F.AgentFee,100)/100) /G.MFeeDays,0) Gross,
                            isnull(isnull(C.Nav,0) * isnull(A.UnitAmount,0) * (isnull(G.MFeePercent,0) * isnull(F.AgentFee,100)/100) /G.MFeeDays * 0.02,0) PPH23,
                            isnull((isnull(C.Nav,0) * isnull(A.UnitAmount,0) * (isnull(G.MFeePercent,0) * isnull(F.AgentFee,100)/100) /G.MFeeDays) -
                            (isnull(C.Nav,0) * isnull(A.UnitAmount,0) * (isnull(G.MFeePercent,0) * isnull(F.AgentFee,100)/100) /G.MFeeDays * 0.02),0) Nett
                            FROM dbo.ZDT_WorkingDays B
                            LEFT JOIN @FCP A ON  A.Date = case when B.IsHoliday = 1 then B.DTM1 else B.Date end 
                            LEFT JOIN @CloseNav C ON  C.Date = case when B.IsHoliday = 1 then B.DTM1 else  B.DTM1 end and A.FundPK = C.FundPK
                            LEFT JOIN @Trx D ON  D.Date = B.Date and A.FundPK = D.FundPK and A.FundClientPK = D.FundClientPK
                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.status in (1,2)
                            LEFT JOIN Agent F on E.SellingAgentPK = F.AgentPK and F.status in (1,2)
                            LEFT JOIN @FundFee G on A.FundPK = G.FundPK
                            LEFT JOIN Fund H on A.FundPK = H.FundPK and H.status in (1,2)
                            WHERE B.Date BETWEEN @Datefrom AND @Dateto and A.FundPK <> 6 
                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _commissionRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _commissionRpt.ValueDateTo);
                            //cmd.Parameters.AddWithValue("@FundPK", _commissionRpt.FundFrom);
                            //cmd.Parameters.AddWithValue("@FundClientPK", _commissionRpt.FundClientFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ReportManagementFee" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ReportManagementFee" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "CommissionReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report Management Fee");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ManagementFee> rList = new List<ManagementFee>();
                                        while (dr0.Read())
                                        {

                                            ManagementFee rSingle = new ManagementFee();
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.FundClientName = Convert.ToString(dr0["FundClientName"]);
                                            rSingle.NavPerUnit = Convert.ToDecimal(dr0["NavPerUnit"]);
                                            rSingle.Units = Convert.ToDecimal(dr0["Units"]);
                                            rSingle.OSUnits = Convert.ToDecimal(dr0["OSUnits"]);
                                            rSingle.TransactionAmount = Convert.ToDecimal(dr0["TransactionAmount"]);
                                            rSingle.FUM = Convert.ToDecimal(dr0["FUM"]);
                                            rSingle.Gross = Convert.ToDecimal(dr0["Gross"]);
                                            rSingle.PPH23 = Convert.ToDecimal(dr0["PPH23"]);
                                            rSingle.Nett = Convert.ToDecimal(dr0["Nett"]);




                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { r.FundName, r.FundClientName } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;


                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "PT INDOASIA ASET MANAJEMEN";
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundClientName;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 7].Value = "bumn";
                                            //worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 8].Value = "equity";


                                            incRowExcel = incRowExcel + 2; ;

                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 1, RowG, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, RowG, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, RowG, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, RowG, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "Date";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "NAV / Unit";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "Units";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "O/S Units";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Transaction Amount";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "FUM";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Gross";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 7].Value = "Mgt Fee / day";
                                            worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "PPH 23 -  2% ";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Nett";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 9].Value = "Mgt Fee / day";
                                            worksheet.Cells[RowG, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["I" + RowG + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 12;
                                            worksheet.Cells[RowG, 1, RowG, 9].Style.Font.Size = 12;









                                            incRowExcel = incRowExcel + 2;

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

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "dd-MMM-yy";
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.NavPerUnit;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Units;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.OSUnits;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.TransactionAmount;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.FUM;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Gross;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.PPH23;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.PPH23;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }


                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).Height = 22;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "TOTAL";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                                            incRowExcel++;
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
                                        worksheet.Column(1).AutoFit();
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
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Report NAV per Periode Rekapitulasi_NAB";


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
                                        if (_commissionRpt.DownloadMode == "PDF")
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