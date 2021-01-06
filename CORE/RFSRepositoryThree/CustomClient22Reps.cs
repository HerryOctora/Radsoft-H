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
using Excel.FinancialFunctions;


namespace RFSRepositoryOne
{
    public class CustomClient22Reps
    {
        Host _host = new Host();

        //public class SubsVSRedempt
        //{
        //    public int NetValue { get; set; }
        //    public int Year { get; set; }
        //    public int Month_calc { get; set; }
        //    public int Month_year { get; set; }
        //    public int NumberOfRecords { get; set; }
        //    public string Bus { get; set; }
        //    public string FundName { get; set; }
        //    public string Month { get; set; }
        //    public int OrderIndex { get; set; }
        //    public int Redemption { get; set; }
        //    public int Subscription { get; set; }
        //}

        //public class PieChart
        //{
        //    //public int FundPK { get; set; }
        //    public int PortfolioBn { get; set; }
        //    public int NumberOfRecords { get; set; }
        //    public string Category { get; set; }
        //    public string Company { get; set; }
        //    public DateTime Date { get; set; }
        //    public string Item { get; set; }
        //    public int Percentage { get; set; }
        //    public int Period { get; set; }
        //    public int Portfolio { get; set; }
        //    public string Fund { get; set; }
        //}

        public class FFSSetup_22
        {
            public DateTime Date { get; set; }
            public int FundPK { get; set; }
            public string Col1 { get; set; }
            public string Col2 { get; set; }
            public string Col3 { get; set; }
            public string Col4 { get; set; }
            public string Col5 { get; set; }
            public string Col6 { get; set; }
            public string Col7 { get; set; }
            public string Col8 { get; set; }
            public string Col9 { get; set; }
            public string Col10 { get; set; }
            public string Col11 { get; set; }
            public string Col12 { get; set; }
            public string Col13 { get; set; }
            public string Col14 { get; set; }
            public string Col15 { get; set; }
            public string Col16 { get; set; }
            public string Col17 { get; set; }
            public string Col18 { get; set; }
            public string Col19 { get; set; }
            public string Col20 { get; set; }
            public string Col21 { get; set; }
            public string Col22 { get; set; }
            public string Col23 { get; set; }
            public int Col24 { get; set; }
            public string Col25 { get; set; }
            public string Col26 { get; set; }
            public string Col27 { get; set; }
            public int Image { get; set; }
            public string FundID { get; set; }
            public string IndexID { get; set; }


            public decimal AUM { get; set; }
            public decimal Nav { get; set; }
            public decimal Unit { get; set; }
            public DateTime EffectiveDate { get; set; }

            public string InstrumentType { get; set; }
            public decimal ExposurePercent { get; set; }
            public string SectorName { get; set; }

            public string InstrumentID { get; set; }
            public string InstrumentName { get; set; }

            public decimal CloseInd { get; set; }
            public int IndexPK { get; set; }


            public decimal ReturnLastMonth { get; set; }
            public decimal Return3Month { get; set; }
            public decimal Return6Month { get; set; }

            public decimal ReturnLast1Year { get; set; }
            public decimal ReturnYTD { get; set; }
            public decimal ReturnInception { get; set; }

            public decimal RateIndex { get; set; }
            public decimal ReturnLastMonthIndex { get; set; }
            public decimal Return3Monthindex { get; set; }
            public decimal Return6Monthindex { get; set; }
            public decimal ReturnLast1YearIndex { get; set; }
            public decimal ReturnYTDIndex { get; set; }
            public decimal ReturnInceptionIndex { get; set; }

            public decimal MinSubs { get; set; }
            public DateTime TanggalPeluncuran { get; set; }
            public string JenisReksadana { get; set; }
            public string MataUang { get; set; }
            public string BankCustodian { get; set; }
            public decimal BiayaPembelian { get; set; }
            public decimal BiayaPenjualankembali { get; set; }
            public string FundName { get; set; }
            public string FundType { get; set; }


            public decimal Rate { get; set; }
            public decimal RateBINDO { get; set; }
            public decimal InceptionIndex { get; set; }

            public DateTime DateFromZWorkingDays { get; set; }
            public decimal ReturnInceptionNAV { get; set; }

        }

        public class TransactionDataKPISoft
        {
            public DateTime TransactionDate { get; set; }
            public string ClientID { get; set; }
            public string ClientType { get; set; }
            public string TransactionType { get; set; }
            public string ProductName { get; set; }
            public string AgentName { get; set; }
            public decimal NAV { get; set; }
            public decimal CashAmount { get; set; }
            public decimal TotalCashAmount { get; set; }
        }

        
        public class CustommerSummaryKPI
        {
            public DateTime Date { get; set; }
            public string ClientID { get; set; }
            public string ClientType { get; set; }
            public string TransactionType { get; set; }
            public string FundName { get; set; }
            public string AgentName { get; set; }
            public decimal NAV { get; set; }
            public decimal NAVOriginal { get; set; }
            public decimal CurrentNAV { get; set; }
        }

        public class DailyTransactionBonds
        {
            public string FundName { get; set; }
            public string InstrumentID { get; set; }
            public string ISIN { get; set; }
            public string SecurityDescription { get; set; }
            public string BS { get; set; }
            public DateTime SettlementDate { get; set; }
            public DateTime LastCouponDate { get; set; }
            public DateTime NextCouponDate { get; set; }
            public DateTime MaturityDate { get; set; }
            public decimal FaceValue { get; set; }
            public decimal PricePercent { get; set; }
            public decimal PriceAmount { get; set; }
            public decimal AccruedInterestDays { get; set; }
            public decimal AccruedInterestPercent { get; set; }
            public decimal AccruedInterestIDR { get; set; }
            public decimal TaxCapitalGain { get; set; }
            public decimal TaxAccruedInt { get; set; }
            public decimal TotalTaxAmount { get; set; }
            public decimal TotalPayment { get; set; }
            public decimal CouponFrequency { get; set; }
        }

        public class CashProjection
        {
            public string FundName { get; set; }
            public decimal AUM { get; set; }
            public DateTime Date { get; set; }
            public decimal Cash { get; set; }
            public decimal Subscription { get; set; }
            public decimal Redemption { get; set; }
            public decimal Switching { get; set; }
            public decimal BuyEquity { get; set; }
            public decimal SellEquity { get; set; }
            public decimal BuyBonds { get; set; }
            public decimal SellMaturedBonds { get; set; }
            public decimal TDPlacement { get; set; }
            public decimal TDMature { get; set; }
            public decimal TotalCash { get; set; }
        }


        public class ReportYieldPorfolio
        {
            public string InstrumentID { get; set; }
            public string InstrumentName { get; set; }
            public string InstrumenttypeName { get; set; }
            public string ISINCODE { get; set; }
            public string FundName { get; set; }
            public decimal Balance { get; set; }
            public decimal Lot { get; set; }
            public decimal AvgPrice { get; set; }
            public decimal CostValue { get; set; }
            public decimal ClosePrice { get; set; }
            public decimal Dividen { get; set; }
            public decimal MarketValue { get; set; }
            public decimal Unrealised { get; set; }
        }


        public class ReportYieldPorfolioBond
        {
            public string FundName { get; set; }
            public string InstrumentID { get; set; }
            public string ISINCODE { get; set; }
            public string SecurityName { get; set; }
            public DateTime SettlementDate { get; set; }
            public DateTime MaturityDate { get; set; }
            public DateTime Date { get; set; }
            public decimal CouponRate { get; set; }
            public decimal FaceValue { get; set; }
            public decimal AveragePrice { get; set; }
            public decimal AverageCost { get; set; }
            public decimal ParValue { get; set; }
            public decimal ClosePrice { get; set; }
            public decimal CouponFrequency { get; set; }
            public decimal Yield { get; set; }
        }



        public Boolean Payment_Voucher(string _userID, Cashier _cashier)
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
                            
                        Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy,  C.Reference ,C.CashierID,      
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
                        Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy, C.Reference , C.CashierID,     
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
                        group by C.EntryUsersID, Valuedate,A.ID, A.Name, C.Reference , C.CashierID    
                        Order By row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
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
                                        rSingle.CashierID = Convert.ToString(dr0["CashierID"]);
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
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.CashierID } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "No Voucher";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CashierID;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Entry Date";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ValueDate;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Value date";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ValueDate;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
                                        //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "No Cek";
                                        ////worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CashierID;
                                        //worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel = incRowExcel + 2;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

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

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsDetail.Rate;
                                            //worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 11].Value = rsDetail.BaseDebit;
                                            //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 12].Value = rsDetail.BaseCredit;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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


