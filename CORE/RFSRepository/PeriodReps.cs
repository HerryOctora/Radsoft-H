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


namespace RFSRepository
{
    public class PeriodReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [dbo].[Period] " +
                            "([PeriodPK],[HistoryPK],[Status],[ID],[DateFrom],[DateTo],[Description],";
        string _paramaterCommand = "@ID,@DateFrom,@DateTo,@Description,";

        //2
        private Period setPeriod(SqlDataReader dr)
        {
            Period M_Period = new Period();
            M_Period.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_Period.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Period.Status = Convert.ToInt32(dr["Status"]);
            M_Period.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Period.Notes = Convert.ToString(dr["Notes"]);
            M_Period.ID = dr["ID"].ToString();
            M_Period.DateFrom = dr["DateFrom"].ToString();
            M_Period.DateTo = dr["DateTo"].ToString();
            M_Period.Description = dr["Description"].ToString();
            M_Period.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Period.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Period.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Period.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Period.EntryTime = dr["EntryTime"].ToString();
            M_Period.UpdateTime = dr["UpdateTime"].ToString();
            M_Period.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Period.VoidTime = dr["VoidTime"].ToString();
            M_Period.DBUserID = dr["DBUserID"].ToString();
            M_Period.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Period.LastUpdate = dr["LastUpdate"].ToString();
            M_Period.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);

            return M_Period;
        }

        public List<Period> Period_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Period> L_Period = new List<Period>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Period where status = @status order by PeriodPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END  StatusDesc,* from Period order by PeriodPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Period.Add(setPeriod(dr));
                                }
                            }
                            return L_Period;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Period_Add(Period _period, bool _havePrivillege)
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
                                  "Select isnull(max(PeriodPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Period";
                             cmd.Parameters.AddWithValue("@ApprovedUsersID", _period.EntryUsersID);
                             cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
   
                         }
                         else
                         {
                             cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                 "Select isnull(max(PeriodPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Period";
                         }
                         cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                         cmd.Parameters.AddWithValue("@ID", _period.ID);
                         cmd.Parameters.AddWithValue("@DateFrom", _period.DateFrom);
                         cmd.Parameters.AddWithValue("@DateTo", _period.DateTo);
                         cmd.Parameters.AddWithValue("@Description", _period.Description);
                         cmd.Parameters.AddWithValue("@EntryUsersID", _period.EntryUsersID);
                         cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                         cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                         cmd.ExecuteNonQuery();

                         return _host.Get_LastPKByLastUpate(_datetimeNow, "Period");
                     }
                 }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Period_Update(Period _period, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_period.PeriodPK, _period.HistoryPK, "period");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Period set status=2, Notes=@Notes,ID=@ID,DateFrom=@DateFrom,DateTo=@DateTo,Description=@Description," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where PeriodPK = @PK and historyPK = @HistoryPK ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _period.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                            cmd.Parameters.AddWithValue("@Notes", _period.Notes);
                            cmd.Parameters.AddWithValue("@ID", _period.ID);
                            cmd.Parameters.AddWithValue("@DateFrom", _period.DateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _period.DateTo);
                            cmd.Parameters.AddWithValue("@Description", _period.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _period.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _period.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Period set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where PeriodPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _period.EntryUsersID);
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
                                cmd.CommandText = "Update Period set Notes=@Notes,ID=@ID,DateFrom=@DateFrom,DateTo=@DateTo,Description=@Description," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where PeriodPK = @PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _period.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                                cmd.Parameters.AddWithValue("@Notes", _period.Notes);
                                cmd.Parameters.AddWithValue("@ID", _period.ID);
                                cmd.Parameters.AddWithValue("@DateFrom", _period.DateFrom);
                                cmd.Parameters.AddWithValue("@DateTo", _period.DateTo);
                                cmd.Parameters.AddWithValue("@Description", _period.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _period.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_period.PeriodPK, "Period");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Period where PeriodPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _period.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _period.ID);
                                cmd.Parameters.AddWithValue("@DateFrom", _period.DateFrom);
                                cmd.Parameters.AddWithValue("@DateTo", _period.DateTo);
                                cmd.Parameters.AddWithValue("@Description", _period.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _period.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Period set status= 4,Notes=@Notes,"+
                                    "lastupdate=@lastupdate where PeriodPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _period.Notes);
                                cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _period.HistoryPK);
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

        public void Period_Approved(Period _period)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Period set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where PeriodPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                        cmd.Parameters.AddWithValue("@historyPK", _period.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _period.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Period set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where PeriodPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _period.ApprovedUsersID);
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

        public void Period_Reject(Period _period)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Period set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where PeriodPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                        cmd.Parameters.AddWithValue("@historyPK", _period.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _period.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Period set status= 2,lastupdate=@lastupdate where PeriodPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
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

        public void Period_Void(Period _period)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Period set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where PeriodPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _period.PeriodPK);
                        cmd.Parameters.AddWithValue("@historyPK", _period.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _period.VoidUsersID);
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

        public int Period_GetPKByID(string _id)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  PeriodPK FROM [Period]  where status = 2 and ID = @ID ";
                        cmd.Parameters.AddWithValue("@ID", _id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["PeriodPK"]);
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<PeriodCombo> Period_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<PeriodCombo> L_Period = new List<PeriodCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  PeriodPK,ID, Description FROM [Period]  where status = 2 order by ID Desc";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    PeriodCombo M_Period = new PeriodCombo();
                                    M_Period.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
                                    M_Period.ID = Convert.ToString(dr["ID"]);
                                    M_Period.Description = Convert.ToString(dr["Description"]);
                                    L_Period.Add(M_Period);
                                }

                            }
                            return L_Period;
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