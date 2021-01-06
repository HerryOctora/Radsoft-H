using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class PermissionReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[Permission] " +
                            "([PermissionPK],[HistoryPK],[Status],[ID],[Description],";
        string _paramaterCommand = "@ID,@Description,";

        //2
        private Permission setPermission(SqlDataReader dr)
        {
            Permission M_Permission = new Permission();
            M_Permission.PermissionPK = Convert.ToInt32(dr["PermissionPK"]);
            M_Permission.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Permission.Status = Convert.ToInt32(dr["Status"]);
            M_Permission.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Permission.Notes = Convert.ToString(dr["Notes"]);
            M_Permission.ID = dr["ID"].ToString();
            M_Permission.Description = dr["Description"].ToString();
            M_Permission.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Permission.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Permission.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Permission.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Permission.EntryTime = dr["EntryTime"].ToString();
            M_Permission.UpdateTime = dr["UpdateTime"].ToString();
            M_Permission.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Permission.VoidTime = dr["VoidTime"].ToString();
            M_Permission.DBUserID = dr["DBUserID"].ToString();
            M_Permission.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Permission.LastUpdate = dr["LastUpdate"].ToString();
            M_Permission.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Permission;
        }

        public List<Permission> Permission_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Permission> L_Permission = new List<Permission>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Permission where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Permission order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Permission.Add(setPermission(dr));
                                }
                            }
                            return L_Permission;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int Permission_Add(Permission _permission, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(PermissionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Permission";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _permission.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(PermissionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Permission";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _permission.ID);
                        cmd.Parameters.AddWithValue("@Description", _permission.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _permission.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Permission");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
          
        }

        public int Permission_Update(Permission _permission, bool _havePrivillege)
        {
           try
            {
               int _newHisPK;
               DateTime _datetimeNow = DateTime.Now;
               int status = _host.Get_Status(_permission.PermissionPK, _permission.HistoryPK, "Permission");
               using (SqlConnection DbCon = new SqlConnection(Tools.conString))
               {
                   DbCon.Open();
                   if (_havePrivillege)
                   {
                       using (SqlCommand cmd = DbCon.CreateCommand())
                       {


                           cmd.CommandText = "Update Permission set status=2, Notes=@Notes,ID=@ID,Description=@Description," +
                               "ApprovedUsersID=@ApprovedUsersID, " +
                               "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate = @LastUpdate " +
                               "where PermissionPK = @PK and historyPK = @HistoryPK";
                           cmd.Parameters.AddWithValue("@HistoryPK", _permission.HistoryPK);
                           cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                           cmd.Parameters.AddWithValue("@Notes", _permission.Notes);
                           cmd.Parameters.AddWithValue("@ID", _permission.ID);
                           cmd.Parameters.AddWithValue("@Description", _permission.Description);
                           cmd.Parameters.AddWithValue("@UpdateUsersID", _permission.EntryUsersID);
                           cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                           cmd.Parameters.AddWithValue("@ApprovedUsersID", _permission.EntryUsersID);
                           cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                           cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                           cmd.ExecuteNonQuery();
                       }
                       using (SqlCommand cmd = DbCon.CreateCommand())
                       {
                           cmd.CommandText = "Update Permission set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate = @LastUpdate where PermissionPK = @PK and status = 4";
                           cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                           cmd.Parameters.AddWithValue("@VoidUsersID", _permission.EntryUsersID);
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
                               cmd.CommandText = "Update Permission set Notes=@Notes,ID=@ID,Description=@Description," +
                               "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate = @LastUpdate " +
                               "where PermissionPK = @PK and historyPK = @HistoryPK";
                               cmd.Parameters.AddWithValue("@HistoryPK", _permission.HistoryPK);
                               cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                               cmd.Parameters.AddWithValue("@Notes", _permission.Notes);
                               cmd.Parameters.AddWithValue("@ID", _permission.ID);
                               cmd.Parameters.AddWithValue("@Description", _permission.Description);
                               cmd.Parameters.AddWithValue("@UpdateUsersID", _permission.EntryUsersID);
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
                               _newHisPK = _host.Get_NewHistoryPK(_permission.PermissionPK, "Permission");
                               cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                               "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                               "From Permission where PermissionPK =@PK and historyPK = @HistoryPK ";

                               cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                               cmd.Parameters.AddWithValue("@HistoryPK", _permission.HistoryPK);
                               cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                               cmd.Parameters.AddWithValue("@ID", _permission.ID);
                               cmd.Parameters.AddWithValue("@Description", _permission.Description);
                               cmd.Parameters.AddWithValue("@UpdateUsersID", _permission.EntryUsersID);
                               cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                               cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                               cmd.ExecuteNonQuery();
                           }

                           using (SqlCommand cmd = DbCon.CreateCommand())
                           {
                               cmd.CommandText = "Update Permission set status= 4,Notes=@Notes, lastUpdate = @LastUpdate where PermissionPK = @PK and historyPK = @HistoryPK";
                               cmd.Parameters.AddWithValue("@Notes", _permission.Notes);
                               cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                               cmd.Parameters.AddWithValue("@HistoryPK", _permission.HistoryPK);
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

        public void Permission_Approved(Permission _permission)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Permission set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate  " +
                            "where PermissionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _permission.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _permission.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Permission set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate = @LastUpdate where PermissionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _permission.ApprovedUsersID);
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

        public void Permission_Reject(Permission _permission)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Permission set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where PermissionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _permission.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _permission.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Permission set status= 2,LastUpdate = @LastUpdate where PermissionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
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

        public void Permission_Void(Permission _permission)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Permission set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where PermissionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _permission.PermissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _permission.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _permission.VoidUsersID);
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
        public List<PermissionCombo> Permission_Combo()
        {
            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<PermissionCombo> L_Roles = new List<PermissionCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  PermissionPK,ID + ' - ' + Description as ID, Description  FROM [Permission]  where status = 2 order by PermissionPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    PermissionCombo M_Permission = new PermissionCombo();
                                    M_Permission.PermissionPK = Convert.ToInt32(dr["PermissionPK"]);
                                    M_Permission.ID = Convert.ToString(dr["ID"]);
                                    M_Permission.Description = Convert.ToString(dr["Description"]);
                                    L_Roles.Add(M_Permission);
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




































