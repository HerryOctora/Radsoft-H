using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class AgentCSRBegBalanceReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AgentCSRBegBalance] " +
                            "([AgentCSRBegBalancePK],[HistoryPK],[Status],[AgentPK],[FundPK],[PeriodPK],[Amount],";
        string _paramaterCommand = "@AgentPK,@FundPK,@PeriodPK,@Amount,";

        //2
        private AgentCSRBegBalance setAgentCSRBegBalance(SqlDataReader dr)
        {
            AgentCSRBegBalance M_AgentCSRBegBalance = new AgentCSRBegBalance();
            M_AgentCSRBegBalance.AgentCSRBegBalancePK = Convert.ToInt32(dr["AgentCSRBegBalancePK"]);
            M_AgentCSRBegBalance.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AgentCSRBegBalance.Status = Convert.ToInt32(dr["Status"]);
            M_AgentCSRBegBalance.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AgentCSRBegBalance.Notes = Convert.ToString(dr["Notes"]);
            M_AgentCSRBegBalance.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentCSRBegBalance.AgentID = Convert.ToString(dr["AgentID"]);
            M_AgentCSRBegBalance.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_AgentCSRBegBalance.FundID = Convert.ToString(dr["FundID"]);
            M_AgentCSRBegBalance.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_AgentCSRBegBalance.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_AgentCSRBegBalance.Amount = Convert.ToDecimal(dr["Amount"]);
            M_AgentCSRBegBalance.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AgentCSRBegBalance.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AgentCSRBegBalance.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AgentCSRBegBalance.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AgentCSRBegBalance.EntryTime = dr["EntryTime"].ToString();
            M_AgentCSRBegBalance.UpdateTime = dr["UpdateTime"].ToString();
            M_AgentCSRBegBalance.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AgentCSRBegBalance.VoidTime = dr["VoidTime"].ToString();
            M_AgentCSRBegBalance.DBUserID = dr["DBUserID"].ToString();
            M_AgentCSRBegBalance.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentCSRBegBalance.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentCSRBegBalance.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentCSRBegBalance;
        }

        public List<AgentCSRBegBalance> AgentCSRBegBalance_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentCSRBegBalance> L_AgentCSRBegBalance = new List<AgentCSRBegBalance>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID, C.ID FundID,D.ID PeriodID,A.* from AgentCSRBegBalance A left join
                            Agent B on A.AgentPK = B.AgentPK  and B.status = 2 left join
                            Fund C on A.FundPK = C.FundPK and C.status = 2 left join
                            Period D on A.PeriodPK = D.PeriodPK and D.status = 2
                            where A.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID, C.ID FundID,D.ID PeriodID,A.* from AgentCSRBegBalance A left join
                            Agent B on A.AgentPK = B.AgentPK  and B.status = 2 left join
                            Fund C on A.FundPK = C.FundPK and C.status = 2 left join
                            Period D on A.PeriodPK = D.PeriodPK and D.status = 2
                            order by AgentPK,FundPK,PeriodPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentCSRBegBalance.Add(setAgentCSRBegBalance(dr));
                                }
                            }
                            return L_AgentCSRBegBalance;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AgentCSRBegBalance_Add(AgentCSRBegBalance _AgentCSRBegBalance, bool _havePrivillege)
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
                                 "Select isnull(max(AgentCSRBegBalancePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from AgentCSRBegBalance";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentCSRBegBalance.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(AgentCSRBegBalancePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from AgentCSRBegBalance";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRBegBalance.AgentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _AgentCSRBegBalance.FundPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _AgentCSRBegBalance.PeriodPK);
                        cmd.Parameters.AddWithValue("@Amount", _AgentCSRBegBalance.Amount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AgentCSRBegBalance.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AgentCSRBegBalance");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AgentCSRBegBalance_Update(AgentCSRBegBalance _AgentCSRBegBalance, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_AgentCSRBegBalance.AgentCSRBegBalancePK, _AgentCSRBegBalance.HistoryPK, "AgentCSRBegBalance"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentCSRBegBalance set status=2, Notes=@Notes,AgentPK=@AgentPK,FundPK=@FundPK,PeriodPK=@PeriodPK,Amount=@Amount," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AgentCSRBegBalancePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRBegBalance.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                            cmd.Parameters.AddWithValue("@Notes", _AgentCSRBegBalance.Notes);
                            cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRBegBalance.AgentPK);
                            cmd.Parameters.AddWithValue("@FundPK", _AgentCSRBegBalance.FundPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _AgentCSRBegBalance.PeriodPK);
                            cmd.Parameters.AddWithValue("@Amount", _AgentCSRBegBalance.Amount);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentCSRBegBalance.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentCSRBegBalance.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentCSRBegBalance set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentCSRBegBalancePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRBegBalance.EntryUsersID);
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
                                cmd.CommandText = "Update AgentCSRBegBalance set Notes=@Notes,AgentPK=@AgentPK,FundPK=@FundPK,PeriodPK=@PeriodPK,Amount=@Amount," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where AgentCSRBegBalancePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRBegBalance.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                                cmd.Parameters.AddWithValue("@Notes", _AgentCSRBegBalance.Notes);
                                cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRBegBalance.AgentPK);
                                cmd.Parameters.AddWithValue("@FundPK", _AgentCSRBegBalance.FundPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _AgentCSRBegBalance.PeriodPK);
                                cmd.Parameters.AddWithValue("@Amount", _AgentCSRBegBalance.Amount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentCSRBegBalance.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_AgentCSRBegBalance.AgentCSRBegBalancePK, "AgentCSRBegBalance");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AgentCSRBegBalance where AgentCSRBegBalancePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRBegBalance.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _AgentCSRBegBalance.AgentPK);
                                cmd.Parameters.AddWithValue("@FundPK", _AgentCSRBegBalance.FundPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _AgentCSRBegBalance.PeriodPK);
                                cmd.Parameters.AddWithValue("@Amount", _AgentCSRBegBalance.Amount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentCSRBegBalance.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AgentCSRBegBalance set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where AgentCSRBegBalancePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _AgentCSRBegBalance.Notes);
                                cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentCSRBegBalance.HistoryPK);
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

        public void AgentCSRBegBalance_Approved(AgentCSRBegBalance _AgentCSRBegBalance)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentCSRBegBalance set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where AgentCSRBegBalancePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentCSRBegBalance.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentCSRBegBalance.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentCSRBegBalance set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentCSRBegBalancePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRBegBalance.ApprovedUsersID);
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

        public void AgentCSRBegBalance_Reject(AgentCSRBegBalance _AgentCSRBegBalance)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentCSRBegBalance set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentCSRBegBalancePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentCSRBegBalance.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRBegBalance.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentCSRBegBalance set status= 2,LastUpdate=@LastUpdate  where AgentCSRBegBalancePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
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

        public void AgentCSRBegBalance_Void(AgentCSRBegBalance _AgentCSRBegBalance)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentCSRBegBalance set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentCSRBegBalancePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentCSRBegBalance.AgentCSRBegBalancePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentCSRBegBalance.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentCSRBegBalance.VoidUsersID);
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
    }
}
