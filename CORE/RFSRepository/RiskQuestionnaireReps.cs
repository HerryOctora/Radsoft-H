using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class RiskQuestionnaireReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[RiskQuestionnaire] " +
                            "([RiskQuestionnairePK],[HistoryPK],[Status],[PriorityNo],[Question],[BitIsEnabled],[InvestorType],";
        string _paramaterCommand = "@PriorityNo,@Question,@BitIsEnabled,@InvestorType,";

        //2
        private RiskQuestionnaire setRiskQuestionnaire(SqlDataReader dr)
        {
            RiskQuestionnaire M_RiskQuestionnaire = new RiskQuestionnaire();
            M_RiskQuestionnaire.RiskQuestionnairePK = Convert.ToInt32(dr["RiskQuestionnairePK"]);
            M_RiskQuestionnaire.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RiskQuestionnaire.Status = Convert.ToInt32(dr["Status"]);
            M_RiskQuestionnaire.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RiskQuestionnaire.Notes = Convert.ToString(dr["Notes"]);
            M_RiskQuestionnaire.PriorityNo = Convert.ToInt32(dr["PriorityNo"]);
            M_RiskQuestionnaire.Question = dr["Question"].ToString();
            M_RiskQuestionnaire.BitIsEnabled = Convert.ToBoolean(dr["BitIsEnabled"]);
            M_RiskQuestionnaire.InvestorType = Convert.ToInt32(dr["InvestorType"]);
            M_RiskQuestionnaire.InvestorTypeDesc = dr["InvestorTypeDesc"].ToString();
            M_RiskQuestionnaire.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RiskQuestionnaire.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RiskQuestionnaire.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RiskQuestionnaire.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RiskQuestionnaire.EntryTime = dr["EntryTime"].ToString();
            M_RiskQuestionnaire.UpdateTime = dr["UpdateTime"].ToString();
            M_RiskQuestionnaire.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RiskQuestionnaire.VoidTime = dr["VoidTime"].ToString();
            M_RiskQuestionnaire.DBUserID = dr["DBUserID"].ToString();
            M_RiskQuestionnaire.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RiskQuestionnaire.LastUpdate = dr["LastUpdate"].ToString();
            M_RiskQuestionnaire.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);

            return M_RiskQuestionnaire;
        }

        public List<RiskQuestionnaire> RiskQuestionnaire_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RiskQuestionnaire> L_RiskQuestionnaire = new List<RiskQuestionnaire>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,case when InvestorType = 1 then 'INDIVIDU' else 'INSTITUSI' end InvestorTypeDesc,* from RiskQuestionnaire where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,case when InvestorType = 1 then 'INDIVIDU' else 'INSTITUSI' end InvestorTypeDesc,* from RiskQuestionnaire";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RiskQuestionnaire.Add(setRiskQuestionnaire(dr));
                                }
                            }
                            return L_RiskQuestionnaire;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RiskQuestionnaire_Add(RiskQuestionnaire _RiskQuestionnaire, bool _havePrivillege)
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
                                 "Select isnull(max(RiskQuestionnairePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from RiskQuestionnaire";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskQuestionnaire.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(RiskQuestionnairePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from RiskQuestionnaire";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PriorityNo", _RiskQuestionnaire.PriorityNo);
                        cmd.Parameters.AddWithValue("@Question", _RiskQuestionnaire.Question);
                        cmd.Parameters.AddWithValue("@BitIsEnabled", _RiskQuestionnaire.BitIsEnabled);
                        cmd.Parameters.AddWithValue("@InvestorType", _RiskQuestionnaire.InvestorType);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RiskQuestionnaire.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RiskQuestionnaire");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RiskQuestionnaire_Update(RiskQuestionnaire _RiskQuestionnaire, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_RiskQuestionnaire.RiskQuestionnairePK, _RiskQuestionnaire.HistoryPK, "RiskQuestionnaire");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RiskQuestionnaire set status=2, Notes=@Notes,PriorityNo=@PriorityNo,Question=@Question,BitIsEnabled=@BitIsEnabled,InvestorType=@InvestorType," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RiskQuestionnairePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaire.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                            cmd.Parameters.AddWithValue("@Notes", _RiskQuestionnaire.Notes);
                            cmd.Parameters.AddWithValue("@PriorityNo", _RiskQuestionnaire.PriorityNo);
                            cmd.Parameters.AddWithValue("@Question", _RiskQuestionnaire.Question);
                            cmd.Parameters.AddWithValue("@BitIsEnabled", _RiskQuestionnaire.BitIsEnabled);
                            cmd.Parameters.AddWithValue("@InvestorType", _RiskQuestionnaire.InvestorType);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskQuestionnaire.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskQuestionnaire.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RiskQuestionnaire set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RiskQuestionnairePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaire.EntryUsersID);
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
                                cmd.CommandText = "Update RiskQuestionnaire set Notes=@Notes,PriorityNo=@PriorityNo,Question=@Question,BitIsEnabled=@BitIsEnabled,InvestorType=@InvestorType," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where RiskQuestionnairePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaire.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                                cmd.Parameters.AddWithValue("@Notes", _RiskQuestionnaire.Notes);
                                cmd.Parameters.AddWithValue("@PriorityNo", _RiskQuestionnaire.PriorityNo);
                                cmd.Parameters.AddWithValue("@Question", _RiskQuestionnaire.Question);
                                cmd.Parameters.AddWithValue("@BitIsEnabled", _RiskQuestionnaire.BitIsEnabled);
                                cmd.Parameters.AddWithValue("@InvestorType", _RiskQuestionnaire.InvestorType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskQuestionnaire.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_RiskQuestionnaire.RiskQuestionnairePK, "RiskQuestionnaire");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RiskQuestionnaire where RiskQuestionnairePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaire.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@PriorityNo", _RiskQuestionnaire.PriorityNo);
                                cmd.Parameters.AddWithValue("@Question", _RiskQuestionnaire.Question);
                                cmd.Parameters.AddWithValue("@BitIsEnabled", _RiskQuestionnaire.BitIsEnabled);
                                cmd.Parameters.AddWithValue("@InvestorType", _RiskQuestionnaire.InvestorType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskQuestionnaire.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RiskQuestionnaire set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where RiskQuestionnairePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _RiskQuestionnaire.Notes);
                                cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RiskQuestionnaire.HistoryPK);
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

        public void RiskQuestionnaire_Approved(RiskQuestionnaire _RiskQuestionnaire)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskQuestionnaire set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where RiskQuestionnairePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskQuestionnaire.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RiskQuestionnaire.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RiskQuestionnaire set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RiskQuestionnairePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaire.ApprovedUsersID);
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

        public void RiskQuestionnaire_Reject(RiskQuestionnaire _RiskQuestionnaire)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskQuestionnaire set status = 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate " +
                            "where RiskQuestionnairePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskQuestionnaire.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaire.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RiskQuestionnaire set status= 2,LastUpdate=@LastUpdate where RiskQuestionnairePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
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

        public void RiskQuestionnaire_Void(RiskQuestionnaire _RiskQuestionnaire)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RiskQuestionnaire set status = 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate " +
                            "where RiskQuestionnairePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RiskQuestionnaire.RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@historyPK", _RiskQuestionnaire.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RiskQuestionnaire.VoidUsersID);
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

        public List<RiskQuestionnaireCombo> RiskQuestionnaire_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RiskQuestionnaireCombo> L_RiskQuestionnaire = new List<RiskQuestionnaireCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT RiskQuestionnairePK,case when InvestorType = 1 Then 'INDIVIDU' else 'INSTITUSI' End +' - '+ Question Question FROM [RiskQuestionnaire] WHERE status = 2 order by RiskQuestionnairePK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    RiskQuestionnaireCombo M_RiskQuestionnaire = new RiskQuestionnaireCombo();
                                    M_RiskQuestionnaire.RiskQuestionnairePK = Convert.ToInt32(dr["RiskQuestionnairePK"]);
                                    M_RiskQuestionnaire.Question = Convert.ToString(dr["Question"]);

                                    L_RiskQuestionnaire.Add(M_RiskQuestionnaire);
                                }

                            }
                            return L_RiskQuestionnaire;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private RiskQuestionnaire setQuestion(SqlDataReader dr)
        {
            RiskQuestionnaire M_RiskQuestionnaire = new RiskQuestionnaire();
            M_RiskQuestionnaire.No = Convert.ToInt32(dr["No"]);
            M_RiskQuestionnaire.RiskQuestionnairePK = Convert.ToInt32(dr["RiskQuestionnairePK"]);
            M_RiskQuestionnaire.RiskQuestionnaireAnswerPK = Convert.ToInt32(dr["RiskQuestionnaireAnswerPK"]);
            M_RiskQuestionnaire.Question = dr["Question"].ToString();
            M_RiskQuestionnaire.Answer = dr["Answer"].ToString();
            return M_RiskQuestionnaire;
        }

        public List<RiskQuestionnaire> RiskQuestionnaire_selectQuestion(int _clientCategory, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RiskQuestionnaire> L_RiskQuestionnaire = new List<RiskQuestionnaire>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @tempTable table (
	                            Answer nvarchar(500),
	                            Question nvarchar(500),
	                            RiskQuestionnairePK int,
								RiskQuestionnaireAnswerPK int
                            )

                            insert into @tempTable (Question,RiskQuestionnairePK,RiskQuestionnaireAnswerPK)
                            select Question,A.RiskQuestionnairePK,0 from RiskQuestionnaire A
                            where A.status = 2 and A.InvestorType = @ClientCategory
                            order by PriorityNo

                            if not exists (select * from FundClientRiskQuestionnaire where FundClientPK = @FundClientPK )
                            begin
	                            select ROW_NUMBER() over ( order by A.RiskQuestionnairePK) No,'' Answer,A.RiskQuestionnairePK,Question,0 RiskQuestionnaireAnswerPK from RiskQuestionnaire A
                                where A.status = 2 and A.InvestorType = @ClientCategory
                                order by PriorityNo
                            end
                            else
                            begin
	                            update A set Answer = B.Answer, A.RiskQuestionnaireAnswerPK = B.RiskQuestionnaireAnswerPK
	                            from @tempTable A
	                            left join 
	                            (
	                            select isnull(C.Answer,'') Answer,A.RiskQuestionnairePK,Question,B.RiskQuestionnaireAnswerPK from RiskQuestionnaire A
                                left join FundClientRiskQuestionnaire B on A.RiskQuestionnairePK = B.RiskQuestionnairePK
                                left join RiskQuestionnaireAnswer C on B.RiskQuestionnaireAnswerPK = C.RiskQuestionnaireAnswerPK and C.Status = 2
                                where A.status = 2 and A.InvestorType = @ClientCategory and B.FundClientPK = @FundClientPK
                                ) B on A.RiskQuestionnairePK = B.RiskQuestionnairePK

	                            select ROW_NUMBER() over ( order by RiskQuestionnairePK) No,isnull(Answer,'') Answer, isnull(Question,'') Question, RiskQuestionnairePK, RiskQuestionnaireAnswerPK from @tempTable
                            end
                        ";
                        cmd.Parameters.AddWithValue("@ClientCategory", _clientCategory);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RiskQuestionnaire.Add(setQuestion(dr));
                                }
                            }
                            return L_RiskQuestionnaire;
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