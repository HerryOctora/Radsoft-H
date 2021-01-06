using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class RolesPermissionNotificationReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[RolesPermissionNotification] " +
                            "([RolesPermissionNotificationPK],[HistoryPK],[Status],[RolesPK],[PermissionPK],";
        string _paramaterCommand = "@RolesPK,@PermissionPK,";

        //2
        private RolesPermissionNotification setRolesPermissionNotification(SqlDataReader dr)
        {
            RolesPermissionNotification M_RolesPermissionNotification = new RolesPermissionNotification();
            M_RolesPermissionNotification.RolesPermissionNotificationPK = Convert.ToInt32(dr["RolesPermissionNotificationPK"]);
            M_RolesPermissionNotification.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RolesPermissionNotification.Selected = Convert.ToBoolean(dr["Selected"]);
            M_RolesPermissionNotification.Status = Convert.ToInt32(dr["Status"]);
            M_RolesPermissionNotification.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RolesPermissionNotification.Notes = Convert.ToString(dr["Notes"]);           
            M_RolesPermissionNotification.RolesPK = Convert.ToInt32(dr["RolesPK"]);
            M_RolesPermissionNotification.RolesID = Convert.ToString(dr["RolesID"]);
            M_RolesPermissionNotification.PermissionPK = Convert.ToInt32(dr["PermissionPK"]);
            M_RolesPermissionNotification.PermissionID = Convert.ToString(dr["PermissionID"]);
            M_RolesPermissionNotification.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RolesPermissionNotification.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RolesPermissionNotification.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RolesPermissionNotification.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RolesPermissionNotification.EntryTime = dr["EntryTime"].ToString();
            M_RolesPermissionNotification.UpdateTime = dr["UpdateTime"].ToString();
            M_RolesPermissionNotification.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RolesPermissionNotification.VoidTime = dr["VoidTime"].ToString();
            M_RolesPermissionNotification.DBUserID = dr["DBUserID"].ToString();
            M_RolesPermissionNotification.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RolesPermissionNotification.LastUpdate = dr["LastUpdate"].ToString();
            M_RolesPermissionNotification.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RolesPermissionNotification;
        }

        public List<RolesPermissionNotification> RolesPermissionNotification_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RolesPermissionNotification> L_RolesPermissionNotification = new List<RolesPermissionNotification>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID PermissionID, R.ID RolesID,GR.* from RolesPermissionNotification GR left join
                            Permission G on GR.PermissionPK = G.PermissionPK and G.status = 2 left join
                            Roles R on GR.RolesPK = R.RolesPK  and R.status = 2 
                            where GR.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID PermissionID, R.ID RolesID,GR.* from RolesPermissionNotification GR left join
                            Permission G on GR.PermissionPK = G.PermissionPK and G.status = 2 left join
                            Roles R on GR.RolesPK = R.RolesPK  and R.status = 2 
                            order by RolesPK,PermissionPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RolesPermissionNotification.Add(setRolesPermissionNotification(dr));
                                }
                            }
                            return L_RolesPermissionNotification;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RolesPermissionNotification_Add(RolesPermissionNotification _RolesPermissionNotification, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(RolesPermissionNotificationPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from RolesPermissionNotification";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RolesPermissionNotification.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(RolesPermissionNotificationPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from RolesPermissionNotification";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);                       
                        cmd.Parameters.AddWithValue("@RolesPK", _RolesPermissionNotification.RolesPK);
                        cmd.Parameters.AddWithValue("@PermissionPK", _RolesPermissionNotification.PermissionPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RolesPermissionNotification.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RolesPermissionNotification");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void RolesPermissionNotification_RejectBySelected(string _usersID, string _permissionID)
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
                        Select @Time,@PermissionID,'RolesPermissionNotification',RolesPermissionNotificationPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from RolesPermissionNotification where  Status = 1 and Selected  = 1 
                       
                        --update RolesPermissionNotification set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        --where RolesPermissionNotificationPK in ( Select RolesPermissionNotificationPK from RolesPermissionNotification where Status = 1 and Selected  = 1 ) and Status = 4 and Selected  = 1 
                        
                        --Update RolesPermissionNotification set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        --where RolesPermissionNotificationPK in (Select RolesPermissionNotificationPK from RolesPermissionNotification where Status = 4 and Selected  = 1)  and Status = 1 and Selected  = 1 

                       update RolesPermissionNotification set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                       where RolesPermissionNotificationPK in ( Select RolesPermissionNotificationPK from RolesPermissionNotification where  Status = 1 and Selected  = 1 )   and Status = 1 and Selected  = 1
                 
                        Update RolesPermissionNotification set status= 2  where RolesPermissionNotificationPK in (Select RolesPermissionNotificationPK from RolesPermissionNotification where  Status = 4 and Selected  = 1)  and Status = 4 and Selected  = 1 



                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
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


        public void RolesPermissionNotification_VoidBySelected(string _usersID, string _permissionID)
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
                                          "Select @Time,@PermissionID,'RolesPermissionNotification',RolesPermissionNotificationPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from RolesPermissionNotification where Status = 2  and Selected  = 1 " +
                                          "\n update RolesPermissionNotification set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where RolesPermissionNotificationPK in ( Select RolesPermissionNotificationPK from RolesPermissionNotification where Status = 2 and Selected  = 1 ) \n " +
                                          " " +
                                          "";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
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

        public void RolesPermissionNotification_ApproveBySelected(string _usersID, string _permissionID)
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
                        Select @Time,@PermissionID,'RolesPermissionNotification',RolesPermissionNotificationPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from RolesPermissionNotification where Status = 1 and Selected  = 1 
                       
                        update RolesPermissionNotification set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and RolesPermissionNotificationPK in ( Select RolesPermissionNotificationPK from RolesPermissionNotification where Status = 1 and Selected  = 1 ) 
                        
                        Update RolesPermissionNotification set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        where status = 4 and RolesPermissionNotificationPK in (Select RolesPermissionNotificationPK from RolesPermissionNotification where Status = 4 and Selected  = 1)   

                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
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
        public bool Validate_ApproveBySelectedData(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        if exists(Select * from RolesPermissionNotification 
	                        where 
	                        and RolesPermissionNotificationPK  in
	                        (
		                        Select RolesPK from RolesPermissionNotification where 
		                        status = 2
	                        ) and status = 1 and selected = 1)
                        BEGIN
	                        select 1 result
                        END
                        ELSE
                        BEGIN
	                        select 0 result
                        END
                        ";
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

        public int RolesPermissionNotification_Update(RolesPermissionNotification _RolesPermissionNotification, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_RolesPermissionNotification.RolesPermissionNotificationPK, _RolesPermissionNotification.HistoryPK, "RolesPermissionNotification"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RolesPermissionNotification set status=2, Notes=@Notes,RolesPK=@RolesPK,PermissionPK=@PermissionPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RolesPermissionNotificationPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _RolesPermissionNotification.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                            cmd.Parameters.AddWithValue("@Notes", _RolesPermissionNotification.Notes);                           
                            cmd.Parameters.AddWithValue("@RolesPK", _RolesPermissionNotification.RolesPK);
                            cmd.Parameters.AddWithValue("@PermissionPK", _RolesPermissionNotification.PermissionPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _RolesPermissionNotification.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RolesPermissionNotification.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RolesPermissionNotification set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesPermissionNotificationPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _RolesPermissionNotification.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = "Update RolesPermissionNotification set Notes=@Notes,RolesPK=@RolesPK,PermissionPK=@PermissionPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where RolesPermissionNotificationPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _RolesPermissionNotification.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                                cmd.Parameters.AddWithValue("@Notes", _RolesPermissionNotification.Notes);  
                                cmd.Parameters.AddWithValue("@RolesPK", _RolesPermissionNotification.RolesPK);
                                cmd.Parameters.AddWithValue("@PermissionPK", _RolesPermissionNotification.PermissionPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RolesPermissionNotification.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_RolesPermissionNotification.RolesPermissionNotificationPK, "RolesPermissionNotification");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RolesPermissionNotification where RolesPermissionNotificationPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RolesPermissionNotification.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);           
                                cmd.Parameters.AddWithValue("@RolesPK", _RolesPermissionNotification.RolesPK);
                                cmd.Parameters.AddWithValue("@PermissionPK", _RolesPermissionNotification.PermissionPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RolesPermissionNotification.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RolesPermissionNotification set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where RolesPermissionNotificationPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _RolesPermissionNotification.Notes);
                                cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RolesPermissionNotification.HistoryPK);
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

        public void RolesPermissionNotification_Approved(RolesPermissionNotification _RolesPermissionNotification)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RolesPermissionNotification set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where RolesPermissionNotificationPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RolesPermissionNotification.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RolesPermissionNotification.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RolesPermissionNotification set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesPermissionNotificationPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RolesPermissionNotification.ApprovedUsersID);
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

        public void RolesPermissionNotification_Reject(RolesPermissionNotification _RolesPermissionNotification)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RolesPermissionNotification set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RolesPermissionNotificationPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RolesPermissionNotification.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RolesPermissionNotification.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RolesPermissionNotification set status= 2,LastUpdate=@LastUpdate  where RolesPermissionNotificationPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
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

        public void RolesPermissionNotification_Void(RolesPermissionNotification _RolesPermissionNotification)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RolesPermissionNotification set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RolesPermissionNotificationPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RolesPermissionNotification.RolesPermissionNotificationPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RolesPermissionNotification.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RolesPermissionNotification.VoidUsersID);
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


        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

    }
}