using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class BudgetSummaryReps
    {
        Host _host = new Host();
        //1
        string _insertCommand = "INSERT INTO [dbo].[BudgetSummary] " +
                            "([BudgetSummaryPK],[HistoryPK],[Status],[PeriodPK],[Type],[AccountName],[ExcelRow],[Amount],";
        string _paramaterCommand = "@PeriodPK,@Type,@AccountName,@ExcelRow,@Amount,";

        //2
        private BudgetSummary setBudgetSummary(SqlDataReader dr)
        {
            BudgetSummary M_BudgetSummary = new BudgetSummary();
            M_BudgetSummary.BudgetSummaryPK = Convert.ToInt32(dr["BudgetSummaryPK"]);
            M_BudgetSummary.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BudgetSummary.Status = Convert.ToInt32(dr["Status"]);
            M_BudgetSummary.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BudgetSummary.Notes = Convert.ToString(dr["Notes"]);
            M_BudgetSummary.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_BudgetSummary.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_BudgetSummary.Type = Convert.ToInt32(dr["Type"]);
            M_BudgetSummary.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_BudgetSummary.AccountName = Convert.ToString(dr["AccountName"]);
            M_BudgetSummary.ExcelRow = Convert.ToInt32(dr["ExcelRow"]);
            M_BudgetSummary.Amount = Convert.ToDecimal(dr["Amount"]);
            M_BudgetSummary.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BudgetSummary.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BudgetSummary.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BudgetSummary.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BudgetSummary.EntryTime = dr["EntryTime"].ToString();
            M_BudgetSummary.UpdateTime = dr["UpdateTime"].ToString();
            M_BudgetSummary.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BudgetSummary.VoidTime = dr["VoidTime"].ToString();
            M_BudgetSummary.DBUserID = dr["DBUserID"].ToString();
            M_BudgetSummary.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BudgetSummary.LastUpdate = dr["LastUpdate"].ToString();
            M_BudgetSummary.LastUpdateDB = dr["LastUpdateDB"].ToString();
            return M_BudgetSummary;
        }

        public List<BudgetSummary> BudgetSummary_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<BudgetSummary> L_BudgetSummary = new List<BudgetSummary>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID PeriodID,
                            case when A.Type = 1 then 'Balance Sheet' else 'Profit and Loss' end TypeDesc , A.* from BudgetSummary A
                            left join Period B on A.PeriodPK = B.PeriodPK and B.Status = 2
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID PeriodID,
                            case when A.Type = 1 then 'Balance Sheet' else 'Profit and Loss' end TypeDesc ,A.* from BudgetSummary A 
                            left join Period B on A.PeriodPK = B.PeriodPK and B.Status = 2
                            order by A.PeriodPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BudgetSummary.Add(setBudgetSummary(dr));
                                }
                            }
                            return L_BudgetSummary;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int BudgetSummary_Add(BudgetSummary _budgetSummary, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(BudgetSummaryPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from BudgetSummary";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _budgetSummary.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(BudgetSummaryPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from BudgetSummary";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _budgetSummary.PeriodPK);
                        cmd.Parameters.AddWithValue("@Type", _budgetSummary.Type);
                        cmd.Parameters.AddWithValue("@AccountName", _budgetSummary.AccountName);
                        cmd.Parameters.AddWithValue("@ExcelRow", _budgetSummary.ExcelRow);
                        cmd.Parameters.AddWithValue("@Amount", _budgetSummary.Amount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _budgetSummary.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "BudgetSummary");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int BudgetSummary_Update(BudgetSummary _budgetSummary, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_budgetSummary.BudgetSummaryPK, _budgetSummary.HistoryPK, "BudgetSummary");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BudgetSummary set status=2,Notes=@Notes,PeriodPK=@PeriodPK," +
                                "Type=@Type,AccountName=@AccountName,ExcelRow=@ExcelRow,Amount=@Amount," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BudgetSummaryPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _budgetSummary.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                            cmd.Parameters.AddWithValue("@Notes", _budgetSummary.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _budgetSummary.PeriodPK);
                            cmd.Parameters.AddWithValue("@Type", _budgetSummary.Type);
                            cmd.Parameters.AddWithValue("@AccountName", _budgetSummary.AccountName);
                            cmd.Parameters.AddWithValue("@ExcelRow", _budgetSummary.ExcelRow);
                            cmd.Parameters.AddWithValue("@Amount", _budgetSummary.Amount);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _budgetSummary.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _budgetSummary.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BudgetSummary set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BudgetSummaryPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _budgetSummary.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        return 0;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BudgetSummary set Notes=@Notes,PeriodPK=@PeriodPK," +
                                "Type=@Type,AccountName=@AccountName,ExcelRow=@ExcelRow,Amount=@Amount," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BudgetSummaryPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _budgetSummary.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                                cmd.Parameters.AddWithValue("@Notes", _budgetSummary.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _budgetSummary.PeriodPK);
                                cmd.Parameters.AddWithValue("@Type", _budgetSummary.Type);
                                cmd.Parameters.AddWithValue("@AccountName", _budgetSummary.AccountName);
                                cmd.Parameters.AddWithValue("@ExcelRow", _budgetSummary.ExcelRow);
                                cmd.Parameters.AddWithValue("@Amount", _budgetSummary.Amount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _budgetSummary.EntryUsersID);
                                cmd.Parameters.AddWithValue("@ApprovedUsersID", _budgetSummary.EntryUsersID);
                                cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_budgetSummary.BudgetSummaryPK, "BudgetSummary");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BudgetSummary where BudgetSummaryPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _budgetSummary.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _budgetSummary.PeriodPK);
                                cmd.Parameters.AddWithValue("@Type", _budgetSummary.Type);
                                cmd.Parameters.AddWithValue("@AccountName", _budgetSummary.AccountName);
                                cmd.Parameters.AddWithValue("@ExcelRow", _budgetSummary.ExcelRow);
                                cmd.Parameters.AddWithValue("@Amount", _budgetSummary.Amount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _budgetSummary.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BudgetSummary set status= 4,Notes=@Notes, " +
                                    "LastUpdate=@LastUpdate where BudgetSummaryPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _budgetSummary.Notes);
                                cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _budgetSummary.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return _newHisPK;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void BudgetSummary_Approved(BudgetSummary _budgetSummary)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BudgetSummary set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where BudgetSummaryPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                        cmd.Parameters.AddWithValue("@historyPK", _budgetSummary.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _budgetSummary.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BudgetSummary set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BudgetSummaryPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _budgetSummary.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void BudgetSummary_Reject(BudgetSummary _budgetSummary)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BudgetSummary set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BudgetSummaryPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                        cmd.Parameters.AddWithValue("@historyPK", _budgetSummary.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _budgetSummary.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BudgetSummary set status= 2,LastUpdate=@LastUpdate where BudgetSummaryPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void BudgetSummary_Void(BudgetSummary _budgetSummary)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BudgetSummary set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BudgetSummaryPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _budgetSummary.BudgetSummaryPK);
                        cmd.Parameters.AddWithValue("@historyPK", _budgetSummary.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _budgetSummary.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        // REA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public string BudgetSummaryImport(string _fileSource, string _userID, string _period)
        {
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table BudgetSummaryTemp";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.BudgetSummaryTemp";
                bulkCopy.WriteToServer(CreateDataTableFromBudgetSummaryTempExcelFile(_fileSource, _period));
            }

            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                        --drop table #BudgetSummaryTemp
                        --drop table #Temp

                        create table #BudgetSummaryTemp
                        (
                        ExcelRow int,
                        Amount decimal(18,4),
                        Type int,
                        PeriodPK int,
                        AccountName Nvarchar(100)
                        )

                        create table #Temp
                        (
                        PeriodPK int,
                        AccountName nvarchar(100),
                        Amount numeric(18,4),
                        Type int
                        )

                        insert into #Temp
                        select B.PeriodPK PeriodPK,rtrim(ltrim(AccountName))AccountName,isnull(CAST(Amount AS numeric(18,4)),0) Amount,CAST(Type AS int) Type 
                        from BudgetSummaryTemp A
                        left join Period B on A.PeriodID = B.ID and B.status in (1,2)

                        insert into #BudgetSummaryTemp
                        select case when AccountName = 'KAS DAN SETARA KAS' then 8 
                        when AccountName = 'DEPOSITO BERJANGKA' then 9 
                        when AccountName = 'PORTOFOLIO EFEK' then 10
                        when AccountName = 'PIUTANG USAHA' then 11
                        when AccountName = 'PIUTANG LAINNYA' then 12
                        when AccountName = 'UANG MUKA' then 13
                        when AccountName = 'BIAYA DIBAYAR DIMUKA' then 14
                        when AccountName = 'PAJAK DIBAYAR DIMUKA' then 15
                        when AccountName = 'INVESTASI JANGKA PANJANG' then 16
                        when AccountName = 'AKTIVA TETAP - NILAI BUKU' then 17
                        when AccountName = 'UANG JAMINAN' then 18
                        when AccountName = 'AKTIVA LAIN-LAIN' then 19
                        when AccountName = 'KEWAJIBAN JANGKA PENDEK' then 24
                        when AccountName = 'HUTANG USAHA' then 25
                        when AccountName = 'HUTANG PAJAK' then 26
                        when AccountName = 'HUTANG LAINNYA' then 27
                        when AccountName = 'BIAYA MASIH HARUS DIBAYAR' then 28
                        when AccountName = 'DEFERRED INCOME' then 29
                        when AccountName = 'KEWAJIBAN JANGKA PANJANG' then 30
                        when AccountName = 'PINJAMAN SUBORDINASI' then 31
                        when AccountName = 'MODAL SAHAM' then 35
                        when AccountName = 'SALDO LABA DITAHAN' then 36
                        when AccountName = 'LABA (RUGI) TAHUN BERJALAN' then 37
                        when AccountName = 'DIVIDEN' then 38
                        when AccountName = 'MANAJER INVESTASI' then 8
                        when AccountName = 'PENASIHAT INVESTASI' then 9
                        when AccountName = 'AGEN PENJUALAN' then 12
                        when AccountName = 'DANA PROGRAM KEGIATAN' then 13
                        when AccountName = 'IURAN OJK' then 14
                        when AccountName = 'PORTOFOLIO' then 18
                        when AccountName = 'KOMISI PENJUALAN' then 19
                        when AccountName = 'REVALUASI PORTOFOLIO' then 20
                        when AccountName = 'GAJI DAN TUNJANGAN' then 24
                        when AccountName = 'MANFAAT PEKERJA' then 25
                        when AccountName = 'SEWA DAN PEMELIHARAAN' then 26
                        when AccountName = 'TELEKOMUNIKASI' then 27
                        when AccountName = 'PENYUSUTAN' then 28
                        when AccountName = 'JASA PROFESIONAL' then 29
                        when AccountName = 'IKLAN, PROMOSI & PEMASARAN' then 30
                        when AccountName = 'BIAYA TRANSAKSI' then 31
                        when AccountName = 'BIAYA PERJALANAN & TRANSPORTASI' then 32
                        when AccountName = 'BIAYA PENDIDIKAN' then 33
                        when AccountName = 'BIAYA ADMINISTRASI & UMUM' then 34
                        when AccountName = 'PENDAPATAN BUNGA' then 39
                        when AccountName = 'BEBAN BUNGA' then 40
                        when AccountName = 'ADMINISTRASI BANK' then 41
                        when AccountName = 'LABA(RUGI) PENJUALAN AKTIVA' then 42
                        when AccountName = 'LABA(RUGI) SELISIH KURS' then 43
                        when AccountName = 'PENDAPATAN BIAYA LAIN-LAIN' then 44
                        when AccountName = 'DIVIDEN DAN BONUS' then 45
                        when AccountName = 'PAJAK KINI' then 50
                        when AccountName = 'DEFERRED TAX' then 51
                        else 0 end ExcelRow,Amount,Type,PeriodPK,AccountName 
                        from #Temp 



                        If Exists(
	                        select * from BudgetSummary A
	                        left join #BudgetSummaryTemp B on A.PeriodPK = B.PeriodPK and A.status in (1,2)
                        )

                        BEGIN
	                        delete A from BudgetSummary A
	                        left join #BudgetSummaryTemp B on A.PeriodPK = B.PeriodPK and A.status in (1,2)
                        END
                        ELSE
                        BEGIN

	                        DECLARE @BudgetSummaryPK BigInt  
	                        SELECT @BudgetSummaryPK = isnull(Max(BudgetSummaryPK),0) FROM BudgetSummary

	                        INSERT INTO BudgetSummary(BudgetSummaryPK,HistoryPK,Status,Notes,PeriodPK,  
	                        Type,AccountName,ExcelRow,Amount,EntryUsersID,EntryTime,LastUpdate)
	                        SELECT Row_number() over(order by Type) + @BudgetSummaryPK,1,1,'',PeriodPK,  
	                        Type,AccountName,ExcelRow,Amount,@EntryUsersID,@Lastupdate,@Lastupdate  
	                        FROM #BudgetSummaryTemp where ExcelRow <> 0



                        select 'Success' Result
                        END
                                                ";
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Lastupdate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return "Import Success";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromBudgetSummaryTempExcelFile(string _path, string _period)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PeriodID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AccountName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Amount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Type";
                    dc.Unique = false;
                    dt.Columns.Add(dc);





                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [BS$] ";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["PeriodID"] = _period;
                                    dr["AccountName"] = odRdr[1];
                                    dr["Amount"] = odRdr[2];
                                    dr["Type"] = 1;

                                    //if (dr["BudgetSummaryTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                    dt.Rows.Add(dr);
                                } while (odRdr.Read());
                            }

                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Pnl$] ";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["PeriodID"] = _period;
                                    dr["AccountName"] = odRdr[1];
                                    dr["Amount"] = odRdr[2];
                                    dr["Type"] = 2;

                                    //if (dr["BudgetSummaryTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                    dt.Rows.Add(dr);
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }




    }
}
