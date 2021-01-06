using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class RolesUsersReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[RolesUsers] " +
                            "([RolesUsersPK],[HistoryPK],[Status],[RolesPK],[UsersPK],";
        string _paramaterCommand = "@RolesPK,@UsersPK,";

        //2
        private RolesUsers setRolesUsers(SqlDataReader dr)
        {
            RolesUsers M_RolesUsers = new RolesUsers();
            M_RolesUsers.RolesUsersPK = Convert.ToInt32(dr["RolesUsersPK"]);
            M_RolesUsers.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RolesUsers.Status = Convert.ToInt32(dr["Status"]);
            M_RolesUsers.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RolesUsers.Notes = Convert.ToString(dr["Notes"]);
            M_RolesUsers.RolesPK = Convert.ToInt32(dr["RolesPK"]);
            M_RolesUsers.RolesID = Convert.ToString(dr["RolesID"]);
            M_RolesUsers.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_RolesUsers.UsersID = Convert.ToString(dr["UsersID"]);
            M_RolesUsers.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RolesUsers.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RolesUsers.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RolesUsers.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RolesUsers.EntryTime = dr["EntryTime"].ToString();
            M_RolesUsers.UpdateTime = dr["UpdateTime"].ToString();
            M_RolesUsers.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RolesUsers.VoidTime = dr["VoidTime"].ToString();
            M_RolesUsers.DBUserID = dr["DBUserID"].ToString();
            M_RolesUsers.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RolesUsers.LastUpdate = dr["LastUpdate"].ToString();
            M_RolesUsers.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_RolesUsers;
        }

        public List<RolesUsers> RolesUsers_Select(int _status)
        {
            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RolesUsers> L_RolesUsers = new List<RolesUsers>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when RU.status=1 then 'PENDING' else Case When RU.status = 2 then 'APPROVED' else Case when RU.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID RolesID, U.ID UsersID,RU.* from RolesUsers RU left join " +
                           "Roles R on RU.rolesPK = R.RolesPK and R.status = 2 left join " +
                           "Users U on RU.UsersPK = U.UsersPK  and U.status = 2 " +
                           "where RU.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {

                            cmd.CommandText = "Select case when RU.status=1 then 'PENDING' else Case When RU.status = 2 then 'APPROVED' else Case when RU.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID RolesID, U.ID UsersID,RU.* from RolesUsers RU left join " +
                           "Roles R on RU.rolesPK = R.RolesPK and R.status = 2 left join " +
                           "Users U on RU.UsersPK = U.UsersPK  and U.status = 2 " +
                           "order by RolesPK,UsersPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RolesUsers.Add(setRolesUsers(dr));
                                }
                            }
                            return L_RolesUsers;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int RolesUsers_Add(RolesUsers _rolesUsers, bool _havePrivillege)
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
                                 "Select isnull(max(RolesUsersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from RolesUsers";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _rolesUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(RolesUsersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from RolesUsers";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@RolesPK", _rolesUsers.RolesPK);
                        cmd.Parameters.AddWithValue("@UsersPK", _rolesUsers.UsersPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _rolesUsers.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RolesUsers");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int RolesUsers_Update(RolesUsers _rolesUsers, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_rolesUsers.RolesUsersPK, _rolesUsers.HistoryPK, "RolesUsers");
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RolesUsers set status=2,Notes=@Notes,RolesPK=@RolesPK,UsersPK=@UsersPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RolesUsersPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _rolesUsers.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                            cmd.Parameters.AddWithValue("@Notes", _rolesUsers.Notes);
                            cmd.Parameters.AddWithValue("@RolesPK", _rolesUsers.RolesPK);
                            cmd.Parameters.AddWithValue("@UsersPK", _rolesUsers.UsersPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _rolesUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _rolesUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RolesUsers set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesUsersPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _rolesUsers.EntryUsersID);
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
                                cmd.CommandText = "Update RolesUsers set Notes=@Notes,RolesPK=@RolesPK,UsersPK=@UsersPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RolesUsersPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _rolesUsers.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                                cmd.Parameters.AddWithValue("@Notes", _rolesUsers.Notes);
                                cmd.Parameters.AddWithValue("@RolesPK", _rolesUsers.RolesPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _rolesUsers.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _rolesUsers.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_rolesUsers.RolesUsersPK, "RolesUsers");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RolesUsers where RolesUsersPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _rolesUsers.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@RolesPK", _rolesUsers.RolesPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _rolesUsers.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _rolesUsers.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RolesUsers set status= 4,Notes=@Notes, "+
                                    " LastUpdate=@lastupdate " +
                                    " where RolesUsersPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _rolesUsers.Notes);
                                cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _rolesUsers.HistoryPK);
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

        public void RolesUsers_Approved(RolesUsers _rolesUsers)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RolesUsers set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where RolesUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _rolesUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _rolesUsers.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RolesUsers set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesUsersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _rolesUsers.ApprovedUsersID);
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

        public void RolesUsers_Reject(RolesUsers _rolesUsers)
        {
             try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RolesUsers set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RolesUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _rolesUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _rolesUsers.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RolesUsers set status= 2,LastUpdate=@LastUpdate where RolesUsersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
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

        public void RolesUsers_Void(RolesUsers _rolesUsers)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RolesUsers set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RolesUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _rolesUsers.RolesUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _rolesUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _rolesUsers.VoidUsersID);
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

        public List<RolesUsers> RolesUsers_SelectByUsersPK(int _usersPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RolesUsers> L_RolesUsers = new List<RolesUsers>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Select case when RU.status=1 then 'PENDING' else Case When RU.status = 2 then 'APPROVED' else Case when RU.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID RolesID, U.ID UsersID,RU.* from RolesUsers RU left join " +
                       "Roles R on RU.rolesPK = R.RolesPK and R.status = 2 left join " +
                       "Users U on RU.UsersPK = U.UsersPK  and U.status = 2 " +
                       "where RU.status in (1,2) and RU.UsersPK = @UsersPK";
                        cmd.Parameters.AddWithValue("@UsersPK", _usersPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RolesUsers.Add(setRolesUsers(dr));
                                }
                            }
                            return L_RolesUsers;
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