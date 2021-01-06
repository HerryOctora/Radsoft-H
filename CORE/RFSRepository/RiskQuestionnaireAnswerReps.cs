using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class RiskQuestionnaireAnswerReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[RiskQuestionnaireAnswer] " +
                            "([RiskQuestionnaireAnswerPK],[HistoryPK],[Status],[RiskQuestionnairePK],[Answer],[Score],";
        string _paramaterCommand = "@RiskQuestionnairePK,@Answer,@Score,";

        //2
        private RiskQuestionnaireAnswer setRiskQuestionnaireAnswer(SqlDataReader dr)
        {
            RiskQuestionnaireAnswer M_RiskQuestionnaireAnswer = new RiskQuestionnaireAnswer();
            M_RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK = Convert.ToInt32(dr["RiskQuestionnaireAnswerPK"]);
            M_RiskQuestionnaireAnswer.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RiskQuestionnaireAnswer.Status = Convert.ToInt32(dr["Status"]);
            M_RiskQuestionnaireAnswer.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RiskQuestionnaireAnswer.Notes = Convert.ToString(dr["Notes"]);
            M_RiskQuestionnaireAnswer.RiskQuestionnairePK = Convert.ToInt32(dr["RiskQuestionnairePK"]);
            M_RiskQuestionnaireAnswer.RiskQuestionnaireQuestion = dr["RiskQuestionnaireQuestion"].ToString();
            M_RiskQuestionnaireAnswer.Answer = dr["Answer"].ToString();
            M_RiskQuestionnaireAnswer.Score = Convert.ToInt32(dr["Score"]);
            M_RiskQuestionnaireAnswer.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RiskQuestionnaireAnswer.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RiskQuestionnaireAnswer.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RiskQuestionnaireAnswer.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RiskQuestionnaireAnswer.EntryTime = dr["EntryTime"].ToString();
            M_RiskQuestionnaireAnswer.UpdateTime = dr["UpdateTime"].ToString();
            M_RiskQuestionnaireAnswer.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RiskQuestionnaireAnswer.VoidTime = dr["VoidTime"].ToString();
            M_RiskQuestionnaireAnswer.DBUserID = dr["DBUserID"].ToString();
            M_RiskQuestionnaireAnswer.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RiskQuestionnaireAnswer.LastUpdate = dr["LastUpdate"].ToString();
            M_RiskQuestionnaireAnswer.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RiskQuestionnaireAnswer;
        }

        public List<RiskQuestionnaireAnswer> RiskQuestionnaireAnswer_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RiskQuestionnaireAnswer> L_RiskQuestionnaireAnswer = new List<RiskQuestionnaireAnswer>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END 
                                        StatusDesc,case when InvestorType = 1 Then 'INDIVIDU' else 'INSTITUSI' End +' - '+G.Question RiskQuestionnaireQuestion,A.* from RiskQuestionnaireAnswer A 
                                        LEFT join dbo.RiskQuestionnaire G on A.RiskQuestionnairePK = G.RiskQuestionnairePK and G.status = 2  
                                        where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END 
                                        StatusDesc,case when InvestorType = 1 Then 'INDIVIDU' else 'INSTITUSI' End +' - '+G.Question RiskQuestionnaireQuestion,A.* FROM RiskQuestionnaireAnswer A 
                                        LEFT join dbo.RiskQuestionnaire G on A.RiskQuestionnairePK = G.RiskQuestionnairePK and G.status = 2
                                        order by RiskQuestionnairePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RiskQuestionnaireAnswer.Add(setRiskQuestionnaireAnswer(dr));
                                }
                            }
                            return L_RiskQuestionnaireAnswer;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RiskQuestionnaireAnswer_Add(RiskQuestionnaireAnswer _RiskQuestionnaireAnswer, bool _havePrivillege)
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
                                 "Select isnull(max(RiskQuestionnaireAnswerPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from RiskQuestionnaireAnswer";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskQuestionnaireAnswer.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(RiskQuestionnaireAnswerPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from RiskQuestionnaireAnswer";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnaireAnswer.RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@Answer", _RiskQuestionnaireAnswer.Answer);
                        cmd.Parameters.AddWithValue("@Score", _RiskQuestionnaireAnswer.Score);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RiskQuestionnaireAnswer.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RiskQuestionnaireAnswer");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RiskQuestionnaireAnswer_Update(RiskQuestionnaireAnswer _RiskQuestionnaireAnswer, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK, _RiskQuestionnaireAnswer.HistoryPK, "RiskQuestionnaireAnswer");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RiskQuestionnaireAnswer set status=2, Notes=@Notes,RiskQuestionnairePK=@RiskQuestionnairePK,Answer=@Answer,Score=@Score," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RiskQuestionnaireAnswerPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaireAnswer.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                            cmd.Parameters.AddWithValue("@Notes", _RiskQuestionnaireAnswer.Notes);
                            cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnaireAnswer.RiskQuestionnairePK);
                            cmd.Parameters.AddWithValue("@Answer", _RiskQuestionnaireAnswer.Answer);
                            cmd.Parameters.AddWithValue("@Score", _RiskQuestionnaireAnswer.Score);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskQuestionnaireAnswer.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskQuestionnaireAnswer.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RiskQuestionnaireAnswer set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RiskQuestionnaireAnswerPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaireAnswer.EntryUsersID);
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
                                cmd.CommandText = "Update RiskQuestionnaireAnswer set Notes=@Notes,RiskQuestionnairePK=@RiskQuestionnairePK,Answer=@Answer,Score=@Score," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RiskQuestionnaireAnswerPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaireAnswer.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                                cmd.Parameters.AddWithValue("@Notes", _RiskQuestionnaireAnswer.Notes);
                                cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnaireAnswer.RiskQuestionnairePK);
                                cmd.Parameters.AddWithValue("@Answer", _RiskQuestionnaireAnswer.Answer);
                                cmd.Parameters.AddWithValue("@Score", _RiskQuestionnaireAnswer.Score);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskQuestionnaireAnswer.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK, "RiskQuestionnaireAnswer");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RiskQuestionnaireAnswer where RiskQuestionnaireAnswerPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaireAnswer.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnaireAnswer.RiskQuestionnairePK);
                                cmd.Parameters.AddWithValue("@Answer", _RiskQuestionnaireAnswer.Answer);
                                cmd.Parameters.AddWithValue("@Score", _RiskQuestionnaireAnswer.Score);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskQuestionnaireAnswer.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RiskQuestionnaireAnswer set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where RiskQuestionnaireAnswerPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _RiskQuestionnaireAnswer.Notes);
                                cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaireAnswer.HistoryPK);
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

        public void RiskQuestionnaireAnswer_Approved(RiskQuestionnaireAnswer _RiskQuestionnaireAnswer)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskQuestionnaireAnswer set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where RiskQuestionnaireAnswerPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskQuestionnaireAnswer.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskQuestionnaireAnswer.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RiskQuestionnaireAnswer set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RiskQuestionnaireAnswerPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaireAnswer.ApprovedUsersID);
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

        public void RiskQuestionnaireAnswer_Reject(RiskQuestionnaireAnswer _RiskQuestionnaireAnswer)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskQuestionnaireAnswer set status = 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate " +
                            "where RiskQuestionnaireAnswerPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskQuestionnaireAnswer.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaireAnswer.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RiskQuestionnaireAnswer set status= 2,LastUpdate=@LastUpdate where RiskQuestionnaireAnswerPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
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

        public void RiskQuestionnaireAnswer_Void(RiskQuestionnaireAnswer _RiskQuestionnaireAnswer)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskQuestionnaireAnswer set status = 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate " +
                            "where RiskQuestionnaireAnswerPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskQuestionnaireAnswer.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaireAnswer.VoidUsersID);
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

        private RiskQuestionnaireAnswer setQuestion(SqlDataReader dr)
        {
            RiskQuestionnaireAnswer M_RiskQuestionnaireAnswer = new RiskQuestionnaireAnswer();
            M_RiskQuestionnaireAnswer.RiskQuestionnaireAnswerPK = Convert.ToInt32(dr["RiskQuestionnaireAnswerPK"]);
            M_RiskQuestionnaireAnswer.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_RiskQuestionnaireAnswer.RiskQuestionnairePK = Convert.ToInt32(dr["RiskQuestionnairePK"]);
            M_RiskQuestionnaireAnswer.Answer = dr["Answer"].ToString();
            M_RiskQuestionnaireAnswer.Score = Convert.ToInt32(dr["Score"]);
            M_RiskQuestionnaireAnswer.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RiskQuestionnaireAnswer.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RiskQuestionnaireAnswer.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RiskQuestionnaireAnswer.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RiskQuestionnaireAnswer.EntryTime = dr["EntryTime"].ToString();
            M_RiskQuestionnaireAnswer.UpdateTime = dr["UpdateTime"].ToString();
            M_RiskQuestionnaireAnswer.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RiskQuestionnaireAnswer.VoidTime = dr["VoidTime"].ToString();
            M_RiskQuestionnaireAnswer.DBUserID = dr["DBUserID"].ToString();
            M_RiskQuestionnaireAnswer.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RiskQuestionnaireAnswer.LastUpdate = dr["LastUpdate"].ToString();
            M_RiskQuestionnaireAnswer.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RiskQuestionnaireAnswer;
        }

        public List<RiskQuestionnaireAnswer> RiskQuestionnaire_selectAnswer(int _RiskQuestionnairePK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RiskQuestionnaireAnswer> L_RiskQuestionnaireAnswer = new List<RiskQuestionnaireAnswer>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select ROW_NUMBER() over (order by RiskQuestionnaireAnswerPK) AutoNo,* from RiskQuestionnaireAnswer where status = 2 and RiskQuestionnairePK = @RiskQuestionnairePK order by RiskQuestionnaireAnswerPK";
                        cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnairePK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RiskQuestionnaireAnswer.Add(setQuestion(dr));
                                }
                            }
                            return L_RiskQuestionnaireAnswer;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string InsertFromFundClient(List<FundClientRiskQuestionnaire> _FundClientRiskQuestionnaire)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    foreach (var _obj in _FundClientRiskQuestionnaire)
                    {
                        FundClientRiskQuestionnaire _m = new FundClientRiskQuestionnaire();
                        _m.RiskQuestionnairePK = _obj.RiskQuestionnairePK;
                        _m.RiskQuestionnaireAnswerPK = _obj.RiskQuestionnaireAnswerPK;
                        _m.FundClientPK = _obj.FundClientPK;

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            cmd.CommandText = @"
                                                
                                            if not exists (select * from FundClientRiskQuestionnaire where FundClientPK = @FundClientPK and RiskQuestionnairePK = @RiskQuestionnairePK)
                                            begin
	                                            insert into FundClientRiskQuestionnaire (FundClientPK,RiskQuestionnairePK,RiskQuestionnaireAnswerPK)
	                                            select @FundClientPK,@RiskQuestionnairePK,@RiskQuestionnaireAnswerPK
                                            end
                                            else
                                            begin
	                                            update FundClientRiskQuestionnaire set RiskQuestionnaireAnswerPK = @RiskQuestionnaireAnswerPK
	                                            where FundClientPK = @FundClientPK and RiskQuestionnairePK = @RiskQuestionnairePK
                                            end

                                          ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _m.RiskQuestionnairePK);
                            cmd.Parameters.AddWithValue("@RiskQuestionnaireAnswerPK", _m.RiskQuestionnaireAnswerPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _m.FundClientPK);
                            cmd.ExecuteNonQuery();

                        }
                    }
                    return "";
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }




    }
}