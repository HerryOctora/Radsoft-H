using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;
using SucorInvest.Connect;
using System.Text;
using System.Security.Cryptography;  

namespace RFSRepository
{
    public class LoginReps
    {
        Host _host = new Host();
        public void SendMail(Login _login)
        {
            string localPath = "";
            string SubjectMail = "Reset Password";
            string _from = "";
            string _randomUsersPK = "";
            string _UsersID = "";

            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
            {
                DbCon.Open();
                string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                using (SqlCommand cmd = DbCon.CreateCommand())
                {
                    cmd.CommandText = @"
                        select UsersPK,ID from Users where email = @Email and status = 2
                        ";
                    cmd.Parameters.AddWithValue("@Email", _login.Email);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            _randomUsersPK = Convert.ToString(dr["UsersPK"]);
                            _UsersID = dr["ID"].ToString();
                        }
                    }
                }
            }


            _randomUsersPK = _randomUsersPK + RandomString(25, false);

            //var bodymail = "Click <a href='" + Tools._urlResetPassword + "/Radsoft/Login/ChangePassword/" + Cipher.Encrypt(Convert.ToString(_login.UsersID)) + "'>This Link</a> To Reset Password";
            var bodymail = "Click <a href='" + Tools._urlResetPassword + "/Radsoft/Login/ChangePassword/" + _randomUsersPK + "'>This Link</a> To Reset Password";

            SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
            dt = SendEmailReps.SendEmailTestingByInput(_UsersID, _login.Email, SubjectMail, bodymail, localPath, _from);
        }

        public string CheckID(string _email)
        {
            try
            {
                var _msg = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS(
                        Select * From Users Where email = @Email and status = 2
                        )
                        BEGIN

	                        SELECT 'true' ReturnDesc

                        END
                        ELSE
                        BEGIN
	                        SELECT 'false' ReturnDesc
                        END
                        ";
                        cmd.Parameters.AddWithValue("@Email", _email);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return _msg = Convert.ToString(dr["ReturnDesc"]);
                            }
                        }
                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 1; i < size + 1; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            else
                return builder.ToString();
        }

        public string ChangePassword(string _usersID)
        {
            var _msg = "";
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                string _email = "";

                string _UsersPK = _usersID.Substring(0, getIndexOfFirstLetter(_usersID));

                string _id = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
								Select Email,ID From Users Where UsersPK = @UsersPK and status = 2
                               ";
                        cmd.Parameters.AddWithValue("@UsersPK", _UsersPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _id = Convert.ToString(dr["ID"]);
                                _email = Convert.ToString(dr["Email"]);
                            }

                        }
                    }
                }
                string _emailUsers = _email;
                int size = 7;
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 1; i < size + 1; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(5 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                string _defaultPassword = builder.ToString().ToLower();
                string _setDefaultPassword = Cipher.Encrypt(Convert.ToString(_defaultPassword));
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS (select distinct * from Users where UsersPK = @UsersPK and Status in (1,2)) 
                                BEGIN

                                Update Users set Password = @DefaultPassword where UsersPK = @UsersPK and status = 2
                                Select 'Success Update' Result

                                END
                                ELSE
                                BEGIN

                                Select 'No Data' Result

                                 
                                END";
                        //cmd.Parameters.AddWithValue("@ApprovedUsersID", _sidRpt.EntryUsersID);
                        cmd.Parameters.AddWithValue("@UsersPK", _UsersPK);
                        cmd.Parameters.AddWithValue("@DefaultPassword", _setDefaultPassword);
                        using (SqlDataReader dr01 = cmd.ExecuteReader())
                        {
                            if (dr01.HasRows)
                            {
                                dr01.Read();

                                string localPath = "";
                                string SubjectMail = "Reset Password";
                                string _from = "";

                                var bodymail = "Your New Password Is : " + _defaultPassword;

                                SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
                                dt = SendEmailReps.SendEmailTestingByInput(_id, _emailUsers, SubjectMail, bodymail, localPath, _from);

                                return _msg = Convert.ToString(dr01["Result"]);

                            }
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return _msg;
        }

        public string CheckEmail(Login _login)
        {
            try
            {
                var _msg = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS(
                        Select * From Users Where Email = @Email
                        )
                        BEGIN

	                        SELECT 'true' ReturnDesc

                        END
                        ELSE
                        BEGIN
	                        SELECT 'false' ReturnDesc
                        END
                        ";
                        cmd.Parameters.AddWithValue("@Email", _login.Email);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return _msg = Convert.ToString(dr["ReturnDesc"]);
                            }
                        }
                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int getIndexOfFirstLetter(string input)
        {
            var index = 0;
            foreach (var c in input)
                if (char.IsLetter(c))
                    return index;
                else
                    index++;

            return input.Length;
        }



    }
}