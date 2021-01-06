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
    public class MarketHolidayReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[MarketHoliday] " +
                            "([MarketHolidayPK],[HistoryPK],[Status],[MarketPK],[Date],[Description],";
        string _paramaterCommand = "@MarketPK,@Date,@Description,";

        //2
        private MarketHoliday setMarketHoliday(SqlDataReader dr)
        {
            MarketHoliday M_MarketHoliday = new MarketHoliday();
            M_MarketHoliday.MarketHolidayPK = Convert.ToInt32(dr["MarketHolidayPK"]);
            M_MarketHoliday.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MarketHoliday.Status = Convert.ToInt32(dr["Status"]);
            M_MarketHoliday.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MarketHoliday.Notes = Convert.ToString(dr["Notes"]);
            M_MarketHoliday.Date = dr["Date"].ToString();
            M_MarketHoliday.MarketPK = Convert.ToInt32(dr["MarketPK"]);
            M_MarketHoliday.MarketID = Convert.ToString(dr["MarketID"]);
            M_MarketHoliday.Description = dr["Description"].ToString();
            M_MarketHoliday.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MarketHoliday.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_MarketHoliday.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MarketHoliday.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MarketHoliday.EntryTime = dr["EntryTime"].ToString();
            M_MarketHoliday.UpdateTime = dr["UpdateTime"].ToString();
            M_MarketHoliday.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MarketHoliday.VoidTime = dr["VoidTime"].ToString();
            M_MarketHoliday.DBUserID = dr["DBUserID"].ToString();
            M_MarketHoliday.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MarketHoliday.LastUpdate = dr["LastUpdate"].ToString();
            M_MarketHoliday.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_MarketHoliday;
        }

        public List<MarketHoliday> MarketHoliday_SelectDataByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MarketHoliday> L_MarketHoliday = new List<MarketHoliday>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                "Select case when mh.Status=1 then 'PENDING' else case when mh.Status = 2 then 'APPROVED' else case when mh.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,M.ID MarketID,MH.* from MarketHoliday MH left join " +
                                "Market M on MH.MarketPK = M.MarketPK and M.status = 2 " +
                                "where MH.status = @status and Date between @DateFrom and @DateTo " +
                                "order by mh.Date";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                "Select case when mh.Status=1 then 'PENDING' else case when mh.Status = 2 then 'APPROVED' else case when mh.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,M.ID MarketID,MH.* from MarketHoliday MH left join " +
                                "Market M on MH.MarketPK = M.MarketPK and M.status = 2 where Date between @DateFrom and @DateTo " +
                                "order by mh.Date";
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MarketHoliday.Add(setMarketHoliday(dr));
                                }
                            }
                            return L_MarketHoliday;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public int MarketHoliday_Add(MarketHoliday _marketHoliday, bool _havePrivillege)
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
                                 "Select isnull(max(MarketHolidayPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from MarketHoliday";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _marketHoliday.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(MarketHolidayPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from MarketHoliday";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _marketHoliday.Date);
                        cmd.Parameters.AddWithValue("@MarketPK", _marketHoliday.MarketPK);
                        cmd.Parameters.AddWithValue("@Description", _marketHoliday.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _marketHoliday.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "MarketHoliday");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public int MarketHoliday_Update(MarketHoliday _marketHoliday, bool _havePrivillege)
        {            
           try
            {
                int _newHisPK;
                int status = _host.Get_Status(_marketHoliday.MarketHolidayPK, _marketHoliday.HistoryPK, "MarketHoliday");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MarketHoliday set status=2,Notes=@Notes,MarketPK=@MarketPK,Date=@Date,Description=@Description," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where MarketHolidayPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _marketHoliday.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                            cmd.Parameters.AddWithValue("@Notes", _marketHoliday.Notes);
                            cmd.Parameters.AddWithValue("@MarketPK", _marketHoliday.MarketPK);
                            cmd.Parameters.AddWithValue("@Date", _marketHoliday.Date);
                            cmd.Parameters.AddWithValue("@Description", _marketHoliday.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _marketHoliday.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _marketHoliday.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MarketHoliday set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MarkeHolidayPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _marketHoliday.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update MarketHoliday set Notes=@Notes,MarketPK=@MarketPK,Date=@Date,Description=@Description," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where MarketHolidayPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _marketHoliday.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                                cmd.Parameters.AddWithValue("@Notes", _marketHoliday.Notes);
                                cmd.Parameters.AddWithValue("@MarketPK", _marketHoliday.MarketPK);
                                cmd.Parameters.AddWithValue("@Date", _marketHoliday.Date);
                                cmd.Parameters.AddWithValue("@Description", _marketHoliday.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _marketHoliday.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_marketHoliday.MarketHolidayPK, "MarketHoliday");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From MarketHoliday where MarketHolidayPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _marketHoliday.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@MarketPK", _marketHoliday.MarketPK);
                                cmd.Parameters.AddWithValue("@Date", _marketHoliday.Date);
                                cmd.Parameters.AddWithValue("@Description", _marketHoliday.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _marketHoliday.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update MarketHoliday set status= 4,Notes=@Notes,"+
                                    "LastUpdate=@LastUpdate where MarketHolidayPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _marketHoliday.Notes);
                                cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _marketHoliday.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
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

        public void MarketHoliday_Approved(MarketHoliday _marketHoliday)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MarketHoliday set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where MarketHolidayPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                        cmd.Parameters.AddWithValue("@historyPK", _marketHoliday.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _marketHoliday.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MarketHoliday set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MarketHolidayPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _marketHoliday.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public void MarketHoliday_Reject(MarketHoliday _marketHoliday)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MarketHoliday set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MarketHolidayPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                        cmd.Parameters.AddWithValue("@historyPK", _marketHoliday.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _marketHoliday.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MarketHoliday set status= 2,LastUpdate=@LastUpdate where MarketHolidayPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public void MarketHoliday_Void(MarketHoliday _marketHoliday)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MarketHoliday set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MarketHolidayPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _marketHoliday.MarketHolidayPK);
                        cmd.Parameters.AddWithValue("@historyPK", _marketHoliday.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _marketHoliday.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }          
        }

        public string Generate_WorkingDays(DateTime _dateFrom, DateTime _dateto)
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

                            --DECLARE @DateFrom DATETIME
                            --DECLARE @DateTo DATETIME

                            --SET @DateFrom = '01/01/16'
                            --SET @DateTo = '12/31/21'


                            DECLARE @CounterDate DATETIME
                            SET @CounterDate = @DateFrom

                            DELETE dbo.ZDT_WorkingDays WHERE date BETWEEN @DateFrom AND @DateTo

                            WHILE @CounterDate <= @DateTo
                            BEGIN
	                            INSERT INTO dbo.ZDT_WorkingDays
	                                    ( Date ,
	                                      DT1 ,
	                                      DT2 ,
	                                      DT3 ,
	                                      DTM1 ,
	                                      DTM2 ,
	                                      DTM3,
			                              IsHoliday
	                                    )
	                            SELECT @CounterDate
	                            ,dbo.FWorkingDay(@CounterDate,1)
	                            ,dbo.FWorkingDay(@CounterDate,2)
	                            ,dbo.FWorkingDay(@CounterDate,3)
	                            ,dbo.FWorkingDay(@CounterDate,-1)
	                            ,dbo.FWorkingDay(@CounterDate,-2)
	                            ,dbo.FWorkingDay(@CounterDate,-3)
	                            ,dbo.CheckTodayIsHoliday(@CounterDate)
	                            SET @CounterDate = DATEADD(DAY,1,@CounterDate)
                            END

                            select 'Generate Working Days Success !' Result

                            ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateto);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return Convert.ToString(dr["Result"]);
                            }
                            else
                            {
                                return "";
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





    }
}