                                        worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "(";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CheckedBy;
                                        worksheet.Cells[incRowExcel, 3].Value = "      )                                     (  ";
                                        worksheet.Cells[incRowExcel, 4].Value = "                                         )";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Value = "(";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 7].Value = ")";
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }

                                    string _rangeDetail = "A:G";

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
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 PAYMENT / JOURNAL VOUCHER";
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


        public Boolean Receipt_Voucher(string _userID, Cashier _cashier)
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
                            Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy,C.Reference , C.CashierID,      
                            valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
                            DebitCredit,(Case When DebitCredit ='C' then 0 else BaseCredit end) Debit,(Case When DebitCredit ='C' then BaseDebit else 0 end) Credit,F.ID DepartmentID , case when DebitCredit ='D' then 2 else 3 end Row    
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)         
                            UNION ALL         
                            Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy, C.Reference ,  C.CashierID,       
                            valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
                            'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID, 1 Row             
                            from Cashier C         
                            left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)       
                            group by C.EntryUsersID , Valuedate,A.ID, A.Name,C.Reference, C.CashierID   
                            Order By Row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Receipt Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.CashierID = Convert.ToString(dr0["CashierID"]);
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
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.CashierID } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "No Voucher";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CashierID;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Entry Date";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ValueDate;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Value date";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ValueDate;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
                                        //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "No Cek";
                                        ////worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CashierID;
                                        //worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel = incRowExcel + 2;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

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

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;

                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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


                                        worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;





                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Value = "(";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CheckedBy;
                                        worksheet.Cells[incRowExcel, 3].Value = "      )                                     (  ";
                                        worksheet.Cells[incRowExcel, 4].Value = "                                         )";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Value = "(";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 7].Value = ")";
                                        worksheet.Row(incRowExcel).PageBreak = true;


                                    }

                                    string _rangeDetail = "A:G";

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
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 Receipt VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 RECEIPT / JOURNAL VOUCHER";
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


        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {
            #region TOP10
            if (_FundAccountingRpt.ReportName.Equals("TOP 10"))
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



                            cmd.CommandText =
                            @"
                            SELECT ISNULL(B.Name,'') Companies
                            ,'STAR' Company
                            , ISNULL(C.Name,'') CompanyType
                            ,ISNULL(A.CostValue,0) CostValue
                            ,A.Date
                            ,ISNULL(D.Name,'') Fund
                            ,ISNULL(A.MarketValue,0) MarketValue
                            ,ISNULL(F.ID,'') Sector
                            ,ISNULL(A.CostValue,0) - ISNULL(A.MarketValue,0) Unrealized
                            FROM dbo.FundPosition A
                            LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.Status IN (1,2)
                            LEFT JOIN dbo.InstrumentCompanyType C ON B.InstrumentCompanyTypePK = C.InstrumentCompanyTypePK AND C.status IN (1,2)
                            LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
                            LEFT JOIN dbo.SubSector E ON B.SectorPK = E.SubSectorPK AND E.status IN (1,2)
                            LEFT JOIN dbo.Sector F ON E.SectorPK = F.SectorPK AND F.status IN (1,2)
                            WHERE A.Date BETWEEN @dateFrom AND @DateTo AND A.Status = 2 " + _paramFundFrom + @" 
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
                                    string filePath = Tools.ReportsPath + "TOP10" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "TOP10" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TOP 10");


                                        int incRowExcel = 1;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<InstrumentCompanyType> rList = new List<InstrumentCompanyType>();
                                        while (dr0.Read())
                                        {

                                            InstrumentCompanyType rSingle = new InstrumentCompanyType();
                                            //rSingle.ID = Convert.ToString(dr0["ID"]);
                                            //rSingle.Name = Convert.ToString(dr0["Name"]);
                                            //rSingle.Client = Convert.ToString(dr0["Client"]);
                                            rSingle.Companies = Convert.ToString(dr0["Companies"]);
                                            rSingle.Company = Convert.ToString(dr0["Company"]);
                                            rSingle.CompanyType = Convert.ToString(dr0["CompanyType"]);
                                            rSingle.CostValue = Convert.ToString(dr0["CostValue"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.Fund = Convert.ToString(dr0["Fund"]);
                                            rSingle.MarketValue = Convert.ToString(dr0["MarketValue"]);
                                            rSingle.Sector = Convert.ToString(dr0["Sector"]);
                                            rSingle.Unrealized = Convert.ToString(dr0["Unrealized"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                                from r in rList
                                                //orderby r.FundID ascending
                                                group r by new { } into rGroup
                                                select rGroup;


                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE FROM : ";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "DATE TO       : ";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 2; ;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Number of Records";
                                            worksheet.Cells[incRowExcel, 2].Value = "Companies";
                                            worksheet.Cells[incRowExcel, 3].Value = "Company";
                                            worksheet.Cells[incRowExcel, 4].Value = "Company Type";
                                            worksheet.Cells[incRowExcel, 5].Value = "Cost Value Bn";
                                            worksheet.Cells[incRowExcel, 6].Value = "Date";
                                            worksheet.Cells[incRowExcel, 7].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 8].Value = "Market Value Bn";
                                            worksheet.Cells[incRowExcel, 9].Value = "Sector";
                                            worksheet.Cells[incRowExcel, 10].Value = "Unrealized Gain Bn";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Companies;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Company;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.CompanyType;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail.Date).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Sector;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Unrealized;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";

                                                //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                //worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAV;
                                                //worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                            incRowExcel = incRowExcel + 2;
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 10];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 45;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 45;
                                        worksheet.Column(8).Width = 25;
                                        worksheet.Column(9).Width = 45;
                                        worksheet.Column(10).Width = 25;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 TOP 10";

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

            #region Pie Chart
            if (_FundAccountingRpt.ReportName.Equals("Pie Chart"))
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



                            cmd.CommandText =
                            @"

DECLARE @TableSTAR TABLE
(
	Category NVARCHAR(200),
	Date DATETIME,
	Item NVARCHAR(200),
	Period NVARCHAR(30),
	Portfolio NUMERIC(26,2),
	Fund NVARCHAR(100)
)



DECLARE @TableSTARMaturity TABLE
(
	Category NVARCHAR(200),
	Date DATETIME,
	Item NVARCHAR(200),
	Period NVARCHAR(30),
	Portfolio NUMERIC(26,2),
	Fund NVARCHAR(100)
)



DECLARE @CounterDate DATETIME

DECLARE @PeriodPK INT

SELECT @PeriodPK = PeriodPK FROM dbo.Period WHERE status IN (1,2) AND @DateFrom BETWEEN DateFrom AND DateTo

SET @CounterDate = @DateFrom

WHILE @CounterDate <= @DateTo
BEGIN
	-- ASSET CLASS: CASH, INTEREST RECEIVEABLE, PrepaidTax,  Other receivable, Fixed Income, Money MARKET,  EQUITIES, 
	
	
	INSERT INTO @TableSTARMaturity
	SELECT 'MATURITY',@CounterDate,
	CASE WHEN A.MaturityDate IS NULL THEN 'NO MATURITY' 
		ELSE CASE WHEN  DATEDIFF(YEAR,@CounterDate,A.MaturityDate) < 1 THEN '<1 Years'
			WHEN DATEDIFF(YEAR,@CounterDate,A.MaturityDate) >= 1 AND DATEDIFF(YEAR,@CounterDate,A.MaturityDate) <  2 THEN '1-2 Years'
				WHEN DATEDIFF(YEAR,@CounterDate,A.MaturityDate) >= 2 AND DATEDIFF(YEAR,@CounterDate,A.MaturityDate) < 5 THEN '2-5 Years'
					WHEN DATEDIFF(YEAR,@CounterDate,A.MaturityDate) >= 5 AND DATEDIFF(YEAR,@CounterDate,A.MaturityDate) < 10 THEN '5-10 Years'
						WHEN DATEDIFF(YEAR,@CounterDate,A.MaturityDate) >= 10 THEN 'Above 10 Years'
	 END END Maturity
	,FORMAT(@CounterDate,'MMM') 
	,SUM(ISNULL(A.CostValue,0))
	,B.Name
	FROM dbo.FundPosition A
	LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	WHERE A.Status = 2 AND A.Date = @CounterDate
	GROUP BY B.Name,A.MaturityDate
	
	
	
	INSERT INTO @TableSTAR
	        ( Category, Date, Item, Period,Portfolio,Fund )


	SELECT 'ASSET CLASS',@CounterDate,'CASH',FORMAT(@CounterDate,'MMM')
	,dbo.[FGetGroupAccountFundJournalBalanceByFundPK](@CounterDate,
	CASE WHEN D.BankPK = 14 THEN 37 ELSE 3 END,C.FundPK)
	,C.Name
	FROM Fund C 
	LEFT JOIN dbo.BankBranch D ON C.BankBranchPK = D.BankBranchPK AND D.status IN (1,2)
	WHERE C.Status = 2 

	UNION ALL

	SELECT 'ASSET CLASS',@CounterDate,'INTEREST RECEIVABLE',FORMAT(@CounterDate,'MMM')
	,dbo.[FGetGroupAccountFundJournalBalanceByFundPK](@CounterDate,
	24,C.FundPK)
	,C.Name
	FROM Fund C 
	LEFT JOIN dbo.BankBranch D ON C.BankBranchPK = D.BankBranchPK AND D.status IN (1,2)
	WHERE C.Status = 2 

	UNION ALL

	SELECT 'ASSET CLASS',@CounterDate,'PREPAID TAX',FORMAT(@CounterDate,'MMM')
	,dbo.[FGetGroupAccountFundJournalBalanceByFundPK](@CounterDate,
	55,C.FundPK)
	,C.Name
	FROM Fund C 
	LEFT JOIN dbo.BankBranch D ON C.BankBranchPK = D.BankBranchPK AND D.status IN (1,2)
	WHERE C.Status = 2 

	UNION ALL

	SELECT 'ASSET CLASS',@CounterDate,'OTHER RECEIVABLE',FORMAT(@CounterDate,'MMM')
	,dbo.[FGetGroupAccountFundJournalBalanceByFundPK](@CounterDate,
	58,C.FundPK)
	,C.Name
	FROM Fund C 
	LEFT JOIN dbo.BankBranch D ON C.BankBranchPK = D.BankBranchPK AND D.status IN (1,2)
	WHERE C.Status = 2 

	UNION ALL

	SELECT 'ASSET CLASS',@CounterDate,'EQUITIES',FORMAT(@CounterDate,'MMM') 
	,SUM(ISNULL(A.CostValue,0))
	,B.Name
	FROM dbo.FundPosition A
	LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	WHERE A.Status = 2 AND A.Date = @CounterDate
	AND C.InstrumentTypePK IN (1,4,16)
	GROUP BY B.Name

	UNION ALL

	SELECT 'ASSET CLASS',@CounterDate,'FIXED INCOME',FORMAT(@CounterDate,'MMM') 
	,SUM(ISNULL(A.CostValue,0))
	,B.Name
	FROM dbo.FundPosition A
	LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	WHERE A.Status = 2 AND A.Date = @CounterDate
	AND C.InstrumentTypePK IN (2,3,8,9,12,13,14,15)
	GROUP BY B.Name

	UNION ALL

	SELECT 'ASSET CLASS',@CounterDate,'MONEY MARKET',FORMAT(@CounterDate,'MMM') 
	,SUM(ISNULL(A.CostValue,0))
	,B.Name
	FROM dbo.FundPosition A
	LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	WHERE A.Status = 2 AND A.Date = @CounterDate
	AND C.InstrumentTypePK IN (5)
	GROUP BY B.Name
	

	UNION ALL

	SELECT 'COMPANY TYPE',@CounterDate,D.Name,FORMAT(@CounterDate,'MMM') 
	,SUM(ISNULL(A.CostValue,0))
	,B.Name
	FROM dbo.FundPosition A
	LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	LEFT JOIN dbo.InstrumentCompanyType D ON C.InstrumentCompanyTypePK = D.InstrumentCompanyTypePK AND D.status IN (1,2)
	WHERE A.Status = 2 AND A.Date = @CounterDate
	AND C.InstrumentTypePK IN (5) AND D.Name IS NOT null
	GROUP BY B.Name,D.Name


	UNION ALL

	SELECT 'SECTOR',@CounterDate,D.Name,FORMAT(@CounterDate,'MMM') 
	,SUM(ISNULL(A.CostValue,0))
	,B.Name
	FROM dbo.FundPosition A
	LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	LEFT JOIN dbo.Instrument C ON A.InstrumentPK = C.InstrumentPK AND C.status IN (1,2)
	LEFT JOIN dbo.SubSector D ON C.SectorPK = D.SubSectorPK AND D.status IN (1,2)
	WHERE A.Status = 2 AND A.Date = @CounterDate
	AND C.InstrumentTypePK IN (5) AND D.Name IS NOT null
	GROUP BY B.Name,D.Name

	UNION ALL

	SELECT Category,Date,item,Period,SUM(ISNULL(Portfolio,0)),Fund FROM @TableSTARMaturity
	WHERE Date = @CounterDate
	GROUP BY Category,Date,item,Period,Fund


	SET @CounterDate = Dbo.FWorkingDay(@CounterDate,1)
END

--SELECT * FROM dbo.FundJournalAccount WHERE name LIKE '%current%'

	--SELECT * FROM instrumentType

SELECT * FROM @TableSTAR
WHERE Portfolio > 0
ORDER BY Category,Fund,date,item asc
						                        
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
                                    string filePath = Tools.ReportsPath + "PieChart" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PieChart" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pie Chart");


                                        int incRowExcel = 0;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PieChart> rList = new List<PieChart>();
                                        while (dr0.Read())
                                        {
                                            PieChart rSingle = new PieChart();
                                            //rSingle.PortfolioBn = Convert.ToString(dr0["PortfolioBn"]);
                                            //rSingle.NumberOfRecords = Convert.ToString(dr0["NumberOfRecords"]);
                                            rSingle.Category = Convert.ToString(dr0["Category"]);
                                            //rSingle.Company = Convert.ToString(dr0["Company"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.Item = Convert.ToString(dr0["Item"]);
                                            //rSingle.Fund = Convert.ToString(dr0["Percentage"]);
                                            rSingle.Period = Convert.ToString(dr0["Period"]);
                                            rSingle.Portfolio = Convert.ToDecimal(dr0["Portfolio"]);
                                            rSingle.Fund = Convert.ToString(dr0["Fund"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                        from r in rList
                                        //orderby r.FundID ascending //kalo mau pakai order
                                        group r by new { } into rGroup
                                        select rGroup;


                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Portfolio Bn";
                                            worksheet.Cells[incRowExcel, 2].Value = "Number Of Records";
                                            worksheet.Cells[incRowExcel, 3].Value = "Category";
                                            worksheet.Cells[incRowExcel, 4].Value = "Company";
                                            worksheet.Cells[incRowExcel, 5].Value = "Date";
                                            worksheet.Cells[incRowExcel, 6].Value = "Item";
                                            worksheet.Cells[incRowExcel, 7].Value = "Percentage";
                                            worksheet.Cells[incRowExcel, 8].Value = "Period";
                                            worksheet.Cells[incRowExcel, 9].Value = "Portfolio";
                                            worksheet.Cells[incRowExcel, 10].Value = "Fund";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;
                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Portfolio / 1000000;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 2].Value = 1;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Category;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "STAR";
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail.Date).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Item;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Percentage + "%";
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Period;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Portfolio;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";

                                                //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                //worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAV;
                                                //worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                            incRowExcel = incRowExcel + 2;
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 10];
                                        worksheet.Column(1).Width = 12;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 30;
                                        worksheet.Column(10).Width = 45;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 PIE CHART";

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

            #region Report Yield Portfolio
            if (_FundAccountingRpt.ReportName.Equals("Report Yield Portfolio"))
            {
                try
                {
                    var _HasRowEquity = 0;
                    var _HasRowBond = 0;
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
                            declare @DividenTable Table
                            (
	                            Date date,
	                            SecurityCode nvarchar(20),
	                            ProceedRatio numeric(32,2)
                            )

                            insert into @DividenTable(Date,SecurityCode)
                            select max(Cast(CumDate as date)),SecurityCode from ZUPLOAD_DIVIDENSCHEDULE_TEMP
                            group by SecurityCode

                            update A set A.ProceedRatio = B.ProceedRatio from @DividenTable A
                            inner join ZUPLOAD_DIVIDENSCHEDULE_TEMP B on A.Date = cast(B.CumDate as date) and A.SecurityCode = B.SecurityCode


                            select B.Name FundName,D.Name InstrumenttypeName,C.ID InstrumentID, C.Name InstrumentName,
                            isnull(A.Balance,0) Balance, isnull(A.Balance/100,0) Lot,isnull(A.AvgPrice,0) AvgPrice,
                            isnull(A.CostValue,0) CostValue,isnull(A.ClosePrice,0) ClosePrice,isnull(A.MarketValue,0) MarketValue,
                            isnull(A.MarketValue-A.CostValue,0) Unrealised, isnull(E.ProceedRatio,0) Dividen ,isnull(B.ISIN,'') ISINCODE
                            from FundPosition A
                            left join Fund B on A.fundpk = B.FundPK and B.status in (1,2)
                            left join Instrument C on A. InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            left join InstrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status in (1,2)
							left join @DividenTable E on C.ID = E.SecurityCode
                            where A.date = @Date and D.InstrumentTypePK in (1) and A.Status = 2
							" + _paramFundFrom + @"
                            order by C.ID
                                ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                string filePath = Tools.ReportsPath + "ReportYieldPortfolio" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "ReportYieldPortfolio" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                int incRowExcel = 0;
                                string _rangeDetail;

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FundAccountingReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report Yield Portfolio Equity");

                                    if (!dr0.HasRows)
                                    {
                                        _HasRowEquity = 1;
                                    }
                                    else
                                    {
                                        incRowExcel = 0;

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ReportYieldPorfolio> rList = new List<ReportYieldPorfolio>();
                                        while (dr0.Read())
                                        {

                                            ReportYieldPorfolio rSingle = new ReportYieldPorfolio();
                                            rSingle.InstrumenttypeName = Convert.ToString(dr0["InstrumenttypeName"]);
                                            rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.ISINCODE = Convert.ToString(dr0["ISINCODE"]);
                                            rSingle.Balance = Convert.ToDecimal(dr0["Balance"]);
                                            rSingle.Lot = Convert.ToDecimal(dr0["Lot"]);
                                            rSingle.AvgPrice = Convert.ToDecimal(dr0["AvgPrice"]);
                                            rSingle.CostValue = Convert.ToDecimal(dr0["CostValue"]);
                                            rSingle.ClosePrice = Convert.ToDecimal(dr0["ClosePrice"]);
                                            rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"]);
                                            rSingle.Dividen = Convert.ToDecimal(dr0["Dividen"]);
                                            rSingle.Unrealised = Convert.ToDecimal(dr0["Unrealised"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                        from r in rList
                                        orderby r.FundName ascending
                                        group r by new { r.FundName, r.InstrumenttypeName } into rGroup
                                        select rGroup;

                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 2].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "INSTRUMENT TYPE : ";
                                            worksheet.Cells[incRowExcel, 7].Value = rsHeader.Key.InstrumenttypeName;
                                            worksheet.Cells[incRowExcel, 9].Value = "DATE: ";
                                            worksheet.Cells[incRowExcel, 10].Value = _FundAccountingRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "dd/MMM/yyyy";

                                            incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "INS. NAME";
                                            worksheet.Cells[incRowExcel, 4].Value = "ISINCODE";
                                            worksheet.Cells[incRowExcel, 5].Value = "BALANCE";
                                            worksheet.Cells[incRowExcel, 6].Value = "LOT";
                                            worksheet.Cells[incRowExcel, 7].Value = "AVG PRICE";
                                            worksheet.Cells[incRowExcel, 8].Value = "COST VALUE";
                                            worksheet.Cells[incRowExcel, 9].Value = "CLOSE PRICE";
                                            worksheet.Cells[incRowExcel, 10].Value = "MARKET VALUE";
                                            worksheet.Cells[incRowExcel, 11].Value = "UNREALIZED";
                                            worksheet.Cells[incRowExcel, 12].Value = "Dividend(Trailing 12 month)";
                                            worksheet.Cells[incRowExcel, 13].Value = "Total Annual Div ";
                                            worksheet.Cells[incRowExcel, 14].Value = "Div Yield ";
                                            worksheet.Cells[incRowExcel, 15].Value = "Capital Gain Yield";
                                            worksheet.Cells[incRowExcel, 16].Value = "Total Yield ";

                                            worksheet.Cells["A" + incRowExcel + ":P" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":P" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":P" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":P" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

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
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.ISINCODE;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 5].Calculate();
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Lot;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.ClosePrice;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,####0.00";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.Unrealised;
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.Dividen;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.000000";

                                                worksheet.Cells[incRowExcel, 13].Formula = "SUM(L" + incRowExcel + "*E" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 13].Calculate();

                                                worksheet.Cells[incRowExcel, 14].Formula = "SUM(M" + incRowExcel + "/H" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 14].Calculate();

                                                worksheet.Cells[incRowExcel, 15].Formula = "SUM(K" + incRowExcel + "/H" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 15].Calculate();

                                                worksheet.Cells[incRowExcel, 16].Formula = "SUM(M" + incRowExcel + "+K" + incRowExcel + ") /H" + incRowExcel;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 16].Calculate();

                                                //worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                _no++;
                                                _endRowDetail = incRowExcel;
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

                                            worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["N" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["P" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00%";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(M" + incRowExcel + "/H" + incRowExcel + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00%";
                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(K" + incRowExcel + "/H" + incRowExcel + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00%";
                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(M" + incRowExcel + "+K" + incRowExcel + ") /H" + incRowExcel;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Calculate();

                                            incRowExcel = incRowExcel + 2;

                                        }

                                        worksheet.Row(incRowExcel).PageBreak = true;

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 15];
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

                                        //worksheet.Column(10).AutoFit();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 REPORT YIELD PORTOFILIO";

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    }

                                    //yield portfolio bond
                                    worksheet = package.Workbook.Worksheets.Add("Report Yield Portfolio Bond");

                                    DbCon.Close();
                                    DbCon.Open();
                                    using (SqlCommand cmd1 = DbCon.CreateCommand())
                                    {

                                        cmd1.CommandText = @"
                                        select B.Name FundName,C.ID InstrumentID, C.Name SecurityName,A.AcqDate SettlementDate ,A.MaturityDate,@date Date,C.InterestPercent CouponRate,
                                        isnull(A.Balance,0) FaceValue, isnull(A.AvgPrice,0) AveragePrice,isnull(A.CostValue,0) AverageCost,100 ParValue,isnull(A.ClosePrice,0) ClosePrice,
                                        isnull(12/E.Priority ,0) CouponFrequency, isnull(C.ISIN,'') ISINCODE,isnull(A.Yield,0) Yield 
                                        from TableTemp22Bond A
                                        left join Fund B on A.fundpk = B.FundPK and B.status in (1,2)
                                        left join Instrument C on A. InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                        left join InstrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status in (1,2)
                                        left join MasterValue E on C.InterestPaymentType = E.Code and E.ID = 'InterestPaymentType' and E.status in (1,2)
                                        where A.date = @Date
                                        " + _paramFundFrom + @"
                                        order by C.ID

                                ";

                                        cmd1.CommandTimeout = 0;
                                        cmd1.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);

                                        using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                        {
                                            if (!dr1.HasRows)
                                            {
                                                _HasRowBond = 1;
                                            }
                                            else
                                            {

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<ReportYieldPorfolioBond> rList1 = new List<ReportYieldPorfolioBond>();
                                                while (dr1.Read())
                                                {

                                                    ReportYieldPorfolioBond rSingle1 = new ReportYieldPorfolioBond();
                                                    rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                    rSingle1.InstrumentID = Convert.ToString(dr1["InstrumentID"]);
                                                    rSingle1.ISINCODE = Convert.ToString(dr1["ISINCODE"]);
                                                    rSingle1.SecurityName = Convert.ToString(dr1["SecurityName"]);
                                                    rSingle1.SettlementDate = Convert.ToDateTime(dr1["SettlementDate"]);
                                                    rSingle1.MaturityDate = Convert.ToDateTime(dr1["MaturityDate"]);
                                                    rSingle1.Date = Convert.ToDateTime(dr1["Date"]);
                                                    rSingle1.CouponRate = Convert.ToDecimal(dr1["CouponRate"]);
                                                    rSingle1.FaceValue = Convert.ToDecimal(dr1["FaceValue"]);
                                                    rSingle1.AveragePrice = Convert.ToDecimal(dr1["AveragePrice"]);
                                                    rSingle1.AverageCost = Convert.ToDecimal(dr1["AverageCost"]);
                                                    rSingle1.ParValue = Convert.ToDecimal(dr1["ParValue"]);
                                                    rSingle1.ClosePrice = Convert.ToDecimal(dr1["ClosePrice"]);
                                                    rSingle1.CouponFrequency = Convert.ToDecimal(dr1["CouponFrequency"]);
                                                    rSingle1.Yield = Convert.ToDecimal(dr1["Yield"]);
                                                    rList1.Add(rSingle1);

                                                }

                                                var GroupByReference1 =
                                                from r in rList1
                                                orderby r.FundName ascending
                                                group r by new { r.FundName } into rGroup
                                                select rGroup;

                                                incRowExcel = 0;

                                                foreach (var rsHeader1 in GroupByReference1)
                                                {
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 2].Value = "FUND : ";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsHeader1.Key.FundName;
                                                    worksheet.Cells[incRowExcel, 7].Value = "DATE: ";
                                                    worksheet.Cells[incRowExcel, 8].Value = _FundAccountingRpt.ValueDateFrom;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd/MMM/yyyy";

                                                    incRowExcel++;
                                                    incRowExcel++;

                                                    int RowB = incRowExcel;
                                                    int RowG = incRowExcel + 1;

                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[incRowExcel, 1, RowG, 20].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells[incRowExcel, 1].Value = "NO";
                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + incRowExcel + ":A" + RowG].Merge = true;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 2].Value = "Name";
                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                    worksheet.Cells["B" + incRowExcel + ":B" + RowG].Merge = true;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = "ISIN Code";
                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                    worksheet.Cells["C" + incRowExcel + ":C" + RowG].Merge = true;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 4].Value = "Security Name";
                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                    worksheet.Cells["D" + incRowExcel + ":D" + RowG].Merge = true;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 5].Value = "Settlement Date";
                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                    worksheet.Cells["E" + incRowExcel + ":E" + RowG].Merge = true;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                    worksheet.Cells["F" + incRowExcel + ":F" + RowG].Merge = true;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 7].Value = "Date";
                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "mm/dd/yyyy";
                                                    worksheet.Cells["G" + incRowExcel + ":G" + RowG].Merge = true;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 8].Value = "Coupon Rate";
                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                    worksheet.Cells["H" + incRowExcel + ":H" + RowG].Merge = true;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 9].Value = "FaceValue";
                                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                    worksheet.Cells["I" + incRowExcel + ":I" + RowG].Merge = true;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 10].Value = "Purchase";
                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                    worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                                    worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[RowG, 10].Value = "Price (%)";
                                                    worksheet.Cells[RowG, 11].Value = "Cost";

                                                    worksheet.Cells[incRowExcel, 12].Value = "Par Value (%)";
                                                    worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                    worksheet.Cells["L" + incRowExcel + ":L" + RowG].Merge = true;
                                                    worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 13].Value = "Close Price";
                                                    worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                    worksheet.Cells["M" + incRowExcel + ":M" + RowG].Merge = true;
                                                    worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 14].Value = "Coupon Frequency";
                                                    worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                    worksheet.Cells["N" + incRowExcel + ":N" + RowG].Merge = true;
                                                    worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 15].Value = "Purchase YTM";
                                                    worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                    worksheet.Cells["O" + incRowExcel + ":O" + RowG].Merge = true;
                                                    worksheet.Cells["O" + RowB + ":O" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["O" + RowB + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 16].Value = "Market YTM";
                                                    worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                                    worksheet.Cells["P" + incRowExcel + ":P" + RowG].Merge = true;
                                                    worksheet.Cells["P" + RowB + ":P" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["P" + RowB + ":P" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 17].Value = "Current Yield";
                                                    worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                                    worksheet.Cells["Q" + incRowExcel + ":Q" + RowG].Merge = true;
                                                    worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 18].Value = "YTM amount";
                                                    worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                                    worksheet.Cells["R" + incRowExcel + ":R" + RowG].Merge = true;
                                                    worksheet.Cells["R" + RowB + ":R" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["R" + RowB + ":R" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 19].Value = "YTM/Total Face Value";
                                                    worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                                    worksheet.Cells["S" + incRowExcel + ":S" + RowG].Merge = true;
                                                    worksheet.Cells["S" + RowB + ":S" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["S" + RowB + ":S" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 20].Value = "Unrealised P/L";
                                                    worksheet.Cells[incRowExcel, 20].Style.Font.Bold = true;
                                                    worksheet.Cells["T" + incRowExcel + ":T" + RowG].Merge = true;
                                                    worksheet.Cells["T" + RowB + ":T" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["T" + RowB + ":T" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 21].Value = "%fr P/L";
                                                    worksheet.Cells[incRowExcel, 21].Style.Font.Bold = true;
                                                    worksheet.Cells["U" + incRowExcel + ":U" + RowG].Merge = true;
                                                    worksheet.Cells["U" + RowB + ":U" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["U" + RowB + ":U" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    incRowExcel = incRowExcel + 2;
                                                    int _startRowDetail = incRowExcel;
                                                    int _endRowDetail = 0;
                                                    int _no = 1;


                                                    //end area header
                                                    foreach (var rsDetail1 in rsHeader1)
                                                    {

                                                        //incRowExcel++;
                                                        worksheet.Cells[incRowExcel, 1].Value = _no;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail1.InstrumentID;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.ISINCODE;
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail1.SecurityName;
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.SettlementDate;
                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "mm/dd/yyyy";
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail1.MaturityDate;
                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "mm/dd/yyyy";
                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail1.Date;
                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "mm/dd/yyyy";
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.CouponRate / 100;
                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00%";
                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail1.FaceValue;
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,####0.00";
                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail1.AveragePrice;
                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail1.AverageCost;
                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 12].Value = rsDetail1.ParValue;
                                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 13].Value = rsDetail1.ClosePrice;
                                                        worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00000000";
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail1.CouponFrequency;
                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00%";
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail1.Yield / 100;
                                                        worksheet.Cells[incRowExcel, 15].Calculate();

                                                        worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00%";
                                                        worksheet.Cells[incRowExcel, 16].Formula = "YIELD(E" + incRowExcel + ",F" + incRowExcel + ",H" + incRowExcel + ",M" + incRowExcel + ",L" + incRowExcel + ",N" + incRowExcel + ",2)";
                                                        worksheet.Cells[incRowExcel, 16].Calculate();

                                                        worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00%";
                                                        worksheet.Cells[incRowExcel, 17].Formula = ("H" + incRowExcel + "*L" + incRowExcel) + "/M" + incRowExcel;
                                                        worksheet.Cells[incRowExcel, 17].Calculate();

                                                        worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 18].Formula = "I" + incRowExcel + "*O" + incRowExcel;
                                                        worksheet.Cells[incRowExcel, 18].Calculate();

                                                        worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00%";
                                                        worksheet.Cells[incRowExcel, 19].Formula = "R" + incRowExcel + "$I$" + incRowExcel + "/100";
                                                        worksheet.Cells[incRowExcel, 19].Calculate();

                                                        worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 20].Formula = ("I" + incRowExcel + "*M" + incRowExcel + "/100") + "-K" + incRowExcel;
                                                        worksheet.Cells[incRowExcel, 20].Calculate();

                                                        worksheet.Cells[incRowExcel, 21].Style.Numberformat.Format = "#,##0.00%";
                                                        worksheet.Cells[incRowExcel, 21].Formula = ("T" + incRowExcel + "/K" + incRowExcel) + "";
                                                        worksheet.Cells[incRowExcel, 21].Calculate();

                                                        _no++;
                                                        _endRowDetail = incRowExcel;
                                                        incRowExcel++;


                                                    }

                                                    int RowF = incRowExcel - 1;

                                                    worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["N" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["N" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["O" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["P" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["P" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["Q" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["Q" + RowB + ":Q" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["Q" + RowB + ":Q" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["Q" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["R" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["R" + RowB + ":R" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["R" + RowB + ":R" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["R" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["S" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["S" + RowB + ":S" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["S" + RowB + ":S" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["S" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["T" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["T" + RowB + ":T" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["T" + RowB + ":T" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["T" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells["U" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["U" + RowB + ":U" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["U" + RowB + ":U" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["U" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00%";
                                                    worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 21].Style.Numberformat.Format = "#,##0.00%";
                                                    worksheet.Cells[incRowExcel, 21].Formula = ("T" + incRowExcel + "/K" + incRowExcel);

                                                    worksheet.Cells[incRowExcel, 9].Calculate();
                                                    worksheet.Cells[incRowExcel, 11].Calculate();
                                                    worksheet.Cells[incRowExcel, 18].Calculate();
                                                    worksheet.Cells[incRowExcel, 19].Calculate();
                                                    worksheet.Cells[incRowExcel, 20].Calculate();
                                                    worksheet.Cells[incRowExcel, 21].Calculate();

                                                    for (int i = _startRowDetail; i <= _endRowDetail; i++)
                                                    {
                                                        worksheet.Cells[i, 19].Formula = "R" + i + "/$I$" + incRowExcel;
                                                        worksheet.Cells[i, 19].Calculate();
                                                    }

                                                    incRowExcel = incRowExcel + 2;

                                                }

                                                worksheet.Row(incRowExcel).PageBreak = true;

                                                _rangeDetail = "A:T";

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
                                                worksheet.Column(1).Width = 7;
                                                worksheet.Column(2).Width = 15;
                                                worksheet.Column(3).AutoFit();
                                                worksheet.Column(4).AutoFit();
                                                worksheet.Column(5).Width = 17;
                                                worksheet.Column(6).Width = 17;
                                                worksheet.Column(7).Width = 17;
                                                worksheet.Column(8).Width = 15;
                                                worksheet.Column(9).AutoFit();
                                                worksheet.Column(10).Width = 20;
                                                worksheet.Column(11).Width = 20;
                                                worksheet.Column(12).Width = 15;
                                                worksheet.Column(13).Width = 15;
                                                worksheet.Column(14).Width = 17;
                                                worksheet.Column(15).Width = 15;
                                                worksheet.Column(16).Width = 15;
                                                worksheet.Column(17).Width = 15;
                                                worksheet.Column(18).Width = 21;
                                                worksheet.Column(19).Width = 21;
                                                worksheet.Column(20).AutoFit();
                                                worksheet.Column(21).AutoFit();

                                                //worksheet.Column(10).AutoFit();
                                                worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                                                                               // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                worksheet.HeaderFooter.OddHeader.CenteredText = "&14 REPORT YIELD PORTOFILIO";

                                                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                                //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                                worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                            }

                                        }
                                    }

                                    if (_HasRowEquity == 1 && _HasRowBond == 1)
                                        return false;
                                    else
                                    {
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
                                select I.ISIN ISINCode,FP.FundPK , FP.AvgPrice AvgPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
F.ID FundID,F.Name FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance Balance,FP.CostValue CostValue,  
FP.ClosePrice ClosePrice
,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) 
else 0 end AccrualHarian
,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate)
* datediff(day,DATEADD(month, DATEDIFF(month, 0, @ValueDate), 0),@ValueDate )
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


,O.SInvestID,O.Name BankName,N.ID BranchID,FP.AcqDate
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
FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,O.SInvestID,O.Name,N.ID,FP.AcqDate,I.TaxExpensePercent,I.ISIN
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
                                            rSingle.ISINCode = Convert.ToString(dr0["ISINCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ISINCode"]));
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
                                                worksheet.Cells[incRowExcel, 3].Value = "ISIN CODE";
                                                worksheet.Cells[incRowExcel, 4].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 5].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 6].Value = "Lot";
                                                worksheet.Cells[incRowExcel, 7].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 8].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 9].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 10].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 11].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 12].Value = "%fr P/L";


                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

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

                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            }

                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                worksheet.Cells[incRowExcel, 3].Value = "ISIN CODE";
                                                worksheet.Cells[incRowExcel, 4].Value = "Securities Description";
                                                worksheet.Cells[incRowExcel, 5].Value = "Qty Of Unit";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Tax Accrued Interest";
                                                worksheet.Cells[incRowExcel, 8].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 9].Value = "Average Cost";
                                                worksheet.Cells[incRowExcel, 10].Value = "Book Value";
                                                worksheet.Cells[incRowExcel, 11].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 12].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 13].Value = "Unrealized Profit/(Loss)";
                                                worksheet.Cells[incRowExcel, 14].Value = "%fr P/L";

                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            }

                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowTotal = 0;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.ISINCode;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.Lot;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.UnrealizedProfitLoss / rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00%";

                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;



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

                                                    worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.ISINCode;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.TaxAccInterestBond;
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
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.UnrealizedProfitLoss / rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00%";

                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                                _endRowTotal = incRowExcel;
                                            }

                                            if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "K" + _endRowTotal + "/H" + _endRowTotal;
                                                worksheet.Cells[incRowExcel, 5].Calculate();
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 12].Calculate();


                                            }
                                            else if (rsHeader.Key.InstrumentTypePK == 5)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 5].Calculate();

                                            }
                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 14].Formula = "M" + _endRowTotal + "/J" + _endRowTotal;
                                                worksheet.Cells[incRowExcel, 5].Calculate();
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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 13];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 50;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 25;
                                        worksheet.Column(7).Width = 30;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 21;
                                        worksheet.Column(10).Width = 22;
                                        worksheet.Column(11).Width = 22;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 25;
                                        worksheet.Column(14).Width = 15;

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

            #region Daily Transaction Bonds
            if (_FundAccountingRpt.ReportName.Equals("Daily Transaction Bonds"))
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
                            select isnull(D.Name,'') FundName,isnull(B.ID,'') InstrumentID,ISNULL (B.ISIN,'') ISIN,ISNULL(B.Name,'') SecurityDescription,isnull(A.TrxTypeID,'') BS, A.ValueDate TradeDate,A.SettlementDate ,isnull(A.LastCouponDate,'') LastCouponDate,
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
                                    string filePath = Tools.ReportsPath + "DailyTransactionBonds" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionBonds" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Security Transaction Bonds");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyTransactionBonds> rList = new List<DailyTransactionBonds>();
                                        while (dr0.Read())
                                        {

                                            DailyTransactionBonds rSingle = new DailyTransactionBonds();
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.ISIN = Convert.ToString(dr0["ISIN"]);
                                            rSingle.SecurityDescription = Convert.ToString(dr0["SecurityDescription"]);
                                            rSingle.BS = Convert.ToString(dr0["BS"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.LastCouponDate = Convert.ToDateTime(dr0["LastCouponDate"]);
                                            rSingle.NextCouponDate = Convert.ToDateTime(dr0["NextCouponDate"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            rSingle.FaceValue = Convert.ToDecimal(dr0["FaceValue"]);
                                            rSingle.PricePercent = Convert.ToDecimal(dr0["PricePercent"]);
                                            rSingle.PriceAmount = Convert.ToDecimal(dr0["PriceAmount"]);
                                            rSingle.AccruedInterestDays = Convert.ToDecimal(dr0["AccruedInterestDays"]);
                                            rSingle.AccruedInterestPercent = Convert.ToDecimal(dr0["AccruedInterestPercent"]);
                                            rSingle.AccruedInterestIDR = Convert.ToDecimal(dr0["AccruedInterestIDR"]);
                                            rSingle.TaxCapitalGain = Convert.ToDecimal(dr0["TaxCapitalGain"]);
                                            rSingle.TaxAccruedInt = Convert.ToDecimal(dr0["TaxAccruedInt"]);
                                            rSingle.TotalTaxAmount = Convert.ToDecimal(dr0["TotalTaxAmount"]);
                                            rSingle.TotalPayment = Convert.ToDecimal(dr0["TotalPayment"]);
                                            rSingle.CouponFrequency = Convert.ToDecimal(dr0["CouponFrequency"]);
                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { } into rGroup
                                                select rGroup;

                                        int incRowExcel = 1;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date";
                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                        worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                        incRowExcel++;






                                        foreach (var rsHeader in GroupByReference)
                                        {


                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 1].Value = "FundName";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "InstrumentID";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "ISIN Code";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Security Description";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "B/S";
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

                                            worksheet.Cells[incRowExcel, 9].Value = "Maturity DATE";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowB, 10].Value = "Face Value";
                                            worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                            worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowB, 11].Value = "Price";
                                            worksheet.Cells[RowB, 11].Style.Font.Bold = true;
                                            worksheet.Cells[RowB, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[RowG, 11].Value = "%";
                                            worksheet.Cells[RowG, 11].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 12].Value = "IDR";
                                            worksheet.Cells[RowG, 12].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowB, 13].Value = "Accrued Interest";
                                            worksheet.Cells[RowB, 13].Style.Font.Bold = true;
                                            worksheet.Cells[RowB, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["M" + incRowExcel + ":O" + incRowExcel].Merge = true;
                                            worksheet.Cells[RowG, 13].Value = "Days";
                                            worksheet.Cells[RowG, 13].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[RowG, 14].Value = "Interest Percent";
                                            worksheet.Cells[RowG, 14].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[RowG, 15].Value = "Nominal";
                                            worksheet.Cells[RowG, 15].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

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

                                            worksheet.Cells[incRowExcel, 18].Value = "Total Tax Gain & Int";
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

                                            worksheet.Cells[incRowExcel, 21].Value = "Purchase YTM";
                                            worksheet.Cells[incRowExcel, 21].Style.Font.Bold = true;
                                            worksheet.Cells["U" + RowB + ":U" + RowG].Merge = true;
                                            worksheet.Cells["U" + RowB + ":U" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["U" + RowB + ":U" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowB, 1, RowG, 21].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[RowB, 1, RowG, 21].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[RowB, 1, RowG, 21].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[RowB, 1, RowG, 21].Style.Border.Right.Style = ExcelBorderStyle.Thin;




                                            incRowExcel = incRowExcel + 2;

                                            int first = incRowExcel;

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 21].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 21].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 21].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 21].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ISIN;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.SecurityDescription;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BS;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.SettlementDate;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.LastCouponDate;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.NextCouponDate;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.FaceValue;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.PricePercent;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00 ";
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.PriceAmount;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.AccruedInterestDays;
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.AccruedInterestPercent;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00 ";
                                                worksheet.Cells[incRowExcel, 14, incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 14, incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.AccruedInterestIDR;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 15, incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 15, incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.TaxCapitalGain;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 16, incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 16, incRowExcel, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.TaxAccruedInt;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 17, incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 17, incRowExcel, 17].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.TotalTaxAmount;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 18, incRowExcel, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 18, incRowExcel, 18].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 19].Value = rsDetail.TotalPayment;
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 19, incRowExcel, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 19, incRowExcel, 19].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 20].Value = rsDetail.CouponFrequency;
                                                worksheet.Cells[incRowExcel, 20, incRowExcel, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 20, incRowExcel, 20].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 21].Formula = "YIELD(F" + incRowExcel + ",I" + incRowExcel + ",N" + incRowExcel + ",K" + incRowExcel + ",100" + ",T" + incRowExcel + ",2" + ")/100";
                                                worksheet.Cells[incRowExcel, 21].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 21].Calculate();


                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;

                                            }









                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel + 21, 21];
                                        worksheet.Column(1).AutoFit();
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(4).AutoFit();
                                        worksheet.Column(5).AutoFit();
                                        worksheet.Column(6).Width = 21;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 21;
                                        worksheet.Column(10).AutoFit();
                                        worksheet.Column(11).AutoFit();
                                        worksheet.Column(12).AutoFit();
                                        worksheet.Column(13).AutoFit();
                                        worksheet.Column(14).AutoFit();
                                        worksheet.Column(15).AutoFit();
                                        worksheet.Column(16).Width = 21;
                                        worksheet.Column(17).Width = 21;
                                        worksheet.Column(18).Width = 21;
                                        worksheet.Column(19).AutoFit();
                                        worksheet.Column(20).Width = 21;
                                        worksheet.Column(21).Width = 21;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Daily Transaction Bonds";


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

            #region Report Cash Projection
            else if (_FundAccountingRpt.ReportName.Equals("Cash Projection"))
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

                        string pdfPath = Tools.ReportsPath + "CashProjection" + "_" + _userID + ".pdf";
                        string filePath = Tools.ReportsPath + "CashProjection" + "_" + _userID + ".xlsx";
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
                                                    select distinct FundPK from Fund where status = 2
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
                                    string _selectsum = "";

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
                            
