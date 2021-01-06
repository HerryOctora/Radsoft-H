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
    public class AumForBudgetBegBalanceReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AumForBudgetBegBalance] " +
                            @"([AumForBudgetBegBalancePK],[HistoryPK],[Status],[ReportPeriodPK],[PeriodPK],[InstrumentPK],[AUM],[AgentPK],[MFeeAmount],[Date],";
        string _paramaterCommand = @"@ReportPeriodPK,@PeriodPK,@InstrumentPK,@AUM,@AgentPK,@MFeeAmount,@Date,";

        //2
        private AumForBudgetBegBalance setAumForBudgetBegBalance(SqlDataReader dr)
        {
            AumForBudgetBegBalance M_AumForBudgetBegBalance = new AumForBudgetBegBalance();
            M_AumForBudgetBegBalance.AumForBudgetBegBalancePK = Convert.ToInt32(dr["AumForBudgetBegBalancePK"]);
            M_AumForBudgetBegBalance.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AumForBudgetBegBalance.Status = Convert.ToInt32(dr["Status"]);
            M_AumForBudgetBegBalance.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AumForBudgetBegBalance.Notes = Convert.ToString(dr["Notes"]);
            M_AumForBudgetBegBalance.ReportPeriodID = dr["ReportPeriodID"].Equals(DBNull.Value) == true ? "" : dr["ReportPeriodID"].ToString();
            M_AumForBudgetBegBalance.ReportPeriodPK = dr["ReportPeriodPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ReportPeriodPK"]);
            M_AumForBudgetBegBalance.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_AumForBudgetBegBalance.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_AumForBudgetBegBalance.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_AumForBudgetBegBalance.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_AumForBudgetBegBalance.AgentPK = dr["AgentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentPK"]);
            M_AumForBudgetBegBalance.AgentID = dr["AgentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AgentID"]);
            M_AumForBudgetBegBalance.AUM = Convert.ToDecimal(dr["AUM"]);
            M_AumForBudgetBegBalance.MFeeAmount = Convert.ToInt32(dr["MFeeAmount"]);
            M_AumForBudgetBegBalance.Date = Convert.ToDateTime(dr["Date"]);
            M_AumForBudgetBegBalance.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AumForBudgetBegBalance.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AumForBudgetBegBalance.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AumForBudgetBegBalance.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AumForBudgetBegBalance.EntryTime = dr["EntryTime"].ToString();
            M_AumForBudgetBegBalance.UpdateTime = dr["UpdateTime"].ToString();
            M_AumForBudgetBegBalance.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AumForBudgetBegBalance.VoidTime = dr["VoidTime"].ToString();
            M_AumForBudgetBegBalance.DBUserID = dr["DBUserID"].ToString();
            M_AumForBudgetBegBalance.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AumForBudgetBegBalance.LastUpdate = dr["LastUpdate"].ToString();
            M_AumForBudgetBegBalance.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AumForBudgetBegBalance;
        }

        public List<AumForBudgetBegBalance> AumForBudgetBegBalance_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AumForBudgetBegBalance> L_AumForBudgetBegBalance = new List<AumForBudgetBegBalance>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                        ab.PeriodPK ReportPeriodPK,ab.ID ReportPeriodID,b.PeriodPK,b.ID PeriodID, d.InstrumentPK, d.ID + ' - ' +  d.Name InstrumentID, e.ID + ' - ' +  e.Name AgentID,* from AumForBudgetBegBalance A
                                        left join Period b on a.PeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.ReportPeriodPK = ab.PeriodPK and ab.status in(1,2)
										left join Instrument d on a.InstrumentPK = d.InstrumentPK and d.Status in(1,2)
										left join Agent e on a.AgentPK = e.AgentPK and e.Status in(1,2)
                                        where A.status = @status order by AumForBudgetBegBalancePK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                        ab.PeriodPK ReportPeriodPK,ab.ID ReportPeriodID,b.PeriodPK,b.ID PeriodID, d.InstrumentPK, d.ID + ' - ' +  d.Name InstrumentID, e.ID + ' - ' +  e.Name AgentID,* from AumForBudgetBegBalance A
                                        left join Period b on a.PeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.ReportPeriodPK = ab.PeriodPK and ab.status in(1,2)
										left join Instrument d on a.InstrumentPK = d.InstrumentPK and d.Status in(1,2) 
										left join Agent e on a.AgentPK = e.AgentPK and e.Status in(1,2) 
                                        order by AumForBudgetBegBalancePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AumForBudgetBegBalance.Add(setAumForBudgetBegBalance(dr));
                                }
                            }
                            return L_AumForBudgetBegBalance;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AumForBudgetBegBalance_Add(AumForBudgetBegBalance _AumForBudgetBegBalance, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(AumForBudgetBegBalancePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From AumForBudgetBegBalance";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AumForBudgetBegBalance.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(AumForBudgetBegBalancePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From AumForBudgetBegBalance";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ReportPeriodPK", _AumForBudgetBegBalance.ReportPeriodPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _AumForBudgetBegBalance.PeriodPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _AumForBudgetBegBalance.InstrumentPK);
                        cmd.Parameters.AddWithValue("@AUM", _AumForBudgetBegBalance.AUM);
                        cmd.Parameters.AddWithValue("@MFeeAmount", _AumForBudgetBegBalance.MFeeAmount);
                        cmd.Parameters.AddWithValue("@Date", _AumForBudgetBegBalance.Date);
                        cmd.Parameters.AddWithValue("@AgentPK", _AumForBudgetBegBalance.AgentPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AumForBudgetBegBalance.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AumForBudgetBegBalance");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int AumForBudgetBegBalance_Update(AumForBudgetBegBalance _AumForBudgetBegBalance, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_AumForBudgetBegBalance.AumForBudgetBegBalancePK, _AumForBudgetBegBalance.HistoryPK, "AumForBudgetBegBalance");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AumForBudgetBegBalance set status=2, Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,InstrumentPK=@InstrumentPK,AUM=@AUM,AgentPK=@AgentPK,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where AumForBudgetBegBalancePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AumForBudgetBegBalance.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                            cmd.Parameters.AddWithValue("@Notes", _AumForBudgetBegBalance.Notes);
                            cmd.Parameters.AddWithValue("@ReportPeriodPK", _AumForBudgetBegBalance.ReportPeriodPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _AumForBudgetBegBalance.PeriodPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _AumForBudgetBegBalance.InstrumentPK);
                            cmd.Parameters.AddWithValue("@AUM", _AumForBudgetBegBalance.AUM);
                            cmd.Parameters.AddWithValue("@MFeeAmount", _AumForBudgetBegBalance.MFeeAmount);
                            cmd.Parameters.AddWithValue("@Date", _AumForBudgetBegBalance.Date);
                            cmd.Parameters.AddWithValue("@AgentPK", _AumForBudgetBegBalance.AgentPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AumForBudgetBegBalance.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AumForBudgetBegBalance.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AumForBudgetBegBalance set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where AumForBudgetBegBalancePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AumForBudgetBegBalance.EntryUsersID);
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
                                cmd.CommandText = "Update AumForBudgetBegBalance set  Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,InstrumentPK=@InstrumentPK,AUM=@AUM,AgentPK=@AgentPK,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where AumForBudgetBegBalancePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AumForBudgetBegBalance.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                                cmd.Parameters.AddWithValue("@Notes", _AumForBudgetBegBalance.Notes);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _AumForBudgetBegBalance.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _AumForBudgetBegBalance.PeriodPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _AumForBudgetBegBalance.InstrumentPK);
                                cmd.Parameters.AddWithValue("@AUM", _AumForBudgetBegBalance.AUM);
                                cmd.Parameters.AddWithValue("@MFeeAmount", _AumForBudgetBegBalance.MFeeAmount);
                                cmd.Parameters.AddWithValue("@Date", _AumForBudgetBegBalance.Date);
                                cmd.Parameters.AddWithValue("@AgentPK", _AumForBudgetBegBalance.AgentPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AumForBudgetBegBalance.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_AumForBudgetBegBalance.AumForBudgetBegBalancePK, "AumForBudgetBegBalance");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AumForBudgetBegBalance where AumForBudgetBegBalancePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AumForBudgetBegBalance.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _AumForBudgetBegBalance.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _AumForBudgetBegBalance.PeriodPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _AumForBudgetBegBalance.InstrumentPK);
                                cmd.Parameters.AddWithValue("@AUM", _AumForBudgetBegBalance.AUM);
                                cmd.Parameters.AddWithValue("@MFeeAmount", _AumForBudgetBegBalance.MFeeAmount);
                                cmd.Parameters.AddWithValue("@Date", _AumForBudgetBegBalance.Date);
                                cmd.Parameters.AddWithValue("@AgentPK", _AumForBudgetBegBalance.AgentPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AumForBudgetBegBalance.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AumForBudgetBegBalance set status= 4,Notes=@Notes," +
                                    "lastupdate=@lastupdate where AumForBudgetBegBalancePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _AumForBudgetBegBalance.Notes);
                                cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AumForBudgetBegBalance.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public void AumForBudgetBegBalance_Approved(AumForBudgetBegBalance _AumForBudgetBegBalance)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AumForBudgetBegBalance set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where AumForBudgetBegBalancePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AumForBudgetBegBalance.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AumForBudgetBegBalance.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AumForBudgetBegBalance set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where AumForBudgetBegBalancePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AumForBudgetBegBalance.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void AumForBudgetBegBalance_Reject(AumForBudgetBegBalance _AumForBudgetBegBalance)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AumForBudgetBegBalance set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where AumForBudgetBegBalancePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AumForBudgetBegBalance.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AumForBudgetBegBalance.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AumForBudgetBegBalance set status= 2,lastupdate=@lastupdate where AumForBudgetBegBalancePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void AumForBudgetBegBalance_Void(AumForBudgetBegBalance _AumForBudgetBegBalance)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AumForBudgetBegBalance set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where AumForBudgetBegBalancePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AumForBudgetBegBalance.AumForBudgetBegBalancePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AumForBudgetBegBalance.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AumForBudgetBegBalance.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )


        private DataTable CreateDataTableFromAumForBudgetBegBalanceTempExcelFile(string _path)
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
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AgentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AUM";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MFeeAmount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ReportPeriod";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 2;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        while (i <= end.Row)
                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["ReportPeriod"] = "";
                            else
                                dr["ReportPeriod"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["PeriodID"] = "";
                            else
                                dr["PeriodID"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["Date"] = "";
                            else
                                dr["Date"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null)
                                dr["InstrumentID"] = "";
                            else
                                dr["InstrumentID"] = worksheet.Cells[i, 4].Value.ToString();

                            if (worksheet.Cells[i, 5].Value == null)
                                dr["AgentID"] = "";
                            else
                                dr["AgentID"] = worksheet.Cells[i, 5].Value.ToString();

                            if (worksheet.Cells[i, 6].Value == null)
                                dr["AUM"] = 0;
                            else
                                dr["AUM"] = Convert.ToDecimal(worksheet.Cells[i, 6].Value).ToString();

                            if (worksheet.Cells[i, 7].Value == null)
                                dr["MFeeAmount"] = 0;
                            else
                                dr["MFeeAmount"] = Convert.ToDecimal(worksheet.Cells[i, 7].Value).ToString();


                            if (worksheet.Cells[i, 1].Value != null || worksheet.Cells[i, 2].Value != null || worksheet.Cells[i, 3].Value != null || worksheet.Cells[i, 4].Value != null || worksheet.Cells[i, 5].Value != null || worksheet.Cells[i, 6].Value != null || worksheet.Cells[i, 7].Value != null) { dt.Rows.Add(dr); }
                            i++;

                        }

                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string AumForBudgetBegBalanceImport(string _fileSource, string _userID)
        {
            string _msg = string.Empty;
            DateTime _now = DateTime.Now;
            try
            {
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table dbo.AumForBudgetBegBalancetemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.AumForBudgetBegBalancetemp";
                    bulkCopy.WriteToServer(CreateDataTableFromAumForBudgetBegBalanceTempExcelFile(_fileSource));
                }

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        // logic Checks
                        // 1. Instrument sudah ada atau belum di investment sesuai valudate
                        // 2. instrument sudah ada atau belum di master instrument
                        // 3. check enddaytrails fund portfolio di hari itu uda ada apa belum
                        // 4. check available cash
                        // 5. check price exposure
                        // 6. 
                        cmd2.CommandTimeout = 0;
                        cmd2.CommandText =
                           @"  
                                  Declare @InstrumentPK int
                                    Declare @AgentPK int
                                    Declare @PeriodPK int
                                    Declare @AUM numeric(32,9)
                                    Declare @AumForBudgetBegBalancePK int
                                    declare @MFeeAmount numeric(32,9)
                                    declare @date date
                                    declare @ReportPeriodPK int

                                    Declare A Cursor For
                                        select Instrumentpk,agentpk,d.PeriodPK,AUM,MFeeAmount,Date,ab.PeriodPK from AumForBudgetBegBalanceTemp A
	                                    left join Instrument b on a.InstrumentID = b.ID and B.status = 2
	                                    left join Agent c on a.AgentID = c.Name and c.status = 2
	                                    left join Period d on a.PeriodID = d.ID and d.status = 2
                                        left join Period ab on a.ReportPeriod = ab.ID and AB.status in(1,2)
                                    Open A
                                    Fetch next From A
                                    into @InstrumentPK,@AgentPK,@PeriodPK, @AUM,@MFeeAmount,@date,@ReportPeriodPK
                                    While @@Fetch_status = 0
                                    BEGIN


                                    IF EXISTS (select distinct * from AumForBudgetBegBalance where Status in (1,2) and Instrumentpk = @InstrumentPK and AgentPK = @AgentPK and PeriodPK = @PeriodPK and ReportPeriodPK = @ReportPeriodPK) 
                                    BEGIN

		                                    update AumForBudgetBegBalance set Status = 3
		                                    where Instrumentpk = @InstrumentPK and AgentPK = @AgentPK and PeriodPK = @PeriodPK and ReportPeriodPK = @ReportPeriodPK

                                            Select @AumForBudgetBegBalancePK = max(AumForBudgetBegBalancePK) + 1 from AumForBudgetBegBalance
		                                    set @AumForBudgetBegBalancePK = isnull(@AumForBudgetBegBalancePK,1)

		                                    insert into 
		                                    [dbo].[AumForBudgetBegBalance]
		                                    ([AumForBudgetBegBalancePK]
		                                    ,[HistoryPK]
		                                    ,[Status]                                        
		                                    ,[Instrumentpk]  
		                                    ,[Agentpk]  
		                                    ,[PeriodPK]
		                                    ,[AUM] 
		                                    ,[MFeeAmount]
		                                    ,Date
		                                    ,[EntryTime]  
		                                    ,[EntryUsersID]  
		                                    ,[ApprovedTime]  
		                                    ,[ApprovedUsersID]  
		                                    ,[LastUpdate] 
                                            ,[ReportPeriodPK]
		                                    )

		                                    select @AumForBudgetBegBalancePK,1,2,@InstrumentPK InstrumentPK,@AgentPK AgentPK,@PeriodPK PeriodPK,@AUM AUM,@MFeeAmount MFeeAmount,@Date,@LastUpdate,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@ReportPeriodPK ReportPeriodPK


                                    END
                                    ELSE
                                    BEGIN

	                                    Select @AumForBudgetBegBalancePK = max(AumForBudgetBegBalancePK) + 1 from AumForBudgetBegBalance
	                                    set @AumForBudgetBegBalancePK = isnull(@AumForBudgetBegBalancePK,1)

	                                    insert into 
	                                    [dbo].[AumForBudgetBegBalance]
	                                    ([AumForBudgetBegBalancePK]
	                                    ,[HistoryPK]
	                                    ,[Status]                                        
	                                    ,[Instrumentpk]  
	                                    ,[Agentpk]  
	                                    ,[PeriodPK]
	                                    ,[AUM]
	                                    ,[MFeeAmount]
		                                    ,Date
	                                    ,[EntryTime]  
	                                    ,[EntryUsersID]  
	                                    ,[ApprovedTime]  
	                                    ,[ApprovedUsersID]  
	                                    ,[LastUpdate]   
                                        ,[ReportPeriodPK]
	                                    )

	                                    select @AumForBudgetBegBalancePK,1,2,@InstrumentPK InstrumentPK,@AgentPK AgentPK,@PeriodPK PeriodPK,@AUM AUM,@MFeeAmount MFeeAmount,@Date,@LastUpdate,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@ReportPeriodPK ReportPeriodPK

                                 
                                    END

                                    fetch next From A into @InstrumentPK,@AgentPK,@PeriodPK,@AUM,@MFeeAmount,@date,@ReportPeriodPK
                                    end
                                    Close A
                                    Deallocate A
                                        ";
                        cmd2.Parameters.AddWithValue("@UsersID", _userID);
                        cmd2.Parameters.AddWithValue("@LastUpdate", _now);
                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                        {
                            if (!dr2.HasRows)
                            {
                                _msg = "Import Aum For Budget BegBalance Canceled, import data not found!";
                            }
                            else
                            {
                                dr2.Read();
                                _msg = Convert.ToString(dr2["ResultMsg"]);
                            }
                        }
                    }
                }

                return _msg;
            }
            catch (Exception err)
            {
                return "Import Aum For Budget BegBalance Error : " + err.Message.ToString();
                throw err;
            }
        }




    }
}