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
    public class UsersAccessTrailReps
    {
        Host _host = new Host();
        
        //1
        //string _insertCommand = "INSERT INTO [dbo].[UsersAccessTrail] " +
        //                    "([UsersAccessTrailPK],[HistoryPK],[Status],[ID],[Name],[Type],[UsersAccessTrailFee],[Description],";
        //string _paramaterCommand = "@ID,@Name,@Type,@UsersAccessTrailFee,@Description,";

        //2
        private UsersAccessTrail setUsersAccessTrail(SqlDataReader dr)
        {
            UsersAccessTrail M_UsersAccessTrail = new UsersAccessTrail();
            M_UsersAccessTrail.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_UsersAccessTrail.LoginSuccessTimeLast = dr["LoginSuccessTimeLast"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LoginSuccessTimeLast"]);
            M_UsersAccessTrail.LoginFailTimeLast = dr["LoginFailTimeLast"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LoginFailTimeLast"]);
            M_UsersAccessTrail.LogoutTimeLast = dr["LogoutTimeLast"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LogoutTimeLast"]);
            M_UsersAccessTrail.ChangePasswordTimeLast = dr["ChangePasswordTimeLast"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ChangePasswordTimeLast"]);
            M_UsersAccessTrail.LoginSuccessFreq = Convert.ToInt32(dr["LoginSuccessFreq"]);
            M_UsersAccessTrail.LoginFailFreq = Convert.ToInt32(dr["LoginFailFreq"]);
            M_UsersAccessTrail.LogoutFreq = Convert.ToInt32(dr["LogoutFreq"]);
            M_UsersAccessTrail.ChangePasswordFreq = Convert.ToInt32(dr["ChangePasswordFreq"]);
            M_UsersAccessTrail.DBUserID = dr["DBUserID"].ToString();
            M_UsersAccessTrail.DBTerminalID = dr["DBTerminalID"].ToString();
            M_UsersAccessTrail.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : dr["LastUpdate"].ToString();
            M_UsersAccessTrail.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_UsersAccessTrail;
        }

        //3
        public UsersAccessTrail UsersAccessTrail_SelectByID(string _usersID)
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
                            select
	                        format(LoginSuccessTimeLast,'dd-MMM-yyyy hh:mm:ss') LoginSuccessTimeLast
	                        ,format(LoginFailTimeLast,'dd-MMM-yyyy hh:mm:ss') LoginFailTimeLast
	                        ,format(LogoutTimeLast,'dd-MMM-yyyy hh:mm:ss') LogoutTimeLast
	                        ,format(ChangePasswordTimeLast,'dd-MMM-yyyy hh:mm:ss') ChangePasswordTimeLast
	                        ,LoginSuccessFreq,LoginFailFreq,LogoutFreq,ChangePasswordFreq
	                        ,uat.DBUserID,uat.DBTerminalID,uat.LastUpdate,uat.LastUpdateDB
	                        ,u.usersPK
                            from UsersAccessTrail uat 
                            left join Users u on u.Status = 2 and uat.UsersPK = u.UsersPK 
                            where u.ID = left(@UsersID, case when charindex(' ', @UsersID) - 1 = -1 then len(@UsersID) else charindex(' ', @UsersID) - 1 end)
                        ";
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setUsersAccessTrail(dr);
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

        public bool UsersAccessTrail_LoginSuccess(string _userID)
        {            
            try
            {                
                int p_UsersPK = _host.Get_UsersPK(_userID);
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " IF Exists( " +
                            " Select * from UsersAccessTrail Where UsersPK = @UsersPK) " +
                            " BEGIN Update UsersAccessTrail Set LoginSuccessTimeLast = GetDate(),LoginSuccessFreq = isnull(LoginSuccessFreq,0) + 1 " +
                            " Where UsersPK = @UsersPK END " +
                            " ELSE BEGIN Insert into UsersAccessTrail (UsersPK,LoginSuccessTimeLast,LoginSuccessFreq) " +
                            " Select @UsersPK,GetDate(),1 END ";
                        cmd.Parameters.AddWithValue("@UsersPK", p_UsersPK);
                        cmd.ExecuteReader();
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }           
        }

        public bool UsersAccessTrail_LoginFailed(string _userID)
        {            
            try
            {                
                int p_UsersPK = _host.Get_UsersPK(_userID);
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " IF Exists( " +
                            " Select * from UsersAccessTrail Where UsersPK = @UsersPK) " +
                            " BEGIN Update UsersAccessTrail Set LoginFailTimeLast = GetDate(),LoginFailFreq = isnull(LoginFailFreq,0) + 1 " +
                            " Where UsersPK = @UsersPK END " +
                            " ELSE BEGIN Insert into UsersAccessTrail (UsersPK,LoginFailTimeLast,LoginFailFreq) " +
                            " Select @UsersPK,GetDate(),1 END ";
                        cmd.Parameters.AddWithValue("@UsersPK", p_UsersPK);
                        cmd.ExecuteReader();
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }            
        }

        public bool UsersAccessTrail_Logout(string _userID)
        {
            try
            {
                int p_UsersPK = _host.Get_UsersPK(_userID);
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " IF Exists( " +
                            " Select * from UsersAccessTrail Where UsersPK = @UsersPK) " +
                            " BEGIN Update UsersAccessTrail Set LogoutTimeLast = GetDate(),LogoutFreq = isnull(LogoutFreq,0) + 1 " +
                            " Where UsersPK = @UsersPK END " +
                            " ELSE BEGIN Insert into UsersAccessTrail (UsersPK,LogoutTimeLast,LogoutFreq) " +
                            " Select @UsersPK,GetDate(),1 END ";
                        cmd.Parameters.AddWithValue("@UsersPK", p_UsersPK);
                        cmd.ExecuteReader();
                        return true;
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