---DECLARE @Date DATETIME
--DECLARE @FundPK int

--SET @Date = '02/19/19'
--SET @FundPK = 2

--DROP TABLE #FinalResult

DECLARE @Result TABLE
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME
)

DECLARE @CounterDate datetime
SET @CounterDate = @DateFrom


WHILE @CounterDate <= @DateTo
BEGIN

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 1,'Cash',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 2,'Subscription',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 3,'Redemption',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 4,'Switching',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 5,'Settle Buy Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 6,'Settle Sell Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 7,'Settle Buy Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 8,'Settle Sell Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 9,'Placement Time Deposit',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 10,'Time Deposit Mature',@CounterDate

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
			
-- Subscription
		SELECT 2 Position,A.ValueDate,CashAmount TotalAmount FROM dbo.ClientSubscription A
		WHERE status <> 3 
		AND ValueDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- Redemption
		SELECT 3 Position,A.ValueDate,CASE when CashAmount > 0 THEN A.CashAmount * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPK) * -1 END TotalAmount
		FROM dbo.ClientRedemption A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- Switching - IN
		SELECT 4 Position,A.ValueDate,CASE when TotalCashAmountFundTo > 0 THEN A.TotalCashAmountFundTo * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPKTo) * -1 END TotalAmount
		FROM dbo.ClientSwitching A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @DateFrom AND @DateTo
		AND FundPKTo = @FundPK

		UNION ALL
		
