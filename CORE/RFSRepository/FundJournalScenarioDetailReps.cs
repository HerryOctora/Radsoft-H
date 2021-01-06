using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class FundJournalScenarioDetailReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundJournalScenarioDetail] " +
                            "([HistoryPK],[FundJournalScenarioPK],[Status],[FundJournalAccountPK],[DebitCredit],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@FundJournalAccountPK,@DebitCredit,@LastUsersID,@LastUpdate";

        //2
        private FundJournalScenarioDetail setFundJournalScenarioDetail(SqlDataReader dr)
        {
            FundJournalScenarioDetail M_FundJournalScenarioDetail = new FundJournalScenarioDetail();
            M_FundJournalScenarioDetail.FundJournalScenarioPK = Convert.ToInt32(dr["FundJournalScenarioPK"]);
            M_FundJournalScenarioDetail.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundJournalScenarioDetail.Status = Convert.ToInt32(dr["Status"]);
            M_FundJournalScenarioDetail.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_FundJournalScenarioDetail.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_FundJournalScenarioDetail.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_FundJournalScenarioDetail.DebitCredit = Convert.ToString(dr["DebitCredit"]);
            M_FundJournalScenarioDetail.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_FundJournalScenarioDetail.LastUpdate = dr["LastUpdate"].ToString();
            M_FundJournalScenarioDetail.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundJournalScenarioDetail;
        }

        public List<FundJournalScenarioDetail> FundJournalScenarioDetail_Select(int _status, int _FundJournalScenarioPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalScenarioDetail> L_FundJournalScenarioDetail = new List<FundJournalScenarioDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select a.ID FundJournalAccountID, a.Name FundJournalAccountName, " +
                             " jd.* from FundJournalScenarioDetail jd Left join     " +
                             " FundJournalAccount a on jd.FundJournalAccountPK = a.FundJournalAccountPK and a.Status = 2 " +
                             " where FundJournalScenarioPK = @FundJournalScenarioPK and jd.Status = @Status " +
                             "";
                            cmd.Parameters.AddWithValue("@FundJournalScenarioPK", _FundJournalScenarioPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " Select a.ID FundJournalAccountID, a.Name FundJournalAccountName, " +
                             " jd.* from FundJournalScenarioDetail jd Left join     " +
                             " FundJournalAccount a on jd.FundJournalAccountPK = a.FundJournalAccountPK and a.Status = 2 " +
                             " where FundJournalScenarioPK = @FundJournalScenarioPK ";
                            cmd.Parameters.AddWithValue("@FundJournalScenarioPK", _FundJournalScenarioPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundJournalScenarioDetail.Add(setFundJournalScenarioDetail(dr));
                                }
                            }
                            return L_FundJournalScenarioDetail;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundJournalScenarioDetail_Add(FundJournalScenarioDetail _FundJournalScenarioDetail)
        {
            try
            {
        
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                                  "update FundJournalScenario set lastupdate = @Lastupdate where FundJournalScenarioPK = @FundJournalScenarioPK and status = 1 \n " +
                                  "update FundJournalScenario set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where FundJournalScenarioPK = @FundJournalScenarioPK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,@FundJournalScenarioPK,@status," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@FundJournalScenarioPK", _FundJournalScenarioDetail.FundJournalScenarioPK);
                        cmd.Parameters.AddWithValue("@status", _FundJournalScenarioDetail.Status);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FundJournalScenarioDetail.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@DebitCredit", _FundJournalScenarioDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FundJournalScenarioDetail.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();
                        return 0;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Update
        public void FundJournalScenarioDetail_Update(FundJournalScenarioDetail _FundJournalScenarioDetail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update FundJournalScenario set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where FundJournalScenarioPK = @FundJournalScenarioPK and status = 2 \n " +
                            "Update FundJournalScenarioDetail " +
                            "Set FundJournalAccountPK = @FundJournalAccountPK,DebitCredit = @DebitCredit, " +
                            "LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                            "Where FundJournalScenarioPK = @FundJournalScenarioPK ";

                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FundJournalScenarioDetail.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@DebitCredit", _FundJournalScenarioDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@FundJournalScenarioPK", _FundJournalScenarioDetail.FundJournalScenarioPK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FundJournalScenarioDetail.LastUsersID);
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

        public void FundJournalScenarioDetail_Delete(FundJournalScenarioDetail _FundJournalScenarioDetail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            "update FundJournalScenario set  lastupdate = @Lastupdate where FundJournalScenarioPK = @FundJournalScenarioPK and status = 1 \n " +
                            "update FundJournalScenario set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where FundJournalScenarioPK = @FundJournalScenarioPK and status = 2 \n " +
                            "delete FundJournalScenarioDetail where FundJournalScenarioPK = @FundJournalScenarioPK ";
                        cmd.Parameters.AddWithValue("@FundJournalScenarioPK", _FundJournalScenarioDetail.FundJournalScenarioPK);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FundJournalScenarioDetail.LastUsersID);
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