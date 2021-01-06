using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class GroupsUsersReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[GroupsUsers] " +
                            "([GroupsUsersPK],[HistoryPK],[Status],[GroupsPK],[UsersPK],";
        string _paramaterCommand = "@GroupsPK,@UsersPK,";

        //2
        private GroupsUsers setGroupsUsers(SqlDataReader dr)
        {
            GroupsUsers M_GroupsUsers = new GroupsUsers();
            M_GroupsUsers.GroupsUsersPK = Convert.ToInt32(dr["GroupsUsersPK"]);
            M_GroupsUsers.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_GroupsUsers.Status = Convert.ToInt32(dr["Status"]);
            M_GroupsUsers.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_GroupsUsers.Notes = Convert.ToString(dr["Notes"]);
            M_GroupsUsers.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
            M_GroupsUsers.GroupsID = Convert.ToString(dr["GroupsID"]);
            M_GroupsUsers.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_GroupsUsers.UsersID = Convert.ToString(dr["UsersID"]);
            M_GroupsUsers.EntryUsersID = dr["EntryUsersID"].ToString();
            M_GroupsUsers.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_GroupsUsers.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_GroupsUsers.VoidUsersID = dr["VoidUsersID"].ToString();
            M_GroupsUsers.EntryTime = dr["EntryTime"].ToString();
            M_GroupsUsers.UpdateTime = dr["UpdateTime"].ToString();
            M_GroupsUsers.ApprovedTime = dr["ApprovedTime"].ToString();
            M_GroupsUsers.VoidTime = dr["VoidTime"].ToString();
            M_GroupsUsers.DBUserID = dr["DBUserID"].ToString();
            M_GroupsUsers.DBTerminalID = dr["DBTerminalID"].ToString();
            M_GroupsUsers.LastUpdate = dr["LastUpdate"].ToString();
            M_GroupsUsers.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_GroupsUsers;
        }

        //3
        public List<GroupsUsers> GroupsUsers_Select(int _status)
        {
              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GroupsUsers> L_GroupsUsers = new List<GroupsUsers>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when GU.status=1 then 'PENDING' else Case When GU.status = 2 then 'APPROVED' else Case when GU.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID GroupsID, U.ID UsersID,GU.* from GroupsUsers GU left join " +
                           "Groups G on GU.GroupsPK = G.GroupsPK and G.status = 2 left join " +
                           "Users U on GU.UsersPK = U.UsersPK and U.status = 2 " +
                           "where GU.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when GU.status=1 then 'PENDING' else Case When GU.status = 2 then 'APPROVED' else Case when GU.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID GroupsID, U.ID UsersID,GU.* from GroupsUsers GU left join " +
                           "Groups G on GU.GroupsPK = G.GroupsPK and G.status = 2 left join " +
                           "Users U on GU.UsersPK = U.UsersPK and U.status = 2 " +
                           "order by GroupsPK,UsersPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GroupsUsers.Add(setGroupsUsers(dr));
                                }
                            }
                            return L_GroupsUsers;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int GroupsUsers_Add(GroupsUsers _groupsUsers, bool _havePrivillege)
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
                                 "Select isnull(max(GroupsUsersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from GroupsUsers";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _groupsUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(GroupsUsersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from GroupsUsers";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@GroupsPK", _groupsUsers.GroupsPK);
                        cmd.Parameters.AddWithValue("@UsersPK", _groupsUsers.UsersPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _groupsUsers.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "GroupsUsers");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int GroupsUsers_Update(GroupsUsers _groupsUsers, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_groupsUsers.GroupsUsersPK, _groupsUsers.HistoryPK, "GroupsUsers");
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update GroupsUsers set status=2,Notes=@Notes,GroupsPK=@GroupsPK,UsersPK=@UsersPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where GroupsUsersPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _groupsUsers.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                            cmd.Parameters.AddWithValue("@Notes", _groupsUsers.Notes);
                            cmd.Parameters.AddWithValue("@GroupsPK", _groupsUsers.GroupsPK);
                            cmd.Parameters.AddWithValue("@UsersPK", _groupsUsers.UsersPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _groupsUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _groupsUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update GroupsUsers set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GroupsUsersPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _groupsUsers.EntryUsersID);
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
                                cmd.CommandText = "Update GroupsUsers set Notes=@Notes,GroupsPK=@GroupsPK,UsersPK=@UsersPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where GroupsUsersPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _groupsUsers.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                                cmd.Parameters.AddWithValue("@Notes", _groupsUsers.Notes);
                                cmd.Parameters.AddWithValue("@GroupsPK", _groupsUsers.GroupsPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _groupsUsers.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _groupsUsers.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_groupsUsers.GroupsUsersPK, "GroupsUsers");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From GroupsUsers where GroupsUsersPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _groupsUsers.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@GroupsPK", _groupsUsers.GroupsPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _groupsUsers.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _groupsUsers.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update GroupsUsers set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where GroupsUsersPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _groupsUsers.Notes);
                                cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _groupsUsers.HistoryPK);
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

        public void GroupsUsers_Approved(GroupsUsers _groupsUsers)
        {
              try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GroupsUsers set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where GroupsUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groupsUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _groupsUsers.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GroupsUsers set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GroupsUsersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groupsUsers.ApprovedUsersID);
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

        public void GroupsUsers_Reject(GroupsUsers _groupsUsers)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GroupsUsers set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GroupsUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groupsUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groupsUsers.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GroupsUsers set status= 2,LastUpdate=@LastUpdate where GroupsUsersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
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

        public void GroupsUsers_Void(GroupsUsers _groupsUsers)
        {
              try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GroupsUsers set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GroupsUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groupsUsers.GroupsUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groupsUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groupsUsers.VoidUsersID);
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