-- Switching - OUT
		SELECT 4 Position,A.ValueDate,CASE when TotalCashAmountFundFrom > 0 THEN A.TotalCashAmountFundFrom * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPKFrom) * -1 END TotalAmount
		FROM dbo.ClientSwitching A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @DateFrom AND @DateTo
		AND FundPKFrom = @FundPK

		UNION ALL	

-- EQUITY BUY
		SELECT 5 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 1
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- EQUITY SELL
		SELECT 6 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 2
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

	UNION ALL

-- BOND BUY
		SELECT 7 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 1
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- BOND SELL
		SELECT 8 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 2
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- DEPOSITO BUY
		SELECT 9 Position,ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 1
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL
	
-- DEPOSITO MATURED
		SELECT 10 Position,MaturityDate ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND MaturityDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		union all
		
-- DEPOSITO BREAK
		SELECT 10 Position,ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 2
		AND ValueDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

	)A
	GROUP BY A.Position,A.ValueDate
)B ON A.Date = B.ValueDate AND A.Position = B.Position


DECLARE @FirstCashDate DATETIME
SET @FirstCashDate = dbo.FWorkingDay(@DateFrom,-1)


UPDATE #FinalResult
SET Balance = dbo.FGetGroupAccountFundJournalBalanceByFundPK(@FirstCashDate,2,@FundPK)
WHERE Date = @DateFrom AND Position = 1



DECLARE @TotalBalance NUMERIC(22,4)


SET @CounterDate = @DateFrom
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
                                                    cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                                                    cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);
                                                    cmd.Parameters.AddWithValue("@FundPK", rsHeader.Key.FundPK);

                                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                                    {
                                                        if (!dr0.HasRows)
                                                        {
                                                            return false;
                                                        }
                                                        else
                                                        {



                                                            worksheet.Cells[incRowExcel, 1].Value = "CASH PROJECTION";
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 14;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Column(incRowExcel).Width = 25;
                                                            incRowExcel++;
                                                            incRowExcel++;
                                                            worksheet.Cells[incRowExcel, 1].Value = "FUND";
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 14;
                                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(rsHeader.Key.FundPK.ToString());
                                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Size = 14;
                                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Bold = true;
                                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                                            worksheet.Column(incRowExcel).Width = 20;
                                                            incRowExcel++;
                                                            worksheet.Cells[incRowExcel, 1].Value = "AUM";
                                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 14;
                                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_AUMFundFrom(rsHeader.Key.FundPK.ToString(), _FundAccountingRpt.ValueDateTo);
                                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0";
                                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Size = 14;
                                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Bold = true;
                                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;



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
                                                            _startRowDetail = incRowExcel;
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
DECLARE @Result TABLE
(
	Position INT,
	Name NVARCHAR(300),
	Date DATETIME
)

DECLARE @CounterDate datetime
SET @CounterDate = @DateFrom


WHILE @CounterDate <= @DateTo
BEGIN

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 1,'Cash',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 2,'Subscription',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 3,'Redemption',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 4,'Switching',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 5,'Settle Buy Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 6,'Settle Sell Equity',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 7,'Settle Buy Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 8,'Settle Sell Bond',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 9,'Placement Time Deposit',@CounterDate

	INSERT INTO @Result
        ( Position, Name, Date )
	SELECT 10,'Time Deposit Mature',@CounterDate

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
			
-- Subscription
		SELECT 2 Position,A.ValueDate,CashAmount TotalAmount FROM dbo.ClientSubscription A
		WHERE status <> 3 
		AND ValueDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- Redemption
		SELECT 3 Position,A.ValueDate,CASE when CashAmount > 0 THEN A.CashAmount * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPK) * -1 END TotalAmount
		FROM dbo.ClientRedemption A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- Switching - IN
		SELECT 4 Position,A.ValueDate,CASE when TotalCashAmountFundTo > 0 THEN A.TotalCashAmountFundTo * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPKTo) * -1 END TotalAmount
		FROM dbo.ClientSwitching A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @DateFrom AND @DateTo
		AND FundPKTo = @FundPK

		UNION ALL
		
-- Switching - OUT
		SELECT 4 Position,A.ValueDate,CASE when TotalCashAmountFundFrom > 0 THEN A.TotalCashAmountFundFrom * -1
		ELSE A.UnitAmount * dbo.FgetLastCloseNav(ValueDate,FundPKFrom) * -1 END TotalAmount
		FROM dbo.ClientSwitching A
		WHERE status <> 3 
		AND A.PaymentDate BETWEEN @DateFrom AND @DateTo
		AND FundPKFrom = @FundPK

		UNION ALL	

-- EQUITY BUY
		SELECT 5 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 1
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- EQUITY SELL
		SELECT 6 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(1,4,16)
		AND TrxType = 2
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

	UNION ALL

-- BOND BUY
		SELECT 7 Position,SettlementDate ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 1
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- BOND SELL
		SELECT 8 Position,SettlementDate ValueDate,TotalAmount  FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK IN(2,3,8,9,11,12,13,14,15)
		AND TrxType = 2
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		UNION ALL

-- DEPOSITO BUY
		SELECT 9 Position,ValueDate,TotalAmount * -1 TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 1
		AND SettlementDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK
        
		UNION ALL
	
-- DEPOSITO MATURED
		SELECT 10 Position,MaturityDate ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType in (1,3)
		AND MaturityDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

		union all
		
-- DEPOSITO BREAK
		SELECT 10 Position,ValueDate,TotalAmount FROM dbo.Investment
		WHERE StatusSettlement = 2
		AND InstrumentTypePK = 5
		AND TrxType = 2
		AND ValueDate BETWEEN @DateFrom AND @DateTo
		AND FundPK = @FundPK

	)A
	GROUP BY A.Position,A.ValueDate
)B ON A.Date = B.ValueDate AND A.Position = B.Position


