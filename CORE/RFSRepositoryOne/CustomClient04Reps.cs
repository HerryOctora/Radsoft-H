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


namespace RFSRepositoryOne
{
    public class CustomClient04Reps
    {
        Host _host = new Host();
        public string CashierReference_GenerateNewReference(string _type, int _periodPK, DateTime _valueDate, int _cashRefPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText =
                        @"Declare @BankID nvarchar(50)
                         Declare @LastNo int   
                         Declare @Reference nvarchar(50) 
      
                         select @BankID = C.ID from CashRef A 
                         left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2
                         left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                         where A.CashRefPK = @CashRefPK 

                         if exists(Select Top 1 * from cashierReference where Type = @type And PeriodPK = @PeriodPK    
    
                         and substring(right(reference,4),1,2) = month(@ValueDate)  )       
    
                         BEGIN       
    
                          Select @LastNo = max(No) + 1 From CashierReference where Type = @type And PeriodPK = @periodPK and   
    
                          substring(right(reference,4),1,2) = month(@ValueDate)       
    
                         Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @type = 'CP' then 'OUT' else    
    
                         Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else    
    
                         case when @type = 'GENERAL' then 'GEN' else    
    
                         case when @type = 'ADJUSTMENT' then 'ADJ' Else Case when @Type = 'INV' then 'INV' else 'IN' END END END END END END +  
    
                          '/' + @BankID + '/' + REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')       
    
                         Update CashierReference Set Reference = @Reference, No = @LastNo where Type = @type And PeriodPK = @periodPK    
    
                         and substring(right(reference,4),1,2) = month(@ValueDate)    
    
                        END    
    
                        ELSE BEGIN       
    
                         Set @Reference = '1/'  +  Case when @type = 'CP' then 'OUT' else    
    
                          Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else    
    
                         case when @type = 'GENERAL' then 'GEN' else    
    
                         case when @type = 'ADJUSTMENT' then 'ADJ' Else Case when @Type = 'INV' then 'INV' else 'IN' END END END END END END + '/' + @BankID + '/' +  + REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')    
    
                          Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)    
    
                          Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,@type,@Reference,1 from CashierReference   
    
                         END       
    
                         Select isnull(@Reference,'')   LastReference   ";


                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.Parameters.AddWithValue("@periodPK", _periodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@CashRefPK", _cashRefPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["LastReference"]);
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

        public string JournalReference_GenerateNewReference(string _type, int _periodPK, DateTime _valueDate)
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
                        Declare @LastNo int   
                     Declare @Reference nvarchar(20) 
      
                     if exists(Select Top 1 * from cashierReference where Type = @type And PeriodPK = @PeriodPK    
        
                     and substring(right(reference,4),1,2) = month(@ValueDate))       
    
                     BEGIN       
    
                     Select @LastNo = max(No) +  1 From CashierReference where Type = @type And PeriodPK = @periodPK and   
        
                     substring(right(reference,4),1,2) = month(@ValueDate)       
        
                     Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @type = 'CP' then 'OUT' else    
        
                     Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else    
        
                     case when @type = 'GJ' then 'GJ' Else Case when @Type = 'INV' then 'INV' else 'IN' END END END END END   
        
                     + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')       
    
                     Update CashierReference Set Reference = @Reference, No = @LastNo where Type = @type And PeriodPK = @periodPK    
        
                     and substring(right(reference,4),1,2) = month(@ValueDate)    
    
                    END    
    
                    ELSE BEGIN       
    
                     Set @Reference = '1/' +  Case when @type = 'CP' then 'OUT' else    
        
                      Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else    
       
                      case when @type = 'GJ' then 'GJ' Else Case when @Type = 'INV' then 'INV' else 'IN' END END END END END + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')    
    
                      Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)    
        
                      Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,@type,@Reference,1 from CashierReference   
    
                     END       
    
                     Select isnull(@Reference,'')   LastReference   ";


                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.Parameters.AddWithValue("@periodPK", _periodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["LastReference"]);
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


