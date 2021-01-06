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
    public class BudgetVersioningReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BudgetVersioning] " +
                             @"([BudgetVersioningPK],[HistoryPK],[Status],[ReportPeriodPK],[PeriodPK],[Date],[BudgetPercent],[RevenuePercent],";
        string _paramaterCommand = @"@ReportPeriodPK,@PeriodPK,@Date,@BudgetPercent,@RevenuePercent,";

        //2
        private BudgetVersioning setBudgetVersioning(SqlDataReader dr)
        {
            BudgetVersioning M_BudgetVersioning = new BudgetVersioning();
            M_BudgetVersioning.BudgetVersioningPK = Convert.ToInt32(dr["BudgetVersioningPK"]);
            M_BudgetVersioning.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BudgetVersioning.Status = Convert.ToInt32(dr["Status"]);
            M_BudgetVersioning.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BudgetVersioning.Notes = Convert.ToString(dr["Notes"]);
            M_BudgetVersioning.ReportPeriodPK = Convert.ToInt32(dr["ReportPeriodPK"]);
            M_BudgetVersioning.ReportPeriodID = Convert.ToString(dr["ReportPeriodID"]);
            M_BudgetVersioning.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_BudgetVersioning.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_BudgetVersioning.BudgetPercent = Convert.ToDecimal(dr["BudgetPercent"]);
            M_BudgetVersioning.RevenuePercent = Convert.ToDecimal(dr["RevenuePercent"]);
            M_BudgetVersioning.Date = Convert.ToDateTime(dr["Date"]);
            M_BudgetVersioning.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BudgetVersioning.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BudgetVersioning.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BudgetVersioning.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BudgetVersioning.EntryTime = dr["EntryTime"].ToString();
            M_BudgetVersioning.UpdateTime = dr["UpdateTime"].ToString();
            M_BudgetVersioning.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BudgetVersioning.VoidTime = dr["VoidTime"].ToString();
            M_BudgetVersioning.DBUserID = dr["DBUserID"].ToString();
            M_BudgetVersioning.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BudgetVersioning.LastUpdate = dr["LastUpdate"].ToString();
            M_BudgetVersioning.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BudgetVersioning;
        }

        public List<BudgetVersioning> BudgetVersioning_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BudgetVersioning> L_BudgetVersioning = new List<BudgetVersioning>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                        isnull(b.ID,'') ReportPeriodID,ab.ID PeriodID,* from BudgetVersioning A
                                        left join Period b on a.ReportPeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.PeriodPK = ab.PeriodPK and ab.status in(1,2)
                                        where A.status = @status order by BudgetVersioningPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                        isnull(b.ID,'') ReportPeriodID,ab.ID PeriodID,* from BudgetVersioning A
                                        left join Period b on a.ReportPeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.PeriodPK = ab.PeriodPK and ab.status in(1,2)
                                        order by BudgetVersioningPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BudgetVersioning.Add(setBudgetVersioning(dr));
                                }
                            }
                            return L_BudgetVersioning;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BudgetVersioning_Add(BudgetVersioning _BudgetVersioning, bool _havePrivillege)
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
                                 "Select isnull(max(BudgetVersioningPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From BudgetVersioning";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BudgetVersioning.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(BudgetVersioningPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From BudgetVersioning";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ReportPeriodPK", _BudgetVersioning.ReportPeriodPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _BudgetVersioning.PeriodPK);
                        cmd.Parameters.AddWithValue("@BudgetPercent", _BudgetVersioning.BudgetPercent);
                        cmd.Parameters.AddWithValue("@RevenuePercent", _BudgetVersioning.RevenuePercent);
                        cmd.Parameters.AddWithValue("@Date", _BudgetVersioning.Date);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _BudgetVersioning.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BudgetVersioning");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int BudgetVersioning_Update(BudgetVersioning _BudgetVersioning, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_BudgetVersioning.BudgetVersioningPK, _BudgetVersioning.HistoryPK, "BudgetVersioning");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update BudgetVersioning set status=2, Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,BudgetPercent=@BudgetPercent,RevenuePercent=@RevenuePercent,Date=@Date,ApprovedUsersID=@ApprovedUsersID,
                                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdat
                                                where BudgetVersioningPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _BudgetVersioning.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                            cmd.Parameters.AddWithValue("@Notes", _BudgetVersioning.Notes);
                            cmd.Parameters.AddWithValue("@ReportPeriodPK", _BudgetVersioning.ReportPeriodPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetVersioning.PeriodPK);
                            cmd.Parameters.AddWithValue("@BudgetPercent", _BudgetVersioning.BudgetPercent);
                            cmd.Parameters.AddWithValue("@RevenuePercent", _BudgetVersioning.RevenuePercent);
                            cmd.Parameters.AddWithValue("@Date", _BudgetVersioning.Date);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _BudgetVersioning.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BudgetVersioning.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BudgetVersioning set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BudgetVersioningPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetVersioning.EntryUsersID);
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
                                cmd.CommandText = @"Update BudgetVersioning set Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,BudgetPercent=@BudgetPercent,RevenuePercent=@RevenuePercent,Date=@Date,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                                                    where BudgetVersioningPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _BudgetVersioning.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                                cmd.Parameters.AddWithValue("@Notes", _BudgetVersioning.Notes);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _BudgetVersioning.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetVersioning.PeriodPK);
                                cmd.Parameters.AddWithValue("@BudgetPercent", _BudgetVersioning.BudgetPercent);
                                cmd.Parameters.AddWithValue("@RevenuePercent", _BudgetVersioning.RevenuePercent);
                                cmd.Parameters.AddWithValue("@Date", _BudgetVersioning.Date);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BudgetVersioning.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_BudgetVersioning.BudgetVersioningPK, "BudgetVersioning");
                                cmd.CommandText = _insertCommand + @"[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])
                                Select @PK,@NewHistoryPK,1," + _paramaterCommand + @"EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate
                                From BudgetVersioning where BudgetVersioningPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BudgetVersioning.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _BudgetVersioning.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetVersioning.PeriodPK);
                                cmd.Parameters.AddWithValue("@BudgetPercent", _BudgetVersioning.BudgetPercent);
                                cmd.Parameters.AddWithValue("@RevenuePercent", _BudgetVersioning.RevenuePercent);
                                cmd.Parameters.AddWithValue("@Date", _BudgetVersioning.Date);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BudgetVersioning.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update BudgetVersioning set status= 4,Notes=@Notes,
                                                    lastupdate=@lastupdate where BudgetVersioningPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _BudgetVersioning.Notes);
                                cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BudgetVersioning.HistoryPK);
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

        public void BudgetVersioning_Approved(BudgetVersioning _BudgetVersioning)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update BudgetVersioning set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate
                                            where BudgetVersioningPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BudgetVersioning.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _BudgetVersioning.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BudgetVersioning set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BudgetVersioningPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetVersioning.ApprovedUsersID);
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

        public void BudgetVersioning_Reject(BudgetVersioning _BudgetVersioning)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update BudgetVersioning set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate 
                                            where BudgetVersioningPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BudgetVersioning.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetVersioning.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BudgetVersioning set status= 2,lastupdate=@lastupdate where BudgetVersioningPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
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

        public void BudgetVersioning_Void(BudgetVersioning _BudgetVersioning)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update BudgetVersioning set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate 
                                            where BudgetVersioningPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BudgetVersioning.BudgetVersioningPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BudgetVersioning.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetVersioning.VoidUsersID);
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



    }
}