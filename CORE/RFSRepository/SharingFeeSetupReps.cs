using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class SharingFeeSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[SharingFeeSetup] " +
                            "([SharingFeeSetupPK],[HistoryPK],[Status],[Date],[FundPK],[AgentPK],[FeeType],";
        string _paramaterCommand = "@Date,@FundPK,@AgentPK,@FeeType,";

        //2
        private SharingFeeSetup setSharingFeeSetup(SqlDataReader dr)
        {
            SharingFeeSetup M_SharingFeeSetup = new SharingFeeSetup();
            M_SharingFeeSetup.SharingFeeSetupPK = Convert.ToInt32(dr["SharingFeeSetupPK"]);
            M_SharingFeeSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SharingFeeSetup.Status = Convert.ToInt32(dr["Status"]);
            M_SharingFeeSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_SharingFeeSetup.Notes = Convert.ToString(dr["Notes"]);
            M_SharingFeeSetup.Date = Convert.ToString(dr["Date"]);
            M_SharingFeeSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_SharingFeeSetup.FundID = Convert.ToString(dr["FundID"]);
            M_SharingFeeSetup.FundName = Convert.ToString(dr["FundName"]);
            M_SharingFeeSetup.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_SharingFeeSetup.AgentID = Convert.ToString(dr["AgentID"]);
            M_SharingFeeSetup.AgentName = Convert.ToString(dr["AgentName"]);
            M_SharingFeeSetup.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_SharingFeeSetup.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_SharingFeeSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SharingFeeSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_SharingFeeSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_SharingFeeSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_SharingFeeSetup.EntryTime = dr["EntryTime"].ToString();
            M_SharingFeeSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_SharingFeeSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_SharingFeeSetup.VoidTime = dr["VoidTime"].ToString();
            M_SharingFeeSetup.DBUserID = dr["DBUserID"].ToString();
            M_SharingFeeSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_SharingFeeSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_SharingFeeSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_SharingFeeSetup;
        }

        public List<SharingFeeSetup> SharingFeeSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SharingFeeSetup> L_SharingFeeSetup = new List<SharingFeeSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when SF.status=1 then 'PENDING' else Case When SF.status = 2 then 'APPROVED' else Case when SF.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,F.Name FundName, A.ID AgentID ,A.Name AgentName,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc,SF.* from SharingFeeSetup SF left join " +
                           "Fund F on SF.FundPK = F.FundPK and F.status = 2 left join " +
                           "Agent A on SF.AgentPK = A.AgentPK and A.status = 2 " +
                           "where SF.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when SF.status=1 then 'PENDING' else Case When SF.status = 2 then 'APPROVED' else Case when SF.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,F.Name FundName, A.ID AgentID, A.Name AgentName,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc ,SF.* from SharingFeeSetup SF left join " +
                          "Fund F on SF.FundPK = F.FundPK and F.status = 2 left join " +
                           "Agent A on SF.AgentPK = A.AgentPK and A.status = 2 " +
                           "order by FundPK,AgentPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SharingFeeSetup.Add(setSharingFeeSetup(dr));
                                }
                            }
                            return L_SharingFeeSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SharingFeeSetup_Add(SharingFeeSetup _SharingFeeSetup, bool _havePrivillege)
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
                                 "Select isnull(max(SharingFeeSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from SharingFeeSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SharingFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(SharingFeeSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from SharingFeeSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _SharingFeeSetup.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _SharingFeeSetup.FundPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _SharingFeeSetup.AgentPK);
                        cmd.Parameters.AddWithValue("@FeeType", _SharingFeeSetup.FeeType);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _SharingFeeSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "SharingFeeSetup");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SharingFeeSetup_Update(SharingFeeSetup _SharingFeeSetup, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_SharingFeeSetup.SharingFeeSetupPK, _SharingFeeSetup.HistoryPK, "SharingFeeSetup"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SharingFeeSetup set status=2, Notes=@Notes,Date=@Date,FundPK=@FundPK,AgentPK=@AgentPK,FeeType=@FeeType," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where SharingFeeSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _SharingFeeSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _SharingFeeSetup.Notes);
                            cmd.Parameters.AddWithValue("@Date", _SharingFeeSetup.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _SharingFeeSetup.FundPK);
                            cmd.Parameters.AddWithValue("@AgentPK", _SharingFeeSetup.AgentPK);
                            cmd.Parameters.AddWithValue("@FeeType", _SharingFeeSetup.FeeType);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _SharingFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SharingFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SharingFeeSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SharingFeeSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _SharingFeeSetup.EntryUsersID);
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
                                cmd.CommandText = "Update SharingFeeSetup set Notes=@Notes,Date=@Date,FundPK=@FundPK,AgentPK=@AgentPK,FeeType=@FeeType," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where SharingFeeSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _SharingFeeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _SharingFeeSetup.Notes);
                                cmd.Parameters.AddWithValue("@Date", _SharingFeeSetup.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _SharingFeeSetup.FundPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _SharingFeeSetup.AgentPK);
                                cmd.Parameters.AddWithValue("@FeeType", _SharingFeeSetup.FeeType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SharingFeeSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_SharingFeeSetup.SharingFeeSetupPK, "SharingFeeSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From SharingFeeSetup where SharingFeeSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SharingFeeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _SharingFeeSetup.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _SharingFeeSetup.FundPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _SharingFeeSetup.AgentPK);
                                cmd.Parameters.AddWithValue("@FeeType", _SharingFeeSetup.FeeType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SharingFeeSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update SharingFeeSetup set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where SharingFeeSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _SharingFeeSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SharingFeeSetup.HistoryPK);
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

        public void SharingFeeSetup_Approved(SharingFeeSetup _SharingFeeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SharingFeeSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where SharingFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SharingFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _SharingFeeSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SharingFeeSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SharingFeeSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SharingFeeSetup.ApprovedUsersID);
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

        public void SharingFeeSetup_Reject(SharingFeeSetup _SharingFeeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SharingFeeSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SharingFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SharingFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SharingFeeSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SharingFeeSetup set status= 2,LastUpdate=@LastUpdate  where SharingFeeSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
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

        public void SharingFeeSetup_Void(SharingFeeSetup _SharingFeeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SharingFeeSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SharingFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SharingFeeSetup.SharingFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SharingFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SharingFeeSetup.VoidUsersID);
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
    }
}
