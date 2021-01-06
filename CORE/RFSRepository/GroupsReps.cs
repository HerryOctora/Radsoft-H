using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class GroupsReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Groups] " +
                            "([GroupsPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private Groups setGroups(SqlDataReader dr)
        {
            Groups M_Groups = new Groups();
            M_Groups.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
            M_Groups.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Groups.Status = Convert.ToInt32(dr["Status"]);
            M_Groups.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Groups.Notes = Convert.ToString(dr["Notes"]);
            M_Groups.ID = dr["ID"].ToString();
            M_Groups.Name = dr["Name"].ToString();
            M_Groups.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Groups.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Groups.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Groups.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Groups.EntryTime = dr["EntryTime"].ToString();
            M_Groups.UpdateTime = dr["UpdateTime"].ToString();
            M_Groups.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Groups.VoidTime = dr["VoidTime"].ToString();
            M_Groups.DBUserID = dr["DBUserID"].ToString();
            M_Groups.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Groups.LastUpdate = dr["LastUpdate"].ToString();
            M_Groups.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Groups;
        }

        public List<Groups> Groups_Select(int _status)
        {

              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Groups> L_Groups = new List<Groups>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Groups where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Groups order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Groups.Add(setGroups(dr));
                                }
                            }
                            return L_Groups;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int Groups_Add(Groups _groups, bool _havePrivillege)
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
                                 "Select isnull(max(GroupsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Groups";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _groups.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(GroupsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Groups";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _groups.ID);
                        cmd.Parameters.AddWithValue("@Name", _groups.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _groups.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Groups");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int Groups_Update(Groups _groups, bool _havePrivillege)
        {
           
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_groups.GroupsPK, _groups.HistoryPK, "Groups");
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Groups set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where GroupsPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _groups.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                            cmd.Parameters.AddWithValue("@Notes", _groups.Notes);
                            cmd.Parameters.AddWithValue("@ID", _groups.ID);
                            cmd.Parameters.AddWithValue("@Name", _groups.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _groups.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _groups.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Groups set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GroupsPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _groups.EntryUsersID);
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
                                cmd.CommandText = "Update Groups set Notes=@Notes,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where GroupsPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _groups.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                                cmd.Parameters.AddWithValue("@Notes", _groups.Notes);
                                cmd.Parameters.AddWithValue("@ID", _groups.ID);
                                cmd.Parameters.AddWithValue("@Name", _groups.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _groups.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_groups.GroupsPK, "Groups");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Groups where GroupsPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _groups.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _groups.ID);
                                cmd.Parameters.AddWithValue("@Name", _groups.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _groups.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Groups set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where GroupsPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _groups.Notes);
                                cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _groups.HistoryPK);
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

        public void Groups_Approved(Groups _groups)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Groups set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where GroupsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groups.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _groups.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Groups set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where GroupsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groups.ApprovedUsersID);
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

        public void Groups_Reject(Groups _groups)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Groups set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GroupsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groups.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groups.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Groups set status= 2,LastUpdate=@LastUpdate where GroupsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
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

        public void Groups_Void(Groups _groups)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Groups set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where GroupsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _groups.GroupsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _groups.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _groups.VoidUsersID);
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

        public List<GroupsCombo> Groups_Combo()
        {
            
              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GroupsCombo> L_Groups = new List<GroupsCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  GroupsPK,ID +' - '+ Name ID, Name FROM [Groups]  where status = 2 order by GroupsPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    GroupsCombo M_Groups = new GroupsCombo();
                                    M_Groups.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
                                    M_Groups.ID = Convert.ToString(dr["ID"]);
                                    M_Groups.Name = Convert.ToString(dr["Name"]);
                                    L_Groups.Add(M_Groups);
                                }

                            }
                            return L_Groups;
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