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
    public class Fxd11AccountReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Fxd11Account] " +
                            "([Fxd11AccountPK],[HistoryPK],[Status],[ID],[Name],[Show],";
        string _paramaterCommand = "@ID,@Name,@Show,";

        private Fxd11Account setFxd11Account(SqlDataReader dr)
        {
            Fxd11Account M_Fxd11Account = new Fxd11Account();
            M_Fxd11Account.Fxd11AccountPK = Convert.ToInt32(dr["Fxd11AccountPK"]);
            M_Fxd11Account.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Fxd11Account.Status = Convert.ToInt32(dr["Status"]);
            M_Fxd11Account.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Fxd11Account.Notes = Convert.ToString(dr["Notes"]);
            M_Fxd11Account.ID = dr["ID"].ToString();
            M_Fxd11Account.Name = dr["Name"].ToString();
            M_Fxd11Account.Show = Convert.ToBoolean(dr["Show"]);
            M_Fxd11Account.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_Fxd11Account.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_Fxd11Account.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_Fxd11Account.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_Fxd11Account.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_Fxd11Account.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_Fxd11Account.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_Fxd11Account.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_Fxd11Account.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_Fxd11Account.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_Fxd11Account.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_Fxd11Account.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Fxd11Account;
        }

        public List<Fxd11Account> Fxd11Account_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Fxd11Account> L_Fxd11Account = new List<Fxd11Account>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Fxd11Account where status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @" Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Fxd11Account order by ID  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Fxd11Account.Add(setFxd11Account(dr));
                                }
                            }
                            return L_Fxd11Account;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Fxd11Account_Add(Fxd11Account _Fxd11Account, bool _havePrivillege)
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
                                 "Select isnull(max(Fxd11AccountPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Fxd11Account";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Fxd11Account.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(Fxd11AccountPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Fxd11Account";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _Fxd11Account.ID);
                        cmd.Parameters.AddWithValue("@Name", _Fxd11Account.Name);
                        cmd.Parameters.AddWithValue("@Show", _Fxd11Account.Show);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _Fxd11Account.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Fxd11Account");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Fxd11Account_Update(Fxd11Account _Fxd11Account, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_Fxd11Account.Fxd11AccountPK, _Fxd11Account.HistoryPK, "Fxd11Account");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Fxd11Account set status=2,Notes=@Notes,ID=@ID,Name=@Name,Show=@Show," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where Fxd11AccountPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _Fxd11Account.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                            cmd.Parameters.AddWithValue("@ID", _Fxd11Account.ID);
                            cmd.Parameters.AddWithValue("@Notes", _Fxd11Account.Notes);
                            cmd.Parameters.AddWithValue("@Name", _Fxd11Account.Name);
                            cmd.Parameters.AddWithValue("@Show", _Fxd11Account.Show);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _Fxd11Account.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Fxd11Account.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Fxd11Account set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where Fxd11AccountPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _Fxd11Account.EntryUsersID);
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
                                cmd.CommandText = "Update Fxd11Account set Notes=@Notes,ID=@ID,Name=@Name,Show=@Show," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where Fxd11AccountPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _Fxd11Account.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                                cmd.Parameters.AddWithValue("@ID", _Fxd11Account.ID);
                                cmd.Parameters.AddWithValue("@Notes", _Fxd11Account.Notes);
                                cmd.Parameters.AddWithValue("@Name", _Fxd11Account.Name);
                                cmd.Parameters.AddWithValue("@Show", _Fxd11Account.Show);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Fxd11Account.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_Fxd11Account.Fxd11AccountPK, "Fxd11Account");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Fxd11Account where Fxd11AccountPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Fxd11Account.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _Fxd11Account.ID);
                                cmd.Parameters.AddWithValue("@Name", _Fxd11Account.Name);
                                cmd.Parameters.AddWithValue("@Show", _Fxd11Account.Show);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Fxd11Account.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Fxd11Account set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where Fxd11AccountPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _Fxd11Account.Notes);
                                cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Fxd11Account.HistoryPK);
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

        public void Fxd11Account_Approved(Fxd11Account _Fxd11Account)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Fxd11Account set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where Fxd11AccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _Fxd11Account.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _Fxd11Account.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Fxd11Account set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where Fxd11AccountPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Fxd11Account.ApprovedUsersID);
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

        public void Fxd11Account_Reject(Fxd11Account _Fxd11Account)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Fxd11Account set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where Fxd11AccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _Fxd11Account.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Fxd11Account.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Fxd11Account set status= 2,lastupdate=@lastupdate where Fxd11AccountPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
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

        public void Fxd11Account_Void(Fxd11Account _Fxd11Account)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Fxd11Account set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where Fxd11AccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Fxd11Account.Fxd11AccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _Fxd11Account.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Fxd11Account.VoidUsersID);
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

        public List<Fxd11AccountCombo> Fxd11Account_Combo()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Fxd11AccountCombo> L_Fxd11Account = new List<Fxd11AccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  Fxd11AccountPK,ID + ' - ' + Name as ID, Name FROM [Fxd11Account]  where status = 2  order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Fxd11AccountCombo M_Fxd11Account = new Fxd11AccountCombo();
                                    M_Fxd11Account.Fxd11AccountPK = Convert.ToInt32(dr["Fxd11AccountPK"]);
                                    M_Fxd11Account.ID = Convert.ToString(dr["ID"]);
                                    M_Fxd11Account.Name = Convert.ToString(dr["Name"]);
                                    L_Fxd11Account.Add(M_Fxd11Account);
                                }
                            }
                            return L_Fxd11Account;
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