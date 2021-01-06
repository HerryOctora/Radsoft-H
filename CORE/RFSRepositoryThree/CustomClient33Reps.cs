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
    public class CustomClient33Reps
    {
        Host _host = new Host();

        public string LaporanKeuangan(string _userID, AccountingRpt _accountingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string FilePath = Tools.ReportsPath + "LaporanKeuangan" + "_" + _userID + ".xlsx";
                File.Copy(Tools.ReportsTemplatePath + "\\33\\" + "33_LaporanPosisiKeuangan.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {

                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets[1];
                    string monthFrom, monthTo;
                    int _monthFrom, _monthTo;
                    monthFrom = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("MM");
                    _monthFrom = Convert.ToInt32(monthFrom);

                    monthTo = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MM");
                    _monthTo = Convert.ToInt32(monthTo);

                    worksheet1.Cells[5, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet1.Column(2).Width = 0;
                    worksheet1.Column(3).Width = 0;
                    worksheet1.Column(4).Width = 0;
                    worksheet1.Column(5).Width = 0;
                    worksheet1.Column(6).Width = 0;
                    worksheet1.Column(7).Width = 0;
                    worksheet1.Column(8).Width = 0;
                    worksheet1.Column(9).Width = 0;
                    worksheet1.Column(10).Width = 0;
                    worksheet1.Column(11).Width = 0;
                    worksheet1.Column(12).Width = 0;
                    worksheet1.Column(13).Width = 0;

                    if (1 >= _monthFrom && 1 <= _monthTo)
                    {
                        string janFrom = "01/01/" + _accountingRpt.Period;
                        string janTo = "01/31/" + _accountingRpt.Period;
                        worksheet1.Column(2).Width = 20;

                        ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                        using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                        {


                            DbCon02.Open();
                            using (SqlCommand cmd02 = DbCon02.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd02.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + janTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + janFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     

								
                                Order BY C.ID";
                                cmd02.CommandTimeout = 0;
                                cmd02.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr02 = cmd02.ExecuteReader())
                                {
                                    if (dr02.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr02.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr02["ID"]);
                                            rSingle.Name = Convert.ToString(dr02["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr02["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr02["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr02["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr02["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         orderby r.ID ascending
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            worksheet1.Cells[1, 1].Value = _host.Get_CompanyName();

                                            worksheet1.Cells[3, 1].Value = "As Of : " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet1.Cells[1, 6].Value = "Date : " + Convert.ToDateTime(_datetimeNow).ToString("dd-MMM-yyyy");
                                            worksheet1.Cells[2, 6].Value = "Time : " + Convert.ToDateTime(_datetimeNow).ToString("hh:mm:ss");

                                            //worksheet1.Cells[6, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MMM");
                                            //worksheet1.Cells[6, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet2.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet2.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet2.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet2.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet2.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet2.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet2.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet2.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet2.PrinterSettings.FitToPage = true;
                                        worksheet2.PrinterSettings.FitToWidth = 1;
                                        worksheet2.PrinterSettings.FitToHeight = 0;
                                        worksheet2.PrinterSettings.PrintArea = worksheet2.Cells[1, 1, incRowExcel2, 6];
                                        worksheet2.Column(1).Width = 15;
                                        worksheet2.Column(2).Width = 15;
                                        worksheet2.Column(3).Width = 15;
                                        worksheet2.Column(4).Width = 15;
                                        worksheet2.Column(5).Width = 15;
                                        worksheet2.Column(6).Width = 15;

                                    }


                                }

                            }

                        }
                    }


                    if (2 >= _monthFrom && 2 <= _monthTo)
                    {
                        string febFrom = "02/01/" + _accountingRpt.Period;
                        string febTo = "02/28/" + _accountingRpt.Period;
                        worksheet1.Column(3).Width = 20;

                        ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                        using (SqlConnection DbCon03 = new SqlConnection(Tools.conString))
                        {
                            DbCon03.Open();
                            using (SqlCommand cmd03 = DbCon03.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd03.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + febTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + febFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)  
								
                                Order BY C.ID";
                                cmd03.CommandTimeout = 0;
                                cmd03.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd03.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd03.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr03 = cmd03.ExecuteReader())
                                {
                                    if (dr03.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr03.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr03["ID"]);
                                            rSingle.Name = Convert.ToString(dr03["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr03["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr03["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr03["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr03["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet3.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet3.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet3.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet3.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet3.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet3.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet3.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet3.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet3.PrinterSettings.FitToPage = true;
                                        worksheet3.PrinterSettings.FitToWidth = 1;
                                        worksheet3.PrinterSettings.FitToHeight = 0;
                                        worksheet3.PrinterSettings.PrintArea = worksheet3.Cells[1, 1, incRowExcel2, 6];
                                        worksheet3.Column(1).AutoFit();
                                        worksheet3.Column(2).AutoFit();
                                        worksheet3.Column(3).AutoFit();
                                        worksheet3.Column(4).AutoFit();
                                        worksheet3.Column(5).AutoFit();
                                        worksheet3.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (3 >= _monthFrom && 3 <= _monthTo)
                    {
                        string marFrom = "03/01/" + _accountingRpt.Period;
                        string marTo = "03/31/" + _accountingRpt.Period;
                        worksheet1.Column(4).Width = 20;

                        ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                        using (SqlConnection DbCon04 = new SqlConnection(Tools.conString))
                        {
                            DbCon04.Open();
                            using (SqlCommand cmd04 = DbCon04.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd04.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + marTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + marFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd04.CommandTimeout = 0;
                                cmd04.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd04.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd04.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr04 = cmd04.ExecuteReader())
                                {
                                    if (dr04.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr04.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr04["ID"]);
                                            rSingle.Name = Convert.ToString(dr04["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr04["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr04["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr04["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr04["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet4.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet4.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet4.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet4.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet4.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet4.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet4.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet4.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet4.PrinterSettings.FitToPage = true;
                                        worksheet4.PrinterSettings.FitToWidth = 1;
                                        worksheet4.PrinterSettings.FitToHeight = 0;
                                        worksheet4.PrinterSettings.PrintArea = worksheet4.Cells[1, 1, incRowExcel2, 6];
                                        worksheet4.Column(1).AutoFit();
                                        worksheet4.Column(2).AutoFit();
                                        worksheet4.Column(3).AutoFit();
                                        worksheet4.Column(4).AutoFit();
                                        worksheet4.Column(5).AutoFit();
                                        worksheet4.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }

                    if (4 >= _monthFrom && 4 <= _monthTo)
                    {
                        string aprFrom = "04/01/" + _accountingRpt.Period;
                        string aprTo = "04/30/" + _accountingRpt.Period;
                        worksheet1.Column(5).Width = 20;

                        ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                        using (SqlConnection DbCon05 = new SqlConnection(Tools.conString))
                        {
                            DbCon05.Open();
                            using (SqlCommand cmd05 = DbCon05.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd05.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + aprTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + aprFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)  
								
                                Order BY C.ID";
                                cmd05.CommandTimeout = 0;
                                cmd05.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd05.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd05.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr04 = cmd05.ExecuteReader())
                                {
                                    if (dr04.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr04.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr04["ID"]);
                                            rSingle.Name = Convert.ToString(dr04["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr04["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr04["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr04["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr04["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet5.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet5.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet5.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet5.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet5.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet5.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet5.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet5.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet5.PrinterSettings.FitToPage = true;
                                        worksheet5.PrinterSettings.FitToWidth = 1;
                                        worksheet5.PrinterSettings.FitToHeight = 0;
                                        worksheet5.PrinterSettings.PrintArea = worksheet5.Cells[1, 1, incRowExcel2, 6];
                                        worksheet5.Column(1).AutoFit();
                                        worksheet5.Column(2).AutoFit();
                                        worksheet5.Column(3).AutoFit();
                                        worksheet5.Column(4).AutoFit();
                                        worksheet5.Column(5).AutoFit();
                                        worksheet5.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (5 >= _monthFrom && 5 <= _monthTo)
                    {
                        string mayFrom = "05/01/" + _accountingRpt.Period;
                        string mayTo = "05/31/" + _accountingRpt.Period;
                        worksheet1.Column(6).Width = 20;

                        ExcelWorksheet worksheet6 = package.Workbook.Worksheets[6];
                        using (SqlConnection DbCon06 = new SqlConnection(Tools.conString))
                        {
                            DbCon06.Open();
                            using (SqlCommand cmd06 = DbCon06.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd06.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + mayTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + mayFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd06.CommandTimeout = 0;
                                cmd06.CommandTimeout = 0;
                                cmd06.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd06.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd06.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr06 = cmd06.ExecuteReader())
                                {
                                    if (dr06.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr06.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr06["ID"]);
                                            rSingle.Name = Convert.ToString(dr06["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr06["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr06["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr06["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr06["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet6.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet6.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet6.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet6.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet6.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet6.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet6.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet6.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet6.PrinterSettings.FitToPage = true;
                                        worksheet6.PrinterSettings.FitToWidth = 1;
                                        worksheet6.PrinterSettings.FitToHeight = 0;
                                        worksheet6.PrinterSettings.PrintArea = worksheet6.Cells[1, 1, incRowExcel2, 6];
                                        worksheet6.Column(1).AutoFit();
                                        worksheet6.Column(2).AutoFit();
                                        worksheet6.Column(3).AutoFit();
                                        worksheet6.Column(4).AutoFit();
                                        worksheet6.Column(5).AutoFit();
                                        worksheet6.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }




                    if (6 >= _monthFrom && 6 <= _monthTo)
                    {
                        string junFrom = "06/01/" + _accountingRpt.Period;
                        string junTo = "06/30/" + _accountingRpt.Period;
                        worksheet1.Column(7).Width = 20;

                        ExcelWorksheet worksheet7 = package.Workbook.Worksheets[7];
                        using (SqlConnection DbCon07 = new SqlConnection(Tools.conString))
                        {
                            DbCon07.Open();
                            using (SqlCommand cmd07 = DbCon07.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd07.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + junTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + junFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd07.CommandTimeout = 0;
                                cmd07.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd07.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd07.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr07 = cmd07.ExecuteReader())
                                {
                                    if (dr07.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr07.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr07["ID"]);
                                            rSingle.Name = Convert.ToString(dr07["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr07["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr07["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr07["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr07["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet7.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet7.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet7.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet7.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet7.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet7.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet7.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet7.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet7.PrinterSettings.FitToPage = true;
                                        worksheet7.PrinterSettings.FitToWidth = 1;
                                        worksheet7.PrinterSettings.FitToHeight = 0;
                                        worksheet7.PrinterSettings.PrintArea = worksheet7.Cells[1, 1, incRowExcel2, 6];
                                        worksheet7.Column(1).AutoFit();
                                        worksheet7.Column(2).AutoFit();
                                        worksheet7.Column(3).AutoFit();
                                        worksheet7.Column(4).AutoFit();
                                        worksheet7.Column(5).AutoFit();
                                        worksheet7.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }

                    if (7 >= _monthFrom && 7 <= _monthTo)
                    {
                        string julFrom = "07/01/" + _accountingRpt.Period;
                        string julTo = "07/31/" + _accountingRpt.Period;
                        worksheet1.Column(8).Width = 20;

                        ExcelWorksheet worksheet8 = package.Workbook.Worksheets[8];
                        using (SqlConnection DbCon08 = new SqlConnection(Tools.conString))
                        {
                            DbCon08.Open();
                            using (SqlCommand cmd08 = DbCon08.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd08.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + julTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + julFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd08.CommandTimeout = 0;
                                cmd08.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd08.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd08.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr08 = cmd08.ExecuteReader())
                                {
                                    if (dr08.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr08.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr08["ID"]);
                                            rSingle.Name = Convert.ToString(dr08["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr08["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr08["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr08["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr08["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet8.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet8.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet8.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet8.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet8.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet8.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet8.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet8.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet8.PrinterSettings.FitToPage = true;
                                        worksheet8.PrinterSettings.FitToWidth = 1;
                                        worksheet8.PrinterSettings.FitToHeight = 0;
                                        worksheet8.PrinterSettings.PrintArea = worksheet8.Cells[1, 1, incRowExcel2, 6];
                                        worksheet8.Column(1).AutoFit();
                                        worksheet8.Column(2).AutoFit();
                                        worksheet8.Column(3).AutoFit();
                                        worksheet8.Column(4).AutoFit();
                                        worksheet8.Column(5).AutoFit();
                                        worksheet8.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (8 >= _monthFrom && 8 <= _monthTo)
                    {
                        string augFrom = "08/01/" + _accountingRpt.Period;
                        string augTo = "08/31/" + _accountingRpt.Period;
                        worksheet1.Column(9).Width = 20;

                        ExcelWorksheet worksheet9 = package.Workbook.Worksheets[9];
                        using (SqlConnection DbCon09 = new SqlConnection(Tools.conString))
                        {
                            DbCon09.Open();
                            using (SqlCommand cmd09 = DbCon09.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd09.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + augTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + augFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd09.CommandTimeout = 0;
                                cmd09.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd09.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd09.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr09 = cmd09.ExecuteReader())
                                {
                                    if (dr09.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr09.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr09["ID"]);
                                            rSingle.Name = Convert.ToString(dr09["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr09["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr09["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr09["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr09["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet9.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet9.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet9.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet9.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet9.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet9.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet9.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet9.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet9.PrinterSettings.FitToPage = true;
                                        worksheet9.PrinterSettings.FitToWidth = 1;
                                        worksheet9.PrinterSettings.FitToHeight = 0;
                                        worksheet9.PrinterSettings.PrintArea = worksheet9.Cells[1, 1, incRowExcel2, 6];
                                        worksheet9.Column(1).AutoFit();
                                        worksheet9.Column(2).AutoFit();
                                        worksheet9.Column(3).AutoFit();
                                        worksheet9.Column(4).AutoFit();
                                        worksheet9.Column(5).AutoFit();
                                        worksheet9.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (9 >= _monthFrom && 9 <= _monthTo)
                    {
                        string sepFrom = "09/01/" + _accountingRpt.Period;
                        string sepTo = "09/30/" + _accountingRpt.Period;
                        worksheet1.Column(10).Width = 20;

                        ExcelWorksheet worksheet10 = package.Workbook.Worksheets[10];
                        using (SqlConnection DbCon10 = new SqlConnection(Tools.conString))
                        {
                            DbCon10.Open();
                            using (SqlCommand cmd10 = DbCon10.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd10.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + sepTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK) and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + sepFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd10.CommandTimeout = 0;
                                cmd10.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd10.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd10.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr10 = cmd10.ExecuteReader())
                                {
                                    if (dr10.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr10.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr10["ID"]);
                                            rSingle.Name = Convert.ToString(dr10["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr10["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr10["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr10["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr10["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet10.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet10.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet10.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet10.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet10.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet10.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet10.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet10.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet10.PrinterSettings.FitToPage = true;
                                        worksheet10.PrinterSettings.FitToWidth = 1;
                                        worksheet10.PrinterSettings.FitToHeight = 0;
                                        worksheet10.PrinterSettings.PrintArea = worksheet10.Cells[1, 1, incRowExcel2, 6];
                                        worksheet10.Column(1).AutoFit();
                                        worksheet10.Column(2).AutoFit();
                                        worksheet10.Column(3).AutoFit();
                                        worksheet10.Column(4).AutoFit();
                                        worksheet10.Column(5).AutoFit();
                                        worksheet10.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (10 >= _monthFrom && 10 <= _monthTo)
                    {
                        string octFrom = "10/01/" + _accountingRpt.Period;
                        string octTo = "10/31/" + _accountingRpt.Period;
                        worksheet1.Column(11).Width = 20;

                        ExcelWorksheet worksheet11 = package.Workbook.Worksheets[11];
                        using (SqlConnection DbCon11 = new SqlConnection(Tools.conString))
                        {
                            DbCon11.Open();
                            using (SqlCommand cmd11 = DbCon11.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd11.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + octTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + octFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd11.CommandTimeout = 0;
                                cmd11.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd11.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd11.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr11 = cmd11.ExecuteReader())
                                {
                                    if (dr11.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr11.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr11["ID"]);
                                            rSingle.Name = Convert.ToString(dr11["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr11["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr11["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr11["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr11["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet11.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet11.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet11.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet11.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet11.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet11.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet11.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet11.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet11.PrinterSettings.FitToPage = true;
                                        worksheet11.PrinterSettings.FitToWidth = 1;
                                        worksheet11.PrinterSettings.FitToHeight = 0;
                                        worksheet11.PrinterSettings.PrintArea = worksheet11.Cells[1, 1, incRowExcel2, 6];
                                        worksheet11.Column(1).AutoFit();
                                        worksheet11.Column(2).AutoFit();
                                        worksheet11.Column(3).AutoFit();
                                        worksheet11.Column(4).AutoFit();
                                        worksheet11.Column(5).AutoFit();
                                        worksheet11.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (11 >= _monthFrom && 11 <= _monthTo)
                    {
                        string novFrom = "11/01/" + _accountingRpt.Period;
                        string novTo = "11/30/" + _accountingRpt.Period;
                        worksheet1.Column(12).Width = 20;

                        ExcelWorksheet worksheet12 = package.Workbook.Worksheets[12];
                        using (SqlConnection DbCon12 = new SqlConnection(Tools.conString))
                        {
                            DbCon12.Open();
                            using (SqlCommand cmd12 = DbCon12.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd12.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + novTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + novFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd12.CommandTimeout = 0;
                                cmd12.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd12.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd12.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr12 = cmd12.ExecuteReader())
                                {
                                    if (dr12.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr12.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr12["ID"]);
                                            rSingle.Name = Convert.ToString(dr12["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr12["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr12["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr12["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr12["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet12.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet12.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet12.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet12.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet12.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet12.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet12.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet12.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet12.PrinterSettings.FitToPage = true;
                                        worksheet12.PrinterSettings.FitToWidth = 1;
                                        worksheet12.PrinterSettings.FitToHeight = 0;
                                        worksheet12.PrinterSettings.PrintArea = worksheet12.Cells[1, 1, incRowExcel2, 6];
                                        worksheet12.Column(1).AutoFit();
                                        worksheet12.Column(2).AutoFit();
                                        worksheet12.Column(3).AutoFit();
                                        worksheet12.Column(4).AutoFit();
                                        worksheet12.Column(5).AutoFit();
                                        worksheet12.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (12 >= _monthFrom && 12 <= _monthTo)
                    {
                        string decFrom = "12/01/" + _accountingRpt.Period;
                        string decTo = "12/31/" + _accountingRpt.Period;
                        worksheet1.Column(13).Width = 20;

                        ExcelWorksheet worksheet13 = package.Workbook.Worksheets[13];
                        using (SqlConnection DbCon13 = new SqlConnection(Tools.conString))
                        {
                            DbCon13.Open();
                            using (SqlCommand cmd13 = DbCon13.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd13.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + decTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + decFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd13.CommandTimeout = 0;
                                cmd13.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd13.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd13.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr13 = cmd13.ExecuteReader())
                                {
                                    if (dr13.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr13.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr13["ID"]);
                                            rSingle.Name = Convert.ToString(dr13["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr13["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr13["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr13["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr13["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet13.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet13.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet13.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet13.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet13.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet13.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet13.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet13.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet13.PrinterSettings.FitToPage = true;
                                        worksheet13.PrinterSettings.FitToWidth = 1;
                                        worksheet13.PrinterSettings.FitToHeight = 0;
                                        worksheet13.PrinterSettings.PrintArea = worksheet13.Cells[1, 1, incRowExcel2, 6];
                                        worksheet13.Column(1).AutoFit();
                                        worksheet13.Column(2).AutoFit();
                                        worksheet13.Column(3).AutoFit();
                                        worksheet13.Column(4).AutoFit();
                                        worksheet13.Column(5).AutoFit();
                                        worksheet13.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    package.Save();
                    return FilePath;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string LaporanLabaRugi(string _userID, AccountingRpt _accountingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string FilePath = Tools.ReportsPath + "LaporanLabaRugi" + "_" + _userID + ".xlsx";
                File.Copy(Tools.ReportsTemplatePath + "\\33\\" + "33_LaporanLabaRugi.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {

                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets[1];
                    string monthFrom, monthTo;
                    int _monthFrom, _monthTo;
                    monthFrom = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("MM");
                    _monthFrom = Convert.ToInt32(monthFrom);

                    monthTo = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MM");
                    _monthTo = Convert.ToInt32(monthTo);

                    worksheet1.Cells[5, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet1.Column(2).Width = 0;
                    worksheet1.Column(3).Width = 0;
                    worksheet1.Column(4).Width = 0;
                    worksheet1.Column(5).Width = 0;
                    worksheet1.Column(6).Width = 0;
                    worksheet1.Column(7).Width = 0;
                    worksheet1.Column(8).Width = 0;
                    worksheet1.Column(9).Width = 0;
                    worksheet1.Column(10).Width = 0;
                    worksheet1.Column(11).Width = 0;
                    worksheet1.Column(12).Width = 0;
                    worksheet1.Column(13).Width = 0;

                    if (1 >= _monthFrom && 1 <= _monthTo)
                    {
                        string janFrom = "01/01/" + _accountingRpt.Period;
                        string janTo = "01/31/" + _accountingRpt.Period;
                        worksheet1.Column(2).Width = 20;

                        ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                        using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                        {


                            DbCon02.Open();
                            using (SqlCommand cmd02 = DbCon02.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd02.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + janTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + janFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     

								
                                Order BY C.ID";
                                cmd02.CommandTimeout = 0;
                                cmd02.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr02 = cmd02.ExecuteReader())
                                {
                                    if (dr02.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr02.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr02["ID"]);
                                            rSingle.Name = Convert.ToString(dr02["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr02["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr02["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr02["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr02["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         orderby r.ID ascending
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            worksheet1.Cells[1, 1].Value = _host.Get_CompanyName();

                                            worksheet1.Cells[3, 1].Value = "As Of : " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet1.Cells[1, 6].Value = "Date : " + Convert.ToDateTime(_datetimeNow).ToString("dd-MMM-yyyy");
                                            worksheet1.Cells[2, 6].Value = "Time : " + Convert.ToDateTime(_datetimeNow).ToString("hh:mm:ss");

                                            //worksheet1.Cells[6, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MMM");
                                            //worksheet1.Cells[6, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet2.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet2.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet2.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet2.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet2.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet2.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet2.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet2.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet2.PrinterSettings.FitToPage = true;
                                        worksheet2.PrinterSettings.FitToWidth = 1;
                                        worksheet2.PrinterSettings.FitToHeight = 0;
                                        worksheet2.PrinterSettings.PrintArea = worksheet2.Cells[1, 1, incRowExcel2, 6];
                                        worksheet2.Column(1).Width = 15;
                                        worksheet2.Column(2).Width = 15;
                                        worksheet2.Column(3).Width = 15;
                                        worksheet2.Column(4).Width = 15;
                                        worksheet2.Column(5).Width = 15;
                                        worksheet2.Column(6).Width = 15;

                                    }


                                }

                            }

                        }
                    }


                    if (2 >= _monthFrom && 2 <= _monthTo)
                    {
                        string febFrom = "02/01/" + _accountingRpt.Period;
                        string febTo = "02/28/" + _accountingRpt.Period;
                        worksheet1.Column(3).Width = 20;

                        ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                        using (SqlConnection DbCon03 = new SqlConnection(Tools.conString))
                        {
                            DbCon03.Open();
                            using (SqlCommand cmd03 = DbCon03.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd03.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + febTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + febFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)  
								
                                Order BY C.ID";
                                cmd03.CommandTimeout = 0;
                                cmd03.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd03.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd03.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr03 = cmd03.ExecuteReader())
                                {
                                    if (dr03.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr03.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr03["ID"]);
                                            rSingle.Name = Convert.ToString(dr03["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr03["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr03["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr03["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr03["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet3.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet3.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet3.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet3.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet3.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet3.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet3.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet3.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet3.PrinterSettings.FitToPage = true;
                                        worksheet3.PrinterSettings.FitToWidth = 1;
                                        worksheet3.PrinterSettings.FitToHeight = 0;
                                        worksheet3.PrinterSettings.PrintArea = worksheet3.Cells[1, 1, incRowExcel2, 6];
                                        worksheet3.Column(1).AutoFit();
                                        worksheet3.Column(2).AutoFit();
                                        worksheet3.Column(3).AutoFit();
                                        worksheet3.Column(4).AutoFit();
                                        worksheet3.Column(5).AutoFit();
                                        worksheet3.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (3 >= _monthFrom && 3 <= _monthTo)
                    {
                        string marFrom = "03/01/" + _accountingRpt.Period;
                        string marTo = "03/31/" + _accountingRpt.Period;
                        worksheet1.Column(4).Width = 20;

                        ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                        using (SqlConnection DbCon04 = new SqlConnection(Tools.conString))
                        {
                            DbCon04.Open();
                            using (SqlCommand cmd04 = DbCon04.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd04.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + marTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + marFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd04.CommandTimeout = 0;
                                cmd04.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd04.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd04.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr04 = cmd04.ExecuteReader())
                                {
                                    if (dr04.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr04.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr04["ID"]);
                                            rSingle.Name = Convert.ToString(dr04["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr04["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr04["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr04["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr04["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet4.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet4.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet4.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet4.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet4.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet4.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet4.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet4.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet4.PrinterSettings.FitToPage = true;
                                        worksheet4.PrinterSettings.FitToWidth = 1;
                                        worksheet4.PrinterSettings.FitToHeight = 0;
                                        worksheet4.PrinterSettings.PrintArea = worksheet4.Cells[1, 1, incRowExcel2, 6];
                                        worksheet4.Column(1).AutoFit();
                                        worksheet4.Column(2).AutoFit();
                                        worksheet4.Column(3).AutoFit();
                                        worksheet4.Column(4).AutoFit();
                                        worksheet4.Column(5).AutoFit();
                                        worksheet4.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }

                    if (4 >= _monthFrom && 4 <= _monthTo)
                    {
                        string aprFrom = "04/01/" + _accountingRpt.Period;
                        string aprTo = "04/30/" + _accountingRpt.Period;
                        worksheet1.Column(5).Width = 20;

                        ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                        using (SqlConnection DbCon05 = new SqlConnection(Tools.conString))
                        {
                            DbCon05.Open();
                            using (SqlCommand cmd05 = DbCon05.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd05.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + aprTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + aprFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)  
								
                                Order BY C.ID";
                                cmd05.CommandTimeout = 0;
                                cmd05.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd05.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd05.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr04 = cmd05.ExecuteReader())
                                {
                                    if (dr04.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr04.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr04["ID"]);
                                            rSingle.Name = Convert.ToString(dr04["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr04["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr04["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr04["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr04["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet5.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet5.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet5.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet5.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet5.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet5.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet5.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet5.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet5.PrinterSettings.FitToPage = true;
                                        worksheet5.PrinterSettings.FitToWidth = 1;
                                        worksheet5.PrinterSettings.FitToHeight = 0;
                                        worksheet5.PrinterSettings.PrintArea = worksheet5.Cells[1, 1, incRowExcel2, 6];
                                        worksheet5.Column(1).AutoFit();
                                        worksheet5.Column(2).AutoFit();
                                        worksheet5.Column(3).AutoFit();
                                        worksheet5.Column(4).AutoFit();
                                        worksheet5.Column(5).AutoFit();
                                        worksheet5.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (5 >= _monthFrom && 5 <= _monthTo)
                    {
                        string mayFrom = "05/01/" + _accountingRpt.Period;
                        string mayTo = "05/31/" + _accountingRpt.Period;
                        worksheet1.Column(6).Width = 20;

                        ExcelWorksheet worksheet6 = package.Workbook.Worksheets[6];
                        using (SqlConnection DbCon06 = new SqlConnection(Tools.conString))
                        {
                            DbCon06.Open();
                            using (SqlCommand cmd06 = DbCon06.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd06.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + mayTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + mayFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd06.CommandTimeout = 0;
                                cmd06.CommandTimeout = 0;
                                cmd06.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd06.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd06.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr06 = cmd06.ExecuteReader())
                                {
                                    if (dr06.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr06.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr06["ID"]);
                                            rSingle.Name = Convert.ToString(dr06["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr06["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr06["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr06["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr06["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet6.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet6.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet6.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet6.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet6.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet6.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet6.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet6.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet6.PrinterSettings.FitToPage = true;
                                        worksheet6.PrinterSettings.FitToWidth = 1;
                                        worksheet6.PrinterSettings.FitToHeight = 0;
                                        worksheet6.PrinterSettings.PrintArea = worksheet6.Cells[1, 1, incRowExcel2, 6];
                                        worksheet6.Column(1).AutoFit();
                                        worksheet6.Column(2).AutoFit();
                                        worksheet6.Column(3).AutoFit();
                                        worksheet6.Column(4).AutoFit();
                                        worksheet6.Column(5).AutoFit();
                                        worksheet6.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }




                    if (6 >= _monthFrom && 6 <= _monthTo)
                    {
                        string junFrom = "06/01/" + _accountingRpt.Period;
                        string junTo = "06/30/" + _accountingRpt.Period;
                        worksheet1.Column(7).Width = 20;

                        ExcelWorksheet worksheet7 = package.Workbook.Worksheets[7];
                        using (SqlConnection DbCon07 = new SqlConnection(Tools.conString))
                        {
                            DbCon07.Open();
                            using (SqlCommand cmd07 = DbCon07.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd07.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + junTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + junFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd07.CommandTimeout = 0;
                                cmd07.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd07.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd07.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr07 = cmd07.ExecuteReader())
                                {
                                    if (dr07.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr07.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr07["ID"]);
                                            rSingle.Name = Convert.ToString(dr07["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr07["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr07["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr07["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr07["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet7.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet7.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet7.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet7.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet7.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet7.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet7.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet7.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet7.PrinterSettings.FitToPage = true;
                                        worksheet7.PrinterSettings.FitToWidth = 1;
                                        worksheet7.PrinterSettings.FitToHeight = 0;
                                        worksheet7.PrinterSettings.PrintArea = worksheet7.Cells[1, 1, incRowExcel2, 6];
                                        worksheet7.Column(1).AutoFit();
                                        worksheet7.Column(2).AutoFit();
                                        worksheet7.Column(3).AutoFit();
                                        worksheet7.Column(4).AutoFit();
                                        worksheet7.Column(5).AutoFit();
                                        worksheet7.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }

                    if (7 >= _monthFrom && 7 <= _monthTo)
                    {
                        string julFrom = "07/01/" + _accountingRpt.Period;
                        string julTo = "07/31/" + _accountingRpt.Period;
                        worksheet1.Column(8).Width = 20;

                        ExcelWorksheet worksheet8 = package.Workbook.Worksheets[8];
                        using (SqlConnection DbCon08 = new SqlConnection(Tools.conString))
                        {
                            DbCon08.Open();
                            using (SqlCommand cmd08 = DbCon08.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd08.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + julTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + julFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd08.CommandTimeout = 0;
                                cmd08.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd08.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd08.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr08 = cmd08.ExecuteReader())
                                {
                                    if (dr08.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr08.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr08["ID"]);
                                            rSingle.Name = Convert.ToString(dr08["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr08["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr08["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr08["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr08["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet8.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet8.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet8.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet8.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet8.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet8.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet8.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet8.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet8.PrinterSettings.FitToPage = true;
                                        worksheet8.PrinterSettings.FitToWidth = 1;
                                        worksheet8.PrinterSettings.FitToHeight = 0;
                                        worksheet8.PrinterSettings.PrintArea = worksheet8.Cells[1, 1, incRowExcel2, 6];
                                        worksheet8.Column(1).AutoFit();
                                        worksheet8.Column(2).AutoFit();
                                        worksheet8.Column(3).AutoFit();
                                        worksheet8.Column(4).AutoFit();
                                        worksheet8.Column(5).AutoFit();
                                        worksheet8.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (8 >= _monthFrom && 8 <= _monthTo)
                    {
                        string augFrom = "08/01/" + _accountingRpt.Period;
                        string augTo = "08/31/" + _accountingRpt.Period;
                        worksheet1.Column(9).Width = 20;

                        ExcelWorksheet worksheet9 = package.Workbook.Worksheets[9];
                        using (SqlConnection DbCon09 = new SqlConnection(Tools.conString))
                        {
                            DbCon09.Open();
                            using (SqlCommand cmd09 = DbCon09.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd09.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + augTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + augFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd09.CommandTimeout = 0;
                                cmd09.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd09.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd09.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr09 = cmd09.ExecuteReader())
                                {
                                    if (dr09.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr09.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr09["ID"]);
                                            rSingle.Name = Convert.ToString(dr09["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr09["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr09["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr09["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr09["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet9.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet9.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet9.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet9.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet9.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet9.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet9.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet9.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet9.PrinterSettings.FitToPage = true;
                                        worksheet9.PrinterSettings.FitToWidth = 1;
                                        worksheet9.PrinterSettings.FitToHeight = 0;
                                        worksheet9.PrinterSettings.PrintArea = worksheet9.Cells[1, 1, incRowExcel2, 6];
                                        worksheet9.Column(1).AutoFit();
                                        worksheet9.Column(2).AutoFit();
                                        worksheet9.Column(3).AutoFit();
                                        worksheet9.Column(4).AutoFit();
                                        worksheet9.Column(5).AutoFit();
                                        worksheet9.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (9 >= _monthFrom && 9 <= _monthTo)
                    {
                        string sepFrom = "09/01/" + _accountingRpt.Period;
                        string sepTo = "09/30/" + _accountingRpt.Period;
                        worksheet1.Column(10).Width = 20;

                        ExcelWorksheet worksheet10 = package.Workbook.Worksheets[10];
                        using (SqlConnection DbCon10 = new SqlConnection(Tools.conString))
                        {
                            DbCon10.Open();
                            using (SqlCommand cmd10 = DbCon10.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd10.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + sepTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK) and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + sepFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd10.CommandTimeout = 0;
                                cmd10.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd10.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd10.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr10 = cmd10.ExecuteReader())
                                {
                                    if (dr10.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr10.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr10["ID"]);
                                            rSingle.Name = Convert.ToString(dr10["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr10["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr10["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr10["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr10["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet10.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet10.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet10.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet10.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet10.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet10.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet10.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet10.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet10.PrinterSettings.FitToPage = true;
                                        worksheet10.PrinterSettings.FitToWidth = 1;
                                        worksheet10.PrinterSettings.FitToHeight = 0;
                                        worksheet10.PrinterSettings.PrintArea = worksheet10.Cells[1, 1, incRowExcel2, 6];
                                        worksheet10.Column(1).AutoFit();
                                        worksheet10.Column(2).AutoFit();
                                        worksheet10.Column(3).AutoFit();
                                        worksheet10.Column(4).AutoFit();
                                        worksheet10.Column(5).AutoFit();
                                        worksheet10.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (10 >= _monthFrom && 10 <= _monthTo)
                    {
                        string octFrom = "10/01/" + _accountingRpt.Period;
                        string octTo = "10/31/" + _accountingRpt.Period;
                        worksheet1.Column(11).Width = 20;

                        ExcelWorksheet worksheet11 = package.Workbook.Worksheets[11];
                        using (SqlConnection DbCon11 = new SqlConnection(Tools.conString))
                        {
                            DbCon11.Open();
                            using (SqlCommand cmd11 = DbCon11.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd11.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + octTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + octFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd11.CommandTimeout = 0;
                                cmd11.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd11.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd11.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr11 = cmd11.ExecuteReader())
                                {
                                    if (dr11.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr11.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr11["ID"]);
                                            rSingle.Name = Convert.ToString(dr11["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr11["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr11["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr11["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr11["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet11.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet11.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet11.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet11.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet11.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet11.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet11.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet11.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet11.PrinterSettings.FitToPage = true;
                                        worksheet11.PrinterSettings.FitToWidth = 1;
                                        worksheet11.PrinterSettings.FitToHeight = 0;
                                        worksheet11.PrinterSettings.PrintArea = worksheet11.Cells[1, 1, incRowExcel2, 6];
                                        worksheet11.Column(1).AutoFit();
                                        worksheet11.Column(2).AutoFit();
                                        worksheet11.Column(3).AutoFit();
                                        worksheet11.Column(4).AutoFit();
                                        worksheet11.Column(5).AutoFit();
                                        worksheet11.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (11 >= _monthFrom && 11 <= _monthTo)
                    {
                        string novFrom = "11/01/" + _accountingRpt.Period;
                        string novTo = "11/30/" + _accountingRpt.Period;
                        worksheet1.Column(12).Width = 20;

                        ExcelWorksheet worksheet12 = package.Workbook.Worksheets[12];
                        using (SqlConnection DbCon12 = new SqlConnection(Tools.conString))
                        {
                            DbCon12.Open();
                            using (SqlCommand cmd12 = DbCon12.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd12.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + novTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + novFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd12.CommandTimeout = 0;
                                cmd12.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd12.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd12.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr12 = cmd12.ExecuteReader())
                                {
                                    if (dr12.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr12.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr12["ID"]);
                                            rSingle.Name = Convert.ToString(dr12["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr12["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr12["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr12["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr12["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet12.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet12.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet12.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet12.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet12.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet12.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet12.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet12.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet12.PrinterSettings.FitToPage = true;
                                        worksheet12.PrinterSettings.FitToWidth = 1;
                                        worksheet12.PrinterSettings.FitToHeight = 0;
                                        worksheet12.PrinterSettings.PrintArea = worksheet12.Cells[1, 1, incRowExcel2, 6];
                                        worksheet12.Column(1).AutoFit();
                                        worksheet12.Column(2).AutoFit();
                                        worksheet12.Column(3).AutoFit();
                                        worksheet12.Column(4).AutoFit();
                                        worksheet12.Column(5).AutoFit();
                                        worksheet12.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (12 >= _monthFrom && 12 <= _monthTo)
                    {
                        string decFrom = "12/01/" + _accountingRpt.Period;
                        string decTo = "12/31/" + _accountingRpt.Period;
                        worksheet1.Column(13).Width = 20;

                        ExcelWorksheet worksheet13 = package.Workbook.Worksheets[13];
                        using (SqlConnection DbCon13 = new SqlConnection(Tools.conString))
                        {
                            DbCon13.Open();
                            using (SqlCommand cmd13 = DbCon13.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd13.CommandText = @"
                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + decTo + @"' and  B.PeriodPK = @PeriodPK 
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + decFrom + @"' and  B.PeriodPK = @PeriodPK  
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd13.CommandTimeout = 0;
                                cmd13.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd13.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd13.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr13 = cmd13.ExecuteReader())
                                {
                                    if (dr13.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr13.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr13["ID"]);
                                            rSingle.Name = Convert.ToString(dr13["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr13["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr13["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr13["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr13["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet13.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet13.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                worksheet13.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet13.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet13.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet13.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet13.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet13.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet13.PrinterSettings.FitToPage = true;
                                        worksheet13.PrinterSettings.FitToWidth = 1;
                                        worksheet13.PrinterSettings.FitToHeight = 0;
                                        worksheet13.PrinterSettings.PrintArea = worksheet13.Cells[1, 1, incRowExcel2, 6];
                                        worksheet13.Column(1).AutoFit();
                                        worksheet13.Column(2).AutoFit();
                                        worksheet13.Column(3).AutoFit();
                                        worksheet13.Column(4).AutoFit();
                                        worksheet13.Column(5).AutoFit();
                                        worksheet13.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    package.Save();
                    return FilePath;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string LaporanArusKas(string _userID, AccountingRpt _accountingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string FilePath = Tools.ReportsPath + "LaporanArusKas" + "_" + _userID + ".xlsx";
                File.Copy(Tools.ReportsTemplatePath + "\\33\\" + "33_LaporanArusKas.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {

                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets[1];
                    string monthFrom, monthTo;
                    int _monthFrom, _monthTo;
                    monthFrom = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("MM");
                    _monthFrom = Convert.ToInt32(monthFrom);

                    monthTo = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MM");
                    _monthTo = Convert.ToInt32(monthTo);

                    worksheet1.Cells[5, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet1.Column(2).Width = 0;
                    worksheet1.Column(3).Width = 0;
                    worksheet1.Column(4).Width = 0;
                    worksheet1.Column(5).Width = 0;
                    worksheet1.Column(6).Width = 0;
                    worksheet1.Column(7).Width = 0;
                    worksheet1.Column(8).Width = 0;
                    worksheet1.Column(9).Width = 0;
                    worksheet1.Column(10).Width = 0;
                    worksheet1.Column(11).Width = 0;
                    worksheet1.Column(12).Width = 0;
                    worksheet1.Column(13).Width = 0;

                    if (1 >= _monthFrom && 1 <= _monthTo)
                    {
                        string janFrom = "01/01/" + _accountingRpt.Period;
                        string janTo = "01/31/" + _accountingRpt.Period;
                        worksheet1.Column(2).Width = 20;

                        ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                        using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                        {


                            DbCon02.Open();
                            using (SqlCommand cmd02 = DbCon02.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd02.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @" and B.PeriodPK = @PeriodPK 


                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + janTo + @"' and  B.PeriodPK = @PeriodPK and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + janFrom + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     

								
                                Order BY C.ID";
                                cmd02.CommandTimeout = 0;
                                cmd02.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr02 = cmd02.ExecuteReader())
                                {
                                    if (dr02.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr02.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr02["ID"]);
                                            rSingle.Name = Convert.ToString(dr02["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr02["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr02["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr02["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr02["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         orderby r.ID ascending
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            worksheet1.Cells[1, 1].Value = _host.Get_CompanyName();

                                            worksheet1.Cells[3, 1].Value = "As Of : " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet1.Cells[1, 6].Value = "Date : " + Convert.ToDateTime(_datetimeNow).ToString("dd-MMM-yyyy");
                                            worksheet1.Cells[2, 6].Value = "Time : " + Convert.ToDateTime(_datetimeNow).ToString("hh:mm:ss");

                                            //worksheet1.Cells[6, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("MMM");
                                            //worksheet1.Cells[6, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet2.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet2.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet2.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet2.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet2.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet2.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet2.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet2.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet2.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet2.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet2.PrinterSettings.FitToPage = true;
                                        worksheet2.PrinterSettings.FitToWidth = 1;
                                        worksheet2.PrinterSettings.FitToHeight = 0;
                                        worksheet2.PrinterSettings.PrintArea = worksheet2.Cells[1, 1, incRowExcel2, 6];
                                        worksheet2.Column(1).Width = 15;
                                        worksheet2.Column(2).Width = 15;
                                        worksheet2.Column(3).Width = 15;
                                        worksheet2.Column(4).Width = 15;
                                        worksheet2.Column(5).Width = 15;
                                        worksheet2.Column(6).Width = 15;

                                    }


                                }

                            }

                        }
                    }


                    if (2 >= _monthFrom && 2 <= _monthTo)
                    {
                        string febFrom = "02/01/" + _accountingRpt.Period;
                        string febTo = "02/28/" + _accountingRpt.Period;
                        worksheet1.Column(3).Width = 20;

                        ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                        using (SqlConnection DbCon03 = new SqlConnection(Tools.conString))
                        {
                            DbCon03.Open();
                            using (SqlCommand cmd03 = DbCon03.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd03.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @" and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + febTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + febFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)  
								
                                Order BY C.ID";
                                cmd03.CommandTimeout = 0;
                                cmd03.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd03.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd03.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr03 = cmd03.ExecuteReader())
                                {
                                    if (dr03.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr03.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr03["ID"]);
                                            rSingle.Name = Convert.ToString(dr03["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr03["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr03["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr03["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr03["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet3.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet3.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet3.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet3.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet3.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet3.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet3.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet3.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet3.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet3.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet3.PrinterSettings.FitToPage = true;
                                        worksheet3.PrinterSettings.FitToWidth = 1;
                                        worksheet3.PrinterSettings.FitToHeight = 0;
                                        worksheet3.PrinterSettings.PrintArea = worksheet3.Cells[1, 1, incRowExcel2, 6];
                                        worksheet3.Column(1).AutoFit();
                                        worksheet3.Column(2).AutoFit();
                                        worksheet3.Column(3).AutoFit();
                                        worksheet3.Column(4).AutoFit();
                                        worksheet3.Column(5).AutoFit();
                                        worksheet3.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (3 >= _monthFrom && 3 <= _monthTo)
                    {
                        string marFrom = "03/01/" + _accountingRpt.Period;
                        string marTo = "03/31/" + _accountingRpt.Period;
                        worksheet1.Column(4).Width = 20;

                        ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                        using (SqlConnection DbCon04 = new SqlConnection(Tools.conString))
                        {
                            DbCon04.Open();
                            using (SqlCommand cmd04 = DbCon04.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd04.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + marTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + marFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd04.CommandTimeout = 0;
                                cmd04.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd04.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd04.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr04 = cmd04.ExecuteReader())
                                {
                                    if (dr04.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr04.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr04["ID"]);
                                            rSingle.Name = Convert.ToString(dr04["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr04["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr04["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr04["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr04["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet4.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet4.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet4.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet4.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet4.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet4.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet4.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet4.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet4.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet4.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet4.PrinterSettings.FitToPage = true;
                                        worksheet4.PrinterSettings.FitToWidth = 1;
                                        worksheet4.PrinterSettings.FitToHeight = 0;
                                        worksheet4.PrinterSettings.PrintArea = worksheet4.Cells[1, 1, incRowExcel2, 6];
                                        worksheet4.Column(1).AutoFit();
                                        worksheet4.Column(2).AutoFit();
                                        worksheet4.Column(3).AutoFit();
                                        worksheet4.Column(4).AutoFit();
                                        worksheet4.Column(5).AutoFit();
                                        worksheet4.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }

                    if (4 >= _monthFrom && 4 <= _monthTo)
                    {
                        string aprFrom = "04/01/" + _accountingRpt.Period;
                        string aprTo = "04/30/" + _accountingRpt.Period;
                        worksheet1.Column(5).Width = 20;

                        ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                        using (SqlConnection DbCon05 = new SqlConnection(Tools.conString))
                        {
                            DbCon05.Open();
                            using (SqlCommand cmd05 = DbCon05.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd05.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + aprTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + aprFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)  
								
                                Order BY C.ID";
                                cmd05.CommandTimeout = 0;
                                cmd05.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd05.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd05.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr04 = cmd05.ExecuteReader())
                                {
                                    if (dr04.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr04.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr04["ID"]);
                                            rSingle.Name = Convert.ToString(dr04["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr04["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr04["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr04["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr04["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet5.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet5.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet5.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet5.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet5.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet5.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet5.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet5.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet5.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet5.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet5.PrinterSettings.FitToPage = true;
                                        worksheet5.PrinterSettings.FitToWidth = 1;
                                        worksheet5.PrinterSettings.FitToHeight = 0;
                                        worksheet5.PrinterSettings.PrintArea = worksheet5.Cells[1, 1, incRowExcel2, 6];
                                        worksheet5.Column(1).AutoFit();
                                        worksheet5.Column(2).AutoFit();
                                        worksheet5.Column(3).AutoFit();
                                        worksheet5.Column(4).AutoFit();
                                        worksheet5.Column(5).AutoFit();
                                        worksheet5.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (5 >= _monthFrom && 5 <= _monthTo)
                    {
                        string mayFrom = "05/01/" + _accountingRpt.Period;
                        string mayTo = "05/31/" + _accountingRpt.Period;
                        worksheet1.Column(6).Width = 20;

                        ExcelWorksheet worksheet6 = package.Workbook.Worksheets[6];
                        using (SqlConnection DbCon06 = new SqlConnection(Tools.conString))
                        {
                            DbCon06.Open();
                            using (SqlCommand cmd06 = DbCon06.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd06.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + mayTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + mayFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd06.CommandTimeout = 0;
                                cmd06.CommandTimeout = 0;
                                cmd06.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd06.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd06.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr06 = cmd06.ExecuteReader())
                                {
                                    if (dr06.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr06.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr06["ID"]);
                                            rSingle.Name = Convert.ToString(dr06["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr06["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr06["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr06["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr06["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet6.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet6.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet6.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet6.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet6.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet6.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet6.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet6.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet6.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet6.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet6.PrinterSettings.FitToPage = true;
                                        worksheet6.PrinterSettings.FitToWidth = 1;
                                        worksheet6.PrinterSettings.FitToHeight = 0;
                                        worksheet6.PrinterSettings.PrintArea = worksheet6.Cells[1, 1, incRowExcel2, 6];
                                        worksheet6.Column(1).AutoFit();
                                        worksheet6.Column(2).AutoFit();
                                        worksheet6.Column(3).AutoFit();
                                        worksheet6.Column(4).AutoFit();
                                        worksheet6.Column(5).AutoFit();
                                        worksheet6.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }




                    if (6 >= _monthFrom && 6 <= _monthTo)
                    {
                        string junFrom = "06/01/" + _accountingRpt.Period;
                        string junTo = "06/30/" + _accountingRpt.Period;
                        worksheet1.Column(7).Width = 20;

                        ExcelWorksheet worksheet7 = package.Workbook.Worksheets[7];
                        using (SqlConnection DbCon07 = new SqlConnection(Tools.conString))
                        {
                            DbCon07.Open();
                            using (SqlCommand cmd07 = DbCon07.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd07.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + junTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + junFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd07.CommandTimeout = 0;
                                cmd07.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd07.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd07.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr07 = cmd07.ExecuteReader())
                                {
                                    if (dr07.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr07.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr07["ID"]);
                                            rSingle.Name = Convert.ToString(dr07["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr07["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr07["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr07["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr07["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet7.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet7.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet7.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet7.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet7.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet7.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet7.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet7.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet7.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet7.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet7.PrinterSettings.FitToPage = true;
                                        worksheet7.PrinterSettings.FitToWidth = 1;
                                        worksheet7.PrinterSettings.FitToHeight = 0;
                                        worksheet7.PrinterSettings.PrintArea = worksheet7.Cells[1, 1, incRowExcel2, 6];
                                        worksheet7.Column(1).AutoFit();
                                        worksheet7.Column(2).AutoFit();
                                        worksheet7.Column(3).AutoFit();
                                        worksheet7.Column(4).AutoFit();
                                        worksheet7.Column(5).AutoFit();
                                        worksheet7.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }

                    if (7 >= _monthFrom && 7 <= _monthTo)
                    {
                        string julFrom = "07/01/" + _accountingRpt.Period;
                        string julTo = "07/31/" + _accountingRpt.Period;
                        worksheet1.Column(8).Width = 20;

                        ExcelWorksheet worksheet8 = package.Workbook.Worksheets[8];
                        using (SqlConnection DbCon08 = new SqlConnection(Tools.conString))
                        {
                            DbCon08.Open();
                            using (SqlCommand cmd08 = DbCon08.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd08.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + julTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + julFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd08.CommandTimeout = 0;
                                cmd08.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd08.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd08.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr08 = cmd08.ExecuteReader())
                                {
                                    if (dr08.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr08.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr08["ID"]);
                                            rSingle.Name = Convert.ToString(dr08["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr08["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr08["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr08["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr08["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet8.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet8.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet8.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet8.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet8.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet8.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet8.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet8.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet8.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet8.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet8.PrinterSettings.FitToPage = true;
                                        worksheet8.PrinterSettings.FitToWidth = 1;
                                        worksheet8.PrinterSettings.FitToHeight = 0;
                                        worksheet8.PrinterSettings.PrintArea = worksheet8.Cells[1, 1, incRowExcel2, 6];
                                        worksheet8.Column(1).AutoFit();
                                        worksheet8.Column(2).AutoFit();
                                        worksheet8.Column(3).AutoFit();
                                        worksheet8.Column(4).AutoFit();
                                        worksheet8.Column(5).AutoFit();
                                        worksheet8.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    if (8 >= _monthFrom && 8 <= _monthTo)
                    {
                        string augFrom = "08/01/" + _accountingRpt.Period;
                        string augTo = "08/31/" + _accountingRpt.Period;
                        worksheet1.Column(9).Width = 20;

                        ExcelWorksheet worksheet9 = package.Workbook.Worksheets[9];
                        using (SqlConnection DbCon09 = new SqlConnection(Tools.conString))
                        {
                            DbCon09.Open();
                            using (SqlCommand cmd09 = DbCon09.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd09.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + augTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + augFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd09.CommandTimeout = 0;
                                cmd09.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd09.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd09.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr09 = cmd09.ExecuteReader())
                                {
                                    if (dr09.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr09.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr09["ID"]);
                                            rSingle.Name = Convert.ToString(dr09["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr09["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr09["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr09["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr09["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet9.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet9.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet9.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet9.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet9.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet9.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet9.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet9.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet9.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet9.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet9.PrinterSettings.FitToPage = true;
                                        worksheet9.PrinterSettings.FitToWidth = 1;
                                        worksheet9.PrinterSettings.FitToHeight = 0;
                                        worksheet9.PrinterSettings.PrintArea = worksheet9.Cells[1, 1, incRowExcel2, 6];
                                        worksheet9.Column(1).AutoFit();
                                        worksheet9.Column(2).AutoFit();
                                        worksheet9.Column(3).AutoFit();
                                        worksheet9.Column(4).AutoFit();
                                        worksheet9.Column(5).AutoFit();
                                        worksheet9.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (9 >= _monthFrom && 9 <= _monthTo)
                    {
                        string sepFrom = "09/01/" + _accountingRpt.Period;
                        string sepTo = "09/30/" + _accountingRpt.Period;
                        worksheet1.Column(10).Width = 20;

                        ExcelWorksheet worksheet10 = package.Workbook.Worksheets[10];
                        using (SqlConnection DbCon10 = new SqlConnection(Tools.conString))
                        {
                            DbCon10.Open();
                            using (SqlCommand cmd10 = DbCon10.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd10.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + sepTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK) and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + sepFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd10.CommandTimeout = 0;
                                cmd10.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd10.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd10.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr10 = cmd10.ExecuteReader())
                                {
                                    if (dr10.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr10.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr10["ID"]);
                                            rSingle.Name = Convert.ToString(dr10["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr10["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr10["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr10["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr10["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet10.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet10.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet10.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet10.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet10.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet10.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet10.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet10.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet10.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet10.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet10.PrinterSettings.FitToPage = true;
                                        worksheet10.PrinterSettings.FitToWidth = 1;
                                        worksheet10.PrinterSettings.FitToHeight = 0;
                                        worksheet10.PrinterSettings.PrintArea = worksheet10.Cells[1, 1, incRowExcel2, 6];
                                        worksheet10.Column(1).AutoFit();
                                        worksheet10.Column(2).AutoFit();
                                        worksheet10.Column(3).AutoFit();
                                        worksheet10.Column(4).AutoFit();
                                        worksheet10.Column(5).AutoFit();
                                        worksheet10.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (10 >= _monthFrom && 10 <= _monthTo)
                    {
                        string octFrom = "10/01/" + _accountingRpt.Period;
                        string octTo = "10/31/" + _accountingRpt.Period;
                        worksheet1.Column(11).Width = 20;

                        ExcelWorksheet worksheet11 = package.Workbook.Worksheets[11];
                        using (SqlConnection DbCon11 = new SqlConnection(Tools.conString))
                        {
                            DbCon11.Open();
                            using (SqlCommand cmd11 = DbCon11.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd11.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + octTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + octFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)
								
                                Order BY C.ID";
                                cmd11.CommandTimeout = 0;
                                cmd11.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd11.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd11.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr11 = cmd11.ExecuteReader())
                                {
                                    if (dr11.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr11.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr11["ID"]);
                                            rSingle.Name = Convert.ToString(dr11["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr11["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr11["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr11["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr11["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet11.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet11.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet11.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet11.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet11.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet11.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet11.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet11.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet11.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet11.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet11.PrinterSettings.FitToPage = true;
                                        worksheet11.PrinterSettings.FitToWidth = 1;
                                        worksheet11.PrinterSettings.FitToHeight = 0;
                                        worksheet11.PrinterSettings.PrintArea = worksheet11.Cells[1, 1, incRowExcel2, 6];
                                        worksheet11.Column(1).AutoFit();
                                        worksheet11.Column(2).AutoFit();
                                        worksheet11.Column(3).AutoFit();
                                        worksheet11.Column(4).AutoFit();
                                        worksheet11.Column(5).AutoFit();
                                        worksheet11.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (11 >= _monthFrom && 11 <= _monthTo)
                    {
                        string novFrom = "11/01/" + _accountingRpt.Period;
                        string novTo = "11/30/" + _accountingRpt.Period;
                        worksheet1.Column(12).Width = 20;

                        ExcelWorksheet worksheet12 = package.Workbook.Worksheets[12];
                        using (SqlConnection DbCon12 = new SqlConnection(Tools.conString))
                        {
                            DbCon12.Open();
                            using (SqlCommand cmd12 = DbCon12.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }

                                cmd12.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + novTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + novFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3 
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd12.CommandTimeout = 0;
                                cmd12.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd12.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd12.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr12 = cmd12.ExecuteReader())
                                {
                                    if (dr12.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr12.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr12["ID"]);
                                            rSingle.Name = Convert.ToString(dr12["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr12["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr12["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr12["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr12["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet12.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet12.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet12.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet12.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet12.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet12.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet12.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet12.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet12.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet12.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet12.PrinterSettings.FitToPage = true;
                                        worksheet12.PrinterSettings.FitToWidth = 1;
                                        worksheet12.PrinterSettings.FitToHeight = 0;
                                        worksheet12.PrinterSettings.PrintArea = worksheet12.Cells[1, 1, incRowExcel2, 6];
                                        worksheet12.Column(1).AutoFit();
                                        worksheet12.Column(2).AutoFit();
                                        worksheet12.Column(3).AutoFit();
                                        worksheet12.Column(4).AutoFit();
                                        worksheet12.Column(5).AutoFit();
                                        worksheet12.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }



                    if (12 >= _monthFrom && 12 <= _monthTo)
                    {
                        string decFrom = "12/01/" + _accountingRpt.Period;
                        string decTo = "12/31/" + _accountingRpt.Period;
                        worksheet1.Column(13).Width = 20;

                        ExcelWorksheet worksheet13 = package.Workbook.Worksheets[13];
                        using (SqlConnection DbCon13 = new SqlConnection(Tools.conString))
                        {
                            DbCon13.Open();
                            using (SqlCommand cmd13 = DbCon13.CreateCommand())
                            {
                                string _status = "";

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
                                    _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status <> 3 ";
                                }
                                else if (_accountingRpt.Status == 6)
                                {
                                    _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status <> 3  ";
                                }


                                cmd13.CommandText = @"
                                Declare @Journal Table 
                                (
	                                JournalPK int
                                )

                                insert into @Journal

                                select distinct B.JournalPK from JournalDetail A
                                left join Journal B on A.JournalPK = B.JournalPK
                                where AccountPK in
                                (
                                select AccountPK from Account where ParentPK in (3)
                                ) " + _status + @"  and PeriodPK = @PeriodPK

                                SELECT C.ID, C.Name,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT A.AccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                WHERE  B.ValueDate <= '" + decTo + @"' and  B.PeriodPK = @PeriodPK  and A.JournalPK in (select JournalPK from @Journal)
	                            " + _status + @"
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE
                                (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.AccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                WHERE  B.ValueDate < '" + decFrom + @"' and  B.PeriodPK = @PeriodPK   and A.JournalPK in (select JournalPK from @Journal)
	                            and B.status <> 3
								
                                Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                OR B.ParentPK9 = A.AccountPK)       and A.Status = 2
                                Group BY A.AccountPK       
                                ) AS B ON A.AccountPK = B.AccountPK        
                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                WHERE (A.CurrentBalance <> 0)          
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0) 
								
                                Order BY C.ID";
                                cmd13.CommandTimeout = 0;
                                cmd13.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                cmd13.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd13.Parameters.AddWithValue("@PeriodPK", _accountingRpt.PeriodPK);

                                using (SqlDataReader dr13 = cmd13.ExecuteReader())
                                {
                                    if (dr13.HasRows)
                                    {
                                        List<AccountingRpt> rList = new List<AccountingRpt>();
                                        while (dr13.Read())
                                        {
                                            AccountingRpt rSingle = new AccountingRpt();
                                            rSingle.ID = Convert.ToString(dr13["ID"]);
                                            rSingle.Name = Convert.ToString(dr13["Name"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr13["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr13["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr13["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr13["CurrentBaseBalance"]);
                                            rList.Add(rSingle);
                                        }
                                        var QueryByClientID2 =
                                         from r in rList
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel2 = 1;
                                        int _IncRow2 = 11;
                                        foreach (var rsHeader in QueryByClientID2)
                                        {

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet13.Cells[incRowExcel2, 1].Value = rsDetail.ID;
                                                worksheet13.Cells[incRowExcel2, 2].Value = rsDetail.Name;
                                                //worksheet13.Cells[incRowExcel2, 3].Value = rsDetail.PreviousBaseBalance;
                                                //worksheet13.Cells[incRowExcel2, 3].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet13.Cells[incRowExcel2, 4].Style.Numberformat.Format = "#,####0.00";
                                                worksheet13.Cells[incRowExcel2, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet13.Cells[incRowExcel2, 5].Style.Numberformat.Format = "#,####0.00";
                                                //worksheet13.Cells[incRowExcel2, 6].Value = rsDetail.CurrentBaseBalance;
                                                //worksheet13.Cells[incRowExcel2, 6].Style.Numberformat.Format = "#,####0.00";

                                                worksheet13.Calculate();

                                                incRowExcel2++;


                                                _IncRow2 = _IncRow2 + 2;
                                            }
                                            worksheet1.Calculate();
                                            int _IncRowA5 = incRowExcel2 + 1000;

                                        }

                                        worksheet13.PrinterSettings.FitToPage = true;
                                        worksheet13.PrinterSettings.FitToWidth = 1;
                                        worksheet13.PrinterSettings.FitToHeight = 0;
                                        worksheet13.PrinterSettings.PrintArea = worksheet13.Cells[1, 1, incRowExcel2, 6];
                                        worksheet13.Column(1).AutoFit();
                                        worksheet13.Column(2).AutoFit();
                                        worksheet13.Column(3).AutoFit();
                                        worksheet13.Column(4).AutoFit();
                                        worksheet13.Column(5).AutoFit();
                                        worksheet13.Column(6).AutoFit();

                                    }


                                }

                            }

                        }
                    }


                    package.Save();
                    return FilePath;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

    }
}