        public List<ReferenceDetail> ReferenceDetail(int _status, string _reference, string _type)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReferenceDetail> L_cashier = new List<ReferenceDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "CP")
                        {
                            cmd.CommandText =
                         @" Select valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
                            DebitCredit,(Case When DebitCredit ='D' then BaseDebit else 0 end) Debit,(Case When DebitCredit ='D' then 0 else BaseCredit end) Credit,isnull(F.ID,'') DepartmentID       
                            from Cashier C       
                            left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.Reference = @Reference  and C.Status in (1,2)      
                            UNION ALL       
                            Select valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                            'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID           
                            from Cashier C       
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.Reference = @Reference and C.Status in (1,2) 
                            group by C.EntryUsersID,Reference , Valuedate,A.ID, A.Name    
                            Order By DebitCredit Desc ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"Select valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
                            DebitCredit,(Case When DebitCredit ='C' then 0 else BaseCredit end) Debit,(Case When DebitCredit ='C' then BaseDebit else 0 end) Credit,F.ID DepartmentID       
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where C.Reference = @Reference  and C.Status in (1,2)         
                            UNION ALL         
                            Select valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
                            'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID             
                            from Cashier C         
                            left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                            Where C.Reference = @Reference  and C.Status in (1,2)       
                            group by C.EntryUsersID,Reference , Valuedate,A.ID, A.Name    
                            Order By DebitCredit Desc ";

                        }


