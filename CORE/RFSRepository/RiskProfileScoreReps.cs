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
    public class RiskProfileScoreReps
    {
        Host _host = new Host();
        string _insertCommand = "INSERT INTO [dbo].[RiskProfileScore] " +
                           "([RiskProfileScorePK],[HistoryPK],[Status],[FromValue],[ToValue],[InvestorRiskProfile],";
        string _paramaterCommand = "@FromValue,@ToValue,@InvestorRiskProfile,";

        //2
        private RiskProfileScore setRiskProfileScore(SqlDataReader dr)
        {
            RiskProfileScore M_RiskProfileScore = new RiskProfileScore();
            M_RiskProfileScore.RiskProfileScorePK = Convert.ToInt32(dr["RiskProfileScorePK"]);
            M_RiskProfileScore.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RiskProfileScore.Status = Convert.ToInt32(dr["Status"]);
            M_RiskProfileScore.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RiskProfileScore.Notes = Convert.ToString(dr["Notes"]);
            M_RiskProfileScore.FromValue = Convert.ToDecimal(dr["FromValue"]);
            M_RiskProfileScore.ToValue = Convert.ToDecimal(dr["ToValue"]);
            M_RiskProfileScore.InvestorRiskProfile = Convert.ToInt32(dr["InvestorRiskProfile"]);
            M_RiskProfileScore.InvestorRiskProfileDesc = Convert.ToString(dr["InvestorRiskProfileDesc"]);
            M_RiskProfileScore.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RiskProfileScore.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RiskProfileScore.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RiskProfileScore.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RiskProfileScore.EntryTime = dr["EntryTime"].ToString();
            M_RiskProfileScore.UpdateTime = dr["UpdateTime"].ToString();
            M_RiskProfileScore.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RiskProfileScore.VoidTime = dr["VoidTime"].ToString();
            M_RiskProfileScore.DBUserID = dr["DBUserID"].ToString();
            M_RiskProfileScore.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RiskProfileScore.LastUpdate = dr["LastUpdate"].ToString();
            M_RiskProfileScore.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_RiskProfileScore;
        }

        public List<RiskProfileScore> RiskProfileScore_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RiskProfileScore> L_RiskProfileScore = new List<RiskProfileScore>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne InvestorRiskProfileDesc, * from RiskProfileScore A
                            left join MasterValue B on A.InvestorRiskProfile = B.Code and B.Status = 2 and B.ID = 'InvestorsRiskProfile'
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne InvestorRiskProfileDesc, * from RiskProfileScore A
                            left join MasterValue B on A.InvestorRiskProfile = B.Code and B.Status = 2 and B.ID = 'InvestorsRiskProfile'
                            Order by RiskProfileScorePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RiskProfileScore.Add(setRiskProfileScore(dr));
                                }
                            }
                            return L_RiskProfileScore;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RiskProfileScore_Add(RiskProfileScore _RiskProfileScore, bool _havePrivillege)
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
                                 "Select isnull(max(RiskProfileScorePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from RiskProfileScore";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskProfileScore.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(RiskProfileScorePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from RiskProfileScore";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FromValue", _RiskProfileScore.FromValue);
                        cmd.Parameters.AddWithValue("@ToValue", _RiskProfileScore.ToValue);
                        cmd.Parameters.AddWithValue("@InvestorRiskProfile", _RiskProfileScore.InvestorRiskProfile);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RiskProfileScore.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RiskProfileScore");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RiskProfileScore_Update(RiskProfileScore _RiskProfileScore, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_RiskProfileScore.RiskProfileScorePK, _RiskProfileScore.HistoryPK, "RiskProfileScore");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RiskProfileScore set status=2, Notes=@Notes,FromValue=@FromValue,ToValue=@ToValue,InvestorRiskProfile=@InvestorRiskProfile, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RiskProfileScorePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _RiskProfileScore.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                            cmd.Parameters.AddWithValue("@Notes", _RiskProfileScore.Notes);
                            cmd.Parameters.AddWithValue("@FromValue", _RiskProfileScore.FromValue);
                            cmd.Parameters.AddWithValue("@ToValue", _RiskProfileScore.ToValue);
                            cmd.Parameters.AddWithValue("@InvestorRiskProfile", _RiskProfileScore.InvestorRiskProfile);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskProfileScore.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskProfileScore.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RiskProfileScore set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RiskProfileScorePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _RiskProfileScore.EntryUsersID);
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
                                cmd.CommandText = "Update RiskProfileScore set Notes=@Notes, FromValue=@FromValue, ToValue=@ToValue, InvestorRiskProfile=@InvestorRiskProfile, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " + 
                                "where RiskProfileScorePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskProfileScore.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                                cmd.Parameters.AddWithValue("@Notes", _RiskProfileScore.Notes);
                                cmd.Parameters.AddWithValue("@FromValue", _RiskProfileScore.FromValue);
                                cmd.Parameters.AddWithValue("@ToValue", _RiskProfileScore.ToValue);
                                cmd.Parameters.AddWithValue("@InvestorRiskProfile", _RiskProfileScore.InvestorRiskProfile);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskProfileScore.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_RiskProfileScore.RiskProfileScorePK, "RiskProfileScore");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From RiskProfileScore where RiskProfileScorePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskProfileScore.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FromValue", _RiskProfileScore.FromValue);
                                cmd.Parameters.AddWithValue("@ToValue", _RiskProfileScore.ToValue);
                                cmd.Parameters.AddWithValue("@InvestorRiskProfile", _RiskProfileScore.InvestorRiskProfile);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskProfileScore.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RiskProfileScore set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where RiskProfileScorePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _RiskProfileScore.Notes);
                                cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskProfileScore.HistoryPK);
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



        public void RiskProfileScore_Approved(RiskProfileScore _RiskProfileScore)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskProfileScore set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where RiskProfileScorepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskProfileScore.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskProfileScore.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RiskProfileScore set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RiskProfileScorePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskProfileScore.ApprovedUsersID);
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

        public void RiskProfileScore_Reject(RiskProfileScore _RiskProfileScore)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskProfileScore set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where RiskProfileScorepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskProfileScore.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskProfileScore.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RiskProfileScore set status= 2,LastUpdate=@LastUpdate  where RiskProfileScorePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
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

        public void RiskProfileScore_Void(RiskProfileScore _RiskProfileScore)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskProfileScore set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RiskProfileScorepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskProfileScore.RiskProfileScorePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskProfileScore.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskProfileScore.VoidUsersID);
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
                        cmd01.CommandText = @"select InvestorRiskProfile, b.DescOne InvestorRiskProfileDesc from RiskProfileScore a left join MasterValue b on a.InvestorRiskProfile = b.Code and b.Status in(1,2) 
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