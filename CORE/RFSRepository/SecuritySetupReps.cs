using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class SecuritySetupReps
    {
        Host _host = new Host();
        
        string _insertCommand =
            "INSERT INTO [dbo].[SecuritySetup] " +
            " ([SecuritySetupPK],[HistoryPK],[Status],[MaxLoginRetry],[PasswordCharacterType],[MinimumPasswordLength], " +
            " [BitChangePasswordAtReset],[PasswordExpireDays],[UsersExpireDays],[BitReusedPassword],[HoursChangePassword], " +
            " [PasswordExpireLevel],[DefaultPassword],[IdleTimeMinutes],[ExpireSessionTime], ";

        string _paramaterCommand =
            " @MaxLoginRetry,@PasswordCharacterType,@MinimumPasswordLength,@BitChangePasswordAtReset,@PasswordExpireDays, " +
            " @UsersExpireDays,@BitReusedPassword,@HoursChangePassword,@PasswordExpireLevel,@DefaultPassword,@IdleTimeMinutes,@ExpireSessionTime, ";

        private SecuritySetup setSecuritySetup(SqlDataReader dr)
        {
            SecuritySetup M_SecuritySetup = new SecuritySetup();
            M_SecuritySetup.SecuritySetupPK = Convert.ToInt32(dr["SecuritySetupPK"]);
            M_SecuritySetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SecuritySetup.Status = Convert.ToInt32(dr["Status"]);
            M_SecuritySetup.StatusDesc = dr["StatusDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["StatusDesc"]);
            M_SecuritySetup.Notes = dr["Notes"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Notes"]);
            M_SecuritySetup.MaxLoginRetry = dr["MaxLoginRetry"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["MaxLoginRetry"]);
            M_SecuritySetup.PasswordCharacterType = dr["PasswordCharacterType"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PasswordCharacterType"]);
            M_SecuritySetup.PasswordCharacterTypeDesc = dr["PasswordCharacterTypeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PasswordCharacterTypeDesc"]);
            M_SecuritySetup.MinimumPasswordLength = dr["MinimumPasswordLength"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["MinimumPasswordLength"]);
            M_SecuritySetup.BitChangePasswordAtReset = dr["BitChangePasswordAtReset"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitChangePasswordAtReset"]);
            M_SecuritySetup.PasswordExpireDays = dr["PasswordExpireDays"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PasswordExpireDays"]);
            M_SecuritySetup.UsersExpireDays = dr["UsersExpireDays"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["UsersExpireDays"]);
            M_SecuritySetup.BitReusedPassword = dr["BitReusedPassword"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitReusedPassword"]);
            M_SecuritySetup.HoursChangePassword = dr["HoursChangePassword"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["HoursChangePassword"]);
            M_SecuritySetup.PasswordExpireLevel = dr["PasswordExpireLevel"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PasswordExpireLevel"]);
            M_SecuritySetup.DefaultPassword = dr["DefaultPassword"].Equals(DBNull.Value) == true ? "" : dr["DefaultPassword"].ToString();
            M_SecuritySetup.IdleTimeMinutes = dr["IdleTimeMinutes"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IdleTimeMinutes"]);
            M_SecuritySetup.ExpireSessionTime = dr["ExpireSessionTime"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ExpireSessionTime"]);
            M_SecuritySetup.EntryUsersID = dr["EntryUsersID"].Equals(DBNull.Value) == true ? "" : dr["EntryUsersID"].ToString();
            M_SecuritySetup.UpdateUsersID = dr["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : dr["UpdateUsersID"].ToString();
            M_SecuritySetup.ApprovedUsersID = dr["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : dr["ApprovedUsersID"].ToString();
            M_SecuritySetup.VoidUsersID = dr["VoidUsersID"].Equals(DBNull.Value) == true ? "" : dr["VoidUsersID"].ToString();
            M_SecuritySetup.EntryTime = dr["EntryTime"].Equals(DBNull.Value) == true ? "" : dr["EntryTime"].ToString();
            M_SecuritySetup.UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) == true ? "" : dr["UpdateTime"].ToString();
            M_SecuritySetup.ApprovedTime = dr["ApprovedTime"].Equals(DBNull.Value) == true ? "" : dr["ApprovedTime"].ToString();
            M_SecuritySetup.VoidTime = dr["VoidTime"].Equals(DBNull.Value) == true ? "" : dr["VoidTime"].ToString();
            M_SecuritySetup.DBUserID = dr["DBUserID"].Equals(DBNull.Value) == true ? "" : dr["DBUserID"].ToString();
            M_SecuritySetup.DBTerminalID = dr["DBTerminalID"].Equals(DBNull.Value) == true ? "" : dr["DBTerminalID"].ToString();
            M_SecuritySetup.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : dr["LastUpdate"].ToString();
            M_SecuritySetup.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_SecuritySetup;
        }

        public List<SecuritySetup> SecuritySetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SecuritySetup> L_SecuritySetup = new List<SecuritySetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                "select case when s.Status=1 then 'PENDING' else case when s.Status = 2 then 'APPROVED' else case when s.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, MV.DescOne PasswordCharacterTypeDesc,* from SecuritySetup S  left join " +
                                "MasterValue MV on S.PasswordCharacterType = MV.Code and ID = 'PasswordType' and MV.status = 2  " +
                                "where S.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "select case when s.Status=1 then 'PENDING' else case when s.Status = 2 then 'APPROVED' else case when s.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,MV.DescOne PasswordCharacterTypeDesc,* from SecuritySetup S  left join " +
                                "MasterValue MV on S.PasswordCharacterType = MV.Code and ID = 'PasswordType' and MV.status = 2 ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SecuritySetup.Add(setSecuritySetup(dr));
                                }
                            }
                            return L_SecuritySetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int SecuritySetup_Add(SecuritySetup _securitySetup, bool _havePrivillege)
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
                                 "Select isnull(max(SecuritySetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from SecuritySetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _securitySetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(SecuritySetupPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from SecuritySetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@MaxLoginRetry", _securitySetup.MaxLoginRetry);
                        cmd.Parameters.AddWithValue("@PasswordCharacterType", _securitySetup.PasswordCharacterType);
                        cmd.Parameters.AddWithValue("@MinimumPasswordLength", _securitySetup.MinimumPasswordLength);
                        cmd.Parameters.AddWithValue("@BitChangePasswordAtReset", _securitySetup.BitChangePasswordAtReset);
                        cmd.Parameters.AddWithValue("@PasswordExpireDays", _securitySetup.PasswordExpireDays);
                        cmd.Parameters.AddWithValue("@UsersExpireDays", _securitySetup.UsersExpireDays);
                        cmd.Parameters.AddWithValue("@BitReusedPassword", _securitySetup.BitReusedPassword);
                        cmd.Parameters.AddWithValue("@HoursChangePassword", _securitySetup.HoursChangePassword);
                        cmd.Parameters.AddWithValue("@PasswordExpireLevel", _securitySetup.PasswordExpireLevel);
                        cmd.Parameters.AddWithValue("@DefaultPassword", _securitySetup.DefaultPassword);
                        cmd.Parameters.AddWithValue("@IdleTimeMinutes", _securitySetup.IdleTimeMinutes);
                        cmd.Parameters.AddWithValue("@ExpireSessionTime", _securitySetup.ExpireSessionTime);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _securitySetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "SecuritySetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int SecuritySetup_Update(SecuritySetup _securitySetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_securitySetup.SecuritySetupPK, _securitySetup.HistoryPK, "SecuritySetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SecuritySetup set status=2,Notes=@Notes, " +
                            "MaxLoginRetry=@MaxLoginRetry,PasswordCharacterType=@PasswordCharacterType,MinimumPasswordLength=@MinimumPasswordLength, " +
                            "BitChangePasswordAtReset=@BitChangePasswordAtReset,PasswordExpireDays=@PasswordExpireDays,UsersExpireDays=@UsersExpireDays, " +
                            "BitReusedPassword=@BitReusedPassword,HoursChangePassword=@HoursChangePassword,PasswordExpireLevel=@PasswordExpireLevel,DefaultPassword=@DefaultPassword,IdleTimeMinutes=@IdleTimeMinutes,ExpireSessionTime=@ExpireSessionTime , " +
                            "ApprovedUsersID=@ApprovedUsersID, " +
                            "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                            "where SecuritySetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _securitySetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _securitySetup.Notes);
                            cmd.Parameters.AddWithValue("@MaxLoginRetry", _securitySetup.MaxLoginRetry);
                            cmd.Parameters.AddWithValue("@PasswordCharacterType", _securitySetup.PasswordCharacterType);
                            cmd.Parameters.AddWithValue("@MinimumPasswordLength", _securitySetup.MinimumPasswordLength);
                            cmd.Parameters.AddWithValue("@BitChangePasswordAtReset", _securitySetup.BitChangePasswordAtReset);
                            cmd.Parameters.AddWithValue("@PasswordExpireDays", _securitySetup.PasswordExpireDays);
                            cmd.Parameters.AddWithValue("@UsersExpireDays", _securitySetup.UsersExpireDays);
                            cmd.Parameters.AddWithValue("@BitReusedPassword", _securitySetup.BitReusedPassword);
                            cmd.Parameters.AddWithValue("@HoursChangePassword", _securitySetup.HoursChangePassword);
                            cmd.Parameters.AddWithValue("@PasswordExpireLevel", _securitySetup.PasswordExpireLevel);
                            cmd.Parameters.AddWithValue("@DefaultPassword", _securitySetup.DefaultPassword);
                            cmd.Parameters.AddWithValue("@IdleTimeMinutes", _securitySetup.IdleTimeMinutes);
                            cmd.Parameters.AddWithValue("@ExpireSessionTime", _securitySetup.ExpireSessionTime);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _securitySetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _securitySetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SecuritySetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SecuritySetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _securitySetup.EntryUsersID);
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
                                cmd.CommandText = "Update SecuritySetup set Notes=@Notes, " +
                                "MaxLoginRetry=@MaxLoginRetry,PasswordCharacterType=@PasswordCharacterType,MinimumPasswordLength=@MinimumPasswordLength, " +
                                "BitChangePasswordAtReset=@BitChangePasswordAtReset,PasswordExpireDays=@PasswordExpireDays,UsersExpireDays=@UsersExpireDays, " +
                                "BitReusedPassword=@BitReusedPassword,HoursChangePassword=@HoursChangePassword,PasswordExpireLevel=@PasswordExpireLevel,DefaultPassword=@DefaultPassword,IdleTimeMinutes=@IdleTimeMinutes,ExpireSessionTime=@ExpireSessionTime,  " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where SecuritySetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _securitySetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _securitySetup.Notes);
                                cmd.Parameters.AddWithValue("@MaxLoginRetry", _securitySetup.MaxLoginRetry);
                                cmd.Parameters.AddWithValue("@PasswordCharacterType", _securitySetup.PasswordCharacterType);
                                cmd.Parameters.AddWithValue("@MinimumPasswordLength", _securitySetup.MinimumPasswordLength);
                                cmd.Parameters.AddWithValue("@BitChangePasswordAtReset", _securitySetup.BitChangePasswordAtReset);
                                cmd.Parameters.AddWithValue("@PasswordExpireDays", _securitySetup.PasswordExpireDays);
                                cmd.Parameters.AddWithValue("@UsersExpireDays", _securitySetup.UsersExpireDays);
                                cmd.Parameters.AddWithValue("@BitReusedPassword", _securitySetup.BitReusedPassword);
                                cmd.Parameters.AddWithValue("@HoursChangePassword", _securitySetup.HoursChangePassword);
                                cmd.Parameters.AddWithValue("@PasswordExpireLevel", _securitySetup.PasswordExpireLevel);
                                cmd.Parameters.AddWithValue("@DefaultPassword", _securitySetup.DefaultPassword);
                                cmd.Parameters.AddWithValue("@IdleTimeMinutes", _securitySetup.IdleTimeMinutes);
                                cmd.Parameters.AddWithValue("@ExpireSessionTime", _securitySetup.ExpireSessionTime);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _securitySetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_securitySetup.SecuritySetupPK, "SecuritySetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From SecuritySetup where SecuritySetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _securitySetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@MaxLoginRetry", _securitySetup.MaxLoginRetry);
                                cmd.Parameters.AddWithValue("@PasswordCharacterType", _securitySetup.PasswordCharacterType);
                                cmd.Parameters.AddWithValue("@MinimumPasswordLength", _securitySetup.MinimumPasswordLength);
                                cmd.Parameters.AddWithValue("@BitChangePasswordAtReset", _securitySetup.BitChangePasswordAtReset);
                                cmd.Parameters.AddWithValue("@PasswordExpireDays", _securitySetup.PasswordExpireDays);
                                cmd.Parameters.AddWithValue("@UsersExpireDays", _securitySetup.UsersExpireDays);
                                cmd.Parameters.AddWithValue("@BitReusedPassword", _securitySetup.BitReusedPassword);
                                cmd.Parameters.AddWithValue("@HoursChangePassword", _securitySetup.HoursChangePassword);
                                cmd.Parameters.AddWithValue("@PasswordExpireLevel", _securitySetup.PasswordExpireLevel);
                                cmd.Parameters.AddWithValue("@DefaultPassword", _securitySetup.DefaultPassword);
                                cmd.Parameters.AddWithValue("@IdleTimeMinutes", _securitySetup.IdleTimeMinutes);
                                cmd.Parameters.AddWithValue("@ExpireSessionTime", _securitySetup.ExpireSessionTime);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _securitySetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update SecuritySetup set status= 4,Notes=@Notes," +
                                    " LastUpdate=@lastupdate " +
                                    " where SecuritySetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _securitySetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _securitySetup.HistoryPK);
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

        public void SecuritySetup_Approved(SecuritySetup _securitySetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SecuritySetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where SecuritySetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _securitySetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _securitySetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SecuritySetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SecuritySetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _securitySetup.ApprovedUsersID);
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

        public void SecuritySetup_Reject(SecuritySetup _securitySetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SecuritySetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SecuritySetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _securitySetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _securitySetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SecuritySetup set status= 2,LastUpdate=@LastUpdate where SecuritySetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
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

        public void SecuritySetup_Void(SecuritySetup _securitySetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SecuritySetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SecuritySetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _securitySetup.SecuritySetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _securitySetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _securitySetup.VoidUsersID);
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

        public SecuritySetup SecuritySetup_SelectApprovedOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            "select case when s.Status=1 then 'PENDING' else case when s.Status = 2 then 'APPROVED' else case when s.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,MV.DescOne PasswordCharacterTypeDesc,* from SecuritySetup S  left join " +
                            "MasterValue MV on S.PasswordCharacterType = MV.Code and ID = 'PasswordType' and MV.status = 2  " +
                            "where  S.status = 2 ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setSecuritySetup(dr);
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

    }
}