using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class AgentCSRFundReps
    {

        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AgentCSRFund] " +
                            "([AgentCSRFundPK],[HistoryPK],[Status],[AgentPK],[FundPK],[Description],[Date],[FeeType],[FundJournalAccountPK],[DanaProgramPercent],[PPH23Percent],[AgentCSRExpenseAccountPK],";

        string _paramaterCommand = "@AgentPK,@FundPK,@Description,@Date,@FeeType,@FundJournalAccountPK,@DanaProgramPercent,@PPH23Percent,@AgentCSRExpenseAccountPK,";



        //2
        private AgentCSRFund setAgentCSRFund(SqlDataReader dr)
        {
            AgentCSRFund M_AgentCSRFund = new AgentCSRFund();

            M_AgentCSRFund.AgentCSRFundPK = Convert.ToInt32(dr["AgentCSRFundPK"]);
            M_AgentCSRFund.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AgentCSRFund.Status = Convert.ToInt32(dr["Status"]);
            M_AgentCSRFund.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AgentCSRFund.Notes = Convert.ToString(dr["Notes"]);
            M_AgentCSRFund.Date = Convert.ToDateTime(dr["Date"]);
            M_AgentCSRFund.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentCSRFund.AgentID = Convert.ToString(dr["AgentID"]);
            M_AgentCSRFund.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_AgentCSRFund.FundID = Convert.ToString(dr["FundID"]);
            M_AgentCSRFund.Description = Convert.ToString(dr["Description"]);
            M_AgentCSRFund.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_AgentCSRFund.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);

            M_AgentCSRFund.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_AgentCSRFund.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_AgentCSRFund.DanaProgramPercent = Convert.ToDecimal(dr["DanaProgramPercent"]);
            M_AgentCSRFund.PPH23Percent = Convert.ToDecimal(dr["PPH23Percent"]);
            M_AgentCSRFund.AgentCSRExpenseAccountPK = Convert.ToInt32(dr["AgentCSRExpenseAccountPK"]);
            M_AgentCSRFund.AgentCSRExpenseAccountName = Convert.ToString(dr["AgentCSRExpenseAccountName"]);
            M_AgentCSRFund.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AgentCSRFund.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AgentCSRFund.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AgentCSRFund.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AgentCSRFund.EntryTime = dr["EntryTime"].ToString();
            M_AgentCSRFund.UpdateTime = dr["UpdateTime"].ToString();
            M_AgentCSRFund.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AgentCSRFund.VoidTime = dr["VoidTime"].ToString();
            M_AgentCSRFund.DBUserID = dr["DBUserID"].ToString();
            M_AgentCSRFund.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentCSRFund.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentCSRFund.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentCSRFund;
        }

        public List<AgentCSRFund> AgentCSRFund_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {


                    DbCon.Open();
                    List<AgentCSRFund> L_AgentCSRFund = new List<AgentCSRFund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID, C.ID FundID,A.Description,case when FeeType = 1 then 'Management Fee' else 'AUM' end FeeTypeDesc ,D.Name FundJournalAccountName,E.Name AgentCSRExpenseAccountName,A.* from AgentCSRFund A 
                            left join Agent B on A.AgentPK = B.AgentPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2)
                            left join Account E on A.AgentCSRExpenseAccountPK = E.AccountPK and E.status in (1,2)
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {

                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID, C.ID FundID,A.Description,case when FeeType = 1 then 'Management Fee' else 'AUM' end FeeTypeDesc ,D.Name FundJournalAccountName,E.Name AgentCSRExpenseAccountName,A.* from AgentCSRFund A 
                            left join Agent B on A.AgentPK = B.AgentPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status in (1,2)
                            left join Account E on A.AgentCSRExpenseAccountPK = E.AccountPK and E.status in (1,2)
                            order by AgentPK,FundPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentCSRFund.Add(setAgentCSRFund(dr));
                                }
                            }
                            return L_AgentCSRFund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AgentCSRFund_Add(AgentCSRFund _AgentCSRFund, bool _havePrivillege)
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
                                 "Select isnull(max(AgentCSRFundPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from AgentCSRFund";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentCSRFund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(AgentCSRFundPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from AgentCSRFund";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _AgentCSRFund.Date);
                        cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRFund.AgentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _AgentCSRFund.FundPK);
                        cmd.Parameters.AddWithValue("@Description", _AgentCSRFund.Description);
                        cmd.Parameters.AddWithValue("@FeeType", _AgentCSRFund.FeeType);

                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _AgentCSRFund.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@DanaProgramPercent", _AgentCSRFund.DanaProgramPercent);
                        cmd.Parameters.AddWithValue("@PPH23Percent", _AgentCSRFund.PPH23Percent);
                        cmd.Parameters.AddWithValue("@AgentCSRExpenseAccountPK", _AgentCSRFund.AgentCSRExpenseAccountPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AgentCSRFund.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AgentCSRFund");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AgentCSRFund_Update(AgentCSRFund _AgentCSRFund, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_AgentCSRFund.AgentCSRFundPK, _AgentCSRFund.HistoryPK, "AgentCSRFund");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentCSRFund set status=2, Notes=@Notes,Date=@Date,AgentPK=@AgentPK,FundPK=@FundPK,Description=@Description,FeeType=@FeeType,FundJournalAccountPK=@FundJournalAccountPK,DanaProgramPercent=@DanaProgramPercent,PPH23Percent=@PPH23Percent,AgentCSRExpenseAccountPK= @AgentCSRExpenseAccountPK,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AgentCSRFundPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRFund.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                            cmd.Parameters.AddWithValue("@Notes", _AgentCSRFund.Notes);
                            cmd.Parameters.AddWithValue("@Date", _AgentCSRFund.Date);
                            cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRFund.AgentPK);
                            cmd.Parameters.AddWithValue("@FundPK", _AgentCSRFund.FundPK);
                            cmd.Parameters.AddWithValue("@Description", _AgentCSRFund.Description);
                            cmd.Parameters.AddWithValue("@FeeType", _AgentCSRFund.FeeType);

                            cmd.Parameters.AddWithValue("@FundJournalAccountPK", _AgentCSRFund.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@DanaProgramPercent", _AgentCSRFund.DanaProgramPercent);
                            cmd.Parameters.AddWithValue("@PPH23Percent", _AgentCSRFund.PPH23Percent);
                            cmd.Parameters.AddWithValue("@AgentCSRExpenseAccountPK", _AgentCSRFund.AgentCSRExpenseAccountPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentCSRFund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentCSRFund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentCSRFund set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentCSRFundPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRFund.EntryUsersID);
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
                                cmd.CommandText = "Update AgentCSRFund set Notes=@Notes,Date=@Date,AgentPK=@AgentPK,FundPK=@FundPK,Description=@Description,FeeType=@FeeType,FundJournalAccountPK=@FundJournalAccountPK,DanaProgramPercent=@DanaProgramPercent,PPH23Percent=@PPH23Percent,AgentCSRExpenseAccountPK=@AgentCSRExpenseAccountPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AgentCSRFundPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRFund.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                                cmd.Parameters.AddWithValue("@Notes", _AgentCSRFund.Notes);
                                cmd.Parameters.AddWithValue("@Date", _AgentCSRFund.Date);
                                cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRFund.AgentPK);
                                cmd.Parameters.AddWithValue("@FundPK", _AgentCSRFund.FundPK);
                                cmd.Parameters.AddWithValue("@Description", _AgentCSRFund.Description);
                                cmd.Parameters.AddWithValue("@FeeType", _AgentCSRFund.FeeType);

                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _AgentCSRFund.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@DanaProgramPercent", _AgentCSRFund.DanaProgramPercent);
                                cmd.Parameters.AddWithValue("@PPH23Percent", _AgentCSRFund.PPH23Percent);
                                cmd.Parameters.AddWithValue("@AgentCSRExpenseAccountPK", _AgentCSRFund.AgentCSRExpenseAccountPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentCSRFund.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_AgentCSRFund.AgentCSRFundPK, "AgentCSRFund");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From AgentCSRFund where AgentCSRFundPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRFund.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _AgentCSRFund.Date);
                                cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRFund.AgentPK);
                                cmd.Parameters.AddWithValue("@FundPK", _AgentCSRFund.FundPK);
                                cmd.Parameters.AddWithValue("@Description", _AgentCSRFund.Description);
                                cmd.Parameters.AddWithValue("@FeeType", _AgentCSRFund.FeeType);

                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _AgentCSRFund.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@DanaProgramPercent", _AgentCSRFund.DanaProgramPercent);
                                cmd.Parameters.AddWithValue("@PPH23Percent", _AgentCSRFund.PPH23Percent);
                                cmd.Parameters.AddWithValue("@AgentCSRExpenseAccountPK", _AgentCSRFund.AgentCSRExpenseAccountPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentCSRFund.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AgentCSRFund set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where AgentCSRFundPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _AgentCSRFund.Notes);
                                cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRFund.HistoryPK);
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

        public void AgentCSRFund_Approved(AgentCSRFund _AgentCSRFund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentCSRFund set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where AgentCSRFundpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentCSRFund.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentCSRFund.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentCSRFund set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentCSRFundPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRFund.ApprovedUsersID);
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

        public void AgentCSRFund_Reject(AgentCSRFund _AgentCSRFund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentCSRFund set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where AgentCSRFundpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentCSRFund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRFund.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentCSRFund set status= 2,LastUpdate=@LastUpdate  where AgentCSRFundPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
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

        public void AgentCSRFund_Void(AgentCSRFund _AgentCSRFund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentCSRFund set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentCSRFundpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRFund.AgentCSRFundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentCSRFund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRFund.VoidUsersID);
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

        public string AgentCSRFund_ValidateAgentAndFund(int _AgentPK, int _FundPK, string _DateFrom)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            @"

                            if exists ( select * from AgentCSRFund where AgentPK = @AgentPK and date = @Date and status in (1,2))
                            BEGIN
	                            SELECT 'This agent has been input on this date, please check again!' Result
                            END
                            ELSE
                            BEGIN
	                            SELECT '' Result
                            END

";

                        cmd.Parameters.AddWithValue("@AgentPK", _AgentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundPK);
                        cmd.Parameters.AddWithValue("@Date", _DateFrom);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"].ToString());

                            }
                            return "";
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