                        cmd.Parameters.AddWithValue("@Reference", _reference);
                        cmd.Parameters.AddWithValue("@Status", _status);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReferenceDetail M_cashier = new ReferenceDetail();
                                    M_cashier.ValueDate = Convert.ToString(dr["ValueDate"]);
                                    M_cashier.AccountID = Convert.ToString(dr["AccountID"]);
                                    M_cashier.AccountName = Convert.ToString(dr["AccountName"]);
                                    M_cashier.Description = Convert.ToString(dr["Description"]);
                                    M_cashier.DebitCredit = Convert.ToString(dr["DebitCredit"]);
                                    M_cashier.Debit = Convert.ToDecimal(dr["Debit"]);
                                    M_cashier.Credit = Convert.ToDecimal(dr["Credit"]);
                                    M_cashier.DepartmentID = Convert.ToString(dr["DepartmentID"]);
                                    L_cashier.Add(M_cashier);
                                }
                            }
                            return L_cashier;
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
            // yang uda dibenerin
           

            #region CALK
            if (_accountingRpt.ReportName.Equals("CALK"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"       
                            select A.AccountPK,Row,Col,A.ID,A.Name,case when Operator = '+' then Amount * 1 else Amount * -1 end Amount from (
                            select 
                            A.Operator,A.AccountPK,Row,Col,B.ID,B.Name,abs(dbo.FGetGroupAccountBalance(@DateTo,A.AccountPK)) Amount from  MapRptCatatanAtasLapKeu A 
                            left join Account B on A.AccountPK = B.AccountPK and B.Status = 2
                            where  A.status = 2 
                            Group By A.AccountPK,Row,Col,B.ID,B.Name,Operator) A
                            order by A.ID Asc ";
                            cmd.CommandTimeout = 0;
                            //cmd.Parameters.AddWithValue("@DateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _accountingRpt.ValueDateTo);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    ////ini untuk mengcopy dari templat Excelnya
                                    string filePath = Tools.ReportsPath + "CALK" + "_" + _userID + ".xlsx";
                                    File.Copy(Tools.ReportsTemplatePath + "LK_1.xlsx", filePath, true);
                                    FileInfo excelFile = new FileInfo(filePath);

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        //// ini posisi Sheet Templat Excelnya
                                        ExcelWorksheet worksheet = package.Workbook.Worksheets["CALK"];
                                        ////worksheet.Cells[1, 8].Value = _host.Get_DepartmentID(Convert.ToInt32(_accountingRpt.DepartmentFrom));
                                        worksheet.Cells[3, 2].Value = _accountingRpt.ValueDateTo;
                                        worksheet.Cells[3, 2].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[51, 4].Value = "Pada tanggal " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ", " + " Perusahaan menerapkan PSAK dan ISAK baru, amandemen, dan penyesuaian ";
                                        worksheet.Cells[51, 4].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[149, 13].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy");
                                        worksheet.Cells[149, 13].Style.Numberformat.Format = "dd-MMM-yy";

                                        worksheet.Cells[42, 4].Value = "Laporan keuangan PT Indo Arthabuana Investama untuk periode 10 (Sepuluh) bulan yang berakhir " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy");
                                        worksheet.Cells[42, 4].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[918, 3].Value = "Pada tanggal " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ", " + "nilai wajar liabilitas keuangan tidak terdapat perbedaan material dengan nilai";
                                        worksheet.Cells[918, 3].Style.Numberformat.Format = "dd-MMM-yy";

                                        worksheet.Cells[794, 3].Value = "Susunan kepemilikan saham per " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + " " + "adalah sebagai berikut :";
                                        worksheet.Cells[794, 3].Style.Numberformat.Format = "dd-MMM-yy";

                                        worksheet.Cells[810, 3].Value = "Perusahaan belum mempunyai penghasilan dalam periode Sembilan bulan yang berakhir " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ". ";
                                        worksheet.Cells[810, 3].Style.Numberformat.Format = "dd-MMM-yy";

                                        worksheet.Cells[1001, 18].Value = "Total per " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ". ";
                                        worksheet.Cells[1001, 18].Style.Numberformat.Format = "dd-MMM-yy";

                                        worksheet.Cells[29, 4].Value = "dan Direksi per " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + " " + " adalah sebagai berikut: ";
                                        worksheet.Cells[29, 4].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[151, 13].Value = _host.Get_CurrencyRate(2, _accountingRpt.ValueDateTo);
                                        worksheet.Cells[663, 4].Value = "yang berakhir " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ". ";
                                        worksheet.Cells[663, 4].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[884, 3].Value = "Pada tanggal " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ", " + " PT Indo Arthabuana Investama belum melakukan perjanjian kerja sama dengan pihak ";
                                        worksheet.Cells[884, 3].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[895, 3].Value = "Pada tanggal " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + " " + "Perusahaan tidak mempunyai aset dan liabilitas moneter dalam mata uang asing. ";
                                        worksheet.Cells[895, 3].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[899, 3].Value = "Tabel berikut menyajikan aset dan liabilitas keuangan Perusahaan per" + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ". ";
                                        worksheet.Cells[899, 3].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[907, 3].Value = "Pada tanggal" + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ", " + "nilai wajar aset keuangan tidak terdapat perbedaan material dengan nilai tercatatnya. ";
                                        worksheet.Cells[907, 3].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[936, 3].Value = "terkait risiko bunga pe " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ". ";
                                        worksheet.Cells[936, 3].Style.Numberformat.Format = "dd-MMM-yy";
                                        worksheet.Cells[981, 3].Value = "didiskontokan pada tanggal " + " " + Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yy") + ".  ";
                                        worksheet.Cells[981, 3].Style.Numberformat.Format = "dd-MMM-yy";



                                        while (dr0.Read())
                                        {
                                            //// ini untuk ambil posisi Col dan Row. di ambil date Mappingan Querynya
                                            string _pos = Convert.ToString(dr0["col"]) + Convert.ToString(dr0["row"]);
                                            //// ini untuk ambil atau masukin nilainya
                                            decimal _amount = Convert.ToDecimal(dr0["Amount"]);
                                            //// ini untuk ngambil posisinya dan nilainya
                                            worksheet.Cells[_pos].Value = Convert.ToDecimal(_amount);
                                        }
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

    }
}