DECLARE @FirstCashDate DATETIME
SET @FirstCashDate = dbo.FWorkingDay(@DateFrom,-1)


UPDATE #FinalResult
SET Balance = dbo.FGetGroupAccountFundJournalBalanceByFundPK(@FirstCashDate,2,@FundPK)
WHERE Date = @DateFrom AND Position = 1



DECLARE @TotalBalance NUMERIC(22,4)


SET @CounterDate = @DateFrom
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
                                                                    cmd1.CommandTimeout = 0;
                                                                    cmd1.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                                                                    cmd1.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);
                                                                    cmd1.Parameters.AddWithValue("@FundPK", rsHeader.Key.FundPK);

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
                                                                                    for (int inc1 = 1; inc1 < dr1.FieldCount; inc1++)
                                                                                    {
                                                                                        _rowCtrp = incColExcel;
                                                                                        //_endRow = dr0.FieldCount + 2;
                                                                                        //_endRowZ = dr0.FieldCount + 1;
                                                                                        _selectsum = _host.GetAlphabet(incColExcel) + Convert.ToString(_startRowDetail) + ":" + _host.GetAlphabet(incColExcel) + Convert.ToString(incRowExcel-1);

                                                                                        worksheet.Cells[incRowExcel, incColExcel].Formula = "SUM(" + _selectsum + ")";
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Calculate();
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                                                                        worksheet.Column(incRowExcel).Width = 20;
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Font.Bold = true;
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                                        worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                                                                        incColExcel++;


                                                                                    }

                                                                                }

                                                                                worksheet.Cells[incRowExcel, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                                worksheet.Cells[incRowExcel, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

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
                            worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                            worksheet.Column(1).Width = 25;
                            worksheet.Column(2).Width = 20;
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

            else
            {
                return false;
            }
        }

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {

            #region Subs VS Redempt
            if (_unitRegistryRpt.ReportName.Equals("Subs VS Redempt"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _statusSubs = "";
                            string _statusRedemp = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                _statusRedemp = "  A.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                _statusRedemp = "  A.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }


                            cmd.CommandText =
                            @"
                            Select Month,Year, Monthcalc,Monthyear,OrderIndex,NumberofRecords,Bu,FundID,FundName, sum(isnull(Subscription,0))Subscription, sum(isnull(Redemption,0))Redemption, sum(isnull(Redemption,0) - isnull(Subscription,0))NetValue from ( 
                            Select format(cast(A.ValueDate as date),'MMM') Month,substring(convert(varchar(8),cast(A.ValueDate as date),112),3,2) Year,
                            Month(A.ValueDate) Monthcalc,substring(convert(varchar(8),cast(A.ValueDate as date),112),5,2) + '''' + substring(convert(varchar(8),cast(A.ValueDate as date),112),3,2) Monthyear,
                            substring(convert(varchar(8),cast(A.ValueDate as date),112),1,6) OrderIndex,'1'NumberofRecords,'STAR'Bu,
                            B.ID FundID,B.Name FundName,isnull(A.TotalCashAmount,0) Subscription, 0 Redemption, 0 NetValue
                            from ClientSubscription A 
                            left join Fund B on A.FundPK = B.fundPK and B.Status=2  
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status=2  
                            where  
                            " + _statusSubs + _paramFund + @"
                            and ValueDate Between @ValueDateFrom and @ValueDateTo 
                            UNION ALL 
                            Select format(cast(A.ValueDate as date),'MMM') Month,substring(convert(varchar(8),cast(A.ValueDate as date),112),3,2) Year,
                            Month(A.ValueDate) Monthcalc,substring(convert(varchar(8),cast(A.ValueDate as date),112),5,2) + '''' + substring(convert(varchar(8),cast(A.ValueDate as date),112),3,2) Monthyear,
                            substring(convert(varchar(8),cast(A.ValueDate as date),112),1,6) OrderIndex,'1'NumberofRecords,'STAR'Bu,
                            B.ID FundID,B.Name FundName, 0 Subscription,isnull(TotalCashAmount,0) Redemption, 0 NetValue
                            from ClientRedemption A 
                            left join Fund B on A.FundPK = B.fundPK and B.Status=2   
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status=2 
                            where 
                            " + _statusRedemp + _paramFund + @"
                            and ValueDate Between @ValueDateFrom and @ValueDateTo 
                            )A
                            Group by A.Month,A.Year,A.Monthcalc,A.Monthyear,A.OrderIndex,A.NumberofRecords,A.Bu,A.FundID,A.FundName
                            order by A.OrderIndex asc
                                              ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SubsVSRedempt" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SubsVSRedempt" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Subs VS Redempt");


                                        int incRowExcel = 1;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<SubsVSRedempt> rList = new List<SubsVSRedempt>();
                                        while (dr0.Read())
                                        {
                                            SubsVSRedempt rSingle = new SubsVSRedempt();
                                            rSingle.Month = dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]);
                                            rSingle.Year = Convert.ToInt32(dr0["Year"]);
                                            rSingle.Monthcalc = Convert.ToInt32(dr0["Monthcalc"]);
                                            rSingle.Monthyear = dr0["Monthyear"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Monthyear"]);
                                            rSingle.OrderIndex = dr0["OrderIndex"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OrderIndex"]);
                                            rSingle.NumberofRecords = Convert.ToInt32(dr0["NumberofRecords"]);
                                            rSingle.Bu = dr0["Bu"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Bu"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["Fundname"]);
                                            rSingle.Subscription = Convert.ToDecimal(dr0["Subscription"]);
                                            rSingle.Redemption = Convert.ToDecimal(dr0["Redemption"]);
                                            rSingle.NetValue = Convert.ToDecimal(dr0["NetValue"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                        from r in rList
                                            //orderby r.FundID ascending //kalo mau pakai order
                                        group r by new { } into rGroup
                                        select rGroup;


                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Net Value";
                                            worksheet.Cells[incRowExcel, 2].Value = "Year";
                                            worksheet.Cells[incRowExcel, 3].Value = "Month_calc";
                                            worksheet.Cells[incRowExcel, 4].Value = "Month_year";
                                            worksheet.Cells[incRowExcel, 5].Value = "Number of Records";
                                            worksheet.Cells[incRowExcel, 6].Value = "Bu";
                                            worksheet.Cells[incRowExcel, 7].Value = "Fundname";
                                            worksheet.Cells[incRowExcel, 8].Value = "Month";
                                            worksheet.Cells[incRowExcel, 9].Value = "OrderIndex";
                                            worksheet.Cells[incRowExcel, 10].Value = "Subscription";
                                            worksheet.Cells[incRowExcel, 11].Value = "Redemption";

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;
                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.NetValue;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Year;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Monthcalc;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Monthyear;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.NumberofRecords;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Bu;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Month;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.OrderIndex;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Subscription;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.Redemption;
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                                ;

                                                no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                            incRowExcel = incRowExcel + 2;
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 5;
                                        worksheet.Column(3).Width = 11;
                                        worksheet.Column(4).Width = 11;
                                        worksheet.Column(5).Width = 18;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 45;
                                        worksheet.Column(8).Width = 18;
                                        worksheet.Column(9).Width = 11;
                                        worksheet.Column(10).Width = 25;
                                        worksheet.Column(11).Width = 25;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 SUBS VS REDEMPT";

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

            #region Transaction Data KPI Soft

            if (_unitRegistryRpt.ReportName.Equals("Transaction Data KPI Soft"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSubs = "";
                            string _statusRedemp = "";
                            string _statusSwitch = "";
                            string _paramFund = "";
                            string _paramAgent = "";
                            string _paramFundClient = "";
                            string _paramDepartment = "";
                            string _paramAgentSwitching = "";
                            string _paramDepartmentSwitching = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                                _paramAgentSwitching = "And AG.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                                _paramAgentSwitching = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }
                            //if (!_host.findString(_unitRegistryRpt.DepartmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.DepartmentFrom))
                            //{
                            //    _paramDepartment = "And A.DepartmentPK in ( " + _unitRegistryRpt.DepartmentFrom + " ) ";
                            //    _paramDepartmentSwitching = "And AG.DepartmentPK in ( " + _unitRegistryRpt.DepartmentFrom + " ) ";
                            //}
                            //else
                            //{
                            //    _paramDepartment = "";
                            //    _paramDepartmentSwitching = "";
                            //}


                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                _statusSwitch = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                _statusSwitch = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                _statusSwitch = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                _statusRedemp = "  A.Status = 1  ";
                                _statusSwitch = "  A.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                _statusRedemp = "  A.Status = 3  ";
                                _statusSwitch = "  A.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusSwitch = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusSwitch = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }


                            cmd.CommandText = @"
                          Select A.TransactionDate,A.ClientID, isnull(A.ClientType,'') ClientType ,A.Type,A.ProductName,isnull(A.AgentName,'') AgentName,A.NAV ,A.CashAmount,A.TotalCashAmount TotalCashAmount
from (  
	Select A.ValueDate TransactionDate,isnull(AG.Name,'') AgentName,F.BloombergCode ProductName,'Subscription' Type,'DBXS'+ Fc.ID +'19' ClientID, TotalCashAmount * isnull(B.Rate,1) TotalCashAmount,
	case when FC.InvestorType = 1 then 'Individual'
		when Fc.InvestorType = 2 and G.Code =4 then 'Insurance'
		else 'Institusi' end ClientType
		,case when F.NavDecimalPlaces = 4 then cast(A.NAV as numeric(22,4)) else case when F.NavDecimalPlaces = 6 then
		cast(A.NAV as numeric(22,6)) else A.NAV end end NAV,CashAmount
	from ClientSubscription A 
	left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)   
	left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)   
	left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
	LEFT JOIN MasterValue G ON FC.Tipe = G.Code AND G.ID = 'CompanyType'
	left join CurrencyRate B on A.CurrencyPK = B.CurrencyPK and B.status in (1,2) and A.ValueDate = B.Date
	where A.status = 2
	and 
	NAVDate Between @ValueDateFrom and @ValueDateTo 

	UNION ALL   

	Select A.ValueDate TransactionDate,isnull(AG.Name,'') AgentName,F.BloombergCode ProductName,'Redemption' Type,'DBXS'+ Fc.ID +'19' ClientID, TotalCashAmount * -1 * isnull(B.Rate,1) TotalCashAmount,
	case when FC.InvestorType = 1 then 'Individual'
		when Fc.InvestorType = 2 and G.Code =4 then 'Insurance'
		else 'Institusi' end ClientType, case when F.NavDecimalPlaces = 4 then cast(A.NAV as numeric(22,4)) else case when F.NavDecimalPlaces = 6 then
		cast(A.NAV as numeric(22,6)) else A.NAV end end NAV,CashAmount*-1
	from ClientRedemption A 
	left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)   
	left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
	left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2) 
	LEFT JOIN MasterValue G ON FC.Tipe = G.Code AND G.ID = 'CompanyType'
	left join CurrencyRate B on A.CurrencyPK = B.CurrencyPK and B.status in (1,2) and A.ValueDate = B.Date
	where A.status = 2
	and 
	NAVDate Between @ValueDateFrom and @ValueDateTo

	UNION ALL   

	Select A.ValueDate TransactionDate,isnull(AG.Name,'') AgentName,F.BloombergCode ProductName,'Switching' Type, 'DBXS'+ Fc.ID +'19' ClientID,0 TotalCashAmount,
	case when FC.InvestorType = 1 then 'Individual'
		when Fc.InvestorType = 2 and H.Code =4 then 'Insurance'
		else 'Institusi' end ClientType, case when F.NavDecimalPlaces = 4 then cast(A.NAVFundFrom  as numeric(22,4)) else case when F.NavDecimalPlaces = 6 then
		cast(A.NAVFundFrom  as numeric(22,6)) else A.NAVFundFrom  end end NAV,CashAmount 
	from ClientSwitching A 
	left join Fund F on A.FundPKFrom = F.fundPK and f.Status in (1,2)  
	left join Fund G on A.FundPKTo = G.fundPK and G.Status in (1,2)   
	left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
	left join Agent AG on FC.SellingAgentPK = AG.AgentPK and AG.Status in (1,2) 
	LEFT JOIN MasterValue H ON FC.Tipe = H.Code AND H.ID = 'CompanyType'
	left join CurrencyRate B on A.CurrencyPK = B.CurrencyPK and B.status in (1,2) and A.ValueDate = B.Date
	where A.status = 2
	and 
	NAVDate Between @ValueDateFrom and @ValueDateTo
)A   
order by  A.TransactionDate Asc

";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "TransactionDataKPISoft" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "TransactionDataKPISoft" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Transaction Data KPI Soft");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<TransactionDataKPISoft> rList = new List<TransactionDataKPISoft>();
                                        while (dr0.Read())
                                        {

                                            TransactionDataKPISoft rSingle = new TransactionDataKPISoft();
                                            rSingle.TransactionDate = Convert.ToDateTime(dr0["TransactionDate"]);
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.ClientType = Convert.ToString(dr0["ClientType"]);
                                            rSingle.TransactionType = Convert.ToString(dr0["Type"]);
                                            rSingle.ProductName = Convert.ToString(dr0["ProductName"]);
                                            rSingle.AgentName = Convert.ToString(dr0["AgentName"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.CashAmount = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.TotalCashAmount = Convert.ToDecimal(dr0["TotalCashAmount"]);

                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    orderby r.TransactionDate ascending
                                                group r by new {  } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                        worksheet.Cells[incRowExcel, 1].Value = "Transaction Date";
                                        worksheet.Cells[incRowExcel, 2].Value = "Client ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "Client Type";
                                        worksheet.Cells[incRowExcel, 4].Value = "Transaction Type";
                                        worksheet.Cells[incRowExcel, 5].Value = "Product Name";
                                        worksheet.Cells[incRowExcel, 6].Value = "Agent Name";
                                        worksheet.Cells[incRowExcel, 7].Value = "NAV";
                                        worksheet.Cells[incRowExcel, 8].Value = "Cash Amount Original";
                                        worksheet.Cells[incRowExcel, 9].Value = "Cash Amount";

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



                                            int first = incRowExcel;

                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.TransactionDate;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "yyyy-MM-dd";
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientType;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.TransactionType;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.ProductName;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.AgentName;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAV;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CashAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.TotalCashAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                _endRowDetail = incRowExcel;

                                            }

                                            worksheet.Cells[_startRowDetail, 1, _startRowDetail, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[_endRowDetail, 1, _endRowDetail, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[_startRowDetail, 1, _endRowDetail, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[_startRowDetail, 1, _endRowDetail, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;



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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel , 9];
                                        worksheet.Column(1).Width = 21;
                                        worksheet.Column(2).Width = 21;
                                        worksheet.Column(3).Width = 21;
                                        worksheet.Column(4).Width = 21;
                                        worksheet.Column(5).Width = 21;
                                        worksheet.Column(6).Width = 25;
                                        worksheet.Column(7).Width = 21;
                                        worksheet.Column(8).Width = 21;
                                        worksheet.Column(9).Width = 21;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Transaction Data KPI Soft";


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

            #region Custommer Summary KPI
            if (_unitRegistryRpt.ReportName.Equals("Customer Summary KPI"))
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
                            if object_id('tempdb..#TempReport', 'u') is not null drop table #TempReport 
create table #TempReport
(
	FundPK int,
	FundClientPK int,
	Date date,
	NavDate date,
	UnitAmount numeric(22,8),
	NAV numeric(22,8),
	CurrencyPK int,
	CurrentNAV numeric(22,8)
)
CREATE CLUSTERED INDEX indx_TempReport ON #TempReport (FundPK,FundClientPK,Date,NavDate,CurrencyPK);


insert into #TempReport(FundPK,FundClientPK,Date,NavDate,NAV,CurrencyPK)
Select A.FundPK,D.FundClientPK,dbo.FWorkingDay(A.Date,-1),D.Date,case when C.NavDecimalPlaces = 4 then cast(A.NAV  as numeric(22,4)) else case when C.NavDecimalPlaces = 6 then
		cast(A.NAV  as numeric(22,6)) else A.NAV  end end NAV,C.CurrencyPK
FROM dbo.CloseNAV A
LEFT JOIN FundClientPosition D ON A.FundPK = D.FundPK AND D.Date = A.Date 
LEFT JOIN fundClient B ON D.FundclientPK = b.FundClientPK AND B.status IN (1,2)
LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
LEFT JOIN dbo.MasterValue E ON B.ClientCategory = E.Code AND E.ID = 'ClientCategory' and E.status in (1,2)
WHERE A.Date between @DateFrom and @DateTo and D.UnitAmount > 0 and A.Status in (1,2)

update A set A.UnitAmount = isnull(B.UnitAmount,0), A.CurrentNAV = isnull(B.UnitAmount,0) * A.Nav from #TempReport A
left join FundClientPosition B on A.FundPk = B.FundPk and A.FundClientPK = B.FundClientPK and A.Date = B.Date


select NavDate Date,'DBXS'+ C.ID +'19' ClientID,case when C.InvestorType = 1 then 'Individual'
	when C.InvestorType = 2 and G.Code =4 then 'Insurance'
	else 'Institusi' end ClientType,B.BloombergCode ProductName,CurrentNAV NAVOriginal, CurrentNAV * isnull(F.Rate,1) CurrentNAV from #TempReport A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN CurrencyRate F on B.CurrencyPK = F.CurrencyPK and F.Status in (1,2) and A.Date = F.Date
LEFT JOIN MasterValue G ON C.Tipe = G.Code AND G.ID = 'CompanyType'
order by date
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
                                    string filePath = Tools.ReportsPath + "CustomerSummaryKPI" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CustomerSummaryKPI" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customer Summary KPI");


                                        int incRowExcel = 0;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<CustommerSummaryKPI> rList = new List<CustommerSummaryKPI>();
                                        while (dr0.Read())
                                        {
                                            CustommerSummaryKPI rSingle = new CustommerSummaryKPI();


                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.ClientID = dr0["ClientID"].ToString();
                                            rSingle.ClientType = dr0["ClientType"].ToString();
                                            rSingle.FundName = dr0["ProductName"].ToString();
                                            rSingle.NAVOriginal = Convert.ToDecimal(dr0["NAVOriginal"]);
                                            rSingle.CurrentNAV = Convert.ToDecimal(dr0["CurrentNAV"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                        from r in rList
                                            //orderby r.Date ascending //kalo mau pakai order
                                        group r by new { } into rGroup
                                        select rGroup;


                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date";
                                            worksheet.Cells[incRowExcel, 2].Value = "Client ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Client Type";
                                            worksheet.Cells[incRowExcel, 4].Value = "Product Name";
                                            worksheet.Cells[incRowExcel, 5].Value = "NAV Original";
                                            worksheet.Cells[incRowExcel, 6].Value = "Current NAV";


                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            int first = incRowExcel;
                                            int no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "yyyy-MM-dd";

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientType;
                                                worksheet.Cells[incRowExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVOriginal;
                                                worksheet.Cells[incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CurrentNAV;
                                                worksheet.Cells[incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";

                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                                no++;
                                                _endRowDetail = incRowExcel;
                                            }


                                            worksheet.Cells[_startRowDetail, 1, _startRowDetail, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[_endRowDetail, 1, _endRowDetail, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[_startRowDetail, 1, _endRowDetail, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[_startRowDetail, 1, _endRowDetail, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            
                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 6];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 27;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 30;
                                        worksheet.Column(6).Width = 30;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Custommer Summary KPI";

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
            //SAMPE SINI KELUAR DARI STANDART


            else
            {
                return false;
            }
        }


        public int CheckTemplateTypeFFS(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        
                        declare @DateLast1Year datetime
                        declare @IssueDate datetime
                        declare @TemplateType int

                        set @DateLast1Year =  dateadd(month, -12, @date)


                        select @IssueDate = IssueDate,@TemplateType = TemplateType from FFSSetup A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where A.status = 2 and A.FundPK = @FundPK and Date = 
                        (
                        select max(date) from FFSSetup where status = 2 and FundPK = @FundPK
                        )


                        IF (@IssueDate > @DateLast1Year)
                        BEGIN
	                        select 3 Result
                        END
                        ELSE
                        BEGIN
	                        select @TemplateType Result
                        END
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return Convert.ToInt32(dr["Result"]);
                            }
                            return Convert.ToInt32(dr["Result"]);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string GenerateFFS(string _userID, FundAccountingRpt _FundAccountingRpt)
        {
            try
            {



                string filePath = Tools.ReportsPath + "FundFactSheet_Report_FFS" + "_" + _userID + ".xlsx";
                string pdfPath = Tools.ReportsPath + "FundFactSheet_Report_FFS" + "_" + _userID + ".pdf";
                File.Copy(Tools.ReportsTemplatePath + "22\\FFS\\" + "FFS_Template_022.xlsx", filePath, true);

                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Performance"];
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets["FFS"];
                    using (SqlConnection DbCon01 = new SqlConnection(Tools.conString))
                    {
                        DbCon01.Open();
                        using (SqlCommand cmd01 = DbCon01.CreateCommand())
                        {
                            cmd01.CommandText =
                            @" select * from CloseNav where  FundPK  = @FundPK and status  = 2 and Date = (
                                SELECT MAX(Date) FROM dbo.CloseNav WHERE Date <= @Date 
                                AND fundPK = @FundPK)
                                ";

                            cmd01.CommandTimeout = 0;
                            cmd01.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd01.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr01 = cmd01.ExecuteReader())
                            {
                                if (dr01.HasRows)
                                {

                                    #region NAV,AUM,UNIT
                                    // NAV,AUM,UNIT
                                    using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon1.Open();
                                        using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                        {
                                            cmd1.CommandText =

                                            @"select A.AUM/1000000000 AUM,A.Nav Nav,sum(UnitAmount) Unit,C.EffectiveDate,F.Name BankCustodian,
                                                Case when C.Type = 1 then 'Penjaminan'
		                                                when C.Type = 2 then 'Pendapatan Tetap'
			                                                when C.Type = 3 then 'Pasar Uang'
				                                                when C.Type = 4 then 'Terproteksi'
					                                                when C.Type = 5 then 'Ekuitas'
						                                                when C.Type = 6 then 'Indeks'
                                                when C.Type = 7 then 'EBA'
	                                                when C.Type = 8 then 'KPD'
		                                                when C.Type = 9 then 'Campuran'	
			                                                when C.Type = 10 then 'ETF'
				                                                when C.Type = 11 then 'DIRE'
					                                                else 'RDPT' end FundType
		                                                 from CloseNav A
                                                left join Fund C on A.FundPK = C.FundPK and C.status = 2
                                                left join MasterValue D on C.Type = D.Code and C.status = 2
                                                left join BankBranch E on C.BankBranchPK = E.BankBranchPK and E.status = 2
                                                left join Bank F on E.BankPK = F.BankPK and F.status = 2
                                                left join FundClientPosition B on A.FundPK = B.FundPK and B.Date  = (
                                                SELECT MAX(Date) FROM dbo.FundClientPosition WHERE Date < @Date 
                                                AND fundPK = @FundPK)
                                                where  A.FundPK  = @FundPK and A.status  = 2 and A.Date  = (
                                                SELECT MAX(Date) FROM dbo.CloseNav WHERE Date <= @Date 
                                                AND fundPK = @FundPK)
                                                Group By A.AUM,A.NAV ,C.EffectiveDate,C.Type,F.Name
                                                ";

                                            cmd1.CommandTimeout = 0;
                                            cmd1.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                            cmd1.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                                            cmd1.ExecuteNonQuery();


                                            using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_22> rList1 = new List<FFSSetup_22>();
                                                while (dr1.Read())
                                                {
                                                    FFSSetup_22 rSingle1 = new FFSSetup_22();
                                                    rSingle1.AUM = Convert.ToDecimal(dr1["AUM"]);
                                                    rSingle1.Nav = dr1["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["Nav"]);
                                                    rSingle1.Unit = Convert.ToDecimal(dr1["Unit"]);
                                                    rSingle1.EffectiveDate = Convert.ToDateTime(dr1["EffectiveDate"]);
                                                    rSingle1.FundType = Convert.ToString(dr1["FundType"]);
                                                    rSingle1.BankCustodian = Convert.ToString(dr1["BankCustodian"]);
                                                    rList1.Add(rSingle1);

                                                }


                                                var QueryByFundID1 =
                                                    from r1 in rList1
                                                    group r1 by new { } into rGroup1
                                                    select rGroup1;



                                                foreach (var rsHeader1 in QueryByFundID1)
                                                {

                                                    foreach (var rsDetail1 in rsHeader1)
                                                    {
                                                        worksheet.Cells[31, 10].Value = _FundAccountingRpt.ValueDateFrom;
                                                        worksheet.Cells[31, 11].Value = rsDetail1.FundType;
                                                        worksheet.Cells[31, 12].Value = rsDetail1.EffectiveDate;
                                                        worksheet.Cells[31, 13].Value = rsDetail1.AUM;
                                                        worksheet.Cells[31, 14].Value = rsDetail1.BankCustodian;
                                                        worksheet.Cells[31, 15].Value = rsDetail1.Nav;



                                                    }


                                                }





                                            }
                                        }
                                    }

                                    #endregion

                                    #region FFSSetup Tulisan
                                    // FFSSetup Tulisan
                                    using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon2.Open();
                                        using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                        {
                                            cmd2.CommandText =

                                            @"  select Date,A.FundPK,B.ID FundID,B.Name FundName,isnull(Col1,'') Col1,isnull(Col2,'') Col2,isnull(col3,'') col3,isnull(col4,'') col4,isnull(col5,'') col5,
                                                isnull(col6,'') col6,isnull(col7,'') col7,isnull(col8,'') col8,Image  from FFSSetup A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                where A.FundPK = @FundPK and Date = @Date and A.status = 2";

                                            cmd2.CommandTimeout = 0;
                                            cmd2.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                            cmd2.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);
                                            cmd2.ExecuteNonQuery();


                                            using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_22> rList1 = new List<FFSSetup_22>();
                                                while (dr2.Read())
                                                {
                                                    FFSSetup_22 rSingle1 = new FFSSetup_22();
                                                    rSingle1.Date = Convert.ToDateTime(dr2["Date"]);
                                                    rSingle1.FundPK = dr2["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr2["FundPK"]);
                                                    rSingle1.Col1 = dr2["Col1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col1"]);
                                                    rSingle1.Col2 = dr2["Col2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col2"]);
                                                    rSingle1.Col3 = dr2["Col3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col3"]);
                                                    rSingle1.Col4 = dr2["Col4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col4"]);
                                                    rSingle1.Col5 = dr2["Col5"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col5"]);
                                                    rSingle1.Col6 = dr2["Col6"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col6"]);
                                                    rSingle1.Col7 = dr2["Col7"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col7"]);
                                                    rSingle1.Col8 = dr2["Col8"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["Col8"]);
                                                    rSingle1.Image = dr2["Image"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr2["Image"]);
                                                    rSingle1.FundID = dr2["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["FundID"]);
                                                    rSingle1.FundName = dr2["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["FundName"]);
                                                    rList1.Add(rSingle1);

                                                }


                                                var QueryByFundID1 =
                                                    from r1 in rList1
                                                    group r1 by new { } into rGroup1
                                                    select rGroup1;


                                                int incRowExcel = 2;

                                                foreach (var rsHeader1 in QueryByFundID1)
                                                {

                                                    foreach (var rsDetail1 in rsHeader1)
                                                    {

                                                        worksheet.Cells[33, 10].Value = rsDetail1.Col1;
                                                        worksheet.Cells[33, 11].Value = rsDetail1.Col2;
                                                        worksheet.Cells[33, 12].Value = rsDetail1.Col3;
                                                        worksheet.Cells[33, 13].Value = rsDetail1.Col4;
                                                        worksheet.Cells[33, 14].Value = rsDetail1.Col5;
                                                        worksheet.Cells[33, 15].Value = rsDetail1.Col6;
                                                        worksheet.Cells[33, 16].Value = rsDetail1.Col7;
                                                        worksheet.Cells[33, 17].Value = rsDetail1.Col8;
                                                        worksheet.Cells[1, 6].Value = rsDetail1.FundName;





                                                        if (rsDetail1.Image == 1)
                                                        {

                                                            Image imgUrl = Image.FromFile(Tools.Path + "\\W1\\Template\\22\\ProfilResikoInvestasi\\1.png");
                                                            ExcelPicture pic = worksheet1.Drawings.AddPicture("FFS_022_1", imgUrl);
                                                            pic.SetPosition(54, 0, 3, 0);
                                                        }
                                                        else if (rsDetail1.Image == 2)
                                                        {

                                                            Image imgUrl = Image.FromFile(Tools.Path + "\\W1\\Template\\22\\ProfilResikoInvestasi\\2.png");
                                                            ExcelPicture pic = worksheet1.Drawings.AddPicture("FFS_022_2", imgUrl);
                                                            pic.SetPosition(54, 0, 3, 0);
                                                        }
                                                        else if (rsDetail1.Image == 3)
                                                        {

                                                            Image imgUrl = Image.FromFile(Tools.Path + "\\W1\\Template\\22\\ProfilResikoInvestasi\\3.png");
                                                            ExcelPicture pic = worksheet1.Drawings.AddPicture("FFS_022_3", imgUrl);
                                                            pic.SetPosition(54, 0, 3, 0);
                                                        }
                                                        else if (rsDetail1.Image == 4)
                                                        {

                                                            Image imgUrl = Image.FromFile(Tools.Path + "\\W1\\Template\\22\\ProfilResikoInvestasi\\4.png");
                                                            ExcelPicture pic = worksheet1.Drawings.AddPicture("FFS_022_4", imgUrl);
                                                            pic.SetPosition(54, 0, 3, 0);
                                                        }
                                                        else
                                                        {

                                                            Image imgUrl = Image.FromFile(Tools.Path + "\\W1\\Template\\22\\ProfilResikoInvestasi\\5.png");
                                                            ExcelPicture pic = worksheet1.Drawings.AddPicture("FFS_022_5", imgUrl);
                                                            pic.SetPosition(54, 0, 3, 0);
                                                        }

                                                    }




                                                }



                                            }
                                        }
                                    }
                                    #endregion

                                    #region Allocation of Investment
                                    // Allocation of Investment
                                    using (SqlConnection DbCon3 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon3.Open();
                                        using (SqlCommand cmd3 = DbCon3.CreateCommand())
                                        {
                                            cmd3.CommandText =

                                            @"
                                                
                                                declare @MV Table
                                                (
                                                Date datetime,
                                                FundPK int,
                                                MarketValue numeric(22,2)
                                                )
	
                                                declare @A Table
                                                (
                                                InstrumentType nvarchar(50),
                                                ExposurePercent numeric(22,8)
                                                )


                                                INSERT INTO @MV
                                                SELECT Date,FundPK,sum(AUM) CloseNAV FROM dbo.CloseNAV WHERE Date = 
                                                (
                                                select max(date) from CloseNAV  where date <= @Date and FundPK = @FundPK and status  = 2
                                                ) AND status = 2 AND FundPK = @FundPK
                                                group by Date,FundPK


                                                insert into @A
                                                select case when D.DescOne = 'BOND/FIXED INCOME' then 'Fixed Income' 
                                                else case when D.DescOne = 'EQUITY/SAHAM' then 'Equity' else 'Money Market' end end InstrumentType ,
                                                isnull(round(cast(sum(A.MarketValue/E.MarketValue) as numeric(22,8)),4),0) ExposurePercent  from FundPosition A
                                                left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                                                left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status in (1,2)  
                                                and A.FundPK  = @FundPK and A.status  = 2 and A.Date = 
                                                (select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2)
                                                left join MasterValue D on C.GroupType = D.Code and D.status in (1,2) and D.ID = 'InstrumentGroupType'
                                                left join @MV E on A.FundPK = E.FundPK and A.Date = E.Date
                                                where D.DescOne is not null and B.InstrumentTypePK <> 5
                                                group by D.DescOne 

                                                if exists(select * from @A)
                                                BEGIN
	                                                select * from @A
	                                                union all
	                                                select 'Money Market',1 - sum(ExposurePercent) from @A 
                                                END
                                                ELSE
                                                BEGIN
	                                                select 'Money Market' InstrumentType,1 ExposurePercent
                                                END ";

                                            cmd3.CommandTimeout = 0;
                                            cmd3.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                            cmd3.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                                            cmd3.ExecuteNonQuery();


                                            using (SqlDataReader dr3 = cmd3.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_22> rList3 = new List<FFSSetup_22>();
                                                while (dr3.Read())
                                                {
                                                    FFSSetup_22 rSingle3 = new FFSSetup_22();
                                                    rSingle3.InstrumentType = Convert.ToString(dr3["InstrumentType"]);
                                                    rSingle3.ExposurePercent = Convert.ToDecimal(dr3["ExposurePercent"]);
                                                    rList3.Add(rSingle3);

                                                }


                                                var QueryByFundID3 =
                                                    from r3 in rList3
                                                    group r3 by new { } into rGroup3
                                                    select rGroup3;



                                                foreach (var rsHeader3 in QueryByFundID3)
                                                {
                                                    int incRowExcel = 35;
                                                    foreach (var rsDetail3 in rsHeader3)
                                                    {
                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail3.InstrumentType;
                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail3.ExposurePercent * 100;
                                                        incRowExcel++;
                                                    }


                                                }



                                            }
                                        }
                                    }

                                    #endregion


                                    #region Allocation of Sector

                                    // Allocation of Sector
                                    using (SqlConnection DbCon4 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon4.Open();
                                        using (SqlCommand cmd4 = DbCon4.CreateCommand())
                                        {
                                            cmd4.CommandText =

                                            @"
                                            declare @MV Table
                                            (
                                            Date datetime,
                                            FundPK int,
                                            MarketValue numeric(22,2)
                                            )


                                            declare @Sector Table
                                            (
                                            SectorName nvarchar(200),
                                            InstrumentType nvarchar(50),
                                            ExposurePercent numeric(22,4)
                                            )

                                            declare @SectorName Table
                                            (
                                            SectorName nvarchar(200),
                                            ExposurePercent numeric(22,4)
                                            )
	

                                            INSERT INTO @MV
                                            SELECT Date,FundPK,sum(AUM) CloseNAV FROM dbo.CloseNAV WHERE Date = 
                                            (
                                            select max(date) from CloseNAV  where date <= @Date and FundPK = @FundPK and status  = 2
                                            ) AND status = 2 AND FundPK = @FundPK
                                            group by Date,FundPK


                                            insert into @Sector
                                            select isnull(lower(G.Name),'No Sector Equity') SectorName,case when D.DescOne = 'BOND/FIXED INCOME' then 'Fixed Income' 
                                            else case when D.DescOne = 'EQUITY/SAHAM' then 'Equity' else 'Money Market' end end InstrumentType,
                                            isnull(round(cast(sum(C.MarketValue/E.MarketValue) as numeric(22,8)),4),0) ExposurePercent  from InstrumentType A
                                            left join Instrument B on A.InstrumentTypePK = B.InstrumentTypePK and B.status in (1,2)
                                            left join FundPosition C on B.InstrumentPK = C.InstrumentPK and C.status in (1,2)  
                                            and C.FundPK  = @FundPK and A.status  = 2 and C.Date = 
                                            (select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2)
                                            left join MasterValue D on A.GroupType = D.Code and D.status in (1,2) and D.ID = 'InstrumentGroupType'
                                            left join @MV E on C.FundPK = E.FundPK and C.Date = E.Date
                                            left join SubSector F on B.SectorPK = F.SubSectorPK and F.status in (1,2)  
                                            left join Sector G on F.SectorPK = G.SectorPK and F.status in (1,2)  
                                            where D.DescOne is not null and A.GroupType in (1)
                                            group by G.Name,D.DescOne


                                            union all
                                            select 'Fixed Income',case when D.DescOne = 'BOND/FIXED INCOME' then 'Fixed Income' 
                                            else case when D.DescOne = 'EQUITY/SAHAM' then 'Equity' else 'Money Market' end end InstrumentType,
                                            isnull(round(cast(sum(C.MarketValue/E.MarketValue) as numeric(22,8)),4),0) ExposurePercent  from InstrumentType A
                                            left join Instrument B on A.InstrumentTypePK = B.InstrumentTypePK and B.status in (1,2)
                                            left join FundPosition C on B.InstrumentPK = C.InstrumentPK and C.status in (1,2)  
                                            and C.FundPK  = @FundPK and A.status  = 2 and C.Date = 
                                            (select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2)
                                            left join MasterValue D on A.GroupType = D.Code and D.status in (1,2) and D.ID = 'InstrumentGroupType'
                                            left join @MV E on C.FundPK = E.FundPK and C.Date = E.Date
                                            left join SubSector F on B.SectorPK = F.SubSectorPK and F.status in (1,2)
                                            left join Sector G on F.SectorPK = G.SectorPK and F.status in (1,2)    
                                            where D.DescOne is not null and A.GroupType in (2)
                                            group by D.DescOne



                                             if exists(select * from @Sector)
                                            BEGIN
	                                            insert into @SectorName
	                                            select SectorName,ExposurePercent from @Sector
	                                            union all
	                                            select 'Money Market',case when sum(ExposurePercent) >= 1 then 0 else 1 - sum(ExposurePercent) end from @Sector  
                                            END
                                            ELSE
                                            BEGIN
	                                            insert into @SectorName
	                                            select 'Money Market',1 ExposurePercent
                                            END



                                            select substring(Upper(SectorName),1,1) + substring(SectorName, 2, LEN(SectorName)) SectorName
                                            , ExposurePercent from @SectorName order by ExposurePercent desc ";

                                            cmd4.CommandTimeout = 0;
                                            cmd4.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                            cmd4.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                                            cmd4.ExecuteNonQuery();


                                            using (SqlDataReader dr4 = cmd4.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_22> rList4 = new List<FFSSetup_22>();
                                                while (dr4.Read())
                                                {
                                                    FFSSetup_22 rSingle4 = new FFSSetup_22();
                                                    rSingle4.SectorName = Convert.ToString(dr4["SectorName"]);
                                                    rSingle4.ExposurePercent = Convert.ToDecimal(dr4["ExposurePercent"]);
                                                    rList4.Add(rSingle4);

                                                }


                                                var QueryByFundID4 =
                                                    from r4 in rList4
                                                    group r4 by new { } into rGroup4
                                                    select rGroup4;



                                                foreach (var rsHeader4 in QueryByFundID4)
                                                {
                                                    int incRowExcel = 35;
                                                    foreach (var rsDetail4 in rsHeader4)
                                                    {
                                                        worksheet.Cells[incRowExcel, 12].Value = rsDetail4.SectorName;
                                                        worksheet.Cells[incRowExcel, 13].Value = rsDetail4.ExposurePercent * 100;
                                                        incRowExcel++;
                                                    }


                                                }



                                            }
                                        }
                                    }


                                    #endregion

                                    #region Top 5 Holding
                                    // Top 5 Holding
                                    using (SqlConnection DbCon5 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon5.Open();
                                        using (SqlCommand cmd5 = DbCon5.CreateCommand())
                                        {
                                            cmd5.CommandText =

                                            @"
                                                
declare @A table
(
InstrumentPK int, MV numeric(22,2)
)					

insert into @A																									
select top 5 A.InstrumentPK, sum(MarketValue) MV from FundPosition A
left join Instrument B on A.InstrumentPK =  B.InstrumentPK and B.status in (1,2)
where A.FundPK  = @FundPK and A.status  = 2 and A.Date = 
(select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2)
and B.InstrumentTypePK <> 5
group by A.InstrumentPK
order by MV desc

select  B.ID InstrumentID,B.Name InstrumentName 
from @A A 
left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
order by B.ID asc
 ";

                                            cmd5.CommandTimeout = 0;
                                            cmd5.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                            cmd5.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                                            cmd5.ExecuteNonQuery();


                                            using (SqlDataReader dr5 = cmd5.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_22> rList5 = new List<FFSSetup_22>();
                                                while (dr5.Read())
                                                {
                                                    FFSSetup_22 rSingle5 = new FFSSetup_22();
                                                    rSingle5.InstrumentID = Convert.ToString(dr5["InstrumentID"]);
                                                    rSingle5.InstrumentName = Convert.ToString(dr5["InstrumentName"]);
                                                    rList5.Add(rSingle5);

                                                }


                                                var QueryByFundID5 =
                                                    from r5 in rList5
                                                    group r5 by new { } into rGroup5
                                                    select rGroup5;



                                                foreach (var rsHeader5 in QueryByFundID5)
                                                {
                                                    int incRowExcel = 35;
                                                    foreach (var rsDetail5 in rsHeader5)
                                                    {
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail5.InstrumentID;
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail5.InstrumentName;
                                                        incRowExcel++;
                                                    }


                                                }



                                            }
                                        }
                                    }
                                    #endregion

                                    #region Benchmark Index
                                    // Benchmark Index
                                    using (SqlConnection DbCon6 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon6.Open();
                                        using (SqlCommand cmd6 = DbCon6.CreateCommand())
                                        {
                                            cmd6.CommandText =

                                            @"  

                                            Declare @EffectiveDate datetime
                                            select @EffectiveDate = IssueDate from Fund where FundPK = @FundPK and status = 2

                                            DECLARE @FFSFund TABLE 
                                            (
                                            Date DATETIME,
                                            FundPK INT
                                            )

                                            DECLARE @FFSFundPK int
                                            DECLARE @FFSDate datetime


                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @EffectiveDate AND @Date and FundPK = @FundPK


                                            DECLARE @CloseNAV TABLE
                                            (
                                            Date DATETIME,
                                            FundPK INT,
                                            AUM numeric(32,2),
                                            Nav numeric(22,8)
                                            )


                                            INSERT INTO @CloseNAV
                                            ( Date, FundPK, AUM, Nav )
		
                                            SELECT  A.Date,A.FundPK,B.AUM,B.Nav   from @FFSFund A
                                            left join [CloseNAV] B  on A.FundPK = B.FundPK and B.status = 2       
                                            where B.DATE = (select max(date) From CloseNAV where date <= A.Date and FundPK = @FundPK and status = 2 and Nav > 0)  
                                            and A.FundPK= @FundPK   and status = 2  


                                            DECLARE @BenchmarkA TABLE
                                            (
                                            Date DATETIME,
                                            FundPK INT,
                                            Rate numeric(22,12)
                                            )


                                            INSERT INTO @BenchmarkA
                                            ( Date, FundPK, Rate )
		
                                            SELECT  A.Date,A.FundPK,B.CloseInd   from @FFSFund A
                                            left join BenchmarkIndex B  on A.FundPK = B.FundPK and B.status = 2       
                                            where B.DATE = (select max(date) From BenchmarkIndex where date <= A.Date and FundPK = @FundPK and status = 2 and CloseInd > 0  and IndexPK = 7)  
                                            and A.FundPK= @FundPK   and status = 2  and B.IndexPK = 7



                                            select A.Date,A.AUM,A.Nav,isnull(B.Rate,0) RateIndex from @CloseNAV A
                                            left join @BenchmarkA B on A.Date = B.Date and A.FundPK = B.FundPK
                                            order by A.date asc
                                             ";

                                            cmd6.CommandTimeout = 0;
                                            cmd6.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                            cmd6.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                                            cmd6.ExecuteNonQuery();


                                            using (SqlDataReader dr6 = cmd6.ExecuteReader())
                                            {

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_22> rList6 = new List<FFSSetup_22>();
                                                while (dr6.Read())
                                                {
                                                    FFSSetup_22 rSingle6 = new FFSSetup_22();
                                                    rSingle6.Date = Convert.ToDateTime(dr6["Date"]);
                                                    rSingle6.AUM = dr6["AUM"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr6["AUM"]);
                                                    rSingle6.Nav = dr6["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr6["Nav"]);
                                                    rSingle6.RateIndex = dr6["RateIndex"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr6["RateIndex"]);
                                                    rList6.Add(rSingle6);

                                                }


                                                var QueryByFundID6 =
                                                    from r6 in rList6
                                                    group r6 by new { } into rGroup6
                                                    select rGroup6;


                                                int incRowExcel = 2;

                                                foreach (var rsHeader6 in QueryByFundID6)
                                                {

                                                    foreach (var rsDetail6 in rsHeader6)
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail6.Date;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail6.Nav;
                                                        incRowExcel++;




                                                    }


                                                }



                                            }
                                        }
                                    }
                                    #endregion



                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;



                                }
                                package.Save();
                                return filePath;

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


        public void Insert_TableTemp22Bond(DateTime _date, EndDayTrailsFundPortfolio _edt)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_edt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_edt.FundFrom))
                        {
                            _paramFund = "And A.FundPK in ( " + _edt.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"
                        --declare @date datetime
                        --set @date = '09/03/2020'

                        --DROP table #CPForTableTemp22Bond
                        create table #CPForTableTemp22Bond
                        (
                        FundPK int,
                        AcqDate datetime,
                        AcqPrice numeric(18,6),
                        InstrumentPK int,
                        Price numeric(18,6)

                        )

                        Delete A from TableTemp22Bond A where A.Date = @date " + _paramFund + @"

                        Insert into TableTemp22Bond
                        select @Date,A.FundPK,A.InstrumentPK,RemainingVolume,AcqPrice,isnull(C.ClosePriceValue,0) ClosePrice,B.InterestPercent,A.AcqDate,B.MaturityDate,
                        (AcqPrice * RemainingVolume)/100 CostValue,(isnull(C.ClosePriceValue,0) * A.RemainingVolume)/100 MarketValue,B.InterestDaysType,B.InterestPaymentType,0 from FiFoBondPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                        left join ClosePrice C on A.InstrumentPK = C.InstrumentPK  and C.Status  = 2 and C.Date = @date 
                        where RemainingVolume > 0 " + _paramFund + @"

                        Insert into TableTemp22Bond
                        select @Date,A.FundPK,A.InstrumentPK,RemainingVolume,AcqPrice,isnull(C.ClosePriceValue,0) ClosePrice,B.InterestPercent,A.AcqDate,B.MaturityDate,
                        (AcqPrice * RemainingVolume)/100 CostValue,(isnull(C.ClosePriceValue,0) * A.RemainingVolume)/100 MarketValue,B.InterestDaysType,B.InterestPaymentType,0 from FifoBondPositionTemp A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                        left join ClosePrice C on A.InstrumentPK = C.InstrumentPK and C.Status  = 2 and C.Date = @date 
                        where RemainingVolume > 0  and A.AcqDate <= @Date " + _paramFund + @"

                        insert into #CPForTableTemp22Bond
                        select distinct A.FundPK,A.AcqDate,A.AvgPrice,A.InstrumentPK,isnull(B.ClosePrice,0) from TableTemp22Bond A
                        left join FundPosition B on A.FundPK = B.FundPK and A.InstrumentPK = B.InstrumentPK and B.status = 2 and B.Date = @Date
                        where A.Date = @Date " + _paramFund + @"

                        update A set A.ClosePrice = B.Price, A.MarketValue = (isnull(B.Price,0) * A.Balance)/100 from TableTemp22Bond A
                        left join #CPForTableTemp22Bond B on A.FundPK = B.FundPK and A.Acqdate = B.AcqDate 
                        and A.AvgPrice = B.AcqPrice and A.InstrumentPK = B.InstrumentPK

                        select distinct FundPK,InstrumentPK,AcqDate,MaturityDate,InterestPercent,AvgPrice,InterestPaymentType,InterestDaysType from TableTemp22Bond";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            decimal Yield; double CostPrice; double InterestPercent; int InterestPaymentType; int InterestDaysType;
                            DateTime MaturityDate; DateTime AcqDate; int FundPK; int InstrumentPK;
                            while (dr0.Read())
                            {
                                FundPK = Convert.ToInt32(dr0["FundPK"]);
                                InstrumentPK = Convert.ToInt32(dr0["InstrumentPK"]);
                                AcqDate = Convert.ToDateTime(dr0["AcqDate"]);
                                MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                InterestPercent = Convert.ToDouble(dr0["InterestPercent"]);
                                CostPrice = Convert.ToDouble(dr0["AvgPrice"]);
                                InterestPaymentType = Convert.ToInt32(dr0["InterestPaymentType"]);
                                InterestDaysType = Convert.ToInt32(dr0["InterestDaysType"]);

                                Yield = Convert.ToDecimal(Financial.Yield(AcqDate, MaturityDate,
                                InterestPercent / 100, CostPrice, 100.0,
                                Tools.BondPaymentPeriodExcelConversion(InterestPaymentType),
                                Tools.BondInterestBasisExcelConvertion(InterestDaysType))
                                );

                                UpdateYieldTableTemp22Bond(Yield, _date, FundPK, InstrumentPK, AcqDate, MaturityDate, InterestPercent, CostPrice, InterestDaysType, InterestPaymentType);

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


        public void UpdateYieldTableTemp22Bond(decimal Yield, DateTime _date, int _fundPK, int _instrumentPK, DateTime _acqDate, DateTime _maturityDate, double _interestPercent, double _costPrice, int _interestDaysType, int _interestPaymentType)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Update TableTemp22Bond set Yield = @Yield * 100 where Date = @Date  and FundPK = @FundPK and InstrumentPK = @InstrumentPK
                        and AcqDate = @AcqDate and MaturityDate = @MaturityDate and InterestPercent = @InterestPercent 
                        and AvgPrice = @AvgPrice and InterestDaysType = @InterestDaysType and InterestPaymentType = @InterestPaymentType ";

                        cmd.Parameters.AddWithValue("@Yield", Yield);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@AcqDate", _acqDate);
                        cmd.Parameters.AddWithValue("@MaturityDate", _maturityDate);
                        cmd.Parameters.AddWithValue("@InterestPercent", _interestPercent);
                        cmd.Parameters.AddWithValue("@AvgPrice", _costPrice);
                        cmd.Parameters.AddWithValue("@InterestDaysType", _interestDaysType);
                        cmd.Parameters.AddWithValue("@InterestPaymentType", _interestPaymentType);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string PTPBond_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, Investment _investment)
        {

            try
            {
                string _paramSettlementPK = "";

                if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                {
                    _paramSettlementPK = " And SettlementPK in (" + _investment.stringInvestmentFrom + ") ";
                }
                else
                {
                    _paramSettlementPK = " And SettlementPK in (0) ";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
 BEGIN  
SET NOCOUNT ON         
DROP TABLE IF EXISTS dbo.#Text
DROP TABLE IF EXISTS dbo.#TaxAmount

create table #TaxAmount(      
InvestmentPK int,
TaxAmount [nvarchar](1000)  NULL,
TotalAmount [nvarchar](1000)  NULL          
)                        
        
truncate table #TaxAmount      


create table #Text(      
InvestmentPK int,
[ResultText] [nvarchar](max)  NULL          
)                        
        
truncate table #Text      
insert into #Text(InvestmentPK,ResultText)     
select 0,'Transaction Status|TA Reference ID|Data Type|TA Reference No.|Trade Date|Settlement Date|IM Code|BR Code|Fund Code|Security Code|Buy/Sell|Price|Face Value|Proceeds|Last Coupon Date|Next Coupon Date|Accrued Days|Accrued Interest Amount|Other Fee|Capital Gain Tax|Interest Income Tax|Withholding Tax|Net Proceeds|Settlement Type|Sellers Tax ID|Purpose of Transaction|Statutory Type|Remarks|Cancellation Reason|Data Type|TA Reference No.|Face Value|Acquisition Date|Acquisition Price(%)|Acquisition Amount|Capital Gain|Days of Holding Interest|Holding Interest Amount|Total Taxable Income|Tax Rate in %|Tax Amount'      
        
insert into #Text(InvestmentPK,ResultText) 
Select  A.InvestmentPK,
'NEWM' --1
+ '|' + ''
+ '|' + '1'
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,''))))
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), settlementdate, 112),''))))
+ '|' + @CompanyID
+ '|' + isnull(A.BrokerCode,'')
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundCode,''))))
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Instrument,''))))
+ '|' + cast(isnull(A.TrxType,'') as nvarchar)
+ '|' + case when A.DonePrice = 0 then '' else cast(isnull(cast(A.DonePrice as decimal(30,6)),'')as nvarchar) end 
+ '|' + case when A.Quantity = 0 then '' else cast(isnull(cast(A.Quantity as decimal(30,0)), '')as nvarchar) end
+ '|' + case when A.DonePrice = 0 then '' else cast(isnull(cast(sum(A.DonePrice * A.Quantity)/100 as decimal(30,2)), '')as nvarchar) end
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), LastCouponDate, 112),''))))
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), NextCouponDate, 112),''))))
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.AccruedDays,'')))) 
+ '|' + case when A.InterestAmount = 0 then '0' else cast(isnull(cast(isnull(A.InterestAmount,0) as decimal(30,2)),'')as nvarchar) end 
+ '|' + '0'
+ '|' + case when A.CapitalGainAmount = 0 then '0' else cast(isnull(cast(isnull(A.CapitalGainAmount,0) as decimal(30,0)),'')as nvarchar) end 
+ '|' + case when A.TaxInterestAmount = 0 then '0' else cast(isnull(cast(isnull(A.TaxInterestAmount,0) as decimal(30,0)),'')as nvarchar) end 
+ '|' + case when A.TrxType = 1 then cast(cast(isnull(sum(A.CapitalGainAmount + A.TaxInterestAmount),0) as decimal (30,0)) as nvarchar) else 'Tax Amount' end
+ '|' + case when A.TrxType = 1 then case when A.TotalAmount = 0 then '' else cast(isnull(cast(isnull(sum(A.DonePrice * A.Quantity)/100,0)  + isnull(A.InterestAmount,0) - isnull(sum(A.CapitalGainAmount + A.TaxInterestAmount),0) as decimal(30,0)),'') as nvarchar) end else 'Total Amount' end
+ '|' + case when A.TrxType = 1 then '2' else case when A.TrxType = 2  then '1' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SettlementMode,'')))) end  end             
+ '|' + case when A.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BankAccountNo,'')))) end              
+ '|' + case when A.InvestmentTrType = 0 then '3' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentTrType,'3'))) ) end 
+ '|' + case when A.StatutoryType = 0 then '2' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.StatutoryType,'2')))) end
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentNotes,''))))
+ '|' + '' 
from (      
select A.InvestmentPK,A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
round(A.DoneAmount,0) TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, A.TrxType TransactionType,
round(A.TotalAmount,0) TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,case when A.InstrumentTypePK in (3,8,9,15) then dbo.FgetDateDiffCorporateBond(A.LastCouponDate,A.SettlementDate) else datediff(day,A.LastCouponDate,A.SettlementDate) end AccruedDays,
round(A.DoneAccruedInterest,0) InterestAmount,round(A.IncomeTaxGainAmount,0) CapitalGainAmount,round(A.IncomeTaxInterestAmount,0) TaxInterestAmount,A.AcqDate,isnull(A.AcqPrice,0) AcqPrice,isnull(A.AcqVolume,0) AcqVolume,A.TaxExpensePercent,C.NPWP BankAccountNo ,A.PurposeOfTransaction,A.StatutoryType,A.InvestmentTrType from investment A
left join counterpart B on A.counterpartpk = B.counterpartpk and b.status = 2
left join Fund C on A.fundpk = C.fundpk and C.status = 2
left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2
left join MasterValue E on E.Code = A.SettlementMode and E.status =2  and E.ID ='SettlementMode'
left join FundCashRef F on A.fundcashrefpk = F.fundcashrefpk and F.status = 2 and F.Type = 3
left join InvestmentTaxDataAcq G on A.InvestmentPK = G.InvestmentPK and G.AcqNo = 1
where    
A.ValueDate = @valuedate and A.InstrumentTypePK in (2,3,9,13,15) 
" + _paramSettlementPK + @" 
and A.statusdealing = 2 

)A    
Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,AcqDate,AcqPrice,AcqVolume,TaxExpensePercent,BankAccountNo,A.PurposeOfTransaction,A.StatutoryType,A.InvestmentPK,A.InvestmentTrType
order by A.ValueDate Asc

								
						

