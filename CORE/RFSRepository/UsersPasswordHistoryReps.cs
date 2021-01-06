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
    public class UsersPasswordHistoryReps
    {
        Host _host = new Host();
        private UsersPasswordHistory setUsersPasswordHistory(SqlDataReader dr)
        {
            UsersPasswordHistory M_uph = new UsersPasswordHistory();
            M_uph.UsersPasswordHistoryPK = Convert.ToInt32(dr["UsersPasswordHistoryPK"]);
            M_uph.Date = Convert.ToString(dr["Date"]);
            M_uph.Password = Convert.ToString(dr["Password"]);
            M_uph.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_uph.UsersID = Convert.ToString(dr["UsersID"]);
            M_uph.DBUserID = dr["DBUserID"].ToString();
            M_uph.DBTerminalID = dr["DBTerminalID"].ToString();
            M_uph.LastUpdate = dr["LastUpdate"].ToString();
            M_uph.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_uph;
        }

        public List<UsersPasswordHistory> UsersPasswordHistory_Select()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<UsersPasswordHistory> L_UsersPasswordHistory = new List<UsersPasswordHistory>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Select B.ID UsersID,A.* from UsersPasswordHistory A Left join Users B on A.UsersPK = B.UsersPK";
                 
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_UsersPasswordHistory.Add(setUsersPasswordHistory(dr));
                                }
                            }
                            return L_UsersPasswordHistory;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public UsersPasswordHistory UsersPasswordHistory_SelectByUsersPKandPassword(int _usersPK, string _password)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select distinct B.ID UsersID,A.* from usersPasswordHistory A Left join Users B on A.UsersPK = B.UsersPK and B.status = 2 where A.UsersPK = @UsersPK and A.Password = @Password ";
                        cmd.Parameters.AddWithValue("@UsersPK", _usersPK);
                        cmd.Parameters.AddWithValue("@Password", _password);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setUsersPasswordHistory(dr);
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