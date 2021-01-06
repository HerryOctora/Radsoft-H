using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class RolesReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[Roles] " +
                            "([RolesPK],[HistoryPK],[Status],[ID],[Name],[Privillege],[IsSystem],";
        string _paramaterCommand = "@ID,@Name,@Privillege,@IsSystem,";

        //2
        private Roles setRoles(SqlDataReader dr)
        {
            Roles M_Roles = new Roles();
            M_Roles.RolesPK = Convert.ToInt32(dr["RolesPK"]);
            M_Roles.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Roles.Status = Convert.ToInt32(dr["Status"]);
            M_Roles.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Roles.Notes = Convert.ToString(dr["Notes"]);
            M_Roles.ID = dr["ID"].ToString();
            M_Roles.Name = dr["Name"].ToString();
            M_Roles.Privillege = Convert.ToBoolean(dr["Privillege"]);
            M_Roles.IsSystem = Convert.ToBoolean(dr["IsSystem"]);
            M_Roles.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Roles.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Roles.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Roles.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Roles.EntryTime = dr["EntryTime"].ToString();
            M_Roles.UpdateTime = dr["UpdateTime"].ToString();
            M_Roles.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Roles.VoidTime = dr["VoidTime"].ToString();
            M_Roles.DBUserID = dr["DBUserID"].ToString();
            M_Roles.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Roles.LastUpdate = dr["LastUpdate"].ToString();
            M_Roles.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Roles;
        }

        public List<Roles> Roles_Select(int _status)
        {
            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Roles> L_Roles = new List<Roles>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Roles where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Roles order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Roles.Add(setRoles(dr));
                                }
                            }
                            return L_Roles;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int Roles_Add(Roles _roles, bool _havePrivillege)
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
                                 "Select isnull(max(RolesPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Roles";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _roles.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(RolesPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From Roles";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _roles.ID);
                        cmd.Parameters.AddWithValue("@Name", _roles.Name);
                        cmd.Parameters.AddWithValue("@Privillege", _roles.Privillege);
                        cmd.Parameters.AddWithValue("@IsSystem", _roles.IsSystem);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _roles.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Roles");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int Roles_Update(Roles _roles, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_roles.RolesPK, _roles.HistoryPK, "Roles");
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Roles set status=2,Notes=@Notes,ID=@ID,Name=@Name,Privillege=@Privillege,IsSystem=@IsSystem," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where RolesPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _roles.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                            cmd.Parameters.AddWithValue("@Notes", _roles.Notes);
                            cmd.Parameters.AddWithValue("@ID", _roles.ID);
                            cmd.Parameters.AddWithValue("@Name", _roles.Name);
                            cmd.Parameters.AddWithValue("@Privillege", _roles.Privillege);
                            cmd.Parameters.AddWithValue("@IsSystem", _roles.IsSystem);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _roles.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _roles.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Roles set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _roles.EntryUsersID);
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
                                cmd.CommandText = "Update Roles set Notes=@Notes,ID=@ID,Name=@Name,Privillege=@Privillege,IsSystem=@IsSystem," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where RolesPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _roles.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                                cmd.Parameters.AddWithValue("@Notes", _roles.Notes);
                                cmd.Parameters.AddWithValue("@ID", _roles.ID);
                                cmd.Parameters.AddWithValue("@Name", _roles.Name);
                                cmd.Parameters.AddWithValue("@Privillege", _roles.Privillege);
                                cmd.Parameters.AddWithValue("@IsSystem", _roles.IsSystem);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _roles.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_roles.RolesPK, "Roles");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Roles where RolesPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _roles.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _roles.ID);
                                cmd.Parameters.AddWithValue("@Name", _roles.Name);
                                cmd.Parameters.AddWithValue("@Privillege", _roles.Privillege);
                                cmd.Parameters.AddWithValue("@IsSystem", _roles.IsSystem);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _roles.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Roles set status= 4,Notes=@Notes," +
                                    " LastUpdate=@lastupdate " +
                                    " where RolesPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _roles.Notes);
                                cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _roles.HistoryPK);
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

        public void Roles_Approved(Roles _roles)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Roles set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where RolesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                        cmd.Parameters.AddWithValue("@historyPK", _roles.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _roles.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Roles set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RolesPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _roles.ApprovedUsersID);
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

        public void Roles_Reject(Roles _roles)
        {
           try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Roles set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RolesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                        cmd.Parameters.AddWithValue("@historyPK", _roles.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _roles.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Roles set status= 2,LastUpdate=@LastUpdate where RolesPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
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

        public void Roles_Void(Roles _roles)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Roles set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RolesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _roles.RolesPK);
                        cmd.Parameters.AddWithValue("@historyPK", _roles.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _roles.VoidUsersID);
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

        public List<RolesCombo> Roles_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RolesCombo> L_Roles = new List<RolesCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  RolesPK,ID + ' - ' + Name as ID, Name FROM [Roles]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    RolesCombo M_Roles = new RolesCombo();
                                    M_Roles.RolesPK = Convert.ToInt32(dr["RolesPK"]);
                                    M_Roles.ID = Convert.ToString(dr["ID"]);
                                    M_Roles.Name = Convert.ToString(dr["Name"]);
                                    L_Roles.Add(M_Roles);
                                }

                            }
                            return L_Roles;
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