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
    public class HighRiskSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[HighRiskSetup] " +
                            "([HighRiskSetupPK],[HistoryPK],[Status],[FundPK],[UsersPK],";
        string _paramaterCommand = "@FundPK,@UsersPK,";

        //2
        private HighRiskSetup setHighRiskSetup(SqlDataReader dr)
        {
            HighRiskSetup M_HighRiskSetup = new HighRiskSetup();
            M_HighRiskSetup.HighRiskSetupPK = Convert.ToInt32(dr["HighRiskSetupPK"]);
            M_HighRiskSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_HighRiskSetup.Status = Convert.ToInt32(dr["Status"]);
            M_HighRiskSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_HighRiskSetup.Notes = dr["Notes"].ToString();
            M_HighRiskSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_HighRiskSetup.FundID = Convert.ToString(dr["FundID"]);
            M_HighRiskSetup.FundName = Convert.ToString(dr["FundName"]);
            M_HighRiskSetup.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_HighRiskSetup.UsersID = Convert.ToString(dr["UsersID"]);
            M_HighRiskSetup.UsersName = Convert.ToString(dr["UsersName"]);
            M_HighRiskSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_HighRiskSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_HighRiskSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_HighRiskSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_HighRiskSetup.EntryTime = dr["EntryTime"].ToString();
            M_HighRiskSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_HighRiskSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_HighRiskSetup.VoidTime = dr["VoidTime"].ToString();
            M_HighRiskSetup.DBUserID = dr["DBUserID"].ToString();
            M_HighRiskSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_HighRiskSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_HighRiskSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_HighRiskSetup;
        }

        public List<HighRiskSetup> HighRiskSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<HighRiskSetup> L_HighRiskSetup = new List<HighRiskSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name UsersName, B.ID UsersID, C.ID FundID, C.Name FundName,A.* from HighRiskSetup A left join
                            Users B on A.UsersPK = B.UsersPK and B.status in(1,2) left join 
                            Fund C on A.FundPK = C.FundPK and C.status in(1,2) 
                                where A.status = @status  order by A.HighRiskSetupPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name UsersName, B.ID UsersID, C.ID FundID, C.Name FundName,A.* from HighRiskSetup A left join
                            Users B on A.UsersPK = B.UsersPK and B.status in(1,2) left join 
                            Fund C on A.FundPK = C.FundPK and C.status in(1,2) order by A.HighRiskSetupPK  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_HighRiskSetup.Add(setHighRiskSetup(dr));
                                }
                            }
                            return L_HighRiskSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int HighRiskSetup_Add(HighRiskSetup _HighRiskSetup, bool _havePrivillege)
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
                                 "Select isnull(max(HighRiskSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from HighRiskSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _HighRiskSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(HighRiskSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from HighRiskSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _HighRiskSetup.FundPK);
                        cmd.Parameters.AddWithValue("@UsersPK", _HighRiskSetup.UsersPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _HighRiskSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "HighRiskSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int HighRiskSetup_Update(HighRiskSetup _HighRiskSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_HighRiskSetup.HighRiskSetupPK, _HighRiskSetup.HistoryPK, "HighRiskSetup");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update HighRiskSetup set status=2, Notes=@NotesFundPK=@FundPK,UsersPK=@UsersPK,
                                    ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate 
                                    where HighRiskSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _HighRiskSetup.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _HighRiskSetup.FundPK);
                            cmd.Parameters.AddWithValue("@UsersPK", _HighRiskSetup.UsersPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _HighRiskSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _HighRiskSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update HighRiskSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where HighRiskSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _HighRiskSetup.EntryUsersID);
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
                                cmd.CommandText = @"Update HighRiskSetup set Notes=@Notes,FundPK=@FundPK,UsersPK=@UsersPK,
                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate 
                                    where HighRiskSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _HighRiskSetup.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _HighRiskSetup.FundPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _HighRiskSetup.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _HighRiskSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_HighRiskSetup.HighRiskSetupPK, "HighRiskSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From HighRiskSetup where HighRiskSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _HighRiskSetup.FundPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _HighRiskSetup.UsersPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _HighRiskSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update HighRiskSetup set status= 4,Notes=@Notes, " +
                                    "lastupdate=@lastupdate where HighRiskSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _HighRiskSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskSetup.HistoryPK);
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

        public void HighRiskSetup_Approved(HighRiskSetup _HighRiskSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update HighRiskSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where HighRiskSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _HighRiskSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _HighRiskSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update HighRiskSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where HighRiskSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _HighRiskSetup.ApprovedUsersID);
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

        public void HighRiskSetup_Reject(HighRiskSetup _HighRiskSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update HighRiskSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where HighRiskSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _HighRiskSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _HighRiskSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update HighRiskSetup set status= 2,lastupdate=@lastupdate where HighRiskSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
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

        public void HighRiskSetup_Void(HighRiskSetup _HighRiskSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update HighRiskSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where HighRiskSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _HighRiskSetup.HighRiskSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _HighRiskSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _HighRiskSetup.VoidUsersID);
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


    }
}