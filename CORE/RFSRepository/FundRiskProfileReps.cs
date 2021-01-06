using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class FundRiskProfileReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundRiskProfile] " +
                            "([FundRiskProfilePK],[HistoryPK],[Status],[FundPK],[RiskProfilePK],";
        string _paramaterCommand = "@FundPK,@RiskProfilePK,";

        //2
        private FundRiskProfile setFundRiskProfile(SqlDataReader dr)
        {
            FundRiskProfile M_FundRiskProfile = new FundRiskProfile();
            M_FundRiskProfile.FundRiskProfilePK = Convert.ToInt32(dr["FundRiskProfilePK"]);
            M_FundRiskProfile.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundRiskProfile.Status = Convert.ToInt32(dr["Status"]);
            M_FundRiskProfile.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundRiskProfile.Notes = Convert.ToString(dr["Notes"]);
            M_FundRiskProfile.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundRiskProfile.FundID = Convert.ToString(dr["FundID"]);
            M_FundRiskProfile.RiskProfilePK = Convert.ToInt32(dr["RiskProfilePK"]);
            M_FundRiskProfile.RiskProfileID = Convert.ToString(dr["RiskProfileID"]);
            M_FundRiskProfile.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundRiskProfile.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundRiskProfile.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundRiskProfile.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundRiskProfile.EntryTime = dr["EntryTime"].ToString();
            M_FundRiskProfile.UpdateTime = dr["UpdateTime"].ToString();
            M_FundRiskProfile.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundRiskProfile.VoidTime = dr["VoidTime"].ToString();
            M_FundRiskProfile.DBUserID = dr["DBUserID"].ToString();
            M_FundRiskProfile.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundRiskProfile.LastUpdate = dr["LastUpdate"].ToString();
            M_FundRiskProfile.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundRiskProfile;
        }

        public List<FundRiskProfile> FundRiskProfile_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundRiskProfile> L_FundRiskProfile = new List<FundRiskProfile>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name FundID, C.descone RiskProfileID, A.* from FundRiskProfile A
                            left join Fund B on A.FundPK = B.FundPK and B.status  = 2
                            left join MasterValue C on A.RiskProfilePK = C.code and C.ID = 'InvestorsRiskProfile'  where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.Name FundID, C.descone RiskProfileID, A.* from FundRiskProfile A
                            left join Fund B on A.FundPK = B.FundPK and B.status  = 2
                            left join MasterValue C on A.RiskProfilePK = C.code and C.ID = 'InvestorsRiskProfile'";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundRiskProfile.Add(setFundRiskProfile(dr));
                                }
                            }
                            return L_FundRiskProfile;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundRiskProfile_Add(FundRiskProfile _FundRiskProfile, bool _havePrivillege)
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
                                 "Select isnull(max(FundRiskProfilePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundRiskProfile";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundRiskProfile.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundRiskProfilePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundRiskProfile";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FundRiskProfile.FundPK);
                        cmd.Parameters.AddWithValue("@RiskProfilePK", _FundRiskProfile.RiskProfilePK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundRiskProfile.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundRiskProfile");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundRiskProfile_Update(FundRiskProfile _FundRiskProfile, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundRiskProfile.FundRiskProfilePK, _FundRiskProfile.HistoryPK, "FundRiskProfile");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundRiskProfile set status=2, Notes=@Notes,FundPK=@FundPK,RiskProfilePK=@RiskProfilePK,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundRiskProfilePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundRiskProfile.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                            cmd.Parameters.AddWithValue("@Notes", _FundRiskProfile.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FundRiskProfile.FundPK);
                            cmd.Parameters.AddWithValue("@RiskProfilePK", _FundRiskProfile.RiskProfilePK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundRiskProfile.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundRiskProfile.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundRiskProfile set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundRiskProfilePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundRiskProfile.EntryUsersID);
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
                                cmd.CommandText = "Update FundRiskProfile set Notes=@Notes,FundPK=@FundPK,RiskProfilePK=@RiskProfilePK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundRiskProfilePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundRiskProfile.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                                cmd.Parameters.AddWithValue("@Notes", _FundRiskProfile.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FundRiskProfile.FundPK);
                                cmd.Parameters.AddWithValue("@RiskProfilePK", _FundRiskProfile.RiskProfilePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundRiskProfile.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            //ini untuk entrier
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundRiskProfile.FundRiskProfilePK, "FundRiskProfile");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From FundRiskProfile where FundRiskProfilePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundRiskProfile.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundRiskProfile.FundPK);
                                cmd.Parameters.AddWithValue("@RiskProfilePK", _FundRiskProfile.RiskProfilePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundRiskProfile.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundRiskProfile set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where FundRiskProfilePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundRiskProfile.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundRiskProfile.HistoryPK);
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

        public void FundRiskProfile_Approved(FundRiskProfile _FundRiskProfile)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundRiskProfile set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FundRiskProfilepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundRiskProfile.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundRiskProfile.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundRiskProfile set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundRiskProfilePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundRiskProfile.ApprovedUsersID);
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

        public void FundRiskProfile_Reject(FundRiskProfile _FundRiskProfile)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundRiskProfile set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where FundRiskProfilepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundRiskProfile.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundRiskProfile.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundRiskProfile set status= 2,LastUpdate=@LastUpdate  where FundRiskProfilePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
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

        public void FundRiskProfile_Void(FundRiskProfile _FundRiskProfile)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundRiskProfile set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundRiskProfilepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundRiskProfile.FundRiskProfilePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundRiskProfile.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundRiskProfile.VoidUsersID);
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