declare @counter int
set @counter = 1
while (@counter <= 30)
begin
	update A set A.ResultText = A.ResultText
	+ case when B.TrxType = 1 then '' else 
	+ '|' + case when B.TrxType = 1 then '' else '2' end  
	+ '|' + case when B.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(B.Reference,'')))) end 
	+ '|' + case when B.TrxType = 1 then '' else case when B.DoneAmount = 0 then '' else cast(isnull(cast(B.DoneAmount as decimal(30,0)), '')as nvarchar) end end 
	+ '|' + case when B.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), B.AcqDate, 112),'')))) end    
	+ '|' + case when B.TrxType = 1 then '' else case when B.AcqPrice = 0 then '' else cast(isnull(cast(B.AcqPrice as decimal(30,6)), '')as nvarchar) end end     
	+ '|' + case when B.TrxType = 1 then '' else case when B.AcqAmount = 0 then '' else cast(isnull(B.AcqAmount, '') as nvarchar) end end     
	+ '|' + case when B.TrxType = 1 then '' else case when B.IncomeTaxGainAmount = 0 then cast(0 as nvarchar) else cast(isnull(cast(isnull(B.IncomeTaxGainAmount,0) as decimal(30,2)),'')as nvarchar) end  end 
	+ '|' + case when B.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), B.DaysOfHoldingInterest , 112),'')))) end 
	+ '|' + case when B.TrxType = 1 then '' else case when B.IncomeTaxInterestAmount = 0 then cast(0 as nvarchar) else cast(isnull(cast(isnull(B.IncomeTaxInterestAmount,0) as decimal(30,0)),'')as nvarchar) end  end 
	+ '|' + case when B.TrxType = 1 then '' else case when B.TotalTaxIncomeAmount = 0 then cast(0 as nvarchar) when (B.IncomeTaxGainAmount + B.IncomeTaxInterestAmount) < 0 then cast(0 as nvarchar) else cast(isnull(cast(B.TotalTaxIncomeAmount as decimal(30,0)),'')as nvarchar) end  end 
	+ '|' + case when B.TrxType = 1 then '' else case when B.TaxExpensePercent = 0 then cast(0 as nvarchar) else cast(isnull(cast(isnull(B.TaxExpensePercent,0) as decimal(30,2)),'')as nvarchar) end  end 
	+ '|' + case when B.TrxType = 1 then '' else case when B.TaxAmount = 0 then cast(0 as nvarchar) else cast(isnull(cast(ceiling(B.TaxAmount) as decimal(30,0)),'')as nvarchar) end  end 
	End  
	from #Text A
	left join InvestmentTaxDataAcq B on A.InvestmentPK = B.InvestmentPK
	where A.InvestmentPK != 0 and B.AcqNo = @counter

	set @counter = @counter + 1
