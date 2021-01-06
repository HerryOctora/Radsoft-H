using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using System.Data;

namespace RFSRepository
{
    public class ActivityReps
    {
        private Activity setActivity(SqlDataReader dr)
        {
            Host _host = new Host();

            Activity M_Activity = new Activity();
            M_Activity.ActivityPK = Convert.ToInt32(dr["ActivityPK"]);
            M_Activity.Status = Convert.ToBoolean(dr["Status"]);
            M_Activity.Time = Convert.ToString(dr["Time"]);
            M_Activity.PermissionID = Convert.ToString(dr["PermissionID"]);
            M_Activity.ObjectTable = Convert.ToString(dr["ObjectTable"]);
            M_Activity.ObjectTablePK = Convert.ToInt32(dr["ObjectTablePK"]);
            M_Activity.Message = Convert.ToString(dr["Message"]);
            M_Activity.StackTrace = Convert.ToString(dr["StackTrace"]);
            M_Activity.Source = Convert.ToString(dr["Source"]);
            M_Activity.OldValue = Convert.ToString(dr["OldValue"]);
            M_Activity.NewValue = Convert.ToString(dr["NewValue"]);
            M_Activity.UsersID = Convert.ToString(dr["UsersID"]);
            M_Activity.IPAddress = Convert.ToString(dr["IPAddress"]);
            M_Activity.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_Activity.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_Activity.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_Activity.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Activity;
        }


        public void Activity_Insert(DateTime _time, string _permissionID, bool _status, string _message,
            string _stackTrace, string _source, string _usersID )
        {
            try
            {

                if(string.IsNullOrEmpty(_permissionID) || _permissionID == "")
                {
                    _permissionID = "NoSessionOrError_";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO [dbo].[Activity]" +
                            " ([Time],[PermissionID],[ObjectTable],[Status],[Message],[StackTrace],[Source],[UsersID],[IPAddress])" +
                            " select @Time,@PermissionID,left(@PermissionID,charindex('_', @PermissionID)-1),@Status,@Message,@StackTrace,@Source,@UsersID, IPAddress From users where ID = @UsersID and status = 2 ";
                        cmd.Parameters.AddWithValue("@Time", _time.ToString("MM/dd/yyyy HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@Message", Tools.RemoveApostrophe(_message));
                        cmd.Parameters.AddWithValue("@StackTrace", Tools.RemoveApostrophe(_stackTrace));
                        cmd.Parameters.AddWithValue("@Source", Tools.RemoveApostrophe(_source));
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Activity_ActionInsert(DateTime _time, string _permissionID, bool _status, string _message,
        string _stackTrace, string _source, string _usersID, int _objectTablePK)
        {
            try
            {

                if (string.IsNullOrEmpty(_permissionID) || _permissionID == "")
                {
                    _permissionID = "NoSessionOrError_";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO [dbo].[Activity]" +
                            " ([Time],[PermissionID],[ObjectTable],[ObjectTablePK],[Status],[Message],[StackTrace],[Source],[UsersID],[IPAddress])" +
                            " select @Time,@PermissionID,left(@PermissionID,charindex('_', @PermissionID)-1),@ObjectTablePK,@Status,@Message,@StackTrace,@Source,@UsersID, IPAddress From users where ID = @UsersID and status = 2 ";
                        cmd.Parameters.AddWithValue("@Time", _time.ToString("MM/dd/yyyy HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@Message", Tools.RemoveApostrophe(_message));
                        cmd.Parameters.AddWithValue("@StackTrace", Tools.RemoveApostrophe(_stackTrace));
                        cmd.Parameters.AddWithValue("@Source", Tools.RemoveApostrophe(_source));
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ObjectTablePK", _objectTablePK);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Activity_LogInsert(DateTime _time, string _permissionID, bool _status, string _message,
        string _stackTrace, string _source, string _usersID, int _pk, int _oldHisPK, int _newHisPK, string _action)
        {
            try
            {
                if (string.IsNullOrEmpty(_permissionID) || _permissionID == "")
                {
                    _permissionID = "NoSessionOrError_";
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ActivityLogInsert";
                        cmd.Parameters.AddWithValue("@Time", _time.ToString("MM/dd/yyyy HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@Message", Tools.RemoveApostrophe(_message));
                        cmd.Parameters.AddWithValue("@StackTrace", Tools.RemoveApostrophe(_stackTrace));
                        cmd.Parameters.AddWithValue("@Source", Tools.RemoveApostrophe(_source));
                        cmd.Parameters.AddWithValue("@UserID", _usersID);
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        cmd.Parameters.AddWithValue("@OldHisPK", _oldHisPK);
                        cmd.Parameters.AddWithValue("@NewHisPK", _newHisPK);
                        cmd.Parameters.AddWithValue("@Action", _action);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Activity> Activity_Select(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Activity> L_Activity = new List<Activity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            @"Select * from Activity where Time between @DateFrom and @DateTo  order by ActivityPK ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"Select * from Activity where Time between @DateFrom and @DateTo  order by ActivityPK";
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Activity.Add(setActivity(dr));
                                }
                            }
                            return L_Activity;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<Activity> Activity_SelectTop30ByUserID(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Activity> L_Activity = new List<Activity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        
                            cmd.CommandText =
                            @"Select top 30 ActivityPK,Status,format(Time,'yyyy-MM-dd HH:mm:ss') Time,PermissionID,ObjectTable,ObjectTablePK,Message 
                                ,StackTrace,Source,OldValue,NewValue,UsersID
                                ,IPAddress
                                ,DBUserID,DBTerminalID,LastUpdateDB,LastUpdate from Activity where UsersID = @UserID  order by ActivityPK desc ";
                      
                            cmd.Parameters.AddWithValue("@UserID", _userID);
                            
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Activity.Add(setActivity(dr));
                                }
                            }
                            return L_Activity;
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