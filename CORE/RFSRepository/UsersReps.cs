using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;


namespace RFSRepository
{
    public class UsersReps
    {
        Host _host = new Host();

        string _insertCommand = "INSERT INTO [dbo].[Users] ([UsersPK],[HistoryPK],[Status],[ID],[Name],[Email],[Password],[JobTitle],[OfficePK],[ExpireUsersDate],[ExpirePasswordDate],[LoginRetry],[BitPasswordReset],[BitEnabled],[UserClientMode],";
        string _paramaterCommand = "@ID,@Name,@Email,@Password,@JobTitle,@OfficePK,@ExpireUsersDate,@ExpirePasswordDate,@LoginRetry,@BitPasswordReset,@BitEnabled,@UserClientMode,";

        private Users setUsers(SqlDataReader dr)
        {
            DateTime _tempDate;
            Users M_Users = new Users();
            M_Users.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_Users.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Users.Status = Convert.ToInt32(dr["Status"]);
            M_Users.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Users.Notes = Convert.ToString(dr["Notes"]);
            M_Users.SessionID = Convert.ToString(dr["SessionID"]);
            M_Users.ID = dr["ID"].ToString();
            M_Users.Name = dr["Name"].ToString();
            M_Users.Email = dr["Email"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Email"]);
            M_Users.Password = dr["Password"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Password"]);
            M_Users.JobTitle = dr["JobTitle"].ToString();
            M_Users.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_Users.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_Users.OfficeName = Convert.ToString(dr["OfficeName"]);
            if (!dr["ExpireUsersDate"].Equals(DBNull.Value))
            {
                _tempDate = Convert.ToDateTime(dr["ExpireUsersDate"]);
                M_Users.ExpireUsersDate = _tempDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (!dr["ExpirePasswordDate"].Equals(DBNull.Value))
            {
                _tempDate = Convert.ToDateTime(dr["ExpirePasswordDate"]);
                M_Users.ExpirePasswordDate = _tempDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
          
            M_Users.LoginRetry = Convert.ToInt32(dr["LoginRetry"]);
            M_Users.BitPasswordReset = Convert.ToBoolean(dr["BitPasswordReset"]);
            M_Users.BitEnabled = Convert.ToBoolean(dr["BitEnabled"]);
            //M_Users.UserClientMode = Convert.ToBoolean(dr["UserClientMode"]);
            if (_host.CheckColumnIsExist(dr, "UserClientMode"))
            {
                M_Users.UserClientMode = dr["UserClientMode"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UserClientMode"]);
            }

            if (_host.CheckColumnIsExist(dr, "UserClientModeDesc"))
            {
                M_Users.UserClientModeDesc = dr["UserClientModeDesc"].ToString();
            }

            //M_Users.UserClientMode = dr["UserClientMode"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UserClientMode"]);
            //M_Users.UserClientModeDesc = Convert.ToString(dr["UserClientModeDesc"]);

            M_Users.PrevPassword1 = dr["PrevPassword1"].ToString();
            M_Users.PrevPassword2 = dr["PrevPassword2"].ToString();
            M_Users.PrevPassword3 = dr["PrevPassword3"].ToString();
            M_Users.PrevPassword4 = dr["PrevPassword4"].ToString();
            M_Users.PrevPassword5 = dr["PrevPassword5"].ToString();
            M_Users.PrevPassword6 = dr["PrevPassword6"].ToString();
            M_Users.PrevPassword7 = dr["PrevPassword7"].ToString();
            M_Users.PrevPassword8 = dr["PrevPassword8"].ToString();
            M_Users.PrevPassword9 = dr["PrevPassword9"].ToString();
            M_Users.PrevPassword10 = dr["PrevPassword10"].ToString();
            M_Users.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Users.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Users.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Users.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Users.EntryTime = dr["EntryTime"].ToString();
            M_Users.UpdateTime = dr["UpdateTime"].ToString();
            M_Users.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Users.VoidTime = dr["VoidTime"].ToString();
            M_Users.DBUserID = dr["DBUserID"].ToString();
            M_Users.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Users.LastUpdate = dr["LastUpdate"].ToString();
            M_Users.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            if (!dr["ExpireSessionTime"].Equals(DBNull.Value))
            {
                _tempDate = Convert.ToDateTime(dr["ExpireSessionTime"]);
                M_Users.ExpireSessionTime = _tempDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            
            
            return M_Users;
        }

        public List<Users> Users_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Users> L_Users = new List<Users>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {                        
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                @"Select case when u.Status=1  then 'PENDING' else case when u.Status = 2 then 'APPROVED' else case when u.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,
                                case when UserClientMode = 1 then 'OPS' when UserClientMode = 2 then 'AGENT' else '' end UserClientModeDesc, o.id OfficeID, o.Name OfficeName, u.* from Users U left join Office O on U.OfficePK = O.OfficePK and o.status= 2 " +
                                "where U.status = @status " +
                                "order by u.UsersPK, u.ID,u.Name";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when u.Status=1 then 'PENDING' else case when u.Status = 2 then 'APPROVED' else case when u.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,
                                case when UserClientMode = 1 then 'OPS' when UserClientMode = 2 then 'AGENT' else '' end UserClientModeDesc, o.id OfficeID, o.Name OfficeName, u.* from Users U left join Office O on U.OfficePK = O.OfficePK and o.status= 2 ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Users.Add(setUsers(dr));
                                }
                            }
                            return L_Users;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Users_Add(Users _users, bool _havePrivillege)
        {
            try
            {
                SecuritySetupReps _secSetupReps = new SecuritySetupReps();
                SecuritySetup _secModel = new SecuritySetup();
                
                string _expireUsersDate;
                string _expirePasswordDate;
                string _password;
                string _defaultPassword;
                DateTime _nowExpireUser;
                DateTime _nowExpirePassword;
                DateTime _datetimeNow =  DateTime.Now;


                _secModel = _secSetupReps.SecuritySetup_SelectApprovedOnly();
                if (_secModel.DefaultPassword == null || _secModel.DefaultPassword == "")
                {
                    _defaultPassword = Tools.RandomChar();
                    _password = Cipher.Encrypt(_defaultPassword);
                }
                else {
                    
                    _defaultPassword = _secModel.DefaultPassword;
                    _password = Cipher.Encrypt(_defaultPassword);
                }
                
                //_password = _secModel.DefaultPassword;
                _nowExpireUser = DateTime.Today.AddDays(_secModel.UsersExpireDays);
                _nowExpirePassword = DateTime.Today.AddDays(_secModel.PasswordExpireDays);

                if (string.IsNullOrEmpty(_users.ExpireUsersDate) || string.IsNullOrWhiteSpace(_users.ExpireUsersDate))
                {
                    _expireUsersDate = _nowExpireUser.ToString();
                }
                else
                {
                    _expireUsersDate = _users.ExpireUsersDate;
                }

                if (string.IsNullOrEmpty(_users.ExpirePasswordDate) || string.IsNullOrWhiteSpace(_users.ExpirePasswordDate))
                {
                    _expirePasswordDate = _nowExpirePassword.ToString();
                }
                else
                {
                    _expirePasswordDate = _users.ExpirePasswordDate;
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(usersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Users";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _users.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(usersPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Users";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _users.ID);
                        cmd.Parameters.AddWithValue("@Name", _users.Name);
                        cmd.Parameters.AddWithValue("@Email", _users.Email);
                        cmd.Parameters.AddWithValue("@Password", _password);
                        cmd.Parameters.AddWithValue("@JobTitle", _users.JobTitle);
                        cmd.Parameters.AddWithValue("@OfficePK", _users.OfficePK);
                        cmd.Parameters.AddWithValue("@ExpireUsersDate", _expireUsersDate);
                        cmd.Parameters.AddWithValue("@ExpirePasswordDate", _expirePasswordDate);
                        cmd.Parameters.AddWithValue("@LoginRetry", 0);
                        //cmd.Parameters.AddWithValue("@BitPasswordReset", false);
                        //cmd.Parameters.AddWithValue("@BitEnabled", true);
                        cmd.Parameters.AddWithValue("@BitPasswordReset", _users.BitPasswordReset);
                        cmd.Parameters.AddWithValue("@BitEnabled", _users.BitEnabled);
                        cmd.Parameters.AddWithValue("@UserClientMode", _users.UserClientMode);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _users.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        // Set Last PK
                        int _lastPK = _host.Get_LastPKByLastUpate(_datetimeNow, "Users");

                        // Send UserID & Password Login
                        Tools.DataSendEmail dt = new Tools.DataSendEmail();
                        dt = Tools.SendMail(_users.Email, "", "", "Users & Password Login", "Users ID : " + _users.ID + "<br />" + "Password : " + _defaultPassword, "", _users.EntryUsersID);

                        // Return Last PK
                        return _lastPK;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }         
        }

        public int Users_Update(Users _users, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_users.UsersPK, _users.HistoryPK, "users");
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText =
                                "Update Users set status = 2, Notes=@Notes,ID=@ID,Name=@Name,Email=@Email,Password=@Password, " +
                                "JobTitle = @JobTitle,OfficePK = @OfficePK,ExpireUsersDate=@ExpireUsersDate,ExpirePasswordDate=@ExpirePasswordDate," +
                                "LoginRetry=@LoginRetry,BitPasswordReset=@BitPasswordReset,BitEnabled=@BitEnabled,UserClientMode=@UserClientMode," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where usersPK = @PK and historyPK = @HistoryPK";

                            // untuk Fungsi Update ada tambahan di commandText bagian set status = 2 dan where  historyPK = @HistoryPK sama cmd yg pling atas
                            cmd.Parameters.AddWithValue("@HistoryPK", _users.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                            cmd.Parameters.AddWithValue("@ID", _users.ID);
                            cmd.Parameters.AddWithValue("@Notes", _users.Notes);
                            cmd.Parameters.AddWithValue("@Name", _users.Name);
                            cmd.Parameters.AddWithValue("@Email", _users.Email);
                            cmd.Parameters.AddWithValue("@Password",_users.Password);
                            cmd.Parameters.AddWithValue("@JobTitle", _users.JobTitle);
                            cmd.Parameters.AddWithValue("@OfficePK", _users.OfficePK);
                            cmd.Parameters.AddWithValue("@ExpireUsersDate", _users.ExpireUsersDate);
                            cmd.Parameters.AddWithValue("@ExpirePasswordDate", _users.ExpirePasswordDate);
                            cmd.Parameters.AddWithValue("@LoginRetry", _users.LoginRetry);
                            cmd.Parameters.AddWithValue("@BitPasswordReset", _users.BitPasswordReset);
                            cmd.Parameters.AddWithValue("@BitEnabled", _users.BitEnabled);
                            cmd.Parameters.AddWithValue("@UserClientMode", _users.UserClientMode);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _users.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _users.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Users set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where usersPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _users.EntryUsersID);
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
                                cmd.CommandText = "Update Users set Notes=@Notes,ID=@ID,Name=@Name,Email=@Email,Password=@Password," +
                                    "JobTitle = @JobTitle,OfficePK=@OfficePK,ExpireUsersDate=@ExpireUsersDate,ExpirePasswordDate=@ExpirePasswordDate," +
                                    "LoginRetry=@LoginRetry,BitPasswordReset=@BitPasswordReset,BitEnabled=@BitEnabled,UserClientMode=@UserClientMode," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where usersPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _users.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                                cmd.Parameters.AddWithValue("@ID", _users.ID);
                                cmd.Parameters.AddWithValue("@Notes", _users.Notes);
                                cmd.Parameters.AddWithValue("@Name", _users.Name);
                                cmd.Parameters.AddWithValue("@Email", _users.Email);
                                cmd.Parameters.AddWithValue("@Password",_users.Password);
                                cmd.Parameters.AddWithValue("@JobTitle", _users.JobTitle);
                                cmd.Parameters.AddWithValue("@OfficePK", _users.OfficePK);
                                cmd.Parameters.AddWithValue("@ExpireUsersDate", _users.ExpireUsersDate);
                                cmd.Parameters.AddWithValue("@ExpirePasswordDate", _users.ExpirePasswordDate);
                                cmd.Parameters.AddWithValue("@LoginRetry", _users.LoginRetry);
                                cmd.Parameters.AddWithValue("@BitPasswordReset", _users.BitPasswordReset);
                                cmd.Parameters.AddWithValue("@BitEnabled", _users.BitEnabled);
                                cmd.Parameters.AddWithValue("@UserClientMode", _users.UserClientMode);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _users.EntryUsersID);
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

                                _newHisPK = _host.Get_NewHistoryPK(_users.UsersPK, "users");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Users where UsersPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _users.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _users.ID);
                                cmd.Parameters.AddWithValue("@Name", _users.Name);
                                cmd.Parameters.AddWithValue("@Email", _users.Email);
                                cmd.Parameters.AddWithValue("@Password", _users.Password);
                                cmd.Parameters.AddWithValue("@JobTitle", _users.JobTitle);
                                cmd.Parameters.AddWithValue("@OfficePK", _users.OfficePK);
                                cmd.Parameters.AddWithValue("@ExpireUsersDate", _users.ExpireUsersDate);
                                cmd.Parameters.AddWithValue("@ExpirePasswordDate", _users.ExpirePasswordDate);
                                cmd.Parameters.AddWithValue("@LoginRetry", _users.LoginRetry);
                                cmd.Parameters.AddWithValue("@BitPasswordReset", _users.BitPasswordReset);
                                cmd.Parameters.AddWithValue("@BitEnabled", _users.BitEnabled);
                                cmd.Parameters.AddWithValue("@UserClientMode", _users.UserClientMode);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _users.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Users set status= 4,Notes=@Notes," +
                                    " LastUpdate=@lastupdate  where usersPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _users.Notes);
                                cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _users.HistoryPK);
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

        public void Users_Approved(Users _users)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update users set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where userspk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _users.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _users.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Users set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where usersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _users.ApprovedUsersID);
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

        public void Users_Reject(Users _users)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update users set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where userspk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _users.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _users.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Users set status= 2,LastUpdate=@LastUpdate where usersPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
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

        public void Users_Void(Users _users)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update users set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where userspk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _users.UsersPK);
                        cmd.Parameters.AddWithValue("@historyPK", _users.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _users.VoidUsersID);
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

        public List<UsersCombo> Users_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UsersCombo> L_Users = new List<UsersCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  UsersPK,ID +' - '+ Name ID, Name FROM [Users]  where status = 2 ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    UsersCombo M_Users = new UsersCombo();
                                    M_Users.UsersPK = Convert.ToInt32(dr["UsersPK"]);
                                    M_Users.ID = Convert.ToString(dr["ID"]);
                                    M_Users.Name = Convert.ToString(dr["Name"]);
                                    L_Users.Add(M_Users);
                                }

                            }
                            return L_Users;
                        }
                    }
                }
   
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Users Users_SelectByUserID(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select 

isnull(B.ID,'') OfficeID,isnull(B.Name,'') OfficeName,'' StatusDesc,A.*
 From Users A
left join Office B on A.OfficePK = B.OfficePK and B.status in (1,2)
Where A.ID= @userID and A.status = 2";
                        cmd.Parameters.AddWithValue("@userID", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setUsers(dr);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public void Users_UpdateSessionID(string _sessionID, string _userID,string _ipAddress)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Users set SessionID= @sessionID,IPAddress = @IPAddress where ID = @userID and status=2";
                        cmd.Parameters.AddWithValue("@sessionID", _sessionID);
                        cmd.Parameters.AddWithValue("@userID", _userID);
                        cmd.Parameters.AddWithValue("@IPAddress", _ipAddress);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Users_Reset_Old12052017(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            "Declare @BitChangePasswordAtReset Bit " +
                            "Select  @BitChangePasswordAtReset = BitChangePasswordAtReset From securitySetup Where Status = 2 " +
                            "Update Users set LoginRetry = 0,Password = @DefaultPassword,BitEnabled = 1 " +
                            "Where ID = left(@UsersID, charindex(' ', @UsersID) - 1) and Status = 2 " +
                            "IF @BitChangePasswordAtReset = 1 " +
                            "BEGIN Update Users set BitPasswordReset = 1 Where ID = left(@UsersID, charindex(' ', @UsersID) - 1)  and Status = 2 END ";
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@DefaultPassword", Cipher.Encrypt(_host.Get_DefaultUserPassword()));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Users_Enable_Old12052017(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Users set LoginRetry = 0,BitEnabled = 1 " +
                            "Where ID = left(@UsersID, charindex(' ', @UsersID) - 1)  and Status = 2 ";
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

        public void Users_Disable_Old12052017(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Users set LoginRetry = 0,BitEnabled = 0 " +
                            "Where ID = left(@UsersID, charindex(' ', @UsersID) - 1)  and Status = 2 ";
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

        public void Users_IncrementLoginRetry_Old12052017(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Users set LoginRetry = isnull(LoginRetry,0) + 1 " +
                            "Where ID = @UsersID  and Status = 2 " +
                            "Declare @MaxLoginRetry INT  " +
                            "Select @MaxLoginRetry = MaxLoginRetry FROM SecuritySetup WHERE Status = 2  " +
                            "Declare @LoginRetry INT  " +
                            "Select @LoginRetry = LoginRetry From Users Where ID = @UsersID and Status = 2  " +
                            "IF @LoginRetry > @MaxLoginRetry BEGIN  " +
                            "UPDATE Users Set BitEnabled = 0 Where ID = @UsersID and Status = 2 END ";
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
         
        public void Users_ResetLoginRetry_Old12052017(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Users set LoginRetry = 0 " +
                            "Where ID = @UsersID  and Status = 2 ";
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

        public void Users_Reset(string _usersID,string _admin)
        {
            try
            {
                SecuritySetupReps _secSetupReps = new SecuritySetupReps();
                SecuritySetup _secModel = new SecuritySetup();

                string _expireUsersDate = string.Empty;
                string _expirePasswordDate = string.Empty;
                string _password = string.Empty;
                string _defaultPassword =string.Empty;

                DateTime _nowExpireUser;
                DateTime _nowExpirePassword;
                DateTime _datetimeNow = DateTime.Now;

                _secModel = _secSetupReps.SecuritySetup_SelectApprovedOnly();
                if (_secModel.DefaultPassword == null || _secModel.DefaultPassword == "")
                {
                    _defaultPassword = Tools.RandomChar();
                }
                else
                {
                    _defaultPassword = _secModel.DefaultPassword;
                }
                _password = Cipher.Encrypt(_defaultPassword);
                _nowExpireUser = DateTime.Today.AddDays(_secModel.UsersExpireDays);
                _nowExpirePassword = DateTime.Today.AddDays(_secModel.PasswordExpireDays);

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                declare @DefaultPassword nvarchar(500),
		                                @BitChangePasswordAtReset bit
                                
                                    set @DefaultPassword = @Password
                                
                                select 
	                                @BitChangePasswordAtReset = BitChangePasswordAtReset 
                                from SecuritySetup
                                where [Status] = 2

                                ---- Check Data
                                --select @DefaultPassword, @BitChangePasswordAtReset, left(@UsersID, charindex(' - ', @UsersID) - 1)

                                update Users set LoginRetry = 0, [Password] = @Password, BitEnabled = 1
                                where ID = left(@UsersID, charindex(' - ', @UsersID) - 1) and [Status] in (1,2) 
                                --and isnull(BitEnabled, 0) = 0

                                if @BitChangePasswordAtReset = 1
                                begin
	                                update Users set BitPasswordReset = 1
	                                where ID = left(@UsersID, charindex(' - ', @UsersID) - 1) and [Status] in (1,2)
                                --and isnull(BitEnabled, 0) = 0
                                end
                            ";
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Password", _password);
                        cmd.ExecuteNonQuery();
                        string _email = string.Empty;
                         _email = _host.Get_UsersMail(_usersID);

                         if (_email != "")
                         {
                             int a = _usersID.IndexOf("-");
                             string _formatUserId = _usersID.Substring(0, a - 1);
                             Tools.DataSendEmail dt = new Tools.DataSendEmail();
                             dt = Tools.SendMail(_host.Get_UsersMail(_usersID), "", "", "Users & Password Login", "Users ID : " + _formatUserId + "<br />" + "Password : " + _defaultPassword, "", _admin); 
                         }
                        
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Users_Enable(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Users set LoginRetry = 0, BitEnabled = 1 where ID = left(@UsersID, charindex(' - ', @UsersID) - 1) and [Status] = 2";
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

        public void Users_Disable(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Users set LoginRetry = 0, BitEnabled = 0 where ID = left(@UsersID, charindex(' - ', @UsersID) - 1) and [Status] = 2";
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

        public void Users_IncrementLoginRetry(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                update Users set LoginRetry = isnull(LoginRetry, 0) + 1
                                where ID = @UsersID and [Status] = 2
                                
                                declare @MaxLoginRetry  int,
                                        @LoginRetry     int
                            
                                select @MaxLoginRetry = isnull(MaxLoginRetry, 0) from SecuritySetup where [Status] = 2
                                select @LoginRetry = isnull(LoginRetry, 0) from Users where ID = @UsersID and [Status] = 2
                                
                                if @LoginRetry > @MaxLoginRetry
                                begin
                                    update Users set BitEnabled = 0 
                                    where ID = @UsersID and [Status] = 2
                                end
                            ";
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

        public void Users_ResetLoginRetry(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Users set LoginRetry = 0 where ID = @UsersID and [Status] = 2 ";
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

        public void Users_UpdateExpireSessionTime(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" 
DECLARE @SessionMinute INT
DECLARE @ExpireSessionTime DATETIME

SELECT @SessionMinute = ExpireSessionTime FROM dbo.SecuritySetup WHERE status = 2
SET @SessionMinute = ISNULL(@SessionMinute,600)

SELECT @ExpireSessionTime = DATEADD(MINUTE,@SessionMinute,GETDATE())

update Users set ExpireSessionTime = @ExpireSessionTime  where ID = @UsersID and [Status] = 2
";
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


        public int Get_UsersClientMode()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " SELECT  isnull(UserClientMode,1) Mode FROM [Users]  where status = 2 ";


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToInt32(dr["Mode"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Get_CheckExistingEmail(string _email, int _usersPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                            IF(@UsersPK = 0)
                            BEGIN
                                select Email from Users where  status in (1,2) and Email = @Email
                            END
                            ELSE
                            BEGIN
                                select Email from Users where UsersPK <> @UsersPK and status in (1,2) and Email = @Email
                            END";
                        cmd.Parameters.AddWithValue("@Email", _email);
                        cmd.Parameters.AddWithValue("@UsersPK", _usersPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
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