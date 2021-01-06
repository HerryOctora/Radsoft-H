using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class AgentUsersReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[AgentUsers] " +
                            "([AgentUsersPK],[HistoryPK],[Status],[AgentPK],[UsersPK],";
        string _paramaterCommand = "@AgentPK,@UsersPK,";

        //2
        private AgentUsers setAgentUsers(SqlDataReader dr)
        {
            AgentUsers M_agentUsers = new AgentUsers();
            M_agentUsers.AgentUsersPK = Convert.ToInt32(dr["AgentUsersPK"]);
            M_agentUsers.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_agentUsers.Status = Convert.ToInt32(dr["Status"]);
            M_agentUsers.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_agentUsers.Notes = Convert.ToString(dr["Notes"]);
            M_agentUsers.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_agentUsers.AgentID = Convert.ToString(dr["AgentID"]);
            M_agentUsers.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_agentUsers.UsersID = Convert.ToString(dr["UsersID"]);
            M_agentUsers.EntryUsersID = dr["EntryUsersID"].ToString();
            M_agentUsers.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_agentUsers.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_agentUsers.VoidUsersID = dr["VoidUsersID"].ToString();
            M_agentUsers.EntryTime = dr["EntryTime"].ToString();
            M_agentUsers.UpdateTime = dr["UpdateTime"].ToString();
            M_agentUsers.ApprovedTime = dr["ApprovedTime"].ToString();
            M_agentUsers.VoidTime = dr["VoidTime"].ToString();
            M_agentUsers.DBUserID = dr["DBUserID"].ToString();
            M_agentUsers.DBTerminalID = dr["DBTerminalID"].ToString();
            M_agentUsers.LastUpdate = dr["LastUpdate"].ToString();
            M_agentUsers.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_agentUsers;
        }

        //3
        public List<AgentUsers> AgentUsers_Select(int _status)
        {
              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentUsers> L_agentUsers = new List<AgentUsers>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when GU.status=1 then 'PENDING' else Case When GU.status = 2 then 'APPROVED' else Case when GU.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID AgentID, U.ID UsersID,GU.* from AgentUsers GU left join " +
                           "Agent G on GU.AgentPK = G.AgentPK and G.status = 2 left join " +
                           "Users U on GU.UsersPK = U.UsersPK and U.status = 2 " +
                           "where GU.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when GU.status=1 then 'PENDING' else Case When GU.status = 2 then 'APPROVED' else Case when GU.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID AgentID, U.ID UsersID,GU.* from AgentUsers GU left join " +
                           "Agent G on GU.AgentPK = G.AgentPK and G.status = 2 left join " +
                           "Users U on GU.UsersPK = U.UsersPK and U.status = 2 " +
                           "order by AgentPK,UsersPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_agentUsers.Add(setAgentUsers(dr));
                                }
                            }
                            return L_agentUsers;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int AgentUsers_Add(AgentUsers _agentUsers, bool _havePrivillege)
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
                                 "Select isnull(max(AgentUsersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from AgentUsers";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _agentUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(AgentUsersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from AgentUsers";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentUsers.AgentPK);
                        cmd.Parameters.AddWithValue("@UsersPK", _agentUsers.UsersPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentUsers.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AgentUsers");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int AgentUsers_Update(AgentUsers _agentUsers, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_agentUsers.AgentUsersPK, _agentUsers.HistoryPK, "AgentUsers");
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentUsers set status=2,Notes=@Notes,AgentPK=@AgentPK,UsersPK=@UsersPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where AgentUsersPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _agentUsers.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                            cmd.Parameters.AddWithValue("@Notes", _agentUsers.Notes);
                            cmd.Parameters.AddWithValue("@AgentPK", _agentUsers.AgentPK);
                            cmd.Parameters.AddWithValue("@UsersPK", _agentUsers.UsersPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _agentUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _agentUsers.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentUsers set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentUsersPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _agentUsers.EntryUsersID);
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
                                cmd.CommandText = "Update AgentUsers set Notes=@Notes,AgentPK=@AgentPK,UsersPK=@UsersPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AgentUsersPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _agentUsers.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                                cmd.Parameters.AddWithValue("@Notes", _agentUsers.Notes);
                                cmd.Parameters.AddWithValue("@AgentPK", _agentUsers.AgentPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _agentUsers.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _agentUsers.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_agentUsers.AgentUsersPK, "AgentUsers");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AgentUsers where AgentUsersPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _agentUsers.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _agentUsers.AgentPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _agentUsers.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _agentUsers.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AgentUsers set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where AgentUsersPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _agentUsers.Notes);
                                cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _agentUsers.HistoryPK);
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

        public void AgentUsers_Approved(AgentUsers _agentUsers)
        {
              try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentUsers set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where AgentUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _agentUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _agentUsers.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentUsers set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentUsersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _agentUsers.ApprovedUsersID);
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

        public void AgentUsers_Reject(AgentUsers _agentUsers)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentUsers set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _agentUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _agentUsers.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentUsers set status= 2,LastUpdate=@LastUpdate where AgentUsersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
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

        public void AgentUsers_Void(AgentUsers _agentUsers)
        {
              try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentUsers set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentUsersPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _agentUsers.AgentUsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _agentUsers.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _agentUsers.VoidUsersID);
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