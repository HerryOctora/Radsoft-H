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
    public class CustomClient28Reps
    {
        Host _host = new Host();


        public class TDListing
        {
            public string BondRating { get; set; }
            public decimal YieldPercent { get; set; }
            public string InvestmentStyle { get; set; }
            public string Revision { get; set; }
            public decimal BIRate { get; set; }
            public string InvestmentStrategy { get; set; }
            public string InvestmentObjective { get; set; }
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
            public string InterestDaysTypeDesc { get; set; }
            public string InterestPaymentTypeDesc { get; set; }
            public string BreakableInfo { get; set; }
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
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo
                                            ,isnull(IV.EntryDealingID,'') DealingID
                                            ,isnull(J.PTPCode,'') PTPCode
                                            ,isnull(K.ID,'') CounterpartID
                                            ,IV.valueDate
                                            ,I.ID InstrumentID
                                            ,I.Name InstrumentName
                                            ,F.ID FundID,IT.Name  InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,IV.Amount,IV.Notes, IV.RangePrice, IV.*   
                                            from Investment IV       
                                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                                            left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                                            left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                                            Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3   and IT.Type = 1
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
                                            rSingle.LastCouponDate = Convert.ToDateTime(dr0["LastCouponDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.YieldPercent = Convert.ToDecimal(dr0["YieldPercent"]);
                                            rSingle.BondRating = Convert.ToString(dr0["BondRating"]);

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
                                        }
                                        else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Rating";
                                            worksheet.Cells[incRowExcel, 4].Value = "Nominal";
                                            worksheet.Cells[incRowExcel, 5].Value = "Range Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 8].Value = "Yield %";
                                            worksheet.Cells[incRowExcel, 9].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 10].Value = "Settlement Date";
                                            worksheet.Cells[incRowExcel, 11].Value = "Last Coupon Date";
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
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;
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
                                            _range = "A" + incRowExcel + ":G" + incRowExcel;
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
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Volume;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.RangePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.SettlementDate;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "dd/MM/yyyy";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.LastCouponDate;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "dd/MM/yyyy";
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

                                    int _RowA = incRowExcel;
                                    int _RowB = incRowExcel + 7;
                                    worksheet.Cells["A" + _RowB + ":I" + _RowB].Style.Font.Bold = true;
                                    worksheet.Cells["A" + _RowB + ":I" + _RowB].Style.Font.Size = 15;


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
                                    incRowExcel++;
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
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo
                                        ,isnull(IV.EntryDealingID,'') DealingID
                                        ,isnull(J.PTPCode,'') PTPCode
                                        ,isnull(K.ID,'') CounterpartID
                                            ,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                                          F.ID FundID,IT.Name  InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,   
                                          IV.Amount,IV.Notes, IV.RangePrice,I.BondRating, IV.*   
                                          from Investment IV       
                                          left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                                          left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                                          left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2
                                          left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                                          left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                                          Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3 and IT.InstrumentTypePK in (2,3,8,9,13,14,15)
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
                                        rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                        rSingle.OrderPrice = Convert.ToDecimal(dr0["DonePrice"]);
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);
                                        rSingle.LastCouponDate = Convert.ToDateTime(dr0["LastCouponDate"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                        rSingle.YieldPercent = Convert.ToDecimal(dr0["YieldPercent"]);
                                        rSingle.BondRating = Convert.ToString(dr0["BondRating"]);
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
                                        worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                        worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "Rating";
                                        worksheet.Cells[incRowExcel, 4].Value = "Nominal";
                                        worksheet.Cells[incRowExcel, 5].Value = "Range Price";
                                        worksheet.Cells[incRowExcel, 6].Value = "Price";
                                        worksheet.Cells[incRowExcel, 7].Value = "Coupon %";
                                        worksheet.Cells[incRowExcel, 8].Value = "Yield %";
                                        worksheet.Cells[incRowExcel, 9].Value = "Amount";
                                        worksheet.Cells[incRowExcel, 10].Value = "Settlement Date";
                                        worksheet.Cells[incRowExcel, 11].Value = "Last Coupon Date";

                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":K" + incRowExcel;
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
                                            _range = "A" + incRowExcel + ":K" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 14;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                            }
                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.InvestmentPK;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Volume;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.RangePrice;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.OrderPrice;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.InterestPercent;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.YieldPercent;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.SettlementDate;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "dd/MM/yyyy";
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.LastCouponDate;
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "dd/MM/yyyy";





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        _range = "A" + incRowExcel + ":K" + incRowExcel;
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

                                        worksheet.Cells["K" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["K" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["K" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 4].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Calculate();

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
                                    worksheet.Cells["A" + _RowB + ":J" + _RowB].Style.Font.Bold = true;
                                    worksheet.Cells["A" + _RowB + ":J" + _RowB].Style.Font.Size = 15;

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

                                    string _rangeA = "A1:K" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 12];
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
                                    worksheet.Column(11).AutoFit();
                                    worksheet.Column(12).AutoFit();
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
                        string _paramFundPK = "";

                        if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And IV.InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
                            _paramMatureInvestmentPk = " InvestmentPK in (" + _listing.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = "";
                            _paramMatureInvestmentPk = "";
                        }


                        if (!_host.findString(_listing.ParamFundID.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.ParamFundID))
                        {
                            _paramFund = " And IV.FundPK in (" + _listing.ParamFundID + ") ";
                            _paramFundPK = "And FundPK in (" + _listing.ParamFundID + ") ";
                        }
                        else
                        {
                            _paramFund = "";
                            _paramFundPK = "";
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

    }
}
