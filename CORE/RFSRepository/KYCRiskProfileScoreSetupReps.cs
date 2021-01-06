using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class KYCRiskProfileScoreSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[KYCRiskProfileScoreSetup] " +
                            "([KYCRiskProfileScoreSetupPK],[HistoryPK],[Status],[FromValue],[ToValue],[InvestorRiskProfile],";
        string _paramaterCommand = "@FromValue,@ToValue,@InvestorRiskProfile,";

        //2
        private KYCRiskProfileScoreSetup setKYCRiskProfileScoreSetup(SqlDataReader dr)
        {
            KYCRiskProfileScoreSetup M_KYCRiskProfileScoreSetup = new KYCRiskProfileScoreSetup();
            M_KYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK = Convert.ToInt32(dr["KYCRiskProfileScoreSetupPK"]);
            M_KYCRiskProfileScoreSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_KYCRiskProfileScoreSetup.Status = Convert.ToInt32(dr["Status"]);
            M_KYCRiskProfileScoreSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_KYCRiskProfileScoreSetup.Notes = Convert.ToString(dr["Notes"]);
            M_KYCRiskProfileScoreSetup.FromValue = Convert.ToInt32(dr["FromValue"]);
            M_KYCRiskProfileScoreSetup.ToValue = Convert.ToInt32(dr["ToValue"]);
            M_KYCRiskProfileScoreSetup.InvestorRiskProfile = Convert.ToInt32(dr["InvestorRiskProfile"]);
            M_KYCRiskProfileScoreSetup.InvestorRiskProfileDesc = Convert.ToString(dr["InvestorRiskProfileDesc"]);
            M_KYCRiskProfileScoreSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_KYCRiskProfileScoreSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_KYCRiskProfileScoreSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_KYCRiskProfileScoreSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_KYCRiskProfileScoreSetup.EntryTime = dr["EntryTime"].ToString();
            M_KYCRiskProfileScoreSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_KYCRiskProfileScoreSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_KYCRiskProfileScoreSetup.VoidTime = dr["VoidTime"].ToString();
            M_KYCRiskProfileScoreSetup.DBUserID = dr["DBUserID"].ToString();
            M_KYCRiskProfileScoreSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_KYCRiskProfileScoreSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_KYCRiskProfileScoreSetup.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_KYCRiskProfileScoreSetup;
        }

        public List<KYCRiskProfileScoreSetup> KYCRiskProfileScoreSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<KYCRiskProfileScoreSetup> L_KYCRiskProfileScoreSetup = new List<KYCRiskProfileScoreSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {//
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne InvestorRiskProfileDesc, * from KYCRiskProfileScoreSetup A
                            left join MasterValue B on A.InvestorRiskProfile = B.Code and B.Status = 2 and B.ID = 'InvestorsRiskProfile'
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {

                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne InvestorRiskProfileDesc, * from KYCRiskProfileScoreSetup A
                            left join MasterValue B on A.InvestorRiskProfile = B.Code and B.Status = 2 and B.ID = 'InvestorsRiskProfile'
                            Order by KYCRiskProfileScoreSetupPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_KYCRiskProfileScoreSetup.Add(setKYCRiskProfileScoreSetup(dr));
                                }
                            }
                            return L_KYCRiskProfileScoreSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int KYCRiskProfileScoreSetup_Add(KYCRiskProfileScoreSetup _kYCRiskProfileScoreSetup, bool _havePrivillege)
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
                                 "Select isnull(max(KYCRiskProfileScoreSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from KYCRiskProfileScoreSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _kYCRiskProfileScoreSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(KYCRiskProfileScoreSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from KYCRiskProfileScoreSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FromValue", _kYCRiskProfileScoreSetup.FromValue);
                        cmd.Parameters.AddWithValue("@ToValue", _kYCRiskProfileScoreSetup.ToValue);
                        cmd.Parameters.AddWithValue("@InvestorRiskProfile", _kYCRiskProfileScoreSetup.InvestorRiskProfile);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _kYCRiskProfileScoreSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "KYCRiskProfileScoreSetup");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int KYCRiskProfileScoreSetup_Update(KYCRiskProfileScoreSetup _kYCRiskProfileScoreSetup, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK, _kYCRiskProfileScoreSetup.HistoryPK, "KYCRiskProfileScoreSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update KYCRiskProfileScoreSetup set status=2,Notes=@Notes,FromValue=@FromValue,ToValue=@ToValue,InvestorRiskProfile=@InvestorRiskProfile," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where KYCRiskProfileScoreSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _kYCRiskProfileScoreSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _kYCRiskProfileScoreSetup.Notes);
                            cmd.Parameters.AddWithValue("@FromValue", _kYCRiskProfileScoreSetup.FromValue);
                            cmd.Parameters.AddWithValue("@ToValue", _kYCRiskProfileScoreSetup.ToValue);
                            cmd.Parameters.AddWithValue("@InvestorRiskProfile", _kYCRiskProfileScoreSetup.InvestorRiskProfile);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _kYCRiskProfileScoreSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _kYCRiskProfileScoreSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update KYCRiskProfileScoreSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where KYCRiskProfileScoreSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _kYCRiskProfileScoreSetup.EntryUsersID);
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
                                cmd.CommandText = "Update KYCRiskProfileScoreSetup set Notes=@Notes,FromValue=@FromValue,ToValue=@ToValue,InvestorRiskProfile=@InvestorRiskProfile," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where KYCRiskProfileScoreSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _kYCRiskProfileScoreSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _kYCRiskProfileScoreSetup.Notes);
                                cmd.Parameters.AddWithValue("@FromValue", _kYCRiskProfileScoreSetup.FromValue);
                                cmd.Parameters.AddWithValue("@ToValue", _kYCRiskProfileScoreSetup.ToValue);
                                cmd.Parameters.AddWithValue("@InvestorRiskProfile", _kYCRiskProfileScoreSetup.InvestorRiskProfile);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _kYCRiskProfileScoreSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK, "KYCRiskProfileScoreSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From KYCRiskProfileScoreSetup where KYCRiskProfileScoreSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _kYCRiskProfileScoreSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FromValue", _kYCRiskProfileScoreSetup.FromValue);
                                cmd.Parameters.AddWithValue("@ToValue", _kYCRiskProfileScoreSetup.ToValue);
                                cmd.Parameters.AddWithValue("@InvestorRiskProfile", _kYCRiskProfileScoreSetup.InvestorRiskProfile);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _kYCRiskProfileScoreSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update KYCRiskProfileScoreSetup set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where KYCRiskProfileScoreSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _kYCRiskProfileScoreSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _kYCRiskProfileScoreSetup.HistoryPK);
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

        public void KYCRiskProfileScoreSetup_Approved(KYCRiskProfileScoreSetup _kYCRiskProfileScoreSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update KYCRiskProfileScoreSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where KYCRiskProfileScoreSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _kYCRiskProfileScoreSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _kYCRiskProfileScoreSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update KYCRiskProfileScoreSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where KYCRiskProfileScoreSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _kYCRiskProfileScoreSetup.ApprovedUsersID);
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

        public void KYCRiskProfileScoreSetup_Reject(KYCRiskProfileScoreSetup _kYCRiskProfileScoreSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update KYCRiskProfileScoreSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where KYCRiskProfileScoreSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _kYCRiskProfileScoreSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _kYCRiskProfileScoreSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update KYCRiskProfileScoreSetup set status= 2,LastUpdate=@LastUpdate where KYCRiskProfileScoreSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
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

        public void KYCRiskProfileScoreSetup_Void(KYCRiskProfileScoreSetup _kYCRiskProfileScoreSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update KYCRiskProfileScoreSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where KYCRiskProfileScoreSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _kYCRiskProfileScoreSetup.KYCRiskProfileScoreSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _kYCRiskProfileScoreSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _kYCRiskProfileScoreSetup.VoidUsersID);
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

        public RiskProfileScore GetRiskProfileByScore(int _score)
        {
            int _investorRiskProfile = 0;
            string _investorRiskProfileDesc = "";
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd01 = DbCon.CreateCommand())
                    {
                        cmd01.CommandText = @"select InvestorRiskProfile, b.DescOne InvestorRiskProfileDesc from KYCRiskProfileScoreSetup a left join MasterValue b on a.InvestorRiskProfile = b.Code and b.Status in(1,2) 
                        where @Score Between FromValue and ToValue and a.status = 2 and b.id = 'InvestorsRiskProfile'";

                        cmd01.Parameters.AddWithValue("@Score", _score);

                        using (SqlDataReader dr = cmd01.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _investorRiskProfile = Convert.ToInt32(dr["InvestorRiskProfile"]);
                                _investorRiskProfileDesc = Convert.ToString(dr["InvestorRiskProfileDesc"]);
                            }
                        }
                    }
                    return new RiskProfileScore()
                    {
                        InvestorRiskProfile = Convert.ToInt32(_investorRiskProfile),
                        InvestorRiskProfileDesc = Convert.ToString(_investorRiskProfileDesc)
                    };
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


    }
}
