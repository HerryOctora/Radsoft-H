using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class RolesPermissionReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[RolesPermission] " +
                            "([RolesPermissionPK],[HistoryPK],[Status],[RolesPK],[PermissionPK],";
        string _paramaterCommand = "@RolesPK,@PermissionPK,";

        

        //2
        private RolesPermission setRolesPermission(SqlDataReader dr)
        {
            RolesPermission M_RolesPermission = new RolesPermission();
   
            M_RolesPermission.RolesPermissionPK = Convert.ToInt32(dr["RolesPermissionPK"]);
            M_RolesPermission.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RolesPermission.Status = Convert.ToInt32(dr["Status"]);
            M_RolesPermission.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RolesPermission.Notes = Convert.ToString(dr["Notes"]);
            M_RolesPermission.RolesPK = Convert.ToInt32(dr["RolesPK"]);
            M_RolesPermission.RolesID = Convert.ToString(dr["RolesID"]);
            M_RolesPermission.PermissionPK = Convert.ToInt32(dr["PermissionPK"]);
            M_RolesPermission.PermissionID = Convert.ToString(dr["PermissionID"]);
            M_RolesPermission.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RolesPermission.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RolesPermission.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RolesPermission.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RolesPermission.EntryTime = dr["EntryTime"].ToString();
            M_RolesPermission.UpdateTime = dr["UpdateTime"].ToString();
            M_RolesPermission.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RolesPermission.VoidTime = dr["VoidTime"].ToString();
            M_RolesPermission.DBUserID = dr["DBUserID"].ToString();
            M_RolesPermission.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RolesPermission.LastUpdate = dr["LastUpdate"].ToString();
            M_RolesPermission.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_RolesPermission;
        }

        public List<RolesPermission> RolesPermission_Select(int _status)
        {
            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    

                    DbCon.Open();
                    List<RolesPermission> L_RolesPermission = new List<RolesPermission>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when RP.status=1 then 'PENDING' else Case When RP.status = 2 then 'APPROVED' else Case when RP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID RolesID, P.ID PermissionID,RP.* from RolesPermission RP left join " +
                            "Roles R on RP.rolesPK = R.RolesPK and R.status = 2 left join " +
                            "Permission P on RP.PermissionPK = P.PermissionPK and P.status = 2 " +
                            "where RP.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                           
                            cmd.CommandText = "Select case when RP.status=1 then 'PENDING' else Case When RP.status = 2 then 'APPROVED' else Case when RP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID RolesID, P.ID PermissionID,RP.* from RolesPermission RP left join " +
                            "Roles R on RP.rolesPK = R.RolesPK and R.status = 2 left join " +
                            "Permission P on RP.PermissionPK = P.PermissionPK and P.status = 2 " +
                            "order by RolesPK,PermissionPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RolesPermission.Add(setRolesPermission(dr));
                                }
                            }
                            return L_RolesPermission;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int RolesPermission_Add(RolesPermission _rolespermission, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(rolespermissionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from RolesPermission";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _rolespermission.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(rolespermissionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from RolesPermission";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@RolesPK", _rolespermission.RolesPK);
                        cmd.Parameters.AddWithValue("@PermissionPK", _rolespermission.PermissionPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _rolespermission.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RolesPermission");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RolesPermission_Update(RolesPermission _rolespermission, bool _havePrivillege)
        {
            
           try
            {
                int _newHisPK;
               int status = _host.Get_Status(_rolespermission.RolesPermissionPK, _rolespermission.HistoryPK, "RolesPermission");
               DateTime _datetimeNow =  DateTime.Now; 
               using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RolesPermission set status=2, Notes=@Notes,RolesPK=@RolesPK,PermissionPK=@PermissionPK,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where rolespermissionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _rolespermission.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                            cmd.Parameters.AddWithValue("@Notes", _rolespermission.Notes);
                            cmd.Parameters.AddWithValue("@RolesPK", _rolespermission.RolesPK);
                            cmd.Parameters.AddWithValue("@PermissionPK", _rolespermission.PermissionPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _rolespermission.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _rolespermission.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RolesPermission set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesPermissionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _rolespermission.EntryUsersID);
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
                                cmd.CommandText = "Update RolesPermission set Notes=@Notes,RolesPK=@RolesPK,PermissionPK=@PermissionPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where rolespermissionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _rolespermission.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                                cmd.Parameters.AddWithValue("@Notes", _rolespermission.Notes);
                                cmd.Parameters.AddWithValue("@RolesPK", _rolespermission.RolesPK);
                                cmd.Parameters.AddWithValue("@PermissionPK", _rolespermission.PermissionPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _rolespermission.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            //ini untuk entrier
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_rolespermission.RolesPermissionPK, "RolesPermission");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From RolesPermission where RolesPermissionPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _rolespermission.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@RolesPK", _rolespermission.RolesPK);
                                cmd.Parameters.AddWithValue("@PermissionPK", _rolespermission.PermissionPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _rolespermission.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RolesPermission set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where RolesPermissionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _rolespermission.Notes);
                                cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _rolespermission.HistoryPK);
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

        public void RolesPermission_Approved(RolesPermission _rolespermission)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update rolespermission set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where rolespermissionpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _rolespermission.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _rolespermission.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RolesPermission set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesPermissionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _rolespermission.ApprovedUsersID);
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

        public void RolesPermission_Reject(RolesPermission _rolespermission)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update rolespermission set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where rolespermissionpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _rolespermission.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _rolespermission.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RolesPermission set status= 2,LastUpdate=@LastUpdate  where rolesPermissionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
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

        public void RolesPermission_Void(RolesPermission _rolespermission)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update rolespermission set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where rolespermissionpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _rolespermission.RolesPermissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _rolespermission.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _rolespermission.VoidUsersID);
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