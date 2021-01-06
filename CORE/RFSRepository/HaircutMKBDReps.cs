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
using SucorInvest.Connect;

namespace RFSRepository
{
    public class HaircutMKBDReps
    {
        Host _host = new Host();

        string _insertCommand = "INSERT INTO [dbo].[HaircutMKBD] " +
                           "([HaircutMKBDPK],[HistoryPK],[Status],[Date],[InstrumentPK],[Haircut],";
        string _paramaterCommand = "@Date,@InstrumentPK,@Haircut,";


        //2
        private HaircutMKBD setHaircutMKBD(SqlDataReader dr)
        {
            HaircutMKBD M_HaircutMKBD = new HaircutMKBD();
            M_HaircutMKBD.HaircutMKBDPK = Convert.ToInt32(dr["HaircutMKBDPK"]);
            M_HaircutMKBD.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_HaircutMKBD.Selected = Convert.ToBoolean(dr["Selected"]);
            M_HaircutMKBD.Status = Convert.ToInt32(dr["Status"]);
            M_HaircutMKBD.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_HaircutMKBD.Notes = Convert.ToString(dr["Notes"]);
            M_HaircutMKBD.Date = Convert.ToString(dr["Date"]);
            M_HaircutMKBD.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_HaircutMKBD.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_HaircutMKBD.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_HaircutMKBD.Haircut = Convert.ToDecimal(dr["Haircut"]);
            M_HaircutMKBD.EntryUsersID = dr["EntryUsersID"].ToString();
            M_HaircutMKBD.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_HaircutMKBD.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_HaircutMKBD.VoidUsersID = dr["VoidUsersID"].ToString();
            M_HaircutMKBD.EntryTime = dr["EntryTime"].ToString();
            M_HaircutMKBD.UpdateTime = dr["UpdateTime"].ToString();
            M_HaircutMKBD.ApprovedTime = dr["ApprovedTime"].ToString();
            M_HaircutMKBD.VoidTime = dr["VoidTime"].ToString();
            M_HaircutMKBD.DBUserID = dr["DBUserID"].ToString();
            M_HaircutMKBD.DBTerminalID = dr["DBTerminalID"].ToString();
            M_HaircutMKBD.LastUpdate = dr["LastUpdate"].ToString();
            return M_HaircutMKBD;
        }

