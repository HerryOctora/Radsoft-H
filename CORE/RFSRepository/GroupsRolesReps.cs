using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class GroupsRolesReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[GroupsRoles] " +
                            "([GroupsRolesPK],[HistoryPK],[Status],[GroupsPK],[RolesPK],";
        string _paramaterCommand = "@GroupsPK,@RolesPK,";

        //2
        private GroupsRoles setGroupsRoles(SqlDataReader dr)
        {
            GroupsRoles M_GroupsRoles = new GroupsRoles();
            M_GroupsRoles.GroupsRolesPK = Convert.ToInt32(dr["GroupsRolesPK"]);
            M_GroupsRoles.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_GroupsRoles.Status = Convert.ToInt32(dr["Status"]);
            M_GroupsRoles.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_GroupsRoles.Notes = Convert.ToString(dr["Notes"]);
            M_GroupsRoles.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
            M_GroupsRoles.GroupsID = Convert.ToString(dr["GroupsID"]);
            M_GroupsRoles.RolesPK = Convert.ToInt32(dr["RolesPK"]);
            M_GroupsRoles.RolesID = Convert.ToString(dr["RolesID"]);
            M_GroupsRoles.EntryUsersID = dr["EntryUsersID"].ToString();
            M_GroupsRoles.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_GroupsRoles.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_GroupsRoles.VoidUsersID = dr["VoidUsersID"].ToString();
            M_GroupsRoles.EntryTime = dr["EntryTime"].ToString();
            M_GroupsRoles.UpdateTime = dr["UpdateTime"].ToString();
            M_GroupsRoles.ApprovedTime = dr["ApprovedTime"].ToString();
            M_GroupsRoles.VoidTime = dr["VoidTime"].ToString();
            M_GroupsRoles.DBUserID = dr["DBUserID"].ToString();
            M_GroupsRoles.DBTerminalID = dr["DBTerminalID"].ToString();
            M_GroupsRoles.LastUpdate = dr["LastUpdate"].ToString();
            M_GroupsRoles.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_GroupsRoles;
        }

        public List<GroupsRoles> GroupsRoles_Select(int _status)
        {
            
              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GroupsRoles> L_GroupsRoles = new List<GroupsRoles>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID GroupsID, R.ID RolesID,GR.* from GroupsRoles GR left join " +
                           "Groups G on GR.GroupsPK = G.GroupsPK and G.status = 2 left join " +
                           "Roles R on GR.RolesPK = R.RolesPK  and R.status = 2 " +
                           "where GR.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID GroupsID, R.ID RolesID,GR.* from GroupsRoles GR left join " +
                           "Groups G on GR.GroupsPK = G.GroupsPK and G.status = 2 left join " +
                           "Roles R on GR.RolesPK = R.RolesPK  and R.status = 2 " +
                           "order by GroupsPK,RolesPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GroupsRoles.Add(setGroupsRoles(dr));
                                }
                            }
                            return L_GroupsRoles;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int GroupsRoles_Add(GroupsRoles _groupsRoles, bool _havePrivillege)
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
                                 "Select isnull(max(GroupsRolesPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from GroupsRoles";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _groupsRoles.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(GroupsRolesPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from GroupsRoles";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@GroupsPK", _groupsRoles.GroupsPK);
                        cmd.Parameters.AddWithValue("@RolesPK", _groupsRoles.RolesPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _groupsRoles.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "GroupsRoles");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int GroupsRoles_Update(GroupsRoles _groupsRoles, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_groupsRoles.GroupsRolesPK, _groupsRoles.HistoryPK, "GroupsRoles");;
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update GroupsRoles set status=2, Notes=@Notes,GroupsPK=@GroupsPK,RolesPK=@RolesPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where GroupsRolesPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _groupsRoles.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                            cmd.Parameters.AddWithValue("@Notes", _groupsRoles.Notes);
                            cmd.Parameters.AddWithValue("@GroupsPK", _groupsRoles.GroupsPK);
                            cmd.Parameters.AddWithValue("@RolesPK", _groupsRoles.RolesPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _groupsRoles.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _groupsRoles.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update GroupsRoles set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GroupsRolesPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _groupsRoles.EntryUsersID);
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
                                cmd.CommandText = "Update GroupsRoles set Notes=@Notes,GroupsPK=@GroupsPK,RolesPK=@RolesPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where GroupsRolesPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _groupsRoles.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                                cmd.Parameters.AddWithValue("@Notes", _groupsRoles.Notes);
                                cmd.Parameters.AddWithValue("@GroupsPK", _groupsRoles.GroupsPK);
                                cmd.Parameters.AddWithValue("@RolesPK", _groupsRoles.RolesPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _groupsRoles.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_groupsRoles.GroupsRolesPK, "GroupsRoles");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From GroupsRoles where GroupsRolesPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _groupsRoles.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@GroupsPK", _groupsRoles.GroupsPK);
                                cmd.Parameters.AddWithValue("@RolesPK", _groupsRoles.RolesPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _groupsRoles.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update GroupsRoles set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where GroupsRolesPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _groupsRoles.Notes);
                                cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _groupsRoles.HistoryPK);
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

        public void GroupsRoles_Approved(GroupsRoles _groupsRoles)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GroupsRoles set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where GroupsRolesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groupsRoles.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _groupsRoles.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GroupsRoles set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GroupsRolesPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groupsRoles.ApprovedUsersID);
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

        public void GroupsRoles_Reject(GroupsRoles _groupsRoles)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GroupsRoles set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GroupsRolesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groupsRoles.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groupsRoles.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GroupsRoles set status= 2,LastUpdate=@LastUpdate  where GroupsRolesPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
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

        public void GroupsRoles_Void(GroupsRoles _groupsRoles)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GroupsRoles set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GroupsRolesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groupsRoles.GroupsRolesPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groupsRoles.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groupsRoles.VoidUsersID);
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