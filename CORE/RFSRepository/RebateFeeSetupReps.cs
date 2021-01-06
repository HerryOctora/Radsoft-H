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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;


namespace RFSRepository
{
    public class RebateFeeSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[RebateFeeSetup] " +
                            "([RebateFeeSetupPK],[HistoryPK],[Status],[ValueDate],[FundPK],[FundClientPK],[RebateFeePercent],";
        string _paramaterCommand = "@ValueDate,@FundPK,@FundClientPK,@RebateFeePercent,";

        //2
        private RebateFeeSetup setRebateFeeSetup(SqlDataReader dr)
        {
            RebateFeeSetup M_RebateFeeSetup = new RebateFeeSetup();
            M_RebateFeeSetup.RebateFeeSetupPK = Convert.ToInt32(dr["RebateFeeSetupPK"]);
            M_RebateFeeSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RebateFeeSetup.Selected = Convert.ToBoolean(dr["Selected"]);
            M_RebateFeeSetup.Status = Convert.ToInt32(dr["Status"]);
            M_RebateFeeSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RebateFeeSetup.Notes = Convert.ToString(dr["Notes"]);
            M_RebateFeeSetup.ValueDate = dr["ValueDate"].ToString();
            M_RebateFeeSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_RebateFeeSetup.FundID = dr["FundID"].ToString();
            M_RebateFeeSetup.FundName = dr["FundName"].ToString();
            M_RebateFeeSetup.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_RebateFeeSetup.FundClientID = dr["FundClientID"].ToString();
            M_RebateFeeSetup.FundClientName = dr["FundClientName"].ToString();
            M_RebateFeeSetup.RebateFeePercent = Convert.ToDecimal(dr["RebateFeePercent"]);
            M_RebateFeeSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RebateFeeSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RebateFeeSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RebateFeeSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RebateFeeSetup.EntryTime = dr["EntryTime"].ToString();
            M_RebateFeeSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_RebateFeeSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RebateFeeSetup.VoidTime = dr["VoidTime"].ToString();
            M_RebateFeeSetup.DBUserID = dr["DBUserID"].ToString();
            M_RebateFeeSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RebateFeeSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_RebateFeeSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RebateFeeSetup;
        }


       
        public List<RebateFeeSetup> RebateFeeSetup_SelectDataByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RebateFeeSetup> L_RebateFeeSetup = new List<RebateFeeSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,
                            B.ID FundID, B.Name FundName, C.ID FundClientID, C.Name FundClientName,* from RebateFeeSetup A 
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2 
                            where ValueDate between @DateFrom and @DateTo and A.status = @Status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,
                            B.ID FundID, B.Name FundName, C.ID FundClientID, C.Name FundClientName,* from RebateFeeSetup A 
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2 
                            where ValueDate between @DateFrom and @DateTo and A.status = @Status";
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RebateFeeSetup.Add(setRebateFeeSetup(dr));
                                }
                            }
                            return L_RebateFeeSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int RebateFeeSetup_Add(RebateFeeSetup _RebateFeeSetup, bool _havePrivillege)
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
                                 "Select isnull(max(RebateFeeSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From RebateFeeSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RebateFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(RebateFeeSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastUpdate From RebateFeeSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ValueDate", _RebateFeeSetup.ValueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _RebateFeeSetup.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _RebateFeeSetup.FundClientPK);
                        cmd.Parameters.AddWithValue("@RebateFeePercent", _RebateFeeSetup.RebateFeePercent);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RebateFeeSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "RebateFeeSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        public int RebateFeeSetup_Update(RebateFeeSetup _RebateFeeSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _dateTimeNow = DateTime.Now;
                int status = _host.Get_Status(_RebateFeeSetup.RebateFeeSetupPK, _RebateFeeSetup.HistoryPK, "RebateFeeSetup");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RebateFeeSetup set status=2,Notes=@Notes,ValueDate=@ValueDate,FundPK=@FundPK,FundClientPK=@FundClientPK,RebateFeePercent=@RebateFeePercent," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where RebateFeeSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _RebateFeeSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _RebateFeeSetup.Notes);
                            cmd.Parameters.AddWithValue("@ValueDate", _RebateFeeSetup.ValueDate);
                            cmd.Parameters.AddWithValue("@FundPK", _RebateFeeSetup.FundPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _RebateFeeSetup.FundClientPK);
                            cmd.Parameters.AddWithValue("@RebateFeePercent", _RebateFeeSetup.RebateFeePercent);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _RebateFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RebateFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RebateFeeSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where RebateFeeSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _RebateFeeSetup.EntryUsersID);
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
                                cmd.CommandText = "Update RebateFeeSetup set Notes=@Notes,ValueDate=@ValueDate,FundPK=@FundPK,FundClientPK=@FundClientPK,RebateFeePercent=@RebateFeePercent," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where RebateFeeSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _RebateFeeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _RebateFeeSetup.Notes);
                                cmd.Parameters.AddWithValue("@ValueDate", _RebateFeeSetup.ValueDate);
                                cmd.Parameters.AddWithValue("@FundPK", _RebateFeeSetup.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _RebateFeeSetup.FundClientPK);
                                cmd.Parameters.AddWithValue("@RebateFeePercent", _RebateFeeSetup.RebateFeePercent);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RebateFeeSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_RebateFeeSetup.RebateFeeSetupPK, "RebateFeeSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RebateFeeSetup where RebateFeeSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RebateFeeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _RebateFeeSetup.ValueDate);
                                cmd.Parameters.AddWithValue("@FundPK", _RebateFeeSetup.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _RebateFeeSetup.FundClientPK);
                                cmd.Parameters.AddWithValue("@RebateFeePercent", _RebateFeeSetup.RebateFeePercent);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RebateFeeSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RebateFeeSetup set status= 4,Notes=@Notes," +
                                    "LastUpdate=@LastUpdate where RebateFeeSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _RebateFeeSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RebateFeeSetup.HistoryPK);
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

        public void RebateFeeSetup_Approved(RebateFeeSetup _RebateFeeSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RebateFeeSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastUpdate=@LastUpdate " +
                            "where RebateFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RebateFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RebateFeeSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RebateFeeSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where RebateFeeSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RebateFeeSetup.ApprovedUsersID);
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

        public void RebateFeeSetup_Reject(RebateFeeSetup _RebateFeeSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RebateFeeSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where RebateFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RebateFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RebateFeeSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RebateFeeSetup set status= 2,LastUpdate=@LastUpdate where RebateFeeSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
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

        public void RebateFeeSetup_Void(RebateFeeSetup _RebateFeeSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RebateFeeSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where RebateFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RebateFeeSetup.RebateFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RebateFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RebateFeeSetup.VoidUsersID);
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

       

        public void RebateFeeSetup_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'RebateFeeSetup',RebateFeeSetupPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                       
                        update RebateFeeSetup set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and RebateFeeSetupPK in ( Select RebateFeeSetupPK from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                        
                        Update RebateFeeSetup set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        where status = 4 and RebateFeeSetupPK in (Select RebateFeeSetupPK from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1)   

                        ";
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

        public void RebateFeeSetup_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                                          "Select @Time,@PermissionID,'RebateFeeSetup',RebateFeeSetupPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "\n update RebateFeeSetup set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where RebateFeeSetupPK in ( Select RebateFeeSetupPK from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) \n " +
                                          "Update RebateFeeSetup set status= 2  where RebateFeeSetupPK in (Select RebateFeeSetupPK from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) " +
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

        public void RebateFeeSetup_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                                          "Select @Time,@PermissionID,'RebateFeeSetup',RebateFeeSetupPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 2  and Selected  = 1 " +
                                          "\n update RebateFeeSetup set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where RebateFeeSetupPK in ( Select RebateFeeSetupPK from RebateFeeSetup where ValueDate between @DateFrom and @DateTo and Status = 2 and Selected  = 1 ) \n " +
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


        public List<SetRebateFee> RebateFeeSetup_GetDataRebateFee(int _pk)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SetRebateFee> L_setRebateFee = new List<SetRebateFee>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select A.Status Status, case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,isnull(A.FundPK,0) FundPK, case when A.FundPK = 0 then 'ALL' else isnull(B.Name,'') end FundName, case when isnull(A.FeeType,0) = 1 then 'Tiering' else case when isnull(A.FeeType,0) = 2
                            then 'Progresive' else case when isnull(A.FeeType,0) = 3 then 'Up Front' else case when isnull(A.FeeType,0) = 4 then 'Amortization'
                            else case when isnull(A.FeeType,0) = 5 then 'Flat'  end end end end end FeeTypeDesc, Date Date, DateAmortize DateAmortize, isnull(MiFeeAmount,0) MiFeeAmount,
                            isnull(MiFeePercent,0) MiFeePercent, isnull(RangeFrom,0) RangeFrom, isnull(RangeTo,0) RangeTo,A.* from RebateFeeSetupDetail A 
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) where A.FundPK = @PK and A.Status = 2
                               ";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_setRebateFee.Add(setRebateFee(dr));
                                }
                            }
                        }
                        return L_setRebateFee;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private SetRebateFee setRebateFee(SqlDataReader dr)
        {
            SetRebateFee M_Fund = new SetRebateFee();
            M_Fund.Status = Convert.ToInt32(dr["Status"]);
            M_Fund.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Fund.Selected = Convert.ToBoolean(dr["Selected"]);
            M_Fund.RebateFeeSetupDetailPK = Convert.ToInt32(dr["RebateFeeSetupDetailPK"]);
            M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_Fund.FundName = Convert.ToString(dr["FundName"]);
            M_Fund.Date = Convert.ToString(dr["Date"]);
            M_Fund.DateAmortize = Convert.ToString(dr["DateAmortize"]);
            M_Fund.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_Fund.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_Fund.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
            M_Fund.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
            M_Fund.MiFeeAmount = Convert.ToDecimal(dr["MiFeeAmount"]);
            M_Fund.MiFeePercent = Convert.ToDecimal(dr["MiFeePercent"]);
            return M_Fund;
        }


        public void AddRebateFee(SetRebateFee _RebateFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Insert into RebateFeeSetupDetail(RebateFeeSetupDetailPK,HistoryPK,Status,FundPK,Date,DateAmortize,RangeFrom,RangeTo,MiFeeAmount,MiFeePercent,FeeType,LastUpdate,UpdateUsersID,UpdateTime) 
                        Select isnull(max(RebateFeeSetupDetailPK),0) + 1,1,2,@FundPK,@Date,@DateAmortize,@RangeFrom,@RangeTo,@MiFeeAmount,@MiFeePercent,@FeeType,@LastUpdate,@EntryUsersID,@EntryTime from RebateFeeSetupDetail";

                        cmd.Parameters.AddWithValue("@FundPK", _RebateFee.FundPK);
                        cmd.Parameters.AddWithValue("@Date", _RebateFee.Date);
                        cmd.Parameters.AddWithValue("@DateAmortize", _RebateFee.DateAmortize);
                        cmd.Parameters.AddWithValue("@FeeType", _RebateFee.FeeType);
                        cmd.Parameters.AddWithValue("@TypeTrx", _RebateFee.TypeTrx);
                        cmd.Parameters.AddWithValue("@RangeTo", _RebateFee.RangeTo);
                        cmd.Parameters.AddWithValue("@RangeFrom", _RebateFee.RangeFrom);
                        cmd.Parameters.AddWithValue("@MiFeeAmount", _RebateFee.MiFeeAmount);
                        cmd.Parameters.AddWithValue("@MiFeePercent", _RebateFee.MiFeePercent);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RebateFee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RebateFee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
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

        public void RejectDataBySelected(string param1, string param2)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RebateFeeSetupDetail set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where Selected = 1";
                        cmd.Parameters.AddWithValue("@VoidUsersID", param1);
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

        public bool CheckHassAdd(int _pk, string _date, int _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from RebateFeeSetupDetail where FundPK = @PK and Status in (1,2) and Date = @Date and FeeType <> @Type";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Type", _type);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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