        public List<HaircutMKBD> HaircutMKBD_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<HaircutMKBD> L_HaircutMKBD = new List<HaircutMKBD>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                "Select case when cp.Status=1 then 'PENDING' else case when cp.Status = 2 then 'APPROVED' else case when cp.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,I.ID InstrumentID, I.Name InstrumentName, CP.* from HaircutMKBD CP left join " +
                                "Instrument I on CP.InstrumentPK = I.InstrumentPK and I.status = 2 " +
                                "where CP.status = @status and Date between @DateFrom and @DateTo " +
                                "order by cp.HaircutMKBDPK, cp.Date";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                "Select case when cp.Status=1 then 'PENDING' else case when cp.Status = 2 then 'APPROVED' else case when cp.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,I.ID InstrumentID, I.Name InstrumentName, CP.* from HaircutMKBD CP left join " +
                                "Instrument I on CP.InstrumentPK = I.InstrumentPK and I.status = 2 " +
                                "where Date between @DateFrom and @DateTo " +
                                "order by cp.HaircutMKBDPK, cp.Date";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_HaircutMKBD.Add(setHaircutMKBD(dr));
                                }
                            }
                            return L_HaircutMKBD;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int HaircutMKBD_Add(HaircutMKBD _HaircutMKBD, bool _havePrivillege)
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
                                 "Select isnull(max(HaircutMKBDPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From HaircutMKBD";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _HaircutMKBD.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(HaircutMKBDPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From HaircutMKBD";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _HaircutMKBD.Date);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _HaircutMKBD.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Haircut", _HaircutMKBD.Haircut);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _HaircutMKBD.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "HaircutMKBD");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int HaircutMKBD_Update(HaircutMKBD _HaircutMKBD, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_HaircutMKBD.HaircutMKBDPK, _HaircutMKBD.HistoryPK, "HaircutMKBD");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update HaircutMKBD set status=2,Notes=@Notes,Date=@Date,InstrumentPK=@InstrumentPK,Haircut=@Haircut, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where HaircutMKBDPK = @PK and historyPK = @HistoryPK ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _HaircutMKBD.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                            cmd.Parameters.AddWithValue("@Date", _HaircutMKBD.Date);
                            cmd.Parameters.AddWithValue("@Notes", _HaircutMKBD.Notes);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _HaircutMKBD.InstrumentPK);
                            cmd.Parameters.AddWithValue("@Haircut", _HaircutMKBD.Haircut);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _HaircutMKBD.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _HaircutMKBD.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update HaircutMKBD set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where HaircutMKBDPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _HaircutMKBD.EntryUsersID);
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
                                cmd.CommandText = "Update HaircutMKBD set Notes=@Notes,Date=@Date,InstrumentPK=@InstrumentPK,Haircut=@Haircut, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where HaircutMKBDPK = @PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _HaircutMKBD.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                                cmd.Parameters.AddWithValue("@Date", _HaircutMKBD.Date);
                                cmd.Parameters.AddWithValue("@Notes", _HaircutMKBD.Notes);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _HaircutMKBD.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Haircut", _HaircutMKBD.Haircut);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _HaircutMKBD.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_HaircutMKBD.HaircutMKBDPK, "HaircutMKBD");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From HaircutMKBD where HaircutMKBDPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _HaircutMKBD.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _HaircutMKBD.Date);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _HaircutMKBD.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Haircut", _HaircutMKBD.Haircut);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _HaircutMKBD.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update HaircutMKBD set status= 4,Notes=@Notes," +
                                "LastUpdate=@LastUpdate where HaircutMKBDPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _HaircutMKBD.Notes);
                                cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _HaircutMKBD.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

        public void HaircutMKBD_Approved(HaircutMKBD _HaircutMKBD)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update HaircutMKBD set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where HaircutMKBDPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                        cmd.Parameters.AddWithValue("@historyPK", _HaircutMKBD.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _HaircutMKBD.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update HaircutMKBD set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where HaircutMKBDPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _HaircutMKBD.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void HaircutMKBD_Reject(HaircutMKBD _HaircutMKBD)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update HaircutMKBD set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where HaircutMKBDPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                        cmd.Parameters.AddWithValue("@historyPK", _HaircutMKBD.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _HaircutMKBD.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update HaircutMKBD set status= 2,LastUpdate=@LastUpdate where HaircutMKBDPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void HaircutMKBD_Void(HaircutMKBD _HaircutMKBD)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update HaircutMKBD set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where HaircutMKBDPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _HaircutMKBD.HaircutMKBDPK);
                        cmd.Parameters.AddWithValue("@historyPK", _HaircutMKBD.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _HaircutMKBD.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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


        public void HaircutMKBD_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                 " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                 "Select @Time,@PermissionID,'HaircutMKBD',HaircutMKBDPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from HaircutMKBD where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                 "\n update HaircutMKBD set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time  where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 \n " +
                                 "Update HaircutMKBD set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1 " +
                                 " " +
                                 "";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void HaircutMKBD_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'HaircutMKBD',HaircutMKBDPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from HaircutMKBD where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "\n update HaircutMKBD set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "Update HaircutMKBD set status= 2  where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1" +
                                          " " +
                                          "";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void HaircutMKBD_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'HaircutMKBD',HaircutMKBDPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from HaircutMKBD where Date between @DateFrom and @DateTo and Status = 2  and Selected  = 1 " +
                                          "\n update HaircutMKBD set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where Date between @DateFrom and @DateTo and Status = 2 and Selected  = 1 " +
                                          " " +
                                          "";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Validate_ApproveHaircutMKBD(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Not Exists(select * From HaircutMKBD where Status = 1 and Date between @ValueDateFrom and @ValueDateTo and (UpdateUsersID is not null or UpdateUsersID <> '')) " +
                        " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string ImportHaircutMKBD(string _fileSource, string _userID)
        {
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table TempHaircutMKBD";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.TempHaircutMKBD";
                bulkCopy.WriteToServer(CreateDataTableFromHaircutMKBDExcelFile(_fileSource));
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
                            declare @Date datetime
                            select @Date = date from TempHaircutMKBD
                            update HaircutMKBD set status = 3 where date = @Date
                            Declare @HaircutMKBDPK bigint
                            Select @HaircutMKBDPK  = isnull(max(HaircutMKBDPK),0) from HaircutMKBD   
                             insert into HaircutMKBD (HaircutMKBDPK, HistoryPK, Status, Date, Haircut, InstrumentPK, EntryUsersID, EntryTime, LastUpdate)
                            select Row_number() over(order by InstrumentPK) + @HaircutMKBDPK, 1, 1, A.Date, A.HCMKBD, B.InstrumentPK, @EntryUsersID, @Lastupdate, @Lastupdate from TempHaircutMKBD A
                            left join Instrument B on A.KodeEfek = B.ID and B.status in (1,2)
                            Where A.KodeEfek in
                            (
	                            Select ID From Instrument where status in (1,2)
                            )
                        ";
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Lastupdate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return "Import HaircutMKBD Success";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromHaircutMKBDExcelFile(string _path)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeEfek";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaSaham";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "HCMKBD";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Keterangan";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(Tools.TxtFilePath + _path);
            string input;

            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                dr = dt.NewRow();
                dr["Date"] = s[0];
                dr["KodeEfek"] = s[1];
                dr["NamaSaham"] = s[2];
                dr["HCMKBD"] = s[3];
                dr["Keterangan"] = s[4];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }


    }
}