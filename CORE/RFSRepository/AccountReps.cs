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
    public class AccountReps
    {
        Host _host = new Host();
   
        //1
        string _insertCommand = "INSERT INTO [dbo].[Account] " +
                            "([AccountPK],[HistoryPK],[Status],[ID],[Name],[Type],[Groups],[Levels],[ParentPK],[CurrencyPK],[OfficePK],[DepartmentPK]," +
                            "[Show],[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],[MKBDMapping],";
        string _paramaterCommand = "@ID,@Name,@Type,@Groups,@Levels,@ParentPK,@CurrencyPK,@OfficePK,@DepartmentPK," +
                            "@Show,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6,@ParentPK7,@ParentPK8,@ParentPK9,@MKBDMapping,";

        //2
        private Account setAccount(SqlDataReader dr)
        {
            Account M_Account = new Account();
            M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
            M_Account.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Account.Status = Convert.ToInt32(dr["Status"]);
            M_Account.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Account.Notes = Convert.ToString(dr["Notes"]);
            M_Account.ID = dr["ID"].ToString();
            M_Account.Name = dr["Name"].ToString();
            M_Account.Type = Convert.ToInt16(dr["Type"]);
            M_Account.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_Account.Groups = Convert.ToBoolean(dr["Groups"]);
            M_Account.Levels = Convert.ToInt32(dr["Levels"]);
            M_Account.ParentPK = dr["ParentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK"]);
            M_Account.ParentID = dr["ParentID"].ToString();
            M_Account.ParentName = Convert.ToString(dr["ParentName"]);
            M_Account.Depth = Convert.ToInt32(dr["Depth"]);
            M_Account.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_Account.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_Account.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_Account.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_Account.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_Account.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_Account.DepartmentName = Convert.ToString(dr["DepartmentName"]);
            M_Account.Show = Convert.ToBoolean(dr["Show"]);
            M_Account.ParentPK1 = dr["ParentPK1"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK1"]);
            M_Account.ParentPK2 = dr["ParentPK2"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK2"]);
            M_Account.ParentPK3 = dr["ParentPK3"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK3"]);
            M_Account.ParentPK4 = dr["ParentPK4"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK4"]);
            M_Account.ParentPK5 = dr["ParentPK5"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK5"]);
            M_Account.ParentPK6 = dr["ParentPK6"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK6"]);
            M_Account.ParentPK7 = dr["ParentPK7"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK7"]);
            M_Account.ParentPK8 = dr["ParentPK8"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK8"]);
            M_Account.ParentPK9 = dr["ParentPK9"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK9"]);
            M_Account.MKBDMapping = Convert.ToInt32(dr["MKBDMapping"]);
            M_Account.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_Account.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_Account.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_Account.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_Account.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_Account.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_Account.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_Account.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_Account.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_Account.DBTerminalID =  Convert.ToString(dr["DBTerminalID"]);
            M_Account.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_Account.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Account;
        }

        public List<Account> Account_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Account> L_Account = new List<Account>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,C.ID CurrencyID, O.ID OfficeID, D.ID DepartmentID,D.Name DepartmentName,ZA.ID ParentID,ZA.Name ParentName, A.* from Account A left join   " +
                            " Currency C on A.CurrencyPK = C.CurrencyPK and C.status = 2  left join   " +
                            " Office O on A.OfficePK = O.OfficePK and O.status = 2 left join   " +
                            " Department D on A.DepartmentPK = D.DepartmentPK and D.status = 2  left join   " +
                            " Account ZA on A.ParentPK = ZA.AccountPK  and ZA.status in (1,2) left join " +
                            " MasterValue MV on A.Type=MV.Code and MV.ID ='AccountType' " +
                            " where A.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,C.ID CurrencyID, O.ID OfficeID,D.ID DepartmentID,D.Name DepartmentName,ZA.ID ParentID,ZA.Name ParentName, A.* from Account A left join   " +
                            " Currency C on A.CurrencyPK = C.CurrencyPK and C.status = 2  left join   " +
                            " Office O on A.OfficePK = O.OfficePK and O.status = 2 left join   " +
                            " Department D on A.DepartmentPK = D.DepartmentPK and D.status = 2  left join   " +
                            " Account ZA on A.ParentPK = ZA.AccountPK  and ZA.status in (1,2) left join " +
                            " MasterValue MV on A.Type=MV.Code and MV.ID ='AccountType' ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Account.Add(setAccount(dr));
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Account_Add(Account _account, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(AccountPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Account";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _account.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
       
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(AccountPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Account";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _account.ID);
                        cmd.Parameters.AddWithValue("@Name", _account.Name);
                        cmd.Parameters.AddWithValue("@Type", _account.Type);
                        cmd.Parameters.AddWithValue("@Groups", _account.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _account.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _account.ParentPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _account.CurrencyPK);
                        cmd.Parameters.AddWithValue("@OfficePK", _account.OfficePK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _account.DepartmentPK);
                        cmd.Parameters.AddWithValue("@Show", _account.Show);
                        cmd.Parameters.AddWithValue("@ParentPK1", _account.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _account.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _account.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _account.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _account.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _account.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _account.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _account.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _account.ParentPK9);
                        cmd.Parameters.AddWithValue("@MKBDMapping", _account.MKBDMapping);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _account.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Account");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Account_Update(Account _account, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_account.AccountPK, _account.HistoryPK, "account");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Account set status=2,Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,Groups=@Groups," +
                                "Levels=@Levels,ParentPK=@ParentPK,CurrencyPK=@CurrencyPK,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK," +
                                "Show=@Show,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,MKBDMapping=@MKBDMapping," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where AccountPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _account.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                            cmd.Parameters.AddWithValue("@ID", _account.ID);
                            cmd.Parameters.AddWithValue("@Notes", _account.Notes);
                            cmd.Parameters.AddWithValue("@Name", _account.Name);
                            cmd.Parameters.AddWithValue("@Type", _account.Type);
                            cmd.Parameters.AddWithValue("@Groups", _account.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _account.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _account.ParentPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _account.CurrencyPK);
                            cmd.Parameters.AddWithValue("@OfficePK", _account.OfficePK);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _account.DepartmentPK);
                            cmd.Parameters.AddWithValue("@Show", _account.Show);
                            cmd.Parameters.AddWithValue("@ParentPK1", _account.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _account.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _account.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _account.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _account.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _account.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _account.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _account.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _account.ParentPK9);
                            cmd.Parameters.AddWithValue("@MKBDMapping", _account.MKBDMapping);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _account.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _account.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Account set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where AccountPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _account.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
                                cmd.CommandText = "Update Account set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,Groups=@Groups," +
                                    "Levels=@Levels,ParentPK=@ParentPK,CurrencyPK=@CurrencyPK,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK," +
                                    "Show=@Show,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                    "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,MKBDMapping=@MKBDMapping," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where AccountPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _account.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                                cmd.Parameters.AddWithValue("@ID", _account.ID);
                                cmd.Parameters.AddWithValue("@Notes", _account.Notes);
                                cmd.Parameters.AddWithValue("@Name", _account.Name);
                                cmd.Parameters.AddWithValue("@Type", _account.Type);
                                cmd.Parameters.AddWithValue("@Groups", _account.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _account.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _account.ParentPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _account.CurrencyPK);
                                cmd.Parameters.AddWithValue("@OfficePK", _account.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _account.DepartmentPK);
                                cmd.Parameters.AddWithValue("@Show", _account.Show);
                                cmd.Parameters.AddWithValue("@ParentPK1", _account.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _account.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _account.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _account.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _account.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _account.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _account.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _account.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _account.ParentPK9);
                                cmd.Parameters.AddWithValue("@MKBDMapping", _account.MKBDMapping);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _account.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_account.AccountPK, "Account");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Account where AccountPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _account.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _account.ID);
                                cmd.Parameters.AddWithValue("@Name", _account.Name);
                                cmd.Parameters.AddWithValue("@Type", _account.Type);
                                cmd.Parameters.AddWithValue("@Groups", _account.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _account.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _account.ParentPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _account.CurrencyPK);
                                cmd.Parameters.AddWithValue("@OfficePK", _account.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _account.DepartmentPK);
                                cmd.Parameters.AddWithValue("@Show", _account.Show);
                                cmd.Parameters.AddWithValue("@ParentPK1", _account.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _account.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _account.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _account.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _account.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _account.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _account.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _account.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _account.ParentPK9);
                                cmd.Parameters.AddWithValue("@MKBDMapping", _account.MKBDMapping);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _account.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Account set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where AccountPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _account.Notes);
                                cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _account.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public void Account_Approved(Account _account)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Account set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where AccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _account.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _account.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Account set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where AccountPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _account.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Account_Reject(Account _account)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Account set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where AccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _account.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _account.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Account set status= 2,lastupdate=@lastupdate where AccountPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Account_Void(Account _account)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Account set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where AccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _account.AccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _account.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _account.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public Account Account_SelectByPK(int _accountPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case 
                                when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,C.ID CurrencyID, 
                                O.ID OfficeID, D.ID DepartmentID,D.Name DepartmentName,ZA.ID ParentID,ZA.Name ParentName, A.* from Account A left join      
                              Currency C on A.CurrencyPK = C.CurrencyPK and C.status = 2  left join      
                              Office O on A.OfficePK = O.OfficePK and O.status = 2 left join      
                              Department D on A.DepartmentPK = D.DepartmentPK and D.status = 2  left join      
                              Account ZA on A.ParentPK = ZA.AccountPK  and ZA.status in (1,2) left join    
                              MasterValue MV on A.Type=MV.Code and MV.ID ='AccountType'    
                             where A.AccountPK = @AccountPK    ";
                        cmd.Parameters.AddWithValue("@AccountPK", _accountPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setAccount(dr);
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


        public Account Account_LookupCurrencyByAccountPK(int _accountPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select * from Account where status = 2 and AccountPK = @AccountPK";
                        cmd.Parameters.AddWithValue("@AccountPK", _accountPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                Account M_Account = new Account();
                                M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                M_Account.ID = dr["ID"].ToString();
                                M_Account.Name = dr["Name"].ToString();
                                M_Account.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                return M_Account;

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


        public List<AccountCombo> Account_Combo()
        {
            try
            {
                
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2  order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ComboChildOnlyRpt()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2 and Groups = 0 union all select 0,'All', '' order by ID";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ComboChildOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2 and Groups = 0 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ComboGroupsOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2 and Groups = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ParentCombo()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ParentID, Name as ParentName FROM [Account]  where status in (1, 2) and Groups = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ParentID"]);
                                    M_Account.Name = Convert.ToString(dr["ParentName"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    

        public bool Account_UpdateParentAndDepth()
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                        {
                            cmd.CommandText = "UPDATE Account SET " +
                                                    "Account.ParentPK1 = isnull(Account_1.AccountPK,0), Account.ParentPK2 = isnull(Account_2.AccountPK,0), " +
                                                    "Account.ParentPK3 = isnull(Account_3.AccountPK,0), Account.ParentPK4 = isnull(Account_4.AccountPK,0), " +
                                                    "Account.ParentPK5 = isnull(Account_5.AccountPK,0), Account.ParentPK6 = isnull(Account_6.AccountPK,0), " +
                                                    "Account.ParentPK7 = isnull(Account_7.AccountPK,0), Account.ParentPK8 = isnull(Account_8.AccountPK,0), " +
                                                    "Account.ParentPK9 = isnull(Account_9.AccountPK,0)  " +
                                              "FROM Account " +
                                                    "LEFT JOIN Account AS Account_1 ON Account.ParentPK = Account_1.AccountPK " +
                                                    "LEFT JOIN Account AS Account_2 ON Account_1.ParentPK = Account_2.AccountPK " +
                                                    "LEFT JOIN Account AS Account_3 ON Account_2.ParentPK = Account_3.AccountPK " +
                                                    "LEFT JOIN Account AS Account_4 ON Account_3.ParentPK = Account_4.AccountPK " +
                                                    "LEFT JOIN Account AS Account_5 ON Account_4.ParentPK = Account_5.AccountPK " +
                                                    "LEFT JOIN Account AS Account_6 ON Account_5.ParentPK = Account_6.AccountPK " +
                                                    "LEFT JOIN Account AS Account_7 ON Account_6.ParentPK = Account_7.AccountPK " +
                                                    "LEFT JOIN Account AS Account_8 ON Account_7.ParentPK = Account_8.AccountPK " +
                                                    "LEFT JOIN Account AS Account_9 ON Account_8.ParentPK = Account_9.AccountPK Where Account.Status = 2";

                            cmd.CommandTimeout = 0;
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "Select AccountPK From Account Where Status = 2 and Groups = 1";
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    try
                                    {
                                        while (dr.Read())
                                        {
                                            using (SqlCommand cmdSubQuery = DbCon.CreateCommand())
                                            {
                                                cmdSubQuery.CommandText = "Update Account set Depth = @Depth Where AccountPK = @AccountPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetAccountDepth(Convert.ToInt32(dr["AccountPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@AccountPK", Convert.ToInt32(dr["AccountPK"]));
                                                cmdSubQuery.ExecuteNonQuery();
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
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private int GetAccountDepth(int _accountPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "DECLARE @Depth INT,@Depth1 INT, @Depth2 INT, @Depth3 INT, @Depth4 INT, @Depth5 INT, " +
                                          "@Depth6 INT, @Depth7 INT, @Depth8 INT, @Depth9 INT, @Depth10 INT " +
                                          "SELECT @Depth1 = MAX(Account_1.ParentPK), @Depth2 = MAX(Account_2.ParentPK), " +
                                          "@Depth3 = MAX(Account_3.ParentPK), @Depth4 = MAX(Account_4.ParentPK), " +
                                          "@Depth5 = MAX(Account_5.ParentPK), @Depth6 = MAX(Account_6.ParentPK), " +
                                          "@Depth7 = MAX(Account_7.ParentPK), @Depth8 = MAX(Account_8.ParentPK), " +
                                          "@Depth9 = MAX(Account_9.ParentPK), @Depth10 = MAX(Account_10.ParentPK) " +
                                          "FROM Account AS Account_10 RIGHT JOIN (Account AS Account_9 " +
                                          "RIGHT JOIN (Account AS Account_8 RIGHT JOIN (Account AS Account_7 " +
                                          "RIGHT JOIN (Account AS Account_6 RIGHT JOIN (Account AS Account_5 " +
                                          "RIGHT JOIN (Account AS Account_4 RIGHT JOIN (Account AS Account_3 " +
                                          "RIGHT JOIN (Account AS Account_2 RIGHT JOIN (Account AS Account_1 " +
                                          "RIGHT JOIN Account ON Account_1.ParentPK = Account.AccountPK) " +
                                          "ON Account_2.ParentPK = Account_1.AccountPK) ON Account_3.ParentPK = Account_2.AccountPK) " +
                                          "ON Account_4.ParentPK = Account_3.AccountPK) ON Account_5.ParentPK = Account_4.AccountPK) " +
                                          "ON Account_6.ParentPK = Account_5.AccountPK) ON Account_7.ParentPK = Account_6.AccountPK) " +
                                          "ON Account_8.ParentPK = Account_7.AccountPK) ON Account_9.ParentPK = Account_8.AccountPK) " +
                                          "ON Account_10.ParentPK = Account_9.AccountPK  " +
                                          "WHERE Account.AccountPK = @AccountPK and Account.Status = 2 " +
                                          "IF @Depth1 IS NULL " +
                                          "SET @Depth = 0  " +
                                          "ELSE IF @Depth2 IS NULL " +
                                          "SET @Depth = 1 " +
                                          "ELSE IF @Depth3 IS NULL " +
                                          "SET @Depth = 2 " +
                                          "ELSE IF @Depth4 IS NULL " +
                                          "SET @Depth = 3 " +
                                          "ELSE IF @Depth5 IS NULL " +
                                          "SET @Depth = 4 " +
                                          "ELSE IF @Depth6 IS NULL " +
                                          "SET @Depth = 5 " +
                                          "ELSE IF @Depth7 IS NULL " +
                                          "SET @Depth = 6 " +
                                          "ELSE IF @Depth8 IS NULL " +
                                          "SET @Depth = 7 " +
                                          "ELSE IF @Depth9 IS NULL " +
                                          "SET @Depth = 8  " +
                                          "ELSE IF @Depth10 IS NULL " +
                                          "SET @Depth = 9  " +
                                          "ELSE " +
                                          "SET @Depth = 0 " +
                                          "select @depth depth";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@AccountPK", _accountPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["depth"]);
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

        public List<AccountCombo> Account_ComboChildOnlyExcludeCashRefRpt()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account] where status = 2 and Groups = 0  
                        and AccountPK not in (select AccountPK from CashRef where status in (1,2))
                        union all select 0,'All', ''  
                        order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ComboChildOnlyExcludeCashRef()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2 and Groups = 0 
                        and AccountPK not in (select AccountPK from CashRef where status in (1,2)) order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ComboAssetChildOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2 and Groups = 0 and Type = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ComboAssetAndLiabilitiesChildOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2 and Groups = 0 and Type in (1,2) order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AccountCombo> Account_ComboExpenseChildOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountCombo> L_Account = new List<AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AccountPK,ID + ' - ' + Name as ID, Name FROM [Account]  where status = 2 and Groups = 0 and Type in (4) order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountCombo M_Account = new AccountCombo();
                                    M_Account.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    M_Account.ID = Convert.ToString(dr["ID"]);
                                    M_Account.Name = Convert.ToString(dr["Name"]);
                                    L_Account.Add(M_Account);
                                }
                            }
                            return L_Account;
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