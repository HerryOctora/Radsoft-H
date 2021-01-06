using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;



namespace RFSRepository
{
    public class FundJournalScenarioReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundJournalScenario] " +
                            "([FundJournalScenarioPK],[HistoryPK],[Status],[FundPK],[Scenario],";

        string _paramaterCommand = "@FundPK,@Scenario,";



        //2
        private FundJournalScenario setFundJournalScenario(SqlDataReader dr)
        {
            FundJournalScenario M_FundJournalScenario = new FundJournalScenario();
            M_FundJournalScenario.FundJournalScenarioPK = Convert.ToInt32(dr["FundJournalScenarioPK"]);
            M_FundJournalScenario.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundJournalScenario.Status = Convert.ToInt32(dr["Status"]);
            M_FundJournalScenario.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundJournalScenario.Notes = Convert.ToString(dr["Notes"]);
            M_FundJournalScenario.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundJournalScenario.FundID = Convert.ToString(dr["FundID"]);
            M_FundJournalScenario.Scenario = Convert.ToInt32(dr["Scenario"]);
            M_FundJournalScenario.ScenarioDesc = Convert.ToString(dr["ScenarioDesc"]);
            M_FundJournalScenario.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundJournalScenario.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundJournalScenario.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundJournalScenario.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundJournalScenario.EntryTime = dr["EntryTime"].ToString();
            M_FundJournalScenario.UpdateTime = dr["UpdateTime"].ToString();
            M_FundJournalScenario.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundJournalScenario.VoidTime = dr["VoidTime"].ToString();
            M_FundJournalScenario.DBUserID = dr["DBUserID"].ToString();
            M_FundJournalScenario.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundJournalScenario.LastUpdate = dr["LastUpdate"].ToString();
            M_FundJournalScenario.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundJournalScenario;
        }

        //3
        public List<FundJournalScenario> FundJournalScenario_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalScenario> L_FundJournalScenario = new List<FundJournalScenario>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,B.DescOne ScenarioDesc,* From FundJournalScenario A Left join " +
                                "MasterValue B on A.Scenario = B.Code and B.Id = 'FundJournalScenario' and B.status = 2  " +
                                "Where A.Status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,B.DescOne ScenarioDesc,* From FundJournalScenario A Left join " +
                                "MasterValue B on A.Scenario = B.Code and B.Id = 'FundJournalScenario' and B.status = 2  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundJournalScenario.Add(setFundJournalScenario(dr));
                                }
                            }
                            return L_FundJournalScenario;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

       

        public FundJournalScenarioAddNew FundJournalScenario_Add(FundJournalScenario _FundJournalScenario, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newFundJournalScenarioPK int \n " +
                                 "Select @newFundJournalScenarioPK = isnull(max(FundJournalScenarioPk),0) + 1 from FundJournalScenario \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate) " +
                                 "Select @newFundJournalScenarioPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newFundJournalScenarioPK newFundJournalScenarioPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundJournalScenario.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newFundJournalScenarioPK int \n " +
                                 "Select @newFundJournalScenarioPK = isnull(max(FundJournalScenarioPk),0) + 1 from FundJournalScenario \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newFundJournalScenarioPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newFundJournalScenarioPK newFundJournalScenarioPK, 1 newHistoryPK ";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FundJournalScenario.FundPK);
                        cmd.Parameters.AddWithValue("@Scenario", _FundJournalScenario.Scenario);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundJournalScenario.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new FundJournalScenarioAddNew()
                                {
                                    FundJournalScenarioPK = Convert.ToInt32(dr["newFundJournalScenarioPK"]),
                                    HistoryPK = Convert.ToInt64(dr["newHistoryPK"]),
                                    Message = "Insert Fund Journal Header Success"
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundJournalScenario_Update(FundJournalScenario _FundJournalScenario, bool _havePrivillege)
        {
            int _newHisPK;
            int _historyPK;
            int _status;
            try
            {
                _historyPK = _host.Get_NewHistoryPK(_FundJournalScenario.FundJournalScenarioPK, "FundJournalScenario");
                _status = _host.Get_Status(_FundJournalScenario.FundJournalScenarioPK, _FundJournalScenario.HistoryPK, "FundJournalScenario");
            }
            catch (Exception err)
            {
                throw err;
            }
            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
            {
                DbCon.Open();
                SqlTransaction sqlT;
                sqlT = DbCon.BeginTransaction("FundJournalScenarioUpdate");
                try
                {
                    DateTime _datetimeNow = DateTime.Now;
                    if (_havePrivillege)
                    {

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update FundJournalScenario set Notes=@Notes,FundPK = @FundPK,Scenario = @Scenario," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FundJournalScenarioPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FundJournalScenario.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundJournalScenario.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FundJournalScenario.FundPK);
                            cmd.Parameters.AddWithValue("@Scenario", _FundJournalScenario.Scenario);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundJournalScenario.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundJournalScenario.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Approvedtime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update FundJournalScenario " +
                                "Set Notes = @Notes,Status = 3, " +
                                "VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate " +
                                "Where FundJournalScenarioPk = @PK and status = 4 ";
                            cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundJournalScenario.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundJournalScenario.Notes);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundJournalScenario.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        sqlT.Commit();
                        return 0;
                    }
                    else
                    {
                        if (_status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.Transaction = sqlT;
                                cmd.CommandText = "Update FundJournalScenario set Notes=@Notes,FundPK=@FundPK,Scenario=@Scenario, " +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where FundJournalScenarioPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _FundJournalScenario.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundJournalScenario.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FundJournalScenario.FundPK);
                                cmd.Parameters.AddWithValue("@Scenario", _FundJournalScenario.Scenario);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundJournalScenario.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            sqlT.Commit();
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.Transaction = sqlT;
                                _newHisPK = _host.Get_NewHistoryPK(_FundJournalScenario.FundJournalScenarioPK, "FundJournalScenario");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundJournalScenario where FundJournalScenarioPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundJournalScenario.HistoryPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundJournalScenario.FundPK);
                                cmd.Parameters.AddWithValue("@Scenario", _FundJournalScenario.Scenario);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundJournalScenario.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }


                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.Transaction = sqlT;
                                cmd.CommandText = "Update FundJournalScenario " +
                                    "Set Notes = @Notes,Status = 4, " +
                                    "LastUpdate=@lastupdate " +
                                    "Where FundJournalScenarioPk = @PK and HistoryPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundJournalScenario.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundJournalScenario.Notes);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            sqlT.Commit();
                            return _newHisPK;
                        }
                    }

                }
                catch (Exception err)
                {
                    sqlT.Rollback();
                    throw err;
                }
            }
        }


     

        //7
        public void FundJournalScenario_Approved(FundJournalScenario _FundJournalScenario)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournalScenario set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                        "where FundJournalScenarioPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundJournalScenario.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundJournalScenario.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundJournalScenario set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where FundJournalScenarioPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundJournalScenario.ApprovedUsersID);
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

        //8
        public void FundJournalScenario_Reject(FundJournalScenario _FundJournalScenario)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournalScenario set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                        "where FundJournalScenarioPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundJournalScenario.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundJournalScenario.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundJournalScenario set status= 2,LastUpdate=@lastUpdate where FundJournalScenarioPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
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

        //9
        public void FundJournalScenario_Void(FundJournalScenario _FundJournalScenario)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournalScenario set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                        "where FundJournalScenarioPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundJournalScenario.FundJournalScenarioPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundJournalScenario.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundJournalScenario.VoidUsersID);
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