end	


insert into #TaxAmount
select A.InvestmentPK,cast(sum(CEILING(B.TaxAmount)) as numeric(30,2)), cast(round(A.DoneAmount,0) + round(A.DoneAccruedInterest,0) - sum(CEILING(B.TaxAmount))  as numeric(30,2)) from Investment A
left join InvestmentTaxDataAcq B on A.InvestmentPK = B.InvestmentPK
where A.InvestmentPK != 0 and A.ValueDate = @valuedate and A.InstrumentTypePK in (2,3,9,13,15)
" + _paramSettlementPK + @" 
and A.statusdealing = 2 
group by A.InvestmentPK,A.DoneAmount,A.DoneAccruedInterest

update A set ResultText = REPLACE(ResultText,'Tax Amount',B.TaxAmount) from #Text A 
left join #TaxAmount B on A.InvestmentPK = B.InvestmentPK
where A.InvestmentPK != 0 

update A set ResultText = REPLACE(ResultText,'Total Amount',B.TotalAmount) from #Text A 
left join #TaxAmount B on A.InvestmentPK = B.InvestmentPK
where A.InvestmentPK != 0 

select ResultText from #text 
END                                

";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "PTP_Bond.txt";
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
                                    return Tools.HtmlSinvestTextPath + "PTP_Bond.txt";
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



    }
}