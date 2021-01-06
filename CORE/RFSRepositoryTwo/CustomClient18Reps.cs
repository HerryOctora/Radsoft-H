﻿using System;
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
    public class CustomClient18Reps
    {
        Host _host = new Host();

        public Boolean Investment_ListingRpt(string _userID, InvestmentRpt _investmentRpt)
        {
            #region Investment Listing
            if (_investmentRpt.ReportName.Equals("Investment Listing"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";

                            if (!_host.findString(_investmentRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investmentRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _investmentRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,  
                                              F.ID FundID,IT.Name InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,  
                                              IV.Amount,IV.Notes, IV.RangePrice, IV.*   
                                              from Investment IV      
                                              left join Fund F on IV.FundPK = F.FundPK and F.status = 2    
                                              left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2     
                                              left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2     
                                              left join MasterValue MV on IV.InstrumentTypePK = MV.Code and MV.Status = 2 and MV.ID = 'InstrumentType'     
                                              Where  IV.ValueDate between @ValueDateFrom and @ValueDateTo and IV.StatusInvestment = 2 and IV.InvestmentPK <> 0 and (IV.StatusDealing not in (3,4)) and (IV.StatusSettlement not in (3,4)) 
                                              " + _paramFund + @"  order by IV.valueDate ";


                            cmd.Parameters.AddWithValue("@ValueDateFrom", _investmentRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _investmentRpt.ValueDateTo);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "InvestmentListing" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "InvestmentListing" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".pdf";
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
                                                rSingle.OrderPrice = Convert.ToDecimal(dr0["OrderPrice"]);

                                            }
                                            else if (rSingle.InstrumentType == "Government Bond" || rSingle.InstrumentType == "Corporate Bond")
                                            {

                                                rSingle.RangePrice = Convert.ToString(dr0["RangePrice"]);
                                                rSingle.OrderPrice = Convert.ToDecimal(dr0["OrderPrice"]);
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
                                            rSingle.Volume = Convert.ToDecimal(dr0["Volume"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.Notes = Convert.ToString(dr0["Notes"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByFundID =
                                             from r in rList
                                             orderby r.ValueDate,r.InstrumentType
                                             group r by new { r.ValueDate, r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                             select rGroup;



                                        int incRowExcel = 1;


                                        foreach (var rsHeader in GroupByFundID)
                                        {
                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                            worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsHeader.Key.ValueDate);
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

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
                                            else if (rsHeader.Key.InstrumentType == "Government Bond" || rsHeader.Key.InstrumentType == "Corporate Bond")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 4].Value = "Range Price";
                                                worksheet.Cells[incRowExcel, 5].Value = "Price";
                                                worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                                worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            }
                                            else
                                            {
                                                if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                    worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                                    worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                    worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                    worksheet.Cells[incRowExcel, 7].Value = "Amount";
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
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.OrderPrice;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
                                                }
                                                else if (rsHeader.Key.InstrumentType == "Government Bond" || rsHeader.Key.InstrumentType == "Corporate Bond")
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
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail.Amount;
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
                                            worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

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

                                            worksheet.Row(incRowExcel).PageBreak = _investmentRpt.PageBreak;
                                            incRowExcel++;
                                        }

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                        //worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
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
                                        //worksheet.Row(incRowExcel).PageBreak = _listing.PageBreak;

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                        worksheet.Column(1).AutoFit();
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).AutoFit();
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
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&34 INVESTMENT APPROVAL";
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_investmentRpt.DownloadMode == "PDF")
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

            #region Dealing Listing
            if (_investmentRpt.ReportName.Equals("Dealing Listing"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";

                            if (!_host.findString(_investmentRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investmentRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _investmentRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            cmd.CommandText = @"   

                        Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                        F.ID FundID,IT.Name InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,IV.InterestPercent,IV.TrxTypeID,isnull(IV.DonePrice,0) DonePrice,    
                        isnull(IV.Amount,0) Amount,IV.Notes, IV.RangePrice,IV.MaturityDate,isnull(IV.DoneVolume,0) DoneVolume
                        ,isnull(IV.DoneAmount,0) DoneAmount,IV.Notes,IV.AcqDate,IV.DealingPK,IV.InvestmentPK,isnull(C.ID,'') CounterpartID 
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2 
                        left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2         
                        Where  IV.ValueDate between @ValueDateFrom and @ValueDateTo and IV.StatusInvestment = 2 and IV.StatusDealing <> 3 " + _paramFund + @"
                        order by IV.valueDate ";


                            cmd.Parameters.AddWithValue("@ValueDateFrom", _investmentRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _investmentRpt.ValueDateTo);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DealingListing" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DealingListing" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".pdf";
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
                                             orderby r.ValueDate, r.FundID, r.InstrumentType
                                             group r by new { r.ValueDate, r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                             select rGroup;



                                        int incRowExcel = 1;



                                        foreach (var rsHeader in GroupByFundID)
                                        {
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                            worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsHeader.Key.ValueDate);
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

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
                                                worksheet.Cells[incRowExcel, 5].Value = "Price";
                                                worksheet.Cells[incRowExcel, 6].Value = "Done Price";
                                                worksheet.Cells[incRowExcel, 7].Value = "Done Amount";
                                                worksheet.Cells[incRowExcel, 8].Value = "Counterpart";
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
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
                                                else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                                {
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

                                            worksheet.Row(incRowExcel).PageBreak = _investmentRpt.PageBreak;
                                            incRowExcel++;

                                        }

                                        incRowExcel = incRowExcel + 2;
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
                                        worksheet.Cells[_RowB, 5].Value = "(                             )";
                                        worksheet.Cells[_RowB, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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
                                        if (_investmentRpt.DownloadMode == "PDF")
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

            #region Settlement Listing
            if (_investmentRpt.ReportName.Equals("Settlement Listing"))
            {
                if (_investmentRpt.ParamInstType == "2")
                {
                    try
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                string _paramFund = "";

                                if (!_host.findString(_investmentRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investmentRpt.FundFrom))
                                {
                                    _paramFund = "And F.FundPK in ( " + _investmentRpt.FundFrom + " ) ";
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
                                            Where  IV.ValueDate between @ValueDateFrom and @ValueDateTo and IV.InstrumentTypePK in (2,3,9,13,15)  and IV.statusSettlement = 2
                                            " + _paramFund + @" order by IV.valueDate ";

                                cmd.Parameters.AddWithValue("@ValueDateFrom", _investmentRpt.ValueDateFrom);
                                cmd.Parameters.AddWithValue("@ValueDateTo", _investmentRpt.ValueDateTo);
                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "SettlementListingBond" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "SettlementListingBond" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".pdf";
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
                                                 orderby r.ValueDate
                                                 group r by new { r.ValueDate, r.FundName, r.InstrumentID } into rGroup
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
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.ValueDate;
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
                                                    worksheet.Cells[incRowExcel, 3].Value = _investmentRpt.Message;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                    worksheet.Cells["C" + incRowExcel + ":F" + (incRowExcel + 4)].Merge = true;
                                                    incRowExcel = incRowExcel + 6;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Please Confirm Upon Settlement";
                                                    incRowExcel = incRowExcel + 2;

                                                    int _RowA = incRowExcel;
                                                    int _RowB = incRowExcel + 11;
                                                    worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Size = 15;
                                           
                                                        worksheet.Cells[incRowExcel, 1].Value = "Check By";
                                                        worksheet.Cells["D" + incRowExcel + ":E" + (incRowExcel)].Merge = true;
                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 4].Value = "Approved By";
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        incRowExcel = incRowExcel + 5;
                                                        worksheet.Cells[incRowExcel, 1].Value = "(    ";
                                                        worksheet.Cells[incRowExcel, 2].Value = ")";
                                                        worksheet.Cells[incRowExcel, 3].Value = "(    ";
                                                        worksheet.Cells[incRowExcel, 4].Value = ")     (";
                                                        worksheet.Cells[incRowExcel, 6].Value = ")";
                                                    

                                                    incRowExcel++;

                                                    worksheet.Cells["A" + _rowLine1 + ":G" + _rowLine1].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _rowLine2 + ":G" + _rowLine2].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    incRowExcel = incRowExcel + 13;
                                                    worksheet.Row(incRowExcel).PageBreak = true;
                                                }

                                                worksheet.Row(incRowExcel).PageBreak = _investmentRpt.PageBreak;
                                                incRowExcel++;
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
                                            if (_investmentRpt.DownloadMode == "PDF")
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

                else if (_investmentRpt.ParamInstType == "1")
                {
                    try
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                string _paramFund = "";

                                if (!_host.findString(_investmentRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investmentRpt.FundFrom))
                                {
                                    _paramFund = "And F.FundPK in ( " + _investmentRpt.FundFrom + " ) ";
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
                                            Where  IV.ValueDate between @ValueDateFrom and @ValueDateTo and IV.InstrumentTypePK = 1 and IV.statusSettlement = 2 
                                            " + _paramFund + @" order by IV.valueDate ";

                                cmd.Parameters.AddWithValue("@ValueDateFrom", _investmentRpt.ValueDateFrom);
                                cmd.Parameters.AddWithValue("@ValueDateTo", _investmentRpt.ValueDateTo);
                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "SettlementListingEquity" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "SettlementListingEquity" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".pdf";
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
                                                   orderby r.ValueDate
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
                                                worksheet.Cells[incRowExcel, 2].Value = _investmentRpt.Message;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet.Cells["B" + incRowExcel + ":N" + (incRowExcel + 2)].Merge = true;
                                                incRowExcel = incRowExcel + 4;

                                                int _RowA = incRowExcel;
                                                int _RowB = incRowExcel + 11;
                                                worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Bold = true;
                                                worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Size = 15;
                                                
                                                    worksheet.Cells[incRowExcel, 3].Value = "Check By";
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells["F" + incRowExcel + ":M" + incRowExcel].Merge = true;
                                                    incRowExcel = incRowExcel + 5;
                                                    worksheet.Cells[incRowExcel, 3].Value = "(    ";
                                                    worksheet.Cells[incRowExcel, 5].Value = ")";
                                                    worksheet.Cells[incRowExcel, 6].Value = "(    ";
                                                    worksheet.Cells[incRowExcel, 8].Value = ")";
                                                    worksheet.Cells[incRowExcel, 11].Value = "(    ";
                                                    worksheet.Cells[incRowExcel, 14].Value = ")";
                                                

                                                incRowExcel = incRowExcel + 13;
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
                                            if (_investmentRpt.DownloadMode == "PDF")
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

                else if (_investmentRpt.ParamInstType == "3")
                {
                    try
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                string _paramFund = "";

                                if (!_host.findString(_investmentRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investmentRpt.FundFrom))
                                {
                                    _paramFund = "And F.FundPK in ( " + _investmentRpt.FundFrom + " ) ";
                                }
                                else
                                {
                                    _paramFund = "";
                                }


                              
                                cmd.CommandText = @"


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
                            Where  IV.ValueDate between @ValueDateFrom and @ValueDateTo and IV.InstrumentTypePK = 5 and IV.statusSettlement = 2   order by IV.ValueDate
                            " + _paramFund;

                                cmd.Parameters.AddWithValue("@ValueDateFrom", _investmentRpt.ValueDateFrom);
                                cmd.Parameters.AddWithValue("@ValueDateTo", _investmentRpt.ValueDateTo);
                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "SettlementListingDeposito" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "SettlementListingDeposito" + "_" + Convert.ToDateTime(_investmentRpt.ValueDateFrom).ToShortDateString().Replace("/", "-") + "_" + _userID + ".pdf";
                                       
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
                                                else if (rSingle.TrxTypeID == "ROLLOVER")
                                                {
                                                    rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["ValueDate"]);
                                                }
                                                //rSingle.PurchaseAmount = Convert.ToDecimal(dr0["PurchaseAmount"]);
                                                rSingle.AccruedInterest = dr0["AccruedInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccruedInterest"]);
                                                //rSingle.TaxCapitalGainLoss = Convert.ToDecimal(dr0["TaxCapitalGainLoss"]);
                                                //rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                                rList.Add(rSingle);

                                            }


                                            var QueryByFundID =
                                                from r in rList
                                                orderby r.ValueDate
                                                group r by new { r.ValueDate, r.FundName, r.InstrumentID } into rGroup
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



                                                // Untuk Cetak Tebal
                                                //string _range = "A" + incRowExcel + ":F" + incRowExcel;
                                                //using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                //{
                                                //    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                //    r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                //    r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                //    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                //    r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                //    //r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                                //    r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                //    r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                //    r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                                //}
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


                                                    incRowExcel++;
                                                    //area detail
                                                    worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                    incRowExcel++;
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianContactPerson;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Fax / Telp ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianFaxNo;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.BankCustodianPhone;
                                                    _rowLine1 = incRowExcel;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankBranchName;
                                                    incRowExcel++;
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.ContactPerson;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Fax / Telp ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.FaxNo;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.Phone;
                                                    _rowLine2 = incRowExcel;
                                                    incRowExcel = incRowExcel + 1;
                                                    worksheet.Cells[incRowExcel, 1].Value = "From ";
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyName();
                                                    worksheet.Cells[incRowExcel, 5].Value = "Reference : ";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.Reference;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Date ";
                                                    worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                    worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 1].Value = "TIME DEPOSIT";
                                                    //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    //worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                    incRowExcel++;
                                                    worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Please execute the following instruction :";
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Benefary Bank ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankBranchName;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Clearing Code";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.ClearingCode;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    _rowLineA = incRowExcel;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "In Favoring of ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                    _rowLineB = incRowExcel;
                                                    incRowExcel++;
                                                    worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Amount (IDR) ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    _rowLineC = incRowExcel;
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
                                                    else
                                                    {
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDateDeposito;
                                                    }

                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Interest (%) ";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    _rowLineD = incRowExcel;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Maturity Date ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Tenor Days ";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.Tenor;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    _rowLineE = incRowExcel;
                                                    incRowExcel = incRowExcel + 2;
                                                    worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                    if (rsDetail.TrxTypeID == "PLACEMENT")
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = "NEW PLACEMENT";
                                                    }
                                                    else if (rsDetail.TrxTypeID == "LIQUIDATE")
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = "LIQUIDATE";
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = "ROLLOVER";
                                                    }

                                                    //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    //worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                    incRowExcel = incRowExcel + 2;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Notes ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells["C" + incRowExcel + ":F" + (incRowExcel + 4)].Merge = true;
                                                    worksheet.Cells[incRowExcel, 3].Value = _investmentRpt.Message;
                                                    worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                    incRowExcel = incRowExcel + 6;
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
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Bank Name ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                    _rowLineF = incRowExcel;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Account Name ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Account Number ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankAccountNo;
                                                    _rowLineG = incRowExcel;
                                                    //incRowExcel = incRowExcel + 2;
                                                    //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                    //worksheet.Cells[incRowExcel, 1].Value = "and interest to  ";
                                                    //incRowExcel++;
                                                    //worksheet.Cells[incRowExcel, 1].Value = "Bank Name ";
                                                    //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    //worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                    //_rowLineH = incRowExcel;
                                                    //incRowExcel++;
                                                    //worksheet.Cells[incRowExcel, 1].Value = "Account Name ";
                                                    //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    //worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                    //incRowExcel++;
                                                    //worksheet.Cells[incRowExcel, 1].Value = "Account Number ";
                                                    //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    //worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankAccountNo;
                                                    //_rowLineI = incRowExcel;
                                                    incRowExcel++;
                                                    incRowExcel = incRowExcel + 4;

                                                    int _RowA = incRowExcel;
                                                    int _RowB = incRowExcel + 11;
                                                    worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Bold = true;
                                                    worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Size = 15;
                                                    
                                                        worksheet.Cells[incRowExcel, 1].Value = "         Sincerely Yours                             Acknowledged by                   Confirmed by                        Verified by";
                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        incRowExcel = incRowExcel + 7;
                                                        worksheet.Cells[incRowExcel, 1].Value = "      (                                 )                       (                                 )            (                                 )          (                                 )";
                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    

                                                    //worksheet.Cells[incRowExcel, 1].Value = "Sincerely Yours";
                                                    //worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    //worksheet.Cells[incRowExcel, 2].Value = "Acknowledged by";
                                                    //worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    //worksheet.Cells[incRowExcel, 4].Value = "Confirmed by";
                                                    //worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    //worksheet.Cells[incRowExcel, 6].Value = "Verified by";
                                                    //worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    //worksheet.Cells[incRowExcel, 2].Value = "(                          )";
                                                    //worksheet.Cells[incRowExcel, 4].Value = "(                          )";
                                                    //worksheet.Cells[incRowExcel, 6].Value = "(                          )";

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

                                                    worksheet.Cells["C" + _rowLineF + ":F" + _rowLineF].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["C" + _rowLineF + ":C" + _rowLineG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["F" + _rowLineF + ":F" + _rowLineG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["C" + _rowLineG + ":F" + _rowLineG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                    //worksheet.Cells["C" + _rowLineH + ":F" + _rowLineH].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    //worksheet.Cells["C" + _rowLineH + ":C" + _rowLineI].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    //worksheet.Cells["F" + _rowLineH + ":F" + _rowLineI].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    //worksheet.Cells["C" + _rowLineI + ":F" + _rowLineI].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                                    incRowExcel = incRowExcel + 14;
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
                                            worksheet.Column(5).Width = 35;
                                            worksheet.Column(6).Width = 35;
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
                                            if (_investmentRpt.DownloadMode == "PDF")
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


                else
                {
                    return false;
                }

            }
            #endregion
            else
            {
                return false;
            }
        }


        public string CloseNav_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, CloseNav _closeNAV)
        {
            try
            {
                string paramCloseNAVSelected = "";
                paramCloseNAVSelected = "CloseNAVPK in (" + _closeNAV.CloseNavSelected + ") ";


                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

                        IF EXISTS(
                        SELECT * FROM CloseNAV 
                        WHERE  " + paramCloseNAVSelected + @" and Approved1 = 1 and Approved2 = 1 and Date Between @DateFrom and @DateTo and status = 1
                        )
                        BEGIN
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'CloseNav',CloseNavPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @"
                       
                        update CloseNav set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and CloseNavPK in ( Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @") 
                        
                        Update CloseNav set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        where status = 4 and CloseNavPK in (Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 4 and " + paramCloseNAVSelected + @")   
                        select 'Approve All By Selected Success'  Result                      

                        END
                        ELSE
                        BEGIN
                            select 'Approve All By Selected Failed, Please Check Approval 1 or Approval 2 '  Result   
                        END
                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToString(dr["Result"]);

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